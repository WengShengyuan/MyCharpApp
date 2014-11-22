using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data;
using System.Collections;


namespace CommonUtils
{
    class ODBCDB 
    {
        public string connectionString;
        public ODBCDB(string connStr)
        {
            connectionString = connStr;
        }
       

        #region 公用的一些函数

        /// BuildQueryCommand函数
        /// 构建 OleDbCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbCommand</returns>
        private OleDbCommand BuildQueryCommand(OleDbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            storedProcName = GetProcName(storedProcName, parameters);
            
            OleDbCommand command = new OleDbCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (OdbcParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        private string GetProcName(string storedProcName, IDataParameter[] parameters)
        {
            foreach (IDataParameter parameter in parameters)
            {
                storedProcName += " ?,";
            }

            if (!string.IsNullOrEmpty(storedProcName)&&storedProcName.EndsWith(","))
                storedProcName = storedProcName.Substring(0, storedProcName.Length - 1);

            return storedProcName;
        }

        /// <summary>
        /// 创建 OleDbCommand 对象实例(用来返回一个整数值)	
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>OleDbCommand 对象实例</returns>
        private OleDbCommand BuildIntCommand(OleDbConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            OleDbCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new OdbcParameter("ReturnValue",
                OdbcType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }

        private void PrepareCommand(OleDbCommand cmd, OleDbConnection conn, OleDbTransaction trans, string cmdText, OdbcParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (OdbcParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion


        #region SQL语句操作

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public object ExecuteScalar(string query)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                return cmd.ExecuteScalar();
            }
            catch (OdbcException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public int ExecuteNonQuery(string SQLString)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLString;
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (OdbcException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public int ExecuteSqlTran(ArrayList SQLStringList)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                OleDbTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return 1;//成功
                }
                catch (OdbcException E)
                {
                    tx.Rollback();
                    throw new Exception(E.Message);
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            //OleDbConnection connection = new OleDbConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                OleDbDataAdapter command = new OleDbDataAdapter(SQLString,connection);
                //OleDbDataAdapter command = new OleDbDataAdapter(SQLString, connection);
                command.Fill(ds, "ds");
                return ds;
            }
            catch (OleDbException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public DataSet Query(string SQLString, IDataParameter[] parameters)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                connection.Open();

                OleDbCommand cmd = BuildCommand(connection, SQLString, parameters);

                OleDbDataAdapter Adapter = new OleDbDataAdapter(cmd);

                Adapter.Fill(ds, "ds");

                return ds;
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        #endregion


        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回影响的行数		
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>返回值</returns>
        public int RunProcedure2(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            int result;
            connection.Open();
            try
            {
                OleDbCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                return result;
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcedure1(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            DataSet dataSet = new DataSet();
            connection.Open();
            try
            {
                OleDbCommand com = BuildQueryCommand(connection, storedProcName, parameters);

                OleDbDataAdapter sqlDA = new OleDbDataAdapter(com);

                sqlDA.Fill(dataSet, tableName);
                return dataSet;
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public object RunProcedure3(string storedProcName, IDataParameter[] parameters)
        {
            OleDbConnection con = new OleDbConnection(connectionString);

            DataTable dt = new DataTable();

            con.Open();
            try
            {
                OleDbCommand cmd = BuildQueryCommand(con, storedProcName, parameters);

                return cmd.ExecuteScalar();
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        private OleDbCommand BuildCommand(OleDbConnection connection, string SqlString, IDataParameter[] parameters)
        {
            OleDbCommand command = new OleDbCommand(SqlString, connection);

            command.CommandType = CommandType.Text;

            foreach (OdbcParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null||parameter.Value==string.Empty))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        public OleDbDataReader ExecuteReader(string SqlString, IDataParameter[] parameters)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            connection.Open();

            try
            {
                OleDbCommand cmd = BuildCommand(connection, SqlString, parameters);

                OleDbDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);

                return reader;
            }
            catch (OdbcException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public int ExecuteNonQuery(string SqlString, IDataParameter[] parameters)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            connection.Open();

            try
            {
                OleDbCommand cmd = BuildCommand(connection, SqlString, parameters);

                int rows = cmd.ExecuteNonQuery();

                return rows;
            }
            catch (OdbcException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        public void QuerySet(DataTable dt, string tableName)
        {
            string sql = "select * from " + tableName;

            OleDbConnection con = new OleDbConnection(connectionString);

            OleDbCommand cmd = con.CreateCommand();

            cmd.CommandType = CommandType.Text;

            cmd.CommandText = sql;

            try
            {
                OleDbDataAdapter adp = new OleDbDataAdapter(cmd);

                OleDbCommandBuilder bdr = new OleDbCommandBuilder(adp);

                adp.InsertCommand = bdr.GetInsertCommand();

                adp.UpdateCommand = bdr.GetUpdateCommand();

                adp.DeleteCommand = bdr.GetDeleteCommand();

                adp.Update(dt);
            }
            catch (OdbcException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable QueryTable(string sql)
        {
            OleDbConnection con = new OleDbConnection(connectionString);

            OleDbCommand cmd = new OleDbCommand(sql, con);

            cmd.CommandType = CommandType.Text;

            con.Open();

            try
            {
                OleDbDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleResult);

                DataTable dt = new DataTable();

                dt.Load(dr);

                dr.Dispose();

                dr.Close();

                return dt;
            }

            catch (OdbcException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        #endregion
    }
}
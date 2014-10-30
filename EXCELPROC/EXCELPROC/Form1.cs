using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataTable = System.Data.DataTable;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using Microsoft.Office.Interop.Excel;

using System.Reflection;


namespace EXCELPROC
{
    public partial class Form1 : Form
    {
        string f1;
        string f2;
        string connstr;
        
        string sqltext;
        OleDbConnection conn;
        OleDbDataAdapter odao;
        DataSet ds;
        DataSet outputds;
        DataTable dt;
        DataTable tempdt;
        public DataTable intempdt;
        public ArrayList selectedrow;
        public int skirow;
        
        int maxscore;
        public int sumrowcount;
        public int sortcol;
        public int topnumber;
        public Form1()
        {
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            f1 = FILEPATH1.Text;
            f2 = FILEPATH2.Text;
            connstr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + f1 + ";" + "Extended Properties=Excel 8.0";
            
            
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //第一个按钮按下的动作,前十名
            skirow = Convert.ToInt32(LINE1A.Text) -2;
            sortcol = Convert.ToInt32(COL1.Text) -1 ;
            topnumber = Convert.ToInt32(XVALUE.Text);
            string pagename = PAGE2.Text;
            tempdt = new DataTable();
            intempdt = new DataTable();
            outputds = new DataSet();
            selectedrow = new ArrayList();
            sumrowcount = 0;
            conn = new OleDbConnection(connstr);
            sqltext = "select * from ["+PAGE1.Text+"$]";
            conn.Open();
            odao = new OleDbDataAdapter(sqltext, conn);
            ds = new DataSet();
            odao.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
            dt = ds.Tables[0];
            intempdt = dt.Copy();
            intempdt.Rows.Clear();
            double themax=0.0;
            while (sumrowcount <= topnumber-1)
            {
                themax = gettempmax(dt);
                selectmaxrow(themax, dt);
                
            }
            outputds.Tables.Add(intempdt);
            //dataGridView1.DataSource = outputds.Tables[0];
            label11.Text = sumrowcount.ToString();
            openasnew(f2, pagename, outputds);
            //writetofile(f2, pagename, outputds);
            
            


        }

        private void writetofile(string fp, string pn, DataSet inds)
        {
            Microsoft.Office.Interop.Excel.Application m_objExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks m_objBooks = (Microsoft.Office.Interop.Excel.Workbooks)m_objExcel.Workbooks;
            m_objBooks.Open(fp, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            
            Microsoft.Office.Interop.Excel.Workbook m_objBook = m_objBooks.get_Item(1);
            Microsoft.Office.Interop.Excel.Sheets m_objSheets = (Microsoft.Office.Interop.Excel.Sheets)m_objBook.Worksheets;
            Microsoft.Office.Interop.Excel._Worksheet m_objSheet = (Microsoft.Office.Interop.Excel._Worksheet)(m_objSheets.get_Item(m_objSheets.Count+1));
            m_objSheet.Name = PAGE2.Text;

            m_objExcel.Visible = true; 
            

        }

        private void openasnew(string fp, string pn, DataSet inds)
        {
            //把计算出来的结果输出到新EXCEL中
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet worksheet;
                                       //是Excel可见
            workbook = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            worksheet = (Worksheet)workbook.Worksheets[1];
           if (inds.Tables[0].Rows.Count > 0)

            {

                for (int j = 0; j < inds.Tables[0].Rows.Count; j++)

                    for (int i = 0; i < inds.Tables[0].Columns.Count; i++)

                    {

                        worksheet.Cells[j + 1, i + 1] = inds.Tables[0].Rows[j][i].ToString();

                    }

            }
           excel.Visible = true; 

        }

        private bool canbeuse(int inrow)
        {
            bool canbeuse = true;
            foreach (int enui in selectedrow)
            {
                if (enui == inrow)
                {
                    
                    canbeuse = false;
                    break;
                }
                
            }
            return canbeuse;
            
        }

        private void selectmaxrow(double maxnumber, DataTable indt)
        {
             
           

            for (int i = 0; i < indt.Rows.Count; i++)
            {
                if (i >= skirow && canbeuse(i))
                {
                    
                    DataRow dr = indt.Rows[i];
                    string tttt = dr[sortcol].ToString();
                    if (tttt!="")
                    {
                        if (Convert.ToDouble(tttt) == maxnumber)
                        {
                            //intempdt.Rows.Add(indt.Rows[i].ItemArray);
                            intempdt.ImportRow(indt.Rows[i]);
                            selectedrow.Add(i);
                            //sumrowcount++;
                            //MessageBox.Show("max="+maxnumber.ToString()+",sumcount="+sumrowcount.ToString()+"intempdt.rowcount="+intempdt.Rows.Count.ToString());
                        }
                    }
                    

                }
                

            }
            
        }

        private double gettempmax(DataTable indt)
        {
            int i = 0;
            double tempmax = 0;
            double nowtemp;
            foreach (DataRow dr in indt.Rows)
            {
                //遍历每行
                if (i >= skirow && canbeuse(i) && (dr[sortcol].ToString()!=""))
                {
                    
                    nowtemp = Convert.ToDouble(dr[sortcol].ToString());
                    if (nowtemp > tempmax)
                    {//得到最大的分数
                        tempmax = nowtemp;
                    }

                }
                i++;
            }
            sumrowcount++;
            return tempmax;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            //第二个按钮按下的动作，后十名
            
        }

        private void getdata()
        {

        }







        private void button3_Click(object sender, EventArgs e)
        {
            //退出程序
            this.Close();
        }

        

        
    }
}

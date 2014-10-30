using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class NetWork
    {
        int inputNodeCount;
        int hiddenNodeCount;
        int outputNodeCount;
        DataTable TrainData;
        public int name;
        Dictionary<int, double[]> TrainDataDeNormalParm;
        Dictionary<int, double[]> TestDataDeNormalParm;
        double nrate;//学习速率
        public double defaulta;
        public double defaultb;
        double threadHold;
        public InputLayer ipl;
        public HiddenLayer hdl;
        public OutputLayer opl;
        public bool needtrain = true;
        bool Normalization;
        int train_Count = 0;
        double[] TempVar;
        Dictionary<int, double[]> Var;
        Dictionary<int, double[]> HisVar;
        
        public NetWork(int inc, int hnc, int onc, DataTable indt , double da, double db, double inrate, double inTH, bool inNormalization, bool autoTrain)
        {
            
            Var = new Dictionary<int, double[]>();
            this.inputNodeCount = inc;
            this.hiddenNodeCount = hnc;
            this.outputNodeCount = onc;
            this.TrainData = indt;
            
            this.defaulta = da;
            this.defaultb = db;
            this.nrate = inrate;
            this.threadHold = inTH;
            this.Normalization= inNormalization;
            if (Normalization)
            {
                this.TrainData = Normalize(this.TrainData,true);
                
            }
            ipl = new InputLayer(getixs(this.TrainData,0), inc, hnc, this, nrate);
            hdl = new HiddenLayer(hnc, onc,da, this, nrate);
            opl = new OutputLayer(onc, db, this);
            HisVar = new Dictionary<int, double[]>();
            TempVar = new double[TrainData.Rows.Count];
            if (autoTrain)
            {
                //this.trainNetWork();
                while (needtrain)
                {
                    this.averageTrain();
                }
            }
        }

        public NetWork(int inname, int inc, int hnc, int onc, DataTable indt, double da, double db, double inrate, double inTH, bool inNormalization, bool autoTrain)
        {
            this.name = inname;
            Var = new Dictionary<int, double[]>();
            this.inputNodeCount = inc;
            this.hiddenNodeCount = hnc;
            this.outputNodeCount = onc;
            this.TrainData = indt;

            this.defaulta = da;
            this.defaultb = db;
            this.nrate = inrate;
            this.threadHold = inTH;
            this.Normalization = inNormalization;
            if (Normalization)
            {
                this.TrainData = Normalize(this.TrainData, true);

            }
            ipl = new InputLayer(getixs(this.TrainData, 0), inc, hnc, this, nrate);
            hdl = new HiddenLayer(hnc, onc, da, this, nrate);
            opl = new OutputLayer(onc, db, this);
            HisVar = new Dictionary<int, double[]>();
            TempVar = new double[TrainData.Rows.Count];
            if (autoTrain)
            {
                //this.trainNetWork();
                
            }
        }

        public void trainToEnd()
        {
            while (needtrain)
            {
                this.averageTrain();
            }
 
        }


        public void averageTrain()
        {
            //用一个状态测试所有的训练集数据，一轮后，调整参数
            
                Var = new Dictionary<int, double[]>();
                for (int i = 0; i < this.TrainData.Rows.Count; i++)
                {
                    
                        this.ipl.InputData(dr2db(TrainData.Rows[i], TrainData.Columns.Count));
                        this.hdl.updateInputValue(ipl);
                        this.opl.updateInputValue(hdl);
                        //Adjust(this.getyys(this.TrainData, i), this.getoys(), i);
                        double[] thisVar = getek(getoys(), getyys(this.TrainData, i));
                        Var.Add(i, thisVar);

                }
                double[] aek = getAveVar(Var);
                Adjust(aek);
                
            
        }

        private double[] getAveVar(Dictionary<int, double[]> inVar)
        {
            int jCount = 0;
            double[] aveVD = new double[inVar[0].Length];
            foreach (KeyValuePair<int, double[]> vp in inVar)
            {
                jCount++;
                for (int i = 0; i < inVar[0].Length; i++)
                {
                    aveVD[i] += vp.Value[i];
                }
            }
            for (int i = 0; i < inVar[0].Length; i++)
            {
                aveVD[i] = aveVD[i]/jCount;
            }
            return aveVD;
        }





    #region 开放方法

        //传入测试集合，把输出集合加入在数据集后边
        public DataTable procTestData(DataTable intestData)
        {
            DataTable rdt=new DataTable();
            if(Normalization)
            {
                intestData = Normalize(intestData,false);
            }
            int oriCol = ipl._nodeCount + opl._nodeCount;
            int oriInputCol = ipl._nodeCount;
            for (int acc = 0; acc < oriCol; acc++)
            {
                //复制原始列
                rdt.Columns.Add(acc.ToString());
            }
            
            for (int acc = oriCol ; acc < (oriCol + opl._nodeCount); acc++)
            {
                //复制输出列
                rdt.Columns.Add(acc.ToString());
                if(Normalization)
                {
                    TestDataDeNormalParm.Add(acc , TestDataDeNormalParm[oriInputCol+acc-oriCol]);
                }
            }

            foreach (DataRow dr in intestData.Rows)
            {
                double[] tempixs = new double[ipl._nodeCount];
                for (int s = 0; s < ipl._nodeCount; s++)
                {
                    tempixs[s] =Convert.ToDouble(dr[s].ToString());
                }
                double[] tempoutput = calData(tempixs);

                DataRow adr = rdt.NewRow();
                for (int s = 0; s < intestData.Columns.Count; s++)
                {
                    adr[s] = dr[s];
                }
               
                for (int s = 0; s < opl._nodeCount; s++)
                {
                    adr[s + oriCol] = tempoutput[s];
                }
                rdt.Rows.Add(adr);
            }
            if(Normalization)
            {
                rdt = DeNorm(rdt, TestDataDeNormalParm);
            }
            return rdt;
            
        }
        
        //训练神经网络
        public void trainNetWork()
        {
            while (needtrain)
            {
                
                
                for (int i = 0; i < this.TrainData.Rows.Count; i++)
                {
                    
                    if (needtrain)
                    {
                        this.ipl.InputData(dr2db(TrainData.Rows[i], TrainData.Columns.Count));
                        this.hdl.updateInputValue(ipl);
                        this.opl.updateInputValue(hdl);
                        Adjust(this.getyys(this.TrainData, i), this.getoys(), i);
                        
                    }
                    
                }
                
            }
            
        }

    #endregion

    #region 私有方法

        private void inputData(double[] ixs)
        {
            for (int i = 0; i < ixs.Length; i++)
            {
                this.ipl._nodeList[i]._inputValue = ixs[i];
            }
            hdl.updateInputValue(this.ipl);
            opl.updateInputValue(this.hdl);
            
        }

        private void InittrainNetWork()
        {
            //train_Count++;
            for (int i = 1; i < this.TrainData.Rows.Count; i++)
            {
                if (needtrain)
                {
                    this.ipl.InputData(dr2db(TrainData.Rows[i], TrainData.Columns.Count));
                    this.hdl.updateInputValue(ipl);
                    this.opl.updateInputValue(hdl);
                    Adjust(this.getyys(this.TrainData, i), this.getoys(), i);
                }


            }

        }

        private void Adjust(double[] aek)
        {
            //修正系数，通过对应的输入与输出数组修改
            //double[] ek = getek(yk,ok);//输出值减去期望值
            double[] ek = aek;

            double sum = 0;
            for (int i = 0; i < ek.Length; i++)
            {
                sum += Math.Pow(ek[i], 2);
            }
            if (sum <= threadHold || train_Count > 1500)
            {
                needtrain = false;
                string tempVarstr = "";
                double[] tempV = new double[train_Count];
                for (int i = 0; i < train_Count; i++)
                {
                    tempV[i] = Math.Abs(HisVar[i][0]);
                    tempVarstr += tempV[i].ToString() + "\t";
                }

            }

            if (needtrain)
            {
                HisVar.Add(train_Count, ek);
                train_Count++;
                opl.updateNodeDiff(ek);
                hdl.updatewjk(ek);
                ipl.updatewij(this.hdl, ek);

                opl.updateb(ek);
                hdl.updatea(ek);
                //needAdjust(ek);
            }
        }

        private void Adjust(double[] yk, double [] ok, int index)
        {
            //修正系数，通过对应的输入与输出数组修改
            //double[] ek = getek(yk,ok);//输出值减去期望值
            double[] ek = getek(ok,yk);
            
            double sum = 0;
            for (int i = 0; i < ek.Length; i++)
            {
                sum += Math.Pow(ek[i], 2);
            }
            if (sum <= threadHold || train_Count > 1000 )
            {
                needtrain = false;
                string tempVarstr = "";
                double[] tempV = new double[train_Count];
                for (int i = 0; i < train_Count; i++)
                {
                    tempV[i] =Math.Abs(HisVar[i][0]);
                    tempVarstr += tempV[i].ToString() + "\t";
                }
                
            }

            if (needtrain)
            {

                HisVar.Add(train_Count, ek);
                train_Count++;
                opl.updateNodeDiff(ek);
                hdl.updatewjk(ek);
                ipl.updatewij(this.hdl, ek);

                opl.updateb(ek);
                hdl.updatea(ek);
                //needAdjust(ek);
            }
       }

        private void  needAdjust(double[] ek)
        {
            double sum = 0;
            for (int i = 0; i < ek.Length; i++)
            {
                sum += Math.Pow(ek[i], 2);
            }
            
        }

        private double[] getek(double[] yk, double[] ok)
        {
            double[] rek = new double[yk.Length];
            for (int i = 0; i < yk.Length; i++)
            {
                rek[i] = yk[i] - ok[i];
            }
            return rek;
        }

        private double[] dr2db(DataRow idr, int columN)
        {
            double[] rdb = new double[columN];
            for (int i = 0; i < columN; i++)
            {
                rdb[i] = Convert.ToDouble(idr[i].ToString());
            }
            return rdb;

            
        }

        private double[] getixs(DataTable indt, int rowIndex)
        {
            int ColumnCount = indt.Columns.Count - this.outputNodeCount;
            DataRow dr = indt.Rows[rowIndex];
            double[] rdouble = new double[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                rdouble[i] = Convert.ToDouble(dr[i].ToString().Trim());
            }
            return rdouble;
        }

        private double[] getyys(DataTable indt, int rowIndex)
        {
            //获取某个输入集合，对应的输出集合
            int ColumnCount = this.outputNodeCount;
            DataRow dr = indt.Rows[rowIndex];
            double[] rdouble = new double[ColumnCount];
            for (int i = 0; i < ColumnCount; i++)
            {
                rdouble[i] =Convert.ToDouble(dr[inputNodeCount + i].ToString().Trim());
            }
            return rdouble;
        }

        private DataTable Normalize(DataTable dt, bool isTrainData)
        {
            Dictionary<int, double[]> minMaxParm = new Dictionary<int, double[]>();

            for (int columnCount = 0; columnCount < dt.Columns.Count; columnCount++)
            {
                //遍历列

                double max = Convert.ToDouble(dt.Rows[0][columnCount]);
                double min = Convert.ToDouble(dt.Rows[0][columnCount]);
                for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
                {
                    //遍历行
                    if (Convert.ToDouble(dt.Rows[rowCount][columnCount].ToString()) > max)
                        max = Convert.ToDouble(dt.Rows[rowCount][columnCount].ToString());
                    if (Convert.ToDouble(dt.Rows[rowCount][columnCount].ToString()) < min)
                        min = Convert.ToDouble(dt.Rows[rowCount][columnCount].ToString());
                }
                for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
                {
                    //遍历行
                    dt.Rows[rowCount][columnCount] = ((Convert.ToDouble(dt.Rows[rowCount][columnCount].ToString()) - min) / (max - min)).ToString();
                    
                }
                minMaxParm.Add(columnCount, new double[] { min, max });
            }
            if (isTrainData)
            {
                TrainDataDeNormalParm = minMaxParm;
            }
            else
            {
                TestDataDeNormalParm = minMaxParm;
            }
            
            return dt;
        }

        private double[] calData(double[] ixs)
        {
            //根据输入数据，输出结果
            this.ipl.InputData(ixs);
            this.hdl.updateInputValue(ipl);
            this.opl.updateInputValue(hdl);
            double[] rd = new double[this.opl._nodeCount];
            foreach (KeyValuePair<int, OutputNode> opn in this.opl._nodeList)
            {
                rd[opn.Key] = opn.Value._inputValue;
            }

            return rd;
        }

        private double[] getoys()
        {
            //获取当前输出端的值
            double[] rdouble = new double[this.opl._nodeCount];
            for (int i = 0; i < this.opl._nodeCount; i++)
            {
                rdouble[i] = this.opl._nodeList[i]._inputValue;
            }
            return rdouble;

        }

        private DataTable DeNorm(DataTable indt, Dictionary<int, double[]> inDic)
        {
            for(int ColIndex =0;ColIndex<indt.Columns.Count;ColIndex++)
            {
                double tmin = inDic[ColIndex][0];
                double tmax = inDic[ColIndex][1];
                for (int RowIndex = 0; RowIndex < indt.Rows.Count; RowIndex++)
                {
                    indt.Rows[RowIndex][ColIndex] = Convert.ToDouble(indt.Rows[RowIndex][ColIndex].ToString()) * (tmax - tmin) + tmin;
                }
            }
            return indt;
        }

    #endregion


    }
}

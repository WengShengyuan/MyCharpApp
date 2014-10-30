using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BPANNDemo
{
    class GeneticNetWork
    {
        int maxNetWorks;//种群规模，不宜过大20~30
        int maxGenerations;
        public string generationVarStr;//每一代的误差
        double mutateRate;//变异概率 一般0.01~0.1
        double crossRate;//交叉概率 一般0.8~0.95
        DataTable trainData;
        DataTable testData;
        Dictionary<int, NetWork> networkContainer;//网络种群
        Dictionary<int, double[]> GeneContainer;//记录种群的基因信息
        Dictionary<int, double> populationVar;
        Dictionary<double, NetWork> BestPopulations;//保留下每代的最优网络，key:误差， value:网络
        
        //public static object locker = new object();//添加一个对象作为锁
        int geneLength;//遗传因子的长度：输入层权重(inc*hnc)+隐藏层权重(hnc*onc)+a,b(2);
        string logInfo="";
        
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innetworkCount">种群规模</param>
        /// <param name="ingenerationCount">最大进化次数</param>
        /// <param name="incrossRate">交叉率</param>
        /// <param name="inmutateRate">变异率</param>
        /// <param name="inc">输入节点数</param>
        /// <param name="hnc">隐藏节点数</param>
        /// <param name="onc">输出节点数</param>
        /// <param name="threadHold">误差阈值</param>
        /// <param name="intraindt">训练集</param>
        /// <param name="intestdt">测试集</param>
        /// <param name="da">隐藏层节点的初始a值</param>
        /// <param name="db">输出层节点的初始b值</param>
        /// <param name="inrate">学习速率</param>
        /// <param name="inNormalization">是否标准化</param>
        /// <param name="autoTrain">是否自动训练至最佳</param>
        /// <param name="useMultiThread">是否使用多线程</param>
        public GeneticNetWork(int innetworkCount, int ingenerationCount, double incrossRate, double inmutateRate, int inc, int hnc, int onc,double threadHold, DataTable intraindt,DataTable intestdt, double da, double db, double inrate,  bool inNormalization, bool autoTrain, bool useMultiThread)
        {
            
            populationVar = new Dictionary<int, double>();
            this.maxNetWorks = innetworkCount;
            this.maxGenerations = ingenerationCount;
            this.mutateRate = inmutateRate;
            this.crossRate = incrossRate;
            this.trainData = intraindt;
            this.testData = intestdt;
            Random ran =new Random();
            networkContainer = new Dictionary<int, NetWork>();
            BestPopulations = new Dictionary<double, NetWork>();
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //生成网络种群
            for (int genCount = 0; genCount < this.maxNetWorks; genCount++)
            {
                double gda = ran.NextDouble();
                double gdb = 0.0;
                while (true)
                {
                    gdb = ran.NextDouble();
                    if (gdb != gda)
                        break;
                }
                NetWork nw = new NetWork(genCount, inc, hnc, onc, trainData.Copy(), gda, gdb, 0.00001, threadHold, true, false);
                networkContainer.Add(genCount, nw);
            }

            
            this.geneLength = inc * hnc + hnc * onc + 2;
            getGeneContainer();//获取网络种群的基因信息
            if (!useMultiThread)
            {
                //不使用多线程
                networksInit();//启动种群训练
            }
            else
            {
                
                List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();
                logInfo += "\n [初始训练] \n";
                //使用多线程
                Task<NetWork>[] tasks = new Task<NetWork>[maxNetWorks];
                for (int i = 0; i < maxNetWorks; i++)
                {
                    ManualResetEvent mre = new ManualResetEvent(false);
                    manualEvents.Add(mre);
                    threadInputParm pra = new threadInputParm();
                    pra.mre = mre;
                    pra.networkIndex = i;
                    pra.nw = networkContainer[i];
                    ThreadPool.QueueUserWorkItem(MultiTrain, pra);
                }
                WaitHandle.WaitAll(manualEvents.ToArray());
            }
            
            for (int generationCount = 0; generationCount < maxGenerations; generationCount++)
            {
                int[] goods = Select(evaluateResults(testData.Copy(), inc, onc));
                populate(goods);//从现有基因容器中选择好的个体，杂交，变异生成新个体
                appliNewGene(GeneContainer, inc, hnc, onc, threadHold);//把基因容器中的新基因应用到网络中去

                if (!useMultiThread)
                {
                    //不使用多线程
                    networksInit();//启动种群训练

                }
                else
                {
                    
                    List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();
                    logInfo += "\n [进化] "+generationCount.ToString()+"\n";
                    //使用多线程
                    Task<NetWork>[] tasks = new Task<NetWork>[maxNetWorks];
                    for (int i = 0; i < maxNetWorks; i++)
                    {
                        ManualResetEvent mre = new ManualResetEvent(false);
                        manualEvents.Add(mre);
                        threadInputParm pra = new threadInputParm();
                        pra.mre = mre;
                        pra.networkIndex = i;
                        pra.nw = networkContainer[i];
                        ThreadPool.QueueUserWorkItem(MultiTrain, pra);
                    }
                    WaitHandle.WaitAll(manualEvents.ToArray());
                    
                        
                }

                //测试记录误差用的
                NetWork thisBest = getThisBest(testData.Copy(), inc, onc);
                DataTable thisResults = thisBest.procTestData(testData.Copy());
                double sumE = 0;
                for (int oCount = 0; oCount < onc; oCount++)
                {
                    foreach (DataRow dr in thisResults.Rows)
                    {
                        sumE += Math.Abs((Convert.ToDouble(dr[inc + onc + oCount]) - Convert.ToDouble(dr[inc + oCount])));
                    }
                }
                generationVarStr += "\t" + sumE.ToString();
                populationVar.Add(generationCount, sumE);
                try
                {
                    BestPopulations.Add(sumE, thisBest);
                }
                catch { }
            }
            sw.Stop();
            generationVarStr+="\tMultiThread:"+ useMultiThread.ToString()+ "->\tstopWatch:" +sw.ElapsedMilliseconds.ToString();
            //generationVarStr = logInfo;

        }

        private void MultiGenerate(object ipra)
        {
            threadInputParm pra = (threadInputParm)ipra;
            pra.mre.Set();
            NetWork nw = new NetWork(pra.genCount, pra.inc, pra.hnc, pra.onc, pra.trainData, pra.rana, pra.ranb, 0.00001, pra.threadHold, pra.normalization, pra.autotrain);
            nw.averageTrain();
            lock (networkContainer)
            {
                networkContainer.Add(pra.genCount, nw);
            }
            
        }
        
        private void MultiTrain(object ipra)
        {

            threadInputParm pra = (threadInputParm)ipra;

            //NetWork rnw = networkContainer[pra.networkIndex];
            NetWork rnw = pra.nw;
            //NetWork rnw = (NetWork)inw;
            if (rnw.name == 0)
            {
                logInfo += "b:dic-" + networkContainer[0].hdl._nodeList[0]._w[0].ToString() + ",b:this-" + rnw.hdl._nodeList[0]._w[0].ToString() + "\n";
            }
            rnw.trainToEnd();
            
            if (rnw.name == 0)
            {
                logInfo += "a:dic-" + networkContainer[0].hdl._nodeList[0]._w[0].ToString() + ",a:this-" + rnw.hdl._nodeList[0]._w[0].ToString() + "\n";
            }
            
            
           networkContainer[rnw.name] = rnw;
            
            
            pra.mre.Set();
            
         }

        private void networksInit()
        {
            //单线程，训练所有网络
            foreach (KeyValuePair<int, NetWork> np in networkContainer)
            {
                
                    np.Value.trainToEnd();
                
            }
        }




        public NetWork getBest()
        {
            double lowestR = 0;
            double lKey = 0;
            foreach (KeyValuePair<double, NetWork> nps in BestPopulations)
            {
                if ((1 / nps.Key) > lowestR)
                {
                    lowestR = (1 / nps.Key);
                    lKey = nps.Key;
                }
            }

            generationVarStr += "\n\tLowest Var:" + lKey.ToString();
            return BestPopulations[lKey];
        }

        private NetWork getThisBest(DataTable intestData, int inc, int onc)
        {
            //从最终种群中选择适应度最好的
            double[] rPosibilities = new double[maxNetWorks];
            double[] results = new double[maxNetWorks];
            //返回每个网络的误差平方和，作为适应度，越小越好
            for (int rCount = 0; rCount < maxNetWorks; rCount++)
            {
                double sumE = 0;
                DataTable tempR = networkContainer[rCount].procTestData(testData.Copy());
                for (int oCount = 0; oCount < onc; oCount++)
                {
                    foreach (DataRow dr in tempR.Rows)
                    {
                        sumE += Math.Pow((Convert.ToDouble(dr[inc + onc + oCount]) - Convert.ToDouble(dr[inc + oCount])), 2);
                    }
                }
                results[rCount] = sumE;
            }
            //返回选择概率
            double sumF = 0;
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                sumF += 1 / results[pC];
            }

            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                rPosibilities[pC] = (1 / results[pC]) / sumF;
            }
            double max = 0;
            int finalIndex=0;
            for (int chooseIndex = 0; chooseIndex < maxNetWorks; chooseIndex++)
            {
                if (max < rPosibilities[chooseIndex])
                    finalIndex = chooseIndex;
            }
            return networkContainer[finalIndex];
        }

        private void appliNewGene(Dictionary<int, double[]> ingenecontainer, int inc, int hnc, int onc, double threadHold)
        {
            //用新的基因组生成新的种群
            
            networkContainer = new Dictionary<int, NetWork>();
            //生成网络种群
            for (int genCount = 0; genCount < this.maxNetWorks; genCount++)
            {
                
                NetWork nw = new NetWork(genCount,inc, hnc, onc, trainData.Copy(), GeneContainer[genCount][geneLength - 2], GeneContainer[genCount][geneLength - 1], 0.00001, threadHold, true, false);
                networkContainer.Add(genCount, nw);
                if (genCount == 0)
                {
                    logInfo += "\n [新生成:] \n" + nw.hdl._nodeList[0]._w[0].ToString() + "\n";
                }
            }
            updatews(networkContainer, GeneContainer);
        }

        private void updatews(Dictionary<int, NetWork> inwc,Dictionary<int, double[]> igec )
        {
            //更新新种群的权重
            foreach (KeyValuePair<int, NetWork> nwp in inwc)
            {
                
                double[] ng = igec[nwp.Value.name];//新基因

                
                int index = 0;
                for (int i = 0; i < nwp.Value.ipl._nodeCount; i++)
                {
                    for (int j = 0; j < nwp.Value.ipl._nodeList[i]._w.Length; j++)
                    {
                        nwp.Value.ipl._nodeList[i]._w[j]=ng[index];
                        index++;
                    }
                }
                for (int i = 0; i < nwp.Value.hdl._nodeCount; i++)
                {
                    for (int j = 0; j < nwp.Value.hdl._nodeList[i]._w.Length; j++)
                    {
                        
                        nwp.Value.hdl._nodeList[i]._w[j]=ng[index];
                        if (nwp.Value.name == 0 && i == 0 && j == 0)
                        {
                            logInfo += "应用参数:" + nwp.Value.hdl._nodeList[0]._w[0].ToString();

                        }
                        index++;
                    }
                }
                nwp.Value.defaulta=ng[index];
                index++;
                nwp.Value.defaultb=ng[index];
                index++;
            }
        }
        

        private void populate(int[] ingoods)
        {
            
            //排除淘汰个体
            int maxSelect = 0;
            if (maxNetWorks % 2 != 0)
            {
                maxSelect = (maxNetWorks +1 ) / 2;
            }
            else
            {
                maxSelect = maxNetWorks / 2;
            }
            int[] badones = new int[maxSelect];
            int tempindex =0;
            for (int i = 0; i < maxNetWorks; i++)
            {
                bool isin = false;
                for (int j = 0; j < ingoods.Length; j++)
                {
                    if (i == ingoods[j])
                        isin = true;

                }
                if (!isin)
                {
                    badones[tempindex] = i;
                    tempindex++;
                }
            }
            for (int i = 0; i < badones.Length; i++)
            {
                networkContainer.Remove(badones[i]);
            }
            //杂交，变异，产生新个体
            CrossAndMutate(badones, ingoods);
        }

        private void CrossAndMutate(int[] inbads, int[] ingoods)
        {
            Random ran = new Random();
            for (int sI = 0; sI < (inbads.Length); sI++)
            {   
                int tempint1 = -1;
                int tempint2 = -1;
                
                while(true)
                {
                    int ci=chooseaGoodOne(ingoods, inbads);
                    if(ci>=0)
                    {tempint1=ci;break;}

                }
                while(true)
                {
                    int ci=chooseaGoodOne(ingoods, inbads);
                    if(ci>=0 && ci!=tempint1)
                    {tempint2=ci;break;}
                }
                
                double[] gene1 = GeneContainer[tempint1];
                double[] gene2 = GeneContainer[tempint2];
                
                if (ran.NextDouble() <= crossRate)
                {
                    //交叉
                    int jIndex = ran.Next(geneLength);
                    double b = ran.NextDouble();
                    gene1[jIndex] = gene1[jIndex] * (1 - b) + gene2[jIndex] * b;
                    gene2[jIndex] = gene2[jIndex] * (1 - b) + gene1[jIndex] * b;
                    GeneContainer.Remove(tempint1);
                    GeneContainer.Remove(tempint2);
                    if (tempint1 == 0)
                    { logInfo += "\ntempint1Mutate:bad"+sI.ToString(); }
                    if (tempint2 == 0)
                    { logInfo += "\ntempint2Mutate:bad"+sI.ToString(); }
                    GeneContainer.Add(tempint1, gene1);
                    try { GeneContainer.Add(tempint2, gene2); }
                    catch { }
                    
                }
                
                if(ran.NextDouble()<=mutateRate)
                {
                    //变异,产生新个体
                    int t=0;
                    while(true)
                    {
                        int ci=chooseaGoodOne(ingoods, inbads);
                        if(ci>=0)
                        {t=ci;break;}
                    }
                    if (t == 0)
                    { logInfo += "\ntMutate:bad" + sI.ToString(); }
                    double[] ngene = GeneContainer[t];
                    ngene[ran.Next(geneLength)]= ran.NextDouble();
                    GeneContainer.Remove(t);
                    GeneContainer.Add(t,ngene);
                }
            }
            
        }

        private int chooseaGoodOne(int[] gs, int[] bs)
        {
            Random ran = new Random();
            
            int ri = ran.Next(maxNetWorks);
            
            for(int j=0;j<gs.Length;j++)
            {
                if(gs[j]==ri)
                    return ri;
            }
            return -1;
 
        }

        /// <summary>
        /// 根据传入的选择概率数组选择优秀的个体
        /// </summary>
        /// <param name="infitness">概率数组</param>
        /// <returns>优秀个体编号数组</returns>
        private int[] Select(double[] infitness)
        {
            int[] goodones;//返回保留的个体序号
            //选择个体，原种群的一半，剩下的由交叉和变异产生
            int maxSelect=0;
            if(maxNetWorks%2!=0)
            {
                maxSelect = (maxNetWorks-1)/2;
            }
            else
            {
                maxSelect = maxNetWorks/2;
            }
            int selected = 0;
            goodones = new int[maxSelect];
            bool firstaddzero = true;
            while (selected < maxSelect)
            {
                    int ti = Ganble(infitness);
                    if (!(ti<0))
                    {
                        bool addable = true;
                        
                        for (int i = 0; i < maxSelect; i++)
                        {
                            if (ti != 0)
                            {
                                if (ti == goodones[i])
                                    addable = false;
                            }
                            
                        }
                        if (ti == 0)
                        {
                            if (!firstaddzero)
                            {
                                addable = false;
                            }
                        }
                        if (addable)
                        {
                            if (ti == 0)
                                firstaddzero = false;
                            goodones[selected] = ti;
                            selected++;
                        }
                        
                    }
            }
            return goodones;
        }

        /// <summary>
        /// 轮盘赌算法
        /// </summary>
        /// <param name="infitness">选择概率数组</param>
        /// <returns>个体编号</returns>
        private int Ganble(double[] infitness)
        {
            
            Random ran = new Random();
            double r = ran.NextDouble();
            if (r < infitness[0])
                return 0;
            for (int i = 1; i < infitness.Length; i++)
            {
                if (infitness[i-1] < r && infitness[i] > r)
                {
                    return i;
                }
            }
            return -1;
            
        }

        /// <summary>
        /// 返回当前代所有网络的误差计算出来的选择概率，用于后续轮盘赌
        /// </summary>
        /// <param name="intestData"></param>
        /// <param name="inc"></param>
        /// <param name="onc"></param>
        /// <returns></returns>
        private double[] evaluateResults(DataTable intestData,int inc, int onc)
        {
            //适应度累加数组，轮盘赌用的
            double[] rSumPosibilities = new double[maxNetWorks];
            double[] rPosibilities = new double[maxNetWorks];
            double[] results = new double[maxNetWorks];
            //返回每个网络的误差平方和，作为适应度，越小越好
            for (int rCount = 0; rCount < maxNetWorks; rCount++)
            {
                double sumE = 0;
                DataTable tempR = networkContainer[rCount].procTestData(testData.Copy());
                //ChartForm cf = new ChartForm(tempR, inc, onc);
                //cf.Show();
                for (int oCount = 0; oCount < onc; oCount++)
                {
                    foreach (DataRow dr in tempR.Rows)
                    {
                        sumE += Math.Abs((Convert.ToDouble(dr[inc + onc + oCount]) - Convert.ToDouble(dr[inc + oCount])));
                    }
                }
                results[rCount] = sumE;   
            }
            //返回选择概率
            double sumF=0;
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                sumF += 1 / results[pC];
            }
            
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                rPosibilities[pC] =(1/ results[pC]) / sumF;
            }
            //适应度累加
            double pSum=0;
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                pSum += rPosibilities[pC];
                rSumPosibilities[pC]=pSum;
            }
            return rSumPosibilities;
        }

        

        private void getGeneContainer()
        {
            
            GeneContainer = new Dictionary<int, double[]>();
            for (int geneCount = 0; geneCount < maxNetWorks; geneCount++)
            {
                double[] tempGene = getGene(networkContainer[geneCount]);
                GeneContainer.Add(geneCount, tempGene);
            }

        }
        private double[] getGene(NetWork inw)
        {
            
            double[] rg = new double[geneLength];
            int index = 0;
            for (int i = 0; i < inw.ipl._nodeCount;i++ )
            {
                for (int j = 0; j < inw.ipl._nodeList[i]._w.Length; j++)
                {
                    rg[index] = inw.ipl._nodeList[i]._w[j];
                    index++;
                }
            }
            for (int i = 0; i < inw.hdl._nodeCount; i++)
            {
                for (int j = 0; j < inw.hdl._nodeList[i]._w.Length; j++)
                {
                    if (j == 0 && i==0 && inw.name==0)
                    {
                        logInfo += "第一次生成:" + inw.hdl._nodeList[0]._w[0].ToString();
                    }
                    rg[index] = inw.hdl._nodeList[i]._w[j];
                    index++;
                }
            }
            rg[index] = inw.defaulta; 
            index++;
            rg[index] = inw.defaultb;
            index++;
            return rg;
        }
    }

    class threadInputParm
    {
        public ManualResetEvent mre;
        public int networkIndex;
        public NetWork nw;
        public int genCount;
        public int inc;
        public int hnc;
        public int onc;
        public DataTable trainData;
        public double rana;
        public double ranb;
        public double nrate;
        public double threadHold;
        public bool normalization;
        public bool autotrain;


    }
}



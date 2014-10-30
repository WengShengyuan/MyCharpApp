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
    class DEGenetic
    {

        #region 变量声明

        /**************基本遗传算法参数******************/
        Random ran = new Random();
        int maxNetWorks;//种群规模，不宜过大20~30
        int maxGenerations;
        public string generationVarStr;
        double mutateRate;//变异概率 一般0.01~0.1
        double crossRate;//交叉概率 一般0.8~0.95
        DataTable trainData;
        DataTable testData;
        Dictionary<int, NetWork> networkContainer;//网络种群
        Dictionary<int, double[]> GeneContainer;//记录种群的基因信息
        Dictionary<int, double> populationVar;
        Dictionary<double, NetWork> BestPopulations;//保留下每代的最优网络，key:误差， value:网络
        int geneLength;//遗传因子的长度：输入层权重(inc*hnc)+隐藏层权重(hnc*onc)+a,b(2);
        string logInfo="";
        int inc, hnc, onc;
        double threadHold;
        
        /**************进化算法参数******************/
        double F1;
        double F2;
        int G1;
        int G2;
        double th1;
        double th2;
        int newChangeCount = 0;
        bool changebeforeG2 = false;
        bool bestchange = false;
        int allGenerationMark = -1;
        NetWork lastBest = null;
        NetWork thisBest = null;
        int lastBestName = -1;
        int thisBestName = -1;

        #endregion

        #region 构造、初始化

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
        /// <param name="F1">F1</param>
        /// <param name="F2">F2</param>
        /// <param name="G1">G1</param>
        /// <param name="G2">G2</param>
        /// <param name="th1">th1</param>
        /// <param name="th2">th2</param>
        public DEGenetic(int innetworkCount, int ingenerationCount, double incrossRate, double inmutateRate, int inc, int hnc, int onc, double threadHold, DataTable intraindt, DataTable intestdt, double da, double db, double inrate, bool inNormalization, bool autoTrain, bool useMultiThread,double F1, double F2, int G1, int G2, double th1, double th2)
        {
            populationVar = new Dictionary<int, double>();
            this.maxNetWorks = innetworkCount;
            this.maxGenerations = ingenerationCount;
            this.mutateRate = inmutateRate;
            this.crossRate = incrossRate;
            this.trainData = intraindt;
            this.testData = intestdt;
            this.inc = inc;
            this.hnc = hnc;
            this.onc = onc;
            this.threadHold = threadHold;
            networkContainer = new Dictionary<int, NetWork>();
            BestPopulations = new Dictionary<double, NetWork>();
            this.F1 = F1;
            this.F2 = F2;
            this.G1 = G1;
            this.G2 = G2;
            this.th1=th1;
            this.th2=th2;


            Stopwatch sw = new Stopwatch();
            sw.Start();
            majorControl();
            //生成网络种群
            sw.Stop();
            generationVarStr +="\tstopWatch:" + sw.ElapsedMilliseconds.ToString();

        }

        #endregion

        #region 主方法


        /// <summary>
        /// 主控中心 
        /// </summary>
        private void majorControl()
        {
            //1、产生第一代种群
            //2、遍历，选择goodones，badones， 从badones中选取个体变异
                //1）G1代以前用混合变异算子-1
                //2）G1代以后开始变异计数，看最好个体是否在连续G2代以内变异，采用不同算子-2,3
            //3、每次变异后纠正一次系数
            genNewGeneration();

            for (int gennerationCount = 0; gennerationCount < maxGenerations; gennerationCount++)
            {
                allGenerationMark = gennerationCount;
                appliNewGene(GeneContainer, inc, hnc, onc, threadHold);
                singleThreadTrain();//种群训练

                int[] goods = Select(evaluateResults(testData.Copy(), inc, onc));//选择优良个体
                thisBest = getThisBest(trainData.Copy(), inc, onc);
                thisBestName = thisBest.name;

                #region 记录误差用的
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
                populationVar.Add(gennerationCount, sumE);
                try
                {
                    BestPopulations.Add(sumE, thisBest);
                }
                catch { }
                #endregion

                if (lastBestName < 0 || bestchange)
                {
                    lastBest = thisBest;
                    lastBestName = thisBestName;
                }

                if (gennerationCount < G1)
                {
                    //G1用算子1
                    CalMethod1(goods);//杂交
                    appliNewGene(GeneContainer, inc, hnc, onc, threadHold);//应用新基因
                }
                else
                {
                    //大于G1代了
                    if (!changebeforeG2 && newChangeCount < G2)
                    {
                        //如果连续G2代没有变异
                        CalMethod2(goods);
                        appliNewGene(GeneContainer, inc, hnc, onc, threadHold);//应用新基因
                        newChangeCount++;
                    }
                    else
                    {
                        //如果变异了
                        changebeforeG2 = false;
                        newChangeCount = 0;
                        CalMethod3(goods);
                        appliNewGene(GeneContainer, inc, hnc, onc, threadHold);//应用新基因
                        newChangeCount++;
                    }
                }
            }
        }


        /// <summary>
        /// 单线程训练所有网络
        /// </summary>
        private void singleThreadTrain()
        {
            foreach (KeyValuePair<int, NetWork> np in networkContainer)
            {
                np.Value.trainToEnd();
            }
        }


        /// <summary>
        /// 产生初始种群
        /// </summary>
        private void genNewGeneration()
        {
            ran = new Random();
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
        }



        /// <summary>
        /// 混合算子-1
        /// </summary>
        private void CalMethod1(int[] inGoods)
        {
            int[] badones = removeBadss(inGoods);
            int badCount = badones.Length;
            int r1 = 0;
            int r2 = 0;
            int r3 = 0;
            for (int tCount = 0; tCount < badCount; tCount++)
            {
                int tIndex = badones[tCount];
                //遍历每个坏的个体，逐一生成
                while(true)
                {
                    r1 = ran.Next(badCount);
                    if (r1 != tIndex)
                        break;
                }
                
                while (true)
                {
                    r2 = ran.Next(badCount);
                    if ((r2 != r1) && (r2 != tIndex))
                        break;
                }
                while (true)
                {
                    r3 = ran.Next(badCount);
                    if ((r3 != r2) && (r3 != r1) && (r3 != tIndex))
                        break;
                }
                //选取不同于t的r1,r2,r3
                r1 = badones[r1]; r2 = badones[r2]; r3 = badones[r3];
                double[] gr1=GeneContainer[r1];
                double[] gr2=GeneContainer[r2];
                double[] gr3=GeneContainer[r3];
                double[] newt = new double[geneLength];
                for (int gi = 0; gi < geneLength; gi++)
                {
                    newt[gi] = gr1[gi] + F2 * (gr2[gi] - gr3[gi]);
                }

                GeneContainer[tIndex] = correctGene(newt); 
            }
            

        }

        /// <summary>
        /// 混合算子-2
        /// </summary>
        private void CalMethod2(int[] inGoods)
        {
            int[] badones = removeBadss(inGoods);
            int badCount = badones.Length;
            NetWork bestnetwork = getThisBest(trainData, inc, onc);
            double[] bestGene = GeneContainer[thisBestName];
            for (int tCount = 0; tCount < badCount; tCount++)
            {
                //遍历每个坏的个体，逐一生成
                int ChooseFlag = ran.NextDouble() > 0.5 ? 1 : 0;
                int tIndex = badones[tCount];
                double[] newt = new double[geneLength];
                switch (ChooseFlag)
                {
                    case 1:
                        {
                            for (int gi = 0; gi < geneLength; gi++)
                            {
                                newt[gi] = GeneContainer[thisBestName][gi] + (1 / th1);
                            }
                            break;
                        }
                    case 0:
                        {
                            for (int gi = 0; gi < geneLength; gi++)
                            {
                                newt[gi] = GeneContainer[thisBestName][gi] + (1 / th2);
                            }
                            break;
                        }
                    default: break;
                }
                GeneContainer[tIndex] = correctGene(newt); 
                
            }
        }

        /// <summary>
        /// 混合算子-3
        /// </summary>
        private void CalMethod3(int[] inGoods)
        {
            int r1 = 0;
            int r2 = 0;
            int r3 = 0;
            int[] badones = removeBadss(inGoods);
            int badCount = badones.Length;
            NetWork bestnetwork = getThisBest(trainData, inc, onc);
            double[] bestGene = GeneContainer[thisBestName];
            for (int tCount = 0; tCount < badCount; tCount++)
            {
                int tIndex = badones[tCount];
                //遍历每个坏的个体，逐一生成
                while (true)
                {
                    r1 = ran.Next(badCount);
                    if (r1 != tIndex)
                        break;
                }

                while (true)
                {
                    r2 = ran.Next(badCount);
                    if ((r2 != r1) && (r2 != tIndex))
                        break;
                }
                while (true)
                {
                    r3 = ran.Next(badCount);
                    if ((r3 != r2) && (r3 != r1) && (r3 != tIndex))
                        break;
                }
                //选取不同于t的r1,r2,r3
                r1 = badones[r1]; r2 = badones[r2]; r3 = badones[r3];
                double[] gr1 = GeneContainer[r1];
                double[] gr2 = GeneContainer[r2];
                double[] gr3 = GeneContainer[r3];
                double[] newt = new double[geneLength];
                F1 = F2;//书上写f1 = f2
                for (int gi = 0; gi < geneLength; gi++)
                {
                    newt[gi] = gr1[gi] + F1 * (bestGene[gi] - gr1[gi]) + F2 * (gr2[gi] - gr3[gi]);
                }
                GeneContainer[tIndex] = correctGene(newt); 
            }
        }

        #endregion

        #region 次级方法

        /// <summary>
        /// 修正基因
        /// </summary>
        /// <param name="inGene">输入的基因</param>
        /// <returns>修正后的基因</returns>
        private double[] correctGene(double[] inGene)
        {
            for (int gi = 0; gi < geneLength; gi++)
            {
                if (inGene[gi] < 0 || inGene[gi] > 1)
                {
                    inGene[gi] = ran.NextDouble();
                }
            }
            return inGene;

        }


        /// <summary>
        /// 该代种群所有的遗传因子，并保存在字典中
        /// </summary>
        private void getGeneContainer()
        {

            GeneContainer = new Dictionary<int, double[]>();
            for (int geneCount = 0; geneCount < maxNetWorks; geneCount++)
            {
                double[] tempGene = getGene(networkContainer[geneCount]);
                GeneContainer.Add(geneCount, tempGene);
            }

        }

        /// <summary>
        /// 获取输入网络的遗传因子
        /// </summary>
        /// <param name="inw">输入的神经网络个体</param>
        /// <returns>遗传因子数组</returns>
        private double[] getGene(NetWork inw)
        {

            double[] rg = new double[geneLength];
            int index = 0;
            for (int i = 0; i < inw.ipl._nodeCount; i++)
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
                    if (j == 0 && i == 0 && inw.name == 0)
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

        /// <summary>
        /// 返回当前代所有网络的误差计算出来的选择概率，用于后续轮盘赌
        /// </summary>
        /// <param name="intestData"></param>
        /// <param name="inc"></param>
        /// <param name="onc"></param>
        /// <returns></returns>
        private double[] evaluateResults(DataTable intestData, int inc, int onc)
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
            double sumF = 0;
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                sumF += 1 / results[pC];
            }

            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                rPosibilities[pC] = (1 / results[pC]) / sumF;
            }
            //适应度累加
            double pSum = 0;
            for (int pC = 0; pC < maxNetWorks; pC++)
            {
                pSum += rPosibilities[pC];
                rSumPosibilities[pC] = pSum;
            }
            return rSumPosibilities;
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
            int maxSelect = 0;
            if (maxNetWorks % 2 != 0)
            {
                maxSelect = (maxNetWorks - 1) / 2;
            }
            else
            {
                maxSelect = maxNetWorks / 2;
            }
            int selected = 0;
            goodones = new int[maxSelect];
            bool firstaddzero = true;
            while (selected < maxSelect)
            {
                int ti = Ganble(infitness);
                if (!(ti < 0))
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
                if (infitness[i - 1] < r && infitness[i] > r)
                {
                    return i;
                }
            }
            return -1;

        }

        /// <summary>
        /// 根据选择的优良个体序号，生成淘汰个体序号，并从种群中移除个体
        /// </summary>
        /// <param name="inGoods">优良个体序号数组</param>
        /// <returns>淘汰个体序号数组</returns>
        private int[] removeBadss(int[] ingoods)
        {
            //排除淘汰个体
            int maxSelect = 0;
            if (maxNetWorks % 2 != 0)
            {
                maxSelect = (maxNetWorks + 1) / 2;
            }
            else
            {
                maxSelect = maxNetWorks / 2;
            }
            int[] badones = new int[maxSelect];
            int tempindex = 0;
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
                    if (i == lastBestName && allGenerationMark > G1)
                    {
                        changebeforeG2 = true;
                    }
                    if (i == lastBestName)
                    {
                        bestchange = true;
                    }
                }
            }
            //for (int i = 0; i < badones.Length; i++)
            //{
            //    networkContainer.Remove(badones[i]);
            //}
            return badones;
 
        }

        /// <summary>
        /// 根据新的geneContainer生成新的种群
        /// </summary>
        /// <param name="ingenecontainer">geneContainer</param>
        /// <param name="inc">输入节点数</param>
        /// <param name="hnc">隐藏节点数</param>
        /// <param name="onc">输出</param>
        /// <param name="threadHold">误差门限</param>
        private void appliNewGene(Dictionary<int, double[]> ingenecontainer, int inc, int hnc, int onc, double threadHold)
        {
            //用新的基因组生成新的种群
            networkContainer = new Dictionary<int, NetWork>();
            //生成网络种群
            for (int genCount = 0; genCount < this.maxNetWorks; genCount++)
            {

                NetWork nw = new NetWork(genCount, inc, hnc, onc, trainData.Copy(), GeneContainer[genCount][geneLength - 2], GeneContainer[genCount][geneLength - 1], 0.00001, threadHold, true, false);
                networkContainer.Add(genCount, nw);
                if (genCount == 0)
                {
                    logInfo += "\n [新生成:] \n" + nw.hdl._nodeList[0]._w[0].ToString() + "\n";
                }
            }
            updatews(networkContainer, GeneContainer);
        }
        /// <summary>
        /// 将geneContainer中的信息赋值给种群
        /// </summary>
        /// <param name="inwc">目标种群</param>
        /// <param name="igec">geneContainer</param>
        private void updatews(Dictionary<int, NetWork> inwc, Dictionary<int, double[]> igec)
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
                        nwp.Value.ipl._nodeList[i]._w[j] = ng[index];
                        index++;
                    }
                }
                for (int i = 0; i < nwp.Value.hdl._nodeCount; i++)
                {
                    for (int j = 0; j < nwp.Value.hdl._nodeList[i]._w.Length; j++)
                    {

                        nwp.Value.hdl._nodeList[i]._w[j] = ng[index];
                        if (nwp.Value.name == 0 && i == 0 && j == 0)
                        {
                            logInfo += "应用参数:" + nwp.Value.hdl._nodeList[0]._w[0].ToString();

                        }
                        index++;
                    }
                }
                nwp.Value.defaulta = ng[index];
                index++;
                nwp.Value.defaultb = ng[index];
                index++;
            }
        }

        /// <summary>
        /// 获取当前代的最优个体
        /// </summary>
        /// <param name="intestData">训练集</param>
        /// <param name="inc">输入层节点数</param>
        /// <param name="onc">输出层节点数</param>
        /// <returns></returns>
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
            int finalIndex = 0;
            for (int chooseIndex = 0; chooseIndex < maxNetWorks; chooseIndex++)
            {
                if (max < rPosibilities[chooseIndex])
                    finalIndex = chooseIndex;
            }
            return networkContainer[finalIndex];
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

        #endregion

    }
}

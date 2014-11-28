using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BPANNDemo
{
    public partial class Form1 : Form
    {
        int inc;
        int hnc;
        int onc;
        double threadHold;
        NetWork nw;
        
        public ChartForm cf;
        DataTable traindata;
        DataTable testdata;
        DataTable outputdata;
        Random ran;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //测试
            outputdata = nw.procTestData(testdata.Copy());
            testDataGrid.DataSource = outputdata;
            cf = new ChartForm(outputdata,inc,onc);
            cf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //读取
            string fP = textBox1.Text;
            traindata = getData(".\\"+ fP+"\\train.txt");
            testdata = getData(".\\" + fP + "\\test.txt");
            
            
            trainDataGrid.DataSource = traindata;

            
        }

        



        private DataTable getData(string fPath)
        {
            DataTable tempdt = new DataTable();
            //FileStream fs = new FileStream();
            //StreamReader sr = new StreamReader();
            try
            {
                FileStream fs = new FileStream(fPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string str = sr.ReadLine().Trim();
                string[] indatas = str.Split('\t');
                int dataCol = indatas.Length;

                for (int i = 0; i < dataCol; i++)
                {
                    tempdt.Columns.Add(i.ToString());

                }
                DataRow dr = tempdt.NewRow();
                for (int i = 0; i < dataCol; i++)
                {
                    dr[i] = indatas[i].ToString().Trim();

                }
                tempdt.Rows.Add(dr);

                while ((str = sr.ReadLine().Trim()) != null)
                {
                    indatas = str.Split('\t');
                    dataCol = indatas.Length;
                    dr = tempdt.NewRow();
                    for (int i = 0; i < dataCol; i++)
                    {
                        dr[i] = indatas[i].ToString().Trim();
                    }
                    tempdt.Rows.Add(dr);
                }
                sr.Dispose();
                fs.Dispose();
            }
            catch 
            {
                //MessageBox.Show(ex.ToString());
                //if (sr != null)
                //    sr.Dispose();
                //if (fs != null)
                //    fs.Dispose();
            }
            finally
            {
                //if (sr != null)
                //    sr.Dispose();
                //if (fs != null)
                //    fs.Dispose();
            }
            return tempdt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            label3.Text = "网络结构：" + inc.ToString() + "--" + hnc.ToString() + "--" + onc.ToString();
            ran = new Random();
            nw = new NetWork(inc, hnc, onc, traindata.Copy(),  ran.NextDouble(), ran.NextDouble(), 0.00001,threadHold,true,true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //并行训练，
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            label3.Text = "网络结构：" + inc.ToString() + "--" + hnc.ToString() + "--" + onc.ToString();
            ran = new Random();
            int j = 0;
            DataTable rtestdata = testdata.Copy();
            for (int i = 0; i < onc; i++)
            {
                rtestdata.Columns.Add((i + inc + onc).ToString());
            }
                rtestdata.Rows.Clear();
            foreach (DataRow dr in traindata.Rows)
            {
                DataTable tempdt = traindata.Copy();
                DataTable temptestdata = testdata.Copy();
                temptestdata.Rows.Clear();
                temptestdata.ImportRow(testdata.Rows[j]);
                j++;

                tempdt.Rows.Clear();
                tempdt.ImportRow(dr);
                NetWork tempnw = new NetWork(inc, hnc, onc, tempdt, ran.NextDouble(), ran.NextDouble(), 0.00001, threadHold, false, true);
                DataTable tempanswerdt = tempnw.procTestData(temptestdata);
                rtestdata.ImportRow(tempanswerdt.Rows[0]);
            }
            testDataGrid.DataSource = rtestdata;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //进化演示
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            label3.Text = "网络结构：" + inc.ToString() + "--" + hnc.ToString() + "--" + onc.ToString();
            Dictionary<int, DataTable> testdataList = new Dictionary<int,DataTable>();
            int j=0;
            ran = new Random();
            //nw = new NetWork(inc, hnc, onc, traindata.Copy(), 0.5, 0.5, 0.001, threadHold, true, false);
            nw = new NetWork(inc, hnc, onc, traindata.Copy(), ran.NextDouble(), ran.NextDouble(), 0.001, threadHold, true, false);
            while (nw.needtrain)
            {
                DataTable tempdt = nw.procTestData(testdata.Copy());
                
                testdataList.Add(j, tempdt);

                j++;
                nw.averageTrain();
            }
            cf = new ChartForm(testdataList, inc, onc);
            cf.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            ran = new Random();
            int maxPop = Convert.ToInt32(TB_population.Text);
            int maxGen = Convert.ToInt32(TB_generation.Text);
            double crossR = Convert.ToDouble(tb_CrossRate.Text);
            double mutateR = Convert.ToDouble(TB_MutaRate.Text);
            GeneticNetWork GN = new GeneticNetWork(maxPop, maxGen, crossR, mutateR, inc, hnc, onc, threadHold, traindata.Copy(), testdata.Copy(), ran.NextDouble(), ran.NextDouble(), 0.00001, true, false,checkBox1.Checked);
            NetWork bestnw = GN.getBest();
            DataTable result = bestnw.procTestData(testdata.Copy());
            testDataGrid.DataSource = result;
            cf = new ChartForm(result,inc,onc);
            richTextBox1.Text = GN.generationVarStr;
            cf.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //正交差分计算
            ran = new Random();
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            int maxPoP = Convert.ToInt32(DE_maxNetwork.Text);
            int maxGen = Convert.ToInt32(DE_maxGeneration.Text);
            double crossR = Convert.ToDouble(DE_crossRate.Text);
            double mutateR = Convert.ToDouble(DE_mutateRate.Text);
            int G1 = Convert.ToInt32(G1V.Text);
            int G2 = Convert.ToInt32(G2V.Text);
            double F1 = Convert.ToDouble(F1V.Text);
            double F2 = Convert.ToDouble(F2V.Text);
            double th1 = Convert.ToDouble(th1V.Text);
            double th2 = Convert.ToDouble(th2V.Text);
            DEGenetic DEG = new DEGenetic(maxPoP, maxGen, crossR, mutateR, inc, hnc, onc, threadHold, traindata.Copy(), testdata.Copy(), ran.NextDouble(), ran.NextDouble(), 0.00001, true, false, false, F1, F2, G1, G2, th1, th2);
            NetWork bestnw = DEG.getBest();
            DataTable result = bestnw.procTestData(testdata.Copy());
            testDataGrid.DataSource = result;
            cf = new ChartForm(result, inc, onc);
            richTextBox2.Text = DEG.generationVarStr;
            cf.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ran = new Random();
            inc = Convert.ToInt32(textBox3.Text);
            hnc = Convert.ToInt32(textBox2.Text);
            onc = traindata.Columns.Count - inc;
            threadHold = Convert.ToDouble(textBox4.Text);
            int maxPoP = Convert.ToInt32(DE_maxNetwork.Text);
            int maxGen = Convert.ToInt32(DE_maxGeneration.Text);
            double crossR = Convert.ToDouble(DE_crossRate.Text);
            double mutateR = Convert.ToDouble(DE_mutateRate.Text);
            int G1 = Convert.ToInt32(G1V.Text);
            int G2 = Convert.ToInt32(G2V.Text);
            double F1 = Convert.ToDouble(F1V.Text);
            double F2 = Convert.ToDouble(F2V.Text);
            double th1 = Convert.ToDouble(th1V.Text);
            double th2 = Convert.ToDouble(th2V.Text);
            StandardDEGenetic SDEG = new StandardDEGenetic(maxPoP, maxGen, crossR, mutateR, inc, hnc, onc, threadHold, traindata.Copy(), testdata.Copy(), ran.NextDouble(), ran.NextDouble(), 0.00001, true, false, false, F1, F2, G1, G2, th1, th2);
            NetWork bestnw = SDEG.getBest();
            DataTable result = bestnw.procTestData(testdata.Copy());
            testDataGrid.DataSource = result;
            cf = new ChartForm(result, inc, onc);
            richTextBox2.Text = SDEG.generationVarStr;
            cf.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //保存结果
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string savePath = sfd.FileName;
                CsvHelper.dt2csv((DataTable)testDataGrid.DataSource, savePath, "计算结果", getColumns(((DataTable)testDataGrid.DataSource).Columns));
            }
        }

        private string getColumns(DataColumnCollection icc)
        {
            string rstr = "";
            foreach (var v in icc)
            {
                rstr += v+",";
            }
            return rstr.Substring(0, rstr.Length - 1);
        }
    }
}

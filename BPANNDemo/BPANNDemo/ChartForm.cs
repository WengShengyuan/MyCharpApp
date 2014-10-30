using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace BPANNDemo
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
        }
        
        Dictionary<int, DataTable> testdataList;
        DataTable data;
        DataTable sourcedt;
        int resultId;
        int resultLength;
        int dataIndex = 0;
        public ChartForm(DataTable indt, int Rid, int Length)
        {
            InitializeComponent();
            button1.Enabled = false;
            button1.Visible = false;
            button2.Enabled = false;
            button2.Visible = false;
            this.data = indt;
            this.resultId = Rid;
            this.resultLength = Length;
            sourcedt=new DataTable();
            sourcedt.Columns.Add("id");
            for (int i = 0; i < Length; i++)
            {
                sourcedt.Columns.Add("ori:" + i.ToString());
            }
            for (int i = 0; i < Length; i++)
            {
                sourcedt.Columns.Add("fore:" + i.ToString());
            }
            int rCount = 0;
            foreach (DataRow dr in data.Rows)
            {
                rCount++;
                DataRow ir = sourcedt.NewRow();
                ir["id"] = rCount;
                for (int i = 0; i < Length; i++)
                {
                    ir["ori:" + i.ToString()] = dr[Rid];
                    ir["fore:" + i.ToString()] = dr[Rid + Length];
                }
                sourcedt.Rows.Add(ir);
            }
            drawGraph(sourcedt, Rid, Length);


        }
        public ChartForm(Dictionary<int, DataTable> testList, int Rid, int Length)
        {
            InitializeComponent();
            button1.Enabled = true;
            button1.Visible = true;
            button2.Enabled = true;
            button2.Visible = true;
            this.testdataList = testList;
            this.resultId = Rid;
            this.resultLength = Length;
            sourcedt = new DataTable();
            sourcedt.Columns.Add("id");
            for (int i = 0; i < resultLength; i++)
            {
                sourcedt.Columns.Add("ori:" + i.ToString());
            }
            for (int i = 0; i < resultLength; i++)
            {
                sourcedt.Columns.Add("fore:" + i.ToString());
            }
            this.data = testdataList[dataIndex];
            int rCount = 0;

            foreach (DataRow dr in data.Rows)
            {
                rCount++;
                DataRow ir = sourcedt.NewRow();
                ir["id"] = rCount;
                for (int i = 0; i < resultLength; i++)
                {
                    ir["ori:" + i.ToString()] = dr[resultId];
                    ir["fore:" + i.ToString()] = dr[resultId + resultLength];
                }
                sourcedt.Rows.Add(ir);
            }
            drawGraph(sourcedt, resultId, resultLength);

            
        }
        private void drawGraph(DataTable sdt,int Rid, int Length)
        {
            
            int rowC = sdt.Rows.Count;
            chart1.DataSource = sdt;
            //chart1.ChartAreas.Add("View");
            chart1.Series.Add("id");

            
            chart1.Series["id"].XValueMember="id";
            for (int i = 0; i < Length; i++)
            {
                chart1.Series.Add("ori:" + i.ToString());
                chart1.Series.Add("fore:" + i.ToString());
                chart1.Series[i+1].YValueMembers = "ori:" + i.ToString();
                chart1.Series[i + 1].BorderWidth = 3;
                chart1.Series[i + 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[i+1+Length].YValueMembers = "fore:" + i.ToString();
                chart1.Series[i + 1 + Length].BorderWidth = 1;
                chart1.Series[i + 1 + Length].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
            
            chart1.DataBind();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //进化
            try
            {
                dataIndex++;
                sourcedt = new DataTable();
                sourcedt.Columns.Add("id");
                for (int i = 0; i < resultLength; i++)
                {
                    sourcedt.Columns.Add("ori:" + i.ToString());
                }
                for (int i = 0; i < resultLength; i++)
                {
                    sourcedt.Columns.Add("fore:" + i.ToString());
                }
                this.data = testdataList[dataIndex];
                int rCount = 0;

                foreach (DataRow dr in data.Rows)
                {
                    rCount++;
                    DataRow ir = sourcedt.NewRow();
                    ir["id"] = rCount;
                    for (int i = 0; i < resultLength; i++)
                    {
                        ir["ori:" + i.ToString()] = dr[resultId];
                        ir["fore:" + i.ToString()] = dr[resultId + resultLength];
                    }
                    sourcedt.Rows.Add(ir);
                }
                chart1.DataSource = sourcedt;
                //chart1.Series["id"].XValueMember = "id";
                //for (int i = 0; i < resultLength; i++)
                //{
                //    //chart1.Series.Add("ori:" + i.ToString());
                //    //chart1.Series.Add("fore:" + i.ToString());
                //    chart1.Series[i + 1].YValueMembers = "ori:" + i.ToString();
                //    chart1.Series[i + 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Series[i + 1 + resultLength].YValueMembers = "fore:" + i.ToString();
                //    chart1.Series[i + 1 + resultLength].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //}
                chart1.DataBind();
            }
            catch
            {
                button1.Text = "结束";

            }
            
        }

        private void chart1_KeyDown(object sender, KeyEventArgs e)
        {
            button1_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataIndex = 0;
            sourcedt = new DataTable();
            sourcedt.Columns.Add("id");
            for (int i = 0; i < resultLength; i++)
            {
                sourcedt.Columns.Add("ori:" + i.ToString());
            }
            for (int i = 0; i < resultLength; i++)
            {
                sourcedt.Columns.Add("fore:" + i.ToString());
            }
            this.data = testdataList[dataIndex];
            int rCount = 0;

            foreach (DataRow dr in data.Rows)
            {
                rCount++;
                DataRow ir = sourcedt.NewRow();
                ir["id"] = rCount;
                for (int i = 0; i < resultLength; i++)
                {
                    ir["ori:" + i.ToString()] = dr[resultId];
                    ir["fore:" + i.ToString()] = dr[resultId + resultLength];
                }
                sourcedt.Rows.Add(ir);
            }
            chart1.DataSource = sourcedt;
            //chart1.Series["id"].XValueMember = "id";
            //for (int i = 0; i < resultLength; i++)
            //{
            //    //chart1.Series.Add("ori:" + i.ToString());
            //    //chart1.Series.Add("fore:" + i.ToString());
            //    chart1.Series[i + 1].YValueMembers = "ori:" + i.ToString();
            //    chart1.Series[i + 1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //    chart1.Series[i + 1 + resultLength].YValueMembers = "fore:" + i.ToString();
            //    chart1.Series[i + 1 + resultLength].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //}
            chart1.DataBind();
        }

        
        
    }
}

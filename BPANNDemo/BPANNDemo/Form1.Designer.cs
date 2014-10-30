namespace BPANNDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.trainDataGrid = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.testDataGrid = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_MutaRate = new System.Windows.Forms.TextBox();
            this.TB_generation = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_CrossRate = new System.Windows.Forms.TextBox();
            this.TB_population = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.th1V = new System.Windows.Forms.TextBox();
            this.th = new System.Windows.Forms.Label();
            this.th2 = new System.Windows.Forms.Label();
            this.th2V = new System.Windows.Forms.TextBox();
            this.l_DE_F1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.F1V = new System.Windows.Forms.TextBox();
            this.G1V = new System.Windows.Forms.TextBox();
            this.l_DE_G1 = new System.Windows.Forms.Label();
            this.l_DE_G2 = new System.Windows.Forms.Label();
            this.G2V = new System.Windows.Forms.TextBox();
            this.F2V = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.DE_maxNetwork = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.DE_crossRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DE_mutateRate = new System.Windows.Forms.TextBox();
            this.DE_maxGeneration = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trainDataGrid)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.testDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(339, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 54);
            this.button1.TabIndex = 4;
            this.button1.Text = "训练";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 54);
            this.button2.TabIndex = 5;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // trainDataGrid
            // 
            this.trainDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trainDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainDataGrid.Location = new System.Drawing.Point(3, 3);
            this.trainDataGrid.Name = "trainDataGrid";
            this.trainDataGrid.RowTemplate.Height = 23;
            this.trainDataGrid.Size = new System.Drawing.Size(424, 459);
            this.trainDataGrid.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(21, 168);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(438, 491);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.trainDataGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(430, 465);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "训练集合";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.testDataGrid);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(430, 465);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "测试集合";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // testDataGrid
            // 
            this.testDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.testDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testDataGrid.Location = new System.Drawing.Point(3, 3);
            this.testDataGrid.Name = "testDataGrid";
            this.testDataGrid.RowTemplate.Height = 23;
            this.testDataGrid.Size = new System.Drawing.Size(424, 459);
            this.testDataGrid.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(277, 21);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 54);
            this.button3.TabIndex = 6;
            this.button3.Text = "读取";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(127, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 21);
            this.textBox1.TabIndex = 9;
            this.textBox1.Text = "set7";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "文件夹路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "隐藏层节点数：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(127, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(142, 21);
            this.textBox2.TabIndex = 11;
            this.textBox2.Text = "9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "输入维数：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(125, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(144, 21);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "检测阈值：";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(125, 110);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(144, 21);
            this.textBox4.TabIndex = 16;
            this.textBox4.Text = "0.000007";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(487, 23);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 50);
            this.button4.TabIndex = 18;
            this.button4.Text = "并行训练";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(277, 135);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(178, 49);
            this.button5.TabIndex = 19;
            this.button5.Text = "进化演示";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(103, 142);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(98, 50);
            this.button6.TabIndex = 20;
            this.button6.Text = "遗传算法选择";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button5);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.button4);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.textBox4);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1170, 667);
            this.splitContainer1.SplitterDistance = 471;
            this.splitContainer1.TabIndex = 21;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.checkBox1);
            this.splitContainer2.Panel1.Controls.Add(this.button6);
            this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            this.splitContainer2.Panel1.Controls.Add(this.TB_MutaRate);
            this.splitContainer2.Panel1.Controls.Add(this.TB_generation);
            this.splitContainer2.Panel1.Controls.Add(this.label9);
            this.splitContainer2.Panel1.Controls.Add(this.tb_CrossRate);
            this.splitContainer2.Panel1.Controls.Add(this.TB_population);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button8);
            this.splitContainer2.Panel2.Controls.Add(this.th1V);
            this.splitContainer2.Panel2.Controls.Add(this.th);
            this.splitContainer2.Panel2.Controls.Add(this.th2);
            this.splitContainer2.Panel2.Controls.Add(this.th2V);
            this.splitContainer2.Panel2.Controls.Add(this.l_DE_F1);
            this.splitContainer2.Panel2.Controls.Add(this.label15);
            this.splitContainer2.Panel2.Controls.Add(this.F1V);
            this.splitContainer2.Panel2.Controls.Add(this.G1V);
            this.splitContainer2.Panel2.Controls.Add(this.l_DE_G1);
            this.splitContainer2.Panel2.Controls.Add(this.l_DE_G2);
            this.splitContainer2.Panel2.Controls.Add(this.G2V);
            this.splitContainer2.Panel2.Controls.Add(this.F2V);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.checkBox2);
            this.splitContainer2.Panel2.Controls.Add(this.label13);
            this.splitContainer2.Panel2.Controls.Add(this.button7);
            this.splitContainer2.Panel2.Controls.Add(this.DE_maxNetwork);
            this.splitContainer2.Panel2.Controls.Add(this.richTextBox2);
            this.splitContainer2.Panel2.Controls.Add(this.DE_crossRate);
            this.splitContainer2.Panel2.Controls.Add(this.label11);
            this.splitContainer2.Panel2.Controls.Add(this.label12);
            this.splitContainer2.Panel2.Controls.Add(this.DE_mutateRate);
            this.splitContainer2.Panel2.Controls.Add(this.DE_maxGeneration);
            this.splitContainer2.Size = new System.Drawing.Size(695, 667);
            this.splitContainer2.SplitterDistance = 301;
            this.splitContainer2.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "种群规模";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(31, 160);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(60, 16);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "多线程";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(23, 198);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(261, 461);
            this.richTextBox1.TabIndex = 29;
            this.richTextBox1.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "交叉率";
            // 
            // TB_MutaRate
            // 
            this.TB_MutaRate.Location = new System.Drawing.Point(105, 114);
            this.TB_MutaRate.Name = "TB_MutaRate";
            this.TB_MutaRate.Size = new System.Drawing.Size(96, 21);
            this.TB_MutaRate.TabIndex = 27;
            this.TB_MutaRate.Text = "0.01";
            // 
            // TB_generation
            // 
            this.TB_generation.Location = new System.Drawing.Point(103, 49);
            this.TB_generation.Name = "TB_generation";
            this.TB_generation.Size = new System.Drawing.Size(98, 21);
            this.TB_generation.TabIndex = 25;
            this.TB_generation.Text = "70";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "变异率";
            // 
            // tb_CrossRate
            // 
            this.tb_CrossRate.Location = new System.Drawing.Point(105, 82);
            this.tb_CrossRate.Name = "tb_CrossRate";
            this.tb_CrossRate.Size = new System.Drawing.Size(96, 21);
            this.tb_CrossRate.TabIndex = 23;
            this.tb_CrossRate.Text = "0.85";
            // 
            // TB_population
            // 
            this.TB_population.Location = new System.Drawing.Point(103, 18);
            this.TB_population.Name = "TB_population";
            this.TB_population.Size = new System.Drawing.Size(98, 21);
            this.TB_population.TabIndex = 21;
            this.TB_population.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 26;
            this.label7.Text = "进化次数";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(282, 36);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(86, 45);
            this.button8.TabIndex = 53;
            this.button8.Text = "标准差分计算";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // th1V
            // 
            this.th1V.Location = new System.Drawing.Point(209, 135);
            this.th1V.Name = "th1V";
            this.th1V.Size = new System.Drawing.Size(57, 21);
            this.th1V.TabIndex = 49;
            this.th1V.Text = "3";
            // 
            // th
            // 
            this.th.AutoSize = true;
            this.th.Location = new System.Drawing.Point(145, 138);
            this.th.Name = "th";
            this.th.Size = new System.Drawing.Size(23, 12);
            this.th.TabIndex = 50;
            this.th.Text = "th1";
            // 
            // th2
            // 
            this.th2.AutoSize = true;
            this.th2.Location = new System.Drawing.Point(145, 170);
            this.th2.Name = "th2";
            this.th2.Size = new System.Drawing.Size(23, 12);
            this.th2.TabIndex = 52;
            this.th2.Text = "th2";
            // 
            // th2V
            // 
            this.th2V.Location = new System.Drawing.Point(209, 167);
            this.th2V.Name = "th2V";
            this.th2V.Size = new System.Drawing.Size(57, 21);
            this.th2V.TabIndex = 51;
            this.th2V.Text = "7";
            // 
            // l_DE_F1
            // 
            this.l_DE_F1.AutoSize = true;
            this.l_DE_F1.Enabled = false;
            this.l_DE_F1.Location = new System.Drawing.Point(145, 11);
            this.l_DE_F1.Name = "l_DE_F1";
            this.l_DE_F1.Size = new System.Drawing.Size(17, 12);
            this.l_DE_F1.TabIndex = 42;
            this.l_DE_F1.Text = "F1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(143, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 46;
            this.label15.Text = "F2";
            // 
            // F1V
            // 
            this.F1V.Enabled = false;
            this.F1V.Location = new System.Drawing.Point(207, 8);
            this.F1V.Name = "F1V";
            this.F1V.Size = new System.Drawing.Size(59, 21);
            this.F1V.TabIndex = 41;
            this.F1V.Text = "1.5";
            // 
            // G1V
            // 
            this.G1V.Location = new System.Drawing.Point(209, 72);
            this.G1V.Name = "G1V";
            this.G1V.Size = new System.Drawing.Size(57, 21);
            this.G1V.TabIndex = 43;
            this.G1V.Text = "3";
            // 
            // l_DE_G1
            // 
            this.l_DE_G1.AutoSize = true;
            this.l_DE_G1.Location = new System.Drawing.Point(145, 75);
            this.l_DE_G1.Name = "l_DE_G1";
            this.l_DE_G1.Size = new System.Drawing.Size(17, 12);
            this.l_DE_G1.TabIndex = 44;
            this.l_DE_G1.Text = "G1";
            // 
            // l_DE_G2
            // 
            this.l_DE_G2.AutoSize = true;
            this.l_DE_G2.Location = new System.Drawing.Point(145, 107);
            this.l_DE_G2.Name = "l_DE_G2";
            this.l_DE_G2.Size = new System.Drawing.Size(17, 12);
            this.l_DE_G2.TabIndex = 48;
            this.l_DE_G2.Text = "G2";
            // 
            // G2V
            // 
            this.G2V.Location = new System.Drawing.Point(209, 104);
            this.G2V.Name = "G2V";
            this.G2V.Size = new System.Drawing.Size(57, 21);
            this.G2V.TabIndex = 47;
            this.G2V.Text = "3";
            // 
            // F2V
            // 
            this.F2V.Location = new System.Drawing.Point(207, 39);
            this.F2V.Name = "F2V";
            this.F2V.Size = new System.Drawing.Size(59, 21);
            this.F2V.TabIndex = 45;
            this.F2V.Text = "1.5";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "种群规模";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(11, 131);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 33;
            this.checkBox2.Text = "多线程";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 37;
            this.label13.Text = "进化次数";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(282, 111);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(86, 45);
            this.button7.TabIndex = 30;
            this.button7.Text = "正交差分计算";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // DE_maxNetwork
            // 
            this.DE_maxNetwork.Location = new System.Drawing.Point(71, 8);
            this.DE_maxNetwork.Name = "DE_maxNetwork";
            this.DE_maxNetwork.Size = new System.Drawing.Size(59, 21);
            this.DE_maxNetwork.TabIndex = 31;
            this.DE_maxNetwork.Text = "20";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(11, 198);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(261, 461);
            this.richTextBox2.TabIndex = 40;
            this.richTextBox2.Text = "";
            // 
            // DE_crossRate
            // 
            this.DE_crossRate.Location = new System.Drawing.Point(73, 72);
            this.DE_crossRate.Name = "DE_crossRate";
            this.DE_crossRate.Size = new System.Drawing.Size(57, 21);
            this.DE_crossRate.TabIndex = 34;
            this.DE_crossRate.Text = "0.85";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 35;
            this.label11.Text = "交叉率";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 107);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 39;
            this.label12.Text = "变异率";
            // 
            // DE_mutateRate
            // 
            this.DE_mutateRate.Location = new System.Drawing.Point(73, 104);
            this.DE_mutateRate.Name = "DE_mutateRate";
            this.DE_mutateRate.Size = new System.Drawing.Size(57, 21);
            this.DE_mutateRate.TabIndex = 38;
            this.DE_mutateRate.Text = "0.01";
            // 
            // DE_maxGeneration
            // 
            this.DE_maxGeneration.Location = new System.Drawing.Point(71, 39);
            this.DE_maxGeneration.Name = "DE_maxGeneration";
            this.DE_maxGeneration.Size = new System.Drawing.Size(59, 21);
            this.DE_maxGeneration.TabIndex = 36;
            this.DE_maxGeneration.Text = "70";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 667);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "BP神经网络演示";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trainDataGrid)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.testDataGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView trainDataGrid;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView testDataGrid;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox TB_MutaRate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TB_population;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_CrossRate;
        private System.Windows.Forms.TextBox TB_generation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label l_DE_F1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox F1V;
        private System.Windows.Forms.TextBox G1V;
        private System.Windows.Forms.Label l_DE_G1;
        private System.Windows.Forms.Label l_DE_G2;
        private System.Windows.Forms.TextBox G2V;
        private System.Windows.Forms.TextBox F2V;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox DE_maxNetwork;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.TextBox DE_crossRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox DE_mutateRate;
        private System.Windows.Forms.TextBox DE_maxGeneration;
        private System.Windows.Forms.TextBox th1V;
        private System.Windows.Forms.Label th;
        private System.Windows.Forms.Label th2;
        private System.Windows.Forms.TextBox th2V;
        private System.Windows.Forms.Button button8;
    }
}


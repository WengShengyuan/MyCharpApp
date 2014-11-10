namespace 无线网卡切换
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
            this.cmbNetWork = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.BN_Stop = new System.Windows.Forms.Button();
            this.BN_Start = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tb_thisNetWork = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_configList = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_ConfigName = new System.Windows.Forms.TextBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_AutoSet = new System.Windows.Forms.Button();
            this.tb_gateWay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_subNet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbNetWork
            // 
            this.cmbNetWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNetWork.FormattingEnabled = true;
            this.cmbNetWork.ItemHeight = 12;
            this.cmbNetWork.Location = new System.Drawing.Point(0, 0);
            this.cmbNetWork.Name = "cmbNetWork";
            this.cmbNetWork.Size = new System.Drawing.Size(482, 428);
            this.cmbNetWork.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmbNetWork);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(671, 428);
            this.splitContainer1.SplitterDistance = 482;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button4);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.BN_Stop);
            this.splitContainer2.Panel1.Controls.Add(this.BN_Start);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Panel2.Controls.Add(this.button2);
            this.splitContainer2.Size = new System.Drawing.Size(185, 428);
            this.splitContainer2.SplitterDistance = 276;
            this.splitContainer2.TabIndex = 2;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(25, 209);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(107, 39);
            this.button4.TabIndex = 3;
            this.button4.Text = "设为有线网卡";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(25, 140);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "设为无线网卡";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BN_Stop
            // 
            this.BN_Stop.Location = new System.Drawing.Point(25, 12);
            this.BN_Stop.Name = "BN_Stop";
            this.BN_Stop.Size = new System.Drawing.Size(107, 39);
            this.BN_Stop.TabIndex = 0;
            this.BN_Stop.Text = "停用";
            this.BN_Stop.UseVisualStyleBackColor = true;
            this.BN_Stop.Click += new System.EventHandler(this.BN_Stop_Click);
            // 
            // BN_Start
            // 
            this.BN_Start.Location = new System.Drawing.Point(25, 77);
            this.BN_Start.Name = "BN_Start";
            this.BN_Start.Size = new System.Drawing.Size(107, 39);
            this.BN_Start.TabIndex = 1;
            this.BN_Start.Text = "启用";
            this.BN_Start.UseVisualStyleBackColor = true;
            this.BN_Start.Click += new System.EventHandler(this.BN_Start_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 52);
            this.button1.TabIndex = 2;
            this.button1.Text = "使用有线网卡";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(31, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 52);
            this.button2.TabIndex = 3;
            this.button2.Text = "使用无线网卡";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(685, 460);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(677, 434);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "线路切换";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tb_thisNetWork);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.lb_configList);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tb_ConfigName);
            this.tabPage2.Controls.Add(this.btn_Save);
            this.tabPage2.Controls.Add(this.btn_Set);
            this.tabPage2.Controls.Add(this.btn_AutoSet);
            this.tabPage2.Controls.Add(this.tb_gateWay);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.tb_subNet);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.tb_IP);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(677, 434);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "网络参数";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tb_thisNetWork
            // 
            this.tb_thisNetWork.Location = new System.Drawing.Point(93, 29);
            this.tb_thisNetWork.Name = "tb_thisNetWork";
            this.tb_thisNetWork.ReadOnly = true;
            this.tb_thisNetWork.Size = new System.Drawing.Size(222, 21);
            this.tb_thisNetWork.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "有线设备:";
            // 
            // lb_configList
            // 
            this.lb_configList.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_configList.FormattingEnabled = true;
            this.lb_configList.ItemHeight = 16;
            this.lb_configList.Location = new System.Drawing.Point(432, 53);
            this.lb_configList.Name = "lb_configList";
            this.lb_configList.Size = new System.Drawing.Size(227, 276);
            this.lb_configList.TabIndex = 11;
            this.lb_configList.SelectedIndexChanged += new System.EventHandler(this.lb_configList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(430, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "配置名称";
            // 
            // tb_ConfigName
            // 
            this.tb_ConfigName.Location = new System.Drawing.Point(504, 14);
            this.tb_ConfigName.Name = "tb_ConfigName";
            this.tb_ConfigName.Size = new System.Drawing.Size(155, 21);
            this.tb_ConfigName.TabIndex = 9;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(321, 120);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(105, 39);
            this.btn_Save.TabIndex = 8;
            this.btn_Save.Text = "-->保存配置-->";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Set
            // 
            this.btn_Set.Location = new System.Drawing.Point(204, 218);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(111, 39);
            this.btn_Set.TabIndex = 7;
            this.btn_Set.Text = "设置";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_AutoSet
            // 
            this.btn_AutoSet.Location = new System.Drawing.Point(51, 218);
            this.btn_AutoSet.Name = "btn_AutoSet";
            this.btn_AutoSet.Size = new System.Drawing.Size(111, 39);
            this.btn_AutoSet.TabIndex = 6;
            this.btn_AutoSet.Text = "自动获取";
            this.btn_AutoSet.UseVisualStyleBackColor = true;
            this.btn_AutoSet.Click += new System.EventHandler(this.btn_AutoSet_Click);
            // 
            // tb_gateWay
            // 
            this.tb_gateWay.Location = new System.Drawing.Point(93, 161);
            this.tb_gateWay.Name = "tb_gateWay";
            this.tb_gateWay.Size = new System.Drawing.Size(222, 21);
            this.tb_gateWay.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "默认网关:";
            // 
            // tb_subNet
            // 
            this.tb_subNet.Location = new System.Drawing.Point(93, 117);
            this.tb_subNet.Name = "tb_subNet";
            this.tb_subNet.Size = new System.Drawing.Size(222, 21);
            this.tb_subNet.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "子网掩码:";
            // 
            // tb_IP
            // 
            this.tb_IP.Location = new System.Drawing.Point(93, 74);
            this.tb_IP.Name = "tb_IP";
            this.tb_IP.Size = new System.Drawing.Size(222, 21);
            this.tb_IP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 460);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox cmbNetWork;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BN_Start;
        private System.Windows.Forms.Button BN_Stop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_AutoSet;
        private System.Windows.Forms.TextBox tb_gateWay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_subNet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lb_configList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_ConfigName;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox tb_thisNetWork;
        private System.Windows.Forms.Label label5;
    }
}


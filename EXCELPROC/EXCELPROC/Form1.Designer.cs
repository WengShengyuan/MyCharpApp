namespace EXCELPROC
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
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FILEPATH1 = new System.Windows.Forms.TextBox();
            this.FILEPATH2 = new System.Windows.Forms.TextBox();
            this.XVALUE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PAGE2 = new System.Windows.Forms.TextBox();
            this.PAGE1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LINE1A = new System.Windows.Forms.TextBox();
            this.COL1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(525, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "前X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(747, 76);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 33);
            this.button3.TabIndex = 2;
            this.button3.Text = "退出";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "风险预警路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(957, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "两违统计路径：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "前X名=";
            // 
            // FILEPATH1
            // 
            this.FILEPATH1.Location = new System.Drawing.Point(128, 28);
            this.FILEPATH1.Name = "FILEPATH1";
            this.FILEPATH1.Size = new System.Drawing.Size(226, 21);
            this.FILEPATH1.TabIndex = 6;
            this.FILEPATH1.Text = "D:\\\\职工考核积分与风险预警1.1版.xlsx";
            // 
            // FILEPATH2
            // 
            this.FILEPATH2.Location = new System.Drawing.Point(1052, 50);
            this.FILEPATH2.Name = "FILEPATH2";
            this.FILEPATH2.Size = new System.Drawing.Size(226, 21);
            this.FILEPATH2.TabIndex = 7;
            this.FILEPATH2.Text = "D:\\\\运转车间2月份两违统计1.1版.xlsx";
            // 
            // XVALUE
            // 
            this.XVALUE.Location = new System.Drawing.Point(131, 73);
            this.XVALUE.Name = "XVALUE";
            this.XVALUE.Size = new System.Drawing.Size(60, 21);
            this.XVALUE.TabIndex = 8;
            this.XVALUE.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(360, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "页名：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1065, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "新建页名：";
            // 
            // PAGE2
            // 
            this.PAGE2.Location = new System.Drawing.Point(1124, 159);
            this.PAGE2.Name = "PAGE2";
            this.PAGE2.Size = new System.Drawing.Size(133, 21);
            this.PAGE2.TabIndex = 11;
            this.PAGE2.Text = "输出结果页面";
            // 
            // PAGE1
            // 
            this.PAGE1.Location = new System.Drawing.Point(409, 28);
            this.PAGE1.Name = "PAGE1";
            this.PAGE1.Size = new System.Drawing.Size(32, 21);
            this.PAGE1.TabIndex = 12;
            this.PAGE1.Text = "二月";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(540, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "行";
            // 
            // LINE1A
            // 
            this.LINE1A.Location = new System.Drawing.Point(577, 25);
            this.LINE1A.Name = "LINE1A";
            this.LINE1A.Size = new System.Drawing.Size(32, 21);
            this.LINE1A.TabIndex = 15;
            this.LINE1A.Text = "10";
            // 
            // COL1
            // 
            this.COL1.Location = new System.Drawing.Point(786, 26);
            this.COL1.Name = "COL1";
            this.COL1.Size = new System.Drawing.Size(32, 21);
            this.COL1.TabIndex = 16;
            this.COL1.Text = "8";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(824, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "列";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(471, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "（首行非空）第：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(679, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 12);
            this.label10.TabIndex = 19;
            this.label10.Text = "（比较依据）第：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 226);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1355, 322);
            this.dataGridView1.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "行数总计：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(84, 199);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 12);
            this.label11.TabIndex = 25;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 142);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.COL1);
            this.Controls.Add(this.LINE1A);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PAGE1);
            this.Controls.Add(this.PAGE2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.XVALUE);
            this.Controls.Add(this.FILEPATH2);
            this.Controls.Add(this.FILEPATH1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FILEPATH1;
        private System.Windows.Forms.TextBox FILEPATH2;
        private System.Windows.Forms.TextBox XVALUE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PAGE2;
        private System.Windows.Forms.TextBox PAGE1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox LINE1A;
        private System.Windows.Forms.TextBox COL1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
    }
}


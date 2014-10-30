using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Myfinancial
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 1;
            i++;
            i++;
            i++;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyMetal mmfr = new MyMetal(textBox1.Text);
            mmfr.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OilDetail odfr = new OilDetail(textBox1.Text);
            odfr.Show();
        }
    }
}

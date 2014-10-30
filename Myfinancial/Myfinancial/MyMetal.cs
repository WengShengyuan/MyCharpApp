using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text;


namespace Myfinancial
{
    public partial class MyMetal : Form
    {
        String MetalFolder;
        String hjpath;
        String bypath;
        String bjpath;
        DataSet hjdata ;
        DataSet bydata ;
        DataSet bjdata ;
        public DataProcess hjdp;
        public DataProcess bydp;
        public DataProcess bjdp;
        public MyMetal(String inpath)
        {
            InitializeComponent();
            MetalFolder = inpath+"\\metal";
            hjpath = MetalFolder + "\\hj.txt";
            bypath = MetalFolder + "\\by.txt";
            bjpath = MetalFolder + "\\bj.txt";
        }

        private void MyMetal_Load(object sender, EventArgs e)
        {
             hjdp = new DataProcess(hjpath, "hj");
             bydp = new DataProcess(bypath, "by");
             bjdp = new DataProcess(bjpath, "bj");
            //MessageBox.Show(MetalFolder + hjpath + bypath + bjpath);
            
            dataGridView1.DataSource = hjdp.ds.Tables[0];
            dataGridView2.DataSource = bydp.ds.Tables[0];
            dataGridView3.DataSource = bjdp.ds.Tables[0];


            textBox1.Text = hjdp.buymoneystr;
            textBox2.Text = hjdp.sellmoneystr;
            textBox3.Text = hjdp.buyamstr;
            textBox4.Text = hjdp.sellamstr; 
            //double totalbalance=0.0;
            //totalbalance =(System.Convert.ToDouble(textBox2.Text) - System.Convert.ToDouble(textBox1.Text));


            
            textBox10.Text = bydp.buymoneystr;
            textBox9.Text = bydp.sellmoneystr;
            textBox7.Text = bydp.buyamstr;
            textBox8.Text = bydp.sellamstr;
            //totalbalance =(System.Convert.ToDouble(textBox9.Text) - System.Convert.ToDouble(textBox10.Text));

            
            textBox12.Text = bjdp.buymoneystr;
            textBox11.Text = bjdp.sellmoneystr;
            textBox5.Text = bjdp.buyamstr;
            textBox6.Text = bjdp.sellamstr;

            label14.Text = (hjdp.sellmoney - hjdp.buymoney).ToString("0.00") + "元";
            label15.Text = (hjdp.buyam - hjdp.sellam).ToString("0.00") + "克黄金";
            label17.Text = (bydp.sellmoney - bydp.buymoney).ToString("0.00") + "元";
            label16.Text = (bydp.buyam - bydp.sellam).ToString("0.00") + "克白银";
            label19.Text = (bjdp.sellmoney - bjdp.buymoney).ToString("0.00") + "元";
            label18.Text = (bjdp.buyam - bjdp.sellam).ToString("0.00") + "克铂金";
            label23.Text = hjdp.sumstr;
            label24.Text = bydp.sumstr;
            label22.Text = bjdp.sumstr;

            textBox13.Text = hjdp.averagebuystr;
            textBox14.Text = hjdp.averagesellstr;
            textBox16.Text = bydp.averagebuystr;
            textBox15.Text = bydp.averagesellstr;
            textBox18.Text = bjdp.averagebuystr;
            textBox17.Text = bjdp.averagesellstr;

        }

        

        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DetailGraph dgfr = new DetailGraph(hjdata,"hj");
            dgfr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}

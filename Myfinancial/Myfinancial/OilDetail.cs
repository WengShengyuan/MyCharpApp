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
    public partial class OilDetail : Form
    {
        String OilFolder;
        String oilpath;
        DataSet oildata;
        public DataProcess oildp;
        public OilDetail(String inpath)
        {
            InitializeComponent();
            OilFolder = inpath + "\\oil";
            oilpath = OilFolder + "\\oil.txt";
        }
         private void OilDetail_Load(object sender, EventArgs e)
        {
            oildp = new DataProcess(oilpath,"Oil");
            
            dataGridView1.DataSource = oildp.ds.Tables[0];

            textBox1.Text = oildp.buymoneystr;
            textBox2.Text = oildp.sellmoneystr;
            textBox3.Text = oildp.buyamstr;
            textBox4.Text = oildp.sellamstr;
            label14.Text = (oildp.sellmoney - oildp.buymoney).ToString("0.00") + "元";
            label15.Text = (oildp.buyam - oildp.sellam).ToString("0.00") + "桶石油";
            //MessageBox.Show("sellam=" + oildp.sellam.ToString());
            label23.Text = oildp.sumstr;
            textBox13.Text = oildp.averagebuystr;
            textBox14.Text = oildp.averagesellstr;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}

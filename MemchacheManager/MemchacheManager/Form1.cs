using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SQLite;

namespace MemchacheManager
{
    public partial class Form1 : Form
    {

        string localDBFile;
        SQLiteConnection Conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void BN_CreateDB_Click(object sender, EventArgs e)
        {
            //创建数据库
            //localDBFile = "data source = " + Application.StartupPath + "@\\" + textBox1.Text + ".db" + ";";
            //if (!File.Exists(localDBFile))
            //{
            //    Conn = new SQLiteConnection(localDBFile);
            //    Conn.Open();
            //}
            //MessageBox.Show(Conn.ToString());
            
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
   
        }
    }
}

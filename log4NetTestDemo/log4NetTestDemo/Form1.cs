using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;



namespace log4NetTestDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logHelper.Debug(MethodBase.GetCurrentMethod().DeclaringType, "Debug");
            logHelper.Error(MethodBase.GetCurrentMethod().DeclaringType, new Exception("Error"));
            logHelper.Fatal(MethodBase.GetCurrentMethod().DeclaringType, new Exception("Fatal"));
            logHelper.Info(MethodBase.GetCurrentMethod().DeclaringType, "info");
            logHelper.Warn(MethodBase.GetCurrentMethod().DeclaringType, "warn");
            
            
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

namespace 无线网卡切换
{
    public partial class Form1 : Form
    {

        //const string WLANCard = "DW1530 Wireless-N WLAN Half-Mini Card";
        //const string LANCard = "Intel(R) 82579LM Gigabit Network Connection";
        string WLANCard;
        string LANCard;
        string proFilePath = Application.StartupPath;
        
        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            try
            {
                if (config.AppSettings.Settings["WLANCard"].Value == "null")
                {
                    
                    button2.Enabled = false;
                }
            }
            catch
            {
                config.AppSettings.Settings.Add("WLANCard", "null");
                button2.Enabled = false;
            }
            try
            {
                if (config.AppSettings.Settings["LANCard"].Value  == "null")
                {
                    
                    button1.Enabled = false;
                }

            }
            catch
            {
                config.AppSettings.Settings.Add("LANCard", "null");
                button1.Enabled = false;
            }
            
            
            WLANCard = config.AppSettings.Settings["WLANCard"].Value;
            LANCard = config.AppSettings.Settings["LANCard"].Value;
            NetWorkList();
            aotuFillConfig();
        }


        /// <summary>
        /// 网卡列表
        /// </summary>
        public void NetWorkList()
        {
            
            
            string manage = "SELECT * From Win32_NetworkAdapter";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(manage);
            ManagementObjectCollection collection = searcher.Get();
            List<string> netWorkList = new List<string>();

            foreach (ManagementObject obj in collection)
            {
                string name = obj["Name"].ToString().Trim();
                if (name == WLANCard)
                    name += ", 无线网卡";
                if(name == LANCard)
                    name +=", 有线网卡"; 
                netWorkList.Add(name);

            }
            this.cmbNetWork.DataSource = netWorkList;
            button2.Enabled = isSetted();
            button1.Enabled = isSetted();

        }

        /// <summary>
        /// 禁用网卡
        /// </summary>5
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool DisableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Disable", null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 启用网卡
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool EnableNetWork(ManagementObject network)
        {
            try
            {
                network.InvokeMethod("Enable", null);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 网卡状态
        /// </summary>
        /// <param name="netWorkName">网卡名</param>
        /// <returns></returns>
        public bool NetWorkState(string netWorkName)
        {
            string netState = "SELECT * From Win32_NetworkAdapter";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();
            foreach (ManagementObject manage in collection)
            {
                
                if (manage["Name"].ToString() == netWorkName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 得到指定网卡
        /// </summary>
        /// <param name="networkname">网卡名字</param>
        /// <returns></returns>
        public ManagementObject NetWork(string networkname)
        {
            string netState = "SELECT * From Win32_NetworkAdapter";

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject manage in collection)
            {
                if (manage["Name"].ToString() == networkname)
                {
                    return manage;
                }
            }


            return null;
        }

        //private void btnStart_Click(object sender, EventArgs e)
        //{
        //    if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
        //    {
        //        if (!EnableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
        //        {
        //            MessageBox.Show("开启网卡失败!");
        //        }
        //        else
        //        {
        //            MessageBox.Show("开启网卡成功!");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("网卡己开启!");
        //    }

        //    NetWorkList();
        //}

        //private void btnStop_Click(object sender, EventArgs e)
        //{

        //    if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
        //    {
        //        if (!DisableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
        //        {
        //            MessageBox.Show("禁用网卡失败!");
        //        }
        //        else
        //        {
        //            MessageBox.Show("禁用网卡成功!");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("网卡己禁用!");
        //    }

        //    NetWorkList();
        //}

        private void BN_Start_Click(object sender, EventArgs e)
        {
            //启用按钮
            if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
            {
                if (!EnableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
                {
                    MessageBox.Show("开启网卡失败!");
                }
                else
                {
                    MessageBox.Show("开启网卡成功!");
                }
            }
            else
            {
                MessageBox.Show("网卡己开启!");
            }

            NetWorkList();
        }

        private bool switchCard(string WLANorLAN)
        {
            if (WLANorLAN=="WLAN")
            {
                //使用WLAN
                return (EnableNetWork(NetWork(WLANCard)) && DisableNetWork(NetWork(LANCard)));
            }
            else
            {
                //使用LAN
                return (EnableNetWork(NetWork(LANCard)) && DisableNetWork(NetWork(WLANCard)));
            }
            
        }

        private void BN_Stop_Click(object sender, EventArgs e)
        {
            //停用按钮
            if (NetWorkState(this.cmbNetWork.SelectedValue.ToString()))
            {
                if (!DisableNetWork(NetWork(this.cmbNetWork.SelectedValue.ToString())))
                {
                    MessageBox.Show("禁用网卡失败!");
                }
                else
                {
                    MessageBox.Show("禁用网卡成功!");
                }
            }
            else
            {
                MessageBox.Show("网卡己禁用!");
            }

            NetWorkList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //使用有线
            if (switchCard("LAN"))
                MessageBox.Show("切换成功");
            else
                MessageBox.Show("切换失败");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //使用无线
            if (switchCard("WLAN"))
                MessageBox.Show("切换成功");
            else
                MessageBox.Show("切换失败");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //设为无线网卡
            string tempStr = cmbNetWork.SelectedItem.ToString();
            config.AppSettings.Settings["WLANCard"].Value = tempStr.Split(',')[0].Trim();
            WLANCard = config.AppSettings.Settings["WLANCard"].Value;
            config.Save();
            if (isSetted())
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            NetWorkList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //设为有线网卡
            string tempStr = cmbNetWork.SelectedItem.ToString();
            config.AppSettings.Settings["LANCard"].Value = tempStr.Split(',')[0].Trim();
            LANCard = config.AppSettings.Settings["LANCard"].Value;
            config.Save();
            if (isSetted())
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
            NetWorkList();
        }

        private bool isSetted()
        {
            int i=0;
            foreach(var obj in cmbNetWork.Items )
            {
                if (obj.ToString().Contains(','))
                    i++;
            }
            return (i == 2);
        }

        private void aotuFillConfig()
        {
            //自动填写配置文件的网络设置，否则为空，load时候调用
            lb_configList.Items.Clear();
            string[] configs = getValuesbyKey("My_AllConfigKey");
            if (configs == null || configs.Length == 0)
            {
                MessageBox.Show("目前没有设置，请添加！","提醒");
            }
            else
            {
                foreach (string iconfig in configs)
                {
                    if(iconfig != null || iconfig!= "")
                        lb_configList.Items.Add(iconfig);
                }
            }
            tb_thisNetWork.Text = LANCard;
            
        }

        private void btn_AutoSet_Click(object sender, EventArgs e)
        {
            //设置自动获取
            //string netState = "SELECT * From Win32_NetworkAdapter";
            ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            //ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection moc = wmi.GetInstances();
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            foreach (ManagementObject mo in moc)
            {
                PropertyDataCollection properties = mo.Properties;

                
                if (properties["Description"].Value.ToString().Trim() == LANCard.Trim())
                {
                    //如果没有启用IP设置的网络设备则跳过
                    if (!(bool)properties["IPEnabled"].Value)
                        continue;

                    //设置IP地址和掩码
                    mo.InvokeMethod("EnableDHCP", null);
                    mo.InvokeMethod("SetDNSServerSearchOrder", null);

                }

            }
            MessageBox.Show("设置成功");
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
            //已输入的参数设置
            try
            {
                SetIPAddress(new string[] {tb_IP.Text.Trim()}, new string[] {tb_subNet.Text.Trim()}, new string[] {tb_gateWay.Text.Trim()});
                MessageBox.Show("设置成功");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            //保存为一个配置
            string Key = tb_ConfigName.Text.Trim();
            string oriValue = "";
            string Value = string.Format("{0};{1};{2}", tb_IP.Text.Trim(), tb_subNet.Text.Trim(), tb_gateWay.Text.Trim());
            oriValue = config.AppSettings.Settings["My_AllConfigKey"].Value.ToString().Trim();
            
            setValueByKey(Key, Value);
            setValueByKey("My_AllConfigKey", string.Format("{0};{1}", Key, oriValue));
            
            aotuFillConfig();
        }

        private string[] getValuesbyKey(string Key)
        {
            try
            {
                if (config.AppSettings.Settings[Key].Value.Trim() == "null" )
                    return null;
                else
                    return config.AppSettings.Settings[Key].Value.Trim().Split(';');
            }
            catch
            {
                config.AppSettings.Settings.Add(Key , string.Empty);
                config.Save();
                return null;
            }
            
 
        }

        private void setValueByKey(string Key, string Value)
        {
            try
            {
                config.AppSettings.Settings[Key].Value = Value;
                config.Save();
            }
            catch
            {
                config.AppSettings.Settings.Add(Key, Value);
                config.Save();
            }
            
        }

        private void lb_configList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择某一个配置
            try
            {
                string[] tempConfig = getValuesbyKey(lb_configList.SelectedItem.ToString().Trim());
                tb_IP.Text = tempConfig[0];
                tb_subNet.Text = tempConfig[1];
                tb_gateWay.Text = tempConfig[2];
            }
            catch {}
            
        }


        /// <summary>
        /// 设置IP地址，掩码，网关和DNS
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="submask"></param>
        /// <param name="getway"></param>
        /// <param name="dns"></param>
        private void SetIPAddress(string[] ip, string[] submask, string[] getway)
        {
            //string netState = "SELECT * From Win32_NetworkAdapter";
            ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher(netState);
            //ManagementObjectCollection collection = searcher.Get();
            ManagementObjectCollection moc = wmi.GetInstances();
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            foreach (ManagementObject mo in moc)
            {
                PropertyDataCollection properties = mo.Properties;

                //foreach (PropertyData property in properties)
                //{
                //    if (property.Value != null)
                //        Console.WriteLine("Name:" + property.Name + "Value:" + property.Value);
                //    if (property.Name == "IPAddress" && (string[])property.Value != null)
                //    { string[] valuesss = (string[])property.Value; }

                //    if (property.Value != null)
                //        Console.WriteLine("Name:" + property.Name + "Value:" + property.Value);
                //    if (property.Name == "DefaultIPGateway" && (string[])property.Value != null)
                //    { string[] valuesss = (string[])property.Value; }

                //    if (property.Value != null)
                //        Console.WriteLine("Name:" + property.Name + "Value:" + property.Value);
                //    if (property.Name == "IPSubnet" && (string[])property.Value != null)
                //    { string[] valuesss = (string[])property.Value; }
                //}

                if (properties["Description"].Value.ToString().Trim() == LANCard.Trim())
                {
                    //如果没有启用IP设置的网络设备则跳过
                    if (!(bool)properties["IPEnabled"].Value)
                        continue;

                    //设置IP地址和掩码
                    if (ip != null && submask != null)
                    {
                        inPar = mo.GetMethodParameters("EnableStatic");
                        inPar["IPAddress"] = ip;
                        inPar["SubnetMask"] = submask;
                        outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                    }

                    //设置网关地址
                    if (getway != null)
                    {
                        inPar = mo.GetMethodParameters("SetGateways");
                        inPar["DefaultIPGateway"] = getway;
                        outPar = mo.InvokeMethod("SetGateways", inPar, null);

                        //DNS在内网环境也是网关
                        inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        inPar["DNSServerSearchOrder"] = getway;
                        outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                    }


                    ////设置DNS地址
                    //if (dns != null)
                    //{
                    //    inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                    //    inPar["DNSServerSearchOrder"] = dns;
                    //    outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                    //}
                }



                //foreach (PropertyData property in properties)
                //{
                //    //if(property.Value!=null)
                //    //    Console.WriteLine("Name:"+property.Name+"Value:"+property.Value);
                //    if (property.Name.Trim() == "Description" && property.Value.ToString().Trim() == LANCard.Trim())
                //    {
                //        //如果没有启用IP设置的网络设备则跳过
                //        if (!(bool)mo["IPEnabled"])
                //            continue;

                //        //设置IP地址和掩码
                //        if (ip != null && submask != null)
                //        {
                //            inPar = mo.GetMethodParameters("EnableStatic");
                //            string oriIp = inPar["IPAddress"].ToString();
                //            string oriSubnetMases = inPar["SubnetMask"].ToString();
                //            inPar["IPAddress"] = ip;
                //            inPar["SubnetMask"] = submask;
                //            outPar = mo.InvokeMethod("EnableStatic", inPar, null);
                //        }

                //        //设置网关地址
                //        if (getway != null)
                //        {
                //            inPar = mo.GetMethodParameters("SetGateways");
                //            inPar["DefaultIPGateway"] = getway;
                //            outPar = mo.InvokeMethod("SetGateways", inPar, null);
                //        }

                //        ////设置DNS地址
                //        //if (dns != null)
                //        //{
                //        //    inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                //        //    inPar["DNSServerSearchOrder"] = dns;
                //        //    outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                //        //}

                //    }
                //}
                

                
            }
        }

        private string[] generateAddress(string inValue)
        {
            string[] rts = inValue.Split('.');
            if (rts.Length == 4)
                return rts;
            else
                return null;

        }

        

        
    }
}

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
    public class DataProcess
    {
        public String Filepath;//文本路径
        public String Caltype;//物品类型：hj,by,bj,oil
        public DataSet ds;
        
        public double buymoney;
        public double sellmoney;
        public double buyam;
        public double sellam;
        public String buymoneystr;
        public String sellmoneystr;
        public String buyamstr;
        public String sellamstr;
        public String sumstr;
        public String searchflag=null;
        public String searchflaghead = null;
        public double averagebuy;
        public String averagebuystr;
        public double averagesell;
        public String averagesellstr;


        public  DataProcess(String inpath,String intype)
        {
            //构造函数重载
            Filepath = inpath;
            Caltype = intype;
            if (Caltype != "Oil")
            {
                ds = getMetaldata();
                searchflaghead = "";
            }
            else
            { 
                ds = getoildata();
                searchflaghead = "^";
            }
            
            calall();
            sumstr = getsum(buymoney, sellmoney, buyam, sellam);
            
        }

        public void calall()
        {
            //计算所有参数
            buymoney = counttotal("buy", "成交金额");
            sellmoney = counttotal("sell", "成交金额");
            buyam = counttotal("buy", "成交数量");
            sellam = counttotal("sell", "成交数量");
            buymoneystr = buymoney.ToString("0.00");
            sellmoneystr = sellmoney.ToString("0.00");
            buyamstr = buyam.ToString("0.00");
            sellamstr = sellam.ToString("0.00");
            calaverage();
            
        }

        private void calaverage()
        {
            String sellm = null;
            String sella = null;
            String buym = null;
            String buya = null;

            

            double sellasum = 0;
            double buyasum = 0;
            double sellmsum = 0;
            double buymsum = 0;
            String sellasumstr = null;
            String buyasumstr = null;
            String sellmsumstr = null;
            String buymsumstr = null;

            
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                
                    if (Caltype == "Oil")
                    {
                        if (dr[3].ToString() == "^买入开仓")
                        { 
                            //买入开仓
                            buym = dr[6].ToString().Remove(0, 1);
                            buym = buym.Substring(0, buym.Length - 3);
                            //MessageBox.Show(buym);
                            buya = dr[7].ToString().Remove(0, 1);
                            buya = buya.Substring(0, buya.Length - 2);
                            buymsum += System.Convert.ToDouble(buym) * System.Convert.ToDouble(buya);
                            buyasum += System.Convert.ToDouble(buya);
                        }
                        else
                        { 
                            //卖出平仓
                            sellm = dr[6].ToString().Remove(0, 1);
                            sellm = sellm.Substring(0, sellm.Length - 3);
                            //MessageBox.Show(sellm);
                            sella = dr[7].ToString().Remove(0, 1);
                            sella = sella.Substring(0, sella.Length - 2);
                            sellmsum += System.Convert.ToDouble(sellm) * System.Convert.ToDouble(sella);
                            sellasum += System.Convert.ToDouble(sella);
                        }



                        averagebuy = buymsum / buyasum;
                        averagesell = sellmsum / sellasum;
                        averagebuystr = averagebuy.ToString("0.00");
                        averagesellstr = averagesell.ToString("0.00");
                        
                    }
                    else
                    {
                        if (dr[3].ToString() == "买入开仓")
                        {
                            //买入开仓

                            buymsum += System.Convert.ToDouble(dr[6].ToString()) * System.Convert.ToDouble(dr[7].ToString());
                            buyasum += System.Convert.ToDouble(dr[7].ToString());
                        }
                        else
                        {
                            //卖出平仓
                            sellmsum += System.Convert.ToDouble(dr[6].ToString()) * System.Convert.ToDouble(dr[7].ToString());
                            sellasum += System.Convert.ToDouble(dr[7].ToString());
                        }
                        //MessageBox.Show(dr[8].ToString());

                        averagebuy = buymsum / buyasum;
                        averagesell = sellmsum / sellasum;
                        averagebuystr = averagebuy.ToString("0.00");
                        averagesellstr = averagesell.ToString("0.00");
                    }

                    


                
            }
            //MessageBox.Show(averagebuystr + "," + averagesellstr);
        }

        private DataSet getoildata()
        {
            StreamReader insr = getstream();
            DataSet ds = new DataSet();
            DataTable tb = new DataTable(); ;
            ArrayList contentlist = new ArrayList();

            String tempstr = insr.ReadLine();
            string[] headtmp = tempstr.Split();
            tempstr = null;
            int i = 0;
            string[] head = new string[9];
            foreach (String X in headtmp)
            {
                if (X != "")
                {
                    //MessageBox.Show(X);
                    head[i++] = X;
                }
            }


            foreach (String X in head)
            {
                tb.Columns.Add(X, typeof(string));
            }


            while (!insr.EndOfStream)
            {
                String contentstrtmp = insr.ReadLine();
                if (contentstrtmp.Trim() == "")
                    continue;
                String[] contenttmp = contentstrtmp.Split();
                contentstrtmp = null;
                int j = 0;
                string[] content = new string[9];
                foreach (String Y in contenttmp)
                {
                    if (Y.Trim() != "")
                    {
                        //MessageBox.Show(X);
                        content[j++] = Y;
                    }
                }

                tb.Rows.Add(content);
            }

            ds.Tables.Add(tb);
            return ds;
        }

        public String getsum(double buymoney, double sellmoney, double buyam, double sellam)
        {
            string summstr = null;
            double delmoney;
            double delam;
            delmoney = Math.Round((sellmoney - buymoney), 2);
            delam = Math.Round((buyam - sellam), 2);
            if (delam == 0.00)
            {
                if (delmoney < 0)
                {
                    summstr = "亏损：" + (-delmoney).ToString() + "元！";
                }
                else
                {
                    summstr = "盈利" + delmoney.ToString() + "元！";
                }
            }
            else
            {
                if (delmoney < 0)
                {
                    summstr = "可能亏损！至少在" + Math.Round((-delmoney / delam), 2).ToString() + "元时候卖出！";
                }
                else
                {
                    summstr = "可能盈利！";
                }
            }

            return summstr;
        }


        

        public Double counttotal(String inorout, String type)
        {
            searchflag = null;

            int typeindex = 8;
            if (type == "成交金额")
            { typeindex = 8; }
            if (type == "成交数量")
            { typeindex = 7; }
            if (type == "成交价格")
            { typeindex = 6; }

            double totalnum = 0;
            
            
            if (inorout == "sell")
            {
                searchflag = "卖出平仓";

            }
            else if (inorout == "buy")
            {
                searchflag = "买入开仓";
            }
            searchflag = searchflaghead + searchflag;
            
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[3].ToString() == searchflag)
                {
                    if (Caltype == "Oil")
                    {

                        String str = dr[typeindex].ToString().Remove(0, 1);
                        str = str.Substring(0, str.Length - 1);

                        totalnum += System.Convert.ToDouble(str);
                    }
                    else
                    {
                        //MessageBox.Show(dr[8].ToString());
                        totalnum += System.Convert.ToDouble(dr[typeindex]);
                    }
                    
                    
                }
            }
            return totalnum;
        }

        private DataSet getMetaldata()
        {
            //仅仅用于读取黄金的DATASET
            StreamReader insr = getstream();
            DataSet ds = new DataSet();
            DataTable tb = new DataTable(); ;
            ArrayList contentlist = new ArrayList();
            insr.ReadLine();
            insr.ReadLine();
            insr.ReadLine();
            insr.ReadLine();
            String tempstr = insr.ReadLine();
            
            string[] headtmp = tempstr.Split();
            tempstr = null;
            int i = 0;
            string[] head = new string[9];
            foreach (String X in headtmp)
            {
                if (X != "")
                {
                    //MessageBox.Show(X);
                    head[i++] = X;
                }
            }


            foreach (String X in head)
            {
                tb.Columns.Add(X, typeof(string));
            }


            while (!insr.EndOfStream)
            {
                String contentstrtmp = insr.ReadLine();
                if (contentstrtmp.Trim() == "")
                    continue;
                String[] contenttmp = contentstrtmp.Split();
                contentstrtmp = null;
                int j = 0;
                string[] content = new string[9];
                foreach (String Y in contenttmp)
                {
                    if (Y.Trim() != "")
                    {
                        //MessageBox.Show(X);
                        content[j++] = Y;
                    }
                }

                tb.Rows.Add(content);
            }

            ds.Tables.Add(tb);
            return ds;
        }

        public StreamReader getstream()
        {
            String inpath = Filepath;
            if (File.Exists(inpath))
            {
                FileStream fs = new FileStream(inpath, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("GB2312"));
                return sr;
            }
            else
            {
                MessageBox.Show("读取失败");
                return null;
            }
        }
    }
}

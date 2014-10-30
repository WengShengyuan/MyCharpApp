using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Myfinancial
{

    public partial class DetailGraph : Form
    {
        DataSet ds = new DataSet();//传入数据
        String metaltype = null;//用于图标标题

        double moneymax=0;
        double moneymin=0;
        double amountmax=0;
        double amountmin=0;
        int moneydatecount = 0;
        int amountdatecount = 0;
        int pw ;

        int ph ;

        public DetailGraph(DataSet inds,String inmetal)
        {
            InitializeComponent();
            ds = inds;
            metaltype = inmetal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetailGraph_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(metaltype);
            
            getdatemoney();
            getdateamount();
            paintdata();
            
            
        }


        private void getdatemoney()
        {
            //MessageBox.Show("1");
            //计算每日金钱变化
            double temptotalmoney = 0;
            
            DataTable dt = ds.Tables[0];
            //得到日期数组
            HashSet<String> datelist = new HashSet<String>();
            foreach (DataRow dr in dt.Rows)
            {
                datelist.Add(dr[0].ToString());
            }
            //MessageBox.Show("2");

            ArrayList totaldate = new ArrayList();
            ArrayList totaltotal = new ArrayList();
            foreach (String thedate in datelist)
            {
                double temptotal = 0;
                foreach (DataRow drr in dt.Rows)
                {
                    if (drr[0].ToString() == thedate)
                    {
                        if (drr[3].ToString().Trim() == "买入开仓")
                        {
                            temptotal = temptotal - System.Convert.ToDouble(drr[8]);
                        }
                        else
                        {
                            temptotal = temptotal + System.Convert.ToDouble(drr[8]);
                        }
                    }

                }
                
                totaldate.Add(thedate);
                totaltotal.Add(temptotal);
                //MessageBox.Show(thedate + "=" + temptotal.ToString());
            }
            //MessageBox.Show("3");
            int i = 0;
            double nowtmp = 0;
            foreach (string nowdate in totaldate)
            {
                nowtmp = (double)totaltotal[i++];
                
                temptotalmoney += nowtmp;
               
                
                if (temptotalmoney > moneymax)
                {
                    moneymax = temptotalmoney;
                    
                }
                if (temptotalmoney < moneymin)
                {
                    moneymin = temptotalmoney;
                    
                }
                moneydatecount++;
                //MessageBox.Show(thetemp.ToString());
            }
            //MessageBox.Show("getdatemoney");
        }

        private void getdateamount()
        {
            double temptotalamount = 0;
           
            //计算每日金钱变化
            DataTable dt = ds.Tables[0];

            //得到日期数组
            HashSet<String> datelist = new HashSet<String>();

            foreach (DataRow dr in dt.Rows)
            {
                datelist.Add(dr[0].ToString());


            }

            //MessageBox.Show("11111");

            //foreach (DictionaryEntry de in datelist)
            //{
            //    MessageBox.Show(de.Key.ToString() + "," + de.Value.ToString()); 
            //}
            ArrayList totaldate = new ArrayList();
            ArrayList totaltotal = new ArrayList();
            foreach (String thedate in datelist)
            {
                double temptotal = 0;
                foreach (DataRow drr in dt.Rows)
                {
                    if (drr[0].ToString() == thedate)
                    {
                        if (drr[3].ToString().Trim() == "买入开仓")
                        {
                            //MessageBox.Show("1"+drr[7].ToString());
                            temptotal = temptotal + System.Convert.ToDouble(drr[7].ToString());
                        }
                        else
                        {
                            //MessageBox.Show("2" + drr[7].ToString());
                            temptotal = temptotal - System.Convert.ToDouble(drr[7].ToString());
                        }
                    }

                }
                totaldate.Add(thedate);
                totaltotal.Add(temptotal);
                //MessageBox.Show(thedate+"="+temptotal.ToString());

            }

            int i = 0;
            double nowtmp = 0;
            foreach (string nowdate in totaldate)
            {
                nowtmp = (double)totaltotal[i++];

                temptotalamount += nowtmp;


                if (temptotalamount > moneymax)
                {
                    moneymax = temptotalamount;

                }
                if (temptotalamount < moneymin)
                {
                    moneymin = temptotalamount;

                }
                amountdatecount++;
                //MessageBox.Show(temptotalamount.ToString());
            }
            //MessageBox.Show("getdatemoney");
        }


        private void paintdata()
        {
            //MessageBox.Show(amountmin.ToString());
            Graphics g = pictureBox1.CreateGraphics();

            pw = pictureBox1.Width;
            ph = pictureBox1.Height;
            double mxx = pw / moneydatecount;
            double myy = ph / (moneymax - moneymin);
            double axx = pw / amountdatecount;
            double ayy = ph  / (amountmax - amountmin);
            MessageBox.Show("pw=" + pw.ToString() + "ph=" + ph.ToString());
            MessageBox.Show(moneymax.ToString() + "-" + moneymin.ToString());
            MessageBox.Show(amountmax.ToString() + "-" + amountmin.ToString());
            MessageBox.Show("mxx=" + mxx.ToString() + "myy" + myy.ToString() + "axx=" + axx.ToString() + "ayy=" + ayy.ToString());
            
            
            MessageBox.Show("moneymax="+moneymax.ToString()+"moneymin="+moneymin.ToString());
            int zeroheight = (int)(-moneymin * myy);
            
            
            //画0轴


            Pen axisp = new Pen(Color.Black);
            Point axisp1 = new Point(standarx(0),standary(zeroheight));
            
            Point axisp2 = new Point(standarx((int)(pw*mxx)),standary(zeroheight));
            g.DrawLine(axisp, axisp1, axisp2);

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paintdata();
        }

        private int standarx(int inx)
        {
            

            int standrx = 0;
            standrx = inx + 30;

            return standrx;

        }
        private int standary(int iny)
        {
            int standry = 0;
            standry = ph - 30 - iny;
            return standry;
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    g.CopyFromScreen(this.Location.X + pictureBox1.Location.X + 4, this.Location.Y + pictureBox1.Location.Y + 28, 0, 0, pictureBox1.Size);
        //    bitmap.Save("D:\\pic.bmp");   
        //}

        
        

    }

    //public class NoSortHashtable : Hashtable
    //{
    //    private ArrayList keys = new ArrayList();

    //    public NoSortHashtable()
    //    {
    //    }


    //    public override void Add(object key, object value)
    //    {
    //        base.Add(key, value);
    //        keys.Add(key);
    //    }

    //    public override ICollection Keys
    //    {
    //        get
    //        {
    //            return keys;
    //        }
    //    }

    //    public override void Clear()
    //    {
    //        base.Clear();
    //        keys.Clear();
    //    }

    //    public override void Remove(object key)
    //    {
    //        base.Remove(key);
    //        keys.Remove(key);
    //    }
    //    public override IDictionaryEnumerator GetEnumerator()
    //    {
    //        return base.GetEnumerator();
    //    }

    //}

}

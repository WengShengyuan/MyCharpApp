using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class HiddenNode
    {
        #region 数据声明区域

        public double _inputValue;
        

        public double[] _w;
        

        public double _a;
        
        public int _id;
        

        public double nrate;

        private double[] historyDeltaW;

        #endregion

        public HiddenNode(int iid, double[] ixs,double[] iws, double ia, double[] iw, double inrate)
        {
            this._id = iid;
            this._a = ia;
            this._w = iw;
            this.nrate = inrate;
            this.countNodeValue(ixs, iws);

            

            //动量法才用得到，用来记录上一次的权值
            historyDeltaW = new double[iw.Length];
            for (int w = 0; w < iw.Length; w++)
            {
                historyDeltaW[w] = 0;
            }
        }

        //计算该节点的输出值
        public void countNodeValue(double[] ixs, double[] iws)
        {
            this._inputValue=getFValue(getSigma(ixs, iws));
        }
        //更新该节点的Wjk权重
        public void updateW(double[] ek)
        {
            for (int s = 0; s < this._w.Length; s++)
            {
                //更新每个节点wjk的权重
                this._w[s] += this.nrate * this._inputValue * ek[s] + 0.00001 * (this._w[s] - this.historyDeltaW[s]);
                this.historyDeltaW[s] = this._w[s];
            }
        }
        //更新该节点的A
        public void updatea(double[] ek, double sigmawjkek)
        {
            for (int s = 0; s < this._w.Length; s++)
            {
                this._a += this.nrate + this._inputValue * (1 - this._inputValue) * sigmawjkek;
            }
        }



        private double getFValue(double inSigma)
        {
            double rFV = 0;
            rFV = 1 / (1 + Math.Exp(-inSigma + this._a));
            return rFV;
        }

        private double getSigma(double[] ixs, double[] iws)
        {
            double rsig = 0;
            int sc = ixs.Length;
            for (int i = 0; i < sc; i++)
            {
                rsig += ixs[i] * iws[i];
            }
            //rsig = rsig - this._a;
            return rsig;
        }
        
    }
}

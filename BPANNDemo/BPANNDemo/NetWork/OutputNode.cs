using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class OutputNode
    {
        #region 数据声明区域

        public double _inputValue;
        
        public double _b;
        

        public int _id;
        

        public double _diffE;
        


        #endregion

        public OutputNode(int iid, double[] ixs, double[] iws, double ib)
        {
            this._id = iid;
            this._b = ib;
            this._inputValue = countNodeValue(ixs, iws);
        }
        private double countNodeValue(double[] ixs, double[] iws)
        {
            double riv = 0;
            int tempLength = ixs.Length;
            for (int j = 0; j < tempLength; j++)
            {
                riv += ixs[j] * iws[j];
            }
            riv = riv - this._b;
            return riv;
        }


        //更新节点的输出值
        public void updateInputValue(double[] ixs, double[] iws)
        {
            this._inputValue = countNodeValue(ixs, iws);
        }
    }
}

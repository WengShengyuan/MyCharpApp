using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class InputNode
    {
        #region 数据声明区域

        public double _inputValue;
        


        public double[] _w;
        private double[] historyW;

        public int _id;
        

        public double nrate;

        #endregion

        public InputNode(int iid, double iv, double[] iw, double inrate)
        {
            this._id = iid;
            this._inputValue = iv;
            this._w = iw;
            this.nrate = inrate;
            historyW = new double[this._w.Length];
            for (int w = 0; w < this._w.Length; w++)
            {
                historyW[w] = 0;
            }
        }

        //更新该节点Wij
        public void updatewij(HiddenLayer il, double[] ek)
        {
            for (int s = 0; s < this._w.Length; s++)
            {
                this._w[s] += this.nrate * il._nodeList[s]._inputValue * (1 - il._nodeList[s]._inputValue) * this._inputValue * getSigmaWjkEk(il, ek) + 0.00001*(this._w[s]-this.historyW[s]);
                this.historyW[s] = this._w[s];
            }
        }

        private double getSigmaWjkEk(HiddenLayer il, double[] ek)
        {
            double rsigma=0;
            foreach (KeyValuePair<int, HiddenNode> hdn in il._nodeList)
            {
                for (int f = 0; f < ek.Length; f++)
                {
                    rsigma += hdn.Value._w[f] * ek[f];
                }
            }
            return rsigma;
        }
    }
}

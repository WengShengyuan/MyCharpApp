using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class HiddenLayer
    {
        #region 数据声明区域
        public Dictionary<int, HiddenNode> _nodeList;
        Random ran = new Random();
        NetWork parentNetWork;

        

        public int _nodeCount;


        public int _nextNodeCount;


        public double _defaulta;


        public double nrate;
        #endregion

        public HiddenLayer(int inc, int innc,double da, NetWork pNW, double inrate)
        {
            //为输入层赋值
            this._nodeCount = inc;
            this._nextNodeCount = innc;
            this.parentNetWork = pNW;
            this._defaulta = da;
            this.nrate = inrate;
            this.genNodeList(parentNetWork.ipl);
        }



        //更新所有节点的输出值
        public void updateInputValue(InputLayer il)
        {
            double[] tempixs = getixs(il);
            foreach (KeyValuePair<int, HiddenNode> hdn in this._nodeList)
            {
                hdn.Value.countNodeValue(tempixs, getiws(il, hdn.Key));
            }
        }
        //更新所有节点的Wjk值
        public void updatewjk(double[] ek)
        {
            foreach (KeyValuePair<int, HiddenNode> hdn in _nodeList)
            {
                hdn.Value.updateW(ek);
            }
        }
        //更新节点的A值
        public void updatea(double[] ek)
        {
            double sigmaWjkEk = this.getSigmaWjkEk(this, ek);
            foreach (KeyValuePair<int, HiddenNode> hdn in _nodeList)
            {
                hdn.Value.updatea(ek, sigmaWjkEk);
            }
        }



        private double getSigmaWjkEk(HiddenLayer il, double[] ek)
        {
            double rsigma = 0;
            foreach (KeyValuePair<int, HiddenNode> hdn in il._nodeList)
            {
                for (int f = 0; f < ek.Length; f++)
                {
                    rsigma += hdn.Value._w[f] * ek[f];
                }
            }
            return rsigma;
        }

        private double[] getixs(InputLayer il)
        {
            //获取输入层各点的值，组成数组
            double[] returnixs = new double[il._nodeCount];
            for (int i = 0; i < il._nodeCount; i++)
            {
                returnixs[i] = il._nodeList[i]._inputValue;
            }
            return returnixs;
        }

        private double[] getiws(InputLayer il, int j)
        {
            //针对第j的点，获取输入层的权重，组成数组
            double[] returniws = new double[il._nodeCount];
            for (int i = 0; i < il._nodeCount; i++)
            {
                returniws[i] = il._nodeList[i]._w[j];
            }
            return returniws;
        }

        private double[] genIws()
        {
            double[] tempd = new double[this._nextNodeCount];
            
            for (int i = 0; i < this._nextNodeCount; i++)
            {
                //tempd[i] = 0.5;
                tempd[i] = ran.NextDouble();
            }
            return tempd;
        }

        private void genNodeList(InputLayer il)
        {
            //获取输入层的数值计算，并生成输出值
            _nodeList = new Dictionary<int, HiddenNode>();
            for (int j = 0; j < this._nodeCount; j++)
            {
                //实例化的时候计算节点的初始值，计算一次相乘求和
                HiddenNode hn = new HiddenNode(j, getixs(il), getiws(il, j), this._defaulta, genIws(), this.nrate);
                this._nodeList.Add(j, hn);
            }

        }

    }
}

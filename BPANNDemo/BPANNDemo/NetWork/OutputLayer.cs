using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class OutputLayer
    {
        #region 数据声明区域
        NetWork parentNetWork;

        public int _nodeCount;


        public Dictionary<int, OutputNode> _nodeList;
        

        public double _defaultb;
        

        

        #endregion

        public OutputLayer(int onc,double db, NetWork pNW)
        {
            this._nodeCount = onc;
            this.parentNetWork = pNW;
            this._defaultb = db;
            this.getNodeList(this.parentNetWork.hdl);
        }

        //计算各节点的误差
        public void updateNodeDiff(double[] ek)
        {
            foreach (KeyValuePair<int, OutputNode> opn in _nodeList)
            {
                opn.Value._diffE = ek[opn.Key];
            }
        }
        //更新各节点B值
        public void updateb(double[] ek)
        {
            foreach (KeyValuePair<int, OutputNode> opn in _nodeList)
            {
                opn.Value._b += ek[opn.Key];
            }
        }
        //更新节点的值
        public void updateInputValue(HiddenLayer il)
        {
            double[] tempixs = getixs(il);
            foreach( KeyValuePair<int, OutputNode> opn in _nodeList )
            {
                opn.Value.updateInputValue(tempixs, getiws(il, opn.Key));
            }
        }


        private void getNodeList(HiddenLayer il)
        {
            //生成节点
            _nodeList = new Dictionary<int, OutputNode>();
            for (int k = 0; k < this._nodeCount; k++)
            {
                OutputNode on = new OutputNode(k, getixs(il), getiws(il, k), this._defaultb);
                _nodeList.Add(k, on);
            }
        }


        private double[] getixs(HiddenLayer il)
        {
            //获取隐藏层各点的值，组成数组
            double[] returnixs = new double[il._nodeCount];
            for (int i = 0; i < il._nodeCount; i++)
            {
                returnixs[i] = il._nodeList[i]._inputValue;
            }
            return returnixs;
        }


        private double[] getiws(HiddenLayer il, int k)
        {
            //针对第j的点，获取输入层的权重，组成数组
            double[] returniws = new double[il._nodeCount];
            for (int i = 0; i < il._nodeCount; i++)
            {
                returniws[i] = il._nodeList[i]._w[k];
            }
            return returniws;
        }

    }
}

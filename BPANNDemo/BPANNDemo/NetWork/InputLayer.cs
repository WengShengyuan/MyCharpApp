using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BPANNDemo
{
    class InputLayer
    {
        #region 数据声明区域
        Random ran = new Random();
        NetWork parentNetWork;

        public Dictionary<int, InputNode> _nodeList;
        

        public int _nodeCount;
        

        public int _nextNodeCount;
        

        public double nrate;
        #endregion

        public InputLayer(double[] ixs,  int inc, int innc, NetWork pNW, double inrate)
        {
            //为输入层赋值
            this.parentNetWork = pNW;
            this._nodeCount = inc;
            this._nextNodeCount = innc;
            this.nrate = inrate;
            this._nodeList = new Dictionary<int, InputNode>();
            for (int i = 0; i < _nodeCount; i++)
            {
                InputNode inode=new InputNode(i, ixs[i], genIws(), nrate);
                this._nodeList.Add(i,inode);
            }

        }


        //更新节点的Wij值
        public void updatewij(HiddenLayer il, double[] ek)
        {
            foreach (KeyValuePair<int, InputNode> ipn in this._nodeList)
            {
                ipn.Value.updatewij(parentNetWork.hdl, ek);
            }
        }
        //向InputNode输入数据
        public void InputData(double[] ixs)
        {
            foreach( KeyValuePair<int, InputNode> ipn in this._nodeList )
            {
                ipn.Value._inputValue = ixs[ipn.Key];
            }

            
            
        }

        private double[] genIws()
        {
            //生成随机数
            double[] tempd = new double[this._nextNodeCount];
            for (int i = 0; i < this._nextNodeCount; i++)
            {
                tempd[i] = ran.NextDouble();
                //tempd[i] = 0.5;
            }
            return tempd;
        }
    }
}

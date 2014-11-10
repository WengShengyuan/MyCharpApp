using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace log4NetTestDemo
{
    //log4Net工具类
    public class log4NetHelper
    {
        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log= log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        public static void WriteLog(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;



[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace log4NetTestDemo
{
    //log4Net工具类
    public class logHelper
    {
        
        
        
        public static void Error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            //记录错误日志
            log.Error("error", ex);
        }

        public static void Fatal(Type t, Exception ex)
        {
            
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            //记录严重错误
            log.Fatal("fatal", ex);
        }

        public static void Info(Type t, string info)
        {
            
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            //记录一般信息
            log.Info(info);
        }
        public static void Debug(Type t, string debug)
        {
            
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            //记录调试信息
            log.Debug(debug);
        }
        public static void Warn(Type t, string warn)
        {
            
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            //记录警告信息
            log.Warn(warn);
        }

        
        
        
        
         
        
    }
}

using log4net.Config;
using System;
using System.IO;

namespace ManagementProject
{
    public class Logger
    {
        public static void Initialize()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }
        public static void Fatal(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal("Exception", ex);
        }
        public static void Fatal(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Fatal("Exception", ex);
        }
        public static void Fatal(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Fatal(msg);
        }

        public static void Fatal(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Fatal(msg);
        }
        public static void Error(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Exception", ex);
        }
        public static void Error(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Error("Exception", ex);
        }
        public static void Error(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Error(msg);
        }

        public static void Error(Type t,string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error(msg);
        }
        public static void Warn(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn("Exception", ex);
        }
        public static void Warn( Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Warn("Exception", ex);
        }
        public static void Warn(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Warn(msg);
        }
        public static void Warn(Type t,string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Warn(msg);
        }
        public static void Debug(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Debug("Exception", ex);
        }
        public static void Debug( Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Debug("Exception", ex);
        }
        public static void Debug(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Debug(msg);
        }
        public static void Debug(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Debug(msg);
        }

        public static void Info(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info("Exception", ex);
        }
        public static void Info( Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Info("Exception", ex);
        }
        public static void Info(Type t, string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Info(msg);
        }
        public static void Info(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("Default");
            log.Info(msg);
        }

    }
}

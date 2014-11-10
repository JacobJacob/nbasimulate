namespace Web.Util
{
    using log4net;
    using System;

    public class Logger
    {
        private static ILog logger;

        static Logger()
        {
            if (logger == null)
            {
                logger = log4net.LogManager.GetLogger("DEFAULT");
            }
            logger.Info("=======本次日志开始=========");
        }

        public static void Debug(string errMsg)
        {
        }

        public static void Error(string errMsg)
        {
            logger.Error(errMsg);
        }

        public static void Info(string errMsg)
        {
            logger.Info(errMsg);
        }

        public static void Warn(string errMsg)
        {
            logger.Warn(errMsg);
        }

        internal static bool IsDebugEnabled
        {
            get
            {
                return logger.IsDebugEnabled;
            }
        }

        internal static bool IsErrorEnabled
        {
            get
            {
                return logger.IsErrorEnabled;
            }
        }

        internal static bool IsInfoEnabled
        {
            get
            {
                return logger.IsInfoEnabled;
            }
        }

        internal static bool IsWarnEnabled
        {
            get
            {
                return logger.IsWarnEnabled;
            }
        }
    }
}


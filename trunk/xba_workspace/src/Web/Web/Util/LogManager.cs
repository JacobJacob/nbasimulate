using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Util
{
    internal class LogManager
    {
        private static string logPath = string.Empty;
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory;
                }
                return logPath;
            }
            set { logPath = value; }
        }

        private static string logFielPrefix = string.Empty;

        public static string LogFielPrefix
        {
            get { return logFielPrefix; }
            set { logFielPrefix = value; }
        }

        public static void WriteLog(string logFile, string msg)
        {
            try
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(
                    LogPath + LogFielPrefix + logFile + " " +
                    DateTime.Now.ToString("yyyyMMdd") + ".Log"
                    );
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                sw.Close();
            }
            catch
            { }
        }

        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }
    }

    public enum LogFile
    {
        Trace,
        Warning,
        Error,
        SQL
    }
}

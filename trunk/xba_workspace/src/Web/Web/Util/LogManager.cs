namespace Web.Util
{
    using System;
    using System.IO;

    internal class LogManager
    {
        private static string logFielPrefix = string.Empty;
        private static string logPath = string.Empty;

        public static void WriteLog(string logFile, string msg)
        {
            try
            {
                StreamWriter writer = File.AppendText(LogPath + LogFielPrefix + logFile + " " + DateTime.Now.ToString("yyyyMMdd") + ".Log");
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
                writer.Close();
            }
            catch
            {
            }
        }

        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }

        public static string LogFielPrefix
        {
            get
            {
                return logFielPrefix;
            }
            set
            {
                logFielPrefix = value;
            }
        }

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
            set
            {
                logPath = value;
            }
        }
    }
}


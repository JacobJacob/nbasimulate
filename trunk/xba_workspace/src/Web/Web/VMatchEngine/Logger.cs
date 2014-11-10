namespace Web.VMatchEngine
{
    using System;
    using System.IO;

    internal class Logger
    {
        private static string logFielPrefix = string.Empty;
        private static string logPath = string.Empty;

        public static string GetLogPath(int matchType, int matchId, string suffix)
        {
            try
            {
                string str = Convert.ToString(matchId);
                string str2 = string.Concat(new object[] { "type_", matchType, @"\", str.Substring(0, 1), @"\", str.Substring(1, 2) });
                string path = LogPath + str2;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return (str2 + @"\" + str + "." + suffix);
            }
            catch
            {
                return "error.log";
            }
        }

        public static string GetTotalLogPath(int matchType, int matchId)
        {
            try
            {
                string str = "match_total";
                string path = LogPath + str;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return string.Concat(new object[] { str, @"\", matchType, "_", matchId, ".json" });
            }
            catch
            {
                return "error.log";
            }
        }

        public static void WriteLog(string logFile, string msg)
        {
            try
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg;
                string text1 = LogPath + LogFielPrefix + logFile;
                StreamWriter writer = File.AppendText(LogPath + LogFielPrefix + logFile);
                writer.WriteLine(str);
                writer.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        public static void WriteLog(LogFile logFile, string msg)
        {
            WriteLog(logFile.ToString(), msg);
        }

        public static void WriteRawLog(string logFile, string msg)
        {
            try
            {
                string str = msg;
                string text1 = LogPath + LogFielPrefix + logFile;
                StreamWriter writer = File.AppendText(LogPath + LogFielPrefix + logFile);
                writer.WriteLine(str);
                writer.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
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
                    logPath = @"E:\xba_log\";
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


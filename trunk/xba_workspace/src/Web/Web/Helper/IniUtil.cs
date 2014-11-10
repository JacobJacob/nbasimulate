namespace Web.Helper
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class IniUtil
    {
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder retVal = new StringBuilder(0x400);
                GetPrivateProfileString(Section, Key, NoText, retVal, 0x400, iniFilePath);
                return retVal.ToString();
            }
            return string.Empty;
        }
    }
}


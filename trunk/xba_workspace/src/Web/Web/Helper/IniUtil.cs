using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Web.Helper
{
	class IniUtil
	{

        #region API函数声明

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section,string key,
            string def,StringBuilder retVal,int size,string filePath);


        #endregion

        #region 读Ini文件

        public static string ReadIniData(string Section,string Key,string NoText,string iniFilePath)
        {
            if(File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section,Key,NoText,temp,1024,iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
	}
}

using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Web.Helper
{
	class IniUtil
	{

        #region API��������

        [DllImport("kernel32")]//����ȡ���ַ����������ĳ���
        private static extern long GetPrivateProfileString(string section,string key,
            string def,StringBuilder retVal,int size,string filePath);


        #endregion

        #region ��Ini�ļ�

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

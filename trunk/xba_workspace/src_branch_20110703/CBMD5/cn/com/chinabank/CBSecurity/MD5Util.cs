namespace cn.com.chinabank.CBSecurity
{
    using System;
    using System.Web.Security;

    public class MD5Util
    {
        public static string getMD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToString();
        }

        public static bool verifyMD5(string str, string v_md5str)
        {
            return getMD5(str).Equals(v_md5str);
        }
    }
}


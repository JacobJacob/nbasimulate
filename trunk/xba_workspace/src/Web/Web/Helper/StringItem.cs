namespace Web.Helper
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Security;
    using Web;
    using Web.DBData;

    public class StringItem
    {
        public static string FormatDate(DateTime datIn, string strFormat)
        {
            try
            {
                string zero = GetZero(datIn.Year);
                string str3 = GetZero(Convert.ToInt32(datIn.Year.ToString().Substring(2, 2)));
                string replacement = (strFormat.IndexOf("yyyy") == -1) ? str3 : zero;
                string str5 = GetZero(datIn.Month);
                string str6 = GetZero(datIn.Day);
                string str7 = GetZero(datIn.Hour);
                string str8 = GetZero(datIn.Minute);
                string str9 = GetZero(datIn.Second);
                return Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(Regex.Replace(strFormat, "dd", str6), "MM", str5), "y{1,4}", replacement), "hh", str7), "mm", str8), "ss", str9);
            }
            catch (Exception exception)
            {
                exception.ToString();
                return "";
            }
        }

        public static string Get51laURL()
        {
            if (ServerParameter.strCopartner == "CGA")
            {
                return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/0/155/27/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/0/155/27/ystat.gif\"></a></noscript>";
            }
            if (ServerParameter.strCopartner == "ZHW")
            {
                return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/0/374/412/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/0/374/412/ystat.gif\"></a></noscript>";
            }
            if (ServerParameter.strCopartner == "51WAN")
            {
                return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/1/41/206/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/1/41/206/ystat.gif\"></a></noscript>";
            }
            if (ServerParameter.strCopartner == "17173")
            {
                return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/1/41/324/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/1/41/324/ystat.gif\"></a></noscript>";
            }
            if (ServerParameter.strCopartner == "DUNIU")
            {
                return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/1/204/216/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/1/204/216/ystat.gif\"></a></noscript>";
            }
            return "<script type=\"text/javascript\" src=\"http://js.tongji.yahoo.com.cn/0/155/28/ystat.js\"></script><noscript><a href=\"http://js.tongji.yahoo.com.cn\"><img src=\"http://js.tongji.yahoo.com.cn/0/155/28/ystat.gif\"></a></noscript>";
        }

        public static string GetBuyCoinURL()
        {
            if (ServerParameter.strCopartner == "CGA")
            {
                return "http://vip.cga.com.cn/game/XBA/";
            }
            if (ServerParameter.strCopartner == "51WAN")
            {
                return "http://pay.51wan.com/";
            }
            if (ServerParameter.strCopartner == "17173")
            {
                return "http://web.17173.com/xba/bangzhu/chongzhi.html";
            }
            if (ServerParameter.strCopartner == "DUNIU")
            {
                return "http://cn.mmoabc.com/buy/";
            }
            if (ServerParameter.strCopartner == "ZHW")
            {
                return "http://xba.game.china.com/PayChinaShow.aspx";
            }
            if (ServerParameter.strCopartner == "DW")
            {
                return "http://xba.duowan.com/0803/m_70641288537.html";
            }
            return (DBLogin.URLString(40) + "PayCenter.aspx");
        }

        public static string GetChsNum(int intNum)
        {
            if (intNum == 0)
            {
                return "零";
            }
            if (intNum == 1)
            {
                return "一";
            }
            if (intNum == 2)
            {
                return "二";
            }
            if (intNum == 3)
            {
                return "三";
            }
            if (intNum == 4)
            {
                return "四";
            }
            if (intNum == 5)
            {
                return "五";
            }
            if (intNum == 6)
            {
                return "六";
            }
            if (intNum == 7)
            {
                return "七";
            }
            if (intNum == 8)
            {
                return "八";
            }
            if (intNum == 9)
            {
                return "九";
            }
            if (intNum == 10)
            {
                return "十";
            }
            return "";
        }

        public static string GetEngNum(int intNum)
        {
            return intNum.ToString("c").Replace("￥", "").Replace("$", "").Replace(".00", "");
        }

        public static double getFloat(double dblNumber, int intLength)
        {
            double num = Math.Pow(10.0, (double) intLength);
            int num2 = (int) (dblNumber * num);
            dblNumber = Convert.ToDouble((double) (((double) num2) / num));
            return dblNumber;
        }

        public static string GetHelperURL(string strPage)
        {
            if (ServerParameter.strCopartner == "CGA")
            {
                return "http://xba.cga.com.cn/gonglue1.html";
            }
            return ("Handbook/Main.htm?Page=" + strPage);
        }

        public static string GetHtmlEncode(string strIn)
        {
            HttpContext current = HttpContext.Current;
            StringWriter output = new StringWriter();
            current.Server.HtmlEncode(strIn, output);
            return output.ToString();
        }

        public static string GetKFURL()
        {
            Global.drParameter["QQService"].ToString().Trim();
            return Global.drParameter["QQService"].ToString().Trim();
        }

        public static string GetLogoutURL()
        {
            if (!ServerParameter.blnUseServer)
            {
                return "LogOut.aspx";
            }
            if (ServerParameter.strCopartner == "CGA")
            {
                return ("http://xba.cga.com.cn/Logout.aspx?S=web" + ServerParameter.intGameCategory + ".xba.cga.com.cn");
            }
            if (ServerParameter.strCopartner == "ZHW")
            {
                return ("http://xba.game.china.com/Logout.aspx?S=xbaw" + ServerParameter.intGameCategory + ".game.china.com");
            }
            if (ServerParameter.strCopartner == "17173")
            {
                if (ServerParameter.intGameCategory == 2)
                {
                    return ("http://xba.web.17173.com/Logout.aspx?S=xbam" + ServerParameter.intGameCategory + ".web.17173.com");
                }
                return ("http://xba.web.17173.com/Logout.aspx?S=xbaw" + ServerParameter.intGameCategory + ".web.17173.com");
            }
            if (ServerParameter.strCopartner == "51WAN")
            {
                if (ServerParameter.intGameCategory == 2)
                {
                    return ("http://xba.51wan.com/Logout.aspx?S=xbam" + ServerParameter.intGameCategory + ".51wan.com");
                }
                return ("http://xba.51wan.com/Logout.aspx?S=xbaw" + ServerParameter.intGameCategory + ".51wan.com");
            }
            if (ServerParameter.strCopartner == "DUNIU")
            {
                return ("http://xbas.mmoabc.com/Logout.aspx?S=xbaw" + ServerParameter.intGameCategory + ".mmoabc.com");
            }
            if (ServerParameter.strCopartner == "DW")
            {
                return ("http://xbas.xba.duowan.com/Logout.aspx?S=xbam" + ServerParameter.intGameCategory + ".xba.duowan.com");
            }
            return (DBLogin.URLString(40) + "MemberCenter.aspx?IsLogout=1");
        }

        public static string GetPercent(int intA, int intB, int intNum)
        {
            if (intB == 0)
            {
                return "--";
            }
            float num = (((float) intA) / ((float) intB)) * 100f;
            return (num.ToString("F" + intNum) + "%");
        }

        public static string GetQQURL(string strQQ)
        {
            if (ServerParameter.strCopartner == "CGA")
            {
                return ("<a target=\"blank\" href=\"tencent://message/?uin=" + strQQ + "&Site=篮球经理浩方区&Menu=yes\"><img border=\"0\" SRC=\"http://wpa.qq.com/pa?p=1:" + strQQ + ":3\" alt=\"有事您找我！\"></a>");
            }
            return ("<a href=\"http://wpa.qq.com/msgrd?v=1&uin=" + strQQ + "&site=篮球经理官方区&menu=yes\" target=\"blank\"><img alt=\"有事您Q我！\" src=\"http://wpa.qq.com/pa?p=1:" + strQQ + ":7\" border=\"0\" width=\"71\" height=\"24\"></a>");
        }

        public static string GetRookieURL(int intRookieOpIndex)
        {
            switch (intRookieOpIndex)
            {
                case 0:
                    return "RookieMain_I.aspx?Type=WELCOME";

                case 1:
                    return "RookieMain_I.aspx?Type=REGCLUB";

                case 2:
                    return "RookieMain_P.aspx?Type=ASSIGNDEVCLUB";

                case 3:
                    return "RookieMain_I.aspx?Type=ENDDEVCREATE";

                case 4:
                    return "RookieMain_P.aspx?Type=ASSIGNCLUB";

                case 5:
                    return "RookieMain_I.aspx?Type=END";
            }
            return "Main.aspx";
        }

        public static string GetShortName(string strIn, int intLength, string strEnd)
        {
            return ("<a style='cursor:hand;' title='" + strIn + "'>" + GetShortString(strIn, intLength, strEnd) + "</a>");
        }

        public static string GetShortName(string strIn, int intLength, string strEnd, byte bytPayType)
        {
            if (bytPayType == 1)
            {
                return ("<a style='cursor:hand;color:#ff6600' title='" + strIn + "</br>[会员]'>" + GetShortString(strIn, intLength, strEnd) + "</a>");
            }
            return ("<a style='cursor:hand;' title='" + strIn + "'>" + GetShortString(strIn, intLength, strEnd) + "</a>");
        }

        public static string GetShortString(string strIn, int intLength)
        {
            string str = strIn;
            int num = 0;
            int length = 0;
            bool flag = false;
            length = 0;
            while (length < str.Length)
            {
                if (num > intLength)
                {
                    flag = true;
                    break;
                }
                if (str[length] > '\x0080')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                length++;
            }
            try
            {
                str = str.Substring(0, length);
                if (flag)
                {
                    str = str + "...";
                }
            }
            catch
            {
                str = strIn;
            }
            return str;
        }

        public static string GetShortString(string strIn, int intLength, string strEnd)
        {
            string str = strIn;
            int num = 0;
            int length = 0;
            bool flag = false;
            length = 0;
            while (length < str.Length)
            {
                if (num > intLength)
                {
                    flag = true;
                    break;
                }
                if (str[length] > '\x0080')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
                length++;
            }
            try
            {
                str = str.Substring(0, length);
                if (flag)
                {
                    str = str + strEnd;
                }
            }
            catch
            {
                str = strIn;
            }
            return str;
        }

        public static string GetShortURLName(string strIn, int intLength, string strEnd, string strURL)
        {
            return ("<a href='" + strURL + "' style='cursor:hand;' title='" + strIn + "' target='_blank'>" + GetShortString(strIn, intLength, strEnd) + "</a>");
        }

        public static int GetStrLength(string strIn)
        {
            byte[] bytes = new ASCIIEncoding().GetBytes(strIn);
            int num = 0;
            for (int i = 0; i <= (bytes.Length - 1); i++)
            {
                if (bytes[i] == 0x3f)
                {
                    num++;
                }
                num++;
            }
            return num;
        }

        public static string GetValidWords(string strIn)
        {
            string str = BTPParameterManager.GetBadWord().Trim();
            strIn = SetValidWord(strIn);
            Cuter cuter = new Cuter(str);
            for (int i = 0; i < cuter.GetSize(); i++)
            {
                strIn = strIn.Replace(cuter.GetCuter(i), "**");
            }
            return strIn;
        }

        public static string GetXMLTrueBody(string strIn)
        {
            return strIn.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;");
        }

        public static string GetZero(int intIn)
        {
            return (((intIn < 10) ? "0" : "") + intIn);
        }

        public static bool HasInvalidWord(string strIn)
        {
            string str = BTPParameterManager.GetBadWord().Trim();
            if (str != "")
            {
                Cuter cuter = new Cuter(str);
                for (int i = 0; i < cuter.GetSize(); i++)
                {
                    string str2 = cuter.GetCuter(i);
                    if (strIn.IndexOf(str2) != -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsNumber(object obj)
        {
            try
            {
                obj = Convert.ToInt64(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidContent(string strIn, int intMin, int intMax)
        {
            int strLength = GetStrLength(strIn);
            if ((strLength < intMin) || (strLength > intMax))
            {
                return false;
            }
            if (!IsValidWord(strIn))
            {
                return false;
            }
            if (HasInvalidWord(strIn))
            {
                return false;
            }
            if (GetHtmlEncode(strIn) != strIn)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(string strIn)
        {
            if (!IsValidContent(strIn, 4, 50))
            {
                return false;
            }
            return Regex.IsMatch(strIn, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        public static bool IsValidLogin(string strIn)
        {
            return Regex.IsMatch(strIn, "^[0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_]{4,16}$");
        }

        public static bool IsValidMsg(string strIn)
        {
            return (((strIn.IndexOf("<") == -1) && (strIn.IndexOf(">") == -1)) && ((strIn.IndexOf("'") == -1) && (strIn.IndexOf("|") == -1)));
        }

        public static bool IsValidMsg(string strIn, int intMin, int intMax)
        {
            int strLength = GetStrLength(strIn);
            if ((strLength < intMin) || (strLength > intMax))
            {
                return false;
            }
            return (((strIn.IndexOf("<") == -1) && (strIn.IndexOf(">") == -1)) && ((strIn.IndexOf("'") == -1) && (strIn.IndexOf("|") == -1)));
        }

        public static bool IsValidName(string strIn, int intMin, int intMax)
        {
            strIn = strIn.Replace("\r\n", "").Replace("\n", "");
            int strLength = GetStrLength(strIn);
            if ((strLength < intMin) || (strLength > intMax))
            {
                return false;
            }
            foreach (byte num2 in Encoding.ASCII.GetBytes(strIn))
            {
                if (num2 < 0x20)
                {
                    return false;
                }
            }
            if (!IsValidWord(strIn))
            {
                return false;
            }
            if (HasInvalidWord(strIn))
            {
                return false;
            }
            if (GetHtmlEncode(strIn) != strIn)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidNameIn(string strIn, int intMin, int intMax)
        {
            strIn = strIn.Replace("\r\n", "").Replace("\n", "");
            int strLength = GetStrLength(strIn);
            if ((strLength < intMin) || (strLength > intMax))
            {
                return false;
            }
            string pattern = @"^(?:[\u4e00-\u9fa5]*\w*\.*\s*)+$";
            if (!Regex.IsMatch(strIn, pattern))
            {
                return false;
            }
            foreach (byte num2 in Encoding.ASCII.GetBytes(strIn))
            {
                if (num2 < 0x20)
                {
                    return false;
                }
            }
            if (!IsValidWord(strIn))
            {
                return false;
            }
            if (HasInvalidWord(strIn))
            {
                return false;
            }
            if (GetHtmlEncode(strIn) != strIn)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidSign(string strCode, string strSign, string sKey)
        {
            strCode = strCode + sKey;
            return (FormsAuthentication.HashPasswordForStoringInConfigFile(strCode, "md5").ToLower() == strSign);
        }

        public static bool IsValidWord(string strIn)
        {
            return ((((strIn.IndexOf(",") == -1) && (strIn.IndexOf("\"") == -1)) && ((strIn.IndexOf("<") == -1) && (strIn.IndexOf(">") == -1))) && (((strIn.IndexOf("'") == -1) && (strIn.IndexOf("|") == -1)) && (strIn.IndexOf("&") == -1)));
        }

        public static bool IsValidWord(string strIn, int intMin, int intMax)
        {
            int strLength = GetStrLength(strIn);
            if ((strLength < intMin) || (strLength > intMax))
            {
                return false;
            }
            return ((((strIn.IndexOf(",") == -1) && (strIn.IndexOf("\"") == -1)) && ((strIn.IndexOf("<") == -1) && (strIn.IndexOf(">") == -1))) && (((strIn.IndexOf("'") == -1) && (strIn.IndexOf("|") == -1)) && (strIn.IndexOf("&") == -1)));
        }

        public static bool IsValidXPassword(string strIn)
        {
            return Regex.IsMatch(strIn, "^[0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ]{3,9}$");
        }

        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] buffer = new byte[pToDecrypt.Length / 2];
            for (int i = 0; i < (pToDecrypt.Length / 2); i++)
            {
                int num2 = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 0x10);
                buffer[i] = (byte) num2;
            }
            provider.Key = Encoding.ASCII.GetBytes(sKey);
            provider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            new StringBuilder();
            return Encoding.Default.GetString(stream.ToArray());
        }

        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
            provider.Key = Encoding.ASCII.GetBytes(sKey);
            provider.IV = Encoding.ASCII.GetBytes(sKey);
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write);
            stream2.Write(bytes, 0, bytes.Length);
            stream2.FlushFinalBlock();
            StringBuilder builder = new StringBuilder();
            foreach (byte num in stream.ToArray())
            {
                builder.AppendFormat("{0:X2}", num);
            }
            builder.ToString();
            return builder.ToString();
        }

        public static string SetValidWord(string strOld)
        {
            return strOld.Replace("'", "’").Replace(",", "，").Replace("<S", "＜Ｓ").Replace("<s", "＜ｓ").Replace("|", "｜").Replace("&", "＆");
        }
    }
}


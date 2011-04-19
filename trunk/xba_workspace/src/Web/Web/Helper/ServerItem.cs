namespace Web.Helper
{
    using LoginParameter;
    using System;
    using System.Web;
    using Web;
    using Web.DBData;

    public class ServerItem
    {
        public static string GameImgName(int intCategory)
        {
            bool flag;
            if (intCategory != 0)
            {
                flag = (bool) BTPGameManager.GetGameRow(DBLogin.ConnString(intCategory))["CantReg"];
            }
            else
            {
                flag = false;
            }
            switch (intCategory)
            {
                case 0:
                    return "Main.gif";

                case 1:
                    if (!flag)
                    {
                        return "server_01.gif";
                    }
                    return "server_01_full.gif";

                case 2:
                    if (!flag)
                    {
                        return "server_02.gif";
                    }
                    return "server_02_full.gif";

                case 3:
                    if (!flag)
                    {
                        return "server_03.gif";
                    }
                    return "server_03_full.gif";

                case 4:
                    if (!flag)
                    {
                        return "server_51edu.gif";
                    }
                    return "server_51edu.gif";

                case 40:
                    return "Main.gif";
            }
            return null;
        }

        public static string ToFrameForumURL(int intGameCategory, string strUserName, string strPassword, int intUserID)
        {
            strUserName = StringItem.MD5Encrypt(strUserName, Global.strMD5Key);
            strPassword = StringItem.MD5Encrypt(strPassword, Global.strMD5Key);
            string str = StringItem.MD5Encrypt(intUserID.ToString(), Global.strMD5Key);
            return string.Concat(new object[] { DBLogin.URLString(intGameCategory), "FrameForum.aspx?tueenfjkgaducjeoslj422854786345654dcujnnvasduggkl=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&ue3tu23ce3vdmbv=", strUserName, "&fdvvyi44398hdfjvcxlhqrugfbvvvaswhfdljjvbxnvjedghasjdbvsklsdg=", RandomItem.rnd.Next(0x3e8, 0x270f), "&stey86yi2jfdace=", strPassword, "&jjaiayfjialeyangfjiow66542388751ddfyykanyo=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&tti28hyj58k99n=", str });
        }

        public static string ToFrameTopicURL(int intGameCategory, string strUserName, string strPassword, int intUserID, string strURL)
        {
            strUserName = StringItem.MD5Encrypt(strUserName, Global.strMD5Key);
            strPassword = StringItem.MD5Encrypt(strPassword, Global.strMD5Key);
            string str = StringItem.MD5Encrypt(intUserID.ToString(), Global.strMD5Key);
            return string.Concat(new object[] { DBLogin.URLString(intGameCategory), strURL, "&Page=1&tueenfjkgaducjeoslj422854786345654dcujnnvasduggkl=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&ue3tu23ce3vdmbv=", strUserName, "&fdvvyi44398hdfjvcxlhqrugfbvvvaswhfdljjvbxnvjedghasjdbvsklsdg=", RandomItem.rnd.Next(0x3e8, 0x270f), "&stey86yi2jfdace=", strPassword, "&jjaiayfjialeyangfjiow66542388751ddfyykanyo=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&tti28hyj58k99n=", str });
        }

        public static void ToOtherServer(int intGameCategory, string strUserName, string strPassword, string strOtherItem)
        {
            strUserName = StringItem.MD5Encrypt(strUserName, Global.strMD5Key);
            strPassword = StringItem.MD5Encrypt(strPassword, Global.strMD5Key);
            HttpContext current = HttpContext.Current;
            string url = string.Concat(new object[] { DBLogin.URLString(intGameCategory), "ServerJump.aspx?tueenfjkgaducjeoslj422854786345654dcujnnvasduggkl=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&ue3tu23ce3vdmbv=", strUserName, "&fdvvyi44398hdfjvcxlhqrugfbvvvaswhfdljjvbxnvjedghasjdbvsklsdg=", RandomItem.rnd.Next(0x3e8, 0x270f), "&stey86yi2jfdace=", strPassword, "&", strOtherItem });
            current.Response.Redirect(url);
        }

        public static string ToOtherServerURL(int intGameCategory, string strUserName, string strPassword, string strOtherItem)
        {
            strUserName = StringItem.MD5Encrypt(strUserName, Global.strMD5Key);
            strPassword = StringItem.MD5Encrypt(strPassword, Global.strMD5Key);
            return string.Concat(new object[] { DBLogin.URLString(intGameCategory), "ServerJump.aspx?tueenfjkgaducjeoslj422854786345654dcujnnvasduggkl=", RandomItem.rnd.Next(0x2710, 0x1869f) * RandomItem.rnd.Next(100, 0x3e7), "&ue3tu23ce3vdmbv=", strUserName, "&fdvvyi44398hdfjvcxlhqrugfbvvvaswhfdljjvbxnvjedghasjdbvsklsdg=", RandomItem.rnd.Next(0x3e8, 0x270f), "&stey86yi2jfdace=", strPassword, "&", strOtherItem });
        }
    }
}


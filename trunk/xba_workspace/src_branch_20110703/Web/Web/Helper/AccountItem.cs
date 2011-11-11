namespace Web.Helper
{
    using System;
    using Web.DBData;

    public class AccountItem
    {
        public static string GetNickCard(string strNickName, bool blnSex, int intAge, string strCity, string strProvince, string strDevLev, DateTime dtActiveTime, int intPayType, string strClubName, long lngOnlyPoint)
        {
            long num = lngOnlyPoint / 0x15L;
            long num2 = num / 6L;
            long num3 = num2 / 6L;
            string str = "";
            string str2 = "";
            string str3 = "";
            for (int i = 0; i < (num3 % 6L); i++)
            {
                str = str + "<img src=\"" + SessionItem.GetImageURL() + "szw1.gif\" width=\"12\" height=\"12\">";
            }
            for (int j = 0; j < (num2 % 6L); j++)
            {
                str2 = str2 + "<img src=\"" + SessionItem.GetImageURL() + "szw2.gif\" width=\"12\" height=\"12\">";
            }
            for (int k = 0; k < (num % 6L); k++)
            {
                str3 = str3 + "<img src=\"" + SessionItem.GetImageURL() + "szw3.gif\" width=\"12\" height=\"12\">";
            }
            string text1 = string.Concat(new object[] { str, str2, str3, "<br><img src=\"", SessionItem.GetImageURL(), "blanks.gif\" width=\"12\" height=\"4\"><br>积　　分：", lngOnlyPoint, "<br><img src=\"", SessionItem.GetImageURL(), "blanks.gif\" width=\"12\" height=\"6\"><br>" });
            if ((strClubName == "") || (strClubName == null))
            {
                strClubName = "暂无联盟";
            }
            string strSex = "";
            string strPayType = "";
            if (blnSex)
            {
                strSex = "MM";
            }
            else
            {
                strSex = "GG";
            }

            if (intPayType == 1)
            {
                strPayType = "尊贵会员";
            }
            else
            {
                strPayType = "普通玩家";
            }

            string strTitle = string.Concat(new object[] { "经 理 名：", strNickName, "&#10;性　　别：", strSex, "&#10;年　　龄：", intAge, "&#10;省　　份：", strProvince, "&#10;城　　市：", strCity, "&#10;联赛等级：", strDevLev, "&#10;所在联盟：", strClubName, "&#10;最后登录：", StringItem.FormatDate(dtActiveTime, "yyyy-MM-dd"), "&#10;王者积分：", lngOnlyPoint, "&#10玩家类型：", strPayType});

            return string.Concat(new object[] {"<img id='NickCard'src='Images/NickCard.gif' alt='", strTitle, "' border='0' width=12 hight=12>"});
        }

        public static string GetNickLogoCard(string strNickName, bool blnSex, int intAge, string strCity, string strProvince, string strDevLev, DateTime dtActiveTime, int intPayType, string strClubName, string strClubLogo, long lngOnlyPoint)
        {
            long num = lngOnlyPoint / 0x15L;
            long num2 = num / 6L;
            long num3 = num2 / 6L;
            string str = "";
            string str2 = "";
            string str3 = "";
            for (int i = 0; i < (num3 % 6L); i++)
            {
                str = str + "<img src=\"" + SessionItem.GetImageURL() + "szw1.gif\" width=\"12\" height=\"12\">";
            }
            for (int j = 0; j < (num2 % 6L); j++)
            {
                str2 = str2 + "<img src=\"" + SessionItem.GetImageURL() + "szw2.gif\" width=\"12\" height=\"12\">";
            }
            for (int k = 0; k < (num % 6L); k++)
            {
                str3 = str3 + "<img src=\"" + SessionItem.GetImageURL() + "szw3.gif\" width=\"12\" height=\"12\">";
            }
            string text1 = string.Concat(new object[] { str, str2, str3, "<br><img src=\"", SessionItem.GetImageURL(), "blanks.gif\" width=\"12\" height=\"4\"><br>积　　分：", lngOnlyPoint, "<br><img src=\"", SessionItem.GetImageURL(), "blanks.gif\" width=\"12\" height=\"6\"><br>" });
            if ((strClubName == "") || (strClubName == null))
            {
                strClubName = "暂无联盟";
            }
            if (blnSex)
            {
                if (intPayType == 1)
                {
                    return string.Concat(new object[] { 
                        "<img id='NickCard' alt='<span style=\"width:146px;\"><strong>经理名片</strong><br><br>经 理 名：<font color=\"ff6600\">", strNickName, "</font><br>性　　别：女<br>年　　龄：", intAge, "<br>省　　份：", strProvince, "<br>城　　市：", strCity, "<br>联赛等级：", strDevLev, "<br>所在联盟：", strClubName, "<br>最后登录：", StringItem.FormatDate(dtActiveTime, "yyyy-MM-dd"), "<br>王者积分：", lngOnlyPoint, 
                        "</span>'src='", strClubLogo, "' border='0' width=46 hight=46>"
                     });
                }
                return string.Concat(new object[] { 
                    "<img id='NickCard' alt='<span style=\"width:146px;\"><strong>经理名片</strong><br><br>经 理 名：", strNickName, "<br>性　　别：女<br>年　　龄：", intAge, "<br>省　　份：", strProvince, "<br>城　　市：", strCity, "<br>联赛等级：", strDevLev, "<br>所在联盟：", strClubName, "<br>最后登录：", StringItem.FormatDate(dtActiveTime, "yyyy-MM-dd"), "<br>王者积分：", lngOnlyPoint, 
                    "</span>'src='", strClubLogo, "' border='0' width=46 hight=46>"
                 });
            }
            if (intPayType == 1)
            {
                return string.Concat(new object[] { 
                    "<img id='NickCard' alt='<span style=\"width:146px;\"><strong>经理名片</strong><br><br>经 理 名：<font color=\"ff6600\">", strNickName, "</font><br>性　　别：男<br>年　　龄：", intAge, "<br>省　　份：", strProvince, "<br>城　　市：", strCity, "<br>联赛等级：", strDevLev, "<br>所在联盟：", strClubName, "<br>最后登录：", StringItem.FormatDate(dtActiveTime, "yyyy-MM-dd"), "<br>王者积分：", lngOnlyPoint, 
                    "</span>' src='", strClubLogo, "' border='0' width=46 hight=46>"
                 });
            }
            return string.Concat(new object[] { 
                "<img id='NickCard' alt='<span style=\"width:146px;\"><strong>经理名片</strong><br><br>经 理 名：", strNickName, "<br>性　　别：男<br>年　　龄：", intAge, "<br>省　　份：", strProvince, "<br>城　　市：", strCity, "<br>联赛等级：", strDevLev, "<br>所在联盟：", strClubName, "<br>最后登录：", StringItem.FormatDate(dtActiveTime, "yyyy-MM-dd"), "<br>王者积分：", lngOnlyPoint, 
                "</span>' src='", strClubLogo, "'border='0' width=46 hight=46>"
             });
        }

        public static string GetNickNameInfo(int intUserID, string strNickName, string strTarget)
        {
            return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" target=\"", strTarget, "\">", strNickName, "</a></div>" });
        }

        public static string GetNickNameInfo(int intUserID, string strNickName, string strTarget, bool blnSex)
        {
            if (blnSex)
            {
                return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" target=\"", strTarget, "\"><font color='#ff005a'>", strNickName, "</font></a></div>" });
            }
            return string.Concat(new object[] { "<div class=\"DIVPlayerName\"><a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" target=\"", strTarget, "\"><font color='blue'>", strNickName, "</font></a></div>" });
        }

        public static string GetNickNameInfo(int intUserID, string strNickName, string strTarget, int intLength)
        {
            return string.Concat(new object[] { "<a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" title=\"", strNickName, "\" target=\"", strTarget, "\">", StringItem.GetShortString(strNickName, intLength, "."), "</a>" });
        }

        public static string GetNickNameInfoA(int intUserID, string strNickName, string strTarget, bool blnSex)
        {
            if (blnSex)
            {
                return string.Concat(new object[] { "<a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" target=\"", strTarget, "\"><font color='#ff005a'>", strNickName, "</font></a>" });
            }
            return string.Concat(new object[] { "<a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" target=\"", strTarget, "\"><font color='blue'>", strNickName, "</font></a>" });
        }

        public static string GetWealthName(int intWealth)
        {
            if ((intWealth < 0) || (intWealth >= 20))
            {
                if ((intWealth >= 20) && (intWealth < 80))
                {
                    return "<font color='#333333'>[贫民]</font>";
                }
                if ((intWealth >= 80) && (intWealth < 200))
                {
                    return "<font color='#00d034'>[平民]</font>";
                }
                if ((intWealth >= 200) && (intWealth < 0x3e8))
                {
                    return "<font color='#0070DD'>[富人]</font>";
                }
                if ((intWealth >= 0x3e8) && (intWealth < 0x2710))
                {
                    return "<font color='#A335EE'>[富翁]</font>";
                }
                if (intWealth >= 0x2710)
                {
                    return "<font color='#FF8000'>[富豪]</font>";
                }
            }
            return "<font color='#666666'>[乞丐]</font>";
        }

        public static bool IsBadUser(int intUserID, int intCategory)
        {
            return BTPAccountManager.InBadUserByUserID(intUserID, intCategory);
        }

        public static int RookieOpIndex(int intUserID)
        {
            Cuter cuter = new Cuter(BTPAccountManager.GetUserRookieOpByUserID(intUserID));
            return cuter.GetIndex("0");
        }

        public static int SetRookieOp(int intUserID, int intRookieOpIndex)
        {
            Cuter cuter = new Cuter(BTPAccountManager.GetUserRookieOpByUserID(intUserID));
            if (cuter.GetIndex("0") == intRookieOpIndex)
            {
                cuter.SetCuter(intRookieOpIndex, "1");
                BTPAccountManager.SetUserRookieOpByUserID(intUserID, cuter.ToString());
                return intRookieOpIndex;
            }
            return cuter.GetIndex("0");
        }
    }
}


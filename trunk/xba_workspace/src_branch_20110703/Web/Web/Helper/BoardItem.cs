namespace Web.Helper
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using Web;
    using Web.DBData;

    public class BoardItem
    {
        public static bool CanView(int intUserID, string strBoardID)
        {
            string str = ROOTUserManager.GetBoardIDByUserID(intUserID).Trim();
            if (str.IndexOf("|") != -1)
            {
                foreach (string str2 in str.Split(new char[] { '|' }))
                {
                    if (str2 == strBoardID)
                    {
                        return true;
                    }
                }
            }
            else if (str == strBoardID)
            {
                return true;
            }
            return false;
        }

        public static string GetBoardJump(string strBoardIDIn)
        {
            string str = "";
            DataTable boardByTopID = ROOTBoardManager.GetBoardByTopID("");
            str = str + "<select id='sltBoardJump' name='sltBoardJump' onchange='ForumJump(this)'>";
            if (boardByTopID != null)
            {
                foreach (DataRow row in boardByTopID.Rows)
                {
                    string str2 = row["BoardID"].ToString().Trim();
                    string str3 = row["Name"].ToString().Trim();
                    if (str2.Length <= 3)
                    {
                        str3 = "--" + str3 + "--";
                    }
                    else
                    {
                        str3 = "　" + str3;
                    }
                    str = str + "<option value='" + str2 + "'";
                    if (strBoardIDIn == str2)
                    {
                        str = str + " selected";
                    }
                    str = str + ">" + str3 + "</option>";
                }
            }
            //boardByTopID.Close();
            return (str + "</select>");
        }

        public static string GetCategory(int intCategory)
        {
            if (intCategory == 0)
            {
                return "";
            }
            if (intCategory == 1)
            {
                return "[ 锁定 ]";
            }
            if (intCategory == 2)
            {
                return "[ 认证 ]";
            }
            return "[ 关闭 ]";
        }

        public static string GetForumLogo(string strLogo)
        {
            return ("<img src='" + SessionItem.GetImageURL() + "Forum/" + strLogo + "' border=0 align=absmiddle>");
        }

        public static string GetForumMasterNickName(string strMaster)
        {
            string[] strArray = strMaster.Split(new char[] { '|' });
            strMaster = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                strMaster = strMaster + strArray[i];
                if (i < (strArray.Length - 1))
                {
                    strMaster = strMaster + ",";
                }
            }
            DataTable masterNickName = ROOTBoardManager.GetMasterNickName(strMaster);
            if (masterNickName != null)
            {
                strMaster = "<select style='width:150px;'><option> ==== 论坛斑竹 ==== </option>";
                int count = masterNickName.Rows.Count;
                for (int j = 0; j < count; j++)
                {
                    string str = masterNickName.Rows[j]["NickName"].ToString().Trim();
                    strMaster = strMaster + "<option>" + str + "</option>";
                }
                strMaster = strMaster + "</select>";
            }
            return strMaster;
        }

        public static string GetFrameIsVoteContent(string strBoardID, int intTopicID, string strContent)
        {
            TagReader reader = new TagReader(strContent);
            strContent = "";
            string str = string.Concat(new object[] { "<Script>function Vote_Click(){\tvar strURL=\"FrameVote.aspx?BoardID=", strBoardID, "&TopicID=", intTopicID, "&Parameter=\";" });
            ArrayList items = reader.GetItems("Vote");
            for (int i = 0; i < items.Count; i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "\tvar blnHasCheck_i", i, "=false;" });
                string line = (string) items[i];
                TagReader reader2 = new TagReader(line);
                string str3 = (string) reader2.GetItems("Title")[0];
                obj2 = strContent;
                strContent = string.Concat(new object[] { obj2, "<b>", i + 1, ". ", str3, "</b><br>" });
                ArrayList list2 = reader2.GetItems("Item");
                for (int j = 0; j < list2.Count; j++)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "\tif(document.getElementById(\"", GetRadioID(i, j), "\").checked){strURL+=\"", GetRadioID(i, j), "|\";blnHasCheck_i", i, "=true;}" });
                    string str6 = (string) list2[j];
                    TagReader reader3 = new TagReader(str6);
                    string str4 = (string) reader3.GetItems("Content")[0];
                    string str5 = (string) reader3.GetItems("VoteCount")[0];
                    string str7 = strContent;
                    strContent = str7 + "　" + GetItemTag(i, j) + str4 + "　<font color='red'>" + str5 + "</font> 票<br>";
                }
                obj2 = str;
                str = string.Concat(new object[] { obj2, "if(!blnHasCheck_i", i, "){alert(\"请对第", i + 1, "个调查进行投票！\");return;}" });
            }
            str = str + "\twindow.location=strURL;}</Script>";
            strContent = strContent + "<br>" + ((string) reader.GetItems("Introduction")[0]) + "<br><p align=\"center\"><input type=\"button\" id=\"btnVote\" value=\"投票\" onclick=\"Vote_Click()\"></p>";
            strContent = "<font style='line-height:150%'>" + strContent + "</font>" + str;
            return strContent;
        }

        public static string GetFramePageInfo(string strInfo, string strBoardIDIn)
        {
            string str = "";
            if (strBoardIDIn == "0")
            {
                return (str + "<font color='#333333'>您的位置：</font> <a href='FrameForum.aspx'>论坛首页</a> &gt; " + strInfo);
            }
            int num = strBoardIDIn.Length / 3;
            str = str + "<font color='#333333'>您的位置：</font> <a href='FrameForum.aspx'>论坛首页</a> ";
            string str2 = "";
            for (int i = 1; i < (num + 1); i++)
            {
                str2 = str2 + strBoardIDIn.Substring(0, i * 3);
                if (i < num)
                {
                    str2 = str2 + "|";
                }
            }
            string[] strArray = str2.Split(new char[] { '|' });
            for (int j = 0; j < strArray.Length; j++)
            {
                string str5;
                string strBoardID = strArray[j];
                string boardNameByBoardID = ROOTBoardManager.GetBoardNameByBoardID(strBoardID);
                if (j == 0)
                {
                    str5 = str;
                    str = str5 + "&gt; <a href='FrameBoardList.aspx?BoardID=" + strBoardID + "&Page=1'>" + boardNameByBoardID + "</a>";
                }
                else
                {
                    str5 = str;
                    str = str5 + " &gt; <a href='FrameBoard.aspx?BoardID=" + strBoardID + "&Page=1'>" + boardNameByBoardID + "</a>";
                }
            }
            if (strInfo != "")
            {
                str = str + " &gt; " + strInfo;
            }
            return str;
        }

        public static string GetIndexEnd()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr>");
            builder.Append("<td colspan='3' align='center' valign='middle' height='25'>");
            builder.Append("<strong>广告合作</strong> 电话：86-532-86667257  邮箱：<a href='mailto:xbamanager@gmail.com' style='color:#FFFFFF' >xbamanager@gmail.com</a>");
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("<tr>");
            builder.Append("<td colspan='3' align='center' valign='middle' height='25'>");
            builder.Append("2005 XBA篮球经理 增值电信业务经营许可证 鲁B2-20051048号 客服中心");
            builder.Append("</td>");
            builder.Append("</tr>");
            return builder.ToString();
        }

        public static string GetIndexHead(int intUserID, string strUserName, string strPassword)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table width='100%' height='103' border='0' cellpadding='0' cellspacing='0'  >");
            builder.Append("<tr>");
            builder.Append("<td width='225' rowspan='2'><img src='Images/web/logo.gif' width='225' height='103'></td>");
            builder.Append("<td width='30' height='77'></td>");
            builder.Append("<td width='68' valign='bottom' align=right><a href=''><img src='Images/web/menu_00.GIF' alt='篮球经理游戏首页' width='58' height='30' border='0'></a></td>");
            builder.Append("<td width='126' valign='bottom' align=right><a href='" + DBLogin.URLString(-1) + "Board.aspx?BoardID=001006&Page=1' target='_blank'><img src='Images/web/menu_01.gif' alt='游戏新手指南' width='98' height='30' border='0'></a></td>");
            builder.Append("<td width='126' valign='bottom'align=right><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='Images/web/menu_02.gif' alt='游戏指南' width='98' height='30' border='0'></a></td>");
            builder.Append("<td width='126' valign='bottom'align=right><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='Images/web/menu_03.gif' alt='游戏常见问题' width='98' height='30' border='0'></a></td>");
            builder.Append("<td width='126' valign='bottom'align=right><a style='cursor:hand;' onclick=NewJieWin();><img src='Images/web/menu_05.gif' alt='游戏截图' width='98' height='30' border='0'></a></td>");
            builder.Append("<td width='126' valign='bottom'align=right><a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target=\"_blank\"><img src='Images/web/menu_06.gif' alt='篮球论坛，NBA/火箭队姚明/街头篮球讨论区' width='98' height='30' border='0'></a></td>");
            builder.Append("<td></td>");
            builder.Append("</tr>");
            builder.Append("<tr>");
            builder.Append("<td height='26' colspan='9'>");
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("</table>");
            return builder.ToString();
        }

        public static string GetIndexLeft()
        {
            object obj2;
            int accountCount = BTPAccountManager.GetAccountCount();
            int onlineCount = DTOnlineManager.GetOnlineCount();
            int num3 = ((onlineCount * RandomItem.rnd.Next(100, 110)) / 100) + RandomItem.rnd.Next(5, 10);
            string str = "<table width=\"170\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            if ((DateTime.Now > DateTime.Today.AddHours((double) Global.intStartUpdate)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
            {
                string str4 = str;
                str = str4 + "<tr><td height='21' align='left' style='padding-left:4px;'>第1轮</td><td>联赛开始</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第2轮</td><td>大杯赛报名</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第3轮</td><td>大杯赛开赛</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第14轮</td><td>上半赛季结束</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>休赛日</td><td>联赛休赛</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>休赛日</td><td>队内选拔</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第15轮</td><td>大杯赛报名</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第16轮</td><td>大杯赛开赛</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第17轮</td><td>千人杯赛报名</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第17轮</td><td>千人杯赛开赛</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>第26轮</td><td>联赛结束</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='21' align='left' style='padding-left:4px;'>联赛后</td><td>联赛升降级</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='20' align='left' style='padding-left:4px;'>联赛后</td><td>新赛季赛程</td></tr><tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr><tr><td height='10'></td></tr>";
            }
            else
            {
                DataTable clubTableByDevCode = BTPDevManager.GetClubTableByDevCode("");
                if (clubTableByDevCode != null)
                {
                    int num7 = 1;
                    foreach (DataRow row in clubTableByDevCode.Rows)
                    {
                        string str2;
                        string str3;
                        int count = clubTableByDevCode.Rows.Count;
                        int num5 = (int) row["Win"];
                        int intClubID = (int) row["ClubID"];
                        if (intClubID == 0)
                        {
                            str2 = "空缺";
                        }
                        else
                        {
                            str2 = BTPClubManager.GetClubNameByClubID(intClubID, 5, "Index", 13);
                        }
                        if (num7 < 3)
                        {
                            str3 = "#fce5d2";
                        }
                        else if (num7 > (count - 4))
                        {
                            str3 = "#e7e7e7";
                        }
                        else
                        {
                            str3 = "";
                        }
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "<tr class='BarContent' bgColor='", str3, "' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width=\"25\" align=\"center\" height=\"20\"><font color='#660066'>", num7, "</font></td><td width=\"110\" align=\"left\">", str2, "</td><td width=\"35\" align=\"center\">", num5, "胜</td></tr>" });
                        str = str + "<tr><td colspan='3' height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif'></td></tr>";
                        num7++;
                    }
                }
            }
            obj2 = str;
            return (string.Concat(new object[] { obj2, "<tr><td style=\"padding-left:30px;\" colspan='3' height=\"19\"><b>注册球队</b>：", accountCount, "</td></tr><tr><td style=\"padding-left:30px;\" colspan='3' height=\"19\"><b>在线经理</b>：", onlineCount, "</td></tr><tr><td style=\"padding-left:30px;\" colspan='3' height=\"19\"><b>论坛游客</b>：", num3, "</td></tr>" }) + "</table>");
        }

        public static string GetIsVoteContent(string strBoardID, int intTopicID, string strContent)
        {
            TagReader reader = new TagReader(strContent);
            strContent = ((string) reader.GetItems("Overview")[0]) + "<br><br>";
            string str = string.Concat(new object[] { "<Script>function Vote_Click(){\tvar strURL=\"Vote.aspx?BoardID=", strBoardID, "&TopicID=", intTopicID, "&Parameter=\";" });
            ArrayList items = reader.GetItems("Vote");
            for (int i = 0; i < items.Count; i++)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "\tvar blnHasCheck_i", i, "=false;" });
                string line = (string) items[i];
                TagReader reader2 = new TagReader(line);
                string str3 = (string) reader2.GetItems("Title")[0];
                obj2 = strContent;
                strContent = string.Concat(new object[] { obj2, "<b>", i + 1, ". ", str3, "</b><br>" });
                ArrayList list2 = reader2.GetItems("Item");
                for (int j = 0; j < list2.Count; j++)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "\tif(document.getElementById(\"", GetRadioID(i, j), "\").checked){strURL+=\"", GetRadioID(i, j), "|\";blnHasCheck_i", i, "=true;}" });
                    string str6 = (string) list2[j];
                    TagReader reader3 = new TagReader(str6);
                    string str4 = (string) reader3.GetItems("Content")[0];
                    string str5 = (string) reader3.GetItems("VoteCount")[0];
                    string str7 = strContent;
                    strContent = str7 + "　" + GetItemTag(i, j) + str4 + "　<font color='red'>" + str5 + "</font> 票<br>";
                }
                obj2 = str;
                str = string.Concat(new object[] { obj2, "if(!blnHasCheck_i", i, "){alert(\"请对第", i + 1, "个调查进行投票！\");return;}" });
            }
            str = str + "\twindow.location=strURL;}</Script>";
            strContent = strContent + "<br>" + ((string) reader.GetItems("Introduction")[0]) + "<br><p align=\"center\"><input type=\"button\" id=\"btnVote\" value=\"投票\" onclick=\"Vote_Click()\"></p>";
            strContent = "<font style='line-height:150%'>" + strContent + "</font>" + str;
            return strContent;
        }

        public static string GetItemTag(int i, int j)
        {
            string str = GetNumToABC(j) + ". ";
            return (string.Concat(new object[] { "<input type=\"radio\" id=\"", GetRadioID(i, j), "\" name=\"i", i, "\"> " }) + str);
        }

        public static string GetMasterNickName(string strMaster)
        {
            string[] strArray = strMaster.Split(new char[] { '|' });
            strMaster = "";
            for (int i = 0; i < strArray.Length; i++)
            {
                strMaster = strMaster + strArray[i];
                if (i < (strArray.Length - 1))
                {
                    strMaster = strMaster + ",";
                }
            }
            DataTable masterNickName = ROOTBoardManager.GetMasterNickName(strMaster);
            if (masterNickName != null)
            {
                strMaster = "";
                int count = masterNickName.Rows.Count;
                for (int j = 0; j < count; j++)
                {
                    string str = masterNickName.Rows[j]["NickName"].ToString().Trim();
                    strMaster = strMaster + str;
                    if (j < (count - 1))
                    {
                        strMaster = strMaster + ", ";
                    }
                }
            }
            return strMaster;
        }

        public static string GetMsgSettingArea(string strUserName, string strPassword)
        {
            return ("<a href='Main_P.aspx?Type=MODIFYCLUB' target='Main'><img src='" + SessionItem.GetImageURL() + "Setting.gif' width='16' height='16' border='0'></a>");
        }

        public static string GetNumToABC(int i)
        {
            return "";
        }

        public static string GetPageInfo(string strInfo, string strBoardIDIn)
        {
            string str = "";
            if (strBoardIDIn == "0")
            {
                return (str + "<font color='#333333'>您的位置：</font> <a href='Forum.aspx'>论坛首页</a> &gt; " + strInfo);
            }
            int num = strBoardIDIn.Length / 3;
            str = str + "<font color='#333333'>您的位置：</font> <a href='Forum.aspx'>论坛首页</a> ";
            string str2 = "";
            for (int i = 1; i < (num + 1); i++)
            {
                str2 = str2 + strBoardIDIn.Substring(0, i * 3);
                if (i < num)
                {
                    str2 = str2 + "|";
                }
            }
            string[] strArray = str2.Split(new char[] { '|' });
            for (int j = 0; j < strArray.Length; j++)
            {
                string str5;
                string strBoardID = strArray[j];
                string boardNameByBoardID = ROOTBoardManager.GetBoardNameByBoardID(strBoardID);
                if (j == 0)
                {
                    str5 = str;
                    str = str5 + "&gt; <a href='BoardList.aspx?BoardID=" + strBoardID + "&Page=1'>" + boardNameByBoardID + "</a>";
                }
                else
                {
                    str5 = str;
                    str = str5 + " &gt; <a href='Board.aspx?BoardID=" + strBoardID + "&Page=1'>" + boardNameByBoardID + "</a>";
                }
            }
            if (strInfo != "")
            {
                str = str + " &gt; " + strInfo;
            }
            return str;
        }

        public static string GetPageIntro(int intUserID, string strNickName, string strUserName, string strPassword)
        {
            string str;
            if (intUserID < 0)
            {
                return ("<a href=''>首页</a> | <a href='Forum.aspx'>论坛</a> | <a href='" + DBLogin.URLString(0) + "Register2.aspx'>注册通行证</a> | <a href=''>登录</a> | <a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\">经理手册</a> | <a style='cursor:hand;' onclick=NewFAQWin('01');>常见问题</a> | <a href='Search.aspx?Type=0&Page=0'>搜索</a>");
            }
            if (ServerParameter.intGameCategory == 0)
            {
                str = "<a href='" + DBLogin.URLString(0) + "MemberCenter.aspx'>通行证管理</a>";
            }
            else
            {
                str = "<a href='Main.aspx'>返回游戏</a>";
            }
            return ("<a href=''>首页</a> | <font class='ForumTime'>" + strNickName + "</font> 您好！ | " + str + " | <a href='AlterInfo.aspx'>设置</a> | <a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\">经理手册</a> | <a style='cursor:hand;' onclick=NewFAQWin('01');>常见问题</a> | <a href='Logout.aspx'>退出</a>");
        }

        public static string GetRadioID(int i, int j)
        {
            return string.Concat(new object[] { "i", i, "_j", j, "" });
        }

        public static string GetSetting(string strUserName, string strPassword)
        {
            return ("<a href='Main_P.aspx?Type=MODIFYCLUB' target='Main'><img alt='俱乐部的基本设置，可以更改俱乐部名称、标志、队服等。' src='" + SessionItem.GetImageURL() + "Setting.gif' width='16' height='16' border='0'></a>");
        }

        public static string GetTopicLogo(string strLogo)
        {
            return ("<img src='" + SessionItem.GetImageURL() + "Forum/TopciLogo/" + strLogo + "' width='12' height='12' border=0 align=absmiddle>");
        }

        public static string GetUnionBBSPageIntro(string strType, int intUnionID)
        {
            switch (strType)
            {
                case "UNION":
                    return string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_01.GIF' border='0' height='24' width='76' border='0'><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", intUnionID, "&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_02.GIF' border='0' height='24' width='75' border='0'></a><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_03.GIF' border='0' height='24' width='89' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });

                case "MYUNION":
                    return ("<a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_C_01.GIF' border='0' height='24' width='76' border='0'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_02.GIF' border='0' height='24' width='75' border='0'><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX'><img src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_C_03.GIF' border='0' height='24' width='89' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");

                case "UNIONCUP":
                    return string.Concat(new object[] { "<a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_01.GIF' border='0' height='24' width='76' border='0'></a><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", intUnionID, "&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_02.GIF' border='0' height='24' width='75' border='0'></a><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_03.GIF' border='0' height='24' width='89' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
            }
            return string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_01.GIF' border='0' height='24' width='76' border='0'><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", intUnionID, "&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_02.GIF' border='0' height='24' width='75' border='0'></a><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX'><img src='", SessionItem.GetImageURL(), "MenuCard/Union/Union_C_03.GIF' border='0' height='24' width='89' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
        }

        public static string GetUnionBBSPageIntro1(bool blnIsTopicAdd, int intUnionID, string strBoardName, string strType, int intUserID)
        {
            string str = "";
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
            int num = (byte) accountRowByUserID["UnionCategory"];
            int num2 = (int) accountRowByUserID["UnionID"];
            string str2 = "";
            if (strType == "UNION")
            {
                str2 = "UNIONINTRO";
            }
            else if (strType == "MYUNION")
            {
                str2 = "UNIONINFO";
            }
            else
            {
                str2 = "UNIONINTRO";
            }
            if (!blnIsTopicAdd)
            {
                if (num2 == intUnionID)
                {
                    str = string.Concat(new object[] { 
                        "<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td height='25' colspan='2'><a href='Union.aspx?Type=", strType, "&Kind=", str2, "&UnionID=", intUnionID, "&Page=1'>联盟介绍</a>&nbsp;|&nbsp;<font color='red'>联盟论坛</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=REPUTATION&Page=1'>功勋录</a>&nbsp|&nbsp;<a href='Union.aspx?Type=", strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=DONATEWEALTH&Page=1'>捐赠游戏币</a>"
                     });
                    if (num == 1)
                    {
                        str = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    object obj2 = str;
                    return string.Concat(new object[] { obj2, "</td></tr><tr><td height='25' style='padding-left:4px' width='80%'><a href='UnionBoard.aspx?Type=", strType, "&UnionID=", intUnionID, "&Page=1'>返回帖子列表</td><td align='right'><a href='UnionTopicAdd.aspx?Type=", strType, "&UnionID=", intUnionID, "'><img src='", SessionItem.GetImageURL(), "UnionNewTitle.gif' width='87' height='20' border='0'></a></td></tr></table>" });
                }
                return string.Concat(new object[] { "<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td height='25'><a href='Union.aspx?Type=UNION&Kind=UNIONINTRO&UnionID=", intUnionID, "&Page=1'>联盟介绍</a>&nbsp;|&nbsp;<font color='red'>联盟论坛</font></td></tr><tr><td height='25' style='padding-left:4px'><a href='UnionBoard.aspx?Type=", strType, "&UnionID=", intUnionID, "&Page=1'>返回帖子列表</a>" });
            }
            if (num2 == intUnionID)
            {
                str = string.Concat(new object[] { 
                    "<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td height='25'><a href='Union.aspx?Type=", strType, "&Kind=", str2, "&UnionID=", intUnionID, "&Page=1'>联盟介绍</a>&nbsp;|&nbsp;<font color='red'>联盟论坛</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=REPUTATION&Page=1'>功勋录</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, 
                    "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", strType, "&Kind=DONATEWEALTH&Page=1'>捐赠游戏币</a>"
                 });
                if (num == 1)
                {
                    str = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                }
                return string.Concat(new object[] { "</td></tr><tr><td height=''25><a href='UnionBoard.aspx?Type=", strType, "&UnionID=", intUnionID, "&Page=1'>返回帖子列表</a>" });
            }
            return string.Concat(new object[] { "<table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td height='25' width='80%'><a href='Union.aspx?Type=", strType, "&Kind=", str2, "&UnionID=", intUnionID, "&Page=1'>联盟介绍</a>&nbsp;|&nbsp;<font color='red'>联盟论坛</font></td></tr><tr><td height='25' style='padding-left:4px'><a href='UnionBoard.aspx?Type=", strType, "&UnionID=", intUnionID, "&Page=1'>返回帖子列表</a>" });
        }

        public static string HeadString()
        {
            return ("<table width='1002' border='0' cellspacing='0' cellpadding='0'><tr><td width='202' height='71' align='center' valign='middle'><img src='" + SessionItem.GetImageURL() + "logo.JPG' width='151' height='68'></td><td width='800' height='71' align='center' valign='middle'><a href='http://www.g248.com/' target='_blank'><img src='" + SessionItem.GetImageURL() + "770_60.gif' width='770' height='60' border='0'></a></td></tr></table>");
        }

        public static bool IsBoardMaster(int intUserID, string strMaster)
        {
            if (IsMainMaster(intUserID))
            {
                return true;
            }
            if (strMaster.IndexOf("|") != -1)
            {
                foreach (string str in strMaster.Split(new char[] { '|' }))
                {
                    if (Convert.ToInt32(str) == intUserID)
                    {
                        return true;
                    }
                }
            }
            else if (intUserID == Convert.ToInt32(strMaster))
            {
                return true;
            }
            return false;
        }

        public static bool IsMainMaster(int intUserID)
        {
            int[] numArray = new int[] { 1, 0x1d, 2, 0x5fe, 0x61ea };
            foreach (int num in numArray)
            {
                if (num == intUserID)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsUnionBoardMaster(int intUserID, int intUnionID)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
            int num = (byte) accountRowByUserID["UnionCategory"];
            int num2 = (int) accountRowByUserID["UnionID"];
            return (((num == 1) || (num == 2)) && (intUnionID == num2));
        }

        public static string OnlineString(string strNickName, int intType)
        {
            int count = ROOTUserManager.GetCount();
            DTOnlineManager.GetOnlineRow();
            string str = "<table width='100%' border='0' cellpadding='0' cellspacing='0' bgcolor='#fcf1eb'>";
            if (intType == 1)
            {
                string str2 = str;
                str = str2 + "<tr><td height='20'></td></tr><tr><td height='30' class='ForumLogin'><img src='" + SessionItem.GetImageURL() + "Forum/OnTop.gif' align='absMiddle'>被置顶的帖子 <img src='" + SessionItem.GetImageURL() + "Forum/OnLock.gif' align='absMiddle'> 被锁定的帖子 <img src='" + SessionItem.GetImageURL() + "Forum/Hot.gif' align='absMiddle'>回复超过20次的帖子 <img src='" + SessionItem.GetImageURL() + "Forum/Elite.gif' align='absMiddle'> 精华帖 <img src='" + SessionItem.GetImageURL() + "Forum/Ordinarily.gif' align='absMiddle'>普通帖子</td></tr>";
            }
            object obj2 = str;
            return string.Concat(new object[] { obj2, "<tr><td height='20'></td></tr><tr><td height='25'><font class='Forum001'>当前论坛在线的经理</font><font class='ForumLogin'>[", DTOnlineManager.GetOnlineCount(), "]</font></td></tr><tr><td height='25'><font class='Forum001'>用户：</font><font class='ForumLogin'>", strNickName, " - \t到目前为止共有", count, "位经理注册</font></td></tr></table>" });
        }
    }
}


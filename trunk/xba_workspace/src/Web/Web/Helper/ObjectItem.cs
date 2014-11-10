namespace Web.Helper
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Web.DBConnection;
    using Web.DBData;

    public class ObjectItem
    {
        public string strBarList;
        public string strForumWealthList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strLeagueList;
        public string strNewsList;
        public string strPlayerPhotoList;
        public string strRMList;
        public string strRMScoreList;
        public string strRocketmanXBAList;
        public string strStreetBallList;
        public string strXBADailyList;

        public void DeleteFile(string strPath)
        {
            try
            {
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
            }
            catch
            {
                Console.WriteLine("未获取资源：" + strPath + "，2秒后重新尝试。");
                Thread.Sleep(0x7d0);
                this.DeleteFile(strPath);
            }
        }

        private string GetBarList()
        {
            this.strBarList = "";
            this.strBarList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001003", 12, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strBarList = this.strBarList;
                    this.strBarList = string.Concat(new object[] { strBarList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strBarList = this.strBarList + "";
            }
            this.strBarList = this.strBarList + "</table>";
            return this.strBarList;
        }

        private string GetForumWealthList()
        {
            this.strForumWealthList = "";
            int num = 1;
            DataTable topWealthTable = ROOTWealthManager.GetTopWealthTable(0x15);
            if (topWealthTable != null)
            {
                foreach (DataRow row in topWealthTable.Rows)
                {
                    int num2 = (int) row["Wealth"];
                    string str = row["NickName"].ToString().Trim();
                    object strForumWealthList = this.strForumWealthList;
                    this.strForumWealthList = string.Concat(new object[] { strForumWealthList, "<tr><td height='24' width='40' style='COLOR:black' align='center'>", num, "</td><td width='100'  style='COLOR:black'>", str, "</td><td width='78'  style='COLOR:black' align='center'>", num2, "</td></tr>" });
                    if (num < topWealthTable.Rows.Count)
                    {
                        this.strForumWealthList = this.strForumWealthList + "<tr><td height='1' background='Images/web/Rocketman_line.gif' colspan='3'></td></tr>";
                    }
                    num++;
                }
            }
            else
            {
                this.strForumWealthList = "<tr align='center'><td height='359' width='40' style='COLOR:black'></td><td width='100'  style='COLOR:black'></td><td width='78'  style='COLOR:black'></td></tr>";
            }
            return this.strForumWealthList;
        }

        public string GetFriendLink(string strLogo)
        {
            return ("<table cellSpacing='0' cellPadding='0' width='100%' border='0'><tr><td width='15' height='28'><IMG height='28' src='Images/web/link_corner_01.gif' width='15'></td><td align='left' background='Images/web/link_top.gif'><IMG height='28' src='Images/web/link.gif' width='96'></td><td width='15'><IMG height='28' src='Images/web/link_corner_02.gif' width='15'></td></tr><tr><td background='Images/web/link_left.gif'></td><td style='COLOR: #666666' bgColor='#ffffff' align='center'>" + strLogo + "</td><td background='Images/web/link_right.gif'></td></tr><tr><td><IMG height='15' src='Images/web/link_corner_04.gif' width='15'></td><td background='Images/web/link_down.gif'></td><td><IMG height='15' src='Images/web/link_corner_03.gif' width='15'></td></tr></table>");
        }

        private string GetLeagueList()
        {
            this.strLeagueList = "";
            this.strLeagueList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001004", 12, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strLeagueList = this.strLeagueList;
                    this.strLeagueList = string.Concat(new object[] { strLeagueList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strLeagueList = this.strLeagueList + "";
            }
            this.strLeagueList = this.strLeagueList + "</table>";
            return this.strLeagueList;
        }

        private string GetNewNick()
        {
            string str = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001006", 12, 1);
            if (table == null)
            {
                return str;
            }
            str = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            foreach (DataRow row in table.Rows)
            {
                string str2 = row["BoardID"].ToString().Trim();
                int num = (int) row["TopicID"];
                string strIn = row["Title"].ToString();
                string str4 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str4, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str2, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
            }
            return (str + "</table>");
        }

        private string GetNewsList()
        {
            this.strNewsList = "";
            this.strNewsList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable tableByBoardID = ROOTTopicManager.GetTableByBoardID("001001", 10);
            if (tableByBoardID != null)
            {
                foreach (DataRow row in tableByBoardID.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strNewsList = this.strNewsList;
                    this.strNewsList = string.Concat(new object[] { strNewsList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strNewsList = this.strNewsList + "";
            }
            this.strNewsList = this.strNewsList + "</table>";
            return this.strNewsList;
        }

        private string GetPlayerPhotoList()
        {
            this.strPlayerPhotoList = "";
            DataTable table = ROOTPhotoManager.GetPhotoTableTop2();
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int intTopicID = (int) row["TopicID"];
                    string str = row["NickName"].ToString().Trim();
                    string str2 = row["PicURL"].ToString().Trim();
                    int intCategory = (byte) row["Category"];
                    string str3 = DBLogin.GameNameChinese(intCategory);
                    bool flag = (bool) row["Gender"];
                    ROOTTopicManager.GetTopicRowByID(intTopicID);
                    string str4 = row["BoardID"].ToString().Trim();
                    if (flag)
                    {
                        str = string.Concat(new object[] { "<a href='http://bbs.xba.com.cn/Topic.aspx?TopicID=", intTopicID, "&BoardID=", str4, "&Page=1' target='_blank'><font color='#ff005a'>", str, "</font></a> <font color='#009900'>", str3, "</font>" });
                    }
                    else
                    {
                        str = string.Concat(new object[] { "<a href='http://bbs.xba.com.cn/Topic.aspx?TopicID=", intTopicID, "&BoardID=", str4, "&Page=1' target='_blank'><font color='blue'>", str, "</font></a> <font color='#009900'>", str3, "</font>" });
                    }
                    object strPlayerPhotoList = this.strPlayerPhotoList;
                    this.strPlayerPhotoList = string.Concat(new object[] { strPlayerPhotoList, "<tr><td background='Images/web/Right02_left.gif'></td><td height='109' align='center' valign='top' bgcolor='#f0efef'><table width='153' border='0' cellspacing='0' cellpadding='0'><tr><td height='10' colspan='3'><img src='Images/web/photo_1.gif' width='153' height='10'></td></tr><tr><td width='28' height='73'><img src='Images/web/photo_2.gif' width='28' height='73'></td><td width='100' align='left' valign='top'><a href='http://bbs.xba.com.cn/Topic.aspx?TopicID=", intTopicID, "&BoardID=", str4, "&Page=1' target='_blank'><img src='", str2, "' width='100' height='73' border='0'></a></td><td width='25' background='Images/web/photo_3.gif'></td></tr><tr align='center'><td height='26' colspan='3' background='Images/web/photo_4.gif'>", str, "</td></tr></table></td><td background='Images/web/Right02_right.gif'></td></tr>" });
                }
            }
            else
            {
                this.strPlayerPhotoList = "<tr><td background='Images/web/Right02_left.gif' height='218'></td><td align='center' valign='center' bgcolor='#f0efef'><font color='black'>暂无玩家照片</font></td><td background='Images/web/Right02_right.gif'></td></tr>";
            }
            return this.strPlayerPhotoList;
        }

        private string GetRMScoreList()
        {
            this.strRMScoreList = "";
            int num = 1;
            DataTable monthScoreList = RMMonthScoreManager.GetMonthScoreList(15);
            if (monthScoreList != null)
            {
                foreach (DataRow row in monthScoreList.Rows)
                {
                    string str;
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["MonthPoint"];
                    DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(intUserID);
                    if (userRowByUserID != null)
                    {
                        str = userRowByUserID["NickName"].ToString();
                    }
                    else
                    {
                        str = "";
                    }
                    object strRMScoreList = this.strRMScoreList;
                    this.strRMScoreList = string.Concat(new object[] { strRMScoreList, "<tr><td height='23' width='50' style='COLOR:black' align='center'>", num, "</td><td width='100'  style='COLOR:black'>", str, "</td><td width='68'  style='COLOR:black' align='center'>", num3, "</td></tr>" });
                    if (num < monthScoreList.Rows.Count)
                    {
                        this.strRMScoreList = this.strRMScoreList + "<tr><td height='1' background='Images/web/Rocketman_line.gif' colspan='3'></td></tr>";
                    }
                    num++;
                }
            }
            else
            {
                this.strRMScoreList = "<tr align='center'><td height='359' width='50' style='COLOR:black'></td><td width='100'  style='COLOR:black'></td><td width='68'  style='COLOR:black'></td></tr>";
            }
            return this.strRMScoreList;
        }

        private string GetRocketmanXBAList()
        {
            this.strRocketmanXBAList = "";
            this.strRocketmanXBAList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002001", 12, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strRocketmanXBAList = this.strRocketmanXBAList;
                    this.strRocketmanXBAList = string.Concat(new object[] { strRocketmanXBAList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#FFFFFF'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strRocketmanXBAList = this.strRocketmanXBAList + "";
            }
            this.strRocketmanXBAList = this.strRocketmanXBAList + "</table>";
            return this.strRocketmanXBAList;
        }

        private string GetstrCento()
        {
            string str = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002002", 12, 1);
            if (table == null)
            {
                return str;
            }
            str = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            foreach (DataRow row in table.Rows)
            {
                string str2 = row["BoardID"].ToString().Trim();
                int num = (int) row["TopicID"];
                string strIn = row["Title"].ToString();
                string str4 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str4, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str2, "&TopicID=", num, "&Page=1' target='_blank'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
            }
            return (str + "</table>");
        }

        private string GetStreetBallList()
        {
            this.strStreetBallList = "";
            this.strStreetBallList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002002", 12, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strStreetBallList = this.strStreetBallList;
                    this.strStreetBallList = string.Concat(new object[] { strStreetBallList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strStreetBallList = this.strStreetBallList + "";
            }
            this.strStreetBallList = this.strStreetBallList + "</table>";
            return this.strStreetBallList;
        }

        private string GetstrNewTopic()
        {
            string str = "";
            DataTable newEliteByTop = ROOTTopicManager.GetNewEliteByTop(10);
            if (newEliteByTop == null)
            {
                return str;
            }
            str = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            foreach (DataRow row in newEliteByTop.Rows)
            {
                string str2 = row["BoardID"].ToString().Trim();
                int num = (int) row["TopicID"];
                string strIn = row["Title"].ToString();
                string str4 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                object obj2 = str;
                str = string.Concat(new object[] { obj2, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str4, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str2, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
            }
            return (str + "</table>");
        }

        private string GetXBADailyList()
        {
            this.strXBADailyList = "";
            this.strXBADailyList = "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001005", 12, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='Images/icon_02.gif' width='4' height='8'>";
                    object strXBADailyList = this.strXBADailyList;
                    this.strXBADailyList = string.Concat(new object[] { strXBADailyList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='http://bbs.xba.com.cn/Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
            }
            else
            {
                this.strXBADailyList = this.strXBADailyList + "";
            }
            this.strXBADailyList = this.strXBADailyList + "</table>";
            return this.strXBADailyList;
        }

        private string RMList()
        {
            int num;
            int num2;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string teamNameByTeamID;
            string str7;
            DataRow nextMatchRow = RMMatchManager.GetNextMatchRow();
            if (nextMatchRow != null)
            {
                int num3 = (int) nextMatchRow["MatchID"];
                int intTeamID = (int) nextMatchRow["HomeTeamID"];
                int num5 = (int) nextMatchRow["AwayTeamID"];
                teamNameByTeamID = RMTeamManager.GetTeamNameByTeamID(intTeamID);
                str7 = RMTeamManager.GetTeamNameByTeamID(num5);
                if (num3 > 1)
                {
                    DataRow matchRowByMatchID = RMMatchManager.GetMatchRowByMatchID(num3 - 1);
                    int num1 = (int) matchRowByMatchID["MatchID"];
                    num = (int) matchRowByMatchID["HomeTeamID"];
                    num2 = (int) matchRowByMatchID["AwayTeamID"];
                    str3 = Convert.ToString((int) matchRowByMatchID["HomeScore"]);
                    str4 = Convert.ToString((int) matchRowByMatchID["AwayScore"]);
                    str5 = Convert.ToString((int) matchRowByMatchID["YaoScore"]);
                    str = RMTeamManager.GetTeamNameByTeamID(num);
                    str2 = RMTeamManager.GetTeamNameByTeamID(num2);
                }
                else
                {
                    num = intTeamID;
                    num2 = num5;
                    str3 = "--";
                    str4 = "--";
                    str5 = "--";
                    str = teamNameByTeamID;
                    str2 = str7;
                }
            }
            else
            {
                DataRow lastEndMatchRow = RMMatchManager.GetLastEndMatchRow();
                int num6 = (int) lastEndMatchRow["MatchID"];
                num = (int) lastEndMatchRow["HomeTeamID"];
                num2 = (int) lastEndMatchRow["AwayTeamID"];
                str3 = Convert.ToString((int) lastEndMatchRow["HomeScore"]);
                str4 = Convert.ToString((int) lastEndMatchRow["AwayScore"]);
                str5 = Convert.ToString((int) lastEndMatchRow["YaoScore"]);
                str = RMTeamManager.GetTeamNameByTeamID(num);
                str2 = RMTeamManager.GetTeamNameByTeamID(num2);
                teamNameByTeamID = "--";
                str7 = "--";
            }
            this.strRMList = string.Concat(new object[] { 
                "<table width='218' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td width='3' height='139'></td>\t\t\t\t\t<td width='204'>\t\t\t\t\t\t<table width='100%' height='139'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td width='204' height='27'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td>\t\t\t\t\t\t\t\t\t<table width='100%' height='75'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='75' width='78'><font style='line-height:150%'><img src='Images/NBALogo/", num, ".gif'><br><strong>", str, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t\t<td width='70'><strong>", str3, "：", str4, "<br>姚明：", str5, "</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='56'><font style='line-height:150%'><img src='Images/NBALogo/", num2, ".gif'><br><strong>", str2, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='1'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='36'>\t\t\t\t\t\t\t\t\t<table width='100%'  border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='36' width='87'><strong>下一场</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='117'><strong>", teamNameByTeamID, 
                " vs ", str7, "</strong></td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t</table>\t\t\t\t\t</td>\t\t\t\t\t<td width='11'></td>\t\t\t\t</tr>\t\t\t</table>"
             });
            return this.strRMList;
        }

        public void WriteIndex(string strPath)
        {
            string commandText = "SELECT * FROM Main_Index";
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
            if (row == null)
            {
                Console.WriteLine("Main_Index表为空");
            }
            else
            {
                string str2 = row["Title"].ToString().Trim();
                string str3 = row["Key"].ToString().Trim();
                row["Logo"].ToString().Trim();
                string str4 = row["Discription"].ToString().Trim();
                string str5 = row["AD01"].ToString().Trim();
                string str6 = row["AD02"].ToString().Trim();
                row["AD03"].ToString().Trim();
                string str7 = "<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN' ><HTML><HEAD><title>" + str2 + "</title><meta http-equiv=Content-Type content='text/html; charset=gb2312'><meta name=\"keywords\" content=\"" + str3 + "\">\n<meta name=\"description\" content=\"" + str4 + "\"><meta content='JavaScript' name='vs_defaultClientScript'><meta content='http://schemas.microsoft.com/intellisense/ie5' name='vs_targetSchema'><LINK href='Css/Base.css' type='text/css' rel='stylesheet'><style type='text/css'> BODY { font-family:'宋体'; background-color:#FEAD38;}table { border-collapse:collapse; }td { color:white; font-size:12px; font-family:'宋体'; }A.FriLink:link { color:#666666; text-decoration:none; }A.FriLink:visited { color:#666666; text-decoration:none; }A.FriLink:hover { color:#999999; text-decoration:underline; }</style><script language='javascript' src='Script/BTPBase.js'></script><script language='javascript' src='Script/Valid.js'></script><script language='javascript'>function CheckForm(objForm){var blnIsValid=true;var objValid=new STHValid();if(blnIsValid)blnIsValid=objValid.IsWord(objForm.tbUserName,'用户名填写错误！');if(blnIsValid)blnIsValid=objValid.IsBetween(objForm.tbUserName,'用户名长度不符合要求！',4,16);if(blnIsValid)blnIsValid=objValid.IsWord(objForm.tbPassword,'密码填写错误！');if(blnIsValid)blnIsValid=objValid.IsBetween(objForm.tbPassword,'密码填写超出字数限制，要求在4至16个英文字符之间！',4,16);return blnIsValid;}</script></HEAD><body leftMargin='0' topMargin='0' Onload=\"GetArrange()\"><table cellSpacing='0' cellPadding='0' width='100%' border='0'><tr><td vAlign='top' align='left' bgColor='#fead38'><table cellSpacing='0' cellPadding='0' width='1002' border='0'><tr><td vAlign='top' colSpan='6'>" + BoardItem.GetIndexHead(-1, "", "") + "</td></tr><tr><td width='6' rowSpan='14'></td><td vAlign='top' align='left' width='205'><table cellSpacing='0' cellPadding='0' width='205' border='0'><tr><td><table cellSpacing='0' cellPadding='0' width='205' border='0'><tr><td width='17' height='15'><IMG height='15' src='Images/web/left_1_corner_1.gif' width='17'></td><td background='Images/web/left_1_side_top.gif'></td><td width='17'><IMG height='15' src='Images/web/left_1_corner_2.gif' width='17'></td></tr><tr><td background='Images/web/left_1_side_left.gif'></td><td vAlign='top' align='left' bgColor='#cb0900' height='218'><table height='218' cellSpacing='0' cellPadding='0' width='100%' border='0'><tr><td align='center'><a href='" + DBLogin.URLString(1) + "' ><IMG alt='北方大陆1网通' src='Images/web/" + ServerItem.GameImgName(1) + "' width='165' height='46' border='0'></a></td></tr><tr><td align='center'><a href='" + DBLogin.URLString(2) + "' ><IMG alt='南方大陆1电信' src='Images/web/" + ServerItem.GameImgName(2) + "' width='165' height='46' border='0'></a></td></tr><tr><td align='center'><a href='" + DBLogin.URLString(3) + "' ><IMG alt='南方大陆2电信' src='Images/web/" + ServerItem.GameImgName(3) + "' width='165' height='46' border='0'></a></td></tr><tr><td align='center'><a href='" + DBLogin.URLString(4) + "'><IMG height='46' alt='精学社铁通' src='Images/web/server_51edu.gif' width='165' border='0'></a></td></tr></table></td><td background='Images/web/left_1_side_right.gif'></td></tr><tr><td><IMG height='17' src='Images/web/left_1_corner_4.gif' width='17'></td><td background='Images/web/left_1_side_down.gif'></td><td width='17' height='17'><IMG height='17' src='Images/web/left_1_corner_3.gif' width='17'></td></tr></table></td></tr><tr><td height='6'></td></tr><tr><td height='6'></td></tr><tr><td><table cellSpacing='0' cellPadding='0' width='205' border='0'><tr><td width='17' height='15'><IMG height='15' src='Images/web/left_2_corner_1.gif' width='17'></td><td background='Images/web/left_2_side_top.gif'></td><td width='17'><IMG height='15' src='Images/web/left_2_corner_2.gif' width='17'></td></tr><tr><td background='Images/web/left_2_side_left.gif' rowSpan='2'></td><td vAlign='top' align='left' bgColor='#ff9000' height='47'><p><IMG height='47' src='Images/web/service.GIF' width='132'></p></td><td background='Images/web/left_2_side_right.gif' rowSpan='2'></td></tr><tr><td bgColor='#ff9000' height='42'><table cellSpacing='0' cellPadding='0' width='100%' border='0'><tr><td vAlign='middle' align='right' width='53' height='20'>QQ：</td><td style='PADDING-LEFT: 4px'><a href='http://wpa.qq.com/msgrd?v=1&amp;uin=15908920&amp;site=www.xba.com.cn&amp;menu=yes'target='blank'><img alt='有事您Q我！' src='http://wpa.qq.com/pa?p=1:15908920:7' border='0' width='71' height='24'></a></td></tr><tr><td align='right' height='22'>信箱：</td><td style='PADDING-LEFT: 4px'><A style='COLOR: #ffffff' href='mailto:xbamanager#gmail.com'>xbamanager@gmail.com</A></td></tr></table></td></tr><tr><td><IMG height='17' src='Images/web/left_2_corner_4.gif' width='17'></td><td background='Images/web/left_2_side_down.gif'></td><td width='17' height='17'><IMG height='17' src='Images/web/left_2_corner_3.gif' width='17'></td></tr></table></td></tr></table></td><td vAlign='top' align='left' width='283'><table cellSpacing='0' cellPadding='0' width='541' border='0'><tr><td width='10'><table cellSpacing='0' cellPadding='0' width='10' border='0'><tr><td><IMG height='28' src='Images/web/center_top.gif' width='10'></td></tr><tr><td bgColor='#ffffff' height='73'></td></tr><tr><td><IMG height='28' src='Images/web/center_down.gif' width='10'></td></tr></table></td><td vAlign='top' align='left' colSpan='3'><table cellSpacing='0' cellPadding='0' width='521' border='0'><tr><td width='10'><IMG height='10' src='Images/web/center_1_corner_01.gif' width='10'></td><td bgColor='#ffffff'></td><td width='10'><IMG height='10' src='Images/web/center_1_corner_02.gif' width='10'></td></tr><tr><td bgColor='#ffffff'></td><td>" + str5 + "</td><td bgColor='#ffffff'></td></tr><tr><td><IMG height='10' src='Images/web/center_1_corner_04.gif' width='10'></td><td bgColor='#ffffff'></td><td><IMG height='10' src='Images/web/center_1_corner_03.gif' width='10'></td></tr></table></td><td width='10'><table cellSpacing='0' cellPadding='0' width='10' border='0'><tr><td><IMG height='28' src='Images/web/center_top.gif' width='10'></td></tr><tr><td bgColor='#ffffff' height='73'></td></tr><tr><td><IMG height='28' src='Images/web/center_down.gif' width='10'></td></tr></table></td></tr><tr><td colSpan='5' height='10'></td></tr><tr><td></td><td vAlign='top' align='left' width='246'><table cellSpacing='0' cellPadding='0' width='254' border='0'><tr><td colSpan='3'><A href='http://bbs.xba.com.cn/Board.aspx?BoardID=001001&amp;Page=1' target='_blank'><IMG height='26' alt='篮球经理游戏官方公告区' src='Images/web/news.gif' width='254' border='0'></A></td></tr><tr><td width='1' background='Images/web/league_side.gif' height='219'></td><td vAlign='top' align='left' width='244' background='Images/web/league_backdrop.gif'>" + this.GetNewsList() + "</td><td width='1' background='Images/web/league_side.gif'></td></tr><tr bgColor='#ff6600'><td colSpan='3' height='1'></td></tr></table></td><td width='13'></td><td vAlign='top' align='left' width='254'><table cellSpacing='0' cellPadding='0' width='254' border='0'><tr><td colSpan='3'><IMG height='26' alt='最新玩家帖子' src='Images/web/newtopic.gif' width='254'border='0'></td></tr><tr><td width='1' background='Images/web/league_side.gif' height='219'></td><td vAlign='top' width='252' background='Images/web/league_backdrop.gif'>" + this.GetstrNewTopic() + "</td><td width='1' background='Images/web/league_side.gif'></td></tr><tr bgColor='#ff6600'><td colSpan='3' height='1'></td></tr></table></td><td></td></tr></table></td><td vAlign='top' align='left' width='238' rowSpan='10'><table cellSpacing='0' cellPadding='0' width='238' border='0'><tr><td><table cellSpacing='0' cellPadding='0' width='238' border='0'><tr><td width='15' height='15'><IMG height='15' src='Images/web/RightTop_corner_01.gif' width='15'></td><td background='Images/web/RightTop_top.gif'></td><td width='15'><IMG height='15' src='Images/web/RightTop_corner_02.gif' width='15'></td></tr><tr><td background='Images/web/RightTop_left.gif'></td><td vAlign='top' align='left' bgColor='#da4c04'><table cellSpacing='0' cellPadding='0' width='100%' border='0'><tr><td><table cellSpacing='0' cellPadding='0' width='100%' border='0'><form onsubmit='return CheckForm(this)' action='LoginOK.aspx' method='post'><TBODY><tr><td width='23' rowSpan='6'></td><td colSpan='4' height='10'><strong></strong></td></tr><tr><td width='55'></td><td colSpan='2'></td><td width='22'></td></tr><tr><td height='20'><strong>用户名：</strong></td><td bgColor='#f4a27a' colSpan='2'><input class='Input_108_20' id='tbUserName' type='text' name='tbUserName'></td><td></td></tr><tr><td colSpan='4' height='12'></td></tr><tr><td height='20'><p><strong>密 码：</strong></p></td><td bgColor='#f4a27a' colSpan='2'><input class='Input_108_20' id='tbPassword' type='password' name='tbPassword'></td><td></td></tr><tr><td colSpan='4' height='10'></td></tr><tr><td height='10'></td><td></td><td align='center' width='54'><input id='btnSubmit' type='image' height='19' alt='登录' width='52' src='Images/web/button_01.gif'></td><td align='center' width='54'><A href='NewReg1.aspx'><IMG height='19' alt='注册' src='Images/web/button_02.gif' width='52' border='0'></A></td><td></td></tr></form></table></td></tr><tr><td height='53'><A href='MemberCenter.aspx'><IMG height='44' alt='XBA通行证' src='Images/web/menu_right_01.gif' width='203' border='0'></A></td></tr><tr><td height='52'><A href='NewReg1.aspx'><IMG height='44' alt='第一步：注册通行证' src='Images/web/inside_button_06.gif' width='203' border='0'></A></td></tr><tr><td height='52'><IMG height='44' alt='第二步：组建俱乐部' src='Images/web/inside_button_02.gif' width='203' border='0'></td></tr><tr><td height='53'><IMG height='44' alt='第三步：加入职业联赛' src='Images/web/inside_button_07.gif' width='203' border='0'></td></tr></table></td><td background='Images/web/RightTop_right.gif'></td></tr><tr><td><IMG height='15' src='Images/web/RightTop_corner_04.gif' width='15'></td><td background='Images/web/RightTop_down.gif'></td><td><IMG height='15' src='Images/web/RightTop_corner_03.gif' width='15'></td></tr></table></td></tr><tr><td height='10'></td></tr><tr><td><table cellSpacing='0' cellPadding='0' width='238' border='0'><tr><td><IMG height='40' src='Images/web/Right02_corner_03.gif' width='16'></td><td background='Images/web/Right02_top.gif'><IMG height='40' src='Images/web/cut.gif' width='206'></td><td><IMG height='40' src='Images/web/Right02_corner_04.gif' width='16'></td></tr><tr><td background='Images/web/Right02_left.gif'></td><td vAlign='middle' align='center' width='206' bgColor='#f0efef' height='116'><a style='CURSOR: hand' onclick='NewJieWin();'><IMG height='116' alt='精彩游戏图片' src='Images/web/cut_image.gif' width='165' border='0'></a></td><td background='Images/web/Right02_right.gif'></td></tr><tr><td width='16' height='16'><IMG height='16' src='Images/web/Right02_corner_01.gif' width='16'></td><td background='Images/web/Right02_down.gif'></td><td vAlign='top' align='left' width='16'><IMG height='16' src='Images/web/Right02_corner_02.gif' width='16'></td></tr></table></td></tr><tr><td height='10'></td></tr></table></td><td vAlign=\"top\" align=\"left\" colSpan=\"2\" rowSpan=\"12\"></td></tr><tr><td height=10></td></tr><tr><td  vAlign=\"top\" align=\"left\" colSpan=\"2\">" + str6 + "</td></tr><tr><td colSpan='2' height='10' ></td></tr><tr><td colSpan='2' height='10'></td></tr><tr>" + BoardItem.GetIndexEnd() + "</table></td></tr></TBODY></table><div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/97413.js\"></script><noscript><a href=\"http://www.51.la/?97413\" target=\"_blank\"><img alt=\"&#x6211;&#x8981;&#x5566;&#x514D;&#x8D39;&#x7EDF;&#x8BA1;\" src=\"http://img.users.51.la/97413.asp\" style=\"border:none\" /></a></noscript></div><iframe src=\"\" frameborder=\"no\" id=\"ifmHidden\" style=\"display:none;\"></iframe><iframe src=\"\" frameborder=\"no\" id=\"ifmHidden1\" style=\"display:none;\"></iframe><map name='Map'><area shape='RECT' coords='134,17,168,33' href='mailto:xbamanager#gmail.com' alt='投稿邮箱：xbamanager@gmail.com'></map></body></HTML>";
                this.DeleteFile(strPath);
                Encoding encoding = Encoding.GetEncoding("gb2312");
                StreamWriter writer = new StreamWriter(strPath, false, encoding);
                writer.Write(str7);
                writer.Flush();
                writer.Close();
            }
        }
    }
}


namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Index : Page
    {
        public string strBarList;
        public string strCento;
        public string strForumWealthList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strLeagueList;
        public string strNewNick;
        public string strNewsList;
        public string strNewTopic;
        public string strPlayerPhotoList;
        public string strRMList;
        private string strRMLogo;
        public string strRMScoreList;
        public string strRocketmanXBAList;
        public string strStreetBallList;
        public string strXBADailyList;

        private void GetBarList()
        {
            this.strBarList = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001003", 12, 1);
            if (table != null)
            {
                this.strBarList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strBarList = this.strBarList;
                    this.strBarList = string.Concat(new object[] { strBarList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strBarList = this.strBarList + "</table>";
            }
        }

        private void GetForumWealthList()
        {
            this.strForumWealthList = "";
            int num = 1;
            DataTable topWealthTable = ROOTWealthManager.GetTopWealthTable(0x15);
            if (topWealthTable != null)
            {
                foreach (DataRow row in topWealthTable.Rows)
                {
                    string str;
                    int intUserID = (int) row["UserID"];
                    int num2 = (int) row["Wealth"];
                    SqlDataReader userRowByUserID = ROOTUserManager.GetUserRowByUserID(intUserID);
                    if (userRowByUserID.Read())
                    {
                        str = userRowByUserID["NickName"].ToString();
                    }
                    else
                    {
                        str = "";
                    }
                    userRowByUserID.Read();
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
        }

        private void GetLeagueList()
        {
            this.strLeagueList = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001004", 12, 1);
            if (table != null)
            {
                this.strLeagueList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strLeagueList = this.strLeagueList;
                    this.strLeagueList = string.Concat(new object[] { strLeagueList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strLeagueList = this.strLeagueList + "</table>";
            }
        }

        private void GetNewsList()
        {
            this.strNewsList = "";
            DataTable tableByBoardID = ROOTTopicManager.GetTableByBoardID("001001", 12);
            if (tableByBoardID != null)
            {
                this.strNewsList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in tableByBoardID.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strNewsList = this.strNewsList;
                    this.strNewsList = string.Concat(new object[] { strNewsList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strNewsList = this.strNewsList + "</table>";
            }
        }

        private void GetPlayerPhotoList()
        {
            this.strPlayerPhotoList = "";
            DataTable table = ROOTPhotoManager.GetPhotoTableTop2();
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["TopicID"];
                    string str2 = row["NickName"].ToString().Trim();
                    string str3 = row["PicURL"].ToString().Trim();
                    int intCategory = (byte) row["Category"];
                    string str = DBLogin.GameNameChinese(intCategory);
                    bool flag = (bool) row["Gender"];
                    string str4 = row["BoardID"].ToString().Trim();
                    if (flag)
                    {
                        str2 = string.Concat(new object[] { "<a href='", DBLogin.URLString(0), "Topic.aspx?TopicID=", num, "&BoardID=", str4, "&Page=1' target='_blank'><font color='#ff005a'>", str2, "</font></a> <font color='#009900'>", str, "</font>" });
                    }
                    else
                    {
                        str2 = string.Concat(new object[] { "<a href='", DBLogin.URLString(0), "Topic.aspx?TopicID=", num, "&BoardID=", str4, "&Page=1' target='_blank'><font color='blue'>", str2, "</font></a> <font color='#009900'>", str, "</font>" });
                    }
                    object strPlayerPhotoList = this.strPlayerPhotoList;
                    this.strPlayerPhotoList = string.Concat(new object[] { strPlayerPhotoList, "<tr><td background='Images/web/Right02_left.gif'></td><td height='109' align='center' valign='top' bgcolor='#f0efef'><table width='153' border='0' cellspacing='0' cellpadding='0'><tr><td height='10' colspan='3'><img src='Images/web/photo_1.gif' width='153' height='10'></td></tr><tr><td width='28' height='73'><img src='Images/web/photo_2.gif' width='28' height='73'></td><td width='100' align='left' valign='top'><a href='", DBLogin.URLString(0), "Topic.aspx?TopicID=", num, "&BoardID=", str4, "&Page=1' target='_blank'><img src='", str3, "' width='100' height='73' border='0'></a></td><td width='25' background='Images/web/photo_3.gif'></td></tr><tr align='center'><td height='26' colspan='3' background='Images/web/photo_4.gif'>", str2, "</td></tr></table></td><td background='Images/web/Right02_right.gif'></td></tr>" });
                }
            }
            else
            {
                this.strPlayerPhotoList = "<tr><td background='Images/web/Right02_left.gif' height='218'></td><td align='center' valign='center' bgcolor='#f0efef'><font color='black'>暂无玩家照片</font></td><td background='Images/web/Right02_right.gif'></td></tr>";
            }
        }

        private void GetRMScoreList()
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
                    int num2 = (int) row["MonthPoint"];
                    SqlDataReader userRowByUserID = ROOTUserManager.GetUserRowByUserID(intUserID);
                    if (userRowByUserID.Read())
                    {
                        str = userRowByUserID["NickName"].ToString();
                    }
                    else
                    {
                        str = "";
                    }
                    userRowByUserID.Close();
                    object strRMScoreList = this.strRMScoreList;
                    this.strRMScoreList = string.Concat(new object[] { strRMScoreList, "<tr><td height='23' width='50' style='COLOR:black' align='center'>", num, "</td><td width='100'  style='COLOR:black'>", str, "</td><td width='68'  style='COLOR:black' align='center'>", num2, "</td></tr>" });
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
        }

        private void GetRocketmanXBAList()
        {
            this.strRocketmanXBAList = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002001", 12, 1);
            if (table != null)
            {
                this.strRocketmanXBAList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strRocketmanXBAList = this.strRocketmanXBAList;
                    this.strRocketmanXBAList = string.Concat(new object[] { strRocketmanXBAList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strRocketmanXBAList = this.strRocketmanXBAList + "</table>";
            }
        }

        private void GetstrCento()
        {
            this.strCento = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002002", 12, 1);
            if (table != null)
            {
                this.strCento = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strCento = this.strCento;
                    this.strCento = string.Concat(new object[] { strCento, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strCento = this.strCento + "</table>";
            }
        }

        private void GetStreetBallList()
        {
            this.strStreetBallList = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("002002", 12, 1);
            if (table != null)
            {
                this.strStreetBallList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strStreetBallList = this.strStreetBallList;
                    this.strStreetBallList = string.Concat(new object[] { strStreetBallList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strStreetBallList = this.strStreetBallList + "</table>";
            }
        }

        private void GetstrNewNick()
        {
            this.strNewNick = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001006", 12, 1);
            if (table != null)
            {
                this.strNewNick = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strNewNick = this.strNewNick;
                    this.strNewNick = string.Concat(new object[] { strNewNick, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strNewNick = this.strNewNick + "</table>";
            }
        }

        private void GetstrNewTopic()
        {
            this.strNewTopic = "";
            DataTable tableByTop = ROOTTopicManager.GetTableByTop(12);
            if (tableByTop != null)
            {
                this.strNewTopic = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in tableByTop.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strNewTopic = this.strNewTopic;
                    this.strNewTopic = string.Concat(new object[] { strNewTopic, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strNewTopic = this.strNewTopic + "</table>";
            }
        }

        private void GetXBADailyList()
        {
            this.strXBADailyList = "";
            DataTable table = ROOTTopicManager.GetTableByBoardIDElite("001005", 12, 1);
            if (table != null)
            {
                this.strXBADailyList = "<table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in table.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strXBADailyList = this.strXBADailyList;
                    this.strXBADailyList = string.Concat(new object[] { strXBADailyList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(0), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strXBADailyList = this.strXBADailyList + "</table>";
            }
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strIndexHead = BoardItem.GetIndexHead(-1, "", "");
            this.strIndexEnd = BoardItem.GetIndexEnd();
            this.GetNewsList();
            this.GetXBADailyList();
            this.GetLeagueList();
            this.GetBarList();
            this.GetRocketmanXBAList();
            this.GetStreetBallList();
            this.GetPlayerPhotoList();
            this.GetstrNewTopic();
            this.GetstrCento();
            this.GetstrNewNick();
            this.GetForumWealthList();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void RMList()
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
                this.strRMLogo = "<img src='" + SessionItem.GetImageURL() + "RM/Border_G_08.gif' border='0'>";
            }
            this.strRMList = string.Concat(new object[] { 
                "<table width='218' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td width='3' height='139'></td>\t\t\t\t\t<td width='204'>\t\t\t\t\t\t<table width='100%' height='139'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td width='204' height='27'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td>\t\t\t\t\t\t\t\t\t<table width='100%' height='75'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='75' width='78'><font style='line-height:150%'><img src='", SessionItem.GetImageURL(), "NBALogo/", num, ".gif'><br><strong>", str, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t\t<td width='70'><strong>", str3, "：", str4, "<br>姚明：", str5, "</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='56'><font style='line-height:150%'><img src='", SessionItem.GetImageURL(), "NBALogo/", num2, 
                ".gif'><br><strong>", str2, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='1'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='36'>\t\t\t\t\t\t\t\t\t<table width='100%'  border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='36' width='87'><strong>下一场</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='117'><strong>", teamNameByTeamID, " vs ", str7, "</strong></td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t</table>\t\t\t\t\t</td>\t\t\t\t\t<td width='11'></td>\t\t\t\t</tr>\t\t\t</table>"
             });
        }
    }
}


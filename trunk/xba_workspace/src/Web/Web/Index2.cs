namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Index2 : Page
    {
        public string strBarList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strLeagueList;
        public string strNewsList;
        public string strRMList;
        private string strRMLogo;
        public string strRMScoreList;
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
                    this.strBarList = string.Concat(new object[] { strBarList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strBarList = this.strBarList + "</table>";
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
                    this.strLeagueList = string.Concat(new object[] { strLeagueList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'><font color='#ffffff'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
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
                    this.strNewsList = string.Concat(new object[] { strNewsList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strNewsList = this.strNewsList + "</table>";
            }
        }

        private void GetRMScoreList()
        {
            this.strRMScoreList = "";
            int num = 1;
            DataTable monthScoreList = RMMonthScoreManager.GetMonthScoreList(6);
            if (monthScoreList != null)
            {
                foreach (DataRow row in monthScoreList.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["MonthPoint"];
                    string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                    object strRMScoreList = this.strRMScoreList;
                    this.strRMScoreList = string.Concat(new object[] { strRMScoreList, "<tr align='center'><td height='23' width='50' style='COLOR:black'>", num, "</td><td width='100'  style='COLOR:black'>", nickNameByUserID, "</td><td width='68'  style='COLOR:black'>", num3, "</td></tr>" });
                    if (num < monthScoreList.Rows.Count)
                    {
                        this.strRMScoreList = this.strRMScoreList + "<tr><td height='1' background='Images/web/Rocketman_line.gif' colspan='3'></td></tr>";
                    }
                    num++;
                }
            }
            else
            {
                this.strRMScoreList = "<tr align='center'><td height='144' width='50' style='COLOR:black'></td><td width='100'  style='COLOR:black'></td><td width='68'  style='COLOR:black'></td></tr>";
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
                    this.strXBADailyList = string.Concat(new object[] { strXBADailyList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
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
            this.RMList();
            this.GetRMScoreList();
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


namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RMCenter : Page
    {
        private bool blnCanSubmit;
        protected ImageButton btnTrue;
        public DateTime datMatchTime;
        private int intCount;
        private int intMatchID;
        private int intPage;
        private int intPerPage;
        private int intTotal;
        private int intUserID;
        public string strAwayName;
        public string strCoin;
        public string strHomeName;
        public string strIndexEnd;
        public string strIndexHead;
        public string strList;
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        public string strRMDownList;
        private string strRMLogo;
        public string strRMUpList;
        public string strScript;
        private string strType;
        protected TextBox tbAwayScore;
        protected TextBox tbHomeScore;
        protected HtmlTable tblGuess;
        protected HtmlTable tblList;
        protected HtmlTable tblNoGuess;
        protected TextBox tbYao;

        private void btnTrue_Click(object sender, ImageClickEventArgs e)
        {
            if (!this.blnCanSubmit)
            {
                this.strMsg = "<font color='red'>您已参加过本场竞猜！</font>";
            }
            else
            {
                int num;
                int intHomeScore = 0;
                int intAwayScore = 0;
                int intYaoScore = 0;
                try
                {
                    intHomeScore = Convert.ToInt32(this.tbHomeScore.Text);
                    intAwayScore = Convert.ToInt32(this.tbAwayScore.Text);
                    intYaoScore = Convert.ToInt32(this.tbYao.Text);
                    num = 1;
                    if (intHomeScore < 0)
                    {
                        num = 2;
                    }
                    else if (intAwayScore < 0)
                    {
                        num = 3;
                    }
                    else if (intYaoScore < 0)
                    {
                        num = 4;
                    }
                    if (intHomeScore > 250)
                    {
                        num = 2;
                    }
                    else if (intAwayScore > 250)
                    {
                        num = 3;
                    }
                    else if (intYaoScore > 250)
                    {
                        num = 4;
                    }
                }
                catch
                {
                    num = 0;
                }
                if (num == 1)
                {
                    if (RMRecordManager.SetRecord(this.intUserID, this.intMatchID, intHomeScore, intAwayScore, intYaoScore) == 0)
                    {
                        this.strMsg = "<font color='red'>您已参加过本场竞猜！</font>";
                    }
                    else
                    {
                        base.Response.Redirect("RMCenter.aspx?TYPE=MYRM&Page=1");
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            this.strMsg = "<font color='red'>请正确输入分数！</font>";
                            return;

                        case 2:
                            this.strMsg = "<font color='red'>请正确输入" + this.strHomeName + "分数！</font>";
                            return;

                        case 3:
                            this.strMsg = "<font color='red'>请正确输入" + this.strAwayName + "分数！</font>";
                            return;

                        case 4:
                            this.strMsg = "<font color='red'>请正确输入姚明分数！</font>";
                            break;
                    }
                }
            }
        }

        private string GetMsgScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetMsgTotal()
        {
            return RMRecordManager.GetRecordCountByUserIDNew(this.intUserID);
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string[] strArray;
            int msgTotal = this.GetMsgTotal();
            int num2 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            if (this.intPage != 1)
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                string text1 = string.Concat(strArray);
            }
            if (this.intPage != num2)
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                string text2 = string.Concat(strArray);
            }
            string str2 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str2 = str2 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str2 = str2 + " selected";
                }
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str2 = str2 + "</select>";
            return string.Concat(new object[] { "共", msgTotal, "个记录 跳转", str2 });
        }

        private void GuessList()
        {
            DataRow nextMatchRow = RMMatchManager.GetNextMatchRow();
            this.intMatchID = (int) nextMatchRow["MatchID"];
            int intTeamID = (int) nextMatchRow["HomeTeamID"];
            int num2 = (int) nextMatchRow["AwayTeamID"];
            this.strHomeName = RMTeamManager.GetTeamNameByTeamID(intTeamID);
            this.strAwayName = RMTeamManager.GetTeamNameByTeamID(num2);
            this.datMatchTime = (DateTime) nextMatchRow["MatchTime"];
            if (this.datMatchTime < DateTime.Now)
            {
                this.tblNoGuess.Visible = true;
                this.strMsg = "<font color='red'>请关注比赛结果，谢谢！</font>";
            }
            else
            {
                this.tblGuess.Visible = true;
                this.blnCanSubmit = !RMRecordManager.GetRecordByUIDMID(this.intUserID, this.intMatchID);
                if (!this.blnCanSubmit)
                {
                    this.strMsg = "<font color='red'>您已参加过本场竞猜！</font>";
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnTrue.Click += new ImageClickEventHandler(this.btnTrue_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MonthList()
        {
            int num = 1;
            DataTable topMonthScore = RMMonthScoreManager.GetTopMonthScore();
            this.strList = "<table width='362' border='2' cellpadding='0' cellspacing='0' bordercolor='#ffffff'><tr><td width='58' height='26' align='center' bgcolor='#c11642'><strong>排名</strong></td><td align='center' bgcolor='#c11642'><strong>会员</strong></td><td width='58' align='center' bgcolor='#c11642'><strong>积分</strong></td></tr>";
            if (topMonthScore == null)
            {
                this.strList = this.strList + "<tr align='center'><td height='25' colspan='3'><font color='#9b052b'>暂时没月度排名</font></td></tr></table>";
            }
            else
            {
                foreach (DataRow row in topMonthScore.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["MonthPoint"];
                    string str = ROOTUserManager.GetUserInfoByID(intUserID)["NickName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center'  onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20'><strong><font color='#9b052b'>", num, "</font></strong></td><td><font color='#9b052b'>", str, "</font></td><td><font color='#9b052b'>", num3, "</font></td></tr>" });
                    num++;
                }
                this.strList = this.strList + "</table>";
            }
        }

        private void MyRMList()
        {
            string strCurrentURL = "RMCenter.aspx?Type=MYRM&";
            this.intPerPage = 11;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intCount = this.intPerPage * this.intPage;
            this.intTotal = this.GetMsgTotal();
            this.strScript = this.GetMsgScript(strCurrentURL);
            this.strList = "<table width='362' border='2' cellpadding='0' cellspacing='0' bordercolor='#FFFFFF'><tr><td width='180' height='26' align='center' bgcolor='#C11642'><strong>比赛竞猜</strong></td><td width='116' align='center' bgcolor='#C11642'><strong>姚明</strong></td><td width='58' align='center' bgcolor='#C11642'><strong>积分</strong></td></tr>";
            DataTable reader = RMRecordManager.GetRecordByUserIDNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    int num1 = (int) row["UserID"];
                    int intMatchID = (int) row["MatchID"];
                    int num2 = (byte) row["HomeScore"];
                    int num3 = (byte) row["AwayScore"];
                    int num4 = (byte) row["YaoScore"];
                    int num5 = (int) row["GainPoint"];
                    DataRow matchRowByMatchID = RMMatchManager.GetMatchRowByMatchID(intMatchID);
                    int intTeamID = (int) matchRowByMatchID["HomeTeamID"];
                    int num7 = (int) matchRowByMatchID["AwayTeamID"];
                    string teamNameByTeamID = RMTeamManager.GetTeamNameByTeamID(intTeamID);
                    string str3 = RMTeamManager.GetTeamNameByTeamID(num7);
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center'  onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20' align='center'><font color='#9B052B'>", teamNameByTeamID, " ", num2, "：", num3, " ", str3, "</font></td><td align='center'> <font color='#9B052B'>", num4, "</font></td><td align='center'><font color='#9B052B'>", num5, "</font></td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='23' align='right' colspan='4'><font color='#9B052B'>" + this.GetMsgViewPage(strCurrentURL) + "</font></td></tr></table>";
            }
            else
            {
                this.strList = this.strList + "<tr align='center'><td height='20' colspan='3'><font color='#9B052B'>暂时没有竞猜记录</font></td></tr></table>";
            }
            //reader.Close();
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.strCoin = Convert.ToString((int) userInfoByID["Coin"]).Trim();
                string strUserName = userInfoByID["UserName"].ToString().Trim();
                string strPassword = userInfoByID["Password"].ToString().Trim();
                this.strIndexHead = BoardItem.GetIndexHead(this.intUserID, strUserName, strPassword);
                this.strIndexEnd = BoardItem.GetIndexEnd();
                this.RMList();
                this.tblList.Visible = false;
                this.intUserID = SessionItem.CheckLogin(0);
                this.tblGuess.Visible = false;
                this.tblNoGuess.Visible = false;
                this.btnTrue.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.strType = SessionItem.GetRequest("TYPE", 1).ToString();
                switch (this.strType)
                {
                    case "MONTH":
                        this.tblList.Visible = true;
                        this.MonthList();
                        return;

                    case "YEAR":
                        this.tblList.Visible = true;
                        this.YearList();
                        return;

                    case "MYRM":
                        this.tblList.Visible = true;
                        if (this.intUserID > 0)
                        {
                            this.MyRMList();
                            return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=482");
                        return;

                    case "GUESS":
                        if (this.intUserID > 0)
                        {
                            this.GuessList();
                            return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=482");
                        return;
                }
                this.tblList.Visible = true;
                this.strType = "MONTH";
                this.MonthList();
            }
        }

        private void RMList()
        {
            int num;
            int num2;
            int num4;
            int num5;
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
                num4 = (int) nextMatchRow["HomeTeamID"];
                num5 = (int) nextMatchRow["AwayTeamID"];
                teamNameByTeamID = RMTeamManager.GetTeamNameByTeamID(num4);
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
                    num = num4;
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
                num4 = 0;
                num5 = 0;
                this.strRMLogo = "<img src='" + SessionItem.GetImageURL() + "RM/Border_G_08.gif' border='0'>";
            }
            string str8 = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "NBALogo/", num4, ".gif'>" });
            string str9 = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "NBALogo/", num5, ".gif'>" });
            if (num4 == 0)
            {
                str8 = "";
            }
            if (num5 == 0)
            {
                str9 = "";
            }
            this.strRMUpList = string.Concat(new object[] { 
                "<table width='234' cellspacing='0' cellpadding='0'><tr><td width='50' height='50' align='right'><font color='#9b052b'><img src='", SessionItem.GetImageURL(), "NBALogo/", num, ".gif'></font></td><td width='39' align='right'><font color='#9b052b'><strong>", str, "</strong></font></td><td width='55' align='center'><font color='#9b052b'><strong>", str3, "：", str4, "</strong></font></td><td width='43' align='left'><font color='#9b052b'><strong>", str2, "</strong></font></td><td width='50' align='left'><img src='", SessionItem.GetImageURL(), "NBALogo/", num2, 
                ".gif'></td></tr><tr bgcolor='#ffffff'><td height='2' colspan='5' align='right'></td></tr><tr><td height='20' colspan='2' align='right'><font color='#9b052b'><strong>姚明：</strong></font></td><td align='center'><font color='#9b052b'><strong>", str5, "</strong></font></td><td colspan='2'></td></tr></table>"
             });
            this.strRMDownList = "<table width='234' cellspacing='0' cellpadding='0'><tr><td width='50' height='50' align='right'>" + str8 + "</td><td width='39' align='right'><font color='#9b052b'><strong>" + teamNameByTeamID + "</strong></font></td><td width='55' align='center'><font color='#9b052b'><strong>VS</strong></font></td><td width='43' align='left'><font color='#9b052b'><strong>" + str7 + "</strong></font></td><td width='50' align='left'>" + str9 + "</td></tr></table>";
        }

        private void YearList()
        {
            int num = 1;
            DataTable topYearScore = RMYearScoreManager.GetTopYearScore();
            this.strList = "<table width='362' border='2' cellpadding='0' cellspacing='0' bordercolor='#ffffff'><tr><td width='58' height='26' align='center' bgcolor='#c11642'><strong>排名</strong></td><td align='center' bgcolor='#c11642'><strong>会员</strong></td><td width='58' align='center' bgcolor='#c11642'><strong>积分</strong></td></tr>";
            if (topYearScore == null)
            {
                this.strList = this.strList + "<tr align='center'><td height='25' colspan='3'><font color='#9b052b'>暂时没年度排名</font></td></tr></table>";
            }
            else
            {
                foreach (DataRow row in topYearScore.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["YearPoint"];
                    string str = ROOTUserManager.GetUserInfoByID(intUserID)["NickName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center'  onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20'><strong><font color='#9b052b'>", num, "</font></strong></td><td><font color='#9b052b'>", str, "</font></td><td><font color='#9b052b'>", num3, "</font></td></tr>" });
                    num++;
                }
                this.strList = this.strList + "</table>";
            }
        }
    }
}


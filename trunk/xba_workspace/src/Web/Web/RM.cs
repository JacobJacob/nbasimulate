namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RM : Page
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
        public string strHomeName;
        public string strList;
        public string strMsg;
        public string strPageIntro;
        public string strScript;
        private string strType;
        protected TextBox tbAwayScore;
        protected TextBox tbHomeScore;
        protected HtmlTable tblGuess;
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
                        base.Response.Redirect("RM.aspx?TYPE=MYRM&Page=1");
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            this.strMsg = "<font color='red'>请正确输入分数！</font>";
                            return;

                        case 1:
                            break;

                        case 2:
                            this.strMsg = "<font color='red'>请正确输入" + this.strHomeName + "分数！</font>";
                            return;

                        case 3:
                            this.strMsg = "<font color='red'>请正确输入" + this.strAwayName + "分数！</font>";
                            return;

                        case 4:
                            this.strMsg = "<font color='red'>请正确输入姚明分数！</font>";
                            break;

                        default:
                            return;
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
            return RMRecordManager.GetRecordCountByUserID(this.intUserID);
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
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                string text1 = string.Concat(strArray);
            }
            if (this.intPage != num2)
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                string text2 = string.Concat(strArray);
            }
            string str = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str = str + "<option value=" + i;
                if (i == this.intPage)
                {
                    str = str + " selected";
                }
                object obj2 = str;
                str = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str = str + "</select>";
            return string.Concat(new object[] { "共", msgTotal, "个记录 跳转", str });
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
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td width='33' height='25'>排名</td><td width='127'>会员</td><td width='50'>积分</td></tr>";
            if (topMonthScore == null)
            {
                this.strList = this.strList + "<tr align='center'><td height='25' colspan='3'>暂时没月年度排名</td></tr></table>";
            }
            else
            {
                foreach (DataRow row in topMonthScore.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["MonthPoint"];
                    string str = ROOTUserManager.GetUserInfoByID(intUserID)["NickName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center'  onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", num, "</td><td>", str, "</td><td>", num3, "</td></tr><tr><td colspan='3' height='1' background='", SessionItem.GetImageURL(), "RM/Border_07.gif'></td></tr>" });
                    num++;
                }
                this.strList = this.strList + "</table>";
            }
        }

        private void MyRMList()
        {
            string strCurrentURL = "RM.aspx?Type=MYRM&";
            this.intPerPage = 12;
            this.intPage = SessionItem.GetRequest("Page", 0);
            this.intCount = this.intPerPage * this.intPage;
            this.intTotal = this.GetMsgTotal();
            this.strScript = this.GetMsgScript(strCurrentURL);
            this.strList = "<table width='210'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td height='25' width='144'>比赛竞猜</td><td width='33'>YAO</td><td width='33'>积分</td></tr>";
            DataTable table = RMRecordManager.GetRecordByUserID(this.intUserID, this.intPage, this.intPerPage, this.intTotal, this.intCount);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
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
                    this.strList = string.Concat(new object[] { strList, "<tr align='center'  onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='23'>", teamNameByTeamID, " ", num2, ":", num3, " ", str3, "</td><td>", num4, "</td><td>", num5, "</td></tr><tr><td colspan='3' height='1' background='", SessionItem.GetImageURL(), "RM/Border_07.gif'></td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='23' align='right' colspan='4'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr></table>";
            }
            else
            {
                this.strList = this.strList + "<tr align='center'><td height='25' colspan='3'>暂时没有竞猜记录</td></tr></table>";
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            this.tblGuess.Visible = false;
            this.tblNoGuess.Visible = false;
            this.btnTrue.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.strType = SessionItem.GetRequest("TYPE", 1).ToString();
            if (this.intUserID > 0)
            {
                switch (this.strType)
                {
                    case "MONTH":
                        this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_01.gif' border='0' height='14' width='50'><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><a href='RM.aspx?Type=MYRM&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_03.gif' border='0' height='14' width='59'></a><a href='RM.aspx?Type=GUESS'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_04.gif' border='0' height='14' width='32'></a>";
                        this.MonthList();
                        return;

                    case "YEAR":
                        this.strPageIntro = "<a href='RM.aspx?Type=MONTH'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_01.gif' border='0' height='14' width='50'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_02.gif' border='0' height='14' width='50'><a href='RM.aspx?Type=MYRM&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_03.gif' border='0' height='14' width='59'></a><a href='RM.aspx?Type=GUESS'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_04.gif' border='0' height='14' width='32'></a>";
                        this.YearList();
                        return;

                    case "MYRM":
                        this.strPageIntro = "<a href='RM.aspx?Type=MONTH'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_01.gif' border='0' height='14' width='50'></a><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_03.gif' border='0' height='14' width='59'><a href='RM.aspx?Type=GUESS'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_04.gif' border='0' height='14' width='32'></a>";
                        this.MyRMList();
                        return;

                    case "GUESS":
                        this.strPageIntro = "<a href='RM.aspx?Type=MONTH'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_01.gif' border='0' height='14' width='50'></a><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><a href='RM.aspx?Type=MYRM&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_03.gif' border='0' height='14' width='59'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_04.gif' border='0' height='14' width='32'>";
                        this.GuessList();
                        return;
                }
                this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_01.gif' border='0' height='14' width='50'><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><a href='RM.aspx?Type=MYRM&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_03.gif' border='0' height='14' width='59'></a><a href='RM.aspx?Type=GUESS'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_04.gif' border='0' height='14' width='32'></a>";
                this.MonthList();
            }
            else
            {
                string strType = this.strType;
                if (strType != null)
                {
                    switch (strType)
                    {
                        case "MONTH":
                            this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_01.gif' border='0' height='14' width='50'><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_03.gif' border='0' height='14' width='59'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_04.gif' border='0' height='14' width='32'>";
                            this.MonthList();
                            return;

                        case "YEAR":
                            this.strPageIntro = "<a href='RM.aspx?Type=MONTH'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_01.gif' border='0' height='14' width='50'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_02.gif' border='0' height='14' width='50'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_03.gif' border='0' height='14' width='59'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_04.gif' border='0' height='14' width='32'>";
                            this.YearList();
                            return;
                    }
                }
                this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_C_01.gif' border='0' height='14' width='50'><a href='RM.aspx?Type=YEAR'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_02.gif' border='0' height='14' width='50'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_03.gif' border='0' height='14' width='59'><img src='" + SessionItem.GetImageURL() + "MenuCard/RM/RMmenu_G_04.gif' border='0' height='14' width='32'>";
                this.MonthList();
            }
        }

        private void YearList()
        {
            int num = 1;
            DataTable topYearScore = RMYearScoreManager.GetTopYearScore();
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td width='33' height='25'>排名</td><td width='127'>会员</td><td width='50'>积分</td></tr>";
            if (topYearScore == null)
            {
                this.strList = this.strList + "<tr align='center'><td colspan='3' height='25'>暂时没有年度排名</td></tr></table>";
            }
            else
            {
                foreach (DataRow row in topYearScore.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    int num3 = (int) row["YearPoint"];
                    string str = ROOTUserManager.GetUserInfoByID(intUserID)["NickName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", num, "</td><td>", str, "</td><td>", num3, "</td></tr><tr><td colspan='3' height='1' background='", SessionItem.GetImageURL(), "RM/Border_07.gif'></td></tr>" });
                    num++;
                }
                this.strList = this.strList + "</table>";
            }
        }
    }
}


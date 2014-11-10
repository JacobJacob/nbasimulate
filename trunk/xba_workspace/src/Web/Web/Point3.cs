namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class Point3 : Page
    {
        private int intClubID;
        public int intPage;
        public int intPrePage = 10;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        public StringBuilder sbList = new StringBuilder();
        public StringBuilder sbMatchList = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder("");
        public string strPageIntro;
        private string strType;
        protected HtmlTable tblArenaList;
        protected HtmlTable tblHistoryList;
        protected HtmlTable tblMyMatchList;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int arenaMatchTableCount = 0;
            if (this.strType == "MYMATCH")
            {
                arenaMatchTableCount = BTPArenaManager.GetArenaMatchTableCount(this.intClubID);
            }
            return arenaMatchTableCount;
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
            this.tblArenaList.Visible = false;
            this.tblMyMatchList.Visible = false;
            this.tblHistoryList.Visible = false;
        }

        private void List()
        {
            bool flag = false;
            if ((BTPGameManager.GetGameDays() == 15) && (DateTime.Now >= DateTime.Today.AddHours(10.0)))
            {
                flag = true;
            }
            foreach (DataRow row in BTPPoint3Manager.GetPoint3MatchTable(this.intClubID).Rows)
            {
                long longPlayerID = Convert.ToInt64(row["PlayerID"]);
                int num3 = Convert.ToInt32(row["Number"]);
                string strName = Convert.ToString(row["Name"]);
                int num4 = Convert.ToInt32(row["Age"]);
                int num5 = 0;
                int num6 = 0;
                int num7 = 0;
                int num8 = 0;
                int num9 = 0;
                int num10 = 0;
                int num11 = 0;
                int num12 = 0;
                int num13 = 0;
                int num14 = 0;
                int num15 = 0;
                int num16 = 0;
                if (row["Status"] != DBNull.Value)
                {
                    num6 = Convert.ToInt32(row["Status"]);
                    num7 = Convert.ToInt32(row["ShotA"]);
                    num8 = Convert.ToInt32(row["ShotAS"]);
                    num9 = Convert.ToInt32(row["ShotB"]);
                    num10 = Convert.ToInt32(row["ShotBS"]);
                    num11 = Convert.ToInt32(row["ShotC"]);
                    num12 = Convert.ToInt32(row["ShotCS"]);
                    num13 = Convert.ToInt32(row["ShotD"]);
                    num14 = Convert.ToInt32(row["ShotDS"]);
                    num15 = Convert.ToInt32(row["ShotE"]);
                    num16 = Convert.ToInt32(row["ShotES"]);
                    num5 = Convert.ToInt32(row["TotalPoint"]);
                }
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num3, ".gif\" width='16' height='19' border='0'></td>" }));
                this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 5, 1, 1) + "</td>");
                this.sbList.Append("<td>" + num4 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num7, "/", num8, "</td>" }));
                this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num9, "/", num10, "</td>" }));
                this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num11, "/", num12, "</td>" }));
                this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num13, "/", num14, "</td>" }));
                this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num15, "/", num16, "</td>" }));
                this.sbList.Append("<td align='center' >" + num5 + "</td>");
                switch (num6)
                {
                    case 1:
                        this.sbList.Append("<td>");
                        this.sbList.Append("练习中");
                        this.sbList.Append("</td>");
                        break;

                    case 2:
                        this.sbList.Append("<td>");
                        this.sbList.Append("比赛中");
                        this.sbList.Append("</td>");
                        break;

                    case 0:
                        this.sbList.Append("<td>");
                        this.sbList.Append("<a title='一个球员可以有无数次的试投的机会' href='SecretaryPage.aspx?Type=POINT3MATCHREG&Flg=1&PlayerID=" + longPlayerID + "'>试投</a>&nbsp;&nbsp;");
                        if (flag)
                        {
                            this.sbList.Append("<a title='一个球员只有一次参赛的机会' href='SecretaryPage.aspx?Type=POINT3MATCHREG&Flg=2&PlayerID=" + longPlayerID + "'>比赛</a>");
                        }
                        else
                        {
                            this.sbList.Append("未开始");
                        }
                        this.sbList.Append("</td>");
                        break;

                    default:
                        this.sbList.Append("<td>");
                        this.sbList.Append("--");
                        this.sbList.Append("--");
                        this.sbList.Append("</td>");
                        break;
                }
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='12'></td></tr>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intUserID = (int) onlineRowByUserID["UserID"];
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                this.intWealth = (int) onlineRowByUserID["Wealth"];
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void Rank()
        {
            DataTable table = BTPPoint3Manager.GetPoint3MatchTopTable();
            if (table == null)
            {
                this.sbList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有排名</td></tr>");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    long longPlayerID = Convert.ToInt64(row["PlayerID"]);
                    int num2 = Convert.ToInt32(row["Number"]);
                    string strName = Convert.ToString(row["Name"]);
                    int num3 = Convert.ToInt32(row["Age"]);
                    int num4 = 0;
                    int num5 = 0;
                    int num6 = 0;
                    int num7 = 0;
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    int num11 = 0;
                    int num12 = 0;
                    int num13 = 0;
                    int num14 = 0;
                    if (row["Status"] != DBNull.Value)
                    {
                        Convert.ToInt32(row["Status"]);
                        num5 = Convert.ToInt32(row["ShotA"]);
                        num6 = Convert.ToInt32(row["ShotAS"]);
                        num7 = Convert.ToInt32(row["ShotB"]);
                        num8 = Convert.ToInt32(row["ShotBS"]);
                        num9 = Convert.ToInt32(row["ShotC"]);
                        num10 = Convert.ToInt32(row["ShotCS"]);
                        num11 = Convert.ToInt32(row["ShotD"]);
                        num12 = Convert.ToInt32(row["ShotDS"]);
                        num13 = Convert.ToInt32(row["ShotE"]);
                        num14 = Convert.ToInt32(row["ShotES"]);
                        num4 = Convert.ToInt32(row["TotalPoint"]);
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\" width='16' height='19' border='0'></td>" }));
                    this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 5, 1, 1) + "</td>");
                    this.sbList.Append("<td>" + num3 + "</td>");
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num5, "/", num6, "</td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num7, "/", num8, "</td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num9, "/", num10, "</td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num11, "/", num12, "</td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' >", num13, "/", num14, "</td>" }));
                    this.sbList.Append("<td align='center' >" + num4 + "</td>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='12'></td></tr>");
                }
            }
        }

        private void SetPageIntro()
        {
            string strType = this.strType;
            if (strType != null)
            {
                if (strType != "LIST")
                {
                    if (strType != "RANK")
                    {
                        if (strType == "HISTORY")
                        {
                            this.strPageIntro = "<ul><li class='qian2a'><a href='Point3.aspx?Type=LIST'>报名参赛</a></li><li class='qian2a'><a href='Point3.aspx?Type=RANK'>当前排名</a></li><li class='qian1'>历届冠军</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblHistoryList.Visible = true;
                        }
                        return;
                    }
                }
                else
                {
                    this.strPageIntro = "<ul><li class='qian1'>报名参赛</li><li class='qian2a'><a href='Point3.aspx?Type=RANK'>当前排名</a></li><li class='qian2a'><a href='Point3.aspx?Type=HISTORY'>历届冠军</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.List();
                    this.tblArenaList.Visible = true;
                    return;
                }
                this.strPageIntro = "<ul><li class='qian2a'><a href='Point3.aspx?Type=LIST'>报名参赛</a></li><li class='qian1'>当前排名</li><li class='qian2a'><a href='Point3.aspx?Type=HISTORY'>历届冠军</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                this.Rank();
                this.tblMyMatchList.Visible = true;
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class StarMatch : Page
    {
        private int intDays;
        private int intPage;
        private int intPerPage;
        private int intPos;
        public int intServerCategory = ServerParameter.intGameCategory;
        public int intTag;
        private int intTotal;
        public int intUserID;
        public StringBuilder PageIntro1sb;
        public StringBuilder PageIntrosb;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        public string strScript;
        public string strServer = ServerParameter.strCopartner;
        public string strType;
        public string strURL = "";
        protected HtmlTable tblUserAbilityTop;
        protected HtmlTable tbPlayerList;

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }

        private string GetViewPage(string strCurrentURL)
        {
            string str;
            string[] strArray;
            int intTotal = this.intTotal;
            int num2 = (intTotal / this.intPerPage) + 1;
            if ((intTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                str = str2;
                strArray = new string[] { str, "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str = str3;
                strArray = new string[] { str, "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", intTotal, "个记录 跳转", str4 });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intPage = Convert.ToInt32(SessionItem.GetRequest("Page", 0));
                this.intPos = Convert.ToInt32(SessionItem.GetRequest("Pos", 0));
                this.intDays = BTPGameManager.GetGameDays();
                if (this.intPos <= 0)
                {
                    this.intPos = 1;
                }
                if (this.intPage <= 0)
                {
                    this.intPage = 1;
                }
                this.intPerPage = 11;
                this.strType = SessionItem.GetRequest("Type", 1);
                if (this.strType == "1")
                {
                    this.strType = "MATCH";
                }
                this.strURL = Config.GetDomain();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
            this.SetList();
        }

        private void SetList()
        {
            string strCurrentURL = null;
            string str3;
            int num2;
            switch (this.strType)
            {
                case "MATCH":
                {
                    string str4;
                    string str5;
                    strCurrentURL = "StarMatch.aspx?Type=Match&";
                    this.sbList = new StringBuilder();
                    int intSeason = (int) BTPGameManager.GetGameRow()["Season"];
                    DataRow oneStarMatch = BTPStarMatchManager.GetOneStarMatch(intSeason);
                    string str2 = "Images/Club/Logo/10.gif";
                    str3 = "Images/Club/Logo/11.gif";
                    if (oneStarMatch == null)
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='4'>本赛季全明星赛尚未开始</td></tr>");
                        return;
                    }
                    this.intTag = Convert.ToInt32(oneStarMatch["StarMatchID"]);
                    num2 = Convert.ToInt32(oneStarMatch["Status"]);
                    int num3 = Convert.ToInt32(oneStarMatch["ScoreA"]);
                    int num4 = Convert.ToInt32(oneStarMatch["ScoreB"]);
                    bool flag = false;
                    if ((this.intDays > 15) || ((this.intDays == 15) && (DateTime.Now >= DateTime.Today.AddHours(10.0))))
                    {
                        flag = true;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='4'>&nbsp;</td></tr>");
                    this.sbList.Append("  <tr>\n");
                    this.sbList.Append("  <td>\n");
                    this.sbList.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                    this.sbList.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                    this.sbList.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>东部明星队</font><br>\n" }));
                    this.sbList.Append("    </td>\t\t\t\t\t\t\t\t\n");
                    this.sbList.Append("    <td width='10%'><img src='" + str2 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                    if ((num2 == 0) || !flag)
                    {
                        this.sbList.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                        break;
                    }
                    int num5 = num3 / 100;
                    if (num5 == 0)
                    {
                        str4 = "99";
                    }
                    else
                    {
                        str4 = num5.ToString();
                    }
                    int num6 = num4 / 100;
                    if (num6 == 0)
                    {
                        str5 = "99";
                    }
                    else
                    {
                        str5 = num6.ToString();
                    }
                    this.sbList.Append(string.Concat(new object[] { "    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/", str4, ".gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/", (num3 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/", num3 % 10, ".gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/", str5, ".gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/", (num4 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/", num4 % 10, ".gif' border='0' width='19' height='28'>\n" }));
                    break;
                }
                case "VOTEA":
                    this.SetSbList(1);
                    return;

                case "VOTEB":
                    this.SetSbList(2);
                    return;

                case "HISTORY":
                    strCurrentURL = "StarMatch.aspx?Type=HISTORY&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">序号</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">赛季</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">东部明星</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">比分</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">西部明星</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">全明星MVP</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPStarMatchManager.GetStarMatchCount();
                    if (this.intTotal > 0)
                    {
                        DataTable starMatch = BTPStarMatchManager.GetStarMatch(this.intPage, this.intPerPage);
                        if (starMatch == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无比赛</td></tr>");
                            return;
                        }
                        int num7 = 1;
                        foreach (DataRow row3 in starMatch.Rows)
                        {
                            int num8 = Convert.ToInt32(row3["Season"]);
                            int num9 = Convert.ToInt32(row3["ScoreA"]);
                            int num10 = Convert.ToInt32(row3["ScoreB"]);
                            string str7 = Convert.ToString(row3["MVPPlayer"]);
                            string str8 = Convert.ToString(row3["ClubNameA"]);
                            string str9 = Convert.ToString(row3["ClubNameB"]);
                            int num11 = Convert.ToInt32(row3["MVPPlayerID"]);
                            int num12 = ((this.intPage - 1) * this.intPerPage) + num7;
                            num7++;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num12 + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num8 + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + str8 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"center\" height=\"24\">", num9, ":", num10, "</td>" }));
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + str9 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"center\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=", num11, "\" target=\"Right\">", str7, "</a></span></td>" }));
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无比赛</td></tr>");
                    return;

                default:
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
            }
            this.sbList.Append("    </td>\t\t\t\t\t\t\t\t\n");
            this.sbList.Append("    <td width='10%' align='center'><img src='" + str3 + "' border='0' width='46' height='46'>\n");
            this.sbList.Append("    </td>\n");
            this.sbList.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'>西部明星队<br>\n" }));
            this.sbList.Append("    </td>\t\t\t\t\t\t\t\n");
            this.sbList.Append("    </tr>\t\t\t\t\n");
            this.sbList.Append("    </table>\n");
            this.sbList.Append("    \n");
            this.sbList.Append("    </td>\n");
            this.sbList.Append("  </tr>\n");
            string str6 = "";
            if (num2 > 0)
            {
                str6 = string.Concat(new object[] { 
                    "<a href='", Config.GetDomain(), "VRep.aspx?Type=9&Tag=", this.intTag, "&A=", 0, "&B=", 0, "' target='_blank'>本场战报</a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=9&Tag=", this.intTag, "&A=", 0, "&B=", 0, 
                    "' target='_blank'>本场统计</a>"
                 });
                if (this.intDays == 15)
                {
                    if (DateTime.Now < DateTime.Today.AddHours(9.0))
                    {
                        str6 = "全明赛即将开始";
                    }
                    else if ((DateTime.Now >= DateTime.Today.AddHours(9.0)) && (DateTime.Now <= DateTime.Today.AddHours(10.0)))
                    {
                        str6 = "<p>&nbsp;</p><p>&nbsp;</p><p>比赛正紧张激烈进行中…</p><p><a onclick=\"NewWin()\" style=\"CURSOR: pointer;\" target=\"_blank\"><img src=\"images/comeongz.gif\" width=\"225\" height=\"52\" border=\"0\"></a></p>";
                    }
                }
            }
            else
            {
                str6 = "全明赛即将开始";
            }
            this.sbList.Append("<tr  id='MatchEndTitle'><td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">" + str6 + "</td>\n");
        }

        private void SetPageIntro()
        {
            string strType = this.strType;
            if (strType != null)
            {
                if (strType != "MATCH")
                {
                    if (strType != "VOTEA")
                    {
                        if (strType == "VOTEB")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1'>东部投票</a></li>");
                            this.sbPageIntro.Append("<li class='qian1'>西部投票</li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
                            return;
                        }
                        if (strType == "HISTORY")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1'>东部投票</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1'>西部投票</a></li>");
                            this.sbPageIntro.Append("<li class='qian1'>历界比赛</li></ul></td>");
                        }
                        return;
                    }
                }
                else
                {
                    this.PageIntro1sb = new StringBuilder();
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>全明星赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1&Zone=1'>东部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1&Zone=2'>西部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
                    return;
                }
                this.sbPageIntro = new StringBuilder();
                this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>");
                this.sbPageIntro.Append("<li class='qian1'>东部投票</li>");
                this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1'>西部投票</a></li>");
                this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
            }
        }

        private void SetPageIntro2()
        {
            this.PageIntro1sb = new StringBuilder();
            switch (this.intPos)
            {
                case 1:
                    this.PageIntro1sb.Append("<font color='#FF0000'>中锋 C</font>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=2&&Page=1'>大前锋 PF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=3&Page=1'>小前锋 SF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=4&Page=1'>得分后卫 SG</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=5&Page=1'>组织后卫 PG</a>");
                    return;

                case 2:
                    this.PageIntro1sb.Append("<a href='StarMatch.aspx?Type=" + this.strType + "&Pos=1&Page=1'>中锋 C</a>");
                    this.PageIntro1sb.Append(" | <font color='#FF0000'>大前锋 PF</font>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=3&Page=1'>小前锋 SF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=4&Page=1'>得分后卫 SG</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=5&Page=1'>组织后卫 PG</a>");
                    return;

                case 3:
                    this.PageIntro1sb.Append("<a href='StarMatch.aspx?Type=" + this.strType + "&Pos=1&Page=1'>中锋 C</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=2&Page=1'>大前锋 PF</a>");
                    this.PageIntro1sb.Append(" | <font color='#FF0000'>小前锋 SF</font>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=4&Page=1'>得分后卫 SG</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=5&Page=1'>组织后卫 PG</a>");
                    return;

                case 4:
                    this.PageIntro1sb.Append("<a href='StarMatch.aspx?Type=" + this.strType + "&Pos=1&Page=1'>中锋 C</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=2&Page=1'>大前锋 PF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=3&Page=1'>小前锋 SF</a>");
                    this.PageIntro1sb.Append(" | <font color='#FF0000'>得分后卫 SG</font>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=5&Page=1'>组织后卫 PG</a>");
                    return;

                case 5:
                    this.PageIntro1sb.Append("<a href='StarMatch.aspx?Type=" + this.strType + "&Pos=1&Page=1'>中锋 C</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=2&Page=1'>大前锋 PF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=3&Page=1'>小前锋 SF</a>");
                    this.PageIntro1sb.Append(" | <a href='StarMatch.aspx?Type=" + this.strType + "&Pos=4&Page=1'>得分后卫 SG</a>");
                    this.PageIntro1sb.Append(" | <font color='#FF0000'>组织后卫 PG</font>");
                    return;
            }
        }

        private void SetSbList(int intZone)
        {
            string str;
            if (intZone == 1)
            {
                str = "StarMatch.aspx?Type=VOTEA&Pos=" + this.intPos + "&";
            }
            else
            {
                str = "StarMatch.aspx?Type=VOTEB&Pos=" + this.intPos + "&";
            }
            this.SetPageIntro2();
            this.sbList = new StringBuilder();
            this.sbList.Append("<tr><td height=\"25\" colspan=\"5\">");
            this.sbList.Append(this.PageIntro1sb);
            this.sbList.Append("</td></tr><tr>");
            this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">序号</td>");
            this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" height=\"24\">球员姓名</td>");
            this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" width=\"20%\">所属球队</td>");
            this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">所在联赛</td>");
            this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">票数</td>");
            this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" width=\"15%\">投票/结果</td>");
            this.sbList.Append("</tr>");
            this.intTotal = BTPStarMatchManager.GetStarPlayerCount(this.intPos, intZone);
            if (this.intTotal > 0)
            {
                DataTable table = BTPStarMatchManager.GetStarPlayer(this.intPos, intZone, this.intPage, this.intPerPage);
                if (table == null)
                {
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                }
                else
                {
                    int num = 1;
                    foreach (DataRow row in table.Rows)
                    {
                        int num2 = Convert.ToInt32(row["PlayerID"]);
                        int num3 = Convert.ToInt32(row["UserID"]);
                        int num4 = Convert.ToInt32(row["Votes"]);
                        string str2 = Convert.ToString(row["ClubName"]);
                        string str3 = Convert.ToString(row["Name"]);
                        string strDevCode = Convert.ToString(row["DevCode"]);
                        Convert.ToInt32(row["ClubID"]);
                        int num5 = Convert.ToInt32(row["Status"]);
                        int num6 = ((this.intPage - 1) * this.intPerPage) + num;
                        num++;
                        this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append("<td align=\"center\" height=\"24\">" + num6 + "</td>");
                        this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=", num2, "\" target=\"Right\">", str3, "</a></span></td>" }));
                        this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=", num3, "\" title=\"", str2, "\" target=\"Right\">", str2, "</a></td>" }));
                        this.sbList.Append("<td align=\"center\" >" + DevCalculator.GetDev(strDevCode) + "</td>");
                        this.sbList.Append("<td align=\"center\" >" + num4 + "</td>");
                        switch (num5)
                        {
                            case 2:
                                this.sbList.Append("<td align=\"center\" >首发</td>");
                                break;

                            case 3:
                                this.sbList.Append("<td align=\"center\" >1号替补</td>");
                                break;

                            case 4:
                                this.sbList.Append("<td align=\"center\" >2号替补</td>");
                                break;

                            case 1:
                                if (Convert.ToInt32(BTPAccountManager.GetAccountRowByUserID(this.intUserID)["PayType"]) == 1)
                                {
                                    if (((this.strType == "VOTEA") && ((this.intUserID % 2) == 1)) || ((this.strType == "VOTEB") && ((this.intUserID % 2) == 0)))
                                    {
                                        this.sbList.Append("<td align=\"center\" >不同赛区</td>");
                                    }
                                    else if (BTPStarMatchManager.GetVoteRecord(this.intUserID, this.intPos) == null)
                                    {
                                        this.sbList.Append("<td align=\"center\" ><a href='SecretaryPage.aspx?Type=STARVOTE&PlayerID=" + num2 + "'>投票</a></td>");
                                    }
                                    else
                                    {
                                        this.sbList.Append("<td align=\"center\" >已投过票</td>");
                                    }
                                }
                                else
                                {
                                    this.sbList.Append("<td align=\"center\" title=\"非会员不能投票\">投票</td>");
                                }
                                break;

                            default:
                                this.sbList.Append("<td align=\"center\" >未当选</td>");
                                break;
                        }
                        this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                    }
                    this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(str) + "</td></tr>");
                    this.strScript = this.GetScript(str);
                }
            }
            else
            {
                this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无数据</td></tr>");
            }
        }
    }
}


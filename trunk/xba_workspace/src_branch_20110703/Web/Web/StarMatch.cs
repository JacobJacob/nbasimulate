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

        private int intPage;
        private int intPerPage;
        private int intTotal;
        public string strType;
        private int intUserID;
        private int intPos;
        public string strScript;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;

        public StringBuilder PageIntro1sb;
        public StringBuilder PageIntrosb;

        protected HtmlTable tblUserAbilityTop;
        protected HtmlTable tbPlayerList;

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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intPage = Convert.ToInt32(SessionItem.GetRequest("Page", 0));
                this.intPos = Convert.ToInt32(SessionItem.GetRequest("Pos", 0));

                if (this.intPos <= 0)
                {
                    this.intPos = 1;
                }

                if (this.intPage <= 0)
                {
                    this.intPage = 1;
                }
                this.intPerPage = 11;
                this.strType = (string)SessionItem.GetRequest("Type", 1);
                if (this.strType == "1")
                {
                    this.strType = "MATCH";
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
            this.SetList();
        }

        private string GetViewPage(string strCurrentURL)
        {
            string str5;
            string[] strArray;
            int msgTotal = this.intTotal;
            int totalPage = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                totalPage--;
            }
            if (totalPage == 0)
            {
                totalPage = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                str5 = str2;
                strArray = new string[6];
                strArray[0] = str5;
                strArray[1] = "<a href='";
                strArray[2] = strCurrentURL;
                strArray[3] = "Page=";
                int num4 = this.intPage - 1;
                strArray[4] = num4.ToString();
                strArray[5] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == totalPage)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str5 = str3;
                strArray = new string[] { str5, "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= totalPage; i++)
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
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }


        private void SetList()
        {
            string strCurrentURL = null;

            DataTable dataTable;
            switch (this.strType)
            {
                case "MATCH":

                    strCurrentURL = "StarMatch.aspx?Type=Match&";

                    this.sbList = new StringBuilder();
                    DataRow gameRow = BTPGameManager.GetGameRow();
                    int intSeason = (int) gameRow["Season"];
                    DataRow starMatch = BTPStarMatchManager.GetOneStarMatch(intSeason);
                    string strClubALogo = "Images/Club/Logo/10.gif";
                    string strClubBLogo = "Images/Club/Logo/11.gif";
                    if (starMatch != null)
                    {

                        int intStarMatchID = Convert.ToInt32(starMatch["StarMatchID"]);
                        int intStatus = Convert.ToInt32(starMatch["Status"]);
                        int intScoreA = Convert.ToInt32(starMatch["ScoreA"]);
                        int intScoreB = Convert.ToInt32(starMatch["ScoreB"]);

                        this.sbList.Append("<tr><td height='30' align='center' colspan='4'>&nbsp;</td></tr>");
                        this.sbList.Append("  <tr>\n");
                        this.sbList.Append("  <td>\n");
                        this.sbList.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                        this.sbList.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                        this.sbList.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>东部明星队</font><br>\n" }));
                        this.sbList.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        this.sbList.Append("    <td width='10%'><img src='" + strClubALogo + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                        if (intStatus != 0)
                        {
                            string str17;
                            string str18;
                            int num33 = intScoreA / 100;
                            if (num33 == 0)
                            {
                                str17 = "99";
                            }
                            else
                            {
                                str17 = num33.ToString();
                            }
                            int num34 = intScoreB / 100;
                            if (num34 == 0)
                            {
                                str18 = "99";
                            }
                            else
                            {
                                str18 = num34.ToString();
                            }
                            this.sbList.Append(string.Concat(new object[] { "    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/", str17, ".gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/", (intScoreA / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/", intScoreA % 10, ".gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/", str18, ".gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/", (intScoreB / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/", intScoreB % 10, ".gif' border='0' width='19' height='28'>\n" }));
                        }
                        else
                        {
                            this.sbList.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                        }
                        this.sbList.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        this.sbList.Append("    <td width='10%' align='center'><img src='" + strClubBLogo + "' border='0' width='46' height='46'>\n");
                        this.sbList.Append("    </td>\n");
                        this.sbList.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'>西部明星队<br>\n" }));
                        this.sbList.Append("    </td>\t\t\t\t\t\t\t\n");
                        this.sbList.Append("    </tr>\t\t\t\t\n");
                        this.sbList.Append("    </table>\n");
                        this.sbList.Append("    \n");
                        this.sbList.Append("    </td>\n");
                        this.sbList.Append("  </tr>\n");

                        string str = "";

                        if(intStatus > 0)
                        {
                            str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=9&Tag=", intStarMatchID, "&A=", 0, "&B=", 0, "' target='_blank'>本场战报</a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=9&Tag=", intStarMatchID, "&A=", 0, "&B=", 0, "' target='_blank'>本场统计</a>" });
                        }
                        else
                        {
                            str = "全明赛即将开始";

                        }

                        this.sbList.Append("<tr  id='MatchEndTitle'><td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">" + str + "</td>\n");
                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='4'>本赛季全明星赛尚未开始</td></tr>");
                    }
                    
                    return;

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
                        dataTable = BTPStarMatchManager.GetStarMatch(this.intPage, this.intPerPage);

                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无比赛</td></tr>");
                            return;
                        }

                        int count = 1;

                        foreach (DataRow row in dataTable.Rows)
                        {

                            int season = Convert.ToInt32(row["Season"]);
                            int intScoreA = Convert.ToInt32(row["ScoreA"]);
                            int intScoreB = Convert.ToInt32(row["ScoreB"]);
                            string strMvpName = Convert.ToString(row["MVPPlayer"]);
                            string strClubNameA = Convert.ToString(row["ClubNameA"]);
                            string strClubNameB= Convert.ToString(row["ClubNameB"]);
                            
                            int intMvpID = Convert.ToInt32(row["MVPPlayerID"]);

                            int intSort = (this.intPage - 1) * this.intPerPage + count;
                            count++;


                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                           
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intSort + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + season + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + strClubNameA + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intScoreA + ":" + intScoreB + "</td>");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + strClubNameB + "</td>");
                            this.sbList.Append("<td align=\"center\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=" + intMvpID + "\" target=\"Right\">" + strMvpName + "</a></span></td>");
                        
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);

                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无比赛</td></tr>");

                    }

                    return;

                default:
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
            }

        }

        private void SetSbList(int intZone)
        {

            string strCurrentURL;
            if (intZone == 1)
            {
                strCurrentURL = "StarMatch.aspx?Type=VOTEA&Pos=" + this.intPos + "&";
            }
            else
            {
                strCurrentURL = "StarMatch.aspx?Type=VOTEB&Pos=" + this.intPos + "&";
            }
            
            DataTable dataTable;

            //设置子导航
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
                dataTable = BTPStarMatchManager.GetStarPlayer(this.intPos, intZone, this.intPage, this.intPerPage);

                if (dataTable == null)
                {
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;
                }

                int count = 1;

                foreach (DataRow row in dataTable.Rows)
                {

                    int intPlayerID = Convert.ToInt32(row["PlayerID"]);
                    int intUserID = Convert.ToInt32(row["UserID"]);
                    int intVotes = Convert.ToInt32(row["Votes"]);
                    string strClubName = Convert.ToString(row["ClubName"]);
                    string strName = Convert.ToString(row["Name"]);
                    string strDevCode = Convert.ToString(row["DevCode"]);
           
                    int intClubID = Convert.ToInt32(row["ClubID"]);
           
                    int intStatus = Convert.ToInt32(row["Status"]);

                    int intSort = (this.intPage - 1) * this.intPerPage + count;
                    count++;

                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align=\"center\" height=\"24\">" + intSort + "</td>");
                    this.sbList.Append("<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=" + intPlayerID + "\" target=\"Right\">" + strName + "</a></span></td>");  
                    this.sbList.Append("<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");
                    this.sbList.Append("<td align=\"center\" >" + DevCalculator.GetDev(strDevCode) + "</td>");
                    this.sbList.Append("<td align=\"center\" >" + intVotes + "</td>");
                    if (intStatus == 1)
                    {
                        DataRow accountRow = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                        int intPayType = Convert.ToInt32(accountRow["PayType"]);
                        if(intPayType == 1)
                        {
                            if ((this.strType == "VOTEA" && this.intUserID % 2 == 1) || (this.strType == "VOTEB" && this.intUserID % 2 == 0))
                            {
                                this.sbList.Append("<td align=\"center\" >不同赛区</td>");
                            }
                            else
                            {
                                DataRow voteRecordRow = BTPStarMatchManager.GetVoteRecord(this.intUserID, this.intPos);
                                if (voteRecordRow == null)
                                {
                                    this.sbList.Append("<td align=\"center\" ><a href='SecretaryPage.aspx?Type=STARVOTE&PlayerID=" + intPlayerID + "'>投票</a></td>");
                                }
                                else
                                {
                                    this.sbList.Append("<td align=\"center\" >已投过票</td>");
                                }
                            }
                        }
                        else
                        {
                            this.sbList.Append("<td align=\"center\" title=\"非会员不能投票\">投票</td>");
                        }
                    }
                    else if (intStatus == 2)
                    {
                        this.sbList.Append("<td align=\"center\" >首发</td>");
                    }
                    else if (intStatus == 3)
                    {
                        this.sbList.Append("<td align=\"center\" >1号替补</td>");
                    }
                    else if (intStatus == 4)
                    {
                        this.sbList.Append("<td align=\"center\" >2号替补</td>");
                    }
                    else
                    {
                        this.sbList.Append("<td align=\"center\" >未当选</td>");
                    }
                        this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                }
                this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                this.strScript = this.GetScript(strCurrentURL);

            }
            else
            {
                this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无数据</td></tr>");

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

        private void SetPageIntro()
        {
            switch (this.strType)
            {
                case "MATCH":

                    this.PageIntro1sb = new StringBuilder();

                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>全明星赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1&Zone=1'>东部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1&Zone=2'>西部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
                    return;

                case "VOTEA":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian1'>东部投票</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1'>西部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
                    return;

                case "VOTEB":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1'>东部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian1'>西部投票</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=HISTORY'>历届比赛</a></li></ul></td>");
                    return;


                case "HISTORY":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=MATCH'>全明星赛</a></li>"); 
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\"'  href='StarMatch.aspx?Type=VOTEA&Page=1'>东部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/StarMatch.htm\" 'href='StarMatch.aspx?Type=VOTEB&Page=1'>西部投票</a></li>");
                    this.sbPageIntro.Append("<li class='qian1'>历界比赛</li></ul></td>");
                    return;
            }

        }
    }
}


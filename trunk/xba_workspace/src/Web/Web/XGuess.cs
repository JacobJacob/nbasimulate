namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class XGuess : Page
    {
        private int intPage;
        private int intPerPage;
        private int intTotal;
        private int intUserID;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        public string strScript;
        public string strType;
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
                if (this.intPage <= 0)
                {
                    this.intPage = 1;
                }
                this.intPerPage = 13;
                this.strType = SessionItem.GetRequest("Type", 1);
                if (this.strType == "1")
                {
                    this.strType = "CLUBLIST";
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

        private void SetList()
        {
            string strCurrentURL = null;
            DataTable xGuess;
            switch (this.strType)
            {
                case "CLUBLIST":
                    strCurrentURL = "XGuess.aspx?Type=CLUBLIST&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球队名</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">夺冠赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">下注</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPXGuessManager.GetXGuessCount();
                    if (this.intTotal > 0)
                    {
                        xGuess = BTPXGuessManager.GetXGuess(this.intPage, this.intPerPage);
                        if (xGuess == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='4'>暂无数据</td></tr>");
                            return;
                        }
                        int num = 1;
                        int xGameRound = BTPXGameManager.GetXGameRound();
                        foreach (DataRow row in xGuess.Rows)
                        {
                            string str2 = Convert.ToString(row["ClubName"]);
                            Convert.ToInt32(row["ClubID"]);
                            int num3 = Convert.ToInt32(row["UserID"]);
                            int num4 = Convert.ToInt32(row["GuessID"]);
                            double num5 = Convert.ToDouble(row["Odds"]);
                            int num6 = ((this.intPage - 1) * this.intPerPage) + num;
                            num++;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num6 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num3, "\" title=\"", str2, "\" target=\"Right\">", str2, "</a></td>" }));
                            this.sbList.Append(" <td align=\"center\">" + num5 + "</td>");
                            if (xGameRound > 15)
                            {
                                this.sbList.Append("<td align=\"center\">竞猜时间已过</td>");
                            }
                            else
                            {
                                this.sbList.Append("<td align=\"center\"><a href='SecretaryPage.aspx?Type=XGUESS&GuessID=" + num4 + "'>下注</a></td>");
                            }
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='4'>暂无数据</td></tr>");
                    return;

                case "MYGUESS":
                    strCurrentURL = "XGuess.aspx?Type=MYGUESS&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">序号</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" height=\"24\">竞猜冠军</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">竞猜资金</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">盈亏</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPXGuessManager.GetXGuessRecordCount(this.intUserID);
                    if (this.intTotal > 0)
                    {
                        xGuess = BTPXGuessManager.GetXGuessRecord(this.intUserID, this.intPage, this.intPerPage);
                        if (xGuess == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        int num7 = 1;
                        foreach (DataRow row2 in xGuess.Rows)
                        {
                            Convert.ToInt32(row2["UserID"]);
                            string str3 = Convert.ToString(row2["ClubName"]);
                            double num8 = Convert.ToDouble(row2["Odds"]);
                            int intClubID = Convert.ToInt32(row2["ClubID"]);
                            int num10 = Convert.ToInt32(row2["Money"]);
                            int num11 = Convert.ToInt32(row2["Status"]);
                            int num12 = ((this.intPage - 1) * this.intPerPage) + num7;
                            num7++;
                            int num13 = Convert.ToInt32(BTPAccountManager.GetAccountRowByClubID5(intClubID)["UserID"]);
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num12 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=", num13, "\" title=\"", str3, "\" target=\"Right\">", str3, "</a></td>" }));
                            this.sbList.Append("<td align=\"center\" >" + num8 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num10 + "</td>");
                            switch (num11)
                            {
                                case 1:
                                    this.sbList.Append("<td align=\"center\" >未平盘</td>");
                                    break;

                                case 2:
                                    this.sbList.Append("<td align=\"center\" ><font color=\"red\">+" + ((int) (num8 * num10)) + "</font></td>");
                                    break;

                                default:
                                    this.sbList.Append("<td align=\"center\" ><font color=\"green\">-" + num10 + "</font></td>");
                                    break;
                            }
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无数据</td></tr>");
                    return;

                case "NEWGUESS":
                    strCurrentURL = "XGuess.aspx?Type=NEWGUESS&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">序号</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" height=\"24\">竞猜经理</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">竞猜冠军</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">竞猜资金</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">盈亏</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPXGuessManager.GetXGuessRecordCount(0);
                    if (this.intTotal > 0)
                    {
                        xGuess = BTPXGuessManager.GetXGuessRecord(0, this.intPage, this.intPerPage);
                        if (xGuess == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        int num15 = 1;
                        foreach (DataRow row4 in xGuess.Rows)
                        {
                            int intUserID = Convert.ToInt32(row4["UserID"]);
                            string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                            string str5 = Convert.ToString(row4["ClubName"]);
                            double num17 = Convert.ToDouble(row4["Odds"]);
                            int num18 = Convert.ToInt32(row4["ClubID"]);
                            int num19 = Convert.ToInt32(row4["Money"]);
                            int num20 = Convert.ToInt32(row4["Status"]);
                            int num21 = ((this.intPage - 1) * this.intPerPage) + num15;
                            num15++;
                            int num22 = Convert.ToInt32(BTPAccountManager.GetAccountRowByClubID5(num18)["UserID"]);
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num21 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=", intUserID, "\" title=\"", nickNameByUserID, "\" target=\"Right\">", nickNameByUserID, "</a></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=", num22, "\" title=\"", str5, "\" target=\"Right\">", str5, "</a></td>" }));
                            this.sbList.Append("<td align=\"center\" >" + num17 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num19 + "</td>");
                            switch (num20)
                            {
                                case 1:
                                    this.sbList.Append("<td align=\"center\" >未平盘</td>");
                                    break;

                                case 2:
                                    this.sbList.Append("<td align=\"center\" ><font color=\"red\">+" + ((int) (num17 * num19)) + "</font></td>");
                                    break;

                                default:
                                    this.sbList.Append("<td align=\"center\" ><font color=\"green\">-" + num19 + "</font></td>");
                                    break;
                            }
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无数据</td></tr>");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void SetPageIntro()
        {
            string strType = this.strType;
            if (strType != null)
            {
                if (strType != "CLUBLIST")
                {
                    if (strType != "MYGUESS")
                    {
                        if (strType == "NEWGUESS")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"' href='XGuess.aspx?Type=CLUBLIST'>竞猜下注</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'  href='XGuess.aspx?Type=MYGUESS&Page=1'>我的下注</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>最新下注</li></ul></td>");
                        }
                        return;
                    }
                }
                else
                {
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>竞猜下注</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'  href='XGuess.aspx?Type=MYGUESS&Page=1'>我的下注</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\" 'href='XGuess.aspx?Type=NEWGUESS'>最新下注</a></li></ul></td>");
                    return;
                }
                this.sbPageIntro = new StringBuilder();
                this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"' href='XGuess.aspx?Type=CLUBLIST'>竞猜下注</a></li>");
                this.sbPageIntro.Append("<li class='qian2'>我的下注</li>");
                this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'href='XGuess.aspx?Type=NEWGUESS'>最新下注</a></li></ul></td>");
            }
        }
    }
}


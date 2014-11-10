namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Net;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using Web.DBData;
    using Web.Helper;

    public class OnlyOneCenter : Page
    {
        protected ImageButton btnFindName;
        protected ImageButton btnOK;
        protected HtmlInputHidden hidCheck;
        private int intClubID5;
        private int intPage = 1;
        private int intPerPage = 10;
        private int intUserID;
        private int intWealth;
        public StringBuilder sbList = new StringBuilder("");
        public StringBuilder sbPageIntro = new StringBuilder("");
        public StringBuilder sbScript = new StringBuilder("");
        private string strClubLogo;
        private string strNickName;
        public string strOnLoad = "";
        public string strRegCount = "";
        private string strType;
        protected HtmlTable tblMatchMy;
        protected HtmlTable tblMatchReg;
        protected HtmlTable tblMatchRun;
        protected HtmlTable tblTop;
        protected TextBox tbNickName;
        protected CheckBox tbOnlyPay;
        protected TextBox tbTopWealth;
        protected HtmlTableRow trFindName;
        protected HtmlTableCell trMsg;
        protected HtmlTableRow trReg;
        protected HtmlTableCell trTitle;

        private void btnFindName_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbNickName.Text.Trim();
            if ((strIn == "") && !StringItem.IsValidName(strIn, 1, 20))
            {
                base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHTODAY&ClubID=0");
            }
            else
            {
                DataRow accountRowByClubName = BTPAccountManager.GetAccountRowByClubName(strIn);
                if (accountRowByClubName == null)
                {
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHTODAY&ClubID=0");
                }
                else
                {
                    int num = (int) accountRowByClubName["ClubID5"];
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHTODAY&ClubID=" + num);
                }
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (DateTime.Now.Hour < 10)
            {
                base.Response.Redirect("SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHREG&Status=-8");
            }
            else
            {
                int num = BTPOnlyOneCenterReg.OnlyOneCenterReg(this.intUserID, 0, 0);
                base.Response.Redirect("SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHREG&Status=" + num);
            }
        }

        private void btnOK_Click2(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT");
        }

        [AjaxMethod]
        public string GetCount()
        {
            int onlyOneCountByStatus = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0, 0);
            int num2 = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0x63, 0);
            return string.Concat(new object[] { "球队数/等待中：", num2, "/", onlyOneCountByStatus });
        }

        private void GetMsgScript(string strCurrentURL)
        {
            this.sbScript.Append("<script language=\"javascript\">");
            this.sbScript.Append("function JumpPage()");
            this.sbScript.Append("{");
            this.sbScript.Append("var strPage=document.all.Page.value;");
            this.sbScript.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.sbScript.Append("}");
            this.sbScript.Append("</script>");
        }

        private int GetMsgTotal()
        {
            int onlyOneTodayCount = 0;
            string request = SessionItem.GetRequest("Type", 1);
            switch (request)
            {
                case "MATCHREG":
                    return BTPOnlyOneCenterReg.GetOnlyOneRegOnePayCount(SessionItem.CheckLogin(1));

                case "MATCHMY":
                case "MATCHTODAY":
                    if (this.strType == "MATCHTODAY")
                    {
                        int intClubID = SessionItem.GetRequest("ClubID", 0);
                        return BTPOnlyOneCenterReg.GetOnlyOneCountMy(intClubID);
                    }
                    return BTPOnlyOneCenterReg.GetOnlyOneCountMy(this.intClubID5);

                case "TOPTODAY":
                    onlyOneTodayCount = BTPOnlyOneCenterReg.GetOnlyOneTodayCount();
                    break;
            }
            if (request == "TOPTODAY")
            {
                onlyOneTodayCount = BTPOnlyOneCenterReg.GetOnlyOneTodayCount();
            }
            else
            {
                onlyOneTodayCount = BTPOnlyOneCenterReg.GetOnlyOneTopCount();
            }
            if (onlyOneTodayCount > 100)
            {
                onlyOneTodayCount = 100;
            }
            return onlyOneTodayCount;
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string str;
            string[] strArray;
            int request = SessionItem.GetRequest("Page", 0);
            if (request < 1)
            {
                request = 1;
            }
            int msgTotal = this.GetMsgTotal();
            int num3 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num3--;
            }
            if (num3 == 0)
            {
                num3 = 1;
            }
            string str2 = "";
            str2 = "";
            if (request == 1)
            {
                str2 = str2 + "上一页";
            }
            else
            {
                str = str2;
                strArray = new string[] { str, "<a href='", strCurrentURL, "Page=", (request - 1).ToString(), "'>上一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (request == num3)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str = str3;
                strArray = new string[] { str, "<a href='", strCurrentURL, "Page=", (request + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num3; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == request)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        [AjaxMethod]
        public string GetOnlyCenter()
        {
            StringBuilder builder = new StringBuilder();
            int intUserID = SessionItem.CheckLogin(1);
            DataRow onlyOneMatchRow = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(intUserID);
            if (onlyOneMatchRow == null)
            {
                return "-1";
            }
            byte byeStatus = (byte) onlyOneMatchRow["Status"];
            DateTime time = (DateTime) onlyOneMatchRow["StatusTime"];
            if ((byeStatus == 1) && (time.AddSeconds(100.0) < DateTime.Now))
            {
                byeStatus = 2;
            }
            if (byeStatus == 0)
            {
                builder.Append("<table cellSpacing=\"0\" cellPadding=\"4\" width=\"552\" border=\"0\" >");
                int num3 = (int) onlyOneMatchRow["TopWealth"];
                int num4 = (int) onlyOneMatchRow["Win"];
                int num1 = (int) onlyOneMatchRow["Box"];
                int num5 = (int) onlyOneMatchRow["Point"];
                int num6 = (int) onlyOneMatchRow["Wealth"];
                bool flag = (bool) onlyOneMatchRow["OnlyPay"];
                onlyOneMatchRow = BTPAccountManager.GetAccountRowByUserID(intUserID);
                if (onlyOneMatchRow == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=12");
                    return "";
                }
                if (!flag)
                {
                    long num16 = (long) onlyOneMatchRow["OnlyPoint"];
                    this.strClubLogo = onlyOneMatchRow["ClubLogo"].ToString().Trim();
                    string str = this.RepBadWord(onlyOneMatchRow["ClubName"].ToString().Trim());
                    string str2 = this.RepBadWord(onlyOneMatchRow["NickName"].ToString().Trim());
                    string str3 = "0/" + num5;
                    string str4 = "0/" + (num6 + num3);
                    str3 = "<a style=\"cursor:hand;\" title=\"应得积分/总积分\">积分：" + str3 + "</a>";
                    builder.Append("  <tr>\n");
                    builder.Append(string.Concat(new object[] { "    <td height=\"20\" bgcolor=\"#fcc6a4\"><span style=\"padding-right:12px\">第 ", num4 + 1, " 场比赛</span><span style=\"padding-left:230px\">", str3, "<span style=\"width:30px\"></span><a style=\"cursor:hand;\" title=\"本场奖金/当前奖金\">奖金：", str4, "</a></span></td>\n" }));
                    builder.Append("  </tr>\n");
                    builder.Append("    <td height=\"22\" align=\"center\" style=\" color:#001d59\">\n");
                    builder.Append("    \n");
                    builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                    builder.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>" + str + "</font><br>\n");
                    builder.Append("      <font color='#666666'>" + str2 + "</font></font>\n");
                    builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='10%'><img src='" + this.strClubLogo + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                    builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='10%' align='center'><img src='Images/NoNick.gif' border='0' width='46' height='46'>\n");
                    builder.Append("    </td>\n");
                    builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font color='#888888' style='line-height:140%'><span id='ClubNameA'>等待中……</span><br>\n");
                    builder.Append("      <font color='#888888'><span id='NickNameA'></span></font></font>\n");
                    builder.Append("    </td>\t\t\t\t\t\t\t\n");
                    builder.Append("    </tr>\t\t\t\t\n");
                    builder.Append("    </table>\n");
                    builder.Append("    \n");
                    builder.Append("    </td>\n");
                    builder.Append("  </tr>\n");
                    builder.Append("  <tr>\n");
                    TimeSpan span = (TimeSpan) (DateTime.Now - time);
                    int num7 = Convert.ToInt32(span.TotalSeconds);
                    builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\" id=\"WaitSay\">等待对手中……已等待<span id='WaitAdd'></span>秒</td>\n");
                    builder.Append("  </tr>\n");
                    builder.Append("  <tr>\n");
                    builder.Append("    <td height=\"180\" align=\"center\" id=\"tbText\">");
                    builder.Append("</td>\n");
                    builder.Append("  </tr>\n");
                    int onlyOneCountByStatus = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0, 0);
                    int num9 = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0x63, 0);
                    builder.Append("<tr><td align='center' id='RegCount'>" + string.Concat(new object[] { "球队数/等待中：", num9, "/", onlyOneCountByStatus }) + "</td></tr>");
                    builder.Append("<tr><td align=\"center\" id='btnImge'>");
                    if (time.AddMinutes(3.0) < DateTime.Now)
                    {
                        builder.Append("<img style='cursor:pointer' onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT\";' src=\"Images/button_MacthOut.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" />\n");
                    }
                    else
                    {
                        builder.Append("<img  src=\"Images/button_MacthOuth.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" />");
                    }
                    builder.Append("</td></tr></table>\n");
                    builder.Append("|2;AddSeconds(" + num7 + ",'WaitAdd');CheckFindOther(10000)");
                }
                else
                {
                    builder.Append("<tr>\n");
                    builder.Append("    <td align=\"center\" width=\"50\" bgColor=\"#fcc6a4\">连胜</td>\n");
                    builder.Append("    <td bgColor=\"#fcc6a4\" style=\"padding-left:5px\" align=\"left\">球队</td>\n");
                    builder.Append("    <td align=\"center\" bgColor=\"#fcc6a4\">奖金</td>\n");
                    builder.Append("    <td align=\"center\" bgColor=\"#fcc6a4\">操作</td>\n");
                    builder.Append("</tr>\n");
                    int request = SessionItem.GetRequest("Page", 0);
                    if (request < 1)
                    {
                        request = 1;
                    }
                    DataTable table = BTPOnlyOneCenterReg.GetOnlyOneRegOnePay(request, 10, intUserID);
                    if (table != null)
                    {
                        foreach (DataRow row2 in table.Rows)
                        {
                            string str5;
                            string strNickName = this.RepBadWord(row2["ClubName"].ToString().Trim());
                            int num11 = (byte) row2["Status"];
                            int num12 = (int) row2["TopWealth"];
                            int num13 = (int) row2["Win"];
                            int num14 = (int) row2["UserID"];
                            if (num11 == 0)
                            {
                                str5 = "<a href='SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=ONLYPAYMATCH&UserID=" + num14 + "'>开始比赛</a>";
                            }
                            else
                            {
                                str5 = "比赛中…";
                            }
                            strNickName = AccountItem.GetNickNameInfo(num14, strNickName, "Right");
                            builder.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                            builder.Append("    <td align=\"center\" >" + num13 + "</td>\n");
                            builder.Append("    <td  style=\"padding-left:5px\" align=\"left\">" + strNickName + "</td>\n");
                            builder.Append("    <td align=\"center\" >" + num12 + "</td>\n");
                            builder.Append("    <td align=\"center\" >" + str5 + "</td>\n");
                            builder.Append("</tr>\n");
                            builder.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                        }
                        string strCurrentURL = "OnlyOneCenter.aspx?Type=MATCHREG&";
                        this.sbList.Append("<tr><td height='30' align='right' colspan='5'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
                        this.GetMsgScript(strCurrentURL);
                    }
                    TimeSpan span2 = (TimeSpan) (DateTime.Now - time);
                    int num15 = Convert.ToInt32(span2.TotalSeconds);
                    builder.Append("<tr><td height='10' align='center' colspan='5'></td></tr>");
                    builder.Append("<tr><td height='25' align='center' style=\"color:#009933\" colspan='5'>请选择比赛对手…已等待<span align='center' id='WaitAdd'>" + num15 + "</span>秒</td></tr>");
                    builder.Append("<tr><td align=\"center\" id='btnImge' colspan='5'>");
                    if (time.AddMinutes(3.0) < DateTime.Now)
                    {
                        builder.Append("<img style='cursor:pointer' onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT\";' src=\"Images/button_MacthOut.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" />\n");
                    }
                    else
                    {
                        builder.Append("<img  src=\"Images/button_MacthOuth.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" />");
                    }
                    builder.Append("</td></tr></table>\n|2;AddSeconds(" + num15 + ",'WaitAdd');CheckFindOther(10000)");
                }
            }
            else
            {
                builder.Append(this.SetMatchWait(onlyOneMatchRow, byeStatus));
            }
            return builder.ToString().Trim();
        }

        private void GetOnlyMatchTop()
        {
            DataTable table;
            string str;
            int request = SessionItem.GetRequest("Status", 0);
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (request < 1)
            {
                request = 1;
            }
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            if (this.strType == "TOPTODAY")
            {
                table = BTPOnlyOneCenterReg.GetOnlyOneTodayTop(request, this.intPage, this.intPerPage);
                this.sbList.Append("  <tr>\n");
                this.sbList.Append("    <td align='center' width='30'  bgcolor=\"#FCC6A4\">名次</td>\n");
                this.sbList.Append("    <td align='left' style='padding-left:3px' bgcolor=\"#FCC6A4\">球队名</td>\n");
                this.sbList.Append("    <td align='left' style='padding-left:3px' bgcolor=\"#FCC6A4\">经理名</td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPTODAY&Status=1&Page=1\">总积分</a></td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPTODAY&Status=2&Page=1\">总奖金</a></td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPTODAY&Status=3&Page=1\">连胜次数</a></td>\n");
                this.sbList.Append("  </tr>\n");
                str = "OnlyOneCenter.aspx?Type=TOPTODAY&Status=" + request + "&";
            }
            else
            {
                table = BTPOnlyOneCenterReg.GetOnlyOneTop(request, this.intPage, this.intPerPage);
                this.sbList.Append("  <tr>\n");
                this.sbList.Append("    <td align='center' width='30'  bgcolor=\"#FCC6A4\">名次</td>\n");
                this.sbList.Append("    <td align='left' style='padding-left:3px' bgcolor=\"#FCC6A4\">球队名</td>\n");
                this.sbList.Append("    <td align='left' style='padding-left:3px' bgcolor=\"#FCC6A4\">经理名</td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPHISTORY&Status=1&Page=1\">总积分</a></td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPHISTORY&Status=2&Page=1\">总奖金</a></td>\n");
                this.sbList.Append("    <td align=\"center\" bgcolor=\"#FCC6A4\"><a href=\"OnlyOneCenter.aspx?Type=TOPHISTORY&Status=3&Page=1\">连胜次数</a></td>\n");
                this.sbList.Append("  </tr>\n");
                str = "OnlyOneCenter.aspx?Type=TOPHISTORY&Status=" + request + "&";
            }
            if (table != null)
            {
                int num2 = 1 + (this.intPerPage * (this.intPage - 1));
                foreach (DataRow row in table.Rows)
                {
                    int intUserID = (int) row["UserID"];
                    string strClubName = row["ClubName"].ToString().Trim();
                    string strNickName = row["NickName"].ToString().Trim();
                    string shortName = row["ShortName"].ToString().Trim();
                    long num4 = Convert.ToInt64(row["WinTop"]);
                    long num5 = Convert.ToInt64(row["PointTop"]);
                    long num6 = Convert.ToInt64(row["WealthTop"]);
                    bool blnSex = (bool) row["Sex"];
                    this.sbList.Append("  <tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                    this.sbList.Append("    <td height='25' align='center' >" + num2 + "</td>\n");
                    this.sbList.Append("    <td align='left' style='padding-left:3px' >" + ClubItem.GetClubNameInfo(intUserID, strClubName, shortName, "Right", 0x12) + "</td>\n");
                    this.sbList.Append("    <td align='left' style='padding-left:3px' >" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" >" + num5 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" >" + num6 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" >" + num4 + "</td>\n");
                    this.sbList.Append("  </tr>\n");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>");
                    num2++;
                }
                this.GetMsgScript(str);
                this.sbList.Append("<tr><td height='30' align='right' colspan='6'>" + this.GetMsgViewPage(str) + "</td></tr>");
            }
        }

        [AjaxMethod]
        public int GetStatus()
        {
            DataRow onlyOneMatchRow = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(SessionItem.CheckLogin(1));
            if (onlyOneMatchRow == null)
            {
                return 0;
            }
            Convert.ToInt32(onlyOneMatchRow["Status"]);
            DateTime time = (DateTime) onlyOneMatchRow["StatusTime"];
            int num = (byte) onlyOneMatchRow["Status"];
            if ((num == 1) && (time.AddSeconds(100.0) < DateTime.Now))
            {
                num = 2;
            }
            return num;
        }

        private void InitializeComponent()
        {
            switch (this.strType)
            {
                case "MATCHREG":
                    this.sbPageIntro.Append("<ul><li class='qian1'>胜者为王</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHMY\"' href='OnlyOneCenter.aspx?Type=MATCHMY'>我的战绩</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPTODAY\"' href='OnlyOneCenter.aspx?Type=TOPTODAY&Page=1'>今日排名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPHISTORY\"' href='OnlyOneCenter.aspx?Type=TOPHISTORY&Page=1'>历史排名</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    Utility.RegisterTypeForAjax(typeof(OnlyOneCenter));
                    this.SetOnlyOneMatch();
                    break;

                case "MATCHMY":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHREG\"' href='OnlyOneCenter.aspx?Type=MATCHREG'>胜者为王</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>我的战绩</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPTODAY\"' href='OnlyOneCenter.aspx?Type=TOPTODAY&Page=1'>今日排名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPHISTORY\"' href='OnlyOneCenter.aspx?Type=TOPHISTORY&Page=1'>历史排名</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblMatchMy.Visible = true;
                    this.OnlyMatchMy();
                    break;

                case "MATCHTODAY":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHREG\"' href='OnlyOneCenter.aspx?Type=MATCHREG'>胜者为王</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHMY\"' href='OnlyOneCenter.aspx?Type=MATCHMY'>我的战绩</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPTODAY\"' href='OnlyOneCenter.aspx?Type=TOPTODAY&Page=1'>今日排名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPHISTORY\"' href='OnlyOneCenter.aspx?Type=TOPHISTORY&Page=1'>历史排名</a></li></ul>");
                    this.sbPageIntro.Append("<img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/OnlyOneMatch/OnlyOneMatch_5.GIF' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblMatchMy.Visible = true;
                    this.OnlyMatchDay();
                    break;

                case "TOPTODAY":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHREG\"' href='OnlyOneCenter.aspx?Type=MATCHREG'>胜者为王</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHMY\"' href='OnlyOneCenter.aspx?Type=MATCHMY'>我的战绩</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>今日排名</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPHISTORY\"' href='OnlyOneCenter.aspx?Type=TOPHISTORY&Page=1'>历史排名</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblTop.Visible = true;
                    this.sbPageIntro.Append("<span style='margin:20px'>胜者为王当天排行</span>");
                    this.GetOnlyMatchTop();
                    break;

                case "TOPHISTORY":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHREG\"' href='OnlyOneCenter.aspx?Type=MATCHREG'>胜者为王</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHMY\"' href='OnlyOneCenter.aspx?Type=MATCHMY'>我的战绩</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPTODAY\"' href='OnlyOneCenter.aspx?Type=TOPTODAY&Page=1'>今日排名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>历史排名</li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblTop.Visible = true;
                    this.sbPageIntro.Append("<span style='margin:20px'>胜者为王历史排行</span>");
                    this.GetOnlyMatchTop();
                    break;

                default:
                    this.sbPageIntro.Append("<ul><li class='qian1'>胜者为王</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=MATCHMY\"' href='OnlyOneCenter.aspx?Type=MATCHMY'>我的战绩</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPTODAY\"' href='OnlyOneCenter.aspx?Type=TOPTODAY&Page=1'>今日排名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlyOneMatch.aspx?Type=TOPHISTORY\"' href='OnlyOneCenter.aspx?Type=TOPHISTORY&Page=1'>历史排名</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<img style='cursor:hand;'onclick='RefreshStatus();'align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'>");
                    break;
            }
            base.Load += new EventHandler(this.Page_Load);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.strType = SessionItem.GetRequest("Type", 1);
                this.tblMatchReg.Visible = false;
                this.tblMatchRun.Visible = false;
                this.tblMatchMy.Visible = false;
                this.tblTop.Visible = false;
                this.intPerPage = 13;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void OnlyMatchDay()
        {
            int request = SessionItem.GetRequest("Status", 0);
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 12;
            int intClubID = SessionItem.GetRequest("ClubID", 0);
            DataTable table = BTPOnlyOneCenterReg.GetOnlyOneMatchMy(intClubID, 1, 13, 0);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num2 = (int) row["FMatchID"];
                    int num3 = (int) row["ClubIDA"];
                    int num4 = (int) row["ClubIDB"];
                    int num5 = (int) row["WealthA"];
                    int num13 = (int) row["WealthB"];
                    int num6 = (int) row["ClubAPoint"];
                    int num14 = (int) row["ClubBPoint"];
                    byte num15 = (byte) row["Category"];
                    int num7 = (byte) row["Type"];
                    if (num7 == 3)
                    {
                        this.strType = "<font color='green'>街球<font>";
                    }
                    else
                    {
                        this.strType = "<font color='red'>职业</font>";
                    }
                    byte num16 = (byte) row["Status"];
                    row["Intro"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str = StringItem.FormatDate(datIn, "MM-dd hh:mm");
                    string strNickName = row["ClubInfoA"].ToString().Trim();
                    string str3 = row["ClubInfoB"].ToString().Trim();
                    row["ChsCategory"].ToString().Trim();
                    string[] strArray = strNickName.Split(new char[] { '|' });
                    int intUserID = Convert.ToInt32(strArray[0]);
                    strNickName = strArray[1].Trim();
                    string[] strArray2 = str3.Split(new char[] { '|' });
                    int num9 = Convert.ToInt32(strArray2[0]);
                    str3 = strArray2[1].Trim();
                    int num10 = (int) row["ScoreA"];
                    int num11 = (int) row["ScoreB"];
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 12);
                    str3 = AccountItem.GetNickNameInfo(num9, str3, "Right", 12);
                    string str4 = string.Concat(new object[] { 
                        "<a href='", Config.GetDomain(), "VRep.aspx?Type=1&Tag=", num2, "&A=", num3, "&B=", num4, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=1&Tag=", num2, "&A=", num3, 
                        "&B=", num4, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                     });
                    if (num3 != this.intClubID5)
                    {
                        if (num10 > num11)
                        {
                            num6 = 0;
                            num5 = 0;
                        }
                        string str5 = strNickName;
                        strNickName = str3;
                        str3 = str5;
                        int num12 = num10;
                        num10 = num11;
                        num11 = num12;
                    }
                    else if (num11 > num10)
                    {
                        num6 = 0;
                        num5 = 0;
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                    this.sbList.Append("    <td align=\"right\">" + strNickName + "</td>\n");
                    this.sbList.Append("    <td align=\"right\" width=\"20\">" + num10 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" width=\"5\">:</td>\n");
                    this.sbList.Append("    <td align=\"left\" width=\"20\">" + num11 + "</td>\n");
                    this.sbList.Append("    <td align=\"left\">" + str3 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + num6 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + num5 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str4 + "</td>\n");
                    this.sbList.Append("</tr>\n");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
                }
            }
            else
            {
                this.sbList.Append("<tr><td height='24' align='center' colspan='7'>暂无记录</td></tr>");
            }
        }

        private void OnlyMatchMy()
        {
            this.sbPageIntro.Append("<span style=\"margin-left:30px\">历史积分 " + ((long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["OnlyPoint"]) + "</span>");
            int request = SessionItem.GetRequest("Status", 0);
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 13;
            DataTable table = BTPOnlyOneCenterReg.GetOnlyOneMatchMy(this.intClubID5, this.intPage, this.intPerPage, request);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str;
                    int num2 = (int) row["FMatchID"];
                    int num3 = (int) row["ClubIDA"];
                    int num4 = (int) row["ClubIDB"];
                    int num5 = (int) row["WealthA"];
                    int num1 = (int) row["WealthB"];
                    int num6 = (int) row["ClubAPoint"];
                    int num13 = (int) row["ClubBPoint"];
                    byte num14 = (byte) row["Category"];
                    int num7 = (byte) row["Type"];
                    if (num7 == 3)
                    {
                        this.strType = "<font color='green'>街球<font>";
                    }
                    else
                    {
                        this.strType = "<font color='red'>职业</font>";
                    }
                    byte num15 = (byte) row["Status"];
                    row["Intro"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str2 = StringItem.FormatDate(datIn, "yy-MM-dd");
                    string strNickName = row["ClubInfoA"].ToString().Trim();
                    string str4 = row["ClubInfoB"].ToString().Trim();
                    row["ChsCategory"].ToString().Trim();
                    string[] strArray = strNickName.Split(new char[] { '|' });
                    int intUserID = Convert.ToInt32(strArray[0]);
                    strNickName = strArray[1].Trim();
                    string[] strArray2 = str4.Split(new char[] { '|' });
                    int num9 = Convert.ToInt32(strArray2[0]);
                    str4 = strArray2[1].Trim();
                    int num10 = (int) row["ScoreA"];
                    int num11 = (int) row["ScoreB"];
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 12);
                    str4 = AccountItem.GetNickNameInfo(num9, str4, "Right", 12);
                    if ((((num10 == 20) || (num10 == 2)) && (num11 == 0)) || (((num11 == 20) || (num11 == 2)) && (num10 == 0)))
                    {
                        str = "— —";
                    }
                    else
                    {
                        str = string.Concat(new object[] { 
                            "<a href='", Config.GetDomain(), "VRep.aspx?Type=1&Tag=", num2, "&A=", num3, "&B=", num4, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=1&Tag=", num2, "&A=", num3, 
                            "&B=", num4, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                         });
                    }
                    if (num3 != this.intClubID5)
                    {
                        if (num10 > num11)
                        {
                            num6 = 0;
                            num5 = 0;
                        }
                        string str5 = strNickName;
                        strNickName = str4;
                        str4 = str5;
                        int num12 = num10;
                        num10 = num11;
                        num11 = num12;
                    }
                    else if (num11 > num10)
                    {
                        num6 = 0;
                        num5 = 0;
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                    this.sbList.Append("    <td align=\"right\">" + strNickName + "</td>\n");
                    this.sbList.Append("    <td align=\"right\" width=\"20\">" + num10 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" width=\"5\">:</td>\n");
                    this.sbList.Append("    <td align=\"left\" width=\"20\">" + num11 + "</td>\n");
                    this.sbList.Append("    <td align=\"left\">" + str4 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + num6 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + num5 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str2 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str + "</td>\n");
                    this.sbList.Append("</tr>\n");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
                }
                string strCurrentURL = "OnlyOneCenter.aspx?Type=MATCHMY&";
                this.GetMsgScript(strCurrentURL);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private string RepBadWord(string strBadWord)
        {
            Encoding aSCII = Encoding.ASCII;
            string str = strBadWord;
            byte[] bytes = new byte[1];
            for (byte i = 0; i < 0x20; i = (byte) (i + 1))
            {
                bytes[0] = i;
                str = str.Replace(aSCII.GetString(bytes), "*");
            }
            return str;
        }

        private string SetMatchWait(DataRow dr, byte byeStatus)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table cellSpacing=\"0\" cellPadding=\"4\" width=\"552\" border=\"0\" >");
            DateTime time = (DateTime) dr["StatusTime"];
            int num = (int) dr["TopWealth"];
            int num2 = (int) dr["Win"];
            int num3 = (int) dr["Box"];
            int num4 = (int) dr["Reputation"];
            int num5 = (int) dr["Point"];
            int num6 = (int) dr["Wealth"];
            bool flag = (bool) dr["OnlyPay"];
            int num7 = (int) dr["BackWealth"];
            int intUserID = SessionItem.CheckLogin(1);
            string str = "";
            int num9 = 0;
            dr = BTPAccountManager.GetAccountRowByUserID(intUserID);
            if (dr != null)
            {
                string str2;
                string str3;
                string str4;
                string str5;
                string str6;
                string str7;
                int num10;
                int num11;
                int num12;
                int num13;
                object obj2;
                int intClubID = (int) dr["ClubID5"];
                long num1 = (long) dr["OnlyPoint"];
                string str8 = dr["ClubLogo"].ToString().Trim();
                string strBadWord = dr["ClubName"].ToString().Trim();
                string str10 = dr["NickName"].ToString().Trim();
                strBadWord = this.RepBadWord(strBadWord);
                str10 = this.RepBadWord(str10);
                dr = BTPFriMatchManager.GetFriMatchRowByUserID(intClubID, 8, 2);
                bool flag2 = false;
                if (dr == null)
                {
                    int num15 = BTPOnlyOneCenterReg.OnlyErrorOutByUserID(intUserID);
                    BTPErrorManager.AddError("onlyone", string.Format("胜者意外退出，获取不到比赛信息，俱乐部ID{0}", intClubID));
                    return ("error|1;GoSay(" + num15 + ");");
                }
                int num16 = (int) dr["ClubIDA"];
                int num44 = (int) dr["ClubIDB"];
                int num17 = (int) dr["ScoreA"];
                int num18 = (int) dr["ScoreB"];
                int num19 = (int) dr["ClubIDA"];
                int num20 = (int) dr["ClubIDB"];
                int num21 = (int) dr["ScoreA"];
                int num22 = (int) dr["ScoreB"];
                int topWealthByClubID = (int) dr["WealthA"];
                int num24 = (int) dr["ClubAPoint"];
                int num25 = (int) dr["FMatchID"];
                int num26 = (int) dr["ReputationA"];
                int num27 = (int) dr["ReputationB"];
                int num28 = 0;
                if (intClubID == num19)
                {
                    num28 = num26;
                }
                else
                {
                    num28 = num27;
                }
                if ((((num21 == 20) || (num21 == 2)) && (num22 == 0)) || (((num22 == 20) || (num22 == 2)) && (num21 == 0)))
                {
                    flag2 = true;
                }
                string url = Config.GetDomain() + dr["RepURL"].ToString().Trim();
                string str12 = dr["ClubIDA"].ToString().Trim();
                string str13 = dr["ClubIDB"].ToString().Trim();
                if (intClubID == num19)
                {
                    num12 = num20;
                    str2 = str8;
                    num10 = intUserID;
                    str4 = AccountItem.GetNickNameInfo(num10, strBadWord, "Right", 0x10);
                    str6 = str10;
                    dr = BTPAccountManager.GetAccountRowByClubID5(num20);
                    if (dr == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return "";
                    }
                    str3 = dr["ClubLogo"].ToString().Trim();
                    num11 = (int) dr["UserID"];
                    str7 = dr["NickName"].ToString().Trim();
                    str7 = this.RepBadWord(str7);
                    str5 = AccountItem.GetNickNameInfo(num11, this.RepBadWord(dr["ClubName"].ToString().Trim()), "Right", 0x10);
                }
                else
                {
                    num12 = num19;
                    string str14 = str12;
                    str12 = str13;
                    str13 = str14;
                    str2 = str8;
                    num10 = intUserID;
                    str4 = AccountItem.GetNickNameInfo(num10, strBadWord, "Right", 0x10);
                    str6 = str10;
                    dr = BTPAccountManager.GetAccountRowByClubID5(num19);
                    if (dr != null)
                    {
                        int num29 = num21;
                        num21 = num22;
                        num22 = num29;
                        str3 = dr["ClubLogo"].ToString().Trim();
                        num11 = (int) dr["UserID"];
                        str7 = this.RepBadWord(dr["NickName"].ToString().Trim());
                        str5 = AccountItem.GetNickNameInfo(num11, this.RepBadWord(dr["ClubName"].ToString().Trim()), "Right", 0x10);
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return "";
                    }
                }
                if ((byeStatus == 5) || (byeStatus == 3))
                {
                    num13 = num2;
                }
                else
                {
                    num13 = num2 + 1;
                }
                string str15 = num24 + "/" + num5;
                int num30 = num2;
                if (((byeStatus < 3) || (byeStatus == 4)) || (byeStatus == 6))
                {
                    num30++;
                }
                int num31 = num5;
                if (byeStatus == 3)
                {
                    num31 -= num24;
                }
                if (num30 < 4)
                {
                    str15 = "<span id='MatchPoint'>1</span>/<span id='AllPoint'>" + num31 + "</span>";
                }
                else if (num30 < 7)
                {
                    str15 = "<span id='MatchPoint'>2</span>/<span id='AllPoint'>" + num31 + "</span>";
                }
                else
                {
                    str15 = "<span id='MatchPoint'>4</span>/<span id='AllPoint'>" + num31 + "</span>";
                }
                topWealthByClubID = BTPOnlyOneCenterReg.GetTopWealthByClubID(intClubID);
                int num32 = 0;
                num32 = topWealthByClubID * 2;
                int num33 = (num6 + num) - topWealthByClubID;
                string str16 = string.Concat(new object[] { "<span id='MatchWealth'>", num32, "</span>/<span id='AllWealth'>", num33, "</span>" });
                if (byeStatus == 3)
                {
                    str16 = string.Concat(new object[] { "<span id='MatchWealth'>", num32, "</span>/<span id='AllWealth'>", num33 - topWealthByClubID, "</span>" });
                }
                if (byeStatus == 5)
                {
                    str16 = string.Concat(new object[] { "<span id='MatchWealth'>", num32, "</span>/<span id='AllWealth'>", num6 + num, "</span>" });
                }
                if (flag)
                {
                    str15 = "";
                }
                else
                {
                    str15 = "<a style=\"cursor:hand;\" title=\"应得积分/总积分\">积分：" + str15 + "</a>";
                }
                builder.Append("  <tr>\n");
                builder.Append(string.Concat(new object[] { "    <td height=\"20\" bgcolor=\"#fcc6a4\"><span style=\"padding-right:12px\" >第 <span id='MatchTurn'>", num13, "</span> 场比赛</span><span style=\"padding-left:230px\">", str15, "<span style=\"width:30px\"></span><a style=\"cursor:hand;\" title=\"本场奖金/当前奖金\">奖金：", str16, "</a></span></td>\n" }));
                builder.Append("  </tr>\n");
                builder.Append("    <td height=\"22\" align=\"center\" style=\" color:#001d59\">\n");
                builder.Append("    \n");
                builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                builder.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                builder.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>", str4, "</font><a href='OnlyOneCenter.aspx?Type=MATCHTODAY&ClubID=", intClubID, "'><img align='absmiddle' style='margin-left:5px' alt='查看此经理战绩' src='", SessionItem.GetImageURL(), "zhanji.gif' border='0' width='12' height='16'></a><br>\n" }));
                builder.Append("      <font color='#666666'>" + str6 + "</font></font>\n");
                builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                builder.Append("    <td width='10%'><img src='" + str2 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                if ((byeStatus == 5) || (byeStatus == 6))
                {
                    string str17;
                    string str18;
                    int num34 = num21 / 100;
                    if (num34 == 0)
                    {
                        str17 = "99";
                    }
                    else
                    {
                        str17 = num34.ToString();
                    }
                    int num35 = num22 / 100;
                    if (num35 == 0)
                    {
                        str18 = "99";
                    }
                    else
                    {
                        str18 = num35.ToString();
                    }
                    builder.Append(string.Concat(new object[] { "    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/", str17, ".gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/", (num21 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/", num21 % 10, ".gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/", str18, ".gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/", (num22 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/", num22 % 10, ".gif' border='0' width='19' height='28'>\n" }));
                }
                else
                {
                    builder.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                }
                builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                builder.Append("    <td width='10%' align='center'><img src='" + str3 + "' border='0' width='46' height='46'>\n");
                builder.Append("    </td>\n");
                builder.Append(string.Concat(new object[] { "    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'>", str5, "<a href='OnlyOneCenter.aspx?Type=MATCHTODAY&ClubID=", num12, "'><img style='margin-left:5px' align='absmiddle' alt='查看此经理战绩' src='", SessionItem.GetImageURL(), "zhanji.gif' border='0' width='12' height='16'></a><br>\n" }));
                builder.Append("      <font color='#666666'>" + str7 + "</font></font>\n");
                builder.Append("    </td>\t\t\t\t\t\t\t\n");
                builder.Append("    </tr>\t\t\t\t\n");
                builder.Append("    </table>\n");
                builder.Append("    \n");
                builder.Append("    </td>\n");
                builder.Append("  </tr>\n");
                if (byeStatus == 1)
                {
                    TimeSpan span = (TimeSpan) (time.AddSeconds(100.0) - DateTime.Now);
                    if (span.TotalSeconds > 0.0)
                    {
                        builder.Append("   <tr id='trMatchWait'> <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">赛前准备中……您还有<span id='ShowTime' style=\"color:red\"></span>秒时间进行战术安排</td></tr>\n");
                        builder.Append("<tr  id='trMatchIn' style='display:none'> <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">球员入场中……比赛将在<span id='ShowTime2' style=\"color:red\"></span>秒后开始</td>");
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "SetRunSeconds(", Convert.ToInt32(span.TotalSeconds), ",'ShowTime',1);" });
                        num9++;
                    }
                    else
                    {
                        int num36 = (((Convert.ToInt32(-span.TotalSeconds) / 30) + 1) * 30) + 60;
                        span = (TimeSpan) (time.AddSeconds((double) num36) - DateTime.Now);
                        builder.Append(" <tr id='trMatchWait'><td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">赛前准备中……您还有<span id='ShowTime' style=\"color:red\"></span>秒时间进行战术安排</td></tr>\n");
                        builder.Append("<tr id='trMatchIn' style='display:none'> <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">球员入场中……比赛将在<span id='ShowTime2' style=\"color:red\"></span>秒后开始</td>");
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "SetRunSeconds(", Convert.ToInt32(span.TotalSeconds), ",'ShowTime',1);" });
                        num9++;
                    }
                }
                else if (byeStatus == 2)
                {
                    TimeSpan span2 = (TimeSpan) (time.AddSeconds(130.0) - DateTime.Now);
                    if (span2.TotalSeconds > 0.0)
                    {
                        builder.Append("<tr> <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">球员入场中……比赛将在<span id='ShowTime2' style=\"color:red\"></span>秒后开始</td>");
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "SetRunSeconds(", Convert.ToInt32(span2.TotalSeconds), ",'ShowTime2',2);" });
                        num9++;
                    }
                    else
                    {
                        int num37 = (((Convert.ToInt32(-span2.TotalSeconds) / 10) + 1) * 10) + 30;
                        span2 = (TimeSpan) (time.AddSeconds((double) num37) - DateTime.Now);
                        builder.Append("<td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">球员入场中……比赛将在<span id='ShowTime2' style=\"color:red\"></span>秒后开始</td>");
                        obj2 = str;
                        str = string.Concat(new object[] { obj2, "SetRunSeconds(", Convert.ToInt32(span2.TotalSeconds), ",'ShowTime2',2);" });
                        num9++;
                    }
                }
                else if (((byeStatus == 3) || (byeStatus == 4)) && !flag2)
                {
                    builder.Append(" <tr  id='TimeTitle'>   <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\"><span id='MatchTime'></span></td></tr>\n");
                    builder.Append("<tr style='display:none' id='MatchEndTitle'><td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">比赛已结束 " + string.Concat(new object[] { 
                        "<a href='", Config.GetDomain(), "VRep.aspx?Type=1&Tag=", num25, "&A=", num19, "&B=", num20, "' target='_blank'>本场战报</a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=1&Tag=", num25, "&A=", num19, "&B=", num20, 
                        "' target='_blank'>本场统计</a>"
                     }) + "</td>\n");
                }
                else if (((byeStatus == 5) || (byeStatus == 6)) || flag2)
                {
                    string str19;
                    if (!flag2)
                    {
                        str19 = string.Concat(new object[] { 
                            "<a href='", Config.GetDomain(), "VRep.aspx?Type=1&Tag=", num25, "&A=", num19, "&B=", num20, "' target='_blank'>本场战报</a>  <a href='", Config.GetDomain(), "VStas.aspx?Type=1&Tag=", num25, "&A=", num19, "&B=", num20, 
                            "' target='_blank'>本场统计</a>"
                         });
                    }
                    else
                    {
                        str19 = "";
                    }
                    builder.Append("<tr  id='MatchEndTitle'><td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\">比赛已结束 " + str19 + "</td>\n");
                }
                builder.Append("  </tr>\n");
                builder.Append("  <tr>\n");
                builder.Append("    <td height=\"180\" align=\"center\" id=\"tbText\">");
                int count = 0;
                if (byeStatus > 2)
                {
                    if (!flag2)
                    {
                        XmlDataDocument document = new XmlDataDocument();
                        document.DataSet.ReadXmlSchema(base.Server.MapPath("../MatchXML/RepSchema.xsd"));
                        try
                        {
                            XmlTextReader reader = new XmlTextReader(url);
                            reader.MoveToContent();
                            document.Load(reader);
                            DataSet dataSet = document.DataSet;
                            DataTable table = dataSet.Tables["Quarter"];
                            DataTable dt = dataSet.Tables["Arrange"];
                            count = table.Rows.Count;
                            int intNum = 1;
                            foreach (DataRow row in table.Rows)
                            {
                                string str20 = row["QuarterID"].ToString();
                                string str21 = row["ScoreH"].ToString();
                                string str22 = row["ScoreA"].ToString();
                                if (intClubID != num19)
                                {
                                    string str23 = str21;
                                    str21 = str22;
                                    str22 = str23;
                                }
                                DataRow row2 = XmlHelper.GetRow(dt, "QuarterID=" + str20 + " AND ClubID=" + str12, "");
                                DataRow row3 = XmlHelper.GetRow(dt, "QuarterID=" + str20 + " AND ClubID=" + str13, "");
                                string vOffName = MatchItem.GetVOffName((int) row2["Offense"]);
                                string vDefName = MatchItem.GetVDefName((int) row2["Defense"]);
                                string str26 = MatchItem.GetVOffName((int) row3["Offense"]);
                                string str27 = MatchItem.GetVDefName((int) row3["Defense"]);
                                if (byeStatus > 4)
                                {
                                    builder.Append("<div class=\"zdiv\" id='MatchNode" + intNum + "'>");
                                }
                                else
                                {
                                    builder.Append("<div class=\"zdiv\" id='MatchNode" + intNum + "' style=\"display:none\">");
                                }
                                builder.Append(string.Concat(new object[] { "<span id='NodeScoreA", intNum, "' style=\"display:none\">", str21, "</span>" }));
                                builder.Append(string.Concat(new object[] { "<span id='NodeScoreB", intNum, "' style=\"display:none\">", str22, "</span>" }));
                                builder.Append("<span class=\"zspan1\">" + MatchItem.GetQName(intNum, 5) + "</span>");
                                builder.Append("<span class=\"zspan1\">" + vOffName + "</span>");
                                builder.Append("<span class=\"zspan2\">" + vDefName + "</span>");
                                builder.Append("<span class=\"zspan1\">" + str26 + "</span>");
                                builder.Append("<span class=\"zspan1\">" + str27 + "</span>");
                                builder.Append("</div>");
                                intNum++;
                            }
                            reader.Close();
                            TimeSpan span3 = (TimeSpan) (time.AddMinutes(1.0) - DateTime.Now);
                            int num40 = 60 - Convert.ToInt32(span3.TotalSeconds);
                            if (byeStatus < 5)
                            {
                                if (num40 > 60)
                                {
                                    int num41 = ((num40 - 60) / 30) + 1;
                                    span3 = (TimeSpan) (time.AddSeconds((double) ((num41 * 30) + 60)) - DateTime.Now);
                                    obj2 = str;
                                    str = string.Concat(new object[] { obj2, "SetRunTime(1,", num40, ",", Convert.ToInt32(span3.TotalSeconds), ",", count, ");" });
                                    num9++;
                                }
                                else
                                {
                                    obj2 = str;
                                    str = string.Concat(new object[] { obj2, "SetRunTime(0,", num40 + 1, ",0,", count, ");" });
                                    num9++;
                                }
                            }
                        }
                        catch (WebException exception)
                        {
                            if (!flag2)
                            {
                                int num42 = 1;
                                string str28 = exception.ToString();
                                BTPErrorManager.AddError("onlyone", string.Format("胜者意外退出，获取不到比赛战报，用户ID{0},错误详情{1}", intUserID, str28));
                                return ("error|1;GoSay(" + num42 + ");");
                            }
                        }
                        catch (Exception exception2)
                        {
                            if (!flag2)
                            {
                                int num43 = BTPOnlyOneCenterReg.OnlyErrorOutByUserID(intUserID);
                                string str29 = exception2.ToString();
                                BTPErrorManager.AddError("onlyone", string.Format("胜者意外退出，获取不到比赛战报，用户ID{0},错误详情{1}", intUserID, str29));
                                return ("error|1;GoSay(" + num43 + ");");
                            }
                        }
                    }
                    else
                    {
                        builder.Append("<div class=\"zdiv\" id='MatchNode1'></div>");
                        builder.Append("<div class=\"zdiv\" id='MatchNode2'></div>");
                        if (num16 == intClubID)
                        {
                            if (num17 < num18)
                            {
                                builder.Append("<div class=\"zdiv\" id='MatchNode3'>您的球员人数不足，无法进行比赛！</div>");
                            }
                            else
                            {
                                builder.Append("<div class=\"zdiv\" id='MatchNode3'>对方的球员人数不足，无法进行比赛！</div>");
                            }
                        }
                        else if (num17 > num18)
                        {
                            builder.Append("<div class=\"zdiv\" id='MatchNode3'>您的球员人数不足，无法进行比赛！</div>");
                        }
                        else
                        {
                            builder.Append("<div class=\"zdiv\" id='MatchNode3'>对方的球员人数不足，无法进行比赛！</div>");
                        }
                        builder.Append("<div class=\"zdiv\" id='MatchNode4'></div>");
                        builder.Append("<div class=\"zdiv\" id='MatchNode5'></div>");
                    }
                }
                else
                {
                    builder.Append("&nbsp;");
                }
                builder.Append("</td>\n");
                builder.Append("  </tr>\n");
                if (byeStatus == 5)
                {
                    string str30 = "";
                    if (!flag)
                    {
                        str30 = "积分<font color=\"green\" style=\"font-size:16px\"><strong>+" + num24 + "</strong></font>";
                    }
                    builder.Append("<tr><td align=\"center\">比赛胜利！获得" + str30);
                    if (topWealthByClubID > 0)
                    {
                        builder.Append("奖金<font color=\"green\" style=\"font-size:16px\"><strong>+" + topWealthByClubID + "</strong></font>游戏币");
                    }
                    if (num28 > 0)
                    {
                        builder.Append("，本场获威望<font color=\"green\" style=\"font-size:16px\"><strong>+" + num28 + "</strong></font>");
                    }
                    if (num4 > 0)
                    {
                        builder.Append("共获威望<font color=\"green\" style=\"font-size:16px\"><strong>+" + num4 + "</strong></font>");
                    }
                    if (num3 > 0)
                    {
                        builder.Append("共获豆腐<font color=\"green\" style=\"font-size:16px\"><strong>" + num3 + "</strong></font>块");
                    }
                    builder.Append("</td></tr>");
                    builder.Append("  <tr>\n");
                    builder.Append("<tr><td width=10></td></tr>");
                }
                else if (byeStatus == 3)
                {
                    string str31 = "";
                    if (!flag)
                    {
                        str31 = "积分<font color=\"green\" style=\"font-size:16px\"><strong>+" + num24 + "</strong></font>";
                    }
                    builder.Append("<tr id='WinPoint' style='display:none'><td align=\"center\">比赛胜利！获得" + str31);
                    if (topWealthByClubID > 0)
                    {
                        builder.Append("，奖金<font color=\"green\" style=\"font-size:16px\"><strong>+" + topWealthByClubID + "</strong></font>游戏币");
                    }
                    if (num28 > 0)
                    {
                        builder.Append("，本场获威望<font color=\"green\" style=\"font-size:16px\"><strong>+" + num28 + "</strong></font>");
                    }
                    if (num4 > 0)
                    {
                        builder.Append("共获威望<font color=\"green\" style=\"font-size:16px\"><strong>" + num4 + "</strong></font>");
                    }
                    if (num3 > 0)
                    {
                        builder.Append("，共获豆腐<font color=\"green\" style=\"font-size:16px\"><strong>" + num3 + "</strong></font>块");
                    }
                    builder.Append("</td></tr>");
                    builder.Append("  <tr>\n");
                    builder.Append("<tr id='WinPoint2' style='display:none'><td width=10></td></tr>");
                }
                else if ((byeStatus == 4) && flag)
                {
                    builder.Append("<tr id='WinPoint' style='display:none'><td align=\"center\"> 比赛失利！损失");
                    builder.Append("，奖金<font color=\"red\" style=\"font-size:16px\"><strong>-" + num7 + "</strong></font>游戏币");
                    builder.Append("</td></tr>");
                    builder.Append("  <tr>\n");
                    builder.Append("<tr id='WinPoint2' style='display:none'><td width=10></td></tr>");
                }
                else if ((byeStatus == 6) && flag)
                {
                    builder.Append("<tr id='WinPoint'><td align=\"center\"> 比赛失利！损失");
                    builder.Append("，奖金<font color=\"red\" style=\"font-size:16px\"><strong>-" + num7 + "</strong></font>游戏币");
                    builder.Append("</td></tr>");
                    builder.Append("  <tr>\n");
                    builder.Append("<tr id='WinPoint2'><td width=10></td></tr>");
                }
                if (byeStatus == 1)
                {
                    builder.Append("<tr><td align=\"center\"><img id='btnSetArrange' style='cursor:pointer' onclick='javascript:window.location=\"VArrange.aspx?Type=0&Jump=1\";' src=\"Images/button_SetArrange.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /> <img src=\"Images/button_MatchGoNoh.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> <img src=\"Images/button_MacthOuth.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> </td>\n");
                }
                else if (((byeStatus == 2) || (byeStatus == 3)) || (byeStatus == 4))
                {
                    builder.Append("<tr><td align=\"center\"><img src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /> <img id='btnGoNo' src=\"Images/button_MatchGoNoh.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> <img id='btnOut' src=\"Images/button_MacthOuth.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> </td>\n");
                }
                if (byeStatus == 5)
                {
                    if (num2 < 9)
                    {
                        builder.Append("<tr><td align=\"center\"><img src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /> <img style='cursor:pointer'onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHGONO\";' src=\"Images/button_MatchGoNo.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> <img style='cursor:pointer'onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT\";' src=\"Images/button_MacthOut.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /></td>\n");
                    }
                    else
                    {
                        builder.Append("<tr><td align=\"center\"><img src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /> <img src=\"Images/button_MatchGoNoh.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> <img style='cursor:pointer'onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT\";' src=\"Images/button_MacthOut.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /></td>\n");
                    }
                }
                if (byeStatus == 6)
                {
                    builder.Append("<tr><td align=\"center\"><img src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /> <img src=\"Images/button_MatchGoNoh.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> <img style='cursor:pointer'onclick='javascript:window.location=\"SecretaryPage.aspx?Type=ONLYONEMATCH&PageType=MATCHOUT\";' src=\"Images/button_MacthOut.gif\" width=\"60\" height=\"24\"  align=\"absmiddle\" /> </td>\n");
                }
                builder.Append("  </tr>\n");
                builder.Append("  <tr>\n");
                this.sbPageIntro.Append("<span style=\"margin-left:50px\"> 奖金上限：" + num + "</span>\n");
                builder.Append("  </tr>\n");
            }
            builder.Append(string.Concat(new object[] { "</table>|", num9, ";", str }));
            return builder.ToString().Trim();
        }

        private void SetOnlyOneMatch()
        {
            DataRow onlyOneMatchRow = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(this.intUserID);
            if (onlyOneMatchRow == null)
            {
                this.sbPageIntro.Append("<img id='Refresh' style='cursor:hand;'onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'>");
                int onlyOneCountByStatus = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0x63, 0);
                int num2 = BTPOnlyOneCenterReg.GetOnlyOneCountByStatus(0x63, 1);
                this.strRegCount = string.Concat(new object[] { "积分区/奖金区：", onlyOneCountByStatus, "/", num2 });
                this.hidCheck.Value = "-1";
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_Reg.gif";
                this.tblMatchReg.Visible = true;
                onlyOneMatchRow = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.intWealth = (int) onlyOneMatchRow["Wealth"];
                if (this.intWealth < 10)
                {
                }
            }
            else
            {
                this.strOnLoad = "GetOnlyOneCenter();";
                this.sbPageIntro.Append("<img id='Refresh' style='cursor:hand;'onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'>");
                byte num3 = (byte) onlyOneMatchRow["Status"];
                DateTime time = (DateTime) onlyOneMatchRow["StatusTime"];
                if ((num3 == 1) && (time.AddSeconds(100.0) < DateTime.Now))
                {
                    num3 = 2;
                }
                this.hidCheck.Value = num3.ToString();
                if ((num3 == 0) && (time.AddMinutes(5.0) < DateTime.Now))
                {
                    this.hidCheck.Value = "-2";
                }
                if (num3 == 0)
                {
                    this.tblMatchRun.Visible = true;
                }
                else
                {
                    this.tblMatchRun.Visible = true;
                }
            }
        }
    }
}


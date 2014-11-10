namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class Stock : Page
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
            DataTable getStockCompany;
            switch (this.strType)
            {
                case "COMPANY":
                    strCurrentURL = "Stock.aspx?Type=COMPANY&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球队名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">俱乐部市值</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">每股价值</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">已出售</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">排名升降</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">买/卖</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPStockManager.GetGetStockCompanyCount();
                    if (this.intTotal > 0)
                    {
                        getStockCompany = BTPStockManager.GetGetStockCompany(this.intPage, this.intPerPage);
                        if (getStockCompany == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row in getStockCompany.Rows)
                        {
                            string str2 = Convert.ToString(row["ClubName"]);
                            int intStockUserId = Convert.ToInt32(row["UserID"]);
                            int num2 = Convert.ToInt32(row["Price"]);
                            int num3 = num2 / 20;
                            int num4 = Convert.ToInt32(row["YestodaySort"]);
                            int num5 = Convert.ToInt32(row["Sort"]);
                            int num6 = Convert.ToInt32(row["TotalSell"]);
                            int stockTeamDay = BTPStockManager.GetStockTeamDay(this.intUserID, intStockUserId);
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num5 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", intStockUserId, "\" title=\"", str2, "\" target=\"Right\">", str2, "</a></td>" }));
                            this.sbList.Append(" <td align=\"left\">" + num2 + "</td>");
                            this.sbList.Append(" <td align=\"left\">" + num3 + "</td>");
                            this.sbList.Append(" <td align=\"center\">" + num6 + "</td>");
                            if (num5 > num4)
                            {
                                this.sbList.Append(" <td align=\"center\"><font color=\"green\">-" + (num5 - num4) + "</font></td>");
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"center\"><font color=\"red\">+" + (num4 - num5) + "</font></td>");
                            }
                            this.sbList.Append("<td align=\"center\">");
                            if (stockTeamDay == 0)
                            {
                                this.sbList.Append("<a href='SecretaryPage.aspx?Type=BUYSTOCK&StockUserID=" + intStockUserId + "'>买</a>");
                            }
                            else
                            {
                                this.sbList.Append("买");
                            }
                            this.sbList.Append("&nbsp;|&nbsp;");
                            if (stockTeamDay > 3)
                            {
                                this.sbList.Append("<a href='SecretaryPage.aspx?Type=SELLSTOCK&StockUserID=" + intStockUserId + "'>卖</a>");
                            }
                            else
                            {
                                this.sbList.Append("卖");
                            }
                            this.sbList.Append("</td>");
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;

                case "MYSTOCK":
                    strCurrentURL = "XBATop.aspx?Type=PLAYERTOP&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球员姓名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">所属经理</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">综合</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">年龄</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">身高</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">体重</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = 200;
                    if (this.intTotal > 0)
                    {
                        getStockCompany = BTPXBATopManager.GetPlayer5AbilityTop(this.intPage, this.intPerPage);
                        int num8 = 1;
                        if (getStockCompany == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row2 in getStockCompany.Rows)
                        {
                            string str3 = Convert.ToString(row2["Name"]);
                            int num9 = Convert.ToInt32(row2["UserID"]);
                            string str4 = Convert.ToString(row2["NickName"]);
                            int intAbility = Convert.ToInt32(row2["Ability"]);
                            int num11 = Convert.ToInt32(row2["Age"]);
                            int num12 = Convert.ToInt32(row2["Height"]);
                            int num13 = Convert.ToInt32(row2["Weight"]);
                            int num14 = Convert.ToInt32(row2["PlayerID"]);
                            int num15 = ((this.intPage - 1) * this.intPerPage) + num8;
                            num8++;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num15 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=", num14, "\" target=\"Right\">", str3, "</a></span></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\"><a href=\"ShowClub.aspx?Type=5&UserID=", num9, "\" title=\"", str4, "\" target=\"Right\">", str4, "</a></td>" }));
                            this.sbList.Append("<td align=\"center\" >" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num11 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num12 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num13 + "</td></tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;

                case "TRADELOG":
                    strCurrentURL = "XBATop.aspx?Type=USERABILITYTOP&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球队名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">经理名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">所属联盟</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">日排名变化</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">排名积分</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPXBATopManager.GetXBATopByCategoryCount(2);
                    if (this.intTotal > 0)
                    {
                        getStockCompany = BTPXBATopManager.GetXBATopByCategory(2, this.intPage, this.intPerPage);
                        if (getStockCompany == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row3 in getStockCompany.Rows)
                        {
                            string str5 = Convert.ToString(row3["ClubName"]);
                            int num16 = Convert.ToInt32(row3["UserID"]);
                            string str6 = Convert.ToString(row3["NickName"]);
                            string str7 = Convert.ToString(row3["UnionName"]);
                            int num17 = Convert.ToInt32(row3["UnionID"]);
                            int num18 = Convert.ToInt32(row3["Place"]);
                            int num19 = Convert.ToInt32(row3["Value1"]);
                            Convert.ToInt32(row3["Value2"]);
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num18 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num16, "\" title=\"", str5, "\" target=\"Right\">", str5, "</a></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num16, "\" title=\"", str6, "\" target=\"Right\">", str6, "</a></td>" }));
                            if (num17 > 0)
                            {
                                this.sbList.Append(string.Concat(new object[] { " <td align=\"left\"><a href='UnionList.aspx?UnionID=", num17, "&Page=1' target='Right'>", str7, "</a></td>" }));
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"left\">暂无联盟</td>");
                            }
                            this.sbList.Append(" <td align=\"center\">-</td>");
                            this.sbList.Append(" <td align=\"center\">" + num19 + "</td>");
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void SetPageIntro()
        {
            string strType = this.strType;
            if (strType != null)
            {
                if (strType != "COMPANY")
                {
                    if (strType != "MYSTOCK")
                    {
                        if (strType == "LOG")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='XBATop.aspx?Type=USERABILITYTOP'>上市公司</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='Stock.aspx?Type=MYSTOCK&Page=1'>我的股票</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>交易日志</li></ul></td>");
                        }
                        return;
                    }
                }
                else
                {
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>上市公司</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='Stock.aspx?Type=MYSTOCK&Page=1'>我的股票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERVBTOP\" 'href='Stock.aspx?Type=TRADELOG'>交易日志</a></li></ul></td>");
                    return;
                }
                this.sbPageIntro = new StringBuilder();
                this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='Stock.aspx?Type=COMPANY'>上市公司</a></li>");
                this.sbPageIntro.Append("<li class='qian2'>我的股票</li>");
                this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERVBTOP\"'href='Stock.aspx?Type=TRADELOG'>交易日志</a></li></ul></td>");
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class XBATop : Page
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
            DataTable table;
            switch (this.strType)
            {
                case "USERABILITYTOP":
                    strCurrentURL = "XBATop.aspx?Type=USERABILITYTOP&";
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球队名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">经理名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">所属联盟</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">球队综合</td>");
                    this.sbList.Append("</tr>");
                    this.intTotal = BTPXBATopManager.GetXBATopByCategoryCount(3);
                    if (this.intTotal > 0)
                    {
                        table = BTPXBATopManager.GetXBATopByCategory(3, this.intPage, this.intPerPage);
                        if (table == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row in table.Rows)
                        {
                            string str2 = Convert.ToString(row["ClubName"]);
                            int num = Convert.ToInt32(row["UserID"]);
                            string str3 = Convert.ToString(row["NickName"]);
                            string str4 = Convert.ToString(row["UnionName"]);
                            int num2 = Convert.ToInt32(row["UnionID"]);
                            int num3 = Convert.ToInt32(row["Place"]);
                            float num5 = ((float) Convert.ToInt32(row["Value1"])) / 10f;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num3 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num, "\" title=\"", str2, "\" target=\"Right\">", str2, "</a></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num, "\" title=\"", str3, "\" target=\"Right\">", str3, "</a></td>" }));
                            if (num2 > 0)
                            {
                                this.sbList.Append(string.Concat(new object[] { " <td align=\"left\"><a href='UnionList.aspx?UnionID=", num2, "&Page=1' target='Right'>", str4, "</a></td>" }));
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"left\">暂无联盟</td>");
                            }
                            this.sbList.Append(" <td align=\"center\">" + num5 + "</td>");
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;

                case "PLAYERTOP":
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
                        table = BTPXBATopManager.GetPlayer5AbilityTop(this.intPage, this.intPerPage);
                        int num6 = 1;
                        if (table == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row2 in table.Rows)
                        {
                            string str5 = Convert.ToString(row2["Name"]);
                            int num7 = Convert.ToInt32(row2["UserID"]);
                            string str6 = Convert.ToString(row2["NickName"]);
                            int intAbility = Convert.ToInt32(row2["Ability"]);
                            int num9 = Convert.ToInt32(row2["Age"]);
                            int num10 = Convert.ToInt32(row2["Height"]);
                            int num11 = Convert.ToInt32(row2["Weight"]);
                            int num12 = Convert.ToInt32(row2["PlayerID"]);
                            int num13 = ((this.intPage - 1) * this.intPerPage) + num6;
                            num6++;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num13 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=", num12, "\" target=\"Right\">", str5, "</a></span></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\"><a href=\"ShowClub.aspx?Type=5&UserID=", num7, "\" title=\"", str6, "\" target=\"Right\">", str6, "</a></td>" }));
                            this.sbList.Append("<td align=\"center\" >" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num9 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num10 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num11 + "</td></tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;

                case "PLAYER3TOP":
                    strCurrentURL = "XBATop.aspx?Type=PLAYER3TOP&";
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
                        table = BTPXBATopManager.GetPlayer3AbilityTop(this.intPage, this.intPerPage);
                        int num14 = 1;
                        if (table == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row3 in table.Rows)
                        {
                            string str7 = Convert.ToString(row3["Name"]);
                            int num15 = Convert.ToInt32(row3["UserID"]);
                            string str8 = Convert.ToString(row3["NickName"]);
                            int num16 = Convert.ToInt32(row3["Ability"]);
                            int num17 = Convert.ToInt32(row3["Age"]);
                            int num18 = Convert.ToInt32(row3["Height"]);
                            int num19 = Convert.ToInt32(row3["Weight"]);
                            int num20 = Convert.ToInt32(row3["PlayerID"]);
                            int num21 = ((this.intPage - 1) * this.intPerPage) + num14;
                            num14++;
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num21 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=3&Kind=0&Check=0&PlayerID=", num20, "\" target=\"Right\">", str7, "</a></span></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\"><a href=\"ShowClub.aspx?Type=5&UserID=", num15, "\" title=\"", str8, "\" target=\"Right\">", str8, "</a></td>" }));
                            this.sbList.Append("<td align=\"center\" >" + PlayerItem.GetAbilityColor(num16) + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num17 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num18 + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + num19 + "</td></tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                        return;
                    }
                    this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                    return;

                case "USERVBTOP":
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
                        table = BTPXBATopManager.GetXBATopByCategory(2, this.intPage, this.intPerPage);
                        if (table == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row4 in table.Rows)
                        {
                            string str9 = Convert.ToString(row4["ClubName"]);
                            int num22 = Convert.ToInt32(row4["UserID"]);
                            string str10 = Convert.ToString(row4["NickName"]);
                            string str11 = Convert.ToString(row4["UnionName"]);
                            int num23 = Convert.ToInt32(row4["UnionID"]);
                            int num24 = Convert.ToInt32(row4["Place"]);
                            int num25 = Convert.ToInt32(row4["Value1"]);
                            Convert.ToInt32(row4["Value2"]);
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + num24 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num22, "\" title=\"", str9, "\" target=\"Right\">", str9, "</a></td>" }));
                            this.sbList.Append(string.Concat(new object[] { "<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num22, "\" title=\"", str10, "\" target=\"Right\">", str10, "</a></td>" }));
                            if (num23 > 0)
                            {
                                this.sbList.Append(string.Concat(new object[] { " <td align=\"left\"><a href='UnionList.aspx?UnionID=", num23, "&Page=1' target='Right'>", str11, "</a></td>" }));
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"left\">暂无联盟</td>");
                            }
                            this.sbList.Append(" <td align=\"center\">-</td>");
                            this.sbList.Append(" <td align=\"center\">" + num25 + "</td>");
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
                if (strType != "USERABILITYTOP")
                {
                    if (strType != "PLAYERTOP")
                    {
                        if (strType == "PLAYER3TOP")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='XBATop.aspx?Type=USERABILITYTOP'>球队综合</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='XBATop.aspx?Type=PLAYERTOP&Page=1'>球员排行</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>街球排行</li></ul></td>");
                            return;
                        }
                        if (strType == "USERVBTOP")
                        {
                            this.sbPageIntro = new StringBuilder();
                            this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='XBATop.aspx?Type=USERABILITYTOP'>球队综合</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='XBATop.aspx?Type=PLAYERTOP&Page=1'>球员排行</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>职业战绩</li></ul></td>");
                        }
                        return;
                    }
                }
                else
                {
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>球队综合</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='XBATop.aspx?Type=PLAYERTOP&Page=1'>球员排行</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYER3TOP\"'href='XBATop.aspx?Type=PLAYER3TOP&Hour=14'>街球排行</a></li></ul></td>");
                    return;
                }
                this.sbPageIntro = new StringBuilder();
                this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='XBATop.aspx?Type=USERABILITYTOP'>球队综合</a></li>");
                this.sbPageIntro.Append("<li class='qian2'>球员排行</li>");
                this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYER3TOP\"'href='XBATop.aspx?Type=PLAYER3TOP&Page=1'>街球排行</a></li></ul></td>");
            }
        }
    }
}


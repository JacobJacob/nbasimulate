﻿namespace Web
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
        public string strType;
        private int intUserID;
        public string strScript;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
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
                if (this.intPage <= 0)
                {
                    this.intPage = 1;
                }
                this.intPerPage = 13;
                this.strType = (string)SessionItem.GetRequest("Type", 1);
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
                case "COMPANY":

                    strCurrentURL = "Stock.aspx?Type=COMPANY&" ;

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
                        dataTable = BTPStockManager.GetGetStockCompany(this.intPage, this.intPerPage);
                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row in dataTable.Rows)
                        {

                            string strClubName = Convert.ToString(row["ClubName"]);
                            int intUserID = Convert.ToInt32(row["UserID"]);
                            int intPrice = Convert.ToInt32(row["Price"]);
                            int intPerPrice = intPrice / 20;
                            int intYestodaySort = Convert.ToInt32(row["YestodaySort"]);
                            int intSort = Convert.ToInt32(row["Sort"]);
                            int intTotalSell = Convert.ToInt32(row["TotalSell"]);

                            int intTeamDay = BTPStockManager.GetStockTeamDay(this.intUserID, intUserID);
  

                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intSort + "</td>");
                            this.sbList.Append("<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");
                            //this.sbList.Append("<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strNickName + "\" target=\"Right\">" + strNickName + "</a></td>");

                            this.sbList.Append(" <td align=\"left\">" + intPrice + "</td>");
                            this.sbList.Append(" <td align=\"left\">" + intPerPrice + "</td>");
                            this.sbList.Append(" <td align=\"center\">" + intTotalSell + "</td>");
                            if (intSort > intYestodaySort)
                            {
                                this.sbList.Append(" <td align=\"center\"><font color=\"green\">-" + (intSort - intYestodaySort) + "</font></td>");
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"center\"><font color=\"red\">+" + (intYestodaySort - intSort) + "</font></td>");
                            }

                            this.sbList.Append("<td align=\"center\">");

                            if (intTeamDay == 0)
                            {
                                this.sbList.Append("<a href='SecretaryPage.aspx?Type=BUYSTOCK&StockUserID=" + intUserID + "'>买</a>");
                            }
                            else
                            {
                                this.sbList.Append("买");
                            }

                            this.sbList.Append("&nbsp;|&nbsp;");

                            if (intTeamDay > 3)
                            {
                                this.sbList.Append("<a href='SecretaryPage.aspx?Type=SELLSTOCK&StockUserID=" + intUserID + "'>卖</a>");
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
                            
                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");

                    }
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
                        dataTable = BTPXBATopManager.GetPlayer5AbilityTop(this.intPage, this.intPerPage);
                        int index = 1;
                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row in dataTable.Rows)
                        {

                            string strName = Convert.ToString(row["Name"]);
                            int intUserID = Convert.ToInt32(row["UserID"]);
                            string strNickName = Convert.ToString(row["NickName"]);
                            int intAbility = Convert.ToInt32(row["Ability"]);
                            int intAge = Convert.ToInt32(row["Age"]);
                            int intHeight = Convert.ToInt32(row["Height"]);
                            int intWeight = Convert.ToInt32(row["Weight"]);
                            int intPlayerID = Convert.ToInt32(row["PlayerID"]);

                            int intPlace = (this.intPage - 1) * this.intPerPage + index;
                            index++;

                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intPlace + "</td>");
                            this.sbList.Append("<td align=\"left\" ><span class=\"DIVPlayerName\" ><a href=\"ShowPlayer.aspx?Type=5&Kind=0&Check=0&PlayerID=" + intPlayerID + "\" target=\"Right\">" + strName + "</a></span></td>");
                            this.sbList.Append("<td align=\"left\"><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strNickName + "\" target=\"Right\">" + strNickName + "</a></td>");
                            this.sbList.Append("<td align=\"center\" >" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + intAge + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + intHeight + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + intWeight + "</td></tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);

                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");

                    }
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
                        dataTable = BTPXBATopManager.GetXBATopByCategory(2, this.intPage, this.intPerPage);
                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }
                        foreach (DataRow row in dataTable.Rows)
                        {

                            string strClubName = Convert.ToString(row["ClubName"]);
                            int intUserID = Convert.ToInt32(row["UserID"]);
                            string strNickName = Convert.ToString(row["NickName"]);
                            string strUnionName = Convert.ToString(row["UnionName"]);
                            int strUnionID = Convert.ToInt32(row["UnionID"]);
                            int intPlace = Convert.ToInt32(row["Place"]);
                            int intValue1 = Convert.ToInt32(row["Value1"]);
                            int intValue2 = Convert.ToInt32(row["Value2"]);
                         
                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intPlace + "</td>");
                            this.sbList.Append("<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");
                            this.sbList.Append("<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strNickName + "\" target=\"Right\">" + strNickName + "</a></td>");
                            if (strUnionID > 0)
                            {
                                this.sbList.Append(" <td align=\"left\"><a href='UnionList.aspx?UnionID=" + strUnionID + "&Page=1' target='Right'>" + strUnionName + "</a></td>");
                            }
                            else
                            {
                                this.sbList.Append(" <td align=\"left\">暂无联盟</td>");
                            }

                            this.sbList.Append(" <td align=\"center\">-</td>");
                            this.sbList.Append(" <td align=\"center\">" + intValue1 + "</td>");
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);

                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");

                    }
                    return;

                default:
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
            }
             
        }

        private void SetPageIntro()
        {
            switch (this.strType)
            {
                case "COMPANY":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>上市公司</li>");  
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='Stock.aspx?Type=MYSTOCK&Page=1'>我的股票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERVBTOP\" 'href='Stock.aspx?Type=TRADELOG'>交易日志</a></li></ul></td>");
                    return;

                case "MYSTOCK":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='Stock.aspx?Type=COMPANY'>上市公司</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>我的股票</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERVBTOP\"'href='Stock.aspx?Type=TRADELOG'>交易日志</a></li></ul></td>");
                    return;

                case "LOG":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=USERABILITYTOP\"' href='XBATop.aspx?Type=USERABILITYTOP'>上市公司</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XBATop.aspx?Type=PLAYERTOP\"'  href='Stock.aspx?Type=MYSTOCK&Page=1'>我的股票</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>交易日志</li></ul></td>");
                    return;
            }
            
        }
    }
}


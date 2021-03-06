﻿namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class WealthMarket : Page
    {
        protected ImageButton ImgBtnOK;
        protected ImageButton ImgBuyBtnOK;
        protected ImageButton ImgSeleBtnOK;
        private int intHasOrder = 1;
        private int intPage;
        private int intPrePage;
        private int intUserID;
        public StringBuilder sbList = new StringBuilder();
        public string strBuyOne;
        public string strDateTime;
        public string strGetScript = "";
        public string strGetViewPage = "";
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strPrice;
        public string strSaleOne;
        private string strType;
        protected HtmlTable tblBusiness;
        protected HtmlTable tblWealthMarket;

        [AjaxMethod]
        public string GetBuyOne()
        {
            return BTPOrderManager.GetOrderParameter()["BuyOne"].ToString().Trim();
        }

        [AjaxMethod]
        public string GetPrice()
        {
            return BTPOrderManager.GetOrderParameter()["Price"].ToString().Trim();
        }

        [AjaxMethod]
        public string GetSaleOne()
        {
            return BTPOrderManager.GetOrderParameter()["SaleOne"].ToString().Trim();
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int orderBusinessCountByUserID = 0;
            if (this.strType == "BUSINESS")
            {
                orderBusinessCountByUserID = BTPOrderManager.GetOrderBusinessCountByUserID(this.intUserID);
            }
            return orderBusinessCountByUserID;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
            int total = this.GetTotal();
            int num2 = (total / this.intPrePage) + 1;
            if ((total % this.intPrePage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str = "";
            if (this.intPage == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (this.intPage == num2)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str3 = str3 + " selected";
                }
                obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            obj2 = str + " " + str2 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str3 });
        }

        private void ImgBtnOK_Click(object sender, ImageClickEventArgs e)
        {
        }

        private void ImgBuyBtnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (this.intHasOrder == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=804");
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                int num = Convert.ToInt32(accountRowByUserID["Category"]);
                int num2 = Convert.ToInt32(accountRowByUserID["DevLvl"]);
                DataRow orderParameter = BTPOrderManager.GetOrderParameter();
                if (((num2 > ((int) orderParameter["Level"])) || (num != 5)) && (this.intUserID != 1))
                {
                    base.Response.Redirect("Report.aspx?Parameter=808");
                }
                else
                {
                    base.Response.Redirect("SecretaryPage.aspx?Type=WEALTHORDER&State=1");
                }
            }
        }

        private void ImgSeleBtnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (this.intHasOrder == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=804");
            }
            else
            {
                base.Response.Redirect("SecretaryPage.aspx?Type=WEALTHORDER&State=2");
            }
        }

        private void InitializeComponent()
        {
            this.ImgSeleBtnOK.Click += new ImageClickEventHandler(this.ImgSeleBtnOK_Click);
            this.ImgBuyBtnOK.Click += new ImageClickEventHandler(this.ImgBuyBtnOK_Click);
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
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strType = SessionItem.GetRequest("Type", 1);
                this.intPage = SessionItem.GetRequest("Page", 0);
                this.intPrePage = 10;
                this.tblWealthMarket.Visible = false;
                this.tblBusiness.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(WealthMarket));
        }

        private void SetBusinessList()
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.sbList.Append("<tr class=\"BarHead\"><td height=24 width=78 align=center>交易种类</td>");
            this.sbList.Append("<td height=24 width=128 align=center>交易价格</td><td height=24 width=66 align=center>交易数量</td>");
            this.sbList.Append("<td height=24 width=128 align=center>交易资金</td><td height=24 width=128  align=center>交易时间</td></tr>");
            string str = "";
            string str2 = "";
            DataTable table = BTPOrderManager.GetOrderBusinessByUserID(this.intUserID, 0, this.intPage, this.intPrePage);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["SaleUserID"];
                    int num2 = (int) row["BuyUserID"];
                    int num3 = (int) row["Price"];
                    int num4 = (int) row["Wealth"];
                    int num5 = (int) row["Money"];
                    DateTime datIn = Convert.ToDateTime(row["DateTime"]);
                    if (num == this.intUserID)
                    {
                        str = "<font color=red>卖出游戏币</font>";
                        str2 = "<font color=green>" + num5 + "</font>";
                    }
                    if (num2 == this.intUserID)
                    {
                        str = "<font color=green>买进游戏币</font>";
                        str2 = "<font color=red>" + num5 + "</font>";
                    }
                    this.sbList.Append("<tr height=24 onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=24 align=center>" + str + "</td>");
                    this.sbList.Append(string.Concat(new object[] { "<td height=24 align=center>", num3, "资金/枚</td><td height=24 align=center>", num4, "枚</td>" }));
                    this.sbList.Append("<td height=24 align=center>" + str2 + "资金</td><td height=24  align=center>" + StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm") + "</td></tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
            else
            {
                this.sbList.Append("<tr class='BarContent'><td height=24 colspan=6 align=center>暂无交易记录</td></tr>");
            }
            this.strGetViewPage = this.GetViewPage("WealthMarket.aspx?Type=BUSINESS&");
            this.strGetScript = this.GetScript("WealthMarket.aspx?Type=BUSINESS&");
        }

        private void SetWealthMarket()
        {
            DataRow orderParameter = BTPOrderManager.GetOrderParameter();
            if (orderParameter != null)
            {
                this.strBuyOne = orderParameter["BuyOne"].ToString();
                this.strSaleOne = orderParameter["SaleOne"].ToString();
                this.strPrice = orderParameter["Price"].ToString();
                if (this.strBuyOne == "0")
                {
                    this.strBuyOne = "<font color=\"red\">无订单</font>";
                }
                else
                {
                    this.strBuyOne = "<font color=\"red\">" + this.strBuyOne + "</font>资金/游戏币";
                }
                if (this.strSaleOne == "0")
                {
                    this.strSaleOne = "<font color=\"green\">无订单</font>";
                }
                else
                {
                    this.strSaleOne = "<font color=\"green\">" + this.strSaleOne + "</font>资金/游戏币";
                }
                this.strDateTime = StringItem.FormatDate(Convert.ToDateTime(orderParameter["LastDateTime"]), "yyyy-MM-dd hh:mm:ss");
                this.sbList.Append("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"536\" bgColor=\"#fcf2ec\" border=\"0\">");
                this.sbList.Append("<tr class=\"BarHead\"><td height=24 width=78 align=center>交易类别</td>");
                this.sbList.Append("<td height=24 width=120 align=center>价格</td><td height=24 width=48 align=center>数量</td>");
                this.sbList.Append("<td height=24 width=142 align=center>交易总资金</td><td height=24 width=108  align=center>提交时间</td><td width=50  align=center>操作</td></tr>");
                DataTable orderRowByUserID = BTPOrderManager.GetOrderRowByUserID(this.intUserID);
                if (orderRowByUserID != null)
                {
                    foreach (DataRow row2 in orderRowByUserID.Rows)
                    {
                        string str;
                        string str2 = "";
                        int num = (int) row2["OrderID"];
                        int num2 = Convert.ToInt32(row2["Category"]);
                        int num3 = (int) row2["Price"];
                        long num4 = Convert.ToInt64(row2["Wealth"]);
                        long num5 = Convert.ToInt64(row2["Money"]);
                        DateTime datIn = Convert.ToDateTime(row2["DateTime"]);
                        if (num2 == 1)
                        {
                            str = "买入游戏币";
                            str2 = "<font color=red>-" + num5 + "</font>";
                        }
                        else
                        {
                            str = "卖出游戏币";
                            str2 = "<font color=green>+" + num5 + "</font>";
                        }
                        this.sbList.Append("<tr height=24 onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=24 align=center>" + str + "</td>");
                        this.sbList.Append(string.Concat(new object[] { "<td height=24 align=center>", num3, "资金/枚</td><td height=24 align=center>", num4, "枚</td>" }));
                        this.sbList.Append("<td height=24 align=center>" + str2 + "资金</td><td height=24  align=center>" + StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm") + "</td>");
                        this.sbList.Append("<td width=40 align=center><a href='SecretaryPage.aspx?Type=CANCELORDER&OrderID=" + num + "'>撤销</a></td></tr>");
                    }
                }
                else
                {
                    this.intHasOrder = 0;
                    this.sbList.Append("<tr class='BarContent'><td height=24 colspan=7 align=center>暂无订单提交</td></tr>");
                    this.sbList.Append("</table>");
                }
            }
        }
    }
}


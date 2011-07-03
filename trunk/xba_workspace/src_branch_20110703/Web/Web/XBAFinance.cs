namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class XBAFinance : Page
    {
        protected ImageButton btnPayOK;
        public int intCoin;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strCoin;
        public string strIndexEnd;
        public string strIndexHead;
        public string strList;
        public string strNickName;
        public string strScript;
        private string strType;
        protected TextBox tbPayCoin;
        protected TextBox tbPayNick;
        protected TextBox tbPayUser;

        private void btnPayOK_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            try
            {
                num = Convert.ToInt32(this.tbPayCoin.Text.ToString().Trim());
            }
            catch
            {
                base.Response.Redirect("Report.aspx?Parameter=433");
                return;
            }
            string validWords = StringItem.GetValidWords(this.tbPayNick.Text.ToString().Trim());
            if (StringItem.IsNumber(num))
            {
                if (!StringItem.IsValidName(validWords, 1, 20))
                {
                    base.Response.Redirect("Report.aspx?Parameter=436");
                }
                else if (!DBLogin.CanConn(0))
                {
                    base.Response.Redirect("Report.aspx?Parameter=445");
                }
                else
                {
                    switch (ROOTUserManager.ConferCoin(this.intUserID, validWords, num))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=431");
                            return;

                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=430");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=429");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=434");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=435");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=433");
            }
        }

        private void GetCoinList()
        {
            string strCurrentURL = "XBAFinance.aspx?Type=COIN&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 11;
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<table width='502' border='2' cellpadding='0' cellspacing='0' bordercolor='#FFFFFF'><tr><td width='30' height='26' align='center' valign='middle' bgcolor='#AE1100'><strong>收入</strong></td><td width='30' align='center' valign='middle' bgcolor='#AE1100'><strong>支出</strong></td><td width='112' align='center' valign='middle' bgcolor='#AE1100'><strong>时间</strong></td><td width='179' align='center' bgcolor='#AE1100'><strong>事件</strong></td><td width='138' align='center' bgcolor='#AE1100'><strong>备注</strong></td></tr>";
            SqlDataReader reader = ROOTUserManager.GetCoinFinanceByUserIDNew(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string str2 = reader["Event"].ToString();
                    string str3 = reader["Remark"].ToString();
                    int num3 = (int) reader["Income"];
                    int num4 = (int) reader["Outcome"];
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    if (str3 == "")
                    {
                        str3 = "无";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20' align='center' style='color:#9E0F00'>", num3, "</td><td align='center' style='color:#9E0F00'>", num4, "</td><td align='center' style='color:#9E0F00'>", StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss"), "</td><td style='color:#9E0F00' style='padding:2px;'>", str2, "</td><td style='color:#9E0F00' style='padding:2px;'>", str3, "</td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='28' align='right' colspan='5' style='color:#9E0F00'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height='25' align='center' colspan='5' style='color:#9E0F00'>暂无消费记录。</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "</table>";
        }

        private void GetFreeCoin()
        {
            string str = "http://www.xba.com.cn/FromURL.aspx?FromID=" + this.intUserID + "&JumpToURL=";
            this.strList = "<table width='502' border='1' cellpadding='0' cellspacing='0' height='292' bordercolor='#FFFFFF'><tr><td><table width='502' border='0' cellpadding='0' cellspacing='0' height='292'><tr><td height='30' colspan='2' style='color:#9E0F00' align='center'><font>免费轻松获得3枚金币</font></td></tr><tr><td height='75' colspan='2' style='color:#9E0F00' style='padding-left:4px'>XBA已经为每位经理分配一个“推广链”。您的朋友通过此链接进入XBA注册后，只要其<font color='red'>街球队</font>达到一定的活跃度，我们将会给您<font color='red'>3枚金币</font>作为奖励。<br>您可以将此地址拷贝发给您QQ、MSN或者论坛上的朋友。</td></tr><tr><td style='color:#9E0F00' valign='top' width='50' align='center'>图例：</td><td style='color:#9E0F00' align='center' width='450'><img src='" + SessionItem.GetImageURL() + "Web/FreeCoin.gif'></td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' style='padding-left:4px'><font color='red'>您的推广链地址是：</font>" + str + "</td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' align='center'><a href=\"javascript:\" onclick=\"clipboardData.setData('Text','" + str + "')\"><img src='" + SessionItem.GetImageURL() + "Web/btnCopy.gif' height='19' width='68' border='0'></a></td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' style='padding-left:4px'><font color='red'>注：</font>您的朋友只有点击<font color='red'>完整的广告链</font>进入XBA注册才能生效，请仔细记录此链接。</td></tr></table></td></tr></table>";
        }

        private void GetOrderList()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str2;
            string str5;
            string str6;
            string strCurrentURL = "XBAFinance.aspx?Type=ORDER&";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 11;
            this.GetTotal();
            string str3 = "--";
            string str4 = "--";
            SqlDataReader reader = ROOTUserManager.GetOrderTableByUserIDNew(this.intUserID, this.intPage, this.intPerPage);
            this.strList = "<table width='502' border='2' cellpadding='0' cellspacing='0' bordercolor='#ffffff'><tr><td width='100' height='26' align='center' valign='middle' bgcolor='#ae1100'><strong>物品</strong></td><td width='129' align='center' valign='middle' bgcolor='#ae1100'><strong>支付方式</strong></td><td width='50' align='center' valign='middle' bgcolor='#ae1100'><strong>数量</strong></td><td width='50' align='center' bgcolor='#ae1100'><strong>价格</strong></td><td width='60' align='center' bgcolor='#ae1100'><strong>状态</strong></td><td width='100' align='center' bgcolor='#AE1100'><strong>操作</strong></td></tr>";
            if (reader.HasRows)
            {
                goto Label_02D0;
            }
            this.strList = this.strList + "<tr><td height='30' colspan='7' align='center' style='color:#9E0F00'>暂时没有订单记录</td></tr>";
            goto Label_02DC;
        Label_0155:
            switch (num4)
            {
                case 1:
                    str3 = "已结算";
                    str4 = "--";
                    break;

                case 2:
                    if (num6 == 1)
                    {
                        str3 = "未付款";
                        str4 = string.Concat(new object[] { "<a href='PayType.aspx?Type=PAY&OrderCategory=", num5, "&OrderID=", num, "&OID=", str5, "'>付款</a>" });
                    }
                    else
                    {
                        str3 = "未充值";
                        str4 = "<a href='#'>充值</a>";
                    }
                    break;

                default:
                    switch (num4)
                    {
                        case 3:
                            str3 = "核查中";
                            str4 = "--";
                            break;

                        case 0:
                            str3 = "未结算";
                            str4 = string.Concat(new object[] { "<a href='SetQuery.aspx?ORDERID=", num, "&OID=", str5, "' onclick=\"return MessageDel(2);\">核查</a>" });
                            break;
                    }
                    break;
            }
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20'><font color='#9e0f00'>", str2, "</font></td><td style='color:#9E0F00'>", str6, "</td><td style='color:#9E0F00'>", num2, "</td><td style='color:#9E0F00'>", num3, "</td><td style='color:#9E0F00'>", str3, "</td><td style='color:#9E0F00'>", str4, "</td></tr>" });
        Label_02D0:
            if (reader.Read())
            {
                num = (int) reader["OrderID"];
                num6 = (byte) reader["Category"];
                str5 = (string) reader["OID"];
                num2 = (int) reader["Count"];
                num3 = (int) reader["Price"];
                num4 = (byte) reader["Status"];
                str2 = reader["OrderName"].ToString().Trim();
                num5 = (byte) reader["OrderCategory"];
                switch (num6)
                {
                    case 1:
                        str6 = "银行卡支付";
                        goto Label_0155;

                    case 2:
                        str6 = "点卡充值";
                        goto Label_0155;
                }
                str6 = "金币充值";
                goto Label_0155;
            }
        Label_02DC:
            reader.Close();
            this.strList = this.strList + "<tr><td height='30' colspan='7' align='right' style='padding-right:15px' style='color:#9E0F00'>" + this.GetViewPage(strCurrentURL) + "</td></tr></table>";
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int wealthCountByUserIDNew = 0;
            if (this.strType == "COIN")
            {
                return ROOTUserManager.GetCoinFinanceCountNew(this.intUserID);
            }
            if (this.strType == "ORDER")
            {
                return ROOTUserManager.GetOrderCountByUserIDNew(this.intUserID);
            }
            if (this.strType == "WEALTH")
            {
                wealthCountByUserIDNew = ROOTWealthManager.GetWealthCountByUserIDNew(this.intUserID);
            }
            return wealthCountByUserIDNew;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
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
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
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
            return string.Concat(new object[] { str2, " ", str3, " 共", total, "个记录 跳转", str4 });
        }

        private void GetWealthList()
        {
            string strCurrentURL = "XBAFinance.aspx?Type=WEALTH&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 11;
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<table width='502' border='2' cellpadding='0' cellspacing='0' bordercolor='#FFFFFF'><tr><td width='65' height='26' align='center' valign='middle' bgcolor='#AE1100'><strong>收入</strong></td><td width='65' align='center' valign='middle' bgcolor='#AE1100'><strong>支出</strong></td><td width='112' align='center' valign='middle' bgcolor='#AE1100'><strong>时间</strong></td><td width='179' align='center' bgcolor='#AE1100'><strong>事件</strong></td><td width='68' align='center' bgcolor='#AE1100'><strong>备注</strong></td></tr>";
            SqlDataReader reader = ROOTWealthManager.GetWealthTableNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string str2 = reader["Event"].ToString();
                    int num = (int) reader["Income"];
                    int num2 = (int) reader["Outcome"];
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20' align='center' style='color:#9E0F00'>", num, "</td><td align='center' style='color:#9E0F00'>", num2, "</td><td align='center' style='color:#9E0F00'>", StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss"), "</td><td style='color:#9E0F00' style='padding:2px;'>", str2, "</td><td style='color:#9E0F00' style='padding:2px;'>无</td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='28' align='right' colspan='5' style='color:#9E0F00'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height='25' align='center' colspan='5' style='color:#9E0F00'>暂无消费记录。</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "</table>";
        }

        private void InitializeComponent()
        {
            this.btnPayOK.Click += new ImageClickEventHandler(this.btnPayOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.btnPayOK.ImageUrl = SessionItem.GetImageURL() + "web/button_03.gif";
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.intCoin = (int) userInfoByID["Coin"];
                this.strCoin = Convert.ToString(this.intCoin).Trim();
                string strUserName = userInfoByID["UserName"].ToString().Trim();
                string strPassword = userInfoByID["Password"].ToString().Trim();
                this.strIndexHead = BoardItem.GetIndexHead(this.intUserID, strUserName, strPassword);
                this.strIndexEnd = BoardItem.GetIndexEnd();
                this.strType = SessionItem.GetRequest("TYPE", 1).ToString().Trim();
                switch (this.strType)
                {
                    case "COIN":
                        this.GetCoinList();
                        break;

                    case "ORDER":
                        this.GetOrderList();
                        break;

                    case "FREE":
                        this.GetFreeCoin();
                        break;

                    case "WEALTH":
                        this.GetWealthList();
                        break;

                    default:
                        this.strType = "COIN";
                        this.GetCoinList();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


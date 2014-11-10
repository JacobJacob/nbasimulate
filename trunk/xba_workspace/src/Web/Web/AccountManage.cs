namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class AccountManage : Page
    {
        protected ImageButton btnJOK;
        protected ImageButton btnOK;
        protected ImageButton btnPayOK;
        protected ImageButton btnRCancel;
        protected ImageButton btnROK;
        protected ImageButton btnUpdate;
        public DateTime datMemberExpireTime;
        public int intCoin;
        public int intConferCoin;
        private int intGameCategory;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intUserID;
        protected RadioButton rbA;
        protected RadioButton rbB;
        protected RadioButton rbC;
        protected RadioButton rbD;
        protected RadioButton rbE;
        public string strConferUserName;
        public string strdatMemberExpireTime;
        public string strList;
        public string strMemberExpireTime;
        public string strMsg;
        public string strNickName;
        public string strPageInfo;
        public string strPageIntro;
        private string strPassword;
        public string strScript;
        private string strType;
        public string strUserGameTable;
        public string strUserName;
        public string strWealth;
        protected TextBox tbFlackURL;
        protected HtmlTable tblConfer;
        protected HtmlTable tblFlack;
        protected HtmlTable tblMember;
        protected HtmlTable tblNotMember;
        protected HtmlTable tblOrder;
        protected HtmlTable tblPay;
        protected HtmlTable tblReg;
        protected HtmlTable tblRegTure;
        protected HtmlTable tblStatus;
        protected HtmlTable tblView;
        protected HtmlTable tblViewFlack;
        protected HtmlTable tblWealth;
        protected TextBox tbPayCoin;
        protected TextBox tbPayNick;
        protected TextBox tbPayUser;
        protected HtmlTableRow trMember;
        protected HtmlTableRow trNotMember;

        private void btnJOK_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("");
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            int num;
            int num2;
            int num3;
            DateTime now = DateTime.Now;
            int num4 = 0x3e8 + RandomItem.rnd.Next(0, 0x3e8);
            string strOID = "34107" + StringItem.FormatDate(now, "yyyyMMddhhmmss") + num4;
            if (this.rbA.Checked)
            {
                str = "20枚金币";
                num = 1;
                num2 = 10;
                num3 = 1;
            }
            else if (this.rbB.Checked)
            {
                str = "40枚金币";
                num = 1;
                num2 = 20;
                num3 = 2;
            }
            else if (this.rbC.Checked)
            {
                str = "100枚金币";
                num = 1;
                num2 = 0x2d;
                num3 = 3;
            }
            else if (this.rbD.Checked)
            {
                str = "250枚金币";
                num = 1;
                num2 = 100;
                num3 = 4;
            }
            else
            {
                str = "3000枚金币";
                num = 1;
                num2 = 0x3e8;
                num3 = 5;
            }
            ROOTUserManager.AddPayOrder(this.intUserID, this.strUserName, 1, strOID, str, num3, num, num2);
            string url = "AccountManage.aspx?Type=ORDER&Page=1";
            base.Response.Redirect(url);
        }

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
            string strIn = this.tbPayUser.Text.ToString().Trim();
            string validWords = this.tbPayNick.Text.ToString().Trim();
            strIn = StringItem.GetValidWords(strIn);
            validWords = StringItem.GetValidWords(validWords);
            if (!StringItem.IsNumber(num))
            {
                base.Response.Redirect("Report.aspx?Parameter=433");
            }
            else if (!StringItem.IsValidName(strIn, 1, 20))
            {
                base.Response.Redirect("Report.aspx?Parameter=432");
            }
            else if (!StringItem.IsValidName(validWords, 1, 20))
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
                        break;

                    case 1:
                        base.Response.Redirect("Report.aspx?Parameter=430");
                        return;

                    case 2:
                    {
                        string strNickName = BTPAccountManager.GetAccountRowByUserName(strIn)["NickName"].ToString().Trim();
                        BTPMessageManager.AddMessage(0, "篮球经理", this.strNickName, string.Concat(new object[] { "您赠与", strNickName, "经理金币", num, "枚" }));
                        BTPMessageManager.AddMessage(0, "篮球经理", strNickName, string.Concat(new object[] { this.strNickName, "经理赠与您", num, "枚金币" }));
                        base.Response.Redirect("Report.aspx?Parameter=429");
                        return;
                    }
                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=434");
                        return;

                    case 4:
                        base.Response.Redirect("Report.aspx?Parameter=435");
                        return;

                    default:
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                }
            }
        }

        private void btnRCancel_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("AccountManage.aspx?Type=REG");
        }

        private void btnROK_Click(object sender, ImageClickEventArgs e)
        {
            int num = 20;
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            int num2 = (byte) userInfoByID["PayType"];
            this.datMemberExpireTime = (DateTime) userInfoByID["MemberExpireTime"];
            if (this.datMemberExpireTime < DateTime.Now)
            {
                this.datMemberExpireTime = DateTime.Now.AddMonths(1);
            }
            else
            {
                this.datMemberExpireTime = this.datMemberExpireTime.AddMonths(1);
            }
            int num3 = (int) userInfoByID["Coin"];
            if (num3 < num)
            {
                base.Response.Redirect("Report.aspx?Parameter=403");
            }
            else
            {
                bool flag;
                try
                {
                    userInfoByID = BTPAccountManager.GetAccountRowByUserID(this.intUserID, DBLogin.ConnString(this.intGameCategory));
                    num2 = (byte) userInfoByID["PayType"];
                    DateTime time = (DateTime) userInfoByID["MemberExpireTime"];
                    if ((num2 == 0) && (time < DateTime.Now))
                    {
                        time = DateTime.Now.AddMonths(1);
                    }
                    else
                    {
                        time = time.AddMonths(1);
                    }
                    string commandText = string.Concat(new object[] { "UPDATE BTP_Account SET PayType=1,MemberExpireTime='", time.ToString(), "' WHERE UserID=", this.intUserID });
                    SqlHelper.ExecuteNonQuery(DBLogin.ConnString(this.intGameCategory), CommandType.Text, commandText);
                    BTPToolLinkManager.PresentTool(this.intUserID, 3, 0, 3, DBLogin.ConnString(this.intGameCategory));
                    BTPToolLinkManager.PresentTool(this.intUserID, 2, 0, 3, DBLogin.ConnString(this.intGameCategory));
                    ROOTUserManager.RegAssociator(this.intUserID, this.datMemberExpireTime);
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
                if (flag)
                {
                    base.Response.Redirect("Report.aspx?Parameter=402");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=4031");
                }
            }
        }

        private void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string strFlackURL = this.tbFlackURL.Text.ToString().Trim().ToLower();
            if (((strFlackURL.Length < 30) || (strFlackURL.IndexOf("http://") == -1)) || ((strFlackURL.IndexOf(".") == -1) || (strFlackURL.IndexOf("http://www.xba.com.cn") > 0)))
            {
                base.Response.Redirect("Report.aspx?Parameter=410");
            }
            else
            {
                switch (ROOTUserManager.AddFlack(this.intUserID, strFlackURL))
                {
                    case 0:
                        base.Response.Redirect("Report.aspx?Parameter=405");
                        return;

                    case 1:
                        base.Response.Redirect("Report.aspx?Parameter=404");
                        return;

                    case 2:
                        base.Response.Redirect("Report.aspx?Parameter=406");
                        return;

                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=409");
                        return;
                }
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void GetConferList()
        {
        }

        private void GetFlack()
        {
        }

        private void GetOrderList()
        {
            string strCurrentURL = "AccountManage.aspx?Type=ORDER&";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPage = SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            string str2 = "--";
            string str3 = "--";
            DataTable table = ROOTUserManager.GetOrderTableByUserID(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='30' colspan='7' align='center'>暂时没有订单记录</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str4;
                    object obj2;
                    int num3 = (int) row["OrderID"];
                    int num4 = (byte) row["Category"];
                    string str5 = (string) row["OID"];
                    int num5 = (int) row["Count"];
                    int num6 = (int) row["Price"];
                    int num7 = (byte) row["Status"];
                    string str6 = row["OrderName"].ToString().Trim();
                    int num8 = (byte) row["OrderCategory"];
                    switch (num4)
                    {
                        case 1:
                            str4 = "银行卡支付";
                            break;

                        case 2:
                            str4 = "点卡充值";
                            break;

                        default:
                            str4 = "金币充值";
                            break;
                    }
                    switch (num7)
                    {
                        case 1:
                            str2 = "已结算";
                            str3 = string.Concat(new object[] { "<a href='DelPayOrder.aspx?ORDERID=", num3, "&OID=", str5, "' onclick=\"return MessageDel(1);\">删除</a>" });
                            goto Label_0320;

                        case 2:
                            if (num4 != 1)
                            {
                                break;
                            }
                            str2 = "未付款";
                            str3 = string.Concat(new object[] { "<a href='Pay.aspx?OrderCategory=", num8, "&OrderID=", num3, "&OID=", str5, "'>付款</a> | <a href='DelPayOrder.aspx?ORDERID=", num3, "&OID=", str5, "' onclick=\"return MessageDel(1);\">删除</a>" });
                            goto Label_0320;

                        default:
                            switch (num7)
                            {
                                case 0:
                                    str2 = "未结算";
                                    str3 = string.Concat(new object[] { "<a href='SetQuery.aspx?ORDERID=", num3, "&OID=", str5, "' onclick=\"return MessageDel(2);\">核查</a> | <a href='DelPayOrder.aspx?ORDERID=", num3, "&OID=", str5, "' onclick=\"return MessageDel(1);\">删除</a>" });
                                    break;

                                case 3:
                                    str2 = "核查中";
                                    str3 = "--";
                                    break;
                            }
                            goto Label_0320;
                    }
                    str2 = "未充值";
                    str3 = string.Concat(new object[] { "<a href='#'>充值</a> | <a href='DelPayOrder.aspx?ORDERID=", num3, "&OID=", str5, "' onclick=\"return MessageDel(1);\">删除</a>" });
                Label_0320:
                    obj2 = this.strList;
                    this.strList = string.Concat(new object[] { obj2, "<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'></td><td>", str6, "</td><td>", str4, num4, "</td><td>", num5, "</td><td>", num6, "</td><td>", str2, "</td><td>", str3, "</td><td></td></tr>" });
                }
            }
            this.strList = this.strList + "<tr><td height='30' colspan='7' align='right' style='padding-right:15px'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void GetPay()
        {
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intCoin = (int) userInfoByID["Coin"];
        }

        private void GetReg()
        {
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intCoin = (int) userInfoByID["Coin"];
            int num = (byte) userInfoByID["PayType"];
            this.datMemberExpireTime = (DateTime) userInfoByID["MemberExpireTime"];
            if (num == 1)
            {
                this.tblMember.Visible = true;
                this.strdatMemberExpireTime = "截至" + StringItem.FormatDate(this.datMemberExpireTime, "yyyy-MM-dd");
                this.strMemberExpireTime = StringItem.FormatDate(this.datMemberExpireTime.AddMonths(1), "yyyy-MM-dd").ToString();
            }
            else
            {
                this.tblNotMember.Visible = true;
                this.strdatMemberExpireTime = "非XBA会员请<a href='AccountManage.aspx?Type=REG'>注册会员。</a>";
                this.strMemberExpireTime = StringItem.FormatDate(DateTime.Now.AddMonths(1), "yyyy-MM-dd").ToString();
            }
            this.strUserGameTable = "";
            DataTable userGameTableByUserID = ROOTUserGameManager.GetUserGameTableByUserID(this.intUserID);
            if (userGameTableByUserID != null)
            {
                foreach (DataRow row2 in userGameTableByUserID.Rows)
                {
                    int intCategory = (int) row2["Category"];
                    try
                    {
                        if (((byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID, DBLogin.ConnString(intCategory))["PayType"]) == 0)
                        {
                            this.strUserGameTable = this.strUserGameTable + "您还没有在" + DBLogin.GameNameChinese(intCategory) + "注册会员，是否注册？按钮<br>";
                        }
                        else
                        {
                            object strUserGameTable = this.strUserGameTable;
                            this.strUserGameTable = string.Concat(new object[] { strUserGameTable, "您是", DBLogin.GameNameChinese(intCategory), "的会员，您要续费吗？<a href=\"AccountManage.aspx?Type=REGTRUE&GameCategory=", intCategory, "\">按钮</a><br>" });
                        }
                    }
                    catch
                    {
                        base.Response.Write("" + DBLogin.GameNameChinese(intCategory) + "游戏服务器暂时无法连通！");
                    }
                }
            }
        }

        private void GetRegTrue()
        {
            this.intGameCategory = SessionItem.GetRequest("GameCategory", 0);
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intCoin = (int) userInfoByID["Coin"];
            if (this.intCoin < 20)
            {
                base.Response.Redirect("Report.aspx?Parameter=403");
            }
            else
            {
                userInfoByID = BTPAccountManager.GetAccountRowByUserID(this.intUserID, DBLogin.ConnString(this.intGameCategory));
                int num = (byte) userInfoByID["PayType"];
                this.datMemberExpireTime = (DateTime) userInfoByID["MemberExpireTime"];
                if (num == 1)
                {
                    this.trMember.Visible = true;
                    this.strdatMemberExpireTime = StringItem.FormatDate(this.datMemberExpireTime.AddMonths(1), "yyyy-MM-dd").ToString();
                }
                else
                {
                    this.trNotMember.Visible = true;
                    this.strdatMemberExpireTime = StringItem.FormatDate(DateTime.Now.AddMonths(1), "yyyy-MM-dd").ToString();
                }
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private void GetStatus()
        {
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intCoin = (int) userInfoByID["Coin"];
            int num = (byte) userInfoByID["PayType"];
            this.datMemberExpireTime = (DateTime) userInfoByID["MemberExpireTime"];
            if (num == 1)
            {
                this.strdatMemberExpireTime = "截至" + StringItem.FormatDate(this.datMemberExpireTime, "yyyy-MM-dd");
            }
            else
            {
                this.strdatMemberExpireTime = "非XBA会员，购买金币后可以在游戏中购买道具会员卡，成为XBA会员。";
            }
        }

        private int GetTotal()
        {
            int orderCountByUserID = 0;
            if (this.strType == "VIEW")
            {
                return ROOTUserManager.GetCoinFinanceCount(this.intUserID);
            }
            if (this.strType == "VIEWFLACK")
            {
                return ROOTUserManager.GetFlackCount(this.intUserID);
            }
            if (this.strType == "WEALTH")
            {
                return ROOTWealthManager.GetWealthCountByUserID(this.intUserID);
            }
            if (this.strType == "ORDER")
            {
                orderCountByUserID = ROOTUserManager.GetOrderCountByUserID(this.intUserID);
            }
            return orderCountByUserID;
        }

        private void GetView()
        {
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intCoin = (int) userInfoByID["Coin"];
            string strCurrentURL = "AccountManage.aspx?Type=VIEW&";
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable table = ROOTUserManager.GetCoinFinanceByUserID(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='25' align='center' colspan='5'>暂无消费记录。</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in table.Rows)
                {
                    string str2 = row2["Event"].ToString();
                    string str3 = row2["Remark"].ToString();
                    int num3 = (int) row2["Income"];
                    int num4 = (int) row2["Outcome"];
                    DateTime datIn = (DateTime) row2["CreateTime"];
                    if (str3 == "")
                    {
                        str3 = "无";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td align='center'><font color='green'>", num3, "</font></td><td align='center'><font color='red'>", num4, "</font></td><td align='center'>", StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss"), "</td><td height='25'>", str2, "</td><td>", str3, "</td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
        }

        private void GetViewFlack()
        {
            string strCurrentURL = "AccountManage.aspx?Type=VIEWFLACK&";
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable table = ROOTUserManager.GetFlackList(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='25' align='center' colspan='3'>暂无宣传帖记录。</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2;
                    string str3 = row["FlackURL"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    int num3 = (byte) row["CoinGain"];
                    row["CoinGain"].ToString().Trim();
                    int num4 = (byte) row["Status"];
                    if (num4 == 0)
                    {
                        str2 = "等待审核";
                    }
                    else if ((num4 == 2) && (num3 == 1))
                    {
                        str2 = "奖励1金币。";
                    }
                    else if ((num4 == 2) && (num3 == 2))
                    {
                        str2 = "奖励2金币。";
                    }
                    else if ((num4 == 2) && (num3 == 3))
                    {
                        str2 = "奖励3金币。";
                    }
                    else
                    {
                        str2 = "未通过审核";
                    }
                    string strList = this.strList;
                    this.strList = strList + "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>" + StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss") + "</td><td><a href='" + str3 + "' target='_blank'>" + str3 + "</a></td><td align='center'>" + str2 + "</td></tr>";
                }
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
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
                object obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            return string.Concat(new object[] { str, " ", str2, " 共", total, "个记录 跳转", str3 });
        }

        private void GetWealth()
        {
            string strCurrentURL = "AccountManage.aspx?Type=WEALTH&";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPage = SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            if (userInfoByID != null)
            {
                int intWealth = (int) userInfoByID["Wealth"];
                string userLevel = UserData.GetUserLevel(intWealth);
                this.strWealth = string.Concat(new object[] { "论坛财富：", intWealth, "　论坛等级：", userLevel });
            }
            DataTable table = ROOTWealthManager.GetWealthTable(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='30' colspan='6' align='center'>暂时没有财富记录</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in table.Rows)
                {
                    int num1 = (int) row2["WealthID"];
                    int num4 = (int) row2["Income"];
                    int num5 = (int) row2["Outcome"];
                    byte num6 = (byte) row2["Category"];
                    string str3 = row2["Event"].ToString().Trim();
                    DateTime datIn = (DateTime) row2["CreateTime"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'></td><td>", str3, "</td><td>", num4, "</td><td>", num5, "</td><td>", StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss"), "</td><td></td></tr>" });
                }
            }
            this.strList = this.strList + "<tr><td height='30' colspan='6' align='right' style='padding-right:15px'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.btnJOK.Click += new ImageClickEventHandler(this.btnJOK_Click);
            this.btnROK.Click += new ImageClickEventHandler(this.btnROK_Click);
            this.btnRCancel.Click += new ImageClickEventHandler(this.btnRCancel_Click);
            this.btnUpdate.Click += new ImageClickEventHandler(this.btnUpdate_Click);
            this.btnPayOK.Click += new ImageClickEventHandler(this.btnPayOK_Click);
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
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.strUserName = userInfoByID["UserName"].ToString().Trim();
                this.intPayType = (byte) userInfoByID["PayType"];
                this.strConferUserName = userInfoByID["UserName"].ToString().Trim();
                this.intConferCoin = (int) userInfoByID["Coin"];
                this.strPassword = userInfoByID["Password"].ToString().Trim();
                this.tblFlack.Visible = false;
                this.tblPay.Visible = false;
                this.tblReg.Visible = false;
                this.tblRegTure.Visible = false;
                this.tblStatus.Visible = false;
                this.tblView.Visible = false;
                this.tblViewFlack.Visible = false;
                this.tblWealth.Visible = false;
                this.tblOrder.Visible = false;
                this.tblConfer.Visible = false;
                this.tblMember.Visible = false;
                this.tblNotMember.Visible = false;
                this.btnJOK.Visible = false;
                this.trNotMember.Visible = false;
                this.trMember.Visible = false;
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_31.gif";
                this.btnJOK.ImageUrl = SessionItem.GetImageURL() + "button_31.gif";
                this.btnUpdate.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.btnPayOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.strType = SessionItem.GetRequest("TYPE", 1).ToString().Trim();
                this.intPerPage = 15;
                switch (this.strType)
                {
                    case "STATUS":
                        this.tblStatus.Visible = true;
                        this.GetStatus();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <font color='red'>用户状态</font> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "PAY":
                        this.tblPay.Visible = true;
                        this.GetPay();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <font color='red'>充值金币</font> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "VIEW":
                        this.tblView.Visible = true;
                        this.GetView();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <font color='red'>财务查看</font> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "FLACK":
                        this.tblFlack.Visible = true;
                        if (!base.IsPostBack)
                        {
                            this.tbFlackURL.Text = "http://";
                        }
                        this.GetFlack();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <font color='red'>宣传帖提交</font> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "VIEWFLACK":
                        this.tblViewFlack.Visible = true;
                        this.GetViewFlack();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <font color='red'>查看宣传帖</font> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "WEALTH":
                        this.tblWealth.Visible = true;
                        this.GetWealth();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a>| <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <font color='red'>论坛财富</font> ";
                        break;

                    case "ORDER":
                        this.tblOrder.Visible = true;
                        this.GetOrderList();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <font color='red'>帐单查询</font> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    case "CONFER":
                        this.tblConfer.Visible = true;
                        this.GetConferList();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <a href='AccountManage.aspx?Type=STATUS'>用户状态</a> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <font color='red'>金币赠与</font> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;

                    default:
                        this.tblStatus.Visible = true;
                        this.GetStatus();
                        this.strPageInfo = "<a href='Forum.aspx'>论坛首页</a> | <font color='red'>用户状态</font> | <a href='AccountManage.aspx?Type=PAY'>充值金币</a> | <a href='AccountManage.aspx?Type=VIEW'>财务查看</a> | <a href='AccountManage.aspx?Type=ORDER&Page=1'>帐单查询</a> | <a href='AccountManage.aspx?Type=FLACK'>宣传帖提交</a> | <a href='AccountManage.aspx?Type=VIEWFLACK'>查看宣传帖</a> | <a href='AccountManage.aspx?Type=CONFER'>金币赠与</a> | <a href='AccountManage.aspx?Type=WEALTH&Page=1'>论坛财富</a>";
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


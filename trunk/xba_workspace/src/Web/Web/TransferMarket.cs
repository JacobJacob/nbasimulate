namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TransferMarket : Page
    {
        public int intCapital;
        private int intCategory;
        private int intOrder;
        private int intPage;
        private int intPerPage;
        private int intPos;
        private int intTotal;
        private int intUserID;
        public long longCapital;
        public StringBuilder PageIntro1sb;
        public StringBuilder PageIntrosb;
        public StringBuilder sb;
        public StringBuilder Scriptsb = new StringBuilder();
        public string strCapital;
        public string strHref = "";
        public string strIsStreet;
        private string strNickName;
        public string strPageIntro1Height = "Height='25'";
        public string strSayScript;
        private string strType;

        private void DevChooseList()
        {
            string str;
            string str3;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            long price;
            DateTime time;
            long num10;
            bool flag;
            DataRow row;
            string str4 = "";
            this.intPerPage = 9;
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='100' align='left' style='padding-left:3px'>姓名</td>");
            this.sb.Append("<td width='40'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 3, "年龄") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 1, "身高") + "</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 2, "体重") + "</td>");
            this.sb.Append("<td width='41'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 4, "综合") + "</td>");
            this.sb.Append("<td width='41'>次数</td>");
            this.sb.Append("<td width='116'>买主</td>");
            this.sb.Append("<td width='73'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 0, "剩余时间") + "</td>");
            this.sb.Append("<td width='74'>操作</td>");
            this.sb.Append("</tr>");
            string strCurrentURL = string.Concat(new object[] { "TransferMarket.aspx?Type=", this.strType, "&Pos=", this.intPos, "&" });
            string zoneCode = MarketZone.GetZoneCode(BTPDevManager.GetDevCodeByUserID(this.intUserID));
            this.GetMsgScript(strCurrentURL);
            this.intTotal = BTPTransferManager.GetDevChooseCountNew(this.intUserID, this.intPos, zoneCode);
            SqlDataReader reader = BTPTransferManager.GetDevChooseListNew(this.intUserID, this.intPos, this.intOrder, zoneCode, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_06DA;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='11' class='BarContent'>暂时没有选秀中的球员！</td></tr>");
            goto Label_06E6;
        Label_03F7:
            row = BTPPlayer5Manager.GetPlayerRowByPlayerID(num10);
            if (row != null)
            {
                int intUserID = (int) row["BidderID"];
                price = Convert.ToInt64(row["BidPrice"]);
                if (intUserID != 0)
                {
                    string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                    str3 = string.Concat(new object[] { "<a title='选秀顺位' style='CURSOR:hand'>", price, "</a><br>", AccountItem.GetNickNameInfo(intUserID, nickNameByUserID, "Right") });
                }
                else
                {
                    str3 = "<a title='选秀顺位' style='CURSOR:hand'>" + price + "</a><br>--";
                }
            }
            else
            {
                price = Convert.ToInt64(reader["BidPrice"]);
                str3 = "<a title='选秀顺位' style='CURSOR:hand'>" + price + "</a><br>--";
            }
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='34' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(num10, str, 5, 0, 0) + "</td>");
            if (flag)
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num, "!</td>" }));
            }
            else
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "' style='CURSOR: hand'>", num, "</td>" }));
            }
            this.sb.Append("<td>" + num2 + "</td>");
            this.sb.Append("<td>" + num3 + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(num4) + "</td>");
            this.sb.Append("<td>" + num6 + "</td>");
            this.sb.Append("<td>" + str3 + "</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(time, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, time) + "</a></td>");
            this.sb.Append("<td>" + str4 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
        Label_06DA:
            if (reader.Read())
            {
                str = reader["Name"].ToString().Trim();
                num = Convert.ToInt32(reader["Age"]);
                Convert.ToInt32(reader["Pos"]);
                num2 = Convert.ToInt32(reader["Height"]);
                num3 = Convert.ToInt32(reader["Weight"]);
                num4 = 999;//(int) reader["Ability"];
                time = (DateTime) reader["EndBidTime"];
                num10 = (long) reader["PlayerID"];
                float single1 = ((float) num4) / 10f;
                num5 = Convert.ToInt32(reader["PlayedYear"]);
                num6 = (int) reader["BidCount"];
                int num9 = (byte) reader["BidStatus"];
                flag = (bool) reader["IsRetire"];
                int clubID = Convert.ToInt32(reader["ClubID"]);
                int bidderID = Convert.ToInt32(reader["BidderID"]);
                if (DateTime.Now >= time)
                {
                    switch (num9)
                    {
                        case 0:
                            str4 = "结算中";
                            goto Label_03F7;

                        case 1:
                            str4 = "<a href='BidDetail.aspx?Type=5&PlayerID=" + num10 + "' target='Right'>成交详情</a>";
                            goto Label_03F7;
                    }
                    str4 = "无成交";
                }
                else
                {
                    if (clubID > 0)
                    {
                        str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVCHOOSE&PlayerID=", num10, "&Market=5'><img src='", SessionItem.GetImageURL(), "xuanxiu.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=5&Status=5&PlayerID=", num10, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                    }
                    else
                    {
                        //if (bidderID > 0)
                        //{
                        //    str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVCHOOSE&PlayerID=", num10, "&Market=5'><img src='", SessionItem.GetImageURL(), "xuanxiu.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=5&Status=5&PlayerID=", num10, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                        //}
                        //else
                        //{
                            str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=SETTRIAL&PlayerID=", num10, "&Market=5'>试训</a><a href='SecretaryPage.aspx?Type=DEVCHOOSE&PlayerID=", num10, "&Market=5'><img src='", SessionItem.GetImageURL(), "xuanxiu.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=5&Status=5&PlayerID=", num10, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                        //}
                    }
                   
                }
                goto Label_03F7;
            }
        Label_06E6:
            reader.Close();
            string msgViewPage = this.GetMsgViewPage(strCurrentURL);
            this.sb.Append("<tr><td align='left' colspan='4' style=\"padding-left:10px\">" + this.strHref + "</td><td height='25' align='right' colspan='5'>" + msgViewPage + "</td></tr></table>");
            this.sb.Append("<div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/536023.js\"></script></div>");
        }

        private void DevStreetList()
        {
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            DateTime time;
            long num8;
            bool flag;
            string str2 = "";
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='100'>姓名</td>");
            this.sb.Append("<td width='40'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 3, "年龄") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 1, "身高") + "</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 2, "体重") + "</td>");
            this.sb.Append("<td width='41'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 4, "综合") + "</td>");
            this.sb.Append("<td width='41'>次数</td>");
            this.sb.Append("<td width='57'>买主</td>");
            this.sb.Append("<td width='132'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 0, "剩余时间") + "</td>");
            this.sb.Append("<td width='74'>操作</td>");
            this.sb.Append("</tr>");
            string strCurrentURL = string.Concat(new object[] { "TransferMarket.aspx?Type=", this.strType, "&Pos=", this.intPos, "&" });
            this.intTotal = BTPTransferManager.GetDevStreetCountNew(this.intPos);
            this.GetMsgScript(strCurrentURL);
            SqlDataReader reader = BTPTransferManager.GetDevStreetNew(this.intPos, this.intOrder, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_05D5;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='11' class='BarContent'>暂时没有拍卖中的球员！</td></tr>");
            goto Label_05E1;
        Label_03C7:
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='25' align='left' style='padding-left:10px'>" + PlayerItem.GetPlayerNameInfo(num8, str, 3, 0, 0) + "</td>");
            if (flag)
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num, "!</a></td>" }));
            }
            else
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "' style='CURSOR: hand'>", num, "</a></td>" }));
            }
            this.sb.Append("<td>" + num2 + "</td>");
            this.sb.Append("<td>" + num3 + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(num4) + "</td>");
            this.sb.Append("<td>" + num6 + "</td>");
            this.sb.Append("<td>隐藏</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(time, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, time) + "</a></td>");
            this.sb.Append("<td>" + str2 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
        Label_05D5:
            if (reader.Read())
            {
                str = reader["Name"].ToString().Trim();
                num = Convert.ToInt32(reader["Age"]);
                Convert.ToInt32(reader["Pos"]);
                num2 = Convert.ToInt32(reader["Height"]);
                num3 = Convert.ToInt32(reader["Weight"]);
                num4 = (int) reader["Ability"];
                time = (DateTime) reader["EndBidTime"];
                num8 = (long) reader["PlayerID"];
                float single1 = ((float) num4) / 10f;
                num5 = Convert.ToInt32(reader["PlayedYear"]);
                num6 = (int) reader["BidCount"];
                int num7 = (byte) reader["BidStatus"];
                flag = (bool) reader["IsRetire"];
                if (DateTime.Now >= time)
                {
                    switch (num7)
                    {
                        case 0:
                            str2 = "结算中";
                            goto Label_03C7;

                        case 1:
                            str2 = "<a href='BidDetail.aspx?Type=3&PlayerID=" + num8 + "' target='Right'>成交详情</a>";
                            goto Label_03C7;
                    }
                    str2 = "无成交";
                }
                else
                {
                    str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVSTREET&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=3&Status=1&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                }
                goto Label_03C7;
            }
        Label_05E1:
            reader.Close();
            string msgViewPage = this.GetMsgViewPage(strCurrentURL);
            this.sb.Append("<tr><td align='left'colspan='4' style=\"padding-left:10px\">" + this.strHref + "</td><td height='25' align='right' colspan='5'>" + msgViewPage + "</td></tr></table>");
            this.sb.Append("<div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/535981.js\"></script></div>");
        }

        private void GetMsgScript(string strCurrentURL)
        {
            strCurrentURL = strCurrentURL + "Order=" + this.intOrder;
            this.Scriptsb.Append("<script language=\"javascript\">");
            this.Scriptsb.Append("function JumpPage()");
            this.Scriptsb.Append("{");
            this.Scriptsb.Append("var strPage=document.all.Page.value;");
            this.Scriptsb.Append("window.location=\"" + strCurrentURL + "&Page=\"+strPage;");
            this.Scriptsb.Append("}");
            this.Scriptsb.Append("</script>");
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            object[] objArray;
            int num = (this.intTotal / this.intPerPage) + 1;
            if ((this.intTotal % this.intPerPage) == 0)
            {
                num--;
            }
            if (num == 0)
            {
                num = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                objArray = new object[7];
                objArray[0] = "<a href='";
                objArray[1] = strCurrentURL;
                objArray[2] = "Order=";
                objArray[3] = this.intOrder;
                objArray[4] = "&Page=";
                int num3 = this.intPage - 1;
                objArray[5] = num3.ToString();
                objArray[6] = "'>上一页</a>";
                str2 = string.Concat(objArray);
            }
            string str3 = "";
            if (this.intPage == num)
            {
                str3 = "下一页";
            }
            else
            {
                objArray = new object[] { "<a href='", strCurrentURL, "Order=", this.intOrder, "&Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(objArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num; i++)
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
            if (this.intOrder == 0)
            {
                this.strHref = "<a href='" + strCurrentURL + "Order=4&Page=1' >按照综合能力排序</a>";
            }
            else
            {
                this.strHref = "<a href='" + strCurrentURL + "Order=0&Page=1' >按照截止时间排序</a>";
            }
            return string.Concat(new object[] { str2, " ", str3, " 共", this.intTotal, "个记录 跳转", str4 });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strNickName = onlineRowByUserID["NickName"].ToString();
            this.intCategory = (int) onlineRowByUserID["Category"];
            if ((this.intCategory == 1) || (this.intCategory == 2))
            {
                this.strIsStreet = "Tommyabc";
            }
            else
            {
                this.strIsStreet = "Tommydef";
            }
            this.strType = (string) SessionItem.GetRequest("Type", 1);
            this.intPos = (int) SessionItem.GetRequest("Pos", 0);
            if ((this.intPos > 5) || (this.intPos < 1))
            {
                this.intPos = 1;
            }
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intOrder = (int) SessionItem.GetRequest("Order", 0);
            this.intPerPage = 12;
            StringBuilder builder = new StringBuilder();
            if (this.intCategory == 5)
            {
                builder.Append(string.Concat(new object[] { "<li class='qian2a'><a title='购买其他球队卖出的球员，直接进入职业队，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=TRANSFER\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=TRANSFER&Pos=", this.intPos, "&Order=4&Page=1'>职业转会</a></li>" }));
                builder.Append(string.Concat(new object[] { "<li class='qian2a'><a title='拥有选秀卡就可在这里获得球员，选秀卡轮次靠前者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=DEVCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=DEVCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>职业选秀</a></li>" }));
                builder.Append(string.Concat(new object[] { "<li class='qian2a'><a title='较好的球员，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=UTMOST\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=UTMOST&Pos=", this.intPos, "&Order=4&Page=1'>极限球员</a></li>" }));
            }
            else
            {
                builder.Append("<li class='qian2a'><font color='#aaaaaa'>职业转会</font></li>");
                builder.Append("<li class='qian2a'><font color='#aaaaaa'>职业选秀</font></li>");
                builder.Append("<li class='qian2a'><font color='#aaaaaa'>极限球员</font></li>");
            }
            this.PageIntrosb = new StringBuilder();
            switch (this.strType)
            {
                case "STREETFREE":
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='MyFocus.aspx?Tommy=" + this.strIsStreet + "'>关注球员</a></li>");
                    this.PageIntrosb.Append("<li class='qian2'>街头自由</li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>街头选秀</a></li>" }));
                    this.PageIntrosb.Append(builder.ToString());

                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=", this.intPos, "&Order=4&Page=1'>街头极限</a></li>" }));
               
                    
                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    if (this.intCategory == 5)
                    {
                        this.DevStreetList();
                    }
                    else
                    {
                        this.StreetFreeList();
                    }
                    break;

                case "STREETUTMOST":
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='MyFocus.aspx?Tommy=" + this.strIsStreet + "'>关注球员</a></li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETFREE&Pos=", this.intPos, "&Order=4&Page=1'>街头自由</a>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>街头选秀</a></li>" }));
                    this.PageIntrosb.Append(builder.ToString());

                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2'>街头极限</li>" }));
               

                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.StreetFreeList();
                    break;


                case "STREETCHOOSE":
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=MYFOCUSPLAYER&Pos=0&Order=4&Page=1'>关注球员</a></li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETFREE&Pos=", this.intPos, "&Order=4&Page=1'>街头自由</a>" }));
                    this.PageIntrosb.Append("<li class='qian2'>街头选秀</li>");
                    this.PageIntrosb.Append(builder.ToString());
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=", this.intPos, "&Order=4&Page=1'>街头极限</a></li>" }));
               
                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.StreetChooseList();
                    break;

                case "TRANSFER":
                    if (this.intCategory != 5)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=90");
                        return;
                    }
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=MYFOCUSPLAYER&Pos=0&Order=4&Page=1'>关注球员</a></li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETFREE&Pos=", this.intPos, "&Order=4&Page=1'>街头自由</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>街头选秀</a></li>" }));
                    this.PageIntrosb.Append("<li class='qian2'>职业转会</li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='拥有选秀卡就可在这里获得球员，选秀卡轮次靠前者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=DEVCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=DEVCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>职业选秀</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=", this.intPos, "&Order=4&Page=1'>街头极限</a></li>" }));
               
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='较好的球员，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=UTMOST\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=UTMOST&Pos=", this.intPos, "&Order=4&Page=1'>极限球员</a></li>" }));
                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.TransferList();
                    break;

                case "DEVCHOOSE":
                    if (this.intCategory != 5)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=90");
                        return;
                    }
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=MYFOCUSPLAYER&Pos=0&Order=4&Page=1'>关注球员</a></li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETFREE&Pos=", this.intPos, "&Order=4&Page=1'>街头自由</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>街头选秀</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='购买其他球队卖出的球员，直接进入职业队，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=TRANSFER\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=TRANSFER&Pos=", this.intPos, "&Order=4&Page=1'>职业转会</a></li>" }));
                    this.PageIntrosb.Append("<li class='qian2'>职业选秀</li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='较好的球员，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=UTMOST\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=UTMOST&Pos=", this.intPos, "&Order=4&Page=1'>极限球员</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=", this.intPos, "&Order=4&Page=1'>街头极限</a></li>" }));
                
                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.DevChooseList();
                    break;

                case "UTMOST":
                    if (this.intCategory != 5)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=90");
                        return;
                    }
                    this.PageIntrosb.Append("<ul><li class='qian1a'><a title='您所拍下的或者关注的球员都会在这里显示，方便您的监控。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=FOCUS\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=MYFOCUSPLAYER&Pos=0&Order=4&Page=1'>关注球员</a></li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETFREE&Pos=", this.intPos, "&Order=4&Page=1'>街头自由</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>街头选秀</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='购买其他球队卖出的球员，直接进入职业队，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=TRANSFER\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=TRANSFER&Pos=", this.intPos, "&Order=4&Page=1'>职业转会</a></li>" }));
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='拥有选秀卡就可在这里获得球员，选秀卡轮次靠前者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=DEVCHOOSE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=DEVCHOOSE&Pos=", this.intPos, "&Order=4&Page=1'>职业选秀</a></li>" }));
                    this.PageIntrosb.Append("<li class='qian2'>极限球员</li>");
                    this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=", this.intPos, "&Order=4&Page=1'>街头极限</a></li>" }));
               
                    //this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.TransferList();
                    break;

                default:
                    base.Response.Redirect("MyFocus.aspx?Tommy=" + this.strIsStreet);
                    break;
            }
            this.PageIntro1sb = new StringBuilder();
            if (this.strType != "MYFOCUSPLAYER")
            {
                switch (this.intPos)
                {
                    case 1:
                        this.PageIntro1sb.Append("<font color='#FF0000'>中锋 C</font>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=2&Order=4&Page=1'>大前锋 PF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=3&Order=4&Page=1'>小前锋 SF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=4&Order=4&Page=1'>得分后卫 SG</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=5&Order=4&Page=1'>组织后卫 PG</a>");
                        goto Label_10B0;

                    case 2:
                        this.PageIntro1sb.Append("<a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=1&Order=4&Page=1'>中锋 C</a>");
                        this.PageIntro1sb.Append(" | <font color='#FF0000'>大前锋 PF</font>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=3&Order=4&Page=1'>小前锋 SF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=4&Order=4&Page=1'>得分后卫 SG</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=5&Order=4&Page=1'>组织后卫 PG</a>");
                        goto Label_10B0;

                    case 3:
                        this.PageIntro1sb.Append("<a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=1&Order=4&Page=1'>中锋 C</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=2&Order=4&Page=1'>大前锋 PF</a>");
                        this.PageIntro1sb.Append(" | <font color='#FF0000'>小前锋 SF</font>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=4&Order=4&Page=1'>得分后卫 SG</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=5&Order=4&Page=1'>组织后卫 PG</a>");
                        goto Label_10B0;

                    case 4:
                        this.PageIntro1sb.Append("<a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=1&Order=4&Page=1'>中锋 C</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=2&Order=4&Page=1'>大前锋 PF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=3&Order=4&Page=1'>小前锋 SF</a>");
                        this.PageIntro1sb.Append(" | <font color='#FF0000'>得分后卫 SG</font>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=5&Order=4&Page=1'>组织后卫 PG</a>");
                        goto Label_10B0;

                    case 5:
                        this.PageIntro1sb.Append("<a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=1&Order=4&Page=1'>中锋 C</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=2&Order=4&Page=1'>大前锋 PF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=3&Order=4&Page=1'>小前锋 SF</a>");
                        this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=4&Order=4&Page=1'>得分后卫 SG</a>");
                        this.PageIntro1sb.Append(" | <font color='#FF0000'>组织后卫 PG</font>");
                        goto Label_10B0;
                }
                this.PageIntro1sb.Append("<font color='#FF0000'>中锋 C</font>");
                this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=2&Order=4&Page=1'>大前锋 PF</a>");
                this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=3&Order=4&Page=1'>小前锋 SF</a>");
                this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=4&Order=4&Page=1'>得分后卫 SG</a>");
                this.PageIntro1sb.Append(" | <a href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=" + this.strType + "&Pos=5&Order=4&Page=1'>组织后卫 PG</a>");
            }
        Label_10B0:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (this.strType == "TRANSFER")
            {
                this.strCapital = "";
            }
            else
            {
                this.strCapital = "";
            }
        }

        private void StreetChooseList()
        {
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            DateTime time;
            long num8;
            bool flag;
            string str2 = "";
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='100' align='left' style='padding-left:10px'>姓名</td>");
            this.sb.Append("<td width='40'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 3, "年龄") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 1, "身高") + "</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 2, "体重") + "</td>");
            this.sb.Append("<td width='41'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 4, "综合") + "</td>");
            this.sb.Append("<td width='31'>次数</td>");
            this.sb.Append("<td width='57'>买主</td>");
            this.sb.Append("<td width='100'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 0, "剩余时间") + "</td>");
            this.sb.Append("<td width='116'>操作</td>");
            this.sb.Append("</tr>");
            string strCurrentURL = string.Concat(new object[] { "TransferMarket.aspx?Type=", this.strType, "&Pos=", this.intPos, "&" });
            this.intTotal = BTPTransferManager.GetStreetChooseCountNew(this.intPos);
            this.GetMsgScript(strCurrentURL);
            SqlDataReader reader = BTPTransferManager.GetTranStreetChooseNew(this.intPos, this.intOrder, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_0605;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='11' class='BarContent'>暂时没有拍卖中的球员！</td></tr>");
            goto Label_0611;
        Label_03F7:
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='25' align='left' style='padding-left:10px'>" + PlayerItem.GetPlayerNameInfo(num8, str, 3, 1, 0) + "</td>");
            if (flag)
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num, "!</td>" }));
            }
            else
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "' style='CURSOR: hand'>", num, "</td>" }));
            }
            this.sb.Append("<td>" + num2 + "</td>");
            this.sb.Append("<td>" + num3 + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(num4) + "</td>");
            this.sb.Append("<td>" + num6 + "</td>");
            this.sb.Append("<td>隐藏</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(time, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, time) + "</a></td>");
            this.sb.Append("<td>" + str2 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
        Label_0605:
            if (reader.Read())
            {
                str = reader["Name"].ToString().Trim();
                num = Convert.ToInt32(reader["Age"]);
                Convert.ToInt32(reader["Pos"]);
                num2 = Convert.ToInt32(reader["Height"]);
                num3 = Convert.ToInt32(reader["Weight"]);
                num4 = (int) reader["Ability"];
                time = (DateTime) reader["EndBidTime"];
                num8 = (long) reader["PlayerID"];
                float single1 = ((float) num4) / 10f;
                num5 = Convert.ToInt32(reader["PlayedYear"]);
                num6 = (int) reader["BidCount"];
                int num7 = (byte) reader["BidStatus"];
                flag = (bool) reader["IsRetire"];
                if (DateTime.Now >= time)
                {
                    switch (num7)
                    {
                        case 0:
                            str2 = "结算中";
                            goto Label_03F7;

                        case 1:
                            str2 = "<a href='BidDetail.aspx?Type=3&PlayerID=" + num8 + "' target='Right'>成交详情</a>";
                            goto Label_03F7;
                    }
                    str2 = "无成交";
                }
                else
                {
                    str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=STREETCHOOSE&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=QUICKBUY&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "yikoujia.gif' border='0' width='33' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=3&Status=2&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                    //str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=STREETCHOOSE&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=3&Status=2&PlayerID=", num8, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                }
                goto Label_03F7;
            }
        Label_0611:
            reader.Close();
            string msgViewPage = this.GetMsgViewPage(strCurrentURL);
            this.sb.Append("<tr><td align='left' colspan='5' style=\"padding-left:10px\">" + this.strHref + "</td><td height='25' align='right' colspan='4'>" + msgViewPage + "</td></tr></table>");
            this.sb.Append("<div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/535987.js\"></script></div>");
        }

        private void StreetFreeList()
        {
            string str;
            int num;
            int num2;
            int num3;
            int num4;
            DateTime time;
            long num5;
            int num6;
            int num7;
            bool flag;
            string str2 = "";
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='100' align='left' style='padding-left:10px'>姓名</td>");
            this.sb.Append("<td width='40'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 3, "年龄") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 1, "身高") + "</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 2, "体重") + "</td>");
            this.sb.Append("<td width='41'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 4, "综合") + "</td>");
            this.sb.Append("<td width='41'>次数</td>");
            this.sb.Append("<td width='57'>买主</td>");
            this.sb.Append("<td width='132'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 0, "剩余时间") + "</td>");
            this.sb.Append("<td width='74'>操作</td>");
            this.sb.Append("</tr>");
            string strCurrentURL = string.Concat(new object[] { "TransferMarket.aspx?Type=", this.strType, "&Pos=", this.intPos, "&" });
            this.intTotal = BTPTransferManager.GetStreetFreeCountNew(this.intPos);
            this.GetMsgScript(strCurrentURL);
            SqlDataReader reader = BTPTransferManager.GetTranStreetFreeNew(this.intPos, this.intOrder, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_05D5;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='11' class='BarContent'>暂时没有拍卖中的球员！</td></tr>");
            goto Label_05E1;
        Label_03C7:
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='25' align='left' style='padding-left:10px'>" + PlayerItem.GetPlayerNameInfo(num5, str, 3, 0, 0) + "</td>");
            if (flag)
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num6, "</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num, "!</a></td>" }));
            }
            else
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num6, "' style='CURSOR: hand'>", num, "</a></td>" }));
            }
            this.sb.Append("<td>" + num2 + "</td>");
            this.sb.Append("<td>" + num3 + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(num4) + "</td>");
            this.sb.Append("<td>" + num7 + "</td>");
            this.sb.Append("<td>隐藏</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(time, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, time) + "</a></td>");
            this.sb.Append("<td>" + str2 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>");
        Label_05D5:
            if (reader.Read())
            {
                str = reader["Name"].ToString().Trim();
                num = Convert.ToInt32(reader["Age"]);
                Convert.ToInt32(reader["Pos"]);
                num2 = Convert.ToInt32(reader["Height"]);
                num3 = Convert.ToInt32(reader["Weight"]);
                num4 = (int) reader["Ability"];
                time = (DateTime) reader["EndBidTime"];
                num5 = (long) reader["PlayerID"];
                float single1 = ((float) num4) / 10f;
                num6 = Convert.ToInt32(reader["PlayedYear"]);
                num7 = (int) reader["BidCount"];
                int num8 = (byte) reader["BidStatus"];
                flag = (bool) reader["IsRetire"];
                if (DateTime.Now >= time)
                {
                    switch (num8)
                    {
                        case 0:
                            str2 = "结算中";
                            goto Label_03C7;

                        case 1:
                            str2 = "<a href='BidDetail.aspx?Type=3&PlayerID=" + num5 + "' target='Right'>成交详情</a>";
                            goto Label_03C7;
                    }
                    str2 = "无成交";
                }
                else
                {
                    //str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=STREETFREE&PlayerID=", num5, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=3&Status=1&PlayerID=", num5, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                    str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVSTREET&PlayerID=", num5, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=3&Status=1&PlayerID=", num5, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                
                }
                goto Label_03C7;
            }
        Label_05E1:
            reader.Close();
            string msgViewPage = this.GetMsgViewPage(strCurrentURL);
            this.sb.Append("<tr><td align='left' colspan='4' style=\"padding-left:10px\">" + this.strHref + "</td><td height='25' align='right' colspan='5'>" + msgViewPage + "</td></tr></table>");
            this.sb.Append("<div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/535981.js\"></script></div>");
        }

        private void TransferList()
        {
            string str;
            string str3;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            long price;
            DateTime time;
            bool flag;
            long num10;
            SqlDataReader reader;
            bool flag2;
            SqlDataReader reader2;
            string str4 = "";
            this.intPerPage = 9;
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='35'></td>");
            this.sb.Append("<td width='100' align='left' style='padding-left:3px'>姓名</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 3, "年龄") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 1, "身高") + "</td>");
            this.sb.Append("<td width='33'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 2, "体重") + "</td>");
            this.sb.Append("<td width='34'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 4, "综合") + "</td>");
            this.sb.Append("<td width='34'>次数</td>");
            this.sb.Append("<td width='116'>买主</td>");
            this.sb.Append("<td width='66'>" + PlayerItem.GetOrderInfo(this.strType, this.intPos, 0, "剩余时间") + "</td>");
            this.sb.Append("<td width='67'>操作</td>");
            this.sb.Append("</tr>");
            string strCurrentURL = string.Concat(new object[] { "TransferMarket.aspx?Type=", this.strType, "&Pos=", this.intPos, "&" });
            string zoneCode = MarketZone.GetZoneCode(BTPDevManager.GetDevCodeByUserID(this.intUserID));
            this.GetMsgScript(strCurrentURL);
            if (this.strType == "TRANSFER")
            {
                this.intTotal = BTPTransferManager.GetTransferCountNew(this.intPos, zoneCode);
                reader = BTPTransferManager.GetTransferNew(this.intPos, this.intOrder, zoneCode, this.intPage, this.intPerPage);
            }
            else if (this.strType == "UTMOST")
            {
                this.intTotal = BTPTransferManager.GetUtmostCountNew(this.intPos);
                reader = BTPTransferManager.GetUtmostListNew(this.intPos, this.intOrder, this.intPage, this.intPerPage);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            if (reader.HasRows)
            {
                goto Label_0844;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='11' class='BarContent'>暂时没有拍卖中的球员！</td></tr>");
            goto Label_0850;
        Label_050C:
            reader2 = BTPTransferManager.GetDevTranTopUser(num10);
            if (reader2.Read())
            {
                int intUserID = (int) reader2["UserID"];
                price = Convert.ToInt64(reader2["Price"]);
                string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                str3 = price + "<br>" + AccountItem.GetNickNameInfo(intUserID, nickNameByUserID, "Right");
            }
            else
            {
                price = Convert.ToInt64(reader["BidPrice"]);
                str3 = price + "<br>--";
            }
            reader2.Close();
            string strDevCode = BTPPlayer5Manager.GetPlayerRowByPlayerID(num10)["MarketCode"].ToString();
            if (DevCalculator.GetLevel(strDevCode) < 9)
            {
                if (strDevCode.Trim() == "AAA")
                {
                    strDevCode = "--";
                }
                else
                {
                    strDevCode = "1-8";
                }
            }
            else
            {
                strDevCode = "9-11";
            }
            if (flag)
            {
                strDevCode = "<font color=\"red\">" + strDevCode + "</font>";
            }
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='34'>" + strDevCode + "</td>");
            this.sb.Append("<td height='34' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(num10, str, 5, 0, 0, flag) + "</td>");
            if (flag2)
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num, "!</td>" }));
            }
            else
            {
                this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num5, "' style='CURSOR: hand'>", num, "</td>" }));
            }
            this.sb.Append("<td>" + num2 + "</td>");
            this.sb.Append("<td>" + num3 + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(num4) + "</td>");
            this.sb.Append("<td>" + num6 + "</td>");
            this.sb.Append("<td>" + str3 + "</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(time, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, time) + "</a></td>");
            this.sb.Append("<td>" + str4 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='10'></td></tr>");
        Label_0844:
            if (reader.Read())
            {
                str = reader["Name"].ToString().Trim();
                num = Convert.ToInt32(reader["Age"]);
                Convert.ToInt32(reader["Pos"]);
                num2 = Convert.ToInt32(reader["Height"]);
                num3 = Convert.ToInt32(reader["Weight"]);
                num4 = (int) reader["Ability"];
                time = (DateTime) reader["EndBidTime"];
                num10 = (long) reader["PlayerID"];
                float single1 = ((float) num4) / 10f;
                num5 = Convert.ToInt32(reader["PlayedYear"]);
                num6 = (int) reader["BidCount"];
                int num9 = (byte) reader["BidStatus"];
                flag = (bool) reader["SellAss"];
                flag2 = (bool) reader["IsRetire"];
                if (DateTime.Now >= time)
                {
                    switch (num9)
                    {
                        case 0:
                            str4 = "结算中";
                            goto Label_050C;

                        case 1:
                            str4 = "<a href='BidDetail.aspx?Type=5&PlayerID=" + num10 + "' target='Right'>成交详情</a>";
                            goto Label_050C;
                    }
                    str4 = "无成交";
                }
                else if (this.strType == "TRANSFER")
                {
                    str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVISION&PlayerID=", num10, "&Market=4'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=5&Status=3&PlayerID=", num10, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                }
                else if (this.strType == "UTMOST")
                {
                    str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=DEVISION&PlayerID=", num10, "&Market=6'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=SETFOCUS&Category=5&Status=4&PlayerID=", num10, "'><img src='", SessionItem.GetImageURL(), "gz.gif' border='0' width='24' height='9'></a>" });
                }
                goto Label_050C;
            }
        Label_0850:
            reader.Close();
            string msgViewPage = this.GetMsgViewPage(strCurrentURL);
            this.sb.Append("<tr><td align='left' colspan='5' style=\"padding-left:10px\">" + this.strHref + "</td><td height='25' align='right' colspan='5'>" + msgViewPage + "</td></tr></table>");
            this.sb.Append("<div id=\"divCount\" style=\"DISPLAY: none\"><script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/535997.js\"></script></div>");
        }
    }
}


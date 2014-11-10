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

    public class MyFocus : Page
    {
        private int intCategory;
        private int intUserID;
        public StringBuilder PageIntrosb;
        public StringBuilder sb;
        private string strIsStreet;
        private string strNickName;
        public string strSalaryTop;
        public string strSayScript;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MyFocusPlayer()
        {
            DataRow playerRowByPlayerID;
            int num;
            string str;
            string str2;
            long num3;
            int num4;
            bool flag;
            int num2 = 0;
            string str3 = "";
            string str4 = "";
            string str5 = "<img src='" + SessionItem.GetImageURL() + "chujia.gif' border='0' width='24' height='9'>";
            this.sb = new StringBuilder();
            this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sb.Append("<tr align='center' class='BarHead'>");
            this.sb.Append("<td height='25' width='96' align='left' style='padding-left:3px'>姓名</td>");
            this.sb.Append("<td width='33'>年龄</td>");
            this.sb.Append("<td width='25'>位</td>");
            this.sb.Append("<td width='45'>身体</td>");
            this.sb.Append("<td width='33'>市场</td>");
            this.sb.Append("<td width='28'>综合</td>");
            this.sb.Append("<td width='28'>次数</td>");
            this.sb.Append("<td width='116'>买主</td>");
            this.sb.Append("<td width='56'>剩余时间</td>");
            this.sb.Append("<td width='81'>操作</td>");
            this.sb.Append("</tr>");
            SqlDataReader focusTableByUserID = BTPBidFocusManager.GetFocusTableByUserID(this.intUserID);
            if (focusTableByUserID.HasRows)
            {
                goto Label_0764;
            }
            this.sb.Append("<tr align='center'><td height='25' colspan='10' class='BarContent'>暂时没有关注中的球员！</td></tr>");
            goto Label_091F;
        Label_013D:
            switch (num2)
            {
                case 1:
                    str5 = "<img src='" + SessionItem.GetImageURL() + "chujia.gif' border='0' width='24' height='9'>";
                    str = "<a style='cursor:hand;' title='街头自由球员'>街</a>";
                    num4 = 0;
                    break;

                case 2:
                    str5 = "<img src='" + SessionItem.GetImageURL() + "xuanxiu.gif' border='0' width='24' height='9'>";
                    str = "<a style='cursor:hand;' title='街球选秀球员'>秀</a>";
                    num4 = 1;
                    break;

                case 3:
                    str5 = "<img src='" + SessionItem.GetImageURL() + "chujia.gif' border='0' width='24' height='9'>";
                    str3 = "&Market=4";
                    str = "<a style='cursor:hand;' title='职业转会球员'>转</a>";
                    num4 = 0;
                    break;

                case 4:
                    str5 = "<img src='" + SessionItem.GetImageURL() + "chujia.gif' border='0' width='24' height='9'>";
                    str3 = "&Market=6";
                    str = "<a style='cursor:hand;' title='极限球员'>极</a>";
                    num4 = 0;
                    break;

                case 5:
                    str5 = "<img src='" + SessionItem.GetImageURL() + "xuanxiu.gif' border='0' width='24' height='9'>";
                    str = "<a style='cursor:hand;' title='职业选秀'>选</a>";
                    num4 = 0;
                    break;

                default:
                    str = "未知";
                    num4 = 0;
                    break;
            }
            if (num == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(num3);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(num3);
            }
            if (playerRowByPlayerID == null)
            {
                goto Label_0764;
            }
            string strName = playerRowByPlayerID["Name"].ToString().Trim();
            int num5 = (byte) playerRowByPlayerID["Age"];
            int intPosition = (byte) playerRowByPlayerID["Pos"];
            int num7 = (byte) playerRowByPlayerID["Height"];
            int num8 = (byte) playerRowByPlayerID["Weight"];
            int intAbility = (int) playerRowByPlayerID["Ability"];
            if (num2 == 5)
            {
                intAbility = 0x3e7;
            }
            DateTime datIn = (DateTime) playerRowByPlayerID["EndBidTime"];
            num3 = (long) playerRowByPlayerID["PlayerID"];
            float single1 = ((float) intAbility) / 10f;
            int num10 = (byte) playerRowByPlayerID["PlayedYear"];
            int num11 = (int) playerRowByPlayerID["BidCount"];
            int num12 = (byte) playerRowByPlayerID["BidStatus"];
            if (num == 5)
            {
                flag = (bool) playerRowByPlayerID["SellAss"];
            }
            else
            {
                flag = false;
            }
            if (DateTime.Now >= datIn)
            {
                switch (num12)
                {
                    case 0:
                        str4 = "结算中";
                        goto Label_0524;

                    case 1:
                        str4 = string.Concat(new object[] { "<a href='BidDetail.aspx?Type=", num, "&PlayerID=", num3, "' target='Right'>成交详情</a>" });
                        goto Label_0524;
                }
                str4 = "无成交";
            }
            else
            {
                string str7;
                if (num == 5)
                {
                    if (num2 == 5)
                    {
                        str7 = "DEVCHOOSE";
                    }
                    else
                    {
                        str7 = "DEVISION";
                    }
                }
                else if ((num == 3) && (num2 == 1))
                {
                    if (this.intCategory == 5)
                    {
                        str7 = "DEVSTREET";
                    }
                    else
                    {
                        str7 = "STREETFREE";
                    }
                }
                else if ((num == 3) && (num2 == 2))
                {
                    str7 = "STREETCHOOSE";
                }
                else
                {
                    str7 = "";
                }
                if (num2 != 2)
                {
                    str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=", str7, "&PlayerID=", num3, str3, "'>", str5, "</a><a href='SecretaryPage.aspx?Type=CANCELFOCUS&Category=", num, "&PlayerID=", num3, "' title='取消关注并不能撤销出价！'><img src='", SessionItem.GetImageURL(), "cancel.gif' border='0' width='24' height='9'></a>" });
                }
                else
                {
                    str4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=STREETCHOOSE&PlayerID=", num3, "'><img src='", SessionItem.GetImageURL(), "chujia.gif' border='0' width='24' height='9'></a><a href='SecretaryPage.aspx?Type=CANCELFOCUS&Category=", num, "&PlayerID=", num3, "' title='取消关注并不能撤销出价！'><img src='", SessionItem.GetImageURL(), "cancel.gif' border='0' width='24' height='9'></a>" });
                }
            }
        Label_0524:
            this.sb.Append("<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("<td height='34' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(num3, strName, num, num4, 0, flag) + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td><a title='球龄：", num10, "' style='CURSOR: hand'>", num5, "</td>" }));
            this.sb.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</td>");
            this.sb.Append(string.Concat(new object[] { "<td>", num7, "CM<br>", num8, "KG</td>" }));
            this.sb.Append("<td>" + str + "</td>");
            this.sb.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
            this.sb.Append("<td>" + num11 + "</td>");
            this.sb.Append("<td>" + str2 + "</td>");
            this.sb.Append("<td><a title='" + StringItem.FormatDate(datIn, "MM-dd hh:mm:ss") + "截止' style='CURSOR: hand'>" + PlayerItem.GetBidLeftTime(DateTime.Now, datIn) + "</a></td>");
            this.sb.Append("<td >" + str4 + "</td>");
            this.sb.Append("</tr>");
            this.sb.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='10'></td></tr>");
        Label_0764:
            if (focusTableByUserID.Read())
            {
                int num13;
                long num14;
                SqlDataReader devTranTopUser;
                str5 = "";
                num3 = (long) focusTableByUserID["PlayerID"];
                num = (byte) focusTableByUserID["Category"];
                num2 = Convert.ToInt32(focusTableByUserID["Status"]);
                switch (((byte) focusTableByUserID["Status"]))
                {
                    case 3:
                    case 4:
                        devTranTopUser = BTPTransferManager.GetDevTranTopUser(num3);
                        if (!devTranTopUser.Read())
                        {
                            str2 = "暂无出价";
                            break;
                        }
                        num13 = (int) devTranTopUser["UserID"];
                        num14 = Convert.ToInt64(devTranTopUser["Price"]);
                        this.strNickName = BTPAccountManager.GetNickNameByUserID(num13);
                        str2 = num14 + "<br>" + AccountItem.GetNickNameInfo(num13, this.strNickName, "Right");
                        break;

                    case 5:
                    {
                        DataRow row2 = BTPPlayer5Manager.GetPlayerRowByPlayerID(num3);
                        if (row2 == null)
                        {
                            str2 = "";
                        }
                        else
                        {
                            num13 = (int) row2["BidderID"];
                            num14 = Convert.ToInt64(row2["BidPrice"]);
                            if (num13 == 0)
                            {
                                str2 = "<a title='选秀顺位' style='CURSOR:hand'>" + num14 + "</a><br>--";
                            }
                            else
                            {
                                this.strNickName = BTPAccountManager.GetNickNameByUserID(num13);
                                str2 = string.Concat(new object[] { "<a title='选秀顺位' style='CURSOR:hand'>", num14, "</a><br>", AccountItem.GetNickNameInfo(num13, this.strNickName, "Right") });
                            }
                        }
                        goto Label_013D;
                    }
                    default:
                        str2 = "隐藏";
                        goto Label_013D;
                }
                devTranTopUser.Close();
                goto Label_013D;
            }
        Label_091F:
            focusTableByUserID.Close();
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
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
                this.strSalaryTop = "";
                StringBuilder builder = new StringBuilder();
                if (this.intCategory == 5)
                {
                    builder.Append("<li class='qian2a'><a title='购买其他球队卖出的球员，直接进入职业队，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=TRANSFER\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=TRANSFER&Pos=0&Order=4&Page=1'>职业转会</a></li>");
                    builder.Append("<li class='qian2a'><a title='拥有选秀卡就可在这里获得球员，选秀卡轮次靠前者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=DEVCHOOSE\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=DEVCHOOSE&Pos=0&Order=4&Page=1'>职业选秀</a></li>");
                    builder.Append("<li class='qian2a'><a title='较好的球员，价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=UTMOST\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=UTMOST&Pos=0&Order=4&Page=1'>极限球员</a></li>");
                }
                else
                {
                    builder.Append("<li class='qian2a'><font color='#aaaaaa'>职业转会</font></li>");
                    builder.Append("<li class='qian2a'><font color='#aaaaaa'>职业选秀</font></li>");
                    builder.Append("<li class='qian2a'><font color='#aaaaaa'>极限球员</font></li>");
                }
                this.PageIntrosb = new StringBuilder();
                this.PageIntrosb.Append("<ul><li class='qian1'>关注球员</li>");
                this.PageIntrosb.Append("<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=STREETFREE&Pos=0&Order=4&Page=1'>街头自由</a></li>");
                this.PageIntrosb.Append("<li class='qian2a'><a title='从街头选秀来的球员可以进入职业球队。价格适当获得球员。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETCHOOSE\"' href='TransferMarket.aspx?Tommy=" + this.strIsStreet + "&Type=STREETCHOOSE&Pos=0&Order=4&Page=1'>街头选秀</a></li>");
                this.PageIntrosb.Append(builder.ToString());
                this.PageIntrosb.Append(string.Concat(new object[] { "<li class='qian2a'><a title='从这里购买的球员可以进入街球队，年轻球员的培养潜力很大。价高者得。' onclick='javascript:window.top.Main.Right.location=\"Intro/TransferMarket.aspx?Type=STREETFREE\"' href='TransferMarket.aspx?Tommy=", this.strIsStreet, "&Type=STREETUTMOST&Pos=1&Order=4&Page=1'>街头极限</a>" }));
                this.PageIntrosb.Append("</ul><a style='cursor:hand;' onclick='javascript:window.location.reload();' title='刷新'><img src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a></li>");
                this.MyFocusPlayer();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


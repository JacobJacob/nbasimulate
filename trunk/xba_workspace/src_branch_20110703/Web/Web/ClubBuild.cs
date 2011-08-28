namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ClubBuild : Page
    {
        protected ImageButton btnOK;
        protected DropDownList ddlTicketPrice;
        public int intADSum;
        private int intCategory;
        private int intClubID5;
        private int intPayType;
        private int intStadiumID;
        private int intTicketPrice;
        private int intUserID;
        public string strDownDList;
        public string strDownList;
        public string strDownRightList;
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strSayScript;
        private string strType;
        protected HtmlTable tblAd;
        protected HtmlTable tblClub;
        protected HtmlTable tblStadium;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int intTicketPrice = Convert.ToInt32(this.ddlTicketPrice.SelectedValue);
            if ((intTicketPrice > 100) && (this.intPayType != 1))
            {
                base.Response.Redirect("Report.aspx?Parameter=127!Type.STADIUM");
            }
            else if (BTPStadiumManager.UpdateTicketPrice(this.intUserID, intTicketPrice) == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=123!Type.STADIUM");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void GetADLinkTable()
        {
            int num4 = 0x1f40;
            DataRow stadiumRowByUserID = BTPStadiumManager.GetStadiumRowByUserID(this.intUserID);
            if (stadiumRowByUserID == null)
            {
                this.strList = "<tr><td width='100%' height='140'></td></tr>";
            }
            else
            {
                string str4;
                this.intStadiumID = (int) stadiumRowByUserID["StadiumID"];
                int num6 = (byte) stadiumRowByUserID["Levels"];
                int num7 = (byte) stadiumRowByUserID["ADCount"];
                int num8 = (int) stadiumRowByUserID["Capacity"];
                this.intTicketPrice = (byte) stadiumRowByUserID["TicketPrice"];
                int num9 = (int) stadiumRowByUserID["FansR"];
                int num5 = (int) stadiumRowByUserID["FansT"];
                num4 = num9 + num5;
                int num10 = (byte) stadiumRowByUserID["TurnLeft"];
                switch (num6)
                {
                    case 1:
                        str4 = "1.gif";
                        if (num10 != 0)
                        {
                            str4 = "1-2.gif";
                        }
                        break;

                    case 2:
                        str4 = "2134pica27xxas.gif";
                        if (num10 != 0)
                        {
                            str4 = "2134pica27xxas2-3p.gif";
                        }
                        break;

                    case 3:
                        str4 = "3548picba38zdgg.gif";
                        if (num10 != 0)
                        {
                            str4 = "3548picba38zdgg3-4l.gif";
                        }
                        break;

                    case 4:
                        str4 = "4685piccba44shfa.gif";
                        if (num10 != 0)
                        {
                            str4 = "4685piccba44shfa4-5a.gif";
                        }
                        break;

                    case 5:
                        str4 = "5597picdcb55mhhs.gif";
                        if (num10 != 0)
                        {
                            str4 = "5597picdcb55mhhs5-6c.gif";
                        }
                        break;

                    case 6:
                        str4 = "6125picedc65jryu.gif";
                        if (num10 != 0)
                        {
                            str4 = "6125picedc65jryu6-7f.gif";
                        }
                        break;

                    case 7:
                        str4 = "7419picfed793lkwep.gif";
                        break;

                    default:
                        str4 = "1.gif";
                        if (num10 != 0)
                        {
                            str4 = "1-2.gif";
                        }
                        break;
                }
                this.strList = string.Concat(new object[] { "<tr><td width='30%' height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:25px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>体育馆</strong>：", num6, " 级<br><strong>座　位</strong>：", num8, " 个<br><strong>广告栏</strong>：", num7, " 个<br><strong>球  迷</strong>：", num4, " 人<br></td><td width='70%' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str4, "' width='350' height='130'></td></tr>" });
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.ddlTicketPrice.SelectedValue = this.intTicketPrice.ToString();
            }
            DataTable aDTable = BTPADManager.GetADTable();
            if (aDTable == null)
            {
                this.strDownList = "<tr align='center' class='BarContent'><td height='40' colspan='5'>暂无可选择的广告。</td></tr>";
            }
            else
            {
                int num11 = (int) BTPParameterManager.GetParameterRow()["ADPercent"];
                foreach (DataRow row2 in aDTable.Rows)
                {
                    string str2;
                    int num = (byte) row2["Turns"];
                    int num2 = (int) row2["Pay"];
                    string str = row2["LogoURL"].ToString().Trim();
                    int intADID = (int) row2["ADID"];
                    string str3 = row2["ADName"].ToString().Trim();
                    int num1 = (int) row2["Order"];
                    num2 = (int) row2["Pay"];
                    num2 = (((100 - num11) * num2) + (((((num2 / 100) * num4) / 10) * num11) / 8)) / 100;
                    if (BTPADLinkManager.GetADLinkRowBy2ID(this.intClubID5, intADID) == null)
                    {
                        str2 = "<a href='SecretaryPage.aspx?Type=ADLINK&ADID=" + intADID + "'>选择</a>";
                    }
                    else
                    {
                        str2 = "已选择";
                    }
                    object strDownList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { strDownList, "<tr class='BarContent'><td height='33'>", str3, "</td><td><img src='", SessionItem.GetImageURL(), "Club/Ad/", str, "' width='88' height='31'></td><td>", num, "</td><td>", num2, "</td><td>", str2, "</td></tr>" });
                }
            }
        }

        private void GetFansInfo()
        {
            int num8 = 0;
            DataTable tableOderByShirt = BTPPlayer5Manager.GetTableOderByShirt(BTPClubManager.GetClubIDByUserID(this.intUserID));
            if (tableOderByShirt != null)
            {
                foreach (DataRow row in tableOderByShirt.Rows)
                {
                    string str2;
                    int intPosition = (byte) row["Pos"];
                    string strName = row["Name"].ToString().Trim();
                    int num2 = (byte) row["Age"];
                    int intAbility = (int) row["Ability"];
                    int intCategory = Convert.ToInt32(row["Category"]);
                    if (intCategory == 3)
                    {
                        intAbility = 999;
                    }
                    float single1 = ((float) intAbility) / 10f;
                    int num5 = (int) row["Shirt"];
                    int num6 = (int) row["SeasonShirt"];
                    int num4 = (byte) row["Pop"];
                    long longPlayerID = (long) row["PlayerID"];
                    num8 += num5;
                    if (num4 != 0)
                    {
                        str2 = num4 + "%";
                    }
                    else
                    {
                        str2 = "--";
                    }
                    object strDownList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { 
                        strDownList, "<tr align='center'  class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 5, 1, 1), "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</a></td><td>", num2, "</td><td>", PlayerItem.GetAbilityColor(intAbility), "</td><td><font color='#AF1A2E'>", str2, "</font></td><td><a title='上轮' style='CURSOR: hand'>", num5, "</a><a title='赛季' style='CURSOR: hand'> [ ", 
                        num6, " ]</a></td></tr>"
                     });
                }
            }
            else
            {
                this.strDownList = "<tr><td valign='top' align='center'><font style='line-height:150%'></font></td></tr>";
            }
            DataRow stadiumRowByUserID = BTPStadiumManager.GetStadiumRowByUserID(this.intUserID);
            if (stadiumRowByUserID == null)
            {
                this.strList = "<tr><td width='100%' height='140'></td></tr>";
            }
            else
            {
                string str3;
                int num1 = (int) stadiumRowByUserID["StadiumID"];
                int num12 = (byte) stadiumRowByUserID["Levels"];
                byte num20 = (byte) stadiumRowByUserID["ADCount"];
                int num21 = (int) stadiumRowByUserID["Capacity"];
                byte num22 = (byte) stadiumRowByUserID["TicketPrice"];
                int num13 = (int) stadiumRowByUserID["FansR"];
                int num11 = (int) stadiumRowByUserID["FansT"];
                int num10 = num13 + num11;
                int num14 = (byte) stadiumRowByUserID["TurnLeft"];
                switch (num12)
                {
                    case 1:
                        str3 = "1p.gif";
                        if (num14 != 0)
                        {
                            str3 = "1-2p.gif";
                        }
                        break;

                    case 2:
                        str3 = "2134pica27xxasp.gif";
                        if (num14 != 0)
                        {
                            str3 = "2134pica27xxasp2-3gp.gif";
                        }
                        break;

                    case 3:
                        str3 = "3548picba38zdggp.gif";
                        if (num14 != 0)
                        {
                            str3 = "3548picba38zdggp3-4tp.gif";
                        }
                        break;

                    case 4:
                        str3 = "4685piccba44shfap.gif";
                        if (num14 != 0)
                        {
                            str3 = "4685piccba44shfap4-5op.gif";
                        }
                        break;

                    case 5:
                        str3 = "5597picdcb55mhhsp.gif";
                        if (num14 != 0)
                        {
                            str3 = "5597picdcb55mhhsp5-6np.gif";
                        }
                        break;

                    case 6:
                        str3 = "6125picedc65jryup.gif";
                        if (num14 != 0)
                        {
                            str3 = "6125picedc65jryup6-7wp.gif";
                        }
                        break;

                    case 7:
                        str3 = "7419picfed793lkwepp.gif";
                        break;

                    default:
                        str3 = "1.gif";
                        if (num14 != 0)
                        {
                            str3 = "1-2p.gif";
                        }
                        break;
                }
                int turn = BTPGameManager.GetTurn();
                this.strList = string.Concat(new object[] { "<tr width='100%' height='130'><td width='180' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str3, "' width='180' height='130'></td></tr><tr><td height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:15px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>球迷数量</strong>：", num10, " 人<br><br><strong>球衣成本</strong>：40 元<br><br>" });
                DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, turn - 1);
                int num16 = this.intClubID5;
                int num17 = 0;
                int num18 = 1;
                int num19 = 0;
                if (devMRowByClubIDRound != null)
                {
                    num16 = (int) devMRowByClubIDRound["ClubHID"];
                    num17 = (int) devMRowByClubIDRound["ClubAID"];
                    num18 = (int) devMRowByClubIDRound["ClubHScore"];
                    num19 = (int) devMRowByClubIDRound["ClubAScore"];
                    if (((this.intPayType == 1) || ((num18 > num19) && (num16 == this.intClubID5))) || ((num18 < num19) && (num17 == this.intClubID5)))
                    {
                        this.strList = this.strList + "<strong>销售价格</strong>：60 元";
                    }
                    else
                    {
                        this.strList = this.strList + "<strong>销售价格</strong>：45 元";
                    }
                }
                else
                {
                    this.strList = this.strList + "<strong>销售价格</strong>：60 元";
                }
                this.strList = string.Concat(new object[] { this.strList, "</td></tr><tr><td style='PADDING-RIGHT:4px;PADDING-LEFT:15px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>上轮球衣销售</strong><br><br><strong>数量</strong>：", num8, "件<br><br>" });
                if (((this.intPayType == 1) || ((num18 > num19) && (num16 == this.intClubID5))) || ((num18 < num19) && (num17 == this.intClubID5)))
                {
                    this.strList = string.Concat(new object[] { this.strList, "<strong>利润</strong>：", num8 * 20, "元" });
                }
                else
                {
                    this.strList = string.Concat(new object[] { this.strList, "<strong>利润</strong>：", num8 * 5, "元" });
                }
                this.strList = this.strList + "</td></tr>";
            }
        }

        private int GetOrderFactor(int intPay)
        {
            int num = Convert.ToInt32(BTPAccountManager.GetAccountRowByUserID(this.intUserID)["DevLvl"]);
            return (intPay = (intPay * (0x15 - num)) / 20);
        }

        private void GetStadiumInfo()
        {
            DataRow stadiumRowByUserID = BTPStadiumManager.GetStadiumRowByUserID(this.intUserID);
            if (stadiumRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                string str;
                int num9;
                int num10;
                this.intStadiumID = (int) stadiumRowByUserID["StadiumID"];
                int num3 = (byte) stadiumRowByUserID["Levels"];
                int num4 = (byte) stadiumRowByUserID["ADCount"];
                int num5 = (int) stadiumRowByUserID["Capacity"];
                this.intTicketPrice = (byte) stadiumRowByUserID["TicketPrice"];
                int num6 = (int) stadiumRowByUserID["FansR"];
                int num2 = (int) stadiumRowByUserID["FansT"];
                int num = num6 + num2;
                int num7 = (byte) stadiumRowByUserID["TurnLeft"];
                string str2 = "<a href='SecretaryPage.aspx?Type=UPDATESTADIUM&StadiumID=" + this.intStadiumID + "'>升级球场</a>";
                if (num7 != 0)
                {
                    str2 = "球场建设剩余" + num7 + "轮";
                }
                switch (num3)
                {
                    case 1:
                        str = "1.gif";
                        if (num7 != 0)
                        {
                            str = "1-2.gif";
                        }
                        break;

                    case 2:
                        str = "2134pica27xxas.gif";
                        if (num7 != 0)
                        {
                            str = "2134pica27xxas2-3p.gif";
                        }
                        break;

                    case 3:
                        str = "3548picba38zdgg.gif";
                        if (num7 != 0)
                        {
                            str = "3548picba38zdgg3-4l.gif";
                        }
                        break;

                    case 4:
                        str = "4685piccba44shfa.gif";
                        if (num7 != 0)
                        {
                            str = "4685piccba44shfa4-5a.gif";
                        }
                        break;

                    case 5:
                        str = "5597picdcb55mhhs.gif";
                        if (num7 != 0)
                        {
                            str = "5597picdcb55mhhs5-6c.gif";
                        }
                        break;

                    case 6:
                        str = "6125picedc65jryu.gif";
                        if (num7 != 0)
                        {
                            str = "6125picedc65jryu6-7f.gif";
                        }
                        break;

                    case 7:
                        str = "7419picfed793lkwep.gif";
                        str2 = "最高级";
                        break;

                    default:
                        str = "1.gif";
                        break;
                }
                this.strList = string.Concat(new object[] { "<tr><td width='33%' height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:25px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>体育馆</strong>：", num3, " 级<br><strong>座　位</strong>：", num5, " 个<br><strong>广告栏</strong>：", num4, " 个<br><strong>球　迷</strong>：", num, " 人<br><strong>", str2, "</strong></font></td><td width='67%' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str, "' width='350' height='130'></td></tr>" });
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                if (!base.IsPostBack)
                {
                    if ((this.intTicketPrice < 20) || (this.intTicketPrice > 50))
                    {
                        this.ddlTicketPrice.SelectedValue = "30";
                    }
                    else
                    {
                        this.ddlTicketPrice.SelectedValue = this.intTicketPrice.ToString();
                    }
                }
                int num8 = (int) BTPGameManager.GetGameRow()["Turn"];
                DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, num8 - 1);
                if (devMRowByClubIDRound != null)
                {
                    num9 = (byte) devMRowByClubIDRound["TicketPrice"];
                    num10 = (int) devMRowByClubIDRound["TicketSold"];
                    int num11 = (int) devMRowByClubIDRound["ClubHID"];
                    if (num11 != this.intClubID5)
                    {
                        num9 = 0;
                        num10 = 0;
                    }
                }
                else
                {
                    num9 = 0;
                    num10 = 0;
                }
                int num12 = num9 * num10;
                string ticketSoldSum = BTPDevMatchManager.GetTicketSoldSum(this.intClubID5);
                if (ticketSoldSum == "")
                {
                    ticketSoldSum = "--";
                }
                string str4 = Convert.ToString(num10);
                string str5 = Convert.ToString(num12);
                if (num10 == 0)
                {
                    str4 = "--";
                    str5 = "--";
                }
                this.strDownDList = string.Concat(new object[] { "<tr><td><strong>上场球票价格为</strong>：", num9, " 元<br><br><strong>上场门票销售</strong>：", str4, " 张<br><br><strong>上场门票收入</strong>：", str5, " 元<br><br><strong>赛季门票收入</strong>：", ticketSoldSum, " 元</td></tr>" });
                DataTable aDLinkTable = BTPADLinkManager.GetADLinkTable(BTPClubManager.GetClubIDByUserID(this.intUserID));
                if (aDLinkTable == null)
                {
                    this.strDownRightList = "<tr><td align='center' height='25' colspan='4'>暂无广告赞助</td></tr>";
                }
                else
                {
                    foreach (DataRow row4 in aDLinkTable.Rows)
                    {
                        int num1 = (int) row4["ADID"];
                        int num13 = (byte) row4["Turns"];
                        string str7 = row4["ADName"].ToString().Trim();
                        int num14 = (int) row4["Pay"];
                        string str6 = row4["LogoURL"].ToString().Trim();
                        this.intADSum += num14;
                        object strDownRightList = this.strDownRightList;
                        this.strDownRightList = string.Concat(new object[] { strDownRightList, "<tr class='BarContent'><td>", str7, "</td><td align='center' height='33'><img src='", SessionItem.GetImageURL(), "Club/Ad/", str6, "' width='88' height='31'></td><td>剩", num13, "轮共", num14, "</td></tr>" });
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
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
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.intPayType = (int) onlineRowByUserID["PayType"];
                SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                if (this.intCategory != 5)
                {
                    base.Response.Redirect("Report.aspx?Parameter=90");
                }
                else
                {
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    this.strType = (string) SessionItem.GetRequest("Type", 1);
                    this.tblStadium.Visible = false;
                    this.tblAd.Visible = false;
                    this.tblClub.Visible = false;
                    switch (this.strType)
                    {
                        case "STADIUM":
                            this.strPageIntro = "<ul><li class='qian1'>基本信息</li><li class='qian2a'><a href='ClubBuild.aspx?Type=AD'>球场广告</a></li><li class='qian2a'><a href='ClubBuild.aspx?TYPE=CLUB'>球衣专卖</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblStadium.Visible = true;
                            this.GetStadiumInfo();
                            break;

                        case "AD":
                            this.strPageIntro = "<ul><li class='qian1a'><a href='ClubBuild.aspx?Type=STADIUM'>基本信息</a></li><li class='qian2'>球场广告</li><li class='qian2a'><a href='ClubBuild.aspx?TYPE=CLUB'>球衣专卖</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblAd.Visible = true;
                            this.GetADLinkTable();
                            break;

                        case "CLUB":
                            this.strPageIntro = "<ul><li class='qian1a'><a href='ClubBuild.aspx?Type=STADIUM'>基本信息</a></li><li class='qian2a'><a href='ClubBuild.aspx?Type=AD'>球场广告</a></li><li class='qian2'>球衣专卖</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblClub.Visible = true;
                            this.GetFansInfo();
                            break;

                        default:
                            this.strPageIntro = "<ul><li class='qian1'>基本信息</li><li class='qian2a'><a href='ClubBuild.aspx?Type=AD'>球场广告</a></li><li class='qian2a'><a href='ClubBuild.aspx?TYPE=CLUB'>球衣专卖</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblStadium.Visible = true;
                            this.GetStadiumInfo();
                            break;
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string s = "<script language=\"javascript\">parent.Right.location=\"TicketSold.aspx?UserID=" + this.intUserID + "\"</script>";
            base.Response.Write(s);
        }
    }
}


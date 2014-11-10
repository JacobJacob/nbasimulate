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
            int num = 0x1f40;
            DataRow stadiumRowByUserID = BTPStadiumManager.GetStadiumRowByUserID(this.intUserID);
            if (stadiumRowByUserID == null)
            {
                this.strList = "<tr><td width='100%' height='140'></td></tr>";
            }
            else
            {
                string str;
                this.intStadiumID = (int) stadiumRowByUserID["StadiumID"];
                int num2 = (byte) stadiumRowByUserID["Levels"];
                int num3 = (byte) stadiumRowByUserID["ADCount"];
                int num4 = (int) stadiumRowByUserID["Capacity"];
                this.intTicketPrice = (byte) stadiumRowByUserID["TicketPrice"];
                int num5 = (int) stadiumRowByUserID["FansR"];
                int num6 = (int) stadiumRowByUserID["FansT"];
                num = num5 + num6;
                int num7 = (byte) stadiumRowByUserID["TurnLeft"];
                switch (num2)
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
                        break;

                    default:
                        str = "1.gif";
                        if (num7 != 0)
                        {
                            str = "1-2.gif";
                        }
                        break;
                }
                this.strList = string.Concat(new object[] { "<tr><td width='30%' height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:25px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>体育馆</strong>：", num2, " 级<br><strong>座　位</strong>：", num4, " 个<br><strong>广告栏</strong>：", num3, " 个<br><strong>球  迷</strong>：", num, " 人<br></td><td width='70%' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str, "' width='350' height='130'></td></tr>" });
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
                int num8 = (int) BTPParameterManager.GetParameterRow()["ADPercent"];
                foreach (DataRow row2 in aDTable.Rows)
                {
                    string str2;
                    int num9 = (byte) row2["Turns"];
                    int num10 = (int) row2["Pay"];
                    string str3 = row2["LogoURL"].ToString().Trim();
                    int intADID = (int) row2["ADID"];
                    string str4 = row2["ADName"].ToString().Trim();
                    int num1 = (int) row2["Order"];
                    num10 = (int) row2["Pay"];
                    num10 = (((100 - num8) * num10) + (((((num10 / 100) * num) / 10) * num8) / 8)) / 100;
                    if (BTPADLinkManager.GetADLinkRowBy2ID(this.intClubID5, intADID) == null)
                    {
                        str2 = "<a href='SecretaryPage.aspx?Type=ADLINK&ADID=" + intADID + "'>选择</a>";
                    }
                    else
                    {
                        str2 = "已选择";
                    }
                    object strDownList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { strDownList, "<tr class='BarContent'><td height='33'>", str4, "</td><td><img src='", SessionItem.GetImageURL(), "Club/Ad/", str3, "' width='88' height='31'></td><td>", num9, "</td><td>", num10, "</td><td>", str2, "</td></tr>" });
                }
            }
        }

        private void GetFansInfo()
        {
            int num = 0;
            DataTable tableOderByShirt = BTPPlayer5Manager.GetTableOderByShirt(BTPClubManager.GetClubIDByUserID(this.intUserID));
            if (tableOderByShirt != null)
            {
                foreach (DataRow row in tableOderByShirt.Rows)
                {
                    string str;
                    int intPosition = (byte) row["Pos"];
                    string strName = row["Name"].ToString().Trim();
                    int num3 = (byte) row["Age"];
                    int intAbility = (int) row["Ability"];
                    if (Convert.ToInt32(row["Category"]) == 3)
                    {
                        intAbility = 0x3e7;
                    }
                    float single1 = ((float) intAbility) / 10f;
                    int num6 = (int) row["Shirt"];
                    int num7 = (int) row["SeasonShirt"];
                    int num8 = (byte) row["Pop"];
                    long longPlayerID = (long) row["PlayerID"];
                    num += num6;
                    if (num8 != 0)
                    {
                        str = num8 + "%";
                    }
                    else
                    {
                        str = "--";
                    }
                    object strDownList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { 
                        strDownList, "<tr align='center'  class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 5, 1, 1), "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</a></td><td>", num3, "</td><td>", PlayerItem.GetAbilityColor(intAbility), "</td><td><font color='#AF1A2E'>", str, "</font></td><td><a title='上轮' style='CURSOR: hand'>", num6, "</a><a title='赛季' style='CURSOR: hand'> [ ", 
                        num7, " ]</a></td></tr>"
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
                int num10 = (byte) stadiumRowByUserID["Levels"];
                byte num21 = (byte) stadiumRowByUserID["ADCount"];
                int num22 = (int) stadiumRowByUserID["Capacity"];
                byte num23 = (byte) stadiumRowByUserID["TicketPrice"];
                int num11 = (int) stadiumRowByUserID["FansR"];
                int num12 = (int) stadiumRowByUserID["FansT"];
                int num13 = num11 + num12;
                int num14 = (byte) stadiumRowByUserID["TurnLeft"];
                switch (num10)
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
                this.strList = string.Concat(new object[] { "<tr width='100%' height='130'><td width='180' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str3, "' width='180' height='130'></td></tr><tr><td height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:15px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>球迷数量</strong>：", num13, " 人<br><br><strong>球衣成本</strong>：40 元<br><br>" });
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
                        this.strList = this.strList + "<strong>销售价格</strong>：150 元";
                    }
                    else
                    {
                        this.strList = this.strList + "<strong>销售价格</strong>：100 元";
                    }
                }
                else
                {
                    this.strList = this.strList + "<strong>销售价格</strong>：150 元";
                }
                this.strList = string.Concat(new object[] { this.strList, "</td></tr><tr><td style='PADDING-RIGHT:4px;PADDING-LEFT:15px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>上轮球衣销售</strong><br><br><strong>数量</strong>：", num, "件<br><br>" });
                if (((this.intPayType == 1) || ((num18 > num19) && (num16 == this.intClubID5))) || ((num18 < num19) && (num17 == this.intClubID5)))
                {
                    this.strList = string.Concat(new object[] { this.strList, "<strong>利润</strong>：", num * 110, "元" });
                }
                else
                {
                    this.strList = string.Concat(new object[] { this.strList, "<strong>利润</strong>：", num * 60, "元" });
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
                int num;
                int num2;
                this.intStadiumID = (int) stadiumRowByUserID["StadiumID"];
                int num3 = (byte) stadiumRowByUserID["Levels"];
                int num4 = (byte) stadiumRowByUserID["ADCount"];
                int num5 = (int) stadiumRowByUserID["Capacity"];
                this.intTicketPrice = (byte) stadiumRowByUserID["TicketPrice"];
                int num6 = (int) stadiumRowByUserID["FansR"];
                int num7 = (int) stadiumRowByUserID["FansT"];
                int num8 = num6 + num7;
                int num9 = (byte) stadiumRowByUserID["TurnLeft"];
                string str2 = "<a href='SecretaryPage.aspx?Type=UPDATESTADIUM&StadiumID=" + this.intStadiumID + "'>升级球场</a>";
                if (num9 != 0)
                {
                    str2 = "球场建设剩余" + num9 + "轮";
                }
                switch (num3)
                {
                    case 1:
                        str = "1.gif";
                        if (num9 != 0)
                        {
                            str = "1-2.gif";
                        }
                        break;

                    case 2:
                        str = "2134pica27xxas.gif";
                        if (num9 != 0)
                        {
                            str = "2134pica27xxas2-3p.gif";
                        }
                        break;

                    case 3:
                        str = "3548picba38zdgg.gif";
                        if (num9 != 0)
                        {
                            str = "3548picba38zdgg3-4l.gif";
                        }
                        break;

                    case 4:
                        str = "4685piccba44shfa.gif";
                        if (num9 != 0)
                        {
                            str = "4685piccba44shfa4-5a.gif";
                        }
                        break;

                    case 5:
                        str = "5597picdcb55mhhs.gif";
                        if (num9 != 0)
                        {
                            str = "5597picdcb55mhhs5-6c.gif";
                        }
                        break;

                    case 6:
                        str = "6125picedc65jryu.gif";
                        if (num9 != 0)
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
                this.strList = string.Concat(new object[] { "<tr><td width='33%' height='140' style='PADDING-RIGHT:4px;PADDING-LEFT:25px;PADDING-BOTTOM:4px;PADDING-TOP:4px'><font style='LINE-HEIGHT:150%'><strong>体育馆</strong>：", num3, " 级<br><strong>座　位</strong>：", num5, " 个<br><strong>广告栏</strong>：", num4, " 个<br><strong>球　迷</strong>：", num8, " 人<br><strong>", str2, "</strong></font></td><td width='67%' align='right'><img src='", SessionItem.GetImageURL(), "Stadium/", str, "' width='350' height='130'></td></tr>" });
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
                int num10 = (int) BTPGameManager.GetGameRow()["Turn"];
                DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, num10 - 1);
                if (devMRowByClubIDRound != null)
                {
                    num = (byte) devMRowByClubIDRound["TicketPrice"];
                    num2 = (int) devMRowByClubIDRound["TicketSold"];
                    int num11 = (int) devMRowByClubIDRound["ClubHID"];
                    if (num11 != this.intClubID5)
                    {
                        num = 0;
                        num2 = 0;
                    }
                }
                else
                {
                    num = 0;
                    num2 = 0;
                }
                int num12 = num * num2;
                string ticketSoldSum = BTPDevMatchManager.GetTicketSoldSum(this.intClubID5);
                if (ticketSoldSum == "")
                {
                    ticketSoldSum = "--";
                }
                string str4 = Convert.ToString(num2);
                string str5 = Convert.ToString(num12);
                if (num2 == 0)
                {
                    str4 = "--";
                    str5 = "--";
                }
                this.strDownDList = string.Concat(new object[] { "<tr><td><strong>上场球票价格为</strong>：", num, " 元<br><br><strong>上场门票销售</strong>：", str4, " 张<br><br><strong>上场门票收入</strong>：", str5, " 元<br><br><strong>赛季门票收入</strong>：", ticketSoldSum, " 元</td></tr>" });
                DataTable aDLinkTable = BTPADLinkManager.GetADLinkTable(BTPClubManager.GetClubIDByUserID(this.intUserID));
                if (aDLinkTable == null)
                {
                    this.strDownRightList = "<tr><td align='center' height='25' colspan='4'>暂无广告赞助</td></tr>";
                }
                else
                {
                    foreach (DataRow row3 in aDLinkTable.Rows)
                    {
                        int num1 = (int) row3["ADID"];
                        int num13 = (byte) row3["Turns"];
                        string str6 = row3["ADName"].ToString().Trim();
                        int num14 = (int) row3["Pay"];
                        string str7 = row3["LogoURL"].ToString().Trim();
                        this.intADSum += num14;
                        object strDownRightList = this.strDownRightList;
                        this.strDownRightList = string.Concat(new object[] { strDownRightList, "<tr class='BarContent'><td>", str6, "</td><td align='center' height='33'><img src='", SessionItem.GetImageURL(), "Club/Ad/", str7, "' width='88' height='31'></td><td>剩", num13, "轮共", num14, "</td></tr>" });
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
                    this.strType = SessionItem.GetRequest("Type", 1);
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


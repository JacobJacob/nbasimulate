namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ShowPlayer : Page
    {
        public bool blnShowSkill;
        protected ImageButton btnModifyBody;
        protected ImageButton btnOK;
        protected DropDownList ddlNumber;
        protected DropDownList ddlPlayerBody;
        protected DropDownList ddlPosition;
        private DataRow drPlayer;
        protected HtmlInputHidden Face;
        public int intCategory;
        private int intCheck;
        private int intClubID3;
        private int intClubID5;
        private int intCreateTime;
        private int intKind;
        public int intLifeAssist;
        public int intLifeAssistP;
        public int intLifeBlock;
        public int intLifeBlockP;
        public int intLifeRebound;
        public int intLifeReboundP;
        public int intLifeScore;
        public int intLifeScoreP;
        public int intLifeSteal;
        public int intLifeStealP;
        public int intNumber;
        public int intPayType;
        public int intPlayed;
        public int intPlayerFaceCost;
        public int intPosition;
        public int intSeasonAssist;
        public int intSeasonAssistP;
        public int intSeasonBlock;
        public int intSeasonBlockP;
        public int intSeasonPlayed;
        public int intSeasonRebound;
        public int intSeasonReboundP;
        public int intSeasonScore;
        public int intSeasonScoreP;
        public int intSeasonSteal;
        public int intSeasonStealP;
        private int intTeamDay;
        private int intType;
        private int intUserID;
        private long longPlayerID;
        public string strAttack;
        public string strBlock;
        public string strCategory;
        public string strChangeMsg;
        public string strDefense;
        public string strDetail;
        public string strDribble;
        public string strFace;
        public string strHardness;
        public string strInfo;
        public string strJump;
        public string strLeadShip;
        public string strLimit = "6|9|16|11|16|4|11|33|11|19|51|20|10";
        public string strLinkURL;
        public string strMainScript;
        public string strName;
        public string strNickName;
        public string strNumber;
        public string strPass;
        public string strPlayerFace = "";
        public string strPoint3;
        public string strRebound;
        public string strShirt;
        public string strShot;
        private string strShow;
        public string strSpeed;
        public string strStamina;
        public string strSteal;
        public string strStrength;
        public string strTeam;
        protected HtmlTable tbHide;
        protected HtmlTable tblBody;
        protected HtmlTable tblDetail;
        protected HtmlTable tblModify;
        protected HtmlTable tblStas;
        protected HtmlTable tblStasing;
        protected TextBox tbName;
        protected HtmlTableRow trName;
        protected HtmlTableRow trNoName;

        private void btnModifyBody_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            string strPlayerBody = this.Face.Value.ToString().Trim();
            if (this.intType == 3)
            {
                num = BTPPlayer3Manager.UpdatePlayer3Body(this.longPlayerID, this.intClubID3, strPlayerBody, this.intUserID, this.strNickName);
            }
            else
            {
                num = BTPPlayer5Manager.UpdatePlayer5Body(this.longPlayerID, this.intClubID5, strPlayerBody, this.intUserID, this.strNickName);
            }
            switch (num)
            {
                case 0:
                    this.strChangeMsg = "修改成功。";
                    base.Response.Redirect(string.Concat(new object[] { "ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFYBODY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck }));
                    return;

                case 1:
                    this.strChangeMsg = "改球员不在您的球队中效力，您无法为其整容。";
                    return;

                case 2:
                    this.strChangeMsg = "您的游戏币不足，无法为其整容。";
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int num = (int) this.drPlayer["ClubID"];
            if ((num != this.intClubID3) && (num != this.intClubID5))
            {
                base.Response.Redirect("Report.aspx?Parameter=4113");
            }
            else
            {
                int intNumber = Convert.ToInt16(this.ddlNumber.SelectedItem.Value);
                int intPosition = Convert.ToInt16(this.ddlPosition.SelectedItem.Value);
                string strPlayerName = this.tbName.Text.Trim();
                if (strPlayerName.Length == 0)
                {
                    strPlayerName = this.strName;
                }
                if (this.intType == 3)
                {
                    if (((this.intPayType == 1) || (this.intCreateTime <= 15)) && (strPlayerName != this.strName))
                    {
                        if (BTPPlayer3Manager.GetPlayer3IDByPlayerName(strPlayerName) > 0L)
                        {
                            this.strChangeMsg = "有重名的球员";
                        }
                        else if (!StringItem.IsValidNameIn(strPlayerName, 1, 10))
                        {
                            this.strChangeMsg = "请勿使用非法球员名称！";
                        }
                        else
                        {
                            BTPPlayer3Manager.UpdateNumPosNameByPlayerID3(this.longPlayerID, intNumber, intPosition, strPlayerName);
                            base.Response.Redirect(string.Concat(new object[] { "ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck }));
                        }
                    }
                    else
                    {
                        BTPPlayer3Manager.UpdateNumPosByPlayerID3(this.longPlayerID, intNumber, intPosition);
                    }
                }
                else if (((this.intPayType == 1) || (this.intCreateTime <= 15)) && (strPlayerName != this.strName))
                {
                    if (BTPPlayer5Manager.GetPlayer5IDByPlayerName(strPlayerName) > 0L)
                    {
                        this.strChangeMsg = "有重名的球员";
                    }
                    else if (!StringItem.IsValidName(strPlayerName, 1, 10))
                    {
                        this.strChangeMsg = "球员名称不合法，请重新输入。";
                    }
                    else
                    {
                        BTPPlayer5Manager.UpdateNumPosNameByPlayerID5(this.longPlayerID, intNumber, intPosition, strPlayerName);
                        base.Response.Redirect(string.Concat(new object[] { "ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck }));
                    }
                }
                else
                {
                    BTPPlayer5Manager.UpdateNumPosByPlayerID5(this.longPlayerID, intNumber, intPosition);
                }
            }
        }

        private int CanUseShowSkill()
        {
            if (ToolItem.HasTool(this.intUserID, 3, 0) < 1)
            {
                return -2;
            }
            return 1;
        }

        private string GetHtmlTable(int intAbility, int intAbilityMax)
        {
            string str;
            int num = intAbility / 9;
            int num2 = intAbilityMax - intAbility;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (((this.intKind == 0) || (this.intTeamDay < 5)) && !this.blnShowSkill)
            {
                str = "0";
            }
            else
            {
                bool flag = false;
                if (this.intType == 3)
                {
                    if (this.blnShowSkill || (((int) this.drPlayer["ClubID"]) == this.intClubID3))
                    {
                        flag = true;
                    }
                    if ((this.intCategory == 4) && (this.intTeamDay >= 5))
                    {
                        flag = true;
                    }
                }
                else if (this.blnShowSkill || (((int) this.drPlayer["ClubID"]) == this.intClubID5))
                {
                    flag = true;
                }
                if (flag)
                {
                    if (intAbilityMax < 0)
                    {
                        str = "12";
                    }
                    else if (num2 > 250)
                    {
                        str = "1";
                    }
                    else if ((num2 <= 250) && (num2 > 200))
                    {
                        str = "2";
                    }
                    else if ((num2 <= 200) && (num2 > 160))
                    {
                        str = "3";
                    }
                    else if ((num2 <= 160) && (num2 > 0x7d))
                    {
                        str = "4";
                    }
                    else if ((num2 <= 0x7d) && (num2 > 0x5f))
                    {
                        str = "5";
                    }
                    else if ((num2 <= 0x5f) && (num2 > 70))
                    {
                        str = "6";
                    }
                    else if ((num2 <= 70) && (num2 > 0x30))
                    {
                        str = "7";
                    }
                    else if ((num2 <= 0x30) && (num2 > 30))
                    {
                        str = "8";
                    }
                    else if ((num2 <= 30) && (num2 > 15))
                    {
                        str = "9";
                    }
                    else if ((num2 <= 15) && (num2 > 7))
                    {
                        str = "10";
                    }
                    else
                    {
                        str = "11";
                    }
                }
                else
                {
                    str = "0";
                }
            }
            string str2 = "<table cellpadding='0' cellspacing='0' border='0' width='157'>";
            object obj2 = str2;
            return string.Concat(new object[] { obj2, "<tr><td width='120' bgcolor='#EEEEEE' height='18'><img src='", SessionItem.GetImageURL(), "Player/Ability/Color", str, ".gif' width='", num, "' height='8'></td><td width='37' align='center'>", PlayerItem.GetAbilityColor(intAbility), "</td></tr></table>" });
        }

        private void InitializeComponent()
        {
            this.btnModifyBody.Click += new ImageClickEventHandler(this.btnModifyBody_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            int num;
            this.longPlayerID = (long) SessionItem.GetRequest("PlayerID", 3);
            this.intKind = (int) SessionItem.GetRequest("Kind", 0);
            this.intType = (int) SessionItem.GetRequest("Type", 0);
            this.intCheck = (int) SessionItem.GetRequest("Check", 0);
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
            this.strMainScript = "";
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.intPayType = (int) onlineRowByUserID["PayType"];
            this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
            this.tblDetail.Visible = false;
            this.tblStas.Visible = false;
            this.tblModify.Visible = false;
            this.tblStasing.Visible = false;
            this.trName.Visible = false;
            this.trNoName.Visible = false;
            this.tbHide.Visible = false;
            this.tblBody.Visible = false;
            this.strShow = base.Request.QueryString["Show"];
            if ((this.strShow == "") || (this.strShow == null))
            {
                this.strShow = "ABILITY";
            }
            else
            {
                this.strShow = (string) SessionItem.GetRequest("Show", 1);
            }
            switch (this.strShow)
            {
                case "MODIFYBODY":
                {
                    this.SetPlayerRow();
                    if (this.drPlayer == null)
                    {
                        return;
                    }
                    num = (int) this.drPlayer["ClubID"];
                    byte num1 = (byte) this.drPlayer["Category"];
                    if (((num != this.intClubID3) || !((this.intType == 3) & (this.intCategory != 0x58))) && ((num != this.intClubID5) || !((this.intType == 5) & (this.intCategory != 0x58))))
                    {
                        this.strLinkURL = string.Concat(new object[] { "<font color='#FF0000'>能力</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>统计</a> " });
                        break;
                    }
                    this.strLinkURL = string.Concat(new object[] { 
                        "<a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=ABILITY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>能力</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, 
                        "'>统计</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>修改</a> | <font color='#FF0000'>整容</font>"
                     });
                    this.intPlayerFaceCost = (int) Global.drParameter["PlayerFaceCost"];
                    if (this.intPayType == 1)
                    {
                        this.strPlayerFace = "尊敬的会员您可以免费为您的球员整容！";
                    }
                    else
                    {
                        this.strPlayerFace = "确定要给球员整容么，这将花费您" + this.intPlayerFaceCost + "游戏币！";
                    }
                    break;
                }
                case "ABILITY":
                {
                    this.SetPlayerRow();
                    if (this.drPlayer == null)
                    {
                        return;
                    }
                    num = (int) this.drPlayer["ClubID"];
                    byte num3 = (byte) this.drPlayer["Category"];
                    if (((num == this.intClubID3) && ((this.intType == 3) & (this.intCategory != 0x58))) || ((num == this.intClubID5) && ((this.intType == 5) & (this.intCategory != 0x58))))
                    {
                        this.strLinkURL = string.Concat(new object[] { 
                            "<font color='#FF0000'>能力</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>统计</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, 
                            "'>修改</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFYBODY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>整容</a>"
                         });
                    }
                    else
                    {
                        this.strLinkURL = string.Concat(new object[] { "<font color='#FF0000'>能力</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>统计</a> " });
                    }
                    this.tblDetail.Visible = true;
                    this.SetInfo();
                    this.SetAbility();
                    goto Label_0F14;
                }
                case "STAS":
                {
                    this.SetPlayerRow();
                    if (this.drPlayer == null)
                    {
                        return;
                    }
                    num = (int) this.drPlayer["ClubID"];
                    byte num4 = (byte) this.drPlayer["Category"];
                    if (((num == this.intClubID3) && ((this.intType == 3) & (this.intCategory != 0x58))) || ((num == this.intClubID5) && ((this.intType == 5) & (this.intCategory != 0x58))))
                    {
                        this.strLinkURL = string.Concat(new object[] { 
                            "<a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=ABILITY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>能力</a> | <font color='#FF0000'>统计</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, 
                            "'>修改</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFYBODY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>整容</a>"
                         });
                    }
                    else
                    {
                        this.strLinkURL = string.Concat(new object[] { "<a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=ABILITY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>能力</a> | <font color='#FF0000'>统计</font> " });
                    }
                    this.SetInfo();
                    if (SessionItem.CanUseAfterUpdate() || (this.intType != 5))
                    {
                        this.tblStas.Visible = true;
                        this.SetStas();
                    }
                    else
                    {
                        this.tblStasing.Visible = true;
                    }
                    goto Label_0F14;
                }
                case "MODIFY":
                {
                    DateTime time = (DateTime) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["CreateTime"];
                    TimeSpan span = (TimeSpan) (DateTime.Today - time);
                    this.intCreateTime = span.Days;
                    this.SetPlayerRow();
                    if (this.drPlayer == null)
                    {
                        return;
                    }
                    num = (int) this.drPlayer["ClubID"];
                    if ((num != this.intClubID3) && (num != this.intClubID5))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                    this.strLinkURL = string.Concat(new object[] { 
                        "<a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=ABILITY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>能力</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, 
                        "'>统计</a> | <font color='#FF0000'>修改</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFYBODY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>整容</a>"
                     });
                    this.tblModify.Visible = true;
                    this.SetInfo();
                    this.SetModify();
                    goto Label_0F14;
                }
                case "HIDE":
                    if (BTPToolLinkManager.CheckHide(this.intUserID, this.longPlayerID, this.intType) == 1)
                    {
                        this.tbHide.Visible = true;
                        this.SetPlayerRow();
                        this.SetInfo();
                        this.SetAbility();
                        this.SetHide();
                    }
                    goto Label_0F14;

                default:
                {
                    this.SetPlayerRow();
                    if (this.drPlayer == null)
                    {
                        return;
                    }
                    num = (int) this.drPlayer["ClubID"];
                    byte num5 = (byte) this.drPlayer["Category"];
                    if (((num == this.intClubID3) && ((this.intType == 3) & (this.intCategory != 0x58))) || ((num == this.intClubID5) && ((this.intType == 5) & (this.intCategory != 0x58))))
                    {
                        this.strLinkURL = string.Concat(new object[] { 
                            "<font color='#FF0000'>能力</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>统计</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, 
                            "'>修改</a> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=MODIFYBODY&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>整容</a>"
                         });
                    }
                    else
                    {
                        this.strLinkURL = string.Concat(new object[] { "<font color='#FF0000'>能力</font> | <a href='ShowPlayer.aspx?PlayerID=", this.longPlayerID, "&Show=STAS&Kind=", this.intKind, "&Type=", this.intType, "&Check=", this.intCheck, "'>统计</a>" });
                    }
                    this.tblDetail.Visible = true;
                    this.SetInfo();
                    this.SetAbility();
                    goto Label_0F14;
                }
            }
            this.tblBody.Visible = true;
            this.SetInfo();
            if (!base.IsPostBack)
            {
                DataView view = new DataView(DDLItem.GetPlayerBodyItem(), "ID<>'0'", "", DataViewRowState.CurrentRows);
                this.ddlPlayerBody.DataSource = view;
                this.ddlPlayerBody.DataTextField = "Name";
                this.ddlPlayerBody.DataValueField = "ID";
                this.ddlPlayerBody.DataBind();
            }
            this.Face.Value = this.strFace;
        Label_0F14:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.btnModifyBody.Attributes["OnClick"] = "return ModifyBody();";
        }

        private void SetAbility()
        {
            this.blnShowSkill = (bool) this.drPlayer["ShowSkill"];
            int intAbility = (int) this.drPlayer["Speed"];
            int num2 = (int) this.drPlayer["Jump"];
            int num3 = (int) this.drPlayer["Strength"];
            int num4 = (int) this.drPlayer["Stamina"];
            int num5 = (int) this.drPlayer["Shot"];
            int num6 = (int) this.drPlayer["Point3"];
            int num7 = (int) this.drPlayer["Dribble"];
            int num8 = (int) this.drPlayer["Pass"];
            int num9 = (int) this.drPlayer["Rebound"];
            int num10 = (int) this.drPlayer["Steal"];
            int num11 = (int) this.drPlayer["Block"];
            int num12 = (int) this.drPlayer["Attack"];
            int num13 = (int) this.drPlayer["Defense"];
            int num14 = (int) this.drPlayer["Team"];
            int intAbilityMax = (int) this.drPlayer["SpeedMax"];
            int num16 = (int) this.drPlayer["JumpMax"];
            int num17 = (int) this.drPlayer["StrengthMax"];
            int num18 = (int) this.drPlayer["StaminaMax"];
            int num19 = (int) this.drPlayer["ShotMax"];
            int num20 = (int) this.drPlayer["Point3Max"];
            int num21 = (int) this.drPlayer["DribbleMax"];
            int num22 = (int) this.drPlayer["PassMax"];
            int num23 = (int) this.drPlayer["ReboundMax"];
            int num24 = (int) this.drPlayer["StealMax"];
            int num25 = (int) this.drPlayer["BlockMax"];
            int num26 = (int) this.drPlayer["AttackMax"];
            int num27 = (int) this.drPlayer["DefenseMax"];
            int num28 = (int) this.drPlayer["TeamMax"];
            if (((this.intKind == 1) && (this.intCategory > 1)) && (BTPToolLinkManager.CheckPrivateSkill(this.intUserID, this.longPlayerID, this.intType) == 1))
            {
                this.blnShowSkill = true;
            }
            if (this.intCategory == 0x58)
            {
                this.blnShowSkill = false;
            }
            this.strSpeed = this.GetHtmlTable(intAbility, intAbilityMax);
            this.strJump = this.GetHtmlTable(num2, num16);
            this.strStrength = this.GetHtmlTable(num3, num17);
            this.strStamina = this.GetHtmlTable(num4, num18);
            this.strShot = this.GetHtmlTable(num5, num19);
            this.strPoint3 = this.GetHtmlTable(num6, num20);
            this.strDribble = this.GetHtmlTable(num7, num21);
            this.strPass = this.GetHtmlTable(num8, num22);
            this.strRebound = this.GetHtmlTable(num9, num23);
            this.strSteal = this.GetHtmlTable(num10, num24);
            this.strBlock = this.GetHtmlTable(num11, num25);
            this.strAttack = this.GetHtmlTable(num12, num26);
            this.strDefense = this.GetHtmlTable(num13, num27);
            this.strTeam = this.GetHtmlTable(num14, num28);
        }

        private void SetHide()
        {
            int intAbility = (int) this.drPlayer["Attack"];
            int num2 = (int) this.drPlayer["Defense"];
            int num3 = (int) this.drPlayer["Team"];
            int num4 = (int) this.drPlayer["LeadShip"];
            int num5 = (int) this.drPlayer["Hardness"];
            int intAbilityMax = (int) this.drPlayer["AttackMax"];
            int num7 = (int) this.drPlayer["DefenseMax"];
            int num8 = (int) this.drPlayer["TeamMax"];
            this.intKind = 1;
            this.blnShowSkill = true;
            this.strLeadShip = this.GetHtmlTable(num4, -1);
            this.strHardness = this.GetHtmlTable(num5, -1);
            this.strAttack = this.GetHtmlTable(intAbility, intAbilityMax);
            this.strDefense = this.GetHtmlTable(num2, num7);
            this.strTeam = this.GetHtmlTable(num3, num8);
        }

        private void SetInfo()
        {
            int num6;
            string str3;
            int num7;
            object obj2;
            int num = 0;
            string str = "";
            string str2 = "";
            if (this.intCheck == 0)
            {
                if ((this.intType == 5) && (this.intCategory == 2))
                {
                    this.strLinkURL = PlayerItem.GetPlayerPotentialStar(this.drPlayer);
                    int num2 = (int) this.drPlayer["Salary"];
                    str = "<table width='109' border='0' cellspacing='0' cellpadding='0'><tr><td height='18' width='37' align='center'>工资</td><td width='78'>" + num2 + "</td></tr></table>";
                }
                else if (this.intCategory == 1)
                {
                    str = "";
                }
                else
                {
                    this.strLinkURL = "";
                    str = "";
                }
                if (this.intType == 3)
                {
                    if ((this.intCategory == 3) || (this.intCategory == 6))
                    {
                        num = 1;
                    }
                    if (this.intCategory == 4)
                    {
                        num = 3;
                    }
                }
                else if (this.intCategory == 2)
                {
                    num = 4;
                }
                else if (this.intCategory == 3)
                {
                    num = 5;
                }
                else if (this.intCategory == 4)
                {
                    num = 6;
                }
            }
            int intClubID = (int) this.drPlayer["ClubID"];
            int num4 = (byte) this.drPlayer["Category"];
            if (((this.intCategory == 1) && (intClubID != this.intClubID3)) && (intClubID != this.intClubID5))
            {
                str2 = "";
            }
            else
            {
                SqlDataReader reader = BTPToolLinkManager.CheckPlayer5PLink(this.intUserID, this.longPlayerID, this.intType);
                bool flag = false;
                bool flag2 = false;
                while (reader.Read())
                {
                    switch (((byte) reader["Category"]))
                    {
                        case 1:
                        {
                            flag = true;
                            continue;
                        }
                        case 2:
                            flag2 = true;
                            break;
                    }
                }
                reader.Close();
                if (this.intCheck == 0)
                {
                    if ((intClubID != this.intClubID3) && (intClubID != this.intClubID5))
                    {
                        if (flag)
                        {
                            str2 = string.Concat(new object[] { "&nbsp;&nbsp;<a href=ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=0&PlayerID=", this.longPlayerID, "><img align='absmiddle' src='", SessionItem.GetImageURL(), "MinPrivateSkill.gif' width='12' height='12' border='0' alt='查看球员潜力'><a>" });
                        }
                        else
                        {
                            str2 = string.Concat(new object[] { "&nbsp;&nbsp;<a href=SecretaryPage.aspx?PlayerType=", this.intType, "&Type=PRIVATESKILL&PlayerID=", this.longPlayerID, "&CheckType=", num, " target='Center'><img align='absmiddle' src='", SessionItem.GetImageURL(), "MinPrivateSkill.gif' width='12' height='12' border='0' alt='查看球员潜力'><a>" });
                        }
                    }
                    else
                    {
                        str2 = "";
                    }
                }
                if (flag2)
                {
                    str2 = string.Concat(new object[] { str2, "&nbsp;&nbsp;<a href=ShowPlayer.aspx?Show=HIDE&Type=", this.intType, "&Kind=1&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "><img  align='absmiddle'src='", SessionItem.GetImageURL(), "MinHideSkill.gif' width='12' height='12' border='0' alt='查看隐藏属性'><a>" });
                }
                else
                {
                    str2 = string.Concat(new object[] { str2, "&nbsp;&nbsp;<a href=SecretaryPage.aspx?PlayerType=", this.intType, "&Type=HIDE&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "&CheckType=", num, " target='Center'><img align='absmiddle' src='", SessionItem.GetImageURL(), "MinHideSkill.gif' width='12' height='12' border='0' alt='查看隐藏属性'><a>" });
                }
                if (((this.intCheck == 1) && ((intClubID == this.intClubID3) || (intClubID == this.intClubID5))) && (this.intCategory == 1))
                {
                    obj2 = str2;
                    str2 = string.Concat(new object[] { obj2, "&nbsp;&nbsp;<a href='SecretaryPage.aspx?PlayerType=", this.intType, "&Type=REFASHION&PlayerID=", this.longPlayerID, "' target='Center'><img align='absmiddle' src='", SessionItem.GetImageURL(), "MinRefashion.gif' width='12' height='12' border='0' alt='球员洗点'><a>" });
                }
            }
            if (this.intType == 10)
            {
                str2 = "";
            }
            if (num4 == 0x58)
            {
                str2 = "&nbsp;&nbsp;";
            }
            this.strName = this.drPlayer["Name"].ToString().Trim();
            this.strFace = this.drPlayer["Face"].ToString().Trim();
            DataRow accountRowByClubID = BTPAccountManager.GetAccountRowByClubID(intClubID);
            if (accountRowByClubID == null)
            {
                num6 = 1;
                str3 = "无";
                num7 = 0;
            }
            else
            {
                num6 = Convert.ToInt16(accountRowByClubID["Shirt"].ToString().Trim());
                str3 = accountRowByClubID["NickName"].ToString().Trim();
                num7 = (int) accountRowByClubID["UserID"];
                str3 = AccountItem.GetNickNameInfo(num7, str3, "", 8);
            }
            this.intNumber = (byte) this.drPlayer["Number"];
            if (num6 > 15)
            {
                this.strNumber = (0x526c + this.intNumber) + "";
            }
            else
            {
                this.strNumber = (0x5208 + this.intNumber) + "";
            }
            int num8 = 0;
            if (this.intType == 5)
            {
                this.strShirt = (0x4e20 + num6) + "";
                num8 = (int) this.drPlayer["Salary"];
            }
            else
            {
                this.strShirt = (0x4e84 + num6) + "";
            }
            this.intPosition = (byte) this.drPlayer["Pos"];
            int num9 = (byte) this.drPlayer["Age"];
            int num10 = (byte) this.drPlayer["PlayedYear"];
            int num11 = (byte) this.drPlayer["Height"];
            int num12 = (byte) this.drPlayer["Weight"];
            int intPower = (byte) this.drPlayer["Power"];
            float num14 = ((float) ((int) this.drPlayer["Ability"])) / 10f;
            int intStatus = (byte) this.drPlayer["Status"];
            int intSuspend = (byte) this.drPlayer["Suspend"];
            string strEvent = this.drPlayer["Event"].ToString();
            this.intTeamDay = (int) this.drPlayer["TeamDay"];
            int num17 = (byte) this.drPlayer["Category"];
            string str5 = this.drPlayer["Name"].ToString().Trim();
            bool flag3 = false;
            if (this.intType != 6)
            {
                flag3 = (bool) this.drPlayer["IsRetire"];
            }
            string str6 = "";
            if (((num17 == 1) && (intClubID == this.intClubID3)) || ((num17 == 1) && (intClubID == this.intClubID5)))
            {
                str6 = string.Concat(new object[] { "<span style='padding-left:15px' width='90' ><img align='absmiddle' src='", SessionItem.GetImageURL(), "llzc.gif' width='12' height='12' border='0' alt='理疗中心'>&nbsp;<a href='PlayerCenter.aspx?PlayerType=", this.intType, "&Type=9&PlayerID=", this.longPlayerID, "' target='Center'>理疗中心</a></span>" });
                str2 = str2 + "&nbsp;&nbsp;";
            }
            string strInfo = this.strInfo;
            this.strInfo = strInfo + "<table width='201' border='0' cellspacing='0' cellpadding='0'><tr><td width='92' valign='top'><img id='imgCharactor' src='" + SessionItem.GetImageURL() + "Player/Charactor/NewPlayer.png' width='90' height='130'><br>" + str6 + "</td><td width='109'><table width='109' border='0' cellspacing='0' cellpadding='0'><tr><td colspan=2 height='25' align='center'>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "&nbsp;<strong>" + str5 + "</strong></td></tr><tr><td height='18' width='37' align='center'>年龄</td>";
            if (flag3)
            {
                obj2 = this.strInfo;
                this.strInfo = string.Concat(new object[] { obj2, "<td width='78'><a title='第", this.drPlayer["BirthTurn"].ToString(), "轮生日</br>球员将在赛季结束后退役' style='CURSOR: hand;color:red'>", num9, "!</a>&nbsp;<a title='球龄' style='CURSOR: hand'>[", num10, "]</a></td></tr>" });
            }
            else
            {
                obj2 = this.strInfo;
                this.strInfo = string.Concat(new object[] { obj2, "<td width='78'><a title='第", this.drPlayer["BirthTurn"].ToString(), "轮生日' style='CURSOR: hand'>", num9, "</a>&nbsp;<a title='球龄' style='CURSOR: hand'>[", num10, "]</a></td></tr>" });
            }
            if ((this.intType == 5) && (this.intCheck == 1))
            {
                obj2 = this.strInfo;
                this.strInfo = string.Concat(new object[] { obj2, "<tr><td height='18' align='center'>薪水</td><td>", num8, "</td></tr><tr>" });
            }
            if (this.intCheck == 0)
            {
                this.strInfo = this.strInfo + "<tr><td height='18' align='center'>经理</td><td>" + str3 + "</td></tr><tr>";
            }
            obj2 = this.strInfo;
            this.strInfo = string.Concat(new object[] { obj2, "<td height='18' width='37' align='center'>位置</td><td width='78'><a title='", PlayerItem.GetPlayerChsPosition(this.intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(this.intPosition), "</td></tr><tr><td height='18' align='center'>身高</td><td>", num11, "CM</td></tr><tr><td height='18' align='center'>体重</td><td>", num12, "KG</td></tr><tr><td height='18' align='center'>体力</td><td>", PlayerItem.GetPowerColor(intPower), "</td></tr><tr><td height='18' align='center'>综合</td><td>", num14, "</td></tr></table></td></tr></table>" });
            this.strInfo = this.strInfo + "<table style='padding:0px;margin:0px' width='201' border='0' cellspacing='0' cellpadding='0'><tr>";
            if (str == "")
            {
                if (this.intCategory == 1)
                {
                    if (this.intCheck == 1)
                    {
                        strInfo = this.strInfo;
                        this.strInfo = strInfo + "<td align='left'>" + str2 + "</td><td align='right' style='padding-right:5px'>" + this.strLinkURL + "</td>";
                    }
                    else if ((this.intClubID3 != intClubID) && (this.intClubID5 != intClubID))
                    {
                        obj2 = this.strInfo;
                        this.strInfo = string.Concat(new object[] { obj2, "<td height='18' style='padding:4px;'><a href='ShowClub.aspx?UserID=", num7, "&Type=", this.intType, "'>返回</a></td><td style='padding:4px;' align='right'>", this.strLinkURL, "</td>" });
                    }
                    else
                    {
                        obj2 = this.strInfo;
                        this.strInfo = string.Concat(new object[] { obj2, "<td height='18' style='padding:4px;' colspan='2'><a href='ShowClub.aspx?UserID=", num7, "&Type=", this.intType, "'>返回</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", this.strLinkURL, "</td>" });
                    }
                }
                else
                {
                    this.strInfo = this.strInfo + "<td height='18' colspan='2' style='padding:4px;' align='right'>" + this.strLinkURL + "</td>";
                }
            }
            else
            {
                strInfo = this.strInfo;
                this.strInfo = strInfo + "<td height='18' style='padding:4px;'>" + this.strLinkURL + "</td><td>" + str + "</td>";
            }
            this.strInfo = this.strInfo + "</tr></table>";
        }

        private void SetModify()
        {
            if (!base.IsPostBack)
            {
                DataTable playerTableByClubID;
                if ((this.intPayType > 0) || (this.intCreateTime <= 15))
                {
                    this.trName.Visible = true;
                    this.tbName.Text = this.strName.Trim();
                }
                else
                {
                    this.trNoName.Visible = true;
                }
                if (this.intType == 3)
                {
                    playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
                }
                else
                {
                    playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(this.intClubID5);
                }
                string rowFilter = "";
                if (playerTableByClubID != null)
                {
                    foreach (DataRow row in playerTableByClubID.Rows)
                    {
                        if (((byte) row["Number"]) != this.intNumber)
                        {
                            rowFilter = rowFilter + "Number<>" + row["Number"].ToString() + " AND ";
                        }
                    }
                    rowFilter = rowFilter + "Number<51";
                }
                DataView view = new DataView(PlayerItem.GetPlayerNumItem(), rowFilter, "", DataViewRowState.CurrentRows);
                this.ddlNumber.DataSource = view;
                this.ddlNumber.DataTextField = "Number";
                this.ddlNumber.DataValueField = "Number";
                this.ddlNumber.DataBind();
                view = new DataView(PlayerItem.GetPlayerPositionItem(), "PositionID<>0", "", DataViewRowState.CurrentRows);
                this.ddlPosition.DataSource = view;
                this.ddlPosition.DataTextField = "PositionName";
                this.ddlPosition.DataValueField = "PositionID";
                this.ddlPosition.DataBind();
                this.ddlNumber.SelectedValue = this.intNumber.ToString();
                this.ddlPosition.SelectedValue = this.intPosition.ToString();
            }
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
        }

        private void SetPlayerRow()
        {
            if (this.intType == 3)
            {
                this.drPlayer = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if (this.drPlayer == null)
                {
                    base.Response.Redirect("PlayerError.htm");
                }
                else
                {
                    int num = (int) this.drPlayer["ClubID"];
                    this.intCategory = (byte) this.drPlayer["Category"];
                    if ((num != this.intClubID3) && (this.intCheck != 0))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                }
            }
            else if (this.intType == 5)
            {
                this.drPlayer = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if (this.drPlayer == null)
                {
                    base.Response.Redirect("PlayerError.htm");
                }
                else
                {
                    int num2 = (int) this.drPlayer["ClubID"];
                    this.intCategory = (byte) this.drPlayer["Category"];
                    if ((num2 != this.intClubID5) && (this.intCheck != 0))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                }
            }
            else if (this.intType == 6)
            {
                this.drPlayer = BTPOldPlayerManager.GetPlayerRowByPlayerID(this.longPlayerID);
                if (this.drPlayer != null)
                {
                    int num1 = (int) this.drPlayer["ClubID"];
                }
                else
                {
                    base.Response.Redirect("PlayerError.htm");
                }
            }
            else if (this.intType == 7)
            {
                this.drPlayer = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if (this.drPlayer != null)
                {
                    int num3 = (int) this.drPlayer["ClubID"];
                }
                else
                {
                    base.Response.Redirect("PlayerError.htm");
                }
            }
            else if (this.intType == 10)
            {
                this.drPlayer = BTPPlayer5Manager.GetSelectPlayerRowByID(this.longPlayerID);
                if (this.drPlayer != null)
                {
                    int num4 = (int) this.drPlayer["ClubID"];
                }
                else
                {
                    base.Response.Redirect("PlayerError.htm");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
        }

        private void SetStas()
        {
            DataRow playerRowByPlayerID;
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else if (this.intType == 7)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else if (this.intType == 10)
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetSelectPlayerRowByID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            this.intSeasonAssist = (int) playerRowByPlayerID["SeasonAssist"];
            this.intSeasonScore = (int) playerRowByPlayerID["SeasonScore"];
            this.intSeasonRebound = (int) playerRowByPlayerID["SeasonRebound"];
            this.intSeasonBlock = (int) playerRowByPlayerID["SeasonBlock"];
            this.intSeasonSteal = (int) playerRowByPlayerID["SeasonSteal"];
            this.intSeasonPlayed = (int) playerRowByPlayerID["SeasonPlayed"];
            this.intPlayed = (int) playerRowByPlayerID["Played"];
            this.intLifeScore = (int) playerRowByPlayerID["LifeScore"];
            this.intLifeRebound = (int) playerRowByPlayerID["LifeRebound"];
            this.intLifeAssist = (int) playerRowByPlayerID["LifeAssist"];
            this.intLifeBlock = (int) playerRowByPlayerID["LifeBlock"];
            this.intLifeSteal = (int) playerRowByPlayerID["LifeSteal"];
            if (this.intSeasonPlayed != 0)
            {
                this.intSeasonAssistP = this.intSeasonAssist / this.intSeasonPlayed;
                this.intSeasonScoreP = this.intSeasonScore / this.intSeasonPlayed;
                this.intSeasonReboundP = this.intSeasonRebound / this.intSeasonPlayed;
                this.intSeasonBlockP = this.intSeasonBlock / this.intSeasonPlayed;
                this.intSeasonStealP = this.intSeasonSteal / this.intSeasonPlayed;
            }
            else
            {
                this.intSeasonAssistP = 0;
                this.intSeasonScoreP = 0;
                this.intSeasonReboundP = 0;
                this.intSeasonBlockP = 0;
                this.intSeasonStealP = 0;
            }
            if (this.intPlayed != 0)
            {
                this.intLifeAssistP = this.intLifeAssist / this.intPlayed;
                this.intLifeScoreP = this.intLifeScore / this.intPlayed;
                this.intLifeReboundP = this.intLifeRebound / this.intPlayed;
                this.intLifeBlockP = this.intLifeBlock / this.intPlayed;
                this.intLifeStealP = this.intLifeSteal / this.intPlayed;
            }
            else
            {
                this.intLifeAssistP = 0;
                this.intLifeScoreP = 0;
                this.intLifeReboundP = 0;
                this.intLifeBlockP = 0;
                this.intLifeStealP = 0;
            }
        }
    }
}


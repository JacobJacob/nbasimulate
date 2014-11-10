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

    public class SecretaryPage : Page
    {
        private int arenaID;
        public bool blIsPresent;
        private bool blnIsHonor;
        private bool blnRefashionSafe;
        private bool blnSex;
        protected ImageButton btnCancelPrearrange;
        protected ImageButton btnInTeam;
        protected ImageButton btnOK;
        protected ImageButton btnOrderOK;
        protected ImageButton btnSearchCancel;
        protected CheckBox cbPresent;
        protected CheckBox cbRefashionSafe;
        protected CheckBox cbType;
        private DateTime datEndBidTime;
        protected DropDownList ddlEndBidTime;
        protected DropDownList ddlEndBidTime5;
        protected DropDownList ddlExtendStaff;
        protected DropDownList ddlStaff;
        protected DropDownList ddlStaff1;
        protected DropDownList ddlTicketPrice;
        private int intAbility;
        private int intADID;
        private int intBadRefashion;
        private int intCategory;
        public int intCess;
        private int intCheck;
        private int intCheckType;
        private int intClubAID;
        private int intClubBID;
        private int intClubID;
        private int intClubID3;
        private int intClubID5;
        private int intClubIDS;
        private int intContract;
        private int intCupID;
        private int intDevCupID;
        private int intHeight;
        private int intLevel;
        private int intLookUserID;
        private int intMarket;
        private int intMarketLevel = 8;
        private int intPayType;
        public int intPayWealth;
        private int intPlayer5Number;
        private int intPlayerCategory;
        private int intPV;
        private int intRank;
        private int intRefashionCost;
        private int intSalary;
        private int intScore;
        private int intStaffID;
        private int intStockUserID;
        private int intTag;
        private int intTrainType;
        private int intType;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        private int intXGuessID;
        private long lgMoney;
        public long longNpcID;
        public long longPlayerID;
        public long longSearchPlayerID;
        private SecretaryItem si;
        public string strBidMsg;
        public string strBidTime;
        public string strBtnCancel;
        public string strBuyOne;
        private string strClubLogo;
        private string strClubName3;
        private string strClubName5;
        private string strDevCode;
        private string strNickName;
        private string strPlayer5Name;
        private string strPlayerName;
        public string strRefashionButton;
        public string strRefashionMsg;
        public string strSaleOne;
        public string strSay;
        public string strSecFace;
        public string strToolMsg;
        private string strType;
        protected TextBox tbBCount;
        protected TextBox tbBidPrice;
        protected TextBox tbBidPrice5;
        protected TextBox tbBLock;
        protected TextBox tbBuyCount;
        protected HtmlTable tbBuyOrder;
        protected TextBox tbBuyPrice;
        protected HtmlTable tbChooseClub;
        protected TextBox tbClubName;
        protected TextBox tbCount;
        protected TextBox tbFreeBidPrice;
        protected HtmlTable tblDevBidHelper;
        protected HtmlTable tblExtendStaff;
        protected HtmlTable tblFreeBid;
        protected HtmlTable tblOldPlayer;
        protected HtmlTable tblPlayer5Bid;
        protected HtmlTable tblPlayerBid;
        protected HtmlTable tblPosistion;
        protected HtmlTable tblRegCup;
        protected HtmlTable tblShowSkill;
        protected HtmlTable tblStaff;
        protected HtmlTable tblStaff1;
        protected HtmlTable tblTicketPrice;
        protected HtmlTable tblTool;
        protected HtmlTable tblXGuess;
        protected TextBox tbMaxPrice;
        protected TextBox tbOldPlayer;
        protected TextBox tbPosistion;
        protected HtmlTable tbPresent;
        protected TextBox tbPresentText;
        protected TextBox tbPrice;
        protected HtmlTable tbSay;
        protected HtmlTable tbWealthOrder;
        protected TextBox tbXGuessAmount;
        protected HtmlTableRow tr1;
        protected HtmlTableRow tr2;
        protected HtmlTableRow tr3;
        protected HtmlTableRow trOther;
        protected HtmlTableRow trPresent;
        protected HtmlTableRow trRefashion;
        protected HtmlTableRow trSafe;

        private void AddADLink()
        {
            this.intADID = SessionItem.GetRequest("ADID", 0);
            DataRow aDRowByADID = BTPADManager.GetADRowByADID(this.intADID);
            int num = (byte) aDRowByADID["Turns"];
            int num2 = (int) aDRowByADID["Pay"];
            string str = aDRowByADID["ADName"].ToString().Trim();
            int num1 = (int) aDRowByADID["Order"];
            aDRowByADID = BTPStadiumManager.GetStadiumRowByClubID(this.intClubID5);
            int num3 = (int) aDRowByADID["FansR"];
            int num4 = (int) aDRowByADID["FansT"];
            int num5 = (int) BTPParameterManager.GetParameterRow()["ADPercent"];
            int num6 = num3 + num4;
            num2 = (((100 - num5) * num2) + (((((num2 / 100) * num6) / 10) * num5) / 8)) / 100;
            int num7 = 0x3e8 + ((num3 + num4) / 4);
            int num8 = num7 / 0x3e8;
            int aDLCountByClubID = BTPADLinkManager.GetADLCountByClubID(this.intClubID5);
            if (num8 > aDLCountByClubID)
            {
                this.strSay = string.Concat(new object[] { this.strNickName, "经理您好，您所要选择的广告是", str, "，这将给俱乐部带来", num2, "元的资金收入，但将占用一个球场广告位", num, "轮。" });
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ADDADLINK);
            }
            else
            {
                this.strSay = this.strNickName + "经理您好，赞助商拒绝悬挂更多的广告，咱们还需要更好的发展俱乐部。";
            }
        }

        private void AddArrangeLvl()
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intWealth = (int) accountRowByUserID["Wealth"];
                int num2 = (int) BTPParameterManager.GetParameterRow()["AddArrangeLvlWealth"];
                int request = SessionItem.GetRequest("DevMatchID", 0);
                accountRowByUserID = BTPDevMatchManager.GetDevMRowByDevMatchID(request);
                if (accountRowByUserID == null)
                {
                    this.strSay = "没有此场比赛！";
                }
                else
                {
                    int num4 = (int) accountRowByUserID["ClubHID"];
                    int num5 = (int) accountRowByUserID["ClubAID"];
                    bool flag1 = (bool) accountRowByUserID["UseStaffH"];
                    bool flag2 = (bool) accountRowByUserID["UseStaffA"];
                    if ((num4 == this.intClubID5) || (num5 == this.intClubID5))
                    {
                        if (this.intWealth < num2)
                        {
                            this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您没有足够游戏币，提升本场比赛战术等级需要花费您 ", num2, " 游戏币！" });
                        }
                        else
                        {
                            this.strSay = "您确定要花 " + num2 + " 游戏币来提升本场比赛的战术等级吗？";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ADDARRANGELVL);
                        }
                    }
                    else
                    {
                        this.strSay = this.strNickName + "经理，您好！您没参加此场职业联赛！";
                    }
                }
            }
        }

        private void AddDataOnly()
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                string str;
                this.intWealth = (int) accountRowByUserID["Wealth"];
                int num = 0;
                int request = SessionItem.GetRequest("Category", 0);
                long longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                int num4 = SessionItem.GetRequest("PlayerType", 0);
                accountRowByUserID = BTPParameterManager.GetParameterRow();
                int num5 = (int) accountRowByUserID["AddPowerWealth"];
                int num6 = (int) accountRowByUserID["AddHappyWealth"];
                int num7 = (int) accountRowByUserID["MinusAgeWealth"];
                int num8 = (int) accountRowByUserID["AddStatusWealth"];
                int num9 = (int) accountRowByUserID["AddMaxWealth"];
                switch (request)
                {
                    case 1:
                        num = num5;
                        break;

                    case 2:
                        num = num6;
                        break;

                    case 3:
                        num = num7;
                        break;

                    case 4:
                        num = num8;
                        break;

                    case 5:
                        num = num9;
                        break;

                    default:
                        return;
                }
                int num10 = 0;
                if (num4 == 3)
                {
                    accountRowByUserID = BTPPlayer3Manager.GetPlayerRowByPlayerID(longPlayerID);
                    if (accountRowByUserID == null)
                    {
                        this.strSay = "无此球员！";
                        return;
                    }
                    int num11 = (int) accountRowByUserID["ClubID"];
                    byte num12 = (byte) accountRowByUserID["Category"];
                    byte num13 = (byte) accountRowByUserID["Power"];
                    byte num14 = (byte) accountRowByUserID["Happy"];
                    num10 = (int) accountRowByUserID["Ability"];
                    str = accountRowByUserID["Name"].ToString().Trim();
                    if ((num11 != this.intClubID3) || (num12 != 1))
                    {
                        this.strSay = this.strNickName + "经理，您好！此球员现在还没在为您的球队效力！";
                        return;
                    }
                    if ((num13 == 100) && (request == 1))
                    {
                        this.strSay = this.strNickName + "此球员体力已充沛，不需要再进行恢复了！";
                        return;
                    }
                    if ((num14 == 100) && (request == 2))
                    {
                        this.strSay = this.strNickName + "此球员热情高涨，不需要再进行恢复了！";
                        return;
                    }
                }
                else
                {
                    accountRowByUserID = BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerID);
                    if (accountRowByUserID == null)
                    {
                        this.strSay = "无此球员！";
                        return;
                    }
                    num10 = (int) accountRowByUserID["Ability"];
                    byte num15 = (byte) accountRowByUserID["Power"];
                    if ((num15 == 100) && (request == 1))
                    {
                        this.strSay = this.strNickName + "此球员体力已充沛，不需要再进行恢复了！";
                        return;
                    }
                    int num16 = (int) accountRowByUserID["ClubID"];
                    byte num17 = (byte) accountRowByUserID["Category"];
                    str = accountRowByUserID["Name"].ToString().Trim();
                    if ((num16 != this.intClubID5) || (num17 != 1))
                    {
                        this.strSay = this.strNickName + "经理，您好！此球员现在还没在为您的球队效力！";
                        return;
                    }
                }
                if (request == 3)
                {
                    if ((num10 - 650) > 0)
                    {
                        num += (num10 - 650) * 10;
                    }
                    else
                    {
                        num += (num10 - 650) * 5;
                    }
                    if (num < 500)
                    {
                        num = 500;
                    }
                }
                if (this.intWealth < num)
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您没有足够游戏币，使用此功能需要花费您 ", num, " 游戏币！" });
                }
                else
                {
                    this.btnOK.Visible = true;
                    switch (request)
                    {
                        case 1:
                            this.strSay = string.Concat(new object[] { "使用该功能，球员 ", str, " 的体力将恢复到100。[花费", num, "游戏币]" });
                            break;

                        case 2:
                            this.strSay = string.Concat(new object[] { "球员 ", str, " 的心情将恢复满。[花费 ", num, " 游戏币]" });
                            break;

                        case 3:
                            this.strSay = string.Concat(new object[] { "球员 ", str, " 年龄将降低1岁。[花费", num, " 游戏币]" });
                            break;

                        case 4:
                            this.strSay = string.Concat(new object[] { "使用伤病治愈后球员 ", str, " 将完全恢复健康。[花费", num, " 游戏币]" });
                            break;

                        default:
                            if (request == 5)
                            {
                                if (ToolItem.HasTool(this.intUserID, 13, 0) > 0)
                                {
                                    this.strSay = string.Concat(new object[] { "球员 ", str, " 扣除10点体力，有一定几率某项属性的潜力增加2并且所有的现有能力减少2。[花费", num, " 游戏币]" });
                                }
                                else
                                {
                                    this.strSay = "对不起，您还没有魔鬼训练的门票，拥有之后训练的效果将更好。门票只能从疯狂豆腐获得，赶紧去<a style='color:blue;cursor:pointer' onclick='javascript:window.location=\"ManagerTool.aspx?Type=STORE&Page=1\";' >买几块</a>咬咬吧！";
                                    this.btnOK.Visible = false;
                                }
                            }
                            break;
                    }
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ADDDATAONLY);
                }
            }
        }

        private void AddFriend()
        {
            string validWords = StringItem.GetValidWords(SessionItem.GetRequest("NickName", 1).ToString());
            if (!StringItem.IsValidName(validWords, 4, 0x10))
            {
                this.strSay = "<p align='center'>您填写的经理名称有错误！";
            }
            else if (validWords == this.strNickName)
            {
                this.strSay = "<p align='center'>您无需将自己添加为好友！</p>";
            }
            else
            {
                switch (BTPFriendManager.SetFriend(this.intUserID, validWords))
                {
                    case -1:
                        this.strSay = "<p align='center'>没有找到此经理！</p>";
                        return;

                    case 0:
                        this.strSay = "<p align='center'>您已经添加过此经理为好友！</p>";
                        return;

                    case 2:
                        this.strSay = "<p align='center'>您不能添加超过20个的好友！</p>";
                        return;
                }
                this.strSay = "<p align='center'>您的好友已经添加成功！</p>";
            }
        }

        private void AddGuessRecord()
        {
            this.tbSay.Visible = true;
            this.intUnionID = SessionItem.GetRequest("GuessID", 0);
            this.intType = SessionItem.GetRequest("ResultType", 0);
            if (this.intType != 0)
            {
                this.intType = 1;
            }
            DataRow guessRowByGuessID = BTPGuessManager.GetGuessRowByGuessID(this.intUnionID);
            if (guessRowByGuessID != null)
            {
                string str = guessRowByGuessID["NameA"].ToString().Trim().Replace("<br>", "");
                string str2 = guessRowByGuessID["NameB"].ToString().Trim().Replace("<br>", "");
                this.strSay = this.strNickName + "经理，您确定支持" + str + "？";
                if (this.intType != 0)
                {
                    this.strSay = this.strNickName + "经理，您确定支持" + str2 + "吗？";
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_AddGuessRecord);
            }
            else
            {
                this.strSay = "没有此场比赛或系统忙请重新尝试！";
            }
        }

        private void AddPlayerAll()
        {
            int num;
            int num2;
            this.tbSay.Visible = true;
            this.tblShowSkill.Visible = true;
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            int num3 = (int) parameterRow["AddPowerWealth"];
            int num4 = (int) parameterRow["AddHappyWealth"];
            parameterRow = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intWealth = (int) parameterRow["Wealth"];
            this.intCategory = SessionItem.GetRequest("Category", 0);
            if (this.intType == 3)
            {
                if (this.intCategory == 1)
                {
                    num = BTPPlayer3Manager.GetPlayer3PowerNotFullCount(this.intClubID3);
                    num2 = num3;
                }
                else
                {
                    num = BTPPlayer3Manager.GetPlayer3HappyNotFullCount(this.intClubID3);
                    num2 = num4;
                }
            }
            else if (this.intCategory == 1)
            {
                num = BTPPlayer5Manager.GetPlayer5PowerNotFullCount(this.intClubID5);
                num2 = num3;
            }
            else
            {
                num = BTPPlayer5Manager.GetPlayer5HappyNotFullCount(this.intClubID5);
                num2 = num4;
            }
            num2 = ((num2 * num) * 90) / 100;
            if (num > 0)
            {
                if (this.intWealth > num2)
                {
                    if (this.intType == 3)
                    {
                        if (this.intCategory == 1)
                        {
                            this.strSay = "通常情况下，每轮（夜间更新）过后球员的体力都会有所恢复。想要立即恢复全体球员的体力的话，则需要花费 " + num2 + " 游戏币。确认立即恢复体力吗？";
                        }
                        else
                        {
                            this.strSay = "通常情况下，每轮（夜间更新）过后球员的心情都会有所恢复。想要立即恢复全体球员的心情的话，则需要花费 " + num2 + " 游戏币。确认立即恢复心情吗？";
                        }
                    }
                    else
                    {
                        this.strSay = "通常，经过一夜的休息，球员将在下一轮联赛开始前恢复一部分体力。想要立即恢复全体球员的体力的话，则需要花费 " + num2 + " 游戏币。确认立即恢复体力吗？";
                    }
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_AddPlayerAll);
                }
                else
                {
                    this.strSay = "<p align='left'>您的游戏币不足 " + num2 + " 无法恢复！</p>";
                    this.btnOK.Visible = false;
                }
            }
            else
            {
                if (this.intCategory == 1)
                {
                    this.strSay = "您暂时没有球员需要恢复体力。";
                }
                else
                {
                    this.strSay = "您暂时没有球员需要恢复心情。";
                }
                this.btnOK.Visible = false;
            }
        }

        private void AddPower()
        {
            DataRow playerRowByPlayerID;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            this.intCheck = SessionItem.GetRequest("Check", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
            byte num1 = (byte) playerRowByPlayerID["Power"];
            int num = (int) playerRowByPlayerID["ClubID"];
            if (((this.intType == 3) && (num != this.intClubID3)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left'>此球员不在我们的球队中效力，无法对其使用理疗卡。</p>";
            }
            else if (((this.intType == 5) && (num != this.intClubID5)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left'>此球员不在我们的球队中效力，无法对其使用理疗卡。</p>";
            }
            else
            {
                int num2 = this.CanUseAddPower();
                if (num2 == 1)
                {
                    if (num2 == 1)
                    {
                        this.strToolMsg = "<p align='left'>您确定要对此球员使用理疗卡吗？球员的体力将会恢复到100，有机会清除所有伤病状态，并有机会让球员的年龄减少一岁！</p>";
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ADDPOWER);
                    }
                    else
                    {
                        this.strToolMsg = "<p align='left'>理疗卡已停止销售，相应功能请进入理疗中心选择使用。</p>";
                        this.btnOK.Visible = false;
                    }
                }
                else
                {
                    this.strToolMsg = "<p align='left'>理疗卡已停止销售，相应功能请进入理疗中心选择使用。</p>";
                    this.btnOK.Visible = false;
                }
            }
        }

        private void AfreshArrange()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Status", 0);
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"VArrange.aspx?Type=5\";'>";
            switch (request)
            {
                case -4:
                    this.strSay = "您只可以免费洗点一次！";
                    return;

                case -3:
                {
                    DataRow parameterRow = BTPParameterManager.GetParameterRow();
                    int num2 = (int) parameterRow["AfreshArrangeWealth"];
                    int num3 = (int) parameterRow["AfreshArrangeLowWealth"];
                    if (num3 < num2)
                    {
                        num2 = num3;
                    }
                    this.strSay = "您没有足够的游戏币，战术等级洗点需要 " + num2 + " 游戏币！";
                    return;
                }
                case -2:
                    this.strSay = "您现在没有战术熟练度，无需洗点！";
                    break;

                case -1:
                    this.strSay = "熟练度设置有误，请重新设置！";
                    return;

                case 0:
                    break;

                case 1:
                    this.strSay = "您的职业战术等级设置成功！";
                    return;

                default:
                    return;
            }
        }

        private void AgreeUnion()
        {
            int request = SessionItem.GetRequest("UnionID", 0);
            int num2 = SessionItem.GetRequest("UserID", 0);
            int num1 = SessionItem.GetRequest("MessageID", 0);
            int unionUserCountByID = BTPUnionManager.GetUnionUserCountByID(request);
            int num4 = 0;
            int num5 = 0;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                num5 = (int) accountRowByUserID["UnionID"];
            }
            accountRowByUserID = BTPUnionManager.GetUnionRowByID(request);
            if (accountRowByUserID != null)
            {
                num4 = (int) accountRowByUserID["Reputation"];
            }
            if (num4 < 1)
            {
                this.strSay = this.strNickName + "经理，此联盟已经濒临解散，您不能加入。";
            }
            else if (num5 != 0)
            {
                this.strSay = this.strNickName + "经理，您已经加入联盟，请先退出。";
            }
            else if (num2 != this.intUserID)
            {
                this.strSay = this.strNickName + "经理，请确定您是否被邀请者。";
            }
            else if (unionUserCountByID >= 600)
            {
                this.strSay = this.strNickName + "经理，该联盟已达到人数上限。";
            }
            else
            {
                this.strSay = this.strNickName + "经理，您是否同意加入该联盟？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_AgreeUnion);
            }
        }

        private void ArenaMemberReg()
        {
            this.arenaID = SessionItem.GetRequest("ArenaID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intUnionID = Convert.ToInt32(accountRowByUserID["UnionID"]);
                DataRow arenaRow = BTPArenaManager.GetArenaRow(this.arenaID);
                string str = Convert.ToString(arenaRow["ArenaName"]);
                int num = Convert.ToInt32(arenaRow["OwnerUnionID"]);
                int num2 = Convert.ToInt32(arenaRow["AttackUnionID"]);
                if (this.intUnionID == num)
                {
                    this.strSay = "你确定要报名参加" + str + "的保卫战吗？";
                }
                else if (this.intUnionID == num2)
                {
                    this.strSay = "你确定要报名参加" + str + "的抢夺战吗？";
                }
                else
                {
                    this.strSay = "你不符合报名的条件";
                    return;
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ArenaMemberReg);
            }
        }

        private void ArenaUnionReg()
        {
            this.arenaID = SessionItem.GetRequest("ArenaID", 0);
            if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                string str = Convert.ToString(BTPArenaManager.GetArenaRow(this.arenaID)["ArenaName"]);
                this.strSay = "你确定要报名抢夺" + str + "吗？";
                this.strSay = this.strSay + "这将花费联盟10威望";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ArenaUnionReg);
            }
        }

        private void BetXGuess()
        {
            this.intXGuessID = SessionItem.GetRequest("GuessID", 0);
            if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else if (BTPXGameManager.GetXGameRound() > 1)
            {
                this.strSay = "冠军杯决赛阶段已经开始，不能再竞猜";
            }
            else
            {
                this.tblXGuess.Visible = true;
                this.strSay = "请输入下注金额(大于10W小于100W)";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_BETXGUESS);
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
        }

        private void btnOK_Click_ADDADLINK(object sender, ImageClickEventArgs e)
        {
            int clubIDByUserID = BTPClubManager.GetClubIDByUserID(this.intUserID);
            int aDLCountByClubID = BTPADLinkManager.GetADLCountByClubID(clubIDByUserID);
            int num3 = (byte) BTPStadiumManager.GetStadiumRowByUserID(this.intUserID)["Levels"];
            DataRow stadiumRowByClubID = BTPStadiumManager.GetStadiumRowByClubID(this.intClubID5);
            int num4 = (int) stadiumRowByClubID["FansR"];
            int num5 = (int) stadiumRowByClubID["FansT"];
            int num6 = 0x3e8 + ((num4 + num5) / 4);
            int num7 = num6 / 0x3e8;
            int num8 = BTPADLinkManager.GetADLCountByClubID(this.intClubID5);
            if (num7 > num8)
            {
                if (num3 > aDLCountByClubID)
                {
                    BTPADLinkManager.AddADLink(clubIDByUserID, this.intADID);
                    base.Response.Redirect("Report.aspx?Parameter=125!Type.AD");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=124!Type.AD");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=126!Type.AD");
            }
        }

        private void btnOK_Click_ADDARRANGELVL(object sender, ImageClickEventArgs e)
        {
            int num;
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            if (parameterRow != null)
            {
                num = (int) parameterRow["AddArrangeLvlWealth"];
                this.btnOK.Visible = false;
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            if (this.intWealth < num)
            {
                this.strSay = this.strNickName + "经理，您好！您没有足够游戏币！";
            }
            else
            {
                int request = SessionItem.GetRequest("DevMatchID", 0);
                int num4 = BTPDevMatchManager.AddArrangeLvlByDevMatchID(this.intUserID, request);
                int num5 = SessionItem.GetRequest("Pos", 0);
                if (num4 != 1)
                {
                    switch (num4)
                    {
                        case -2:
                            this.strSay = this.strNickName + "经理，您好，您的球队没有参加这场职业联赛！";
                            return;

                        case -1:
                            this.strSay = this.strNickName + "经理，您好，您没有足够的游戏币！";
                            return;
                    }
                }
                else
                {
                    int num6 = SessionItem.GetRequest("Ref", 0);
                    switch (num5)
                    {
                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=904b!Type.5");
                            return;

                        case 3:
                            base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=904c!UserID.", this.intUserID, "^Ref.", num6 }));
                            return;
                    }
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=904!UserID.", this.intUserID, "^Devision.", this.strDevCode, "^Page.1" }));
                }
            }
        }

        private void btnOK_Click_ADDDATAONLY(object sender, ImageClickEventArgs e)
        {
            int num;
            int num2 = 0;
            int num3 = 0;
            int request = SessionItem.GetRequest("Category", 0);
            long longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            int intType = SessionItem.GetRequest("PlayerType", 0);
            switch (request)
            {
                case 3:
                case 5:
                {
                    this.strBtnCancel = "";
                    this.btnOK.Visible = true;
                    int num7 = 0x1b;
                    if (request == 3)
                    {
                        if (intType == 5)
                        {
                            DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerID);
                            if (playerRowByPlayerID != null)
                            {
                                num7 = Convert.ToInt32(playerRowByPlayerID["Age"]);
                                num2 = (int) playerRowByPlayerID["Ability"];
                            }
                        }
                        else
                        {
                            DataRow row2 = BTPPlayer3Manager.GetPlayerRowByPlayerID(longPlayerID);
                            if (row2 != null)
                            {
                                num7 = Convert.ToInt32(row2["Age"]);
                                num2 = (int) row2["Ability"];
                            }
                        }
                        if (num7 > 0x1b)
                        {
                            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_ag.GIF";
                        }
                        else
                        {
                            this.btnOK.Visible = false;
                        }
                    }
                    else
                    {
                        this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_ag.GIF";
                        this.btnOK.Visible = true;
                    }
                    this.btnOK.Width = 70;
                    this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?PlayerType=", intType, "&Type=9&PlayerID=", longPlayerID, "\";'>" });
                    break;
                }
                default:
                    this.btnOK.Visible = false;
                    this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?PlayerType=", intType, "&Type=9&PlayerID=", longPlayerID, "\";'>" });
                    break;
            }
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            int num8 = (int) parameterRow["AddPowerWealth"];
            int num9 = (int) parameterRow["AddHappyWealth"];
            int num10 = (int) parameterRow["MinusAgeWealth"];
            int num11 = (int) parameterRow["AddStatusWealth"];
            int num12 = (int) parameterRow["AddMaxWealth"];
            switch (request)
            {
                case 1:
                    num3 = num8;
                    break;

                case 2:
                    num3 = num9;
                    break;

                case 3:
                    num3 = num10;
                    break;

                case 4:
                    num3 = num11;
                    break;

                case 5:
                    num3 = num12;
                    break;

                default:
                    return;
            }
            if (intType == 3)
            {
                num = this.intClubID3;
            }
            else
            {
                num = this.intClubID5;
            }
            if (request == 3)
            {
                if ((num2 - 650) > 0)
                {
                    num3 += (num2 - 650) * 10;
                }
                else
                {
                    num3 += (num2 - 650) * 5;
                }
                if (num3 < 500)
                {
                    num3 = 500;
                }
            }
            if (this.intWealth < num3)
            {
                this.strSay = this.strNickName + "经理，您好！您没有足够游戏币。";
            }
            else
            {
                parameterRow = BTPToolLinkManager.AddDataOnly(longPlayerID, intType, num, request, 0);
                int num13 = Convert.ToInt32(parameterRow["State"]);
                int intAddType = Convert.ToInt32(parameterRow["AddType"]);
                switch (num13)
                {
                    case -5:
                        this.btnOK.Visible = false;
                        this.strSay = "对不起，您还没有魔鬼训练的门票，拥有之后训练的效果将更好。门票只能从疯狂豆腐获得，赶紧去<a onclick='javascript:window.location=\"ManagerTool.aspx?Type=STORE&Page=1\";' style='color:blue;cursor:pointer' >买几块</a>咬咬吧！";
                        break;

                    case -4:
                        this.btnOK.Visible = false;
                        this.strSay = this.strNickName + "您的球员已经很累了，现在还让他进行魔鬼训练您可真是魔鬼啊！";
                        return;

                    case -3:
                    case 0:
                        break;

                    case -2:
                        this.btnOK.Visible = false;
                        this.strSay = this.strNickName + "经理，您好，您没有足够游戏币！";
                        return;

                    case -1:
                        this.btnOK.Visible = false;
                        this.strSay = this.strNickName + "经理，您好，此球员没在您球队中效力！";
                        return;

                    case 1:
                        base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", intType, "&Kind=1&Check=1&PlayerID=", longPlayerID, "\";</script>" }));
                        this.ReturnOkSay(request, intAddType);
                        return;

                    default:
                        return;
                }
            }
        }

        private void btnOK_Click_AddGuessRecord(object sender, ImageClickEventArgs e)
        {
            long request = SessionItem.GetRequest("Money", 3);
            int num1 = SessionItem.GetRequest("ResultType", 0);
            switch (BTPGuessManager.AddGuessRecord(this.intUserID, this.intUnionID, request, this.intType))
            {
                case -7:
                    base.Response.Redirect("Report.aspx?Parameter=820");
                    return;

                case -6:
                    base.Response.Redirect("Report.aspx?Parameter=818");
                    return;

                case -5:
                    base.Response.Redirect("Report.aspx?Parameter=817");
                    return;

                case -4:
                    base.Response.Redirect("Report.aspx?Parameter=819");
                    return;

                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=814");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=813");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=812");
                    return;

                case 0:
                    base.Response.Redirect("Report.aspx?Parameter=811");
                    return;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=815!Type.GUESS");
                    return;
            }
        }

        private void btnOK_Click_AddPlayerAll(object sender, ImageClickEventArgs e)
        {
            this.btnOK.Visible = false;
            this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, ";\"'>" });
            if (BTPToolLinkManager.AddPlayerAll(this.intUserID, this.intType, this.intCategory) == 1)
            {
                base.Response.Write(string.Concat(new object[] { "<script>window.location=\"TrainPlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";</script>" }));
            }
            else
            {
                this.strSay = "您的游戏币不足！";
            }
        }

        private void btnOK_Click_ADDPOWER(object sender, ImageClickEventArgs e)
        {
            int num = Convert.ToInt32(this.Session["AddPower" + this.longPlayerID]);
            bool blnCanAdd = false;
            if ((num != 0) && ((num % 20) == 0))
            {
                blnCanAdd = true;
            }
            else
            {
                blnCanAdd = false;
            }
            BTPToolLinkManager.UseAddPower(this.intUserID, this.longPlayerID, this.intType, blnCanAdd);
            num++;
            this.Session["AddPower" + this.longPlayerID] = num;
            base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=1&PlayerID=", this.longPlayerID, "\";window.location=\"TrainPlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";</script>" }));
        }

        private void btnOK_Click_AgreeUnion(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UnionID", 0);
            int intUserID = SessionItem.GetRequest("UserID", 0);
            int intMessageID = SessionItem.GetRequest("MessageID", 0);
            int unionUserCountByID = BTPUnionManager.GetUnionUserCountByID(request);
            if (((((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) == 0) && (intUserID == this.intUserID)) && (unionUserCountByID < 600))
            {
                BTPUnionManager.IntoUnion(intUserID, request, intMessageID);
                DTOnlineManager.ChangeUnionIDByUserID(this.intUserID, request);
                base.Response.Redirect("Report.aspx?Parameter=416!Type.MYUNION^Kind.UNIONINFO^UnionID." + request + "^Page.1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=419!Type.UNION^Kind.VIEWUNION^Page.1");
            }
        }

        private void btnOK_Click_ArenaMemberReg(object sender, ImageClickEventArgs e)
        {
            int num = BTPArenaManager.ArenaMemberReg(this.intUserID, this.arenaID);
            base.Response.Redirect("Report.aspx?Parameter=ARENAREG" + num);
        }

        private void btnOK_Click_ArenaUnionReg(object sender, ImageClickEventArgs e)
        {
            int num = BTPArenaManager.ArenaUnionReg(this.intUserID, this.arenaID);
            base.Response.Redirect("Report.aspx?Parameter=ARENA" + num);
        }

        private void btnOK_Click_BETXGUESS(object sender, ImageClickEventArgs e)
        {
            int intAmount = 0;
            try
            {
                intAmount = Convert.ToInt32(this.tbXGuessAmount.Text);
            }
            catch (Exception exception)
            {
                intAmount = 0;
                exception.ToString();
                this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                return;
            }
            if (intAmount >= 0x186a0)
            {
                if (intAmount > 0xf4240)
                {
                    this.strSay = this.strNickName + "经理，单次最大只能竞猜1000000资金。";
                }
                else
                {
                    switch (BTPXGuessManager.BetXGuess(this.intUserID, this.intXGuessID, intAmount))
                    {
                        case -4:
                            base.Response.Redirect("Report.aspx?Parameter=XGE04");
                            return;

                        case -3:
                            base.Response.Redirect("Report.aspx?Parameter=XGE03");
                            return;

                        case -2:
                            base.Response.Redirect("Report.aspx?Parameter=XGE02");
                            return;

                        case -1:
                            this.strSay = this.strNickName + "经理，咱们俱乐部可没这么多资金啊。";
                            return;

                        case 0:
                            return;

                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=XGS01");
                            return;
                    }
                }
            }
            else
            {
                this.strSay = this.strNickName + "经理，您最少要下注100000资金。";
            }
        }

        private void btnOK_Click_BUYSTOCK(object sender, ImageClickEventArgs e)
        {
            switch (BTPStockManager.BuyStock(this.intUserID, this.intStockUserID))
            {
                case -6:
                    base.Response.Redirect("Report.aspx?Parameter=SE06");
                    return;

                case -5:
                    base.Response.Redirect("Report.aspx?Parameter=SE05");
                    return;

                case -4:
                    base.Response.Redirect("Report.aspx?Parameter=SE04");
                    return;

                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=SE03");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=SE02");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=SE01");
                    return;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=SS01");
                    break;

                default:
                    return;
            }
        }

        private void btnOK_Click_BuyTool(object sender, ImageClickEventArgs e)
        {
            int num;
            int num11;
            string strNickName = this.tbPresentText.Text.Trim();
            string str2 = this.tbCount.Text.Trim();
            int intCount = 1;
            if (StringItem.IsNumber(str2))
            {
                intCount = Convert.ToInt32(str2);
                if (intCount <= 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=207!Type.TOOLS^Page.1");
                }
            }
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow toolRowByID = BTPToolManager.GetToolRowByID(request);
            int intWealth = (int) toolRowByID["CoinCost"];
            string str3 = toolRowByID["ToolName"].ToString().Trim();
            int num6 = (int) toolRowByID["AmountInStock"];
            int intCategory = (byte) toolRowByID["Category"];
            int intTicketCategory = (byte) toolRowByID["TicketCategory"];
            if (strNickName != "")
            {
                if (!BTPAccountManager.HasNickName(strNickName))
                {
                    base.Response.Redirect("Report.aspx?Parameter=557!Type.BUYWEALTHTOOL^ToolID." + request);
                    return;
                }
                DataRow accountRowByNickName = BTPAccountManager.GetAccountRowByNickName(strNickName);
                this.intLookUserID = (int) accountRowByNickName["UserID"];
            }
            int num9 = this.intWealth;
            if (num9 < (intWealth * intCount))
            {
                base.Response.Redirect("Report.aspx?Parameter=203!Type.TOOLS^Page.1");
                return;
            }
            if (num6 <= 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=204!Type.TOOLS^Page.1");
                return;
            }
            switch (intCategory)
            {
                case 7:
                {
                    bool flag;
                    if (num9 < intWealth)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=203!Type.TOOLS^Page.1");
                        return;
                    }
                    if (strNickName != "")
                    {
                        toolRowByID = BTPAccountManager.GetAccountRowByUserID(this.intLookUserID);
                    }
                    else
                    {
                        toolRowByID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    }
                    int num12 = (byte) toolRowByID["PayType"];
                    DateTime datMemberExpireTime = (DateTime) toolRowByID["MemberExpireTime"];
                    if ((num12 == 0) && (datMemberExpireTime < DateTime.Now))
                    {
                        datMemberExpireTime = DateTime.Now.AddMonths(1);
                    }
                    else
                    {
                        datMemberExpireTime = datMemberExpireTime.AddMonths(1);
                    }
                    if (!DBLogin.CanConn(40))
                    {
                        flag = false;
                    }
                    else
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, "购买1个" + str3);
                        if (strNickName != "")
                        {
                            BTPToolLinkManager.BuyVIPCard(this.intLookUserID, datMemberExpireTime);
                        }
                        else
                        {
                            BTPToolLinkManager.BuyVIPCard(this.intUserID, datMemberExpireTime);
                        }
                        DataTable giftTable = BTPGiftManager.GetGiftTable();
                        string strContent = "购买会员卡";
                        int intUserID = 0;
                        if (strNickName != "")
                        {
                            intUserID = this.intUserID;
                            this.intUserID = this.intLookUserID;
                        }
                        if (giftTable != null)
                        {
                            foreach (DataRow row3 in giftTable.Rows)
                            {
                                int intToolID = (int) row3["ToolID"];
                                int num15 = (int) row3["Amount"];
                                int intType = (byte) row3["Type"];
                                if (intType == 3)
                                {
                                    BTPAccountManager.AddWealthByFinance(this.intUserID, num15, 1, "购买会员卡赠送！");
                                    object obj2 = strContent;
                                    strContent = string.Concat(new object[] { obj2, "获赠", num15, "枚游戏币！" });
                                }
                                else if ((intToolID == 0x21) && (intType == 1))
                                {
                                    this.intType = BTPToolLinkManager.BuyDoubleExperience(intToolID, this.intUserID, num15);
                                }
                                else
                                {
                                    BTPToolLinkManager.GiftTool(this.intUserID, intToolID, num15, intType);
                                    if (strContent.IndexOf("游戏币道具") == -1)
                                    {
                                        strContent = strContent + "获赠超值游戏币道具！";
                                    }
                                }
                            }
                        }
                        if (strNickName != "")
                        {
                            string text1 = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", datMemberExpireTime.ToString(), "' WHERE UserID=", this.intLookUserID });
                            this.intUserID = intUserID;
                            BTPMessageManager.AddMessage(this.intLookUserID, 2, 0, "秘书报告", AccountItem.GetNickNameInfoA(this.intUserID, this.strNickName, "Right", this.blnSex) + "赠送给你会员卡<br><br>请您重新登录享受会员特惠！");
                            BTPMessageManager.AddMessage(this.intUserID, 2, 0, "秘书报告", "您赠送给" + strNickName + "会员卡成功！");
                            BTPMessageManager.SetHasMsg(this.strNickName);
                            BTPMessageManager.SetHasMsg(strNickName);
                        }
                        else
                        {
                            BTPMessageManager.AddMessage(this.intUserID, 2, 0, "秘书报告", strContent);
                            BTPMessageManager.SetHasMsg(this.strNickName);
                            string text2 = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", datMemberExpireTime.ToString(), "' WHERE UserID=", this.intUserID });
                        }
                        flag = true;
                    }
                    if (flag)
                    {
                        DTOnlineManager.ChangePayTypeByUserID(this.intUserID, 1);
                        if (strNickName != "")
                        {
                            base.Response.Redirect("Report.aspx?Parameter=206!Type.TOOLS^Page.1");
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=402");
                        }
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=205!Type.TOOLS^Page.1");
                    }
                    goto Label_0BFE;
                }
                case 8:
                    num = 0;
                    switch (intTicketCategory)
                    {
                        case 1:
                            num = 200;
                            goto Label_0A13;

                        case 2:
                            num = 500;
                            goto Label_0A13;

                        case 3:
                            num = 0x640;
                            goto Label_0A13;
                    }
                    break;

                case 11:
                {
                    int num2 = 0;
                    switch (intTicketCategory)
                    {
                        case 1:
                            num2 = 0x1e8480;
                            break;

                        case 2:
                            num2 = 0xe4e1c0;
                            break;
                    }
                    if (strNickName == "")
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, "购买" + (num2 * intCount) + "游戏资金");
                        BTPAccountManager.AddMoneyWithFinance(num2 * intCount, this.intUserID, 3, "购买" + (num2 * intCount) + "游戏资金", intCategory, intTicketCategory, intCount);
                    }
                    goto Label_0BFE;
                }
                case 0x10:
                    switch (BTPToolLinkManager.BuyNewerToolBox(request, this.intUserID))
                    {
                        case -2:
                            base.Response.Redirect("Report.aspx?Parameter=BNE02!Type.TOOLS^Page.1");
                            return;

                        case -1:
                            base.Response.Redirect("Report.aspx?Parameter=BNE01!Type.TOOLS^Page.1");
                            return;
                    }
                    BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth, 2, string.Concat(new object[] { "购买新人大礼包" }));
                    base.Response.Redirect("Report.aspx?Parameter=BNS01!Type.TOOLS^Page.1");
                    return;

                case 9:
                case 10:
                    if (intCategory == 9)
                    {
                        intCount = 1;
                    }
                    if (strNickName == "")
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, string.Concat(new object[] { "购买", intCount, "个", str3 }));
                        num11 = BTPToolLinkManager.BuyDoubleExperience(request, this.intUserID, intCount);
                    }
                    else
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, string.Concat(new object[] { "赠送给", strNickName, str3, intCount, "个" }));
                        num11 = BTPToolLinkManager.BuyDoubleExperience(request, this.intLookUserID, intCount);
                        BTPMessageManager.AddMessage(this.intLookUserID, 2, 0, "秘书报告", string.Concat(new object[] { AccountItem.GetNickNameInfoA(this.intUserID, this.strNickName, "Right", this.blnSex), "赠送给您", str3, " ", intCount, "个。" }));
                        BTPMessageManager.SetHasMsg(strNickName);
                    }
                    if (num11 == 2)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=561");
                    }
                    goto Label_0BFE;

                case 0xc9:
                    if (strNickName == "")
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, string.Concat(new object[] { "购买", intCount, "个", str3 }));
                        num11 = BTPToolLinkManager.BuyLocationCard(request, this.intUserID, intCount);
                    }
                    else
                    {
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, string.Concat(new object[] { "赠送给", strNickName, str3, intCount, "个" }));
                        BTPToolLinkManager.BuyLocationCard(request, this.intLookUserID, intCount);
                        BTPMessageManager.AddMessage(this.intLookUserID, 2, 0, "秘书报告", string.Concat(new object[] { AccountItem.GetNickNameInfoA(this.intUserID, this.strNickName, "Right", this.blnSex), "赠送给您", str3, " ", intCount, "个。" }));
                        BTPMessageManager.SetHasMsg(strNickName);
                    }
                    goto Label_0BFE;

                default:
                    if (strNickName == "")
                    {
                        if (intCategory == 2)
                        {
                            intCount = 1;
                        }
                        BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth * intCount, 2, "购买1个" + str3);
                        if (BTPToolLinkManager.BuyTool(request, this.intUserID, DateTime.Now.AddDays(20.0), intCount) == -1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=208!Type.TOOLS^Page.1");
                            return;
                        }
                    }
                    else
                    {
                        ROOTUserManager.SpendCoin40(this.intUserID, intWealth * intCount, "赠送给" + strNickName + str3 + "1个", "");
                        BTPToolLinkManager.BuyTool(request, this.intLookUserID, DateTime.Now.AddDays(20.0), intCount);
                        BTPMessageManager.AddMessage(this.intLookUserID, 2, 0, "秘书报告", string.Concat(new object[] { AccountItem.GetNickNameInfoA(this.intUserID, this.strNickName, "Right", this.blnSex), "赠送给您", str3, intCount, "个。" }));
                        BTPMessageManager.SetHasMsg(strNickName);
                    }
                    goto Label_0BFE;
            }
        Label_0A13:
            if (strNickName == "")
            {
                ROOTUserManager.SpendCoin40(this.intUserID, intWealth * intCount, "购买" + num + "游戏币", "");
                BTPAccountManager.AddWealth(this.intUserID, num * intCount, string.Concat(new object[] { "成功购买", intCount, "个", str3, "，获得", num * intCount, "游戏币。" }), 8, intTicketCategory, intCount);
            }
            else
            {
                ROOTUserManager.SpendCoin40(this.intUserID, intWealth * intCount, string.Concat(new object[] { "赠送给", strNickName, str3, intCount, "个" }), "");
                BTPAccountManager.AddWealth(this.intLookUserID, num * intCount, string.Concat(new object[] { this.strNickName, "赠送给您", str3, intCount, "个。" }), 8, intTicketCategory, intCount);
                BTPMessageManager.AddMessage(this.intLookUserID, 2, 0, "秘书报告", AccountItem.GetNickNameInfoA(this.intUserID, this.strNickName, "Right", this.blnSex) + "赠送给您" + str3 + "1个。");
                BTPMessageManager.SetHasMsg(strNickName);
            }
        Label_0BFE:
            base.Response.Redirect("Report.aspx?Parameter=202!Type.TOOLS^Page.1");
        }

        private void btnOK_Click_BUYWEALTHPLAYER(object sender, ImageClickEventArgs e)
        {
            DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            int num = (byte) playerRowByPlayerID["Category"];
            string str = playerRowByPlayerID["Name"].ToString().Trim();
            if (((bool) playerRowByPlayerID["SellAss"]) && (this.intPayType != 1))
            {
                this.strSay = this.strNickName + "经理，您好！球员" + str + "只对会员出售，请加入会员获得更多服务！";
            }
            else
            {
                int num2;
                long num3;
                switch (num)
                {
                    case 2:
                        num2 = 3;
                        break;

                    case 3:
                        num2 = 5;
                        break;

                    case 4:
                        num2 = 4;
                        break;

                    default:
                        num2 = 0;
                        return;
                }
                long num12 = this.CheckCanBid(5, 8);
                if ((num12 <= -3L) && (num12 >= -12L))
                {
                    switch (((int) (num12 - -12L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，咱们俱乐部工资总和已经达到或超过工资帽了不能再购买球员了！";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！此球员无法跨等级进行转会！";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员已经不在拍卖中！";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 7:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 8:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 9:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                try
                {
                    num3 = Convert.ToInt64(this.tbFreeBidPrice.Text);
                }
                catch (Exception exception)
                {
                    num3 = 0L;
                    exception.ToString();
                    this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                    return;
                }
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                int num4 = (int) accountRowByUserID["Wealth"];
                accountRowByUserID["DevCode"].ToString().Trim();
                int num1 = (int) accountRowByUserID["ClubID5"];
                if (num4 >= num3)
                {
                    string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                    string strCreateTime = DateTime.Now.ToString();
                    DataRow devTranTopUser = BTPTransferManager.GetDevTranTopUser(this.longPlayerID, true);
                    if (devTranTopUser != null)
                    {
                        int num5 = (int) devTranTopUser["UserID"];
                        long num6 = (int) devTranTopUser["Price"];
                        long num7 = (num6 * 0x66L) / 100L;
                        long num8 = (num6 * 13L) / 10L;
                        if (num5 == this.intUserID)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=692!Type.TRANSFER^Pos.1^Order.0^Page.1");
                        }
                        else if ((num7 > num3) || (num8 < num3))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=693");
                        }
                        else
                        {
                            DateTime time = (DateTime) BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID)["EndBidTime"];
                            if (time < DateTime.Now.AddMinutes(1.0))
                            {
                                BTPPlayer5Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(1.0));
                            }
                            BTPTransferManager.SetDevisionTran(this.longPlayerID, this.intUserID, num3, strCreateTime, strIP, num2);
                            base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                        }
                    }
                    else
                    {
                        DataRow row4 = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                        long num9 = Convert.ToInt64(row4["BidPrice"]);
                        long num10 = (num9 * 13L) / 10L;
                        if ((num9 > num3) || (num10 < num3))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=693");
                        }
                        else
                        {
                            DateTime time2 = (DateTime) row4["EndBidTime"];
                            if (time2 < DateTime.Now.AddMinutes(1.0))
                            {
                                BTPPlayer5Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(1.0));
                            }
                            BTPTransferManager.SetDevisionTran(this.longPlayerID, this.intUserID, num3, strCreateTime, strIP, num2);
                            base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                        }
                    }
                }
                else
                {
                    this.strSay = this.strNickName + "经理，咱这么出价会被人笑话的，起码高于1000吧？";
                }
            }
        }

        private void btnOK_Click_BuyWealthTool(object sender, ImageClickEventArgs e)
        {
            string strNickName = this.tbPresentText.Text.Trim();
            string str2 = this.tbCount.Text.Trim();
            int intCount = 1;
            if (StringItem.IsNumber(str2))
            {
                intCount = Convert.ToInt32(str2);
                if (intCount <= 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=207!Type.TOOLS^Page.1");
                }
            }
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow wealthToolRowByID = BTPWealthToolManager.GetWealthToolRowByID(request);
            int num3 = (int) wealthToolRowByID["WealthCost"];
            int num4 = (int) wealthToolRowByID["PayCost"];
            wealthToolRowByID["ToolName"].ToString().Trim();
            int num5 = (int) wealthToolRowByID["AmountInStock"];
            int num6 = (byte) wealthToolRowByID["Category"];
            byte num1 = (byte) wealthToolRowByID["TicketCategory"];
            string str3 = "";
            if (this.intPayType == 1)
            {
                num3 = num4;
            }
            if (this.intWealth >= (num3 * intCount))
            {
                if (num5 > 0)
                {
                    if (num6 == 7)
                    {
                        wealthToolRowByID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                        int num7 = (byte) wealthToolRowByID["PayType"];
                        DateTime datMemberExpireTime = (DateTime) wealthToolRowByID["MemberExpireTime"];
                        int num8 = (int) wealthToolRowByID["Wealth"];
                        str3 = wealthToolRowByID["NickName"].ToString().Trim();
                        if (strNickName != "")
                        {
                            if (!BTPAccountManager.HasNickName(strNickName))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=557!Type.BUYWEALTHTOOL^ToolID." + request);
                                return;
                            }
                            int intUserID = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["UserID"];
                            wealthToolRowByID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                            num7 = (byte) wealthToolRowByID["PayType"];
                            datMemberExpireTime = (DateTime) wealthToolRowByID["MemberExpireTime"];
                            str3 = wealthToolRowByID["NickName"].ToString().Trim();
                        }
                        if (num8 < (num3 * intCount))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=203!Type.TOOLS^Page.1");
                        }
                        else
                        {
                            string str4;
                            if ((num7 == 0) || (datMemberExpireTime < DateTime.Now))
                            {
                                datMemberExpireTime = DateTime.Now.AddDays(15.0);
                            }
                            else
                            {
                                datMemberExpireTime = datMemberExpireTime.AddDays(15.0);
                            }
                            if (strNickName != "")
                            {
                                if (!BTPAccountManager.HasNickName(this.tbPresentText.Text.Trim()))
                                {
                                    base.Response.Redirect("Report.aspx?Parameter=557!Type.BUYWEALTHTOOL^ToolID." + request);
                                    return;
                                }
                                BTPToolLinkManager.BuyWealthVIPCard(this.intUserID, datMemberExpireTime, strNickName, 2);
                                BTPMessageManager.SetHasMsg(str3);
                                int num10 = (int) BTPAccountManager.GetAccountRowByNickName(this.tbPresentText.Text.Trim())["UserID"];
                                str4 = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", datMemberExpireTime.ToString(), "' WHERE UserID=", num10 });
                            }
                            else
                            {
                                BTPToolLinkManager.BuyWealthVIPCard(this.intUserID, datMemberExpireTime, strNickName, 1);
                                str4 = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", datMemberExpireTime.ToString(), "' WHERE UserID=", this.intUserID });
                            }
                            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, str4);
                            DTOnlineManager.ChangePayTypeByUserID(this.intUserID, 1);
                            if (strNickName != "")
                            {
                                base.Response.Redirect("Report.aspx?Parameter=202!Type.TOOLS^Page.1");
                            }
                            else
                            {
                                base.Response.Redirect("Report.aspx?Parameter=402");
                            }
                        }
                    }
                    else
                    {
                        if (strNickName != "")
                        {
                            if (!BTPAccountManager.HasNickName(this.tbPresentText.Text.Trim()))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=557!Type.BUYWEALTHTOOL^ToolID." + request);
                                return;
                            }
                            BTPToolLinkManager.BuyWealthTool(request, this.intUserID, DateTime.Now.AddDays(20.0), strNickName, 2, intCount);
                            BTPMessageManager.SetHasMsg(str3);
                        }
                        else
                        {
                            BTPToolLinkManager.BuyWealthTool(request, this.intUserID, DateTime.Now.AddDays(20.0), strNickName, 1, intCount);
                        }
                        base.Response.Redirect("Report.aspx?Parameter=202!Type.TOOLS^Page.1");
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=204!Type.TOOLS^Page.1");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=203!Type.TOOLS^Page.1");
            }
        }

        private void btnOK_Click_CANCELFOCUS(object sender, ImageClickEventArgs e)
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            int intCategory = SessionItem.GetRequest("Category", 0);
            BTPBidFocusManager.CancelFocus(this.intUserID, request, intCategory);
            base.Response.Redirect("MyFocus.aspx");
        }

        private void btnOK_Click_CancelOrder(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("OrderID", 0);
            switch (BTPOrderManager.CancelOrder(request, this.intUserID))
            {
                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=806");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=805");
                    return;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=807!Type.WEALTHWEALLTH");
                    break;

                default:
                    return;
            }
        }

        private void btnOK_Click_CancelPrearrange(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            base.Response.Redirect("SecretaryPage.aspx?Type=CANCELPREARR&Tag=" + request + "");
        }

        private void btnOK_Click_CancelPrearrangeA(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            BTPDevMatchManager.CancelPrearrange(this.intClubID5, request);
            base.Response.Redirect(string.Concat(new object[] { "DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1" }));
        }

        private void btnOK_Click_CancelPrearrangeAXBA(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            BTPXGroupMatchManager.CancelChampionArrange(this.intClubID5, request);
            base.Response.Redirect("ChampionCup.aspx?Kind=TRYOUT&Index=" + (request + 2));
        }

        private void btnOK_Click_CancelPrearrangeXBA(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            base.Response.Redirect("SecretaryPage.aspx?Type=CANCELPREARRXBA&Tag=" + request + "");
        }

        private void btnOK_Click_CHOOSE(object sender, ImageClickEventArgs e)
        {
            int num = 0;
            DataRow row = BTPToolLinkManager.GetToolByUserIDTCategory(this.intUserID, 6, 0);
            if (row != null)
            {
                num = (int) row["Amount"];
            }
            if (num <= 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=6970");
            }
            else
            {
                string marketCodeByClubID = BTPClubManager.GetMarketCodeByClubID(this.intClubID5);
                int intNumber = PlayerItem.GetGoodNum5(this.intClubID5);
                BTPPlayer5Manager.SetPlayer3to5(this.intClubID5, this.longPlayerID, intNumber, marketCodeByClubID);
                BTPToolLinkManager.DeleteTicket(this.intUserID, 6, 0);
                string str2 = this.strClubName5;
                string strLogEvent = "球员" + this.strPlayer5Name + "通过" + str2 + "的队内选拔，成为职业球员。";
                BTPDevManager.SetDevLogByClubID5(this.intClubID5, strLogEvent);
                base.Response.Redirect("Report.aspx?Parameter=P101!Type.5");
            }
        }

        private void btnOK_Click_CHOOSECLUB(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbClubName.Text.Trim();
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            string pToEncrypt = accountRowByUserID["UserName"].ToString().Trim();
            string str3 = accountRowByUserID["Password"].ToString().Trim();
            strIn = StringItem.GetValidWords(strIn);
            if (BTPAccountManager.HasClubName(strIn) && !StringItem.IsValidName(strIn, 2, 20))
            {
                base.Response.Redirect("SecretaryPage.aspx?Tag=1&Type=CHOOSECLUB&UserID=" + this.intLookUserID);
            }
            int num = BTPAccountManager.ReplaceClub(this.intUserID, this.intLookUserID, strIn);
            if (num == 1)
            {
                pToEncrypt = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
                str3 = StringItem.MD5Encrypt(str3, Global.strMD5Key);
                if (SessionItem.SetSelfLogin(pToEncrypt, str3, false) > 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=558");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=10");
                }
            }
            else if (num == 2)
            {
                base.Response.Redirect("Report.aspx?Parameter=560");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=559!Type.CHOOSECLUB");
            }
        }

        private void btnOK_Click_CreateDevCup(object sender, ImageClickEventArgs e)
        {
            string strCupName;
            string strPassword;
            string strDevCupIntro;
            string strLogo;
            string strRequirementXML;
            string strRewardXML;
            string strCupLadder;
            int intUnionID;
            int intRegClub;
            int intRegCharge;
            int intCupSize;
            int intCreateCharge;
            int intMedalCharge;
            int intHortationAll;
            DateTime datEndTime;
            int num8 = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionCategory"];
            DevCupData data = (DevCupData) this.Session["DevCup" + this.intUnionID];
            if (data != null)
            {
                strCupName = data.strCupName;
                intUnionID = data.intUnionID;
                intRegClub = data.intRegClub;
                strPassword = data.strPassword;
                strDevCupIntro = data.strDevCupIntro;
                intRegCharge = data.intRegCharge;
                strLogo = data.strLogo;
                strRequirementXML = data.strRequirementXML;
                strRewardXML = data.strRewardXML;
                intCupSize = data.intCupSize;
                datEndTime = data.datEndTime;
                intCreateCharge = data.intCreateCharge;
                intMedalCharge = data.intMedalCharge;
                intHortationAll = data.intHortationAll;
                strCupLadder = data.strCupLadder;
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=610!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                return;
            }
            this.intType = BTPDevCupManager.CreateDevCup(this.intUserID, intUnionID, intRegClub, strCupName, strPassword, strDevCupIntro, intRegCharge, strLogo, strRequirementXML, strRewardXML, intCupSize, datEndTime, intCreateCharge, intMedalCharge, intHortationAll, strCupLadder);
            if (this.intType == 0)
            {
                if (num8 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=611!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=611!Type.MYUNION^Kind.CREATECUP^Page.1");
                }
            }
            else if (this.intType == 1)
            {
                if (num8 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=606!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=606!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                }
            }
            else if (this.intType == 2)
            {
                if (num8 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=612!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=612!Type.MYUNION^Kind.CREATECUP^Page.1");
                }
            }
            else if (this.intType == 3)
            {
                if (num8 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=613!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=613!Type.MYUNION^Kind.CREATECUP^Page.1");
                }
            }
            else if (this.intType == 9)
            {
                if (num8 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=6147!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=6147!Type.MYUNION^Kind.CREATECUP^Page.1");
                }
            }
            else if (num8 == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=614!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=614!Type.MYUNION^Kind.CREATECUP^Page.1");
            }
        }

        private void btnOK_Click_CreateUserCup(object sender, ImageClickEventArgs e)
        {
            string strCupName;
            string strPassword;
            string strDevCupIntro;
            string strLogo;
            string strRequirementXML;
            string strRewardXML;
            string strCupLadder;
            int intUnionID;
            int intRegClub;
            int intRegCharge;
            int intCupSize;
            int intCreateCharge;
            int intMedalCharge;
            int intHortationAll;
            DateTime datEndTime;
            DevCupData data = (DevCupData) this.Session["DevCup" + this.intUserID];
            if (data != null)
            {
                strCupName = data.strCupName;
                intUnionID = data.intUnionID;
                intRegClub = data.intRegClub;
                strPassword = data.strPassword;
                strDevCupIntro = data.strDevCupIntro;
                intRegCharge = data.intRegCharge;
                strLogo = data.strLogo;
                strRequirementXML = data.strRequirementXML;
                strRewardXML = data.strRewardXML;
                intCupSize = data.intCupSize;
                datEndTime = data.datEndTime;
                intCreateCharge = data.intCreateCharge;
                intMedalCharge = data.intMedalCharge;
                intHortationAll = data.intHortationAll;
                strCupLadder = data.strCupLadder;
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=626!Type.CREATEDEVCUP^Status.CREATE");
                return;
            }
            this.intType = BTPDevCupManager.CreateUserDevCup(this.intUserID, intUnionID, intRegClub, strCupName, strPassword, strDevCupIntro, intRegCharge, strLogo, strRequirementXML, strRewardXML, intCupSize, datEndTime, intCreateCharge, intMedalCharge, intHortationAll, strCupLadder);
            if (this.intType == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=623!Type.CREATEDEVCUP^Status.MANAGER");
            }
            else if (this.intType == 2)
            {
                base.Response.Redirect("Report.aspx?Parameter=624!Type.DEVCUPREG");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=625!Type.CREATEDEVCUP^Status.CREATE");
            }
        }

        private void btnOK_Click_CreateWealthOrder(object sender, ImageClickEventArgs e)
        {
            int num;
            int num2;
            int num3;
            if (this.intType == 1)
            {
                num = 1;
                if (!StringItem.IsNumber(this.tbBuyPrice.Text) || !StringItem.IsNumber(this.tbBuyCount.Text))
                {
                    base.Response.Redirect("Report.aspx?Parameter=800");
                    return;
                }
                num2 = Convert.ToInt32(this.tbBuyPrice.Text);
                num3 = Convert.ToInt32(this.tbBuyCount.Text);
            }
            else
            {
                num = 2;
                if (StringItem.IsNumber(this.tbPrice.Text) && StringItem.IsNumber(this.tbBCount.Text))
                {
                    num2 = Convert.ToInt32(this.tbPrice.Text);
                    num3 = Convert.ToInt32(this.tbBCount.Text);
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=800");
                    return;
                }
            }
            if (num2 < 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=809");
            }
            else
            {
                DataRow row = BTPOrderManager.AddWealthOrder(this.intUserID, num, num2, num3, 0);
                int num4 = (int) row["Count"];
                long num5 = (long) row["AllMoney"];
                switch (num4)
                {
                    case -6:
                        base.Response.Redirect("Report.aspx?Parameter=810");
                        return;

                    case -5:
                        base.Response.Redirect("Report.aspx?Parameter=802");
                        return;

                    case -4:
                        base.Response.Redirect("Report.aspx?Parameter=808");
                        return;

                    case -3:
                        base.Response.Redirect("Report.aspx?Parameter=804");
                        return;

                    case -2:
                        base.Response.Redirect("Report.aspx?Parameter=490");
                        return;

                    case -1:
                        base.Response.Redirect("Report.aspx?Parameter=801");
                        return;

                    case 0:
                        base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=ORDERSAY&Category=", num, "&Price=", num2, "&Wealth=", num3 }));
                        return;
                }
                if (num4 > 0)
                {
                    base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=ORDERSAY&Money=", num5, "&Count=", num4, "&Category=", num, "&Price=", num2, "&Wealth=", num3 }));
                }
            }
        }

        private void btnOK_Click_DeleteDevCup(object sender, ImageClickEventArgs e)
        {
            int num = BTPDevCupManager.DeleteDevCup(this.intUserID, this.intDevCupID);
            byte num2 = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionCategory"];
            switch (num)
            {
                case 0:
                    if (num2 != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=600!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=600!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                    return;

                case 1:
                    if (num2 != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=601!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=601!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                    return;

                case 2:
                    if (num2 != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=602!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=602!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                    return;

                case 3:
                    if (num2 != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=603!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=603!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                    return;

                case 4:
                    if (num2 != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=604!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=604!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void btnOK_Click_DeleteUserDevCup(object sender, ImageClickEventArgs e)
        {
            switch (BTPDevCupManager.DeleteUserDevCup(this.intUserID, this.intDevCupID))
            {
                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=628!Type.CREATEDEVCUP^Status.MANAGER^Page.1");
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=629!Type.CREATEDEVCUP^Status.MANAGER^Page.1");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=631!Type.CREATEDEVCUP^Status.MANAGER^Page.1");
                    return;

                case 4:
                    base.Response.Redirect("Report.aspx?Parameter=630!Type.CREATEDEVCUP^Status.MANAGER^Page.1");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void btnOK_Click_DEVBIDHELPER(object sender, ImageClickEventArgs e)
        {
            long num = this.CheckCanBid(5, 10);
            if ((this.intPayType == 1) && (num > 0L))
            {
                int num2;
                try
                {
                    num2 = Convert.ToInt32(this.tbMaxPrice.Text);
                }
                catch
                {
                    num2 = 0;
                    this.strBidMsg = "最高价格填写错误！";
                    return;
                }
                if (num2 >= ((num / 10L) * 15L))
                {
                    if (!BTPAccountManager.HasEnoughMoney(this.intUserID, num2))
                    {
                        this.strBidMsg = "没有足够的资金。";
                    }
                    else
                    {
                        BTPToolLinkManager.UseBidHelper(this.intUserID, this.intMarket, this.longPlayerID, num2);
                        base.Response.Redirect("Report.aspx?Parameter=691a!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                    }
                }
                else
                {
                    this.strBidMsg = "委托金额不足。";
                }
            }
        }

        private void btnOK_Click_DEVCHOOSE(object sender, ImageClickEventArgs e)
        {
            if (BTPPlayer5Manager.GetSellClub5PCount(this.intClubID5) > 14)
            {
                base.Response.Redirect("Report.aspx?Parameter=6996!Type.DEVCHOOSE^Pos.1^Order.0^Page.1");
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                playerRowByPlayerID["Name"].ToString().Trim();
                DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
                int num = (int) playerRowByPlayerID["BidderID"];
                if ((time <= DateTime.Now.AddHours(3.0)) && (num > 0))
                {
                    base.Response.Redirect("Report.aspx?Parameter=6994!Type.DEVCHOOSE^Pos.1^Order.0^Page.1");
                }
                else if (time <= DateTime.Now)
                {
                    base.Response.Redirect("Report.aspx?Parameter=6995!Type.DEVCHOOSE^Pos.1^Order.0^Page.1");
                }
                else
                {
                    switch (BTPPlayer5Manager.PickPlayer(this.longPlayerID, this.intUserID))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=6990!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                            return;

                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=6991!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=6992!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=6993!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
            }
        }

        private void btnOK_Click_DEVISION(object sender, ImageClickEventArgs e)
        {
            DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            int num = (byte) playerRowByPlayerID["Category"];
            string str = playerRowByPlayerID["Name"].ToString().Trim();
            if (((bool) playerRowByPlayerID["SellAss"]) && (this.intPayType != 1))
            {
                this.strSay = this.strNickName + "经理，您好！球员" + str + "只对会员出售，请加入会员获得更多服务！";
            }
            else
            {
                int num2;
                long num3;
                switch (num)
                {
                    case 2:
                        num2 = 3;
                        break;

                    case 3:
                        num2 = 5;
                        break;

                    case 4:
                        num2 = 4;
                        break;

                    default:
                        num2 = 0;
                        return;
                }
                long num10 = this.CheckCanBid(5, 4);
                if ((num10 <= -3L) && (num10 >= -12L))
                {
                    switch (((int) (num10 - -12L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，咱们俱乐部工资总和已经达到或超过工资帽了不能再购买球员了！";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！此球员无法跨等级进行转会！";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员已经不在拍卖中！";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 7:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 8:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 9:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                try
                {
                    num3 = Convert.ToInt64(this.tbFreeBidPrice.Text);
                }
                catch (Exception exception)
                {
                    num3 = 0L;
                    exception.ToString();
                    this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                    return;
                }
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                long num4 = (long) accountRowByUserID["Money"];
                accountRowByUserID["DevCode"].ToString().Trim();
                int num1 = (int) accountRowByUserID["ClubID5"];
                if (num4 >= num3)
                {
                    string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                    string strCreateTime = DateTime.Now.ToString();
                    DataRow devTranTopUser = BTPTransferManager.GetDevTranTopUser(this.longPlayerID, true);
                    if (devTranTopUser != null)
                    {
                        int num5 = (int) devTranTopUser["UserID"];
                        long num7 = (Convert.ToInt64(devTranTopUser["Price"]) / 100L) * 0x66L;
                        if (num5 == this.intUserID)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=692!Type.TRANSFER^Pos.1^Order.0^Page.1");
                        }
                        else if (num7 > num3)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=693");
                        }
                        else
                        {
                            DateTime time = (DateTime) BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID)["EndBidTime"];
                            if (time < DateTime.Now.AddMinutes(2.0))
                            {
                                BTPPlayer5Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(2.0));
                            }
                            BTPTransferManager.SetDevisionTran(this.longPlayerID, this.intUserID, num3, strCreateTime, strIP, num2);
                            base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                        }
                    }
                    else
                    {
                        DataRow row4 = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                        if (Convert.ToInt64(row4["BidPrice"]) > num3)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=693");
                        }
                        else
                        {
                            DateTime time2 = (DateTime) row4["EndBidTime"];
                            if (time2 < DateTime.Now.AddMinutes(2.0))
                            {
                                BTPPlayer5Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(2.0));
                            }
                            BTPTransferManager.SetDevisionTran(this.longPlayerID, this.intUserID, num3, strCreateTime, strIP, num2);
                            base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                        }
                    }
                }
                else
                {
                    this.strSay = this.strNickName + "经理，咱没有足够多的资金对该球员进行出价。";
                }
            }
        }

        private void btnOK_Click_DEVSTREETBID(object sender, ImageClickEventArgs e)
        {
            int num = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Category"];
            if (num != 5)
            {
                this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
            }
            else
            {
                int num2;
                long num5 = this.CheckCanBid(3, 2);
                if ((num5 <= -3L) && (num5 >= -9L))
                {
                    switch (((int) (num5 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                try
                {
                    num2 = Convert.ToInt32(this.tbFreeBidPrice.Text);
                }
                catch (Exception exception)
                {
                    num2 = 0;
                    exception.ToString();
                    this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                    return;
                }
                long num3 = (long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Money"];
                int num4 = Convert.ToInt32(this.tbFreeBidPrice.Text);
                if (num4 >= 0x3e8)
                {
                    if (num3 < num4)
                    {
                        this.strSay = this.strNickName + "经理，咱们俱乐部已经没有太多的资金了，怎么能出这么高的价钱呢？";
                    }
                    else
                    {
                        DateTime time = (DateTime) BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID)["EndBidTime"];
                        if (time < DateTime.Now.AddMinutes(1.0))
                        {
                            BTPPlayer3Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(1.0));
                        }
                        string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                        string strCreateTime = DateTime.Now.ToString();
                        BTPTransferManager.SetFreeBid(this.intUserID, this.longPlayerID, 3, 2, num2, strCreateTime, strIP);
                        base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                    }
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您就这么小气，起码要超过1000元吧？";
                }
            }
        }

        private void btnOK_Click_EXTENDSTAFF(object sender, ImageClickEventArgs e)
        {
            if (this.intPayType != 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=85!Type.STREET");
            }
            else
            {
                this.intContract = 5;
                string strEvent = "";
                int intDCategory = 3;
                string url = "StaffManage.aspx?Type=STREET";
                if (this.intClubIDS == this.intClubID3)
                {
                    this.intClubID = this.intClubID3;
                    intDCategory = 3;
                    strEvent = "街球队职员续约";
                    url = "StaffManage.aspx?Type=STREET";
                }
                else if (this.intClubIDS == this.intClubID5)
                {
                    this.intClubID = this.intClubID5;
                    intDCategory = 5;
                    strEvent = "职业队职员续约";
                    url = "StaffManage.aspx?Type=PRO";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=86!Type.STREET");
                    return;
                }
                if (this.intClubID == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=82");
                }
                else if (BTPStaffManager.HasEnoughMoney(this.intUserID, this.intSalary * this.intContract))
                {
                    BTPStaffManager.ExtendStaff(this.intStaffID, this.intContract, this.intSalary, this.intClubID, intDCategory, strEvent);
                    base.Response.Redirect(url);
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=81");
                }
            }
        }

        private void btnOK_Click_FIELDDEF(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            int request = SessionItem.GetRequest("CID", 0);
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"UnionField.aspx?Type=FIELDLIST&Page=1\";'>";
            switch (BTPUnionFieldManager.UnionFieldDef(this.intUserID, request))
            {
                case -4:
                    this.strSay = "您没有职业队！";
                    return;

                case -3:
                    this.strSay = "您已经参加过一场盟战比赛！";
                    return;

                case -2:
                    this.strSay = "对不起，您的可支配联盟威望点不足！";
                    return;

                case -1:
                    this.strSay = "您不是此盟的盟员！";
                    return;

                case 1:
                    this.strSay = "您已接受本次挑战，比赛将于24小时后正式开始！";
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"UnionField.aspx?Type=FIELDMY\";'>";
                    return;
            }
            this.strSay = "系统错误请重试！";
        }

        private void btnOK_Click_FIELDREG(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            int request = SessionItem.GetRequest("UID", 0);
            int intReputation = SessionItem.GetRequest("RT", 0);
            this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"UnionField.aspx?Type=FIELDLIST&Page=1&UID=", request, "\";'>" });
            DataRow row = BTPUnionFieldManager.UnionFieldReg(this.intUserID, request, intReputation);
            if (row != null)
            {
                int num3 = (int) row["Type"];
                int num4 = (int) row["UnionReputationA"];
                string str = "";
                if (num4 > 0)
                {
                    str = "您的可用联盟威望已更新为" + num4;
                }
                switch (num3)
                {
                    case -12:
                        this.strSay = "本次挑战最少要押10威望！" + str;
                        return;

                    case -11:
                        this.strSay = "您的联盟没有足够的可用威望！" + str;
                        return;

                    case -10:
                        this.strSay = "你没有职业队不能发起挑战！" + str;
                        return;

                    case -9:
                        this.strSay = "您的联盟不符合挑战条件！" + str;
                        return;

                    case -8:
                        this.strSay = "对方联盟参加盟战的人数超过限制！" + str;
                        return;

                    case -7:
                        this.strSay = "对方联盟的联盟威望不足！" + str;
                        return;

                    case -6:
                        this.strSay = "输入的威望抵押不在有效范围内！" + str;
                        return;

                    case -5:
                        this.strSay = "对方联盟不符合挑战条件！" + str;
                        return;

                    case -4:
                        this.strSay = "您有一场盟战比赛正在进行！" + str;
                        return;

                    case -3:
                        this.strSay = "您所在的联盟参加盟战的人数超过限制！" + str;
                        return;

                    case -2:
                        this.strSay = "对不起，您不能挑战自己的联盟！" + str;
                        return;

                    case -1:
                        this.strSay = "您不在任何一个联盟内！" + str;
                        return;

                    case 1:
                        this.strSay = "挑战成功，请注意查看！" + str;
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"UnionField.aspx?Type=FIELDMY\";'>";
                        return;

                    case 2:
                        this.strSay = string.Concat(new object[] { "挑战成功，您所在的联盟威望已降低，您发起的挑战威望为", num4, "！", str });
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"UnionField.aspx?Type=FIELDMY\";'>";
                        return;
                }
                this.strSay = "系统错误请重试！";
            }
        }

        private void btnOK_Click_FREEBOX(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            int num = ToolItem.HasTool(this.intUserID, 12, 1);
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"ManagerTool.aspx?Type=USEBOX&Page=1\";'>";
            if (num > 0)
            {
                DataRow freeBoxByUserID = BTPBoxManager.GetFreeBoxByUserID(this.intUserID);
                switch (((int) freeBoxByUserID["Status"]))
                {
                    case 1:
                    {
                        string str = freeBoxByUserID["Name"].ToString().Trim();
                        this.strSay = "恭喜您抽到了新的奖品：" + str + " ，请注意查收！";
                        return;
                    }
                    case 2:
                        DTOnlineManager.ChangePayTypeByUserID(this.intUserID, 1);
                        base.Response.Redirect("Report.aspx?Parameter=402");
                        return;
                }
                this.strSay = "您还没有使用过！";
            }
            else
            {
                this.strSay = "您没有领取奖品的资格！";
            }
        }

        private void btnOK_Click_FRIESTAFF(object sender, ImageClickEventArgs e)
        {
            if (BTPStaffManager.FireStaff(this.intStaffID) == 3)
            {
                base.Response.Redirect("Report.aspx?Parameter=84!Type.STREET");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=84!Type.PRO");
            }
        }

        private void btnOK_Click_GETSTAFF(object sender, ImageClickEventArgs e)
        {
            int num;
            if ((this.intCategory == 5) || (this.intCategory == 2))
            {
                num = Convert.ToInt32(this.ddlStaff.SelectedValue);
            }
            else
            {
                num = Convert.ToInt32(this.ddlStaff1.SelectedValue);
            }
            int intDCategory = 3;
            string strEvent = "";
            switch (num)
            {
                case 3:
                    this.intClubID = this.intClubID3;
                    intDCategory = 3;
                    strEvent = "街球队雇佣职员";
                    break;

                case 5:
                    this.intClubID = this.intClubID5;
                    intDCategory = 5;
                    strEvent = "职业队雇佣职员";
                    break;
            }
            if (this.intClubID == 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=82");
            }
            else if (BTPStaffManager.HasEnoughMoney(this.intUserID, this.intSalary * this.intContract))
            {
                if (BTPStaffManager.HasStaff(this.intClubID, this.intType))
                {
                    base.Response.Redirect("Report.aspx?Parameter=80");
                }
                else
                {
                    BTPStaffManager.GetStaff(this.intStaffID, this.intClubID, intDCategory, strEvent);
                    base.Response.Redirect("Staff.aspx?Type=0&Grade=0&Page=1&Refresh=1");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=81");
            }
        }

        private void btnOK_Click_HIDE(object sender, ImageClickEventArgs e)
        {
            if (this.CanUseHide() == 1)
            {
                BTPToolLinkManager.UseHide(this.intUserID, this.longPlayerID, this.intCheckType, 2, this.intType);
                if (this.intPlayerCategory != 1)
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Show=HIDE&Type=", this.intType, "&Kind=1&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "\";window.location=\"MyFocus.aspx\";</script>" }));
                }
                else
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Show=HIDE&Type=", this.intType, "&Kind=1&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "\";window.location=\"PlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";</script>" }));
                }
            }
            else
            {
                base.Response.Write("<script>window.history.back();</script>");
            }
        }

        private void btnOK_Click_HirePlayer(object sender, ImageClickEventArgs e)
        {
            this.btnInTeam.Visible = false;
            this.btnOK.Visible = false;
            this.btnSearchCancel.Visible = false;
            this.longSearchPlayerID = SessionItem.GetRequest("PlayerID", 3);
            if (BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longSearchPlayerID) != null)
            {
                DataTable table = BTPPlayer3Manager.GetPlayer3TableByCIDCat(this.intClubID3, 0x58);
                if (table != null)
                {
                    int num = 0;
                    if (BTPToolLinkManager.HasTool(this.intUserID, 1, 0xc9, 0))
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 8;
                    }
                    if (table.Rows.Count != 8)
                    {
                        this.strSay = "您尚未雇佣球探，请先雇佣球探。";
                        this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                    }
                    else if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3) < num)
                    {
                        int intWealth = (int) BTPParameterManager.GetParameterRow()["SearchPlayer"];
                        if (Convert.ToInt32(BTPAccountManager.GetAccountRowByUserID(this.intUserID)["ActivePoint"]) >= intWealth)
                        {
                            BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth, 3, "录用球探找到的球员花费。");
                            DataRow row2 = BTPPlayer3Manager.SearchPlayerInTeamNew(this.intUserID, this.intClubID3, this.longSearchPlayerID);
                            int num4 = -1;
                            if (row2 != null)
                            {
                                num4 = (int) row2["Status"];
                            }
                            switch (num4)
                            {
                                case -2:
                                    this.strSay = "对不起，您的游戏币不足" + intWealth + "，不能录用该球员。";
                                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                                    return;

                                case -1:
                                    this.strSay = "该球员不是球探为您找到的球员，无法与其签约。";
                                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                                    return;

                                case 0:
                                    base.Response.Redirect(string.Concat(new object[] { "PlayerCenter.aspx?Type=8&UserID=", this.intUserID, "&INID=", this.longSearchPlayerID }));
                                    return;
                            }
                        }
                        else if (BTPAccountManager.HasEnoughWealth(this.intUserID, intWealth) == 1)
                        {
                            DataRow row3 = BTPPlayer3Manager.SearchPlayerInTeamNew(this.intUserID, this.intClubID3, this.longSearchPlayerID);
                            int num5 = -1;
                            if (row3 != null)
                            {
                                num5 = (int) row3["Status"];
                            }
                            BTPAccountManager.AddWealthByFinance(this.intUserID, intWealth, 2, "录用球探找到的球员花费。");
                            switch (num5)
                            {
                                case -2:
                                    this.strSay = "对不起，您的游戏币不足" + intWealth + "，不能录用该球员。";
                                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                                    return;

                                case -1:
                                    this.strSay = "该球员不是球探为您找到的球员，无法与其签约。";
                                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                                    return;

                                case 0:
                                    base.Response.Redirect(string.Concat(new object[] { "PlayerCenter.aspx?Type=8&UserID=", this.intUserID, "&INID=", this.longSearchPlayerID }));
                                    return;
                            }
                            base.Response.Redirect("Report.aspx?Parameter=3");
                        }
                        else
                        {
                            this.strSay = "对不起，您的游戏币不足" + intWealth + "，不能录用该球员。";
                            this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                        }
                    }
                    else
                    {
                        this.strSay = "对不起，您的街球队人数已满，不能录用该球员。";
                        this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                    }
                }
                else
                {
                    this.strSay = "您尚未雇佣球探，请先雇佣球探。";
                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                }
            }
            else
            {
                this.strSay = "您所要录用的球员不存在，请重新尝试。";
                this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
            }
        }

        private void btnOK_Click_KickOut(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            row2["NickName"].ToString().Trim();
            if ((num3 != 1) && (num3 != 2))
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
            else if (num4 != num2)
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
            else if ((num5 == 2) && (num3 != 1))
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
            else if ((num5 == 2) && (num3 == 1))
            {
                base.Response.Redirect("Report.aspx?Parameter=417!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
            else if (num5 == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
            else
            {
                BTPUnionManager.FireUnionUser(request);
                base.Response.Redirect("Report.aspx?Parameter=416!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
        }

        private void btnOK_Click_MANGERSAY(object sender, ImageClickEventArgs e)
        {
            int num = 0;
            this.btnOK.Visible = false;
            if ((this.intLevel != 1) && (this.intLevel < 10))
            {
                num = 0x1e8480 + (0x493e0 * (10 - this.intLevel));
            }
            else if (this.intLevel == 1)
            {
                num = 0x4c4b40;
            }
            else
            {
                num = 0x1e8480;
            }
            if (this.lgMoney < (num / 10))
            {
                this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您的资金不足，对球员进行奖励需要花费您 ", num / 0x2710, " 万资金！" });
            }
            else
            {
                int request = SessionItem.GetRequest("DevMatchID", 0);
                int num3 = BTPDevMatchManager.SetMangerSay(this.intUserID, request);
                if (num3 == 1)
                {
                    int num4 = SessionItem.GetRequest("Ref", 0);
                    switch (((int) SessionItem.GetRequest("Pos", 0)))
                    {
                        case 1:
                            base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=901!UserID.", this.intUserID, "^Devision.", this.strDevCode, "^Page.1" }));
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=901b!UserID." + this.intUserID + "^Type.5^Page.1");
                            return;
                    }
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=901c!UserID.", this.intUserID, "^Ref.", num4 }));
                }
                else
                {
                    switch (num3)
                    {
                        case -2:
                            this.strSay = this.strNickName + "经理，您好，您的球队没有参加这场职业联赛！";
                            return;

                        case -1:
                            this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您的资金不足，对球员进行奖励需要花费您 ", num / 0x2710, " 万资金！" });
                            return;
                    }
                }
            }
        }

        private void btnOK_Click_MATCHLEV(object sender, ImageClickEventArgs e)
        {
            this.intType = SessionItem.GetRequest("ClubType", 0);
            this.intLookUserID = SessionItem.GetRequest("UserID", 0);
            this.intClubID = SessionItem.GetRequest("ClubID", 0);
            if ((this.CanUseMatchLev() == 1) || (this.intPayType == 1))
            {
                BTPToolLinkManager.UseMatchLev(this.intUserID, this.intClubID, this.intType);
                if (this.intType == 3)
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.location=\"SArrange.aspx?ClubID=", this.intClubID, "&Type=5&UserID=", this.intLookUserID, "\";</script>" }));
                }
                else
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.location=\"VArrange.aspx?ClubID=", this.intClubID, "&Type=6&UserID=", this.intLookUserID, "\";</script>" }));
                }
            }
            else
            {
                base.Response.Write("<script>window.history.back();</script>");
            }
        }

        private void btnOK_Click_MATCHLOOK(object sender, ImageClickEventArgs e)
        {
            switch (BTPToolLinkManager.UseMatchLook(this.intUserID, this.intTag, this.intPayType))
            {
                case 1:
                    base.Response.Write(string.Concat(new object[] { "<script>window.location=\"DevisionView.aspx?Type=MATCHLOOK&UserID=", this.intLookUserID, "&Devision=", this.strDevCode, "&Status=1&Page=1\";</script>" }));
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=550");
                    return;
            }
        }

        private void btnOK_Click_MAX(object sender, ImageClickEventArgs e)
        {
            if (this.CanUseMax() == 1)
            {
                BTPToolLinkManager.UseMax(this.intUserID, this.longPlayerID, this.intCheckType, 3, this.intType);
                if (this.intPlayerCategory != 1)
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Show=MAX&Type=", this.intType, "&Kind=1&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "\";window.location=\"MyFocus.aspx\";</script>" }));
                }
                else
                {
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Show=MAX&Type=", this.intType, "&Kind=1&Check=", this.intCheck, "&PlayerID=", this.longPlayerID, "\";window.location=\"PlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";</script>" }));
                }
            }
            else
            {
                base.Response.Write("<script>window.history.back();</script>");
            }
        }

        private void btnOK_Click_MINUSAGE(object sender, ImageClickEventArgs e)
        {
            BTPToolLinkManager.MinusAge(this.intUserID, this.longPlayerID, this.intType);
            base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=1&PlayerID=", this.longPlayerID, "\";window.location=\"TrainPlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";</script>" }));
        }

        private void btnOK_Click_NEWTRAINPLAYER3(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("RookieTrainPlayerCenter.aspx?Type=3");
        }

        private void btnOK_Click_ONLYMATCHOUT(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'>";
            switch (BTPOnlyOneCenterReg.OnlyOneMatchOut(this.intUserID))
            {
                case -1:
                    this.strSay = "对不起，你还不能退出胜者为王赛场！";
                    break;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                    return;

                default:
                    return;
            }
        }

        private void btnOK_Click_Ordain(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int intUnionID = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            row2["NickName"].ToString().Trim();
            if (BTPUnionManager.GetUManagerCount(intUnionID) > 7)
            {
                base.Response.Redirect("Report.aspx?Parameter=427!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
            else if (num4 != intUnionID)
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
            else if (num5 == 2)
            {
                base.Response.Redirect("Report.aspx?Parameter=418!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
            else if ((num3 == 1) && (num5 != 1))
            {
                BTPUnionManager.SetUnionUserCategory(request, 2);
                base.Response.Redirect("Report.aspx?Parameter=416!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
        }

        private void btnOK_Click_Out(object sender, ImageClickEventArgs e)
        {
            if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) != 0)
            {
                BTPUnionManager.OutUnion(this.intUserID);
                DTOnlineManager.ChangeUnionIDByUserID(this.intUserID, 0);
                base.Response.Redirect("Report.aspx?Parameter=422");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=421");
            }
        }

        private void btnOK_Click_PLAYER3BID(object sender, ImageClickEventArgs e)
        {
            if (this.intPlayerCategory != 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=P103!Type.3");
            }
            int intBidPrice = PlayerItem.GetPlayerValue(this.intPV, this.intAbility, this.intHeight);
            int num2 = Convert.ToInt16(this.ddlEndBidTime.SelectedValue);
            string text = this.tbBLock.Text;
            this.datEndBidTime = DateTime.Now.AddHours((double) num2);
            switch (BTPPlayer3Manager.SetPlayer3Bid(this.longPlayerID, intBidPrice, this.datEndBidTime, text))
            {
                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=P104!Type.3");
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=P105!Type.3");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=P106!Type.3");
                    break;
            }
        }

        private void btnOK_Click_PLAYER3FIRE(object sender, ImageClickEventArgs e)
        {
            BTPPlayer3Manager.FirePlayer3ByPlayerID(this.intClubID3, this.longPlayerID);
            base.Response.Redirect("Report.aspx?Parameter=P102!Type.3");
        }

        private void btnOK_Click_PLAYER5BID(object sender, ImageClickEventArgs e)
        {
            if (this.intPlayerCategory != 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=P103!Type.5");
            }
            BTPDevManager.GetDevCodeByUserID(this.intUserID);
            string strMarketCode = "AAA";
            Convert.ToInt16(this.ddlEndBidTime5.SelectedValue);
            this.datEndBidTime = DateTime.Today.AddDays(2.0);
            this.datEndBidTime = this.datEndBidTime.AddHours((double) RandomItem.rnd.Next(8, 0x18));
            long intBidPrice = BTPPlayer5Manager.GetSellMoneyPlayer5(this.intUserID, this.longPlayerID);
            string strEvent = string.Concat(new object[] { "您将", this.intPlayer5Number, "号球员", this.strPlayer5Name, "卖出得到", intBidPrice });
            int num2 = 2;
            if (intBidPrice > 0L)
            {
                num2 = BTPPlayer5Manager.NewSellPlayer5(this.intUserID, this.longPlayerID, strMarketCode, intBidPrice, this.datEndBidTime, strEvent);
            }
            if (num2 == 4)
            {
                this.strSay = "此球员即将在赛季末退役，无法挂牌出售。";
            }
            else if (num2 == 3)
            {
                this.strSay = "球队可上场比赛的球员只有8个，您不能再将其挂牌出售！";
            }
            else if (num2 == 2)
            {
                this.strSay = "您要卖出的球员必须为您的球队效劳3天以上。";
            }
            else if (num2 == 0)
            {
                this.strSay = "请确认该球员是否在您的球队中效力。";
            }
            else if (num2 == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=P104!Type.5");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void btnOK_Click_PLAYER5FIRE(object sender, ImageClickEventArgs e)
        {
        }

        private void btnOK_Click_PLAYERCONTRACT(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            long request = SessionItem.GetRequest("PID", 3);
            switch (((int) SessionItem.GetRequest("Status", 0)))
            {
                case 1:
                    BTPPlayer5Manager.UpdateContractByPlayerID(request, 13, 1);
                    base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                    return;

                case 2:
                    BTPPlayer5Manager.UpdateContractByPlayerID(request, 0x1a, 2);
                    base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                    return;

                case 3:
                    BTPPlayer5Manager.UpdateContractByPlayerID(request, 0x27, 3);
                    base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                    return;
            }
            this.strSay = "系统错误，请重试！";
        }

        private void btnOK_Click_POSITION(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            string str = row2["NickName"].ToString().Trim();
            if ((num2 == num4) && (num3 == 1))
            {
                string validWords = StringItem.GetValidWords(this.tbPosistion.Text.ToString().Trim());
                if (!StringItem.IsValidName(validWords, 0, 12))
                {
                    this.strSay = "封号中有非法字符或者长度不符合要求。";
                }
                else
                {
                    BTPAccountManager.SetUnionPosition(request, validWords);
                    base.Response.Redirect("Report.aspx?Parameter=416!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
                }
            }
            else
            {
                this.strSay = this.strNickName + "经理，你无权为" + str + "设定的封号。";
            }
        }

        private void btnOK_Click_PrearrangeManage(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            base.Response.Redirect("VArrangeDev.aspx?Tag=" + request);
        }

        private void btnOK_Click_PrearrangeManageXBA(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            base.Response.Redirect("VArrangeChampion.aspx?Tag=" + request);
        }

        private void btnOK_Click_PrearrangeOpen(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            BTPDevMatchManager.PrearrangeOpenbyCIDMID(this.intClubID5, request);
            base.Response.Redirect("VArrangeDev.aspx?Tag=" + request);
        }

        private void btnOK_Click_PrearrangeOpenXBA(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Tag", 0);
            BTPXGroupMatchManager.ChampionOpenbyCIDMID(this.intClubID5, request);
            base.Response.Redirect("VArrangeChampion.aspx?Tag=" + request);
        }

        private void btnOK_Click_PRIVATESKILL(object sender, ImageClickEventArgs e)
        {
            if (this.CanUsePrivateSkill() == 1)
            {
                BTPToolLinkManager.UsePrivateSkill(this.intUserID, this.longPlayerID, this.intCheckType);
                base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=0&PlayerID=", this.longPlayerID, "\";window.location=\"MyFocus.aspx\";</script>" }));
            }
            else
            {
                base.Response.Write("<script>window.history.back();</script>");
            }
        }

        private void btnOK_Click_QUICKBUY(object sender, ImageClickEventArgs e)
        {
            this.btnOK.Visible = false;
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TransferMarket.aspx?Type=MYFOCUSPLAYER&Pos=1&Order=1&Page=1;\"'>";
            DataRow bidderByUserID = BTPBidderManager.GetBidderByUserID(this.intUserID);
            if ((bidderByUserID != null) && ((bool) bidderByUserID["TopPrice"]))
            {
                this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
            }
            else
            {
                long num3 = this.CheckCanBid(3, 3);
                if ((num3 <= -3L) && (num3 >= -9L))
                {
                    switch (((int) (num3 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                long num = (int) playerRowByPlayerID["PV"];
                long num2 = (long) playerRowByPlayerID["QuickBuy"];
                if (num2 > 0L)
                {
                    this.strSay = this.strNickName + "经理，您好！这名球员已被其它经理购买！";
                }
                else
                {
                    num *= 0x3a98L + (this.longPlayerID % 50L);
                    if (this.lgMoney < num)
                    {
                        this.strSay = "您没有足够的资金！";
                    }
                    else
                    {
                        string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                        switch (BTPBidderManager.BidQuickBuyByUserID(this.intUserID, this.longPlayerID, strIP))
                        {
                            case -4:
                                this.strSay = this.strNickName + "经理，您好！系统检测到你过多使用外挂，你将被禁止一口价球员！";
                                return;

                            case -3:
                                this.strSay = this.strNickName + "经理，您好！您的资金不足！";
                                return;

                            case -1:
                                this.strSay = this.strNickName + "经理，您好！您现在职业队中人数过多无法再购买球员了！";
                                return;

                            case 1:
                                base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                                return;
                        }
                        this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                    }
                }
            }
        }

        private void btnOK_Click_QUICKBUYRETURN(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
        }

        private void btnOK_Click_RefashionPlayerSkill(object sender, ImageClickEventArgs e)
        {
            float num;
            float num2;
            float num3;
            float num4;
            float num5;
            float num6;
            float num7;
            float num8;
            float num9;
            float num10;
            float num11;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            string str8;
            string str9;
            string str10;
            string str11;
            DataRow row;
            int num12 = -1;
            this.blnRefashionSafe = this.cbRefashionSafe.Checked;
            if (!this.blnRefashionSafe)
            {
                row = BTPAccountManager.RefashionPlayerSkill(this.intUserID, this.longPlayerID, this.intType);
            }
            else
            {
                row = BTPAccountManager.RefashionSafePlayerSkill(this.intUserID, this.longPlayerID, this.intType);
            }
            if (row != null)
            {
                int num13;
                int num14;
                int num15;
                int num16;
                int num17;
                int num18;
                int num19;
                int num20;
                int num21;
                int num22;
                int num23;
                num12 = (int) row["Status"];
                if (num12 == 0)
                {
                    num13 = (int) row["SpeedDifference"];
                    num14 = (int) row["JumpDifference"];
                    num15 = (int) row["StrengthDifference"];
                    num16 = (int) row["StaminaDifference"];
                    num17 = (int) row["ShotDifference"];
                    num18 = (int) row["Point3Difference"];
                    num19 = (int) row["DribbleDifference"];
                    num20 = (int) row["PassDifference"];
                    num21 = (int) row["ReboundDifference"];
                    num22 = (int) row["StealDifference"];
                    num23 = (int) row["BlockDifference"];
                }
                else
                {
                    num13 = 0;
                    num14 = 0;
                    num15 = 0;
                    num16 = 0;
                    num17 = 0;
                    num18 = 0;
                    num19 = 0;
                    num20 = 0;
                    num21 = 0;
                    num22 = 0;
                    num23 = 0;
                }
                num = ((float) num13) / 10f;
                num2 = ((float) num14) / 10f;
                num3 = ((float) num15) / 10f;
                num4 = ((float) num16) / 10f;
                num5 = ((float) num17) / 10f;
                num6 = ((float) num18) / 10f;
                num7 = ((float) num19) / 10f;
                num8 = ((float) num20) / 10f;
                num9 = ((float) num21) / 10f;
                num10 = ((float) num22) / 10f;
                num11 = ((float) num23) / 10f;
                if (num13 > 0)
                {
                    str = "<font color='red'>+" + num + "</font>";
                }
                else
                {
                    str = "<font color='green'>" + num + "</font>";
                }
                if (num14 > 0)
                {
                    str2 = "<font color='red'>+" + num2 + "</font>";
                }
                else
                {
                    str2 = "<font color='green'>" + num2 + "</font>";
                }
                if (num15 > 0)
                {
                    str3 = "<font color='red'>+" + num3 + "</font>";
                }
                else
                {
                    str3 = "<font color='green'>" + num3 + "</font>";
                }
                if (num16 > 0)
                {
                    str4 = "<font color='red'>+" + num4 + "</font>";
                }
                else
                {
                    str4 = "<font color='green'>" + num4 + "</font>";
                }
                if (num17 > 0)
                {
                    str5 = "<font color='red'>+" + num5 + "</font>";
                }
                else
                {
                    str5 = "<font color='green'>" + num5 + "</font>";
                }
                if (num18 > 0)
                {
                    str6 = "<font color='red'>+" + num6 + "</font>";
                }
                else
                {
                    str6 = "<font color='green'>" + num6 + "</font>";
                }
                if (num19 > 0)
                {
                    str7 = "<font color='red'>+" + num7 + "</font>";
                }
                else
                {
                    str7 = "<font color='green'>" + num7 + "</font>";
                }
                if (num20 > 0)
                {
                    str8 = "<font color='red'>+" + num8 + "</font>";
                }
                else
                {
                    str8 = "<font color='green'>" + num8 + "</font>";
                }
                if (num21 > 0)
                {
                    str9 = "<font color='red'>+" + num9 + "</font>";
                }
                else
                {
                    str9 = "<font color='green'>" + num9 + "</font>";
                }
                if (num22 > 0)
                {
                    str10 = "<font color='red'>+" + num10 + "</font>";
                }
                else
                {
                    str10 = "<font color='green'>" + num10 + "</font>";
                }
                if (num23 > 0)
                {
                    str11 = "<font color='red'>+" + num11 + "</font>";
                }
                else
                {
                    str11 = "<font color='green'>" + num11 + "</font>";
                }
            }
            else
            {
                num12 = -1;
                num = 0f;
                num2 = 0f;
                num3 = 0f;
                num4 = 0f;
                num5 = 0f;
                num6 = 0f;
                num7 = 0f;
                num8 = 0f;
                num9 = 0f;
                num10 = 0f;
                num11 = 0f;
                str = "0";
                str2 = "0";
                str3 = "0";
                str4 = "0";
                str5 = "0";
                str6 = "0";
                str7 = "0";
                str8 = "0";
                str9 = "0";
                str10 = "0";
                str11 = "0";
            }
            switch (num12)
            {
                case 0:
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=1&PlayerID=", this.longPlayerID, "\";</script>" }));
                    this.strRefashionButton = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_Finish.gif' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=", this.intType, "&UserID=", this.intUserID, "\";'>" });
                    this.trRefashion.Visible = true;
                    this.trOther.Visible = false;
                    this.strRefashionMsg = "<table cellSpacing='0' cellPadding='0' width='203' border='0'><tr><td colspan='2' height='25'>球员" + this.strPlayerName + "属性再分配成功：</td></tr><tr><td width='101' height='20'>速度潜力:" + str + "</td><td width='102'>弹跳潜力:" + str2 + "</td></tr><tr><td height='20'>强壮潜力:" + str3 + "</td><td>耐力潜力:" + str4 + "</td></tr><tr><td height='20'>投篮潜力:" + str5 + "</td><td>三分潜力:" + str6 + "</td></tr><tr><td height='20'>运球潜力:" + str7 + "</td><td>传球潜力:" + str8 + "</td></tr><tr><td height='20'>篮板潜力:" + str9 + "</td><td>抢断潜力:" + str10 + "</td></tr><tr><td height='20'>封盖潜力:" + str11 + "</td><td></td></tr></table>";
                    this.strSay = "您需要对球员" + this.strPlayerName + "再次进行洗点么？";
                    if (this.blnRefashionSafe)
                    {
                        this.strSay = this.strSay + "<font color='red'>已加保险。</font>";
                    }
                    this.btnOK.ImageUrl = "Images/button_Refashion.gif";
                    this.btnOK.Width = 0x42;
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_RefashionPlayerSkill);
                    this.strBtnCancel = "";
                    return;

                case 1:
                    if (!this.blnRefashionSafe)
                    {
                        base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=1&PlayerID=", this.longPlayerID, "\";window.location=\"Report.aspx?Parameter=708!UserID.", this.intUserID, "^Type.", this.intType, "\";</script>" }));
                        return;
                    }
                    base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=", this.intType, "&Kind=1&Check=1&PlayerID=", this.longPlayerID, "\";window.location=\"Report.aspx?Parameter=708a!UserID.", this.intUserID, "^Type.", this.intType, "\";</script>" }));
                    return;

                case 2:
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=705!UserID.", this.intUserID, "^Type.", this.intType }));
                    return;

                case 3:
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=707!UserID.", this.intUserID, "^Type.", this.intType }));
                    return;

                case 4:
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=709!UserID.", this.intUserID, "^Type.", this.intType }));
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=3");
        }

        private void btnOK_Click_RefuseUnion(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UnionID", 0);
            int intUserID = SessionItem.GetRequest("UserID", 0);
            int intMessageID = SessionItem.GetRequest("MessageID", 0);
            if (intUserID == this.intUserID)
            {
                BTPUnionManager.RefuseUnion(intUserID, request, intMessageID);
                base.Response.Redirect("Report.aspx?Parameter=416!Type.UNION^Kind.VIEWUNION^Page.1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void btnOK_Click_REGCUP(object sender, ImageClickEventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intLevel = (byte) accountRowByUserID["Levels"];
            this.intScore = (int) accountRowByUserID["Score"];
            this.intRank = this.intScore;
            int num = Convert.ToInt32(ROOTUserManager.GetUserRowByUIDName40(this.strNickName, this.intUserID)["Coin"]);
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            byte num1 = (byte) cupRowByCupID["Category"];
            int intTicketCategory = (byte) cupRowByCupID["TicketCategory"];
            if ((intTicketCategory > 0) && !BTPToolLinkManager.HasTicket(this.intUserID, 1, intTicketCategory))
            {
                base.Response.Redirect("Report.aspx?Parameter=98");
            }
            else
            {
                int intCoin = (byte) cupRowByCupID["Coin"];
                string str = cupRowByCupID["Name"].ToString().Trim();
                int num4 = (int) cupRowByCupID["RegCount"];
                int num5 = (int) cupRowByCupID["Capacity"];
                if (num4 >= num5)
                {
                    base.Response.Redirect("Report.aspx?Parameter=97");
                }
                else if ((intCoin > 0) && (num < intCoin))
                {
                    if (DBLogin.CanConn(0))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=411");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=445");
                    }
                }
                else if (((intCoin > 0) && (num >= intCoin)) && !DBLogin.CanConn(0))
                {
                    base.Response.Redirect("Report.aspx?Parameter=445");
                }
                else if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3) < 4)
                {
                    base.Response.Redirect("Report.aspx?Parameter=96");
                }
                else
                {
                    DataTable setIDsTable = BTPCupManager.GetSetIDsTable(accountRowByUserID["CupIDs"].ToString().Trim().Replace("|", ","));
                    int num6 = 0;
                    if (setIDsTable != null)
                    {
                        for (int i = 0; i < setIDsTable.Rows.Count; i++)
                        {
                            DataRow row3 = setIDsTable.Rows[i];
                            int num8 = (int) row3["SetID"];
                            int num9 = (int) BTPCupManager.GetCupRowByCupID(this.intCupID)["SetID"];
                            if (num9 == num8)
                            {
                                num6 = 1;
                            }
                        }
                    }
                    if (num6 == 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=94");
                    }
                    else
                    {
                        int num10 = BTPCupRegManager.SetCupReg(this.intUserID, this.intCupID, this.intClubID3, this.strClubName3, this.strNickName, this.intLevel, this.strClubLogo, this.intScore, this.intRank, "");
                        if (num10 == 1)
                        {
                            if (intCoin > 0)
                            {
                                ROOTUserManager.SpendCoin40(this.intUserID, intCoin, str + "报名", "");
                            }
                            base.Response.Redirect("Report.aspx?Parameter=93");
                        }
                        else if (num10 == 2)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=95");
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=94");
                        }
                    }
                }
            }
        }

        private void btnOK_Click_RegPoint3Match(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("PlayerID", 0);
            int intFlg = SessionItem.GetRequest("Flg", 0);
            if (BTPPoint3Manager.RegPoint3Match(request, intFlg) == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=P3S01");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=P3E01");
            }
        }

        private void btnOK_Click_RETURNOKSAY(object sender, ImageClickEventArgs e)
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            int num2 = SessionItem.GetRequest("PlayerType", 0);
            int num1 = SessionItem.GetRequest("Category", 0);
            base.Response.Write(string.Concat(new object[] { "<script>window.location=\"PlayerCenter.aspx?PlayerType=", num2, "&Type=9&PlayerID=", request, "\";</script>" }));
        }

        private void btnOK_Click_SearchAgain(object sender, ImageClickEventArgs e)
        {
            this.btnInTeam.Visible = false;
            this.btnOK.Visible = false;
            this.btnSearchCancel.Visible = false;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                int num1 = (int) accountRowByUserID["ClubID3"];
                int num = (int) accountRowByUserID["Wealth"];
                int num2 = (int) accountRowByUserID["ActivePoint"];
                if (num2 >= 100)
                {
                    BTPAccountManager.AddWealthByFinance(this.intUserID, 100, 3, "搜人经费，花费100枚游戏币");
                    BTPPlayer3Manager.DeletePlayer3ByCIDCat(this.intClubID3, 0x58);
                    BTPPlayer3Manager.SearchPlayer3New(this.intClubID3, 0x58, DateTime.Now.ToString().Trim(), 120, 0x69);
                    base.Response.Redirect("PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=7");
                }
                else if (num >= 100)
                {
                    BTPAccountManager.AddWealthByFinance(this.intUserID, 100, 2, "搜人经费，花费100枚游戏币");
                    BTPPlayer3Manager.DeletePlayer3ByCIDCat(this.intClubID3, 0x58);
                    BTPPlayer3Manager.SearchPlayer3New(this.intClubID3, 0x58, DateTime.Now.ToString().Trim(), 120, 0x69);
                    base.Response.Redirect("PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=7");
                }
                else
                {
                    this.tbSay.Visible = true;
                    this.strSay = "对不起，您的游戏币不足，不能再次搜索球员。";
                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                }
            }
            else
            {
                this.tbSay.Visible = true;
                this.strSay = "您的信息不存在，或登录信息丢失，请重新进入游戏并尝试。";
                this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
            }
        }

        private void btnOK_Click_SETAUTOTRAIN(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Hours", 0);
            int intDevHours = SessionItem.GetRequest("DevH", 0);
            if ((request < 1) && (intDevHours < 1))
            {
                this.strSay = "请设置正确的训练时间！";
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
            }
            else if (intDevHours < 1)
            {
                intDevHours = 0;
                if ((request < 1) || (request > 200))
                {
                    this.strSay = "街球训练时间必须在1至200小时以内！";
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                }
                else
                {
                    BTPToolLinkManager.UseAutoTrain(this.intUserID, request, intDevHours);
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetLogoutURL() + "\";</script>");
                }
            }
            else if (request < 1)
            {
                request = 0;
                if ((intDevHours < 1) || (intDevHours > 200))
                {
                    this.strSay = "街球训练时间必须在1至200小时以内！";
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                }
                else
                {
                    BTPToolLinkManager.UseAutoTrain(this.intUserID, request, intDevHours);
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetLogoutURL() + "\";</script>");
                }
            }
            else if ((request < 1) || (request > 200))
            {
                this.strSay = "街球训练时间必须在1至200小时以内！";
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
            }
            else if ((intDevHours < 1) || (intDevHours > 200))
            {
                this.strSay = "职业训练时间必须在1至200小时以内！";
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
            }
            else
            {
                BTPToolLinkManager.UseAutoTrain(this.intUserID, request, intDevHours);
                base.Response.Write("<script>window.top.location=\"" + StringItem.GetLogoutURL() + "\";</script>");
            }
        }

        private void btnOK_Click_SETFOCUS(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("MyFocus.aspx");
        }

        private void btnOK_Click_SETHONOR(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Category", 0);
            long longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            int intCategory = 1;
            switch (request)
            {
                case 3:
                    this.intClubID = this.intClubID3;
                    intCategory = 1;
                    break;

                case 5:
                    this.intClubID = this.intClubID5;
                    intCategory = 2;
                    break;
            }
            if (this.intClubID == 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=90");
            }
            else
            {
                string validWords = StringItem.GetValidWords(this.tbOldPlayer.Text.Trim());
                if (!StringItem.IsValidContent(validWords, 0, 100))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3ab");
                }
                else if (this.intPayType == 0)
                {
                    if (BTPOldPlayerManager.GetHPlayerCount(this.intUserID) > 4)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=91");
                    }
                    else if (this.blnIsHonor)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=92!Type.PLAYER^Page.1");
                    }
                    else
                    {
                        BTPOldPlayerManager.SetOldPlayer(longPlayerID, this.intUserID, intCategory, validWords);
                        base.Response.Redirect("Honour.aspx?UserID=" + this.intUserID + "&Type=PLAYER&Page=1");
                    }
                }
                else if (this.blnIsHonor)
                {
                    base.Response.Redirect("Report.aspx?Parameter=92!Type.PLAYER^UserID." + this.intUserID + "^Page.1");
                }
                else
                {
                    BTPOldPlayerManager.SetOldPlayer(longPlayerID, this.intUserID, intCategory, validWords);
                    base.Response.Redirect("Honour.aspx?UserID=" + this.intUserID + "&Type=PLAYER&Page=1");
                }
            }
        }

        private void btnOK_Click_SETNPC(object sender, ImageClickEventArgs e)
        {
            switch (BTPNPCManager.AddNPCMatch(this.intUserID, (int) this.longNpcID, Config.DayNpcFreeTimes()))
            {
                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=SNE03");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=SNE02");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=SNE01");
                    return;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=SNS01");
                    break;

                default:
                    return;
            }
        }

        private void btnOK_Click_SETTRIAL(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=5");
        }

        private void btnOK_Click_SHOWSKILL(object sender, ImageClickEventArgs e)
        {
            DataRow playerRowByPlayerID;
            if (this.intCheckType < 4)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
            if (time < DateTime.Now.AddMinutes(30.0))
            {
                this.strToolMsg = "在距截止时间不足30分钟时，不能使用潜力公告。";
                this.btnOK.Visible = false;
            }
            else
            {
                BTPToolLinkManager.UseToolCategory3(this.intUserID, this.longPlayerID, this.intCheckType);
                base.Response.Redirect("Report.aspx?Parameter=691b!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
            }
        }

        private void btnOK_Click_STARVOTE(object sender, ImageClickEventArgs e)
        {
            switch (BTPStarMatchManager.VoteStarPlayer(this.intUserID, this.longPlayerID))
            {
                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=SVE03");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=SVE02");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=SVE01");
                    return;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=SVS01");
                    break;

                default:
                    return;
            }
        }

        private void btnOK_Click_STREETCHOOSEBID(object sender, ImageClickEventArgs e)
        {
            DataRow bidderByUserID = BTPBidderManager.GetBidderByUserID(this.intUserID);
            if ((bidderByUserID != null) && ((bool) bidderByUserID["TopPrice"]))
            {
                this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
            }
            else
            {
                int num;
                long num4 = this.CheckCanBid(3, 3);
                if ((num4 <= -3L) && (num4 >= -9L))
                {
                    switch (((int) (num4 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                try
                {
                    num = Convert.ToInt32(this.tbFreeBidPrice.Text);
                }
                catch (Exception exception)
                {
                    num = 0;
                    exception.ToString();
                    this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                    return;
                }
                long num2 = (long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Money"];
                int num3 = Convert.ToInt32(this.tbFreeBidPrice.Text);
                if (num3 >= 0x3e8)
                {
                    if (num2 < num3)
                    {
                        this.strSay = this.strNickName + "经理，咱们俱乐部已经没有太多的资金了，您千万不要意气用事啊！";
                    }
                    else
                    {
                        string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                        string strCreateTime = DateTime.Now.ToString();
                        BTPTransferManager.SetChooseBid(this.intUserID, this.longPlayerID, 3, 1, num, strCreateTime, strIP);
                        DateTime time = (DateTime) BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID)["EndBidTime"];
                        if (time < DateTime.Now.AddMinutes(1.0))
                        {
                            BTPPlayer5Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(1.0));
                        }
                        base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                    }
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您就这么小气，起码要超过1000元吧？";
                }
            }
        }

        private void btnOK_Click_STREETFREEBID(object sender, ImageClickEventArgs e)
        {
            int num = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Category"];
            if ((num != 1) && (num != 2))
            {
                this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
            }
            else
            {
                int num2;
                long num6 = this.CheckCanBid(3, 1);
                if ((num6 <= -3L) && (num6 >= -9L))
                {
                    switch (((int) (num6 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                try
                {
                    num2 = Convert.ToInt32(this.tbFreeBidPrice.Text);
                }
                catch (Exception exception)
                {
                    num2 = 0;
                    exception.ToString();
                    this.strSay = this.strNickName + "经理，您这是填写的什么啊，请用半角的数字。";
                    return;
                }
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                int num3 = (int) accountRowByUserID["Wealth"];
                long num4 = (long) accountRowByUserID["Money"];
                int num5 = Convert.ToInt32(this.tbFreeBidPrice.Text);
                if (num3 >= this.intPayWealth)
                {
                    if (num5 < 0x3e8)
                    {
                        this.strSay = this.strNickName + "经理，您就这么小气，起码要超过1000元吧？";
                    }
                    else if (num4 < num5)
                    {
                        this.strSay = this.strNickName + "经理，咱们俱乐部已经没有太多的资金了，怎么能出这么高的价钱呢？";
                    }
                    else
                    {
                        DateTime time = (DateTime) BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID)["EndBidTime"];
                        if (time < DateTime.Now.AddMinutes(1.0))
                        {
                            BTPPlayer3Manager.UpdateEndBidTime(this.longPlayerID, DateTime.Now.AddMinutes(1.0));
                        }
                        string strIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                        string strCreateTime = DateTime.Now.ToString();
                        BTPTransferManager.SetFreeBid(this.intUserID, this.longPlayerID, 3, 2, num2, strCreateTime, strIP);
                        if ((bool) Global.drParameter["MarketPay"])
                        {
                            BTPAccountManager.AddWealthByFinance(this.intUserID, this.intPayWealth, 2, "街头自由转会市场出价");
                        }
                        base.Response.Redirect("Report.aspx?Parameter=691!Type.MYFOCUSPLAYER^Pos.1^Order.0^Page.1");
                    }
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您没有足够的游戏币，请从游戏币管理中提出游戏币。";
                }
            }
        }

        private void btnOK_Click_TIMEHOUSE(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            long request = SessionItem.GetRequest("PlayerID", 3);
            int num2 = (int) BTPParameterManager.GetParameterRow()["TimeHouseWealth"];
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intWealth = (int) accountRowByUserID["Wealth"];
            if (this.intWealth < num2)
            {
                this.strSay = "您没有足够的游戏币，进入神之领域需要 " + num2 + " 游戏币！";
            }
            else
            {
                accountRowByUserID = BTPPlayer3Manager.AddPlayerMaxByPlayerID(request, this.intUserID);
                int num3 = (int) accountRowByUserID["Type"];
                if (num3 == 1)
                {
                    object strSay;
                    double num4 = Convert.ToDouble(accountRowByUserID["AbilityAdd"]) / 10.0;
                    int num5 = (int) accountRowByUserID["HeightAdd"];
                    int num6 = (int) accountRowByUserID["WeightAdd"];
                    this.strSay = "您的球员从神之领域中走出来，成长了一岁，增加了 " + num4 + " 的综合潜力";
                    if (num5 > 0)
                    {
                        strSay = this.strSay;
                        this.strSay = string.Concat(new object[] { strSay, "，身高增长了 ", num5, "CM" });
                    }
                    if (num6 != 0)
                    {
                        strSay = this.strSay;
                        this.strSay = string.Concat(new object[] { strSay, "，体重增长了 ", num6, "KG" });
                    }
                    this.strSay = this.strSay + "！";
                    base.Response.Write("<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=3&Kind=1&Check=1&PlayerID=" + request + "\";</script>");
                    this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?PlayerType=3&Type=9&PlayerID=", request, "\";'>" });
                }
                else
                {
                    switch (num3)
                    {
                        case -2:
                            this.strSay = "您没有足够的游戏币，进入神之领域需要 " + num2 + " 游戏币！";
                            return;

                        case -1:
                            this.strSay = "您的球员年龄太大了进不了神之领域！";
                            return;
                    }
                    this.strSay = "这个球员没有在您球队效力，不要在他身上浪费游戏币！";
                }
            }
        }

        private void btnOK_Click_TRAINPLAYER3(object sender, ImageClickEventArgs e)
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.intTrainType = SessionItem.GetRequest("TrainType", 0);
            base.Response.Write(string.Concat(new object[] { "<script>window.location=\"SecretaryPage.aspx?Type=TRAINPLAYER3&PlayerID=", this.longPlayerID, "&TrainType=", this.intTrainType, "\";</script>" }));
        }

        private void btnOK_Click_TRAINPLAYER5(object sender, ImageClickEventArgs e)
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.intTrainType = SessionItem.GetRequest("TrainType", 0);
            base.Response.Write(string.Concat(new object[] { "<script>window.location=\"SecretaryPage.aspx?Type=TRAINPLAYER5&PlayerID=", this.longPlayerID, "&TrainType=", this.intTrainType, "\";</script>" }));
        }

        private void btnOK_Click_Unchain(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            row2["NickName"].ToString().Trim();
            if (((num5 == 2) && (num3 == 1)) && (num2 == num4))
            {
                BTPUnionManager.SetUnionUserCategory(request, 0);
                base.Response.Redirect("Report.aspx?Parameter=416!Type.MYUNION^Kind.UNIONMANAGE^Status.UNIONER^Page.1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
        }

        private void btnOK_Click_UNLAY(object sender, ImageClickEventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int intUnionID = (int) accountRowByUserID["UnionID"];
            int num2 = (byte) accountRowByUserID["UnionCategory"];
            if ((BTPUnionManager.GetUnionUserCountByID(intUnionID) == 1) && (num2 == 1))
            {
                int num3 = BTPUnionManager.UnlayUnion(this.intUserID);
                switch (num3)
                {
                    case 0:
                        base.Response.Redirect("Report.aspx?Parameter=424!Type.UNION^Kind.VIEWUNION^Page.1");
                        return;

                    case 1:
                        base.Response.Redirect("Report.aspx?Parameter=424!Type.UNION^Kind.VIEWUNION^Page.1");
                        return;

                    case 2:
                        DTOnlineManager.ChangeUnionIDByUserID(this.intUserID, 0);
                        base.Response.Redirect("Report.aspx?Parameter=423!Type.UNION^Kind.VIEWUNION^Page.1");
                        return;

                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=425a!Type.UNION^Kind.VIEWUNION^Page.1");
                        return;
                }
                if (num3 == 3)
                {
                    base.Response.Redirect("Report.aspx?Parameter=425b!Type.UNION^Kind.VIEWUNION^Page.1");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=425!Type.UNION^Kind.VIEWUNION^Page.1");
            }
        }

        private void btnOK_Click_UPDATEPRICE(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("ClubID", 0);
            if (BTPClubManager.GetClubRowByUIDCate(this.intUserID, 5) != null)
            {
                base.Response.Redirect("Report.aspx?Parameter=698");
            }
            else
            {
                DataRow clubRowByID = BTPClubManager.GetClubRowByID(request);
                if (clubRowByID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=1");
                }
                else if (BTPClubManager.HasBuyClub(this.intUserID))
                {
                    base.Response.Redirect("Report.aspx?Parameter=695!Type.DEVNEW^Page.1");
                }
                else
                {
                    DateTime time1 = (DateTime) clubRowByID["EndBidTime"];
                    switch (BTPClubManager.BuyClub5(this.intUserID, request))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=693a!Type.NEW^Page.1");
                            return;

                        case 1:
                        {
                            int intMoney = (int) Global.drParameter["RegPMatchMoney"];
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 2);
                            int devCount = BTPDevManager.GetDevCount();
                            BTPDevManager.GetDevTotal();
                            if (BTPDevManager.GetDevBlank() <= 0)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=67!Type.DEVNEW^Page.1");
                                return;
                            }
                            if (!BTPAccountManager.HasEnoughMoney(this.intUserID, intMoney))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=101");
                                return;
                            }
                            switch (BTPDevManager.SetFinanlDevByUserID(this.intUserID))
                            {
                                case 0:
                                    base.Response.Redirect("Report.aspx?Parameter=101");
                                    return;

                                case 1:
                                    DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 5);
                                    base.Response.Redirect("Report.aspx?Parameter=102");
                                    return;

                                case 2:
                                    base.Response.Redirect("Report.aspx?Parameter=103");
                                    return;
                            }
                            base.Response.Redirect("Report.aspx?Parameter=106");
                            return;
                        }
                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=694");
                            return;

                        case 5:
                            base.Response.Redirect("Report.aspx?Parameter=699!Type.NEW^Page.1");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=1");
                }
            }
        }

        private void btnOK_Click_UPDATESTADIUM(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("StadiumID", 0);
            switch (BTPStadiumManager.SetStadiumUpdate(this.intUserID, request))
            {
                case 0:
                    base.Response.Redirect("Report.aspx?Parameter=120!Type.STADIUM");
                    return;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=121!Type.STADIUM");
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=122!Type.STADIUM");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=1");
        }

        private void btnOK_Click_UPDATETICKETPRICE(object sender, ImageClickEventArgs e)
        {
            int intTicketPrice = Convert.ToInt32(this.ddlTicketPrice.SelectedValue);
            if (BTPStadiumManager.UpdateTicketPrice(this.intUserID, intTicketPrice) == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=123!Type.STADIUM");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void btnOK_Click_UPDATETOPWEALTH(object sender, ImageClickEventArgs e)
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'>";
            int request = SessionItem.GetRequest("UserID", 0);
            switch (BTPOnlyOneCenterReg.SetTopWealth(this.intUserID, request))
            {
                case -3:
                    this.strSay = "您没有足够的游戏币！";
                    break;

                case -2:
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                    return;

                case -1:
                    this.strSay = "对不起，对手已经开始比赛！";
                    return;

                case 0:
                    break;

                case 1:
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                    return;

                default:
                    return;
            }
        }

        private void btnOK_Click_USESTAFF(object sender, ImageClickEventArgs e)
        {
            int num2 = (int) BTPParameterManager.GetParameterRow()["UseStaffWealth"];
            this.btnOK.Visible = false;
            if (this.intWealth < num2)
            {
                this.strSay = this.strNickName + "经理，您好！您没有足够游戏币";
            }
            else
            {
                int request = SessionItem.GetRequest("DevMatchID", 0);
                int num4 = BTPDevMatchManager.SetUseStaff(this.intUserID, request);
                int num5 = SessionItem.GetRequest("Pos", 0);
                if (num4 == 1)
                {
                    int num6 = SessionItem.GetRequest("Ref", 0);
                    if (this.blnIsHonor)
                    {
                        if (num5 == 1)
                        {
                            base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=902b!UserID.", this.intUserID, "^Ref.", num6 }));
                        }
                        else
                        {
                            base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=902!UserID.", this.intUserID, "^Devision.", this.strDevCode, "^Page.1" }));
                        }
                    }
                    else if (num5 == 1)
                    {
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=903b!UserID.", this.intUserID, "^Ref.", num6 }));
                    }
                    else
                    {
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=903!UserID.", this.intUserID, "^Devision.", this.strDevCode, "^Page.1" }));
                    }
                }
                else
                {
                    switch (num4)
                    {
                        case -2:
                            this.strSay = this.strNickName + "经理，您好，您的球队没有参加这场职业联赛！";
                            return;

                        case -1:
                            this.strSay = this.strNickName + "经理，您好，您没有足够的游戏币！";
                            return;
                    }
                }
            }
        }

        private void btnOrderOK_Click_CreateWealthOrder(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("Price", 0);
            int intWealth = SessionItem.GetRequest("Wealth", 0);
            DataRow row = BTPOrderManager.AddWealthOrder(this.intUserID, this.intType, request, intWealth, 1);
            int num3 = (int) row["Count"];
            long num4 = (long) row["AllMoney"];
            switch (num3)
            {
                case -5:
                    base.Response.Redirect("Report.aspx?Parameter=802");
                    return;

                case -3:
                    base.Response.Redirect("Report.aspx?Parameter=804");
                    return;

                case -2:
                    base.Response.Redirect("Report.aspx?Parameter=490");
                    return;

                case -1:
                    base.Response.Redirect("Report.aspx?Parameter=801");
                    return;

                case 0:
                    base.Response.Redirect("Report.aspx?Parameter=803!Type.WEALTHWEALLTH");
                    return;
            }
            if (num3 > 0)
            {
                base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=ORDERSAY&Money=", num4, "&Count=", num3 }));
            }
        }

        private void BuyStock()
        {
            this.intStockUserID = SessionItem.GetRequest("StockUserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intStockUserID);
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (row2 == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.lgMoney = (long) row2["Money"];
            }
            if ((this.intStockUserID <= 0) || (accountRowByUserID == null))
            {
                this.strSay = this.strNickName + "经理，您好！请确认要购入的股票！";
            }
            else
            {
                string str = (string) accountRowByUserID["NickName"];
                int num = Convert.ToInt32(accountRowByUserID["PayType"]);
                int stockTeamDay = BTPStockManager.GetStockTeamDay(this.intUserID, this.intStockUserID);
                int userStockPrice = BTPStockManager.GetUserStockPrice(this.intStockUserID);
                int num4 = userStockPrice / 20;
                if (stockTeamDay > 0)
                {
                    this.strSay = this.strNickName + "经理您好，您已经拥有" + str + "经理的股份，不可以再购入";
                }
                else if (num == 0)
                {
                    this.strSay = this.strNickName + "经理您好，" + str + "经理不是会员，你不可以购入他的股票";
                }
                else if (userStockPrice < 0x1e8480)
                {
                    this.strSay = this.strNickName + "经理您好，" + str + "经理市值小于200万，你不可以购入他的股票";
                }
                else if (this.lgMoney < num4)
                {
                    this.strSay = this.strNickName + "经理您好，您的资金不足以购买该股票" + this.lgMoney;
                }
                else
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，购入", str, "经理的股票将花费你资金:", num4, "，您确定要购入吗？" });
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_BUYSTOCK);
                }
            }
        }

        private void BuyTool()
        {
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow toolRowByID = BTPToolManager.GetToolRowByID(request);
            int num3 = (int) toolRowByID["CoinCost"];
            string str = toolRowByID["ToolName"].ToString().Trim();
            int num4 = (int) toolRowByID["AmountInStock"];
            int num5 = (byte) toolRowByID["Category"];
            this.tbPresent.Visible = true;
            this.intWealth = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
            if (this.intWealth < num3)
            {
                this.strSay = this.strNickName + "经理，你的游戏币不足，无法购买该道具。";
                this.tbPresent.Visible = false;
                return;
            }
            if (num4 <= 0)
            {
                this.strSay = this.strNickName + "经理，该物品存货不足，无法购买。";
                return;
            }
            switch (num5)
            {
                case 7:
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str, "么？<br><strong>这将花费您<font color='red' size='5'>", num3, "</font>游戏币。</strong><br>持有会员卡，您将会获得会员相应的功能。" });
                    this.tbCount.Enabled = false;
                    goto Label_0337;

                case 8:
                {
                    int num7 = (byte) toolRowByID["TicketCategory"];
                    int num8 = 0;
                    switch (num7)
                    {
                        case 1:
                            num8 = 200;
                            break;

                        case 2:
                            num8 = 500;
                            break;

                        case 3:
                            num8 = 0x640;
                            break;
                    }
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str, "么？<br><strong>这将花费您<font color='red' size='5'>", num3, "</font>游戏币。</strong><br>并使您获得", num8, "的游戏币。" });
                    goto Label_0337;
                }
                case 11:
                {
                    this.tbPosistion.Visible = false;
                    int num6 = (byte) toolRowByID["TicketCategory"];
                    int num = 0;
                    switch (num6)
                    {
                        case 1:
                            num = 0x1e8480;
                            break;

                        case 2:
                            num = 0xe4e1c0;
                            break;
                    }
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str, "么？<br><strong>这将花费您<font color='red' size='5'>", num3, "</font>游戏币。</strong><br>并使您获得", num, "的游戏资金。" });
                    goto Label_0337;
                }
                case 2:
                case 9:
                    this.tbCount.Enabled = false;
                    this.cbPresent.Visible = false;
                    break;

                case 0x10:
                    this.tbCount.Enabled = false;
                    this.cbPresent.Visible = false;
                    break;
            }
            this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str, "么？<br><strong>这将花费您<font color='red' size='5'>", num3, "</font>游戏币。</strong>" });
        Label_0337:
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_BuyTool);
        }

        private void BuyWealthTool()
        {
            string str;
            this.intWealth = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow wealthToolRowByID = BTPWealthToolManager.GetWealthToolRowByID(request);
            int num2 = (int) wealthToolRowByID["WealthCost"];
            int num3 = (int) wealthToolRowByID["PayCost"];
            string str2 = wealthToolRowByID["ToolName"].ToString().Trim();
            int num4 = (int) wealthToolRowByID["AmountInStock"];
            int num5 = (byte) wealthToolRowByID["Category"];
            int num6 = num2;
            this.tbPresent.Visible = true;
            if (this.intPayType == 1)
            {
                str = "您已经加入了会员，只需要付<font color='red' size='5'>" + num3 + "</font>枚游戏币！";
                num6 = num3;
            }
            else
            {
                str = "加入会员您只需要付<font color='red' size='5'>" + num3 + "</font>枚游戏币！";
            }
            if (this.intWealth >= num6)
            {
                if (num4 > 0)
                {
                    if (num5 == 7)
                    {
                        this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str2, "么？<br><strong>这将花费您<font color='red' size='5'>", num2, "</font>游戏币。</strong><br>持有会员卡，您将会获得会员相应的功能。", str });
                        this.tbCount.Enabled = false;
                    }
                    else
                    {
                        this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定要购买", str2, "么？<br><strong>这将花费您<font color='red' size='5'>", num2, "</font>游戏币。</strong>", str });
                    }
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_BuyWealthTool);
                }
                else
                {
                    this.strSay = this.strNickName + "经理，该物品存货不足，无法购买。";
                }
            }
            else
            {
                this.tbPresent.Visible = false;
                this.strSay = this.strNickName + "经理，你的游戏币不足，无法购买该道具。";
            }
        }

        private void CancelFocus()
        {
            this.strSay = "您是否要撤销对此球员的关注？撤销关注并不能撤销您对其的出价。";
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CANCELFOCUS);
        }

        private void CancelOrder()
        {
            this.tbSay.Visible = true;
            this.strSay = this.strNickName + "经理，您确定要撤销订单吗？";
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CancelOrder);
        }

        private void CancelPrearrange()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Tag", 0);
            DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(request);
            if (devMRowByDevMatchID != null)
            {
                int num2 = (int) devMRowByDevMatchID["ClubAID"];
                int num3 = (int) devMRowByDevMatchID["ClubHID"];
                devMRowByDevMatchID["ArrangeH"].ToString().Trim();
                devMRowByDevMatchID["ArrangeA"].ToString().Trim();
                if ((num2 == this.intClubID5) || (num3 == this.intClubID5))
                {
                    this.strSay = "您确定要撤销该场比赛的预设战术么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrangeA);
                }
                else
                {
                    this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                }
            }
        }

        private void CancelPrearrangeXBA()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Tag", 0);
            DataRow xGroupMatchRowByID = BTPXGroupMatchManager.GetXGroupMatchRowByID(request);
            if (xGroupMatchRowByID != null)
            {
                int num2 = (int) xGroupMatchRowByID["ClubBID"];
                int num3 = (int) xGroupMatchRowByID["ClubAID"];
                xGroupMatchRowByID["ArrangeH"].ToString().Trim();
                xGroupMatchRowByID["ArrangeA"].ToString().Trim();
                if ((num2 == this.intClubID5) || (num3 == this.intClubID5))
                {
                    this.strSay = "您确定要撤销该场比赛的预设战术么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrangeAXBA);
                }
                else
                {
                    this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                }
            }
        }

        private int CanUseAddPower()
        {
            if (ToolItem.HasWealthTool(this.intUserID, 11, 0) < 1)
            {
                return -2;
            }
            return 1;
        }

        private long CanUseBidAuto(bool blnOnlyHas)
        {
            if (ToolItem.HasTool(this.intUserID, 2, 0) < 1)
            {
                return -2L;
            }
            if (blnOnlyHas)
            {
                return 1L;
            }
            return this.CheckCanBid(5, 10);
        }

        private int CanUseHide()
        {
            int intWealth = (int) Global.drParameter["HideSkill"];
            return BTPAccountManager.HasEnoughWealth(this.intUserID, intWealth);
        }

        private int CanUseMatchLev()
        {
            int intWealth = (int) Global.drParameter["MatchLev"];
            return BTPAccountManager.HasEnoughWealth(this.intUserID, intWealth);
        }

        private int CanUseMax()
        {
            int intWealth = (int) Global.drParameter["MaxSkill"];
            return BTPAccountManager.HasEnoughWealth(this.intUserID, intWealth);
        }

        private int CanUseMinusAge()
        {
            if (ToolItem.HasWealthTool(this.intUserID, 13, 0) < 1)
            {
                return -2;
            }
            return 1;
        }

        private int CanUsePrivateSkill()
        {
            int intWealth = (int) Global.drParameter["PrivateSkill"];
            return BTPAccountManager.HasEnoughWealth(this.intUserID, intWealth);
        }

        private int CanUseShowSkill()
        {
            if (ToolItem.HasTool(this.intUserID, 3, 0) < 1)
            {
                return -2;
            }
            return 1;
        }

        private void CanUseTool(int intCheckType)
        {
            object strToolMsg;
            this.strToolMsg = "";
            if ((intCheckType == 4) || (intCheckType == 6))
            {
                strToolMsg = this.strToolMsg;
                this.strToolMsg = string.Concat(new object[] { strToolMsg, "<a href='SecretaryPage.aspx?Type=DEVBIDHELPER&PlayerID=", this.longPlayerID, "&Market=", this.intMarket, "'><img alt='拍卖委托' src='", SessionItem.GetImageURL(), "BidAuto.gif' height='16' width='16' border='0'></a>&nbsp;&nbsp;" });
            }
            strToolMsg = this.strToolMsg;
        }

        private long CheckCanBid(int intClubType, int intCheckType)
        {
            int num;
            int num2;
            int num3;
            DataRow playerRowByPlayerID;
            int num4;
            if (BTPBidAutoManager.GetAutoBidRowByUserID(this.intUserID) != null)
            {
                return -8L;
            }
            if (intCheckType == 5)
            {
                num3 = 10;
            }
            else
            {
                num3 = 0;
            }
            if (intCheckType > 2)
            {
                num = BTPPlayer5Manager.GetPlayer5CountByCID(this.intClubID5);
                num2 = Config.Player5Count();
            }
            else
            {
                num = BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3);
                if (BTPToolLinkManager.HasTool(this.intUserID, 1, 0xc9, 0))
                {
                    num2 = 9;
                }
                else
                {
                    num2 = 8;
                }
            }
            if (num >= num2)
            {
                return -3L;
            }
            if (intClubType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            int intClubID = (int) playerRowByPlayerID["ClubID"];
            int num6 = (byte) playerRowByPlayerID["Category"];
            BTPClubManager.GetUserIDByClubID(intClubID);
            int num7 = 0;
            if ((intCheckType == 4) || (intCheckType == 10))
            {
                string devCodeByUserID = BTPDevManager.GetDevCodeByUserID(this.intUserID);
                string strDevCode = playerRowByPlayerID["MarketCode"].ToString().Trim();
                int level = DevCalculator.GetLevel(strDevCode);
                int num9 = DevCalculator.GetLevel(devCodeByUserID);
                if ((((level > this.intMarketLevel) || (num9 > this.intMarketLevel)) && ((level <= this.intMarketLevel) || (num9 <= this.intMarketLevel))) && (strDevCode != "AAA"))
                {
                    return -10L;
                }
            }
            if (intClubType == 3)
            {
                if ((num6 == 6) && (this.intCategory != 5))
                {
                    return -4L;
                }
                if (((num6 == 4) && (this.intCategory != 2)) && (this.intCategory != 5))
                {
                    return -4L;
                }
            }
            if (intClubType == 5)
            {
                if ((num6 == 2) && (this.intCategory != 5))
                {
                    return -4L;
                }
                if ((num6 == 3) && (this.intCategory != 5))
                {
                    return -4L;
                }
                if ((num6 == 4) && (this.intCategory != 5))
                {
                    return -4L;
                }
            }
            if (num7 > 100)
            {
                return -4L;
            }
            if (num6 == 1)
            {
                return -9L;
            }
            DataRow bidderByUserID = BTPBidderManager.GetBidderByUserID(this.intUserID);
            if ((bidderByUserID != null) && ((bool) bidderByUserID["TopPrice"]))
            {
                return -5L;
            }
            playerRowByPlayerID["Name"].ToString();
            DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
            if (DateTime.Now.AddMinutes((double) num3) > time)
            {
                return -6L;
            }
            if (intCheckType > 3)
            {
                num4 = this.intClubID5;
            }
            else
            {
                num4 = this.intClubID3;
            }
            if (intClubID == num4)
            {
                return -7L;
            }
            long num10 = Convert.ToInt64(playerRowByPlayerID["BidPrice"]);
            int num1 = (int) playerRowByPlayerID["BidCount"];
            if (intCheckType == 8)
            {
                num10 = (int) playerRowByPlayerID["BidWealth"];
            }
            if (intCheckType < 4)
            {
                num10 = Convert.ToInt32(BTPPlayer3Manager.GetPlayer3Cost(this.longPlayerID));
            }
            if ((intClubType == 3) && (num6 == 3))
            {
                return 0x186a0L;
            }
            return num10;
        }

        private void Choose()
        {
            int num = 0;
            DataRow row = BTPToolLinkManager.GetToolByUserIDTCategory(this.intUserID, 6, 0);
            if (row != null)
            {
                num = (int) row["Amount"];
            }
            if (num <= 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=6970");
            }
            else
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                DataRow row2 = BTPArrange3Manager.GetCheckArrange3(this.intClubID3, this.longPlayerID);
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                this.intCategory = (byte) playerRowByPlayerID["Category"];
                DataRow row4 = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                string str = row4["Name"].ToString().Trim();
                this.strPlayer5Name = str;
                int num2 = (byte) row4["Age"];
                int num3 = BTPPlayer3Manager.GetPlayer3CountByClubID(this.intClubID3);
                int num4 = BTPPlayer5Manager.GetSellClub5PCount(this.intClubID5);
                if ((BTPBidderManager.GetBidderCountByUserID(this.intUserID) > 0) && (num4 > 12))
                {
                    this.strSay = this.strNickName + "经理，咱们俱乐部在转会市场有出价记录，暂时不能提拔街球队员到职业队来！";
                }
                else if (this.intCategory == 1)
                {
                    if (row2 != null)
                    {
                        this.strSay = this.strNickName + "经理您好，球员" + str + "处于阵容中，请将其移出阵容再进行选拔。";
                    }
                    else if (num2 < 0x12)
                    {
                        this.strSay = this.strNickName + "经理您好，球员" + str + "年龄过小，不能进入职业队。";
                    }
                    else if (num3 < 5)
                    {
                        this.strSay = this.strNickName + "经理您好，您的街球队球员过少，无法选拔。";
                    }
                    else if (num4 >= Config.Player5Count())
                    {
                        this.strSay = this.strNickName + "经理您好，您的职业队球员过多，无法选拔。";
                    }
                    else
                    {
                        this.strSay = this.strNickName + "经理您好，您确定要将" + str + "从街球队选拔到职业队么？选拔后将会花费您一个选拔卡。";
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CHOOSE);
                    }
                }
                else if (this.intCategory == 4)
                {
                    this.strSay = this.strNickName + "经理您好，球员" + str + "正在进行拍卖，无法选拔。";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=1");
                }
            }
        }

        private void ChooseClub()
        {
            this.tbChooseClub.Visible = true;
            this.intLookUserID = SessionItem.GetRequest("UserID", 0);
            int request = SessionItem.GetRequest("Tag", 0);
            if (request == 1)
            {
                this.strToolMsg = "您输入的俱乐部名有重复（不符合规定）";
            }
            else
            {
                this.strToolMsg = "您要认领此俱乐部吗？请输入新的俱乐部名称并确定。";
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CHOOSECLUB);
        }

        private void CreateDevCup()
        {
            string strCupName;
            int num;
            DevCupData data = (DevCupData) this.Session["DevCup" + this.intUnionID];
            if (data != null)
            {
                strCupName = data.strCupName;
                int intCreateCharge = data.intCreateCharge;
                int intMedalCharge = data.intMedalCharge;
                int intHortationAll = data.intHortationAll;
                num = (intCreateCharge + intMedalCharge) + intHortationAll;
            }
            else
            {
                this.strSay = "杯赛暂时无法被创建，请稍候再试。";
                return;
            }
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num5 = (byte) accountRowByUserID["UnionCategory"];
            int num6 = (int) accountRowByUserID["Wealth"];
            this.strSay = string.Concat(new object[] { this.strNickName, "经理，建立杯赛", strCupName, "将花费联盟", num, "游戏币，是否创建？" });
            if (num5 != 1)
            {
                if (num6 < num)
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理您游戏币不足，建立杯赛", strCupName, "需要您先向联盟捐赠", num, "游戏币！" });
                }
                else
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，建立杯赛", strCupName, "，您需要先向联盟捐赠", num, "游戏币，是否创建？" });
                }
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CreateDevCup);
        }

        private void CreateUserCup()
        {
            string strCupName;
            int num;
            DevCupData data = (DevCupData) this.Session["DevCup" + this.intUserID];
            if (data != null)
            {
                strCupName = data.strCupName;
                int intCreateCharge = data.intCreateCharge;
                int intMedalCharge = data.intMedalCharge;
                int intHortationAll = data.intHortationAll;
                num = (intCreateCharge + intMedalCharge) + intHortationAll;
            }
            else
            {
                this.strSay = "杯赛暂时无法被创建，请稍候再试。";
                return;
            }
            this.strSay = string.Concat(new object[] { this.strNickName, "经理，建立杯赛", strCupName, "将花费联盟", num, "游戏币，是否创建？" });
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CreateUserCup);
        }

        private void CreateWealthOrder()
        {
            this.intType = SessionItem.GetRequest("State", 0);
            DataRow orderParameter = BTPOrderManager.GetOrderParameter();
            this.strBuyOne = orderParameter["BuyOne"].ToString();
            this.strSaleOne = orderParameter["SaleOne"].ToString();
            if (this.strBuyOne.Trim() == "0")
            {
                this.strBuyOne = "无订单";
            }
            if (this.strSaleOne.Trim() == "0")
            {
                this.strSaleOne = "无订单";
            }
            if (this.intType == 1)
            {
                this.tbBuyCount.Attributes["onkeyup"] = " getBuyMoney();";
                this.tbBuyPrice.Attributes["onkeyup"] = " getBuyMoneyP();";
                this.tbBuyOrder.Visible = true;
                this.tbBuyCount.Attributes["onblur"] = " EndBuyEvents();";
            }
            else
            {
                this.tbBCount.Attributes["onkeyup"] = " getMoney();";
                this.tbPrice.Attributes["onkeyup"] = " getMoneyP();";
                this.tbWealthOrder.Visible = true;
                this.tbBCount.Attributes["onblur"] = " EndEvents();";
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_CreateWealthOrder);
        }

        private void DeleteDevCup()
        {
            this.intDevCupID = SessionItem.GetRequest("DevCupID", 0);
            string str = "";
            DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID);
            if (devCupRowByDevCupID != null)
            {
                str = devCupRowByDevCupID["Name"].ToString().Trim();
            }
            else
            {
                this.strSay = "杯赛不存在，请确认后再删除。";
                return;
            }
            this.strSay = this.strNickName + "经理，您确定要删除自定义杯赛" + str + "么？";
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DeleteDevCup);
        }

        private void DeleteUserDevCup()
        {
            this.intDevCupID = SessionItem.GetRequest("DevCupID", 0);
            string str = "";
            DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID);
            if (devCupRowByDevCupID != null)
            {
                str = devCupRowByDevCupID["Name"].ToString().Trim();
            }
            else
            {
                this.strSay = "杯赛不存在，请确认后再删除。";
                return;
            }
            this.strSay = this.strNickName + "经理，您确定要删除自定义杯赛" + str + "么？";
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DeleteUserDevCup);
        }

        private void DevBidHelper()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.intMarket = SessionItem.GetRequest("Market", 0);
            if (this.intPayType != 1)
            {
                this.strSay = "您该功能只对会员开放。";
            }
            else
            {
                long num = this.CheckCanBid(5, 10);
                this.tblTool.Visible = false;
                if (num > 0L)
                {
                    this.tblDevBidHelper.Visible = true;
                    this.strSay = "只要告诉我您能接受的最高价，我就可以使用拍卖委托帮助您出价，此球员我建议您最好出价高于" + ((num / 10L) * 15L) + "，我会尽全力帮您拍到球员，但有时也会流拍。";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DEVBIDHELPER);
                }
                else
                {
                    long num2 = num;
                    if ((num2 <= -2L) && (num2 >= -12L))
                    {
                        switch (((int) (num2 - -12L)))
                        {
                            case 0:
                                this.strSay = "经理，咱们俱乐部工资总和已经达到或超过工资帽了不能再购买球员了！";
                                return;

                            case 1:
                                return;

                            case 2:
                                this.strSay = "这名球员不能进行跨级转会啊！";
                                return;

                            case 3:
                                this.strSay = "这名球员并不在拍卖中，您看看是否是搞错了？";
                                return;

                            case 4:
                                this.strSay = "一次只能使用一个拍卖委托，不然我照顾不过来啊！";
                                return;

                            case 5:
                                this.strSay = "这是咱们球队中的球员啊。";
                                return;

                            case 6:
                                this.strSay = "这个球员的截止时间已经接近了，您没有必要使用拍卖委托了。";
                                return;

                            case 7:
                                this.strSay = "您是否在此段时间已经有过出价？我还不能在此段时间使用拍卖委托。";
                                return;

                            case 8:
                                this.strSay = "咱们不被允许对此球员进行出价，请选择其他的球员。";
                                return;

                            case 9:
                                this.strSay = "咱们球队中的球员已经足够多了，我们不能再引进更多的球员了。";
                                return;

                            case 10:
                                this.strSay = "您已经没有拍卖委托了，我无法帮您进行出价，请到游戏币商店购买更多的拍卖委托。";
                                return;
                        }
                    }
                }
            }
        }

        private void DevChoose()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            if (BTPPlayer5Manager.GetSellClub5PCount(this.intClubID5) > 13)
            {
                this.strSay = this.strNickName + "经理您好，您的职业队球员过多，无法进行选秀。";
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                Convert.ToInt32(playerRowByPlayerID["ClubID"]);
                int num = (int) playerRowByPlayerID["BidderID"];
                if ((num > 0) && (this.intPayType == 0))
                {
                    this.strSay = this.strNickName + "只有会员才可以对已被选球员进行选秀";
                }
                else
                {
                    string str = playerRowByPlayerID["Name"].ToString().Trim();
                    DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
                    if (time > DateTime.Now)
                    {
                        this.strSay = this.strNickName + "经理，您确定要对球员" + str + "进行选秀么？";
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DEVCHOOSE);
                    }
                    else
                    {
                        this.strSay = this.strNickName + "经理，球员" + str + "的允许选秀时间已经结束，无法再对其进行选秀了。";
                    }
                }
            }
        }

        private void DevCupTest()
        {
            this.tbSay.Visible = true;
            this.strSay = "自建杯赛已经移至（赛程管理－>自建杯赛）请到相应的菜单栏查看！";
        }

        private void Devision()
        {
            if (this.intCategory != 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=60!Type.TRANSFER^Pos.1^Order.4^Page.1");
            }
            else
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.intMarket = SessionItem.GetRequest("Market", 0);
                this.CanUseTool(this.intMarket);
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if ((this.intMarket == 4) || (this.intMarket == 6))
                {
                    DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
                    int day = DateTime.Now.Day;
                    int num2 = time.Day;
                    if (day >= num2)
                    {
                        int num1 = DateTime.Now.Hour - time.Hour;
                    }
                }
                bool flag = (bool) playerRowByPlayerID["SellAss"];
                string str = playerRowByPlayerID["Name"].ToString().Trim();
                if (flag && (this.intPayType != 1))
                {
                    this.strSay = this.strNickName + "经理，您好！球员" + str + "只对会员出售，请加入会员获得更多服务！";
                }
                else
                {
                    long num3 = this.CheckCanBid(5, 4);
                    long num6 = num3;
                    if ((num6 <= -3L) && (num6 >= -12L))
                    {
                        switch (((int) (num6 - -12L)))
                        {
                            case 0:
                                this.strSay = this.strNickName + "经理，咱们俱乐部工资总和已经达到或超过工资帽了不能再购买球员了！";
                                return;

                            case 2:
                                this.strSay = this.strNickName + "经理，您好！此球员无法跨等级进行转会！";
                                return;

                            case 3:
                                this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                                return;

                            case 4:
                                this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                                return;

                            case 5:
                                this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                                return;

                            case 6:
                                this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                                return;

                            case 7:
                                this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                                return;

                            case 8:
                                this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                                return;

                            case 9:
                                this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                                return;
                        }
                    }
                    this.tblFreeBid.Visible = true;
                    this.tbBidPrice.Visible = false;
                    this.strBidMsg = "采用明拍方式";
                    long num4 = (num3 / 100L) * 0x66L;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，此次竞拍您至少需要出价", num4, "。" });
                    int num5 = (byte) Global.drParameter["Duty"];
                    if (num5 > 0)
                    {
                        object strSay = this.strSay;
                        this.strSay = string.Concat(new object[] { strSay, "，并且在赢得竟拍后缴纳", num5, "%的消费税。" });
                    }
                    this.strSay = this.strSay + "<br/>";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DEVISION);
                }
            }
        }

        private void DevStreetBid()
        {
            int num = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Category"];
            if (num != 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=2");
            }
            else
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.CanUseTool(2);
                long num2 = this.CheckCanBid(3, 2);
                int num3 = 0;
                int num4 = 0;
                long num6 = num2;
                if ((num6 <= -3L) && (num6 >= -9L))
                {
                    switch (((int) (num6 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                this.tblFreeBid.Visible = true;
                this.tbBidPrice.Visible = false;
                this.strBidMsg = "采用暗拍方式";
                long num5 = num2;
                num5 = ((num5 * 3L) / 2L) + 0x1388L;
                num5 /= 0x2710L;
                if (num5 < 1L)
                {
                    num5 = 1L;
                }
                if ((num3 == 0) && (num4 == 0))
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num5, "万以上。" });
                }
                else if (this.intPayType == 1)
                {
                    this.intPayWealth = 0;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num5, "万以上。" });
                }
                else
                {
                    this.intPayWealth = num4;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num5, "万以上。" });
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DEVSTREETBID);
            }
        }

        private void ExtendStaff()
        {
            if (this.intPayType == 1)
            {
                this.intStaffID = SessionItem.GetRequest("StaffID", 0);
                BTPStaffManager.SetStaffSalary(this.intStaffID);
                DataRow staffRowByID = BTPStaffManager.GetStaffRowByID(this.intStaffID);
                string str = staffRowByID["Name"].ToString().Trim();
                this.intType = Convert.ToInt32(staffRowByID["Type"]);
                string staffChsPosition = PlayerItem.GetStaffChsPosition(this.intType);
                this.intSalary = (int) staffRowByID["Salary"];
                this.intSalary = (this.intSalary * (100 + (10 + (this.intStaffID % 8)))) / 100;
                this.intClubIDS = (int) staffRowByID["ClubID"];
                this.strSay = string.Concat(new object[] { "您将以每轮", this.intSalary, "的资金，与", staffChsPosition, str, "续约，续约期限为5轮。您愿一次性支付", this.intSalary * 5, "的资金与其续约吗？" });
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_EXTENDSTAFF);
            }
            else
            {
                this.strSay = "职员续约功能仅对会员开放，请加入会员以便得到更多的服务。";
            }
        }

        private void FieldDef()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("CID", 0);
            int intUserID = SessionItem.GetRequest("UID", 0);
            if (this.intClubID5 < 1)
            {
                this.strSay = "您没有职业队！";
            }
            else if (intUserID > 0)
            {
                this.tbSay.Visible = true;
                if (request < 1)
                {
                    this.strSay = "此场比赛不存在！";
                }
                else
                {
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                    if (accountRowByUserID != null)
                    {
                        this.intUnionID = (int) accountRowByUserID["UnionID"];
                        if (this.intUnionID >= 1)
                        {
                            string str = accountRowByUserID["NickName"].ToString().Trim();
                            this.strSay = "您确定要接受" + str + "的挑战吗？";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_FIELDDEF);
                        }
                        else
                        {
                            this.strSay = "您要挑战的联盟不存在，请重新尝试！";
                        }
                    }
                    else
                    {
                        this.strSay = "您要挑战的联盟不存在，请重新尝试！";
                    }
                }
            }
            else
            {
                this.strSay = "您要挑战的联盟不存在，请重新尝试！";
            }
        }

        private void FieldReg()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("UID", 0);
            int num2 = SessionItem.GetRequest("RT", 0);
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(request);
            if (this.intClubID5 < 1)
            {
                this.strSay = "您没有职业队！";
            }
            else if (unionRowByID != null)
            {
                this.tbSay.Visible = true;
                if (request == this.intUnionID)
                {
                    this.strSay = "您不能挑战自己的联盟！";
                }
                else
                {
                    string str = unionRowByID["Name"].ToString().Trim();
                    this.strSay = string.Concat(new object[] { "您确定要对", str, "联盟进行挑战吗？（抵押威望为", num2, "）" });
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_FIELDREG);
                }
            }
            else
            {
                this.strSay = "您要挑战的联盟不存在，请重新尝试！";
            }
        }

        private void FireStaff()
        {
            this.intStaffID = SessionItem.GetRequest("StaffID", 0);
            DataRow staffRowByID = BTPStaffManager.GetStaffRowByID(this.intStaffID);
            int num = (int) staffRowByID["ClubID"];
            string str = staffRowByID["Name"].ToString();
            if ((num != this.intClubID3) && (num != this.intClubID5))
            {
                this.strSay = this.strNickName + "经理，" + str + "不是" + this.strClubName3 + "中的职员您无权将其解雇。";
            }
            else
            {
                byte intPosition = (byte) staffRowByID["Type"];
                this.strSay = this.strNickName + "经理，" + str + "在" + this.strClubName3 + "中担任" + PlayerItem.GetStaffChsPosition(intPosition) + "。您确定要将其解雇吗？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_FRIESTAFF);
            }
        }

        private void GetFreeBox()
        {
            this.tbSay.Visible = true;
            int num = ToolItem.HasTool(this.intUserID, 12, 1);
            string str = SessionItem.GetRequest("Name", 1).ToString().Trim();
            if (num > 0)
            {
                this.strSay = "您确定要领取您的奖品 " + str + " 吗？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_FREEBOX);
            }
            else
            {
                this.strSay = "您没有领取奖品的资格！";
            }
        }

        private void GetNoBox()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Status", 0);
            int num2 = (int) BTPParameterManager.GetParameterRow()["BoxWealth"];
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='cursor:pointer' onclick='javascript:window.location=\"ManagerTool.aspx?Type=USEBOX&Page=1\";'>";
            this.strSay = "您已经没有豆腐了";
        }

        private int GetOrderFactor(int intPay)
        {
            int num = Convert.ToInt32(BTPAccountManager.GetAccountRowByUserID(this.intUserID)["DevLvl"]);
            return (intPay = (intPay * (0x15 - num)) / 20);
        }

        private void GetPayBox()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Status", 0);
            int num2 = (int) BTPParameterManager.GetParameterRow()["BoxWealth"];
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='cursor:pointer' onclick='javascript:window.location=\"ManagerTool.aspx?Type=USEBOX&Page=1\";'>";
            switch (request)
            {
                case -4:
                    this.strSay = "您的游戏币不足 " + num2 + " 请到金币商店购买<a style='cursor:pointer' href='ManagerTool.aspx?Type=STORE&Page=1'>纪念章！</a>";
                    return;

                case 1:
                {
                    string str = SessionItem.GetRequest("Name", 1).ToString().Trim();
                    this.strSay = "您已经成功领取了您的奖品 " + str + " ，请注意查收！";
                    return;
                }
            }
            this.strSay = "您没有领取奖品的资格！";
        }

        private void GetStaff()
        {
            this.intStaffID = SessionItem.GetRequest("StaffID", 0);
            DataRow staffRowByID = BTPStaffManager.GetStaffRowByID(this.intStaffID);
            string str = "";
            string staffChsPosition = "";
            if (staffRowByID != null)
            {
                str = staffRowByID["Name"].ToString().Trim();
                this.intType = Convert.ToInt32(staffRowByID["Type"]);
                staffChsPosition = PlayerItem.GetStaffChsPosition(this.intType);
                this.intSalary = (int) staffRowByID["Salary"];
                this.intContract = (byte) staffRowByID["Contract"];
            }
            this.strSay = string.Concat(new object[] { "您将花费", this.intSalary * this.intContract, "资金，雇佣", staffChsPosition, "：", str, "。请选择球队" });
            if ((this.intCategory == 5) || (this.intCategory == 2))
            {
                this.tblStaff.Visible = true;
            }
            else
            {
                this.tblStaff1.Visible = true;
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_GETSTAFF);
        }

        private void Hide()
        {
            DataRow playerRowByPlayerID;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            this.intCheck = SessionItem.GetRequest("Check", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            int num = (int) playerRowByPlayerID["TeamDay"];
            int num2 = (int) playerRowByPlayerID["ClubID"];
            this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
            if (((this.intType == 3) && (num2 != this.intClubID3)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的意识水平。</p>";
            }
            else if (((this.intType == 5) && (num2 != this.intClubID5)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的意识水平。</p>";
            }
            else if (this.CanUseHide() == 1)
            {
                if ((this.intPlayerCategory == 1) && (num < 5))
                {
                    this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您的球员为您效力的时间太短，你还不能了解他的意识水平！</p>";
                    this.btnOK.Visible = false;
                }
                else
                {
                    this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您确定要对此球员使用意识评估报告吗？使用后您就能看到此球员的意识！这将花费您 " + Global.drParameter["HideSkill"].ToString().Trim() + " 枚游戏币。</p>";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_HIDE);
                }
            }
            else
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您的游戏币不足" + Global.drParameter["HideSkill"].ToString().Trim() + "，无法使用意识评估报告。</p>";
                this.btnOK.Visible = false;
            }
        }

        private void HirePlayer()
        {
            this.tbSay.Visible = true;
            int num = (int) BTPParameterManager.GetParameterRow()["SearchPlayer"];
            long request = SessionItem.GetRequest("PlayerID", 3);
            this.btnOK.Visible = false;
            if (BTPPlayer3Manager.GetPlayerRowByPlayerID(request) != null)
            {
                DataTable table = BTPPlayer3Manager.GetPlayer3TableByCIDCat(this.intClubID3, 0x58);
                if (table != null)
                {
                    if (table.Rows.Count == 8)
                    {
                        this.strSay = "您确定录用该球员吗？<br />（需花费" + num + "枚游戏币）<br />(优先使用活跃积分)";
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_HirePlayer);
                    }
                    else
                    {
                        base.Response.Redirect("PlayerCenter.aspx?Type=3&UserID=" + this.intUserID);
                    }
                }
                else
                {
                    this.btnOK.Visible = false;
                    this.strSay = "您尚未雇佣球探，请先雇佣球探。";
                    this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
                }
            }
            else
            {
                this.btnOK.Visible = false;
                this.strSay = "您所要录用的球员不存在，请重新尝试。";
                this.strBtnCancel = string.Concat(new object[] { "<a href='PlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'><img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' border='0'></a>" });
            }
        }

        private void InitializeComponent()
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.strClubName3 = onlineRowByUserID["ClubName3"].ToString();
                this.strClubName5 = onlineRowByUserID["ClubName5"].ToString();
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strClubLogo = onlineRowByUserID["ClubLogo"].ToString().Trim();
                this.blnSex = (bool) onlineRowByUserID["Sex"];
                this.intPayType = (int) onlineRowByUserID["PayType"];
                this.intWealth = (int) onlineRowByUserID["Wealth"];
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.strDevCode = onlineRowByUserID["DevCode"].ToString();
                this.si = new SecretaryItem(this.intUserID, this.blnSex);
                this.strSecFace = this.si.GetImgFace();
                this.btnCancelPrearrange.Visible = false;
                this.tblPlayerBid.Visible = false;
                this.tblFreeBid.Visible = false;
                this.tblStaff.Visible = false;
                this.tblOldPlayer.Visible = false;
                this.tblTicketPrice.Visible = false;
                this.tblStaff1.Visible = false;
                this.tblPosistion.Visible = false;
                this.tblDevBidHelper.Visible = false;
                this.tblTool.Visible = false;
                this.tblShowSkill.Visible = false;
                this.tblExtendStaff.Visible = false;
                this.tblPlayer5Bid.Visible = false;
                this.tbPresent.Visible = false;
                this.tbChooseClub.Visible = false;
                this.btnOK.Visible = false;
                this.tbWealthOrder.Visible = false;
                this.trRefashion.Visible = false;
                this.tbBuyOrder.Visible = false;
                this.btnOrderOK.Visible = false;
                this.trSafe.Visible = false;
                this.btnInTeam.Visible = false;
                this.tblXGuess.Visible = false;
                this.btnSearchCancel.Visible = false;
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                this.strType = SessionItem.GetRequest("Type", 1);
                if (this.strType != "")
                {
                    switch (this.strType)
                    {
                        case "PLAYERCONTRACT":
                            this.PlayerContract();
                            break;

                        case "OLDUSER":
                            this.OldUser();
                            break;

                        case "SEARCHAGAIN":
                            this.SearchAgain();
                            break;

                        case "HIRE":
                            this.HirePlayer();
                            break;

                        case "FIELDDEF":
                            this.FieldDef();
                            break;

                        case "FIELDREG":
                            this.FieldReg();
                            break;

                        case "NOXBACUP":
                            this.NoXBACup();
                            break;

                        case "CANCELPREARR":
                            this.CancelPrearrange();
                            break;

                        case "CANCELPREARRXBA":
                            this.CancelPrearrangeXBA();
                            break;

                        case "PREARRANGEXBA":
                            this.PrearrangeXBA();
                            break;

                        case "PREARRANGE":
                            this.Prearrange();
                            break;

                        case "ONLYONEMATCH":
                            this.OnlyOneMatch();
                            break;

                        case "AFRESHARRANGE":
                            this.AfreshArrange();
                            break;

                        case "TIMEHOUSE5":
                            this.TimeHouse5();
                            break;

                        case "NOBOX":
                            this.GetNoBox();
                            break;

                        case "PAYBOX":
                            this.GetPayBox();
                            break;

                        case "FREEBOX":
                            this.GetFreeBox();
                            break;

                        case "DEVCUPTEST":
                            this.DevCupTest();
                            break;

                        case "TIMEHOUSE":
                            this.TimeHouse();
                            break;

                        case "SETAUTOTRAIN":
                            this.SetAutoTrain();
                            break;

                        case "MODIFYCLUBRETURN":
                            this.ModifyClubReturn();
                            break;

                        case "ADDDATAONLY":
                            this.AddDataOnly();
                            break;

                        case "ADDARRANGELVL":
                            this.AddArrangeLvl();
                            break;

                        case "USESTAFF":
                            this.SetUseStaff();
                            break;

                        case "MANGERSAY":
                            this.MangerSay();
                            break;

                        case "QUICKBUY":
                            this.QuickBuy();
                            break;

                        case "ADDPLAYERALL":
                            this.AddPlayerAll();
                            break;

                        case "ADDGUESSRECORD":
                            this.AddGuessRecord();
                            break;

                        case "ORDERSAY":
                            this.OrderSay();
                            break;

                        case "CANCELORDER":
                            this.CancelOrder();
                            break;

                        case "WEALTHORDER":
                            this.CreateWealthOrder();
                            break;

                        case "REFASHION":
                            this.RefashionPlayerSkill();
                            break;

                        case "CREATEUSERCUP":
                            this.CreateUserCup();
                            break;

                        case "CHOOSECLUB":
                            this.ChooseClub();
                            break;

                        case "CREATEDEVCUP":
                            this.CreateDevCup();
                            break;

                        case "INTROWEALTHTOOL":
                            this.IntroWealthTool();
                            break;

                        case "BUYWEALTHTOOL":
                            this.BuyWealthTool();
                            break;

                        case "DELDEVCUP":
                            this.DeleteDevCup();
                            break;

                        case "DELUSERDEVCUP":
                            this.DeleteUserDevCup();
                            break;

                        case "PLAYER3BID":
                            this.Player3Bid();
                            break;

                        case "PLAYER5BID":
                            this.Player5Bid();
                            break;

                        case "PLAYER3FIRE":
                            this.Player3Fire();
                            break;

                        case "PLAYER5FIRE":
                            this.Player5Fire();
                            break;

                        case "TRAINPLAYER3":
                            this.TrainPlayer3();
                            break;

                        case "NEWTRAINPLAYER3":
                            this.NewTrainPlayer3();
                            break;

                        case "TRAINPLAYER5":
                            this.TrainPlayer5();
                            break;

                        case "FIRESTAFF":
                            this.FireStaff();
                            break;

                        case "SHOWSKILL":
                            this.ShowSkill();
                            break;

                        case "PRIVATESKILL":
                            this.PrivateSkill();
                            break;

                        case "HIDE":
                            this.Hide();
                            break;

                        case "MAX":
                            this.Max();
                            break;

                        case "ADDPOWER":
                            this.AddPower();
                            break;

                        case "MINUSAGE":
                            this.MinusAge();
                            break;

                        case "MATCHLOOK":
                            this.MatchLook();
                            break;

                        case "MATCHLEV":
                            this.MatchLev();
                            break;

                        case "STREETFREE":
                            this.StreetFreeBid();
                            break;

                        case "STREETCHOOSE":
                            this.StreetChooseBid();
                            break;

                        case "DEVBIDHELPER":
                            this.DevBidHelper();
                            break;

                        case "GETSTAFF":
                            this.GetStaff();
                            break;

                        case "HONOR":
                            this.SetHonor();
                            break;

                        case "SETFOCUS":
                            this.SetFocus();
                            break;

                        case "SETTRIAL":
                            this.SetTrial();
                            break;

                        case "UNSETTRIAL":
                            this.UnSetTrial();
                            break;

                        case "CANCELFOCUS":
                            this.CancelFocus();
                            break;

                        case "BUYCLUB":
                            this.UpdatePrice();
                            break;

                        case "DEVSTREET":
                            this.DevStreetBid();
                            break;

                        case "CHOOSE":
                            this.Choose();
                            break;

                        case "UPDATESTADIUM":
                            this.UpdateStadium();
                            break;

                        case "TICKET":
                            this.UpdateTicketPrice();
                            break;

                        case "ADLINK":
                            this.AddADLink();
                            break;

                        case "DEVISION":
                            this.Devision();
                            break;

                        case "REGCUP":
                            this.RegCup();
                            break;

                        case "KICKOUT":
                            this.KickOut();
                            break;

                        case "ORDAIN":
                            this.Ordain();
                            break;

                        case "UNCHAIN":
                            this.Unchain();
                            break;

                        case "AGREE":
                            this.AgreeUnion();
                            break;

                        case "REFUSE":
                            this.RefuseUnion();
                            break;

                        case "OUT":
                            this.OutUnion();
                            break;

                        case "UNLAY":
                            this.UnlayUnion();
                            break;

                        case "POSITION":
                            this.SetPosition();
                            break;

                        case "BUYTOOL":
                            this.BuyTool();
                            break;

                        case "INTROTOOL":
                            this.IntroTool();
                            break;

                        case "DEVCHOOSE":
                            this.DevChoose();
                            break;

                        case "EXTENDSTAFF":
                            this.ExtendStaff();
                            break;

                        case "KICKOUTDEVCUP":
                            this.KickOutDevCup();
                            break;

                        case "KICKOUTUSERDEVCUP":
                            this.KickOutUserDevCup();
                            break;

                        case "ADDFRIEND":
                            this.AddFriend();
                            break;

                        case "BUYSTOCK":
                            this.BuyStock();
                            break;

                        case "XGUESS":
                            this.BetXGuess();
                            break;

                        case "STARVOTE":
                            this.StarVote();
                            break;

                        case "SETNPC":
                            this.SetNPC();
                            break;

                        case "POINT3MATCHREG":
                            this.RegPoint3Match();
                            break;

                        case "ARENAREG1":
                            this.ArenaUnionReg();
                            break;

                        case "ARENAREG2":
                            this.ArenaMemberReg();
                            break;
                    }
                }
                base.Load += new EventHandler(this.Page_Load);
            }
        }

        private void IntroTool()
        {
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow toolRowByID = BTPToolManager.GetToolRowByID(request);
            int num2 = (int) toolRowByID["CoinCost"];
            string str = toolRowByID["ToolName"].ToString().Trim();
            string str2 = toolRowByID["ToolIntroduction"].ToString().Trim();
            this.strSay = string.Concat(new object[] { "名称：", str, "<br>价值：", num2, "枚金币<br>说明：", str2 });
        }

        private void IntroWealthTool()
        {
            int request = SessionItem.GetRequest("ToolID", 0);
            DataRow wealthToolRowByID = BTPWealthToolManager.GetWealthToolRowByID(request);
            int num2 = (int) wealthToolRowByID["WealthCost"];
            string str = wealthToolRowByID["ToolName"].ToString().Trim();
            string str2 = wealthToolRowByID["ToolIntroduction"].ToString().Trim();
            this.strSay = string.Concat(new object[] { "名称：", str, "<br>价值：", num2, "游戏币<br>说明：", str2 });
        }

        private void KickOut()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            string str = row2["NickName"].ToString().Trim();
            row2 = BTPUnionFieldManager.GegUnionFieldRowByUserID(request);
            int num6 = 9;
            int num7 = 0;
            accountRowByUserID = BTPUnionPolity.GetDelMasterRow(this.intUnionID);
            if (accountRowByUserID != null)
            {
                num7 = (byte) accountRowByUserID["Status"];
            }
            if (num7 == 1)
            {
                this.strSay = this.strNickName + "经理，您的联盟正在进行盟主弹劾不能踢出盟员。";
            }
            else
            {
                if (row2 != null)
                {
                    num6 = (byte) row2["Status"];
                }
                if (num6 < 2)
                {
                    int num8 = 0;
                    int num9 = (byte) row2["OprationH"];
                    int num10 = (byte) row2["OprationA"];
                    int num11 = (int) row2["UserIDH"];
                    if (num11 == request)
                    {
                        num8 = num9;
                    }
                    else
                    {
                        num8 = num10;
                    }
                    switch (num8)
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，" + str + "有一场盟战正在进行，你将标记踢出此经理，此经理将在盟战结束后被踢出！";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_KickOut);
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，" + str + "已被您设置为踢出状态，他将在盟战结束后被踢出您的联盟。";
                            return;
                    }
                    this.strSay = "系统错误请重试！";
                }
                else if ((num3 != 1) && (num3 != 2))
                {
                    this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
                }
                else if (num4 != num2)
                {
                    this.strSay = this.strNickName + "经理，" + str + "不是您联盟的成员，请返回。";
                }
                else if ((num5 == 2) && (num3 != 1))
                {
                    this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
                }
                else if ((num5 == 2) && (num3 == 1))
                {
                    this.strSay = this.strNickName + "经理，" + str + "在您的联盟中担任管理员，请先解任。";
                }
                else if (num5 == 1)
                {
                    this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您确定将" + str + "踢出联盟么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_KickOut);
                }
            }
        }

        private void KickOutDevCup()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            int intDevCupID = SessionItem.GetRequest("DevCupID", 0);
            byte num3 = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionCategory"];
            if (request > 0)
            {
                switch (BTPDevCupRegManager.DelDevCupReg(request, intDevCupID, this.intUserID))
                {
                    case 1:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        if (num3 != 1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=531!Type.MYUNION^Kind.CUPREGMANAGE^DevCupID." + intDevCupID);
                            return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=531!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPREGMANAGE^DevCupID." + intDevCupID);
                        return;

                    case 2:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        if (num3 != 1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=533!Type.MYUNION^Kind.CUPREGMANAGE^DevCupID." + intDevCupID);
                            return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=533!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPREGMANAGE^DevCupID." + intDevCupID);
                        return;

                    case 3:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        if (num3 != 1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=534!Type.MYUNION^Kind.CUPREGMANAGE^DevCupID." + intDevCupID);
                            break;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=534!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPREGMANAGE^DevCupID." + intDevCupID);
                        return;

                    default:
                        return;
                }
            }
        }

        private void KickOutUserDevCup()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            int intDevCupID = SessionItem.GetRequest("DevCupID", 0);
            if (request > 0)
            {
                switch (BTPDevCupRegManager.DelUserDevCupReg(request, intDevCupID, this.intUserID))
                {
                    case 1:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        base.Response.Redirect("Report.aspx?Parameter=632!Type.CREATEDEVCUP^Status.CUPREGMANAGE^DevCupID." + intDevCupID);
                        return;

                    case 2:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        base.Response.Redirect("Report.aspx?Parameter=633!Type.CREATEDEVCUP^Status.CUPREGMANAGE^DevCupID." + intDevCupID);
                        return;

                    case 3:
                        BTPDevCupRegManager.SetDevCupDeadRound(intDevCupID, request, 0);
                        base.Response.Redirect("Report.aspx?Parameter=634!Type.CREATEDEVCUP^Status.MANAGER");
                        break;

                    default:
                        return;
                }
            }
        }

        private void MangerSay()
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.lgMoney = (long) accountRowByUserID["Money"];
                this.intLevel = Convert.ToInt32(accountRowByUserID["DevLvl"]);
                int num = 0;
                if ((this.intLevel != 1) && (this.intLevel < 10))
                {
                    num = 0x1e8480 + (0x493e0 * (10 - this.intLevel));
                }
                else if (this.intLevel == 1)
                {
                    num = 0x4c4b40;
                }
                else
                {
                    num = 0x1e8480;
                }
                num /= 10;
                if (this.lgMoney < num)
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您的资金不足，对球员进行奖励需要花费您 ", num / 0x2710, " 万资金！" });
                }
                else
                {
                    this.strSay = "您确定要向球员发 " + (num / 0x2710) + " 万奖金以鼓舞士气吗？重复使用效果不累计。";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MANGERSAY);
                }
            }
        }

        private void MatchLev()
        {
            this.intType = SessionItem.GetRequest("ClubType", 0);
            this.tblShowSkill.Visible = true;
            this.intLookUserID = SessionItem.GetRequest("UserID", 0);
            int num = this.CanUseMatchLev();
            this.intClubID = SessionItem.GetRequest("ClubID", 0);
            if (this.intLookUserID == this.intUserID)
            {
                base.Response.Redirect("Report.aspx?Parameter=552");
            }
            else if (this.intPayType == 1)
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">尊敬的会员您可以免费观看此经理的战术等级！</p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MATCHLEV);
            }
            else if (num == 1)
            {
                if (this.intType == 3)
                {
                    this.strToolMsg = "<p align='left' style=\"line-height:20px;\">是否要使用战术等级报告？使用后可以看到此经理的街球战术等级！这将花费您 " + Global.drParameter["MatchLev"].ToString().Trim() + " 枚游戏币。<font color='red'>会员可免费观看！</font></p>";
                }
                else
                {
                    this.strToolMsg = "<p align='left' style=\"line-height:20px;\">是否要使用战术等级报告？使用后可以看到此经理的职业战术等级！这将花费您 " + Global.drParameter["MatchLev"].ToString().Trim() + " 枚游戏币。<font color='red'>会员可免费观看！</font></p>";
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MATCHLEV);
            }
            else
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您的游戏币不足" + Global.drParameter["MatchLev"].ToString().Trim() + "，无法使用职业战术等级。</p>";
                this.btnOK.Visible = false;
            }
        }

        private void MatchLook()
        {
            string request = SessionItem.GetRequest("Tag", 1);
            this.intTag = Convert.ToInt32(StringItem.MD5Decrypt(request, Global.strMD5Key));
            this.tblShowSkill.Visible = true;
            this.strDevCode = SessionItem.GetRequest("DevCode", 1);
            this.intClubAID = SessionItem.GetRequest("A", 0);
            this.intClubBID = SessionItem.GetRequest("B", 0);
            this.intLookUserID = SessionItem.GetRequest("UserID", 0);
            if (this.intPayType == 1)
            {
                string str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", this.intTag, "&A=", this.intClubAID, "&B=", this.intClubBID, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                string str3 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", this.intTag, "&A=", this.intClubAID, "&B=", this.intClubBID, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                this.strToolMsg = "<p align='left'>尊敬的会员您可以免费观看此场比赛战报！</p><p align='center'>" + str2 + "&nbsp;" + str3 + "</p>";
                this.btnOK.Visible = false;
            }
            else if (BTPToolLinkManager.CheckMatchLook(this.intUserID, this.intTag) == 0)
            {
                this.strToolMsg = "<p align='left'>是否要花20枚游戏币来观看此场比赛战报？购买比赛录像后将在本赛季内随时点击录像图标进行查看！<br/></p><p align='center'><font color='red'>此功能会员免费！</font> <br/> <a href='ManagerTool.aspx?Type=STORE&Page=1' color=red>点击购买会员</font> </p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MATCHLOOK);
            }
            else
            {
                string str4 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", this.intTag, "&A=", this.intClubAID, "&B=", this.intClubBID, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                string str5 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", this.intTag, "&A=", this.intClubAID, "&B=", this.intClubBID, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                this.strToolMsg = "<p align='left'>点击下面的战报和技术统计图标进行查看</p><p align='center'>" + str4 + "&nbsp;" + str5 + "</p>";
                this.btnOK.Visible = false;
            }
        }

        private void Max()
        {
            DataRow playerRowByPlayerID;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            this.intCheck = SessionItem.GetRequest("Check", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            int num1 = (int) playerRowByPlayerID["TeamDay"];
            int num = (int) playerRowByPlayerID["ClubID"];
            this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
            if (((this.intType == 3) && (num != this.intClubID3)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的最终能力。</p>";
            }
            else if (((this.intType == 5) && (num != this.intClubID5)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的最终能力。</p>";
            }
            else if (this.CanUseMax() == 1)
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您确定要对此球员使用最终能力评估报告吗？使用后您就能看到此球员的最终能力！这将花费您 " + Global.drParameter["MaxSkill"].ToString().Trim() + " 枚游戏币。</p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MAX);
            }
            else
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您的游戏币不足" + Global.drParameter["MaxSkill"].ToString().Trim() + "，无法使用最终能力评估报告。</p>";
                this.btnOK.Visible = false;
            }
        }

        private void MinusAge()
        {
            DataRow playerRowByPlayerID;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            this.intCheck = SessionItem.GetRequest("Check", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
            byte num1 = (byte) playerRowByPlayerID["Power"];
            int num = (int) playerRowByPlayerID["ClubID"];
            int num2 = Convert.ToInt32(playerRowByPlayerID["Age"]);
            if (((this.intType == 3) && (num != this.intClubID3)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left'>此球员不在我们的球队中效力，无法对其使用返老还童卡。</p>";
            }
            else if (((this.intType == 5) && (num != this.intClubID5)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left'>此球员不在我们的球队中效力，无法对其使用返老还童卡。</p>";
            }
            else if (num2 > 0x1c)
            {
                this.strToolMsg = "<p align='left'>您确定要对此球员使用返老还童卡，让球员的年龄减少一岁？</p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_MINUSAGE);
            }
            else
            {
                this.strToolMsg = "<p align='left'>您球员的年龄太小还不能对他使用！</p>";
            }
        }

        private void ModifyClubReturn()
        {
            this.tbSay.Visible = true;
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"ModifyClub.aspx\";'>";
            this.strSay = "您的球队信息修改成功！";
        }

        private void NewTrainPlayer3()
        {
            if (!base.IsPostBack)
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.intTrainType = SessionItem.GetRequest("TrainType", 0);
                DataRow row = BTPPlayer3Manager.TrainPlayer3(this.longPlayerID, this.intTrainType);
                int num = (int) row["IsTrain"];
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int num2 = (int) playerRowByPlayerID["Speed"];
                int num3 = (int) playerRowByPlayerID["Jump"];
                int num4 = (int) playerRowByPlayerID["Strength"];
                int num5 = (int) playerRowByPlayerID["Stamina"];
                int num6 = (int) playerRowByPlayerID["Shot"];
                int num7 = (int) playerRowByPlayerID["Point3"];
                int num8 = (int) playerRowByPlayerID["Dribble"];
                int num9 = (int) playerRowByPlayerID["Pass"];
                int num10 = (int) playerRowByPlayerID["Rebound"];
                int num11 = (int) playerRowByPlayerID["Steal"];
                int num12 = (int) playerRowByPlayerID["Block"];
                int num13 = (int) playerRowByPlayerID["Attack"];
                int num14 = (int) playerRowByPlayerID["Defense"];
                int num15 = (int) playerRowByPlayerID["Team"];
                int num16 = (int) playerRowByPlayerID["SpeedMax"];
                int num17 = (int) playerRowByPlayerID["JumpMax"];
                int num18 = (int) playerRowByPlayerID["StrengthMax"];
                int num19 = (int) playerRowByPlayerID["StaminaMax"];
                int num20 = (int) playerRowByPlayerID["ShotMax"];
                int num21 = (int) playerRowByPlayerID["Point3Max"];
                int num22 = (int) playerRowByPlayerID["DribbleMax"];
                int num23 = (int) playerRowByPlayerID["PassMax"];
                int num24 = (int) playerRowByPlayerID["ReboundMax"];
                int num25 = (int) playerRowByPlayerID["StealMax"];
                int num26 = (int) playerRowByPlayerID["BlockMax"];
                int num27 = (int) playerRowByPlayerID["AttackMax"];
                int num28 = (int) playerRowByPlayerID["DefenseMax"];
                int num29 = (int) playerRowByPlayerID["TeamMax"];
                switch (num)
                {
                    case -1:
                        this.strSay = this.strNickName + "经理，此项能力不能训练！";
                        return;

                    case 1:
                    {
                        string str = row["PlayerName"].ToString().Trim();
                        int num30 = (int) row["SpendPoint"];
                        int num31 = (int) row["TrainAdd"];
                        float num32 = ((float) num31) / 10f;
                        int num33 = (int) row["PowerCut"];
                        this.strSay = string.Concat(new object[] { "球员", str, "训练完毕，花费", num30, "点训练点数，提高", num32, "点", PlayerItem.GetPlayerChsTrainType(this.intTrainType), "能力值，消耗", num33, "体力！" });
                        goto Label_0486;
                    }
                }
                if ((((((num2 >= num16) && (this.intTrainType == 1)) || ((num3 >= num17) && (this.intTrainType == 2))) || (((num4 >= num18) && (this.intTrainType == 3)) || ((num5 >= num19) && (this.intTrainType == 4)))) || ((((num6 >= num20) && (this.intTrainType == 5)) || ((num7 >= num21) && (this.intTrainType == 6))) || (((num8 >= num22) && (this.intTrainType == 7)) || ((num9 >= num23) && (this.intTrainType == 8))))) || (((((num10 >= num24) && (this.intTrainType == 9)) || ((num11 >= num25) && (this.intTrainType == 10))) || (((num12 >= num26) && (this.intTrainType == 11)) || ((num13 >= num27) && (this.intTrainType == 12)))) || (((num14 >= num28) && (this.intTrainType == 13)) || ((num15 >= num29) && (this.intTrainType == 14)))))
                {
                    this.strSay = this.strNickName + "经理，此球员的训练项目已达到最大值。";
                }
                else
                {
                    this.strSay = this.strNickName + "经理，此球员的训练点数不够！";
                }
            }
        Label_0486:
            this.btnOK.Visible = true;
            this.strBtnCancel = "";
            if (((this.Session["PlayerNew"] == null) || (((long) this.Session["PlayerNew"]) != this.longPlayerID)) & (this.longPlayerID != 0L))
            {
                this.Session["TrainNew"] = ((int) this.Session["TrainNew"]) + 1;
                this.Session["PlayerNew"] = this.longPlayerID;
            }
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_NEWTRAINPLAYER3);
        }

        private void NoXBACup()
        {
            this.tbSay.Visible = true;
            this.strSay = "<font color=\"red\"><strong>XBA杯赛将在一个赛季后开放，敬请期待。</strong></font>";
        }

        private void OldUser()
        {
            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.top.location=\"Main.aspx\";'>";
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                int num = (byte) accountRowByUserID["OldUser"];
                if (num == 1)
                {
                    BTPAccountManager.GiveOldUserGift(this.intUserID);
                    this.strSay = "欢迎回到XBA篮球经理，您已成功领取回归大礼，包括球队资金一千万，双倍训练卡一张以及一个月的会员期限。点击确定查收奖品！";
                }
                else
                {
                    this.strSay = "您好，您已经领取过奖品，将不能重复领取！";
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void OnlyOneMatch()
        {
            this.tbSay.Visible = true;
            string request = SessionItem.GetRequest("PageType", 1);
            if (request == "MATCHREG")
            {
                switch (((int) SessionItem.GetRequest("Status", 0)))
                {
                    case -8:
                        this.strSay = "胜者为王赛场的开放时间为：10：00-24：00 请在开放时间内进行比赛。";
                        break;

                    case -7:
                        this.strSay = "您的职业队中的可上场球员人数小于5名！";
                        break;

                    case -6:
                        this.strSay = "报名额度应为 10-10000 游戏币！";
                        break;

                    case -5:
                        this.strSay = "报名额度应为 10-10000 游戏币";
                        break;

                    case -4:
                        this.strSay = "您有一场比赛正在进行中，无法进入胜者为王赛场！";
                        break;

                    case -3:
                        this.strSay = "您还没有职业队，不能进入胜者为王赛场！";
                        break;

                    case -2:
                        this.strSay = "您没有足够的游戏币来预支比赛奖金！";
                        break;

                    case -1:
                        this.strSay = "报名额度应为 10-10000 游戏币！";
                        break;

                    case 1:
                        this.strSay = "您已成功进入胜者为王赛场，系统将为您找到适合的对手！";
                        break;

                    case 2:
                        this.strSay = "您已报名进入奖金赛场，请根据球队实力自行选择比赛对手！";
                        break;
                }
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'>";
            }
            else
            {
                string str5 = request;
                if (str5 != null)
                {
                    if (str5 == "MATCHGONO")
                    {
                        if (DateTime.Now.Hour < 10)
                        {
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=2\";'>";
                            this.strSay = "胜者为王赛场的开放时间为：10：00-24：00 请在开放时间内进行比赛。";
                        }
                        else
                        {
                            DataRow onlyOneMatchRow = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(this.intUserID);
                            if (onlyOneMatchRow == null)
                            {
                                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=3\";'>";
                                this.strSay = "对不起，您还没有在胜者为王赛场报名！";
                            }
                            else
                            {
                                byte num = (byte) onlyOneMatchRow["Status"];
                                if (num != 5)
                                {
                                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=3\";'>";
                                    this.strSay = "对不起，你还不能继续打下一场胜者为王比赛！";
                                }
                                else
                                {
                                    switch (BTPOnlyOneCenterReg.OnlyOneMatchGoOn(this.intUserID))
                                    {
                                        case -2:
                                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=3\";'>";
                                            this.strSay = "恭喜您，已经连胜9场了，不能再继续下一场比赛了，请退出赛场！";
                                            return;

                                        case -1:
                                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=3\";'>";
                                            this.strSay = "对不起，你还不能继续打下一场胜者为王比赛！";
                                            return;

                                        case 1:
                                            base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                                            return;
                                    }
                                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=3\";'>";
                                    this.strSay = "对不起，你还不能继续打下一场胜者为王比赛！";
                                }
                            }
                        }
                    }
                    else if (str5 == "MATCHOUT")
                    {
                        DataRow row2 = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(this.intUserID);
                        if (row2 == null)
                        {
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=4\";'>";
                            this.strSay = "对不起，您还没有在胜者为王赛场报名！";
                        }
                        else
                        {
                            byte num2 = (byte) row2["Status"];
                            DateTime time = (DateTime) row2["StatusTime"];
                            if (((num2 != 6) && (num2 != 5)) && ((num2 != 0) || (time.AddMinutes(3.0) >= DateTime.Now)))
                            {
                                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=4\";'>";
                                this.strSay = "对不起，你还不能退出胜者为王赛场！";
                            }
                            else
                            {
                                int num3 = (int) row2["Win"];
                                int num4 = (int) row2["Box"];
                                int num5 = (int) row2["Point"];
                                int num6 = (int) row2["Reputation"];
                                int num7 = (int) row2["Wealth"];
                                int num8 = (int) row2["TopWealth"];
                                int num9 = (int) row2["BackWealth"];
                                bool flag = (bool) row2["OnlyPay"];
                                this.btnOK.Visible = true;
                                int num10 = 0;
                                if ((num5 > 0) && ((num2 == 5) || ((num2 == 0) && (time.AddMinutes(3.0) < DateTime.Now))))
                                {
                                    object strSay;
                                    if ((((num3 == 3) || (num3 == 6)) || (num3 == 9)) || (((num2 == 0) && (time.AddMinutes(3.0) < DateTime.Now)) && !flag))
                                    {
                                        string str2 = "";
                                        if (!flag)
                                        {
                                            str2 = "，您获得<font color=\"green\" style=\"font-size:16px\"><strong>+" + num5 + "</strong></font>积分";
                                        }
                                        if (flag)
                                        {
                                            num5 = 0;
                                        }
                                        if ((num2 == 0) && (time.AddMinutes(3.0) < DateTime.Now))
                                        {
                                            num10 = num7 + num8;
                                            this.strSay = "系统无法帮您找到合适的对手" + str2;
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，奖金<font color=\"green\" style=\"font-size:16px\"><strong>+", num10, "</strong></font>游戏币" });
                                            if (num6 > 0)
                                            {
                                                strSay = this.strSay;
                                                this.strSay = string.Concat(new object[] { strSay, "，联盟威望<font color=\"green\" style=\"font-size:16px\"><strong>+", num6, "</strong></font>" });
                                            }
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，疯狂豆腐<font color=\"green\" style=\"font-size:16px\"><strong>+", num4, "</strong></font>块" });
                                            this.strSay = this.strSay + "！";
                                        }
                                        else
                                        {
                                            num10 = num7 + num8;
                                            this.strSay = string.Concat(new object[] { "恭喜您连胜了", num3, "场比赛", str2 });
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，奖金<font color=\"green\" style=\"font-size:16px\"><strong>+", num10, "</strong></font>游戏币" });
                                            if (num6 > 0)
                                            {
                                                strSay = this.strSay;
                                                this.strSay = string.Concat(new object[] { strSay, "，联盟威望<font color=\"green\" style=\"font-size:16px\"><strong>+", num6, "</strong></font>" });
                                            }
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，疯狂豆腐<font color=\"green\" style=\"font-size:16px\"><strong>+", num4, "</strong></font>块" });
                                            this.strSay = this.strSay + "！";
                                        }
                                    }
                                    else
                                    {
                                        string str3 = "";
                                        if (!flag)
                                        {
                                            str3 = "<font color=\"green\" style=\"font-size:16px\"><strong>+" + num5 + "</strong></font>积分，";
                                        }
                                        int num11 = (int) BTPParameterManager.GetParameterRow()["OnlyOnePercent"];
                                        num10 = ((num7 * num11) / 100) + num8;
                                        this.strSay = "您现在已经获得" + str3;
                                        strSay = this.strSay;
                                        this.strSay = string.Concat(new object[] { strSay, "<font color=\"green\" style=\"font-size:16px\"><strong>+", num7 + num8, "</strong></font>奖金" });
                                        if (num6 > 0)
                                        {
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，联盟威望<font color=\"green\" style=\"font-size:16px\"><strong>+", num6, "</strong></font>" });
                                        }
                                        if (num10 < 1)
                                        {
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，<font color=\"green\" style=\"font-size:16px\"><strong>+", num4, "</strong></font>块疯狂豆腐，如果现在退出赛场，您将一无所获！" });
                                        }
                                        else
                                        {
                                            strSay = this.strSay;
                                            this.strSay = string.Concat(new object[] { strSay, "，<font color=\"green\" style=\"font-size:16px\"><strong>+", num4, "</strong></font>块疯狂豆腐，如果现在退出赛场，您只能获得", num10, "游戏币！" });
                                        }
                                    }
                                }
                                else if ((num2 == 0) && ((num5 == 0) || flag))
                                {
                                    this.strSay = "系统未找到对手，您确定要退出吗？";
                                }
                                else
                                {
                                    string str4 = "";
                                    if (num9 > 0)
                                    {
                                        str4 = "，损失" + num9 + "游戏币";
                                    }
                                    this.strSay = "您输掉了胜者为王的比赛" + str4 + "，确定退出吗？";
                                    int num12 = BTPOnlyOneCenterReg.OnlyOneMatchOut(this.intUserID);
                                    this.btnOK.Visible = false;
                                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=1\";'>";
                                    switch (num12)
                                    {
                                        case -1:
                                            this.strSay = "对不起，你还不能退出胜者为王赛场！";
                                            return;

                                        case 1:
                                            base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                                            return;
                                    }
                                }
                                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_ONLYMATCHOUT);
                            }
                        }
                    }
                    else if (str5 == "MATCHERROR")
                    {
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG&Status=4\";'>";
                        int num13 = SessionItem.GetRequest("Status", 0);
                        if (num13 == 1)
                        {
                            this.strSay = "您在胜者为王中停留时间过长，系统已帮您自动退出！";
                        }
                        else
                        {
                            this.strSay = "您在胜者为王赛场等待时间过长，系统将为您重新分配对手！";
                        }
                    }
                    else if (str5 == "ONLYPAYMATCH")
                    {
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"OnlyOneCenter.aspx?Type=MATCHREG\";'>";
                        int intUserIDB = SessionItem.GetRequest("UserID", 0);
                        switch (BTPOnlyOneCenterReg.SetOnlyPayMatch(this.intUserID, intUserIDB))
                        {
                            case -3:
                                this.strSay = "您没有足够的游戏币！";
                                return;

                            case -2:
                            {
                                DataRow row3 = BTPOnlyOneCenterReg.GetOnlyOneMatchRow(intUserIDB);
                                if (row3 == null)
                                {
                                    this.strSay = "对手没有在胜者为王赛场报名！";
                                    return;
                                }
                                int num15 = (int) row3["TopWealth"];
                                this.strSay = "要挑战此对手，您的奖金额度将提高到 " + num15 + " 游戏币，确定要发起挑战吗？";
                                this.btnOK.Visible = true;
                                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_UPDATETOPWEALTH);
                                return;
                            }
                            case -1:
                                this.strSay = "对方已经开始比赛，请选择其它对手！";
                                return;

                            case 0:
                                return;

                            case 1:
                                base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                                return;
                        }
                    }
                }
            }
        }

        private void Ordain()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int intUnionID = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            string str = row2["NickName"].ToString().Trim();
            if (BTPUnionManager.GetUManagerCount(intUnionID) > 7)
            {
                this.strSay = this.strNickName + "经理，您的联盟管理员最多8位，无法再任命";
            }
            else if (num4 != intUnionID)
            {
                this.strSay = this.strNickName + "经理，" + str + "不是您联盟的成员，请返回。";
            }
            else if (num5 == 2)
            {
                this.strSay = this.strNickName + "经理，" + str + "已经是管理员，无须重复任命。";
            }
            else if ((num3 == 1) && (num5 != 1))
            {
                this.strSay = this.strNickName + "经理，您确定将" + str + "任命管理员么？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Ordain);
            }
            else
            {
                this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
            }
        }

        private void OrderSay()
        {
            this.intType = SessionItem.GetRequest("Category", 0);
            this.intWealth = SessionItem.GetRequest("Count", 0);
            this.lgMoney = SessionItem.GetRequest("Money", 3);
            int request = SessionItem.GetRequest("Wealth", 0);
            if (this.intType == 1)
            {
                if (this.intWealth < 1)
                {
                    this.strSay = "<p>您的订单不能立即成交，提交订单后需等待订单成交！</p><p>订单不能成交原因：<font color=red>游戏币价格过低，不能立即成交！</font></p>";
                    this.btnOrderOK.Visible = true;
                    this.btnOrderOK.Click += new ImageClickEventHandler(this.btnOrderOK_Click_CreateWealthOrder);
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "Orderbutton_4.GIF' width='66' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                }
                else if (request != this.intWealth)
                {
                    request -= this.intWealth;
                    this.strSay = string.Concat(new object[] { "<p>", this.intWealth, "枚游戏币已成交，您花费<font color=red>", this.lgMoney, "</font>资金购得<font color=green>", this.intWealth, "</font>枚游戏币，系统收取一定税金请注意查收！</p><p>剩余", request, "枚游戏币的购买订单已生成请到交易大厅查看！</font></p>" });
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='CancelButton2()'>";
                }
                else
                {
                    this.strSay = string.Concat(new object[] { "<p>", this.intWealth, "枚游戏币已成交，您花费<font color=red>", this.lgMoney, "</font>资金购得<font color=green>", this.intWealth, "</font>枚游戏币，系统收取一定税金请注意查收！您可以在交易记录中再次核对此笔交易！" });
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='CancelButton2()'>";
                }
            }
            if (this.intType == 2)
            {
                if (this.intWealth < 1)
                {
                    this.strSay = "<p>您的订单不能立即成交，提交订单后需等待订单成交！</p><p>订单不能成交原因：<font color=red>游戏币价格过高，不能立即成交！</font></p>";
                    this.btnOrderOK.Visible = true;
                    this.btnOrderOK.Click += new ImageClickEventHandler(this.btnOrderOK_Click_CreateWealthOrder);
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "Orderbutton_4.GIF' width='66' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                }
                else if (request != this.intWealth)
                {
                    request -= this.intWealth;
                    this.strSay = string.Concat(new object[] { "<p>", this.intWealth, "枚游戏币已成交，您卖出了<font color=red>", this.intWealth, "</font>游戏币获得<font color=green>", this.lgMoney, "</font>资金，系统收取一定税金请注意查收！</p><p>剩余", request, "枚游戏币的卖出订单已生成请到交易大厅查看！</font></p>" });
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='CancelButton2()'>";
                }
                else
                {
                    this.strSay = string.Concat(new object[] { "<p>", this.intWealth, "枚游戏币已成交，您卖出了<font color=red>", this.intWealth, "</font>游戏币获得<font color=green>", this.lgMoney, "</font>资金，系统收取一定税金请注意查收！您可以在交易记录中再次核对此笔交易！" });
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='CancelButton2()'>";
                }
            }
        }

        private void OutUnion()
        {
            DataRow delMasterRow = BTPUnionFieldManager.GegUnionFieldRowByUserID(this.intUserID);
            if (delMasterRow != null)
            {
                int num = (byte) delMasterRow["Status"];
                int num2 = 0;
                delMasterRow = BTPUnionPolity.GetDelMasterRow(this.intUnionID);
                if (delMasterRow != null)
                {
                    int num3 = (byte) delMasterRow["Status"];
                    if (num3 == 1)
                    {
                        num2 = (int) delMasterRow["CreateID"];
                    }
                }
                if (num2 == this.intUserID)
                {
                    this.strSay = this.strNickName + "经理，您发起的盟主弹劾正在进行中不能退出联盟";
                }
                else if (num < 2)
                {
                    this.strSay = this.strNickName + "经理，您有一场盟战正在进行无法退出联盟";
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您确定要退出联盟么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Out);
                }
            }
            else
            {
                int num4 = 0;
                delMasterRow = BTPUnionPolity.GetDelMasterRow(this.intUnionID);
                if (delMasterRow != null)
                {
                    int num5 = (byte) delMasterRow["Status"];
                    if (num5 == 1)
                    {
                        num4 = (int) delMasterRow["CreateID"];
                    }
                }
                if (num4 == this.intUserID)
                {
                    this.strSay = this.strNickName + "经理，您发起的盟主弹劾正在进行中不能退出联盟";
                }
                else
                {
                    this.strSay = this.strNickName + "经理，您确定要退出联盟么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Out);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void Player3Bid()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            long num = (long) accountRowByUserID["Money"];
            int num2 = (byte) accountRowByUserID["Category"];
            if ((num >= 0x493e0L) && (num2 != 5))
            {
                this.strSay = this.strSay = this.strNickName + "经理，您已经有足够的资金，不必让此球员进行选秀！";
            }
            else if (BTPPlayer3Manager.GetPlayer3CountByClubID(this.intClubID3) < 5)
            {
                this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过少，您不能再让其参加职业选秀！";
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int intClubID = (int) playerRowByPlayerID["ClubID"];
                DataRow row3 = BTPArrange3Manager.GetCheckArrange3(intClubID, this.longPlayerID);
                string str = playerRowByPlayerID["Name"].ToString();
                int num4 = (byte) playerRowByPlayerID["Age"];
                this.intAbility = (int) playerRowByPlayerID["Ability"];
                this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
                if (row3 != null)
                {
                    this.strSay = this.strNickName + "经理您好，球员" + str + "处于阵容中，请将其移出阵容再设置选秀。";
                }
                else if (this.intPlayerCategory != 1)
                {
                    this.strSay = this.strNickName + "经理，" + str + "正在拍卖中，请返回！";
                }
                else if (num4 < 0x12)
                {
                    this.strSay = this.strNickName + "经理，" + str + "年龄太小，不能参加选秀。";
                }
                else if (intClubID != this.intClubID3)
                {
                    this.strSay = this.strNickName + "经理，" + str + "不是" + this.strClubName3 + "中的球员您无权让其参加职业选秀。";
                }
                else
                {
                    this.tblPlayerBid.Visible = true;
                    this.tbBidPrice.Visible = false;
                    this.tbBLock.Visible = true;
                    this.strBidMsg = "采用估拍方式";
                    if (!base.IsPostBack)
                    {
                        DataView view = new DataView(PlayerItem.GetEndBidTime());
                        this.ddlEndBidTime.DataSource = view;
                        this.ddlEndBidTime.DataTextField = "Hours";
                        this.ddlEndBidTime.DataValueField = "Hours";
                        this.ddlEndBidTime.DataBind();
                    }
                    int num5 = (byte) playerRowByPlayerID["Number"];
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定让", num5, "号球员", str, "参加职业选秀吗？" });
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PLAYER3BID);
                }
            }
        }

        private void Player3Fire()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            if (BTPPlayer3Manager.GetPlayer3CountByClubID(this.intClubID3) < 5)
            {
                this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过少，您不能再将其下放！";
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int intClubID = (int) playerRowByPlayerID["ClubID"];
                string str = playerRowByPlayerID["Name"].ToString();
                if (BTPArrange3Manager.GetCheckArrange3(intClubID, this.longPlayerID) != null)
                {
                    this.strSay = this.strNickName + "经理您好，球员" + str + "处于阵容中，请将其移出阵容再下放。";
                }
                else if (intClubID != this.intClubID3)
                {
                    this.strSay = this.strNickName + "经理，" + str + "不是" + this.strClubName3 + "中的球员您无权将其下放。";
                }
                else
                {
                    byte num1 = (byte) playerRowByPlayerID["Age"];
                    byte num4 = (byte) playerRowByPlayerID["Pos"];
                    float single1 = ((float) ((int) playerRowByPlayerID["Ability"])) / 10f;
                    int num5 = (int) playerRowByPlayerID["PV"];
                    int num2 = (byte) playerRowByPlayerID["Category"];
                    int num3 = (byte) playerRowByPlayerID["Number"];
                    if (num2 != 1)
                    {
                        this.strSay = this.strNickName + "经理，" + str + "正在拍卖中，您不能将其下放。";
                    }
                    else
                    {
                        this.strSay = string.Concat(new object[] { this.strNickName, "经理，下放", num3, "号球员", str, "您可以得到大约为球员身价70%的补偿金。下放后，", str, "将离开XBA，不再为您的球队效命，您确定要将其下放吗？" });
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PLAYER3FIRE);
                    }
                }
            }
        }

        private void Player5Bid()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            int num = BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5);
            DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            if (playerRowByPlayerID == null)
            {
                this.strSay = this.strNickName + "经理，拍卖的球员不存在。";
            }
            else
            {
                int num2 = Convert.ToInt32(playerRowByPlayerID["Suspend"]);
                if (((int) BTPClubManager.GetClubRowByID(this.intClubID5)["Devision"]) == 0)
                {
                    this.strSay = this.strNickName + "经理，您还未进入职业联赛，无法拍卖职业球员。";
                }
                else if ((num < 9) && (num2 == 0))
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，现在", this.strClubName5, "中可上场比赛的球员只有", num, "个，您不能再将其挂牌出售！" });
                }
                else
                {
                    int intClubID = (int) playerRowByPlayerID["ClubID"];
                    string str = playerRowByPlayerID["Name"].ToString();
                    this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
                    int num4 = (int) playerRowByPlayerID["TeamDay"];
                    if (((bool) playerRowByPlayerID["IsRetire"]) && (BTPGameManager.GetTurn() > 0x17))
                    {
                        this.strSay = this.strNickName + "经理您好，球员" + str + "即将在赛季末退役，无法挂牌出售。";
                    }
                    else
                    {
                        DataRow row2 = BTPArrange5Manager.GetCheckArrange5(intClubID, this.longPlayerID);
                        long num5 = BTPPlayer5Manager.GetSellMoneyPlayer5(this.intUserID, this.longPlayerID);
                        if (num5 < 1L)
                        {
                            this.strSay = this.strNickName + "经理您好，球员" + str + "不在您队中效力，您无法将其买出。";
                        }
                        else if (row2 != null)
                        {
                            this.strSay = this.strNickName + "经理您好，球员" + str + "处于阵容中，请将其移出阵容再设置出售。";
                        }
                        else if (this.intPlayerCategory != 1)
                        {
                            this.strSay = this.strNickName + "经理，" + str + "正在拍卖中，请返回！";
                        }
                        else if (intClubID != this.intClubID5)
                        {
                            this.strSay = this.strNickName + "经理，" + str + "不是" + this.strClubName5 + "中的球员您无权将其出售。";
                        }
                        else if (num4 < 3)
                        {
                            this.strSay = this.strNickName + "经理，" + str + "在您的球队必须满3天才能出售！";
                        }
                        else
                        {
                            this.strBidMsg = "";
                            if (!base.IsPostBack)
                            {
                                DataView view = new DataView(PlayerItem.GetEndBidTime());
                                this.ddlEndBidTime5.DataSource = view;
                                this.ddlEndBidTime5.DataTextField = "Hours";
                                this.ddlEndBidTime5.DataValueField = "Hours";
                                this.ddlEndBidTime5.DataBind();
                            }
                            DataRow parameterRow = BTPParameterManager.GetParameterRow();
                            this.intCess = (int) parameterRow["Cess"];
                            byte num1 = (byte) playerRowByPlayerID["Age"];
                            byte num7 = (byte) playerRowByPlayerID["Pos"];
                            float single1 = ((float) ((int) playerRowByPlayerID["Ability"])) / 10f;
                            this.intPV = (int) playerRowByPlayerID["PV"];
                            this.intHeight = (byte) playerRowByPlayerID["Height"];
                            int num6 = (byte) playerRowByPlayerID["Number"];
                            MarketZone.GetZoneCode(BTPDevManager.GetDevCodeByUserID(this.intUserID));
                            this.strPlayer5Name = str;
                            this.intPlayer5Number = num6;
                            this.strSay = string.Concat(new object[] { this.strNickName, "经理，您确定出售", num6, "号球员", str, "吗？<br>出售价为", num5 });
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PLAYER5BID);
                        }
                    }
                }
            }
        }

        private void Player5Fire()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) < 9)
            {
                this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName5 + "中球员过少，您不能再将其下放！";
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int intClubID = (int) playerRowByPlayerID["ClubID"];
                string str = playerRowByPlayerID["Name"].ToString();
                if (BTPArrange5Manager.GetCheckArrange5(intClubID, this.longPlayerID) != null)
                {
                    this.strSay = this.strNickName + "经理您好，球员" + str + "处于阵容中，请将其移出阵容再下放。";
                }
                else if (intClubID != this.intClubID5)
                {
                    this.strSay = this.strNickName + "经理，" + str + "不是" + this.strClubName5 + "中的球员您无权将其下放。";
                }
                else
                {
                    byte num1 = (byte) playerRowByPlayerID["Age"];
                    byte num5 = (byte) playerRowByPlayerID["Pos"];
                    float single1 = ((float) ((int) playerRowByPlayerID["Ability"])) / 10f;
                    int num2 = (BTPPlayer5Manager.GetPlayer5Cost(this.longPlayerID) * 7) / 10;
                    int num3 = (byte) playerRowByPlayerID["Category"];
                    int num4 = (byte) playerRowByPlayerID["Number"];
                    if (num3 != 1)
                    {
                        this.strSay = this.strNickName + "经理，" + str + "正在拍卖中，您不能将其下放。";
                    }
                    else
                    {
                        this.strSay = string.Concat(new object[] { this.strNickName, "经理，下放", num4, "号球员", str, "您可以得到", num2, "元的补偿金。下放后，", str, "将离开XBA，不再为您的球队效命，您确定要将其下放吗？" });
                        this.btnOK.Visible = true;
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PLAYER5FIRE);
                    }
                }
            }
        }

        private void PlayerContract()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            long request = SessionItem.GetRequest("PID", 3);
            int num2 = SessionItem.GetRequest("Status", 0);
            if (accountRowByUserID != null)
            {
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(request);
                int num3 = (int) accountRowByUserID["ClubID5"];
                int num4 = (int) playerRowByPlayerID["ClubID"];
                bool flag = (bool) playerRowByPlayerID["ContractType"];
                if (num3 != num4)
                {
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?Type=5\";'>";
                    this.strSay = "您好，此球员没在您的球队效力！";
                }
                else if (num2 == 4)
                {
                    this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?Type=5\";'>";
                    int num5 = (int) BTPParameterManager.GetParameterRow()["VIPContract"];
                    long num6 = (long) accountRowByUserID["Money"];
                    if (num6 >= num5)
                    {
                        BTPPlayer5Manager.UpdateContractByPlayerID(request, 3, 4);
                        this.strSay = "本次续约成功！3轮之内该球员的工资要求为正常工资的80%！";
                    }
                    else
                    {
                        this.strSay = "对不起，您的资金不足" + num5 + "，不能签VIP短合同！";
                    }
                }
                else if (flag)
                {
                    this.strSay = "您已经签过VIP短合同，现在签本合同将会取消之前的VIP合同，您确定吗？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PLAYERCONTRACT);
                }
                else
                {
                    switch (num2)
                    {
                        case 1:
                            BTPPlayer5Manager.UpdateContractByPlayerID(request, 13, 1);
                            base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                            return;

                        case 2:
                            BTPPlayer5Manager.UpdateContractByPlayerID(request, 0x1a, 2);
                            base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                            return;

                        case 3:
                            BTPPlayer5Manager.UpdateContractByPlayerID(request, 0x27, 3);
                            base.Response.Redirect("Report.aspx?Parameter=100!Type.5");
                            return;
                    }
                    this.strSay = "系统错误，请重试！";
                }
            }
        }

        private void Prearrange()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Tag", 0);
            DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(request);
            if (devMRowByDevMatchID != null)
            {
                int num2 = (int) devMRowByDevMatchID["ClubAID"];
                int num3 = (int) devMRowByDevMatchID["ClubHID"];
                string str = devMRowByDevMatchID["ArrangeH"].ToString().Trim();
                string str2 = devMRowByDevMatchID["ArrangeA"].ToString().Trim();
                int turn = BTPGameManager.GetTurn();
                int num5 = (int) devMRowByDevMatchID["Round"];
                if ((num5 == (turn + 1)) || (num5 == (turn + 2)))
                {
                    if (num2 == this.intClubID5)
                    {
                        if (str2 == "NO")
                        {
                            this.strSay = "您还没有启用本场比赛的预设战术，是否为本场比赛启用预设战术？";
                            this.btnOK.ImageUrl = "Images/PrearrangeOpen.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeOpen);
                        }
                        else
                        {
                            this.strSay = "您已经启用本场比赛的预设战术，请选择管理或撤销预设战术。";
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                            this.btnOK.ImageUrl = "Images/PrearrangeManage.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeManage);
                            this.btnCancelPrearrange.Visible = true;
                            this.btnCancelPrearrange.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrange);
                        }
                    }
                    else if (num3 == this.intClubID5)
                    {
                        if (str == "NO")
                        {
                            this.strSay = "您还没有启用本场比赛的预设战术，是否为本场比赛启用预设战术？";
                            this.btnOK.ImageUrl = "Images/PrearrangeOpen.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeOpen);
                        }
                        else
                        {
                            this.strSay = "您已经启用本场比赛的预设战术，请选择管理或撤销预设战术。";
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                            this.btnOK.ImageUrl = "Images/PrearrangeManage.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeManage);
                            this.btnCancelPrearrange.Visible = true;
                            this.btnCancelPrearrange.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrange);
                        }
                    }
                    else
                    {
                        this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                    }
                }
                else
                {
                    this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                }
            }
        }

        private void PrearrangeXBA()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Tag", 0);
            DataRow xGroupMatchRowByID = BTPXGroupMatchManager.GetXGroupMatchRowByID(request);
            if (xGroupMatchRowByID != null)
            {
                int num2 = (int) xGroupMatchRowByID["ClubBID"];
                int num3 = (int) xGroupMatchRowByID["ClubAID"];
                string str = xGroupMatchRowByID["ArrangeH"].ToString().Trim();
                string str2 = xGroupMatchRowByID["ArrangeA"].ToString().Trim();
                int num4 = (byte) xGroupMatchRowByID["Round"];
                int num5 = (byte) BTPXGameManager.GetLastGameRowByCategory(1)["Round"];
                if ((num4 == num5) || (num4 == (num5 + 1)))
                {
                    if (num2 == this.intClubID5)
                    {
                        if (str2 == "NO")
                        {
                            this.strSay = "您还没有启用本场比赛的预设战术，是否为本场比赛启用预设战术？";
                            this.btnOK.ImageUrl = "Images/PrearrangeOpen.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeOpenXBA);
                        }
                        else
                        {
                            this.strSay = "您已经启用本场比赛的预设战术，请选择管理或撤销预设战术。";
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                            this.btnOK.ImageUrl = "Images/PrearrangeManage.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeManageXBA);
                            this.btnCancelPrearrange.Visible = true;
                            this.btnCancelPrearrange.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrangeXBA);
                        }
                    }
                    else if (num3 == this.intClubID5)
                    {
                        if (str == "NO")
                        {
                            this.strSay = "您还没有启用本场比赛的预设战术，是否为本场比赛启用预设战术？";
                            this.btnOK.ImageUrl = "Images/PrearrangeOpen.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeOpenXBA);
                        }
                        else
                        {
                            this.strSay = "您已经启用本场比赛的预设战术，请选择管理或撤销预设战术。";
                            this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.history.back();'>";
                            this.btnOK.ImageUrl = "Images/PrearrangeManage.gif";
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PrearrangeManageXBA);
                            this.btnCancelPrearrange.Visible = true;
                            this.btnCancelPrearrange.Click += new ImageClickEventHandler(this.btnOK_Click_CancelPrearrangeXBA);
                        }
                    }
                    else
                    {
                        this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                    }
                }
                else
                {
                    this.strSay = "您无法为本场比赛设置预设战术，请返回。";
                }
            }
        }

        private void PrivateSkill()
        {
            DataRow playerRowByPlayerID;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            int num = (int) playerRowByPlayerID["ClubID"];
            this.intPlayerCategory = (byte) playerRowByPlayerID["Category"];
            if (((this.intType == 3) && (num != this.intClubID3)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的潜力。</p>";
            }
            else if (((this.intType == 5) && (num != this.intClubID5)) && (this.intPlayerCategory == 1))
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">此球员不在我们的球队中效力，无法了解他的潜力。</p>";
            }
            else if (this.CanUsePrivateSkill() == 1)
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您确定要对此球员使用潜力报告吗？使用后就只有您能看到此球员的潜力！这将花费您 " + Global.drParameter["PrivateSkill"].ToString().Trim() + " 枚游戏币。</p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_PRIVATESKILL);
            }
            else
            {
                this.strToolMsg = "<p align='left' style=\"line-height:20px;\">您的游戏币不足" + Global.drParameter["PrivateSkill"].ToString().Trim() + "，无法使用潜力报告。</p>";
                this.btnOK.Visible = false;
            }
        }

        private void QuickBuy()
        {
            this.tbSay.Visible = true;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.CanUseTool(3);
            long num = this.CheckCanBid(3, 3);
            this.btnOK.Visible = false;
            long num5 = num;
            if ((num5 <= -3L) && (num5 >= -9L))
            {
                switch (((int) (num5 - -9L)))
                {
                    case 0:
                        this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！(使用外挂购买球员将会导致永久禁止一口价球员)";
                        BTPBidderManager.BidBlockRecord(this.intUserID);
                        return;

                    case 1:
                        this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                        return;

                    case 2:
                        this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                        return;

                    case 3:
                        this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                        return;

                    case 4:
                        this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                        return;

                    case 5:
                        this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                        return;

                    case 6:
                        this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                        return;
                }
            }
            this.strBidMsg = "采用估拍方式";
            DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            long num2 = (int) playerRowByPlayerID["PV"];
            long num3 = (long) playerRowByPlayerID["QuickBuy"];
            long num4 = (int) playerRowByPlayerID["LockUserID"];
            if (num3 > 0L)
            {
                this.strSay = this.strNickName + "经理，您好！这名球员已被其它经理购买！";
            }
            else if ((num4 > 0L) && (num4 != this.intUserID))
            {
                this.strSay = this.strNickName + "经理，您好！这名球员使用一口绑定，您不可以对其出价！";
            }
            else
            {
                num2 *= 0x3a98L + (this.longPlayerID % 50L);
                playerRowByPlayerID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.lgMoney = (long) playerRowByPlayerID["Money"];
                if (this.lgMoney >= num2)
                {
                    this.strSay = "您确定要花 " + num2 + " 资金来购买此球员？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_QUICKBUY);
                }
                else
                {
                    this.strSay = string.Concat(new object[] { "您没有足够的资金，购买此球员需要花费 ", num2, " 资金，您还缺少 ", num2 - this.lgMoney, " 资金！" });
                }
            }
        }

        private void RefashionPlayerSkill()
        {
            DataRow playerRowByPlayerID;
            this.tbSay.Visible = true;
            this.trSafe.Visible = true;
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.intType = SessionItem.GetRequest("PlayerType", 0);
            if (this.intType == 3)
            {
                playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            if (playerRowByPlayerID != null)
            {
                this.strPlayerName = playerRowByPlayerID["Name"].ToString().Trim();
                this.intClubID = (int) playerRowByPlayerID["ClubID"];
            }
            else
            {
                this.strPlayerName = "";
                this.intClubID = -1;
            }
            if ((this.intClubID3 != this.intClubID) && (this.intType == 3))
            {
                this.strSay = "球员" + this.strPlayerName + "不在您的球队中效力，您无法对其进行属性再分配。";
            }
            else if ((this.intClubID5 != this.intClubID) && (this.intType == 5))
            {
                this.strSay = "球员" + this.strPlayerName + "不在您的球队中效力，您无法对其进行属性再分配。";
            }
            else
            {
                DataRow parameterRow = BTPParameterManager.GetParameterRow();
                if (parameterRow != null)
                {
                    this.intRefashionCost = (int) parameterRow["RefashionCost"];
                    this.intBadRefashion = (byte) parameterRow["BadRefashion"];
                }
                else
                {
                    this.intRefashionCost = 100;
                    this.intBadRefashion = 0;
                }
                this.strSay = string.Concat(new object[] { "您确定要对球员", this.strPlayerName, "进行洗点么？每次洗点将花费您<font style='FONT-WEIGHT: bold; FONT-SIZE: 16px; COLOR: red'>", this.intRefashionCost, "</font>游戏币，并会有<font style='FONT-WEIGHT: bold; COLOR: red'>一定的</font>几率洗爆，洗爆后现有能力值减半，潜力值将会有一定降低。" });
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_RefashionPlayerSkill);
            }
        }

        private void RefuseUnion()
        {
            int request = SessionItem.GetRequest("UnionID", 0);
            int num = SessionItem.GetRequest("UserID", 0);
            int num2 = SessionItem.GetRequest("MessageID", 0);
            if (num == this.intUserID)
            {
                this.strSay = this.strNickName + "经理，您是否拒绝加入该联盟？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_RefuseUnion);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void RegCup()
        {
            this.tblRegCup.Visible = true;
            this.intCupID = SessionItem.GetRequest("CupID", 0);
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            if (cupRowByCupID != null)
            {
                int num = (byte) cupRowByCupID["Coin"];
                this.strSay = "报名此杯赛将花费您<font color='red' style='font-size:14px;'><strong>" + num + "</strong></font>个金币，是否确认报名？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_REGCUP);
            }
            else
            {
                this.strSay = "您所要报名的杯赛不存在。";
            }
        }

        private void RegPoint3Match()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            int request = SessionItem.GetRequest("Flg", 0);
            if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if (playerRowByPlayerID == null)
                {
                    this.strSay = "该球员不存在";
                }
                else if (Convert.ToInt32(playerRowByPlayerID["ClubID"]) != this.intClubID5)
                {
                    this.strSay = "该球员不在你队中";
                }
                else
                {
                    if (request == 1)
                    {
                        this.strSay = "你确定要报名试投吗?";
                    }
                    else
                    {
                        this.strSay = "你确定要报名比赛吗?";
                    }
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_RegPoint3Match);
                }
            }
        }

        private void ReturnOkSay(int intCategory, int intAddType)
        {
            if (intCategory == 1)
            {
                this.strSay = "恢复球员的体力成功！";
            }
            else if (intCategory == 2)
            {
                this.strSay = "球员的比赛热情得到了恢复！";
            }
            else if (intCategory == 3)
            {
                this.strSay = "球员的各项身体特征恢复到一年前了！";
            }
            else if (intCategory == 4)
            {
                this.strSay = "您球员的伤病已经恢复！";
            }
            else if (intCategory == 5)
            {
                if ((intAddType == 0) || (intAddType == -1))
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力！未达预期效果！";
                }
                else if (intAddType == 1)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 速度 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 2)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 弹跳 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 3)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 强壮 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 4)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 耐力 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 5)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 投篮 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 6)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 三分 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 7)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 运球 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 8)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 传球 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 9)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 篮板 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 10)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 抢断 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 11)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 封盖 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 12)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 进攻 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 13)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 防守 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 14)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，所有当前能力减 2 点 团队 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 20)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力！未达预期效果！";
                }
                else if (intAddType == 0x15)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，速度 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x16)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，弹跳 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x17)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，强壮 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x18)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，耐力 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x19)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，投篮 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x1a)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，三分 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x1b)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，运球 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x1c)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，传球 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x1d)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，篮板 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 30)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，抢断 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x1f)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，封盖 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x20)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，进攻 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x21)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，防守 的潜力的最大值增加 2 点！";
                }
                else if (intAddType == 0x22)
                {
                    this.strSay = "魔鬼训练完毕，球员消耗 10 点体力，团队 的潜力的最大值增加 2 点！";
                }
            }
        }

        private void SearchAgain()
        {
            this.tbSay.Visible = true;
            this.strSay = "再次搜索需要消费您100游戏币，您确定吗？<br/>(优先使用活跃积分)";
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SearchAgain);
        }

        private void SetAutoTrain()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Hours", 0);
            int num2 = SessionItem.GetRequest("DevH", 0);
            if ((request < 1) && (num2 < 1))
            {
                this.strSay = "请设置正确的训练时间！";
            }
            else if (num2 < 1)
            {
                num2 = 0;
                if ((request < 1) || (request > 200))
                {
                    this.strSay = "街球训练时间必须在1至200小时以内！";
                }
                else
                {
                    this.strSay = string.Concat(new object[] { "您确定要设置离线训练 街球队", request, " 小时，职业队", num2, " 小时吗？(设置成功后将会自动退出游戏)" });
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETAUTOTRAIN);
                }
            }
            else if (request < 1)
            {
                request = 0;
                if ((num2 < 1) || (num2 > 200))
                {
                    this.strSay = "街球训练时间必须在1至200小时以内！";
                }
                else
                {
                    this.strSay = string.Concat(new object[] { "您确定要设置离线训练 街球队", request, " 小时，职业队", num2, " 小时吗？(设置成功后将会自动退出游戏)" });
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETAUTOTRAIN);
                }
            }
            else if ((request < 1) || (request > 200))
            {
                this.strSay = "街球训练时间必须在1至200小时以内！";
            }
            else if ((num2 < 1) || (num2 > 200))
            {
                this.strSay = "职业训练时间必须在1至200小时以内！";
            }
            else
            {
                this.strSay = string.Concat(new object[] { "您确定要设置离线训练 街球队", request, " 小时，职业队", num2, " 小时吗？(设置成功后将会自动退出游戏)" });
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETAUTOTRAIN);
            }
        }

        private void SetFocus()
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            int intStatus = SessionItem.GetRequest("Status", 0);
            int intCategory = SessionItem.GetRequest("Category", 0);
            switch (BTPBidFocusManager.AddFocusValues(this.intUserID, request, intCategory, intStatus))
            {
                case 0:
                    this.strSay = "您已经有10个球员在关注中，不能添加更多的关注了。";
                    break;

                case 1:
                    this.strSay = "您已经关注过此球员了。";
                    break;

                default:
                    this.strSay = "您已经成功的关注此球员，请点击确定查看所有关注球员。";
                    break;
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETFOCUS);
        }

        private void SetHonor()
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            switch (((int) SessionItem.GetRequest("Category", 0)))
            {
                case 3:
                {
                    DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(request);
                    string str = playerRowByPlayerID["Name"].ToString().Trim();
                    this.blnIsHonor = (bool) playerRowByPlayerID["IsHonor"];
                    this.strSay = "您确定要将球队" + this.strClubName3 + "中的球员" + str + "设置为荣誉球员么？";
                    break;
                }
                case 5:
                {
                    DataRow row2 = BTPPlayer5Manager.GetPlayerRowByPlayerID(request);
                    string str2 = row2["Name"].ToString().Trim();
                    this.blnIsHonor = (bool) row2["IsHonor"];
                    this.strSay = "您确定要将球队" + this.strClubName5 + "中的球员" + str2 + "设置为荣誉球员么？";
                    break;
                }
            }
            this.tblOldPlayer.Visible = true;
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETHONOR);
        }

        private void SetNPC()
        {
            this.longNpcID = SessionItem.GetRequest("NpcID", 3);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                int num = Convert.ToInt32(accountRowByUserID["DayNpcMatchCount"]);
                int num2 = Convert.ToInt32(accountRowByUserID["DayNpc7WinCount"]);
                int num3 = Convert.ToInt32(accountRowByUserID["DayNpc8WinCount"]);
                DataRow nPCRow = BTPNPCManager.GetNPCRow(this.longNpcID);
                if (nPCRow == null)
                {
                    this.strSay = "经理，该球队不存在。";
                }
                else if ((this.longNpcID == 5L) && (num2 > Config.NpcHurenTimes()))
                {
                    this.strSay = "当日战胜湖人次数已超限制，不能挑战湖人队 。";
                }
                else if ((this.longNpcID == 6L) && (num3 > Config.NpcBuXinZheTimes()))
                {
                    this.strSay = "当日战胜步行者次数已超限制，不能挑战步行者队 。";
                }
                else
                {
                    string str = Convert.ToString(nPCRow["Name"]);
                    this.strSay = "你确定要挑战" + str + "吗？";
                    if (num >= Config.DayNpcFreeTimes())
                    {
                        int num5 = (Convert.ToInt32(nPCRow["Level"]) - 1) * 5;
                        if (num5 > 30)
                        {
                            num5 = 30;
                        }
                        if (num5 > 0)
                        {
                            this.strSay = string.Concat(new object[] { this.strSay, "这将花费你", num5, "游戏币" });
                        }
                    }
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETNPC);
                }
            }
        }

        private void SetPosition()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            string str = row2["NickName"].ToString().Trim();
            if ((num2 == num4) && (num3 == 1))
            {
                this.strSay = this.strNickName + "经理，请填写你要为" + str + "设定的封号：";
                this.tblPosistion.Visible = true;
                string str2 = row2["UnionPosition"].ToString().Trim();
                if (!base.IsPostBack)
                {
                    this.tbPosistion.Text = str2;
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_POSITION);
            }
            else
            {
                this.strSay = this.strNickName + "经理，你无权为" + str + "设定的封号。";
            }
        }

        private void SetTrial()
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            switch (BTPPlayer5Manager.SetTrialPlayer(this.intUserID, request))
            {
                case 0:
                    this.strSay = "该球员目前在其它队中试训";
                    break;

                case 1:
                    this.strSay = "同时试训的球员不能超过两个";
                    break;

                case 2:
                    this.strSay = "邀请试训成功，该球员已经到您队中报到";
                    break;

                case 3:
                    this.strSay = "该球员目前已经有人对其选秀，不能再进行试训";
                    break;

                default:
                    this.strSay = "邀请试训成功失败";
                    break;
            }
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETTRIAL);
        }

        private void SetUseStaff()
        {
            this.tbSay.Visible = true;
            this.btnOK.Visible = false;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intWealth = (int) accountRowByUserID["Wealth"];
                int num2 = (int) BTPParameterManager.GetParameterRow()["UseStaffWealth"];
                int request = SessionItem.GetRequest("DevMatchID", 0);
                accountRowByUserID = BTPDevMatchManager.GetDevMRowByDevMatchID(request);
                if (accountRowByUserID == null)
                {
                    this.strSay = "没有此场比赛！";
                }
                else
                {
                    int num4 = (int) accountRowByUserID["ClubHID"];
                    int num5 = (int) accountRowByUserID["ClubAID"];
                    bool flag = (bool) accountRowByUserID["UseStaffH"];
                    bool flag2 = (bool) accountRowByUserID["UseStaffA"];
                    if ((num4 == this.intClubID5) || (num5 == this.intClubID5))
                    {
                        if (num4 == this.intClubID5)
                        {
                            if (flag)
                            {
                                this.strSay = "您确定要取消助理教练吗？（不会退回游戏币）";
                                this.blnIsHonor = false;
                                this.btnOK.Visible = true;
                                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_USESTAFF);
                            }
                            else if (this.intWealth < num2)
                            {
                                this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您没有足够游戏币，雇用助理教练需要花费您 ", num2, " 游戏币！" });
                            }
                            else
                            {
                                this.blnIsHonor = true;
                                this.strSay = "雇用助理教练可以在您不在线的时候自动为您安排本轮职业联赛的战术，费用 " + num2 + " 游戏币，您要雇用吗？";
                                this.btnOK.Visible = true;
                                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_USESTAFF);
                            }
                        }
                        else if (flag2)
                        {
                            this.strSay = "您确定要取消助理教练吗？（不会退回游戏币）";
                            this.blnIsHonor = false;
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_USESTAFF);
                        }
                        else if (this.intWealth < num2)
                        {
                            this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好！您没有足够游戏币，雇用助理教练需要花费您 ", num2, " 游戏币！" });
                        }
                        else
                        {
                            this.strSay = "雇用助理教练可以在您不在线的时候自动为您安排本轮职业联赛的战术，费用 " + num2 + " 游戏币，您要雇用吗？";
                            this.blnIsHonor = true;
                            this.btnOK.Visible = true;
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_USESTAFF);
                        }
                    }
                    else
                    {
                        this.strSay = this.strNickName + "经理，您好！您没参加此场职业联赛！";
                    }
                }
            }
        }

        private void ShowSkill()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.tblShowSkill.Visible = true;
            this.intCheckType = SessionItem.GetRequest("CheckType", 0);
            if (this.CanUseShowSkill() == 1)
            {
                this.strToolMsg = "<p align='left'>您确定要对此球员使用潜力公告吗？使用后将会把此球员的潜力完全公开，所有的经理都将会看到此球员的潜力。</p>";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SHOWSKILL);
            }
            else
            {
                this.strToolMsg = "您不能使用潜力公告。请查看是否拥有潜力公告，如果没能请到游戏币商店购买";
                this.btnOK.Visible = false;
            }
        }

        private void StarVote()
        {
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.strSay = "你确定要将可贵的一票投给该名球员吗?";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_STARVOTE);
            }
        }

        private void StreetChooseBid()
        {
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                base.Response.Redirect("Report.aspx?Parameter=60!Type.STREETFREE^Pos.1^Order.4^Page.1");
            }
            else
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.CanUseTool(3);
                long num = this.CheckCanBid(3, 3);
                int num2 = 0;
                int num3 = 0;
                long num4 = num;
                if ((num4 <= -3L) && (num4 >= -9L))
                {
                    switch (((int) (num4 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                this.tblFreeBid.Visible = true;
                this.tbBidPrice.Visible = false;
                this.strBidMsg = "采用估拍方式";
                if ((num2 == 0) && (num3 == 0))
                {
                    this.strSay = this.strNickName + "经理，请填写您估算的球员价值，不过请注意，无论您出价过高还是过低，都有可能错过这名球员。";
                }
                else if (this.intPayType == 1)
                {
                    this.intPayWealth = 0;
                    this.strSay = this.strNickName + "经理，请填写您估算的球员价值，不过请注意，无论您出价过高还是过低，都有可能错过这名球员。";
                }
                else
                {
                    this.intPayWealth = num3;
                    this.strSay = this.strNickName + "经理，请填写您估算的球员价值，不过请注意，无论您出价过高还是过低，都有可能错过这名球员。";
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_STREETCHOOSEBID);
            }
        }

        private void StreetFreeBid()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num = (int) accountRowByUserID["Wealth"];
            byte num1 = (byte) accountRowByUserID["Category"];
            this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
            this.CanUseTool(1);
            long num2 = this.CheckCanBid(3, 1);
            int num3 = 0;
            int num4 = 0;
            int num5 = (int) BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID)["BidCount"];
            if (num5 > 3)
            {
                num4 = (num5 - 3) * 2;
                if (num4 > 50)
                {
                    num4 = 50;
                }
            }
            if (num < this.intPayWealth)
            {
                this.strSay = this.strNickName + "经理，咱们俱乐部已经没有足够的游戏币了，可以先从论坛财富中取出一些。";
            }
            else
            {
                long num7 = num2;
                if ((num7 <= -3L) && (num7 >= -9L))
                {
                    switch (((int) (num7 - -9L)))
                    {
                        case 0:
                            this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                            return;

                        case 1:
                            this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                            return;

                        case 2:
                            this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                            return;

                        case 3:
                            this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                            return;

                        case 4:
                            this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                            return;

                        case 5:
                            this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                            return;

                        case 6:
                            this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                            return;
                    }
                }
                this.tblFreeBid.Visible = true;
                this.tbBidPrice.Visible = false;
                this.strBidMsg = "采用暗拍方式";
                long num6 = num2;
                num6 = (num6 / 2L) + 0x1388L;
                num6 /= 0x2710L;
                if (num6 < 1L)
                {
                    num6 = 1L;
                }
                if ((num3 == 0) && (num4 == 0))
                {
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num6, "万以上。" });
                }
                else if (this.intPayType == 1)
                {
                    this.intPayWealth = 0;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num6, "万以上。非会员将为此次出价付出", num4, "游戏币，您是会员无须缴纳游戏币。" });
                }
                else
                {
                    this.intPayWealth = num4;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，您好，对于这名球员，建议您出价在", num6, "万以上。您将为此次出价付出", num4, "游戏币，会员免费。" });
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_STREETFREEBID);
            }
        }

        private void TimeHouse()
        {
            this.tbSay.Visible = true;
            long request = SessionItem.GetRequest("PlayerID", 3);
            int num = (int) BTPParameterManager.GetParameterRow()["TimeHouseWealth"];
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intWealth = (int) accountRowByUserID["Wealth"];
            if (this.intWealth < num)
            {
                this.strSay = "您没有足够的游戏币，进入神之领域需要 " + num + " 游戏币！";
            }
            else
            {
                this.strSay = "您确定要让您的球员进入神之领域吗，这需要 " + num + " 游戏币！";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_TIMEHOUSE);
            }
        }

        private void TimeHouse5()
        {
            this.tbSay.Visible = true;
            int request = SessionItem.GetRequest("Status", 0);
            long num2 = SessionItem.GetRequest("PlayerID", 3);
            this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_11.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?PlayerType=5&Type=9&PlayerID=", num2, "\";'>" });
            switch (request)
            {
                case -4:
                    this.strSay = "对不起，您的游戏币不足！";
                    return;

                case -3:
                    this.strSay = "对不起，您的球员已经到退役的年龄了！";
                    return;

                case -2:
                    this.strSay = "对不起，此球员不没有在您的球队效力！";
                    return;

                case -1:
                    this.strSay = "对不起，您输入的训练轮次必须为26轮！";
                    return;

                case 1:
                    this.strSay = "您的球员参加极速特训结束，成长了一岁,请点击确定，并在右侧查看球员的成长情况！";
                    base.Response.Write("<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=5&Kind=1&Check=1&PlayerID=" + num2 + "\";</script>");
                    return;
            }
            this.strSay = "对不起，系统错误请再试一次！";
        }

        private void TrainPlayer3()
        {
            if (!base.IsPostBack)
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.intTrainType = SessionItem.GetRequest("TrainType", 0);
                DataRow row = BTPPlayer3Manager.TrainPlayer3(this.longPlayerID, this.intTrainType);
                int num = (int) row["IsTrain"];
                DataRow playerRowByPlayerID = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int num2 = (int) playerRowByPlayerID["Speed"];
                int num3 = (int) playerRowByPlayerID["Jump"];
                int num4 = (int) playerRowByPlayerID["Strength"];
                int num5 = (int) playerRowByPlayerID["Stamina"];
                int num6 = (int) playerRowByPlayerID["Shot"];
                int num7 = (int) playerRowByPlayerID["Point3"];
                int num8 = (int) playerRowByPlayerID["Dribble"];
                int num9 = (int) playerRowByPlayerID["Pass"];
                int num10 = (int) playerRowByPlayerID["Rebound"];
                int num11 = (int) playerRowByPlayerID["Steal"];
                int num12 = (int) playerRowByPlayerID["Block"];
                int num13 = (int) playerRowByPlayerID["Attack"];
                int num14 = (int) playerRowByPlayerID["Defense"];
                int num15 = (int) playerRowByPlayerID["Team"];
                int num16 = (int) playerRowByPlayerID["SpeedMax"];
                int num17 = (int) playerRowByPlayerID["JumpMax"];
                int num18 = (int) playerRowByPlayerID["StrengthMax"];
                int num19 = (int) playerRowByPlayerID["StaminaMax"];
                int num20 = (int) playerRowByPlayerID["ShotMax"];
                int num21 = (int) playerRowByPlayerID["Point3Max"];
                int num22 = (int) playerRowByPlayerID["DribbleMax"];
                int num23 = (int) playerRowByPlayerID["PassMax"];
                int num24 = (int) playerRowByPlayerID["ReboundMax"];
                int num25 = (int) playerRowByPlayerID["StealMax"];
                int num26 = (int) playerRowByPlayerID["BlockMax"];
                int num27 = (int) playerRowByPlayerID["AttackMax"];
                int num28 = (int) playerRowByPlayerID["DefenseMax"];
                int num29 = (int) playerRowByPlayerID["TeamMax"];
                base.Response.Write("<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=3&Kind=1&Check=1&PlayerID=" + this.longPlayerID + "\";</script>");
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='cursor:pointer' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=3\";'>";
                switch (num)
                {
                    case -1:
                        this.strSay = this.strNickName + "经理，此项能力不能训练！";
                        return;

                    case 1:
                    {
                        string str = row["PlayerName"].ToString().Trim();
                        int num30 = (int) row["SpendPoint"];
                        int num31 = (int) row["TrainAdd"];
                        float num32 = ((float) num31) / 10f;
                        int num33 = (int) row["PowerCut"];
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=3\";'>";
                        this.strSay = string.Concat(new object[] { "球员", str, "训练完毕，花费", num30, "点训练点数，提高", num32, "点", PlayerItem.GetPlayerChsTrainType(this.intTrainType), "能力值，消耗", num33, "体力！<br><br><p align='center' style='color:red'>进入“理疗中心”可恢复球员状态！</p>" });
                        this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_ag.gif";
                        this.btnOK.Width = 70;
                        this.btnOK.Visible = true;
                        goto Label_0532;
                    }
                    case 2:
                        this.strSay = this.strNickName + "经理，此球员的体力不足，无法进行训练了！";
                        return;
                }
                if ((((((num2 >= num16) && (this.intTrainType == 1)) || ((num3 >= num17) && (this.intTrainType == 2))) || (((num4 >= num18) && (this.intTrainType == 3)) || ((num5 >= num19) && (this.intTrainType == 4)))) || ((((num6 >= num20) && (this.intTrainType == 5)) || ((num7 >= num21) && (this.intTrainType == 6))) || (((num8 >= num22) && (this.intTrainType == 7)) || ((num9 >= num23) && (this.intTrainType == 8))))) || (((((num10 >= num24) && (this.intTrainType == 9)) || ((num11 >= num25) && (this.intTrainType == 10))) || (((num12 >= num26) && (this.intTrainType == 11)) || ((num13 >= num27) && (this.intTrainType == 12)))) || (((num14 >= num28) && (this.intTrainType == 13)) || ((num15 >= num29) && (this.intTrainType == 14)))))
                {
                    this.strSay = this.strNickName + "经理，此球员的训练项目已达到最大值。";
                    return;
                }
                this.strSay = this.strNickName + "经理，此球员的训练点数不够！";
                return;
            }
        Label_0532:
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_TRAINPLAYER3);
        }

        private void TrainPlayer5()
        {
            if (!base.IsPostBack)
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.intTrainType = SessionItem.GetRequest("TrainType", 0);
                DataRow row = BTPPlayer5Manager.TrainPlayer5ByPoint(this.longPlayerID, this.intTrainType);
                int num = (int) row["IsTrain"];
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int num2 = (int) playerRowByPlayerID["Speed"];
                int num3 = (int) playerRowByPlayerID["Jump"];
                int num4 = (int) playerRowByPlayerID["Strength"];
                int num5 = (int) playerRowByPlayerID["Stamina"];
                int num6 = (int) playerRowByPlayerID["Shot"];
                int num7 = (int) playerRowByPlayerID["Point3"];
                int num8 = (int) playerRowByPlayerID["Dribble"];
                int num9 = (int) playerRowByPlayerID["Pass"];
                int num10 = (int) playerRowByPlayerID["Rebound"];
                int num11 = (int) playerRowByPlayerID["Steal"];
                int num12 = (int) playerRowByPlayerID["Block"];
                int num13 = (int) playerRowByPlayerID["Attack"];
                int num14 = (int) playerRowByPlayerID["Defense"];
                int num15 = (int) playerRowByPlayerID["Team"];
                int num16 = (int) playerRowByPlayerID["SpeedMax"];
                int num17 = (int) playerRowByPlayerID["JumpMax"];
                int num18 = (int) playerRowByPlayerID["StrengthMax"];
                int num19 = (int) playerRowByPlayerID["StaminaMax"];
                int num20 = (int) playerRowByPlayerID["ShotMax"];
                int num21 = (int) playerRowByPlayerID["Point3Max"];
                int num22 = (int) playerRowByPlayerID["DribbleMax"];
                int num23 = (int) playerRowByPlayerID["PassMax"];
                int num24 = (int) playerRowByPlayerID["ReboundMax"];
                int num25 = (int) playerRowByPlayerID["StealMax"];
                int num26 = (int) playerRowByPlayerID["BlockMax"];
                int num27 = (int) playerRowByPlayerID["AttackMax"];
                int num28 = (int) playerRowByPlayerID["DefenseMax"];
                int num29 = (int) playerRowByPlayerID["TeamMax"];
                base.Response.Write("<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=5&Kind=1&Check=1&PlayerID=" + this.longPlayerID + "\";</script>");
                this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_48.GIF' width='40' height='24' style='cursor:pointer' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=5\";'>";
                switch (num)
                {
                    case -1:
                        this.strSay = this.strNickName + "经理，此项能力不能训练！";
                        return;

                    case 1:
                    {
                        string str = row["PlayerName"].ToString().Trim();
                        int num30 = (int) row["SpendPoint"];
                        int num31 = (int) row["TrainAdd"];
                        float num32 = ((float) num31) / 10f;
                        int num33 = (int) row["PowerCut"];
                        this.strBtnCancel = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=5\";'>";
                        this.strSay = string.Concat(new object[] { "球员", str, "训练完毕，花费", num30, "点训练点数，提高", num32, "点", PlayerItem.GetPlayerChsTrainType(this.intTrainType), "能力值，消耗", num33, "体力！<br><br><p align='center' style='color:red'>进入“理疗中心”可恢复球员状态！</p>" });
                        this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_ag.gif";
                        this.btnOK.Width = 70;
                        this.btnOK.Visible = true;
                        goto Label_0532;
                    }
                    case 2:
                        this.strSay = this.strNickName + "经理，此球员的体力不足，无法进行训练了！";
                        return;
                }
                if ((((((num2 >= num16) && (this.intTrainType == 1)) || ((num3 >= num17) && (this.intTrainType == 2))) || (((num4 >= num18) && (this.intTrainType == 3)) || ((num5 >= num19) && (this.intTrainType == 4)))) || ((((num6 >= num20) && (this.intTrainType == 5)) || ((num7 >= num21) && (this.intTrainType == 6))) || (((num8 >= num22) && (this.intTrainType == 7)) || ((num9 >= num23) && (this.intTrainType == 8))))) || (((((num10 >= num24) && (this.intTrainType == 9)) || ((num11 >= num25) && (this.intTrainType == 10))) || (((num12 >= num26) && (this.intTrainType == 11)) || ((num13 >= num27) && (this.intTrainType == 12)))) || (((num14 >= num28) && (this.intTrainType == 13)) || ((num15 >= num29) && (this.intTrainType == 14)))))
                {
                    this.strSay = this.strNickName + "经理，此球员的训练项目已达到最大值。";
                    return;
                }
                this.strSay = this.strNickName + "经理，此球员的训练点数不够！";
                return;
            }
        Label_0532:
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_TRAINPLAYER5);
        }

        private void Unchain()
        {
            int request = SessionItem.GetRequest("UserID", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num2 = (int) accountRowByUserID["UnionID"];
            int num3 = (byte) accountRowByUserID["UnionCategory"];
            DataRow row2 = BTPAccountManager.GetAccountRowByUserID(request);
            int num4 = (int) row2["UnionID"];
            int num5 = (byte) row2["UnionCategory"];
            string str = row2["NickName"].ToString().Trim();
            if ((num3 != 1) && (num3 != 2))
            {
                this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
            }
            else if (num4 != num2)
            {
                this.strSay = this.strNickName + "经理，" + str + "不是您联盟的成员，请返回。";
            }
            else if ((num5 == 2) && (num3 != 1))
            {
                this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
            }
            else if ((num5 == 2) && (num3 == 1))
            {
                this.strSay = this.strNickName + "经理，您确定将" + str + "解任么？";
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Unchain);
            }
            else if (num5 == 1)
            {
                this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
            }
            else
            {
                this.strSay = this.strNickName + "经理，您没有权限进行此操作。";
            }
        }

        private void UnlayUnion()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int intUnionID = (int) accountRowByUserID["UnionID"];
            int num2 = (byte) accountRowByUserID["UnionCategory"];
            if ((BTPUnionManager.GetUnionUserCountByID(intUnionID) == 1) && (num2 == 1))
            {
                if (BTPUnionFieldManager.GetUnionFieldRowByUnionID(intUnionID) < 1)
                {
                    this.strSay = this.strNickName + "经理，您确定要解散联盟么？";
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_UNLAY);
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=425b!Type.UNION^Kind.VIEWUNION^Page.1");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=425!Type.UNION^Kind.VIEWUNION^Page.1");
            }
        }

        private void UnSetTrial()
        {
            long request = SessionItem.GetRequest("PlayerID", 3);
            if (BTPArrange5Manager.GetCheckArrange5(this.intClubID5, request) != null)
            {
                this.strSay = "请将该试训球员从阵容中移出";
            }
            else
            {
                if (BTPPlayer5Manager.UnSetTrialPlayer(this.intClubID5, request) == 1)
                {
                    this.strSay = "该球员不在你试训";
                }
                else
                {
                    this.strSay = "该球员已经离队";
                }
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_SETTRIAL);
            }
        }

        private void UpdatePrice()
        {
            int num = (byte) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Category"];
            if (num != 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=698");
            }
            else if (!BTPClubManager.HasBuyClub(this.intUserID))
            {
                int request = SessionItem.GetRequest("ClubID", 0);
                DataRow clubRowByID = BTPClubManager.GetClubRowByID(request);
                clubRowByID["Name"].ToString().Trim();
                long num3 = Convert.ToInt64(clubRowByID["Price"]);
                this.strSay = string.Concat(new object[] { this.strNickName, "经理您好，您正在购买第", request, "号职业球队，其价格为", num3, "您确定要购买该球队么？" });
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_UPDATEPRICE);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=695!Type.NEW^Page.1");
            }
        }

        private void UpdateStadium()
        {
            string str = "";
            int num = 0;
            switch (((byte) BTPStadiumManager.GetStadiumRowByUserID(this.intUserID)["Levels"]))
            {
                case 1:
                    str = "本次升级您需要花费10000的球队资金，3轮比赛时间。";
                    num = 0x2710;
                    break;

                case 2:
                    str = "本次升级您需要花费25000的球队资金，5轮比赛时间。";
                    num = 0xc350;
                    break;

                case 3:
                    str = "本次升级您需要花费75000的球队资金，7轮比赛时间。";
                    num = 0x186a0;
                    break;

                case 4:
                    str = "本次升级您需要花费200000的球队资金，10轮比赛时间。";
                    num = 0x30d40;
                    break;

                case 5:
                    str = "本次升级您需要花费500000的球队资金，15轮比赛时间。";
                    num = 0x7a120;
                    break;

                case 6:
                    str = "本次升级您需要花费1000000的球队资金，20轮比赛时间。";
                    num = 0xf4240;
                    break;

                case 7:
                    base.Response.Redirect("Report.aspx?Parameter=1");
                    break;
            }
            long num2 = (long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Money"];
            if (num2 < num)
            {
                this.strSay = this.strNickName + "经理您好，您的资金不足以升级球场。";
            }
            else
            {
                this.strSay = this.strNickName + "经理您好，您确定要升级您的球场么？" + str;
                this.btnOK.Visible = true;
                this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_UPDATESTADIUM);
            }
        }

        private void UpdateTicketPrice()
        {
            this.strSay = this.strNickName + "经理您好，请您选择所要设置的球票价格。";
            this.tblTicketPrice.Visible = true;
            this.btnOK.Visible = true;
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_UPDATETICKETPRICE);
        }

        private void UtmostMarket()
        {
            if (this.intCategory != 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=60!Type.TRANSFER^Pos.1^Order.4^Page.1");
            }
            else
            {
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.intMarket = SessionItem.GetRequest("Market", 0);
                this.CanUseTool(this.intMarket);
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                if ((this.intMarket == 4) || (this.intMarket == 6))
                {
                    DateTime time = (DateTime) playerRowByPlayerID["EndBidTime"];
                    int day = DateTime.Now.Day;
                    int num2 = time.Day;
                    if (day >= num2)
                    {
                        int num1 = DateTime.Now.Hour - time.Hour;
                    }
                }
                bool flag = (bool) playerRowByPlayerID["SellAss"];
                string str = playerRowByPlayerID["Name"].ToString().Trim();
                if (flag && (this.intPayType != 1))
                {
                    this.strSay = this.strNickName + "经理，您好！球员" + str + "只对会员出售，请加入会员获得更多服务！";
                }
                else
                {
                    long num3 = this.CheckCanBid(5, 4);
                    long num6 = num3;
                    if ((num6 <= -3L) && (num6 >= -10L))
                    {
                        switch (((int) (num6 - -10L)))
                        {
                            case 0:
                                this.strSay = this.strNickName + "经理，您好！此球员无法跨等级进行转会！";
                                return;

                            case 1:
                                this.strSay = this.strNickName + "经理，您好！这名球员不在拍卖中！";
                                return;

                            case 2:
                                this.strSay = this.strNickName + "经理，您好！您已经委托我对另一个球员进行出价了。";
                                return;

                            case 3:
                                this.strSay = this.strNickName + "经理，您好！这是咱们俱乐部中的球员，您将无法向其出价。";
                                return;

                            case 4:
                                this.strSay = this.strNickName + "经理，您好！这名球员的拍卖时间已过，无法再出价了。";
                                return;

                            case 5:
                                this.strSay = this.strNickName + "经理，您好！您在转会市场已经有过拍卖记录，您不能再举牌了，请等待上次拍卖结束。";
                                return;

                            case 6:
                                this.strSay = this.strNickName + "经理，您好！您不能对此球员出价，请选择其他球员。";
                                return;

                            case 7:
                                this.strSay = this.strNickName + "经理，您好！现在" + this.strClubName3 + "中球员过多，您不能再购买球员了！";
                                return;
                        }
                    }
                    this.tblFreeBid.Visible = true;
                    this.tbBidPrice.Visible = false;
                    this.strBidMsg = "采用明拍方式";
                    long num4 = (num3 * 0x66L) / 100L;
                    this.strSay = string.Concat(new object[] { this.strNickName, "经理，此次竞拍您至少需要出价", num4, "。" });
                    int num5 = (byte) Global.drParameter["Duty"];
                    if (num5 > 0)
                    {
                        object strSay = this.strSay;
                        this.strSay = string.Concat(new object[] { strSay, "，并且在赢得竟拍后缴纳", num5, "%的消费税。" });
                    }
                    this.btnOK.Visible = true;
                    this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DEVISION);
                }
            }
        }
    }
}


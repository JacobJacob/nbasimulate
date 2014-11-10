namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Main_P : Page
    {
        private int intCategory;
        private int intUserID;
        public string strCenterURL;
        private string strDevCode;
        public string strRightURL;
        public string strSayScript = "";
        private string strType;

        [AjaxMethod]
        public string CheckPopWin(int intNum)
        {
            return UserGuide.NoteGetGuide(DTOnlineManager.GetOnlineRowByUserID(SessionItem.CheckLogin(0))["GuideCode"].ToString(), intNum);
        }

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
                return;
            }
            this.strType = SessionItem.GetRequest("Type", 1);
            switch (this.strType)
            {
                case "UNIONLIST":
                    this.strCenterURL = "Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1";
                    this.strRightURL = "Intro/Union.htm";
                    goto Label_098B;

                case "STOCK":
                    this.strCenterURL = "Stock.aspx?Page=1&Type=COMPANY";
                    this.strRightURL = "Intro/Stock.htm";
                    goto Label_098B;

                case "UNIONFIELD":
                    this.strCenterURL = "UnionField.aspx?Type=FIELDLIST";
                    this.strRightURL = "Intro/UnionField.htm";
                    goto Label_098B;

                case "ONLYONEMATCH":
                    this.strCenterURL = "OnlyOneCenter.aspx?Type=MATCHREG";
                    this.strRightURL = "Intro/OnlyOneMatch.aspx?Type=MATCHREG";
                    goto Label_098B;

                case "TEST":
                    this.strCenterURL = "ShowBox.aspx";
                    this.strRightURL = "Temp_Right.aspx?Type=AUTOTRAIN";
                    goto Label_098B;

                case "DEVCUPTEST":
                    this.strCenterURL = "SecretaryPage.aspx?Type=DEVCUPTEST";
                    this.strRightURL = "Temp_Right.aspx?Type=AUTOTRAIN";
                    goto Label_098B;

                case "SETAUTOTRAIN":
                    this.strCenterURL = "SetAutoTrain.aspx";
                    this.strRightURL = "Temp_Right.aspx?Type=AUTOTRAIN";
                    goto Label_098B;

                case "GUESS":
                    this.strCenterURL = "Guess.aspx?Type=CHAMPIONREG";
                    this.strRightURL = "Intro/GuessCenter.htm";
                    goto Label_098B;

                case "XGUESS":
                    this.strCenterURL = "XGuess.aspx?Type=CLUBLIST";
                    this.strRightURL = "Intro/XGuessCenter.htm";
                    goto Label_098B;

                case "WEALTHMARKET":
                    this.strCenterURL = "WealthMarket.aspx?Type=WEALTHWEALLTH";
                    this.strRightURL = "Temp_Right.aspx?Type=WHEALTHMARKET";
                    goto Label_098B;

                case "CHOOSECLUB":
                    this.strCenterURL = "ChooseClub.aspx";
                    this.strRightURL = "Temp_Right.aspx?Type=CHOOSECLUB";
                    goto Label_098B;

                case "DEVCUP":
                    this.strCenterURL = "DevCup.aspx?Type=DEVCUPREG";
                    this.strRightURL = "Intro/DevCup.aspx";
                    goto Label_098B;

                case "FINDNICK":
                    this.strCenterURL = "MessageCenter.aspx?Type=SEARCH&Status=CITY";
                    this.strRightURL = "Intro/MessageCenter.aspx?Type=SEARCH";
                    goto Label_098B;

                case "ASSIGNCLUB":
                    this.strCenterURL = "AssignClub.aspx";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;

                case "UNIONCUP":
                    this.strCenterURL = "Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&Page=1";
                    this.strRightURL = "Temp_Right.aspx?Type=UNIONCUP";
                    goto Label_098B;

                case "XBACUP":
                    this.strCenterURL = "ChampionCup.aspx?Type=CHAMPIONREG&Tag=" + this.intUserID;
                    this.strRightURL = "Intro/XBACup.htm";
                    goto Label_098B;

                case "PLAYERCENTER":
                    this.strCenterURL = "PlayerCenter.aspx?Type=3&UserID=" + this.intUserID;
                    this.strRightURL = "Intro/PlayerCenter.aspx";
                    goto Label_098B;

                case "MESSAGCENTER":
                    this.strCenterURL = "MessageCenter.aspx?Type=MSGLIST&Page=1";
                    this.strRightURL = "Intro/MessageCenter.aspx?Type=MSGLIST";
                    goto Label_098B;

                case "TRAINPLAYERCENTER":
                    this.strCenterURL = "TrainPlayerCenter.aspx?Type=3&UserID=" + this.intUserID;
                    this.strRightURL = "Intro/TrainPlayer.aspx";
                    goto Label_098B;

                case "SCOREMATCH":
                    this.strCenterURL = "ScoreMatchCenter.aspx?Type=SCOREJOIN";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;

                case "FMATCHMSG":
                    this.strCenterURL = "FMatchCenter.aspx?Type=TRAINCENTER&Page=1";
                    this.strRightURL = "Intro/MatchMy.aspx";
                    goto Label_098B;

                case "FMATCH":
                    this.strCenterURL = "FMatchCenter.aspx?Type=FMATCHLIST&Page=1";
                    this.strRightURL = "Intro/FriMatchCenter.htm";
                    goto Label_098B;

                case "FRIMATCHCENTER":
                    this.strCenterURL = "FriMatchMessage.aspx?Type=TRAINCENTERREG&Page=1&States=1";
                    this.strRightURL = "Intro/OnlineList.aspx?Type=TRAINCENTER";
                    goto Label_098B;

                case "TRANSFERMAEKET":
                    this.strCenterURL = "TransferMarket.aspx?Type=MYFOCUSPLAYER&Pos=0&Order=4&Page=1";
                    this.strRightURL = "Intro/TransferMarket.aspx";
                    goto Label_098B;

                case "HONOUR":
                    this.strCenterURL = "Honour.aspx?UserID=" + this.intUserID + "&Type=TEAM&Page=1";
                    this.strRightURL = "Intro/Honour.aspx";
                    goto Label_098B;

                case "CLUBBUILD":
                    this.strCenterURL = "ClubBuild.aspx?Type=STADIUM";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;

                case "UNION":
                {
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    this.intCategory = (int) onlineRowByUserID["Category"];
                    int num = (int) onlineRowByUserID["UnionID"];
                    if (num > 0)
                    {
                        this.strCenterURL = "Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=" + num + "&Page=1";
                    }
                    else
                    {
                        this.strCenterURL = "Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1";
                    }
                    this.strRightURL = "Intro/Union.htm";
                    goto Label_098B;
                }
                case "MYUNION":
                    this.strRightURL = "Temp_Right.aspx?Type=UNION";
                    goto Label_098B;

                case "SARRANGE":
                    this.strCenterURL = "SArrange.aspx?Type=0";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;

                case "VARRANGE":
                    this.strCenterURL = "VArrange.aspx?Type=0";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;

                case "CUPLIST":
                    this.strCenterURL = "CupList.aspx?Type=SIGNUP&Grade=0&Page=1";
                    this.strRightURL = "Intro/Cup.aspx";
                    goto Label_098B;

                case "DEVISION":
                    this.strDevCode = BTPDevManager.GetDevCodeByUserID(this.intUserID).Trim();
                    if (this.intCategory == 5)
                    {
                        this.strCenterURL = string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=VIEW&Devision=", this.strDevCode, "&Page=1" });
                        break;
                    }
                    this.strCenterURL = string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1" });
                    break;

                case "DevLog":
                    this.strDevCode = BTPDevManager.GetDevCodeByUserID(this.intUserID).Trim();
                    this.strCenterURL = string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Devision=", this.strDevCode, "&Page=1" });
                    this.strRightURL = "Intro/Devision.aspx?Type=PIC";
                    goto Label_098B;

                case "DevMsg":
                    this.strCenterURL = "DevMessage.aspx?Type=VIEW&Page=1";
                    this.strRightURL = "Intro/Devision.aspx?Type=MSG";
                    goto Label_098B;

                case "MODIFYCLUB":
                    this.strCenterURL = "ModifyClub.aspx";
                    this.strRightURL = "Intro/Box.aspx?Type=MODIFYCLUB";
                    goto Label_098B;

                case "TOOLS":
                    this.strCenterURL = "ManagerTool.aspx?Type=TOOLS&Page=1";
                    this.strRightURL = "Intro/Tools.aspx";
                    goto Label_098B;

                case "ONLINELIST":
                    this.strCenterURL = "FriMatchMessage.aspx?Type=ONLINE&Page=1";
                    this.strRightURL = "Intro/OnlineList.aspx?Type=ONLINELIST";
                    goto Label_098B;

                case "XBATOP":
                    this.strCenterURL = "XBATop.aspx?Type=USERABILITYTOP";
                    this.strRightURL = "Intro/XBATop.aspx";
                    goto Label_098B;

                case "STARMATCH":
                    this.strCenterURL = "STARMATCH.aspx?Type=MATCH";
                    this.strRightURL = "Intro/StarMatch.htm";
                    goto Label_098B;

                case "UNIONCUP1":
                    this.strCenterURL = "UnionCup.aspx?Type=CUPREG";
                    this.strRightURL = "Intro/UnionCup.htm";
                    goto Label_098B;

                case "NPC":
                    this.strCenterURL = "NPC.aspx?Type=NPCLIST";
                    this.strRightURL = "Intro/Npc.htm";
                    goto Label_098B;

                case "ARENA":
                    this.strCenterURL = "ARENA.aspx?Type=LIST";
                    this.strRightURL = "Intro/Arena.htm";
                    goto Label_098B;

                case "POINT3":
                    this.strCenterURL = "Point3.aspx?Type=LIST";
                    this.strRightURL = "Intro/Point3.htm";
                    goto Label_098B;

                case "":
                    goto Label_098B;

                default:
                    this.strCenterURL = "Temp_Center.aspx";
                    this.strRightURL = "Temp_Right.aspx";
                    goto Label_098B;
            }
            this.strRightURL = "Intro/Devision.aspx";
        Label_098B:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        [AjaxMethod]
        public void SetGuideCode(int intNum)
        {
            int intUserID = SessionItem.CheckLogin(0);
            string strGuideCode = DTOnlineManager.GetOnlineRowByUserID(intUserID)["GuideCode"].ToString();
            UserGuide.NoteCompleteGuide(intUserID, strGuideCode, intNum);
        }
    }
}


namespace Web
{
    using LoginParameter;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Union : Page
    {
        protected ImageButton btnAdd;
        protected ImageButton btnCreate;
        protected ImageButton btnDemise;
        protected ImageButton btnDonate;
        protected ImageButton btnFindUnion;
        protected ImageButton btnInviter;
        protected ImageButton btnModify;
        protected ImageButton btnO;
        protected ImageButton btnOK;
        protected ImageButton btnBuyRep;
        protected ImageButton btnOKA;
        protected ImageButton btnPicOK;
        protected ImageButton btnSearch;
        protected ImageButton btnUBBSCan;
        protected ImageButton btnUBBSOK;
        protected ImageButton btnUOK;
        protected ImageButton btnXBAOK;
        protected ImageButton btnXResult;
        protected ImageButton btnXSchedule;
        public DateTime datMatchTime;
        protected DropDownList ddlClubLevel;
        protected DropDownList ddlHortation;
        protected DropDownList ddlHortation1;
        protected DropDownList ddlHortation2;
        protected DropDownList ddlHortation3;
        protected DropDownList ddlRegCharge;
        protected HtmlGenericControl divUnionIntro;
        protected HtmlInputHidden hidLogo;
        protected HtmlInputHidden hidLogo1;
        protected HtmlInputHidden hidMedal;
        protected HtmlImage imgMedal;
        public int intCapacity;
        private int intClubID;
        private int intClubID5;
        private int intCupID;
        private int intDevCupID;
        public int intGroupCount;
        public int intLogo;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intTotal;
        private int intUnionCategory;
        private int intUnionID;
        private int intUnionIDS;
        public int intUnionWealthAll = 0;
        private int intUserID;
        public int intWealthCost;
        public int intXRoundA;
        protected Label Label1;
        protected Label lblCupName;
        protected RadioButton rbCharge1000A;
        protected RadioButton rbCharge100A;
        protected RadioButton rbCharge1200A;
        protected RadioButton rbCharge150A;
        protected RadioButton rbCharge1600A;
        protected RadioButton rbCharge2000A;
        protected RadioButton rbCharge200A;
        protected RadioButton rbCharge200B;
        protected RadioButton rbCharge250A;
        protected RadioButton rbCharge300A;
        protected RadioButton rbCharge400A;
        protected RadioButton rbCharge400B;
        protected RadioButton rbCharge500A;
        protected RadioButton rbCharge600A;
        protected RadioButton rbCharge800A;
        protected RadioButton rbCharge800B;
        protected RadioButton rbIsNotSelfUnion;
        protected RadioButton rbIsSelfUnion;
        protected RadioButtonList rblRegClub;
        public StringBuilder sbCupRegManagePage = new StringBuilder("");
        public StringBuilder sbCupRegManager = new StringBuilder("");
        public StringBuilder sbDevCupManageList = new StringBuilder();
        public StringBuilder sbLookList = new StringBuilder("");
        public StringBuilder sbWealthList = new StringBuilder("");
        public StringBuilder sbWealthPage = new StringBuilder("");
        protected HtmlGenericControl spnFindUnion;
        public string strBBSLink = "";
        public string strCupName;
        public string strErrMsg;
        public string strFindUnionNick = "";
        public string strGroup;
        public string strGroupTeam;
        public string strKempList;
        public string strKind;
        public string strLevel;
        public string strList;
        public string strLogo;
        public string strModalTime;
        public string strMsg;
        public string strMsg1;
        public string strNickName;
        public string strPageIntro;
        public string strPageIntro1;
        public string strPageIntro2;
        public string strReward1;
        public string strReward2;
        public string strReward3;
        public string strReward4;
        public string strSayScript;
        public string strScript;
        public string strStatus;
        public string strTeamList;
        public string strType;
        public string strUBBSU = "联盟论坛";
        public string strUblank = "";
        public string strUnionMember = "";
        public string strXClubLogo;
        public string strXClubName;
        public string strXGameLogo;
        public string strXIntro;
        public string strXMsg;
        public string strXResultList;
        public string strXRound;
        public string strXRoundA;
        protected TextBox tbCupName;
        protected TextBox tbDemise;
        protected TextBox tbDevCupIntro;
        protected TextBox tbEndTime;
        protected TextBox tbFindNick;
        protected TextBox tbGroup;
        protected TextBox tbIntro;
        protected TextBox tbIntroM;
        protected TextBox tbInviter;
        protected HtmlTable tblCreateDevCup;
        protected HtmlTable tblCreateSociety;
        protected HtmlTable tblCupManage;
        protected HtmlTable tblCupRegManager;
        protected HtmlTable tblDemise;
        protected HtmlTable tblDonateWealth;
        protected HtmlTable tblInvite;
        protected HtmlTable tblKemp;
        protected HtmlTable tblModifyDevCup;
        protected HtmlTable tblPic;
        protected HtmlTable tblRegXBACup;
        protected HtmlTable tblReputation;
        protected HtmlTable tblSetIntro;
        protected HtmlTable tblSetUBBS;
        protected HtmlTable tblTryout;
        protected HtmlTable tblUnionBBS;
        protected HtmlTable tblUnionCup;
        protected HtmlTable tblUnionCupList;
        protected HtmlTable tblUnionIntro;
        protected HtmlTable tblUnionManage;
        protected HtmlTable tblUnionUser;
        protected HtmlTable tblUnionUserManage;
        protected HtmlTable tblViewSociety;
        protected HtmlTable tblViewUnion;
        protected HtmlTable tblViewUnionCup;
        protected HtmlTable tblWealthFinance;
        protected HtmlTable tblXCupIntro;
        protected HtmlTable tblXResult;
        protected HtmlTable tblBuyRep;
        protected TextBox tbMessage;
        protected TextBox tbName;
        protected TextBox tbNickNameA;
        protected TextBox tbNickNameB;
        protected TextBox tbNickNameC;
        protected TextBox tbNickNameD;
        protected TextBox tbNickNameE;
        protected TextBox tbNickNameF;
        protected TextBox tbNickNameG;
        protected TextBox tbNickNameH;
        protected TextBox tbPass;
        protected TextBox tbPasswordM;
        protected TextBox tbRegPassword;
        protected TextBox tbShort;
        protected TextBox tbSize;
        protected TextBox tbUBBS;
        protected TextBox tbUBBSM;
        protected TextBox tbUnionIntro;
        protected TextBox tbUnionName;
        protected TextBox tbUQQ;
        protected TextBox tbUQQM;
        protected TextBox tbWealth;
        protected TextBox tbRep;
        protected HtmlTableRow trXCupReg;

        private void Assign()
        {
            this.strPageIntro = "";
            this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
            this.strKind = SessionItem.GetRequest("Kind", 1).ToString().Trim();
            this.strStatus = SessionItem.GetRequest("Status", 1).ToString().Trim();
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(this.intUnionIDS);
            if (unionRowByID != null)
            {
                string str = unionRowByID["UBBS"].ToString().Trim();
                if (str.IndexOf("UnionBoard.aspx?Type=UNIONBBS") != -1)
                {
                    this.strUBBSU = "<a href=\"UnionBoard.aspx?Type=UNIONBBS&UnionID=" + this.intUnionIDS + "&Page=1\">联盟论坛</a>&nbsp;|&nbsp;";
                }
                else if (str != "")
                {
                    this.strUBBSU = "<a href=\"" + str + "\" target=\"_blank\">联盟论坛</a>&nbsp;|&nbsp;";
                }
                else
                {
                    this.strUBBSU = "联盟论坛&nbsp;|&nbsp;";
                }
            }
            this.SetIntro();
            this.SetIntro1();
            if (this.intUnionCategory == 1)
            {
                this.SetIntro2();
            }
        }

        private void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.tbFindNick.Text.Trim();
            base.Response.Redirect("Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=UNIONER&Page=1&NickName=" + str);
        }

        private void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            DateTime time;
            int num6;
            int num7;
            int num8;
            int num9;
            string str4;
            int num10;
            int num11;
            int num12;
            string str5;
            string str = this.hidMedal.Value.ToString().Trim();
            string validWords = StringItem.GetValidWords(this.tbCupName.Text.ToString().Trim());
            if (!StringItem.IsValidName(validWords, 1, 12))
            {
                this.strErrMsg = "请正确填写杯赛名称。";
                return;
            }
            int num = (int) BTPGameManager.GetGameRow()["DevLevelSum"];
            int num2 = Convert.ToInt32(this.ddlClubLevel.SelectedItem.Value.ToString());
            if ((num2 > num) || (num2 < 0))
            {
                num2 = 0;
            }
            int intRegCharge = 0;//Convert.ToInt32(this.ddlRegCharge.SelectedValue.ToString());
            if (intRegCharge < 0)
            {
                intRegCharge = 0;
            }
            if (intRegCharge > 0x7d0)
            {
                intRegCharge = 0x7d0;
            }
            string str3 = this.tbRegPassword.Text.ToString().Trim();
            if ((str3 != "") && (!StringItem.IsNumber(str3) || (StringItem.GetStrLength(str3) != 4)))
            {
                this.strErrMsg = "请正确填写密码。";
                return;
            }
            int intRegClub = Convert.ToInt32(this.rblRegClub.SelectedValue.ToString());
            int intUnionIDS = this.intUnionIDS;
            try
            {
                time = Convert.ToDateTime(this.tbEndTime.Text);
            }
            catch
            {
                this.strErrMsg = "请按照正确格式填写报名截止时间。";
                return;
            }
            if (time <= DateTime.Now.AddDays(5.0))
            {
                if (time < DateTime.Now.AddDays(1.0))
                {
                    this.strErrMsg = "报名截止时间最少1天后。";
                    return;
                }
                if (time.Hour < 10)
                {
                    this.strErrMsg = "截止时间必须在每日的10点之后。";
                    return;
                }
                num6 = Convert.ToInt32(this.ddlHortation.SelectedValue.ToString());
                num7 = Convert.ToInt32(this.ddlHortation1.SelectedValue.ToString());
                num8 = Convert.ToInt32(this.ddlHortation2.SelectedValue.ToString());
                num9 = Convert.ToInt32(this.ddlHortation3.SelectedValue.ToString());
                str4 = StringItem.GetValidWords(StringItem.GetHtmlEncode(this.tbDevCupIntro.Text.ToString().Trim()));
                if (!StringItem.IsValidContent(str4, 1, 400))
                {
                    this.strErrMsg = "请正确填写杯赛介绍，要求1至200汉字之间。";
                    return;
                }
                switch (str)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        num10 = 32;
                        num12 = 100;
                        if (str == "1")
                        {
                            num11 = 0;
                        }
                        else if (str == "2")
                        {
                            num11 = 100;
                        }
                        else if (str == "3")
                        {
                            num11 = 200;
                        }
                        else if (str == "4")
                        {
                            num11 = 400;
                        }
                        else
                        {
                            str = "1";
                            num11 = 0;
                        }
                        goto Label_0518;

                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        num10 = 64;
                        num12 = 200;
                        if (str == "5")
                        {
                            num11 = 75;
                        }
                        else if (str == "6")
                        {
                            num11 = 150;
                        }
                        else if (str == "7")
                        {
                            num11 = 300;
                        }
                        else if (str == "8")
                        {
                            num11 = 600;
                        }
                        else
                        {
                            str = "1";
                            num11 = 0;
                        }
                        goto Label_0518;

                    case "9":
                    case "10":
                    case "11":
                    case "12":
                        num10 = 128;
                        num12 = 400;
                        if (str == "9")
                        {
                            num11 = 100;
                        }
                        else if (str == "10")
                        {
                            num11 = 200;
                        }
                        else if (str == "11")
                        {
                            num11 = 400;
                        }
                        else if (str == "12")
                        {
                            num11 = 800;
                        }
                        else
                        {
                            str = "1";
                            num11 = 0;
                        }
                        goto Label_0518;

                    case "13":
                    case "14":
                    case "15":
                    case "16":
                        num10 = 256;
                        num12 = 800;
                        if (str == "13")
                        {
                            num11 = 125;
                        }
                        else if (str == "14")
                        {
                            num11 = 250;
                        }
                        else if (str == "15")
                        {
                            num11 = 500;
                        }
                        else if (str == "16")
                        {
                            num11 = 1000;
                        }
                        else
                        {
                            str = "1";
                            num11 = 100;
                        }
                        goto Label_0518;
                }
                num10 = 0x20;
                str = "1";
                num11 = 100;
                num12 = 100;
            }
            else
            {
                this.strErrMsg = "报名截止时间只能设置在5天以内。";
                return;
            }
        Label_0518:
            //num11 = num11 / 2;
            num12 = 0;
            if (intRegClub == 1)
            {
                str5 = "<UnionID>" + intUnionIDS + "</UnionID>";
            }
            else
            {
                str5 = "";
            }
            object obj2 = str5;
            str5 = string.Concat(new object[] { obj2, "<DevLvl>", num2, "</DevLvl>" });
            string strRewardXML = string.Concat(new object[] { "<Reward><Place>1</Place><Wealth>", num6, "</Wealth></Reward><Reward><Place>2</Place><Wealth>", num7, "</Wealth></Reward><Reward><Place>3</Place><Wealth>", num8, "</Wealth></Reward><Reward><Place>4</Place><Wealth>", num9, "</Wealth></Reward>" });
            string strLogo = str + ".gif";
            int intHortationAll = ((num6 + num7) + (num8 * 2)) + (num9 * 4);
            string str8 = StringItem.FormatDate(DateTime.Now, "yyyyMM");
            string strCupLadder = Config.GetDomain() + "DevCupLadder/" + str8 + "/";
            DevCupData data = new DevCupData(intUnionIDS, intRegClub, validWords, str3, str4, intRegCharge, strLogo, str5, strRewardXML, num10, time, num12, num11, intHortationAll, strCupLadder);
            this.Session["DevCup" + intUnionIDS] = data;
            base.Response.Redirect("SecretaryPage.aspx?Type=CREATEDEVCUP");
        }

        private void btnDemise_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbDemise.Text.ToString().Trim();
            if (strIn != "")
            {
                strIn = StringItem.GetValidWords(strIn);
                if (!StringItem.IsValidName(strIn, 1, 20))
                {
                    base.Response.Redirect("Report.aspx?Parameter=616!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                }
                else
                {
                    switch (BTPUnionManager.DemiseCreater(this.intUnionIDS, this.intUserID, strIn))
                    {
                        case -2:
                            base.Response.Redirect("Report.aspx?Parameter=617c!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case -1:
                            base.Response.Redirect("Report.aspx?Parameter=617b!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=617!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=618!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=619!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=620!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=621!Type.MYUNION^Kind.UNIONINFO^UnionID." + this.intUnionIDS + "^Page.1");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=615!Type.MYUNION^Kind.UNIONMANAGE^Status.DEMISE");
            }
        }

        private void btnDonate_Click(object sender, ImageClickEventArgs e)
        {
            int num2 = 0;
            string strIn = this.tbMessage.Text.ToString();
            string str2 = this.tbWealth.Text.ToString();
            if (StringItem.IsNumber(str2))
            {
                strIn = StringItem.GetValidWords(strIn);
                if (StringItem.IsValidContent(strIn, 0, 100))
                {
                    int intWealth = Convert.ToInt32(str2);
                    strIn = StringItem.GetHtmlEncode(strIn);
                    num2 = BTPUnionManager.SetUWealthByID(this.intUserID, this.intUnionIDS, 1, 2, 1, intWealth, strIn);
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=487");
                return;
            }
            switch (num2)
            {
                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=536");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=520");
                    return;

                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=521!Type.MYUNION^Kind.WEALTHFINANCE^Page.1");
                    break;
            }
        }

        private void btnBuyRep_Click(object sender, ImageClickEventArgs e)
        {
            int intResult = 0;
            string strRep = this.tbRep.Text.ToString();
            if (StringItem.IsNumber(strRep))
            {
                int intRep = Convert.ToInt32(strRep);
                if (intRep<=0)
                {
                    intRep = intRep * -1;   
                }
                intResult = BTPUnionManager.BuyUnionRep(this.intUserID, this.intUnionIDS, intRep);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=BURE03");
                return;
            }
            switch (intResult)
            {
                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=BURE01");
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=BURE02");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=BURS01!Type.MYUNION^Kind.WEALTHFINANCE^Page.1");
                    break;
            }
        }

        private void btnFindUnion_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbUnionName.Text.Trim();
            if (StringItem.IsValidWord(strIn))
            {
                base.Response.Redirect("Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1&UName=" + strIn);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3ab");
            }
        }

        private void btnInviter_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbInviter.Text.ToString().Trim();
            int intUnionID = 0;
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                intUnionID = (int) accountRowByUserID["UnionID"];
            }
            if (!StringItem.IsValidName(strIn, 1, 30))
            {
                base.Response.Redirect("Report.aspx?Parameter=420a");
            }
            else if (BTPAccountManager.GetAccountRowByNickName(strIn.Trim()) != null)
            {
                if (BTPUnionManager.GetUnionUserCountByID(this.intUnionIDS) > 0x257)
                {
                    base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=428!Type.", this.strType, "^Kind.UNIONINFO^UnionID.", this.intUnionIDS, "^Page.1" }));
                }
                else
                {
                    switch (BTPUnionManager.InviteUser(intUnionID, strIn, this.intUserID))
                    {
                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=412");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=420");
                            return;

                        case -1:
                            base.Response.Redirect("Report.aspx?Parameter=420b");
                            return;

                        case -2:
                            base.Response.Redirect("Report.aspx?Parameter=420c");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=413");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=420d");
            }
        }

        private void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            string str3;
            string line = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID)["RequirementXML"].ToString().Trim();
            TagReader reader = new TagReader();
            string str2 = reader.GetTagline(line, "<DevLvl>", "</DevLvl>").ToString().Trim();
            reader.GetTagline(line, "<UnionID>", "</UnionID>").ToString().Trim();
            if (this.rbIsSelfUnion.Checked)
            {
                num = 1;
                str3 = string.Concat(new object[] { "<UnionID>", this.intUnionIDS, "</UnionID><DevLvl>", str2, "</DevLvl>" });
            }
            else if (this.rbIsNotSelfUnion.Checked)
            {
                num = 0;
                str3 = "<DevLvl>" + str2 + "</DevLvl>";
            }
            else
            {
                num = 1;
                str3 = string.Concat(new object[] { "<UnionID>", this.intUnionIDS, "</UnionID><DevLvl>", str2, "</DevLvl>" });
            }
            string str4 = this.tbPasswordM.Text.ToString().Trim();
            if ((str4 != "") && (!StringItem.IsNumber(str4) || (StringItem.GetStrLength(str4) != 4)))
            {
                this.strErrMsg = "请正确填写密码。";
            }
            else
            {
                string validWords = StringItem.GetValidWords(StringItem.GetHtmlEncode(this.tbIntroM.Text.ToString().Trim()));
                if (!StringItem.IsValidContent(validWords, 1, 400))
                {
                    this.strErrMsg = "请正确填写杯赛介绍，要求1至200汉字之间。";
                }
                else
                {
                    int num2 = BTPDevCupManager.ModifyDevCup(this.intUserID, this.intDevCupID, num, str4, validWords, str3);
                    switch (num2)
                    {
                        case 0:
                            this.strErrMsg = "您不是盟主，不能修改该杯赛信息。";
                            return;

                        case 1:
                            this.strErrMsg = "您不是本盟盟主，不能修改该杯赛信息。";
                            return;
                    }
                    if (num2 == 2)
                    {
                        if (this.intUnionCategory == 1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=605!Type.MYUNION^Kind.UNIONMANAGE^Status.CUPMANAGE^Page.1");
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=605!Type.MYUNION^Kind.CUPMANAGE^Page.1");
                        }
                    }
                    else if (num2 == 3)
                    {
                        this.strErrMsg = "杯赛不存在，请查询后再做更改。";
                    }
                    else
                    {
                        this.strErrMsg = "杯赛修改出错，请确认无误后再修改。";
                    }
                }
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbName.Text.ToString().Trim();
            string validWords = this.tbShort.Text.ToString().Trim();
            string str3 = this.tbIntro.Text.ToString().Trim();
            string str4 = this.tbUQQ.Text.ToString().Trim();
            string str5 = this.tbUBBS.Text.ToString().Trim();
            strIn = StringItem.GetValidWords(strIn);
            validWords = StringItem.GetValidWords(validWords);
            str3 = StringItem.GetValidWords(str3);
            str4 = StringItem.GetValidWords(str4);
            str5 = StringItem.GetValidWords(str5);
            if (BTPUnionManager.HasUnionName(strIn))
            {
                this.strMsg = "<font color='Red'>该名称已存在，请重新填写。</font>";
            }
            else if (!StringItem.IsValidName(strIn, 1, 12))
            {
                this.strMsg = "<font color='Red'>联盟名称中含有非法字符，或长度不符合要求。</font>";
            }
            else if (BTPUnionManager.HasShortName(validWords))
            {
                this.strMsg = "<font color='Red'>该名称已存在，请重新填写。</font>";
            }
            else if (!StringItem.IsValidName(validWords, 1, 2))
            {
                this.strMsg = "<font color='Red'>联盟缩写中含有非法字符，或长度不符合要求。</font>";
            }
            else if (!StringItem.IsValidContent(str4, 0, 50))
            {
                this.strMsg = "<font color='Red'>联盟QQ群中含有非法字符或长度不符合要求。</font>";
            }
            else
            {
                if (((str5.IndexOf("UnionBoard.aspx?Type=UNIONBBS&UnionID") == -1) && (str5 != "")) && (str5.ToLower().IndexOf("http://") == -1))
                {
                    str5 = "http://" + str5;
                }
                if (!StringItem.IsValidContent(str5, 0, 200))
                {
                    this.strMsg = "<font color='Red'>联盟论坛中含有非法字符或长度不符合要求。</font>";
                }
                else if (!StringItem.IsValidContent(str3, 1, 300))
                {
                    this.strMsg = "<font color='Red'>联盟介绍中含有非法字符或长度不符合要求。</font>";
                }
                else if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) != 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=408!Type.UNION^Kind.VIEWUNION^Page.1");
                }
                else if (BTPUnionManager.CreateSociety(this.intUserID, 2, strIn, validWords, str3, this.hidLogo.Value, str4, str5) == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=408!Type.UNION^Kind.VIEWUNION^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=407!Type.UNION^Kind.VIEWUNION^Page.1");
                }
            }
        }

        private void btnPicOK_Click(object sender, ImageClickEventArgs e)
        {
            this.strLogo = this.hidLogo1.Value.ToString().Trim();
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            switch (((byte) accountRowByUserID["UnionCategory"]))
            {
                case 1:
                case 2:
                    BTPUnionManager.SetUnionLogo(this.intUnionID, this.strLogo);
                    this.strMsg1 = "<font color='Red'>联盟标志修改成功。</font>";
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=415");
        }

        private void btnUBBSCan_Click(object sender, ImageClickEventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            switch (((byte) accountRowByUserID["UnionCategory"]))
            {
                case 1:
                case 2:
                    BTPUnionManager.SetUBBS(this.intUnionID, 1);
                    base.Response.Redirect("Report.aspx?Parameter=414");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=415");
        }

        private void btnUBBSOK_Click(object sender, ImageClickEventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            switch (((byte) accountRowByUserID["UnionCategory"]))
            {
                case 1:
                case 2:
                    BTPUnionManager.SetUBBS(this.intUnionID, 0);
                    base.Response.Redirect("Report.aspx?Parameter=414");
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=415");
        }

        private void btnUOK_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbUnionIntro.Text.ToString().Trim();
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            int num = (byte) accountRowByUserID["UnionCategory"];
            string validWords = this.tbUQQM.Text.ToString().Trim();
            string str3 = this.tbUBBSM.Text.ToString().Trim();
            if (((str3.IndexOf("UnionBoard.aspx?Type=UNIONBBS&UnionID") == -1) && (str3 != "")) && (str3.ToLower().IndexOf("http://") == -1))
            {
                str3 = "http://" + str3;
            }
            switch (num)
            {
                case 1:
                case 2:
                    strIn = StringItem.GetValidWords(strIn);
                    validWords = StringItem.GetValidWords(validWords);
                    str3 = StringItem.GetValidWords(str3);
                    if (!StringItem.IsValidContent(validWords, 0, 50))
                    {
                        this.strMsg = "<font color='Red'>联盟QQ中含有非法字符，或长度不符合要求。</font>";
                        return;
                    }
                    if (!StringItem.IsValidContent(str3, 0, 200))
                    {
                        this.strMsg = "<font color='Red'>联盟论坛中含有非法字符，或长度不符合要求。</font>";
                        return;
                    }
                    if (!StringItem.IsValidContent(strIn, 1, 300))
                    {
                        this.strMsg = "<font color='Red'>联盟介绍中含有非法字符，或长度不符合要求。</font>";
                        return;
                    }
                   // strIn = strIn;
                    BTPUnionManager.SetUnionRemark(this.intUnionID, strIn, validWords, str3);
                    this.strMsg = "<font color='Red'>联盟介绍修改成功。</font>";
                    return;
            }
            base.Response.Redirect("Report.aspx?Parameter=415");
        }

        private void CupManage()
        {
            string str5;
            string str3 = "";
            if (this.intUnionCategory == 1)
            {
                str5 = "Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&";
            }
            else
            {
                str5 = "Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&";
            }
            this.strScript = this.GetScript(str5);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 4;
            this.sbDevCupManageList.Append("<table width=\"536\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbDevCupManageList.Append("<tr class=\"BarHead\">");
            this.sbDevCupManageList.Append("<td width=\"50\" height=\"25\">奖章</td>");
            this.sbDevCupManageList.Append("<td width=\"100\">杯赛名称</td>");
            this.sbDevCupManageList.Append("<td width=\"15\"></td>");
            this.sbDevCupManageList.Append("<td width=\"80\">报名/名额</td>");
            this.sbDevCupManageList.Append("<td width=\"100\">报名游戏币</td>");
            this.sbDevCupManageList.Append("<td width=\"70\">状态</td>");
            this.sbDevCupManageList.Append("<td width=\"130\">操作</td>");
            this.sbDevCupManageList.Append("</tr>");
            SqlDataReader reader = BTPDevCupManager.GetDevCupTableByUID(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string str4;
                    string str = reader["Name"].ToString().Trim();
                    bool flag = (bool) reader["IsSelfUnion"];
                    int num = (int) reader["RegCount"];
                    int num2 = (int) reader["Capacity"];
                    int num3 = (byte) reader["Status"];
                    int num4 = (int) reader["WealthCost"];
                    int num5 = (int) reader["DevCupID"];
                    string str2 = reader["BigLogo"].ToString().Trim();
                    int num6 = (byte) reader["Round"];
                    int num7 = (int) reader["UserID"];
                    if (flag)
                    {
                        string text1 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='10' width='9' border='0'>";
                    }
                    switch (num3)
                    {
                        case 0:
                            str3 = "报名中";
                            break;

                        case 1:
                            str3 = "比赛中<br>[<font color='green'>" + num6 + "</font>轮]";
                            break;

                        case 2:
                            str3 = "比赛结束";
                            break;

                        case 3:
                            str3 = "奖励结束";
                            break;
                    }
                    if (num3 == 0)
                    {
                        if (this.intUnionCategory == 1)
                        {
                            str4 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=", this.strKind, "&Status=MODIFYDEVCUP&DevCupID=", num5, "'>设置</a> | <a href='Union.aspx?Type=", this.strType, "&Kind=", this.strKind, "&Status=CUPREGMANAGE&DevCupID=", num5, "'>管理</a> | <a href='SecretaryPage.aspx?Type=DELDEVCUP&DevCupID=", num5, "'>删除</a>" });
                        }
                        else if (num7 == this.intUserID)
                        {
                            str4 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=MODIFYDEVCUP&DevCupID=", num5, "'>设置</a> | <a href='Union.aspx?Type=", this.strType, "&Kind=CUPREGMANAGE&DevCupID=", num5, "'>管理</a> | <a href='SecretaryPage.aspx?Type=DELDEVCUP&DevCupID=", num5, "'>删除</a>" });
                        }
                        else
                        {
                            str4 = "-- | -- | --";
                        }
                    }
                    else
                    {
                        str4 = "-- | -- | --";
                    }
                    this.sbDevCupManageList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbDevCupManageList.Append("<td height=\"55\"><img src=\"Images/DevMedal/" + str2 + "\" height=\"50\" width=\"40\" border=\"0\"></td>");
                    this.sbDevCupManageList.Append("<td align='left' style='padding-left:4px'>" + str + "</td>");
                    this.sbDevCupManageList.Append("<td></td>");
                    this.sbDevCupManageList.Append(string.Concat(new object[] { "<td>", num, "/", num2, "</td>" }));
                    this.sbDevCupManageList.Append("<td>" + num4 + "</td>");
                    this.sbDevCupManageList.Append("<td>" + str3 + "</td>");
                    this.sbDevCupManageList.Append("<td>" + str4 + "</td>");
                    this.sbDevCupManageList.Append("</tr>");
                    this.sbDevCupManageList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            else
            {
                this.sbDevCupManageList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbDevCupManageList.Append("<td height=\"55\" colspan=\"7\">暂无自定义杯赛</td>");
                this.sbDevCupManageList.Append("</tr>");
            }
            reader.Close();
            this.sbDevCupManageList.Append("<tr>");
            this.sbDevCupManageList.Append("<td height=\"25\" colspan=\"7\" align=\"right\" style=\"padding-right:5px\">" + this.GetViewPage(str5) + "</td>");
            this.sbDevCupManageList.Append("</tr>");
            this.sbDevCupManageList.Append("</table>");
        }

        private void CupRegManage()
        {
            string str;
            this.intPerPage = 10;
            int request = (int) SessionItem.GetRequest("DevCupID", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            if (this.intUnionCategory == 1)
            {
                str = "Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=CUPREGMANAGE&DevCupID=" + request + "&";
            }
            else
            {
                str = "Union.aspx?Type=MYUNION&Kind=CUPREGMANAGE&DevCupID=" + request + "&";
            }
            this.strScript = this.GetScript(str);
            this.sbCupRegManagePage.Append(this.GetViewPage(str));
            SqlDataReader reader = BTPDevCupRegManager.GetDevCupTableByDevCupID(0, this.intPage, this.intPerPage, request);
            while (reader.Read())
            {
                int intUserID = (int) reader["UserID"];
                string strNickName = reader["NickName"].ToString().Trim();
                string str3 = reader["ClubName"].ToString().Trim();
                bool blnSex = (bool) reader["Sex"];
                string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                this.sbCupRegManager.Append("<tr><td height=\"25\" style=\"padding-left:4px\">" + AccountItem.GetNickNameInfo(intUserID, str3, "Right") + "</td>");
                this.sbCupRegManager.Append("<td style=\"padding-left:4px\">" + AccountItem.GetNickNameInfoA(intUserID, strNickName, "Right", blnSex) + "</td>");
                this.sbCupRegManager.Append("<td align=\"center\">" + dev + "</td>");
                this.sbCupRegManager.Append(string.Concat(new object[] { "<td align=\"center\"><a href='SecretaryPage.aspx?Type=KICKOUTDEVCUP&DevCupID=", request, "&UserID=", intUserID, "'>踢出</a></td></tr>" }));
                this.sbCupRegManager.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>");
            }
            reader.Close();
            if ((this.sbCupRegManager.ToString().Trim() == "") || (this.sbCupRegManager == null))
            {
                this.sbCupRegManager.Append("<tr><td height=\"25\" align=\"center\" style=\"padding-left:4px\" colspan=\"4\">暂无人员报名</td></tr>");
            }
        }

        private void GetCheckList()
        {
            this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
            string strCurrentURL = "Union.aspx?Type=MYUNION&Kind=UNIONCUPCHECK&CupID=" + this.intCupID + "&";
            this.intPerPage = 5;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<tr class='BarHead'><td height='25' width='50'>队标</td><td width='200'>球队名称</td><td width='100'>经理名</td><td width='86'>等级</td><td width='100'>积分</td></tr>";
            SqlDataReader reader = BTPCupRegManager.GetRegCupListNew(this.intCupID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num1 = (int) reader["ClubID"];
                    int num = (int) reader["Levels"];
                    int num2 = (int) reader["Score"];
                    string str2 = reader["NickName"].ToString().Trim();
                    string strIn = reader["ClubName"].ToString().Trim();
                    string str4 = reader["ClubLogo"].ToString().Trim();
                    int num3 = (int) reader["UserID"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='50'><img src='Images/Club/Logo/", num, "/", str4, ".gif'></td><td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right' title='", strIn, "'>", StringItem.GetShortString(strIn, 0x17, "."), "</a></td><td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right'>", str2, "</a></td><td>", 
                        num, "</td><td>", num2, "</td></tr>"
                     });
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='5'>暂时没有球队报名本联盟杯赛。</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void GetImperialCheckList()
        {
            this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
            string strCurrentURL = "Union.aspx?Type=UNIONCUP&Kind=IMPERIALCUPCHECK&CupID=" + this.intCupID + "&";
            this.intPerPage = 5;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<tr class='BarHead'><td height='25' width='50'>队标</td><td width='200'>球队名称</td><td width='100'>经理名</td><td width='86'>等级</td><td width='100'>积分</td></tr>";
            SqlDataReader reader = BTPCupRegManager.GetRegCupListNew(this.intCupID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num1 = (int) reader["ClubID"];
                    int num = (int) reader["Levels"];
                    int num2 = (int) reader["Score"];
                    string str2 = reader["NickName"].ToString().Trim();
                    string strIn = reader["ClubName"].ToString().Trim();
                    string str4 = reader["ClubLogo"].ToString().Trim();
                    int num3 = (int) reader["UserID"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='50'><img src='Images/Club/Logo/", num, "/", str4, ".gif'></td><td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right' title='", strIn, "'>", StringItem.GetShortString(strIn, 0x17, "."), "</a></td><td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right'>", str2, "</a></td><td>", 
                        num, "</td><td>", num2, "</td></tr>"
                     });
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='5'>暂时没有球队报名本联盟杯赛。</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void GetRegTrueList()
        {
            this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            string str = cupRowByCupID["BigLogo"].ToString().Trim();
            string str2 = cupRowByCupID["Name"].ToString().Trim();
            int num = (byte) cupRowByCupID["Levels"];
            int num2 = (int) cupRowByCupID["MoneyCost"];
            int num3 = (int) cupRowByCupID["UnionID"];
            string str3 = cupRowByCupID["Introduction"].ToString().Trim();
            if ((num3 != 0) && (num3 != this.intUnionIDS))
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                if (this.strType == "REG")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在申请参加的杯赛</td></tr>";
                }
                else if (this.strType == "VIEWSMALLA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看小杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "VIEWBIGA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看大杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "VIEWBIGA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看大杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "KILOCUPA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看千人杯赛奖励情况</td></tr>";
                }
                else if (this.strKind == "VIEWUNIONCUP")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看联盟杯赛奖励情况</td></tr>";
                }
                else if (this.strKind == "VIEWIMPERIALCUP")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看联盟至尊杯奖励情况</td></tr>";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
                object strList = this.strList;
                this.strList = string.Concat(new object[] { strList, "<tr align='center' class='BarContent'><td height='50'><img src='Images/Cup/", str, "'></td><td><font color=''><strong>", str2, "</strong></font></td><td><font color=''><strong>第", num, "等级</strong></font></td><td><font color=''><strong>报名费：", num2, "</strong></font></td></tr><tr><td align='center' height='50'>介绍：</td><td colspan='3' style='padding-left:4px'>", str3, "</td></tr>" });
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int num = 0;
            if (this.strKind == "VIEWUNION")
            {
                return BTPUnionManager.GetUnionCountNew(SessionItem.GetRequest("UName", 1).ToString().Trim());
            }
            if (this.strKind == "VIEWSOCIETY")
            {
                return BTPUnionManager.GetSocietyCount();
            }
            if (this.strStatus == "UNIONER")
            {
                return BTPUnionManager.GetUnionUserCountByIDNew(this.intUnionID);
            }
            if (this.strKind == "UNIONCUPCHECK")
            {
                return BTPCupRegManager.GetRegCupCountNew(this.intCupID);
            }
            if (this.strKind == "UNIONCUP")
            {
                return BTPCupManager.GetUnionCupCountNew(this.intUserID, 3);
            }
            if (this.strKind == "REPUTATION")
            {
                int request = (int) SessionItem.GetRequest("UID", 0);
                if (request < 1)
                {
                    return BTPUnionManager.GetReputationByUnionCount(this.intUnionID);
                }
                return BTPUnionManager.GetReputationByUserIDCount(request);
            }
            if (this.strKind == "UNIONCUPINDEX")
            {
                return BTPCupManager.GetImperialCupCountNew();
            }
            if (this.strKind == "IMPERIALCUPCHECK")
            {
                return BTPCupRegManager.GetRegCupCountNew(this.intCupID);
            }
            if (this.strKind == "KEMP")
            {
                return BTPXGameManager.GetXGameCount();
            }
            if (this.strKind == "WEALTHFINANCE")
            {
                return BTPUWealthFinanceManager.GetUnionWealthCount(this.intUnionID);
            }
            if (this.strStatus == "CUPMANAGE")
            {
                return BTPDevCupManager.GetDevCupCountByUID(this.intUserID);
            }
            if (!(this.strStatus == "CUPREGMANAGE") && !(this.strKind == "CUPREGMANAGE"))
            {
                return num;
            }
            this.intDevCupID = (int) SessionItem.GetRequest("DevCupID", 0);
            return BTPDevCupRegManager.GetDevCupUserCount(this.intDevCupID);
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
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
            if (this.strKind == "WEALTHFINANCE")
            {
                str2 = "<span style='margin-right:30px' algin='left'><a href='Union.aspx?Type=MYUNION&Kind=DONATEWEALTH&Page=1'>捐赠游戏币</a></span>";


                DataRow allInfoByUnionID = BTPUnionManager.GetAllInfoByUnionID(this.intUnionID);
                int intUserID = (int)allInfoByUnionID["Creater"];
                if (intUserID == this.intUserID)
                {
                    str2 = str2 + "<span style='margin-right:30px' algin='left'><a href='Union.aspx?Type=MYUNION&Kind=BUYREP&Page=1'>购买联盟威望</a></span>";
                }
            }
            if (this.intPage == 1)
            {
                str2 = str2 + "上一页";
            }
            else
            {
                string str5 = str2;
                strArray = new string[6];
                strArray[0] = str5;
                strArray[1] = "<a href='";
                strArray[2] = strCurrentURL;
                strArray[3] = "Page=";
                int num4 = this.intPage - 1;
                strArray[4] = num4.ToString();
                strArray[5] = "'>上一页</a>";
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
                obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            string str = str2 + " " + str3 + " ";
            if (this.strKind == "VIEWUNION")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "联盟总数:", total, " 跳转", str4 });
            }
            if (this.strKind == "VIEWSOCIETY")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "球会总数:", total, " 跳转", str4 });
            }
            if (this.strStatus == "UNIONER")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "盟员总数:", total, " 跳转", str4 });
            }
            if (this.strKind == "UNIONCUPCHECK")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总人数:", total, "跳转", str4 });
            }
            if (this.strKind == "UNIONCUP")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strKind == "REPUTATION")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strKind == "UNIONCUPINDEX")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strKind == "KEMP")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strKind == "WEALTHFINANCE")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strStatus == "CUPMANAGE")
            {
                obj2 = str;
                return string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            if (this.strStatus == "CUPREGMANAGE")
            {
                obj2 = str;
                str = string.Concat(new object[] { obj2, "总数:", total, "跳转", str4 });
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.btnFindUnion.Click += new ImageClickEventHandler(this.btnFindUnion_Click);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.btnInviter.Click += new ImageClickEventHandler(this.btnInviter_Click);
            this.btnDonate.Click += new ImageClickEventHandler(this.btnDonate_Click);
            this.btnAdd.Click += new ImageClickEventHandler(this.btnAdd_Click);
            this.btnDemise.Click += new ImageClickEventHandler(this.btnDemise_Click);
            this.btnPicOK.Click += new ImageClickEventHandler(this.btnPicOK_Click);
            this.btnUBBSOK.Click += new ImageClickEventHandler(this.btnUBBSOK_Click);
            this.btnUBBSCan.Click += new ImageClickEventHandler(this.btnUBBSCan_Click);
            this.btnUOK.Click += new ImageClickEventHandler(this.btnUOK_Click);
            this.btnCreate.Click += new ImageClickEventHandler(this.btnCreate_Click);
            this.btnModify.Click += new ImageClickEventHandler(this.btnModify_Click);
            this.btnBuyRep.Click += new ImageClickEventHandler(this.btnBuyRep_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void ModifyDevCup()
        {
            this.intDevCupID = (int) SessionItem.GetRequest("DevCupID", 0);
            TagReader reader = new TagReader();
            if (!base.IsPostBack)
            {
                DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID);
                if (devCupRowByDevCupID == null)
                {
                    this.btnModify.Visible = false;
                }
                else
                {
                    string str = devCupRowByDevCupID["BigLogo"].ToString().Trim();
                    string str2 = devCupRowByDevCupID["Name"].ToString().Trim();
                    string line = devCupRowByDevCupID["RequirementXML"].ToString().Trim();
                    string str4 = devCupRowByDevCupID["Introduction"].ToString().Trim();
                    string str5 = devCupRowByDevCupID["Password"].ToString().Trim();
                    string str6 = devCupRowByDevCupID["RewardXML"].ToString().Trim();
                    int num = (int) devCupRowByDevCupID["Capacity"];
                    int num2 = (int) devCupRowByDevCupID["WealthCost"];
                    bool flag = (bool) devCupRowByDevCupID["IsSelfUnion"];
                    DateTime time = (DateTime) devCupRowByDevCupID["MatchTime"];
                    int num4 = (int) devCupRowByDevCupID["UserID"];
                    if ((this.intUnionCategory != 1) && (num4 != this.intUserID))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=422b");
                    }
                    else
                    {
                        this.strLevel = reader.GetTagline(line, "<DevLvl>", "</DevLvl>").ToString().Trim();
                        this.strLevel = "第" + this.strLevel + "级联赛以下";
                        IEnumerator enumerator = reader.GetItems(str6, "<Reward>", "</Reward>").GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            string current = (string) enumerator.Current;
                            switch (Convert.ToInt32(reader.GetTagline(current, "<Place>", "</Place>")))
                            {
                                case 1:
                                {
                                    this.strReward1 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                    continue;
                                }
                                case 2:
                                {
                                    this.strReward2 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                    continue;
                                }
                                case 3:
                                {
                                    this.strReward3 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                    continue;
                                }
                                case 4:
                                {
                                    this.strReward4 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                    continue;
                                }
                            }
                            this.strReward1 = this.strReward2 = this.strReward3 = this.strReward4 = "0";
                        }
                        if (flag)
                        {
                            this.rbIsSelfUnion.Checked = true;
                        }
                        else
                        {
                            this.rbIsNotSelfUnion.Checked = true;
                        }
                        this.imgMedal.Src = SessionItem.GetImageURL() + "DevMedal/" + str;
                        this.strCupName = str2;
                        this.datMatchTime = time;
                        this.intWealthCost = num2;
                        this.intCapacity = num;
                        this.tbIntroM.Text = str4;
                        this.tbPasswordM.Text = str5;
                    }
                }
            }
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
                this.tblViewSociety.Visible = false;
                this.tblViewUnion.Visible = false;
                this.tblCreateSociety.Visible = false;
                this.tblInvite.Visible = false;
                this.tblUnionCupList.Visible = false;
                this.tblUnionManage.Visible = false;
                this.tblUnionBBS.Visible = false;
                this.tblUnionCup.Visible = false;
                this.tblUnionIntro.Visible = false;
                this.tblSetIntro.Visible = false;
                this.tblSetUBBS.Visible = false;
                this.tblUnionUserManage.Visible = false;
                this.tblPic.Visible = false;
                this.tblViewUnionCup.Visible = false;
                this.tblReputation.Visible = false;
                this.tblRegXBACup.Visible = false;
                this.tblTryout.Visible = false;
                this.tblKemp.Visible = false;
                this.tblXCupIntro.Visible = false;
                this.tblXResult.Visible = false;
                this.tblDonateWealth.Visible = false;
                this.tblBuyRep.Visible = false;
                this.tblWealthFinance.Visible = false;
                this.tblCreateDevCup.Visible = false;
                this.tblCupManage.Visible = false;
                this.tblModifyDevCup.Visible = false;
                this.tblCupRegManager.Visible = false;
                this.tblDemise.Visible = false;
                this.tbUnionName.Visible = false;
                this.btnFindUnion.Visible = false;
                this.spnFindUnion.Visible = false;
                this.divUnionIntro.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionIDS = (int) accountRowByUserID["UnionID"];
            this.intUnionCategory = (byte) accountRowByUserID["UnionCategory"];
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.intClubID = (int) onlineRowByUserID["ClubID3"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.intPayType = Convert.ToInt16(onlineRowByUserID["PayType"]);
            this.sbWealthList.Append("");
            this.Assign();
            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_15.gif";
            this.btnInviter.ImageUrl = SessionItem.GetImageURL() + "button_18.gif";
            this.btnUOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnUBBSOK.ImageUrl = SessionItem.GetImageURL() + "button_19.gif";
            this.btnUBBSCan.ImageUrl = SessionItem.GetImageURL() + "button_20.gif";
            this.btnPicOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnXBAOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnSearch.ImageUrl = SessionItem.GetImageURL() + "button_34.gif";
            this.btnXResult.ImageUrl = SessionItem.GetImageURL() + "button_35.gif";
            this.btnXSchedule.ImageUrl = SessionItem.GetImageURL() + "button_36.gif";
            this.btnDemise.ImageUrl = SessionItem.GetImageURL() + "btn_Demise.gif";
            this.btnCreate.ImageUrl = SessionItem.GetImageURL() + "button_46.gif";
            this.btnModify.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnAdd.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnDonate.ImageUrl = SessionItem.GetImageURL() + "button_44.gif";
            this.btnBuyRep.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
        }

        private void SetImperialCupList()
        {
            int num;
            string str2;
            string str3;
            string str4;
            DateTime time;
            string str5;
            int num3;
            int num4;
            int num5;
            string str6;
            string str7;
            string strCurrentURL = "Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<tr class='BarHead'><td height='25' width='50'>奖杯</td><td width='100'>杯赛名称</td><td width='70'>轮次</td><td width='100'>报名/名额</td><td width='120'>时间</td><td width='96'>操作</td></tr>";
            SqlDataReader imperialCupListNew = BTPCupManager.GetImperialCupListNew(this.intPage, this.intPerPage);
            if (imperialCupListNew.HasRows)
            {
                goto Label_0441;
            }
            this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有联盟至尊杯赛。</td></tr>";
            return;
        Label_029E:
            switch (num5)
            {
                case 2:
                    str5 = "等待发奖";
                    break;

                case 3:
                    str5 = "已结束";
                    break;

                case 0:
                    str5 = "<a title='赛程排定时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;

                default:
                    str5 = "<a title='下轮比赛时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;
            }
            if (num5 == 0)
            {
                str4 = string.Concat(new object[] { "<a href='Union.aspx?Type=UNIONCUP&Kind=IMPERIALCUPCHECK&Page=1&CupID=", num, "'>查看</a> | <a href='Union.aspx?Type=UNIONCUP&Kind=VIEWIMPERIALCUP&CupID=", num, "'>介绍</a>" });
            }
            else
            {
                str4 = string.Concat(new object[] { "<a href='", str6, "'>赛程</a> | <a href='Union.aspx?Type=UNIONCUP&Kind=VIEWIMPERIALCUP&CupID=", num, "'>介绍</a>" });
            }
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='50'><img src='Images/Cup/", str2, "'></td><td>", str3, "</td><td>", str7, "</td><td>", num3, "/", num4, "</td><td>", str5, "</td><td>", str4, "</td></tr>" });
            this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
        Label_0441:
            if (imperialCupListNew.Read())
            {
                num = (int) imperialCupListNew["CupID"];
                int deadRound = BTPCupRegManager.GetDeadRound(num, this.intClubID);
                str2 = imperialCupListNew["BigLogo"].ToString().Trim();
                str3 = imperialCupListNew["Name"].ToString().Trim();
                byte num1 = (byte) imperialCupListNew["Levels"];
                int num2 = (byte) imperialCupListNew["Round"];
                time = (DateTime) imperialCupListNew["MatchTime"];
                DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(num);
                num3 = (int) cupRowByCupID["RegCount"];
                num4 = (int) cupRowByCupID["Capacity"];
                num5 = (byte) cupRowByCupID["Status"];
                str6 = cupRowByCupID["LadderURL"].ToString().Trim();
                if (deadRound == 100)
                {
                    deadRound = num2;
                }
                if (deadRound < num2)
                {
                    switch (num5)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num2, "</a></font>" });
                            goto Label_029E;
                    }
                    str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num2, "</a></font>" });
                }
                else
                {
                    switch (num5)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num2, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num2, "</a></font>" });
                            goto Label_029E;
                    }
                    str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num2, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num2, "</a></font>" });
                }
                goto Label_029E;
            }
            imperialCupListNew.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void SetIntro()
        {
            switch (this.strType)
            {
                case "UNION":
                    this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1'>联盟列表</li><li class='qian2a'><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>我的联盟</a></li><li class='qian2a'><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&Page=1'>至尊杯</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absbottom' src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                    this.spnFindUnion.Visible = true;
                    this.tbUnionName.Visible = true;
                    this.btnFindUnion.Visible = true;
                    this.btnFindUnion.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    return;

                case "MYUNION":
                {
                    this.strPageIntro = "<ul><li class='qian1a'><a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'>联盟列表</a></li><li class='qian2'>我的联盟</li><li class='qian2a'><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&Page=1'>至尊杯</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    int num = (int) accountRowByUserID["UnionID"];
                    this.intUnionCategory = (byte) accountRowByUserID["UnionCategory"];
                    if (num == 0)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=421");
                        return;
                    }
                    break;
                }
                case "UNIONCUP":
                    this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1a'><a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'>联盟列表</a></li><li class='qian2a'><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>我的联盟</a></li><li class='qian2'>至尊杯</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                    return;

                default:
                    this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1'>联盟列表</li><li class='qian2a'><a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>我的联盟</a></li><li class='qian2a'><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&Page=1' >至尊杯</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                    break;
            }
        }

        private void SetIntro1()
        {
            string str;
            switch (this.strKind)
            {
                case "VIEWUNION":
                    this.strPageIntro1 = "<font color='red'>查看联盟</font>";
                    if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) == 0)
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATESOCIETY'>组建联盟</a>";
                    }
                    this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<font color='red'>请给盟主留言，以获得加入联盟邀请。（加入联盟具体要求见右侧说明！）</font>";
                    this.tblViewUnion.Visible = true;
                    this.SetUnionList();
                    return;

                case "UNIONINTRO":
                    this.strPageIntro1 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=VIEWUNION&Page=1'>联盟列表</a>";
                    if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) == 0)
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATESOCIETY'>组建联盟</a>";
                    }
                    this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<font color='red'>请给盟主留言，以获得加入联盟邀请。（加入联盟具体要求见右侧说明！）</font>";
                    this.tblUnionIntro.Visible = true;
                    this.divUnionIntro.Visible = true;
                    this.SetUnionIntro();
                    return;

                case "CREATESOCIETY":
                    if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) == 0)
                    {
                        this.strPageIntro1 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=VIEWUNION&Page=1'>联盟列表</a>&nbsp;|&nbsp;<font color='red'>组建联盟</font>";
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<font color='red'>请给盟主留言，以获得加入联盟邀请。（加入联盟具体要求见右侧说明！）</font>";
                        this.tblCreateSociety.Visible = true;
                        this.SetSociety();
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=408!Type.UNION^Kind.VIEWUNION^Page.1");
                    return;

                case "UNIONINFO":
                    this.strPageIntro1 = "<font color='red'>联盟主页</font>&nbsp;|&nbsp;" + this.strUBBSU.Replace("＆", "&").ToString().Trim() + "<a href='Union.aspx?Type=" + this.strType + "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>";
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                        break;
                    }
                    this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    break;

                case "UNIONBBS":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;<font color='red'>联盟论坛</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionBBS.Visible = true;
                    return;

                case "INVITE":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<font color='red'>联盟邀请</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    if (BTPUnionManager.GetUnionUserCountByID(this.intUnionIDS) > 0x257)
                    {
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=428!Type.", this.strType, "^Kind.UNIONINFO^UnionID.", this.intUnionIDS, "^Page.1" }));
                        return;
                    }
                    this.tblInvite.Visible = true;
                    this.SetInvite();
                    return;

                case "UNIONMANAGE":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a><font color='red'>&nbsp;|&nbsp;联盟管理</font>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1");
                        return;
                    }
                    this.tblUnionManage.Visible = true;
                    return;

                case "UNIONCUP":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<font color='red'>联盟杯</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionCup.Visible = true;
                    this.SetUnionCupList();
                    return;

                case "UNIONCUPCHECK":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<font color='red'>联盟杯</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionCup.Visible = true;
                    this.GetCheckList();
                    return;

                case "VIEWUNIONCUP":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<font color='red'>联盟杯</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblViewUnionCup.Visible = true;
                    this.GetRegTrueList();
                    return;

                case "UNIONCUPLIST":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<font color='red'>联盟冠军榜</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionCupList.Visible = true;
                    return;

                case "UNIONCUPINDEX":
                    this.strPageIntro1 = "<font color='red'>查看杯赛</font>";
                    this.tblUnionCupList.Visible = true;
                    this.SetImperialCupList();
                    return;

                case "IMPERIALCUPCHECK":
                    this.strPageIntro1 = "<font color='red'>查看杯赛</font>";
                    this.tblUnionCupList.Visible = true;
                    this.GetImperialCheckList();
                    return;

                case "VIEWIMPERIALCUP":
                    this.strPageIntro1 = "<font color='red'>查看杯赛</font>";
                    this.tblViewUnionCup.Visible = true;
                    this.GetRegTrueList();
                    return;

                case "REPUTATION":
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<font color='red'>功勋榜</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblReputation.Visible = true;
                    this.SetReputation();
                    return;

                case "WEALTHFINANCE":
                {
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    this.intUnionID = (int) accountRowByUserID["UnionID"];
                    accountRowByUserID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
                    this.intUnionWealthAll = (int) accountRowByUserID["Wealth"];
                    this.strPageIntro1 = string.Concat(new object[] { "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<font color='red'>游戏币财政</font>" });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblWealthFinance.Visible = true;
                    this.SetWealthList();
                    return;
                }
                case "DONATEWEALTH":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblDonateWealth.Visible = true;
                    return;

                case "BUYREP":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblBuyRep.Visible = true;
                    return;

                case "CREATECUP":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;创建杯赛&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    if (!base.IsPostBack)
                    {
                        DataView view = new DataView(DDLItem.GetDevLevelItem());
                        this.ddlClubLevel.DataSource = view;
                        this.ddlClubLevel.DataTextField = "Name";
                        this.ddlClubLevel.DataValueField = "Category";
                        this.ddlClubLevel.DataBind();
                    }
                    this.strModalTime = StringItem.FormatDate(DateTime.Today.AddHours(63.0), "yyyy-MM-dd hh:mm");
                    if (!base.IsPostBack)
                    {
                        this.tbEndTime.Text = this.strModalTime;
                    }
                    this.tblUnionManage.Visible = true;
                    this.tblCreateDevCup.Visible = true;
                    return;

                case "CUPMANAGE":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionManage.Visible = true;
                    this.tblCupManage.Visible = true;
                    this.CupManage();
                    return;

                case "MODIFYDEVCUP":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionManage.Visible = true;
                    this.tblModifyDevCup.Visible = true;
                    this.ModifyDevCup();
                    return;

                case "CUPREGMANAGE":
                    this.strPageIntro1 = string.Concat(new object[] { 
                        "<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINFO&UnionID=", this.intUnionIDS, "&Page=1'>联盟主页</a>&nbsp;|&nbsp;", this.strUBBSU.Replace("＆", "&").ToString().Trim(), "<a href='Union.aspx?Type=", this.strType, "&Kind=INVITE&Page=1'>联盟邀请</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUP&Page=1'>联盟杯</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=UNIONCUPLIST&Page=1'>联盟冠军榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, "&Kind=REPUTATION&Page=1'>功勋榜</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=", this.strType, 
                        "&Kind=WEALTHFINANCE&Page=1'>游戏币财政</a>"
                     });
                    if (this.intUnionCategory != 1)
                    {
                        str = this.strPageIntro1;
                        this.strPageIntro1 = str + "&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATECUP&Page=1'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=CUPMANAGE&Page=1'>杯赛管理</a>";
                    }
                    else
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<a href='Union.aspx?Type=MYUNION&Kind=UNIONMANAGE&Status=INTRO'>联盟管理</a>";
                    }
                    this.tblUnionManage.Visible = true;
                    this.tblCupRegManager.Visible = true;
                    this.CupRegManage();
                    return;

                default:
                    this.strPageIntro1 = "<font color='red'>联盟列表</font>&nbsp;|&nbsp;";
                    if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) == 0)
                    {
                        this.strPageIntro1 = this.strPageIntro1 + "<a href='Union.aspx?Type=" + this.strType + "&Kind=CREATESOCIETY'>组建联盟</a>";
                    }
                    this.strPageIntro1 = this.strPageIntro1 + "&nbsp;|&nbsp;<font color='red'>请给盟主留言，以获得加入联盟邀请。（加入联盟具体要求见右侧说明！）</font>";
                    this.tblViewUnion.Visible = true;
                    this.SetUnionList();
                    return;
            }
            this.tblUnionIntro.Visible = true;
            this.SetUnionIntro();
        }

        private void SetIntro2()
        {
            switch (this.strStatus)
            {
                case "INTRO":
                    this.strPageIntro2 = "<font color='red'>联盟主页</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp|&nbsp<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    this.tblSetIntro.Visible = true;
                    this.SetIntros();
                    return;

                case "SETBBS":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<font color='red'>论坛设置</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp|&nbsp<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    this.tblSetUBBS.Visible = true;
                    return;

                case "UNIONER":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<font color='red'>盟员管理</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp|&nbsp<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    this.tblUnionUserManage.Visible = true;
                    this.SetUnionUserList();
                    return;

                case "PIC":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<font color='red'>联盟标志</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp|&nbsp<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    this.tblPic.Visible = true;
                    this.SetPic();
                    return;

                case "CREATECUP":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<font color='red'>创建杯赛</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    if (!base.IsPostBack)
                    {
                        DataView view = new DataView(DDLItem.GetDevLevelItem());
                        this.ddlClubLevel.DataSource = view;
                        this.ddlClubLevel.DataTextField = "Name";
                        this.ddlClubLevel.DataValueField = "Category";
                        this.ddlClubLevel.DataBind();
                    }
                    this.strModalTime = StringItem.FormatDate(DateTime.Today.AddHours(63.0), "yyyy-MM-dd hh:mm");
                    if (!base.IsPostBack)
                    {
                        this.tbEndTime.Text = this.strModalTime;
                    }
                    if (this.intUnionCategory == 1)
                    {
                        this.tblCreateDevCup.Visible = true;
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=1");
                    return;

                case "CUPMANAGE":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
                    if (this.intUnionCategory != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1");
                        return;
                    }
                    this.tblCupManage.Visible = true;
                    this.CupManage();
                    return;

                case "MODIFYDEVCUP":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
                    if (this.intUnionCategory != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1");
                        return;
                    }
                    this.tblModifyDevCup.Visible = true;
                    this.ModifyDevCup();
                    return;

                case "CUPREGMANAGE":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
                    if (this.intUnionCategory != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1");
                        return;
                    }
                    this.tblCupRegManager.Visible = true;
                    this.CupRegManage();
                    return;

                case "DEMISE":
                    this.strPageIntro2 = "<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=INTRO'>联盟主页</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<font color='red'>禅让盟主</font>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
                    if (this.intUnionCategory != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1");
                        return;
                    }
                    this.tblDemise.Visible = true;
                    return;
            }
            this.strPageIntro2 = "<font color='red'>联盟主页</font>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=PIC'>联盟标志</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=SETBBS'>论坛设置</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=UNIONER&Page=1'>盟员管理</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=UNIONMANAGE&Status=DEMISE&Page=1'>禅让盟主</a>&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=UNLAY'>解散联盟</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CREATECUP'>创建杯赛</a>&nbsp;|&nbsp;<a href='Union.aspx?Type=" + this.strType + "&Kind=" + this.strKind + "&Status=CUPMANAGE&Page=1'>杯赛管理</a>";
            this.tblSetIntro.Visible = true;
            this.SetIntros();
        }

        private void SetIntros()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
            string str = "";
            string str2 = "";
            string str3 = "";
            if (unionRowByID != null)
            {
                str = unionRowByID["Remark"].ToString().Trim();
                str2 = unionRowByID["UQQ"].ToString().Trim();
                str3 = unionRowByID["UBBS"].ToString().Trim();
            }
            if (!base.IsPostBack)
            {
                if ((str.IndexOf("发布时间") > -1) && (str.Length > 0x20))
                {
                    str = str.Substring(0, str.Length - 0x20);
                }
                this.tbUnionIntro.Text = str;
                this.tbUQQM.Text = str2;
                this.tbUBBSM.Text = str3;
            }
            if (BTPUnionBBSManager.GetUnionBoardRowByUnionID(this.intUnionID) != null)
            {
                this.strBBSLink = "原联盟论坛地址：UnionBoard.aspx?Type=UNIONBBS&UnionID=" + this.intUnionID + "&Page=1";
            }
            else
            {
                this.strBBSLink = "";
            }
        }

        private void SetInvite()
        {
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            if ((this.intUnionCategory != 1) && (this.intUnionCategory != 2))
            {
                this.tbInviter.Enabled = false;
            }
            this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
        }

        private void SetPic()
        {
            int intUnionID = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"];
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(intUnionID);
            this.strLogo = unionRowByID["BigLogo"].ToString().Trim();
            int num2 = (int) unionRowByID["Creater"];
            if (num2 == this.intUserID)
            {
                if (!base.IsPostBack)
                {
                    this.hidLogo1.Value = this.strLogo;
                }
                this.intLogo = Convert.ToInt32(this.strLogo);
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=415");
            }
        }

        private void SetReputation()
        {
            this.intPerPage = 10;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            int request = (int) SessionItem.GetRequest("UID", 0);
            if (request > 0)
            {
                this.SetReputationMy(request);
            }
            else
            {
                object strList;
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.intUnionID = (int) accountRowByUserID["UnionID"];
                this.GetTotal();
                this.strList = "<tr class='BarHead'><td height='25' width='40' align='center'>排名</td><td align='left' style='padding-left:5px'>经理名</td><td >称号</td><td >功勋总值</td><td width='100' align='center' >操作</td></tr>";
                SqlDataReader reader = BTPUnionManager.GetReputationByUnion(this.intUnionID, this.intPage, this.intPerPage);
                int num2 = (this.intPerPage * (this.intPage - 1)) + 1;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int intUserID = (int) reader["UserID"];
                        long num4 = (long) reader["SumReputation"];
                        string strNickName = reader["NickName"].ToString().Trim();
                        string str2 = reader["UnionPosition"].ToString().Trim();
                        if (str2 == "")
                        {
                            str2 = "普通盟员";
                        }
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' width='40' align='center' >", num2, "</td><td align='left'  style='padding-left:5px'>", AccountItem.GetNickNameInfo(intUserID, strNickName, "Right"), "</td><td align='center'>", str2, "</td><td align='center'>", num4, "</td><td align='center'width='100'><a href=\"Union.aspx?Type=MYUNION&Kind=REPUTATION&UID=", intUserID, "&P=", this.intPage, "\">查看详情</a></td></tr>" });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                        num2++;
                    }
                }
                else
                {
                    this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有功勋</td></tr>";
                }
                reader.Close();
                string strCurrentURL = "Union.aspx?Type=" + this.strType + "&Kind=REPUTATION&";
                this.strScript = this.GetScript(strCurrentURL);
                strList = this.strList;
                this.strList = string.Concat(new object[] { strList, "<tr><td height='25' align='right' colspan='7'><span style='margin-right:200px'><a href=\"Union.aspx?Type=MYUNION&Kind=REPUTATION&UID=", this.intUserID, "&P=", this.intPage, "\">查看我的功勋</a></span>", this.GetViewPage(strCurrentURL), "</td></tr>" });
            }
        }

        private void SetReputationMy(int intUserID)
        {
            object strList;
            int request = (int) SessionItem.GetRequest("P", 0);
            this.strList = "<tr class='BarHead'><td height='25' align='left' style='padding-left:5px'>经理名</td><td >功勋</td><td >事件</td><td >时间</td></tr>";
            SqlDataReader reader = BTPUnionManager.GetReputationByUserID(intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num2 = (int) reader["Reputation"];
                    string strNickName = reader["NickName"].ToString().Trim();
                    string strIn = reader["Note"].ToString().Trim();
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25 align='left' style='padding-left:5px'>", AccountItem.GetNickNameInfo(intUserID, strNickName, "Right"), "</td><td align='center'>", num2, "</td><td align='center'><a href=\"javascript:;\" title='", strIn, "'>", StringItem.GetShortString(strIn, 50, "."), "</a></td><td align='center'>", StringItem.FormatDate(datIn, "MM-dd hh:mm"), "</td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>";
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有功勋</td></tr>";
            }
            reader.Close();
            string strCurrentURL = string.Concat(new object[] { "Union.aspx?Type=", this.strType, "&Kind=REPUTATION&UID=", intUserID, "&P=", request, "&" });
            this.strScript = this.GetScript(strCurrentURL);
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr><td height='25' align='right' colspan='7'><span style='margin-right:200px'><a href='Union.aspx?Type=MYUNION&Kind=REPUTATION&Page=", request, "'>返回功勋榜</a></span>", this.GetViewPage(strCurrentURL), "</td></tr>" });
        }

        private void SetSociety()
        {
            int num1 = (int) DTOnlineManager.GetOnlineRowByUserID(this.intUserID)["Levels"];
            if (((int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"]) != 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=408!Type.UNION^Kind.VIEWUNION^Page.1");
            }
        }

        private void SetSocietyList()
        {
            string strCurrentURL = "Union.aspx?Type=" + this.strType + "&Kind=VIEWSOCIETY&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 6;
            int intCount = this.intPerPage * this.intPage;
            this.intTotal = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            BTPUnionManager.GetAllUnionUserCount();
            DataTable table = BTPUnionManager.GetSocietyList(this.intPage, this.intPerPage, intCount, this.intTotal);
            if (table == null)
            {
                this.strList = "<tr><td height='25' colspan='6'>暂时没有联盟。</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str = row["Name"].ToString().Trim();
                    int intUserID = (int) row["Creater"];
                    string str3 = row["ShortName"].ToString().Trim();
                    int num1 = (int) row["Reputation"];
                    int intUnionID = (int) row["UnionID"];
                    int num3 = (int) row["StreetCupNum"];
                    int num4 = (int) row["DevCupNum"];
                    DateTime datIn = (DateTime) row["CreateTime"];
                    int unionUserCount = BTPAccountManager.GetUnionUserCount(intUnionID);
                    string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                    string str5 = row["BigLogo"].ToString().Trim();
                    DataRow unionBoardRowByUnionID = BTPUnionBBSManager.GetUnionBoardRowByUnionID(intUnionID);
                    if (unionBoardRowByUnionID != null)
                    {
                        int num7 = (int) unionBoardRowByUnionID["TopicCount"];
                    }
                    if (num3 != 0)
                    {
                        string text1 = "<img src='Images/Cup/StreetCup/StreetCup_" + num3 + ".gif' height='50' width='67' border='0'>";
                    }
                    if (num4 != 0)
                    {
                        string text2 = "<img src='Images/Cup/DevisionCup/DevisionCup_" + num4 + ".gif' height='50' width='67' border='0'>";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover='this.style.backgroundColor='#FBE2D4'' onmouseout='this.style.backgroundColor='''><td height='55'><img src='Images/UnionLogo/", str5, ".gif' height='46' width='46' border='0'></td><td><font style='line-height:130%'><strong>球会：</strong>", str, "[", str3, "]<br><strong>会长：</strong>", nickNameByUserID, "</font></td><td><font style='line-height:130%'><strong>创建：</strong>", StringItem.FormatDate(datIn, "yy-MM-dd"), "<br><strong>主题：</strong>69</font></td><td valign='middle'><font style='line-height:130%'><br><strong>会员：</strong>", unionUserCount, "</font></td><td align='center'><font style='line-height:130%'><a href='UnionBoard.aspx?Type=MYUNION&UnionID=", intUnionID, "&Page=1>论坛</a> | 介绍<br>会员列表</font></td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
                this.strList = this.strList + "<tr><td height='5' colspan='6'></td></tr><tr height='25'><td align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
        }

        private void SetUnionCupList()
        {
            int num;
            string str2;
            string str3;
            string str4;
            DateTime time;
            string str5;
            int num3;
            int num4;
            int num5;
            string str6;
            string str7;
            string strCurrentURL = "Union.aspx?Type=MYUNION&Kind=UNIONCUP&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<tr class='BarHead'><td height='25' width='50'>奖杯</td><td width='100'>杯赛名称</td><td width='70'>轮次</td><td width='100'>报名/名额</td><td width='120'>时间</td><td width='96'>操作</td></tr>";
            SqlDataReader reader = BTPCupManager.GetUnionCupListByUserIDNew(this.intUserID, 3, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_042C;
            }
            this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有联盟杯赛。</td></tr>";
            goto Label_0438;
        Label_02A9:
            switch (num5)
            {
                case 2:
                    str5 = "等待发奖";
                    break;

                case 3:
                    str5 = "已结束";
                    break;

                case 0:
                    str5 = "<a title='赛程排定时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;

                default:
                    str5 = "<a title='下轮比赛时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;
            }
            if (num5 == 0)
            {
                str4 = string.Concat(new object[] { "<a href='Union.aspx?Type=MYUNION&Kind=UNIONCUPCHECK&Page=1&CupID=", num, "'>查看</a> | <a href='Union.aspx?Type=MYUNION&Kind=VIEWUNIONCUP&CupID=", num, "'>介绍</a>" });
            }
            else
            {
                str4 = string.Concat(new object[] { "<a href='", str6, "'>赛程</a> | <a href='Union.aspx?Type=MYUNION&Kind=VIEWUNIONCUP&CupID=", num, "'>介绍</a>" });
            }
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='50'><img src='Images/Cup/", str2, "'></td><td>", str3, "</td><td>", str7, "</td><td>", num3, "/", num4, "</td><td>", str5, "</td><td>", str4, "</td></tr>" });
        Label_042C:
            if (reader.Read())
            {
                num = (int) reader["CupID"];
                int deadRound = BTPCupRegManager.GetDeadRound(num, this.intClubID);
                str2 = reader["BigLogo"].ToString().Trim();
                str3 = reader["Name"].ToString().Trim();
                byte num1 = (byte) reader["Levels"];
                int num2 = (byte) reader["Round"];
                time = (DateTime) reader["MatchTime"];
                DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(num);
                num3 = (int) cupRowByCupID["RegCount"];
                num4 = (int) cupRowByCupID["Capacity"];
                num5 = (byte) cupRowByCupID["Status"];
                str6 = cupRowByCupID["LadderURL"].ToString().Trim();
                if (deadRound == 100)
                {
                    deadRound = num2;
                }
                if (deadRound < num2)
                {
                    switch (num5)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='red'>", deadRound, "</font> / <font color='red'>", num2, "</font>" });
                            goto Label_02A9;
                    }
                    str7 = string.Concat(new object[] { "<font color='red'>", deadRound, "</font> / <font color='green'>", num2, "</font>" });
                }
                else
                {
                    switch (num5)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='green'>", num2, "</font> / <font color='red'>", num2, "</font>" });
                            goto Label_02A9;
                    }
                    str7 = string.Concat(new object[] { "<font color='green'>", num2, "</font> / <font color='green'>", num2, "</font>" });
                }
                goto Label_02A9;
            }
        Label_0438:
            reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void SetUnionIntro()
        {

            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int)accountRowByUserID["UnionID"];
            
            object obj2;
            this.divUnionIntro.Visible = true;
            int request = (int) SessionItem.GetRequest("UnionID", 0);
            DataRow allInfoByUnionID = BTPUnionManager.GetAllInfoByUnionID(request);
            int intUserID = (int) allInfoByUnionID["Creater"];
            int num2 = (int) allInfoByUnionID["Reputation"];
            int num3 = (int) allInfoByUnionID["UCount"];
            int num1 = (int) allInfoByUnionID["TopicCount"];
            string[] strArray = allInfoByUnionID["NameInfo"].ToString().Trim().Split(new char[] { '|' });
            string str4 = AccountItem.GetNickNameInfo(intUserID, strArray[0].Trim(), "Right", 20);
            string str2 = allInfoByUnionID["Name"].ToString().Trim();
            intUserID = (int) allInfoByUnionID["Creater"];
            string str3 = allInfoByUnionID["ShortName"].ToString().Trim();
            num2 = (int) allInfoByUnionID["Reputation"];
            string str = allInfoByUnionID["BigLogo"].ToString().Trim();
            string str5 = allInfoByUnionID["Remark"].ToString().Trim();
            DateTime datIn = (DateTime) allInfoByUnionID["RemarkTime"];
            string str6 = allInfoByUnionID["UQQ"].ToString().Trim();
            string str7 = "论坛";
            string str8 = allInfoByUnionID["UBBS"].ToString().Trim();
            if (this.intUnionID == request)
            {
                DataRow unionPolityRow = BTPUnionPolity.GetDelMasterRow(this.intUnionID);
                if (unionPolityRow == null)
                {
                    if (this.intUserID != intUserID)
                    {
                        str4 = str4 + "<a href='Temp_Right.aspx?Type=DELATEMASTER' target=\"Right\"><font color=red>[弹劾]</font></a>";
                    }
                }
                else
                {
                    str4 = str4 + "<a href='UnionPolity.aspx?Type=DELMASTER' ><font color=red>[投票]</font></a>";
                }
            }
            
            if (str8.IndexOf("UnionBoard.aspx?Type=UNIONBBS") != -1)
            {
                str7 = "<a href=\"UnionBoard.aspx?Type=UNIONBBS&UnionID=" + request + "&Page=1\">论坛</a>";
            }
            else if (str8 != "")
            {
                str7 = "<a href=\"" + str8.Replace("＆", "&").ToString().Trim() + "\" target=\"_blank\">论坛</a>";
            }
            else
            {
                str7 = "论坛";
            }
            string str9 = "<a target=\"Right\" href='Temp_Right.aspx?Type=UNIONMSGSEND&UID=" + request + "'>短信群发</a>";
            this.strList = string.Concat(new object[] { 
                "<tr><td><table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='60'><img src='Images/UnionLogo/", str, ".gif' height='46' width='46' border='0'></td><td><font style='line-height:130%'><strong>联盟：</strong><a href='UnionList.aspx?UnionID=", request, "&Page=1' target='Right'>", str2, "[", str3, "]</a><br><strong>盟主：</strong>", str4, "</font></td><td><font style='line-height:130%'><strong>威望：</strong>", num2, "<br><strong>盟员：</strong><a href='UnionList.aspx?UnionID=", request, "&Page=1' target='Right'>", num3, 
                "</a></font></td><td cospan='2'><font style='line-height:130%'>", str9, "<br> ", str7
             });
               
            int num5 = (int) accountRowByUserID["UnionID"];
            int num6 = (byte) accountRowByUserID["UnionCategory"];
            if ((num5 == request) && (num6 != 1))
            {
                obj2 = this.strList;
                this.strList = string.Concat(new object[] { obj2, "&nbsp;|&nbsp;<a href='SecretaryPage.aspx?Type=OUT&UserID=", this.intUserID, "'>退出联盟</a>" });
            }
            if ((num2 < 1) && (request != this.intUnionID))
            {
                this.strList = this.strList + "&nbsp;|&nbsp;竞技场";
            }
            else
            {
                obj2 = this.strList;
                this.strList = string.Concat(new object[] { obj2, "&nbsp;|&nbsp;<a href='UnionField.aspx?Type=FIELDLIST&UID=", request, "'>竞技场</a>" });
            }
            string strList = this.strList;
            this.strList = strList + "</font></td></tr></table></td></tr><tr><td><table width='100%'  border='0' cellspacing='0' cellpadding='4' bgcolor=\"#fbe2d4\"><tr>\t<td width=\"80\" align=\"left\" style=\"border-bottom:2px solid #fbaf7e;line-height:18px\"><strong>联盟公告</strong></td>\t<td align=\"right\" style=\"border-bottom:2px solid #fbaf7e\">发布时间：" + StringItem.FormatDate(datIn, "yy-MM-dd hh-mm") + "</td></tr><tr>\t<td colspan=\"2\" valign=\"top\" height=\"80\" style=\"text-indent:2em\"><p>" + str5 + "<br />\t</p>\t</td></tr><tr>\t<td align=\"left\" colspan=2><strong>联盟群：" + str6 + "</strong></td></tr><tr>\t<td align=\"left\" colspan=2><strong>管理层：</strong><span>";
            DataTable uManagerTable = BTPUnionManager.GetUManagerTable(request);
            if (uManagerTable != null)
            {
                foreach (DataRow row3 in uManagerTable.Rows)
                {
                    int num7 = (int) row3["UserID"];
                    string strNickName = row3["NickName"].ToString().Trim();
                    this.strList = this.strList + AccountItem.GetNickNameInfo(num7, strNickName, "Right", 20) + " ";
                }
            }
            else
            {
                this.strList = this.strList + "暂无管理层成员";
            }
            this.strList = this.strList + "</span></td></tr></table></td></tr>";
            if (this.intUnionIDS == request)
            {
                this.strList = this.strList + "<tr><td><table width=\"100%\" border=\"0\" cellpadding=\"2\" cellspacing=\"0\"  style=\"margin-top:10px;\"><tr >\t<td width=\"100\" align=\"left\" style=\"border-bottom:0px solid #fbaf7e;line-height:18px;\"><strong>联盟留言</strong></td>\t<td colspan=\"2\" align=\"right\" style=\"border-bottom:0px solid #fbaf7e;\"><a href=\"DevMessage.aspx?Type=UNIONMSG&Page=1\">发言 更多…</a></td></tr>  ";
                SqlDataReader reader = BTPUnionManager.GetMessageByUnionID(request, 1, 5, false);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string str11 = reader["NickName"].ToString().Trim();
                        int num8 = (int) reader["UserID"];
                        string str12 = reader["Message"].ToString().Trim();
                        DateTime time2 = (DateTime) reader["CreateTime"];
                        strList = this.strList;
                        this.strList = strList + "<tr style=\"text-align:center;color:#AF1F30;background-color:#fbe2d4;\"><td height='25' width='100'><font color='#660066'>" + AccountItem.GetNickNameInfo(num8, str11, "Right", 10) + "</font></td><td width='260' align='left' style='padding-left:5px'>" + StringItem.FormatDate(time2, "yyyy-MM-dd hh:mm:ss") + "</td><td width='170'></td></tr><tr class='BarContent' onmouseover=\"this.style.backgroundColor='#ffebdf'\" onmouseout=\"this.style.backgroundColor=''\" style='padding:4px;table-layout:fixed;word-break:break-all;'><td height='40' colspan='3' align='left' valign='middle'>" + str12 + "</td></tr>";
                    }
                }
                else
                {
                    this.strList = this.strList + "<tr>\t<td align=\"center\" colspan=\"3\">暂无留言</td></tr>";
                }
                reader.Close();
                this.strList = this.strList + "</table></td></tr>";
            }
        }

        private void SetUnionList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 12;
            this.intTotal = this.GetTotal();
            this.strList = "";
            BTPUnionManager.GetAllUnionUserCount();
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string strName = SessionItem.GetRequest("UName", 1).ToString().Trim();
            string strCurrentURL = "Union.aspx?Type=" + this.strType + "&Kind=VIEWUNION&UName=" + strName + "&";
            this.strScript = this.GetScript(strCurrentURL);
            SqlDataReader reader = BTPUnionManager.GetUnionListNew(strName, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num7;
                    string str8;
                    strName = reader["Name"].ToString().Trim();
                    int intUserID = (int) reader["Creater"];
                    string str3 = reader["ShortName"].ToString().Trim();
                    int num = (int) reader["Reputation"];
                    int intUnionID = (int) reader["UnionID"];
                    int num4 = (int) reader["StreetCupNum"];
                    int num5 = (int) reader["DevCupNum"];
                    int unionUserCount = BTPAccountManager.GetUnionUserCount(intUnionID);
                    reader["BigLogo"].ToString().Trim();
                    string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                    nickNameByUserID = AccountItem.GetNickNameInfo(intUserID, nickNameByUserID, "Right", 20);
                    reader["UQQ"].ToString().Trim();
                    str4 = reader["UBBS"].ToString().Trim();
                    DateTime time = (DateTime) reader["CreateTime"];
                    DataRow unionBoardRowByUnionID = BTPUnionBBSManager.GetUnionBoardRowByUnionID(intUnionID);
                    if (unionBoardRowByUnionID != null)
                    {
                        int num1 = (int) unionBoardRowByUnionID["TopicCount"];
                        num7 = (byte) unionBoardRowByUnionID["Category"];
                    }
                    else
                    {
                        num7 = 1;
                    }
                    if (str4.IndexOf("UnionBoard.aspx?Type=UNIONBBS") != -1)
                    {
                        if (num7 == 1)
                        {
                            string text1 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='10' width='9' border='0'>";
                        }
                        str5 = string.Concat(new object[] { "UnionBoard.aspx?Type=", this.strType, "&UnionID=", intUnionID, "&Page=1" });
                        str6 = "";
                    }
                    else
                    {
                        str5 = str4;
                        str6 = "target=\"_blank\"";
                    }
                    if (str5 != "")
                    {
                        string text2 = "<a href='" + str5.Replace("＆", "&").ToString().Trim() + "' " + str6 + ">论坛</a>";
                    }
                    if (num4 != 0)
                    {
                        string text3 = "<img src='Images/Cup/StreetCup/StreetCup_" + num4 + ".gif' height='50' width='85' border='0'>";
                    }
                    if (num5 != 0)
                    {
                        string text4 = "<img src='Images/Cup/DevisionCup/DevisionCup_" + num5 + ".gif' height='50' width='85' border='0'>";
                    }
                    if ((num < 1) || (time >= DateTime.Now.AddDays(-15.0)))
                    {
                        str8 = "竞技场";
                    }
                    else
                    {
                        str8 = string.Concat(new object[] { "<a href='UnionField.aspx?Back=", this.intPage, "&Type=FIELDLIST&UID=", intUnionID, "'>竞技场</a>" });
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"25\" style=\"padding-left:5px\" align=\"left\" ><a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINTRO&UnionID=", intUnionID, "&Page=1'>[", str3, "]</a></td><td height=\"25\" align=\"left\" ><a href='Union.aspx?Type=", this.strType, "&Kind=UNIONINTRO&UnionID=", intUnionID, "&Page=1'>", strName, "</a></td><td align=\"left\" >", nickNameByUserID, "</td><td align=\"center\" >", 
                        num, "</td><td align=\"center\" ><a href='UnionList.aspx?UnionID=", intUnionID, "&Page=1' target='Right'>", unionUserCount, "</a></td><td align=\"right\" width=\"10\"></td><td align=\"left\"  >", str8, "</td></tr>"
                     });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>";
                }
                reader.Close();
                if (SessionItem.GetRequest("UName", 1).ToString().Trim() == "")
                {
                    this.strList = this.strList + "<tr><td height='5' colspan='7'></td></tr><tr height='25'><td align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
                }
                else
                {
                    this.strList = this.strList + "<tr><td height='5' colspan='7'></td></tr><tr height='25'><td><a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'>返回联盟列表</a></td><td align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
                }
            }
            else
            {
                this.strList = "<tr><td><a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'>返回联盟列表</a></td><td height='25' colspan='5' algin='center'>暂时没有联盟。</td></tr>";
            }
        }

        private void SetUnionUserList()
        {
            SqlDataReader unionUserByNickName;
            this.intPerPage = 9;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            int request = (int) SessionItem.GetRequest("Category", 0);
            string strCurrentURL = string.Concat(new object[] { "Union.aspx?Category=", request, "&Type=", this.strType, "&Kind=", this.strKind, "&Status=UNIONER&" });
            this.strUnionMember = string.Concat(new object[] { "Union.aspx?Category=1&Type=", this.strType, "&Kind=", this.strKind, "&Status=UNIONER&Page=", this.intPage });
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            this.GetTotal();
            string str6 = "";
            int num4 = (int) BTPGameManager.GetGameRow()["Turn"];
            int num5 = 0;
            if (accountRowByUserID != null)
            {
                num5 = (byte) accountRowByUserID["UnionCategory"];
            }
            string strNickName = SessionItem.GetRequest("NickName", 1).ToString();
            if (strNickName.Trim() != "")
            {
                unionUserByNickName = BTPUnionManager.GetUnionUserByNickName(this.intUnionID, strNickName);
            }
            else
            {
                unionUserByNickName = BTPUnionManager.GetUnionUserListByIDNew(this.intUnionID, this.intPage, this.intPerPage, request);
                this.strFindUnionNick = "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
            if (unionUserByNickName.HasRows)
            {
                while (unionUserByNickName.Read())
                {
                    string str4;
                    int intUserID = (int) unionUserByNickName["UserID"];
                    string str2 = unionUserByNickName["NickName"].ToString().Trim();
                    string str3 = unionUserByNickName["ClubName"].ToString().Trim();
                    int num3 = (byte) unionUserByNickName["UnionCategory"];
                    string str5 = unionUserByNickName["UnionPosition"].ToString().Trim();
                    string str8 = unionUserByNickName["UnionReputation"].ToString().Trim();
                    if (str3 == "")
                    {
                        str3 = "无";
                    }
                    switch (num3)
                    {
                        case 1:
                            str4 = "<font color='red'>[盟主]</font>";
                            break;

                        case 2:
                            str4 = "<font color='red'>[管理员]</font>";
                            break;

                        default:
                            str4 = "";
                            break;
                    }
                    if (str5 == "")
                    {
                        str5 = "暂无";
                    }
                    str5 = "<strong>" + str5 + "</strong>" + str4 + "";
                    if (num5 == 1)
                    {
                        if ((num3 != 1) && (num3 != 2))
                        {
                            str6 = string.Concat(new object[] { "<a title='踢出' href='SecretaryPage.aspx?Type=KICKOUT&UserID=", intUserID, "'>踢</a>|<a title='任命' href='SecretaryPage.aspx?Type=ORDAIN&UserID=", intUserID, "'>任</a>|<a title='封号' href='SecretaryPage.aspx?Type=POSITION&UserID=", intUserID, "'>封</a>" });
                        }
                        else if (num3 == 2)
                        {
                            str6 = string.Concat(new object[] { "<a title='踢出' href='SecretaryPage.aspx?Type=KICKOUT&UserID=", intUserID, "'>踢</a>|<a title='解任' href='SecretaryPage.aspx?Type=UNCHAIN&UserID=", intUserID, "'>解</a>|<a title='封号' href='SecretaryPage.aspx?Type=POSITION&UserID=", intUserID, "'>封</a>" });
                        }
                        else if (num3 == 1)
                        {
                            str6 = "<a title='封号' href='SecretaryPage.aspx?Type=POSITION&UserID=" + intUserID + "'>封</a>";
                        }
                        object obj2 = str6;
                        obj2 = string.Concat(new object[] { obj2, "|<a title='奖励' target=\"Right\" href='Temp_Right.aspx?Type=SENDWEALTH&UserID=", intUserID, "'>奖</a>" });
                        str6 = string.Concat(new object[] { obj2, "|<a title='盟战可支配威望' href='Temp_Right.aspx?Type=SETUREPUTATION&Tag=", intUserID, "' target=\"Right\">战</a>" });
                        if (num4 < 4)
                        {
                            obj2 = str6;
                            str6 = string.Concat(new object[] { obj2, "|<a title='邀请参加冠军杯' target=\"Right\" href='Temp_Right.aspx?Type=CHAMPIONCUP&UserID=", intUserID, "'>邀</a>" });
                        }

                        if(true)
                        {
                            obj2 = str6;
                            str6 = string.Concat(new object[] { obj2, "|<a title='邀请参加联盟争霸赛' target=\"Right\" href='Temp_Right.aspx?Type=UNIONCUPSEND&UserID=", intUserID, "'>霸</a>" });
                        }

                    }
                    else if (((num5 == 2) && (num3 != 2)) && (num3 != 1))
                    {
                        str6 = "<a href='SecretaryPage.aspx?Type=KICKOUT&UserID=" + intUserID + "'>踢出</a>";
                    }
                    else
                    {
                        str6 = "--";
                    }
                    string strList = this.strList;
                    this.strList = strList + "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>" + AccountItem.GetNickNameInfo(intUserID, str2, "Right") + "</td><td>" + AccountItem.GetNickNameInfo(intUserID, str3, "Right") + "</td><td>" + str5 + "</td><td>" + str8 + "</td><td>" + str6 + "</td></tr>";
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
                unionUserByNickName.Close();
            }
            this.strList = this.strList + this.strFindUnionNick;
        }

        private void SetWealthList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "Union.aspx?Type=MYUNION&Kind=WEALTHFINANCE&";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPerPage = 10;
            this.sbWealthPage.Append(this.GetViewPage(strCurrentURL));
            SqlDataReader reader = BTPUWealthFinanceManager.GetUWFinanceTableByUnionID(0, this.intPage, this.intPerPage, this.intUnionID);
            while (reader.Read())
            {
                int num = (int) reader["Income"];
                int num2 = (int) reader["Outcome"];
                int num3 = Convert.ToInt32(reader["UserCategory"]);
                DateTime datIn = Convert.ToDateTime(reader["CreateTime"]);
                string str2 = reader["NickName"].ToString();
                string str3 = reader["Remark"].ToString();
                this.sbWealthList.Append("<tr class=\"BarContent\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbWealthList.Append("<td height=\"25\" align=\"center\"><font color=\"#008000\">" + num + "</font></td>");
                this.sbWealthList.Append("<td align=\"center\"><font color=\"#ff0000\">" + num2 + "</font></td>");
                this.sbWealthList.Append("<td align=\"center\">" + StringItem.FormatDate(datIn, "yyyy-MM-dd") + "</td>");
                switch (num3)
                {
                    case 1:
                        this.sbWealthList.Append("<td align=\"center\"><font color=\"#0000FF\">盟主</font></td>");
                        break;

                    case 2:
                        this.sbWealthList.Append("<td align=\"center\"><font color=\"#B11F32\">" + str2 + "</font></td>");
                        break;

                    case 3:
                        this.sbWealthList.Append("<td align=\"center\"><font color=\"#660066\">杯赛</font></td>");
                        break;
                }
                this.sbWealthList.Append("<td align=\"left\" style=\"PADDING-LEFT:4px\">" + str3 + "</td><tr>");
                this.sbWealthList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
            }
            reader.Close();
        }
    }
}


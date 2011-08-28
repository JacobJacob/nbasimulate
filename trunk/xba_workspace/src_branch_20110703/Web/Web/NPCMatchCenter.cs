namespace Web
{
    using AjaxPro;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class NPCMatchCenter : Page
    {
        protected LinkButton btnAccMatch;
        protected ImageButton btnOK;
        protected ImageButton btnReg;
        protected ImageButton btnReg5;
        protected ImageButton btnSearch;
        protected ImageButton btnSend;
        protected CheckBoxList cbMatch;
        protected DropDownList ddlCategory;
        protected HtmlInputHidden hidSelectValue;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        private int intLevel;
        private int intOwnWealth;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intRUserID;
        private int intUserID;
        protected RadioButton rbAlway;
        protected RadioButton rbMain;
        protected RadioButton rbS;
        protected RadioButtonList rbSendScore;
        protected RadioButton rbStatus1;
        protected RadioButton rbStatus2;
        protected RadioButton rbStatus3;
        protected RadioButton rbStatus4;
        protected RadioButton rbStatus51;
        protected RadioButton rbStatus52;
        protected RadioButton rbStatus53;
        protected RadioButton rbStatus54;
        protected RadioButton rbV;
        public StringBuilder sbList = new StringBuilder("");
        public StringBuilder sbMatchTypeList = new StringBuilder();
        public StringBuilder sbPage = new StringBuilder();
        public StringBuilder sbPageIntro = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbWealthList = new StringBuilder();
        public StringBuilder sbWealthMatch = new StringBuilder();
        public string strAdMatch;
        public string strAdMatchA;
        public string strClubName;
        private string strDevCode;
        public string strErrList;
        private string strLogo;
        public string strMainScript;
        public string strMatchCategory;
        public string strMatchCategoryA;
        public string strMatchIntro;
        public string strNickName;
        public string strOnload = "";
        public string strSayScript;
        public string strTrainMoney = "";
        public string strType;
        public string strURL;
        public string strUserlink;
        public string strWealthMatchMsg;
        protected TextBox tbDevLevel;
        protected TextBox tbIntro;
        protected HtmlTable tblFMatchMsg;
        protected HtmlTable tblFMatchSend;
        protected HtmlTable tblOnlineList;
        protected HtmlTable tblTrainCenter;
        protected HtmlTable tblTrainCenter5;
        protected HtmlTable tblTrainCenterNew;
        protected HtmlTable tblTrainReg;
        protected HtmlTable tblTrainReg5;
        protected HtmlTable tblWealthMatchCenter;
        protected TextBox tbNickName;
        protected TextBox tbPoint;
        protected TextBox tbTrainWealth;
        protected TextBox tbTrainWealth5;
        protected TextBox tbWealth;
        protected TextBox tbWealthA;
        protected TextBox tbWealthB;
        protected TextBox tbWealthSend;
        protected TextBox Textbox1;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
        }

        private void btnReg_Click(object sender, ImageClickEventArgs e)
        {
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                string strShortName = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["ShortName"].ToString().Trim();
                int num = MatchItem.TrainMatchType(this.intClubID3, 0, 2);
                if (BTPFriMatchManager.GetRegRowByClubID(this.intClubID3, 3) == null)
                {
                    switch (num)
                    {
                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=4100");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=4102");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=4102");
                            return;

                        case 1:
                        {
                            int intConsume = 0;
                            int intType = 1;
                            if (this.rbStatus1.Checked)
                            {
                                intType = 1;
                            }
                            else if (this.rbStatus2.Checked)
                            {
                                intType = 2;
                            }
                            else if (this.rbStatus3.Checked)
                            {
                                intType = 3;
                            }
                            else if (this.rbStatus4.Checked)
                            {
                                intType = 4;
                            }
                            if (intType == 3)
                            {
                                if (!StringItem.IsNumber(this.tbTrainWealth.Text))
                                {
                                    base.Response.Redirect("Report.aspx?Parameter=4108b!Type.TRAINCENTERREG");
                                    return;
                                }
                                intConsume = Convert.ToInt32(this.tbTrainWealth.Text.Trim());
                                if ((intConsume < 100) || (intConsume > 0x2710))
                                {
                                    base.Response.Redirect("Report.aspx?Parameter=4108b!Type.TRAINCENTERREG");
                                    return;
                                }
                            }
                            if ((intType == 4) && (this.intPayType == 1))
                            {
                                base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTER");
                                return;
                            }
                            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                            if (accountRowByUserID == null)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=12");
                                return;
                            }
                            int num4 = (int) accountRowByUserID["Wealth"];
                            if (num4 >= intConsume)
                            {
                                BTPFriMatchManager.RegTrainCenter(this.intClubID3, this.intLevel, this.strClubName, strShortName, this.strLogo, intType, intConsume, 3);
                                base.Response.Redirect("Report.aspx?Parameter=4108!Type.TRAINCENTER");
                                return;
                            }
                            base.Response.Redirect("Report.aspx?Parameter=4108d!Type.TRAINCENTERREG");
                            return;
                        }
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=4107");
                }
            }
        }

        private void btnReg_Click_AD(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTER");
        }

        private void btnReg5_Click(object sender, ImageClickEventArgs e)
        {
            if (this.intClubID5 < 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=4112x");
            }
            else
            {
                string strShortName = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["ShortName"].ToString().Trim();
                int num = MatchItem.TrainMatchType5(this.intClubID5, 0, 2);
                if (BTPFriMatchManager.GetRegRowByClubID(this.intClubID5, 5) == null)
                {
                    if (num == 1)
                    {
                        int intConsume = 0;
                        int intType = 1;
                        if (this.rbStatus51.Checked)
                        {
                            intType = 1;
                        }
                        else if (this.rbStatus52.Checked)
                        {
                            intType = 2;
                        }
                        else if (this.rbStatus53.Checked)
                        {
                            intType = 3;
                        }
                        else if (this.rbStatus54.Checked)
                        {
                            intType = 4;
                        }
                        if (intType == 3)
                        {
                            if (!StringItem.IsNumber(this.tbTrainWealth.Text))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=4108b!Type.TRAINCENTERREG5");
                                return;
                            }
                            intConsume = Convert.ToInt32(this.tbTrainWealth5.Text.Trim());
                            if ((intConsume < 100) || (intConsume > 0x2710))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=4108b!Type.TRAINCENTERREG5");
                                return;
                            }
                        }
                        if ((intType == 4) && (this.intPayType == 1))
                        {
                            base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTER5");
                            return;
                        }
                        DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                        if (accountRowByUserID == null)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=12");
                            return;
                        }
                        int num4 = (int) accountRowByUserID["Wealth"];
                        if (num4 >= intConsume)
                        {
                            BTPFriMatchManager.RegTrainCenter(this.intClubID5, DevCalculator.GetLevel(this.strDevCode), this.strClubName, strShortName, this.strLogo, intType, intConsume, 5);
                            base.Response.Redirect("Report.aspx?Parameter=4108!Type.TRAINCENTER5");
                            return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=4108d!Type.TRAINCENTERREG5");
                    }
                    switch (num)
                    {
                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=4112a5");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=4112b5");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=4112c5");
                            return;

                        case 5:
                            base.Response.Redirect("Report.aspx?Parameter=4112d5");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=4112e5");
                            return;

                        case 7:
                            base.Response.Redirect("Report.aspx?Parameter=4112f5");
                            return;

                        case 8:
                            base.Response.Redirect("Report.aspx?Parameter=4112g5");
                            return;

                        case 9:
                            base.Response.Redirect("Report.aspx?Parameter=4112h5");
                            return;

                        case 10:
                            base.Response.Redirect("Report.aspx?Parameter=4112i5");
                            return;

                        case 11:
                            base.Response.Redirect("Report.aspx?Parameter=4112j5");
                            return;
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=4107");
                }
            }
        }

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            int intWealthA = 0;
            int intWealthB = 0;
            int intClubAPoint = 0;
            int intClubBPoint = 0;
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                int num9;
                string strNickName = this.tbNickName.Text.ToString().Trim();
                string strIn = this.tbIntro.Text.ToString().Trim();
                int intCategory = Convert.ToInt16(this.hidSelectValue.Value);
                int intUserID = 0;
                int num7 = 0;
                int num8 = 0;
                if (this.rbS.Checked)
                {
                    num9 = 3;
                }
                else
                {
                    num9 = 5;
                }
                if (strNickName == "")
                {
                    base.Response.Redirect("Report.aspx?Parameter=420e");
                }
                else
                {
                    switch (num9)
                    {
                        case 3:
                        {
                            if (intCategory == 7)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=498c");
                                return;
                            }
                            if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3) < 4)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=495a");
                                return;
                            }
                            int clubIDByNickName = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                            if (clubIDByNickName == 0)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=497a");
                                return;
                            }
                            if (BTPPlayer3Manager.GetPlayer3CountByCID(clubIDByNickName) < 4)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=496a");
                                return;
                            }
                            intUserID = BTPClubManager.GetUserIDByClubID(clubIDByNickName);
                            break;
                        }
                        case 5:
                        {
                            if (intCategory == 2)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=499");
                                return;
                            }
                            if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) < 6)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=495a");
                                return;
                            }
                            int intClubIDB = BTPClubManager.GetClubIDByNickName(strNickName, 5);
                            if (MatchItem.InOnlyOneMatch(this.intClubID5, intClubIDB))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=491b");
                                return;
                            }
                            if (intClubIDB == 0)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=497a");
                                return;
                            }
                            if (BTPPlayer5Manager.GetPlayer5CountByClubID(intClubIDB) < 6)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=496a");
                                return;
                            }
                            intUserID = BTPClubManager.GetUserIDByClubID(intClubIDB);
                            break;
                        }
                    }
                    if (intCategory == 2)
                    {
                        int clubIDByUIDCategory = BTPClubManager.GetClubIDByUIDCategory(this.intUserID, 3);
                        int num17 = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                        switch (MatchItem.TrainMatchType(clubIDByUIDCategory, num17, 1))
                        {
                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=4100");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=4101");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=4102");
                                return;

                            case 5:
                                base.Response.Redirect("Report.aspx?Parameter=4103");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=4106");
                                return;
                        }
                    }
                    if (intCategory == 10)
                    {
                        int intClubIDA = BTPClubManager.GetClubIDByUIDCategory(this.intUserID, 5);
                        int num20 = BTPClubManager.GetClubIDByNickName(strNickName, 5);
                        switch (MatchItem.TrainMatchType5(intClubIDA, num20, 1))
                        {
                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=4112a");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=4112b");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=4112c");
                                return;

                            case 5:
                                base.Response.Redirect("Report.aspx?Parameter=4112d");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=4112e");
                                return;

                            case 7:
                                base.Response.Redirect("Report.aspx?Parameter=4112f");
                                return;

                            case 8:
                                base.Response.Redirect("Report.aspx?Parameter=4112g");
                                return;

                            case 9:
                                base.Response.Redirect("Report.aspx?Parameter=4112h");
                                return;

                            case 10:
                                base.Response.Redirect("Report.aspx?Parameter=4112i");
                                return;

                            case 11:
                                base.Response.Redirect("Report.aspx?Parameter=4112j");
                                return;
                        }
                    }
                    if (intCategory == 3)
                    {
                        if (num9 == 3)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=500");
                            return;
                        }
                        num7 = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
                        num8 = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["Wealth"];
                        string str3 = this.tbWealthA.Text.ToString();
                        string str4 = this.tbWealthB.Text.ToString();
                        if (StringItem.IsNumber(str3) && StringItem.IsNumber(str4))
                        {
                            if ((StringItem.GetStrLength(str3.Trim()) <= 4) || (StringItem.GetStrLength(str4.Trim()) <= 4))
                            {
                                intWealthA = Convert.ToInt32(str3);
                                intWealthB = Convert.ToInt32(str4);
                            }
                            else
                            {
                                base.Response.Redirect("Report.aspx?Parameter=516");
                                return;
                            }
                            if (((intWealthA > 0x3e8) || (intWealthB > 0x3e8)) || ((intWealthA < 10) || (intWealthB < 10)))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=516");
                                return;
                            }
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=487");
                            return;
                        }
                        if (intWealthA > num7)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=490");
                            return;
                        }
                        if (intWealthB > num8)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=488");
                            return;
                        }
                        int runMatchCount = BTPFriMatchManager.GetRunMatchCount();
                        if ((this.intPayType != 1) && (runMatchCount >= 20))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=4911");
                            return;
                        }
                    }
                    if (intCategory == 4)
                    {
                        if (num9 == 3)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=500");
                            return;
                        }
                        num7 = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
                        num8 = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["Wealth"];
                        string str5 = this.tbWealth.Text.ToString();
                        string str6 = this.tbPoint.Text.ToString();
                        if (StringItem.IsNumber(str5) && StringItem.IsNumber(str6))
                        {
                            if (StringItem.GetStrLength(str5.Trim()) <= 4)
                            {
                                intWealthA = Convert.ToInt32(str5);
                                intWealthB = Convert.ToInt32(str5);
                            }
                            else
                            {
                                base.Response.Redirect("Report.aspx?Parameter=516");
                                return;
                            }
                            if ((intWealthA > 0x3e8) || (intWealthA < 10))
                            {
                                base.Response.Redirect("Report.aspx?Parameter=516");
                                return;
                            }
                            if (!this.rbMain.Checked && !this.rbAlway.Checked)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=514");
                                return;
                            }
                            if (StringItem.GetStrLength(str6.Trim()) >= 3)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=517");
                                return;
                            }
                            if (this.rbMain.Checked)
                            {
                                intClubAPoint = Convert.ToInt32(str6);
                            }
                            if (this.rbAlway.Checked)
                            {
                                intClubBPoint = Convert.ToInt32(str6);
                            }
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=487");
                            return;
                        }
                        if (intWealthA > num7)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=512");
                            return;
                        }
                        if (intWealthB > num8)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=488");
                            return;
                        }
                        int num23 = BTPFriMatchManager.GetRunMatchCount();
                        if ((this.intPayType != 1) && (num23 >= 20))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=4911");
                            return;
                        }
                    }
                    strIn = StringItem.GetValidWords(strIn);
                    if (!StringItem.IsValidContent(strIn, 0, 200))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3ab");
                    }
                    else
                    {
                        switch (BTPFriMatchManager.SetFriMatch(strNickName, this.intUserID, intCategory, num9, strIn, intWealthA, intWealthB, intClubAPoint, intClubBPoint))
                        {
                            case 0x15:
                                base.Response.Redirect("Report.aspx?Parameter=500");
                                return;

                            case 0x16:
                                base.Response.Redirect("Report.aspx?Parameter=500b");
                                return;

                            case 8:
                                base.Response.Redirect("Report.aspx?Parameter=515");
                                return;

                            case 9:
                                base.Response.Redirect("Report.aspx?Parameter=516");
                                return;

                            case 10:
                                base.Response.Redirect("Report.aspx?Parameter=517");
                                return;

                            case 11:
                                base.Response.Redirect("Report.aspx?Parameter=518");
                                return;

                            case 12:
                                base.Response.Redirect("Report.aspx?Parameter=519");
                                return;

                            case 1:
                                DTOnlineManager.SetHasMsgByUserID(intUserID);
                                base.Response.Redirect("Report.aspx?Parameter=40a");
                                return;

                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=48");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=491");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=492");
                                return;

                            case 0:
                                base.Response.Redirect("Report.aspx?Parameter=493");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=498a");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=47");
                    }
                }
            }
        }

        private void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void DelTrianReg()
        {
            int request = (int) SessionItem.GetRequest("MType", 0);
            int num2 = this.intClubID5;
            if (request != 5)
            {
                request = 3;
                num2 = this.intClubID3;
            }
            BTPFriMatchManager.DelTrainRegRowByClubID(num2, request);
            if (request == 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=4108c5");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=4108c");
            }
        }

        private void FMatchMsgList()
        {
            string str;
            this.sbList = new StringBuilder();
            DataTable tableWealthMatchMsg = this.GetTableWealthMatchMsg(0);
            DataRow drParameter = Global.drParameter;
            bool flag = false;
            if (drParameter != null)
            {
                flag = (bool) drParameter["Canbet"];
            }
            if (flag)
            {
                this.sbList.Append("<tr><td height='25' colspan='3'><marquee scrollamount=\"3\" scrolldelay=\"90\" loop=\"0\" >");
                if (tableWealthMatchMsg != null)
                {
                    foreach (DataRow row in tableWealthMatchMsg.Rows)
                    {
                        str = row["Content"].ToString().Trim();
                        this.sbList.Append(str + "　　　　　　");
                    }
                }
                this.sbList.Append("</marquee></td></tr>");
                //tableWealthMatchMsg.Close();
            }
            string strCurrentURL = "FriMatchMessage.aspx?Type=FMATCHMSG&";
            this.intPerPage = 10;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetMsgTotal();
            this.GetMsgScript(strCurrentURL);
            DataTable friMatchMsgTableNew = BTPFriMatchMsgManager.GetFriMatchMsgTableNew(this.intPage, this.intPerPage);
            if (friMatchMsgTableNew!=null)
            {
                foreach (DataRow row in friMatchMsgTableNew.Rows)
                {
                    string strNickName = row["NickName"].ToString().Trim();
                    DateTime datIn = (DateTime)row["CreateTime"];
                    string str3 = StringItem.FormatDate(datIn, "hh:mm:ss");
                    str = row["Content"].ToString().Trim();
                    int intUserID = (int)row["UserID"];
                    row["ClubName"].ToString().Trim();
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td style='padding:4px' width='560'>[ " + str3 + " ] " + MessageItem.GetNickNameLink(intUserID, strNickName, 1) + "：" + str + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' ></td></tr>");
                }
                //friMatchMsgTableNew.Close();
                this.sbList.Append("<tr><td height='25' align='right'>" + this.GetMsgViewPage2(strCurrentURL) + "</td></tr>");
            }
            else
            {
                this.sbList.Append("<tr>");
                this.sbList.Append("<td height='25' colspan='3' align='center'>暂无留言</td>");
                this.sbList.Append("</tr>");
            }
        }

        private void FMatchSend()
        {
            this.intRUserID = (int) SessionItem.GetRequest("UserID", 0);
            this.sbList = new StringBuilder();
            this.btnSend.ImageUrl = SessionItem.GetImageURL() + "SendFMatch.gif";
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                this.rbV.Visible = false;
            }
            SessionItem.GetRequest("NickName", 1).ToString().Trim();
            if (!base.IsPostBack)
            {
                DataView view = new DataView(DDLItem.GetFriMatchItem(1, this.intCategory));
                this.ddlCategory.DataSource = view;
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Category";
                this.ddlCategory.DataBind();
                if (this.intRUserID == 0)
                {
                    this.tbNickName.Text = "";
                }
                else if (this.intRUserID == this.intUserID)
                {
                    base.Response.Redirect("Report.aspx?Parameter=498a");
                }
                else
                {
                    string str = BTPAccountManager.GetNickNameByUserID(this.intRUserID).Trim();
                    this.tbNickName.Text = str;
                }
                int request = (int) SessionItem.GetRequest("Category", 0);
                int num2 = (int) SessionItem.GetRequest("Wealth", 0);
                int num3 = (int) SessionItem.GetRequest("MatchType", 0);
                if (((request > 0) && (num2 > 0)) && (num3 > 0))
                {
                    this.ddlCategory.SelectedValue = request.ToString();
                    switch (request)
                    {
                        case 3:
                            this.tbWealthB.Text = num2.ToString();
                            break;

                        case 4:
                            this.tbWealth.Text = num2.ToString();
                            break;
                    }
                    if (num3 == 5)
                    {
                        this.rbV.Checked = true;
                    }
                }
            }
        }

        private void GetMatchTypeList(int intPageCategor)
        {
        }

        private void GetMsgScript(string strCurrentURL)
        {
            this.sbScript.Append("<script language=\"javascript\">");
            this.sbScript.Append("function JumpPage()");
            this.sbScript.Append("{");
            this.sbScript.Append("var strPage=document.all.Page.value;");
            this.sbScript.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.sbScript.Append("}");
            this.sbScript.Append("</script>");
        }

        private int GetMsgTotal()
        {
            int friMatchMsgCountNew = 0;
            if (this.strType == "FMATCHMSG")
            {
                friMatchMsgCountNew = BTPFriMatchMsgManager.GetFriMatchMsgCountNew();
            }
            if (this.strType == "ONLINE")
            {
                friMatchMsgCountNew = DTOnlineManager.GetOnlineMCount();
            }
            if (this.strType == "TRAINCENTER")
            {
                return BTPFriMatchManager.GetTrainCenterCount(this.intUserID, 1, this.intPayType, 3);
            }
            if (this.strType == "TRAINCENTER5")
            {
                friMatchMsgCountNew = BTPFriMatchManager.GetTrainCenterCount(this.intUserID, 1, this.intPayType, 5);
            }
            return friMatchMsgCountNew;
        }

        private string GetMsgViewPage2(string strCurrentURL)
        {
            string str5;
            string[] strArray;
            int msgTotal = this.GetMsgTotal();
            int num2 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            if (this.strType == "TRAINCENTER")
            {
                str2 = "";
            }
            else
            {
                str2 = "";
            }
            if (this.intPage == 1)
            {
                str2 = str2 + "上一页";
            }
            else
            {
                str5 = str2;
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
                str3 = str3 + "下一页";
            }
            else
            {
                str5 = str3;
                strArray = new string[] { str5, "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
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
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        private DataTable GetTableWealthMatchMsg(int intWMMID)
        {
            string commandText = "Exec NewBTP.dbo.GetTableWealthMatchMsg " + intWMMID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        private void GetWealthList(int intCategor)
        {
        }

        private void GetWealthMatchList()
        {
        }

        private void InitializeComponent()
        {
            this.ddlCategory.SelectedIndexChanged += new EventHandler(this.ddlCategory_SelectedIndexChanged);
            this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
            Cuter cuter = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID]));
            if (cuter.GetIndex("0") < 0)
            {
                this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click);
            }
            else
            {
                this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click_AD);
            }
            this.btnReg5.Click += new ImageClickEventHandler(this.btnReg5_Click);
            //this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void List()
        {
            this.sbList = new StringBuilder();
            string strCurrentURL = "FriMatchMessage.aspx?Type=ONLINE&";
            this.intPerPage = 11;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            DataRow[] onlineMRow = DTOnlineManager.GetOnlineMRow();
            int length = onlineMRow.Length;
            this.GetMsgScript(strCurrentURL);
            int num2 = this.intPage * this.intPerPage;
            int num3 = (this.intPage - 1) * this.intPerPage;
            int num4 = length;
            if (length > num2)
            {
                num4 = num2;
            }
            if (length > 0)
            {
                for (int i = num3; i < num4; i++)
                {
                    string str6;
                    string str9;
                    DataRow row = onlineMRow[i];
                    int intUserID = (int) row["UserID"];
                    int num6 = (int) row["UnionID"];
                    if (num6 < 1)
                    {
                        str6 = "";
                    }
                    else
                    {
                        str6 = row["ShortName"].ToString().Trim() + "-";
                    }
                    string strNickName = row["NickName"].ToString();
                    string str4 = row["Levels"].ToString();
                    string str3 = row["ClubLogo"].ToString();
                    string str5 = row["ClubName3"].ToString();
                    str3 = SessionItem.GetImageURL() + "Club/Logo/" + str4 + "/" + str3 + ".gif";
                    str3 = "<img src='" + str3 + "' width='46' height='46' border='0'>";
                    bool blnSex = (bool) row["Sex"];
                    string str7 = row["QQ"].ToString().Trim();
                    int num9 = (int) row["Category"];
                    if (num9 == 5)
                    {
                        str9 = DevCalculator.GetLevel(row["DevCode"].ToString().Trim()).ToString().Trim();
                    }
                    else
                    {
                        str9 = "无";
                    }
                    int intWealth = (int) row["Wealth"];
                    string wealthName = AccountItem.GetWealthName(intWealth);
                    if (str7 != "")
                    {
                        str7 = "<a href=\"http://wpa.qq.com/msgrd?v=1&uin=" + str7 + "&site=www.xba.com.cn&menu=yes\" target=\"blank\"><img alt=\"有事您Q我！\" src=\"http://wpa.qq.com/pa?p=1:" + str7 + ":5\" border=\"0\" width=\"61\" height=\"15\"></a>";
                    }
                    else
                    {
                        str7 = "暂无资料";
                    }
                    string str10 = "<a href='FriMatchMessage.aspx?Type=FMATCHSEND&UserID=" + intUserID + "'>约战</a>";
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td>" + wealthName + "</td>");
                    this.sbList.Append("<td height='28' align='left'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbList.Append("<td align='left'>" + AccountItem.GetNickNameInfo(intUserID, str6 + str5, "Right", 13) + "</td>");
                    this.sbList.Append("<td>" + str4 + "</td>");
                    this.sbList.Append("<td>" + str9 + "</td>");
                    this.sbList.Append("<td>" + str7 + "</td>");
                    this.sbList.Append("<td>" + str10 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
                this.sbList.Append("<tr><td height='25' align='right' colspan='7'>" + this.GetMsgViewPage2(strCurrentURL) + "</td></tr>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            string str;
            DataRow trainCenterRowByClubID;
            int index;
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            this.strMainScript = "";
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strNickName = onlineRowByUserID["NickName"].ToString();
            this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
            this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
            this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.intCategory = (int) onlineRowByUserID["Category"];
            this.intPayType = (int) onlineRowByUserID["PayType"];
            this.intOwnWealth = (int) onlineRowByUserID["Wealth"];
            this.intLevel = (int) onlineRowByUserID["Levels"];
            this.strDevCode = onlineRowByUserID["DevCode"].ToString().Trim();
            this.strLogo = onlineRowByUserID["ClubLogo"].ToString().Trim();
            this.tblFMatchMsg.Visible = false;
            this.tblOnlineList.Visible = false;
            this.tblFMatchSend.Visible = false;
            this.tblTrainCenter.Visible = false;
            this.tblWealthMatchCenter.Visible = false;
            this.tblTrainReg.Visible = false;
            this.tblTrainReg5.Visible = false;
            this.tblTrainCenterNew.Visible = false;
            this.tblTrainCenter5.Visible = false;
            this.btnReg.ImageUrl = SessionItem.GetImageURL() + "button_29.GIF";
            this.btnReg5.ImageUrl = SessionItem.GetImageURL() + "button_29.GIF";
            switch (this.strType)
            {
                case "FMATCHMSG":
                    this.tblFMatchMsg.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_05.GIF' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick='javascript:window.location.reload();' title='刷新'><img src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a>");
                    this.FMatchMsgList();
                    goto Label_0E6E;

                case "ONLINE":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>在线经理</li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    if (ServerParameter.strCopartner == "XBA")
                    {
                        this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "blanks.gif' border='0' height='0' width='119'><a href='ModifyClub.aspx' target='Center'><img src='" + SessionItem.GetImageURL() + "55.gif' border='0' height='16' width='93'</a>");
                    }
                    this.tblOnlineList.Visible = true;
                    this.List();
                    goto Label_0E6E;

                case "FMATCHSEND":
                    this.tblFMatchSend.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>我的约战</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align=\"absmiddle\" src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.rbS.Attributes.Add("onclick", "CheckRB(this)");
                    this.rbV.Attributes.Add("onclick", "CheckRB(this)");
                    this.FMatchSend();
                    this.strOnload = "";
                    goto Label_0E6E;

                case "TRAINCENTER5":
                    str = SessionItem.GetRequest("CC", 1).ToString().Trim();
                    this.strURL = "";
                    if (str == "AdMatch")
                    {
                        this.strURL = "&CC=MMA";
                    }
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>职业训练</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1" + this.strURL + "'>我的约战</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align=\"absmiddle\" src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    trainCenterRowByClubID = BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID5, 5);
                    if (trainCenterRowByClubID != null)
                    {
                        byte num = (byte) trainCenterRowByClubID["Type"];
                        int num2 = (int) trainCenterRowByClubID["Consume"];
                        if (num == 3)
                        {
                            this.strTrainMoney = "<a style='margin-left:50px' title='剩余经费余额'>经费余额：" + num2 + "</a>";
                        }
                    }
                    this.sbPageIntro.Append("<span>" + this.strTrainMoney + "</span>");
                    if ((trainCenterRowByClubID == null) && (this.intPayType != 1))
                    {
                        base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTERREG5");
                        return;
                    }
                    this.tblTrainCenter.Visible = true;
                    this.TrainCenterRegList5();
                    goto Label_0E6E;

                case "TRAINCENTER":
                {
                    str = SessionItem.GetRequest("CC", 1).ToString().Trim();
                    this.strURL = "";
                    if (str == "AdMatch")
                    {
                        this.strURL = "&CC=MMA";
                    }
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球训练</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER5\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1" + this.strURL + "'>我的约战</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align=\"absmiddle\" src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    trainCenterRowByClubID = BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID3, 3);
                    if (trainCenterRowByClubID != null)
                    {
                        byte num3 = (byte) trainCenterRowByClubID["Type"];
                        int num4 = (int) trainCenterRowByClubID["Consume"];
                        if (num3 == 3)
                        {
                            this.strTrainMoney = "<a style='margin-left:50px' title='剩余经费余额'>经费余额：" + num4 + "</a>";
                        }
                    }
                    this.sbPageIntro.Append("<span>" + this.strTrainMoney + "</span>");
                    Cuter cuter = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID]));
                    index = cuter.GetIndex("0");
                    if (index == -1)
                    {
                        if ((trainCenterRowByClubID == null) && (this.intPayType != 1))
                        {
                            base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTERREG");
                            return;
                        }
                        this.tblTrainCenter.Visible = true;
                        this.TrainCenterRegList();
                    }
                    else if (index == 1)
                    {
                        this.strClubName = string.Concat(new object[] { "<a href='ShowClub.aspx?ClubID=", this.intClubID3, "&Type=3' title='", this.strClubName, "' target='Right'>", StringItem.GetShortString(this.strClubName, 20, "."), "</a>" });
                        if (str == "AdMatch")
                        {
                            this.strAdMatch = "--";
                            this.strAdMatchA = "--";
                            this.strMatchCategoryA = "比赛中...";
                            this.strMatchCategory = "<a href='FMatchCenter.aspx?Type=TRAINCENTER&CC=MMA'>比赛中...</a>";
                            this.strMatchIntro = "<tr><td colspan=\"5\" height=\"25\" align=\"center\"><font color=\"red\">请点击上面的“比赛中...”，来完成此次比赛任务。</font></td></tr>";
                        }
                        else
                        {
                            this.strAdMatch = "<a href='Report.aspx?Parameter=40b'>开始比赛</a>";
                            this.strAdMatchA = "<font color='#333333'>退出</font>";
                            this.strMatchCategoryA = "等待中...";
                            this.strMatchCategory = "等待中...";
                            this.strMatchIntro = "<tr><td colspan=\"5\" height=\"25\" align=\"center\"><font color=\"red\">任务提示：点击“开始比赛”即可挑战魔鬼队。</font></td></tr>";
                        }
                        this.tblTrainCenterNew.Visible = true;
                        this.tblTrainCenter.Visible = false;
                    }
                    else
                    {
                        base.Response.Write("<script>window.top.Main.location='Main_I.aspx';</script>");
                    }
                    goto Label_0E6E;
                }
                case "TRAINCENTERREG5":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>职业训练</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    if (this.intClubID5 >= 1)
                    {
                        index = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID])).GetIndex("0");
                        if ((index == 1) && (index != -1))
                        {
                            this.rbStatus52.Enabled = false;
                            this.rbStatus53.Enabled = false;
                            this.rbStatus54.Enabled = false;
                        }
                        else
                        {
                            if (BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID5, 5) != null)
                            {
                                base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTER5");
                                return;
                            }
                            this.rbStatus51.Attributes.Add("onclick", "CheckTrainStatus5(this);");
                            this.rbStatus52.Attributes.Add("onclick", "CheckTrainStatus5(this);");
                            this.rbStatus53.Attributes.Add("onclick", "CheckTrainStatus5(this);");
                            if (this.intPayType == 1)
                            {
                                this.rbStatus54.Attributes.Add("onclick", "CheckTrainStatus5(this);");
                            }
                            else
                            {
                                this.rbStatus54.Enabled = false;
                            }
                            this.tbTrainWealth.Attributes.Add("onkeyup", "CheckText5(this);");
                        }
                        this.tblTrainReg5.Visible = true;
                        goto Label_0E6E;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=4112x");
                    return;

                case "TRAINCENTERREG":
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球训练</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    index = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID])).GetIndex("0");
                    if ((index != 1) || (index == -1))
                    {
                        if (BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID3, 3) != null)
                        {
                            base.Response.Redirect("FriMatchMessage.aspx?Type=TRAINCENTER");
                            return;
                        }
                        this.rbStatus1.Attributes.Add("onclick", "CheckTrainStatus(this);");
                        this.rbStatus2.Attributes.Add("onclick", "CheckTrainStatus(this);");
                        this.rbStatus3.Attributes.Add("onclick", "CheckTrainStatus(this);");
                        if (this.intPayType == 1)
                        {
                            this.rbStatus4.Attributes.Add("onclick", "CheckTrainStatus(this);");
                        }
                        else
                        {
                            this.rbStatus4.Enabled = false;
                        }
                        this.tbTrainWealth.Attributes.Add("onkeyup", "CheckText(this);");
                        break;
                    }
                    this.rbStatus2.Enabled = false;
                    this.rbStatus3.Enabled = false;
                    this.rbStatus4.Enabled = false;
                    break;

                case "SIFTCENTER":
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球训练</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=FMATCHMSG\"' href='FriMatchMessage.aspx?Type=FMATCHMSG&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_05.GIF' border='0' height='24' width='75'></a>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblTrainCenter.Visible = true;
                    this.TrainCenterRegList();
                    goto Label_0E6E;

                case "SENDTRAIN":
                    this.SendTrain();
                    goto Label_0E6E;

                case "SENDTRAIN5":
                    this.SendTrain5();
                    goto Label_0E6E;

                case "DELTRAINREG":
                    this.DelTrianReg();
                    goto Label_0E6E;

                default:
                    this.tblFMatchMsg.Visible = true;
                    this.sbPageIntro.Append("<a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=FMATCHMSG\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'><img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_04.GIF' border='0' height='24' width='76'></a>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>我的约战</a></li>");
                    this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_05.GIF' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<a href='FriMatchMessage.aspx?Type=ONLINE&Page=1'><img onclick='<script>window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"</script>' src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_06.GIF' border='0' height='24' width='75'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick='javascript:window.location.reload();' title='刷新'><img src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a>");
                    this.FMatchMsgList();
                    goto Label_0E6E;
            }
            this.tblTrainReg.Visible = true;
        Label_0E6E:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        public void SendTrain()
        {
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                int request = (int) SessionItem.GetRequest("ClubID", 0);
                int intClubID = this.intClubID3;
                string strNickName = BTPAccountManager.GetAccountRowByClubID(request)["NickName"].ToString().Trim();
                string strIntro = "";
                if (request == intClubID)
                {
                    base.Response.Redirect("Report.aspx?Parameter=4104");
                }
                else if (BTPPlayer3Manager.GetPlayer3CountByCID(intClubID) < 4)
                {
                    base.Response.Redirect("Report.aspx?Parameter=495");
                }
                else if (BTPClubManager.GetClubIDByNickName(strNickName, 3) == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=497");
                }
                else if (BTPPlayer3Manager.GetPlayer3CountByCID(request) < 4)
                {
                    base.Response.Redirect("Report.aspx?Parameter=496");
                }
                else
                {
                    int num6 = MatchItem.TrainMatchType(intClubID, request, 1);
                    switch (num6)
                    {
                        case 3:
                        case 4:
                            BTPFriMatchManager.DeleteFriMatchRowByCIDB(request);
                            break;
                    }
                    if (num6 == 1)
                    {
                        switch (BTPFriMatchManager.SetTrainMatch(strNickName, this.intUserID, 5, 3, strIntro))
                        {
                            case 1:
                                base.Response.Redirect("Report.aspx?Parameter=40");
                                return;

                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=48");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=491");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=492");
                                return;

                            case 0:
                                base.Response.Redirect("Report.aspx?Parameter=493");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=498");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=47");
                    }
                    else
                    {
                        switch (num6)
                        {
                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=4100");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=4101");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=4102");
                                return;

                            case 5:
                                base.Response.Redirect("Report.aspx?Parameter=4103");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=4106");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                }
            }
        }

        public void SendTrain5()
        {
            if (this.intClubID5 < 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=4112x");
            }
            else
            {
                int request = (int) SessionItem.GetRequest("ClubID", 0);
                string strNickName = BTPAccountManager.GetAccountRowByClubID(request)["NickName"].ToString().Trim();
                if (request == this.intClubID5)
                {
                    base.Response.Redirect("Report.aspx?Parameter=4104");
                }
                else
                {
                    int num2 = MatchItem.TrainMatchType5(this.intClubID5, 0, 2);
                    if (num2 == 1)
                    {
                        switch (BTPFriMatchManager.SetTrainMatch(strNickName, this.intUserID, 11, 5, ""))
                        {
                            case 1:
                                base.Response.Redirect("Report.aspx?Parameter=40a5");
                                return;

                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=48");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=491");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=492");
                                return;

                            case 0:
                                base.Response.Redirect("Report.aspx?Parameter=493");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=498");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=47");
                    }
                    else
                    {
                        switch (num2)
                        {
                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=4112a");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=4112b");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=4112c");
                                return;

                            case 5:
                                base.Response.Redirect("Report.aspx?Parameter=4112d");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=4112e");
                                return;

                            case 7:
                                base.Response.Redirect("Report.aspx?Parameter=4112f");
                                return;

                            case 8:
                                base.Response.Redirect("Report.aspx?Parameter=4112g");
                                return;

                            case 9:
                                base.Response.Redirect("Report.aspx?Parameter=4112h");
                                return;

                            case 10:
                                base.Response.Redirect("Report.aspx?Parameter=4112i");
                                return;

                            case 11:
                                base.Response.Redirect("Report.aspx?Parameter=4112j");
                                break;
                        }
                    }
                }
            }
        }

        private void TrainCenterRegList()
        {
            int num;
            int num2;
            string str;
            string str2;
            string str3;
            string str4 = "";
            this.sbList = new StringBuilder();
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPerPage = 12;
            DataRow trainCenterRowByClubID = BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID3, 3);
            if (trainCenterRowByClubID != null)
            {
                num = (int) trainCenterRowByClubID["ClubID"];
                num2 = (int) trainCenterRowByClubID["Levels"];
                str = trainCenterRowByClubID["ClubName"].ToString().Trim();
                str2 = "<a href='FriMatchMessage.aspx?Type=DELTRAINREG&ClubID=" + num + "'>退&nbsp;&nbsp;&nbsp;&nbsp;出</a>";
                int num1 = (int) trainCenterRowByClubID["RegID"];
                str3 = trainCenterRowByClubID["ShortName"].ToString().Trim();
                trainCenterRowByClubID["ClubLogo"].ToString().Trim();
                if (MatchItem.CanTrainMatchType(num) == 1)
                {
                    str4 = "等待中";
                }
                else
                {
                    str4 = "<a href='FMatchCenter.aspx?Type=TRAINCENTER&Page=1'>比赛中</a>";
                }
                if (str3 != "")
                {
                    str = str3 + "-" + str;
                }
                this.sbList.Append("<tr class='BarContent'style=\" background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                this.sbList.Append("<td align='center' height='25' style='color:green'>[<span style='width:20px;'>" + num2 + "</span>]</td>");
                this.sbList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px'><a href='ShowClub.aspx?ClubID=", num, "&Type=3' title='", str, "' target='Right'>" }));
                this.sbList.Append(StringItem.GetShortString(str, 20, ".") + "</a></td>");
                this.sbList.Append("<td>" + str4 + "</td>");
                this.sbList.Append("<td>" + str2 + "</td>");
                this.sbList.Append("<td></td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
            }
            DataTable table = BTPFriMatchManager.GetTrainCenterTableNew(0, this.intPage, this.intPerPage, this.intUserID, 1, 3);
            if ((table == null) && (trainCenterRowByClubID == null))
            {
                this.sbList.Append("<tr><td height='25' colspan='5' align='center'>暂时没有等待中的球队！</td></tr>");
            }
            else if (table != null)
            {
                foreach (DataRow row2 in table.Rows)
                {
                    num = (int) row2["ClubID"];
                    num2 = (int) row2["Levels"];
                    str = row2["ClubName"].ToString().Trim();
                    str2 = "<a href='FriMatchMessage.aspx?Type=SENDTRAIN&ClubID=" + num + "'>开始比赛</a>";
                    int num5 = (int) row2["RegID"];
                    str3 = row2["ShortName"].ToString().Trim();
                    row2["ClubLogo"].ToString().Trim();
                    Convert.ToInt32(row2["Status"]);
                    if (((int) row2["RunCount"]) == 0)
                    {
                        str4 = "等待中";
                        str2 = "<a href='FriMatchMessage.aspx?Type=SENDTRAIN&ClubID=" + num + "'>开始比赛</a>";
                    }
                    else
                    {
                        str4 = "比赛中";
                        str2 = "-- --";
                    }
                    if (str3 != "")
                    {
                        str = str3 + "-" + str;
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align='center' height='25' style='color:green'>[<span style='width:20px;'>" + num2 + "</span>]</td>");
                    this.sbList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px'><a href='ShowClub.aspx?ClubID=", num, "&Type=3' title='", str3, "' target='Right'>" }));
                    this.sbList.Append(StringItem.GetShortString(str, 20, ".") + "</a></td>");
                    this.sbList.Append("<td>" + str4 + "</td>");
                    this.sbList.Append("<td>" + str2 + "</td>");
                    this.sbList.Append("<td></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
                this.sbList.Append("<tr><td colspan=5 align='right'>" + this.GetMsgViewPage2("FriMatchMessage.aspx?Type=TRAINCENTER&") + "</td></tr>");
                this.GetMsgScript("FriMatchMessage.aspx?Type=TRAINCENTER&");
            }
        }

        private void TrainCenterRegList5()
        {
            int num;
            int num2;
            string str;
            string str2;
            string str3;
            string str4 = "";
            this.sbList = new StringBuilder();
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPerPage = 12;
            DataRow trainCenterRowByClubID = BTPFriMatchManager.GetTrainCenterRowByClubID(this.intClubID5, 5);
            if (trainCenterRowByClubID != null)
            {
                num = (int) trainCenterRowByClubID["ClubID"];
                num2 = (int) trainCenterRowByClubID["Levels"];
                str = trainCenterRowByClubID["ClubName"].ToString().Trim();
                str2 = "<a href='FriMatchMessage.aspx?Type=DELTRAINREG&MType=5&ClubID=" + num + "'>退&nbsp;&nbsp;&nbsp;&nbsp;出</a>";
                int num1 = (int) trainCenterRowByClubID["RegID"];
                str3 = trainCenterRowByClubID["ShortName"].ToString().Trim();
                trainCenterRowByClubID["ClubLogo"].ToString().Trim();
                int num3 = (int) trainCenterRowByClubID["RunCount"];
                if (num3 < 1)
                {
                    str4 = "等待中";
                }
                else
                {
                    str4 = "<a href='FMatchCenter.aspx?Type=TRAINCENTER5&Page=1'>比赛中</a>";
                }
                if (str3 != "")
                {
                    str = str3 + "-" + str;
                }
                this.sbList.Append("<tr class='BarContent'style=\" background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                this.sbList.Append("<td align='center' height='25' style='color:green'>[<span style='width:20px;'>" + num2 + "</span>]</td>");
                this.sbList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px'><a href='ShowClub.aspx?ClubID=", num, "&Type=5' title='", str, "' target='Right'>" }));
                this.sbList.Append(StringItem.GetShortString(str, 20, ".") + "</a></td>");
                this.sbList.Append("<td>" + str4 + "</td>");
                this.sbList.Append("<td>" + str2 + "</td>");
                this.sbList.Append("<td></td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
            }
            DataTable table = BTPFriMatchManager.GetTrainCenterTableNew(0, this.intPage, this.intPerPage, this.intUserID, 1, 5);
            if ((table == null) && (trainCenterRowByClubID == null))
            {
                this.sbList.Append("<tr><td height='25' colspan='5' align='center'>暂时没有等待中的球队！</td></tr>");
            }
            else if (table != null)
            {
                foreach (DataRow row2 in table.Rows)
                {
                    num = (int) row2["ClubID"];
                    num2 = (int) row2["Levels"];
                    str = row2["ClubName"].ToString().Trim();
                    str2 = "<a href='FriMatchMessage.aspx?Type=SENDTRAIN5&ClubID=" + num + "'>开始比赛</a>";
                    int num5 = (int) row2["RegID"];
                    str3 = row2["ShortName"].ToString().Trim();
                    row2["ClubLogo"].ToString().Trim();
                    Convert.ToInt32(row2["Status"]);
                    if (((int) row2["RunCount"]) == 0)
                    {
                        str4 = "等待中";
                        str2 = "<a href='FriMatchMessage.aspx?Type=SENDTRAIN5&ClubID=" + num + "'>开始比赛</a>";
                    }
                    else
                    {
                        str4 = "比赛中";
                        str2 = "-- --";
                    }
                    if (str3 != "")
                    {
                        str = str3 + "-" + str;
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align='center' height='25' style='color:green'>[<span style='width:20px;'>" + num2 + "</span>]</td>");
                    this.sbList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px'><a href='ShowClub.aspx?ClubID=", num, "&Type=5' title='", str3, "' target='Right'>" }));
                    this.sbList.Append(StringItem.GetShortString(str, 20, ".") + "</a></td>");
                    this.sbList.Append("<td>" + str4 + "</td>");
                    this.sbList.Append("<td>" + str2 + "</td>");
                    this.sbList.Append("<td></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
                this.sbList.Append("<tr><td colspan=5 align='right'>" + this.GetMsgViewPage2("FriMatchMessage.aspx?Type=TRAINCENTER5&") + "</td></tr>");
                this.GetMsgScript("FriMatchMessage.aspx?Type=TRAINCENTER5&");
            }
        }
    }
}


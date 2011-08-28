namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Temp_Right : Page
    {
        protected ImageButton btnDelMaster;
        protected ImageButton btnOK;
        protected ImageButton btnSend;
        protected ImageButton btnSendB;
        protected ImageButton btnSendMsg;
        protected ImageButton btnSetReputation;
        protected CheckBox cbNoName;
        public int intChampionB = 0;
        public int intTopReputation = 0;
        private int intUnionCategory;
        private int intUnionID;
        private int intUnionIDB;
        private int intUserID;
        private int intUserIDB;
        public string strDelMaster = "";
        public string strMsg = "";
        public string strNickName = "";
        public string strReputation = "";
        public string strSendUnion = "";
        protected HtmlTable tbAutoTrain;
        protected HtmlTable tbChooseClub;
        protected TextBox tbContent;
        protected HtmlTable tblChampionCup;
        protected HtmlTable tblCupList;
        protected HtmlTable tblDelMaster;
        protected HtmlTable tblDevsion;
        protected HtmlTable tblExchange;
        protected HtmlTable tblFmatchMsg;
        protected HtmlTable tblGuess;
        protected HtmlTable tblHonour;
        protected HtmlTable tblMessageCenter;
        protected HtmlTable tblModifyClub;
        protected HtmlTable tblPlayerCenter;
        protected HtmlTable tblReputation;
        protected HtmlTable tblSendUnionWealth;
        protected HtmlTable tblTools;
        protected HtmlTable tblTrainPlayerCenter;
        protected HtmlTable tblTransferMarket;
        protected HtmlTable tblUnion;
        protected HtmlTable tblUnionCup;
        protected HtmlTable tblUnionMsgSend;
        protected HtmlTable tblWealthMarket;
        protected HtmlTable tblXBACup;
        protected HtmlTable tbMatchLev;
        protected TextBox tbNickName;
        protected TextBox tbNickNameCup;
        protected TextBox tbReason;
        protected TextBox tbReputation;
        protected TextBox tbSendWealth;
        protected HtmlTable tbStreenLev;
        protected TextBox tbUnionWealth;
        protected TextBox tbWealth;

        private void btnDelMaster_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbReason.Text.Trim();
            if (StringItem.IsValidName(strIn, 1, 250))
            {
                switch (BTPUnionPolity.DelateMaster(this.intUserID, strIn))
                {
                    case 1:
                        this.strMsg = "发起弹劾成功！弹劾将在72小时后结束。";
                        return;

                    case -1:
                        this.strMsg = "<font color=red>您没有联盟或已经被盟主标记踢出</font>";
                        return;

                    case -2:
                        this.strMsg = "<font color=red>您加入联盟的时间小于7天不能发起盟主弹劾</font>";
                        return;

                    case -3:
                        this.strMsg = "<font color=red>您没有足够的游戏币</font>";
                        return;

                    case -4:
                        this.strMsg = "<font color=red>对不起，当前联盟正处在弹劾中，暂时不能发起弹劾！</font>";
                        return;

                    case -5:
                        this.strMsg = "<font color=red>您已经是盟主了</font>";
                        return;

                    case -6:
                        this.strMsg = "<font color=red>对不起，距上次弹劾结束不到5天，暂时不能发起新一轮弹劾</font>";
                        return;
                }
                this.strMsg = "<font color=red>系统错误请重试！</font>";
            }
            else
            {
                this.strMsg = "<font color=red>请输入正确的弹劾原因</font>";
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.tbWealth.Text.ToString().Trim();
            int intWealth = 0;
            if (StringItem.IsNumber(str))
            {
                intWealth = Convert.ToInt32(str);
            }
            else
            {
                this.strMsg = "只能输入半角数字。";
                return;
            }
            if (intWealth < 1)
            {
                this.strMsg = "最少兑换1分王者积分。";
            }
            else if (intWealth >= 0x186a0)
            {
                this.strMsg = "您输入的王者积分数量过大，请重新输入。";
            }
            else
            {
                switch (BTPAccountManager.ExchangeOnlyPoint(this.intUserID, intWealth))
                {
                    case 1:
                        this.strMsg = "成功兑换到" + intWealth + "游戏币，请查收。";
                        return;

                    case -1:
                        this.strMsg = "您的王者积分不足，无法进行游戏币兑换。";
                        return;
                }
                this.strMsg = "不能输入负数，请重新输入。";
            }
        }

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.tbUnionWealth.Text.Trim();
            if (StringItem.IsNumber(str))
            {
                int intWealth = Convert.ToInt32(str);
                string strRemark = "联盟奖励给经理：" + this.tbNickName.Text.Trim();
                switch (BTPUnionManager.SetUWealthByID(this.intUserIDB, this.intUnionID, 2, 2, 1, intWealth, strRemark))
                {
                    case 1:
                        this.strMsg = "奖励成功，请注意查收！";
                        return;

                    case 2:
                        this.strMsg = "联盟没有足够的游戏币进行奖励！";
                        return;
                }
                this.strMsg = "奖励数额需在（10－999999）之间！";
            }
            else
            {
                this.strMsg = "请输入正确的奖励数额！";
            }
        }

        private void btnSendB_Click(object sender, ImageClickEventArgs e)
        {
            switch (BTPXGroupMatchManager.SendChampionCupB(this.intUserID, this.intUserIDB))
            {
                case 1:
                    this.strMsg = "<font color=red>发放成功！</font>";
                    break;

                case -3:
                    this.strMsg = "<font color=red>对不起，联盟内的B级邀请函已发送完毕！</font>";
                    break;

                case -4:
                    this.strMsg = "<font color=red>对不起，对方已经有本届冠军杯的邀请函了！</font>";
                    break;

                case -1:
                    this.strMsg = "<font color=red>对不起，此经理不是您联盟成员！</font>";
                    break;

                case -2:
                    this.strMsg = "<font color=red>对不起，您不是联盟盟主！</font>";
                    break;

                case -5:
                    this.strMsg = "<font color=red>对不起，此经理没有职业队！</font>";
                    break;
            }
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
            if (unionRowByID != null)
            {
                this.intChampionB = (int) unionRowByID["XBACupCount"];
            }
        }

        private void btnSendMsg_Click(object sender, ImageClickEventArgs e)
        {
            int request = (int) SessionItem.GetRequest("UID", 0);
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(request);
            if (unionRowByID != null)
            {
                bool blnNoName = this.cbNoName.Checked;
                string str = unionRowByID["Name"].ToString().Trim();
                string strIn = this.tbContent.Text.Trim();
                if (AccountItem.IsBadUser(this.intUserID, 1))
                {
                    this.strMsg = "您无此权限，不能进行短信群发！";
                }
                else if (StringItem.IsValidName(strIn, 2, 500))
                {
                    switch (BTPUnionManager.SendMsgReadByUserID(this.intUserID, request, strIn, blnNoName))
                    {
                        case 1:
                            this.strMsg = "发送成功，" + str + "所有盟员将在1分钟内收到该信息！";
                            this.tbContent.Text = "";
                            this.tbSendWealth.Text = BTPUnionManager.SendWealthByUserID(this.intUserID, request).ToString();
                            return;

                        case -1:
                            this.strMsg = "您要群发短信的联盟不存在，无法群发消息！";
                            return;

                        case -2:
                            this.strMsg = "您的游戏币不足！";
                            return;
                    }
                    this.strMsg = "发送时出现错误，请重试！";
                }
                else
                {
                    this.strMsg = "您输入的短信内容有误，请重新输入！";
                }
            }
            else
            {
                this.strMsg = "无此联盟，请重新输入！";
            }
        }

        private void btnSetReputation_Click(object sender, ImageClickEventArgs e)
        {
            int request = (int) SessionItem.GetRequest("Tag", 0);
            string str = this.tbReputation.Text.Trim();
            if (StringItem.IsNumber(str))
            {
                int intReputation = Convert.ToInt32(str);
                if ((intReputation < 1) || (intReputation > this.intTopReputation))
                {
                    this.strMsg = "可支配的联盟威望最小为1最高为" + this.intTopReputation;
                    this.tbReputation.Text = "";
                }
                else
                {
                    switch (BTPUnionFieldManager.SetUReputation(this.intUserID, intReputation, request))
                    {
                        case 1:
                            this.strMsg = "操作成功，此经理的可支配威望为" + intReputation;
                            return;

                        case -1:
                            this.strMsg = "您不是联盟的盟主无权进行此操作！";
                            return;

                        case -2:
                            this.strMsg = "此经理不是您联盟的盟员！";
                            return;

                        case -3:
                            this.strMsg = "可支配的联盟威望最小为1最高为" + this.intTopReputation;
                            this.tbReputation.Text = "";
                            return;

                        case -9:
                            this.strMsg = "您的联盟将要解散不能进行此设置！";
                            return;
                    }
                    this.strMsg = "系统错误请重试！";
                }
            }
            else
            {
                this.strMsg = "可支配的联盟威望最小为1最高为" + this.intTopReputation;
                this.tbReputation.Text = "";
            }
        }

        private void DelateMaster()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                this.intUnionID = (int) accountRowByUserID["UnionID"];
                if (this.intUnionID < 1)
                {
                    this.btnDelMaster.Visible = false;
                    this.strMsg = "<font color=red>您没有联盟不能使用此功能</font>";
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.btnOK.Attributes["onclick"] = base.GetPostBackEventReference(this.btnOK) + ";this.disabled=true;";
            this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
            this.btnSend.Attributes["onclick"] = base.GetPostBackEventReference(this.btnSend) + ";this.disabled=true;";
            this.btnSendMsg.Click += new ImageClickEventHandler(this.btnSendMsg_Click);
            this.btnSendMsg.Attributes["onclick"] = base.GetPostBackEventReference(this.btnSendMsg) + ";this.disabled=true;";
            this.btnSendB.Click += new ImageClickEventHandler(this.btnSendB_Click);
            this.btnSendB.Attributes["onclick"] = base.GetPostBackEventReference(this.btnSendB) + ";this.disabled=true;";
            this.btnSetReputation.Click += new ImageClickEventHandler(this.btnSetReputation_Click);
            this.btnDelMaster.Click += new ImageClickEventHandler(this.btnDelMaster_Click);
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
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.tblDevsion.Visible = false;
            this.tblHonour.Visible = false;
            this.tblPlayerCenter.Visible = false;
            this.tblTools.Visible = false;
            this.tblTrainPlayerCenter.Visible = false;
            this.tblTransferMarket.Visible = false;
            this.tblUnion.Visible = false;
            this.tblUnionCup.Visible = false;
            this.tblXBACup.Visible = false;
            this.tblCupList.Visible = false;
            this.tblModifyClub.Visible = false;
            this.tblMessageCenter.Visible = false;
            this.tblFmatchMsg.Visible = false;
            this.tbChooseClub.Visible = false;
            this.tbMatchLev.Visible = false;
            this.tbStreenLev.Visible = false;
            this.tblWealthMarket.Visible = false;
            this.tblGuess.Visible = false;
            this.tbAutoTrain.Visible = false;
            this.tblExchange.Visible = false;
            this.tblSendUnionWealth.Visible = false;
            this.tblUnionMsgSend.Visible = false;
            this.tblChampionCup.Visible = false;
            this.tblReputation.Visible = false;
            this.tblDelMaster.Visible = false;

            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnSendMsg.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.btnSetReputation.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";

            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "AUTOTRAIN":
                    this.tbAutoTrain.Visible = true;
                    return;

                case "GUESS":
                    this.tblGuess.Visible = true;
                    return;

                case "WHEALTHMARKET":
                    this.tblWealthMarket.Visible = true;
                    return;

                case "MATCHLEV":
                    this.tbMatchLev.Visible = true;
                    return;

                case "STREENLEV":
                    this.tbStreenLev.Visible = true;
                    return;

                case "CHOOSECLUB":
                    this.tbChooseClub.Visible = true;
                    return;

                case "UNIONCUP":
                    this.tblUnionCup.Visible = true;
                    return;

                case "XBACUP":
                    this.tblXBACup.Visible = true;
                    return;

                case "PLAYERCENTER":
                    this.tblPlayerCenter.Visible = true;
                    return;

                case "TRAINPLAYERCENTER":
                    this.tblTrainPlayerCenter.Visible = true;
                    return;

                case "TRANSFERMAEKET":
                    this.tblTransferMarket.Visible = true;
                    return;

                case "HONOUR":
                    this.tblHonour.Visible = true;
                    return;

                case "UNION":
                    this.tblUnion.Visible = true;
                    return;

                case "CUPLIST":
                    this.tblCupList.Visible = true;
                    return;

                case "DEVISION":
                    this.tblDevsion.Visible = true;
                    return;

                case "TOOLS":
                    this.tblTools.Visible = true;
                    return;

                case "MODIFYCLUB":
                    this.tblModifyClub.Visible = true;
                    return;

                case "MESSAGCENTER":
                    this.tblMessageCenter.Visible = true;
                    return;

                case "FMATCHMSG":
                    this.tblFmatchMsg.Visible = true;
                    return;

                case "FMATCH":
                    this.tblFmatchMsg.Visible = true;
                    return;

                case "EXCHANGE":
                    this.tblExchange.Visible = true;
                    return;

                case "SENDWEALTH":
                {
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    if (accountRowByUserID != null)
                    {
                        this.intUnionCategory = (byte) accountRowByUserID["UnionCategory"];
                        this.intUnionID = (int) accountRowByUserID["UnionID"];
                        this.intUserIDB = (int) SessionItem.GetRequest("UserID", 0);
                        accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserIDB);
                        if (accountRowByUserID == null)
                        {
                            this.strMsg = "<font color=red>查无此经理！</font>";
                            this.btnSend.Visible = false;
                            return;
                        }
                        this.intUnionIDB = (int) accountRowByUserID["UnionID"];
                        this.tbNickName.Text = accountRowByUserID["NickName"].ToString().Trim();
                        this.tbNickName.Enabled = false;
                        if ((this.intUnionIDB == this.intUnionID) && (this.intUnionCategory == 1))
                        {
                            this.tblSendUnionWealth.Visible = true;
                            return;
                        }
                        this.strMsg = "<font color=red>您没有奖励此经理的权限！</font>";
                        this.btnSend.Visible = false;
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=12");
                    return;
                }
                case "UNIONMSGSEND":
                    this.tblUnionMsgSend.Visible = true;
                    this.SetUnionMsg();
                    return;

                case "CHAMPIONCUP":
                    this.tblChampionCup.Visible = true;
                    this.tbNickNameCup.Enabled = false;
                    this.SendChampionCupB();
                    return;

                case "SETUREPUTATION":
                {
                    this.tblReputation.Visible = true;
                    DataRow row2 = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    if (row2 != null)
                    {
                        this.intUnionID = (int) row2["UnionID"];
                        this.SetUReputation();
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=12");
                    return;
                }
                case "DELATEMASTER":
                    this.tblDelMaster.Visible = true;
                    this.DelateMaster();
                    return;
            }
            this.tblPlayerCenter.Visible = true;
        }

        private void SendChampionCupB()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intUnionCategory = (byte) accountRowByUserID["UnionCategory"];
                this.intUnionID = (int) accountRowByUserID["UnionID"];
                if (this.intUnionCategory != 1)
                {
                    this.strMsg = "<font color=red>对不起您不是联盟盟主不能发放邀请！</font>";
                    this.btnSend.Visible = false;
                }
                else
                {
                    this.intUserIDB = (int) SessionItem.GetRequest("UserID", 0);
                    accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserIDB);
                    if (accountRowByUserID == null)
                    {
                        this.strMsg = "<font color=red>查无此经理！</font>";
                        this.btnSend.Visible = false;
                    }
                    else
                    {
                        this.intUnionIDB = (int) accountRowByUserID["UnionID"];
                        this.tbNickNameCup.Text = accountRowByUserID["NickName"].ToString().Trim();
                        if (this.intUnionIDB != this.intUnionID)
                        {
                            this.strMsg = "<font color=red>此经理不在您的联盟中！</font>";
                            this.btnSend.Visible = false;
                        }
                        else
                        {
                            accountRowByUserID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
                            if (accountRowByUserID != null)
                            {
                                this.intChampionB = (int) accountRowByUserID["XBACupCount"];
                            }
                        }
                    }
                }
            }
        }

        private void SetUnionMsg()
        {
            int request = (int) SessionItem.GetRequest("UID", 0);
            if (request < 1)
            {
                request = this.intUnionID;
            }
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(request);
            if (unionRowByID != null)
            {
                string str = unionRowByID["Name"].ToString().Trim();
                int num2 = (int) unionRowByID["Creater"];
                this.tbSendWealth.Text = BTPUnionManager.SendWealthByUserID(this.intUserID, request).ToString();
                this.tbSendWealth.Enabled = false;
                unionRowByID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                if (unionRowByID != null)
                {
                    this.intUnionID = (int) unionRowByID["UnionID"];
                }
                if (request != this.intUnionID)
                {
                    this.strSendUnion = "向 " + str + " 联盟群发短信";
                }
                else
                {
                    this.strSendUnion = "向本盟全体盟员发送短信（盟主每24小时可免费发送一次）";
                    if (num2 != this.intUserID)
                    {
                        this.strDelMaster = "<a href='Temp_Right.aspx?Type=DELATEMASTER'>发起盟主弹劾</a>";
                    }
                }
            }
            else
            {
                this.strSendUnion = "无此联盟，请重试！";
                this.btnSendMsg.Visible = false;
            }
        }

        private void SetUReputation()
        {
            int request = (int) SessionItem.GetRequest("Tag", 0);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(request);
            if (accountRowByUserID != null)
            {
                this.strNickName = accountRowByUserID["NickName"].ToString().Trim();
                this.strReputation = accountRowByUserID["UnionReputation"].ToString().Trim();
                accountRowByUserID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
                if (accountRowByUserID != null)
                {
                    int topRInField = BTPUnionFieldManager.GetTopRInField(this.intUnionID);
                    int num3 = (int) accountRowByUserID["Reputation"];
                    this.intTopReputation = num3 / 10;
                    if (this.intTopReputation < 1)
                    {
                        this.intTopReputation = 1;
                    }
                    if (topRInField > this.intTopReputation)
                    {
                        this.intTopReputation = topRInField;
                    }
                    if (num3 < 1)
                    {
                        this.btnSetReputation.Visible = false;
                        this.strMsg = "您的联盟威望小于1即将被解散，不能进行此操作！";
                    }
                }
            }
            else
            {
                this.strNickName = "未找到此经理";
                this.strReputation = "0";
                this.btnSetReputation.Visible = false;
            }
        }
    }
}


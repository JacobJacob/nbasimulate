namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class AutoPlayerTrain : Page
    {
        protected ImageButton btnCancel;
        protected ImageButton btnOK;
        private DateTime dtAutoTrainTime;
        private int intAutoTrain;
        private int intAutoTrainDev;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        public int intCoin;
        public int intPayCoin;
        private int intPercent;
        private int intTurn;
        private int intUserID;
        public StringBuilder sbDevList = new StringBuilder("");
        public StringBuilder sbList = new StringBuilder("");
        public string strBtnCancel = "";
        public string strConMsg = "";
        public string strNickName;

        private void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            BTPPlayer3Manager.AddPlayerExpByUserID(this.intUserID, 0, 0);
            BTPPlayer5Manager.AddPlayer5ExpByUserID(this.intUserID, 0, 0);
            base.Response.Redirect("Report.aspx?Parameter=907");
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (this.intCoin < this.intPayCoin)
            {
                int num;
                try
                {
                    num = ROOTUserManager.SpendCoin40Return(this.intUserID, this.intCoin, "使用离线训练", "");
                }
                catch
                {
                    this.Session.Add("AutoType", 1);
                    base.Response.Redirect("Report.aspx?Parameter=905");
                    return;
                }
                if (num == 1)
                {
                    BTPPlayer3Manager.AddPlayerExpByUserID(this.intUserID, this.intPercent, this.intCoin);
                    BTPPlayer5Manager.AddPlayer5ExpByUserID(this.intUserID, this.intPercent, this.intCoin);
                    base.Response.Redirect("Report.aspx?Parameter=906");
                }
                else
                {
                    this.Session.Add("AutoType", 1);
                    base.Response.Redirect("Report.aspx?Parameter=905");
                }
            }
            else
            {
                int num2;
                try
                {
                    num2 = ROOTUserManager.SpendCoin40Return(this.intUserID, this.intPayCoin, "使用离线训练", "");
                }
                catch
                {
                    this.Session.Add("AutoType", 1);
                    base.Response.Redirect("Report.aspx?Parameter=905");
                    return;
                }
                if (num2 != 1)
                {
                    this.Session.Add("AutoType", 1);
                    base.Response.Redirect("Report.aspx?Parameter=905");
                }
                else
                {
                    BTPPlayer3Manager.AddPlayerExpByUserID(this.intUserID, 100, this.intPayCoin);
                    BTPPlayer5Manager.AddPlayer5ExpByUserID(this.intUserID, 100, this.intPayCoin);
                    base.Response.Redirect("Report.aspx?Parameter=906");
                }
            }
        }

        private void GetPlayerList()
        {
            if (this.intAutoTrain > 0)
            {
                this.sbList.Append("<p><font color=\"green\">街球队</font></p>");
                this.sbList.Append("<table width=\"300\" border=\"1\" cellpadding=\"3\" cellspacing=\"0\" bordercolor=\"#e8cab8\" style=\"BORDER-COLLAPSE:collapse\">");
                foreach (DataRow row in BTPPlayer3Manager.GetArrage3PlayerList(this.intClubID3).Rows)
                {
                    string str = row["Name"].ToString().Trim();
                    string str2 = row["TrainPointAuto"].ToString().Trim();
                    this.sbList.Append("<tr><td width=\"121\">" + str + "</td><td width=\"179\">" + str2 + "</td></tr>");
                }
                this.sbList.Append("</table>");
            }
            if (this.intAutoTrainDev > 0)
            {
                if (this.intAutoTrain > 0)
                {
                    this.sbDevList.Append("<p></p>");
                }
                this.sbDevList.Append("<p><font color=\"red\">职业队</font></p>");
                this.sbDevList.Append("<table width=\"300\" border=\"1\" cellpadding=\"3\" cellspacing=\"0\" bordercolor=\"#e8cab8\" style=\"BORDER-COLLAPSE:collapse\">");
                DataTable autoTrainPlayerList = BTPPlayer5Manager.GetAutoTrainPlayerList(this.intClubID5);
                if (autoTrainPlayerList != null)
                {
                    foreach (DataRow row2 in autoTrainPlayerList.Rows)
                    {
                        string str3 = row2["Name"].ToString().Trim();
                        string str4 = row2["TrainPointAuto"].ToString().Trim();
                        this.sbDevList.Append("<tr><td width=\"121\">" + str3 + "</td><td width=\"179\">" + str4 + "</td></tr>");
                    }
                }
                this.sbDevList.Append("</table>");
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.btnCancel.Click += new ImageClickEventHandler(this.btnCancel_Click);
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
                int num = 0;
                if (this.Session["AutoType"] != null)
                {
                    num = (int) this.Session["AutoType"];
                }
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                if (num == 1)
                {
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_ag.gif";
                    this.btnOK.Width = 70;
                    this.btnCancel.ImageUrl = SessionItem.GetImageURL() + "fogive.gif";
                    this.btnCancel.Visible = true;
                }
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.intCategory = Convert.ToInt32(accountRowByUserID["Category"]);
                this.intClubID3 = (int) accountRowByUserID["ClubID3"];
                this.intClubID5 = (int) accountRowByUserID["ClubID5"];
                this.intAutoTrain = (int) accountRowByUserID["AutoTrain"];
                this.intAutoTrainDev = (int) accountRowByUserID["AutoTrainDev"];
                this.dtAutoTrainTime = (DateTime) accountRowByUserID["AutoTrainTime"];
                this.strNickName = accountRowByUserID["NickName"].ToString().Trim();
                if ((this.intAutoTrain == 0) && (this.intAutoTrainDev == 0))
                {
                    base.Response.Redirect("Main.aspx");
                }
                if (this.intAutoTrain > 0)
                {
                    this.intTurn = BTPPlayer3Manager.AddPlayerAutoExpByUserID(this.intUserID);
                }
                else
                {
                    this.intTurn = 0;
                }
                this.intPayCoin = this.intTurn * 0x12;
                this.intPayCoin = (this.intPayCoin / 10) + (((this.intPayCoin % 10) + 9) / 10);
                int num2 = 0;
                if (this.intAutoTrainDev > 0)
                {
                    num2 = BTPPlayer5Manager.AddPlayer5AutoExpByUserID(this.intUserID);
                }
                int num3 = num2;
                this.intPayCoin += num3;
                try
                {
                    DataRow row2 = ROOTUserManager.GetUserInfoByID40(this.intUserID);
                    if (row2 != null)
                    {
                        this.intCoin = Convert.ToInt32(row2["Coin"]);
                        if (this.intCoin >= this.intPayCoin)
                        {
                            this.strConMsg = "您拥有 " + this.intCoin + " 金币！";
                        }
                        else
                        {
                            this.intPercent = (this.intCoin * 100) / this.intPayCoin;
                            this.btnOK.Attributes["onclick"] = "return CanOK(" + this.intPercent + ");";
                            this.strConMsg = string.Concat(new object[] { "您拥有 ", this.intCoin, " 金币，数额不足，只能获得 ", this.intPercent, "% 的训练点数！请<a style='color:red;cursor:pointer' onclick='javascript:window.location=\"", StringItem.GetBuyCoinURL(), "\"'>立即充值</a>" });
                        }
                    }
                    else
                    {
                        this.strConMsg = "您拥有的金币数量未知，请尝试领取训练点！";
                    }
                }
                catch
                {
                    this.strConMsg = "您拥有的金币数量未知，请尝试领取训练点！";
                }
                this.GetPlayerList();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


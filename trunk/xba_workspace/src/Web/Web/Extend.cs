namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Extend : Page
    {
        private bool blnContractType;
        protected ImageButton btnCancel;
        protected ImageButton btnOK;
        public int intSalary;
        public int intSalary1;
        public int intSalary2;
        public int intSalary3;
        public int intSalary4;
        private int intUserID;
        public int intVIPContract;
        public int intWealth;
        private long longPlayerID;
        protected RadioButton rb1;
        protected RadioButton rb2;
        protected RadioButton rb3;
        protected RadioButton rb4;
        public string strSay;
        protected HtmlTableRow VIPContract;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int num = 1;
            if (this.rb1.Checked)
            {
                num = 1;
            }
            else if (this.rb2.Checked)
            {
                num = 2;
            }
            else if (this.rb3.Checked)
            {
                num = 3;
            }
            else if (this.rb4.Checked)
            {
                num = 4;
            }
            base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=PLAYERCONTRACT&PID=", this.longPlayerID, "&Status=", num }));
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.longPlayerID = (long) SessionItem.GetRequest("PlayerID", 3);
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                int intClubId = BTPClubManager.GetClubIDByUserID(this.intUserID);
                string str = playerRowByPlayerID["Name"].ToString().Trim();
                int num = (int) playerRowByPlayerID["PV"];
                int intFromClubID = Convert.ToInt32(playerRowByPlayerID["FromClubID"]);
                int intPlayedYear = Convert.ToInt32(playerRowByPlayerID["PlayedYear"]);
                int num2 = (byte) playerRowByPlayerID["Contract"];
                int age = Convert.ToInt32(playerRowByPlayerID["Age"]);
                this.blnContractType = (bool) playerRowByPlayerID["ContractType"];
                if (num2 > 10)
                {
                    base.Response.Redirect("Report.aspx?Parameter=494");
                }
                else
                {
                    this.intSalary = Convert.ToInt32((long) ((num * (80L + (30L - (this.longPlayerID % 30L)))) / 100L));
                    this.intSalary = (this.intSalary * 80) / 100;
                    if (this.intSalary < 500)
                    {
                        this.intSalary = 500;
                    }
                    else if (this.intSalary > 0xc350)
                    {
                        this.intSalary = 0xc350;
                    }

                    if (intFromClubID == intClubId && intPlayedYear <= 1 && this.intSalary > 20000)
                    {
                        this.intSalary = 20000;
                    }

                    if (age >= 32 && this.intSalary > 20000)
                    {
                        this.intSalary = 20000;
                    }

                    playerRowByPlayerID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    if (playerRowByPlayerID != null)
                    {
                        this.intWealth = (int) playerRowByPlayerID["Wealth"];
                        this.intVIPContract = (int) BTPParameterManager.GetParameterRow()["VIPContract"];
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=12");
                        return;
                    }
                    this.strSay = "请选择要与球员" + str + "续约的时间。";
                    this.intSalary1 = this.intSalary;
                    this.intSalary2 = (this.intSalary * 0x67) / 100;
                    this.intSalary3 = (this.intSalary * 0x6a) / 100;
                    this.intSalary4 = (this.intSalary1 * 80) / 100;
                }
            }
        }
    }
}


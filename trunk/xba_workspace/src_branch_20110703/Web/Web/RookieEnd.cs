namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieEnd : Page
    {
        protected ImageButton btnNext;
        public int intAge;
        public int intCategory;
        public int intUserID;
        public string strCardImg;
        public string strGender;
        public string strNickName;
        public string strNow;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int intMoney = (int) Global.drParameter["RegPMatchMoney"];
            if (this.intCategory == 2)
            {
                if (BTPAccountManager.HasEnoughMoney(this.intUserID, intMoney))
                {
                    switch (BTPDevManager.SetFinanlDevByUserID(this.intUserID))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=101");
                            return;

                        case 1:
                        {
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 5);
                            int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 5);
                            if (intRookieOpIndex != 5)
                            {
                                base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex).ToString().Trim() + "\";</script>");
                            }
                            else
                            {
                                base.Response.Write("<script>window.top.location=\"Main.aspx\";</script>");
                            }
                            base.Response.Redirect("Report.aspx?Parameter=102");
                            return;
                        }
                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=103");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=106");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=101");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
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
                DateTime time;
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                bool flag = (bool) onlineRowByUserID["Sex"];
                this.intCategory = (int) onlineRowByUserID["Category"];
                if (flag)
                {
                    this.strGender = "女";
                }
                else
                {
                    this.strGender = "男";
                }
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                try
                {
                    time = Convert.ToDateTime(accountRowByUserID["Birth"]);
                }
                catch
                {
                    time = DateTime.Now.AddYears(-20);
                }
                this.intAge = DateTime.Now.Year - time.Year;
                this.strNow = StringItem.FormatDate(DateTime.Now, "yyyy-MM-dd");
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex == -1)
                {
                    base.Response.Write("<script>window.top.location=\"Main.aspx\";</script>");
                }
                else if (intRookieOpIndex != 5)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex).ToString().Trim() + "\";</script>");
                }
                else
                {
                    if (ServerParameter.strCopartner == "17173")
                    {
                        this.strCardImg = "x4173.gif";
                    }
                    else
                    {
                        this.strCardImg = "New/x4.gif";
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


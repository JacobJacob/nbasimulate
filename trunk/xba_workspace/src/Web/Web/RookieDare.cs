namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieDare : Page
    {
        protected ImageButton btnNext;
        public int intUserID;
        public string strClubLogo;
        public string strClubName;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 4);
            if (intRookieOpIndex != 4)
            {
                base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex).ToString().Trim() + "\";</script>");
            }
            else
            {
                base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(5).ToString().Trim() + "\";</script>");
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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strClubLogo = onlineRowByUserID["ClubLogo"].ToString().Trim();
                this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex != 4)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex).ToString().Trim() + "\";</script>");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


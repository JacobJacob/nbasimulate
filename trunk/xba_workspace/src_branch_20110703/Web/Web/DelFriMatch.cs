namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class DelFriMatch : Page
    {
        private int intFMatchID;
        private int intType;
        private int intUserID;

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
            }
            else
            {
                this.intFMatchID = (int) SessionItem.GetRequest("FMatchID", 0);
                this.intType = (short) SessionItem.GetRequest("Type", 2);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (BTPFriMatchManager.CancelFriMatch(this.intUserID, this.intFMatchID, this.intType))
            {
                base.Response.Redirect("Report.aspx?Parameter=41");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=44");
            }
        }
    }
}


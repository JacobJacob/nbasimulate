namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class FriendDel : Page
    {
        private int intFriendID;
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
                this.intFriendID = SessionItem.GetRequest("UserID", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (BTPFriendManager.DeleteFriend(this.intUserID, this.intFriendID))
            {
                base.Response.Redirect("Report.aspx?Parameter=31");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=32");
            }
        }
    }
}


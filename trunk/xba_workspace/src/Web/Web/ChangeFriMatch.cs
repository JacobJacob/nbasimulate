namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ChangeFriMatch : Page
    {
        private int intFMatchID;
        private int intStatus;
        private int intType;
        private int intUserID;
        private int intUserIDA;

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
                this.intFMatchID = SessionItem.GetRequest("FMatchID", 0);
                this.intStatus = SessionItem.GetRequest("Status", 0);
                this.intType = SessionItem.GetRequest("Type", 2);
                this.intUserIDA = SessionItem.GetRequest("UserIDA", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (this.intStatus != 2)
            {
                if (BTPFriMatchManager.UpDateFriMatchStatus(this.intUserID, this.intFMatchID, this.intStatus, this.intType) == 1)
                {
                    DTOnlineManager.SetHasMsgByUserID(this.intUserIDA);
                    base.Response.Redirect("Report.aspx?Parameter=43");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=46");
                }
            }
            else if (MatchItem.InOnlyOneMatch(BTPClubManager.GetClubIDByUserID(this.intUserID)) && (this.intType == 5))
            {
                base.Response.Redirect("Report.aspx?Parameter=491b");
            }
            else
            {
                switch (BTPFriMatchManager.UpDateFriMatchStatus(this.intUserID, this.intFMatchID, this.intStatus, this.intType))
                {
                    case 0:
                        base.Response.Redirect("Report.aspx?Parameter=451");
                        return;

                    case 1:
                        DTOnlineManager.SetHasMsgByUserID(this.intUserIDA);
                        base.Response.Redirect("Report.aspx?Parameter=42");
                        return;

                    case 2:
                        base.Response.Redirect("Report.aspx?Parameter=537");
                        return;

                    case 5:
                        base.Response.Redirect("Report.aspx?Parameter=452");
                        return;

                    case 0x15:
                        base.Response.Redirect("Report.aspx?Parameter=500c");
                        return;

                    case 0x16:
                        base.Response.Redirect("Report.aspx?Parameter=500d");
                        return;
                }
                base.Response.Redirect("Report.aspx?Parameter=45");
            }
        }
    }
}


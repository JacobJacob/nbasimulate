namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class RookieMain_P : Page
    {
        private int intUserID;
        public string strLeftURL;
        public string strRightURL;

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
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "ASSIGNDEVCLUB":
                    this.strLeftURL = "RookieAssignDevClub.aspx";
                    this.strRightURL = "Intro/PlayerCenter.aspx?Type=NEWPLAYER";
                    break;

                case "ASSIGNCLUB":
                    this.strLeftURL = "RookieAssignClub.aspx";
                    this.strRightURL = "Intro/PlayerCenter.aspx?Type=NEWPLAYER";
                    break;
            }
        }
    }
}


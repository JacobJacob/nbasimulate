namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class ShowClubIFrom : Page
    {
        public int intHeight = 0;
        public string strUrl = "";

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            int request = (int) SessionItem.GetRequest("UserID", 0);
            if (request == -99)
            {
                this.intHeight = 370;
                this.strUrl = "ForumElite.aspx";
            }
            else
            {
                this.intHeight = 430;
                this.strUrl = "ShowClub.aspx?Type=5&UserID=" + request;
            }
        }
    }
}


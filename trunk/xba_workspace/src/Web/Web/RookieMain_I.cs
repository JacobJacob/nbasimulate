namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class RookieMain_I : Page
    {
        public string strCenter = "";

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
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "WELCOME":
                    this.strCenter = "RookieWelcome.aspx";
                    return;

                case "REGCLUB":
                    this.strCenter = "RookieRegClub.aspx";
                    return;

                case "ENDDEVCREATE":
                    this.strCenter = "RookieEndDevCreate.aspx";
                    break;

                case "END":
                    this.strCenter = "RookieEnd.aspx";
                    break;
            }
        }
    }
}


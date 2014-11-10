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
            string str = SessionItem.GetRequest("Type", 1).ToString().Trim();
            if (str != null)
            {
                if (str != "WELCOME")
                {
                    if (str != "REGCLUB")
                    {
                        if (str == "ENDDEVCREATE")
                        {
                            this.strCenter = "RookieEndDevCreate.aspx";
                            return;
                        }
                        if (str == "END")
                        {
                            this.strCenter = "RookieEnd.aspx";
                        }
                        return;
                    }
                }
                else
                {
                    this.strCenter = "RookieWelcome.aspx";
                    return;
                }
                this.strCenter = "RookieRegClub.aspx";
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class ChangeLogin : Page
    {
        private string strPassword;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strUserName = SessionItem.GetRequest("ue3tu23ce3vdmbv", 1);
            this.strPassword = SessionItem.GetRequest("stey86yi2jfdace", 1);
            int num = SessionItem.SetSelfLogin(this.strUserName, this.strPassword, true);
            if (num > 0)
            {
                base.Response.Redirect("");
            }
            else if (num == -2)
            {
                base.Response.Redirect("Report.aspx?Parameter=10111");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=10");
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


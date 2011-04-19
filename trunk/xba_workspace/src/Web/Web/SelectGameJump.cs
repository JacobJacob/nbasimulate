namespace Web
{
    using System;
    using System.Web.UI;

    public class SelectGameJump : Page
    {
        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            base.Response.Redirect("Report.aspx?Parameter=10120");
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


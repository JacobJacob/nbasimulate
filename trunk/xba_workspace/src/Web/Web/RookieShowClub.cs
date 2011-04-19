namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class RookieShowClub : Page
    {
        public string strNow;

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
            this.strNow = StringItem.FormatDate(DateTime.Now, "yyyy-MM-dd");
        }
    }
}


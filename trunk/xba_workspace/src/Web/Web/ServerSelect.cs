namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class ServerSelect : Page
    {
        public string strPageIntro;

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
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(-1, "", "", "");
        }
    }
}


namespace Web
{
    using LoginParameter;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.Helper;

    public class Announce : Page
    {
        protected ImageButton btnFalse;
        protected ImageButton btnTrue;
        public string strPageIntro;

        private void btnFalse_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("");
        }

        private void btnTrue_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect(DBLogin.URLString(1) + "Register.aspx");
        }

        private void InitializeComponent()
        {
            this.btnTrue.Click += new ImageClickEventHandler(this.btnTrue_Click);
            this.btnFalse.Click += new ImageClickEventHandler(this.btnFalse_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnTrue.ImageUrl = SessionItem.GetImageURL() + "button_26.gif";
            this.btnFalse.ImageUrl = SessionItem.GetImageURL() + "button_25.gif";
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


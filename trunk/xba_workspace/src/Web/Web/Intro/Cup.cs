namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class Cup : Page
    {
        protected HtmlTable tblChampion;
        protected HtmlTable tblKiloCup;
        protected HtmlTable tblSignUp;
        protected HtmlTable tblView;
        protected HtmlTable tblViewBig;

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
            this.tblSignUp.Visible = false;
            this.tblView.Visible = false;
            this.tblViewBig.Visible = false;
            this.tblKiloCup.Visible = false;
            this.tblChampion.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "SIGNUP":
                    this.tblSignUp.Visible = true;
                    return;

                case "VIEW":
                    this.tblView.Visible = true;
                    return;

                case "VIEWBIG":
                    this.tblViewBig.Visible = true;
                    return;

                case "KILOCUP":
                    this.tblKiloCup.Visible = true;
                    return;

                case "CHAMPION":
                    this.tblChampion.Visible = true;
                    return;
            }
            this.tblSignUp.Visible = true;
        }
    }
}


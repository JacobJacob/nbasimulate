namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class XBATop : Page
    {
        protected HtmlTable tblUserAbilityTop;
        protected HtmlTable tblUserVBTop;
        protected HtmlTable tblPlayerTop;

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
            this.tblUserAbilityTop.Visible = false;
            this.tblUserVBTop.Visible = false;
            this.tblPlayerTop.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "USERABILITYTOP":
                    this.tblUserAbilityTop.Visible = true;
                    return;

                case "PLAYERTOP":
                    this.tblPlayerTop.Visible = true;
                    return;

                case "USERVBTOP":
                    this.tblUserVBTop.Visible = true;
                    return;
            }
            this.tblUserAbilityTop.Visible = true;

        }
    }
}


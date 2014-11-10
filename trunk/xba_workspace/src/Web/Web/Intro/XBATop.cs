namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class XBATop : Page
    {
        protected HtmlTable tblPlayer3Top;
        protected HtmlTable tblPlayerTop;
        protected HtmlTable tblUserAbilityTop;
        protected HtmlTable tblUserVBTop;

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
            this.tblPlayer3Top.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "USERABILITYTOP":
                    this.tblUserAbilityTop.Visible = true;
                    return;

                case "PLAYERTOP":
                    this.tblPlayerTop.Visible = true;
                    return;

                case "PLAYER3TOP":
                    this.tblPlayer3Top.Visible = true;
                    return;

                case "USERVBTOP":
                    this.tblUserVBTop.Visible = true;
                    return;
            }
            this.tblUserAbilityTop.Visible = true;
        }
    }
}


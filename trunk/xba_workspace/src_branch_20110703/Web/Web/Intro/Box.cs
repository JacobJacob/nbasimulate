namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class Box : Page
    {
        protected HtmlTable tblBoxSet;
        protected HtmlTable tblBoxShow;
        protected HtmlTable tblModifyClub;

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
            this.tblBoxShow.Visible = false;
            this.tblBoxSet.Visible = false;
            this.tblModifyClub.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "BOXSHOW":
                    this.tblBoxShow.Visible = true;
                    return;

                case "BOXSEND":
                    this.tblBoxSet.Visible = true;
                    return;

                case "MODIFYCLUB":
                    this.tblModifyClub.Visible = true;
                    return;
            }
            this.tblBoxShow.Visible = true;
        }
    }
}


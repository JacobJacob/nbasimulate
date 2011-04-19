namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class Honour : Page
    {
        protected HtmlTable tbHonorList;
        protected HtmlTable tbTeamList;

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
            this.tbTeamList.Visible = false;
            this.tbHonorList.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "TEAM":
                    this.tbTeamList.Visible = true;
                    return;

                case "PLAYER":
                    this.tbHonorList.Visible = true;
                    return;
            }
            this.tbTeamList.Visible = true;
        }
    }
}


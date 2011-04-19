namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class MatchMy : Page
    {
        protected HtmlTable tbFMatchHistory;
        protected HtmlTable tbFMatchList;

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
            this.tbFMatchList.Visible = false;
            this.tbFMatchHistory.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "FMATCHLIST":
                    this.tbFMatchList.Visible = true;
                    return;

                case "FMATCHHISTORY":
                    this.tbFMatchHistory.Visible = true;
                    return;
            }
            this.tbFMatchList.Visible = true;
        }
    }
}


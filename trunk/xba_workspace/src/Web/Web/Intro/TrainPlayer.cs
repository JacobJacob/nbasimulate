namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class TrainPlayer : Page
    {
        protected HtmlTable tblTPlayer3;
        protected HtmlTable tblTPlayer5;
        protected HtmlTable tbPlayer3;
        protected HtmlTable tbPlayer5;
        protected HtmlTable tbTrain5;

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
            this.tbPlayer3.Visible = false;
            this.tbPlayer5.Visible = false;
            this.tbTrain5.Visible = false;
            this.tblTPlayer3.Visible = false;
            this.tblTPlayer5.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "PLAYER3":
                    this.tbPlayer3.Visible = true;
                    return;

                case "PLAYER5":
                    this.tbPlayer5.Visible = true;
                    return;

                case "TPLAYER3":
                    this.tblTPlayer3.Visible = true;
                    return;

                case "TPLAYER5":
                    this.tblTPlayer5.Visible = true;
                    return;

                case "TRAIN5":
                    this.tbTrain5.Visible = true;
                    return;
            }
            this.tbPlayer3.Visible = true;
        }
    }
}


namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class OnlineList : Page
    {
        protected HtmlTable tbFMatchMsg;
        protected HtmlTable tbFMatchSend;
        protected HtmlTable tbONlineList;
        protected HtmlTable tbTrainCenter;

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
            this.tbONlineList.Visible = false;
            this.tbFMatchMsg.Visible = false;
            this.tbFMatchSend.Visible = false;
            this.tbTrainCenter.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "ONLINELIST":
                    this.tbONlineList.Visible = true;
                    return;

                case "FMATCHMSG":
                    this.tbFMatchMsg.Visible = true;
                    return;

                case "FMATCHSEND":
                    this.tbFMatchSend.Visible = true;
                    return;

                case "TRAINCENTER":
                    this.tbTrainCenter.Visible = true;
                    return;
            }
            this.tbONlineList.Visible = true;
        }
    }
}


namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class Devision : Page
    {
        protected HtmlTable tblDevMsg;
        protected HtmlTable tbList;
        protected HtmlTable tbMatchLook;
        protected HtmlTable tbPic;
        protected HtmlTable tbRival;
        protected HtmlTable tbStat;
        protected HtmlTable tbView;

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
            this.tbView.Visible = false;
            this.tbRival.Visible = false;
            this.tbStat.Visible = false;
            this.tbList.Visible = false;
            this.tbPic.Visible = false;
            this.tbMatchLook.Visible = false;
            this.tblDevMsg.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "VIEW":
                    this.tbView.Visible = true;
                    return;

                case "RIVAL":
                    this.tbRival.Visible = true;
                    return;

                case "STAT":
                    this.tbStat.Visible = true;
                    return;

                case "LIST":
                    this.tbList.Visible = true;
                    return;

                case "PIC":
                    this.tbPic.Visible = true;
                    return;

                case "MATCHLOOK":
                    this.tbMatchLook.Visible = true;
                    return;

                case "MSG":
                    this.tblDevMsg.Visible = true;
                    return;
            }
            this.tbView.Visible = true;
        }
    }
}


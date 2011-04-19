namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class TransferMarket : Page
    {
        protected HtmlTable tblDevChooseList;
        protected HtmlTable tblMyFocus;
        protected HtmlTable tblStreetChooseList;
        protected HtmlTable tblStreetList;
        protected HtmlTable tblTransferList;
        protected HtmlTable tbTransferList;

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
            this.tblMyFocus.Visible = false;
            this.tblStreetList.Visible = false;
            this.tblStreetChooseList.Visible = false;
            this.tblTransferList.Visible = false;
            this.tblDevChooseList.Visible = false;
            this.tbTransferList.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "FOCUS":
                    this.tblMyFocus.Visible = true;
                    return;

                case "STREETFREE":
                    this.tblStreetList.Visible = true;
                    return;

                case "STREETCHOOSE":
                    this.tblStreetChooseList.Visible = true;
                    return;

                case "TRANSFER":
                    this.tblTransferList.Visible = true;
                    return;

                case "DEVCHOOSE":
                    this.tblDevChooseList.Visible = true;
                    return;

                case "UTMOST":
                    this.tbTransferList.Visible = true;
                    return;
            }
            this.tblMyFocus.Visible = true;
        }
    }
}


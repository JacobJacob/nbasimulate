namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class OnlyOneMatch : Page
    {
        protected HtmlTable tblOnlyPay;
        protected HtmlTable tblOnlyShow;
        protected HtmlTable tblOnlyWealth;

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
            this.tblOnlyPay.Visible = false;
            this.tblOnlyShow.Visible = false;
            this.tblOnlyWealth.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "SHOW":
                    this.tblOnlyShow.Visible = true;
                    return;

                case "PAY":
                    this.tblOnlyPay.Visible = true;
                    return;

                case "WEALTH":
                    this.tblOnlyWealth.Visible = true;
                    return;
            }
            this.tblOnlyShow.Visible = true;
        }
    }
}


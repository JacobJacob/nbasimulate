namespace Web.Intro
{
    using ServerManage;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class Tools : Page
    {
        protected HtmlTable tbBuyCoin;
        protected HtmlTable tbl17173;
        protected HtmlTable tbl51Wan;
        protected HtmlTable tblCGACoin;
        protected HtmlTable tblContainer;
        protected HtmlTable tblDuNiu;
        protected HtmlTable tblDW;
        protected HtmlTable tblStore;
        protected HtmlTable tblWealthManage;
        protected HtmlTable tblZHWCoin;

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
            this.tblContainer.Visible = false;
            this.tblStore.Visible = false;
            this.tblWealthManage.Visible = false;
            this.tbBuyCoin.Visible = false;
            this.tblCGACoin.Visible = false;
            this.tblZHWCoin.Visible = false;
            this.tbl51Wan.Visible = false;
            this.tbl17173.Visible = false;
            this.tblDuNiu.Visible = false;
            this.tblDW.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "STORE":
                    if (ServerParameter.strCopartner == "CGA")
                    {
                        this.tblCGACoin.Visible = true;
                        return;
                    }
                    if (ServerParameter.strCopartner == "ZHW")
                    {
                        this.tblZHWCoin.Visible = true;
                        return;
                    }
                    if (ServerParameter.strCopartner == "51WAN")
                    {
                        this.tbl51Wan.Visible = true;
                        return;
                    }
                    if (ServerParameter.strCopartner == "17173")
                    {
                        this.tbl17173.Visible = true;
                        return;
                    }
                    if (ServerParameter.strCopartner == "DUNIU")
                    {
                        this.tblDuNiu.Visible = true;
                        return;
                    }
                    this.tblContainer.Visible = true;
                    return;

                case "TOOLS":
                    this.tblStore.Visible = true;
                    return;

                case "WEALTHMANAGE":
                    this.tblWealthManage.Visible = true;
                    return;
            }
            if (ServerParameter.strCopartner == "CGA")
            {
                this.tblCGACoin.Visible = true;
            }
            else if (ServerParameter.strCopartner == "ZHW")
            {
                this.tblZHWCoin.Visible = true;
            }
            else if (ServerParameter.strCopartner == "51WAN")
            {
                this.tbl51Wan.Visible = true;
            }
            else if (ServerParameter.strCopartner == "17173")
            {
                this.tbl17173.Visible = true;
            }
            else if (ServerParameter.strCopartner == "DUNIU")
            {
                this.tblDuNiu.Visible = true;
            }
            else if (ServerParameter.strCopartner == "DW")
            {
                this.tblDW.Visible = true;
            }
            else
            {
                this.tblContainer.Visible = true;
            }
        }
    }
}


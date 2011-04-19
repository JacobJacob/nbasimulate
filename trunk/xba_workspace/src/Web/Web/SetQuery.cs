namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class SetQuery : Page
    {
        private int intOrderID;
        private int intUserID;
        private string strOID;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intOrderID = (int) SessionItem.GetRequest("ORDERID", 0);
                this.strOID = (string) SessionItem.GetRequest("OID", 1);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (ROOTUserManager.SetQuery(this.intUserID, this.intOrderID, this.strOID))
            {
                base.Response.Redirect("Report.aspx?Parameter=26");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=27");
            }
        }
    }
}


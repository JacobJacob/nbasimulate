namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class SendGift : Page
    {
        public int intUserID;

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
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                if (accountRowByUserID != null)
                {
                    int num = (byte) accountRowByUserID["OldUser"];
                    if (num != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=915");
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=12");
                }
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ReceiveADUnionsky : Page
    {
        private int intUserID;

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
            this.intUserID = SessionItem.GetRequest("UserID", 0);
            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim().Equals("218.108.237.11") && (this.intUserID > 0))
            {
                string strUserName = ROOTUserManager.GetUserTableRowByUserID(this.intUserID)["UserName"].ToString();
                string strOID = this.intUserID.ToString() + DateTime.Now.ToString();
                ROOTUserManager.AddPayOrder(this.intUserID, strUserName, 4, strOID, "5枚金币", 0, 1, 5);
                ROOTUserManager.AddCoinReceive(strUserName, 5, strOID);
            }
        }
    }
}


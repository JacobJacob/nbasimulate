namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class MyMoveBill : Page
    {
        private int intUserID;
        private string strUserName;
        protected TextBox tbMoveBillNumber;

        private void InitializeComponent()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intUserID = (int) onlineRowByUserID["UserID"];
                this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


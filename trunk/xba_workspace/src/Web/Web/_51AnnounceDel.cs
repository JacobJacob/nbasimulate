namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBConnection;
    using Web.Helper;

    public class _51AnnounceDel : Page
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
            this.intUserID = SessionItem.Check51Login();
            if (this.intUserID <= 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=701");
            }
            else
            {
                int request = SessionItem.GetRequest("ID", 0);
                string commandText = "DELETE * FROM BTP_Announce WHERE AnnounceID=" + request;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                base.Response.Redirect("Report.aspx?Parameter=702");
            }
        }
    }
}


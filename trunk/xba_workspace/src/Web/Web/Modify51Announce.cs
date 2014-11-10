namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBConnection;
    using Web.Helper;

    public class Modify51Announce : Page
    {
        protected Button btnOK;
        public int intAnnounceID;
        public int intAnnounceIDM;
        public int intUserID;
        public string strErrMsg;
        public string strTitle;
        protected TextBox tbTitle;

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!StringItem.IsValidContent(this.strTitle, 1, 300))
            {
                this.strErrMsg = "输入的公告过长，请重新输入。";
            }
            else
            {
                string commandText = string.Concat(new object[] { "UPDATE BTP_Announce SET Title='", this.strTitle, "' WHERE AnnounceID=", this.intAnnounceIDM });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                this.strErrMsg = "修改成功，请返回";
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
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
                this.intAnnounceID = SessionItem.GetRequest("ID", 0);
                string commandText = "SELECT * FROM BTP_Announce WHERE AnnounceID=" + this.intAnnounceID;
                DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (row != null)
                {
                    this.strTitle = row["Title"].ToString().Trim();
                    this.intAnnounceIDM = (int) row["AnnounceID"];
                }
                else
                {
                    this.strTitle = "";
                    this.intAnnounceIDM = 0;
                }
                if (!base.IsPostBack)
                {
                    this.tbTitle.Text = this.strTitle;
                }
            }
        }
    }
}


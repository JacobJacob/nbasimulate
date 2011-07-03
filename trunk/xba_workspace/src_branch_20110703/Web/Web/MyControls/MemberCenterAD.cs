namespace Web.MyControls
{
    using System;
    using System.Text;
    using System.Data;
    using System.Web.UI;
    using Web.DBConnection;

    public class MemberCenterAD : UserControl
    {
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
        }

        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder();
            string commandText = "SELECT * FROM Main_Index";
            string str2 = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText)["MemberAD01"].ToString();
            builder.Append(str2);
            output.Write(builder.ToString());
        }
    }
}


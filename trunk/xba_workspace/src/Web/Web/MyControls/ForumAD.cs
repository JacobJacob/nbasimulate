namespace Web.MyControls
{
    using System;
    using System.Text;
    using System.Web.UI;
    using Web.DBConnection;

    public class ForumAD : UserControl
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
            string str2 = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), 1, commandText)["ForumAD01"].ToString();
            builder.Append("<table width='1002' border='0' cellspacing='0' cellpadding='0'>");
            builder.Append("<tr>");
            builder.Append("<td width='202' height='71' align='center' valign='middle'><img src='Images/logo.JPG' width='151' height='68'></td>");
            builder.Append("<td width='800' height='71' align='center' valign='middle'>");
            builder.Append(str2);
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("</table>");
            output.Write(builder.ToString());
        }
    }
}


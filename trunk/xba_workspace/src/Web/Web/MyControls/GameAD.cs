namespace Web.MyControls
{
    using ServerManage;
    using System;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;

    public class GameAD : UserControl
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
            string str = BTPGameADManager.GetGameADRow()["GameAD01"].ToString();
            builder.Append("<table width='1002' border='0' cellspacing='0' cellpadding='0'>");
            builder.Append("<tr>");
            builder.Append("<td width='202' height='71' align='center' valign='middle'><img src='Images/");
            if (ServerParameter.strCopartner == "17173")
            {
                builder.Append("logo173.gif'");
            }
            else
            {
                builder.Append("logo.gif'");
            }
            builder.Append("width='184' height='51'></td>");
            builder.Append("<td width='800' height='71' align='center' valign='middle'>");
            builder.Append(str);
            builder.Append("</td>");
            builder.Append("</tr>");
            builder.Append("</table>");
            output.Write(builder.ToString());
        }
    }
}


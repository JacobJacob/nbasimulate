namespace Web.MyControls
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ForumElite : UserControl
    {
        private int intUserID;
        private string strPassword;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                this.strUserName = "";
                this.strPassword = "";
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strUserName = onlineRowByUserID["UserName"].ToString();
                this.strPassword = onlineRowByUserID["Password"].ToString();
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                DataTable eliteTopic = ROOTTopicManager.GetEliteTopic(4);
                builder.Append("<table width='200' border='0' cellspacing='0' cellpadding='0'>");
                builder.Append("<tr><td style='padding-left:8px' height='25'><font color='red'>论坛最新推荐</font></td></tr>");
                foreach (DataRow row in eliteTopic.Rows)
                {
                    string strIn = row["Title"].ToString().Trim();
                    string str2 = row["NickName"].ToString().Trim();
                    DateTime time = (DateTime) row["CreateTime"];
                    string str3 = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    builder.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    builder.Append(string.Concat(new object[] { "<td width='200' height='20' valign='middle' style='padding-left:8px'><a title='", strIn, "\n\n发表时间:", time, " 作者:", str2, "' href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Topic.aspx!TopicID.", num, "^BoardID.", str3, "^Page.1' target=\"_blank\">", StringItem.GetShortString(strIn, 0x19), "</a></td>" }));
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
            }
            catch
            {
                builder.Append("");
            }
            output.Write(builder.ToString());
        }
    }
}


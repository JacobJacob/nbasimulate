namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Main_M : Page
    {
        private int intUserID;
        public string strCenterURL;
        public string strRightURL;
        private string strType;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
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
                string str;
                string str2;
                string str3;
                this.strType = SessionItem.GetRequest("Type", 1);
                if (this.strType.IndexOf("TOPIC!") >= 0)
                {
                    str = "FrameTopic.aspx";
                    str = str + "?" + this.strType.Replace("^", "&").Replace(".", "=").Replace("TOPIC!", "");
                }
                else
                {
                    str = "FrameForum.aspx?";
                }
                DataRow row = ROOTUserManager.Get40UserRowByUserID(this.intUserID);
                if (row != null)
                {
                    str2 = row["UserName"].ToString().Trim();
                    str3 = row["Password"].ToString().Trim();
                }
                else
                {
                    str2 = "";
                    str3 = "";
                }
                this.strCenterURL = ServerItem.ToFrameTopicURL(0, str2, str3, this.intUserID, str);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


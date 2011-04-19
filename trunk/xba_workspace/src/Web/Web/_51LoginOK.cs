namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class _51LoginOK : Page
    {
        private string strPassword;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strUserName = base.Request["tbUserName"].ToString().Trim();
            this.strPassword = base.Request["tbPassword"].ToString().Trim();
            this.strUserName = StringItem.MD5Encrypt(this.strUserName, Global.strMD5Key);
            this.strPassword = StringItem.MD5Encrypt(this.strPassword, Global.strMD5Key);
            if (SessionItem.SetSelfLogin(this.strUserName, this.strPassword, true) > 0)
            {
                base.Response.Redirect("WebAD.aspx");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=10");
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


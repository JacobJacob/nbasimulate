namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class LoginOK : Page
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
            DataRow userRowByUserNamePWD = ROOTUserManager.GetUserRowByUserNamePWD(this.strUserName, this.strPassword);
            if (userRowByUserNamePWD != null)
            {
                SessionItem.SetMainLogin(userRowByUserNamePWD, true);
                base.Response.Redirect("MemberCenter.aspx");
            }
            else
            {
                //userRowByUserNamePWD.Close();
                base.Response.Redirect("Report.aspx?Parameter=10a");
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


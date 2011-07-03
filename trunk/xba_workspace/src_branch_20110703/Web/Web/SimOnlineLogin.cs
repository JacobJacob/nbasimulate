namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class SimOnlineLogin : Page
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
            string str;
            int onlineCount = DTOnlineManager.GetOnlineCount();
            int intUserID = 1;
            int num3 = SessionItem.SetSomebodyLogin(intUserID);
            SessionItem.SetLogout(intUserID);
            if (num3 == intUserID)
            {
                str = "成功登录";
            }
            else
            {
                str = "登录失败";
            }
            base.Response.Write(onlineCount + "," + str);
        }
    }
}


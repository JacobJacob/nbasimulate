namespace Web
{
    using ServerManage;
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class SetCookieExpire : Page
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
            int num;
            try
            {
                if (ServerParameter.intGameCategory == 0)
                {
                    num = Convert.ToInt32(base.Request.Cookies["LoginS"].Value);
                }
                else
                {
                    num = Convert.ToInt32(base.Request.Cookies["LoginG"].Value);
                }
            }
            catch
            {
                num = 0;
            }
            if (num > 0)
            {
                SessionItem.SetLogout(num);
            }
            SessionItem.SetLoginCookies(-1, 0);
            base.Response.Redirect("");
        }
    }
}


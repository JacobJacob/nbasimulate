namespace Web
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class _51Admin : Page
    {
        protected Button btnLogin;
        public int intUserID;
        public string strMessage;
        protected TextBox tbPassword;
        protected TextBox tbUserName;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string str = this.tbUserName.Text.ToString().Trim();
            string str2 = this.tbPassword.Text.ToString().Trim();
            int num = 1;
            if ((str == "Admin") && (str2 == "Admin51edu"))
            {
                this.Session[Global.strSessionName] = num;
                HttpCookie cookie = new HttpCookie(Global.strSessionName);
                cookie.Expires = DateTime.Now.AddHours(6.0);
                cookie.Value = num.ToString();
                base.Response.Cookies.Add(cookie);
                base.Response.Redirect("51AdminMain.aspx");
            }
            else
            {
                this.strMessage = "您输入的用户名/密码错误，请确认后重新登陆。";
            }
        }

        private void InitializeComponent()
        {
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
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
    }
}


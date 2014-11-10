namespace Web
{
    using System;
    using System.Web;
    using System.Web.UI;
    using Web.Helper;

    public class Register2 : Page
    {
        public string strFromName = "";
        public string strIndexEnd;
        public string strIndexHead;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strIndexHead = BoardItem.GetIndexHead(-1, "", "");
            this.strIndexEnd = BoardItem.GetIndexEnd();
            if (base.Request.Cookies["FromName"] != null)
            {
                this.strFromName = base.Request.Cookies["FromName"].Value.ToString();
                HttpCookie cookie = new HttpCookie("FromName") {
                    Value = ""
                };
                base.Response.Cookies.Add(cookie);
            }
            else
            {
                this.strFromName = "";
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


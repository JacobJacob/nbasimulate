namespace Web
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class FromURL : Page
    {
        private int intFromID;
        private string strFromName;
        private string strFromURL;
        private string strJumpToURL;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strFromURL = SessionItem.GetRequest("FromURL", 1);
            this.intFromID = SessionItem.GetRequest("FromID", 0);
            this.strJumpToURL = SessionItem.GetRequest("JumpToURL", 1);
            if (this.strFromURL != "")
            {
                HttpCookie cookie = new HttpCookie("FromURL") {
                    Value = this.strFromURL
                };
                base.Response.Cookies.Add(cookie);
            }
            if (this.strJumpToURL == "")
            {
                this.strJumpToURL = "lead/index.htm";
            }
            if (this.intFromID > 0)
            {
                DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intFromID);
                if (userRowByUserID != null)
                {
                    this.strFromName = userRowByUserID["NickName"].ToString().Trim();
                }
                else
                {
                    this.strFromName = "";
                }
                HttpCookie cookie2 = new HttpCookie("FromName") {
                    Value = this.strFromName
                };
                base.Response.Cookies.Add(cookie2);
            }
            base.Response.Redirect(this.strJumpToURL);
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


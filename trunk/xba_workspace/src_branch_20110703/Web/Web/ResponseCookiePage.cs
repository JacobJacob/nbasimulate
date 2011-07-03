namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ResponseCookiePage : Page
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
            string request = (string) SessionItem.GetRequest("CheckCookie", 1);
            int intUserID = Convert.ToInt32(StringItem.MD5Decrypt(SessionItem.GetRequest("UserID", 1).ToString().Trim(), Global.strMainMD5Key));
            if (request == "true")
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                if (accountRowByUserID == null)
                {
                    DataRow dr = ROOTUserManager.Get40UserRowByUserID(intUserID);
                    if (dr != null)
                    {
                        //dr.Close();
                        base.Response.Redirect("Report.aspx?Parameter=12");
                        return;
                    }
                    SessionItem.SetMainLogin(dr, false);
                }
                else
                {
                    string strUserName = StringItem.MD5Encrypt(accountRowByUserID["UserName"].ToString().Trim(), Global.strMD5Key);
                    string strPassword = StringItem.MD5Encrypt(accountRowByUserID["Password"].ToString().Trim(), Global.strMD5Key);
                    SessionItem.SetSelfLogin(strUserName, strPassword, false);
                }
            }
            HttpCookie cookie = new HttpCookie("HasCheckMainCookie");
            cookie["HasCheckCookie"] = "true";
            base.Response.Cookies.Add(cookie);
            SessionItem.JumpToTargetPage();
        }
    }
}


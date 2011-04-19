namespace Web
{
    using ServerManage;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ServerJump : Page
    {
        public string strPassword;
        public string strURL;
        public string strUserName;

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
            int request = (int) SessionItem.GetRequest("Kind", 0);
            int num2 = (int) SessionItem.GetRequest("GameCategory", 0);
            this.strURL = (string) SessionItem.GetRequest("URL", 1);
            if ((this.strURL == null) || (this.strURL == ""))
            {
                this.strURL = "1";
            }
            else
            {
                string strURL = this.strURL;
                string str2 = "";
                if (this.strURL.IndexOf("!") >= 0)
                {
                    string[] strArray = this.strURL.Split(new char[] { '!' });
                    strURL = strArray[0];
                    str2 = strArray[1];
                    str2 = ("?" + str2.Replace("^", "&").Replace(".", "=")).Replace("$", ".");
                }
                string url = strURL + str2;
                this.strUserName = (string) SessionItem.GetRequest("ue3tu23ce3vdmbv", 1);
                this.strPassword = (string) SessionItem.GetRequest("stey86yi2jfdace", 1);
                string strUserName = StringItem.MD5Decrypt(this.strUserName, Global.strMD5Key);
                string strPassword = StringItem.MD5Decrypt(this.strPassword, Global.strMD5Key);
                if (((ServerParameter.intGameCategory == 0) || (url == "RegClub.aspx")) || (url == "SmartMain.aspx"))
                {
                    if (url == "RegClub.aspx")
                    {
                        url = "RegClub.aspx?Type=NEXT";
                    }
                    SqlDataReader userRowByUserNamePWD = ROOTUserManager.GetUserRowByUserNamePWD(strUserName, strPassword);
                    if (userRowByUserNamePWD.HasRows)
                    {
                        SessionItem.SetMainLogin(userRowByUserNamePWD, false);
                        base.Response.Redirect(url);
                    }
                    else
                    {
                        userRowByUserNamePWD.Close();
                        base.Response.Redirect("Report.aspx?Parameter=12");
                    }
                }
                else
                {
                    if (url.IndexOf("AlterFace.aspx") != -1)
                    {
                        SessionItem.SetSelfLogin(this.strUserName, this.strPassword, false);
                    }
                    else if (url == "MemberCenter.aspx")
                    {
                        SessionItem.SetMainLogin(ROOTUserManager.GetUserRowByUserNamePWD(strUserName, strPassword), false);
                    }
                    else
                    {
                        SessionItem.SetSelfLogin(this.strUserName, this.strPassword, true);
                    }
                    base.Response.Redirect(url);
                }
            }
        }
    }
}


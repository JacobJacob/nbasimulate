namespace Web
{
    using System;
    using System.IO;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class RegOK : Page
    {
        private bool blnSex;
        private string strCity;
        private string strEmail;
        private string strIntroNickName;
        private string strNickName;
        private string strPassword;
        private string strPrv;
        private string strRePassword;
        private string strSay;
        private string strSex;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strUserName = base.Request["tbUserName"].ToString().Trim();
            this.strPassword = base.Request["tbPassword"].ToString().Trim();
            this.strRePassword = base.Request["tbRePassword"].ToString().Trim();
            this.strNickName = base.Request["tbNickName"].ToString().Trim();
            this.strEmail = base.Request["tbEmail"].ToString().Trim();
            this.strPrv = base.Request["prv"].ToString().Trim();
            this.strCity = base.Request["City"].ToString().Trim();
            try
            {
                this.strIntroNickName = base.Request["tbIntroNickName"].ToString().Trim();
            }
            catch
            {
                this.strIntroNickName = "";
            }
            this.strSay = "目标总冠军！达不到目标找豆腐撞死。";
            bool flag = false;
            if (base.Request["cbCheck"] == null)
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=450");
            }
            if (this.strPrv == "")
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=455");
            }
            if (this.strCity == "")
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=456");
            }
            if (base.Request["Sex"] == null)
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=460");
            }
            else
            {
                this.strSex = base.Request["Sex"].ToString().Trim();
                if (this.strSex == "rbMale")
                {
                    this.blnSex = Convert.ToBoolean(0);
                }
                else
                {
                    this.blnSex = Convert.ToBoolean(1);
                }
            }
            if (!StringItem.IsValidLogin(this.strUserName))
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=462");
            }
            else if ((StringItem.GetStrLength(this.strUserName) < 4) || (StringItem.GetStrLength(this.strUserName) > 0x10))
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=463");
            }
            if (this.strPassword == "")
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=464");
            }
            else if (!StringItem.IsValidLogin(this.strPassword))
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=465");
            }
            else if (this.strPassword != this.strRePassword)
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=466");
            }
            if (!StringItem.IsValidEmail(this.strEmail))
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=467");
            }
            if (!StringItem.IsValidName(this.strNickName, 2, 0x10))
            {
                flag = true;
                base.Response.Redirect("Report.aspx?Parameter=468");
            }
            switch (ROOTUserManager.HasInputInfo(this.strUserName, this.strNickName, this.strEmail))
            {
                case 1:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=469");
                    break;

                case 2:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=470");
                    break;

                case 3:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=471");
                    break;

                case 4:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=472");
                    break;

                case 5:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=473");
                    break;

                case 6:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=474");
                    break;

                case 7:
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=475");
                    break;
            }
            if (this.strIntroNickName != "")
            {
                if (!StringItem.IsValidName(this.strIntroNickName, 2, 0x10))
                {
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=478");
                }
                else if (!ROOTUserManager.HasNickName(this.strIntroNickName))
                {
                    flag = true;
                    base.Response.Redirect("Report.aspx?Parameter=479");
                }
            }
            if (!flag)
            {
                string str;
                string str2;
                string strFace = "0|0|0|0|0|0|0|0|0";
                string strDiskURL = "NetDisk/" + StringItem.FormatDate(DateTime.Now, "yyyyMM") + "/";
                if (base.Request.Cookies["FromURL"] != null)
                {
                    str = base.Request.Cookies["FromURL"].Value.ToString();
                    HttpCookie cookie = new HttpCookie("FromURL") {
                        Value = ""
                    };
                    base.Response.Cookies.Add(cookie);
                }
                else
                {
                    str = "";
                }
                if (base.Request.Cookies["UserCookies"] != null)
                {
                    str2 = base.Request.Cookies["UserCookies"].Value.ToString();
                }
                else
                {
                    str2 = StringItem.FormatDate(DateTime.Now, "yyyyMMddhhmmss").ToString() + RandomItem.rnd.Next(100, 0x3e7).ToString();
                    HttpCookie cookie2 = new HttpCookie("UserCookies") {
                        Value = str2,
                        Expires = DateTime.Now.AddYears(50)
                    };
                    base.Response.Cookies.Add(cookie2);
                }
                int intUserID = ROOTUserManager.AddUser(this.strUserName, 0, this.strPassword, this.strNickName, this.blnSex, strFace, "", "", "", this.strEmail, this.strPrv, this.strCity, this.strIntroNickName, this.strSay, base.Request.ServerVariables["REMOTE_ADDR"], strDiskURL, str, "", "", str2);
                string str5 = "Boy";
                if (this.blnSex)
                {
                    str5 = "Girl";
                }
                strDiskURL = strDiskURL + intUserID + "/";
                strDiskURL = base.Server.MapPath(strDiskURL);
                if (!Directory.Exists(strDiskURL))
                {
                    Directory.CreateDirectory(strDiskURL);
                }
                FaceItem.CreateFace(base.Server.MapPath("Images/Face/" + str5 + "/"), strDiskURL, strFace);
                SessionItem.SetMainLogin(ROOTUserManager.GetUserRowByUserID(intUserID), false);
                base.Response.Redirect("MemberCenter.aspx");
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


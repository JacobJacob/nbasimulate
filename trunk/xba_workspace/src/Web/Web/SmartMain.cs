namespace Web
{
    using ServerManage;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class SmartMain : Page
    {
        private int intUserID;
        private string strFromMemberCenter = "";

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strFromMemberCenter = SessionItem.GetRequest("Type", 1).ToString().Trim();
            DataRow accountRowByUserID = null;
            string strCopartner = ServerParameter.strCopartner;
            switch (strCopartner)
            {
                case "17173":
                case "ZHW":
                case "DUNIU":
                case "DW":
                    if (base.Request.UrlReferrer == null)
                    {
                        base.Response.Write("<script>alert(\"您的访问被拒绝，请通过主页访问重试。\");window.top.location=\"" + ConfigurationSettings.AppSettings.Get("MainUrl").Trim() + "\";</script>");
                        return;
                    }
                    if (!base.Request.UrlReferrer.ToString().StartsWith(ConfigurationSettings.AppSettings.Get("MainUrl").Trim()))
                    {
                        base.Response.Write("<script>alert(\"您的访问被拒绝，请通过主页访问重试。\");window.top.location=\"" + ConfigurationSettings.AppSettings.Get("MainUrl").Trim() + "\";</script>");
                        return;
                    }
                    break;
            }
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                if (strCopartner == "XBA")
                {
                    if (this.strFromMemberCenter == "")
                    {
                        base.Response.Redirect("Report.aspx?Parameter=12");
                        return;
                    }
                    string str2 = SessionItem.GetDomainUserID().ToString().Trim();
                    if (str2 == string.Empty)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=12");
                        return;
                    }
                    this.intUserID = Convert.ToInt32(str2);
                    accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    SessionItem.SetSomebodyGameLogin(this.intUserID);
                }
                else
                {
                    int request = (int) SessionItem.GetRequest("F", 0);
                    if (request == 1)
                    {
                        int intUserID = (int) SessionItem.GetRequest("U", 0);
                        string str3 = SessionItem.GetRequest("S", 1).ToString().Trim();
                        accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                        if (accountRowByUserID == null)
                        {
                            SqlDataReader reader = ROOTUserManager.Get40UserRowByUserID(intUserID);
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    string str = reader["UserName"].ToString().Trim();
                                    switch (strCopartner)
                                    {
                                        case "51WAN":
                                            str = str.ToLower();
                                            break;

                                        case "CGA":
                                            str = HttpUtility.UrlEncode(str, Encoding.GetEncoding("GB2312"));
                                            break;
                                    }
                                    if (StringItem.MD5Encrypt(request.ToString() + intUserID.ToString() + str, "a)@8Kh~7").ToString().Trim().Substring(0, 8) != str3)
                                    {
                                        base.Response.Redirect("Report.aspx?Parameter=12");
                                        return;
                                    }
                                    this.intUserID = intUserID;
                                    SessionItem.SetSomebodyGameLogin(this.intUserID);
                                }
                            }
                            else
                            {
                                base.Response.Redirect("Report.aspx?Parameter=12");
                                return;
                            }
                            reader.Close();
                        }
                        else
                        {
                            string str6 = accountRowByUserID["UserName"].ToString().Trim();
                            switch (strCopartner)
                            {
                                case "51WAN":
                                    str6 = str6.ToLower();
                                    break;

                                case "CGA":
                                    str6 = HttpUtility.UrlEncode(str6, Encoding.GetEncoding("GB2312"));
                                    break;
                            }
                            if (StringItem.MD5Encrypt(request.ToString() + intUserID.ToString() + str6, "a)@8Kh~7").ToString().Trim().Substring(0, 8) == str3)
                            {
                                this.intUserID = intUserID;
                                SessionItem.SetSomebodyGameLogin(this.intUserID);
                            }
                            else
                            {
                                base.Response.Redirect("Report.aspx?Parameter=12");
                                return;
                            }
                        }
                    }
                }
            }
            accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("RookieMain_M.aspx");
            }
            else
            {
                int num3 = (byte) accountRowByUserID["Category"];
                int num4 = (byte) accountRowByUserID["LockTime"];
                if (num4 > 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=19");
                }
                else if ((num3 == 0) || (num3 == 4))
                {
                    base.Response.Redirect("RookieMain_M.aspx");
                }
                else if (((num3 == 3) || (num3 == 10)) || (num3 == 2))
                {
                    SessionItem.SetSomebodyGameLogin(this.intUserID);
                    if (accountRowByUserID["RookieOp"].ToString().Trim().IndexOf("0") != -1)
                    {
                        base.Response.Redirect("RookieMain_M.aspx");
                    }
                    else
                    {
                        base.Response.Redirect("WebAD.aspx");
                    }
                }
                else
                {
                    SessionItem.SetSomebodyGameLogin(this.intUserID);
                    base.Response.Redirect("WebAD.aspx");
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


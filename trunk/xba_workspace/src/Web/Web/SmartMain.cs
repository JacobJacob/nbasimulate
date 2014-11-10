namespace Web
{
    using ServerManage;
    using System;
    using System.Configuration;
    using System.Data;
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
            string str6;
            this.strFromMemberCenter = SessionItem.GetRequest("Type", 1).ToString().Trim();
            DataRow accountRowByUserID = null;
            string strCopartner = ServerParameter.strCopartner;
            if (((str6 = strCopartner) != null) && (((str6 == "17173") || (str6 == "ZHW")) || ((str6 == "DUNIU") || (str6 == "DW"))))
            {
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
                    int request = SessionItem.GetRequest("F", 0);
                    if (request == 1)
                    {
                        int intUserID = SessionItem.GetRequest("U", 0);
                        string str3 = SessionItem.GetRequest("S", 1).ToString().Trim();
                        accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                        if (accountRowByUserID != null)
                        {
                            string str = accountRowByUserID["UserName"].ToString().Trim();
                            string str8 = strCopartner;
                            if (str8 != null)
                            {
                                if (str8 == "51WAN")
                                {
                                    str = str.ToLower();
                                }
                                else if (str8 == "CGA")
                                {
                                    str = HttpUtility.UrlEncode(str, Encoding.GetEncoding("GB2312"));
                                }
                            }
                            if (StringItem.MD5Encrypt(request.ToString() + intUserID.ToString() + str, "a)@8Kh~7").ToString().Trim().Substring(0, 8) == str3)
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
                        else
                        {
                            DataRow row2 = ROOTUserManager.Get40UserRowByUserID(intUserID);
                            if (row2 == null)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=12");
                                return;
                            }
                            string str4 = row2["UserName"].ToString().Trim();
                            string str7 = strCopartner;
                            if (str7 != null)
                            {
                                if (str7 == "51WAN")
                                {
                                    str4 = str4.ToLower();
                                }
                                else if (str7 == "CGA")
                                {
                                    str4 = HttpUtility.UrlEncode(str4, Encoding.GetEncoding("GB2312"));
                                }
                            }
                            if (StringItem.MD5Encrypt(request.ToString() + intUserID.ToString() + str4, "a)@8Kh~7").ToString().Trim().Substring(0, 8) != str3)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=12");
                                return;
                            }
                            this.intUserID = intUserID;
                            SessionItem.SetSomebodyGameLogin(this.intUserID);
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
                else
                {
                    switch (num3)
                    {
                        case 0:
                        case 4:
                            base.Response.Redirect("RookieMain_M.aspx");
                            return;

                        case 3:
                        case 10:
                        case 2:
                            SessionItem.SetSomebodyGameLogin(this.intUserID);
                            if (accountRowByUserID["RookieOp"].ToString().Trim().IndexOf("0") != -1)
                            {
                                base.Response.Redirect("RookieMain_M.aspx");
                                return;
                            }
                            base.Response.Redirect("WebAD.aspx");
                            return;
                    }
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


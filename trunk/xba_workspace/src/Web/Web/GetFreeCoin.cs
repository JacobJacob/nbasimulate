namespace Web
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class GetFreeCoin : Page
    {
        private int intUserID;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                int index = -1;
                int num2 = 20;
                int num3 = 100;
                DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intUserID);
                if (userRowByUserID != null)
                {
                    num2 = (int) userRowByUserID["Coin"];
                    num3 = (int) userRowByUserID["FreeCoin"];
                }
                index = base.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("Alexa Toolbar");
                if ((num2 < 10) && (index > 0))
                {
                    int num4 = 10;
                    if (base.Request.Cookies["FromFromR"] != null)
                    {
                        num4 = Convert.ToInt32(base.Request.Cookies["FromFromR"].Value);
                    }
                    else
                    {
                        num4 = RandomItem.rnd.Next(0, Convert.ToInt32(Math.Pow(10.0, (double) (num3 + 1))));
                        HttpCookie cookie = new HttpCookie("FromFromR") {
                            Value = num4.ToString(),
                            Expires = DateTime.Now.AddHours(24.0)
                        };
                        base.Response.Cookies.Add(cookie);
                    }
                    if (num4 < 1)
                    {
                        ROOTUserManager.AddFreeCoin(this.intUserID, 1);
                        HttpCookie cookie2 = new HttpCookie("FromFromR") {
                            Value = "10",
                            Expires = DateTime.Now.AddHours(24.0)
                        };
                        base.Response.Cookies.Add(cookie2);
                    }
                }
                base.Response.Redirect("XBAFinance.aspx?Type=COIN&Page=1");
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


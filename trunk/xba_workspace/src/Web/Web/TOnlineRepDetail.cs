namespace Web
{
    using AjaxPro;
    using System;
    using System.Net;
    using System.Web.UI;
    using Web.Helper;
    using Web.zh.com.xba.match_5;

    public class TOnlineRepDetail : Page
    {
        public static CookieContainer Cookie = new CookieContainer();
        public int intTag;

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public string GetDetail(int intTag)
        {
            Web.zh.com.xba.match_5.TOnlineReport report = new Web.zh.com.xba.match_5.TOnlineReport {
                CookieContainer = Cookie
            };
            return report.TOnlineReportDetail(intTag);
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(TOnlineRepDetail));
            this.intTag = SessionItem.GetRequest("Tag", 0);
            Cookie = new CookieContainer();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


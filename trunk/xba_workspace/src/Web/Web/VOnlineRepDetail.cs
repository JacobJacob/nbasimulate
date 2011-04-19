namespace Web
{
    using AjaxPro;
    using ServerManage;
    using System;
    using System.Net;
    using System.Web.UI;
    using Web.zh.com.cga.xba.match1;
    using Web.zh.com.cga.xba.match2;
    using Web.zh.com.xba.match_1;
    using Web.zh.com.xba.match_2;
    using Web.zh.com.xba.match_3;
    using Web.zh.com.xba.match_4;
    using Web.com.china.game.xbam1;
    using Web.Helper;
    using Web.localhost;
    using Web.WebReference;

    public class VOnlineRepDetail : Page
    {
        public static CookieContainer Cookie = new CookieContainer();
        public int intTag;

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public string GetDetail(int intTag)
        {
            if (ServerParameter.blnUseServer)
            {
                if (ServerParameter.strCopartner == "XBA")
                {
                    if (ServerParameter.intGameCategory == 1)
                    {
                        Web.zh.com.xba.match_1.VOnlineReport report = new Web.zh.com.xba.match_1.VOnlineReport();
                        report.CookieContainer = Cookie;
                        return report.VOnlineReportDetail(intTag);
                    }
                    if (ServerParameter.intGameCategory == 2)
                    {
                        Web.zh.com.xba.match_2.VOnlineReport report2 = new Web.zh.com.xba.match_2.VOnlineReport();
                        report2.CookieContainer = Cookie;
                        return report2.VOnlineReportDetail(intTag);
                    }
                    if (ServerParameter.intGameCategory == 3)
                    {
                        Web.zh.com.xba.match_3.VOnlineReport report3 = new Web.zh.com.xba.match_3.VOnlineReport();
                        report3.CookieContainer = Cookie;
                        return report3.VOnlineReportDetail(intTag);
                    }
                    Web.zh.com.xba.match_4.VOnlineReport report4 = new Web.zh.com.xba.match_4.VOnlineReport();
                    report4.CookieContainer = Cookie;
                    return report4.VOnlineReportDetail(intTag);
                }
                if (ServerParameter.strCopartner == "CGA")
                {
                    if (ServerParameter.intGameCategory == 1)
                    {
                        Web.zh.com.cga.xba.match1.VOnlineReport report5 = new Web.zh.com.cga.xba.match1.VOnlineReport();
                        report5.CookieContainer = Cookie;
                        return report5.VOnlineReportDetail(intTag);
                    }
                    Web.zh.com.cga.xba.match2.VOnlineReport report6 = new Web.zh.com.cga.xba.match2.VOnlineReport();
                    report6.CookieContainer = Cookie;
                    return report6.VOnlineReportDetail(intTag);
                }
                if (ServerParameter.strCopartner == "ZHW")
                {
                    if (ServerParameter.intGameCategory == 1)
                    {
                        Web.com.china.game.xbam1.VOnlineReport report7 = new Web.com.china.game.xbam1.VOnlineReport();
                        report7.CookieContainer = Cookie;
                        return report7.VOnlineReportDetail(intTag);
                    }
                    Web.com.china.game.xbam1.VOnlineReport report8 = new Web.com.china.game.xbam1.VOnlineReport();
                    report8.CookieContainer = Cookie;
                    return report8.VOnlineReportDetail(intTag);
                }
                Web.localhost.VOnlineReport report9 = new Web.localhost.VOnlineReport();
                report9.CookieContainer = Cookie;
                return report9.VOnlineReportDetail(intTag);
            }
            Web.WebReference.VOnlineReport report10 = new Web.WebReference.VOnlineReport();
            report10.CookieContainer = Cookie;
            return report10.VOnlineReportDetail(intTag);
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(VOnlineRepDetail));
            this.intTag = (int) SessionItem.GetRequest("Tag", 0);
            Cookie = new CookieContainer();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


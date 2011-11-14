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

    public class TOnlineRepDetail : Page
    {
        public static CookieContainer Cookie = new CookieContainer();
        public int intTag;

        [AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public string GetDetail(int intTag)
        {
            
            Web.zh.com.xba.match_5.TOnlineReport report5 = new Web.zh.com.xba.match_5.TOnlineReport();
            report5.CookieContainer = Cookie;
            return report5.TOnlineReportDetail(intTag);
     
                   
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(TOnlineRepDetail));
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


namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;

    public class OnlineCount : Page
    {
        public double dblMS;
        public int intOnlineCount;
        public string strResult;

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
            DateTime now = DateTime.Now;
            this.intOnlineCount = DTOnlineManager.GetOnlineCount();
            TimeSpan span = (TimeSpan) (DateTime.Now - now);
            this.dblMS = span.TotalMilliseconds;
            this.strResult = string.Concat(new object[] { "在线：", this.intOnlineCount, "人，执行：", this.dblMS });
        }
    }
}


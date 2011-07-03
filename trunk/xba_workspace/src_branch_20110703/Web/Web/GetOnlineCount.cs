namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;

    public class GetOnlineCount : Page
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
            int onlineCount = DTOnlineManager.GetOnlineCount();
            base.Response.Write(onlineCount);
        }
    }
}


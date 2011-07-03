namespace Web.WebService
{
    using System;
    using System.ComponentModel;
    using System.Web.Services;
    using Web.DBData;
    using Web.Helper;

    public class OnlineService : WebService
    {
        private IContainer components = null;

        public OnlineService()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [WebMethod]
        public int GetOnlineCount()
        {
            return DTOnlineManager.GetOnlineCount();
        }

        private void InitializeComponent()
        {
        }

        public void SetLogout()
        {
        }

        [WebMethod]
        public int SetOnline(int intUserID)
        {
            if (intUserID > -1)
            {
                SessionItem.SetLoginCookies(intUserID, 30);
                int num = (int) DTOnlineManager.GetOnlineRowByUserID(intUserID)["HasMsg"];
                if (num > 0)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}


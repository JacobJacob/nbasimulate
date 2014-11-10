namespace Web
{
    using AjaxPro;
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;
    using Web.Util;

    public class BidWeibo : Page
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
            Utility.RegisterTypeForAjax(typeof(BidWeibo));
        }

        [AjaxMethod]
        public int weiboBid(string uid, int type, string username, string password)
        {
            Logger.Debug(string.Concat(new object[] { "微博绑定.uid[", uid, "], type[", type, "]" }));
            int num = BTPAccountManager.WeiboBid(uid, type, username, password);
            if (num == 1)
            {
                username = StringItem.MD5Encrypt(username, Global.strMD5Key);
                password = StringItem.MD5Encrypt(password, Global.strMD5Key);
                SessionItem.SetSelfLogin(username, password, false);
            }
            return num;
        }
    }
}


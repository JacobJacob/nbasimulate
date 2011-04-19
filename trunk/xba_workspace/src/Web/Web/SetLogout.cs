namespace Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class SetLogout : Page
    {
        protected Button btnOK;
        public string strMsg;
        protected TextBox tbNickName;

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strNickName = this.tbNickName.Text.Trim();
            int intUserID = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["UserID"];
            SessionItem.SetLogout(intUserID);
            this.strMsg = strNickName + "已经被踢出在线！";
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


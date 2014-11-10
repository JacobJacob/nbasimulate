namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class MemberCenter : Page
    {
        public int intUserID;
        public string strCoin;
        public string strFreeCoin;
        public string strGameList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strNickName;
        public string strSmartPath;
        protected HtmlTable tblGameServer;
        protected HtmlTableCell tdFreeCoinButton;

        public void GetServerVariables()
        {
            string str = base.Request.ServerVariables["HTTP_USER_AGENT"];
            int index = str.IndexOf("Alexa Toolbar");
            base.Response.Write(str + "|" + index);
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Register.aspx");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


namespace Web
{
    using cn.com.chinabank.CBSecurity;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Pay : Page
    {
        private int intOrderCategory;
        private int intUserID;
        protected string key;
        protected string remark1;
        protected string remark2;
        private string strNickName;
        public string strPageIntro;
        private string strPassword;
        public string strUserName;
        protected string style;
        protected string v_amount;
        protected string v_md5info;
        protected string v_mid;
        protected string v_moneytype;
        protected string v_oid;
        protected string v_orderemail;
        protected string v_ordername;
        protected string v_orderstatus;
        protected string v_rcvaddr;
        protected string v_rcvname;
        protected string v_rcvpost;
        protected string v_rcvtel;
        protected string v_url;
        protected string v_ymd;

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
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strUserName = userInfoByID["UserName"].ToString().Trim();
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.strPassword = userInfoByID["Password"].ToString().Trim();
                this.v_mid = "34107";
                this.key = "stonehenge20031118";
                this.v_oid = (string) SessionItem.GetRequest("OID", 1);
                this.intOrderCategory = (int) SessionItem.GetRequest("OrderCategory", 0);
                int request = (int) SessionItem.GetRequest("OrderID", 0);
                ROOTUserManager.SetOrderStatus(request, 0);
                if (this.intOrderCategory == 1)
                {
                    this.remark1 = this.strUserName + " 充20枚金币";
                    this.v_amount = "10";
                }
                else if (this.intOrderCategory == 2)
                {
                    this.remark1 = this.strUserName + " 充40枚金币";
                    this.v_amount = "20";
                }
                else if (this.intOrderCategory == 3)
                {
                    this.remark1 = this.strUserName + " 充100枚金币";
                    this.v_amount = "45";
                }
                else if (this.intOrderCategory == 4)
                {
                    this.remark1 = this.strUserName + " 充250枚金币";
                    this.v_amount = "100";
                }
                else if (this.intOrderCategory == 5)
                {
                    this.remark1 = this.strUserName + " 充3000枚金币";
                    this.v_amount = "1000";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
                this.v_moneytype = "0";
                this.v_url = "http://www.xba.com.cn/Receive.aspx";
                this.style = "0";
                string str = this.v_amount + this.v_moneytype + this.v_oid + this.v_mid + this.v_url + this.key;
                this.v_md5info = MD5Util.getMD5(str);
                this.v_rcvname = "";
                this.v_rcvaddr = "";
                this.v_rcvtel = "";
                this.v_rcvpost = "";
                this.v_ordername = "";
                this.v_orderemail = "";
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


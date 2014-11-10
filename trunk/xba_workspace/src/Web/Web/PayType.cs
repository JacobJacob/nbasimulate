namespace Web
{
    using cn.com.chinabank.CBSecurity;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class PayType : Page
    {
        protected ImageButton btnOK;
        protected HtmlForm Form1;
        public int intCoin;
        private int intOrderCategory;
        private int intUserID;
        protected string key;
        protected RadioButton rbA;
        protected RadioButton rbB;
        protected RadioButton rbC;
        protected RadioButton rbD;
        protected RadioButton rbE;
        protected string remark1;
        protected string remark2;
        public string strCoin;
        public string strIndexEnd;
        public string strIndexHead;
        public string strNickName;
        private string strPassword;
        private string strType;
        public string strUserName;
        protected string style;
        protected HtmlTable tblBank;
        protected HtmlTable tblJPAY;
        protected HtmlTable tblPay;
        protected HtmlTable tblPPAY;
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

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            int num;
            int num2;
            int num3;
            DateTime now = DateTime.Now;
            int num4 = 0x3e8 + RandomItem.rnd.Next(0, 0x3e8);
            string strOID = "34107" + StringItem.FormatDate(now, "yyyyMMddhhmmss") + num4;
            if (this.rbA.Checked)
            {
                str = "20枚金币";
                num = 1;
                num2 = 10;
                num3 = 1;
            }
            else if (this.rbB.Checked)
            {
                str = "40枚金币";
                num = 1;
                num2 = 20;
                num3 = 2;
            }
            else if (this.rbC.Checked)
            {
                str = "100枚金币";
                num = 1;
                num2 = 0x2d;
                num3 = 3;
            }
            else if (this.rbD.Checked)
            {
                str = "250枚金币";
                num = 1;
                num2 = 100;
                num3 = 4;
            }
            else
            {
                str = "3000枚金币";
                num = 1;
                num2 = 0x3e8;
                num3 = 5;
            }
            ROOTUserManager.AddPayOrder(this.intUserID, this.strUserName, 1, strOID, str, num3, num, num2);
            string url = "XBAFinance.aspx?Type=ORDER&Page=1";
            base.Response.Redirect(url);
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.tblBank.Visible = false;
                this.tblJPAY.Visible = false;
                this.tblPPAY.Visible = false;
                this.tblPay.Visible = false;
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.intCoin = (int) userInfoByID["Coin"];
                this.strCoin = Convert.ToString(this.intCoin).Trim();
                this.strUserName = userInfoByID["UserName"].ToString().Trim();
                this.strPassword = userInfoByID["Password"].ToString().Trim();
                this.strIndexHead = BoardItem.GetIndexHead(this.intUserID, this.strUserName, this.strPassword);
                this.strIndexEnd = BoardItem.GetIndexEnd();
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                switch (this.strType)
                {
                    case "PAYBANK":
                        this.tblBank.Visible = true;
                        break;

                    case "JCARD":
                        this.tblJPAY.Visible = true;
                        break;

                    case "PHOTO":
                        this.tblPPAY.Visible = true;
                        break;

                    case "PAY":
                    {
                        this.v_mid = "34107";
                        this.key = "stonehenge20031118";
                        this.v_oid = SessionItem.GetRequest("OID", 1);
                        this.intOrderCategory = SessionItem.GetRequest("OrderCategory", 0);
                        int request = SessionItem.GetRequest("OrderID", 0);
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
                        this.tblPay.Visible = true;
                        break;
                    }
                    default:
                        this.strType = "PAYBANK";
                        this.tblBank.Visible = true;
                        break;
                }
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "web/button_09.gif";
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


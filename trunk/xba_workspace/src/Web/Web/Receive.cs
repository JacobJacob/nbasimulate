namespace Web
{
    using cn.com.chinabank.CBSecurity;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Receive : Page
    {
        private int intUserID;
        protected string key;
        protected string remark1;
        protected string remark2;
        private string strNickName;
        public string strUserName;
        protected string v_amount;
        protected string v_md5str;
        protected string v_moneytype;
        protected string v_oid;
        protected string v_pmode;
        protected string v_pstatus;
        protected string v_pstring;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strUserName = userInfoByID["UserName"].ToString().Trim();
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.v_oid = base.Request["v_oid"];
                this.v_pmode = base.Request["v_pmode"];
                this.v_pstatus = base.Request["v_pstatus"];
                this.v_pstring = base.Request["v_pstring"];
                this.v_amount = base.Request["v_amount"];
                this.v_moneytype = base.Request["v_moneytype"];
                this.remark1 = base.Request["remark1"];
                this.remark2 = base.Request["remark2"];
                this.v_md5str = base.Request["v_md5str"];
                this.key = "stonehenge20031118";
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            bool flag = MD5Util.verifyMD5(this.v_oid + this.v_pstatus + this.v_amount + this.v_moneytype + this.key, this.v_md5str);
            int num = -1;
            try
            {
                num = int.Parse(this.v_pstatus);
                if (!flag)
                {
                    num = -1;
                }
            }
            catch
            {
            }
            if (num == -1)
            {
                base.Response.Redirect("Report.aspx?Parameter=28");
            }
            else if (num == 20)
            {
                DataRow orderRowByOID = ROOTUserManager.GetOrderRowByOID(this.v_oid);
                int num2 = (byte) orderRowByOID["Status"];
                if (num2 == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=29");
                }
                else
                {
                    int num3;
                    switch (((byte) orderRowByOID["OrderCategory"]))
                    {
                        case 1:
                            num3 = 20;
                            break;

                        case 2:
                            num3 = 40;
                            break;

                        case 3:
                            num3 = 100;
                            break;

                        case 4:
                            num3 = 250;
                            break;

                        case 5:
                            num3 = 0xbb8;
                            break;

                        default:
                            base.Response.Redirect("Report.aspx?Parameter=3");
                            return;
                    }
                    ROOTUserManager.AddCoinReceive(this.strUserName, num3, this.v_oid);
                    base.Response.Redirect("Report.aspx?Parameter=29");
                }
            }
            else if (num == 30)
            {
                base.Response.Redirect("Report.aspx?Parameter=210");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
        }
    }
}


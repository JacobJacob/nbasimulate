namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class CheckJNet : Page
    {
        protected string iJNetBillID;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.iJNetBillID = (string) SessionItem.GetRequest("iJNetBillID", 1);
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            DataRow orderRowByOID = ROOTUserManager.GetOrderRowByOID(this.iJNetBillID);
            if (orderRowByOID == null)
            {
                base.Response.Write("iReturn=-1&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iJNetBillID + "&sSign=金币充值失败！");
            }
            else
            {
                int num = (byte) orderRowByOID["Status"];
                if (num == 1)
                {
                    base.Response.Write("iReturn=1&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iJNetBillID + "&sSign=金币充值成功！");
                }
                else
                {
                    base.Response.Write("iReturn=-1&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iJNetBillID + "&sSign=金币充值失败！");
                }
            }
        }
    }
}


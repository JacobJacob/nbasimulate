namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class ReceiveJNet : Page
    {
        protected string iAreaID;
        protected string iBizBillID;
        protected string iCardType;
        protected string iJNetBillID;
        private int intUserID;
        protected string iServerID;
        protected string key;
        protected string sSign;
        public string strUserName;
        protected string sUsername;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.iServerID = SessionItem.GetRequest("iServerID", 1);
            this.iAreaID = SessionItem.GetRequest("iAreaID", 1);
            this.sUsername = SessionItem.GetRequest("sUsername", 1);
            this.iCardType = SessionItem.GetRequest("iCardType", 1);
            this.iJNetBillID = SessionItem.GetRequest("iJNetBillID", 1);
            this.sSign = SessionItem.GetRequest("sSign", 1);
            this.key = "sth21318";
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (StringItem.IsValidSign(this.sUsername + this.iCardType + this.iJNetBillID, this.sSign, this.key))
            {
                string str;
                int num;
                int num2;
                int num3;
                int num4;
                if (this.iCardType == "5")
                {
                    str = "30枚金币";
                    num4 = 30;
                    num = 1;
                    num2 = 1;
                    num3 = 15;
                }
                else
                {
                    str = "60枚金币";
                    num4 = 60;
                    num = 2;
                    num2 = 1;
                    num3 = 30;
                }
                DataRow userRowByUserName = ROOTUserManager.GetUserRowByUserName(this.sUsername);
                if (userRowByUserName != null)
                {
                    this.intUserID = (int) userRowByUserName["UserID"];
                    this.strUserName = this.sUsername;
                    string commandText = "SELECT OrderID FROM ROOT_PayOrder WHERE OID='" + this.iJNetBillID + "'";
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText) != null)
                    {
                        base.Response.Write("iReturn=0&iServerID=0&iAreaID=0&sUsername=" + this.strUserName + "&iCardType=" + this.iCardType + "&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iBizBillID + "&sSign=" + this.sSign + "&sMessage=订单重复充值失败！");
                    }
                    else
                    {
                        ROOTUserManager.AddPayOrder(this.intUserID, this.strUserName, 2, this.iJNetBillID, str, num, num2, num3);
                        if (ROOTUserManager.AddCoinReceive(this.strUserName, num4, this.iJNetBillID))
                        {
                            base.Response.Write("iReturn=1&iServerID=0&iAreaID=0&sUsername=" + this.strUserName + "&iCardType=" + this.iCardType + "&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iBizBillID + "&sSign=" + this.sSign + "&sMessage=充值成功！");
                            base.Response.End();
                        }
                        else
                        {
                            base.Response.Write("iReturn=0&iServerID=0&iAreaID=0&sUsername=" + this.strUserName + "&iCardType=" + this.iCardType + "&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iBizBillID + "&sSign=" + this.sSign + "&sMessage=充值失败！");
                            base.Response.End();
                        }
                    }
                }
                else
                {
                    base.Response.Write("iReturn=0&iServerID=0&iAreaID=0&sUsername=" + this.strUserName + "&iCardType=" + this.iCardType + "&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iBizBillID + "&sSign=" + this.sSign + "&sMessage=未找到相关用户，充值失败！");
                    base.Response.End();
                }
            }
            else
            {
                base.Response.Write("iReturn=0&iServerID=0&iAreaID=0&sUsername=" + this.strUserName + "&iCardType=" + this.iCardType + "&iJNetBillID=" + this.iJNetBillID + "&iBizBillID=" + this.iBizBillID + "&sSign=" + this.sSign + "&sMessage=验证码错误导致充值失败！");
                base.Response.End();
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.Security;
    using System.Web.UI;
    using Web.DBConnection;
    using Web.Helper;

    public class JCardCheck : Page
    {
        protected string iJNetBillID;
        protected string key;

        private DataRow GetOrderRowByOID(string strOID)
        {
            string commandText = "SELECT * FROM ROOT_PayOrder WHERE OID=@OID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@OID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strOID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            this.iJNetBillID = (string) SessionItem.GetRequest("iJNetBillID", 1);
            this.key = "sth21318";
        }

        private void Page_Load(object sender, EventArgs e)
        {
            int num;
            int num2;
            DataRow orderRowByOID = this.GetOrderRowByOID(this.iJNetBillID);
            if (orderRowByOID != null)
            {
                num = (byte) orderRowByOID["Status"];
            }
            else
            {
                num = 0;
            }
            if (num == 1)
            {
                num2 = 1;
            }
            else
            {
                num2 = -1;
            }
            string iJNetBillID = this.iJNetBillID;
            string str2 = this.iJNetBillID;
            string str3 = FormsAuthentication.HashPasswordForStoringInConfigFile(string.Concat(new object[] { num2, iJNetBillID, str2, this.key }), "md5");
            base.Response.Write(string.Concat(new object[] { "iReturn=", num2, "&iJNetBillID=", iJNetBillID, "&iBizBillID=", str2, "&sSign=", str3 }));
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TicketSold : Page
    {
        private int intUserID;
        public string strList;

        private void GetList()
        {
            DataTable devTicketSoldTable = BTPDevMatchManager.GetDevTicketSoldTable(BTPClubManager.GetClubIDByUserID(this.intUserID));
            if (devTicketSoldTable == null)
            {
                this.strList = "<tr class='BarContent' align='center'><td colspan='3'>暂无</td></tr>";
            }
            else
            {
                foreach (DataRow row in devTicketSoldTable.Rows)
                {
                    int num = (int) row["Round"];
                    int num2 = (byte) row["TicketPrice"];
                    int num3 = (int) row["TicketSold"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' align='center'><td height='25'><font color='#7B1F76'>", num, "</font></td><td>", num2, "</td><td>", num3, "</td></tr>" });
                }
            }
        }

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
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.GetList();
            }
        }
    }
}


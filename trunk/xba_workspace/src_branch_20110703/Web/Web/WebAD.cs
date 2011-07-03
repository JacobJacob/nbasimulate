namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class WebAD : Page
    {
        public string strBlockSec;
        public string strWebAD;

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
            DataRow webADRow = BTPWebADManager.GetWebADRow();
            this.strWebAD = webADRow["WebAD"].ToString();
            this.strBlockSec = webADRow["BlockSec"].ToString();
            bool flag = (bool) webADRow["OpenTag"];
            int intUserID = SessionItem.CheckLogin(1);
            if (intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
                int num2 = (int) onlineRowByUserID["PayType"];
                switch (((int) onlineRowByUserID["Category"]))
                {
                    case 0:
                    case 4:
                        base.Response.Redirect("RookieMain_M.aspx");
                        return;

                    case 3:
                        base.Response.Redirect("RookieMain_M.aspx");
                        return;
                }
                if ((!((bool) webADRow["MemberBlock"]) && (num2 == 1)) || !flag)
                {
                    base.Response.Redirect("Main.aspx");
                }
            }
        }
    }
}


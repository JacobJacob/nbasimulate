namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Main_S : Page
    {
        private int intCategory;
        private int intUserID;
        public string strCenterURL;
        private string strGuideCode;
        public string strRightURL;
        public string strSayScript;
        private string strType;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                if (((int) onlineRowByUserID["ClubID3"]) == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.strType = (string) SessionItem.GetRequest("Type", 1);
            this.strGuideCode = BTPAccountManager.GetGuideCode(this.intUserID);
            int index = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID])).GetIndex("0");
            if (this.strType != "")
            {
                switch (this.strType)
                {
                    case "STAFF":
                        if ((index != -1) && (index != 2))
                        {
                            base.Response.Write("<script>window.top.Main.location='Main_I.aspx';</script>");
                            return;
                        }
                        this.strCenterURL = "StaffManage.aspx?Type=STREET";
                        this.strRightURL = "Staff.aspx?Type=0&Grade=0&Page=1&Refresh=0";
                        return;

                    case "FINANCE":
                        if (index != -1)
                        {
                            base.Response.Write("<script>window.top.Main.location='Main_I.aspx';</script>");
                            return;
                        }
                        SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                        this.strCenterURL = "TFinance.aspx?Type=TURN&Kind=SHOW&Page=1";
                        this.strRightURL = "TUFinance.aspx?Status=0&Page=1";
                        return;
                }
                this.strCenterURL = "Temp_Center.aspx";
                this.strRightURL = "Temp_Right.aspx";
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }
    }
}


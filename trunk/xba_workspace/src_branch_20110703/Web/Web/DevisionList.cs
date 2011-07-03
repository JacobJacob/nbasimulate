namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class DevisionList : Page
    {
        private int intUserID;
        public string strList;

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
                this.strList = "";
            }
        }
    }
}


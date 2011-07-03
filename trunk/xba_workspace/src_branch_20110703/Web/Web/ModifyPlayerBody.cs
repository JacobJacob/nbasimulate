namespace Web
{
    using ServerManage;
    using System;
    using System.Web.UI;

    public class ModifyPlayerBody : Page
    {
        public string strCategory;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


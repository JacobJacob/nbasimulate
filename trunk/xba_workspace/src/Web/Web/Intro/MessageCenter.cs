namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class MessageCenter : Page
    {
        protected HtmlTable tbFriendList;
        protected HtmlTable tbMsgList;
        protected HtmlTable tbSearch;

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
            this.tbMsgList.Visible = false;
            this.tbSearch.Visible = false;
            this.tbFriendList.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "MSGLIST":
                    this.tbMsgList.Visible = true;
                    return;

                case "SEARCH":
                    this.tbSearch.Visible = true;
                    return;

                case "FRIENDLIST":
                    this.tbFriendList.Visible = true;
                    return;
            }
            this.tbMsgList.Visible = true;
        }
    }
}


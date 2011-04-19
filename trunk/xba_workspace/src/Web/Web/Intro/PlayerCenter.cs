namespace Web.Intro
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.Helper;

    public class PlayerCenter : Page
    {
        protected HtmlTable PlayerNew;
        protected HtmlTable tblContract;
        protected HtmlTable tblPlayerTran;
        protected HtmlTable tblSearch;
        protected HtmlTable tbNoNickInfo;
        protected HtmlTable tbPlayer3;
        protected HtmlTable tbPlayer3To5;
        protected HtmlTable tbPlayer5;

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
            this.tbPlayer3.Visible = false;
            this.tbPlayer5.Visible = false;
            this.tbPlayer3To5.Visible = false;
            this.tbNoNickInfo.Visible = false;
            this.PlayerNew.Visible = false;
            this.tblPlayerTran.Visible = false;
            //this.tblSearch.Visible = false;
            //this.tblContract.Visible = false;
            switch (SessionItem.GetRequest("Type", 1).ToString().Trim())
            {
                case "PLAYER3":
                    this.tbPlayer3.Visible = true;
                    return;

                case "PLAYER5":
                    this.tbPlayer5.Visible = true;
                    return;

                case "PLAYER3TO5":
                    this.tbPlayer3To5.Visible = true;
                    return;

                case "NONICKINFO":
                    this.tbNoNickInfo.Visible = true;
                    return;

                case "NEWPLAYER":
                    this.PlayerNew.Visible = true;
                    return;

                case "PLAYERTRAIN":
                    this.tblPlayerTran.Visible = true;
                    return;

                case "SEARCH":
                    this.tblSearch.Visible = true;
                    return;

                case "CONTRACT":
                    this.tblContract.Visible = true;
                    return;
            }
            this.tbPlayer3.Visible = true;
        }
    }
}


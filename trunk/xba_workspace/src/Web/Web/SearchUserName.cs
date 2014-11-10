namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class SearchUserName : Page
    {
        private string strNickName;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strUserName = SessionItem.GetRequest("UserName", 1).ToString().Trim();
            this.strNickName = SessionItem.GetRequest("NickName", 1).ToString().Trim();
            if (StringItem.IsValidLogin(this.strUserName))
            {
                if ((StringItem.GetStrLength(this.strUserName) >= 4) && (StringItem.GetStrLength(this.strUserName) <= 0x10))
                {
                    this.strNickName = StringItem.GetValidWords(this.strNickName);
                    if (StringItem.IsValidName(this.strNickName, 2, 0x10))
                    {
                        switch (ROOTUserManager.HasUserNameNickName(this.strUserName, this.strNickName))
                        {
                            case 1:
                                base.Response.Redirect("Report.aspx?Parameter=446");
                                goto Label_012E;

                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=447");
                                goto Label_012E;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=449");
                                goto Label_012E;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=483");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=453");
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=448");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=454");
            }
        Label_012E:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


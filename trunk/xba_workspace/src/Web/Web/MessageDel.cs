namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class MessageDel : Page
    {
        private int intMessageID;
        private int intUserID;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intMessageID = (int) SessionItem.GetRequest("MessageID", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            int request = (int) SessionItem.GetRequest("DType", 0);
            int intType = (int) SessionItem.GetRequest("Type", 0);
            if (request == 1)
            {
                int num3 = (int) SessionItem.GetRequest("Page", 0);
                if (BTPUnionManager.DelUnionMessage(this.intUserID, this.intMessageID) == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=21b!Type.UNIONMSG^Page." + num3);
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=22b!Type.UNIONMSG^Page." + num3);
                }
            }
            else if (BTPMessageManager.DeleteMessage(this.intUserID, this.intMessageID, intType))
            {
                if (intType == 2)
                {
                    base.Response.Redirect("Report.aspx?Parameter=21!Type.SENDMSGMY^Page.1");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=21!Type.MSGLIST^Page.1");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=22");
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.Helper;

    public class RookieEndDevCreate : Page
    {
        protected ImageButton btnNext;
        private int intRookieOpIndex;
        private int intUserID;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 3);
            if (intRookieOpIndex != 3)
            {
                base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
            }
            else
            {
                base.Response.Write("<script>window.top.Main.location=\"RookieMain_P.aspx?Type=ASSIGNCLUB\"</script>");
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
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
                this.intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (this.intRookieOpIndex == -1)
                {
                    base.Response.Write("<script>window.top.location=\"Main.aspx\";</script>");
                }
                else if (this.intRookieOpIndex != 3)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(this.intRookieOpIndex) + "\";</script>");
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
        }
    }
}


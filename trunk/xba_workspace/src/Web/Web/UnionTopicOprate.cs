namespace Web
{
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class UnionTopicOprate : Page
    {
        private int intTopicID;
        private int intUnionID;
        private int intUserID;
        private string strOp;
        private string strType;

        private void CancelElite()
        {
            BTPUnionBBSManager.CancelUnionEliteByID(this.intTopicID);
        }

        private void CancelOnTop()
        {
            BTPUnionBBSManager.CancelUnionOnTopByID(this.intTopicID);
        }

        private void DelTopic()
        {
            BTPUnionBBSManager.DeleteUnionTopicByID(this.intTopicID);
        }

        private void InitializeComponent()
        {
            BTPUnionBBSManager.GetUnionBoardRowByUnionID(this.intUnionID);
            if (BoardItem.IsUnionBoardMaster(this.intUserID, this.intUnionID))
            {
                switch (this.strOp)
                {
                    case "DELETE":
                        this.DelTopic();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=50u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "ONTOP":
                        this.SetOnTop();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=51u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "CANCELONTOP":
                        this.CancelOnTop();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=52u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "ELITE":
                        this.SetElite();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=53u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "CANCELELITE":
                        this.CancelElite();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=54u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "LOCK":
                        this.SetLock();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=55u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;

                    case "UNLOCK":
                        this.SetUnLock();
                        base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=56u!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" }));
                        goto Label_0305;
                }
                base.Response.Redirect("Report.aspx?Parameter=57u");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=57");
                return;
            }
        Label_0305:
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
                this.intTopicID = (int) SessionItem.GetRequest("TopicID", 0);
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.strOp = (string) SessionItem.GetRequest("OP", 1);
                this.intUnionID = (int) SessionItem.GetRequest("UnionID", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetElite()
        {
            BTPUnionBBSManager.SetUnionEliteByID(this.intTopicID);
        }

        private void SetLock()
        {
            BTPUnionBBSManager.SetUnionLockByID(this.intTopicID);
        }

        private void SetOnTop()
        {
            BTPUnionBBSManager.SetUnionOnTopByID(this.intTopicID);
        }

        private void SetUnLock()
        {
            BTPUnionBBSManager.SetUnionUnLockByID(this.intTopicID);
        }
    }
}


﻿namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Logout : Page
    {
        private int intUserID;

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
            string str3;
            string str4;
            int request = (int) SessionItem.GetRequest("GameCategory", 0);
            string str = (string) SessionItem.GetRequest("Type", 1);
            string str2 = (string) SessionItem.GetRequest("JumpURL", 1);
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID > 0)
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                str3 = onlineRowByUserID["UserName"].ToString();
                str4 = onlineRowByUserID["Password"].ToString();
            }
            else
            {
                this.intUserID = -1;
                str3 = "";
                str4 = "";
            }
            if (str == "OnlyJump")
            {
                base.Response.Redirect(ServerItem.ToOtherServerURL(request, str3, str4, "URL=" + str2));
            }
            else
            {
                if (this.intUserID > 0)
                {
                    if (str == "MemberCenter")
                    {
                        SessionItem.SetLogout(this.intUserID);
                        base.Response.Redirect(ServerItem.ToOtherServerURL(0, str3, str4, "URL=MemberCenter.aspx"));
                        return;
                    }
                    SessionItem.SetLogout(this.intUserID);
                    if (10 > RandomItem.rnd.Next(0, 100))
                    {
                        GC.Collect();
                    }
                }
                base.Response.Redirect("Report.aspx?Parameter=13");
            }
        }
    }
}


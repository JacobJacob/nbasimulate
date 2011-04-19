namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class AssMain : Page
    {
        private int intPayType;
        private int intUserID;
        public string strIframe;
        public string strMsg;
        public string strNickName;
        public string strTimeName;
        public string strType;

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
                if ((DateTime.Now > DateTime.Today.AddHours(6.0)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
                {
                    this.strTimeName = "时间表";
                }
                else
                {
                    this.strTimeName = "第一联赛排名";
                }
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                int num = (int) onlineRowByUserID["Category"];
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                if ((num != 3) && (this.strType != "CHOOSECLUB"))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string strUserName = onlineRowByUserID["UserName"].ToString();
                    string strPassword = onlineRowByUserID["Password"].ToString();
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    string str3 = onlineRowByUserID["DiskURL"].ToString();
                    this.intPayType = (int) onlineRowByUserID["PayType"];
                    string str4 = DBLogin.URLString(0) + str3 + "Face.png";
                    int num2 = RandomItem.rnd.Next(0, 10);
                    string str5 = "";
                    if (this.intPayType == 1)
                    {
                        str5 = "<font color=red >&nbsp;&nbsp;[会员]</font>";
                    }
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='40' align='center' width='40%'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str4, "?RndID=", num2, ");width:37px;height:40px'></div></td><td width='60%'>　", BoardItem.GetMsgSettingArea(strUserName, strPassword), "</td></tr><tr><td height='20' align='center' colspan=2><font class='ForumTime'>", this.strNickName, str5, "</font></td></tr><tr><td height='27' align='center' colspan='2'>" });
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "<a href='" + DBLogin.URLString(40) + "MemberCenter.aspx'><img src='" + SessionItem.GetImageURL() + "Button_portmanager.gif' width='76' height='24' border='0'></a>&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                    if (this.strType == "CHOOSECLUB")
                    {
                        this.strIframe = "<td height=\"443\" valign=\"top\"><iframe name=\"Main\" src=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=CHOOSECLUB\" frameBorder=\"0\">";
                    }
                    else
                    {
                        this.strIframe = "<td height=\"443\" valign=\"top\"><iframe name=\"Main\" src=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=ASSIGNCLUB\" frameBorder=\"0\">";
                    }
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


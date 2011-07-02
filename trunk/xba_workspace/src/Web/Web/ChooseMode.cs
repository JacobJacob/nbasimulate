namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ChooseMode : Page
    {
        protected ImageButton btnRegClub;
        protected HtmlInputHidden hidClothes;
        protected HtmlInputHidden hidLogo;
        private int intUserID;
        public StringBuilder sbList = new StringBuilder("");
        public string strErrClubName;
        public string strMsg;
        public string strNickName;
        public string strTimeName;
        protected TextBox tbClubName;
        protected HtmlTable tblLogin_02;

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
                string pToEncrypt = onlineRowByUserID["UserName"].ToString().Trim();
                string str2 = onlineRowByUserID["Password"].ToString().Trim();
                int num = (int) onlineRowByUserID["Category"];
                string strQQ = onlineRowByUserID["QQ"].ToString().Trim();
                if (((num != 0) && (num != 4)) && (num != 6))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string str4;
                    string str5;
                    string str6;
                    bool flag;
                    string str7;
                    DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intUserID);
                    if (userRowByUserID != null)
                    {
                        str4 = userRowByUserID["City"].ToString().Trim();
                        str5 = userRowByUserID["Province"].ToString().Trim();
                        str6 = userRowByUserID["Birth"].ToString().Trim();
                        //userRowByUserID.Close();
                    }
                    else
                    {
                        str5 = "";
                        str4 = "";
                        str6 = "";
                       // userRowByUserID.Close();
                    }
                    DataRow gameRow = BTPGameManager.GetGameRow();
                    if (gameRow != null)
                    {
                        flag = (bool) gameRow["CantReg"];
                    }
                    else
                    {
                        flag = true;
                    }
                    if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) == null)
                    {
                        if (flag)
                        {
                            return;
                        }
                        string strUserName = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
                        string strPassword = StringItem.MD5Encrypt(str2, Global.strMD5Key);
                        this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                        bool blnSex = (bool) onlineRowByUserID["Sex"];
                        str7 = onlineRowByUserID["DiskURL"].ToString().Trim();
                        BTPAccountManager.AddFullAccount(this.intUserID, pToEncrypt, this.strNickName, str2, blnSex, 0, str7 + "/", str6, str5, str4, strQQ);
                        if (ROOTUserGameManager.AddUserGame(this.intUserID, ServerParameter.intGameCategory, DBLogin.GameNameEnglish(ServerParameter.intGameCategory)) == 2)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=10116");
                            return;
                        }
                        SessionItem.SetSelfLogin(strUserName, strPassword, false);
                    }
                    onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    str7 = onlineRowByUserID["DiskURL"].ToString();
                    string str10 = DBLogin.URLString(0) + str7 + "Face.png";
                    int num3 = RandomItem.rnd.Next(0, 10);
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='60' align='center'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str10, "?RndID=", num3, ");width:37px;height:40px'></div><font class='ForumTime'>", this.strNickName, "</font></td></tr><tr><td height='27' align='center'>" });
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "<a href='" + ServerItem.ToOtherServerURL(0, pToEncrypt, str2, "URL=MemberCenter.aspx") + "'><img src='" + SessionItem.GetImageURL() + "Button_portmanager.gif' width='76' height='24' border='0'></a>&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                    this.InitializeComponent();
                    base.OnInit(e);
                    string request = (string) SessionItem.GetRequest("Type", 1);
                    if (request == "NEXT")
                    {
                    }
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


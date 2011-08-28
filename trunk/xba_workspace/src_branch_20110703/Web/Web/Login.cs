namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Login : Page
    {
        protected ImageButton btnLogin;
        protected ImageButton btnReg;
        public string strLoginADScript;
        public string strMsg;
        public string strNewsList;
        public string strRMList;
        public string strServerName;
        public string strTimeName;
        public string strTopicList;
        protected HtmlTable tblLogin_01;
        protected HtmlTable tblLogin_02;
        protected TextBox tbPassword;
        protected TextBox tbUserName;

        private void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            string text = this.tbUserName.Text;
            string pToEncrypt = this.tbPassword.Text;
            if (ServerParameter.strCopartner == "XBA")
            {
                text = StringItem.MD5Encrypt(text, Global.strMD5Key);
                pToEncrypt = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
                if (SessionItem.SetSelfLogin(text, pToEncrypt, true) > 0)
                {
                    base.Response.Redirect("Login.aspx");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=10");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=10");
            }
        }

        private void btnReg_Click(object sender, ImageClickEventArgs e)
        {

            base.Response.Redirect(SessionItem.GetMainUrl() + "Register.aspx");

        }

        private void InitializeComponent()
        {
            this.btnLogin.Click += new ImageClickEventHandler(this.btnLogin_Click);
            this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strServerName = DBLogin.GameNameChinese(ServerParameter.intGameCategory);
            if (ServerParameter.intGameCategory != 4)
            {
                DataRow gameADRow = BTPGameADManager.GetGameADRow();
                if (gameADRow != null)
                {
                    string str = gameADRow["LoginAD01"].ToString().Trim();
                    string str2 = gameADRow["LoginAD02"].ToString().Trim();
                    if (str == null)
                    {
                        str = "";
                    }
                    if (str2 == null)
                    {
                        str2 = "";
                    }
                    this.strLoginADScript = str + "&nbsp;" + str2;
                }
                else
                {
                    this.strLoginADScript = "";
                }
            }
            else
            {
                this.strLoginADScript = "";
            }
            if (!base.IsPostBack)
            {
                int intUserID = SessionItem.CheckLogin(1);
                if (intUserID == -1)
                {
                    this.tblLogin_01.Visible = true;
                    //this.tblLogin_02.Visible = false;
                    this.btnLogin.ImageUrl = SessionItem.GetImageURL() + "usa_5.gif";
                    this.btnReg.ImageUrl = SessionItem.GetImageURL() + "usa_2.gif";
                }
                else
                {
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
                    onlineRowByUserID["UserName"].ToString();
                    onlineRowByUserID["Password"].ToString();
                    string str3 = onlineRowByUserID["NickName"].ToString();
                    string str4 = onlineRowByUserID["DiskURL"].ToString();
                    string str5 = DBLogin.URLString(0) + str4 + "Face.png";
                    int num2 = RandomItem.rnd.Next(0, 10);
                    int num3 = (int) onlineRowByUserID["Category"];
                    if ((num3 != 5) && (BTPAccountManager.GetAccountRowByUserID(intUserID)["RookieOp"].ToString().Trim().IndexOf("0") != -1))
                    {
                        base.Response.Redirect("RookieMain_M.aspx");
                        return;
                    }
                    this.tblLogin_01.Visible = false;
                    //this.tblLogin_02.Visible = true;
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='40' align='center' width='40%'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str5, "?RndID=", num2, ");width:37px;height:40px'></div></td><td width='60%'>　<a href='Main_P.aspx?Tag=", intUserID, "&Type=MODIFYCLUB' target='Main'><img src='", SessionItem.GetImageURL(), "Setting.gif' width='16' height='16' border='0'></a></td></tr><tr><td height='20' align='center' colspan=2><font class='ForumTime'>", str3, "</font></td></tr><tr><td height='27' align='center' colspan='2'>" });
                    if ((((int) onlineRowByUserID["Category"]) == 0) || (((int) onlineRowByUserID["Category"]) == 4))
                    {
                        this.strMsg = this.strMsg + "<a href='RegClub.aspx?Type=NEXT'><img src='" + SessionItem.GetImageURL() + "Button_09.gif' width='40' height='24' border='0'></a>";
                    }
                    else if (((int) onlineRowByUserID["Category"]) == 3)
                    {
                        this.strMsg = this.strMsg + "<a href='AssMain.aspx'><img src='" + SessionItem.GetImageURL() + "Button_08.gif' width='40' height='24' border='0'></a>";
                    }
                    else
                    {
                        base.Response.Redirect("Main.aspx");
                    }
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "&nbsp;&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;&nbsp;<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                }
            }
            if ((DateTime.Now > DateTime.Today.AddHours((double) Global.intStartUpdate)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
            {
                this.strTimeName = "时间表";
            }
            else
            {
                this.strTimeName = "第一联赛排名";
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


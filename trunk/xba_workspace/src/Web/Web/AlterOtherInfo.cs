namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class AlterOtherInfo : Page
    {
        private bool blnSex;
        protected Button btnMNickName;
        protected Button btnMPassword;
        protected Button btnMSexFace;
        protected DropDownList ddlBody;
        protected HtmlInputHidden Face;
        private int intCategory;
        private int intPayType;
        public int intUserID;
        protected RadioButton rbFemale;
        protected RadioButton rbMale;
        public string strBoyLimit;
        public string strCategory;
        public string strDefaultFace;
        private string strDiskURL;
        public string strErrEmail;
        public string strErrNewPwd;
        public string strErrNickName;
        public string strErrOldPwd;
        public string strErrRePwd;
        public string strFace;
        public string strFaceLimit;
        public string strGirlLimit;
        public string strImage = "";
        private string strKind;
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        private string strPassword;
        public string strScript;
        public string strSexWord;
        private string strType;
        public string strUserName;
        protected TextBox tbEmail;
        protected HtmlTable tblNickName;
        protected HtmlTable tblPassword;
        protected HtmlTable tblSexFace;
        protected TextBox tbNewPwd;
        protected TextBox tbNickName;
        protected TextBox tbOldPwd;
        protected TextBox tbRePwd;

        private void btnMNickName_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string validWords = StringItem.GetValidWords(this.tbNickName.Text);
            if (!StringItem.IsValidName(validWords, 2, 0x10))
            {
                this.strErrNickName = "<font color='#FF0000'>*昵称填写错误！</font>";
                flag = true;
            }
            else if (ROOTUserManager.HasNickName(validWords))
            {
                this.strErrNickName = "<font color='#FF0000'>*所填写昵称已存在，请重新填写！</font>";
                flag = true;
            }
            else if (validWords == "")
            {
                this.strErrNickName = "<font color='#FF0000'>*所填写昵称不能为空，请重新填写！</font>";
                flag = true;
            }
            if (!flag)
            {
                ROOTUserManager.ChangeNickName(this.intUserID, validWords);
                BTPAccountManager.ChangeNickName(this.intUserID, validWords);
                SessionItem.SetSomebodyLogin(this.intUserID);
                base.Response.Redirect("AlterOtherInfo.aspx?Type=NICKNAME&Kind=YES");
            }
        }

        private void btnMPassword_Click(object sender, EventArgs e)
        {
            string text = this.tbEmail.Text;
            string strIn = this.tbOldPwd.Text;
            string str3 = this.tbNewPwd.Text;
            string str4 = this.tbRePwd.Text;
            bool flag = false;
            if (!StringItem.IsValidEmail(text))
            {
                this.strErrEmail = "<font color='#FF0000'>*Email填写错误！</font>";
                flag = true;
            }
            else if (!StringItem.IsValidLogin(strIn))
            {
                this.strErrOldPwd = "<font color='#FF0000'>*密码填写错误！</font>";
                flag = true;
            }
            else if (!StringItem.IsValidLogin(str3))
            {
                this.strErrNewPwd = "<font color='#FF0000'>*密码填写错误！</font>";
                flag = true;
            }
            else if (str3 != str4)
            {
                this.strErrRePwd = "<font color='#FF0000'>*两次密码填写不同！</font>";
                flag = true;
            }
            if (!flag)
            {
                if (!DBLogin.CanConn(0))
                {
                    base.Response.Redirect("Report.aspx?Parameter=10114");
                }
                else
                {
                    switch (ROOTUserManager.ChangePS(this.intUserID, text, strIn, str3))
                    {
                        case 1:
                            if (this.intCategory > 0)
                            {
                                DataTable userGameTableByUserID = ROOTUserGameManager.GetUserGameTableByUserID(this.intUserID);
                                if (userGameTableByUserID != null)
                                {
                                    foreach (DataRow row in userGameTableByUserID.Rows)
                                    {
                                        int intCategory = (int) row["Category"];
                                        try
                                        {
                                            BTPAccountManager.ChangePassword(this.intUserID, str3, DBLogin.ConnString(intCategory));
                                            continue;
                                        }
                                        catch
                                        {
                                            base.Response.Write("您在" + DBLogin.GameNameChinese(intCategory) + "的密码修改失败！系统将会在1个工作日内将密码同步，请届时再使用新密码登录。");
                                            base.Response.End();
                                            return;
                                        }
                                    }
                                }
                            }
                            base.Response.Redirect("AlterOtherInfo.aspx?Type=PASSWORD&Kind=YES");
                            return;

                        case 2:
                            this.strErrOldPwd = "<font color='#FF0000'>*所写密码与原密码不同！</font>";
                            flag = true;
                            return;

                        case 3:
                            this.strErrEmail = "<font color='#FF0000'>*所写Email与原Email不同！</font>";
                            flag = true;
                            return;
                    }
                }
            }
        }

        private void btnMSexFace_Click(object sender, EventArgs e)
        {
            string str2;
            string str3;
            bool blnSex = this.rbFemale.Checked;
            string strFace = this.Face.Value;
            DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intUserID);
            if (userRowByUserID != null)
            {
                str2 = userRowByUserID["UserName"].ToString().Trim();
                str3 = userRowByUserID["Password"].ToString().Trim();
                byte num1 = (byte) userRowByUserID["Category"];
                //userRowByUserID.Close();
            }
            else
            {
                //userRowByUserID.Close();
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            bool flag2 = DBLogin.CanConn(0);
            if (flag2)
            {
                ROOTUserManager.ChangeSexFace(this.intUserID, blnSex, strFace);
            }
            if (!flag2)
            {
                base.Response.Redirect("Report.aspx?Parameter=10114");
            }
            else
            {
                DataTable userGameTableByUserID = ROOTUserGameManager.GetUserGameTableByUserID(this.intUserID);
                if (userGameTableByUserID != null)
                {
                    foreach (DataRow row in userGameTableByUserID.Rows)
                    {
                        int intCategory = (int) row["Category"];
                        try
                        {
                            BTPAccountManager.ChangeSex(this.intUserID, blnSex, DBLogin.ConnString(intCategory));
                            continue;
                        }
                        catch
                        {
                            base.Response.Write("您在" + DBLogin.GameNameChinese(intCategory) + "的性别头像修改失败！系统将会在1个工作日内将密码同步。");
                            base.Response.End();
                            return;
                        }
                    }
                }
                string strUserName = StringItem.MD5Encrypt(str2, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(str3, Global.strMD5Key);
                SessionItem.SetSelfLogin(strUserName, strPassword, false);
                ServerItem.ToOtherServer(0, str2, str3, "URL=AlterFace.aspx!GameCategory=" + ServerParameter.intGameCategory);
            }
        }

        private void InitializeComponent()
        {
            if (this.strType == "NICKNAME")
            {
                base.Response.Redirect("Report.aspx?Parameter=4007");
            }
            else
            {
                switch (this.strType)
                {
                    case "PASSWORD":
                        this.tblPassword.Visible = true;
                        if (this.strKind == "NO")
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 修改密码时必须确认原始密码和Email。";
                        }
                        else
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 密码修改成功，下次使用新密码登录！";
                        }
                        this.btnMPassword.Click += new EventHandler(this.btnMPassword_Click);
                        break;

                    case "NICKNAME":
                        if (this.intPayType == 0)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=18");
                            return;
                        }
                        this.tblNickName.Visible = true;
                        if (this.strKind == "NO")
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 您是尊贵的付费玩家，可以修改昵称。";
                        }
                        else
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 昵称修改成功！";
                        }
                        this.btnMNickName.Click += new EventHandler(this.btnMNickName_Click);
                        break;

                    case "SEXFACE":
                        this.tblSexFace.Visible = true;
                        if (this.strKind == "NO")
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 付费玩家可以选择更多更个性的头像。";
                        }
                        else
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 您的头像已经修改成功！";
                        }
                        this.btnMSexFace.Click += new EventHandler(this.btnMSexFace_Click);
                        this.SetSexFace();
                        break;

                    default:
                        this.tblPassword.Visible = true;
                        if (this.strKind == "NO")
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 修改密码时必须确认原始密码和Email。";
                        }
                        else
                        {
                            this.strMsg = "<a href='Forum.aspx'>论坛</a> | <a href='AlterInfo.aspx'>设置</a> | 您的密码已经修改成功！";
                        }
                        this.btnMPassword.Click += new EventHandler(this.btnMPassword_Click);
                        break;
                }
                base.Load += new EventHandler(this.Page_Load);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow userInfoByID;
                bool flag;
                this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strUserName = onlineRowByUserID["UserName"].ToString();
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intPayType = (int) onlineRowByUserID["PayType"];
                this.blnSex = (bool) onlineRowByUserID["Sex"];
                this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
                try
                {
                    userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                    flag = true;
                }
                catch
                {
                    userInfoByID = null;
                    flag = false;
                }
                if (!flag)
                {
                    base.Response.Redirect("Report.aspx?Parameter=10114");
                }
                else
                {
                    this.strFace = userInfoByID["Face"].ToString().Trim();
                    this.strDiskURL = userInfoByID["DiskURL"].ToString().Trim();
                    this.intCategory = (byte) userInfoByID["Category"];
                    this.tblNickName.Visible = false;
                    this.tblPassword.Visible = false;
                    this.tblSexFace.Visible = false;
                    this.strType = (string) SessionItem.GetRequest("Type", 1);
                    this.strKind = (string) SessionItem.GetRequest("Kind", 1);
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
            this.SetScript();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }

        private void SetScript()
        {
            if (this.strType == "SEXFACE")
            {
                this.strScript = "PageLoad();";
            }
            else
            {
                this.strScript = "";
            }
        }

        private void SetSexFace()
        {
            if (!base.IsPostBack)
            {
                if (this.blnSex)
                {
                    this.rbFemale.Checked = true;
                    this.rbMale.Checked = false;
                    this.strDefaultFace = SessionItem.GetImageURL() + "Face/Girl/Default.png";
                    this.strSexWord = "Girl";
                    if (this.intPayType == 0)
                    {
                        this.strFaceLimit = "1|1|7|2|15|6|2|27|1";
                    }
                    else
                    {
                        this.strFaceLimit = "1|3|7|2|15|6|2|47|1";
                    }
                    this.strGirlLimit = this.strFaceLimit;
                    if (this.intPayType == 0)
                    {
                        this.strFaceLimit = "1|5|21|2|16|13|2|27|5";
                    }
                    else
                    {
                        this.strFaceLimit = "1|15|21|2|16|13|2|61|5";
                    }
                    this.strBoyLimit = this.strFaceLimit;
                }
                else
                {
                    this.rbFemale.Checked = false;
                    this.rbMale.Checked = true;
                    this.strDefaultFace = SessionItem.GetImageURL() + "Face/Boy/Default.png";
                    this.strSexWord = "Boy";
                    if (this.intPayType == 0)
                    {
                        this.strFaceLimit = "1|1|7|2|15|6|2|27|1";
                    }
                    else
                    {
                        this.strFaceLimit = "1|3|7|2|15|6|2|40|1";
                    }
                    this.strGirlLimit = this.strFaceLimit;
                    if (this.intPayType == 0)
                    {
                        this.strFaceLimit = "1|5|21|2|16|13|2|27|5";
                    }
                    else
                    {
                        this.strFaceLimit = "1|15|21|2|16|13|2|44|5";
                    }
                    this.strBoyLimit = this.strFaceLimit;
                }
                DataView view = new DataView(DDLItem.GetUserFaceItem(), "ID<>'0'", "", DataViewRowState.CurrentRows);
                this.ddlBody.DataSource = view;
                this.ddlBody.DataTextField = "Name";
                this.ddlBody.DataValueField = "ID";
                this.ddlBody.DataBind();
                this.Face.Value = this.strFace;
            }
        }
    }
}


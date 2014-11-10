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

    public class AlterOtherInfo : Page
    {
        private bool blnSex;
        protected Button btnMEmail;
        protected Button btnMNickName;
        protected Button btnMPassword;
        protected Button btnMSexFace;
        protected DateTime dateLastChangeNickname;
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
        public string strEmail;
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
        protected HtmlTable tblEmail;
        protected HtmlTable tblNickName;
        protected HtmlTable tblPassword;
        protected HtmlTable tblSexFace;
        protected TextBox tbNewPwd;
        protected TextBox tbNickName;
        protected TextBox tbOldPwd;
        protected TextBox tbRePwd;

        private void btnMEmail_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string text = this.tbEmail.Text;
            if (!StringItem.IsValidEmail(text))
            {
                this.strErrEmail = "<font color='#FF0000'>*Email填写错误！</font>";
                flag = true;
            }
            if (!flag)
            {
                BTPAccountManager.UpdateEmail(this.intUserID, text);
                SessionItem.SetSomebodyLogin(this.intUserID);
                base.Response.Redirect("AlterOtherInfo.aspx?Type=EMAIL&Kind=YES");
            }
        }

        private void btnMNickName_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string validWords = StringItem.GetValidWords(this.tbNickName.Text);
            if (!StringItem.IsValidName(validWords, 2, 0x10))
            {
                this.strErrNickName = "<font color='#FF0000'>*昵称填写错误！</font>";
                flag = true;
            }
            else if (BTPAccountManager.HasNickName(validWords))
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
                BTPAccountManager.ChangeNickName(this.intUserID, validWords);
                SessionItem.SetSomebodyLogin(this.intUserID);
                base.Response.Redirect("AlterOtherInfo.aspx?Type=NICKNAME&Kind=YES");
            }
        }

        private void btnMPassword_Click(object sender, EventArgs e)
        {
            string text = this.tbOldPwd.Text;
            string strIn = this.tbNewPwd.Text;
            string str3 = this.tbRePwd.Text;
            bool flag = false;
            if (!StringItem.IsValidLogin(text))
            {
                this.strErrOldPwd = "<font color='#FF0000'>*密码填写错误！</font>";
                flag = true;
            }
            else if (!StringItem.IsValidLogin(strIn))
            {
                this.strErrNewPwd = "<font color='#FF0000'>*密码填写错误！</font>";
                flag = true;
            }
            else if (strIn != str3)
            {
                this.strErrRePwd = "<font color='#FF0000'>*两次密码填写不同！</font>";
                flag = true;
            }
            if (!flag)
            {
                switch (BTPAccountManager.ChangePassword(this.intUserID, text, strIn))
                {
                    case 1:
                        base.Response.Redirect("AlterOtherInfo.aspx?Type=PASSWORD&Kind=YES");
                        return;

                    case 2:
                        this.strErrOldPwd = "<font color='#FF0000'>*所写密码与原密码不同！</font>";
                        flag = true;
                        break;

                    default:
                        return;
                }
            }
        }

        private void btnMSexFace_Click(object sender, EventArgs e)
        {
            string str;
            string str2;
            bool blnSex = this.rbFemale.Checked;
            string strFace = this.Face.Value;
            DataRow userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intUserID);
            if (userRowByUserID != null)
            {
                str = userRowByUserID["UserName"].ToString().Trim();
                str2 = userRowByUserID["Password"].ToString().Trim();
                byte num1 = (byte) userRowByUserID["Category"];
            }
            else
            {
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
                    foreach (DataRow row2 in userGameTableByUserID.Rows)
                    {
                        int intCategory = (int) row2["Category"];
                        try
                        {
                            BTPAccountManager.ChangeSex(this.intUserID, blnSex, DBLogin.ConnString(intCategory));
                        }
                        catch
                        {
                            base.Response.Write("您在" + DBLogin.GameNameChinese(intCategory) + "的性别头像修改失败！系统将会在1个工作日内将密码同步。");
                            base.Response.End();
                            return;
                        }
                    }
                }
                string strUserName = StringItem.MD5Encrypt(str, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(str2, Global.strMD5Key);
                SessionItem.SetSelfLogin(strUserName, strPassword, false);
                ServerItem.ToOtherServer(0, str, str2, "URL=AlterFace.aspx!GameCategory=" + ServerParameter.intGameCategory);
            }
        }

        private void InitializeComponent()
        {
            switch (this.strType)
            {
                case "PASSWORD":
                    this.tblPassword.Visible = true;
                    if (this.strKind == "YES")
                    {
                        this.strMsg = "密码修改成功，下次使用新密码登录！";
                    }
                    this.btnMPassword.Click += new EventHandler(this.btnMPassword_Click);
                    break;

                case "NICKNAME":
                {
                    if (this.intPayType == 0)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=18");
                        return;
                    }
                    this.tblNickName.Visible = true;
                    this.tbNickName.Text = this.strNickName;
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    this.dateLastChangeNickname = (DateTime) accountRowByUserID["LastChangeNickname"];
                    if (this.dateLastChangeNickname.AddDays(180.0) > DateTime.Now)
                    {
                        this.tbNickName.Enabled = false;
                    }
                    if (this.strKind == "YES")
                    {
                        this.strMsg = "昵称修改成功！";
                    }
                    this.btnMNickName.Click += new EventHandler(this.btnMNickName_Click);
                    break;
                }
                case "EMAIL":
                    this.tblEmail.Visible = true;
                    this.tbEmail.Text = this.strEmail;
                    if ((this.strEmail != null) && (this.strEmail.Length > 0))
                    {
                        this.tbEmail.Enabled = false;
                        this.btnMEmail.Enabled = false;
                    }
                    if (this.strKind == "YES")
                    {
                        this.strMsg = "邮箱绑定成功！";
                    }
                    this.btnMEmail.Click += new EventHandler(this.btnMEmail_Click);
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

                case "NICKNAMEAA":
                    base.Response.Redirect("Report.aspx?Parameter=4007");
                    return;

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

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                bool flag;
                this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.strUserName = accountRowByUserID["UserName"].ToString();
                this.strNickName = accountRowByUserID["NickName"].ToString();
                this.intPayType = Convert.ToInt32(accountRowByUserID["PayType"]);
                this.strPassword = accountRowByUserID["Password"].ToString().Trim();
                this.strEmail = accountRowByUserID["Email"].ToString().Trim();
                try
                {
                    flag = true;
                }
                catch
                {
                    flag = false;
                }
                if (!flag)
                {
                    base.Response.Redirect("Report.aspx?Parameter=10114");
                }
                else
                {
                    this.tblNickName.Visible = false;
                    this.tblPassword.Visible = false;
                    this.tblSexFace.Visible = false;
                    this.tblEmail.Visible = false;
                    this.strType = SessionItem.GetRequest("Type", 1);
                    this.strKind = SessionItem.GetRequest("Kind", 1);
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


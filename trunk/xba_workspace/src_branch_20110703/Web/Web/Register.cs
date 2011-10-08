namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Register : Page
    {
        protected ImageButton btnNext;
        protected DropDownList ddlMonth;
        protected DropDownList ddlProvince;
        protected DropDownList ddlYear;
        protected RadioButton rbFemale;
        protected RadioButton rbMale;
        public string strErrCity;
        public string strErrEmail;
        public string strErrIntroNickName;
        public string strErrNickName;
        public string strErrPassword;
        public string strErrSay;
        public string strErrUserName;
        public string strErrInviteCode;
        public string strMsg;
        public string strPageIntro;
        public int intRecomUserID;
        protected TextBox tbCity;
        protected TextBox tbEmail;
        protected TextBox tbIntroNickName;
        protected TextBox tbNickName;
        protected TextBox tbPassword;
        protected TextBox tbRePassword;
        protected TextBox tbSay;
        protected TextBox tbUserName;
        protected TextBox tbInviteCode;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            string text = this.tbUserName.Text;
            string strIn = this.tbPassword.Text;
            string str3 = this.tbRePassword.Text;
            string htmlEncode = StringItem.SetValidWord(this.tbNickName.Text.Trim());
            string str5 = StringItem.SetValidWord(this.tbCity.Text);
            string str6 = this.tbEmail.Text;
            string str7 = this.tbIntroNickName.Text;
            string strFace = "0|0|0|0|0|0|0|0|0";
            bool blnSex = this.rbFemale.Checked;
            string selectedValue = this.ddlYear.SelectedValue;
            string strMonth = this.ddlMonth.SelectedValue;
            string strProvince = this.ddlProvince.SelectedValue;
            string str12 = StringItem.SetValidWord(this.tbSay.Text);
            //string strInviteCode = this.tbInviteCode.Text;
            if(str7 != null)
            {
                str7 = str7.Trim();
            }
            bool flag2 = false;
            if (!StringItem.IsValidLogin(text))
            {
                this.strErrUserName = "<font color='#FF0000'>*用户名填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidLogin(strIn) || (strIn != str3))
            {
                this.strErrPassword = "<font color='#FF0000'>*密码填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(htmlEncode, 2, 0x10))
            {
                this.strErrNickName = "<font color='#FF0000'>*昵称填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(str5, 2, 0x10))
            {
                this.strErrCity = "<font color='#FF0000'>*城市填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidEmail(str6))
            {
                this.strErrEmail = "<font color='#FF0000'>*Email填写错误！</font>";
                flag2 = true;
            }
            else if ((str7.Length > 0) && !StringItem.IsValidName(str7, 2, 0x10))
            {
                this.strErrIntroNickName = "<font color='#FF0000'>*介绍人填写错误！</font>";
                flag2 = true;
            }
            else if (!StringItem.IsValidName(str12, 2, 200))
            {
                this.strErrSay = "<font color='#FF0000'>*宣言填写错误！</font>";
                flag2 = true;
            }
            /*else if (!StringItem.IsValidInviteCode(strInviteCode))
            {
                this.strErrInviteCode = "<font color='#FF0000'>*邀请码填写错误！</font>";
                flag2 = true;
            }*/
            text = StringItem.GetHtmlEncode(text);
            htmlEncode = StringItem.GetHtmlEncode(htmlEncode);
            str5 = StringItem.GetHtmlEncode(str5);
            str7 = StringItem.GetHtmlEncode(str7);
            str12 = StringItem.GetHtmlEncode(str12);

           /* if (!flag2)
            {
                if(!BTPAccountManager.CheckInviteCode(strInviteCode)){
                     this.strErrInviteCode = "<font color='#FF0000'>*邀请码无效，请联系客服！</font>";
                    flag2 = true;
                }
            }*/


            if (!flag2)
            {
                switch (BTPAccountManager.CheckRegisterInfo(text, htmlEncode, str6))
                {
                    case 1:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 2:
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 3:
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 4:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 5:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 6:
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;

                    case 7:
                        this.strErrUserName = "<font color='#FF0000'>*用户名已存在，请重新输入！</font>";
                        this.strErrNickName = "<font color='#FF0000'>*昵称已存在，请重新输入！</font>";
                        this.strErrEmail = "<font color='#FF0000'>*Email已存在，请重新输入！</font>";
                        flag2 = true;
                        break;
                }
                if ((str7 != ""))
                {
                    DataRow recomUserRow = BTPAccountManager.GetAccountRowByNickName(str7.Trim());
                    if (recomUserRow == null)
                    {
                        this.strErrIntroNickName = "<font color='#FF0000'>*您输入的介绍人并不存在，请重新输入或留空！</font>";
                        flag2 = true;
                    }
                    else
                    {
                        this.intRecomUserID = Convert.ToInt32(recomUserRow["UserID"]);
                    }
                }
                //if (DateTime.Now.AddMinutes(-60.0) < ROOTUserManager.GetLatestRegTimeByIP(base.Request.ServerVariables["REMOTE_ADDR"]))
                //{
                //    this.strMsg = "<font color='#FF0000'>使用同一IP注册间隔时间必须在60分钟以上。</font>";
                //}
                if (!flag2)
                {
                    bool flag3;
                    string str14;
                    string strDiskURL = "NetDisk/" + StringItem.FormatDate(DateTime.Now, "yyyyMM") + "/";
                    if (base.Request.Cookies["FromURL"] != null)
                    {
                        str14 = base.Request.Cookies["FromURL"].Value.ToString();
                    }
                    else
                    {
                        str14 = "";
                    }
                    try
                    {
                        int userId = BTPAccountManager.GetMaxUserID();
                        BTPAccountManager.AddFullAccount(userId, text, htmlEncode, strIn, blnSex, 0, strDiskURL, "", strProvince, str5, "", this.intRecomUserID);
                        //BTPAccountManager.UpdateInviteCodeUsed(strInviteCode);
                        flag3 = true;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.ToString());
                        flag3 = false;
                    }
                    if (!flag3)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=10114");
                    }
                    string username = StringItem.MD5Encrypt(text, Global.strMD5Key);
                    string password = StringItem.MD5Encrypt(strIn, Global.strMD5Key);
                    if (SessionItem.SetSelfLogin(username, password, true) > 0)
                    {
                        base.Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=10");
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            if (!base.IsPostBack)
            {
                this.ddlYear.DataSource = DDLItem.GetYear();
                this.ddlMonth.DataSource = DDLItem.GetMonth();
                this.ddlProvince.DataSource = DDLItem.GetProvince();
                this.ddlYear.DataBind();
                this.ddlMonth.DataBind();
                this.ddlProvince.DataBind();
                this.ddlYear.SelectedValue = "1980";
                this.ddlMonth.SelectedValue = "1";
                this.ddlProvince.SelectedValue = "北京市";
                this.strMsg = "请严格按照注释填写下列各项。";
            }
            this.btnNext.ImageUrl = SessionItem.GetImageURL() + "button_11.GIF";

            int recomUserID = (int)SessionItem.GetRequest("u", 0);
            if (recomUserID > 0)
            {
                DataRow recomUser = BTPAccountManager.GetAccountRowByUserID(recomUserID);
                if (recomUser != null)
                {
                    this.tbIntroNickName.Text = Convert.ToString(recomUser["NickName"]);
                }

            }

        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(-1, "", "", "");
        }
    }
}


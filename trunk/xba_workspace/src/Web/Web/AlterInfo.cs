namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class AlterInfo : Page
    {
        protected Button btnNext;
        protected HtmlSelect city;
        protected HtmlSelect DD;
        protected DropDownList ddlHonour;
        private DataRow dr;
        protected HtmlInputHidden hddCity;
        protected HtmlInputHidden hddDay;
        protected HtmlInputHidden hddMonth;
        protected HtmlInputHidden hddYear;
        public int intDay;
        public int intMonth;
        public int intUserID;
        public int intYear;
        protected HtmlSelect MM;
        protected HtmlSelect prv;
        public string strCity = "";
        public string strErrCity;
        public string strErrPrv;
        public string strErrQQ;
        public string strErrSay;
        public string strErrYMD;
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        private string strPassword;
        public string strProvince = "";
        public string strQQ;
        public string strUserName;
        protected TextBox tbCity;
        protected TextBox tbQQ;
        protected TextBox tbSay;
        protected HtmlSelect YYYY;

        private void btnNext_Click(object sender, EventArgs e)
        {
            string strYear = "";
            string strMonth = "";
            string strDay = "";
            string strProvince = "";
            string strIn = "";
            strYear = this.hddYear.Value.ToString().Trim();
            strMonth = this.hddMonth.Value.ToString().Trim();
            strDay = this.hddDay.Value.ToString().Trim();
            strIn = this.hddCity.Value.ToString().Trim();
            strProvince = this.prv.Items[this.prv.SelectedIndex].Value.ToString().Trim();
            string validWords = StringItem.SetValidWord(this.tbSay.Text);
            string str7 = this.tbQQ.Text.ToString().Trim();
            int intCategory = Convert.ToInt16(this.ddlHonour.SelectedValue);
            bool flag = false;
            strIn = StringItem.GetValidWords(strIn);
            if (strYear == "")
            {
                this.strErrYMD = "<font color='#FF0000'>*请选择出生年！</font>";
                flag = true;
            }
            else if (strMonth == "")
            {
                this.strErrYMD = "<font color='#FF0000'>*请选择出生月！</font>";
                flag = true;
            }
            else if (strDay == "")
            {
                this.strErrYMD = "<font color='#FF0000'>*请选择出生日！</font>";
                flag = true;
            }
            strIn = StringItem.GetValidWords(strIn);
            if (strProvince == "")
            {
                this.strErrPrv = "<font color='#FF0000'>*请选择省份！</font>";
                flag = true;
            }
            else if (strIn == "")
            {
                this.strErrCity = "<font color='#FF0000'>*请选择城市！</font>";
                flag = true;
            }
            else if (!StringItem.IsValidName(strIn, 2, 0x10))
            {
                this.strErrCity = "<font color='#FF0000'>*城市填写错误！</font>";
                flag = true;
            }
            validWords = StringItem.GetValidWords(validWords);
            if ((validWords != "") && !StringItem.IsValidContent(validWords, 6, 200))
            {
                this.strErrSay = "<font color='#FF0000'>*宣言填写错误！</font>";
                flag = true;
            }
            if (str7 != "")
            {
                if (!StringItem.IsNumber(str7))
                {
                    this.strErrQQ = "<font color='#FF0000'>*QQ填写错误！</font>";
                    flag = true;
                }
                else if ((StringItem.GetStrLength(str7) < 4) || (StringItem.GetStrLength(str7) > 11))
                {
                    this.strErrQQ = "<font color='#FF0000'>*QQ位数填写错误！</font>";
                    flag = true;
                }
            }
            if (!flag)
            {
                try
                {
                    ROOTUserManager.UpdateUser(this.intUserID, strYear, strMonth, strDay, strProvince, strIn, validWords, str7, intCategory);
                    this.strMsg = "您的信息已经修改成功。";
                }
                catch
                {
                    this.strMsg = "暂时无法修改信息，请稍候再试。";
                }
                base.Response.Redirect("AlterInfo.aspx");
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new EventHandler(this.btnNext_Click);
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
                try
                {
                    this.dr = ROOTUserManager.GetUserInfoByID(this.intUserID);
                    flag = true;
                }
                catch
                {
                    this.dr = null;
                    flag = false;
                }
                this.strNickName = this.dr["NickName"].ToString().Trim();
                this.strUserName = this.dr["UserName"].ToString().Trim();
                if (this.dr["Birth"].ToString().Trim().IndexOf("-") != -1)
                {
                    string str;
                    string[] strArray = this.dr["Birth"].ToString().Trim().Split(new char[] { '-' });
                    string str2 = strArray[0];
                    string str3 = strArray[1];
                    this.intYear = Convert.ToInt32(str2);
                    this.intMonth = Convert.ToInt32(str3);
                    try
                    {
                        str = strArray[2];
                        this.intDay = Convert.ToInt32(str);
                    }
                    catch
                    {
                        str = "1";
                        this.intDay = 1;
                    }
                }
                this.strProvince = this.dr["Province"].ToString().Trim();
                this.strCity = this.dr["City"].ToString().Trim();
                string str4 = this.dr["Say"].ToString();
                this.strPassword = this.dr["Password"].ToString().Trim();
                this.strQQ = this.dr["QQ"].ToString().Trim();
                int num = (byte) this.dr["Category"];
                if (!flag)
                {
                    base.Response.Redirect("Report.aspx?Parameter=10114");
                }
                else
                {
                    if (!base.IsPostBack)
                    {
                        DataView view = new DataView(DDLItem.GetServerItem(this.intUserID));
                        this.ddlHonour.DataSource = view;
                        this.ddlHonour.DataTextField = "Name";
                        this.ddlHonour.DataValueField = "Category";
                        this.ddlHonour.DataBind();
                        this.ddlHonour.SelectedValue = num.ToString();
                        this.hddYear.Value = Convert.ToString(this.intYear);
                        this.hddMonth.Value = Convert.ToString(this.intMonth);
                        this.hddDay.Value = Convert.ToString(this.intDay);
                        this.hddCity.Value = this.strCity;
                        this.tbCity.Text = this.strCity;
                        this.tbSay.Text = str4;
                        this.tbQQ.Text = this.strQQ;
                        this.strMsg = "<a href='Forum.aspx'>论坛</a> | 改动时请参看各项的注释。";
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


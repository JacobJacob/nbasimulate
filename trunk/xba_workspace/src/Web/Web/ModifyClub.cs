namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ModifyClub : Page
    {
        protected HtmlTableRow AutoPay;
        private bool blnChangeName;
        protected ImageButton btnOK;
        protected HtmlTableRow ChangeLogo;
        private DateTime datChangeClubTime;
        private DateTime dtCreateTime;
        protected HtmlInputHidden hidClothes;
        protected HtmlInputHidden hidLogo;
        protected ImageButton Imagebutton2;
        public int intLevels;
        private int intPayType;
        public int intUserID;
        protected RadioButton rbCanclePay;
        protected RadioButton rbContinuePay;
        protected RadioButton rbMylogo;
        protected RadioButton rbSystemLogo;
        private string strCity;
        public string strClothes;
        public string strClubLogo = "";
        public string strClubName;
        public string strClubNameA;
        public string strDevSays;
        public string strimgLogo = "";
        public string strLogo;
        private string strLogoLink;
        public string strMsg;
        private string strProvince;
        private string strQQ;
        public string strTest = "";
        protected TextBox tbClubName;
        protected TextBox tbDevSays;
        protected HtmlTable tblChangeClothes;
        protected TextBox tbLogoLink;
        protected TextBox tbQQ;
        protected HtmlTableRow trQQ;
        protected HtmlInputFile uploadfile;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.hidLogo.Value;
            this.strLogo = str;
            if (this.rbSystemLogo.Checked && (str.Trim() != ""))
            {
                string strLogo = "Images/Club/Logo/" + str + ".gif";
                BTPAccountManager.ModifyClubLogo(this.intUserID, strLogo);
                BTPAccountManager.SetAccountLogoLink(this.intUserID, "");
                DTOnlineManager.ChangeClubLogoByUserID(this.intUserID, strLogo);
            }
            if (this.tbDevSays.Text.ToString().Trim() != this.strDevSays)
            {
                this.strDevSays = this.tbDevSays.Text.ToString().Trim();
                this.strDevSays = StringItem.GetValidWords(this.strDevSays);
                if (!StringItem.IsValidName(this.strDevSays, 0, 20))
                {
                    this.strMsg = "联赛宣言中含有非法字符或长度不符合要求。";
                    return;
                }
                if (this.tbDevSays.Text.ToString().Trim() == "")
                {
                    this.strDevSays = this.strProvince + " " + this.strCity;
                }
                BTPAccountManager.UpdateDevSays(this.intUserID, this.strDevSays);
                this.strMsg = "球队信息修改成功。";
            }
            if ((this.tbLogoLink.Text.Trim() == "") || this.tbLogoLink.Text.Trim().ToUpper().Equals("HTTP://"))
            {
                BTPAccountManager.SetAccountLogoLink(this.intUserID, "");
            }
            else
            {
                BTPAccountManager.SetAccountLogoLink(this.intUserID, this.tbLogoLink.Text.Trim());
            }
            string str3 = this.tbQQ.Text.Trim();
            if (StringItem.IsNumber(str3) || (str3.Trim() == ""))
            {
                this.strQQ = StringItem.GetValidWords(this.strQQ);
                if (!StringItem.IsValidName(this.strQQ, 0, 20) && (str3.Trim() != ""))
                {
                    this.strMsg = "经理QQ中含有非法字符或长度不符合要求。";
                    return;
                }
                BTPAccountManager.UpdateQQ(this.intUserID, str3);
                DTOnlineManager.ChangeQQByUserID(this.intUserID, str3);
                this.strMsg = "球队信息修改成功。";
            }
            else
            {
                this.strMsg = "经理QQ中含有非法字符或长度不符合要求。";
                return;
            }
            bool flag = this.rbContinuePay.Checked;
            int intContinuePay = 0;
            if (flag)
            {
                intContinuePay = 1;
            }
            BTPAccountManager.SetContinuePayByUserID(this.intUserID, intContinuePay);
            if (this.intPayType == 1)
            {
                this.tblChangeClothes.Visible = true;
                string str4 = this.hidClothes.Value.ToString();
                if (!StringItem.IsNumber(str4))
                {
                    str4 = "0";
                }
                if (this.strClothes != str4)
                {
                    BTPAccountManager.ModifyClothes(this.intUserID, str4);
                    this.strClothes = str4;
                    this.strMsg = "球队信息修改成功";
                }
                this.strClubName = this.tbClubName.Text.ToString().Trim();
                this.strClubName = StringItem.GetValidWords(this.strClubName);
                if (!StringItem.IsValidNameIn(this.strClubName, 2, 0x10))
                {
                    this.strMsg = "球队名称超出字数限制或包含非法字符，请重新填写。";
                    return;
                }
                if (BTPAccountManager.HasClubName(this.strClubName))
                {
                    if (this.strClubName != this.strClubNameA)
                    {
                        this.strMsg = "你所填写的球队名已存在，请重新填写。";
                        return;
                    }
                }
                else
                {
                    if (this.datChangeClubTime.AddHours(72.0) > DateTime.Now)
                    {
                        this.strMsg = "球队名更改时间不足72小时。";
                        return;
                    }
                    BTPAccountManager.ChangeClubName(this.intUserID, this.strClubName, DateTime.Now);
                    DTOnlineManager.ChangeClubNameByUserID(this.intUserID, this.strClubName);
                }
            }
            else
            {
                base.Response.Redirect("SecretaryPage.aspx?Type=MODIFYCLUBRETURN");
                return;
            }
            base.Response.Redirect("SecretaryPage.aspx?Type=MODIFYCLUBRETURN");
        }

        private void Imagebutton2_Click(object sender, ImageClickEventArgs e)
        {
            if ((this.uploadfile.PostedFile.ContentLength != 0) && (this.intPayType == 1))
            {
                string path = "Images/ClubLogo/" + StringItem.FormatDate(this.dtCreateTime, "yyyyMMdd") + "/" + this.intUserID.ToString() + ".gif";
                string str2 = "Images/ClubLogo/" + StringItem.FormatDate(this.dtCreateTime, "yyyyMMdd") + "/";
                str2 = base.Server.MapPath(str2);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(str2);
                }
                if (this.uploadfile.PostedFile.ContentLength > 0x6400)
                {
                    base.Response.Redirect("Report.aspx?Parameter=553");
                }
                else
                {
                    string str3 = this.uploadfile.PostedFile.FileName.Trim();
                    if (str3.Substring(str3.LastIndexOf(".") + 1, 3).ToUpper().Equals("GIF"))
                    {
                        path = "Images/ClubLogo/" + StringItem.FormatDate(this.dtCreateTime, "yyyyMMdd") + "/" + this.intUserID.ToString() + ".gif";
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=555");
                        return;
                    }
                    path = base.Server.MapPath(path);
                    this.uploadfile.PostedFile.SaveAs(path);
                    string strLogo = "Images/ClubLogo/" + StringItem.FormatDate(this.dtCreateTime, "yyyyMMdd") + "/" + this.intUserID.ToString() + ".gif";
                    BTPAccountManager.ModifyClubLogo(this.intUserID, strLogo);
                    if ((this.tbLogoLink.Text.Trim() == "") || this.tbLogoLink.Text.Trim().ToUpper().Equals("HTTP://"))
                    {
                        BTPAccountManager.SetAccountLogoLink(this.intUserID, "");
                    }
                    else
                    {
                        BTPAccountManager.SetAccountLogoLink(this.intUserID, this.tbLogoLink.Text.Trim());
                    }
                    DTOnlineManager.ChangeClubLogoByUserID(this.intUserID, strLogo);
                    base.Response.Redirect("ModifyClub.aspx");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=556");
            }
        }

        private void InitializeComponent()
        {
            this.Imagebutton2.Click += new ImageClickEventHandler(this.Imagebutton2_Click);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                this.tblChangeClothes.Visible = false;
                if (ServerParameter.strCopartner == "CGA")
                {
                    this.trQQ.Visible = false;
                    this.AutoPay.BgColor = "#fdf7f4";
                }
                else
                {
                    this.trQQ.Visible = true;
                }
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.Imagebutton2.ImageUrl = SessionItem.GetImageURL() + "button_51.GIF";
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intLevels = (int) onlineRowByUserID["Levels"];
                this.hidLogo.Value = this.strLogo;
                this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
                this.strClubNameA = onlineRowByUserID["ClubName3"].ToString().Trim();
                this.intPayType = (int) onlineRowByUserID["PayType"];
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.strProvince = accountRowByUserID["Province"].ToString().Trim();
                this.strCity = accountRowByUserID["City"].ToString().Trim();
                this.strQQ = accountRowByUserID["QQ"].ToString().Trim();
                this.strLogoLink = accountRowByUserID["LogoLink"].ToString().Trim();
                this.dtCreateTime = (DateTime) accountRowByUserID["CreateTime"];
                this.strLogo = accountRowByUserID["ClubLogo"].ToString();
                this.datChangeClubTime = (DateTime) accountRowByUserID["ChangeClubTime"];
                this.strClothes = accountRowByUserID["Shirt"].ToString().Trim();
                this.hidClothes.Value = this.strClothes;
                int startIndex = this.strLogo.LastIndexOf(".") - 2;
                this.rbContinuePay.Checked = (bool) accountRowByUserID["ContinuePay"];
                if (!this.rbContinuePay.Checked)
                {
                    this.rbCanclePay.Checked = true;
                }
                if (startIndex >= 0)
                {
                    this.strClubLogo = this.strLogo.Substring(startIndex, 2);
                }
                this.tbQQ.Text = this.strQQ;
                if (this.strClubLogo.IndexOf("/") >= 0)
                {
                    this.strClubLogo = this.strClubLogo.Substring(1, 1);
                }
                else
                {
                    this.strClubLogo = "1";
                }
                if (this.strLogoLink.Trim() == "")
                {
                    this.strimgLogo = "<img id=imgLogo height=46 src='" + this.strLogo + "'width=46 border=0>";
                    this.tbLogoLink.Text = "http://";
                }
                else
                {
                    this.strimgLogo = "<a href='" + this.strLogoLink + "' target='_blank'><img id=imgLogo border=0 height=46 src='" + this.strLogo + "'width=46></a>";
                    this.tbLogoLink.Text = this.strLogoLink;
                }
                if (!base.IsPostBack)
                {
                    this.tbClubName.Text = this.strClubName;
                }
                DataRow row3 = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.blnChangeName = (bool) row3["ChangeName"];
                this.strDevSays = row3["DevSay"].ToString().Trim();
                if (this.intPayType == 1)
                {
                    this.tblChangeClothes.Visible = true;
                    if (this.datChangeClubTime.AddHours(72.0) > DateTime.Now)
                    {
                        this.tbClubName.Enabled = false;
                    }
                    else
                    {
                        this.tbClubName.Enabled = true;
                    }
                }
                else
                {
                    this.rbMylogo.Enabled = false;
                    this.tbClubName.Enabled = false;
                }
                this.tbDevSays.Text = this.strDevSays;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


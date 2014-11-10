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

    public class RookieRegClub : Page
    {
        protected ImageButton btnRegClub;
        protected HtmlInputHidden hidClothes;
        protected HtmlInputHidden hidLogo;
        protected HtmlInputHidden hidMedal;
        protected int intRecomUserID;
        private int intUserID;
        public string strErrClubName;
        public string strMsg;
        public string strNickName;
        public string strTimeName;
        protected TextBox tbClubName;
        protected HtmlTable tblLogin_02;

        private void btnRegClub_Click(object sender, ImageClickEventArgs e)
        {
            int num = (int) DTOnlineManager.GetOnlineRowByUserID(this.intUserID)["Category"];
            if (((num != 0) && (num != 4)) && (num != 6))
            {
                base.Response.Redirect("Report.aspx?Parameter=3newa");
            }
            else
            {
                string text = this.tbClubName.Text;
                bool flag = false;
                if (BTPClubManager.GetClubRowByUIDCate(this.intUserID, 3) != null)
                {
                    int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                    if (intRookieOpIndex != 1)
                    {
                        base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=14");
                    }
                }
                else
                {
                    text = StringItem.GetValidWords(text);
                    if (!StringItem.IsValidNameIn(text, 2, 0x10))
                    {
                        this.strErrClubName = "<font color='#FF0000'>*俱乐部名称填写错误！</font>";
                        flag = true;
                    }
                    text = StringItem.GetHtmlEncode(text);
                    if (!flag)
                    {
                        if (BTPClubManager.HasClubName(text))
                        {
                            this.strErrClubName = "<font color='#FF0000'>*俱乐部名称已经存在，请重新填写！</font>";
                            flag = true;
                        }
                        if (!flag)
                        {
                            string strClubLogo = "Images/Club/Logo/" + this.hidLogo.Value + ".gif";
                            BTPClubManager.CreateNewClub(this.intUserID, text, strClubLogo, this.hidClothes.Value);
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 3);
                            SessionItem.SetSomebodyGameLogin(this.intUserID);
                            int num3 = AccountItem.SetRookieOp(this.intUserID, 1);
                            if (num3 != 1)
                            {
                                base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(num3) + "\";</script>");
                            }
                            else
                            {
                                base.Response.Write("<script>window.top.Main.location=\"RookieMain_P.aspx?Type=ASSIGNDEVCLUB\"</script>");
                            }
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnRegClub.Click += new ImageClickEventHandler(this.btnRegClub_Click);
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
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex == -1)
                {
                    base.Response.Write("<script>window.top.location=\"Main.aspx\";</script>");
                }
                else if (intRookieOpIndex != 1)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                else
                {
                    bool flag;
                    string str5;
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    string pToEncrypt = onlineRowByUserID["UserName"].ToString().Trim();
                    string str7 = onlineRowByUserID["Password"].ToString().Trim();
                    int num1 = (int) onlineRowByUserID["Category"];
                    string strQQ = onlineRowByUserID["QQ"].ToString().Trim();
                    string strProvince = "";
                    string strCity = "";
                    string strBirth = "";
                    string strDeptTag = "";
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
                        string strPassword = StringItem.MD5Encrypt(str7, Global.strMD5Key);
                        this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                        bool blnSex = (bool) onlineRowByUserID["Sex"];
                        str5 = onlineRowByUserID["DiskURL"].ToString().Trim();
                        BTPAccountManager.AddFullAccount(this.intUserID, pToEncrypt, this.strNickName, str7, blnSex, 0, str5 + "/", strBirth, strProvince, strCity, strQQ, this.intRecomUserID, "");
                        string str11 = Convert.ToString((int) (ServerParameter.intGameCategory + 0x7d0)).Trim();
                        strDeptTag = strDeptTag.Trim() + str11 + ",";
                        ROOTUserGameManager.UpdateDeptTagByUserID(this.intUserID, strDeptTag);
                        SessionItem.SetSelfLogin(strUserName, strPassword, false);
                    }
                    onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    str5 = onlineRowByUserID["DiskURL"].ToString();
                    string str12 = DBLogin.URLString(0) + str5 + "Face.png";
                    int num2 = RandomItem.rnd.Next(0, 10);
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='60' align='center'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str12, "?RndID=", num2, ");width:37px;height:40px'></div><font class='ForumTime'>", this.strNickName, "</font></td></tr><tr><td height='27' align='center'>" });
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "<a href='" + DBLogin.URLString(40) + "MemberCenter.aspx'><img src='" + SessionItem.GetImageURL() + "Button_portmanager.gif' width='76' height='24' border='0'></a>&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                    this.InitializeComponent();
                    base.OnInit(e);
                    string request = SessionItem.GetRequest("Type", 1);
                    if (request == "NEXT")
                    {
                        this.btnRegClub.ImageUrl = SessionItem.GetImageURL() + "new/b2.gif";
                    }
                    else
                    {
                        this.btnRegClub.ImageUrl = SessionItem.GetImageURL() + "new/b2.gif";
                    }
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


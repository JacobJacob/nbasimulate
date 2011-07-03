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

    public class RegClub : Page
    {
        protected ImageButton btnRegClub;
        protected HtmlInputHidden hidClothes;
        protected HtmlInputHidden hidLogo;
        protected HtmlInputHidden hidMedal;
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
                base.Response.Redirect("Report.aspx?Parameter=3a");
            }
            else
            {
                string text = this.tbClubName.Text;
                bool flag = false;
                if (BTPClubManager.GetClubRowByUIDCate(this.intUserID, 3) != null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=14");
                }
                else
                {
                    text = StringItem.GetValidWords(text);
                    if (!StringItem.IsValidName(text, 2, 0x10))
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
                            BTPClubManager.AddClub3(this.intUserID, text, strClubLogo, this.hidClothes.Value);
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 3);
                            SessionItem.SetSomebodyLogin(this.intUserID);
                            int intClubID = (int) DTOnlineManager.GetOnlineRowByUserID(this.intUserID)["ClubID3"];
                            BTPPlayer3Manager.CreateSelectPlayer3(intClubID);
                            base.Response.Redirect("AssMain.aspx");
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
                    string str7;
                    string str8;
                    bool flag;
                    string str9;
                    DataRow reader = ROOTUserManager.Get40UserRowByUserID(this.intUserID);
                    if (reader != null)
                    {
                        str4 = reader["City"].ToString().Trim();
                        str5 = reader["Province"].ToString().Trim();
                        str6 = reader["BirthDay"].ToString().Trim();
                        str7 = reader["DeptTag"].ToString().Trim();
                        str8 = reader["Email"].ToString().Trim();
                        //reader.Close();
                    }
                    else
                    {
                        str5 = "";
                        str4 = "";
                        str6 = "";
                        str7 = "";
                        str8 = "";
                        //reader.Close();
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
                        str9 = onlineRowByUserID["DiskURL"].ToString().Trim();
                        BTPAccountManager.AddFullAccount(this.intUserID, pToEncrypt, this.strNickName, str2, blnSex, 0, str9 + "/", str6, str5, str4, strQQ, str8);
                        string str12 = Convert.ToString((int) (ServerParameter.intGameCategory + 0x7d0)).Trim();
                        if (str7.IndexOf(str12) > 0)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=10116");
                            return;
                        }
                        str7 = str7.Trim() + str12 + ",";
                        ROOTUserGameManager.UpdateDeptTagByUserID(this.intUserID, str7);
                        SessionItem.SetSelfLogin(strUserName, strPassword, false);
                    }
                    onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    this.strNickName = onlineRowByUserID["NickName"].ToString();
                    str9 = onlineRowByUserID["DiskURL"].ToString();
                    string str13 = DBLogin.URLString(0) + str9 + "Face.png";
                    int num2 = RandomItem.rnd.Next(0, 10);
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='60' align='center'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str13, "?RndID=", num2, ");width:37px;height:40px'></div><font class='ForumTime'>", this.strNickName, "</font></td></tr><tr><td height='27' align='center'>" });
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "<a href='" + DBLogin.URLString(40) + "MemberCenter.aspx'><img src='" + SessionItem.GetImageURL() + "Button_portmanager.gif' width='76' height='24' border='0'></a>&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                    this.InitializeComponent();
                    base.OnInit(e);
                    string request = (string) SessionItem.GetRequest("Type", 1);
                    if (request == "NEXT")
                    {
                        this.btnRegClub.ImageUrl = SessionItem.GetImageURL() + "button_Next.gif";
                    }
                    else
                    {
                        this.btnRegClub.ImageUrl = SessionItem.GetImageURL() + "button_RegClub.gif";
                    }
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


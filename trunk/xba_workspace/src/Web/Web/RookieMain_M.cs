namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class RookieMain_M : Page
    {
        public bool blnSex;
        private int intClubID;
        private int intMsgFlag;
        private int intPayType;
        public int intUserID;
        private SecretaryItem si;
        public string strAnnounce;
        public string strBtnClubBuild;
        public string strBtnMap3;
        public string strBtnMap5;
        public string strBtnVArrange;
        public string strFace;
        public string strFrameURL = "";
        public string strGetInfoList;
        public string strKFQQ = "客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=1&uin=15908920&site=www.xba.com.cn&menu=yes\" target=\"blank\"><img alt=\"有事您Q我:15908920！\" src=\"http://wpa.qq.com/pa?p=1:15908920:5\" border=\"0\" width=\"61\" height=\"15\"></a>";
        public string strMainScript;
        public string strMap3;
        public string strMap5;
        public string strMenuList;
        public string strMsg;
        public string strNickName;
        public string strPassword;
        public string strSecFace;
        public string strServerName;
        public string strServerTime;
        public string strSetting;
        public string strTimeName;
        public string strUserName;
        public string strXBApic = (SessionItem.GetImageURL() + "XBApic.gif");

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            bool flag;
            string strMsg;
            this.strServerName = DBLogin.GameNameChinese(ServerParameter.intGameCategory);
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            string pToEncrypt = onlineRowByUserID["UserName"].ToString().Trim();
            string str3 = onlineRowByUserID["Password"].ToString().Trim();
            int num = (int) onlineRowByUserID["Category"];
            string strQQ = onlineRowByUserID["QQ"].ToString().Trim();
            if (ServerParameter.strCopartner == "CGA")
            {
                this.strKFQQ = "<a target=\"blank\" href=\"tencent://message/?uin=15908920&Site=XBA篮球经理&Menu=yes\"><img border=\"0\" SRC=\"http://wpa.qq.com/pa?p=1:15908920:3\" alt=\"有问题找客服\"></a>";
            }
            else if (ServerParameter.strCopartner == "17173")
            {
                this.strKFQQ = StringItem.GetKFURL();
                this.strXBApic = SessionItem.GetImageURL() + "/XBApic173.gif";
            }
            else
            {
                this.strKFQQ = "客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=1&uin=15908920&site=www.xba.com.cn&menu=yes\" target=\"blank\"><img alt=\"有事您Q我:15908920！\" src=\"http://wpa.qq.com/pa?p=1:15908920:5\" border=\"0\" width=\"61\" height=\"15\"></a>";
            }
            this.strKFQQ = StringItem.GetKFURL();
            if (num == 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
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
            if (BTPDevManager.GetDevBlank() > 0)
            {
                if (flag)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = true;
            }
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            string str5 = "";
            if (accountRowByUserID == null)
            {
                string str6;
                string str7;
                string str8;
                string str9;
                string str10;
                DataRow row4 = ROOTUserManager.Get40UserRowByUserID(this.intUserID);
                if (row4 != null)
                {
                    str6 = row4["City"].ToString().Trim();
                    str7 = row4["Province"].ToString().Trim();
                    DateTime datIn = (DateTime) row4["BirthDay"];
                    str8 = StringItem.FormatDate(datIn, "yyyy-MM-dd");
                    str9 = row4["DeptTag"].ToString().Trim();
                    str10 = row4["Email"].ToString().Trim();
                }
                else
                {
                    str7 = "";
                    str6 = "";
                    str8 = "";
                    str9 = "";
                    str10 = "";
                }
                if (flag)
                {
                    this.strFrameURL = "RookieMain_I.aspx?Type=WELCOME";
                    return;
                }
                string strUserName = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(str3, Global.strMD5Key);
                this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                bool blnSex = (bool) onlineRowByUserID["Sex"];
                BTPAccountManager.AddFullAccount(this.intUserID, pToEncrypt, this.strNickName, str3, blnSex, 0, onlineRowByUserID["DiskURL"].ToString().Trim() + "/", str8, str7, str6, strQQ, str10);
                string str13 = Convert.ToString((int) (ServerParameter.intGameCategory + 0x7d0)).Trim();
                if (str9.IndexOf(str13) > 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=10116");
                    return;
                }
                str9 = str9.Trim() + str13 + ",";
                ROOTUserGameManager.UpdateDeptTagByUserID(this.intUserID, str9);
                SessionItem.SetSelfLogin(strUserName, strPassword, false);
            }
            onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strUserName = onlineRowByUserID["UserName"].ToString();
            this.strPassword = onlineRowByUserID["Password"].ToString();
            this.strNickName = onlineRowByUserID["NickName"].ToString();
            str5 = onlineRowByUserID["DiskURL"].ToString();
            this.intClubID = (int) onlineRowByUserID["ClubID5"];
            this.strFace = DBLogin.URLString(0) + str5 + "Face.png";
            this.strFace = string.Concat(new object[] { "<div style='position:absolute;top:114px;left:82px;filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", this.strFace, "?RndID=", RandomItem.rnd.Next(0, 10), ");width:37px;height:40px'></div>" });
            num = (int) onlineRowByUserID["Category"];
            this.intMsgFlag = (int) onlineRowByUserID["HasMsg"];
            this.intPayType = (int) onlineRowByUserID["PayType"];
            this.blnSex = (bool) onlineRowByUserID["Sex"];
            this.si = new SecretaryItem(this.intUserID, this.blnSex);
            this.strSecFace = this.si.GetImgFace();
            string str14 = "";
            if (this.intPayType == 1)
            {
                str14 = "<font color=red >&nbsp;&nbsp;[会员]</font>";
            }
            this.strMsg = "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='40' align='center' width='100%'></td><tr><td height='20' align='center' colspan=2><font class='ForumTime'>" + this.strNickName + str14 + "</font></td></tr><tr><td height='27' align='center' colspan='2'>";
            if (ServerParameter.strCopartner == "DUNIU")
            {
                this.strMsg = this.strMsg + "<a target=\"_blank\" href='http://bbs.mmoabc.com/forums/18'><img src='" + SessionItem.GetImageURL() + "dnluntan.gif' width='51' height='24' border='0'></a>&nbsp;";
            }
            else
            {
                strMsg = this.strMsg;
                this.strMsg = strMsg + "<a href='" + DBLogin.URLString(40) + "MemberCenter.aspx'><img src='" + SessionItem.GetImageURL() + "passport.gif' width='51' height='24' border='0'></a>&nbsp;";
            }
            this.strMsg = this.strMsg + "<img src='" + SessionItem.GetImageURL() + "offlinetrain.gif' width='66' height='24' border='0'>&nbsp;";
            strMsg = this.strMsg;
            this.strMsg = strMsg + "<a href='" + StringItem.GetLogoutURL() + "'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
            this.strSetting = this.strSetting + BoardItem.GetSetting(this.strUserName, this.strPassword);
            if ((DateTime.Now > DateTime.Today.AddHours((double) Global.intStartUpdate)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
            {
                this.strTimeName = "时间表";
            }
            else
            {
                this.strTimeName = "";
            }
            accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID != null)
            {
                if (accountRowByUserID["RookieOp"].ToString().Trim().IndexOf("0") == -1)
                {
                    base.Response.Redirect("Main.aspx");
                    return;
                }
                switch (AccountItem.RookieOpIndex(this.intUserID))
                {
                    case 0:
                        this.strFrameURL = "RookieMain_I.aspx?Type=WELCOME";
                        goto Label_06F2;

                    case 1:
                        this.strFrameURL = "RookieMain_I.aspx?Type=REGCLUB";
                        goto Label_06F2;

                    case 2:
                        this.strFrameURL = "RookieMain_P.aspx?Type=ASSIGNDEVCLUB";
                        goto Label_06F2;

                    case 3:
                        this.strFrameURL = "RookieMain_I.aspx?Type=ENDDEVCREATE";
                        goto Label_06F2;

                    case 4:
                        this.strFrameURL = "RookieMain_P.aspx?Type=ASSIGNCLUB";
                        goto Label_06F2;

                    case 5:
                        this.strFrameURL = "RookieMain_I.aspx?Type=END";
                        goto Label_06F2;
                }
                this.strFrameURL = "Main.aspx";
            }
            else
            {
                this.strFrameURL = "RookieMain_I.aspx?Type=WELCOME";
                return;
            }
        Label_06F2:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


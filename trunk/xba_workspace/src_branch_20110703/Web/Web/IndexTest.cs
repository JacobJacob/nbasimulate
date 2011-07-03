namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class IndexTest : Page
    {
        protected ImageButton btnLogin;
        protected ImageButton btnReg;
        public string strMsg;
        public string strNewsList;
        public string strRMList;
        private string strRMLogo;
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
            text = StringItem.MD5Encrypt(text, Global.strMD5Key);
            pToEncrypt = StringItem.MD5Encrypt(pToEncrypt, Global.strMD5Key);
            if (SessionItem.SetSelfLogin(text, pToEncrypt, true) > 0)
            {
                base.Response.Redirect("");
            }
            else
            {
                DataRow userRowByUserName = ROOTUserManager.GetUserRowByUserName(this.tbUserName.Text.Trim());
                if (userRowByUserName != null)
                {
                    if (this.tbPassword.Text.Trim() == userRowByUserName["Password"].ToString().Trim())
                    {
                        int num1 = (int) userRowByUserName["UserID"];
                        userRowByUserName["NickName"].ToString().Trim();
                        byte num2 = (byte) userRowByUserName["Category"];
                        userRowByUserName["DiskURL"].ToString();
                        SessionItem.SetMainLogin(userRowByUserName, true);
                        base.Response.Redirect("");
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=10");
                    }
                }
                else
                {
                    //userRowByUserName.Close();
                    base.Response.Redirect("Report.aspx?Parameter=10");
                }
            }
        }

        private void btnReg_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("Announce.aspx");
        }

        private void InitializeComponent()
        {
            this.btnLogin.Click += new ImageClickEventHandler(this.btnLogin_Click);
            this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            if (!base.IsPostBack)
            {
                int intUserID = SessionItem.CheckLogin(1);
                if (intUserID == -1)
                {
                    this.tblLogin_01.Visible = true;
                    this.tblLogin_02.Visible = false;
                    this.btnLogin.ImageUrl = SessionItem.GetImageURL() + "/button_01.gif";
                    this.btnReg.ImageUrl = SessionItem.GetImageURL() + "/button_03.gif";
                    this.strRMLogo = "<img src='" + SessionItem.GetImageURL() + "RM/Border_G_08.gif' border='0'>";
                }
                else
                {
                    this.strRMLogo = "<a href='RM.aspx?Type=GUESS' target='MyRM'><img src='" + SessionItem.GetImageURL() + "RM/Border_08.gif' border='0'></a>";
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
                    string strUserName = onlineRowByUserID["UserName"].ToString();
                    string strPassword = onlineRowByUserID["Password"].ToString();
                    string str3 = onlineRowByUserID["NickName"].ToString();
                    string str4 = onlineRowByUserID["DiskURL"].ToString();
                    string str5 = DBLogin.URLString(0) + str4 + "Face.png";
                    int num2 = RandomItem.rnd.Next(0, 10);
                    this.tblLogin_01.Visible = false;
                    this.tblLogin_02.Visible = true;
                    this.strMsg = string.Concat(new object[] { "<table width='170' border='0' cellpadding='0' cellspacing='0'><tr><td height='40' align='center' width='40%'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str5, "?RndID=", num2, ");width:37px;height:40px'></div></td><td width='60%'>　", BoardItem.GetMsgSettingArea(strUserName, strPassword), "</td></tr><tr><td height='20' align='center' colspan=2><font class='ForumTime'>", str3, "</font></td></tr><tr><td height='27' align='center' colspan='2'>" });
                    Convert.ToBoolean(SessionItem.GetRequest("IsGameServer", 2));
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
                        this.strMsg = this.strMsg + "<a href='WebAD.aspx'><img src='" + SessionItem.GetImageURL() + "button_05.gif' width='40' height='24' border='0'></a>";
                    }
                    string strMsg = this.strMsg;
                    this.strMsg = strMsg + "&nbsp;&nbsp;<a href='Logout.aspx?GameCategory=-1&Type=OnlyJump&JumpURL=Index.aspx' target='_blank'><img src='" + SessionItem.GetImageURL() + "button_06.gif' width='40' height='24' border='0'></a>&nbsp;&nbsp;<a href='http://www.xba.com.cn/MemberCenter.aspx?IsLogout=1'><img src='" + SessionItem.GetImageURL() + "button_07.gif' width='40' height='24' border='0'></a></td></tr></table>";
                }
                if ((DateTime.Now > DateTime.Today.AddHours(6.0)) && (DateTime.Now < DateTime.Today.AddHours(10.0)))
                {
                    this.strTimeName = "时间表";
                }
                else
                {
                    this.strTimeName = "第一联赛排名";
                }
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetList();
            this.strRMList = "";
            this.RMList();
        }

        private void RMList()
        {
            int num;
            int num2;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string teamNameByTeamID;
            string str7;
            DataRow nextMatchRow = RMMatchManager.GetNextMatchRow();
            if (nextMatchRow != null)
            {
                int num3 = (int) nextMatchRow["MatchID"];
                int intTeamID = (int) nextMatchRow["HomeTeamID"];
                int num5 = (int) nextMatchRow["AwayTeamID"];
                teamNameByTeamID = RMTeamManager.GetTeamNameByTeamID(intTeamID);
                str7 = RMTeamManager.GetTeamNameByTeamID(num5);
                if (num3 > 1)
                {
                    DataRow matchRowByMatchID = RMMatchManager.GetMatchRowByMatchID(num3 - 1);
                    int num1 = (int) matchRowByMatchID["MatchID"];
                    num = (int) matchRowByMatchID["HomeTeamID"];
                    num2 = (int) matchRowByMatchID["AwayTeamID"];
                    str3 = Convert.ToString((int) matchRowByMatchID["HomeScore"]);
                    str4 = Convert.ToString((int) matchRowByMatchID["AwayScore"]);
                    str5 = Convert.ToString((int) matchRowByMatchID["YaoScore"]);
                    str = RMTeamManager.GetTeamNameByTeamID(num);
                    str2 = RMTeamManager.GetTeamNameByTeamID(num2);
                }
                else
                {
                    num = intTeamID;
                    num2 = num5;
                    str3 = "--";
                    str4 = "--";
                    str5 = "--";
                    str = teamNameByTeamID;
                    str2 = str7;
                }
            }
            else
            {
                DataRow lastEndMatchRow = RMMatchManager.GetLastEndMatchRow();
                int num6 = (int) lastEndMatchRow["MatchID"];
                num = (int) lastEndMatchRow["HomeTeamID"];
                num2 = (int) lastEndMatchRow["AwayTeamID"];
                str3 = Convert.ToString((int) lastEndMatchRow["HomeScore"]);
                str4 = Convert.ToString((int) lastEndMatchRow["AwayScore"]);
                str5 = Convert.ToString((int) lastEndMatchRow["YaoScore"]);
                str = RMTeamManager.GetTeamNameByTeamID(num);
                str2 = RMTeamManager.GetTeamNameByTeamID(num2);
                teamNameByTeamID = "--";
                str7 = "--";
                this.strRMLogo = "<img src='" + SessionItem.GetImageURL() + "RM/Border_G_08.gif' border='0'>";
            }
            this.strRMList = string.Concat(new object[] { 
                "<table width='224' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td width='3' height='147'></td>\t\t\t\t\t<td width='210'>\t\t\t\t\t\t<table width='100%' height='147'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td width='210' height='27'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td>\t\t\t\t\t\t\t\t\t<table width='100%' height='75'  border='0' cellpadding='0' cellspacing='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='75' width='70'><font style='line-height:150%'><img src='", SessionItem.GetImageURL(), "NBALogo/", num, ".gif'><br><strong>", str, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t\t<td width='70'><strong>", str3, "：", str4, "<br>姚明：", str5, "</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='70'><font style='line-height:150%'><img src='", SessionItem.GetImageURL(), "NBALogo/", num2, 
                ".gif'><br><strong>", str2, "</strong></font></td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='1'></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td height='44'>\t\t\t\t\t\t\t\t\t<table width='100%'  border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\t\t\t<tr align='center' style='color:#FFFFFF'>\t\t\t\t\t\t\t\t\t\t\t<td height='44' width='55'><strong>下一场</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='100'><strong>", teamNameByTeamID, " vs ", str7, "</strong></td>\t\t\t\t\t\t\t\t\t\t\t<td width='55'>", this.strRMLogo, "</td>\t\t\t\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t\t\t\t</table>\t\t\t\t\t\t\t\t</td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t</table>\t\t\t\t\t</td>\t\t\t\t\t<td width='11'></td>\t\t\t\t</tr>\t\t\t</table>"
             });
        }

        private void SetList()
        {
            DataRow row;
            string str;
            int num;
            string str3;
            string str4;
            string str5;
            object strNewsList;
            this.strNewsList = "";
            this.strTopicList = "";
            DataTable tableByBoardID = ROOTTopicManager.GetTableByBoardID("001001", 12);
            if (tableByBoardID != null)
            {
                for (int i = 0; i < tableByBoardID.Rows.Count; i++)
                {
                    row = tableByBoardID.Rows[i];
                    str = row["BoardID"].ToString().Trim();
                    num = (int) row["TopicID"];
                    str3 = row["Title"].ToString();
                    string str2 = row["Keyword"].ToString().Trim();
                    if (str2 == "")
                    {
                        str2 = "新闻";
                    }
                    if (str2 == "新闻")
                    {
                        str5 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                        str4 = "#ff3301";
                    }
                    else if (str2 == "开发")
                    {
                        str5 = "<img src='" + SessionItem.GetImageURL() + "/icon_03.gif' width='4' height='8'>";
                        str4 = "#039a01";
                    }
                    else
                    {
                        str5 = "<img src='" + SessionItem.GetImageURL() + "/icon_04.gif' width='4' height='8'>";
                        str4 = "#0001fe";
                    }
                    strNewsList = this.strNewsList;
                    this.strNewsList = string.Concat(new object[] { strNewsList, str5, "&nbsp;[<font color='", str4, "'>", str2, "</font>]&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'>", StringItem.GetShortString(str3, 0x1c), "</a>" });
                    if (i < (tableByBoardID.Rows.Count - 1))
                    {
                        this.strNewsList = this.strNewsList + "<br>";
                    }
                }
            }
            tableByBoardID = ROOTTopicManager.GetTableWithoutBoardID("001001", 12);
            if (tableByBoardID != null)
            {
                for (int j = 0; j < tableByBoardID.Rows.Count; j++)
                {
                    row = tableByBoardID.Rows[j];
                    str = row["BoardID"].ToString().Trim();
                    num = (int) row["TopicID"];
                    str3 = row["Title"].ToString();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    if (datIn.AddHours(24.0) > DateTime.Now)
                    {
                        str4 = "#ff0000";
                    }
                    else
                    {
                        str4 = "#666666";
                    }
                    str5 = "<img src='" + SessionItem.GetImageURL() + "/icon_01.gif' width='4' height='8'>";
                    strNewsList = this.strTopicList;
                    this.strTopicList = string.Concat(new object[] { strNewsList, str5, "&nbsp;[<font color='", str4, "'>", StringItem.FormatDate(datIn, "hh:mm"), "</font>]&nbsp;<a href='Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1'>", StringItem.GetShortString(str3, 0x1c), "</a>" });
                    if (j < (tableByBoardID.Rows.Count - 1))
                    {
                        this.strTopicList = this.strTopicList + "<br>";
                    }
                }
            }
        }
    }
}


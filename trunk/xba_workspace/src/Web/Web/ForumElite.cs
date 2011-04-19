namespace Web
{
    using AjaxPro;
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class ForumElite : Page
    {
        private int intCategory;
        private int intUserID;
        protected HtmlInputHidden MaxID;
        public StringBuilder sb = new StringBuilder();
        public StringBuilder sbMsg = new StringBuilder("");
        public StringBuilder sbNewTopic = new StringBuilder();
        public StringBuilder sbOutScript = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder("");
        public StringBuilder sbTodayElite = new StringBuilder();
        public string strAddMsg = "";
        private string strPassword;
        public string strTitle1 = "";
        public string strTitle2 = "";
        private string strUserName;

        [AjaxMethod]
        public string GetAddFriMatchMsg(int intFMatchMsgID, string strContent)
        {
            try
            {
                int intUserID = SessionItem.CheckLogin(1);
                int intMax = 100;
                strContent = StringItem.SetValidWord(strContent);
                int strLength = StringItem.GetStrLength(strContent.Trim());
                if (intUserID < 1)
                {
                    return "-4|0|0";
                }
                if (strLength > intMax)
                {
                    return "-2|0|0";
                }
                if (strContent.Trim() == "")
                {
                    return "-3|0|0";
                }
                if (!StringItem.IsValidContent(strContent, 1, intMax))
                {
                    return "-1|0|0";
                }
                SqlDataReader reader = BTPFriMatchMsgManager.GetAddFriMatchMsg(intFMatchMsgID, strContent, intUserID);
                StringBuilder builder = new StringBuilder("");
                int num4 = 0;
                int num5 = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateTime datIn = (DateTime) reader["CreateTime"];
                        num4 = (int) reader["FMatchMsgID"];
                        string text1 = "[" + StringItem.FormatDate(datIn, "hh:mm") + "]";
                        string strIn = reader["Content"].ToString().Trim();
                        string str2 = reader["NickName"].ToString().Trim();
                        bool flag = (bool) reader["Sex"];
                        int num6 = (int) reader["UserID"];
                        if (flag)
                        {
                            str2 = "<font color='#ff005a'>" + StringItem.GetShortString(str2, 10) + "</font>";
                        }
                        else
                        {
                            str2 = "<font color='blue'>" + StringItem.GetShortString(str2, 10) + "</font>";
                        }
                        builder.Append(string.Concat(new object[] { "<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num6, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str2, "</a>:", StringItem.GetShortString(strIn, 100), "&" }));
                        num5++;
                    }
                    reader.Close();
                    return string.Concat(new object[] { num5, "|", builder.ToString().Trim(), "|", num4 });
                }
                return "0|null";
            }
            catch
            {
                return "0|null";
            }
        }

        private void getList()
        {
        }

        [AjaxMethod]
        public string GetMsgTable(int intFMatchMsgID)
        {
            try
            {
                SqlDataReader friMatchMsgNew = BTPFriMatchMsgManager.GetFriMatchMsgNew(intFMatchMsgID);
                StringBuilder builder = new StringBuilder("");
                int num = 0;
                int num2 = 0;
                if (friMatchMsgNew.HasRows)
                {
                    while (friMatchMsgNew.Read())
                    {
                        DateTime datIn = (DateTime) friMatchMsgNew["CreateTime"];
                        num = (int) friMatchMsgNew["FMatchMsgID"];
                        string text1 = "[" + StringItem.FormatDate(datIn, "hh:mm") + "]";
                        string strIn = friMatchMsgNew["Content"].ToString().Trim();
                        int num3 = (int) friMatchMsgNew["UserID"];
                        string str2 = friMatchMsgNew["NickName"].ToString().Trim();
                        if ((bool) friMatchMsgNew["Sex"])
                        {
                            str2 = "<font color='#ff005a'>" + StringItem.GetShortString(str2, 10) + "</font>";
                        }
                        else
                        {
                            str2 = "<font color='blue'>" + StringItem.GetShortString(str2, 10) + "</font>";
                        }
                        builder.Append(string.Concat(new object[] { "<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num3, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str2, "</a>:", StringItem.GetShortString(strIn, 100), "&" }));
                        num2++;
                    }
                    friMatchMsgNew.Close();
                    return string.Concat(new object[] { num2, "|", builder.ToString().Trim(), "|", num });
                }
                return "0|null";
            }
            catch
            {
                return "0|null";
            }
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                this.strUserName = "";
                this.strPassword = "";
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strUserName = onlineRowByUserID["UserName"].ToString();
                this.strPassword = onlineRowByUserID["Password"].ToString();
                this.intCategory = Convert.ToInt32(onlineRowByUserID["Category"]);
            }
            if (this.intCategory == 5)
            {
                this.SetMsg();
            }
            else
            {
                this.SetMsg();
            }
            Utility.RegisterTypeForAjax(typeof(ForumElite));
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetList()
        {
            this.sbTodayElite.Append("<table width=\"184\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\">");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td height=\"22\" style=\"border-bottom:1px dotted #ad1a2c\">【<strong>新人专区</strong>】</td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td height=\"20\"> 1.新人入门全攻略</td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td style=\"padding:0px 0 20px 10px;\">完全新手指南，从组建球队到训练球员、设置战术一直到经营球队等的方方面面都有图文并茂的介绍 <a href=\"" + DBLogin.URLString(40) + "xbanews2.html\" target=\"_blank\">点这里查看</a></td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td>2.XBA篮球经理手册</td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td style=\"padding:0px 0 20px 10px;\">完整的XBA游戏攻略，由XBA官方编辑提供，为你在XBA里更好的游戏提供详尽指南 <a href=\"Handbook/Main.htm?Page=01\" target=\"_blank\">点这里查看</a>          </td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td>3.XBA玩家QQ群</td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("<tr>");
            this.sbTodayElite.Append("<td style=\"padding:0px 0 20px 10px;\">XBA各大联盟的QQ群，在这里你可以和广大XBA玩家共同交流进步 <br />");
            this.sbTodayElite.Append("<a href=\"http://www.xba.com.cn/images/home/zhuanti/union/union3.htm\" target=\"_blank\">点这里查看</a></td>");
            this.sbTodayElite.Append("</tr>");
            this.sbTodayElite.Append("</table>");
        }

        private void SetMsg()
        {
            SqlDataReader friMatchMsgNew = BTPFriMatchMsgManager.GetFriMatchMsgNew(0);
            int num = 0;
            int num2 = 0;
            if (friMatchMsgNew.HasRows)
            {
                while (friMatchMsgNew.Read())
                {
                    DateTime datIn = (DateTime) friMatchMsgNew["CreateTime"];
                    num = (int) friMatchMsgNew["FMatchMsgID"];
                    string text1 = "[" + StringItem.FormatDate(datIn, "hh:mm") + "]";
                    string strIn = friMatchMsgNew["Content"].ToString().Trim();
                    int num3 = (int) friMatchMsgNew["UserID"];
                    string str2 = friMatchMsgNew["NickName"].ToString().Trim();
                    if ((bool) friMatchMsgNew["Sex"])
                    {
                        str2 = "<font color='#ff005a'>" + StringItem.GetShortString(str2, 10) + "</font>";
                    }
                    else
                    {
                        str2 = "<font color='blue'>" + StringItem.GetShortString(str2, 10) + "</font>";
                    }
                    this.sbMsg.Append(string.Concat(new object[] { "<li onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><a href=\"javascript:;\" onclick=\"window.open('ShowClubIFrom.aspx?UserID=", num3, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');\">", str2, "</a>:", StringItem.GetShortString(strIn, 100), "</li>" }));
                    num2++;
                }
                friMatchMsgNew.Close();
                for (int i = 0x19 - num2; i > 0; i--)
                {
                    this.strAddMsg = this.strAddMsg + "<li>&nbsp;</li>";
                }
            }
            else
            {
                for (int j = 0x19 - num2; j > 0; j--)
                {
                    this.strAddMsg = this.strAddMsg + "<li>&nbsp;</li>";
                }
            }
            this.MaxID.Value = num.ToString();
        }

        private void SetsbList()
        {
            this.strTitle1 = "<a style=\"cursor:pointer\" onclick=\"javascript:window.top.Main.location='Main_P.aspx?Tag=" + this.intUserID + "&Type=DevLog'\">【联赛日志】...</a>";
            this.strTitle2 = "<a style=\"cursor:pointer\" onclick=\"javascript:window.top.Main.location='Main_P.aspx?Tag=" + this.intUserID + "&Type=DevMsg'\">【联赛留言】...</a>";
            string strDevCode = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["DevCode"].ToString().Trim();
            SqlDataReader reader = BTPDevMatchManager.GetDevLogByDevCode(0, 1, 3, strDevCode);
            int num = 0;
            this.sbTodayElite.Append("<div style=\"COLOR: red;margin-top:2px;color:red;margin-bottom: 3px\"><strong >" + this.strTitle1 + "</strong></div>");
            this.sbTodayElite.Append("<div style=\"Z-INDEX: 1; OVERFLOW: hidden; WIDTH: 195px; WORD-BREAK: break-all; LINE-HEIGHT: 20px; HEIGHT: 145px\">");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (num > 0)
                    {
                        this.sbTodayElite.Append("<div style=\"padding:0; margin-top:0px; margin-bottom:0px;OVERFLOW: hidden;margin-left:15px;WIDTH:165px;height:1px;background:url(Images/RM/Border_07.gif) repeat-x; \"></div>");
                    }
                    this.sbTodayElite.Append("<div onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">" + (("<font color='#660066'>[" + StringItem.FormatDate((DateTime) reader["CreateTime"], "MM-dd").Trim() + "]</font> ") + reader["LogEvent"].ToString().Trim()) + "</div>");
                    num = 1;
                }
                reader.Close();
            }
            else
            {
                this.sbTodayElite.Append("<div style=\"text-align:center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">暂无留言</div>");
            }
            this.sbTodayElite.Append("</div>");
            this.sbNewTopic.Append("<div style=\"Z-INDEX: 2; COLOR: red;margin-top:2px\"><strong >" + this.strTitle2 + "</strong></div>");
            this.sbNewTopic.Append("<div style=\"Z-INDEX:2; OVERFLOW:hidden; WIDTH:195px; WORD-BREAK:break-all; LINE-HEIGHT:20px; HEIGHT:145px\">");
            int devMessageCountByCode = BTPDevMessageManager.GetDevMessageCountByCode(strDevCode);
            DataTable table = BTPDevMessageManager.GetTableByDevCode(strDevCode, 1, 10, 7, devMessageCountByCode);
            num = 0;
            if (table != null)
            {
                foreach (DataRow row2 in table.Rows)
                {
                    if (num > 0)
                    {
                        this.sbNewTopic.Append("<div style=\"padding:0; margin-top:0px; margin-bottom:0px;OVERFLOW: hidden;margin-left:15px;WIDTH:165px;height:1px;background:url(Images/RM/Border_07.gif) repeat-x; \"></div>");
                    }
                    string str4 = row2["Content"].ToString().Trim();
                    string str5 = row2["NickName"].ToString().Trim();
                    this.sbNewTopic.Append("<div onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">" + (("<font color='#660066'>[" + StringItem.FormatDate((DateTime) row2["CreateTime"], "MM-dd").Trim() + "] " + str5 + ":</font>") + str4) + "</div>");
                    num = 1;
                }
            }
            else
            {
                this.sbNewTopic.Append("<div style=\"text-align:center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">暂无留言</div>");
            }
            this.sbNewTopic.Append("</div>");
        }
    }
}


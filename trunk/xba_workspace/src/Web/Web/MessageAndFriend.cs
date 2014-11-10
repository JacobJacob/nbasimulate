namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class MessageAndFriend : Page
    {
        protected ImageButton btnAdd;
        public int intCount;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public StringBuilder sbList = new StringBuilder();
        public StringBuilder sbList2 = new StringBuilder();
        public StringBuilder sbPageIntro = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbSearchPageIntro = new StringBuilder();
        public StringBuilder sbStringList = new StringBuilder();
        private string strNickName;
        private string strType;
        protected TextBox tbFriendName;
        protected HtmlTable tblAddFriend;
        protected HtmlTable tblFriend;
        protected HtmlTable tblMessage;
        protected HtmlTableCell tdDelAll;

        private void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            this.sbList2 = new StringBuilder();
            string validWords = StringItem.GetValidWords(this.tbFriendName.Text);
            if (!StringItem.IsValidName(validWords, 2, 0x10))
            {
                this.sbList2.Append("<font color='#FF0000'>您填写的经理名称有错误！</font>");
            }
            else if (validWords == this.strNickName)
            {
                this.sbList2.Append("<font color='#FF0000'>您无需将自己添加为好友！</font>");
            }
            else
            {
                switch (BTPFriendManager.SetFriend(this.intUserID, validWords))
                {
                    case -1:
                        this.sbList2.Append("<font color='#FF0000'>没有找到此经理！</font>");
                        return;

                    case 0:
                        this.sbList2.Append("<font color='#FF0000'>您已经添加过此经理为好友！</font>");
                        return;

                    case 2:
                        this.sbList2.Append("<font color='#FF0000'>您不能添加超过20个的好友！</font>");
                        return;
                }
                base.Response.Redirect("Report.aspx?Parameter=30");
            }
        }

        private void FriendList()
        {
            this.sbList = new StringBuilder();
            DataTable friendTableByUserID = BTPFriendManager.GetFriendTableByUserID(this.intUserID);
            if (friendTableByUserID != null)
            {
                foreach (DataRow row in friendTableByUserID.Rows)
                {
                    string str;
                    int intUserID = (int) row["FUserID"];
                    string strNickName = row["NickName"].ToString().Trim();
                    string strLogo = row["ClubLogo"].ToString().Trim();
                    int intLevels = (byte) row["Levels"];
                    byte num1 = (byte) row["Category"];
                    bool blnSex = (bool) row["Sex"];
                    string str4 = row["QQ"].ToString().Trim();
                    if (str4 != "")
                    {
                        str4 = "<a href=\"http://wpa.qq.com/msgrd?v=1&uin=" + str4 + "&site=www.xba.com.cn&menu=yes\" target=\"blank\"><img alt=\"有事您Q我！\" src=\"http://wpa.qq.com/pa?p=1:" + str4 + ":7\" border=\"0\" width=\"71\" height=\"24\"></a>";
                    }
                    else
                    {
                        str4 = "暂无资料";
                    }
                    if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                    {
                        str = "<font color='666666'>否</font>";
                    }
                    else
                    {
                        str = "<font color='Red'>是</font>";
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='50'></td>");
                    this.sbList.Append("<td>" + MessageItem.GetClubLogo(strLogo, intLevels) + "</td>");
                    this.sbList.Append("<td>" + AccountItem.GetNickNameInfoA(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbList.Append("<td>" + intLevels + "</td>");
                    this.sbList.Append("<td>" + str + "</td>");
                    this.sbList.Append("<td>" + str4 + "</td>");
                    this.sbList.Append("<td><a href='MessageCenter.aspx?Type=SENDMSG&UserID=" + intUserID + "'>消息</a>");
                    this.sbList.Append(string.Concat(new object[] { " | <a href='FriendDel.aspx?UserID=", intUserID, "' onclick=\"return FriendDel('", strNickName, "');\">删除</a></td>" }));
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
        }

        private void GetMsgScript(string strCurrentURL)
        {
            this.sbScript.Append("<script language=\"javascript\">");
            this.sbScript.Append("function JumpPage()");
            this.sbScript.Append("{");
            this.sbScript.Append("var strPage=document.all.Page.value;");
            this.sbScript.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.sbScript.Append("}");
            this.sbScript.Append("</script>");
        }

        private int GetMsgSendTotal()
        {
            return BTPMessageManager.GetMessageCountBySendID(this.intUserID);
        }

        private string GetMsgSendViewPage(string strCurrentURL)
        {
            string[] strArray;
            int msgSendTotal = this.GetMsgSendTotal();
            int num2 = (msgSendTotal / this.intPerPage) + 1;
            if ((msgSendTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str = "";
            if (this.intPage == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (this.intPage == num2)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str3 = str3 + " selected";
                }
                object obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            return string.Concat(new object[] { str, " ", str2, " 共", msgSendTotal, "个记录 跳转", str3 });
        }

        private int GetMsgTotal()
        {
            return BTPMessageManager.GetMessageCountByUserIDNew(this.intUserID);
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string[] strArray;
            int msgTotal = this.GetMsgTotal();
            int num2 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str = "";
            if (this.intPage == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (this.intPage == num2)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str3 = str3 + " selected";
                }
                object obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            return string.Concat(new object[] { str, " ", str2, " 共", msgTotal, "个记录 跳转", str3 });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MsgList()
        {
            DTOnlineManager.SetNoHasMsgByUserID(this.intUserID);
            this.sbList = new StringBuilder();
            string strCurrentURL = "MessageAndFriend.aspx?Type=MSGLIST&";
            this.intPerPage = 5;
            this.intPage = SessionItem.GetRequest("Page", 0);
            this.GetMsgTotal();
            this.GetMsgScript(strCurrentURL);
            DataTable table = BTPMessageManager.GetMessageTableByUserIDNew(this.intUserID, this.intPage, this.intPerPage);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int intMessageID = (int) row["MessageID"];
                    int intUserID = (int) row["SendID"];
                    string strNickName = row["Sender"].ToString().Trim();
                    int intCategory = (byte) row["Category"];
                    DateTime datIn = (DateTime) row["SendTime"];
                    string str3 = StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss");
                    string str4 = row["Content"].ToString();
                    this.sbList.Append("<tr class='BarHead'>");
                    if (intCategory == 3)
                    {
                        if (intUserID > 0)
                        {
                            string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                            this.sbList.Append(string.Concat(new object[] { "<td height='25' width='150'><div class=\"DIVPlayerName\"><font color='#FF0000'>联盟</font>[<a href=\"ShowClub.aspx?UserID=", intUserID, "&Type=3\" target=\"Right\">", StringItem.GetShortString(nickNameByUserID, 10, "."), "</a>]</div></td>" }));
                        }
                        else
                        {
                            this.sbList.Append("<td height='25' width='150'><div class=\"DIVPlayerName\" style=><font color='#FF0000'>联盟</font>[<font color='#999999'>系统</font>]</div></td>");
                        }
                    }
                    else if (intCategory == 5)
                    {
                        this.sbList.Append("<td height='25' width='150'><div class=\"DIVPlayerName\" style=><font color='#FF0000'>联盟</font>[<font color='#999999'>匿名</font>]</div></td>");
                    }
                    else
                    {
                        this.sbList.Append("<td height='25' width='150'>" + MessageItem.GetNickNameInfo(intUserID, strNickName, intCategory) + "</td>");
                    }
                    this.sbList.Append("<td width='130' align='left' style='padding-left:5px'>" + str3 + "</td>");
                    this.sbList.Append("<td width='50'></td>");
                    this.sbList.Append("<td width='210'>" + MessageItem.GetOptionInfo(intMessageID, intCategory, intUserID, strNickName) + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\" style='padding:4px;table-layout:fixed;word-break:break-all;'>");
                    this.sbList.Append("<td height='40' colspan='4'>" + str4 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td>");
                    this.sbList.Append("</tr>");
                }
                this.sbList.Append("<tr><td height='25' align='right' colspan='4'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
            }
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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.tblFriend.Visible = false;
                this.tblMessage.Visible = false;
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                switch (this.strType)
                {
                    case "MSGLIST":
                        this.tblMessage.Visible = true;
                        this.sbPageIntro.Append("<ul><li class='qian1'>短信列表</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageAndFriend.aspx?Type=SENDMSGMY'>发件箱</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=FRIENDLIST\"' href='MessageAndFriend.aspx?Type=FRIENDLIST'>好友列表</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                        this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        this.MsgList();
                        break;

                    case "SENDMSGMY":
                        this.tblMessage.Visible = true;
                        this.tdDelAll.InnerHtml = "<a href=\"MessageDel.aspx?MessageID=-1&Type=2\" onclick=\"return MessageDel(2);\">全部删除</a>";
                        this.tdDelAll.Visible = true;
                        this.sbPageIntro.Append("<ul><li class='qian1a'><a href='MessageAndFriend.aspx?Type=MSGLIST&Page=1'>短信列表</a></li>");
                        this.sbPageIntro.Append("<li class='qian2'>发件箱</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=FRIENDLIST\"' href='MessageAndFriend.aspx?Type=FRIENDLIST'>好友列表</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                        this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        this.SendMsgMyList();
                        break;

                    case "FRIENDLIST":
                        this.tblFriend.Visible = true;
                        this.sbPageIntro.Append("<ul><li class='qian1a'><a href='MessageAndFriend.aspx?Type=MSGLIST&Page=1'>短信列表</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageAndFriend.aspx?Type=SENDMSGMY'>发件箱</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                        this.sbPageIntro.Append("<li class='qian2'>好友列表</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                        this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        this.FriendList();
                        this.btnAdd.ImageUrl = SessionItem.GetImageURL() + "button_addf.gif";
                        this.btnAdd.Click += new ImageClickEventHandler(this.btnAdd_Click);
                        break;

                    default:
                        this.tblMessage.Visible = true;
                        this.sbPageIntro.Append("<ul><li class='qian1'>短信列表</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageAndFriend.aspx?Type=SENDMSGMY'>发件箱</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=FRIENDLIST\"' href='MessageAndFriend.aspx?Type=FRIENDLIST'>好友列表</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                        this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        this.MsgList();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SendMsgMyList()
        {
            this.sbList = new StringBuilder();
            string strCurrentURL = "MessageAndFriend.aspx?Type=SENDMSGMY&";
            this.intPerPage = 5;
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.GetMsgSendTotal();
            this.GetMsgScript(strCurrentURL);
            DataTable table = BTPMessageManager.GetMessageTableBySendID(this.intUserID, this.intPage, this.intPerPage);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2;
                    int num = (int) row["MessageID"];
                    int intUserID = (int) row["UserID"];
                    row["Sender"].ToString().Trim();
                    int intCategory = (byte) row["Category"];
                    DateTime datIn = (DateTime) row["SendTime"];
                    string str3 = StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss");
                    string str4 = row["Content"].ToString();
                    string strNickName = row["NameInfo"].ToString().Split(new char[] { '|' })[0].Trim();
                    strNickName = MessageItem.GetNickNameInfo(intUserID, strNickName, intCategory);
                    switch (intCategory)
                    {
                        case 1:
                            str2 = "<a href='MessageDel.aspx?Type=2&MessageID=" + num + "' onclick='return MessageDel(1);'>删除</a>";
                            break;

                        case 3:
                        case 5:
                            str2 = "<font color='#999999'>删除</font>";
                            strNickName = "联盟短信";
                            break;

                        default:
                            str2 = "<font color='#999999'>删除</font>";
                            break;
                    }
                    this.sbList.Append("<tr class='BarHead'>");
                    this.sbList.Append("<td height='25' width='100'>" + strNickName + "</td>");
                    this.sbList.Append("<td width='130' align='left' style='padding-left:5px'>" + str3 + "</td>");
                    this.sbList.Append("<td width='100'></td>");
                    this.sbList.Append("<td width='210'>" + str2 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\" style='padding:4px;table-layout:fixed;word-break:break-all;'>");
                    this.sbList.Append("<td height='40' colspan='4'>" + str4 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td>");
                    this.sbList.Append("</tr>");
                }
                this.sbList.Append("<tr><td height='25' align='right' colspan='4'>" + this.GetMsgSendViewPage(strCurrentURL) + "</td></tr>");
            }
        }
    }
}


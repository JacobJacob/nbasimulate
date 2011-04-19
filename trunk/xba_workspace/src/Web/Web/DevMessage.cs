namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class DevMessage : Page
    {
        protected ImageButton btnSend;
        protected ImageButton btnSendQ;
        protected ImageButton btnSendUnionMsg;
        protected TextBox ebContent;
        protected ImageButton Imagebutton1;
        private int intClubID5;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        private string strDevCode;
        public string strErrList;
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strReturn = "";
        public string strScript;
        private string strType;
        protected HtmlTable tblMessage;
        protected HtmlTable tblSendMsg;
        protected HtmlTable tblUnionMsg;
        protected TextBox tbNickName;
        protected TextBox tbQContent;
        protected TextBox tbUnionMsg;
        protected TextBox Textbox1;

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            string validWords = StringItem.GetValidWords(this.ebContent.Text);
            if (!StringItem.IsValidContent(validWords, 2, 200))
            {
                this.strList = "<font color='#FF0000'>留言内容不符合要求或存在非法字符！</font>";
            }
            else
            {
                BTPDevMessageManager.AddDevMessage(this.strDevCode, this.intUserID, this.strNickName, validWords);
                base.Response.Redirect("Report.aspx?Parameter=23!Type.VIEW^Page.1");
            }
        }

        private void btnSendQ_Click(object sender, ImageClickEventArgs e)
        {
            string validWords = StringItem.GetValidWords(this.tbQContent.Text);
            if (StringItem.IsValidContent(validWords, 2, 200))
            {
                if ((StringItem.GetStrLength(validWords) > 200) || (StringItem.GetStrLength(validWords) < 2))
                {
                    this.strErrList = "<font color='#FF0000'>留言内容必须在5-200个字符之间！</font>";
                }
                else
                {
                    BTPDevMessageManager.AddDevMessage(this.strDevCode, this.intUserID, this.strNickName, validWords);
                    base.Response.Redirect("DevMessage.aspx?Type=VIEW&Page=1");
                }
            }
            else
            {
                this.strErrList = "<font color='#FF0000'>留言内容中含有非法字符！</font>";
            }
        }

        private void btnSendUnionMsg_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbUnionMsg.Text.Trim();
            if (StringItem.IsValidContent(strIn, 2, 200))
            {
                switch (BTPUnionManager.AddMessageByUserID(this.intUserID, strIn))
                {
                    case 1:
                    {
                        int request = (int) SessionItem.GetRequest("Page", 0);
                        base.Response.Redirect("DevMessage.aspx?Type=UNIONMSG&Page=" + request);
                        return;
                    }
                    case -1:
                        this.strErrList = "<tr><td height=\"25\" align=\"center\" style=\"color:red\" colspan=\"3\">您还没有加入联盟！</td></tr>";
                        return;

                    case -2:
                        this.strErrList = "<tr><td height=\"25\" align=\"center\" style=\"color:red\" colspan=\"3\">您不能重复发言！</td></tr>";
                        return;
                }
                this.strErrList = "<tr><td height=\"25\" align=\"center\" style=\"color:red\" colspan=\"3\">发言时出现错误请重试！</td></tr>";
            }
            else
            {
                this.strErrList = "<tr><td height=\"25\" align=\"center\" style=\"color:red\" colspan=\"3\">您输入留言非法，请输入（2-200）个合法字符！</td></tr>";
            }
        }

        private string GetMsgScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetMsgTotal()
        {
            this.strType = (string) SessionItem.GetRequest("Type", 1);
            if ((this.strType != "VIEW") && (this.strType == "UNIONMSG"))
            {
                int intUnionID = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["UnionID"];
                return BTPUnionManager.GetMessageCount(intUnionID);
            }
            return BTPDevMessageManager.GetDevMessageCountByCode(this.strDevCode);
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
            string str2 = "";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        private void getUnionMsg()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int intUnionID = (int) accountRowByUserID["UnionID"];
            int num2 = (byte) accountRowByUserID["UnionCategory"];
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 5;
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            SqlDataReader reader = BTPUnionManager.GetMessageByUnionID(intUnionID, this.intPage, this.intPerPage, false);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int intUserID = (int) reader["UserID"];
                    string str = reader["Message"].ToString().Trim();
                    string strNickName = reader["NickName"].ToString().Trim();
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    int num4 = (int) reader["UnionMessageID"];
                    string str3 = "";
                    if (num2 == 1)
                    {
                        str3 = string.Concat(new object[] { "<a href='MessageDel.aspx?Page=", this.intPage, "&DType=1&MessageID=", num4, "' onclick='return MessageDel(1);'>删除</a>" });
                    }
                    string strList = this.strList;
                    this.strList = strList + "<tr class='BarHead'><td height='25' width='100'><font color='#660066'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20) + "</font></td><td width='260' align='left' style='padding-left:5px'>" + StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss") + "</td><td width='170'>" + str3 + "</td></tr><tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\" style='padding:4px;table-layout:fixed;word-break:break-all;'><td height='40' colspan='3' align='left' valign='middle'>" + str + "</td></tr>";
                }
                reader.Close();
                string strCurrentURL = "DevMessage.aspx?Type=UNIONMSG&";
                this.strScript = this.GetMsgScript(strCurrentURL);
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='3'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height='25' colspan='3' align='center'>暂无留言</td></tr>";
            }
            object strPageIntro = this.strPageIntro;
            this.strPageIntro = string.Concat(new object[] { strPageIntro, "<span style='margin-left:230px'><a href=\"Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", intUnionID, "&Page=1\" ><img align='absmiddle' border=\"0\" src=\"", SessionItem.GetImageURL(), "button_48.gif\" width=\"40\" height=\"24\"></a></span>" });
        }

        private void InitializeComponent()
        {
            switch (this.strType)
            {
                case "VIEW":
                    this.tblMessage.Visible = true;
                    this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_01.gif' border='0' height='24' width='76'><a href='DevMessage.aspx?Type=ADD'><img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_C_02.gif' border='0' height='24' width='75'></a>";
                    this.btnSendQ.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.MsgList();
                    this.tbQContent.Attributes.Add("onkeypress", "EnterTextBox(this)");
                    this.btnSendQ.Click += new ImageClickEventHandler(this.btnSendQ_Click);
                    break;

                case "ADD":
                    this.tblSendMsg.Visible = true;
                    this.strPageIntro = "<a href='DevMessage.aspx?Type=VIEW&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_C_01.gif' border='0' height='24' width='76'></a><img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_02.gif' border='0' height='24' width='75'>";
                    this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.SendMsg();
                    this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
                    this.btnSend.Attributes["onclick"] = base.GetPostBackEventReference(this.btnSend) + ";this.disabled=true;";
                    break;

                case "UNIONMSG":
                    this.strPageIntro = "<a href='Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1'><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_C_01.GIF' border='0' height='24' width='76' border='0'></a><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_02.GIF' border='0' height='24' width='75' border='0'><a href='Union.aspx?Type=UNIONCUP&Kind=UNIONCUPINDEX&Page=1'><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Union/Union_C_03.GIF' border='0' height='24' width='89' border='0'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.tblUnionMsg.Visible = true;
                    this.btnSendUnionMsg.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.getUnionMsg();
                    this.btnSendUnionMsg.Click += new ImageClickEventHandler(this.btnSendUnionMsg_Click);
                    this.tbUnionMsg.Attributes.Add("onkeypress", "EnterTextBox(this)");
                    this.btnSendUnionMsg.Attributes["onclick"] = base.GetPostBackEventReference(this.btnSendUnionMsg) + ";this.disabled=true;";
                    break;

                default:
                    this.tblMessage.Visible = true;
                    this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_01.gif' border='0' height='24' width='76'><a href='DevMessage.aspx?Type=ADD'><img src='" + SessionItem.GetImageURL() + "MenuCard/DevMessage/DevMessage_C_02.gif' border='0' height='24' width='75'></a>";
                    this.MsgList();
                    break;
            }
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MsgList()
        {
            string strCurrentURL = "DevMessage.aspx?Type=VIEW&";
            this.intPerPage = 4;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            int msgTotal = this.GetMsgTotal();
            this.strScript = this.GetMsgScript(strCurrentURL);
            DataTable table = BTPDevMessageManager.GetTableByDevCode(this.strDevCode, this.intPage, this.intPerPage, intCount, msgTotal);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string strNickName = row["NickName"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str2 = StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss");
                    string str3 = row["Content"].ToString();
                    string strList = this.strList;
                    this.strList = strList + "<tr class='BarHead'><td height='25' width='100'><font color='#660066'>" + MessageItem.GetNameInfo(strNickName) + "</font></td><td width='260' align='left' style='padding-left:5px'>" + str2 + "</td><td width='170'></td></tr><tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\" style='padding:4px;table-layout:fixed;word-break:break-all;'><td height='40' colspan='3' align='left' valign='middle'>" + str3 + "</td></tr>";
                }
                BTPDevManager.UpdateHasNewMsg(this.intClubID5);
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='3'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height='25' colspan='3' align='center'>暂无留言</td></tr>";
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
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.tblMessage.Visible = false;
                this.tblSendMsg.Visible = false;
                this.tblUnionMsg.Visible = false;
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.strDevCode = BTPDevManager.GetDevCodeByUserID(this.intUserID);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SendMsg()
        {
        }
    }
}


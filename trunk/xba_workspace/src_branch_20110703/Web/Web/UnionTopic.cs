namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class UnionTopic : Page
    {
        protected ImageButton btnSubmit;
        protected TextBox ebContent;
        protected HtmlForm Form1;
        private int intMyUnionID;
        private int intPage;
        private int intPerPage;
        private int intTopicID;
        private int intUnionID;
        private int intUserID;
        protected RadioButton rbLogo10;
        protected RadioButton rbLogo11;
        protected RadioButton rbLogo12;
        protected RadioButton rbLogo13;
        protected RadioButton rbLogo14;
        protected RadioButton rbLogo15;
        protected RadioButton rbLogo16;
        protected RadioButton rbLogo17;
        protected RadioButton rbLogo18;
        protected RadioButton rbLogo19;
        public string strList;
        public string strMaster;
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        public string strPageIntro1;
        public string strScript;
        public string strType;
        protected HtmlTable tblReply;

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (BTPUnionBBSManager.GetLatestByUserID(this.intUserID).AddSeconds(10.0) > DateTime.Now)
            {
                this.strMsg = "<p align=center><font color='#FF0000'>发表帖子间隔不能小于10秒！</font></p>";
            }
            else
            {
                string str;
                if (this.rbLogo10.Checked)
                {
                    str = "TopicPic10.gif";
                }
                else if (this.rbLogo11.Checked)
                {
                    str = "TopicPic11.gif";
                }
                else if (this.rbLogo12.Checked)
                {
                    str = "TopicPic12.gif";
                }
                else if (this.rbLogo13.Checked)
                {
                    str = "TopicPic13.gif";
                }
                else if (this.rbLogo14.Checked)
                {
                    str = "TopicPic14.gif";
                }
                else if (this.rbLogo15.Checked)
                {
                    str = "TopicPic15.gif";
                }
                else if (this.rbLogo16.Checked)
                {
                    str = "TopicPic16.gif";
                }
                else if (this.rbLogo17.Checked)
                {
                    str = "TopicPic17.gif";
                }
                else if (this.rbLogo18.Checked)
                {
                    str = "TopicPic18.gif";
                }
                else
                {
                    str = "TopicPic19.gif";
                }
                string validWords = StringItem.GetValidWords(this.ebContent.Text);
                if (!StringItem.IsValidName(validWords, 10, 0xea60))
                {
                    this.strMsg = "<p align=center><font color='#FF0000'>正文内容不符合规则或还有非法字符！</font></p>";
                }
                else
                {
                    DataRow unionTopicRowByID = BTPUnionBBSManager.GetUnionTopicRowByID(this.intTopicID);
                    if (unionTopicRowByID == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                    else
                    {
                        string str6;
                        string strMainTitle = unionTopicRowByID["Title"].ToString();
                        string strMainLogo = unionTopicRowByID["Logo"].ToString().Trim();
                        string strTitle = "Re:" + strMainTitle;
                        try
                        {
                            string strSendIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                            BTPUnionBBSManager.AddUnionReply(this.intUnionID, this.intUserID, this.strNickName, str, strTitle, validWords, this.intTopicID, strMainTitle, strMainLogo, strSendIP);
                            str6 = string.Concat(new object[] { "Report.aspx?Parameter=4005!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" });
                        }
                        catch
                        {
                            str6 = "Report.aspx?Parameter=3";
                        }
                        base.Response.Redirect(str6);
                    }
                }
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPUnionBBSManager.GetUnionTopicDetailCount(this.intUnionID, this.intTopicID);
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
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
                obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            obj2 = str2 + " " + str3 + " ";
            return string.Concat(new object[] { obj2, "帖子总数:", total, " 跳转", str4 });
        }

        private void InitializeComponent()
        {
            this.btnSubmit.Click += new ImageClickEventHandler(this.btnSubmit_Click);
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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intMyUnionID = (int) onlineRowByUserID["UnionID"];
                this.intTopicID = (int) SessionItem.GetRequest("TopicID", 0);
                this.intUnionID = (int) SessionItem.GetRequest("UnionID", 0);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                DataRow unionBoardRowByUnionID = BTPUnionBBSManager.GetUnionBoardRowByUnionID(this.intUnionID);
                if (unionBoardRowByUnionID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string strBoardName = unionBoardRowByUnionID["Name"].ToString();
                    int num = (byte) unionBoardRowByUnionID["Category"];
                    if ((num == 1) && (this.intUnionID != this.intMyUnionID))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=58u");
                    }
                    else
                    {
                        this.strType = SessionItem.GetRequest("Type", 1).ToString();
                        this.strPageIntro = BoardItem.GetUnionBBSPageIntro(this.strType, this.intUnionID);
                        this.strPageIntro1 = BoardItem.GetUnionBBSPageIntro1(false, this.intUnionID, strBoardName, this.strType, this.intUserID);
                        this.btnSubmit.ImageUrl = SessionItem.GetImageURL() + "button_21.gif";
                        this.InitializeComponent();
                        base.OnInit(e);
                    }
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intPerPage = 10;
            this.SetList();
        }

        private void SetList()
        {
            string strCurrentURL = string.Concat(new object[] { "UnionTopic.aspx?Type=", this.strType, "&TopicID=", this.intTopicID, "&UnionID=", this.intUnionID, "&" });
            this.strScript = this.GetScript(strCurrentURL);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            DataTable table = BTPUnionBBSManager.GetUnionTopicDetailTable(this.intUnionID, this.intTopicID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                int num3 = RandomItem.rnd.Next(0, 10);
                string str11 = "";
                bool flag3 = false;
                this.strList = "<tr bgcolor='#fcc6a4'><td width='85' height='25' class='Forum001'>发表人</td>";
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    string str2;
                    string str4;
                    string str6;
                    string str7;
                    string str8;
                    int num6;
                    object strList;
                    DataRow row = table.Rows[i];
                    int num7 = (int) row["ReplyID"];
                    if (i == 0)
                    {
                        string unionMainTitle;
                        if (num7 == 0)
                        {
                            unionMainTitle = row["Title"].ToString().Trim();
                            this.strList = this.strList + "<td class='Forum001'>主题：" + unionMainTitle + "</td></tr>";
                            flag3 = Convert.ToBoolean(row["OnLock"]);
                        }
                        else
                        {
                            unionMainTitle = BTPUnionBBSManager.GetUnionMainTitle(this.intTopicID);
                            this.strList = this.strList + "<td class='Forum001'>主题：" + unionMainTitle + "</td></tr>";
                        }
                    }
                    string str3 = row["Logo"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    int num4 = (int) row["TopicID"];
                    string str5 = row["Content"].ToString().Trim();
                    int intUserID = (int) row["UserID"];
                    try
                    {
                        DataRow row2 = ROOTUserManager.GetUserInfoByID40(intUserID);
                        str6 = "";
                        str7 = row2["DiskURL"].ToString().Trim() + "Face.png";
                        str2 = row2["NickName"].ToString().Trim();
                        DateTime time2 = (DateTime) row2["CreateTime"];
                        StringItem.FormatDate(time2, "yyyy-MM-dd");
                    }
                    catch
                    {
                        str6 = "";
                        str7 = SessionItem.GetImageURL() + "Images/Face/Del.png";
                        num6 = 0;
                        str2 = "<font color='#666666'>用户已注销</font>";
                    }
                    bool flag = (bool) row["Elite"];
                    bool flag2 = (bool) row["OnTop"];
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(intUserID);
                    if (accountRowByUserID != null)
                    {
                        num6 = Convert.ToInt16(accountRowByUserID["PayType"]);
                    }
                    else
                    {
                        num6 = 0;
                    }
                    if (num6 == 1)
                    {
                        str8 = "<font color='red'>[会员]</font>";
                    }
                    else
                    {
                        str8 = "[普通用户]";
                    }
                    if ((i % 2) == 1)
                    {
                        str4 = "#FCF1EB";
                    }
                    else
                    {
                        str4 = "#FFFFFF";
                    }
                    DataRow row4 = BTPAccountManager.GetAccountRowByUserID(intUserID);
                    int num9 = (byte) row4["UnionCategory"];
                    string str10 = row4["UnionPosition"].ToString().Trim();
                    if (str10 == "")
                    {
                        switch (num9)
                        {
                            case 1:
                                str10 = "盟主";
                                goto Label_0362;

                            case 2:
                                str10 = "管理人员";
                                goto Label_0362;
                        }
                        str10 = "盟员";
                    }
                Label_0362:
                    if (BoardItem.IsUnionBoardMaster(this.intUserID, this.intUnionID))
                    {
                        if (this.intUserID == intUserID)
                        {
                            str11 = string.Concat(new object[] { " <a href='UnionTopicEdit.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "'>编辑</a> <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=DELETE' onclick='return TopicChange(1);'>删除</a>" });
                        }
                        else
                        {
                            str11 = string.Concat(new object[] { " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=DELETE' onclick='return TopicChange(1);'>删除</a>" });
                        }
                        if (num7 == 0)
                        {
                            if (flag)
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=CANCELELITE' onclick='return TopicChange(2);'>取消精华</a>" });
                            }
                            else
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=ELITE' onclick='return TopicChange(3);'>精华</a>" });
                            }
                            if (flag2)
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=CANCELONTOP' onclick='return TopicChange(4);'>取消置顶</a>" });
                            }
                            else
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=ONTOP' onclick='return TopicChange(5);'>置顶</a>" });
                            }
                            if (flag3)
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=UNLOCK' onclick='return TopicChange(6);'>解锁</a>" });
                            }
                            else
                            {
                                strList = str11;
                                str11 = string.Concat(new object[] { strList, " <a href='UnionTopicOprate.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "&OP=LOCK' onclick='return TopicChange(7);'>锁帖</a>" });
                            }
                        }
                    }
                    else if (this.intUserID == intUserID)
                    {
                        str11 = string.Concat(new object[] { " <a href='UnionTopicEdit.aspx?Type=", this.strType, "&TopicID=", num4, "&UnionID=", this.intUnionID, "'>编辑</a>" });
                    }
                    else
                    {
                        str11 = "";
                    }
                    strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr bgcolor='", str4, "'><td height='150' valign='top' width='85'><table width='85' cellspacing='0' cellpadding='0'><tr><td height='5'></td></tr><tr><td height='20' class='ForumUName'>", StringItem.GetShortString(str2, 10, "."), "</td></tr><tr><td height='20' class='ForumLogin'>", str8, "</td></tr><tr><td height='5'></td></tr><tr><td height='40' class='ForumLogin'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str7, "?RndID=", num3, ");width:37px;height:40px'></div></td></tr><tr><td height='5'></td></tr><tr><td height='25'  class='ForumLevel'>", str10, "</td></tr></table></td><td width='440'><table width='440' cellspacing='0' cellpadding='0' height='100%' style='padding:4px;table-layout:fixed;word-break:break-all;'><tr><td height='*'><img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/", 
                        str3, "' width='12' height='12' border=0 align='absmiddle'> ", StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh:mm:ss</font>"), str11, "<hr size='1' noshade color='#FCC6A4'></td></tr><tr><td valign='Top' height='100%' style='color:#333333;'>", str5, "</td></tr><tr><td height='*'><hr size='1' noshade color='#FCC6A4'>", str6, "</td></tr></table></td></tr>"
                     });
                }
                this.strList = this.strList + "<tr><td height='30' colspan='6' align='right' style='padding-right:15px'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
                if (flag3)
                {
                    this.tblReply.Visible = false;
                }
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class UnionBoard : Page
    {
        private int intMyUnionID;
        private int intPage;
        private int intPerPage;
        private int intUnionID;
        private int intUserID;
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strPageIntro1;
        public string strScript;
        public string strType;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPUnionBBSManager.GetUnionTopicCount(this.intUnionID);
        }

        public string GetViewPage(string strCurrentURL)
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
                        this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                        this.strPageIntro = BoardItem.GetUnionBBSPageIntro(this.strType, this.intUnionID);
                        this.strPageIntro1 = BoardItem.GetUnionBBSPageIntro1(false, this.intUnionID, strBoardName, this.strType, this.intUserID);
                        this.InitializeComponent();
                        base.OnInit(e);
                    }
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intPerPage = 11;
            this.SetList();
        }

        private void SetList()
        {
            string strCurrentURL = string.Concat(new object[] { "UnionBoard.aspx?Type=", this.strType, "&UnionID=", this.intUnionID, "&" });
            this.strScript = this.GetScript(strCurrentURL);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            DataTable table = BTPUnionBBSManager.GetUnionTopicTable(this.intUnionID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='25' align='center' colspan='4'><font color='red'>暂时没有任何信息。</font></td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2;
                    string str3 = row["Logo"].ToString().Trim();
                    string str4 = row["Title"].ToString().Trim();
                    string str5 = row["NickName"].ToString().Trim();
                    string str6 = row["ReplyUser"].ToString().Trim();
                    int num3 = (int) row["TopicID"];
                    bool flag = (bool) row["OnTop"];
                    bool flag2 = (bool) row["Hot"];
                    bool flag3 = (bool) row["Elite"];
                    bool flag4 = (bool) row["OnLock"];
                    if (flag)
                    {
                        str2 = "OnTop.gif";
                    }
                    else if (flag3)
                    {
                        str2 = "Elite.gif";
                    }
                    else if (flag2)
                    {
                        str2 = "Hot.gif";
                    }
                    else if (flag4)
                    {
                        str2 = "OnLock.gif";
                    }
                    else
                    {
                        str2 = "Ordinarily.gif";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr><td height='25' align='center'><img src='", SessionItem.GetImageURL(), "Forum/", str2, "' width='15' height='19' border=0 align='absmiddle'></td><td class='Forum003'><img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/", str3, "' width='12' height='12' border=0 align='absmiddle'> <a href='UnionTopic.aspx?Type=", this.strType, "&TopicID=", num3, "&UnionID=", this.intUnionID, "&Page=1'> ", 
                        str4, "</a></td><td align='center'>", str5, "</td><td class='Forum005' align='center'>", str6, " </td></tr>"
                     });
                }
                this.strList = this.strList + "<tr><td height='30' colspan='4' align='right' style='padding-right:10px'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
        }
    }
}


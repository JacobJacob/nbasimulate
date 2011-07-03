namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Search : Page
    {
        protected ImageButton btnSearch;
        protected DropDownList ddlCategory;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public StringBuilder sb = new StringBuilder();
        private string strCategory;
        private string strKeyword;
        private string strNickName;
        public string strPageInfo;
        public string strPageIntro;
        private string strPassword;
        public string strScript;
        private string strUserName;
        protected TextBox tbKeyword;

        private void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            bool flag = true;
            if (this.Session["SearchTime"] != null)
            {
                DateTime time = (DateTime) this.Session["SearchTime"];
                if (time.AddSeconds(30.0) > DateTime.Now)
                {
                    flag = false;
                }
            }
            if (flag)
            {
                this.Session["SearchTime"] = DateTime.Now;
                this.SetList();
            }
            else
            {
                this.sb = new StringBuilder();
                this.sb.Append("<tr><td height='50' colspan='6' align='center'>本论坛设置搜索间隔时间为30秒</td></tr>");
            }
        }

        public string GetScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language=\"javascript\">");
            builder.Append("function JumpPage()");
            builder.Append("{");
            builder.Append("var strPage=document.all.Page.value;");
            builder.Append("window.location=\"Search.aspx?Type=1&Page=\"+strPage;");
            builder.Append("}");
            builder.Append("</script>");
            return builder.ToString();
        }

        public int GetTotal()
        {
            return ROOTTopicManager.GetSearchCountNew(this.strKeyword, this.strCategory);
        }

        public string GetViewPage()
        {
            StringBuilder builder = new StringBuilder();
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
            string str = "";
            string str2 = "";
            string str3 = "";
            if (this.intPage == 1)
            {
                str = "<font color='#A9A9A9'>首页</font>";
                str3 = "<font color='#A9A9A9'>上一页</font>";
            }
            else
            {
                int num4 = this.intPage - 1;
                str3 = "<a href='Search.aspx?Type=1&Page=" + num4.ToString() + "'>上一页</a>";
                str = "<a href='Search.aspx?Type=1&Page=1'>首页</a>";
            }
            string str4 = "";
            if (this.intPage == num2)
            {
                str4 = "<font color='#A9A9A9'>下一页</font>";
                str2 = "<font color='#A9A9A9'>尾页</font>";
            }
            else
            {
                str4 = "<a href='Search.aspx?Type=1&Page=" + ((this.intPage + 1)).ToString() + "'>下一页</a>";
                str2 = "<a href='Search.aspx?Type=1&Page=" + num2 + "'>尾页</a>";
            }
            string str5 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str5 = str5 + "<option value=" + i;
                if (i == Convert.ToInt32(this.intPage))
                {
                    str5 = str5 + " selected";
                }
                object obj2 = str5;
                str5 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str5 = str5 + "</select>";
            builder.Append("<font class='Forum001'>分页</font> ");
            builder.Append(str);
            builder.Append(" ");
            builder.Append(str3);
            builder.Append(" ");
            builder.Append(str4);
            builder.Append(" ");
            builder.Append(str2);
            builder.Append(" 页次：");
            builder.Append(this.intPage);
            builder.Append("/");
            builder.Append(num2);
            builder.Append("页 ");
            builder.Append(this.intPerPage);
            builder.Append("个记录/页 共");
            builder.Append(total);
            builder.Append("个记录 跳转");
            builder.Append(str5);
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            this.btnSearch.ImageUrl = SessionItem.GetImageURL() + "Button_10.gif";
            this.btnSearch.Click += new ImageClickEventHandler(this.btnSearch_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID > 0)
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=12a");
                return;
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 20;
            this.strScript = this.GetScript();
            if ((this.intPage > 0) && (this.Session["strKeyword"] != null))
            {
                this.SetList();
            }
            else
            {
                this.sb.Append("<tr><td height='50' colspan='6' align='center'>请输入关键字进行查找</td></tr>");
            }
            this.SetPageIntro();
            this.SetPageInfo();
        }

        private void SetList()
        {
            if ((this.intPage == 1) && (this.tbKeyword.Text.ToString().Trim() != ""))
            {
                this.strKeyword = this.tbKeyword.Text.ToString().Trim();
                this.Session["strKeyword"] = this.strKeyword;
                this.strCategory = this.ddlCategory.SelectedItem.Value.ToString();
                this.Session["strCategory"] = this.strCategory;
            }
            else if (this.Session["strKeyword"] != null)
            {
                this.strKeyword = this.Session["strKeyword"].ToString();
                this.strCategory = this.Session["strCategory"].ToString();
            }
            this.GetTotal();
            this.sb = new StringBuilder();
            if (!(this.strKeyword != ""))
            {
                this.sb.Append("<tr><td height='50' colspan='6' class='Forum0011'>您在查找之前必须设置一个关键字！</td></tr>");
            }
            else
            {
                SqlDataReader reader = null;//ROOTTopicManager.GetSearchTableNew(this.strKeyword, this.strCategory, this.intPage, this.intPerPage);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string str;
                        string str2 = reader["Logo"].ToString().Trim();
                        int num = (int) reader["Hits"];
                        string str3 = reader["Title"].ToString().Trim();
                        string str4 = reader["NickName"].ToString().Trim();
                        int num2 = (int) reader["ReplyCount"];
                        DateTime datIn = (DateTime) reader["ReplyTime"];
                        string str5 = reader["ReplyUser"].ToString().Trim();
                        int num3 = (int) reader["TopicID"];
                        bool flag = (bool) reader["OnTop"];
                        bool flag2 = (bool) reader["Hot"];
                        bool flag3 = (bool) reader["Elite"];
                        bool flag4 = (bool) reader["OnLock"];
                        string str6 = reader["BoardID"].ToString().Trim();
                        if (flag)
                        {
                            str = "OnTop.gif";
                        }
                        else if (flag3)
                        {
                            str = "Elite.gif";
                        }
                        else if (flag2)
                        {
                            str = "Hot.gif";
                        }
                        else if (flag4)
                        {
                            str = "OnLock.gif";
                        }
                        else
                        {
                            str = "Ordinarily.gif";
                        }
                        if (str5 != "")
                        {
                            str5 = "by " + str5;
                        }
                        this.sb.Append("<tr>");
                        this.sb.Append("<td height='25' align='center'><img src='" + SessionItem.GetImageURL() + "Forum/" + str + "' width='15' height='19' border=0 align='absmiddle'></td>");
                        this.sb.Append("<td align='center'>" + num + "</td>");
                        this.sb.Append("<td class='Forum003'><img src='" + SessionItem.GetImageURL() + "Forum/TopicLogo/");
                        this.sb.Append(string.Concat(new object[] { str2, "' width='12' height='12' border=0 align='absmiddle'> <a href='Topic.aspx?TopicID=", num3, "&BoardID=", str6, "&Page=1'> ", str3, "</a></td>" }));
                        this.sb.Append("<td align='center'>" + str4 + "</td>");
                        this.sb.Append("<td align='center'>" + num2 + "</td>");
                        this.sb.Append("<td class='Forum005'>" + StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh-mm-ss</font>") + " " + str5 + " </td>");
                        this.sb.Append("</tr>");
                    }
                    reader.Close();
                    this.sb.Append("<tr>");
                    this.sb.Append("<td height='30' colspan='6' align='right' style='padding-right:15px'>" + this.GetViewPage() + "</td>");
                    this.sb.Append("</tr>");
                }
                else
                {
                    this.sb.Append("<tr><td height='50' colspan='6' class='Forum0011'>未找到相关的帖子！</td></tr>");
                }
            }
        }

        private void SetPageInfo()
        {
            this.strPageInfo = "<font color='#333333'>您的位置：</font> <a href='Forum.aspx'>论坛首页</a> &gt; 帖子搜索";
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


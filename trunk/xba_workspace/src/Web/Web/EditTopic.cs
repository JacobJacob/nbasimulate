namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;
    using Web.MyControls;

    public class EditTopic : Page
    {
        protected Button btnSubmit;
        protected DropDownList ddlKeyword;
        protected EditBox ebContent;
        private int intPage;
        private int intReplyTopicID;
        private int intTopicID;
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
        private string strBoardID;
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        private string strPassword;
        private string strUserName;
        protected TextBox tbTitle;
        protected HtmlTableRow trKeyword;

        private void Assign()
        {
            DataRow topicRowByID = ROOTTopicManager.GetTopicRowByID(this.intTopicID);
            if (topicRowByID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                int num = (int) topicRowByID["UserID"];
                if (this.intUserID != num)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string str = topicRowByID["Title"].ToString();
                    string str2 = topicRowByID["Keyword"].ToString().Trim();
                    string str3 = topicRowByID["Logo"].ToString().Trim();
                    string str4 = topicRowByID["Content"].ToString();
                    this.intReplyTopicID = (int) topicRowByID["ReplyID"];
                    if (this.intReplyTopicID == 0)
                    {
                        this.intReplyTopicID = this.intTopicID;
                    }
                    this.Session["ReplyTopicID"] = this.intReplyTopicID;
                    switch (str3)
                    {
                        case "TopicPic10.gif":
                            this.rbLogo10.Checked = true;
                            break;

                        case "TopicPic11.gif":
                            this.rbLogo11.Checked = true;
                            break;

                        case "TopicPic12.gif":
                            this.rbLogo12.Checked = true;
                            break;

                        case "TopicPic13.gif":
                            this.rbLogo13.Checked = true;
                            break;

                        case "TopicPic14.gif":
                            this.rbLogo14.Checked = true;
                            break;

                        case "TopicPic15.gif":
                            this.rbLogo15.Checked = true;
                            break;

                        case "TopicPic16.gif":
                            this.rbLogo16.Checked = true;
                            break;

                        case "TopicPic17.gif":
                            this.rbLogo17.Checked = true;
                            break;

                        case "TopicPic18.gif":
                            this.rbLogo18.Checked = true;
                            break;

                        case "TopicPic19.gif":
                            this.rbLogo19.Checked = true;
                            break;

                        default:
                            this.rbLogo10.Checked = true;
                            break;
                    }
                    switch (str2)
                    {
                        case "":
                            this.ddlKeyword.SelectedIndex = 0;
                            break;

                        case "新闻":
                            this.ddlKeyword.SelectedIndex = 1;
                            break;

                        case "开发":
                            this.ddlKeyword.SelectedIndex = 2;
                            break;

                        case "活动":
                            this.ddlKeyword.SelectedIndex = 3;
                            break;

                        default:
                            this.ddlKeyword.SelectedIndex = 0;
                            break;
                    }
                    this.tbTitle.Text = str;
                    this.ebContent.Text = str4;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string str2;
            string strKeyword = "";
            if (BoardItem.IsMainMaster(this.intUserID))
            {
                strKeyword = this.ddlKeyword.SelectedValue;
            }
            if (this.rbLogo10.Checked)
            {
                str2 = "TopicPic10.gif";
            }
            else if (this.rbLogo11.Checked)
            {
                str2 = "TopicPic11.gif";
            }
            else if (this.rbLogo12.Checked)
            {
                str2 = "TopicPic12.gif";
            }
            else if (this.rbLogo13.Checked)
            {
                str2 = "TopicPic13.gif";
            }
            else if (this.rbLogo14.Checked)
            {
                str2 = "TopicPic14.gif";
            }
            else if (this.rbLogo15.Checked)
            {
                str2 = "TopicPic15.gif";
            }
            else if (this.rbLogo16.Checked)
            {
                str2 = "TopicPic16.gif";
            }
            else if (this.rbLogo17.Checked)
            {
                str2 = "TopicPic17.gif";
            }
            else if (this.rbLogo18.Checked)
            {
                str2 = "TopicPic18.gif";
            }
            else
            {
                str2 = "TopicPic19.gif";
            }
            string validWords = StringItem.GetValidWords(StringItem.SetValidWord(this.tbTitle.Text.ToString().Trim()));
            if (!StringItem.IsValidName(validWords, 2, 100))
            {
                this.strMsg = "<font color='#FF0000'>标题内容必须合法且大于2个字符小于100个字符！</font>";
            }
            else
            {
                string strIn = StringItem.GetValidWords(this.ebContent.Text);
                if (!StringItem.IsValidName(strIn, 10, 0xea60))
                {
                    this.strMsg = "<p align=center><font color='#FF0000'>正文内容必须大于10个字符且不得超过60000个字符！</font></p>";
                }
                else
                {
                    string str5;
                    strIn = strIn + "<p align=right>此帖由作者于" + StringItem.FormatDate(DateTime.Now, "yyyy-MM-dd <font CLASS=ForumTime>hh-mm-ss</font>") + "编辑</p>";
                    this.intReplyTopicID = (int) this.Session["ReplyTopicID"];
                    try
                    {
                        ROOTTopicManager.EditTopic(this.intTopicID, this.strNickName, strKeyword, str2, strIn, validWords);
                        str5 = string.Concat(new object[] { "Report.aspx?Parameter=4002!BoardID.", this.strBoardID, "^TopicID.", this.intReplyTopicID, "^Page.", this.intPage });
                    }
                    catch
                    {
                        str5 = "Report.aspx?Parameter=3";
                    }
                    base.Response.Redirect(str5);
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
                this.intTopicID = (int) SessionItem.GetRequest("TopicID", 0);
                this.strBoardID = (string) SessionItem.GetRequest("BoardID", 1);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                this.trKeyword.Visible = false;
                this.tbTitle.Enabled = false;
                if (BoardItem.IsMainMaster(this.intUserID))
                {
                    this.trKeyword.Visible = true;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Assign();
            }
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


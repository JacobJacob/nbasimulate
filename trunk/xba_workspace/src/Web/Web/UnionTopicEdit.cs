namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;
    using Web.MyControls;

    public class UnionTopicEdit : Page
    {
        protected ImageButton btnSubmit;
        protected EditBox ebContent;
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
        public string strMsg;
        public string strNickName;
        public string strPageIntro;
        public string strPageIntro1;
        public string strScript;
        public string strType;
        protected TextBox tbTitle;

        public void Assign()
        {
            DataRow unionTopicRowByID = BTPUnionBBSManager.GetUnionTopicRowByID(this.intTopicID);
            if (unionTopicRowByID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                int num = (int) unionTopicRowByID["UserID"];
                if (this.intUserID != num)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string str = unionTopicRowByID["Title"].ToString();
                    unionTopicRowByID["Keyword"].ToString().Trim();
                    string str2 = unionTopicRowByID["Logo"].ToString().Trim();
                    string str3 = unionTopicRowByID["Content"].ToString();
                    switch (str2)
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
                    this.tbTitle.Text = str;
                    this.ebContent.Text = str3;
                }
            }
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
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
                this.strMsg = "<p align=center><font color='#FF0000'>正文内容必须大于10个字符且不得超过60000个字符！</font></p>";
            }
            else
            {
                string str3;
                validWords = validWords + "<p align=right>此帖由作者于" + StringItem.FormatDate(DateTime.Now, "yyyy-MM-dd <font CLASS=ForumTime>hh-mm-ss</font>") + "编辑</p>";
                try
                {
                    BTPUnionBBSManager.EditUnionTopic(this.intTopicID, this.strNickName, str, validWords);
                    str3 = string.Concat(new object[] { "Report.aspx?Parameter=4006!Type.", this.strType, "^UnionID.", this.intUnionID, "^Page.1" });
                }
                catch
                {
                    str3 = "Report.aspx?Parameter=3";
                }
                base.Response.Redirect(str3);
            }
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
                this.intTopicID = SessionItem.GetRequest("TopicID", 0);
                this.intUnionID = SessionItem.GetRequest("UnionID", 0);
                DataRow unionBoardRowByUnionID = BTPUnionBBSManager.GetUnionBoardRowByUnionID(this.intUnionID);
                if (unionBoardRowByUnionID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    string strBoardName = unionBoardRowByUnionID["Name"].ToString();
                    this.strType = SessionItem.GetRequest("Type", 1).ToString();
                    this.strPageIntro = BoardItem.GetUnionBBSPageIntro(this.strType, this.intUnionID);
                    this.strPageIntro1 = BoardItem.GetUnionBBSPageIntro1(false, this.intUnionID, strBoardName, this.strType, this.intUserID);
                    this.btnSubmit.ImageUrl = SessionItem.GetImageURL() + "button_23.gif";
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                this.Assign();
            }
        }
    }
}


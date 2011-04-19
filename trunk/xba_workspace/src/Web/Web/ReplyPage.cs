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

    public class ReplyPage : Page
    {
        private bool blnCanReply = true;
        protected Button btnSubmit;
        protected DropDownList ddlKeyword;
        protected EditBox ebContent;
        private int intPage;
        private int intTopicID;
        public int intUserID;
        public int intWealth;
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
        public string strMaster;
        public string strMsg;
        public string strNickName;
        public string strPageInfo;
        public string strPageIntro;
        public string strPassword;
        public string strUserName;
        protected TextBox tbTitle;
        protected HtmlTableRow trKeyword;

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else if (ROOTLockedIPManager.GetLockedIPRowByIP(base.Request.UserHostAddress.ToString()) != null)
            {
                base.Response.Redirect("Report.aspx?Parameter=502!BoardID." + this.strBoardID + "^Page.1");
            }
            else if (ROOTTopicManager.GetLatestByUserID(this.intUserID).AddSeconds(10.0) > DateTime.Now)
            {
                this.strMsg = "<p align=center><font color='#FF0000'>发表帖子间隔不能小于10秒！</font></p>";
            }
            else
            {
                string str2;
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
                string validWords = StringItem.GetValidWords(this.ebContent.Text);
                if (!StringItem.IsValidName(validWords, 10, 0xea60))
                {
                    this.strMsg = "<p align=center><font color='#FF0000'>正文内容必须大于10个字符且不得超过60000个字符！</font></p>";
                }
                else
                {
                    string str7;
                    DataRow topicRowByID = ROOTTopicManager.GetTopicRowByID(this.intTopicID);
                    string strMainTitle = topicRowByID["Title"].ToString();
                    string strMainLogo = topicRowByID["Logo"].ToString().Trim();
                    string strTitle = "Re:" + strMainTitle;
                    try
                    {
                        ROOTTopicManager.AddReply(this.strBoardID, this.intUserID, this.strNickName, str2, strTitle, validWords, this.intTopicID, strMainTitle, strMainLogo, false);
                        ROOTTopicManager.AddTopicWealth(this.intUserID, 1);
                        str7 = string.Concat(new object[] { "Report.aspx?Parameter=4001!BoardID.", this.strBoardID, "^TopicID.", this.intTopicID, "^Page.", this.intPage });
                    }
                    catch
                    {
                        str7 = "Report.aspx?Parameter=3";
                    }
                    base.Response.Redirect(str7);
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
                return;
            }
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strNickName = onlineRowByUserID["NickName"].ToString();
            this.strUserName = onlineRowByUserID["UserName"].ToString();
            this.strPassword = onlineRowByUserID["Password"].ToString();
            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
            this.intWealth = (int) userInfoByID["Wealth"];
            int num = (byte) userInfoByID["LockTime"];
            if (num > 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=19b");
                return;
            }
            this.strBoardID = (string) SessionItem.GetRequest("BoardID", 1);
            this.intTopicID = (int) SessionItem.GetRequest("TopicID", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.strBoardID == "")
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            this.trKeyword.Visible = false;
            DataRow boardRowByBoardID = ROOTBoardManager.GetBoardRowByBoardID(this.strBoardID);
            if (boardRowByBoardID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                this.strMaster = boardRowByBoardID["Master"].ToString().Trim();
                switch (((byte) boardRowByBoardID["Category"]))
                {
                    case 1:
                        if ((this.intUserID > -1) && (this.intWealth >= 0))
                        {
                            if (!BoardItem.IsBoardMaster(this.intUserID, this.strMaster))
                            {
                                this.blnCanReply = true;
                            }
                        }
                        else
                        {
                            this.blnCanReply = false;
                        }
                        goto Label_0221;

                    case 2:
                        if ((this.intUserID <= -1) || (this.intWealth < 0))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=4004");
                            return;
                        }
                        if (!BoardItem.CanView(this.intUserID, this.strBoardID))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=4004");
                            return;
                        }
                        goto Label_0221;

                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=4003");
                        return;
                }
            }
        Label_0221:
            if (!this.blnCanReply)
            {
                base.Response.Redirect("Report.aspx?Parameter=57");
            }
            else
            {
                DataRow topicRowByID = ROOTTopicManager.GetTopicRowByID(this.intTopicID);
                string str = topicRowByID["Title"].ToString();
                topicRowByID["Logo"].ToString().Trim();
                string str2 = "Re:" + str;
                this.tbTitle.Enabled = false;
                this.tbTitle.Text = str2;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageInfo();
            this.SetPageIntro();
        }

        private void SetPageInfo()
        {
            this.strPageInfo = BoardItem.GetPageInfo("回复帖子", this.strBoardID);
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


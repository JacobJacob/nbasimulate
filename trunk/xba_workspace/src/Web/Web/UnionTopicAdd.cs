namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class UnionTopicAdd : Page
    {
        protected ImageButton btnSubmit;
        protected TextBox ebContent;
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
                string validWords = StringItem.GetValidWords(StringItem.SetValidWord(this.tbTitle.Text));
                if (!StringItem.IsValidName(validWords, 2, 100))
                {
                    this.strMsg = "<p align=center><font color='#FF0000'>标题内容必须合法且大于2个字符小于100个字符！</font></p>";
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
                        string str4;
                        try
                        {
                            string strSendIP = base.Request.ServerVariables["REMOTE_ADDR"].ToString().Trim();
                            BTPUnionBBSManager.AddUnionTopic(this.intUnionID, this.intUserID, this.strNickName, str, validWords, strIn, strSendIP);
                            str4 = "Report.aspx?Parameter=4005!UnionID." + this.intUnionID + "^Page.1";
                        }
                        catch
                        {
                            str4 = "Report.aspx?Parameter=3";
                        }
                        base.Response.Redirect(str4);
                    }
                }
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
                    this.btnSubmit.ImageUrl = SessionItem.GetImageURL() + "button_22.gif";
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


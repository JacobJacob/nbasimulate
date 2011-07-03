namespace Web
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class SendFMatchMsg : Page
    {
        protected ImageButton btnSendMsg;
        private int intUserID;
        private string strClubName;
        public string strErrList;
        private string strNickName;
        protected TextBox tbQContent;

        private void btnSendMsg_Click(object sender, ImageClickEventArgs e)
        {
            string validWords = StringItem.GetValidWords(this.tbQContent.Text.ToString().Trim());
            if (StringItem.IsValidName(validWords, 5, 200))
            {
                if ((StringItem.GetStrLength(validWords) > 200) || (StringItem.GetStrLength(validWords) < 5))
                {
                    this.strErrList = "<font color='#FF0000'>留言内容必须在5-200个字符之间！</font>";
                }
                else
                {
                    DataRow row = BTPFriMatchMsgManager.GetFriMatchMsgRowTop1ByUserID(this.intUserID);
                    string str2 = "";
                    if (row != null)
                    {
                        str2 = row["Content"].ToString().Trim();
                    }
                    string str3 = validWords;
                    string str4 = str2;
                    if (str2.Length < 10)
                    {
                        str4 = str2.Substring(0, str2.Length);
                    }
                    else
                    {
                        str4 = str2.Substring(0, 10);
                    }
                    if (validWords.Length < 10)
                    {
                        str3 = validWords.Substring(0, validWords.Length);
                    }
                    else
                    {
                        str3 = validWords.Substring(0, 10);
                    }
                    if (str4 == str3)
                    {
                        this.strErrList = "<font color='#FF0000'>请不要重复发言！</font>";
                    }
                    else
                    {
                        DateTime time;
                        try
                        {
                            time = Convert.ToDateTime(base.Request.Cookies["SendTime"].Value);
                        }
                        catch
                        {
                            time = DateTime.Now.AddYears(-10);
                        }
                        if (time.AddSeconds(30.0) > DateTime.Now)
                        {
                            this.strErrList = "<font color='#FF0000'>您的发言速度过快，请等待30秒后再发言。</font>";
                        }
                        else
                        {
                            BTPFriMatchMsgManager.AddFriMatchMsg(this.intUserID, this.strNickName, this.strClubName, validWords);
                            HttpCookie cookie = new HttpCookie("SendTime");
                            cookie.Value = DateTime.Now.ToString();
                            base.Response.Cookies.Add(cookie);
                            base.Response.Write("<script language='javascript'>window.parent.location.reload(true);</script>");
                        }
                    }
                }
            }
            else
            {
                this.strErrList = "<font color='#FF0000'>留言内容中含有非法字符或者不在5-200个字符之间！</font>";
            }
        }

        private void InitializeComponent()
        {
            this.btnSendMsg.Click += new ImageClickEventHandler(this.btnSendMsg_Click);
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
                this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
                this.btnSendMsg.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


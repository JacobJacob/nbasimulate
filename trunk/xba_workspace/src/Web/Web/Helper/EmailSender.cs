namespace Web.Helper
{
    using jmail;
    using System;

    public class EmailSender
    {
        private string strContent;
        private string strEmail;
        private string strNickName;
        private string strSender = "postmaster@xba.com.cn";
        private string strSendServiceName = "mail.1xba.com.cn";
        private string strSendServicePW = "20031118";
        private string strSmtp;
        private string strTitle;

        public bool Send()
        {
            MessageClass class2 = new MessageClass();
            class2.Silent = true;
            class2.Logging = true;
            class2.Charset = "GB2312";
            class2.ContentType = "text/html";
            class2.AddRecipient(this.strEmail, "", "");
            class2.From = this.strSendServiceName;
            class2.FromName = "XBA管理团队";
            class2.MailServerUserName = this.strSender;
            class2.MailServerPassWord = this.strSendServicePW;
            class2.Subject = this.strTitle;
            class2.HTMLBody = this.strContent;
            try
            {
                class2.Send(this.strSmtp, false);
                class2.Close();
                class2 = null;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetEmail(string strNickName, string strTitle, string strContent, string strReceiveEmail)
        {
            this.strNickName = strNickName;
            this.strEmail = strReceiveEmail;
            this.strTitle = strTitle;
            this.strContent = strContent.Replace("+NickName+", this.strNickName);
        }

        public void SetService(string strSmtp, string strSendServiceName, string strSender, string PassWord)
        {
            this.strSmtp = strSmtp;
            this.strSendServiceName = strSendServiceName;
            this.strSendServicePW = PassWord;
            this.strSender = strSender;
        }
    }
}


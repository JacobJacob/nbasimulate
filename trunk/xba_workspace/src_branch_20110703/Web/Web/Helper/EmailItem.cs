namespace Web.Helper
{
    using System;
    using System.Web.Mail;

    public class EmailItem
    {
        private string body;
        private string from;
        private string subject;
        private string to;

        public EmailItem()
        {
        }

        public EmailItem(string to, string from, string subject, string body)
        {
            this.to = to;
            if (from == string.Empty)
            {
                this.from = "\"XBA体育\"<messager@email.xba.com.cn>";
            }
            else
            {
                this.from = from;
            }
            this.subject = subject;
            this.body = body;
        }

        public string Send()
        {
            try
            {
                MailMessage message = new MailMessage();
                message.Headers.Add("X-CS-ThreadId", AppDomain.GetCurrentThreadId().ToString());
                message.Headers.Add("X-CS-AppDomain", AppDomain.CurrentDomain.FriendlyName);
                message.To = this.to;
                message.From = this.from;
                message.Subject = this.subject;
                message.Body = this.body;
                message.BodyFormat = MailFormat.Html;
                message.Priority = MailPriority.High;
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "messager");
                message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "111111");
                SmtpMail.SmtpServer = "mail.xba.com.cn";
                SmtpMail.Send(message);
                return "SENDOK";
            }
            catch (Exception exception)
            {
                return exception.ToString();
            }
        }
    }
}


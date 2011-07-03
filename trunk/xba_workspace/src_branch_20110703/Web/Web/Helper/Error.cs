namespace Web.Helper
{
    using System;

    public class Error
    {
        public string strErrorMessage;
        public string strTarget;
        public string strURL;

        public Error(string strErrorMessage, string strURL, string strTarget)
        {
            this.strErrorMessage = strErrorMessage;
            this.strURL = strURL;
            this.strTarget = strTarget;
        }
    }
}


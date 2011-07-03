namespace Web
{
    using System;
    using System.Web.UI;
    using Web.Helper;

    public class Report : Page
    {
        public string strErrMsg;
        public string strTarget;
        public string strURL;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string str = base.Request.QueryString["Parameter"];
            switch (str)
            {
                case null:
                case "":
                    str = "1";
                    return;
            }
            string str2 = str;
            string str3 = "";
            if (str.IndexOf("!") >= 0)
            {
                string[] strArray = str.Split(new char[] { '!' });
                str2 = strArray[0];
                str3 = strArray[1];
                str3 = "?" + str3.Replace("^", "&").Replace(".", "=");
            }
            Error error = (Error) Global.htError[str2];
            this.strErrMsg = error.strErrorMessage;
            this.strURL = error.strURL + str3;
            this.strTarget = error.strTarget;
        }
    }
}


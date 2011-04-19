namespace Web
{
    using AjaxPro;
    using ServerManage;
    using System;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class _51Reg : Page
    {
        public string strIndexEnd;
        public string strIndexHead;
        public string strScriptURL;

        public string CEmail(string strEmail)
        {
            if (!StringItem.IsValidEmail(strEmail))
            {
                return "1";
            }
            if (ROOTUserManager.HasEmail40(strEmail))
            {
                return "2";
            }
            return "0";
        }

        [AjaxMethod]
        public string CheckEmail(string strEmail)
        {
            return this.CEmail(strEmail);
        }

        [AjaxMethod]
        public string CheckNickName(string strNickName)
        {
            return this.CNickName(strNickName);
        }

        [AjaxMethod]
        public string CheckPassword(string strPassword)
        {
            return this.CPassword(strPassword);
        }

        [AjaxMethod]
        public string CheckUserName(string strUserName)
        {
            return this.CUserName(strUserName);
        }

        public string CNickName(string strNickName)
        {
            int strLength = StringItem.GetStrLength(strNickName);
            bool flag = ROOTUserManager.HasNickName40(strNickName);
            if (strLength < 2)
            {
                return "1";
            }
            if (strLength > 0x10)
            {
                return "2";
            }
            strNickName = StringItem.GetValidWords(strNickName);
            if (!StringItem.IsValidName(strNickName, 2, 0x10))
            {
                return "3";
            }
            if (flag)
            {
                return "4";
            }
            return "0";
        }

        public string CPassword(string strPassword)
        {
            int strLength = StringItem.GetStrLength(strPassword);
            if (strLength < 4)
            {
                return "1";
            }
            if (strLength > 0x10)
            {
                return "2";
            }
            if (strPassword.IndexOf("'") != -1)
            {
                return "3";
            }
            return "0";
        }

        public string CUserName(string strUserName)
        {
            int strLength = StringItem.GetStrLength(strUserName);
            bool flag = ROOTUserManager.HasUserName40(strUserName);
            if (strLength < 4)
            {
                return "1";
            }
            if (strLength > 0x10)
            {
                return "2";
            }
            if (!StringItem.IsValidLogin(strUserName))
            {
                return "3";
            }
            if (flag)
            {
                return "4";
            }
            return "0";
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strIndexHead = BoardItem.GetIndexHead(-1, "", "");
            this.strIndexEnd = BoardItem.GetIndexEnd();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (ServerParameter.blnUseServer)
            {
                this.strScriptURL = "";
            }
            else
            {
                this.strScriptURL = "Web/";
            }
        }
    }
}


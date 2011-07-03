namespace Web
{
    using System;
    using System.Data;
    using System.IO;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class CreateNetDisk : Page
    {
        private string strPassword;
        private string strUserName;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strUserName = (string) SessionItem.GetRequest("ue3tu23ce3vdmbv", 1);
            this.strPassword = (string) SessionItem.GetRequest("stey86yi2jfdace", 1);
            this.strUserName = StringItem.MD5Decrypt(this.strUserName, Global.strMD5Key);
            this.strPassword = StringItem.MD5Decrypt(this.strPassword, Global.strMD5Key);
            DataRow userRowByUserNamePWD = ROOTUserManager.GetUserRowByUserNamePWD(this.strUserName, this.strPassword);
            int num1 = (int) userRowByUserNamePWD["UserID"];
            string path = userRowByUserNamePWD["DiskURL"].ToString();
            byte num2 = (byte) userRowByUserNamePWD["Category"];
            bool flag = (bool) userRowByUserNamePWD["Sex"];
            string str2 = "Boy";
            if (flag)
            {
                str2 = "Girl";
            }
            string strFace = "0|0|0|0|0|0|0|0|0";
            path = base.Server.MapPath(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FaceItem.CreateFace(base.Server.MapPath("Images/Face/" + str2 + "/"), path, strFace);
            SessionItem.SetMainLogin(userRowByUserNamePWD, false);
            base.Response.Redirect("");
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


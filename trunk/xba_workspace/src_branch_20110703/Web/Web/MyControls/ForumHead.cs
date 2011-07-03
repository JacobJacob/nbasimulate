namespace Web.MyControls
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Web.UI;

    public class ForumHead : UserControl
    {
        public int intUserID;
        public string strNickName;
        public string strPassword;
        public string strUserName;

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
        }

        protected override void Render(HtmlTextWriter output)
        {
            string str;
            if (this.intUserID < 0)
            {
                str = "<a href=''>首页</a> | <a href='Forum.aspx'>论坛</a> | <a href='" + DBLogin.URLString(0) + "Register2.aspx'>注册通行证</a> | <a href=''>登录</a> | <a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\">经理手册</a> | <a style='cursor:hand;' onclick=NewFAQWin('01');>常见问题</a> | <a href='Search.aspx?Type=0&Page=0'>搜索</a>";
            }
            else
            {
                string str2;
                if (ServerParameter.intGameCategory == 0)
                {
                    str2 = "<a href='" + DBLogin.URLString(0) + "MemberCenter.aspx'>通行证管理</a>";
                }
                else
                {
                    str2 = "<a href='Main.aspx'>返回游戏</a>";
                }
                str = "<a href=''>首页</a> | <a href='Forum.aspx'>论坛</a> | <font class='ForumTime'>" + this.strNickName + "</font> 您好！ | " + str2 + " | <a href='AlterInfo.aspx'>设置</a> | <a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\">经理手册</a> | <a style='cursor:hand;' onclick=NewFAQWin('01');>常见问题</a> | <a href='Search.aspx?Type=0&Page=0'>搜索</a> | <a href='Logout.aspx'>退出</a>";
            }
            output.Write(str);
        }
    }
}


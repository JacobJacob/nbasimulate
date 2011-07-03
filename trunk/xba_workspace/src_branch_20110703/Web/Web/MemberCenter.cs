namespace Web
{
    using System;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class MemberCenter : Page
    {
        public int intUserID;
        public string strCoin;
        public string strFreeCoin;
        public string strGameList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strNickName;
        public string strSmartPath;
        private string strType;
        protected HtmlTable tblGameServer;
        protected HtmlTableCell tdFreeCoinButton;

        public void GetServerVariables()
        {
            string str = base.Request.ServerVariables["HTTP_USER_AGENT"];
            int index = str.IndexOf("Alexa Toolbar");
            base.Response.Write(str + "|" + index);
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Register2.aspx");
            }
            else
            {
                string str3;
                this.strIndexHead = BoardItem.GetIndexHead(-1, "", "");
                this.tdFreeCoinButton.Visible = false;
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                int num = (int) userInfoByID["Coin"];
                this.strCoin = Convert.ToString(num).Trim();
                string strUserName = userInfoByID["UserName"].ToString().Trim();
                string strPassword = userInfoByID["Password"].ToString().Trim();
                int num2 = (int) userInfoByID["FreeCoin"];
                this.strIndexHead = BoardItem.GetIndexHead(this.intUserID, strUserName, strPassword);
                this.strIndexEnd = BoardItem.GetIndexEnd();
                int index = base.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("Alexa");
                if ((RandomItem.rnd.Next(0, 100) < 0) && (DateTime.Now < DateTime.Today.AddHours(22.0)))
                {
                    this.strFreeCoin = "恭喜您将有机会获得免费5枚金币，<a href='http://www.xba.com.cn/advertisement/index.htm' target='_blank'><font color='#FFE941'>点击此处</font></a>领取金币。";
                }
                else if (index < 0)
                {
                    this.strFreeCoin = "官方建议安装<a href='http://client.alexa.com/install/AlexaInstaller_xbabasketbman-20.exe' target='_blank'><font color='#FFE941'>XBA专用Alexa工具条</font></a>，安装工具条后每天登录都将有机会获得免费金币。";
                }
                else if (num < 10)
                {
                    int num5 = 10;
                    if (base.Request.Cookies["FromFromR"] != null)
                    {
                        num5 = Convert.ToInt32(base.Request.Cookies["FromFromR"].Value);
                    }
                    else
                    {
                        num5 = RandomItem.rnd.Next(0, Convert.ToInt32(Math.Pow(10.0, (double) (num2 + 1))));
                        HttpCookie cookie = new HttpCookie("FromFromR");
                        cookie.Value = num5.ToString();
                        cookie.Expires = DateTime.Now.AddHours(24.0);
                        base.Response.Cookies.Add(cookie);
                    }
                    if (num5 < 1)
                    {
                        this.tdFreeCoinButton.Visible = true;
                        this.strFreeCoin = "恭喜您获得1枚金币的奖励，请点击按钮以获得奖励金币。";
                    }
                    else
                    {
                        this.strFreeCoin = "您已经安装XBA专用Alexa工具条，您在每天登录时都将有机会获得免费金币！";
                    }
                }
                else
                {
                    this.strFreeCoin = "您已经安装XBA专用Alexa工具条，您在每天登录时都将有机会获得免费金币！";
                }
                this.strSmartPath = "<a href=\"" + ServerItem.ToOtherServerURL(1, strUserName, strPassword, "URL=SmartMain.aspx") + "\"><font color='#FFFFFF'>北方大陆1</font></a> <a href=\"" + ServerItem.ToOtherServerURL(2, strUserName, strPassword, "URL=SmartMain.aspx") + "\"><font color='#FFFFFF'>南方大陆1</font></a> <a href=\"" + ServerItem.ToOtherServerURL(3, strUserName, strPassword, "URL=SmartMain.aspx") + "\"><font color='#FFFFFF'>南方大陆2</font></a>";
                this.tblGameServer.Visible = false;
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                if (((str3 = this.strType) != null) && (string.IsInterned(str3) == "Server"))
                {
                    this.tblGameServer.Visible = true;
                }
                else
                {
                    this.tblGameServer.Visible = true;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


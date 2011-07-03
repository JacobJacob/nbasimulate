namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieWelcome : Page
    {
        protected ImageButton btnNext;
        private int intClubID3;
        private int intUserID;
        public string strSay = "在开始您的职业生涯前，请您先完成几个简单的步骤。";

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 0);
            if (intRookieOpIndex != 0)
            {
                base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
            }
            else
            {
                base.Response.Write("<script>window.top.Main.location=\"RookieMain_I.aspx?Type=REGCLUB\";</script>");
            }
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
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
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                int intRookieOpIndex = 0;
                try
                {
                    intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                }
                catch
                {
                    intRookieOpIndex = 0;
                }
                if (intRookieOpIndex != 0)
                {
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                DataRow gameRow = BTPGameManager.GetGameRow();
                bool flag = false;
                if (gameRow != null)
                {
                    flag = (bool) gameRow["CantReg"];
                }
                if (BTPDevManager.GetDevBlank() <= 0)
                {
                    if (ServerParameter.strCopartner == "CGA")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.cga.com.cn/webzone.aspx\";'>返回服务器选择</a>。";
                    }
                    else if (ServerParameter.strCopartner == "ZHW")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://game.china.com/XBA/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "17173")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.web.17173.com/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "DUNIU")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.mmoabc.com/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "51WAN")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://www.51wan.com/xba/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "DW")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.duowan.com/\";'>返回游戏主站</a>。";
                    }
                    else
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://www.xba.com.cn/XBALogin.aspx\";'>返回服务器选择</a>。";
                    }
                    this.btnNext.Visible = false;
                }
                else if (flag)
                {
                    if (ServerParameter.strCopartner == "CGA")
                    {
                        this.strSay = "对不起，当前服务器已满，请您访问其他服务器，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.cga.com.cn/webzone.aspx\";'>返回服务器选择</a>。";
                    }
                    else if (ServerParameter.strCopartner == "ZHW")
                    {
                        this.strSay = "对不起，当前服务器已满，请您访问其他服务器，<a href=\"#\" onclick='javascript:window.top.location=\"http://game.china.com/XBA/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "17173")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.web.17173.com/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "DUNIU")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.mmoabc.com/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "51WAN")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://www.51wan.com/xba/\";'>返回游戏主站</a>。";
                    }
                    else if (ServerParameter.strCopartner == "DW")
                    {
                        this.strSay = "对不起，当前联赛已满，暂时没有空位，请等待下赛季，或访问其他游戏区，<a href=\"#\" onclick='javascript:window.top.location=\"http://xba.duowan.com/\";'>返回游戏主站</a>。";
                    }
                    else
                    {
                        this.strSay = "对不起，当前服务器已满，请您访问其他服务器，<a href=\"#\" onclick='javascript:window.top.location=\"http://www.xba.com.cn/XBALogin.aspx\";'>返回服务器选择</a>。";
                    }
                    this.btnNext.Visible = false;
                }
                else
                {
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


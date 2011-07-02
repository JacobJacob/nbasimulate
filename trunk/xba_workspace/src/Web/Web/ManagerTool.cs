namespace Web
{
    using AjaxPro;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ManagerTool : Page
    {
        protected ImageButton btGetWealth;
        protected ImageButton btnOK;
        protected ImageButton btSaveWealth;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public long lngCoin;
        protected HtmlGenericControl MyCoinTitle;
        public StringBuilder sbCoin = new StringBuilder("");
        public StringBuilder sbWealthFinance = new StringBuilder("");
        public StringBuilder sbWealthFinancePage = new StringBuilder("");
        public string strBoxCount;
        public string strCoin;
        public string strList;
        public string strMsg = "";
        private string strNickName;
        public string strPage = "";
        public string strPageIntro;
        public string strPlayerWealth;
        public string strSayScript;
        public string strScript;
        private string strType;
        public string strUseWealth;
        public string strWealthToMoney = "";
        protected HtmlTable Table2;
        protected TextBox tbCoin;
        protected TextBox tbGetWealth;
        protected HtmlTable tblBox;
        protected HtmlTable tblCoinFinance;
        protected HtmlTable tblContainer;
        protected HtmlGenericControl tblMsg;
        protected HtmlTable tblMyCoin;
        protected HtmlTable tblSendCoin;
        protected HtmlTable tblWealth;
        protected TextBox tbNickname;
        protected TextBox tbnPW;
        protected TextBox tbnPWA;
        protected TextBox tbSaveWealth;
        protected HtmlTableRow trPw;
        protected HtmlTableRow trPwa;

        private void btGetWealth_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.tbGetWealth.Text.ToString();
            if (StringItem.IsNumber(str))
            {
                int getWealth = Convert.ToInt32(str);
                if (getWealth > 0)
                {
                    if (ROOTUserManager.SetWealthByUserID40(this.intUserID, 1, getWealth, "从论坛中提取" + getWealth + "财富到游戏里") != 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=485");
                        return;
                    }
                    int num = BTPAccountManager.SetWealthByUserID(this.intUserID, 1, getWealth);
                    BTPWealthFinanceManager.SetWealthFinance(this.intUserID, this.strNickName, 1, getWealth, "从论坛中提取" + getWealth + "财富到游戏里", "无内容");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=484");
                    return;
                }
                base.Response.Redirect("ManagerTool.aspx?Type=WEALTHMANAGE&Page=1");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=487");
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            DataRow row = ROOTUserManager.GetUserRow40(this.intUserID);
            string str = row["Password"].ToString().Trim();
            string str2 = row["Nickname"].ToString().Trim();
            string strCopartner = ServerParameter.strCopartner;
            DateTime time1 = (DateTime) row["CreateTime"];
            this.lngCoin = (long) row["Coin"];
            string str5 = this.tbnPW.Text.Trim();
            string str6 = this.tbnPWA.Text.Trim();
            string strIn = this.tbNickname.Text.Trim();
            if (StringItem.IsNumber(this.tbCoin.Text))
            {
                num = Convert.ToInt32(this.tbCoin.Text);
                if (num < 1)
                {
                    this.tbCoin.Text = "";
                    base.Response.Redirect("Report.aspx?Parameter=909");
                    return;
                }
                if ((this.lngCoin - num) < 20L)
                {
                    this.tbCoin.Text = "";
                    base.Response.Redirect("Report.aspx?Parameter=909b");
                    return;
                }
            }
            else
            {
                this.tbCoin.Text = "";
                base.Response.Redirect("Report.aspx?Parameter=816");
                return;
            }
            if ((strCopartner != "XBA") || str5.Equals(str6))
            {
                if ((strCopartner == "XBA") && !str5.Equals(str))
                {
                    this.tbnPW.Text = "";
                    this.tbnPWA.Text = "";
                    base.Response.Redirect("Report.aspx?Parameter=911");
                }
                else if (StringItem.IsValidName(strIn, 1, 20))
                {
                    int userIDByNickname = ROOTUserManager.GetUserIDByNickname(strIn);
                    if (userIDByNickname > 0)
                    {
                        ROOTUserManager.CoinFinance(userIDByNickname, num, string.Concat(new object[] { str2, "赠送给您", num, "枚金币" }), "", 1);
                        ROOTUserManager.CoinFinance(this.intUserID, num, string.Concat(new object[] { "您赠送给", strIn, "经理", num, "枚金币" }), "", 2);
                        base.Response.Redirect("Report.aspx?Parameter=910");
                    }
                    else
                    {
                        this.tbNickname.Text = "";
                        base.Response.Redirect("Report.aspx?Parameter=539");
                    }
                }
            }
            else
            {
                this.tbnPW.Text = "";
                this.tbnPWA.Text = "";
                base.Response.Redirect("Report.aspx?Parameter=911");
            }
        }

        private void CoinFinance()
        {
            switch (((int) SessionItem.GetRequest("Status", 0)))
            {
                case 1:
                {
                    this.tblMyCoin.Visible = true;
                    DataRow row = ROOTUserManager.GetUserRow40(this.intUserID);
                    this.lngCoin = (long) row["Coin"];
                    DateTime time1 = (DateTime) row["CreateTime"];
                    switch (ServerParameter.strCopartner)
                    {
                        case "CGA":
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            this.MyCoinTitle.InnerHtml = "1、XBA金币可以使用浩方点券进行兑换，它用于购买《XBA篮球经理》游戏里出售的道具，成功购买道具将消耗相应的金币。<a target=\"_blank\" href=\"http://vip.cga.com.cn/game/XBA/\">点击兑换</a>。<br>2、充值成功后，您可以在此页面查看到您的金币总数。<br>3、如果您遇到浩方点券充值问题请拨打客服电话：021-50504723,如果您在点券兑换金币时遇到问题请拨打客服电话：0532-86667257";
                            return;

                        case "ZHW":
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            this.MyCoinTitle.InnerHtml = "1、XBA金币:XBA篮球经理游戏的一种“特殊积分”，它用于购买《XBA篮球经理》游戏里出售的道具，成功购买道具将消耗相应的金币。<br>2、在兑换时请先确认您的中华网支付中心(pay.china.com)账户内有足够的龙币，如果没有足够的龙币进行兑换，请先充值中华网龙币(<a target=\"_blank\" href=\"https://pay.china.com/WebPerson/html/help/ChongZhi.html\">帮助</a>)。兑换XBA金币将扣除账户内相应的龙币数。<br>3、兑换成功后，您可以登录游戏-道具商店-金币财政页面查看自己的XBA金币数。<br>4、如果您在兑换过程中遇到什么问题可以咨询我们的客服QQ:15908920 <br>";
                            return;

                        case "51WAN":
                            this.MyCoinTitle.InnerHtml = "1、玩家可以使用51wan“新娱币”以1：2的比例兑换XBA金币，使用金币可以购买游戏里出售的道具，成功购买道具将消耗相应的金币。<br>2、进入51wan“充值中心”选择《XBA大灌篮》游戏——立即充值，您可以使用各种充值渠道直接充值XBA金币。<a target=\"_blank\" href=\"http://pay.51wan.com\">点击进入</a><br>3、充值成功后，您可以在此页面查看到您的金币总数。4、如果您在兑换过程中遇到什么问题可以咨询我们的客服信箱：<a target=\"_blank\" href=\"xbakefu@51wan.com\">xbakefu@51wan.com</a><br>";
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            return;

                        case "17173":
                            this.MyCoinTitle.InnerHtml = "1、玩家可以使用17173狐币以1：2的比例兑换XBA金币，使用金币可以购买游戏里出售的道具，成功购买道具将消耗相应的金币。<a target=\"_blank\" href=\"http://web.17173.com/xba/bangzhu/chongzhi.html\">点击兑换金币</a><br>2、在兑换前请先确认您的17173账户中有足够的狐币，如果数量不够，请先充值狐币。<a target=\"_blank\" href=\"http://up.sohu.com/17173giftpackage/index1.html\">点击充值狐币</a><br>3、如果您遇到狐币充值问题请拨打客服电话：0591-80807120,如果您在狐币兑换金币中遇到问题请拨打客服电话：0532-86667257。<br>4、如果您在充值狐币的过程中遇到问题，请联系<a href=\"http://up.sohu.com/help.up\" target=\"_blank\">17173客服</a>，如果您在兑换金币的过程中遇到问题请联系我们游戏客服QQ：15908920<br>";
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            return;

                        case "DUNIU":
                            this.MyCoinTitle.InnerHtml = "XBA提供多种金币充值渠道，采用即时到帐服务，您可以选择自己方便的方式进行充值。<a target=\"_blank\" href=\"http://cn.mmoabc.com/buy/\">进入金币充值页</a><br><a target=\"_blank\" href=\"http://bbs.mmoabc.com/forums/show/19\">充值问题您可以进入客服论坛</a><<br>";
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            return;

                        case "DW":
                            this.MyCoinTitle.InnerHtml = "进入金币充值页：http://xba.duowan.com/0803/m_70641288537.html<br>充值问题您可以进入客服论坛进行咨询：http://bbs.duowan.com/forum-585-1.html<a target=\"_blank\" href=\"http://cn.mmoabc.com/buy/\">进入金币充值页</a><br><a target=\"_blank\" href=\"http://bbs.mmoabc.com/forums/show/19\">充值问题您可以进入客服论坛</a><<br>";
                            this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                            return;
                    }
                    this.MyCoinTitle.InnerHtml = "XBA提供多种金币充值渠道，采用即时到帐服务，您可以选择自己方便的方式进行充值<br><a target=\"_blank\" href=\"http://www.xba.com.cn/PayCBShow.aspx\">使用银行卡充值方法</a><br><a target=\"_blank\" href=\"http://www.xba.com.cn/PayJCardSend.aspx\">使用骏网一卡通充值方法</a><br><a target=\"_blank\" href=\"http://www.xba.com.cn/PaySZXShow.aspx\">使用神州行充值方法</a><br><a target=\"_blank\" href=\"http://www.xba.com.cn/PayAliShow.aspx\">使用支付宝充值方法</a><br><a target=\"_blank\" href=\"http://shop35235002.taobao.com/\">使用官方认证的淘宝店铺充值</a><br>充值问题您可以直接咨询客服QQ：15908920";
                    this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                    return;
                }
                case 2:
                {
                    this.tblCoinFinance.Visible = true;
                    this.intPage = (int) SessionItem.GetRequest("Page", 0);
                    if (this.intPage == 0)
                    {
                        this.intPage = 1;
                    }
                    DateTime datIn = (DateTime) ROOTUserManager.GetUserRow40(this.intUserID)["CreateTime"];
                    string strCopartner = ServerParameter.strCopartner;
                    if (strCopartner == "CGA")
                    {
                        this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                    }
                    else if (strCopartner == "ZHW")
                    {
                        this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                        this.MyCoinTitle.InnerHtml = "金币充值暂未开通。";
                    }
                    else
                    {
                        this.strCoin = "<a href=\"ManagerTool.aspx?Type=COIN&Status=3\">金币转赠</a>";
                    }
                    DataTable reader = ROOTUserManager.GetCoinInFinanceList(this.intUserID, this.intPage, 12);
                    if (reader != null)
                    {
                        foreach (DataRow row in reader.Rows)
                        {
                            byte num1 = (byte) row["Category"];
                            long num2 = (long) row["Income"];
                            string str3 = row["Event"].ToString().Trim();
                            datIn = (DateTime) row["CreateTime"];
                            this.sbCoin.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                            this.sbCoin.Append("    <td align=\"center\">收入</td>\n");
                            this.sbCoin.Append("    <td align=\"center\">" + num2 + "</td>\n");
                            this.sbCoin.Append("    <td align=\"center\">" + StringItem.FormatDate(datIn, "yy-MM-dd hh:mm") + "</td>\n");
                            this.sbCoin.Append("    <td align=\"left\" style=\"padding-left:6px\">" + str3 + "</td>\n");
                            this.sbCoin.Append("</tr>\n");
                        }
                        //reader.Close();
                        string strCurrentURL = "ManagerTool.aspx?Type=COIN&Status=2&";
                        this.strScript = this.GetScript(strCurrentURL);
                        this.strPage = "<div style=\"padding:6px 0 0 0;text-align:right;\">" + this.GetViewPage(strCurrentURL) + "</div>";
                        return;
                    }
                    this.sbCoin.Append("<tr><td align='center' colspan=4>暂无记录</td></tr>");
                    return;
                }
                case 3:
                    this.tblSendCoin.Visible = true;
                    if (ServerParameter.strCopartner != "XBA")
                    {
                        this.tbnPW.Visible = false;
                        this.tbnPWA.Visible = false;
                        this.trPw.Visible = false;
                        this.trPwa.Visible = false;
                    }
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    break;
            }
        }

        [AjaxMethod]
        public string GetBoxMsg()
        {
            DataTable announceTableByType = BTPAnnounceManager.GetAnnounceTableByType();
            string str = "";
            if ((announceTableByType == null) || (announceTableByType.Rows.Count < 1))
            {
                return "<li>暂时无人中奖</li>";
            }
            foreach (DataRow row in announceTableByType.Rows)
            {
                string str2 = StringItem.FormatDate((DateTime) row["CreateTime"], "MM-dd hh-mm");
                string str3 = row["Title"].ToString().Trim() + " [" + str2 + "]";
                str = str + "<li>" + str3 + "</li>";
            }
            return str;
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int coinInCount = 0;
            if (this.strType == "TOOLS")
            {
                return BTPToolLinkManager.GetToolLinkCount(this.intUserID);
            }
            if (this.strType == "STORE")
            {
                return BTPToolManager.GetToolCount();
            }
            if (this.strType == "WEALTHSTORE")
            {
                return BTPWealthToolManager.GetWealthToolCount();
            }
            if (this.strType == "WEALTHMANAGE")
            {
                return BTPWealthFinanceManager.GameServiceTotal(this.intUserID);
            }
            if (this.strType == "COIN")
            {
                coinInCount = ROOTUserManager.GetCoinInCount(this.intUserID);
            }
            return coinInCount;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", total, "个记录 跳转", str4 });
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                string str;
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                this.intPerPage = 10;
                this.tblContainer.Visible = false;
                this.tblWealth.Visible = false;
                this.tblBox.Visible = false;
                this.tblMyCoin.Visible = false;
                this.tblCoinFinance.Visible = false;
                this.tblSendCoin.Visible = false;
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.strPlayerWealth = accountRowByUserID["Wealth"].ToString();
                if ((DateTime.Now.Hour < 10) && (DateTime.Now.Hour >= 4))
                {
                    if (this.strType == "WEALTHMANAGE")
                    {
                        str = "<li class='qian2a'><font color='#aaaaaa'>幸运抽奖</font></li>";
                    }
                    else
                    {
                        str = "<li class='qian2a'><font color='#aaaaaa'>幸运抽奖</font></li>";
                    }
                }
                else if (this.strType == "WEALTHMANAGE")
                {
                    str = "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=USEBOX&Status=1'>幸运抽奖</a></li>";
                }
                else
                {
                    str = "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=USEBOX&Status=1'>幸运抽奖</a></li>";
                }
                switch (this.strType)
                {
                    case "TOOLS":
                        //this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=STORE&Page=1'>金币商店</a></li>" + str + "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=COIN&Status=1'>金币财政</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span style='margin-left:50px;' width='100' height='24'  align='center'>游戏币: " + this.strPlayerWealth + "</span>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.tblContainer.Visible = true;
                        this.SetToolList();
                        break;

                    case "STORE":
                        //this.strPageIntro = "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=TOOLS\"' href='ManagerTool.aspx?Type=TOOLS&Page=1'>我的道具</a></li><li class='qian2'>金币商店</li>" + str + "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=COIN&Status=1'>金币财政</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span width=\"89\">&nbsp;</span><span style=\" padding-left:100px\"><a href='" + StringItem.GetBuyCoinURL() + "' target='_blank'><img  align='absmiddle' border=0 src='Images/chongzhi.gif'  height='21px' width='60' /></a></span>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.tblContainer.Visible = true;
                        this.strWealthToMoney = "<a href=\"Temp_Right.aspx?Type=EXCHANGE\" target=\"Right\">游戏币兑换资金</a>";
                        this.SetStoreList();
                        break;

                    case "WEALTHMANAGE":
                        //this.strPageIntro = "<a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=TOOLS\"' href='ManagerTool.aspx?Type=TOOLS&Page=1'><img align='absmiddle'  src='" + SessionItem.GetImageURL() + "MenuCard/Goods/Goods_C_01.GIF' border='0' height='24' width='76'></a><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=STORE&Page=1'><img align='absmiddle'  src='" + SessionItem.GetImageURL() + "MenuCard/Goods/Goods_C_02.GIF' border='0' height='24' width='75'></a><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_07.GIF' border='0' height='24' width='90'>" + str + "<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span width=\"10\">&nbsp;</span><span style='margin-left:12px;'>现有游戏币: " + this.strPlayerWealth + "</span>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.tblContainer.Visible = true;
                        this.tblWealth.Visible = true;
                        this.SetWealthManage();
                        this.strWealthToMoney = "<a href=\"Temp_Right.aspx?Type=EXCHANGE\" target=\"Right\">游戏币兑换资金</a>";
                        break;

                    case "USEBOX":
                        //this.strPageIntro = "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=TOOLS\"' href='ManagerTool.aspx?Type=TOOLS&Page=1'>我的道具</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=STORE&Page=1'>金币商店</a></li><li class='qian2'>幸运抽奖</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=COIN&Status=1'>金币财政</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.tblContainer.Visible = false;
                        this.tblWealth.Visible = false;
                        if ((DateTime.Now.Hour >= 10) || (DateTime.Now.Hour < 4))
                        {
                            this.tblBox.Visible = true;
                        }
                        this.strBoxCount = ToolItem.HasTool(this.intUserID, 12, 1).ToString().Trim();
                        break;

                    case "COIN":
                        //this.strPageIntro = "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=TOOLS\"' href='ManagerTool.aspx?Type=TOOLS&Page=1'>我的道具</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=STORE&Page=1'>金币商店</a></li>" + str + "<li class='qian2'>金币财政</li></ul><a href='ManagerTool.aspx?Type=WEALTHMANAGE&Page=1'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span style='margin-left:50px;' width='100' height='24'  align='center'>游戏币: " + this.strPlayerWealth + "</span>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.CoinFinance();
                        break;

                    default:
                        //this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=STORE&Page=1'>金币商店</a></li>" + str + "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Tools.aspx?Type=STORE\"' href='ManagerTool.aspx?Type=COIN&Status=1'>金币财政</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span style='margin-left:50px;' width='100' height='24'  align='center'>游戏币: " + this.strPlayerWealth + "</span>";
                        this.strPageIntro = "<ul><li class='qian1'>我的道具</a></li>";
                        this.tblContainer.Visible = true;
                        this.SetToolList();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(ManagerTool));
        }

        private void SetStoreList()
        {
            string strCurrentURL = "ManagerTool.aspx?Type=STORE&";
            this.intPerPage = 8;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable table = BTPToolManager.GetToolList(this.intPage, 100, 0x63, total);
            if (table == null)
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>暂时没有道具。</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    int num2 = (int) row["ToolID"];
                    int num3 = (byte) row["Category"];
                    int num4 = (int) row["CoinCost"];
                    int num1 = (int) row["AmountInStock"];
                    byte num5 = (byte) row["TicketCategory"];
                    string str3 = row["ToolImage"].ToString().Trim();
                    string str4 = row["ToolIntroduction"].ToString().Trim();
                    string str2 = row["ToolName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='40' width='60' style='padding-right:4px;' align='right'><img alt='", str4, "' src='", SessionItem.GetImageURL(), "Tools/", str3, "' height='30' width='30' border='0'></td><td width='163' align='left' style='padding-left:5px;'><font color='blue'><span title='", str4, "'>", str2, "</span></font></td><td width='70' align='left'></td><td width='95' align='left'><font color='blue'>", num4, "</font>枚金币</td>" });
                    if (((num3 == 14) && (DateTime.Now.Hour < 10)) && (DateTime.Now.Hour >= 4))
                    {
                        this.strList = this.strList + "<td width='127'></td>";
                    }
                    else
                    {
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { strList, "<td width='127'><a href='SecretaryPage.aspx?Type=BUYTOOL&ToolID=", num2, "'>购 买</a></td>" });
                    }
                    this.strList = this.strList + "<td >&nbsp;</td></tr>";
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
            this.strList = this.strList + "<tr><td height='25' style='padding-left:10px;' colspan='6'>" + this.strWealthToMoney + "</td></tr>";
        }

        private void SetToolList()
        {
            string strCurrentURL = "ManagerTool.aspx?Type=TOOLS&";
            this.intPerPage = 8;
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            string str5 = "";
            DataTable table = BTPToolLinkManager.GetToolLinkList(this.intUserID, this.intPage, 0x3e8, intCount, total);
            if (table == null)
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>暂时没有道具。</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in table.Rows)
                {
                    DataRow toolRowByID;
                    int intToolID = (int) row2["ToolID"];
                    int num1 = (int) row2["ToolLinkID"];
                    int num7 = (int) row2["UserID"];
                    int num4 = (int) row2["Amount"];
                    DateTime datIn = (DateTime) row2["ExpireTime"];
                    int num5 = (byte) row2["Type"];
                    if (num5 == 1)
                    {
                        toolRowByID = BTPToolManager.GetToolRowByID(intToolID);
                        int num8 = (int) toolRowByID["CoinCost"];
                        byte num6 = (byte) toolRowByID["Category"];
                        if (((num6 == 12) && (DateTime.Now.Hour < 10)) && (DateTime.Now.Hour >= 4))
                        {
                            continue;
                        }
                        if (num6 == 12)
                        {
                            str5 = "<a onclick='javascript:window.top.Main.Right.location=\"Intro/Box.aspx?Type=BOXSHOW\"' href='ManagerTool.aspx?Type=USEBOX'>使用</a>";
                        }
                        else
                        {
                            str5 = "";
                        }
                    }
                    else
                    {
                        toolRowByID = BTPWealthToolManager.GetWealthToolRowByID(intToolID);
                        int num9 = (int) toolRowByID["WealthCost"];
                        str5 = "";
                    }
                    byte num10 = (byte) toolRowByID["Category"];
                    int num11 = (int) toolRowByID["AmountInStock"];
                    byte num12 = (byte) toolRowByID["TicketCategory"];
                    string str3 = toolRowByID["ToolImage"].ToString().Trim();
                    string str4 = toolRowByID["ToolIntroduction"].ToString().Trim();
                    string str2 = toolRowByID["ToolName"].ToString().Trim();
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='40' width='60' style='padding-right:4px;' align='right'><img alt='", str4, "' src='", SessionItem.GetImageURL(), "Tools/", str3, "' height='30' width='30' border='0'></td><td width='163'  align='left' style='padding-left:5px;><span title='", str4, "'><font color='blue'>", str2, "</font></span></td><td width='70' align='left'>", str5, "<td width='95' align='left'><strong>数量</strong>：<font color='blue'>", num4, "</font></td><td width='127'>有效期：<font color='blue'>", 
                        StringItem.FormatDate(datIn, "yyyy-MM-dd"), "</font></td><td >&nbsp;</td></tr>"
                     });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
        }

        private void SetWealthManage()
        {
            this.strUseWealth = ROOTUserManager.GetWealthByUserID40(this.intUserID);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            DataTable reader = BTPWealthFinanceManager.GetWealthFinanceTableByUserID(0, this.intPage, 10, this.intUserID);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    int num = (int)row["Income"];
                    int num2 = (int)row["Outcome"];
                    DateTime datIn = (DateTime)row["CreateTime"];
                    string str = row["Event"].ToString().Trim();
                    row["Remark"].ToString().Trim();
                    this.sbWealthFinance.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbWealthFinance.Append("<td height=\"25\" align=\"center\"><font color='green'>" + num + "</font></td>");
                    this.sbWealthFinance.Append("<td align=\"center\"><font color='red'>" + num2 + "</font></td>");
                    this.sbWealthFinance.Append("<td align=\"center\"><font color='#333333'>" + StringItem.FormatDate(datIn, "MM-dd hh:mm") + "</font></td>");
                    this.sbWealthFinance.Append("<td style=\"padding:2px;\"><font color='#333333'>" + str + "</font></td>");
                    this.sbWealthFinance.Append("</tr>");
                    this.sbWealthFinance.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>");
                }
            }
            //reader.Close();
            string strCurrentURL = "ManagerTool.aspx?Type=WEALTHMANAGE&";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPerPage = 10;
            this.sbWealthFinancePage.Append(this.GetViewPage(strCurrentURL));
        }

        private void SetWealthStoreList()
        {
            string strCurrentURL = "ManagerTool.aspx?Type=WEALTHSTORE&";
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable wealthToolList = BTPWealthToolManager.GetWealthToolList(this.intPage, this.intPerPage);
            if (wealthToolList == null)
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='5'>暂时没有道具。</td></tr>";
            }
            else
            {
                this.strList = "<tr ><td colspan=5 height='25' align=right><font color=red>注：标红色为会员价格</font></td></tr>";
                this.strList = this.strList + "<tr><td colspan='5' height='5'></td></tr>";
                foreach (DataRow row in wealthToolList.Rows)
                {
                    int num = (int) row["ToolID"];
                    byte num1 = (byte) row["Category"];
                    int num2 = (int) row["WealthCost"];
                    int num3 = (int) row["AmountInStock"];
                    byte num5 = (byte) row["TicketCategory"];
                    string str3 = row["ToolImage"].ToString().Trim();
                    row["ToolIntroduction"].ToString().Trim();
                    string str2 = row["ToolName"].ToString().Trim();
                    int num4 = (int) row["PayCost"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='40' width='40' align='center'><img src='", SessionItem.GetImageURL(), "Tools/", str3, "' height='30' width='30' border='0'></td><td width='100' style='padding-left:4px'><font color='blue'>", str2, "</font></td><td width='100' align='center'><strong>数量</strong>：<font color='blue'>", num3, "</font></td><td width='206'><strong>价格</strong>：<font color='blue'>", num2, "</font>枚 / <font color='red'>", num4, "</font>枚</td><td width='100' align='center'><a href='SecretaryPage.aspx?Type=INTROWEALTHTOOL&ToolID=", num, "'>说明</a> | 购买</td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
            }
            //wealthToolList.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }
    }
}


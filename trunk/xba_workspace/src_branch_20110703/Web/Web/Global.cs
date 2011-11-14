namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Web;
    using Web.DBData;
    using Web.Helper;

    public class Global : HttpApplication
    {
        public static bool blnServerTag = true;
        private IContainer components = null;
        public static DataRow drParameter;
        public static Hashtable htError;
        public static int intOnlineLimit = 0x5dc;
        public static int intOnlineRefresh = 0x4e20;
        public static int intStartUpdate = 4;
        public static string strMainMD5Key = "x1~baK07";
        public static string strMD5Key = "sth31118";
        public static string strRoot = "D:";
        public static string strSessionName = "BestXBA";
        public static string strWebFoot = "<table width=\"100%\" height=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td align=\"center\"><font color='#FFFFFF'>XBA无道具篮球经理2011 <a style='cursor:hand;' onclick=\"javascript:NewEditionWin();\">v0403</a></font>&nbsp;&nbsp;&nbsp;&nbsp;<font color='#FFFFFF'>增值电信业务经营许可证 </font> <a href=\"http://www.miibeian.gov.cn\" target=\"_blank\"><font color='#FFFFFF'>粤ICP备09072237号-1</font></a><script src=\"http://s22.cnzz.com/stat.php?id=3254719&web_id=3254719\" language=\"JavaScript\"></script></td></tr></table>";
        public static string strWebKey = "<meta name=\"keywords\" content=\"街头篮球游戏，NBA，姚明，CBA，篮球论坛，赛事在线直播，校园篮球，basketball，online game，NBA live\">\n<meta name=\"description\" content=\"XBA篮球经理游戏是全球首款在线篮球经理游戏，仿真实NBA体育职业球队管理，含NBA、姚明新闻，街头篮球、篮球论坛、CBA、NBA live、球员转会选秀、火箭竞猜、赛事在线直播、灌篮高手、小游戏等游戏内容。\">";
        public static string strWebName = "无道具XBA篮球经理--让你体会无道具带来的乐趣";
        public static string strWebURL = "113388.net";

        public Global()
        {
            this.InitializeComponent();
            if (ServerParameter.intGameCategory > 0)
            {
                drParameter = BTPParameterManager.GetParameterRow(ServerParameter.intGameCategory);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string error_url = "/";
            if (base.Request != null)
            {
                error_url = base.Request.Url.ToString();
            }
            string error_msg = "error";
            if (base.Server != null)
            {
                error_msg = base.Server.GetLastError().InnerException.Message;
            }
            string message = "错误页面：" + error_url + " 错误信息：" + error_msg;
            EventLog.WriteEntry("MyWebError", message, EventLogEntryType.Error);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            this.CreateError();
            this.CreateAppRepTable();
            this.CreateAppParameterTable();
        }

        private void CreateAppParameterTable()
        {
            DataTable table = new DataTable("Parameter");
            DataColumn column = new DataColumn("ParameterID", typeof(int));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1L;
            column.AutoIncrementStep = 1L;
            table.Columns.Add(column);
            table.PrimaryKey = new DataColumn[] { column };
            table.Columns.Add(new DataColumn("LatestReadTime", typeof(DateTime)));
            table.BeginLoadData();
            table.LoadDataRow(new object[] { 1, DateTime.Now.AddMinutes(-1.0) }, true);
            table.EndLoadData();
            HttpContext current = HttpContext.Current;
            current.Application.Lock();
            current.Application.Add("ParameterTable", table);
            current.Application.UnLock();
        }

        private void CreateAppRepTable()
        {
            DataTable table = new DataTable("Report");
            DataColumn column = new DataColumn("RepID", typeof(int));
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1L;
            column.AutoIncrementStep = 1L;
            table.Columns.Add(column);
            table.PrimaryKey = new DataColumn[] { column };
            table.Columns.Add(new DataColumn("TagID", typeof(int)));
            table.Columns.Add(new DataColumn("RepDetail", typeof(VOnlineReportData)));
            HttpContext current = HttpContext.Current;
            current.Application.Lock();
            current.Application.Add("RepTable", table);
            current.Application.UnLock();
        }

        private void CreateError()
        {
            htError = new Hashtable();
            htError.Add("1", new Error("出现错误，您需要跳转至首页", "", "TOP"));
            htError.Add("1b", new Error("您的联盟没有发起过相关活动，您需要跳转至首页", "", "TOP"));
            htError.Add("1a", new Error("您还没有加入联盟不能进行此操作。", "", "TOP"));
            htError.Add("2", new Error("出现错误，您需要关闭此页", "NULL", "CLOSE"));
            htError.Add("3", new Error("出现错误，您需要返回上一页面", "NULL", "NULL"));
            htError.Add("3a", new Error("出现错误，您需要返回上一页面", "login.aspx", "TOP"));
            htError.Add("3newa", new Error("出现错误，您需要返回上一页面", "NULL", "NULL"));
            htError.Add("3newb", new Error("您最少要训练2名球员的某项属性！", "NULL", "NULL"));
            htError.Add("3ab", new Error("您填写的内容含有非法字符或不符合要求，请重新输入。", "NULL", "NULL"));
            htError.Add("4", new Error("请成为我们的会员以享受更多功能。", "NULL", "NULL"));
            htError.Add("5", new Error("暂时无法与主服务器进行通讯，请稍后进行此操作。", "NULL", "NULL"));
            htError.Add("1001", new Error("请在10:00之后进行此项管理，会员可在9:00观看在线直播。", "Main.aspx", "TOP"));
            htError.Add("1001b", new Error("比赛直播只在9:00至10:00进行，现在没有比赛直播。", "NULL", "CLOSE"));
            htError.Add("1001c", new Error("您只能观看本轮比赛直播。", "NULL", "CLOSE"));
            htError.Add("1001a", new Error("请在10:00之后进行此项管理，会员可在9:00观看在线直播。", "Main.aspx", "TOP"));
            htError.Add("1002", new Error("比赛正在进行中，请在10:00之后访问游戏，会员可在9:00观看在线直播。", "Main.aspx", "TOP"));
            htError.Add("1003", new Error("游戏服务器日常维护，请稍后访问。", "", "TOP"));
            htError.Add("1004", new Error("游戏暂时锁定，请稍后访问。", "", "TOP"));
            htError.Add("1005", new Error("正在观看战报的人数过多，请稍后观看。加入会员后，将取消此限制。", "NULL", "CLOSE"));
            htError.Add("1005a", new Error("正在观看战报的人数过多，请稍后观看。加入会员后，将取消此限制。", "NULL", "CLOSE"));
            htError.Add("1006", new Error("正在进行日常维护，请在10:00之后进行此项操作，会员可在9:00观看在线比赛直播。", "Main.aspx", "TOP"));
            htError.Add("1006d", new Error("请在10:00之后进行此项操作，会员可在9:00观看在线比赛直播。", "Main.aspx", "TOP"));
            htError.Add("1006a", new Error("赛季中休息一轮，暂时没有比赛，下轮比赛将于明日9点开始。", "Main.aspx", "TOP"));
            htError.Add("1006b", new Error("赛季末，暂时没有比赛，比赛将于下赛季开始。", "Main.aspx", "TOP"));
            htError.Add("1006c", new Error("赛季初，暂时没有比赛，下轮比赛将于明日9点开始。", "Main.aspx", "TOP"));
            htError.Add("1007", new Error("请勿同时登录两个帐号或同时登录两个赛区！请通过“退出”按钮退出登录的帐号，如果非法退出，请关闭所有浏览器并等待1分钟后重新登录，或点此设置<a href=\"SetCookieExpire.aspx\">登出</a>。", "NULL", "NULL"));
            htError.Add("10", new Error("您的用户名或密码错误，请确定您已经注册了XBA通行证并且在此大陆拥有球队！", "", "NULL"));
            htError.Add("10a", new Error("您的用户名或密码错误，请确定您已经注册了XBA通行证！", "", "NULL"));
            htError.Add("10112", new Error("登录人数已达上限，请注册成为会员，或者稍候再试！", "", "NULL"));
            htError.Add("10111", new Error("您一次只能登录一个账号，XBA不建议一人打理多个帐号，请使用第一次登录的帐号！", "", "NULL"));
            htError.Add("10113", new Error(DBLogin.GameNameChinese(ServerParameter.intGameCategory) + "暂时无法登录，请先访问论坛！", "Forum.aspx", "NULL"));
            htError.Add("10114", new Error("与主服务器暂时无法通讯，请稍后再试！", "NULL", "NULL"));
            htError.Add("10115", new Error("您暂时无法修改性别和头像！", "NULL", "NULL"));
            htError.Add("101151", new Error("您暂时无法修改此信息！", "NULL", "NULL"));
            htError.Add("10116", new Error("您已经在" + DBLogin.GameNameChinese(ServerParameter.intGameCategory) + "中申请过帐号，无法重复申请！", "NULL", "NULL"));
            htError.Add("10117", new Error("您还没有在" + DBLogin.GameNameChinese(ServerParameter.intGameCategory) + "注册！", "SelectGameJump.aspx?Kind=1&URL=SelectGame.aspx", "NULL"));
            htError.Add("10118", new Error("您还没有在" + DBLogin.GameNameChinese(ServerParameter.intGameCategory) + "组建球队，请点击左上角头像下的“组队”按钮组建一支属于您自己的街头篮球队！", "Login.aspx", "NULL"));
            htError.Add("10119", new Error("您在" + DBLogin.GameNameChinese(ServerParameter.intGameCategory) + "的球队中还没有球员，请点击左上角头像下的“招人”按钮，从列出的街头自由球员中选择4名球员加入您的球队！", "Login.aspx", "NULL"));
            htError.Add("10120", new Error("请在主站点击此按钮以进行操作！", "", "NULL"));
            htError.Add("10121", new Error("在线经理过多，您可以稍后再尝试登录！XBA会员将无此限制。", "Logout.aspx", "TOP"));
            htError.Add("11", new Error("您的用户注册出现错误，请重新登录确定是否注册成功！", "", "NULL"));
            htError.Add("12", new Error("您的登录信息丢失，出现此错误提示的原因可能是您很长时间没有活动、网络有暂时的中断或者系统局部更新，请您重新登录进行游戏。", "", "TOP"));
            htError.Add("12a", new Error("您必须登录才能使用此功能。", "NULL", "NULL"));
            htError.Add("13", new Error("您已经成功退出！", "", "NULL"));
            htError.Add("14", new Error("您可能已经注册成功，请重新登录！", "Logout.aspx", "TOP"));
            htError.Add("15", new Error("您初始化街球队球员产生错误，请重新选择球员！", "RookieAssignClub.aspx", "THIS"));
            htError.Add("15a", new Error("您初始化职业队球员产生错误，请重新选择球员！", "RookieAssignDevClub.aspx", "THIS"));
            htError.Add("151", new Error("您没有选择足够的球员，请重新选择球员！", "NULL", "NULL"));
            htError.Add("16", new Error("您已经成功的初始化球队，在前期，小秘书会通过短消息给您一些管理球队的意见。", "Main.aspx", "TOP"));
            htError.Add("17", new Error("您填写的身价错误，请使用半角的阿拉伯数字！", "NULL", "NULL"));
            htError.Add("18", new Error("您的用户等级不够，暂时不能使用此功能。", "NULL", "NULL"));
            htError.Add("19", new Error("你在此服务器的帐号已经被封停，请解封后再登录。", "NULL", "NULL"));
            htError.Add("19a", new Error("你的帐号已经被封停，请解封后再登录，或请直接登录游戏服务器管理游戏。", "NULL", "NULL"));
            htError.Add("19b", new Error("你的帐号已经被封停，无法发表话题。", "NULL", "NULL"));
            htError.Add("19c", new Error("您正处在沉迷时间不能登录游戏！", "", "TOP"));
            htError.Add("19d", new Error("您已进入沉迷时间，请休息5小时后再进入XBA篮球世界！", "", "TOP"));
            htError.Add("20", new Error("您的消息已经发送成功！", "MessageCenter.aspx?Type=MSGLIST&Page=1", "THIS"));
            htError.Add("21", new Error("您的消息已经成功删除！", "MessageAndFriend.aspx", "THIS"));
            htError.Add("21b", new Error("您的消息已经成功删除！", "DevMessage.aspx", "THIS"));
            htError.Add("22b", new Error("您的消息删除时出现错误！", "DevMessage.aspx", "THIS"));
            htError.Add("22", new Error("您的消息删除时出现错误！", "MessageCenter.aspx?Type=MSGLIST&Page=1", "THIS"));
            htError.Add("23", new Error("您的留言已经添加成功！", "DevMessage.aspx", "THIS"));
            htError.Add("24", new Error("您的定单记录已经成功删除！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("25", new Error("您的订单记录删除时出现错误！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("26", new Error("您的定单记录核查已提交！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("27", new Error("您的定单记录核查提交出错！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("28", new Error("支付信息出现错误，请核对！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("29", new Error("您已经成功地购买了金币！", "XBAFinance.aspx?Type=COIN&Page=1", "THIS"));
            htError.Add("210", new Error("没有支付成功，请核查！", "XBAFinance.aspx?Type=ORDER&Page=1", "THIS"));
            htError.Add("211", new Error("您进行了非法操作，请返回！", "NULL", "NULL"));
            htError.Add("30", new Error("您的好友已经添加成功！", "MessageCenter.aspx?Type=FRIENDLIST", "THIS"));
            htError.Add("31", new Error("您的好友已经成功删除！", "MessageCenter.aspx?Type=FRIENDLIST", "THIS"));
            htError.Add("32", new Error("您的好友删除时出现错误！", "MessageCenter.aspx?Type=FRIENDLIST", "THIS"));
            htError.Add("40", new Error("您的约战请求已发送成功！", "FriMatchMessage.aspx?Type=TRAINCENTER", "THIS"));
            htError.Add("40a5", new Error("您的约战请求已发送成功！", "FriMatchMessage.aspx?Type=TRAINCENTER5", "THIS"));
            htError.Add("40b", new Error("训练赛进行中……您可以点击“比赛中”链接查看当前比赛详情及比分！", "FriMatchMessage.aspx?Type=TRAINCENTER&CC=AdMatch", "THIS"));
            htError.Add("40a", new Error("您的约战请求已发送成功！", "FMatchCenter.aspx?Type=TRAINCENTER&Page=1", "THIS"));
            htError.Add("41", new Error("您已取消了该次约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("42", new Error("您已经同意该次约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("43", new Error("您已经拒绝该次约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("44", new Error("您在取消约战时出错！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("45", new Error("您在同意该次约战时出错！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("451", new Error("有比赛正在进行，暂时不能再次约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("452", new Error("对方有比赛正在进行，暂时不能再次约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("46", new Error("您在拒绝该次约战时出错！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("47", new Error("您无法对其发送约战请求，请核查！", "NULL", "NULL"));
            htError.Add("48", new Error("没有此球队，您不能向其发送约战邀请！", "NULL", "NULL"));
            htError.Add("491", new Error("您本轮已经打过一场街球训练赛了！", "NULL", "NULL"));
            htError.Add("491b", new Error("有一方在胜者为王赛中报名，无法发起约战！", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("492", new Error("您本轮已经打过一场职业训练赛了！", "NULL", "NULL"));
            htError.Add("493", new Error("您或此球队正在进行一场比赛，暂时不能再次发送请求！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("494", new Error("您暂时无需与此球员续约！", "NULL", "NULL"));
            htError.Add("495", new Error("您的球队人数不足，无法发出约战请求！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("496", new Error("对方球队人数不足，无法发出约战请求！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("497", new Error("未找到该球队！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("498", new Error("您不能与自己约战！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("495a", new Error("您的球队人数不足，无法发出约战请求！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("496a", new Error("对方球队人数不足，无法发出约战请求！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("497a", new Error("未找到该球队！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("498a", new Error("您不能与自己约战！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("498b", new Error("您不能与自己约战！", "NULL", "NULL"));
            htError.Add("498c", new Error("街球队不能进行表演赛！", "NULL", "NULL"));
            htError.Add("499", new Error("暂时没有开放职业训练赛功能！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4100", new Error("您的球员训练赛热情不足！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4101", new Error("对方球员训练赛热情不足！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4102", new Error("有正在进行或等待进行的比赛，无法进行训练赛！请在约战信息中查看是否有没有接受或者拒绝的训练赛、友谊赛等。", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4103", new Error("您与此球队在本赛季已经训练过了，不能重复训练！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4104", new Error("您不能与自己的球队打训练赛！", "FriMatchMessage.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("4105", new Error("您还不是会员，请申请会员以获得该项服务！", "NULL", "NULL"));
            htError.Add("4106", new Error("您或者对方在今日已经不能再打训练赛了！", "NULL", "NULL"));
            htError.Add("4107", new Error("您已经报名参加了训练大厅，无法再次报名！", "NULL", "NULL"));
            htError.Add("4108", new Error("您已经报名成功！点击“开始比赛”可以开始一场训练赛，您也可以等待其他球队发出的邀请！", "FriMatchMessage.aspx", "THIS"));
            htError.Add("4108c", new Error("您已经退出训练中心。如果当前有比赛正在进行，比赛将继续进行直到结束。您可以在<span style='color:red'><strong>我的比赛</strong></span>页签中查看详细情况！", "FriMatchMessage.aspx?Type=TRAINCENTERREG", "THIS"));
            htError.Add("4108c5", new Error("您已经退出训练中心。如果当前有比赛正在进行，比赛将继续进行直到结束。您可以在<span style='color:red'><strong>我的比赛</strong></span>页签中查看详细情况！", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4108b", new Error("恢复经费必须在100游戏币至10000游戏币之间！", "FriMatchMessage.aspx", "THIS"));
            htError.Add("4108d", new Error("您的游戏币不足请重新输入恢复经费！", "FriMatchMessage.aspx", "THIS"));
            htError.Add("4112", new Error("您暂时无法报名训练赛，请确认是否有街球球队。", "NULL", "NULL"));
            htError.Add("4112x", new Error("您暂时无法报名训练赛，请确认是否有职业球队。", "NULL", "NULL"));
            htError.Add("4112a", new Error("您有正在等待进行的友谊赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112b", new Error("您有正在等待进行的表演赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112c", new Error("您有正在等待进行的训练赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112d", new Error("与该球队在本赛季有过训练赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112e", new Error("您本日的训练赛已经超过三场，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112f", new Error("对方有正在等待进行的友谊赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112g", new Error("对方有正在等待进行的表演赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112h", new Error("对方有正在等待进行的训练赛，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112i", new Error("您的球队体力不足，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112j", new Error("对方球队体力不足，无法发送训练赛。", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("4112a5", new Error("您有正在等待进行的友谊赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112b5", new Error("您有正在等待进行的表演赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112c5", new Error("您有正在等待进行的训练赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112d5", new Error("与该球队在本赛季有过训练赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112e5", new Error("您本日的训练赛已经超过三场，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112f5", new Error("对方有正在等待进行的友谊赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112g5", new Error("对方有正在等待进行的表演赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112h5", new Error("对方有正在等待进行的训练赛，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112i5", new Error("您的球队体力不足，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4112j5", new Error("对方球队体力不足，无法进入职业训练中心。", "FriMatchMessage.aspx?Type=TRAINCENTERREG5", "THIS"));
            htError.Add("4109", new Error("球员姓名中不能包含敏感字符，请返回！", "NULL", "NULL"));
            htError.Add("4110", new Error("球员姓名不符合规则，请填写10个英文字符以内！", "NULL", "NULL"));
            htError.Add("4111", new Error("球员姓名已存在，请重新填写！", "NULL", "NULL"));
            htError.Add("4113", new Error("该球员不在您的球队中，无法对其进行修改。", "NULL", "NULL"));
            htError.Add("50", new Error("帖子删除成功！", "Board.aspx", "NULL"));
            htError.Add("51", new Error("帖子置顶成功！", "Topic.aspx", "NULL"));
            htError.Add("52", new Error("取消置顶成功！", "Topic.aspx", "NULL"));
            htError.Add("53", new Error("添加精华成功！", "Topic.aspx", "NULL"));
            htError.Add("54", new Error("取消精华成功！", "Topic.aspx", "NULL"));
            htError.Add("55", new Error("帖子锁定成功！", "Topic.aspx", "NULL"));
            htError.Add("56", new Error("取消锁定成功！", "Topic.aspx", "NULL"));
            htError.Add("57", new Error("您不具备该权限，请返回！", "NULL", "NULL"));
            htError.Add("58", new Error("设定已解决成功！", "Topic.aspx", "NULL"));
            htError.Add("59", new Error("设定未解决成功！", "Topic.aspx", "NULL"));
            htError.Add("501", new Error("您的财富不够，不能发表新帖！", "Board.aspx", "NULL"));
            htError.Add("502", new Error("您暂时无法在论坛发言。", "Board.aspx", "NULL"));
            htError.Add("50f", new Error("帖子删除成功！", "FrameBoard.aspx", "THIS"));
            htError.Add("51f", new Error("帖子置顶成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("52f", new Error("取消置顶成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("53f", new Error("添加精华成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("54f", new Error("取消精华成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("55f", new Error("帖子锁定成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("56f", new Error("取消锁定成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("57f", new Error("您不具备该权限，请返回！", "NULL", "THIS"));
            htError.Add("58f", new Error("设定已解决成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("59f", new Error("设定未解决成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("501f", new Error("您的财富不够，不能发表新帖！", "FrameBoard.aspx", "THIS"));
            htError.Add("502f", new Error("您暂时无法在论坛发言。", "FrameBoard.aspx", "THIS"));
            htError.Add("50u", new Error("帖子删除成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("51u", new Error("帖子置顶成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("52u", new Error("取消置顶成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("53u", new Error("添加精华成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("54u", new Error("取消精华成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("55u", new Error("帖子锁定成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("56u", new Error("取消锁定成功！", "UnionBoard.aspx", "THIS"));
            htError.Add("57u", new Error("您不具备该权限，请返回！", "NULL", "NULL"));
            htError.Add("58u", new Error("此联盟论坛已经锁定，您还未加入该联盟，或者您需要重新登录以更新您的联盟所属信息！", "NULL", "NULL"));
            htError.Add("60", new Error("您还没有成立职业球队，无法再此进行出价！", "TransferMarket.aspx", "THIS"));
            htError.Add("61", new Error("您填写的价格错误，请使用半角的阿拉伯数字！", "TransferMarket.aspx", "THIS"));
            htError.Add("62", new Error("您填写的价格错误，请使用半角的阿拉伯数字！", "TransferMarket.aspx?Type=STREETCHOOSE&Pos=1&Order=0&Page=1", "THIS"));
            htError.Add("63", new Error("您已经有过出价记录，不能再次出价了！", "TransferMarket.aspx?Type=STREETCHOOSE&Pos=1&Order=0&Page=1", "THIS"));
            htError.Add("64", new Error("举牌成功，若拍卖成功，该球员将于拍卖截止后半小时到队！", "TransferMarket.aspx?Type=STREETCHOOSE&Pos=1&Order=0&Page=1", "THIS"));
            htError.Add("65", new Error("举牌成功，若拍卖成功，该球员将于拍卖截止后半小时到队！", "TransferMarket.aspx?Type=STREETFREE&Pos=1&Order=0&Page=1", "THIS"));
            htError.Add("66", new Error("资金不足，请返回！", "TransferMarket.aspx?Type=STREETFREE&Pos=1&Order=0&Page=1", "THIS"));
            htError.Add("67", new Error("购买成功，您已经拥有一支职业队！联赛暂时没有空位，在有空位时您可以申请加入职业联赛。", "TopCenter.aspx", "THIS"));
            htError.Add("67a", new Error("购买成功，您已经拥有一支职业队！并成功加入职业联赛。", "TopCenter.aspx", "THIS"));
            htError.Add("68", new Error("球员挂牌成功，请返回！", "PlayerCenter.aspx", "THIS"));
            htError.Add("69", new Error("资金不足，请返回！", "TransferMarket.aspx", "THIS"));
            htError.Add("691", new Error("举牌成功，若拍卖成功，该球员将于拍卖截止后半小时到队！", "TransferMarket.aspx", "THIS"));
            htError.Add("691a", new Error("成功使用拍卖委托，即将返回关注页面！", "TransferMarket.aspx", "THIS"));
            htError.Add("691b", new Error("成功使用潜力公告，即将返回关注页面！", "TransferMarket.aspx", "THIS"));
            htError.Add("692", new Error("您是最高出价者,无需再次出价，请返回！", "TransferMarket.aspx", "THIS"));
            htError.Add("693", new Error("您的出价不在范围内，请返回！", "NULL", "NULL"));
            htError.Add("693a", new Error("您的球队资金不足，无法购买该职业队，请返回！", "TopCenter.aspx", "THIS"));
            htError.Add("694", new Error("此球队已经卖出，请选择其他球队！", "NULL", "NULL"));
            htError.Add("695", new Error("您已经对一个球队出价，请不要重复出价！", "TopCenter.aspx", "THIS"));
            htError.Add("696", new Error("出价金额不能低于1000，请重新填写！", "NULL", "NULL"));
            htError.Add("697", new Error("本次选拔已经结束，请等待下次选拔。", "NULL", "NULL"));
            htError.Add("6970", new Error("您已无选拔卡，请等待下赛季发放或从道具商店中购买。", "NULL", "NULL"));
            htError.Add("698", new Error("您已经拥有职业队，请重新登录进行管理。", "Logout.aspx", "TOP"));
            htError.Add("699", new Error("本次职业队竞价时间已截止！", "TopCenter.aspx", "THIS"));
            htError.Add("6990", new Error("选秀成功，请返回！若拍卖成功，该球员将于拍卖截止后半小时到队", "TransferMarket.aspx", "THIS"));
            htError.Add("6991", new Error("您没有选秀权，无法进行选秀！", "TransferMarket.aspx", "THIS"));
            htError.Add("6992", new Error("您在职业选秀中有过选秀记录，无法再次选秀！", "TransferMarket.aspx", "THIS"));
            htError.Add("6993", new Error("您的选秀顺位不足以选择该球员！", "TransferMarket.aspx", "THIS"));
            htError.Add("6994", new Error("选秀截止前3小时内，您不得顶掉他人的选秀权，请选择其他球员！", "TransferMarket.aspx", "THIS"));
            htError.Add("6995", new Error("选秀已截止！", "TransferMarket.aspx", "THIS"));
            htError.Add("6996", new Error("您的职业队球员过多，不能再进行选秀了！", "TransferMarket.aspx", "THIS"));
            htError.Add("6910", new Error("成功使用潜力报告！（点击球员属性页面的私开图标您将会看到此球员的潜力）即将返回关注页面！", "TransferMarket.aspx", "THIS"));
            htError.Add("6911", new Error("您没有对些球员使用过意识评估报告。", "NULL", "NULL"));
            htError.Add("70", new Error("您的阵容（战术）设置不完整，请重新设置！", "NULL", "NULL"));
            htError.Add("71", new Error("您已经成功的设置了阵容（战术）！", "SArrange.aspx", "THIS"));
            htError.Add("72", new Error("您的阵容（战术）已经配置完毕！", "SArrange.aspx", "THIS"));
            htError.Add("73", new Error("您的阵容（战术）名称有误，请保证为20个字符长度以内的合法字符！", "SArrange.aspx", "THIS"));
            htError.Add("74", new Error("您的阵容（战术）设置不完整，请重新设置！", "NULL", "NULL"));
            htError.Add("75a", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=green>100%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75b", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>99%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75c", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>98%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75d", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>97%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75e", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>96%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75f", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>95%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75g", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>94%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75h", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>93%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75i", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>92%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75j", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>91%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75k", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>90%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75l", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>89%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75m", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>88%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75n", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>87%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75o", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>86%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75p", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>85%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75q", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>84%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75r", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>83%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75s", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>82%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75t", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>81%</font>", "VArrange.aspx", "THIS"));
            htError.Add("75u", new Error("您已经成功的设置了阵容（战术）！阵容状态为 <font color=red>80%</font>", "VArrange.aspx", "THIS"));
            htError.Add("76", new Error("您的阵容（战术）已经配置完毕！", "VArrange.aspx", "THIS"));
            htError.Add("77", new Error("您的阵容（战术）名称有误，请保证为20个字符长度以内的合法字符！", "VArrange.aspx", "THIS"));
            htError.Add("78", new Error("进攻战术请选择中锋策应，防守战术请选择盯人内线，这样才能克制对方！", "RookieMain_P.aspx?Type=SARRANGE", "NULL"));
            htError.Add("78a", new Error("预设战术设置成功！", "DevisionView.aspx", "THIS"));
            htError.Add("78b", new Error("预设战术设置成功！", "ChampionCup.aspx", "THIS"));
            htError.Add("80", new Error("你已经有该职业的职员无法再次雇佣。", "Staff.aspx?Type=0&Grade=0&Page=1&Refresh=0", "THIS"));
            htError.Add("81", new Error("您的资金不够支付该职员的薪水，无法雇佣该职员。", "Staff.aspx?Type=0&Grade=0&Page=1&Refresh=0", "THIS"));
            htError.Add("82", new Error("您还没有职业球队，无法雇佣职员。", "Staff.aspx?Type=0&Grade=0&Page=1&Refresh=0", "THIS"));
            htError.Add("83", new Error("球员已经被下放。", "PlayerCenter.aspx", "THIS"));
            htError.Add("83a", new Error("球员已经被下放。", "PlayerCenter.aspx", "THIS"));
            htError.Add("84", new Error("职员已经被解雇。", "StaffManage.aspx", "THIS"));
            htError.Add("85", new Error("请成为我们的会员以享受更多功能。", "StaffManage.aspx", "THIS"));
            htError.Add("86", new Error("该职员不在您的俱乐部中任职，请核对！。", "StaffManage.aspx", "THIS"));
            htError.Add("90", new Error("您还没有进入职业联赛，无法使用该功能！", "NULL", "NULL"));
            htError.Add("91", new Error("您的荣誉球员添加过多，无法继续添加！", "NULL", "NULL"));
            htError.Add("92", new Error("该球员已经是荣誉球员，您无法再次添加！", "Honour.aspx", "THIS"));
            htError.Add("93", new Error("杯赛报名成功！", "CupList.aspx?Type=SIGNUP&Grade=0&Page=1", "THIS"));
            htError.Add("94", new Error("报名出错，请确认无误后再报名！", "CupList.aspx?Type=SIGNUP&Grade=0&Page=1", "THIS"));
            htError.Add("95", new Error("您现在的资金不足够参加该杯赛！", "CupList.aspx?Type=SIGNUP&Grade=0&Page=1", "THIS"));
            htError.Add("96", new Error("您的球队人数不足，无法参加杯赛！", "CupList.aspx?Type=SIGNUP&Grade=0&Page=1", "THIS"));
            htError.Add("97", new Error("杯赛人数已报满！", "CupList.aspx?Type=SIGNUP&Grade=0&Page=1", "THIS"));
            htError.Add("98", new Error("报名该杯赛需要邀请函。", "NULL", "NULL"));
            htError.Add("100", new Error("球员续约成功，请返回。", "PlayerCenter.aspx", "THIS"));
            htError.Add("P101", new Error("此球员已经被成功提拔到职业队。", "PlayerCenter.aspx", "THIS"));
            htError.Add("P102", new Error("此球员被成功下放，离开XBA。", "PlayerCenter.aspx", "THIS"));
            htError.Add("P103", new Error("此球员已经是挂牌状态，无需重复操作。", "PlayerCenter.aspx", "THIS"));
            htError.Add("P104", new Error("此球员成功在转会市场挂牌。", "PlayerCenter.aspx", "THIS"));
            htError.Add("101", new Error("您的资金不够，无法参加联赛，请返回。", "NULL", "NULL"));
            htError.Add("102", new Error("联赛申请成功，如果您所属的联赛满14人，XBA将会为您安排赛程，次日开始职业联赛的比赛。", "Main.aspx", "TOP"));
            htError.Add("103", new Error("您还没有职业队，不能申请联赛。", "NULL", "NULL"));
            htError.Add("104", new Error("您已经参加职业联赛，请进入专门为您开放的街球自由转会。", "NULL", "NULL"));
            htError.Add("105", new Error("您输入的联赛不存在，请正确输入联赛。", "Devision.aspx", "THIS"));
            htError.Add("106", new Error("已经没有职业联赛空位，不能申请联赛。", "NULL", "NULL"));
            htError.Add("107", new Error("该球员不再您的球队中效力，无法对话。", "", "TOP"));
            htError.Add("110", new Error("球员选拔只能在赛季中与赛季末进行，请返回。", "NULL", "NULL"));
            htError.Add("111", new Error("本次选拔已经结束，请等待下次机会。", "NULL", "NULL"));
            htError.Add("112", new Error("您的职业队球员过多无法再选拔球员，请返回。", "NULL", "NULL"));
            htError.Add("113", new Error("您的街球队球员过少无法再选拔球员，请返回。", "NULL", "NULL"));
            htError.Add("114", new Error("职业球员选拔成功。", "PlayerCenter.aspx", "THIS"));
            htError.Add("120", new Error("您的球场正在建设中，请返回。", "ClubBuild.aspx", "THIS"));
            htError.Add("121", new Error("您的资金不足，无法建设球场，请返回。", "ClubBuild.aspx", "THIS"));
            htError.Add("122", new Error("球场已经开始建设，请返回。", "ClubBuild.aspx", "THIS"));
            htError.Add("123", new Error("球票价格更改成功，请返回。", "ClubBuild.aspx", "THIS"));
            htError.Add("124", new Error("您的广告位已满，请升级球场以获得更多的广告位。", "ClubBuild.aspx", "THIS"));
            htError.Add("125", new Error("广告选择成功，请返回。", "ClubBuild.aspx", "THIS"));
            htError.Add("126", new Error("赞助商拒绝悬挂更多的广告，请您更好的经营俱乐部以获得更多赞助。", "ClubBuild.aspx", "THIS"));
            htError.Add("127", new Error("您只有成为会员才能选择高于30的球票价格。", "ClubBuild.aspx", "THIS"));
            htError.Add("130", new Error("您已经参加过本场竞猜，请返回。", "RM.aspx", "THIS"));
            htError.Add("131", new Error("竞猜成功，请返回。", "RM.aspx", "THIS"));
            htError.Add("132", new Error("请正确输入分数。", "RM.aspx", "THIS"));
            htError.Add("201", new Error("比赛正在进行中，请在10:00之后进行此项管理，会员可在9:00观看比赛直播。请先进行其它的管理。", "NULL", "NO"));
            htError.Add("202", new Error("购买道具成功！", "ManagerTool.aspx", "THIS"));
            htError.Add("203", new Error("您的金币不足，无法购买该道具！", "ManagerTool.aspx", "THIS"));
            htError.Add("204", new Error("道具存货不足，无法购买该道具！", "ManagerTool.aspx", "THIS"));
            htError.Add("205", new Error("您暂时无法进行此操作，请稍候再试！", "ManagerTool.aspx", "THIS"));
            htError.Add("206", new Error("赠送会员卡成功！", "ManagerTool.aspx", "THIS"));
            htError.Add("207", new Error("购买道具个数不正确！", "ManagerTool.aspx", "THIS"));
            htError.Add("208", new Error("您只能拥有1个拍卖委托！", "ManagerTool.aspx", "THIS"));
            htError.Add("4001", new Error("帖子发表成功，按确定返回帖子！", "Topic.aspx", "NULL"));
            htError.Add("4002", new Error("帖子编辑成功，按确定返回帖子！", "Topic.aspx", "NULL"));
            htError.Add("4003", new Error("此板块已关闭！", "Forum.aspx", "NULL"));
            htError.Add("4004", new Error("要访问此板块需要通过认证！", "Forum.aspx", "NULL"));
            htError.Add("4001f", new Error("帖子发表成功，按确定返回帖子！", "FrameTopic.aspx", "THIS"));
            htError.Add("4002f", new Error("帖子编辑成功，按确定返回帖子！", "FrameTopic.aspx", "THIS"));
            htError.Add("4003f", new Error("此板块已关闭！", "FrameForum.aspx", "THIS"));
            htError.Add("4004f", new Error("要访问此板块需要通过认证！", "FrameForum.aspx", "THIS"));
            htError.Add("4005", new Error("帖子发表成功，按确定返回帖子列表！", "UnionBoard.aspx", "THIS"));
            htError.Add("4006", new Error("帖子编辑成功，按确定返回帖子列表！", "UnionBoard.aspx", "THIS"));
            htError.Add("4007", new Error("此功能暂时不开放，请返回上一页！", "NULL", "NULL"));
            htError.Add("4008", new Error("财富不够，无法加分！", "Topic.aspx", "THIS"));
            htError.Add("4009", new Error("加分成功，请返回。", "Board.aspx", "THIS"));
            htError.Add("4010", new Error("您已经参与过本次投票！", "NULL", "NULL"));
            htError.Add("4011", new Error("投票成功！", "Topic.aspx", "THIS"));
            htError.Add("4008f", new Error("财富不够，无法加分！", "FrameTopic.aspx", "THIS"));
            htError.Add("4009f", new Error("加分成功，请返回。", "FrameBoard.aspx", "THIS"));
            htError.Add("4011f", new Error("投票成功！", "FrameTopic.aspx", "THIS"));
            htError.Add("4012", new Error("新主题发表成功，按确定返回帖子列表！", "Board.aspx", "THIS"));
            htError.Add("4012f", new Error("新主题发表成功，按确定返回帖子列表！", "FrameBoard.aspx", "THIS"));
            htError.Add("401", new Error("冲值已完成。", "XBAFinance.aspx?Type=COIN&Page=1", "NULL"));
            htError.Add("402", new Error("已成功加入XBA会员阵营，在会员卡的有效期内您将获得更多的超值服务。", "Main.aspx", "TOP"));
            htError.Add("403", new Error("您的金币数量不足，暂时不能申请会员。", "AccountManage.aspx?Type=PAY", "NULL"));
            htError.Add("4031", new Error("游戏数据库暂时无法连通，会员付费失败，请稍候再试。", "NULL", "NULL"));
            htError.Add("404", new Error("地址提交成功。", "XBAFinanceAD.aspx?Type=FLACK", "NULL"));
            htError.Add("405", new Error("所提交的地址以被使用过。", "XBAFinanceAD.aspx?Type=FLACK", "NULL"));
            htError.Add("406", new Error("你已经达到提交上限。", "XBAFinanceAD.aspx?Type=FLACK", "NULL"));
            htError.Add("407", new Error("联盟建立成功，请返回。", "Union.aspx", "THIS"));
            htError.Add("408", new Error("您已经加入一个联盟，请先退出联盟。", "Union.aspx", "THIS"));
            htError.Add("409", new Error("本次宣传活动已结束，请下次活动时再次提交宣传帖。多谢您对XBA的热爱和支持！", "XBAFinanceAD.aspx?Type=FLACK", "NULL"));
            htError.Add("410", new Error("请正确填写宣传帖地址！", "XBAFinanceAD.aspx?Type=FLACK", "NULL"));
            htError.Add("411", new Error("您没有足够的金币！", "NULL", "NULL"));
            htError.Add("412", new Error("邀请发送成功！", "NULL", "NULL"));
            htError.Add("413", new Error("对方已有联盟！", "NULL", "NULL"));
            htError.Add("414", new Error("操作已完成！", "NULL", "NULL"));
            htError.Add("415", new Error("您无权进行操作！", "NULL", "NULL"));
            htError.Add("416", new Error("操作成功，请返回！", "Union.aspx", "THIS"));
            htError.Add("417", new Error("请先解任其管理员身份！", "Union.aspx", "THIS"));
            htError.Add("418", new Error("已经是管理员，无须重复任命！", "Union.aspx", "THIS"));
            htError.Add("419", new Error("您无法加入联盟！", "Union.aspx", "THIS"));
            htError.Add("420", new Error("您无法对该经理发出邀请。", "NULL", "NULL"));
            htError.Add("420b", new Error("无法对该经理发出联盟邀请。", "NULL", "NULL"));
            htError.Add("420c", new Error("只有盟主和管理员才能发出联盟邀请。", "NULL", "NULL"));
            htError.Add("420a", new Error("经理名不符合规则，请确认。", "NULL", "NULL"));
            htError.Add("420e", new Error("经理名不能为空，请重新填写。", "NULL", "NULL"));
            htError.Add("420d", new Error("邀请的经理不存在，请确认。", "NULL", "NULL"));
            htError.Add("421", new Error("您暂时没有联盟，请加入联盟后进行此项操作。", "Union.aspx?Type=UNION&Kind=VIEWUNION&Page=1", "THIS"));
            htError.Add("422", new Error("您已经退出联盟，请返回。", "", "TOP"));
            htError.Add("422b", new Error("此杯赛不是您建立的，请返回。", "Union.aspx", "THIS"));
            htError.Add("423", new Error("联盟解散成功，请返回。", "Union.aspx", "THIS"));
            htError.Add("424", new Error("您不是联盟盟主不能使用该功能。", "Union.aspx", "THIS"));
            htError.Add("425", new Error("您不是联盟盟主或者联盟还有其他成员，无法解散联盟。", "Union.aspx", "THIS"));
            htError.Add("425b", new Error("您的联盟有一场盟场正在进行，无法解散联盟。", "Union.aspx", "THIS"));
            htError.Add("425a", new Error("还有未结束的自定义杯赛，无法解散联盟。", "Union.aspx", "THIS"));
            htError.Add("426", new Error("您只能在赛季末退出联盟，请返回。", "NULL", "NULL"));
            htError.Add("427", new Error("管理员人数已达到上限！", "Union.aspx", "THIS"));
            htError.Add("428", new Error("联盟人数最高上限为600！", "Union.aspx", "THIS"));
            htError.Add("429", new Error("已将金币赠与对方，请查验。", "XBAFinance.aspx?Type=COIN&Page=1", "NULL"));
            htError.Add("430", new Error("用户不存在，请核对。", "NULL", "NULL"));
            htError.Add("431", new Error("金币不足，无法进行赠与操作。", "XBAFinance.aspx?Type=COIN&Page=1", "NULL"));
            htError.Add("432", new Error("帐户填写错误，请重新填写。", "NULL", "NULL"));
            htError.Add("433", new Error("金币填写错误，请重新填写。", "NULL", "NULL"));
            htError.Add("434", new Error("赠与金币需最少1枚，请重新填写。", "NULL", "NULL"));
            htError.Add("435", new Error("金币赠与后，您的金币余额将不足20枚，无法赠与。", "NULL", "NULL"));
            htError.Add("436", new Error("昵称填写错误，请重新填写。", "NULL", "NULL"));
            htError.Add("437", new Error("分组编号填写错误，请重新填写。", "NULL", "NULL"));
            htError.Add("438", new Error("对不起，只有持有冠军杯邀请函的玩家可以在今天报名，如果您持有的是冠军杯邀请函（盟），请明天再来！", "NULL", "NULL"));
            htError.Add("438b", new Error("对不起，您没有冠军杯邀请函或冠军杯邀请函（盟），只能在联赛第三轮报名！", "NULL", "NULL"));
            htError.Add("438c", new Error("您不在职业联赛中，请返回首页申请加入职业联赛。", "NULL", "NULL"));
            htError.Add("439", new Error("您已经报名参加过本次比赛。", "NULL", "NULL"));
            htError.Add("440", new Error("比赛报名人数已满。", "NULL", "NULL"));
            htError.Add("441", new Error("拥有相同分组编号的球队过多，请重新填写分组编号。", "NULL", "NULL"));
            htError.Add("442", new Error("您已成功报名，比赛会在联赛第三轮当天的下午4点正式开始。", "ChampionCup.aspx?Kind=CHAMPIONREG", "THIS"));
            htError.Add("443", new Error("您的金币不够4枚，无法报名杯赛。", "NULL", "NULL"));
            htError.Add("444", new Error("比赛报名人数已满。", "NULL", "NULL"));
            htError.Add("445", new Error("您暂时无法进行此操作，请稍候再试！", "NULL", "NULL"));
            htError.Add("446", new Error("用户名已存在，请重新输入！", "NULL", "CLOSE"));
            htError.Add("447", new Error("昵称已存在，请重新输入！", "NULL", "CLOSE"));
            htError.Add("448", new Error("用户名长度不符合规定，请重新填写。", "NULL", "CLOSE"));
            htError.Add("449", new Error("用户名，昵称已存在，请重新输入！", "NULL", "CLOSE"));
            htError.Add("450", new Error("请确定已同意XBA用户协议。", "register2.aspx", "NULL"));
            htError.Add("453", new Error("昵称填写不符合规定，请重新填写。", "NULL", "CLOSE"));
            htError.Add("454", new Error("用户名填写不符合规定，请重新填写。", "NULL", "CLOSE"));
            htError.Add("455", new Error("请填写所在省份。", "register2.aspx", "NULL"));
            htError.Add("456", new Error("请填写所在城市。", "register2.aspx", "NULL"));
            htError.Add("457", new Error("请填写年份。", "register2.aspx", "NULL"));
            htError.Add("458", new Error("请填写月份。", "register2.aspx", "NULL"));
            htError.Add("459", new Error("请填写日期。", "register2.aspx", "NULL"));
            htError.Add("460", new Error("请填写性别。", "register2.aspx", "NULL"));
            htError.Add("461", new Error("请填写您从哪里知道的XBA。", "register2.aspx", "NULL"));
            htError.Add("462", new Error("用户名填写错误。", "register2.aspx", "NULL"));
            htError.Add("463", new Error("用户名长度填写错误。", "register2.aspx", "NULL"));
            htError.Add("464", new Error("请先填写密码。", "register2.aspx", "NULL"));
            htError.Add("465", new Error("密码填写不符合规定，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("466", new Error("两次密码输入不相同，请确认后重新输入。", "register2.aspx", "NULL"));
            htError.Add("467", new Error("请正确填写Email。", "register2.aspx", "NULL"));
            htError.Add("468", new Error("昵称填写错误，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("469", new Error("用户名已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("470", new Error("昵称存在，昵称已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("471", new Error("Email已使用，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("472", new Error("用户名，昵称已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("473", new Error("用户名，Email已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("474", new Error("昵称，Email已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("475", new Error("用户名，昵称，Email已存在，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("476", new Error("QQ号码只允许填写数字，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("477", new Error("QQ号码位数不符合要求，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("478", new Error("邀请人昵称填写不符合规定，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("479", new Error("邀请人不存在，请确认后重新填写。", "register2.aspx", "NULL"));
            htError.Add("480", new Error("宣言超出字数限制，请重新填写。", "register2.aspx", "NULL"));
            htError.Add("481", new Error("请正确填写宣言内容，不要带有特殊符号。", "register2.aspx", "NULL"));
            htError.Add("482", new Error("您尚未登录，无法管理火箭竞猜，请先登录。", "", "NULL"));
            htError.Add("483", new Error("您的用户名和昵称符合要求，可以注册！", "NULL", "CLOSE"));
            htError.Add("484", new Error("请输入正确的游戏币（大于10）！", "NULL", "NULL"));
            htError.Add("485", new Error("您没有足够的游戏币可供提取！", "NULL", "NULL"));
            htError.Add("486", new Error("您没足够的游戏币可供存入！", "NULL", "NULL"));
            htError.Add("487", new Error("请您在游戏币和让分栏中输入正确的半角数字！", "NULL", "NULL"));
            htError.Add("488", new Error("对方现在没有足够游戏币，不能超出他游戏币的50%！", "NULL", "NULL"));
            htError.Add("489", new Error("有10场以上约战在进行中请稍后再约（会员不受此限制）！", "NULL", "NULL"));
            htError.Add("490", new Error("不能超过你取出的游戏币！", "NULL", "NULL"));
            htError.Add("4911", new Error("有20场以上约战在进行中请稍后再约（会员不受此限制）！", "NULL", "NULL"));
            htError.Add("4912", new Error("发送已成功！", "NULL", "NULL"));
            htError.Add("500", new Error("暂时只对职业队开放！", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("500b", new Error("有一方无法支付表演赛的场地费用！", "FriMatchMessage.aspx?Type=FMATCHSEND", "THIS"));
            htError.Add("500c", new Error("暂时只对职业队开放！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("500d", new Error("有一方无法支付表演赛的场地费用！", "FMatchCenter.aspx?Type=FMATCHSEND&Page=1", "THIS"));
            htError.Add("513", new Error("您已经报过名了！", "NULL", "NULL"));
            htError.Add("512", new Error("不能超过你取出的游戏币！", "NULL", "NULL"));
            htError.Add("515", new Error("接收到的约战信息有错误！", "NULL", "NULL"));
            htError.Add("514", new Error("请选择是主队让分还是客队让分！", "NULL", "NULL"));
            htError.Add("516", new Error("您输入的游戏币超出范围请输入10-1000之内的（半角）数字！", "NULL", "NULL"));
            htError.Add("517", new Error("您输入的让分超出范围请输入1-100之内（半角）数字！", "NULL", "NULL"));
            htError.Add("518", new Error("游戏币不能小于10，让分不能小于1！", "NULL", "NULL"));
            htError.Add("519", new Error("游戏币不能小于10！", "NULL", "NULL"));
            htError.Add("520", new Error("捐献游戏币数不能小于10大于999999！", "NULL", "NULL"));
            htError.Add("521", new Error("捐献游戏币成功！", "Union.aspx", "THIS"));
            htError.Add("522", new Error("报名成功！", "DevCup.aspx", "THIS"));
            htError.Add("523", new Error("您已经在此杯赛中报过名！", "NULL", "NULL"));
            htError.Add("525", new Error("您提出的游戏币太少不够报名费用！", "NULL", "NULL"));
            htError.Add("526", new Error("您的球队不在联赛等级要求范围之内！", "NULL", "NULL"));
            htError.Add("527", new Error("您的捐赠留言不能多于100个字符！", "NULL", "NULL"));
            htError.Add("528", new Error("此杯赛已报满！", "NULL", "NULL"));
            htError.Add("529", new Error("无此杯赛！", "NULL", "NULL"));
            htError.Add("530", new Error("您没有职业队不能报名！", "NULL", "NULL"));
            htError.Add("531", new Error("已成功踢出！", "Union.aspx", "THIS"));
            htError.Add("532", new Error("报名已截止！", "NULL", "NULL"));
            htError.Add("533", new Error("杯赛已经开始您不能再踢出参赛玩家！", "Union.aspx", "THIS"));
            htError.Add("534", new Error("您不是举办杯赛的盟主！", "Union.aspx", "THIS"));
            htError.Add("535", new Error("您的杯赛报名密码不正确！", "NULL", "NULL"));
            htError.Add("535b", new Error("您只可以同时报名参加两场自定义杯赛！", "NULL", "NULL"));
            htError.Add("536", new Error("您没有足够的游戏币可供捐献！", "NULL", "NULL"));
            htError.Add("537", new Error("您没有足够的游戏币来进行比赛！", "NULL", "NULL"));
            htError.Add("538", new Error("您填写的经理名称有错误！", "NULL", "NULL"));
            htError.Add("539", new Error("没有此经理名！", "NULL", "NULL"));
            htError.Add("550", new Error("您没有足够的游戏币，请到游戏币管理中提取！", "NULL", "NULL"));
            htError.Add("551", new Error("这是您自己的球队，比赛录像可以在联赛赛程中观看！", "NULL", "NULL"));
            htError.Add("552", new Error("这是您自己的球队，比赛战术等级可以在战术设定中看到！", "NULL", "NULL"));
            htError.Add("553", new Error("您上传的自定义图标不能大于10K！", "ModifyClub.aspx", "THIS"));
            htError.Add("555", new Error("您上传的自定义图标必须为.GIF格式！", "ModifyClub.aspx", "THIS"));
            htError.Add("556", new Error("您上传的自定义图标的路径有问题！", "ModifyClub.aspx", "THIS"));
            htError.Add("557", new Error("您填写的经理名有误！", "SecretaryPage.aspx", "THIS"));
            htError.Add("558", new Error("你已经成功接手此俱乐部，您需要跳转至首页", "", "TOP"));
            htError.Add("559", new Error("此俱乐部还有其他经理管理，请重新选择！", "AssMain.aspx", "TOP"));
            htError.Add("560", new Error("您已经成功拥有了一个俱乐部！请重新登录。", "Login.aspx", "TOP"));
            htError.Add("561", new Error("此道具您只能购买一个！", "ManagerTool.aspx?Type=TOOLS&Page=1", "THIS"));
            htError.Add("600", new Error("您不是盟主，无法删除该杯赛！", "Union.aspx", "THIS"));
            htError.Add("601", new Error("您不是该联盟的盟主，无法删除该杯赛！", "Union.aspx", "THIS"));
            htError.Add("602", new Error("杯赛删除成功！", "Union.aspx", "THIS"));
            htError.Add("603", new Error("杯赛不存在！", "Union.aspx", "THIS"));
            htError.Add("604", new Error("还有经理在报名中，无法删除该杯赛！", "Union.aspx", "THIS"));
            htError.Add("605", new Error("杯赛修改成功！", "Union.aspx", "THIS"));
            htError.Add("606", new Error("杯赛创建成功！", "Union.aspx", "THIS"));
            htError.Add("607", new Error("您没有足够的游戏币，无法报名该杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("608", new Error("该杯赛只允许盟内成员报名，您不属于该联盟！", "DevCup.aspx", "THIS"));
            htError.Add("609", new Error("您所在的联赛等级不符合要求，不能参加该杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("610", new Error("杯赛暂时无法被创建，请稍候再试。", "Union.aspx", "THIS"));
            htError.Add("611", new Error("您不是盟主，无法建立杯赛。", "Union.aspx", "THIS"));
            htError.Add("612", new Error("您联盟没有足够的游戏币用来建立该杯赛。", "Union.aspx", "THIS"));
            htError.Add("613", new Error("您不是该联盟的盟主，无法建立该杯赛。", "Union.aspx", "THIS"));
            htError.Add("614", new Error("请确认填写无误后，再创建杯赛。", "Union.aspx", "THIS"));
            htError.Add("622", new Error("您已经参加过本界杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("623", new Error("杯赛创建成功！", "DevCup.aspx", "THIS"));
            htError.Add("624", new Error("您没有足够的游戏币用来建立该杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("625", new Error("请确认填写无误后，再创建杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("626", new Error("请确认填写无误后，再创建杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("627", new Error("杯赛修改成功！", "DevCup.aspx", "THIS"));
            htError.Add("628", new Error("不是杯赛创建人，无法删除杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("629", new Error("杯赛删除成功！", "DevCup.aspx", "THIS"));
            htError.Add("630", new Error("杯赛报名人数大于0，无法删除杯赛！", "DevCup.aspx", "THIS"));
            htError.Add("631", new Error("杯赛不存在！", "DevCup.aspx", "THIS"));
            htError.Add("632", new Error("已成功踢出！", "DevCup.aspx", "THIS"));
            htError.Add("633", new Error("杯赛已经开始您不能再踢出参赛玩家！", "DevCup.aspx", "THIS"));
            htError.Add("634", new Error("您不是杯赛创建人，无法踢出玩家！", "DevCup.aspx", "THIS"));
            htError.Add("615", new Error("请填写继位经理名称。", "Union.aspx", "THIS"));
            htError.Add("616", new Error("请正确填写继位经理名称。", "Union.aspx", "THIS"));
            htError.Add("617", new Error("您不是盟主，无法禅让盟主位置。", "Union.aspx", "THIS"));
            htError.Add("617b", new Error("您的联盟正有正在时行弹劾盟主，在此期间不能进行禅让。", "Union.aspx", "THIS"));
            htError.Add("617c", new Error("此盟员已被标记删除。", "Union.aspx", "THIS"));
            htError.Add("618", new Error("继任经理不是您联盟的成员，无法禅让。", "Union.aspx", "THIS"));
            htError.Add("619", new Error("继任经理已经是盟主，无法继任。", "Union.aspx", "THIS"));
            htError.Add("620", new Error("输入的继任经理不存在，请确认后再禅让。", "Union.aspx", "THIS"));
            htError.Add("621", new Error("禅让盟主成功，请返回。", "Union.aspx", "THIS"));
            htError.Add("701", new Error("您没有登陆，请先登录再执行操作。", "51Admin.aspx", "NULL"));
            htError.Add("702", new Error("信息删除成功，请返回", "51AdminMain.aspx?Type=ANNOUNCE", "NULL"));
            htError.Add("703", new Error("信息修改成功，请返回", "51AdminMain.aspx?Type=ANNOUNCE", "NULL"));
            htError.Add("704", new Error("信息添加成功，请返回", "51AdminMain.aspx?Type=ANNOUNCE", "NULL"));
            htError.Add("705", new Error("您的游戏币不足，无法进行球员洗点", "PlayerCenter.aspx", "THIS"));
            htError.Add("706", new Error("球员洗点成功，请返回。", "PlayerCenter.aspx", "THIS"));
            htError.Add("707", new Error("该球员不在您的俱乐部效力，无法进行球员洗点。", "PlayerCenter.aspx", "THIS"));
            htError.Add("708", new Error("您的球员洗点失败，所有能力值减半。", "PlayerCenter.aspx", "THIS"));
            htError.Add("709", new Error("您的球员已经没有足够的潜力，无需洗点。", "PlayerCenter.aspx", "THIS"));
            htError.Add("708a", new Error("您的球员洗点失败，由于您使用了洗点保险，最大能力值不会减少，但各项技术属性降低6点。", "PlayerCenter.aspx", "THIS"));
            htError.Add("710", new Error("此球员现在不在您队中效力！", "NULL", "NULL"));
            htError.Add("800", new Error("请您在交易价格和交易数量栏中输入正确的半角数字！", "NULL", "NULL"));
            htError.Add("801", new Error("您的资金不足！", "NULL", "NULL"));
            htError.Add("802", new Error("每张订单价格属性须大于0，数量最少10个游戏币！", "NULL", "NULL"));
            htError.Add("803", new Error("订单提交成功！", "WealthMarket.aspx", "THIS"));
            htError.Add("804", new Error("订单提交失败，您只能同时提交一张订单！", "NULL", "NULL"));
            htError.Add("805", new Error("该订单不存在或订单已成交，无法撤销！", "NULL", "NULL"));
            htError.Add("806", new Error("此订单不是您所提交的，您无权撤销该订单！", "NULL", "NULL"));
            htError.Add("807", new Error("订单撤销成功！", "WealthMarket.aspx", "THIS"));
            htError.Add("808", new Error("您的联赛等级过低，无法买入游戏币！", "NULL", "NULL"));
            htError.Add("809", new Error("拍卖价格不得少于1个！", "NULL", "NULL"));
            htError.Add("810", new Error("您的输入超限制（价格1-1000000；数量1-2000）", "NULL", "NULL"));
            htError.Add("811", new Error("您所要参加的竞猜不存在。", "NULL", "NULL"));
            htError.Add("812", new Error("此项竞猜已过报名时间", "NULL", "NULL"));
            htError.Add("813", new Error("您没有足够的资金", "NULL", "NULL"));
            htError.Add("814", new Error("您没有足够的游戏币", "NULL", "NULL"));
            htError.Add("815", new Error("报名成功！", "Guess.aspx", "THIS"));
            htError.Add("816", new Error("请输入半角数字！", "NULL", "NULL"));
            htError.Add("817", new Error("资金数额需要在（10000到1千万之间）！", "NULL", "NULL"));
            htError.Add("818", new Error("游戏币数额需要在（10到1万之间）！", "NULL", "NULL"));
            htError.Add("819", new Error("此竞猜已经停止报名！", "NULL", "NULL"));
            htError.Add("820", new Error("您对本场比赛的下注金额已达1亿，将无法再对本场比赛进行投注！", "NULL", "NULL"));
            htError.Add("821", new Error("您对本场比赛的下注金额已达1亿，将无法再对本场比赛进行投注！", "NULL", "NULL"));
            htError.Add("901", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ManagerSayOK.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>发放球员奖金成功！</div>", "DevisionView.aspx", "THIS"));
            htError.Add("901b", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ManagerSayOK.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>发放球员奖金成功！</div>", "TrainPlayerCenter.aspx", "THIS"));
            htError.Add("901c", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ManagerSayOK.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>发放球员奖金成功！</div>", "VTList.aspx", "THIS"));
            htError.Add("902", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "UseStaff.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>设置助理教练成功！</div>", "DevisionView.aspx", "THIS"));
            htError.Add("902b", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "UseStaff.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>设置助理教练成功！</div>", "VTList.aspx", "THIS"));
            htError.Add("903", new Error("取消助理教练成功，请返回！", "DevisionView.aspx", "THIS"));
            htError.Add("903b", new Error("取消助理教练成功，请返回！", "VTList.aspx", "THIS"));
            htError.Add("904", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ArrageLvl.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>提升职业队战术等级成功！</div>", "DevisionView.aspx", "THIS"));
            htError.Add("904b", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ArrageLvl.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>提升职业队战术等级成功！</div>", "VArrange.aspx", "THIS"));
            htError.Add("904c", new Error("<div style='width:100%;text-align:center;'><img src='" + SessionItem.GetImageURL() + "ArrageLvl.gif' border='0' width='160px' height='85px' style='margin:10px;'></br>提升职业队战术等级成功！</div>", "VTList.aspx", "THIS"));
            htError.Add("905", new Error("服务器连接失败请重试！", "AutoPlayerTrain.aspx", "THIS"));
            htError.Add("906", new Error("训练点领取成功，确定进入游戏！", "Main.aspx", "THIS"));
            htError.Add("907", new Error("您取消领取训练点，确定进入游戏！", "Main.aspx", "THIS"));
            htError.Add("908", new Error("您的设置的战术熟练度有误，请重新设置！", "AfreshArrange.aspx", "THIS"));
            htError.Add("909", new Error("您输入的金币数有误，请重新输入！", "NULL", "NULL"));
            htError.Add("909b", new Error("您剩余金币不足！", "NULL", "NULL"));
            htError.Add("909c", new Error("您的账号不能转赠金币！", "NULL", "NULL"));
            htError.Add("910", new Error("金币赠送成功，请注意查收！", "NULL", "NULL"));
            htError.Add("911", new Error("密码输入不正确请重新输入！", "NULL", "NULL"));
            htError.Add("912", new Error("冠军杯比赛进行中，请在17点以后查看战况！", "NULL", "NULL"));
            htError.Add("912b", new Error("竞猜平盘中，请5分钟后查看！", "NULL", "NULL"));
            htError.Add("913a", new Error("您的球员没有完成全部训练，请再次训练！", "TrainPlayer5.aspx", "THIS"));
            htError.Add("913b", new Error("您的球员没有足够的体力来完成此次训练，请重新训练！", "TrainPlayer5.aspx", "THIS"));
            htError.Add("913c", new Error("您没有雇用此球员！", "NULL", "NULL"));
            htError.Add("913d", new Error("您没有足够训练点！", "NULL", "NULL"));
            htError.Add("914", new Error("球员训练成功！", "TrainPlayerCenter.aspx", "THIS"));
            htError.Add("915", new Error("您好，您已经领取过奖品，将不能重复领取！", "NULL", "NULL"));
            htError.Add("916", new Error("对不起，您的游戏币不足，不能签VIP短合同！", "NULL", "NULL"));
            htError.Add("917", new Error("本次续约成功！3轮之内该球员的工资要求为正常工资的80%！", "PlayerCenter.aspx?Type=5", "THIS"));

            /*-1 对方不是会员 -2 价钱不够, -3 已经持有股票, -4经理不存在, -5市值低于200W -6自己不是会员*/
            htError.Add("SE01", new Error("该经理不是会员，无法购买其股票。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SE02", new Error("你的资金不足。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SE03", new Error("你已经持有该经理的股票，无法再购买。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SE04", new Error("该经理尚未上市。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SE05", new Error("该经理市值低于200万，无法购买。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SE06", new Error("你不是会员，无法购买股票。", "Stock.aspx?Type=COMPANY", "THIS"));
            htError.Add("SS01", new Error("已成功购入该股票，您可以在我的股票列表中查看。", "Stock.aspx?Type=COMPANY", "THIS"));

            htError.Add("XGE01", new Error("您的资金不足，无法下注。", "XGuess.aspx?Type=CLUBLIST", "THIS"));
            htError.Add("XGE02", new Error("该竞猜已经超时，无法下注。", "XGuess.aspx?Type=CLUBLIST", "THIS"));
            htError.Add("XGE03", new Error("该竞猜不存在，无法下注。", "XGuess.aspx?Type=CLUBLIST", "THIS"));
            htError.Add("XGE04", new Error("您已经竞猜过该经理了，对同一个经理只能下注一次。", "XGuess.aspx?Type=CLUBLIST", "THIS"));
            htError.Add("XGS01", new Error("已经成功下注，您可以在我的竞猜中查看所有竞猜历史。", "XGuess.aspx?Type=MYGUESS", "THIS"));

            htError.Add("SVE01", new Error("该球员未能入选明星参选赛。", "StarMatch.aspx?Type=MATCH", "THIS"));
            htError.Add("SVE02", new Error("您不是会员，无法投票。", "StarMatch.aspx?Type=MATCH", "THIS"));
            htError.Add("SVE03", new Error("您已经投过票了。", "StarMatch.aspx?Type=MATCH", "THIS"));
            htError.Add("SVS01", new Error("投票成功，如果该名球员获得前三名的票数，将会获得参加全明星赛的资格。", "StarMatch.aspx?Type=MATCH", "THIS"));

            /* 1 成功 -2 不是新人 -1 只可以买一次*/
            htError.Add("BNS01", new Error("恭喜你，购买新人大礼包成功！", "ManagerTool.aspx", "THIS"));
            htError.Add("BNE01", new Error("您已经购买过新人大礼包了，每个经理只可以购买一次！", "ManagerTool.aspx", "THIS"));
            htError.Add("BNE02", new Error("您注册时间超过7天，无法购买新人大礼包", "ManagerTool.aspx", "THIS"));
           
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
    }
}


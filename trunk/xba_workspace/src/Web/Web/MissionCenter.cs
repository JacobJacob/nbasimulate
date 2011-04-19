namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class MissionCenter : Page
    {
        protected ImageButton btnFinish;
        private int intUserID;
        public string strMissionIntro = "";
        public string strMissionName = "";

        private void btnFinish_Click(object sender, ImageClickEventArgs e)
        {
            Cuter cuter = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID]));
            int index = cuter.GetIndex("0");
            if ((index != -1) && (index == 6))
            {
                cuter.SetCuter(6, "1");
                string strAdvanceOp = cuter.GetCuter();
                BTPAccountManager.UpdateAdvanceOp(this.intUserID, strAdvanceOp);
                this.Session["Advance" + this.intUserID] = strAdvanceOp;
                BTPGameManager.UpdateGameFinish();
                BTPAccountManager.AddMoneyWithFinance(0x1fbd0, this.intUserID, 3, "XBA董事会奖励球队初始资金。");
                base.Response.Write("<script>window.top.location='Main.aspx';</script>");
            }
        }

        private void InitializeComponent()
        {
            this.btnFinish.Click += new ImageClickEventHandler(this.btnFinish_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnFinish.Visible = false;
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                string str = "";
                if (accountRowByUserID != null)
                {
                    str = accountRowByUserID["AdvanceOp"].ToString().Trim();
                }
                Cuter cuter = new Cuter(str);
                switch (cuter.GetIndex("0"))
                {
                    case 0:
                        this.strMissionName = "<strong>任务一：运筹帷幄，决胜千里</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">要想成为一支百战百胜的球队，不能只知道球员的能力，合理的安排战术也是很重要的一环。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">请您尝试更换一套战术。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; font-family:simsun; line-height:150%;\">1、点击<strong><font color='blue'>战术设定 -> 街球战术 -> </font><font color='red'>阵容战术一</font></strong>，更换您的战术。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">10000资金</font> ] </td></tr></table>";
                        break;

                    case 1:
                        this.strMissionName = "<strong>任务二：纸上谈兵，空口无凭</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">没想到这么快您就成功的更换了战术并获得了<font color=\"#FF0000\">10000资金</font>的奖励，您可点击资金旁的<font color=\"#FF0000\">“更新”</font>查看。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">请您参加一场训练比赛，亲身体验一下这套战术的实用价值。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; line-height:150%; font-family:simsun;\">1、点击<strong><font color='blue'>竞技中心 -> 训练中心 -> </font><font color='blue'>报名挑战一支球队</font></strong>。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">20000资金</font> ] </td></tr></table>";
                        break;

                    case 2:
                        this.strMissionName = "<strong>任务三：用人不疑，疑人不用</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">您简直就是天才！我们决定再次奖励您<font color=\"#FF0000\">20000资金</font>！您可点击资金旁的<font color=\"#FF0000\">“更新”</font>查看。现在您已经完成了一场训练赛，您的球员也应该获得了一定的训练点。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">雇佣一名训练员。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; line-height:150%; font-family:simsun;\">1、点击<strong><font color='blue'>球队管理 -> 职员管理 -> 然后在右侧的职员列表中选择一名</font><font color='red'>一级的训练员</font></strong>。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">25000资金</font> ] </td></tr></table>";
                        break;

                    case 3:
                        this.strMissionName = "<strong>任务四：忠臣良将，各司其职</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">没想到这个任务也难不倒您，<font color=\"#FF0000\">25000资金</font>再次赠送给您！您可点击资金旁的<font color=\"#FF0000\">“更新”</font>查看。现在您拥有了一名训练员，是时候验证一下他的能力了。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">训练两个球员。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; line-height:150%; font-family:simsun;\">1、点击<strong><font color='blue'>球队管理 -> 球员训练—选择某球员的训练项目 -> </font><font color='red'>点击训练</font></strong>。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">20000资金</font> ] </td></tr></table>";
                        break;

                    case 4:
                        this.strMissionName = "<strong>任务五：康复理疗，振奋士气</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">您已经很出色完成了以上的任务并获得了<font color=\"#FF0000\">20000资金</font>的奖励！您可点击资金旁的<font color=\"#FF0000\">“更新”</font>查看。可是球员们已经感觉到疲惫和懈怠了。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">到理疗中心去为球员们恢复体力和比赛热情。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; line-height:150%; font-family:simsun;\">1、点击<strong><font color='blue'>球队管理 -> 球员管理 -> 球员姓名 -> 在球员信息栏中</font><font color='red'>点击理疗中心图标<img src=\"Images/llzc.gif\" width='12' height='12'/></font><font color='blue'> -> 点击康复理疗 -> 点击振奋士气</font></strong>。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">30000资金</font> ] </td></tr></table>";
                        break;

                    case 5:
                        this.strMissionName = "<strong>任务五：康复理疗，振奋士气</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%;\">您已经很出色完成了以上的任务并获得了<font color=\"#FF0000\">20000资金</font>的奖励！您可点击资金旁的<font color=\"#FF0000\">“更新”</font>查看。可是球员们已经感觉到疲惫和懈怠了。</td></tr><tr><td align=\"left\"><strong>新任务描述：</strong></td></tr><tr><td align=\"left\" style=\"text-indent:25px;\">到理疗中心去为球员们恢复体力和比赛热情。</td></tr><tr><td align=\"left\"><strong>任务要求：</strong></td></tr><tr><td align=\"left\" style=\"padding-left:30px; line-height:150%; font-family:simsun;\">1、点击<strong><font color='blue'>球队管理 -> 球员管理 -> 球员姓名 -> 在球员信息栏中</font><font color='red'>点击理疗中心图标<img src=\"Images/llzc.gif\" width='12' height='12'/></font><font color='blue'> -> 点击康复理疗 -> 点击振奋士气</font></strong>。<br />2、完成后请<strong><font color='red'>点击首页</font></strong>。</td></tr><tr><td align=\"left\"><strong>任务奖励：</strong> [ <font color=\"#FF0000\">30000资金</font> ] </td></tr></table>";
                        break;

                    case 6:
                        this.strMissionName = "<strong>全部任务完成！</strong>";
                        this.strMissionIntro = "<table width=\"400\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\"><tr><td align=\"left\" style=\"text-indent:25px; height:25px;\"></td></tr><tr><td align=\"left\"></td></tr><tr><td align=\"left\" style=\"text-indent:25px; line-height:150%\">真的没想到这么快您就完成了所有的任务！我从没有见过任何人像您如此的出色过！董事会决定再拨给你<strong><font color='red'>130000</font></strong>的球队资金！我现在可以很放心的把一支职业球队交给您管理了！现在就去<strong><font color='blue'>购买一支职业球队并加入职业联赛</font></strong>吧！我很期待您在联赛中的优异表现！</td></tr><tr><td align=\"left\"></td></tr><tr><td align=\"left\" style=\"padding-left:30px;\"></td></tr><tr><td align=\"left\"></td></tr></table>";
                        this.btnFinish.Visible = true;
                        break;

                    case -1:
                        base.Response.Write("<script>window.top.location='Main.aspx';</script>");
                        break;

                    default:
                        this.strMissionName = "";
                        this.strMissionIntro = "";
                        break;
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


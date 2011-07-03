namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class RookieDetail : Page
    {
        private int intUserID;
        public string strOut;

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
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                string str = onlineRowByUserID["ClubName3"].ToString().Trim();
                int intClubID = (int) onlineRowByUserID["ClubID3"];
                DataRow row2 = BTPArrange3Manager.GetArrange3FirstByClubID(intClubID);
                long longPlayerID = (long) row2["CID"];
                long num3 = (long) row2["FID"];
                long num4 = (long) row2["GID"];
                string str2 = BTPPlayer3Manager.GetPlayer3NameByPlayerID(longPlayerID);
                string str3 = BTPPlayer3Manager.GetPlayer3NameByPlayerID(num3);
                string str4 = BTPPlayer3Manager.GetPlayer3NameByPlayerID(num4);
                this.strOut = "arrOut[0]=\"<u>" + str + "</u> 进攻战术：中锋策应；防守战术：盯人外线<br>XBA魔鬼队 进攻战术：外线投篮；防守战术：盯人外线<br>比赛正式开始...<br>|START|DOING\";arrOut[1]=\"[15:00] <u>" + str + "</u>的球权，<u>" + str2 + "</u> 中线发球。[0:0]<br>|0:0|DOING\";arrOut[2]=\"[11:45] <u>" + str2 + "</u>和<u>" + str3 + "</u>出色的内线配合使XBA魔鬼队的防守破绽百出，打出小高潮。[10:4]<br>|10:4|DOING\";arrOut[3]=\"[07:30] 魔鬼得分在<u>" + str + "</u>严密的外线防守下，仍然连续命中三分球，<u>" + str + "</u>被追平。[14:14]<br>|14:14|DOING\";arrOut[4]=\"[04:12] <u>" + str + "</u>和XBA魔鬼队展开了拉锯战，比分交替上升。[26:26]<br>|26:26|DOING\";arrOut[5]=\"[01:00] 比赛还剩最后一分钟，双方仍然战平，XBA魔鬼队的球权。[30:30]<br>|30:30|DOING\";arrOut[6]=\"[00:40] 魔鬼小前锋得到魔鬼得分的传球后三分线外出手，<u>" + str4 + "</u>虽然封到眼前，球仍然应声入网！[30:33]<br>|30:33|DOING\";arrOut[7]=\"[00:30] <u>" + str3 + "</u>得到<u>" + str4 + "</u>的妙传中投得分。[32:33]<br>|32:33|DOING\";arrOut[8]=\"[00:09] 魔鬼得分出手投篮，但被<u>" + str4 + "</u>干扰。<u>" + str2 + "</u>抓下至关重要的一个篮板球。[32:33]<br>|32:33|DOING\";arrOut[9]=\"[00:01] XBA魔鬼队紧逼<u>" + str + "</u>外围，预防<u>" + str + "</u>投篮，但是<u>" + str2 + "</u>为<u>" + str3 + "</u>巧妙策应，<u>" + str3 + "</u>接<u>" + str2 + "</u>传球上篮得分。[34:33]<br>|34:33|DOING\";arrOut[10]=\"[00:00] 比赛结束，由于战术运用得当，<u>" + str + "</u>险胜强敌XBA魔鬼队！请点击下面按钮进入下一步。<br>|34:33|END\";";
            }
        }
    }
}


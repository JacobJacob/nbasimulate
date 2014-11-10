namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class UnionPolity : Page
    {
        protected ImageButton btnDel;
        private DateTime dtUnionTime;
        private int intClubID5;
        private int intPolityID;
        private int intUnionID;
        private int intUserID;
        protected RadioButton rbA;
        protected RadioButton rbB;
        public string strCreateName = "";
        public string strEndTime = "";
        public string strMsg = "";
        private string strNickName;
        public string strReason = "";
        public string strScore = "";
        public string strScript = "";
        public string strTime = "加载中...";
        private string strType;
        public string strUnionName = "";
        private string strUnionPolity;

        private void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            if (this.strUnionPolity.Substring(0, 1) == "1")
            {
                this.btnDel.Visible = false;
                this.strMsg = "您已经投过票了！";
            }
            else
            {
                bool blnCategory = this.rbA.Checked;
                int num = BTPUnionPolity.DelateMasterReg(this.intUserID, this.intPolityID, blnCategory);
                this.btnDel.Visible = false;
                if (num == 1)
                {
                    if (blnCategory)
                    {
                        base.Response.Redirect("UnionPolity.aspx?Type=DELMASTER&Chose=1");
                    }
                    else
                    {
                        base.Response.Redirect("UnionPolity.aspx?Type=DELMASTER&Chose=2");
                    }
                }
                else
                {
                    switch (num)
                    {
                        case -4:
                            this.strMsg = "此次弹劾已结束";
                            return;

                        case -3:
                            this.strMsg = "您已经投过票了";
                            return;

                        case -2:
                            this.strMsg = "您加入盟时间小于7天，不能时行投票";
                            return;

                        case -1:
                            this.strMsg = "您没有加入此联盟不能进行投票";
                            return;
                    }
                    this.strMsg = "系统错误请重试";
                }
            }
        }

        [AjaxMethod]
        public int GetDelMastStatus()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(SessionItem.CheckLogin(1));
            if (accountRowByUserID == null)
            {
                return 10;
            }
            int intUnionID = (int) accountRowByUserID["UnionID"];
            accountRowByUserID = BTPUnionPolity.GetDelMasterRow(intUnionID);
            if (accountRowByUserID == null)
            {
                return 10;
            }
            int num2 = (int) accountRowByUserID["ScoreA"];
            int num3 = (int) accountRowByUserID["ScoreB"];
            DateTime time = (DateTime) accountRowByUserID["EndTime"];
            if (time > DateTime.Now)
            {
                TimeSpan span = (TimeSpan) (time - DateTime.Now);
                return Convert.ToInt32(span.TotalSeconds);
            }
            if (num2 > num3)
            {
                return -1;
            }
            return -2;
        }

        private void InitializeComponent()
        {
            string str;
            if (((str = this.strType) != null) && (string.IsInterned(str) == "DELMASTER"))
            {
                this.SetDelMaster();
            }
            else
            {
                this.SetDelMaster();
            }
            base.Load += new EventHandler(this.Page_Load);
            this.btnDel.Click += new ImageClickEventHandler(this.btnDel_Click);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(UnionPolity));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                if (accountRowByUserID != null)
                {
                    this.strNickName = accountRowByUserID["NickName"].ToString();
                    this.intClubID5 = (int) accountRowByUserID["ClubID5"];
                    this.intUnionID = (int) accountRowByUserID["UnionID"];
                    this.strUnionPolity = accountRowByUserID["UnionPolity"].ToString().Trim();
                    this.dtUnionTime = (DateTime) accountRowByUserID["UnionTime"];
                    this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetDelMaster()
        {
            string str;
            string str2;
            this.btnDel.ImageUrl = SessionItem.GetImageURL() + "vote.gif";
            DataRow delMasterRow = BTPUnionPolity.GetDelMasterRow(this.intUnionID);
            if (delMasterRow == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=1b");
                return;
            }
            int num = (byte) delMasterRow["Status"];
            int intUserID = (int) delMasterRow["CreateID"];
            int num3 = (int) delMasterRow["ScoreA"];
            int num4 = (int) delMasterRow["ScoreB"];
            int num5 = (int) delMasterRow["PolityID"];
            this.strCreateName = delMasterRow["CreateName"].ToString().Trim();
            this.strReason = delMasterRow["Reason"].ToString().Trim();
            DateTime time = (DateTime) delMasterRow["EndTime"];
            int num6 = num3 / 100;
            if (num6 == 0)
            {
                str = "99";
            }
            else
            {
                str = num6.ToString();
            }
            int num7 = num4 / 100;
            if (num7 == 0)
            {
                str2 = "99";
            }
            else
            {
                str2 = num7.ToString();
            }
            this.strScore = string.Concat(new object[] { "<img id='ScoreA1' src='Images/Score/", str, ".gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/", (num3 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/", num3 % 10, ".gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/", str2, ".gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/", (num4 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/", num4 % 10, ".gif' border='0' width='19' height='28'>" });
            if (this.strUnionPolity.Substring(0, 1) == "1")
            {
                this.btnDel.Visible = false;
                switch (((int) SessionItem.GetRequest("Chose", 0)))
                {
                    case 1:
                        this.strMsg = "您投赞成票成功！";
                        goto Label_0221;

                    case 2:
                        this.strMsg = "您投反对票成功！";
                        goto Label_0221;
                }
                this.strMsg = "您已经投过票了！";
            }
        Label_0221:
            if (this.dtUnionTime > DateTime.Now.AddDays(-7.0))
            {
                this.btnDel.Visible = false;
                this.strMsg = "您加入联盟不够7天不能进行投票！";
            }
            switch (num)
            {
                case 1:
                {
                    this.intPolityID = num5;
                    if (time <= DateTime.Now)
                    {
                        this.btnDel.Visible = false;
                        this.strTime = "已结束";
                        if (num3 > num4)
                        {
                            this.strMsg = AccountItem.GetNickNameInfo(intUserID, this.strCreateName, "Right", 0x12) + "经理成功弹劾了老盟主，新盟主将于明日即位！";
                        }
                        else
                        {
                            this.strMsg = AccountItem.GetNickNameInfo(intUserID, this.strCreateName, "Right", 0x12) + "经理弹劾失败，老盟主继续执政！";
                        }
                        break;
                    }
                    TimeSpan span = (TimeSpan) (time - DateTime.Now);
                    this.strScript = "BeginRunTime(" + Convert.ToInt32(span.TotalSeconds) + ")";
                    break;
                }
                case 2:
                    this.btnDel.Visible = false;
                    this.strTime = "已结束";
                    if (num3 <= num4)
                    {
                        this.strMsg = AccountItem.GetNickNameInfo(intUserID, this.strCreateName, "Right", 0x12) + "经理弹劾失败，老盟主继续执政！";
                        return;
                    }
                    this.strMsg = AccountItem.GetNickNameInfo(intUserID, this.strCreateName, "Right", 0x12) + "经理成功弹劾了老盟主，接任盟主！";
                    return;

                default:
                    return;
            }
            this.strCreateName = AccountItem.GetNickNameInfo(intUserID, this.strCreateName, "Right", 0x10);
            delMasterRow = BTPUnionManager.GetUnionRowByID(this.intUnionID);
            if (delMasterRow != null)
            {
                this.strUnionName = delMasterRow["Name"].ToString().Trim();
            }
        }
    }
}


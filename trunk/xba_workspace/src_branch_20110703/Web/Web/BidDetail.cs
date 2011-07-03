namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class BidDetail : Page
    {
        private DataRow drPlayer;
        public int intBidCount;
        public int intBidPrice;
        private int intType;
        private long longPlayerID;
        public string strBidder;
        public string strBidPrice;
        public string strBidStatus;
        public string strCategory;
        public string strEndBidTime;
        public string strFace;
        public string strInfo;
        public string strNumber;
        public string strOwner;
        public string strShirt;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
            this.longPlayerID = (long) SessionItem.GetRequest("PlayerID", 3);
            this.intType = (int) SessionItem.GetRequest("Type", 0);
            if (this.intType == 3)
            {
                this.drPlayer = BTPPlayer3Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            else
            {
                this.drPlayer = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
            }
            if (this.drPlayer == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else if (((byte) this.drPlayer["Category"]) == 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                this.SetInfo();
                this.SetBidInfo();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetBidInfo()
        {
            this.strBidder = "-- --";
            this.strBidStatus = "-- --";
            this.strEndBidTime = "-- --";
            this.intBidCount = 0;
            this.strOwner = "-- --";
            this.intBidPrice = 0;
            switch (((byte) this.drPlayer["BidStatus"]))
            {
                case 0:
                    this.strBidStatus = "结算中";
                    break;

                case 1:
                    this.strBidStatus = "成交";
                    break;

                default:
                    this.strBidStatus = "不成交";
                    break;
            }
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID((int) this.drPlayer["BidderID"]);
            if (accountRowByUserID != null)
            {
                this.strBidder = string.Concat(new object[] { "<a href='ShowClub.aspx?Type=3&UserID=", (int) accountRowByUserID["UserID"], "'>", accountRowByUserID["NickName"].ToString().Trim(), "</a>" });
            }
            DataRow accountRowByClubID = BTPAccountManager.GetAccountRowByClubID((int) this.drPlayer["ClubID"]);
            if (accountRowByClubID != null)
            {
                this.strOwner = string.Concat(new object[] { "<a href='ShowClub.aspx?Type=3&UserID=", (int) accountRowByClubID["UserID"], "'>", accountRowByClubID["NickName"].ToString().Trim(), "</a>" });
            }
            DateTime datIn = (DateTime) this.drPlayer["EndBidTime"];
            this.strEndBidTime = StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss");
            if (this.intType == 3)
            {
                if (((byte) this.drPlayer["Category"]) == 4)
                {
                    if (((long) this.drPlayer["QuickBuy"]) > 0L)
                    {
                        this.intBidCount = 1;
                    }
                    else
                    {
                        this.intBidCount = BTPBidderManager.GetChooseBidCount(this.longPlayerID);
                    }
                }
                else
                {
                    this.intBidCount = (int) this.drPlayer["BidCount"];
                }
            }
            else
            {
                this.intBidCount = BTPBidderManager.GetBidCount(this.longPlayerID);
            }
            if (this.intType == 3)
            {
                if (((long) this.drPlayer["QuickBuy"]) > 0L)
                {
                    this.strBidPrice = this.drPlayer["BidPrice"].ToString().Trim() + " (一口价)";
                }
                else
                {
                    this.strBidPrice = this.drPlayer["BidPrice"].ToString().Trim();
                }
            }
            else
            {
                this.strBidPrice = this.drPlayer["BidPrice"].ToString().Trim();
            }
        }

        private void SetInfo()
        {
            int num2;
            object strInfo;
            string str = this.drPlayer["Name"].ToString().Trim();
            this.strFace = this.drPlayer["Face"].ToString().Trim();
            int intClubID = (int) this.drPlayer["ClubID"];
            DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(intClubID);
            if (clubRowByClubID == null)
            {
                num2 = 1;
            }
            else
            {
                num2 = (byte) clubRowByClubID["Shirt"];
            }
            int num3 = (byte) this.drPlayer["Number"];
            this.strShirt = (0x4e20 + num2) + "";
            if (num2 > 15)
            {
                this.strNumber = (0x526c + num3) + "";
            }
            else
            {
                this.strNumber = (0x5208 + num3) + "";
            }
            int num1 = (int) this.drPlayer["PV"];
            int intPosition = (byte) this.drPlayer["Pos"];
            int num5 = (byte) this.drPlayer["Age"];
            int num6 = (byte) this.drPlayer["PlayedYear"];
            int num7 = (byte) this.drPlayer["Height"];
            int num8 = (byte) this.drPlayer["Weight"];
            int intPower = (byte) this.drPlayer["Power"];
            float num10 = ((float) ((int) this.drPlayer["Ability"])) / 10f;
            int intStatus = (byte) this.drPlayer["Status"];
            int intSuspend = (byte) this.drPlayer["Suspend"];
            string strEvent = this.drPlayer["Event"].ToString();
            if (intSuspend <= 0)
            {
                strEvent = "";
            }
            else
            {
                strInfo = strEvent;
                strEvent = string.Concat(new object[] { strInfo, "&nbsp;停赛：", intSuspend, "轮。" });
            }
            strInfo = this.strInfo;
            this.strInfo = string.Concat(new object[] { strInfo, "<table width='201' border='0' cellspacing='0' cellpadding='0'><tr><td width='92' valign='top'><img id='imgCharactor' src='", SessionItem.GetImageURL(), "Player/Charactor/NewPlayer.png' width='90' height='130'></td><td width='109'><table width='109' border='0' cellspacing='0' cellpadding='0'><tr><td colspan=2 height='25' align='center'>", PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend), "&nbsp;<strong>", str, "</strong></td></tr><tr><td height='18' width='37' align='center'>年龄</td><td width='78'>", num5, "&nbsp;<a title='球龄' style='CURSOR: hand'>[", num6, "]</a></td></tr>" });
            strInfo = this.strInfo;
            this.strInfo = string.Concat(new object[] { strInfo, "<td height='18' width='37' align='center'>位置</td><td width='78'><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</td></tr><tr><td height='18' align='center'>身高</td><td>", num7, "CM</td></tr><tr><td height='18' align='center'>体重</td><td>", num8, "KG</td></tr><tr><td height='18' align='center'>体力</td><td>", PlayerItem.GetPowerColor(intPower), "</td></tr><tr><td height='18' align='center'>综合</td><td>", num10, "</td></tr></table></td></tr></table>" });
        }
    }
}


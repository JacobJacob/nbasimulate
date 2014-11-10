namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class VOnlineReport : Page
    {
        public int intA;
        public int intB;
        public int intTag;
        public int intType;
        public string strInfo;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intType = SessionItem.GetRequest("Type", 0);
            this.intTag = SessionItem.GetRequest("Tag", 0);
            this.intA = SessionItem.GetRequest("A", 0);
            this.intB = SessionItem.GetRequest("B", 0);
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            int num = 0;
            if (parameterRow != null)
            {
                num = (int) parameterRow["IsFreeReport"];
            }
            if (num != 1)
            {
                base.Response.Redirect("Report.aspx?Parameter=201");
            }
            else
            {
                if (DateTime.Now.Hour != 9)
                {
                    base.Response.Redirect("Report.aspx?Parameter=1001b");
                }
                int num2 = BTPGameManager.GetTurn() - 1;
                int num3 = -1;
                DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(this.intTag);
                if (devMRowByDevMatchID != null)
                {
                    num3 = (int) devMRowByDevMatchID["Round"];
                }
                if (num2 != num3)
                {
                    base.Response.Redirect("Report.aspx?Parameter=1001c");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string str;
            string str2;
            string str3;
            string str4;
            DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intA);
            DataRow row2 = BTPClubManager.GetClubRowByClubID(this.intB);
            if (clubRowByClubID != null)
            {
                str = clubRowByClubID["ClubName"].ToString().Trim();
                str2 = clubRowByClubID["ClubLogo"].ToString().Trim();
            }
            else
            {
                str = "轮空";
                str2 = Config.GetDomain() + "Images/Club/Logo/0.gif";
            }
            if (row2 != null)
            {
                str3 = row2["ClubName"].ToString().Trim();
                str4 = row2["ClubLogo"].ToString().Trim();
            }
            else
            {
                str3 = "轮空";
                str4 = Config.GetDomain() + "Images/Club/Logo/0.gif";
            }
            string domain = Config.GetDomain();
            this.strInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0' style=\"background:url(images/teambg.gif) repeat-x; margin:1px 0;\"><tr align='center'><td width='25%' height=\"60\"><strong><font color='#660066'>" + str + "</font></strong></td><td width='10%'><img src='" + domain + str2 + "' width='46' height='46'></td><td width='30%'><div id='divScore'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/Bar.gif' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/Bar.gif' width='19' height='28'></div></td><td width='10%'><img src='" + domain + str4 + "' width='46' height='46'></td><td width='25%'><strong><font color='#660066'>" + str3 + "</font></strong></td></tr></table>";
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TOnlineReport : Page
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
                BTPGameManager.GetTurn();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string str = "东部明星队";
            string str2 = "Images/Club/Logo/10.gif";
            string str3 = "西部明星队";
            string str4 = "Images/Club/Logo/11.gif";
            string domain = Config.GetDomain();
            this.strInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0' style=\"background:url(images/teambg.gif) repeat-x; margin:1px 0;\"><tr align='center'><td width='25%' height=\"60\"><strong><font color='#660066'>" + str + "</font></strong></td><td width='10%'><img src='" + domain + str2 + "' width='46' height='46'></td><td width='30%'><div id='divScore'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/Bar.gif' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/99.gif' width='19' height='28'><img src='Images/Score/Bar.gif' width='19' height='28'></div></td><td width='10%'><img src='" + domain + str4 + "' width='46' height='46'></td><td width='25%'><strong><font color='#660066'>" + str3 + "</font></strong></td></tr></table>";
        }
    }
}


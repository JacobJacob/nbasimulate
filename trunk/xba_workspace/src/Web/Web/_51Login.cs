namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class _51Login : Page
    {
        public string strAD1;
        public string strAD2;
        public string strBarList;
        public string strCento;
        public string strForumWealthList;
        public string strIndexEnd;
        public string strIndexHead;
        public string strLeagueList;
        public string strNewNick;
        public string strNewsList;
        public string strNewTopic;
        public string strPlayerPhotoList;
        public string strRMList;
        public string strRMScoreList;
        public string strRocketmanXBAList;
        public string strStreetBallList;
        public string strXBADailyList;

        private void GetNewsList()
        {
            this.strNewsList = "";
            DataTable tableByBoardID = ROOTTopicManager.GetTableByBoardID("001001", 10);
            if (tableByBoardID != null)
            {
                this.strNewsList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='10' width='100%'></td></tr>";
                foreach (DataRow row in tableByBoardID.Rows)
                {
                    string str = row["BoardID"].ToString().Trim();
                    int num = (int) row["TopicID"];
                    string strIn = row["Title"].ToString();
                    string str3 = "<img src='" + SessionItem.GetImageURL() + "/icon_02.gif' width='4' height='8'>";
                    object strNewsList = this.strNewsList;
                    this.strNewsList = string.Concat(new object[] { strNewsList, "<tr><td height='19' style='padding-left:5px;word-break:break-all;'>", str3, "&nbsp;<a href='", DBLogin.URLString(-1), "Topic.aspx?BoardID=", str, "&TopicID=", num, "&Page=1' target='_blank'><font color='#333333'>", StringItem.GetShortString(strIn, 0x20), "</font></a></td></tr>" });
                }
                this.strNewsList = this.strNewsList + "</table>";
            }
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            DataRow gameADRow = BTPGameADManager.GetGameADRow();
            this.strAD1 = gameADRow["LoginAD01"].ToString().Trim();
            this.strAD2 = gameADRow["LoginAD02"].ToString().Trim();
            this.strIndexHead = BoardItem.GetIndexHead(-1, "", "");
            this.strIndexEnd = BoardItem.GetIndexEnd();
            this.strNewsList = "";
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


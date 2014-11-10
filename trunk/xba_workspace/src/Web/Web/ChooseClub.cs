namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ChooseClub : Page
    {
        private int intUserID;
        public StringBuilder sbList = new StringBuilder("");
        private string strNickName;

        public void GetList()
        {
            foreach (DataRow row in BTPAccountManager.GetChooseClub(this.intUserID).Rows)
            {
                int intUserID = (int) row["UserID"];
                string strNickName = row["ClubName"].ToString().Trim();
                int num2 = (byte) row["Levels"];
                bool blnSex = (bool) row["Sex"];
                string str2 = row["ClubLogo"].ToString().Trim();
                string dev = DevCalculator.GetDev(row["DevCode"].ToString().Trim());
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width=60 align=center valign=middle ><img src='" + str2 + "' width=46 height=46></td>");
                this.sbList.Append("<td width=200 height=50 align=center>" + AccountItem.GetNickNameInfoA(intUserID, strNickName, "Right", blnSex) + "</td>");
                this.sbList.Append("<td width=70 align=center><a href='Temp_Right.aspx?Type=MATCHLEV' target=Right>" + dev + "</a></td>");
                this.sbList.Append("<td width=70 align=center><a href='Temp_Right.aspx?Type=STREENLEV' target=Right>" + num2 + "</a></td>");
                this.sbList.Append("<td width=136 align=center><a href='ShowClub.aspx?Type=5&UserID=" + intUserID + "' target=\"Right\"><font color='blue'>详情</font></a>");
                this.sbList.Append(" | <a href='SecretaryPage.aspx?Type=CHOOSECLUB&UserID=" + intUserID + "'>认领</A></td></tr>");
            }
        }

        private void InitializeComponent()
        {
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
                this.intUserID = (int) onlineRowByUserID["UserID"];
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.GetList();
        }
    }
}


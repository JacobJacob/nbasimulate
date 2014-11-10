namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieTrainPlayerCenter : Page
    {
        protected ImageButton btnOK;
        protected HtmlInputHidden hidTrains;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        public int intType;
        private int intUserID;
        public long longFirstPlayerID;
        public string strDevMsg;
        public string strList;
        public string strMainScript;
        public string strPageIntro;
        public string strScript;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (((int) this.Session["TrainNew"]) >= 2)
            {
                int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 3);
                if (intRookieOpIndex != 3)
                {
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                else
                {
                    base.Response.Write("<script>window.top.Main.location=\"RookieMain_P.aspx?Type=DARE\";</script>");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3newb");
            }
        }

        private string GetOldPlayerList(DataTable dt)
        {
            if (dt != null)
            {
                int num = 0;
                foreach (DataRow row in dt.Rows)
                {
                    long num2 = (long) row["PlayerID"];
                    if (num == 0)
                    {
                        this.longFirstPlayerID = num2;
                    }
                    num++;
                }
            }
            return "";
        }

        private string GetPlayerList(DataTable dt)
        {
            string str = "";
            if (dt == null)
            {
                return "";
            }
            int num = 0;
            foreach (DataRow row in dt.Rows)
            {
                string str2;
                string str3;
                long longPlayerID = (long) row["PlayerID"];
                if (num == 0)
                {
                    this.longFirstPlayerID = longPlayerID;
                }
                string strName = row["Name"].ToString();
                int num3 = (byte) row["Number"];
                int num4 = (byte) row["Age"];
                int intPosition = (byte) row["Pos"];
                int intPower = (byte) row["Power"];
                int num7 = (byte) row["Height"];
                int num8 = (byte) row["Weight"];
                float single1 = ((float) ((int) row["Ability"])) / 10f;
                int intStatus = (byte) row["Status"];
                byte num1 = (byte) row["Category"];
                float single2 = ((float) ((int) row["Speed"])) / 10f;
                float single3 = ((float) ((int) row["Jump"])) / 10f;
                float single4 = ((float) ((int) row["Strength"])) / 10f;
                float single5 = ((float) ((int) row["Stamina"])) / 10f;
                float single6 = ((float) ((int) row["Shot"])) / 10f;
                float single7 = ((float) ((int) row["Point3"])) / 10f;
                float single8 = ((float) ((int) row["Dribble"])) / 10f;
                float single9 = ((float) ((int) row["Pass"])) / 10f;
                float single10 = ((float) ((int) row["Rebound"])) / 10f;
                float single11 = ((float) ((int) row["Steal"])) / 10f;
                float single12 = ((float) ((int) row["Block"])) / 10f;
                float single13 = ((float) ((int) row["Attack"])) / 10f;
                float single14 = ((float) ((int) row["Defense"])) / 10f;
                float single15 = ((float) ((int) row["Team"])) / 10f;
                int num10 = (int) row["TrainPoint"];
                int intTrainType = (byte) row["TrainType"];
                string strEvent = row["Event"].ToString();
                int intAbility = (int) row["Ability"];
                int intSuspend = (byte) row["Suspend"];
                string str6 = "<td>" + num10 + "</td>";
                if (intStatus == 1)
                {
                    str3 = "<td><a href='#' onclick='ChangeTrain(" + longPlayerID + ")'>训练</a></td>";
                }
                else
                {
                    str3 = "<td align='center'>--</td>";
                }
                if (intStatus == 1)
                {
                    str2 = PlayerItem.GetTrainSelect(intTrainType, longPlayerID, this.intType);
                }
                else
                {
                    str2 = PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend);
                }
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, this.intType, 1, 1), "</td><td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num3, ".gif\"></td><td>", num4, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</td><td>", PlayerItem.GetPowerColor(intPower), "</td><td>", 
                    num7, "</td><td>", num8, "</td><td>", PlayerItem.GetAbilityColor(intAbility), "</td>", str6, "<td>", str2, "</td>", str3, "</tr>"
                 });
                str = str + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='10'></td></tr>";
                num++;
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                this.strMainScript = "";
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.intType = 3;
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "new/b2.gif";
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex != 3)
                {
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                if (this.Session["TrainNew"] == null)
                {
                    this.Session["TrainNew"] = 0;
                }
                if (!base.IsPostBack && (this.intType == 5))
                {
                    DataTable pTrainTableByClubID = BTPPlayer5Manager.GetPTrainTableByClubID(this.intClubID5);
                    string str = "";
                    for (int i = 0; i < pTrainTableByClubID.Rows.Count; i++)
                    {
                        onlineRowByUserID = pTrainTableByClubID.Rows[i];
                        string str2 = onlineRowByUserID["PlayerID"].ToString();
                        string str3 = onlineRowByUserID["TrainType"].ToString();
                        string str4 = str;
                        str = str4 + "sltTrain" + str2 + ":" + str3;
                        if (i < (pTrainTableByClubID.Rows.Count - 1))
                        {
                            str = str + "|";
                        }
                    }
                    this.hidTrains.Value = str;
                }
                if (((this.intCategory == 1) && (this.intType != 3)) && (this.intType != 6))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetScript();
            this.SetList();
        }

        private void SetList()
        {
            this.strList = "";
            DataTable playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
            this.strList = this.strList + "<tr class='BarHead'><td height='25' width='100' align='left' style='padding-left:3px'>姓名</td><td width='40'>号码</td><td width='40'>年龄</td><td width='40'>位置</td><td width='40'>体力</td><td width='40'>身高</td><td width='40'>体重</td><td width='40'>综合</td><td width='60'>训练点</td><td width='50'>项目</td><td width='40'>操作</td></tr>";
            this.strList = this.strList + this.GetPlayerList(playerTableByClubID);
        }

        public void SetScript()
        {
            this.strScript = "";
            if (this.intType == 3)
            {
                this.strScript = this.strScript + "function ChangeTrain(longPlayerID){var strTrainType=document.getElementById('sltTrain'+longPlayerID).value;window.top.Main.Right.location='ShowPlayer.aspx?Type=3&Kind=1&Check=1&PlayerID='+longPlayerID;window.location='SecretaryPage.aspx?Type=NEWTRAINPLAYER3&PlayerID='+longPlayerID+'&TrainType='+strTrainType;}";
            }
        }
    }
}


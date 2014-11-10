namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class AssignClub : Page
    {
        protected Button btnOK;
        protected HtmlInputHidden hihPlayerIDs;
        private int intCategory;
        private int intClubID3;
        private int intUserID;
        public string strIntro;
        public string strList;
        private string strNickName;
        private string strPassword;
        private string strUserName;

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] strArray = this.hihPlayerIDs.Value.Split(new char[] { '|' });
            if (BTPClubManager.GetClub3PlayerByClubID(this.intClubID3) > 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=16");
            }
            else if (strArray.Length > 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=15");
            }
            else if (strArray.Length < 5)
            {
                base.Response.Redirect("Report.aspx?Parameter=151");
            }
            else if (BTPPlayer3Manager.SetAssignPlayerInClub(strArray[0], strArray[1], strArray[2], strArray[3], (int) Global.drParameter["RegValueMax"], this.intClubID3, this.intUserID))
            {
                long[] numArray = new long[4];
                DataTable clubPlayerByHeight = BTPPlayer3Manager.GetClubPlayerByHeight(this.intClubID3);
                int index = 0;
                foreach (DataRow row in clubPlayerByHeight.Rows)
                {
                    numArray[index] = (long) row["PlayerID"];
                    index++;
                }
                int[] numArray2 = new int[] { 1, 2, 3, 4 };
                for (int i = 0; i < 4; i++)
                {
                    numArray2[i] = RandomItem.rnd.Next(i * 12, (i + 1) * 12);
                }
                for (int j = 0; j < 4; j++)
                {
                    BTPPlayer3Manager.ArrangeNumber(numArray[j], numArray2[j]);
                }
                BTPArrange3Manager.AddArrange(this.intClubID3, numArray[0], numArray[1], numArray[2], 1, 1, 100, 100);
                int num4 = (int) BTPArrange3Manager.GetArrByCategory(this.intClubID3, 1)["Arrange3ID"];
                BTPClubManager.AssignArrange(this.intClubID3, num4);
                BTPArrangeLvlManager.AddArrangeLvl(this.intUserID);
                BTPAccountManager.SetAccountCategory(this.intUserID, 1);
                BTPToolLinkManager.AddTool(this.intUserID, 1, 6, 40, 20);
                string strUserName = StringItem.MD5Encrypt(this.strUserName, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(this.strPassword, Global.strMD5Key);
                SessionItem.SetSelfLogin(strUserName, strPassword, false);
                base.Response.Redirect("Report.aspx?Parameter=16");
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=15");
            }
        }

        private void GetList()
        {
            this.strList = "";
            DataTable table = BTPPlayer3Manager.GetSelectPlayer3Table(this.intClubID3);
            if (table == null)
            {
                BTPPlayer3Manager.CreateSelectPlayer3(this.intClubID3);
                table = BTPPlayer3Manager.GetSelectPlayer3Table(this.intClubID3);
            }
            foreach (DataRow row in table.Rows)
            {
                long longPlayerID = (long) row["PlayerID"];
                string strName = row["Name"].ToString();
                int num2 = (byte) row["Number"];
                int num3 = (byte) row["Age"];
                int intPosition = (byte) row["Pos"];
                int num5 = (byte) row["Height"];
                int num6 = (byte) row["Weight"];
                int intAbility = (int) row["Ability"];
                float single1 = ((float) ((int) row["Ability"])) / 10f;
                int num8 = (int) row["Value"];
                object strList = this.strList;
                this.strList = string.Concat(new object[] { 
                    strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 3, 0, 1), "</td><td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\"</td><td>", num3, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</a></td><td>", num5, "</td><td>", 
                    num6, "</td><td>", PlayerItem.GetAbilityColor(intAbility), "</td><td><font color='#660066'>", num8, "</font></td><td><input type='checkbox' id='cb", longPlayerID, "' name='cb", longPlayerID, "' onclick='CBChange(this,this.checked)' value='", longPlayerID, "'><input type='hidden' id='PlayerID_", longPlayerID, "' name='PlayerID_", longPlayerID, "' value='", 
                    num8, "'></td></tr>"
                 });
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
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
                this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.intCategory = (byte) accountRowByUserID["Category"];
                if (this.intCategory != 3)
                {
                    base.Response.Redirect("Report.aspx?Parameter=14");
                }
                else
                {
                    this.strIntro = "请您选择4名球员，球员总价值必须在" + Global.drParameter["RegValueMax"].ToString() + "之内。";
                    this.GetList();
                }
            }
        }
    }
}


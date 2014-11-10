namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieAssignDevClub : Page
    {
        protected ImageButton btnOK;
        protected HtmlInputHidden Hidden1;
        protected HtmlInputHidden hihPlayerIDs;
        private int intCategory;
        private int intClubID5;
        private int intRookieOpIndex;
        private int intUserID;
        private string strClubName;
        public string strIntro;
        public string strList;
        private string strNickName;
        private string strPassword;
        private string strUserName;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string[] strArray = this.hihPlayerIDs.Value.Split(new char[] { '|' });
            if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) > 0)
            {
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex != 2)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
            }
            else if (strArray.Length > 8)
            {
                base.Response.Redirect("Report.aspx?Parameter=15a");
            }
            else if (strArray.Length < 8)
            {
                base.Response.Redirect("Report.aspx?Parameter=151");
            }
            else if (BTPPlayer5Manager.SetAssignDevPlayerInClub(strArray[0], strArray[1], strArray[2], strArray[3], strArray[4], strArray[5], strArray[6], strArray[7], (int) Global.drParameter["RegDevPlayerMax"], this.intClubID5, this.intUserID))
            {
                long[] numArray = new long[8];
                DataTable table = BTPPlayer5Manager.GetClub5PlayerByHeight(this.intClubID5);
                int index = 0;
                foreach (DataRow row in table.Rows)
                {
                    numArray[index] = (long) row["PlayerID"];
                    index++;
                }
                BTPArrange5Manager.AddArrange(this.intClubID5, numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], 1, 1, 100, 100);
                int num3 = (int) BTPArrange5Manager.GetArrByCategory(this.intClubID5, 1)["Arrange5ID"];
                BTPClubManager.AssignArrange5(this.intClubID5, num3);
                BTPArrangeLvlManager.AddArrange5Lvl(this.intUserID);
                BTPAccountManager.SetAccountCategory(this.intUserID, 10);
                DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 10);
                string strUserName = StringItem.MD5Encrypt(this.strUserName, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(this.strPassword, Global.strMD5Key);
                SessionItem.SetSelfLogin(strUserName, strPassword, false);
                int num4 = AccountItem.SetRookieOp(this.intUserID, 2);
                if (num4 != 2)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(num4) + "\";</script>");
                }
                else
                {
                    base.Response.Write("<script>window.top.Main.location=\"RookieMain_I.aspx?Type=ENDDEVCREATE\"</script>");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=15a");
            }
        }

        private void GetList()
        {
            this.strList = "";
            if (BTPPlayer5Manager.GetPlayerTableByClubID(this.intClubID5) != null)
            {
                if (this.intRookieOpIndex != 2)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(this.intRookieOpIndex) + "\";</script>");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
            }
            else
            {
                DataTable table = BTPPlayer5Manager.GetSelectPlayer5Table();
                if (table != null)
                {
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
                        long num8 = Convert.ToInt64(row["BidPrice"]);
                        object strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", num2, "</td><td align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 10, 0, 1), "</td><td>", num3, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</a></td><td>", num5, "</td><td>", num6, "</td><td>", 
                            PlayerItem.GetAbilityColor(intAbility), "</td><td><font color='#660066'>", num8, "</font></td><td><input type='checkbox' id='cb", longPlayerID, "' name='cb", longPlayerID, "' onclick='CBChange(this,this.checked)' value='", longPlayerID, "'><input type='hidden' id='PlayerID_", longPlayerID, "' name='PlayerID_", longPlayerID, "' value='", num8, "'></td></tr>"
                         });
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                this.intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (this.intRookieOpIndex != 2)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(this.intRookieOpIndex) + "\";</script>");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
            this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
            this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.strClubName = onlineRowByUserID["ClubName5"].ToString().Trim();
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intCategory = (byte) accountRowByUserID["Category"];
            if (this.intCategory != 3)
            {
                base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(this.intRookieOpIndex) + "\";</script>");
            }
            this.strIntro = "请您选择8名球员。";
            if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) > 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=16");
            }
            this.GetList();
        }
    }
}


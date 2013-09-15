namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieAssignClub : Page
    {
        protected ImageButton btnOK;
        protected Button Button1;
        protected HtmlInputHidden Hidden1;
        protected HtmlInputHidden hihPlayerIDs;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
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
                int num5 = (int) BTPArrange3Manager.GetArrByCategory(this.intClubID3, 1)["Arrange3ID"];
                BTPClubManager.AssignArrange(this.intClubID3, num5);
                BTPArrangeLvlManager.AddArrangeLvl(this.intUserID);
                BTPAccountManager.SetAccountCategory(this.intUserID, 2);
                BTPToolLinkManager.AddTool(this.intUserID, 1, 6, 40, 20);
                BTPGameManager.UpdateGameBegin();
                string strContent = "　尊敬的" + this.strNickName + "经理：" + this.strClubName + "俱乐部董事会宣布，正式任命您为俱乐部的总经理兼主教练！希望您能够不负众望，为球队带来新赛季的冠军奖杯！对于您的经营和管理，董事会也提出了许多有用的意见，相信会给您带来很大的帮助。<a target=\"_blank\" href=\"directorate.html\">点此阅读董事会的来信。</a>";
                BTPMessageManager.AddMessage(this.intUserID, 2, 0, "秘书报告", strContent);
                string strUserName = StringItem.MD5Encrypt(this.strUserName, Global.strMD5Key);
                string strPassword = StringItem.MD5Encrypt(this.strPassword, Global.strMD5Key);

                BTPMessageManager.AddMessage(this.intUserID, 2, 0, "秘书报告", "尊敬的经理您好，欢迎加入无道篮球，如果在游戏中有任何问题，请加入无道篮球经理新手群进行咨询,群号码:180011696,如果想与更多老玩家交流，请加入无道篮球大群，群号码:4269274");

                ///创建填充球员
                BTPPlayer5Manager.CreatePlayer5ForReg(this.intClubID5, 634, 709, 24, 1);
                BTPPlayer5Manager.CreatePlayer5ForReg(this.intClubID5, 675, 723, 23, 2);
                BTPPlayer5Manager.CreatePlayer5ForReg(this.intClubID5, 654, 701, 20, 3);
                BTPPlayer5Manager.CreatePlayer5ForReg(this.intClubID5, 681, 706, 23, 4);
                BTPPlayer5Manager.CreatePlayer5ForReg(this.intClubID5, 673, 732, 25, 0);
                BTPMessageManager.AddMessage(this.intUserID, 2, 0, "秘书报告", "尊敬的经理您好，董事会对你的能力表示肯定，为了打造一支冠军球队，我们已经从市场上为你买入了五名有实力的球员，他们已经到队，请将他们加入阵容中，检验一下他们的实力吧!");

                SessionItem.SetSelfLogin(strUserName, strPassword, false);
                int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 4);
                if (intRookieOpIndex != 4)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                else
                {
                    base.Response.Write("<script>window.top.Main.location=\"RookieMain_I.aspx?Type=END\"</script>");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=15");
            }
        }

        private void GetList()
        {
            this.strList = "";
            if (BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3) != null)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                DataTable table2 = BTPPlayer3Manager.GetSelectPlayer3Table();
                if (table2 != null)
                {
                    foreach (DataRow row in table2.Rows)
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
                        long num7 = Convert.ToInt64(row["BidPrice"]);
                        object strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", num2, "</td><td align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 10, 0, 1), "</td><td>", num3, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", PlayerItem.GetPlayerEngPosition(intPosition), "</a></td><td>", num5, "</td><td>", num6, "</td><td>", 
                            PlayerItem.GetAbilityColor(intAbility), "</td><td><font color='#660066'>", num7, "</font></td><td><input type='checkbox' id='cb", longPlayerID, "' name='cb", longPlayerID, "' onclick='CBChange(this,this.checked)' value='", longPlayerID, "'><input type='hidden' id='PlayerID_", longPlayerID, "' name='PlayerID_", longPlayerID, "' value='", num7, "'></td></tr>"
                         });
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex == -1)
                {
                    base.Response.Write("<script>window.top.location=\"Main.aspx\";</script>");
                }
                else if (intRookieOpIndex != 4)
                {
                    base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                else
                {
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                    this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                    this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
                    this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                    this.intClubID5 = (int)onlineRowByUserID["ClubID5"];
                    this.strClubName = onlineRowByUserID["ClubName5"].ToString().Trim();
                    DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                    this.intCategory = (byte) accountRowByUserID["Category"];
                    if (this.intCategory != 10)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                    this.strIntro = "请您选择4名球员。";
                    if (BTPClubManager.GetClub3PlayerByClubID(this.intClubID3) > 0)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=16");
                    }
                    this.GetList();
                }
            }
        }
    }
}


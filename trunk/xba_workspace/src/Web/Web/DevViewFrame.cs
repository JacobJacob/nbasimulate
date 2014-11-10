namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class DevViewFrame : Page
    {
        private int intCategory;
        private int intClubID;
        public int intTurn;
        private int intUserID;
        public string strDevCode;
        public string strDownList;
        public string strIntro;
        public string strList = "";
        public string strMainScript;
        private string strNickName;
        public string strPageIntro;
        public string strUPList;
        public string strViewScript = "";

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private bool IsMatchH(int intClubHID)
        {
            return (intClubHID == this.intClubID);
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
                DataRow gameRow = BTPGameManager.GetGameRow();
                this.intTurn = (int) gameRow["Turn"];
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                SessionItem.CheckCanUseAfterUpdate(5);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strDevCode = SessionItem.GetRequest("Devision", 1);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetView();
        }

        private void SetView()
        {
            string str = "";
            string str2 = "";
            DataTable devMatchTableByClubID = BTPDevMatchManager.GetDevMatchTableByClubID(this.intClubID);
            if (devMatchTableByClubID == null)
            {
                this.strUPList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in devMatchTableByClubID.Rows)
                {
                    string str3;
                    string str4;
                    string str5;
                    int num = (int) row["Round"];
                    int intDevMatchID = (int) row["DevMatchID"];
                    int intClubHID = (int) row["ClubHID"];
                    int intClubID = (int) row["ClubAID"];
                    int num5 = (int) row["ClubHScore"];
                    int num6 = (int) row["ClubAScore"];
                    num = Convert.ToInt32(row["Round"]);
                    row["RepURL"].ToString().Trim();
                    row["StasURL"].ToString().Trim();
                    bool flag1 = (bool) row["UseStaffH"];
                    bool flag2 = (bool) row["UseStaffA"];
                    byte num1 = (byte) row["MangerSayH"];
                    byte num7 = (byte) row["MangerSayA"];
                    bool flag3 = (bool) row["AddArrangeLvlH"];
                    bool flag4 = (bool) row["AddArrangeLvlA"];
                    if (((intClubHID != 0) && (intClubID != 0)) && ((num5 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", intDevMatchID, "&A=", intClubHID, "&B=", intClubID, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", intDevMatchID, "&A=", intClubHID, "&B=", intClubID, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else if (num < this.intTurn)
                    {
                        str = "";
                        str2 = "";
                    }
                    else if (this.IsMatchH(intClubHID))
                    {
                        str = "";
                    }
                    else
                    {
                        str = "";
                    }
                    if (intClubHID != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(intClubHID, 5, 15, intDevMatchID);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (intClubID != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(intClubID, 5, 15, intDevMatchID);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    string str6 = "VS";
                    if ((num5 == 0) && (num6 == 0))
                    {
                        if (num < this.intTurn)
                        {
                            str5 = "--";
                        }
                        else if (num == this.intTurn)
                        {
                            str = string.Concat(new object[] { "<img onclick=\"javascript:window.top.Main.location='Main_P.aspx?Tag=", this.intUserID, "&Type=VARRANGE';\" style=\"cursor:pointer;\" alt='本轮的阵容战术请在战术设定中管理' src='", SessionItem.GetImageURL(), "Tactics_1.gif' border='0' width='12' height='16'>" });
                        }
                        else if ((num == (this.intTurn + 1)) || (num == (this.intTurn + 2)))
                        {
                            string str7 = row["ArrangeA"].ToString().Trim();
                            string str8 = row["ArrangeH"].ToString().Trim();
                            if (this.IsMatchH(intClubHID))
                            {
                                if (str8 != "NO")
                                {
                                    str = string.Concat(new object[] { "<img onclick=\"javascript:window.parent.location='SecretaryPage.aspx?Type=PREARRANGE&Tag=", intDevMatchID, "';\" style=\"cursor:pointer;\" alt='管理预设战术' src='", SessionItem.GetImageURL(), "Tactics_e.gif' border='0' width='12' height='16'>" });
                                }
                                else
                                {
                                    str = string.Concat(new object[] { "<img onclick=\"javascript:window.parent.location='SecretaryPage.aspx?Type=PREARRANGE&Tag=", intDevMatchID, "';\" style=\"cursor:pointer;\" alt='启用预设战术' src='", SessionItem.GetImageURL(), "Tactics.gif' border='0' width='12' height='16'>" });
                                }
                            }
                            else if (str7 != "NO")
                            {
                                str = string.Concat(new object[] { "<img onclick=\"javascript:window.parent.location='SecretaryPage.aspx?Type=PREARRANGE&Tag=", intDevMatchID, "';\" style=\"cursor:pointer;\" alt='管理预设战术' src='", SessionItem.GetImageURL(), "Tactics_e.gif' border='0' width='12' height='16'>" });
                            }
                            else
                            {
                                str = string.Concat(new object[] { "<img onclick=\"javascript:window.parent.location='SecretaryPage.aspx?Type=PREARRANGE&Tag=", intDevMatchID, "';\" style=\"cursor:pointer;\" alt='启用预设战术' src='", SessionItem.GetImageURL(), "Tactics.gif' border='0' width='12' height='16'>" });
                            }
                        }
                        else
                        {
                            str = "&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        str5 = str + "&nbsp;&nbsp;" + str2;
                    }
                    else
                    {
                        str5 = str + "&nbsp;&nbsp;" + str2;
                        str6 = num5 + ":" + num6;
                    }
                    string str9 = "";
                    string str10 = "BarContent border";
                    if (this.intTurn == num)
                    {
                        str9 = "#FBE2D4";
                        str10 = "BarContent1 border";
                    }
                    object strUPList = this.strUPList;
                    this.strUPList = string.Concat(new object[] { 
                        strUPList, "<tr class='", str10, "' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor='", str9, "'\"><td width=\"50\" height=\"24\" align=\"left\" style=\"padding-left:20px\" ><font color='#7B1F76'>", num, "</font><a name=\"", num, "\" id=\"", num, "\"></a></td><td width=\"120\" align=\"right\" >", str3, "</td><td width=\"80\" align=\"center\" ><font color='40'>", str6, "</font></td><td width=\"120\" align=\"left\" >", 
                        str4, "</td><td >&nbsp;</td><td width=\"90\" align=\"left\" >", str5, "</td></tr>"
                     });
                    this.strUPList = this.strUPList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
        }
    }
}


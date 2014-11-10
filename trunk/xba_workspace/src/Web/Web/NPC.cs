namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;
    using Web.Util;

    public class NPC : Page
    {
        private int intClubID;
        public int intPage;
        public int intPrePage = 10;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        public StringBuilder sbDropOffList = new StringBuilder();
        public StringBuilder sbMatchList = new StringBuilder();
        public StringBuilder sbNPCList = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder("");
        public string strPageIntro;
        private string strType;
        protected HtmlTable tblDropOffList;
        protected HtmlTable tblMyMatchList;
        protected HtmlTable tblNPCList;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int dropOffTableCount = 0;
            if (this.strType == "MYMATCH")
            {
                return BTPNPCManager.GetNPCMatchTableCount(this.intClubID);
            }
            if (this.strType == "DROPOFF")
            {
                dropOffTableCount = BTPNPCManager.GetDropOffTableCount(this.intUserID);
            }
            return dropOffTableCount;
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
            this.tblNPCList.Visible = false;
            this.tblMyMatchList.Visible = false;
            this.tblDropOffList.Visible = false;
        }

        private void MyDropOffList()
        {
            string url = "NPC.aspx?Type=DROPOFF&";
            if (this.GetTotal() > 0)
            {
                foreach (DataRow row in BTPNPCManager.GetDropOffTableByPage(0, this.intPage, this.intPrePage, this.intUserID).Rows)
                {
                    int type = Convert.ToInt32(row["Type"]);
                    int num3 = Convert.ToInt32(row["Level"]);
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string dropOffName = DropOffUtil.GetDropOffName(type);
                    string str3 = StringItem.FormatDate(datIn, "hh:mm:ss");
                    this.sbDropOffList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbDropOffList.Append("<td height=32 align=center>");
                    this.sbDropOffList.Append(dropOffName);
                    this.sbDropOffList.Append("</td>");
                    this.sbDropOffList.Append("<td height=32 align=center>");
                    this.sbDropOffList.Append(num3);
                    this.sbDropOffList.Append("</td>");
                    this.sbDropOffList.Append("<td>");
                    this.sbDropOffList.Append(str3);
                    this.sbDropOffList.Append("</td>");
                    this.sbDropOffList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
            else
            {
                this.sbDropOffList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有掉落物品</td></tr>");
            }
            this.sbDropOffList.Append("<tr><td height='30' align='right' colspan='7'>" + PageUtil.GetViewPage(url, this.intPage, this.intPrePage, this.GetTotal()) + "</td></tr>");
            this.sbScript.Append(this.GetScript(url));
        }

        private void MyMatchList()
        {
            string url = "NPC.aspx?Type=MYMATCH&";
            if (this.GetTotal() > 0)
            {
                foreach (DataRow row in BTPNPCManager.GetNPCMatchTableByPage(0, this.intPage, this.intPrePage, this.intClubID).Rows)
                {
                    string str7;
                    int num2 = Convert.ToInt32(row["FMatchID"]);
                    int num3 = Convert.ToInt32(row["ClubIDA"]);
                    int num4 = Convert.ToInt32(row["ClubIDB"]);
                    int num5 = Convert.ToInt32(row["ScoreA"]);
                    int num6 = Convert.ToInt32(row["ScoreB"]);
                    Convert.ToInt32(row["ClubAPoint"]);
                    Convert.ToInt32(row["ClubBPoint"]);
                    int num7 = Convert.ToInt32(row["Status"]);
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str2 = StringItem.FormatDate(datIn, "hh:mm:ss");
                    string str3 = row["ClubInfoA"].ToString().Trim();
                    string str4 = row["ClubInfoB"].ToString().Trim();
                    string[] strArray = str3.Split(new char[] { '|' });
                    int intUserID = Convert.ToInt32(strArray[0]);
                    string strNickName = strArray[1];
                    string[] strArray2 = str4.Split(new char[] { '|' });
                    int num9 = Convert.ToInt32(strArray2[0]);
                    string str6 = strArray2[1];
                    this.sbMatchList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbMatchList.Append("<td height=32 align=center>");
                    this.sbMatchList.Append(MessageItem.GetNickNameInfo(intUserID, strNickName, 1, 5));
                    this.sbMatchList.Append("</td>");
                    this.sbMatchList.Append("<td height=32 align=center>");
                    if ((num5 > 0) || (num6 > 0))
                    {
                        this.sbMatchList.Append(num5 + ":" + num6);
                    }
                    else
                    {
                        this.sbMatchList.Append("----");
                    }
                    this.sbMatchList.Append("</td>");
                    this.sbMatchList.Append("<td height=32 align=center>");
                    this.sbMatchList.Append(MessageItem.GetNickNameInfo(num9, str6, 1, 5));
                    this.sbMatchList.Append("</td>");
                    this.sbMatchList.Append("<td>");
                    this.sbMatchList.Append(str2);
                    this.sbMatchList.Append("</td>");
                    if (num7 == 2)
                    {
                        str7 = "--";
                    }
                    else
                    {
                        str7 = string.Concat(new object[] { 
                            "<a href='", Config.GetDomain(), "VRep.aspx?Type=1&Tag=", num2, "&A=", num3, "&B=", num4, "' target='_blank'>战报</a> | <a href='", Config.GetDomain(), "VStas.aspx?Type=1&Tag=", num2, "&A=", num3, "&B=", num4, 
                            "' target='_blank'>统计</a>"
                         });
                    }
                    this.sbMatchList.Append("<td>");
                    this.sbMatchList.Append(str7);
                    this.sbMatchList.Append("</td>");
                    this.sbMatchList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
            else
            {
                this.sbMatchList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有比赛</td></tr>");
            }
            this.sbMatchList.Append("<tr><td height='30' align='right' colspan='7'>" + PageUtil.GetViewPage(url, this.intPage, this.intPrePage, this.GetTotal()) + "</td></tr>");
            this.sbScript.Append(this.GetScript(url));
        }

        private void NPCList()
        {
            DataTable nPCTable = BTPNPCManager.GetNPCTable();
            if ((nPCTable != null) && (nPCTable.Rows.Count > 0))
            {
                foreach (DataRow row in nPCTable.Rows)
                {
                    string str2;
                    string str3;
                    string strIn = Convert.ToString(row["Name"]);
                    int num = Convert.ToInt32(row["Level"]);
                    int num2 = Convert.ToInt32(row["NpcID"]);
                    int num3 = Convert.ToInt32(row["ClubID"]);
                    if (Convert.ToInt32(row["ClubIDA"]) == 0)
                    {
                        str3 = "等待中";
                        str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=SETNPC&NpcID=", num2, "'>挑战</a>" });
                    }
                    else
                    {
                        str3 = "比赛中";
                        str2 = "-- --";
                    }
                    this.sbNPCList.Append("<tr class='BarContent'style=\" background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                    this.sbNPCList.Append("<td align='center' height='25' style='color:green'>[<span style='width:20px;'>" + num + "</span>]</td>");
                    this.sbNPCList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px'><a href='ShowClub.aspx?ClubID=", num3, "&Type=5' title='", strIn, "' target='Right'>" }));
                    this.sbNPCList.Append(StringItem.GetShortString(strIn, 20, ".") + "</a></td>");
                    this.sbNPCList.Append("<td>" + str3 + "</td>");
                    this.sbNPCList.Append("<td>" + str2 + "</td>");
                    this.sbNPCList.Append("<td></td>");
                    this.sbNPCList.Append("</tr>");
                    this.sbNPCList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
            else
            {
                this.sbNPCList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有NPC</td></tr>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intUserID = (int) onlineRowByUserID["UserID"];
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                this.intWealth = (int) onlineRowByUserID["Wealth"];
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            switch (this.strType)
            {
                case "NPCLIST":
                    this.strPageIntro = "<ul><li class='qian1'>NBA球队</li><li class='qian2a'><a href='NPC.aspx?Type=MYMATCH'>我的比赛</a></li><li class='qian2a'><a href='NPC.aspx?Type=DROPOFF'>掉落物品</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.NPCList();
                    this.tblNPCList.Visible = true;
                    return;

                case "MYMATCH":
                    this.strPageIntro = "<ul><li class='qian1a'><a href='NPC.aspx?Type=NPCLIST'>NBA球队</a></li><li class='qian2'>我的比赛</li><li class='qian2a'><a href='NPC.aspx?Type=DROPOFF'>掉落物品</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.MyMatchList();
                    this.tblMyMatchList.Visible = true;
                    return;
            }
            this.strPageIntro = "<ul><li class='qian1a'><a href='NPC.aspx?Type=NPCLIST'>NBA球队</a></li><li class='qian2a'><a href='NPC.aspx?Type=MYMATCH'>我的比赛</a></li><li class='qian2'>掉落物品</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
            this.MyDropOffList();
            this.tblDropOffList.Visible = true;
        }
    }
}


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

    public class Arena : Page
    {
        private int arenaID;
        private int intClubID;
        public int intPage;
        public int intPrePage = 10;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        public StringBuilder sbArenaList = new StringBuilder();
        public StringBuilder sbMatchList = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder("");
        public string strPageIntro;
        private string strType;
        protected HtmlTable tblArenaList;
        protected HtmlTable tblArenaRegList;
        protected HtmlTable tblMyMatchList;

        private void AreaList()
        {
            foreach (DataRow row in BTPArenaManager.GetArenaTable().Rows)
            {
                int num = Convert.ToInt32(row["ArenaID"]);
                string str = Convert.ToString(row["ArenaName"]);
                Convert.ToInt32(row["OwnerUnionID"]);
                string str2 = Convert.ToString(row["OwnerUnionName"]);
                int num2 = Convert.ToInt32(row["AttackUnionID"]);
                string str3 = Convert.ToString(row["AttackUnionName"]);
                Convert.ToInt32(row["Level"]);
                this.sbArenaList.Append("<tr class='BarContent'style=\" background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                this.sbArenaList.Append(string.Concat(new object[] { "<td   height='25'  align='left' style='padding-left:10px'>" + num + "</td>" }));
                this.sbArenaList.Append(string.Concat(new object[] { "<td   height='25'  align='left' style='padding-left:10px'>" + str + "</td>" }));
                this.sbArenaList.Append("<td>" + str2 + "</td>");
                if (num2 > 0)
                {
                    this.sbArenaList.Append("<td>" + str3 + "</td>");
                }
                else
                {
                    this.sbArenaList.Append("<td>暂无抢夺联盟</td>");
                }
                this.sbArenaList.Append("<td>");
                this.sbArenaList.Append(string.Concat(new object[] { "<a title='盟主报名抢夺该球馆' href='SecretaryPage.aspx?Type=ARENAREG1&ArenaID=", num, "'>盟主</a>" }));
                this.sbArenaList.Append("&nbsp;&nbsp;|&nbsp;&nbsp;");
                this.sbArenaList.Append(string.Concat(new object[] { "<a title='盟员报名防守或者抢夺该球馆' href='SecretaryPage.aspx?Type=ARENAREG2&ArenaID=", num, "'>盟员</a>" }));
                this.sbArenaList.Append("&nbsp;&nbsp;|&nbsp;&nbsp;");
                this.sbArenaList.Append(string.Concat(new object[] { "<a title='查看所有参赛球队列表' href='Arena.aspx?Type=ENTER&ArenaID=", num, "'>球队</a>" }));
                this.sbArenaList.Append("&nbsp;&nbsp;|&nbsp;&nbsp;");
                this.sbArenaList.Append(string.Concat(new object[] { "<a title='查看所有比赛列表' href='Arena.aspx?Type=MATCHLIST&ArenaID=", num, "'>比赛</a>" }));
                this.sbArenaList.Append("</td>");
                this.sbArenaList.Append("</tr>");
                this.sbArenaList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
            }
        }

        private void AreaRegList()
        {
            DataTable arenaRegTable = BTPArenaManager.GetArenaRegTable(this.arenaID);
            DataRow arenaRow = BTPArenaManager.GetArenaRow(this.arenaID);
            string str = Convert.ToString(arenaRow["OwnerUnionName"]);
            string str2 = Convert.ToString(arenaRow["AttackUnionName"]);
            int num = 1;
            if (arenaRegTable == null)
            {
                this.sbArenaList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有球队报名参战</td></tr>");
            }
            else
            {
                foreach (DataRow row2 in arenaRegTable.Rows)
                {
                    int intUserID = Convert.ToInt32(row2["UserID"]);
                    int num3 = Convert.ToInt32(row2["Status"]);
                    int num4 = Convert.ToInt32(row2["Flag"]);
                    string strNickName = Convert.ToString(row2["NickName"]);
                    this.sbArenaList.Append("<tr class='BarContent'style=\" background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                    this.sbArenaList.Append("<td height=32 align=center>" + num + "</td>");
                    num++;
                    this.sbArenaList.Append("<td height=32 align=center>");
                    this.sbArenaList.Append(MessageItem.GetNickNameInfo(intUserID, strNickName, 1, 5));
                    this.sbArenaList.Append("</td>");
                    this.sbArenaList.Append("<td height=32 align=center>");
                    if (num4 == 1)
                    {
                        this.sbArenaList.Append(str);
                    }
                    else
                    {
                        this.sbArenaList.Append(str2);
                    }
                    this.sbArenaList.Append("</td>");
                    this.sbArenaList.Append("<td height=32 align=center>");
                    if (num4 == 1)
                    {
                        this.sbArenaList.Append("<font color=''>防守</font>");
                    }
                    else
                    {
                        this.sbArenaList.Append("<font color=''>进攻</font>");
                    }
                    this.sbArenaList.Append("</td>");
                    this.sbArenaList.Append("<td height=32 align=center>");
                    switch (num3)
                    {
                        case 1:
                            this.sbArenaList.Append("<font color=''>可战</font>");
                            break;

                        case 2:
                            this.sbArenaList.Append("<font color=''>战败</font>");
                            break;

                        case 3:
                            this.sbArenaList.Append("<font color=''>三连胜下阵</font>");
                            break;
                    }
                    this.sbArenaList.Append("</td>");
                    this.sbArenaList.Append("</tr>");
                    this.sbArenaList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int arenaMatchTableCount = 0;
            if (this.strType == "MYMATCH")
            {
                arenaMatchTableCount = BTPArenaManager.GetArenaMatchTableCount(this.intClubID);
            }
            return arenaMatchTableCount;
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
            this.tblArenaList.Visible = false;
            this.tblMyMatchList.Visible = false;
            this.tblArenaRegList.Visible = false;
        }

        private void MyMatchList()
        {
            string url = "Arena.aspx?Type=MYMATCH&";
            if (this.GetTotal() > 0)
            {
                foreach (DataRow row in BTPArenaManager.GetArenaMatchTableByPage(0, this.intPage, this.intPrePage, this.intClubID).Rows)
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

        protected override void OnInit(EventArgs e)
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            this.arenaID = SessionItem.GetRequest("ArenaID", 0);
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
            string strType = this.strType;
            if (strType != null)
            {
                if (strType != "LIST")
                {
                    if (strType != "ENTER")
                    {
                        if (strType == "MYMATCH")
                        {
                            this.strPageIntro = "<ul><li class='qian1a'><a href='Arena.aspx?Type=LIST'>球馆争夺</a></li><li class='qian2'>我的比赛</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.MyMatchList();
                            this.tblMyMatchList.Visible = true;
                        }
                        return;
                    }
                }
                else
                {
                    this.strPageIntro = "<ul><li class='qian1'>球馆争夺</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    this.AreaList();
                    this.tblArenaList.Visible = true;
                    return;
                }
                this.strPageIntro = "<ul><li class='qian1'>球馆争夺</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                this.AreaRegList();
                this.tblArenaRegList.Visible = true;
            }
        }
    }
}


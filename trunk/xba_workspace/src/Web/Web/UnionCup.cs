namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class UnionCup : Page
    {
        protected ImageButton btnCreate;
        protected ImageButton btnModify;
        public DateTime datMatchTime;
        protected DropDownList ddlClubLevel;
        protected DropDownList ddlHortation;
        protected DropDownList ddlHortation1;
        protected DropDownList ddlHortation2;
        protected DropDownList ddlHortation3;
        protected DropDownList ddlRegCharge;
        protected ImageButton IBtnAddUMatch;
        protected ImageButton IBtnBreak;
        public int intCapacity;
        public int intCategory;
        private int intClubID;
        public int intGrade;
        public int intPage;
        public int intPrePage = 5;
        public int intUnionCupID;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        public int intWealthCost;
        public int intWealthCostM;
        protected RadioButtonList rblSize;
        public StringBuilder sbCupRegManagePage = new StringBuilder();
        public StringBuilder sbCupRegManager = new StringBuilder();
        public StringBuilder sbLookList = new StringBuilder("");
        public StringBuilder sbLookListPage = new StringBuilder("");
        public StringBuilder sbScript = new StringBuilder("");
        public StringBuilder sbUnionCupChampion = new StringBuilder("");
        public StringBuilder sbUnionCupChampionPage = new StringBuilder("");
        public StringBuilder sbUnionCupList = new StringBuilder("");
        public StringBuilder sbUnionCupManageList = new StringBuilder();
        public StringBuilder sbUnionCupStats = new StringBuilder("");
        public StringBuilder sbUnionCupStatshPage = new StringBuilder("");
        public StringBuilder sbWealthPage = new StringBuilder("");
        public string strBeginTime;
        public string strBigLogo = "";
        private string strClubLogo;
        private string strClubName;
        public string strCount;
        public string strCupName;
        private string strDevCode;
        public string strErrMsg;
        public string strIntroduction;
        public string strLevel;
        public string strMatchTime;
        public string strModalTime;
        private string strNickName;
        public string strPageIntro;
        public string strPageIntro1;
        public string strRequirement;
        public string strRequirementXML;
        public string strReward;
        public string strReward1;
        public string strReward2;
        public string strReward3;
        public string strReward4;
        private string strShortName;
        private string strStatus;
        public string strType;
        public string strUnionCupName;
        public string strUnionName;
        protected TextBox tbCupName;
        protected TextBox tbEndTime;
        protected TextBox tbIntroM;
        protected HtmlTable tblCreate;
        protected HtmlTable tblCreateUnionCup;
        protected HtmlTable tblCupRegManager;
        protected HtmlTable tblManager;
        protected HtmlTable tblModifyUnionCup;
        protected HtmlTable tbLookList;
        protected HtmlTable tblPassWord;
        protected HtmlTable tblUMatchAdd;
        protected HtmlTable tblUMatchList;
        protected TextBox tbPassWord;
        protected TextBox tbPasswordM;
        protected TextBox tbRegPassword;
        protected HtmlTable tbUnionCupChampion;
        protected TextBox tbUnionCupIntro;
        protected HtmlTable tbUnionCupStats;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int unionCupClubRegCount = 0;
            int request = SessionItem.GetRequest("Grade", 0);
            if (this.strType == "CUPREG")
            {
                switch (request)
                {
                    case 0:
                        return BTPUnionCupManager.GetUnionCupCount(0);

                    case 2:
                        unionCupClubRegCount = BTPUnionCupManager.GetUnionCupClubRegCount();
                        break;
                }
                return unionCupClubRegCount;
            }
            if (this.strType == "UNIONCUPSTATS")
            {
                return BTPUnionCupManager.GetUnionCupCount(1);
            }
            if (this.strType != "UNIONCUPCHAMPION")
            {
                unionCupClubRegCount = BTPUnionCupManager.GetUnionCupCount(3);
            }
            return unionCupClubRegCount;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
            int total = this.GetTotal();
            int num2 = (total / this.intPrePage) + 1;
            if ((total % this.intPrePage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str = "";
            if (this.intPage == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (this.intPage == num2)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str3 = str3 + " selected";
                }
                obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            obj2 = str + " " + str2 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str3 });
        }

        private void IBtnAddUMatch_Click(object sender, ImageClickEventArgs e)
        {
            int request = SessionItem.GetRequest("UnionCupID", 0);
            BTPUnionCupManager.GetUnionCupRowByUnionCupID(request);
            switch (BTPUnionCupManager.AddUnionCupReg(this.intUserID, request))
            {
                case 1:
                    base.Response.Redirect("Report.aspx?Parameter=UCRS01!Type.CUPREG");
                    return;

                case 2:
                    base.Response.Redirect("Report.aspx?Parameter=UCRE01");
                    return;

                case 3:
                    base.Response.Redirect("Report.aspx?Parameter=UCRE02");
                    return;

                case 4:
                    base.Response.Redirect("Report.aspx?Parameter=UCRE03");
                    return;

                case 5:
                    base.Response.Redirect("Report.aspx?Parameter=UCRE04");
                    return;
            }
        }

        private void IBtnBreak_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("UnionCup.aspx?Type=" + this.strType);
        }

        private void InitializeComponent()
        {
            this.IBtnAddUMatch.Click += new ImageClickEventHandler(this.IBtnAddUMatch_Click);
            this.IBtnBreak.Click += new ImageClickEventHandler(this.IBtnBreak_Click);
            this.IBtnAddUMatch.ImageUrl = SessionItem.GetImageURL() + "button_47.gif";
            this.IBtnBreak.ImageUrl = SessionItem.GetImageURL() + "button_48.gif";
            base.Load += new EventHandler(this.Page_Load);
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
                this.strClubName = onlineRowByUserID["CLubName5"].ToString();
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strShortName = onlineRowByUserID["ShortName"].ToString();
                this.strDevCode = onlineRowByUserID["DevCode"].ToString().Trim();
                this.strClubLogo = onlineRowByUserID["ClubLogo"].ToString();
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intWealth = (int) onlineRowByUserID["Wealth"];
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.tbUnionCupStats.Visible = false;
                this.tblUMatchList.Visible = false;
                this.tblUMatchAdd.Visible = false;
                this.IBtnAddUMatch.Visible = false;
                this.tbLookList.Visible = false;
                this.tblPassWord.Visible = false;
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                this.strStatus = SessionItem.GetRequest("Status", 1).ToString().Trim();
                this.tbUnionCupChampion.Visible = false;
                this.tblCreateUnionCup.Visible = false;
                this.tblCreate.Visible = false;
                this.tblManager.Visible = false;
                this.tblModifyUnionCup.Visible = false;
                this.tblCupRegManager.Visible = false;
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
                case "CUPREG":
                    this.intGrade = SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tblUMatchList.Visible = true;
                        this.UnionCupList();
                    }
                    else if (this.intGrade == 1)
                    {
                        this.IBtnAddUMatch.Visible = true;
                        this.tblUMatchAdd.Visible = true;
                        this.SetUnionCupAdd();
                    }
                    else if (this.intGrade == 2)
                    {
                        this.tbLookList.Visible = true;
                        this.UnionCupLookList();
                    }
                    else if (this.intGrade == 3)
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetUnionCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1'>持卡报名</li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPSTATS'>比赛成绩</a></li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "UNIONCUPSTATS":
                    this.intGrade = SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tbUnionCupStats.Visible = true;
                        this.UnionCupStatsList();
                    }
                    else if (this.intGrade == 1)
                    {
                        this.tbLookList.Visible = true;
                        this.UnionCupLookList();
                    }
                    else if (this.intGrade == 2)
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetUnionCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1a'><a href='UnionCup.aspx?Type=CUPREG'>持卡报名</a></li><li class='qian2'>比赛成绩</li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "UNIONCUPCHAMPION":
                    this.intGrade = SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tbUnionCupChampion.Visible = true;
                        this.UnionCupChampionList();
                    }
                    this.strPageIntro = "<ul><li class='qian1a'><a href='UnionCup.aspx?Type=CUPREG'>持卡报名</a></li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPSTATS'>比赛成绩</a></li><li class='qian2'>冠军榜</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "CREATEDEVCUP":
                    this.tblCreate.Visible = true;
                    this.strPageIntro = "<ul><li class='qian1a'><a href='UnionCup.aspx?Type=CUPREG'>持卡报名</a></li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPSTATS'>比赛成绩</a></li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                default:
                    this.intGrade = SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tblUMatchList.Visible = true;
                        this.UnionCupList();
                    }
                    else
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetUnionCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1'>持卡报名</li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPSTATS'>比赛成绩</a></li><li class='qian2a'><a href='UnionCup.aspx?Type=UNIONCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;
            }
            this.strPageIntro1 = "<font color='red'>创建杯赛</font>&nbsp;|&nbsp;<a href='UnionCup.aspx?Type=CREATEDEVCUP&Status=MANAGER'>杯赛管理</a>";
            this.tblCreateUnionCup.Visible = true;
            this.strModalTime = StringItem.FormatDate(DateTime.Today.AddHours(63.0), "yyyy-MM-dd hh:mm");
        }

        private void SetUnionCupAdd()
        {
            int request = SessionItem.GetRequest("UnionCupID", 0);
            DevCalculator.GetLevel(this.strDevCode);
            new TagReader();
            DataRow unionCupRowByUnionCupID = BTPUnionCupManager.GetUnionCupRowByUnionCupID(request);
            if (unionCupRowByUnionCupID != null)
            {
                this.strBigLogo = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/12.gif' height='50' width='40'>";
                this.strUnionCupName = "无道联盟争霸赛";
                this.strIntroduction = unionCupRowByUnionCupID["Introduction"].ToString().Trim();
                int num2 = Convert.ToInt32(unionCupRowByUnionCupID["Status"]);
                unionCupRowByUnionCupID["RewardXML"].ToString().Trim();
                if (num2 == 0)
                {
                    this.strBeginTime = StringItem.FormatDate((DateTime) unionCupRowByUnionCupID["MatchTime"], "yyyy-MM-dd hh:mm");
                }
                else
                {
                    this.strBeginTime = "已截止";
                }
                this.strMatchTime = StringItem.FormatDate((DateTime) unionCupRowByUnionCupID["MatchTime"], "yyyy-MM-dd hh:mm");
                this.strReward = "第一名奖励：1000联盟威望<br>第二名奖励：500联盟威望<br>第三-四名奖励：200联盟威望";
            }
        }

        private void UnionCupChampionList()
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "UnionCup.aspx?Type=UNIONCUPCHAMPION&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbUnionCupChampionPage.Append(this.GetViewPage(strCurrentURL));
            DataTable table = BTPUnionCupManager.GetUnionCupTableByPage(0, this.intPage, this.intPrePage, 3);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num1 = (int) row["UnionCupID"];
                    int num = (int) row["Season"];
                    string str2 = row["ChampionUnionName"].ToString().Trim();
                    string str3 = "无道联盟冠军争霸赛";
                    DateTime datIn = (DateTime) row["MatchTime"];
                    string str4 = row["LadderURL"].ToString().Trim();
                    this.sbUnionCupChampion.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\" width=\"50\">" + ("<IMG src='" + SessionItem.GetImageURL() + "DevMedal/12.gif'>") + "</td>");
                    this.sbUnionCupChampion.Append("<td width=\"157\" align=\"left\" style='padding-left:4px'><font color=#3333>" + str3 + "</font></td>");
                    this.sbUnionCupChampion.Append("<td width=90  align=\"center\">" + num + "</td>");
                    this.sbUnionCupChampion.Append("<td  align=\"center\" width=\"80\">" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</td>");
                    this.sbUnionCupChampion.Append("<td align=\"center\" width=\"50\"><a href='" + str4 + "'>赛程</a></td>");
                    this.sbUnionCupChampion.Append("<td align=\"left\" style='padding-left:4px' width=\"136\"><font color='#fc5402'>" + str2 + "</font></td></tr>");
                    this.sbUnionCupChampion.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            if (this.sbUnionCupChampion.ToString().Trim() == "")
            {
                this.sbUnionCupChampion.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有联盟争霸冠军榜</td></tr>");
            }
            this.sbUnionCupChampion.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbUnionCupChampionPage.ToString() + "</td></tr>");
        }

        private void UnionCupList()
        {
            string strCurrentURL = "UnionCup.aspx?Type=CUPREG&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbWealthPage.Append(this.GetViewPage(strCurrentURL));
            DataTable table = BTPUnionCupManager.GetUnionCupTableByPage(0, this.intPage, this.intPrePage, 0);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2 = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/12.GIF' height='50' width='40' border='0'>";
                    int num = (int) row["UnionCupID"];
                    string str3 = Convert.ToString(row["MatchTime"]);
                    string str4 = "无道联盟冠军争霸赛";
                    this.sbUnionCupList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\">" + str2 + "</td>");
                    this.sbUnionCupList.Append("<td>" + str4 + "</td>");
                    this.sbUnionCupList.Append("<td>&nbsp;</td>");
                    this.sbUnionCupList.Append("<td align=\"center\"><font color=#3333>" + str3 + "</font></td>");
                    this.sbUnionCupList.Append(string.Concat(new object[] { "<td align=\"center\"><a href='UnionCup.aspx?Type=CUPREG&Grade=1&UnionCupID=", num, "'>报名</a>| <a href='UnionCup.aspx?Type=CUPREG&Grade=2&UnionCupID=", num, "'>查看</a></td></tr>" }));
                    this.sbUnionCupList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            if (this.sbUnionCupList.ToString().Trim() == "")
            {
                this.sbUnionCupList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有联盟争霸赛</td></tr>");
            }
            this.sbUnionCupList.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbWealthPage.ToString() + "</td></tr>");
        }

        private void UnionCupLookList()
        {
            int request = SessionItem.GetRequest("UnionCupID", 0);
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 13;
            string strCurrentURL = "UnionCup.aspx?Type=CUPREG&Grade=2&UnionCupID=" + request + "&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbLookListPage.Append(this.GetViewPage(strCurrentURL));
            DataTable table = BTPUnionCupManager.GetUnionCupClubRegTable(0, this.intPage, this.intPrePage);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2 = row["ClubName"].ToString().Trim();
                    string str3 = row["UnionName"].ToString().Trim();
                    this.sbLookList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td align=\"left\" width=\"120\" height=\"25\" style='padding-left:5px;'>" + str2 + "</td>");
                    this.sbLookList.Append("<td align=\"left\">" + str3 + "</td></tr>");
                }
            }
            if (this.sbLookList.ToString().Trim() == "")
            {
                this.sbLookList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有球队报名</td></tr>");
            }
            this.sbLookList.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbLookListPage.ToString() + "</td></tr>");
        }

        private void UnionCupStatsList()
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "UnionCup.aspx?Type=UNIONCUPSTATS&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbUnionCupStatshPage.Append(this.GetViewPage(strCurrentURL));
            DataTable table = BTPUnionCupManager.GetUnionCupTableByPage(0, this.intPage, this.intPrePage, 1);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["UnionCupID"];
                    Convert.ToInt32(row["Round"]);
                    int num2 = Convert.ToInt32(row["Season"]);
                    int num3 = Convert.ToInt32(row["Status"]);
                    DateTime datIn = (DateTime) row["MatchTime"];
                    string str2 = "无道冠军争霸赛";
                    string str3 = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/12.gif'>";
                    string str4 = row["LadderURL"].ToString().Trim();
                    this.sbUnionCupStats.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\" width=\"50\">" + str3 + "</td>");
                    this.sbUnionCupStats.Append("<td width=\"105\" align=\"left\" style='padding-left:4px'><font color=#3333>" + str2 + "</font></td>");
                    this.sbUnionCupStats.Append(string.Concat(new object[] { "<td width=\"90\" align=\"center\"><font color=#3333>", num2, "</font></td>" }));
                    switch (num3)
                    {
                        case 0:
                            this.sbUnionCupStats.Append("<td width=\"80\" align=\"center\"><a title='报名截止时间' style='CURSOR: hand'><font color=#3333>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</a></font></td>");
                            break;

                        case 1:
                            this.sbUnionCupStats.Append("<td width=\"80\" align=\"center\"><a title='下轮比赛时间' style='CURSOR: hand'><font color=#3333>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</a></font></td>");
                            break;

                        default:
                            this.sbUnionCupStats.Append("<td width=\"80\" align=\"center\"><font color=#3333>已结束</font></td>");
                            break;
                    }
                    if (num3 == 0)
                    {
                        this.sbUnionCupStats.Append(string.Concat(new object[] { "<td align=\"center\"><a href='UnionCup.aspx?Type=UNIONCUPSTATS&Grade=1&UnionCupID=", num, "'>查看</a> | <a href='UnionCup.aspx?Type=UNIONCUPSTATS&Grade=2&UnionCupID=", num, "'>介绍</a></td></tr>" }));
                    }
                    else
                    {
                        this.sbUnionCupStats.Append(string.Concat(new object[] { "<td align=\"center\"><a href='", str4, "'>赛程</a> | <a href='UnionCup.aspx?Type=UNIONCUPSTATS&Grade=2&UnionCupID=", num, "'>介绍</a></td></tr>" }));
                    }
                    this.sbUnionCupStats.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            if (this.sbUnionCupStats.ToString().Trim() == "")
            {
                this.sbUnionCupStats.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有进行中的联盟争霸赛</td></tr>");
            }
            this.sbUnionCupStats.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbUnionCupStatshPage.ToString() + "</td></tr>");
        }
    }
}


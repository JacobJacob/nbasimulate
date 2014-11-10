namespace Web
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class ChampionCup : Page
    {
        public bool blnCanOpen = true;
        public bool blnIsStop;
        protected ImageButton btnFindMatch;
        protected ImageButton btnReg;
        protected ImageButton btnXResult;
        protected ImageButton btnXSchedule;
        protected HtmlGenericControl divMyMatch;
        private int intClubID5;
        private int intPage;
        private int intPerPage = 6;
        private int intStatus;
        public int intTop;
        private int intTurn;
        private int intUserID;
        public StringBuilder sbReturn = new StringBuilder();
        public string strChampion;
        public string strChampionSay = "";
        public string strCupURL = "";
        public string strDownList = "";
        private string strKind;
        public string strList;
        public string strMatchList = "";
        public string strMyMsg = "";
        public string strPageIntro1;
        public string strScript = "";
        public string strUPList;
        public string strUserID = "";
        public string strXIntro = "";
        public string strXML = "";
        protected TextBox tbClubName;
        protected HtmlTable tblChampionCup;
        protected HtmlTable tblChampionInfo;
        protected HtmlTable tblChampionReg;
        protected HtmlTable tblKemp;
        protected HtmlTable tblRegBegin;
        protected HtmlTable tblRegEnd;
        protected HtmlTable tblTryoutMatch;
        protected HtmlTable tblTryoutTop;
        protected HtmlTable tblXBAGame;
        protected HtmlTable tblXCupXml;

        private void btnFindMatch_Click(object sender, ImageClickEventArgs e)
        {
            string strIn = this.tbClubName.Text.Trim();
            if (StringItem.IsValidWord(strIn, 1, 30))
            {
                DataRow accountRowByClubName = BTPAccountManager.GetAccountRowByClubName(strIn);
                if (accountRowByClubName != null)
                {
                    int num = (int) accountRowByClubName["ClubID5"];
                    base.Response.Redirect(string.Concat(new object[] { "ChampionCup.aspx?CID=", num, "&Kind=CHMAPIONCUP&Tag=", this.intUserID }));
                }
            }
        }

        private void btnReg_Click(object sender, ImageClickEventArgs e)
        {
            if (this.intClubID5 > 0)
            {
                switch (BTPXGroupTeamManager.RegXBACup(this.intClubID5))
                {
                    case -6:
                        base.Response.Redirect("Report.aspx?Parameter=440");
                        return;

                    case -5:
                        base.Response.Redirect("Report.aspx?Parameter=439");
                        return;

                    case -4:
                        base.Response.Redirect("Report.aspx?Parameter=444");
                        return;

                    case -3:
                        base.Response.Redirect("Report.aspx?Parameter=438c");
                        return;

                    case -2:
                        base.Response.Redirect("Report.aspx?Parameter=438b");
                        return;

                    case -1:
                        base.Response.Redirect("Report.aspx?Parameter=438");
                        return;

                    case 0:
                        return;

                    case 1:
                        base.Response.Redirect("Report.aspx?Parameter=442");
                        return;
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=438c");
            }
        }

        private void GetLostMatch()
        {
            int num = (byte) BTPXCupRegManager.GetXCupRegRowByClubID(this.intClubID5)["DeadRound"];
            string str = "0";
            string str2 = "0";
            string str3 = "";
            DataRow lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
            if (lastGameRowByCategory != null)
            {
                string str4;
                string line = lastGameRowByCategory["RewardXML"].ToString().Trim();
                byte num1 = (byte) lastGameRowByCategory["Round"];
                string str6 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                if (((this.intStatus > 0) && (str6 != "")) && (str6.ToLower().IndexOf("http://match") == -1))
                {
                    str6 = Config.GetDomain() + str6;
                }
                TagReader reader = new TagReader();
                IEnumerator enumerator = reader.GetItems(line, "<Reward>", "</Reward>").GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string current = (string) enumerator.Current;
                    if (Convert.ToInt32(reader.GetTagline(current, "<Round>", "</Round>")) == num)
                    {
                        str = reader.GetTagline(current, "<Money>", "</Money>");
                        str2 = reader.GetTagline(current, "<Reputation>", "</Reputation>");
                    }
                }
                this.intStatus = (byte) lastGameRowByCategory["Status"];
                int num2 = (byte) lastGameRowByCategory["Round"];
                int num3 = (int) lastGameRowByCategory["Capacity"];
                int num4 = 1;
                if (this.intStatus == 1)
                {
                    str3 = string.Concat(new object[] { "您在第", num, "轮淘汰赛中被淘汰出局，获得", str, "资金与", str2, "点威望。" });
                    while (num3 > 1)
                    {
                        num3 /= 2;
                        num4++;
                    }
                    int num5 = num4 - num2;
                    if (num5 > 5)
                    {
                        object obj2 = str3;
                        str3 = string.Concat(new object[] { obj2, "目前淘汰赛进行至第", num2, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str6, "')>请点击此处</a>" });
                    }
                    else if (num5 > 2)
                    {
                        str4 = str3;
                        str3 = str4 + "目前淘汰赛进行至第" + Math.Pow(2.0, (double) num5).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str6 + "')>请点击此处</a>";
                    }
                    else
                    {
                        switch (num5)
                        {
                            case 1:
                                str3 = str3 + "目前淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str6 + "')>请点击此处</a>";
                                break;

                            case 2:
                                str3 = str3 + "目前淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str6 + "')>请点击此处</a>";
                                break;
                        }
                    }
                }
                else
                {
                    string strNickName = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                    int intUserID = (int) lastGameRowByCategory["ChampionUserID"];
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20);
                    str4 = str3;
                    str3 = str4 + "本赛季冠军杯正式结束，" + strNickName + "获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str6 + "')>请点击此处</a>";
                }
            }
            this.sbReturn.Append("<tr>\n");
            this.sbReturn.Append("<td colspan=3 align=\"left\">" + str3 + "</td>\n");
            this.sbReturn.Append("</tr>\n");
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPXGameManager.GetChampionCupKempCount();
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
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
            return string.Concat(new object[] { obj2, "总数:", total, "跳转", str3 });
        }

        private void InitializeComponent()
        {
            this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click);
            this.btnReg.Attributes["onclick"] = base.GetPostBackEventReference(this.btnReg) + ";this.disabled=true;";
            base.Load += new EventHandler(this.Page_Load);
        }

        private bool IsMatchH(int intClubHID)
        {
            return (intClubHID == this.intClubID5);
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
                DataRow lastGameRowByCategory;
                string str;
                string str2;
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.intTurn = (int) BTPGameManager.GetGameRow()["Turn"];
                int num = (byte) BTPXGameManager.GetLastGameRowByCategory(5)["Status"];
                if (((this.intTurn > 2) && (num < 3)) && (DateTime.Now.Hour == 0x10))
                {
                    lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(1);
                    int num2 = (byte) lastGameRowByCategory["Status"];
                    if (num2 != 3)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=912");
                        return;
                    }
                    DateTime time = (DateTime) lastGameRowByCategory["MatchTime"];
                    if (DateTime.Now.Day != time.Day)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=912");
                        return;
                    }
                    this.blnIsStop = true;
                }
                this.strUserID = this.intUserID.ToString().Trim();
                this.tblChampionReg.Visible = false;
                this.tblTryoutMatch.Visible = false;
                this.tblTryoutTop.Visible = false;
                this.tblRegEnd.Visible = false;
                this.tblRegBegin.Visible = false;
                this.tblKemp.Visible = false;
                this.tblChampionCup.Visible = false;
                this.tblChampionInfo.Visible = false;
                this.tblXCupXml.Visible = false;
                this.strKind = SessionItem.GetRequest("Kind", 1).ToString().Trim();
                this.intStatus = (byte) BTPXGameManager.GetLastGameRowByCategory(1)["Status"];
                DataRow parameterRow = BTPParameterManager.GetParameterRow();
                bool flag = true;
                if (parameterRow != null)
                {
                    flag = (bool) parameterRow["CanXBAGame"];
                }
                if (!flag)
                {
                    this.blnCanOpen = false;
                }
                if ((this.strKind == "CHMAPIONCUP") || !this.blnCanOpen)
                {
                    if (this.blnCanOpen)
                    {
                        str2 = "<li class='qian2'>淘汰赛</li>";
                    }
                    else
                    {
                        str2 = "<li class='qian2a'><font color='#aaaaaa'>淘汰赛</font></li>";
                    }
                }
                else
                {
                    str2 = "<li class='qian2a'><a href='ChampionCup.aspx?Kind=CHMAPIONCUP&Tag=" + this.intUserID + "'>淘汰赛</a></li>";
                }
                if (((this.intTurn < 3) || ((this.intTurn == 3) && (DateTime.Now.Hour < 0x10))) || !this.blnCanOpen)
                {
                    str = "<li class='qian2a'><font color='#aaaaaa'>小组赛程</font></li><li class='qian2a'><font color='#aaaaaa'>小组排名</font></li>" + str2;
                }
                else
                {
                    str = string.Concat(new object[] { "<li class='qian2a'><a href='ChampionCup.aspx?Kind=TRYOUT&Tag=", this.intUserID, "'>小组赛程</a></li><li class='qian2a'><a href='ChampionCup.aspx?Kind=TRYOUTTOP&Tag=", this.intUserID, "'>小组排名</a></li>", str2 });
                }
                if (!this.blnCanOpen && (this.strKind != "KEMP"))
                {
                    this.strKind = "CHAMPIONREG";
                }
                switch (this.strKind)
                {
                    case "CHAMPIONREG":
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1'>大赛报名</li>", str, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.tblChampionReg.Visible = true;
                        this.btnReg.ImageUrl = SessionItem.GetImageURL() + "SendXBA.gif";
                        this.SetChampionReg();
                        break;

                    case "TRYOUT":
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1a'><a href='ChampionCup.aspx?Kind=CHAMPIONREG&Tag=", this.intUserID, "'>大赛报名</a></li><li class='qian2'>小组赛程</li><li class='qian2a'><a href='ChampionCup.aspx?Kind=TRYOUTTOP&Tag=", this.intUserID, "'>小组排名</a></li>", str2, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.tblTryoutMatch.Visible = true;
                        this.SetTryout();
                        break;

                    case "TRYOUTTOP":
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1a'><a href='ChampionCup.aspx?Kind=CHAMPIONREG&Tag=", this.intUserID, "'>大赛报名</a></li><li class='qian2a'><a href='ChampionCup.aspx?Kind=TRYOUT&Tag=", this.intUserID, "'>小组赛程</a></li><li class='qian2'>小组排名</li>", str2, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a><span style='margin-left:5px'>输入分组编号：<input typ=\"text\" id=\"GroupIndex\" class=\"Input_20_20\" > <img style='cursor:pointer' onclick='JumpOtherGroup(2)' align='absmiddle' src='", SessionItem.GetImageURL(), "button_go.gif' width='30' height='20' broder=0></span>" });
                        this.tblTryoutTop.Visible = true;
                        this.SetTryoutTop();
                        break;

                    case "CHMAPIONCUP":
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1a'><a href='ChampionCup.aspx?Kind=CHAMPIONREG&Tag=", this.intUserID, "'>大赛报名</a></li>", str, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                        if (lastGameRowByCategory != null)
                        {
                            if (((byte) lastGameRowByCategory["Status"]) != 0)
                            {
                                this.tblChampionCup.Visible = true;
                                this.SetXCupMatch();
                                break;
                            }
                            DataRow row4 = BTPXGameManager.GetLastGameRowByCategory(1);
                            if (lastGameRowByCategory != null)
                            {
                                this.intTop = (byte) row4["Round"];
                            }
                            row4 = BTPXGameManager.GetLastGameRowByCategory(5);
                            this.strXIntro = row4["Introduction"].ToString().Trim();
                            this.tblChampionInfo.Visible = true;
                        }
                        break;

                    case "KEMP":
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1a'><a href='ChampionCup.aspx?Kind=CHAMPIONREG&Tag=", this.intUserID, "'>大赛报名</a></li>", str, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.tblKemp.Visible = true;
                        this.SetKemp();
                        break;

                    case "XCUPXML":
                    {
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1a'><a href='ChampionCup.aspx?Kind=CHAMPIONREG&Tag=", this.intUserID, "'>大赛报名</a></li>", str, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                        string str3 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                        int num3 = (byte) lastGameRowByCategory["Status"];
                        if ((num3 > 0) && (str3 != ""))
                        {
                            if (str3.ToLower().IndexOf("http://match") == -1)
                            {
                                str3 = Config.GetDomain() + str3;
                            }
                            this.tblChampionCup.Visible = false;
                            this.tblXCupXml.Visible = true;
                            this.strCupURL = str3;
                        }
                        break;
                    }
                    default:
                        this.strPageIntro1 = string.Concat(new object[] { "<ul><li class='qian1'>大赛报名</li>", str, "<li class='qian2a'><a href='ChampionCup.aspx?Kind=KEMP&Tag=", this.intUserID, "'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.tblChampionReg.Visible = true;
                        this.btnReg.ImageUrl = SessionItem.GetImageURL() + "SendXBA.gif";
                        this.SetChampionReg();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetChampionReg()
        {
            DataRow groupTeamRowByCID = BTPXGroupTeamManager.GetGroupTeamRowByCID(this.intClubID5);
            if (!this.blnCanOpen)
            {
                this.tblRegBegin.Visible = true;
                this.tblRegEnd.Visible = false;
                this.btnReg.Visible = false;
                this.strChampionSay = "本赛季暂不开放冠军杯赛，敬请在下个赛季关注！";
            }
            else
            {
                string str;
                string str13;
                if (groupTeamRowByCID != null)
                {
                    if ((this.intTurn <= 4) && ((this.intTurn != 4) || (DateTime.Now.Hour < 0x11)))
                    {
                        this.tblRegBegin.Visible = true;
                        this.tblRegEnd.Visible = false;
                        this.btnReg.Visible = false;
                        this.strChampionSay = "您已成功报名，比赛会在联赛第三轮当天的下午4点安排赛程。";
                        return;
                    }
                    this.tblRegEnd.Visible = true;
                    DataTable championCupUPDown = BTPXGroupTeamManager.GetChampionCupUPDown(this.intClubID5);
                    if (championCupUPDown == null)
                    {
                        return;
                    }
                    if (championCupUPDown.Rows.Count != 2)
                    {
                        return;
                    }
                    groupTeamRowByCID = championCupUPDown.Rows[0];
                    if (groupTeamRowByCID == null)
                    {
                        return;
                    }
                    int num10 = (int) groupTeamRowByCID["MatchID"];
                    int num11 = (int) groupTeamRowByCID["ClubAID"];
                    int num12 = (int) groupTeamRowByCID["ClubBID"];
                    int num13 = (int) groupTeamRowByCID["ClubAScore"];
                    int num14 = (int) groupTeamRowByCID["ClubBScore"];
                    int intUserID = (int) groupTeamRowByCID["UserIDA"];
                    int num16 = (int) groupTeamRowByCID["UserIDB"];
                    string strNickName = groupTeamRowByCID["NameA"].ToString().Trim();
                    string str8 = groupTeamRowByCID["NameB"].ToString().Trim();
                    int num17 = (byte) groupTeamRowByCID["Type"];
                    if ((num11 != -1) || (num12 != -1))
                    {
                        if ((num11 == -2) && (num12 == -2))
                        {
                            this.tblRegBegin.Visible = true;
                            this.tblRegEnd.Visible = false;
                            this.btnReg.Visible = false;
                            groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(5);
                            this.intStatus = (byte) groupTeamRowByCID["Status"];
                            string str9 = groupTeamRowByCID["ChampionClubName"].ToString().Trim();
                            int num18 = (int) groupTeamRowByCID["ChampionUserID"];
                            str9 = AccountItem.GetNickNameInfo(num18, str9, "Right", 20);
                            string str10 = groupTeamRowByCID["LadderURL"].ToString().Trim();
                            if (((this.intStatus > 0) && (str10 != "")) && (str10.ToLower().IndexOf("http://match") == -1))
                            {
                                str10 = Config.GetDomain() + str10;
                            }
                            this.strChampionSay = "本赛季冠军杯正式结束，" + str9 + "获得总冠军！详情<a href='javascript:;' onclick=window.open('" + str10 + "')>请点击此处</a> ";
                            return;
                        }
                        if ((num11 == -3) && (num12 == -3))
                        {
                            this.GetLostMatch();
                            return;
                        }
                        if ((num11 != 0) || (num12 != 0))
                        {
                            if (intUserID > 0)
                            {
                                strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20);
                            }
                            if (num16 > 0)
                            {
                                str8 = AccountItem.GetNickNameInfo(num16, str8, "Right", 20);
                            }
                            this.sbReturn.Append("<tr>\n");
                            this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>上轮战况</strong></td>\n");
                            this.sbReturn.Append("</tr>\n");
                            this.sbReturn.Append("<tr>\n");
                            this.sbReturn.Append("\t<td width=\"43%\" align=\"right\">" + strNickName + "</td>\n");
                            this.sbReturn.Append(string.Concat(new object[] { "\t<td align=\"center\">", num13, ":", num14, "</td>\n" }));
                            this.sbReturn.Append("\t<td width=\"43%\" align=\"left\">" + str8 + string.Concat(new object[] { 
                                "<a href='", Config.GetDomain(), "VRep.aspx?Tag=", num10, "&Type=", num17, "&A=", num11, "&B=", num12, "' style='padding-left:12px' target='_blank'>战报</a>|<a href='", Config.GetDomain(), "VStas.aspx?Tag=", num10, "&Type=", num17, 
                                "&A=", num11, "&B=", num12, "' target='_blank'>统计</a>"
                             }) + "</td>\n");
                            this.sbReturn.Append("</tr>\n");
                            this.sbReturn.Append("<tr>\n");
                        }
                        groupTeamRowByCID = championCupUPDown.Rows[1];
                        if (groupTeamRowByCID != null)
                        {
                            string str11;
                            num10 = (int) groupTeamRowByCID["MatchID"];
                            num11 = (int) groupTeamRowByCID["ClubAID"];
                            num12 = (int) groupTeamRowByCID["ClubBID"];
                            num13 = (int) groupTeamRowByCID["ClubAScore"];
                            num14 = (int) groupTeamRowByCID["ClubBScore"];
                            intUserID = (int) groupTeamRowByCID["UserIDA"];
                            num16 = (int) groupTeamRowByCID["UserIDB"];
                            strNickName = groupTeamRowByCID["NameA"].ToString().Trim();
                            str8 = groupTeamRowByCID["NameB"].ToString().Trim();
                            int num19 = (byte) groupTeamRowByCID["Type"];
                            if ((num19 == 5) && (num17 == 4))
                            {
                                string str12;
                                groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(1);
                                int num20 = (byte) groupTeamRowByCID["Status"];
                                DateTime time2 = (DateTime) groupTeamRowByCID["MatchTime"];
                                if ((DateTime.Now.Day == time2.AddDays(1.0).Day) && (num20 == 3))
                                {
                                    this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>下轮对手</strong></td>\n");
                                    this.sbReturn.Append("</tr>\n");
                                    this.sbReturn.Append("<tr>\n");
                                    this.sbReturn.Append("<td colspan=3 align=\"center\">小组赛已结束，淘汰赛将于明日正式开始</td>\n");
                                    this.sbReturn.Append("</tr>\n");
                                    return;
                                }
                                if ((num11 == -1) && (num11 == -1))
                                {
                                    this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>下轮对手</strong></td>\n");
                                    this.sbReturn.Append("</tr>\n");
                                    this.sbReturn.Append("<tr>\n");
                                    this.sbReturn.Append("<td colspan=3 align=\"center\">您已经输掉了冠军杯赛，下赛季继续努力</td>\n");
                                    this.sbReturn.Append("</tr>\n");
                                    return;
                                }
                                this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>下轮对手</strong></td>\n");
                                this.sbReturn.Append("</tr>\n");
                                this.sbReturn.Append("<tr>\n");
                                this.sbReturn.Append("<td width=\"43%\" align=\"right\">" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20) + "</td>\n");
                                this.sbReturn.Append("<td align=\"center\">VS</td>\n");
                                if (this.intStatus > 1)
                                {
                                    str12 = "ChampionCup.aspx?Kind=CHMAPIONCUP";
                                }
                                else
                                {
                                    str12 = "ChampionCup.aspx?Kind=TRYOUT";
                                }
                                this.sbReturn.Append("<td width=\"43%\" align=\"left\">" + AccountItem.GetNickNameInfo(num16, str8, "Right", 20) + "<a style='padding-left:12px' href=\"" + str12 + "\">查看赛程</a></td>\n");
                                this.sbReturn.Append("</tr>\n");
                                this.sbReturn.Append("<td colspan=3 style=\"padding:0\" align=\"center\">比赛会在每天16点进行</td>\n");
                                this.sbReturn.Append("</tr>\n");
                                return;
                            }
                            if ((num11 == -1) && (num11 == -1))
                            {
                                this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>下轮对手</strong></td>\n");
                                this.sbReturn.Append("</tr>\n");
                                this.sbReturn.Append("<tr>\n");
                                this.sbReturn.Append("<td colspan=3 align=\"center\">您已经输掉了冠军杯赛，下赛季继续努力</td>\n");
                                this.sbReturn.Append("</tr>\n");
                                return;
                            }
                            if (intUserID > 0)
                            {
                                strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20);
                            }
                            if (num16 > 0)
                            {
                                str8 = AccountItem.GetNickNameInfo(num16, str8, "Right", 20);
                            }
                            this.sbReturn.Append("\t<td colspan=\"3\" align=\"center\" bgcolor=\"#f8ece6\" style=\"BORDER-BOTTOM:#f1c2a3 1px dotted\"><strong>下轮对手</strong></td>\n");
                            this.sbReturn.Append("</tr>\n");
                            this.sbReturn.Append("<tr>\n");
                            this.sbReturn.Append("<td width=\"43%\" align=\"right\">" + strNickName + "</td>\n");
                            this.sbReturn.Append("<td align=\"center\">VS</td>\n");
                            if (this.intStatus > 1)
                            {
                                str11 = "ChampionCup.aspx?Kind=CHMAPIONCUP";
                            }
                            else
                            {
                                str11 = "ChampionCup.aspx?Kind=TRYOUT";
                            }
                            this.sbReturn.Append("<td width=\"43%\" align=\"left\">" + str8 + "<a style='padding-left:12px' href=\"" + str11 + "\">查看赛程</a></td>\n");
                            this.sbReturn.Append("</tr>\n");
                            this.sbReturn.Append("<tr>\n");
                            this.sbReturn.Append("<td colspan=3 style=\"padding:0\" align=\"center\">比赛会在每天16点进行</td>\n");
                            this.sbReturn.Append("</tr>\n");
                        }
                        return;
                    }
                    str13 = "";
                    this.tblRegBegin.Visible = false;
                    this.tblRegEnd.Visible = true;
                    groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(5);
                    if (groupTeamRowByCID != null)
                    {
                        this.intStatus = (byte) groupTeamRowByCID["Status"];
                        int num21 = (byte) groupTeamRowByCID["Round"];
                        int num22 = (int) groupTeamRowByCID["Capacity"];
                        int num23 = 1;
                        string str14 = groupTeamRowByCID["LadderURL"].ToString().Trim();
                        if (((this.intStatus > 0) && (str14 != "")) && (str14.ToLower().IndexOf("http://match") == -1))
                        {
                            str14 = Config.GetDomain() + str14;
                        }
                        if (this.blnIsStop)
                        {
                            str13 = "您没有在小组赛出线，请下个赛季继续努力。现在停赛一轮正在安排淘汰赛的赛程。点击<a href='ChampionCup.aspx?Kind=CHMAPIONCUP&Tag=" + this.intUserID + "'> 淘汰赛</a>";
                        }
                        else if (this.intStatus == 1)
                        {
                            while (num22 > 1)
                            {
                                num22 /= 2;
                                num23++;
                            }
                            int num24 = num23 - num21;
                            if (num21 == 1)
                            {
                                str13 = "您没有在小组赛出线，请下个赛季继续努力。现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str14 + "')>淘汰赛</a>";
                            }
                            else if (num24 > 5)
                            {
                                str13 = string.Concat(new object[] { "冠军杯淘汰赛进行到第", num21, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str14, "')>请点击此处</a>" });
                            }
                            else if (num24 > 2)
                            {
                                str13 = "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num24).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str14 + "')>请点击此处</a>";
                            }
                            else
                            {
                                switch (num24)
                                {
                                    case 1:
                                        str13 = "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str14 + "')>请点击此处</a>";
                                        break;

                                    case 2:
                                        str13 = "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str14 + "')>请点击此处</a>";
                                        break;
                                }
                            }
                        }
                        else
                        {
                            string str15 = groupTeamRowByCID["ChampionClubName"].ToString().Trim();
                            int num25 = (int) groupTeamRowByCID["ChampionUserID"];
                            str15 = AccountItem.GetNickNameInfo(num25, str15, "Right", 20);
                            if (num25 != this.intUserID)
                            {
                                str = str13;
                                str13 = str + "本赛季冠军杯正式结束，" + str15 + "获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str14 + "')>请点击此处</a>";
                            }
                            else
                            {
                                str13 = str13 + "本赛季冠军杯正式结束，恭喜您获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str14 + "')>请点击此处</a>";
                            }
                        }
                    }
                }
                else
                {
                    string str2 = "";
                    string str3 = "left";
                    if ((this.intTurn < 3) || ((this.intTurn == 3) && (DateTime.Now.Hour < 0x10)))
                    {
                        int xCupRegCount = BTPXGameManager.GetXCupRegCount();
                        int num2 = (int) BTPXGameManager.GetLastGameRowByCategory(1)["Capacity"];
                        this.tblRegBegin.Visible = true;
                        this.tblRegEnd.Visible = true;
                        if (xCupRegCount >= num2)
                        {
                            this.btnReg.Visible = false;
                            str2 = "<span style='margin-left:210px'>冠军杯赛已经报满！</span>";
                        }
                        else
                        {
                            str2 = string.Concat(new object[] { "已报名", xCupRegCount, "人，还剩下", num2 - xCupRegCount, "空位。" });
                            if (this.intTurn == 1)
                            {
                                if (ToolItem.HasTool(this.intUserID, 1, 100) < 1)
                                {
                                    this.btnReg.Visible = false;
                                }
                                str2 = str2 + "只有持有冠军杯邀请函的经理可以在今天报名，如果持有的是冠军杯邀请函（盟），请明天再来。冠军杯邀请函（盟）可以向盟主索取。";
                            }
                            else if (this.intTurn == 2)
                            {
                                int num3 = ToolItem.HasTool(this.intUserID, 1, 100);
                                if (num3 < 1)
                                {
                                    num3 = ToolItem.HasTool(this.intUserID, 1, 0x65);
                                }
                                if (num3 < 1)
                                {
                                    this.btnReg.Visible = false;
                                }
                                str2 = str2 + "持有冠军杯邀请函或冠军杯邀请函（盟）的经理可以在今天报名，如果没有这两种邀请函请明天再来。冠军杯邀请函（盟）可以向盟主索取。";
                            }
                            else
                            {
                                str3 = "center";
                            }
                        }
                        this.sbReturn.Append("<tr>\n");
                        this.sbReturn.Append("<td colspan=3 align=\"" + str3 + "\">" + str2 + "</td>\n");
                        this.sbReturn.Append("</tr>\n");
                        return;
                    }
                    this.tblRegBegin.Visible = true;
                    this.tblRegEnd.Visible = false;
                    this.btnReg.Visible = false;
                    groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(1);
                    int num4 = (byte) groupTeamRowByCID["Status"];
                    int num5 = (byte) groupTeamRowByCID["Round"];
                    DateTime time = (DateTime) groupTeamRowByCID["MatchTime"];
                    bool flag = false;
                    if ((DateTime.Now.Day == time.Day) && (num4 == 3))
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(5);
                        if (groupTeamRowByCID != null)
                        {
                            string str4 = groupTeamRowByCID["LadderURL"].ToString().Trim();
                            num4 = (byte) groupTeamRowByCID["Status"];
                            if (((num4 > 0) && (str4 != "")) && (str4.ToLower().IndexOf("http://match") == -1))
                            {
                                str4 = Config.GetDomain() + str4;
                            }
                            str2 = "冠军杯小组赛结束，当前正停赛一轮安排淘汰赛的赛程，查看赛程<a href='javascript:;' onclick=window.open('" + str4 + "')>请点击此处</a>";
                        }
                    }
                    else if (num4 == 1)
                    {
                        str2 = "冠军杯小组赛进行至第" + num5 + "轮";
                    }
                    else
                    {
                        groupTeamRowByCID = BTPXGameManager.GetLastGameRowByCategory(5);
                        if (groupTeamRowByCID != null)
                        {
                            num4 = (byte) groupTeamRowByCID["Status"];
                            num5 = (byte) groupTeamRowByCID["Round"];
                            int num6 = (int) groupTeamRowByCID["Capacity"];
                            int num7 = 1;
                            string str5 = groupTeamRowByCID["LadderURL"].ToString().Trim();
                            if (((num4 > 0) && (str5 != "")) && (str5.ToLower().IndexOf("http://match") == -1))
                            {
                                str5 = Config.GetDomain() + str5;
                            }
                            if (num4 == 1)
                            {
                                while (num6 > 1)
                                {
                                    num6 /= 2;
                                    num7++;
                                }
                                int num8 = num7 - num5;
                                if (num5 == 1)
                                {
                                    str2 = "现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str5 + "')>淘汰赛</a>";
                                }
                                else if (num8 > 5)
                                {
                                    str2 = string.Concat(new object[] { "冠军杯淘汰赛进行到第", num5, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str5, "')>请点击此处</a>" });
                                }
                                else if (num8 > 2)
                                {
                                    str2 = "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num8).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str5 + "')>请点击此处</a>";
                                }
                                else
                                {
                                    switch (num8)
                                    {
                                        case 1:
                                            str2 = "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str5 + "')>请点击此处</a>";
                                            break;

                                        case 2:
                                            str2 = "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str5 + "')>请点击此处</a>";
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                string str6 = groupTeamRowByCID["ChampionClubName"].ToString().Trim();
                                int num9 = (int) groupTeamRowByCID["ChampionUserID"];
                                str6 = AccountItem.GetNickNameInfo(num9, str6, "Right", 20);
                                if (num9 != this.intUserID)
                                {
                                    str = str2;
                                    str2 = str + "本赛季冠军杯正式结束，" + str6 + "获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str5 + "')>请点击此处</a>";
                                }
                                else
                                {
                                    str2 = str2 + "本赛季冠军杯正式结束，恭喜您获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str5 + "')>请点击此处</a>";
                                }
                            }
                        }
                    }
                    this.strChampionSay = str2;
                    return;
                }
                this.sbReturn.Append("<tr>\n");
                this.sbReturn.Append("<td colspan=3 align=\"center\">" + str13 + "</td>\n");
                this.sbReturn.Append("</tr>\n");
            }
        }

        private void SetKemp()
        {
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPerPage = 8;
            DataTable championCupKemp = BTPXGameManager.GetChampionCupKemp(this.intPage, this.intPerPage);
            if (championCupKemp != null)
            {
                foreach (DataRow row in championCupKemp.Rows)
                {
                    string str;
                    string strNickName = row["ChampionClubName"].ToString().Trim();
                    int intUserID = (int) row["ChampionUserID"];
                    int intUnionID = (int) row["UnionID"];
                    DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(intUnionID);
                    if (unionRowByID != null)
                    {
                        str = unionRowByID["Name"].ToString().Trim();
                    }
                    else
                    {
                        str = "暂无联盟";
                    }
                    string str3 = row["NickName"].ToString().Trim();
                    DateTime datIn = (DateTime) row["MatchTime"];
                    this.sbReturn.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbReturn.Append("\t<td align=\"left\">" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20) + "</td>");
                    this.sbReturn.Append("\t<td align=\"left\">" + AccountItem.GetNickNameInfo(intUserID, str3, "Right", 20) + "</td>");
                    this.sbReturn.Append("\t<td align=\"left\">" + str + "</td>");
                    this.sbReturn.Append("\t<td align=\"left\">" + StringItem.FormatDate(datIn, "yy-MM-dd") + "</td>");
                    this.sbReturn.Append("</tr>");
                    this.sbReturn.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>");
                }
                string strCurrentURL = "ChampionCup.aspx?Tag=" + this.intUserID + "&Kind=KEMP&";
                this.strScript = this.GetScript(strCurrentURL);
                this.sbReturn.Append("<tr><td height='25' align='right' colspan='4'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
            }
            else
            {
                this.sbReturn.Append("<tr><td align=\"center\" colspan=4>暂无记录</td></tr>");
            }
        }

        private void SetTryout()
        {
            int num;
            string str = "";
            string str2 = "";
            DataRow groupTeamRowByCID = BTPXGroupTeamManager.GetGroupTeamRowByCID(this.intClubID5);
            if (groupTeamRowByCID != null)
            {
                num = (int) groupTeamRowByCID["GroupIndex"];
            }
            else
            {
                num = 1;
            }
            int intRound = (byte) BTPXGameManager.GetLastGameRowByCategory(1)["Round"];
            DataTable table = BTPXGroupMatchManager.GetGroupMatchByCategoryGroupIndex(this.intClubID5, 1, num);
            if ((table == null) || (table.Rows.Count == 0))
            {
                this.strMatchList = "<tr class='BarContent'><td height='25' colspan='6'>暂时没有比赛</td></tr>";
            }
            else
            {
                int num3;
                int num4;
                int num5;
                int num6;
                int num7;
                int num8;
                string str3;
                string str4;
                string str5;
                string strUPList;
                foreach (DataRow row2 in table.Rows)
                {
                    string str7;
                    num3 = (byte) row2["Round"];
                    num4 = (int) row2["XGroupMatchID"];
                    num5 = (int) row2["ClubAID"];
                    num7 = (int) row2["ClubBID"];
                    num6 = (int) row2["ClubAScore"];
                    num8 = (int) row2["ClubBScore"];
                    num3 = Convert.ToInt32(row2["Round"]);
                    string strNickName = row2["ClubInfoA"].ToString().Trim();
                    string str9 = row2["ClubInfoB"].ToString().Trim();
                    string[] strArray = strNickName.Split(new char[] { '|' });
                    int intUserID = Convert.ToInt32(strArray[0]);
                    strNickName = strArray[1].Trim();
                    string str10 = strArray[3].Trim();
                    string[] strArray2 = str9.Split(new char[] { '|' });
                    int num10 = Convert.ToInt32(strArray2[0]);
                    str9 = strArray2[1].Trim();
                    string str11 = strArray2[3].Trim();
                    if ((str10 != "") && (str10 != null))
                    {
                        strNickName = str10 + "-" + strNickName;
                    }
                    if ((str11 != "") && (str11 != null))
                    {
                        str9 = str11 + "-" + str9;
                    }
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 0x10);
                    str9 = AccountItem.GetNickNameInfo(num10, str9, "Right", 0x10);
                    if (((num5 != 0) && (num7 != 0)) && ((num6 != 0) || (num8 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str2 = "";
                        if ((num3 == intRound) || (num3 == (intRound + 1)))
                        {
                            string str12 = row2["ArrangeA"].ToString().Trim();
                            string str13 = row2["ArrangeH"].ToString().Trim();
                            if (this.IsMatchH(num5))
                            {
                                if (str13 == "NO")
                                {
                                }
                            }
                            else
                            {
                                bool flag1 = str12 != "NO";
                            }
                            str = "";
                        }
                        else
                        {
                            str = "";
                        }
                    }
                    str5 = str + "&nbsp;&nbsp;" + str2;
                    if ((num6 == 0) && (num8 == 0))
                    {
                        str7 = "--";
                    }
                    else
                    {
                        str7 = string.Concat(new object[] { "<font color='40'>", num6, ":", num8, "</font>" });
                    }
                    string str14 = "";
                    string str15 = "#FBE2D4";
                    string str16 = "BarContent border";
                    if (intRound == num3)
                    {
                        str14 = "#FBE2D4";
                        str15 = "";
                        str16 = "BarContent1 border;background-color:#FBE2D4";
                    }
                    object strMatchList = this.strMatchList;
                    this.strMatchList = string.Concat(new object[] { 
                        strMatchList, "<tr class='", str16, "' onmouseover=\"this.style.backgroundColor='", str15, "'\" onmouseout=\"this.style.backgroundColor='", str14, "'\"><td width=\"50\" height=\"24\" align=\"left\" style=\"padding-left:20px\" ><font color='#7B1F76'>", num3, "</font></td><td width=\"120\" align=\"right\" >", strNickName, "</td><td width=\"80\" align=\"center\" ><font color='40'>", str7, "</font></td><td width=\"120\" align=\"left\" >", str9, "</td><td >&nbsp;</td><td width=\"90\" align=\"left\" >", 
                        str5, "</td></tr>"
                     });
                    this.strMatchList = this.strMatchList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
                DataTable table2 = BTPXGroupMatchManager.ChampionMatchByTurn(this.intClubID5, intRound - 1);
                if (table2 == null)
                {
                    this.strUPList = "<tr class='BarContent'><td height='25'>无比赛</td></tr>";
                }
                else
                {
                    foreach (DataRow row3 in table2.Rows)
                    {
                        num3 = (byte) row3["Round"];
                        num4 = (int) row3["XGroupMatchID"];
                        num5 = (int) row3["ClubAID"];
                        num7 = (int) row3["ClubBID"];
                        num6 = (int) row3["ClubAScore"];
                        num8 = (int) row3["ClubBScore"];
                        string str17 = row3["ClubInfoA"].ToString().Trim();
                        string str18 = row3["ClubInfoB"].ToString().Trim();
                        if (str17.Length > 1)
                        {
                            string[] strArray3 = str17.Split(new char[] { '|' });
                            int num11 = Convert.ToInt32(strArray3[0]);
                            str3 = strArray3[1].Trim();
                            string str19 = strArray3[3].Trim();
                            if (str19 != "")
                            {
                                str3 = str19 + "-" + str3;
                            }
                            str3 = AccountItem.GetNickNameInfo(num11, str3, "Right", 12);
                        }
                        else
                        {
                            str3 = "轮空";
                        }
                        if (str18.Length > 1)
                        {
                            string[] strArray4 = str18.Split(new char[] { '|' });
                            int num12 = Convert.ToInt32(strArray4[0]);
                            str4 = strArray4[1].Trim();
                            string str20 = strArray4[3].Trim();
                            if (str20 != "")
                            {
                                str4 = str20 + "-" + str4;
                            }
                            str4 = AccountItem.GetNickNameInfo(num12, str4, "Right", 12);
                        }
                        else
                        {
                            str4 = "轮空";
                        }
                        if (((num5 != 0) && (num7 != 0)) && ((num6 != 0) || (num8 != 0)))
                        {
                            str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                            str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                        }
                        else
                        {
                            str = "";
                            str2 = "";
                        }
                        if ((num6 == 0) && (num8 == 0))
                        {
                            str5 = "<td width='20' align='center' cospan='3'>--</td>";
                        }
                        else
                        {
                            str5 = string.Concat(new object[] { "<td width='20' align='right'>", num6, "</td><td width='10' align='center'>:</td><td width='20' align='left'>", num8, "</td>" });
                        }
                        strUPList = this.strUPList;
                        this.strUPList = strUPList + "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' width='90' align='right'>" + str3 + "</td>" + str5 + "<td width='90' align='left'>" + str4 + "</td><td width='40'>" + str + "&nbsp;" + str2 + "</td></tr>";
                        this.strUPList = this.strUPList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                    }
                }
                table2 = BTPXGroupMatchManager.ChampionMatchByTurn(this.intClubID5, intRound);
                if (table2 == null)
                {
                    this.strDownList = "<tr class='BarContent'><td height='25'>无比赛</td></tr>";
                }
                else
                {
                    foreach (DataRow row4 in table2.Rows)
                    {
                        num3 = (byte) row4["Round"];
                        num4 = (int) row4["XGroupMatchID"];
                        num5 = (int) row4["ClubAID"];
                        num7 = (int) row4["ClubBID"];
                        num6 = (int) row4["ClubAScore"];
                        num8 = (int) row4["ClubBScore"];
                        string str21 = row4["ClubInfoA"].ToString().Trim();
                        string str22 = row4["ClubInfoB"].ToString().Trim();
                        if (str21.Length > 1)
                        {
                            string[] strArray5 = str21.Split(new char[] { '|' });
                            int num13 = Convert.ToInt32(strArray5[0]);
                            str3 = strArray5[1].Trim();
                            string str23 = strArray5[3].Trim();
                            if (str23 != "")
                            {
                                str3 = str23 + "-" + str3;
                            }
                            str3 = AccountItem.GetNickNameInfo(num13, str3, "Right", 12);
                        }
                        else
                        {
                            str3 = "轮空";
                        }
                        if (str22.Length > 1)
                        {
                            string[] strArray6 = str22.Split(new char[] { '|' });
                            int num14 = Convert.ToInt32(strArray6[0]);
                            str4 = strArray6[1].Trim();
                            string str24 = strArray6[3].Trim();
                            if (str24 != "")
                            {
                                str4 = str24 + "-" + str4;
                            }
                            str4 = AccountItem.GetNickNameInfo(num14, str4, "Right", 12);
                        }
                        else
                        {
                            str4 = "轮空";
                        }
                        if (((num5 != 0) && (num7 != 0)) && ((num6 != 0) || (num8 != 0)))
                        {
                            str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                            str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=4&Tag=", num4, "&A=", num5, "&B=", num7, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                        }
                        else
                        {
                            str = "";
                            str2 = "";
                        }
                        if ((num6 == 0) && (num8 == 0))
                        {
                            str5 = "<td width='20' align='center' cospan='3'>--</td>";
                        }
                        else
                        {
                            str5 = string.Concat(new object[] { "<td width='20' align='right'>", num6, "</td><td width='10' align='center'>:</td><td width='20' align='left'>", num8, "</td>" });
                        }
                        strUPList = this.strDownList;
                        this.strDownList = strUPList + "<tr class='BarContent' bgColor='#FBE2D4' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' width='90' align='right'>" + str3 + "</td>" + str5 + "<td width='90' align='left'>" + str4 + "</td><td >&nbsp;</td><td width='40'>" + str + "&nbsp;" + str2 + "</td></tr>";
                        this.strDownList = this.strDownList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                    }
                }
            }
        }

        private void SetTryoutTop()
        {
            int request = SessionItem.GetRequest("Index", 0);
            if (request == 0)
            {
                DataRow groupTeamRowByCID = BTPXGroupTeamManager.GetGroupTeamRowByCID(this.intClubID5);
                if (groupTeamRowByCID != null)
                {
                    request = (int) groupTeamRowByCID["GroupIndex"];
                    if (request < 1)
                    {
                        request = 1;
                    }
                }
                else
                {
                    request = 1;
                }
            }
            this.intTop = request;
            DataTable groupTeamByCG = BTPXGroupTeamManager.GetGroupTeamByCG(1, request);
            int num2 = 1;
            if ((groupTeamByCG == null) || (groupTeamByCG.Rows.Count == 0))
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>第" + request + "组没有参赛队伍</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height=\"25\" align='center' width=\"88\">排名</td>\t\t<td align='left' width=\"150\">球队</td>\t\t\t<td align='center' width=\"80\">胜</td>\t\t\t<td align='center' width=\"80\">负</td>\t\t\t<td align='center' width=\"80\">净胜</td>\t\t\t<td ></td></tr>";
                foreach (DataRow row2 in groupTeamByCG.Rows)
                {
                    string str;
                    int count = groupTeamByCG.Rows.Count;
                    int num3 = (int) row2["Win"];
                    int num4 = (int) row2["Lose"];
                    int num5 = (int) row2["Score"];
                    int num6 = (int) row2["ClubID"];
                    string strNickName = row2["ClubName"].ToString().Trim();
                    int intUserID = (int) row2["UserID"];
                    int num8 = (int) row2["UnionID"];
                    string str3 = row2["ShortName"].ToString().Trim();
                    if (num8 > 0)
                    {
                        strNickName = str3 + "-" + strNickName;
                    }
                    if (num6 == 0)
                    {
                        strNickName = "空缺";
                    }
                    if (num2 < 3)
                    {
                        str = "#fce5d2";
                    }
                    else
                    {
                        str = "";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' bgColor='", str, "' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='center' width=\"88\"><font color='#660066'>", num2, "</font></td><td  align='left' width=\"150\">", AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 20), "</td><td align='center' width=\"80\">", num3, "</td><td align='center' width=\"80\">", num4, "</td><td align='center' width=\"80\">", num5, "</td><td></td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                    num2++;
                }
            }
        }

        private void SetXCupMatch()
        {
            string str = "";
            string str2 = "";
            int num = 1;
            int request = SessionItem.GetRequest("CID", 0);
            bool flag = false;
            bool flag2 = false;
            this.btnFindMatch.ImageUrl = SessionItem.GetImageURL() + "button_34.gif";
            this.tbClubName.Style.Add("color", "#999999");
            this.tbClubName.Text = "请输入球队名";
            this.tbClubName.Attributes["onfocus"] = "SetText(this)";
            if (request < 1)
            {
                request = this.intClubID5;
                flag = true;
                this.strMyMsg = "";
            }
            DataTable xCupMatchByClubID = BTPXCupMatchManager.GetXCupMatchByClubID(request);
            if (xCupMatchByClubID != null)
            {
                foreach (DataRow row in xCupMatchByClubID.Rows)
                {
                    string str3;
                    string str4;
                    string str5;
                    int num3 = (int) row["XCupMatchID"];
                    int num4 = (int) row["ClubAID"];
                    int num5 = (int) row["ClubBID"];
                    int num6 = (int) row["ScoreA"];
                    int num7 = (int) row["ScoreB"];
                    string str6 = row["ClubInfoA"].ToString().Trim();
                    string str7 = row["ClubInfoB"].ToString().Trim();
                    int num8 = (int) row["ClubOA"];
                    int num9 = (int) row["ClubOB"];
                    if (str6.Length > 1)
                    {
                        string[] strArray = str6.Split(new char[] { '|' });
                        int intUserID = Convert.ToInt32(strArray[0]);
                        str3 = strArray[1].Trim();
                        string str8 = strArray[3].Trim();
                        if (str8 != "")
                        {
                            str3 = str8 + "-" + str3;
                        }
                        str3 = AccountItem.GetNickNameInfo(intUserID, str3, "Right", 12);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (str7.Length > 1)
                    {
                        string[] strArray2 = str7.Split(new char[] { '|' });
                        int num11 = Convert.ToInt32(strArray2[0]);
                        str4 = strArray2[1].Trim();
                        string str9 = strArray2[3].Trim();
                        if (str9 != "")
                        {
                            str4 = str9 + "-" + str4;
                        }
                        str4 = AccountItem.GetNickNameInfo(num11, str4, "Right", 12);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num8 > 0) && (num9 > 0))
                    {
                        flag2 = true;
                        int num12 = (int) row["OScoreA"];
                        int num1 = (int) row["OScoreB"];
                        if ((num12 > 0) && (num12 > 0))
                        {
                            str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=5&Tag=", num3, "&A=", num8, "&B=", num9, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo2.gif' border='0' width='13' height='13'></a>" });
                            str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=5&Tag=", num3, "&A=", num8, "&B=", num9, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo2.gif' border='0' width='13' height='13'></a>" });
                        }
                        else
                        {
                            str = "";
                            str2 = "";
                        }
                        if (flag)
                        {
                            DataRow row2 = BTPXGameManager.GetLastGameRowByCategory(5);
                            if (row2 != null)
                            {
                                int num13 = (byte) row2["Round"];
                                this.strMyMsg = "恭喜您通过淘汰赛第" + (num13 - 1) + "轮，您可以点击下轮对手后方的图标查看他上一轮的战报，也可以在下方输入球队名称查看该球队的历史赛程。查看淘汰赛的全部请点击右下角的全部赛程按钮。";
                            }
                        }
                    }
                    else if (((num4 != 0) && (num5 != 0)) && ((num6 != 0) || (num7 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=5&Tag=", num3, "&A=", num4, "&B=", num5, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=5&Tag=", num3, "&A=", num4, "&B=", num5, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str = "";
                        str2 = "";
                    }
                    if ((num6 == 0) && (num7 == 0))
                    {
                        str5 = "--";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num6, ":", num7, "</font>" });
                    }
                    object strMatchList = this.strMatchList;
                    this.strMatchList = string.Concat(new object[] { 
                        strMatchList, "<tr class='BarContent1 border;background-color:#FBE2D4' onmouseover=\"this.style.backgroundColor='FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width=\"50\" height=\"24\" align=\"left\" style=\"padding-left:20px\" ><font color='#7B1F76'>", num, "</font><a name=\"", num, "\" id=\"", num, "\"></a></td><td width=\"120\" align=\"right\" >", str3, "</td><td width=\"80\" align=\"center\" ><font color='40'>", str5, "</font></td><td width=\"120\" align=\"left\" >", str4, "</td><td >&nbsp;</td><td width=\"90\" align=\"left\" >", str, str2, 
                        "</td></tr>"
                     });
                    this.strMatchList = this.strMatchList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                    num++;
                }
                if (!flag2 && (this.intClubID5 == request))
                {
                    DataRow xCupRegRowByClubID = BTPXCupRegManager.GetXCupRegRowByClubID(this.intClubID5);
                    int num14 = 0;
                    string str10 = "0";
                    string str11 = "0";
                    if (xCupRegRowByClubID != null)
                    {
                        num14 = (byte) xCupRegRowByClubID["DeadRound"];
                        xCupRegRowByClubID = BTPXGameManager.GetLastGameRowByCategory(5);
                        string line = xCupRegRowByClubID["RewardXML"].ToString().Trim();
                        if (line.ToLower().IndexOf("http://match") == -1)
                        {
                            line = Config.GetDomain() + line;
                        }
                        this.strXML = line;
                        byte num16 = (byte) xCupRegRowByClubID["Round"];
                        TagReader reader = new TagReader();
                        IEnumerator enumerator = reader.GetItems(line, "<Reward>", "</Reward>").GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            string current = (string) enumerator.Current;
                            if (Convert.ToInt32(reader.GetTagline(current, "<Round>", "</Round>")) == num14)
                            {
                                str10 = reader.GetTagline(current, "<Money>", "</Money>");
                                str11 = reader.GetTagline(current, "<Reputation>", "</Reputation>");
                            }
                        }
                        if (num14 == 100)
                        {
                            this.strMyMsg = "恭喜您获得了总冠军，得到" + str10 + "资金和" + str11 + "点球队威望。现在您可以查看自己的历史战报，也可以在下方输入球队名点击查看关注其他球队的战况。查看全部赛程请点击画面右下角的全部赛程按钮。";
                        }
                        else
                        {
                            this.strMyMsg = string.Concat(new object[] { "您不幸在第", num14, "轮被淘汰，得到", str10, "资金和", str11, "点球队威望。现在您可以查看自己的历史战报，也可以在下方输入球队名点击查看关注其他球队的战况。查看全部赛程请点击画面右下角的全部赛程按钮。" });
                        }
                    }
                }
            }
            else if (BTPXCupRegManager.GetXCupRegRowByClubID(this.intClubID5) != null)
            {
                this.strMatchList = this.strMatchList + "<tr><td height=24' align='center' colspan='6'>冠军杯淘汰赛还未开始</td></tr>";
            }
            else
            {
                this.strMatchList = this.strMatchList + "<tr><td height=24' align='center' colspan='6'>未参加冠军杯淘汰赛</td></tr>";
            }
            DataRow lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
            if (lastGameRowByCategory != null)
            {
                string str14 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                int num15 = (byte) lastGameRowByCategory["Status"];
                if (((num15 > 0) && (str14 != "")) && (str14.ToLower().IndexOf("http://match") == -1))
                {
                    str14 = Config.GetDomain() + str14;
                }
                this.strXML = str14;
            }
        }
    }
}


namespace Web
{
    using LoginParameter;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class DevCup : Page
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
        public int intDevCupID;
        public int intGrade;
        public int intPage = 0;
        public int intPrePage = 5;
        private int intUnionID;
        private int intUserID;
        private int intWealth;
        public int intWealthCost;
        public int intWealthCostM;
        protected RadioButtonList rblSize;
        public StringBuilder sbCupRegManagePage = new StringBuilder();
        public StringBuilder sbCupRegManager = new StringBuilder();
        public StringBuilder sbDevCupChampion = new StringBuilder("");
        public StringBuilder sbDevCupChampionPage = new StringBuilder("");
        public StringBuilder sbDevCupList = new StringBuilder("");
        public StringBuilder sbDevCupManageList = new StringBuilder();
        public StringBuilder sbDevCupStats = new StringBuilder("");
        public StringBuilder sbDevCupStatshPage = new StringBuilder("");
        public StringBuilder sbLookList = new StringBuilder("");
        public StringBuilder sbLookListPage = new StringBuilder("");
        public StringBuilder sbScript = new StringBuilder("");
        public StringBuilder sbWealthPage = new StringBuilder("");
        public string strBeginTime;
        public string strBigLogo = "";
        private string strClubLogo;
        private string strClubName;
        public string strCount;
        public string strCupName;
        private string strDevCode;
        public string strDevCupName;
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
        public string strUnionName;
        protected TextBox tbCupName;
        protected HtmlTable tbDevCupChampion;
        protected TextBox tbDevCupIntro;
        protected HtmlTable tbDevCupStats;
        protected TextBox tbEndTime;
        protected TextBox tbIntroM;
        protected HtmlTable tblCreate;
        protected HtmlTable tblCreateDevCup;
        protected HtmlTable tblCupRegManager;
        protected HtmlTable tblManager;
        protected HtmlTable tblModifyDevCup;
        protected HtmlTable tbLookList;
        protected HtmlTable tblPassWord;
        protected HtmlTable tblUMatchAdd;
        protected HtmlTable tblUMatchList;
        protected TextBox tbPassWord;
        protected TextBox tbPasswordM;
        protected TextBox tbRegPassword;

        private void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            string str = "0";
            string validWords = StringItem.GetValidWords(this.tbCupName.Text.ToString().Trim());
            if (!StringItem.IsValidName(validWords, 1, 12))
            {
                this.strErrMsg = "请正确填写杯赛名称。";
            }
            else
            {
                int num = (int) BTPGameManager.GetGameRow()["DevLevelSum"];
                int num2 = Convert.ToInt32(this.ddlClubLevel.SelectedItem.Value.ToString());
                if ((num2 > num) || (num2 < 0))
                {
                    num2 = 0;
                }
                int intRegCharge = Convert.ToInt32(this.ddlRegCharge.SelectedValue.ToString());
                if (intRegCharge < 0)
                {
                    intRegCharge = 0;
                }
                if (intRegCharge > 500)
                {
                    intRegCharge = 500;
                }
                string str3 = this.tbRegPassword.Text.ToString().Trim();
                if ((str3 != "") && (!StringItem.IsNumber(str3) || (StringItem.GetStrLength(str3) != 4)))
                {
                    this.strErrMsg = "请正确填写密码。";
                }
                else
                {
                    DateTime time;
                    int intRegClub = 0;
                    int intUserID = this.intUserID;
                    try
                    {
                        time = Convert.ToDateTime(this.tbEndTime.Text);
                    }
                    catch
                    {
                        this.strErrMsg = "请按照正确格式填写报名截止时间。";
                        return;
                    }
                    if (time <= DateTime.Now.AddDays(5.0))
                    {
                        if (time < DateTime.Now.AddDays(1.0))
                        {
                            this.strErrMsg = "报名截止时间最少1天后。";
                        }
                        else if (time.Hour < 10)
                        {
                            this.strErrMsg = "截止时间必须在每日的10点之后。";
                        }
                        else
                        {
                            int num6 = Convert.ToInt32(this.ddlHortation.SelectedValue.ToString());
                            int num7 = Convert.ToInt32(this.ddlHortation1.SelectedValue.ToString());
                            int num8 = Convert.ToInt32(this.ddlHortation2.SelectedValue.ToString());
                            int num9 = Convert.ToInt32(this.ddlHortation3.SelectedValue.ToString());
                            string strIn = StringItem.GetValidWords(StringItem.GetHtmlEncode(this.tbDevCupIntro.Text.ToString().Trim()));
                            if (!StringItem.IsValidContent(strIn, 1, 400))
                            {
                                this.strErrMsg = "请正确填写杯赛介绍，要求1至200汉字之间。";
                            }
                            else
                            {
                                string str5;
                                int intCupSize = Convert.ToInt32(this.rblSize.SelectedValue.ToString());
                                int intMedalCharge = 0;
                                int intCreateCharge = 0;
                                if (intRegClub == 1)
                                {
                                    str5 = "<UnionID>" + intUserID + "</UnionID>";
                                }
                                else
                                {
                                    str5 = "";
                                }
                                object obj2 = str5;
                                str5 = string.Concat(new object[] { obj2, "<DevLvl>", num2, "</DevLvl>" });
                                string strRewardXML = string.Concat(new object[] { "<Reward><Place>1</Place><Wealth>", num6, "</Wealth></Reward><Reward><Place>2</Place><Wealth>", num7, "</Wealth></Reward><Reward><Place>3</Place><Wealth>", num8, "</Wealth></Reward><Reward><Place>4</Place><Wealth>", num9, "</Wealth></Reward>" });
                                string strLogo = str + ".gif";
                                int intHortationAll = ((num6 + num7) + (num8 * 2)) + (num9 * 4);
                                string str8 = StringItem.FormatDate(DateTime.Now, "yyyyMM");
                                string strCupLadder = Config.GetDomain() + "DevCupLadder/" + str8 + "/";
                                DevCupData data = new DevCupData(intUserID, intRegClub, validWords, str3, strIn, intRegCharge, strLogo, str5, strRewardXML, intCupSize, time, intCreateCharge, intMedalCharge, intHortationAll, strCupLadder);
                                this.Session["DevCup" + this.intUserID] = data;
                                base.Response.Redirect("SecretaryPage.aspx?Type=CREATEUSERCUP");
                            }
                        }
                    }
                    else
                    {
                        this.strErrMsg = "报名截止时间只能设置在5天以内。";
                    }
                }
            }
        }

        private void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            string line = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID)["RequirementXML"].ToString().Trim();
            TagReader reader = new TagReader();
            string str2 = reader.GetTagline(line, "<DevLvl>", "</DevLvl>").ToString().Trim();
            reader.GetTagline(line, "<UnionID>", "</UnionID>").ToString().Trim();
            int intIsSelfUnion = 0;
            string strRequirement = "<DevLvl>" + str2 + "</DevLvl>";
            string str4 = this.tbPasswordM.Text.ToString().Trim();
            if ((str4 != "") && (!StringItem.IsNumber(str4) || (StringItem.GetStrLength(str4) != 4)))
            {
                this.strErrMsg = "请正确填写密码。";
            }
            else
            {
                string validWords = StringItem.GetValidWords(StringItem.GetHtmlEncode(this.tbIntroM.Text.ToString().Trim()));
                if (!StringItem.IsValidContent(validWords, 1, 400))
                {
                    this.strErrMsg = "请正确填写杯赛介绍，要求1至200汉字之间。";
                }
                else
                {
                    switch (BTPDevCupManager.ModifyUserDevCup(this.intUserID, this.intDevCupID, intIsSelfUnion, str4, validWords, strRequirement))
                    {
                        case 1:
                            this.strErrMsg = "您不是杯赛创建人，不能修改该杯赛信息。";
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=627!Type.CREATEDEVCUP^Status.MANAGER");
                            return;

                        case 3:
                            this.strErrMsg = "杯赛不存在，请查询后再做更改。";
                            return;
                    }
                    this.strErrMsg = "杯赛修改出错，请确认无误后再修改。";
                }
            }
        }

        private bool CanRegDevCup(string strRequirementXML, int intUnionID, string strDevCode, int intWealth, int intSetID)
        {
            int num2;
            TagReader reader = new TagReader();
            string str = reader.GetTagline(strRequirementXML, "<DevLvl>", "</DevLvl>").ToString().Trim();
            string str2 = reader.GetTagline(strRequirementXML, "<UnionID>", "</UnionID>").ToString().Trim();
            int num = 0;
            if (str.IndexOf("|") >= 0)
            {
                string[] strArray = new string[] { str.Substring(0, str.IndexOf("|")), str.Substring(str.IndexOf("|") + 1) };
                num = Convert.ToInt32(strArray[0]);
                num2 = Convert.ToInt32(strArray[1]);
            }
            else
            {
                num2 = Convert.ToInt32(str);
            }
            int level = DevCalculator.GetLevel(strDevCode);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intWealth = (int) accountRowByUserID["Wealth"];
            this.intUnionID = (int) accountRowByUserID["UnionID"];
            if (intWealth > this.intWealth)
            {
                base.Response.Redirect("Report.aspx?Parameter=607");
                return false;
            }
            if ((str2 != "") && (str2 != this.intUnionID.ToString().Trim()))
            {
                base.Response.Redirect("Report.aspx?Parameter=608");
                return false;
            }
            if (BTPDevCupRegManager.CheckDevCupReg(this.intUserID, intSetID) && (intSetID > 0))
            {
                base.Response.Redirect("Report.aspx?Parameter=622");
                return false;
            }
            if (num == 0)
            {
                if (num2 > level)
                {
                    base.Response.Redirect("Report.aspx?Parameter=609");
                    return false;
                }
                return true;
            }
            if ((level >= num) && (level <= num2))
            {
                return true;
            }
            base.Response.Redirect("Report.aspx?Parameter=609");
            return false;
        }

        private void CupRegManager()
        {
            this.intPrePage = 10;
            int request = (int) SessionItem.GetRequest("DevCupID", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "DevCup.aspx?Type=CREATEDEVCUP&Status=CUPREGMANAGE&DevCupID=" + request + "&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbCupRegManagePage.Append(this.GetViewPage(strCurrentURL));
            DataTable reader = BTPDevCupRegManager.GetDevCupTableByDevCupID(0, this.intPage, this.intPrePage, request);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    int intUserID = (int)row["UserID"];
                    string strNickName = row["NickName"].ToString().Trim();
                    string str3 = row["ClubName"].ToString().Trim();
                    bool blnSex = (bool)row["Sex"];
                    string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                    this.sbCupRegManager.Append("<tr><td height=\"25\" style=\"padding-left:4px\" algin=\"align\" >" + AccountItem.GetNickNameInfo(intUserID, str3, "Right") + "</td>");
                    this.sbCupRegManager.Append("<td style=\"padding-left:4px\" align=\"left\">" + AccountItem.GetNickNameInfoA(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbCupRegManager.Append("<td align=\"center\">" + dev + "</td>");
                    this.sbCupRegManager.Append(string.Concat(new object[] { "<td align=\"center\"><a href='SecretaryPage.aspx?Type=KICKOUTUSERDEVCUP&DevCupID=", request, "&UserID=", intUserID, "'>踢出</a></td></tr>" }));
                    this.sbCupRegManager.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>");
                }
            }
            //reader.Close();
            if ((this.sbCupRegManager.ToString().Trim() == "") || (this.sbCupRegManager == null))
            {
                this.sbCupRegManager.Append("<tr><td height=\"25\" align=\"center\" style=\"padding-left:4px\" colspan=\"4\">暂无人员报名</td></tr>");
            }
        }

        private void DevCupChampionList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "DevCup.aspx?Type=DEVCUPCHAMPION&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbDevCupChampionPage.Append(this.GetViewPage(strCurrentURL));
            DataTable reader = BTPDevCupManager.GetDevCupTableByPage(0, this.intPage, this.intPrePage, 3);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str5;
                    int num1 = (int)row["DevCupID"];
                    int num = (int)row["RegCount"];
                    int num2 = (int)row["Capacity"];
                    num = (int)row["RegCount"];
                    string str2 = row["ChampionClubName"].ToString().Trim();
                    string str3 = row["Name"].ToString().Trim();
                    bool flag = (bool)row["IsSelfUnion"];
                    DateTime datIn = (DateTime)row["MatchTime"];
                    int num3 = (int)row["ChampionUserID"];
                    string str6 = row["LadderURL"].ToString().Trim();
                    string str4 = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/" + row["BigLogo"].ToString().Trim() + "'>";
                    if (flag)
                    {
                        str5 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='10' width='9' border='0'>";
                    }
                    else
                    {
                        str5 = "";
                    }
                    this.sbDevCupChampion.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\" width=\"50\">" + str4 + "</td>");
                    this.sbDevCupChampion.Append("<td width=\"107\" align=\"left\" style='padding-left:4px'><font color=#3333>" + str3 + "</font></td>");
                    this.sbDevCupChampion.Append("<td width=20>" + str5 + "</td>");
                    this.sbDevCupChampion.Append(string.Concat(new object[] { "<td width=\"90\" align=\"center\"><font color=#3333>", num, "/", num2, "</font></td>" }));
                    this.sbDevCupChampion.Append("<td  align=\"center\" width=\"80\">" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</td>");
                    this.sbDevCupChampion.Append("<td align=\"center\" width=\"50\"><a href='" + str6 + "'>赛程</a></td>");
                    this.sbDevCupChampion.Append("<td align=\"left\" style='padding-left:4px' width=\"136\"><font color='#fc5402'>" + str2 + "</font></td></tr>");
                    this.sbDevCupChampion.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            //reader.Close();
            if (this.sbDevCupChampion.ToString().Trim() == "")
            {
                this.sbDevCupChampion.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有自定义冠军榜</td></tr>");
            }
            this.sbDevCupChampion.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbDevCupChampionPage.ToString() + "</td></tr>");
        }

        private void DevCupList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "DevCup.aspx?Type=DEVCUPREG&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbWealthPage.Append(this.GetViewPage(strCurrentURL));
            DataTable reader = BTPDevCupManager.GetDevCupTableByPage(0, this.intPage, this.intPrePage, 0);
            string str2 = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["DevCupIDs"].ToString();
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str5;
                    int num4 = (int)row["DevCupID"];
                    int num = (int)row["RegCount"];
                    int num2 = (int)row["Capacity"];
                    num = (int)row["RegCount"];
                    int num1 = (int)row["UnionID"];
                    int num3 = (int)row["WealthCost"];
                    bool flag = (bool)row["IsSelfUnion"];
                    string str3 = row["Name"].ToString().Trim();
                    string str4 = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/" + row["BigLogo"].ToString().Trim() + "' height='50' width='40' border='0'>";
                    if (flag)
                    {
                        str5 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='9' width='10' border='0'>";
                    }
                    else
                    {
                        str5 = "";
                    }
                    string str6 = row["UnionName"].ToString().Trim();
                    string str7 = row["ShortName"].ToString().Trim();
                    int num5 = (int)row["UserID"];
                    if (num5 > 0)
                    {
                        //str6 = str6;
                    }
                    else
                    {
                        str6 = str6 + "[" + str7 + "]";
                    }
                    this.sbDevCupList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\">" + str4 + "</td>");
                    this.sbDevCupList.Append(string.Concat(new object[] { "<td align=\"left\" style='padding-left:4px'><a href='DevCup.aspx?Type=", this.strType, "&Grade=3&DevCupID=", num4, "'><font color=#3333>", str3, "</font></a></td>" }));
                    this.sbDevCupList.Append("<td>" + str5 + "</td>");
                    this.sbDevCupList.Append(string.Concat(new object[] { "<td align=\"center\"><font color=#3333>", num, "/", num2, "</font></td>" }));
                    this.sbDevCupList.Append("<td align=\"center\"><font color=#3333>" + str6 + "</font></td>");
                    this.sbDevCupList.Append("<td align=\"center\"><font color=#3333>" + num3 + "</font></td>");
                    if (str2.IndexOf(num4.ToString()) < 0)
                    {
                        this.sbDevCupList.Append(string.Concat(new object[] { "<td align=\"center\"><a href='DevCup.aspx?Type=DEVCUPREG&Grade=1&DevCupID=", num4, "'>报名</a>| <a href='DevCup.aspx?Type=DEVCUPREG&Grade=2&DevCupID=", num4, "'>查看</a></td></tr>" }));
                    }
                    else
                    {
                        this.sbDevCupList.Append("<td align=\"center\"><font color=#3333 >报名</font>| <a href='DevCup.aspx?Type=DEVCUPREG&Grade=2&DevCupID=" + num4 + "'>查看</a></td></tr>");
                    }
                    this.sbDevCupList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            //reader.Close();
            if (this.sbDevCupList.ToString().Trim() == "")
            {
                this.sbDevCupList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有自定义杯赛</td></tr>");
            }
            this.sbDevCupList.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbWealthPage.ToString() + "</td></tr>");
        }

        private void DevCupLookList()
        {
            int request = (int) SessionItem.GetRequest("DevCupID", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 13;
            string strCurrentURL = "DevCup.aspx?Type=DEVCUPSTATS&Grade=1&DevCupID=" + request + "&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbLookListPage.Append(this.GetViewPage(strCurrentURL));
            DataTable reader = BTPDevCupRegManager.GetDevCupTableByDevCupID(0, this.intPage, this.intPrePage, request);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    int intUserID = (int)row["UserID"];
                    string strNickName = row["NickName"].ToString().Trim();
                    string str3 = row["ClubName"].ToString().Trim();
                    row["ShortName"].ToString().Trim();
                    string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                    this.sbLookList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td align=\"left\" width=\"120\" height=\"25\" style='padding-left:5px;'>" + AccountItem.GetNickNameInfo(intUserID, str3, "Right") + "</td>");
                    this.sbLookList.Append("<td align=\"left\" style='padding-left:5px;'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right") + "</td>");
                    this.sbLookList.Append("<td align=\"center\">" + dev + "</td></tr>");
                }
            }
            //reader.Close();
            if (this.sbLookList.ToString().Trim() == "")
            {
                this.sbLookList.Append("<tr><td colspan=7 align=\"center\" height=\"25\">暂时没有经理报名</td></tr>");
            }
            this.sbLookList.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbLookListPage.ToString() + "</td></tr>");
        }

        private void DevCupStatsList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string strCurrentURL = "DevCup.aspx?Type=DEVCUPSTATS&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbDevCupStatshPage.Append(this.GetViewPage(strCurrentURL));
            DataTable reader = BTPDevCupManager.GetDevCupTableByUserID(0, this.intPage, this.intPrePage, this.intUserID);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str4;
                    int intDevCupID = (int)row["DevCupID"];
                    int num = (int)row["RegCount"];
                    int num2 = (int)row["Capacity"];
                    int num1 = (int)row["UnionID"];
                    int num4 = Convert.ToInt32(row["Round"]);
                    int num6 = Convert.ToInt32(row["Status"]);
                    DateTime datIn = (DateTime)row["MatchTime"];
                    bool flag = (bool)row["IsSelfUnion"];
                    string str2 = row["Name"].ToString().Trim();
                    string str3 = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/" + row["BigLogo"].ToString().Trim() + "'>";
                    if (flag)
                    {
                        str4 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='10' width='9' border='0'>";
                    }
                    else
                    {
                        str4 = "";
                    }
                    string str5 = row["LadderURL"].ToString().Trim();
                    int num5 = Convert.ToInt32(BTPDevCupRegManager.GetRegRowByDevCupIDUserID(intDevCupID, this.intUserID)["DeadRound"]);
                    this.sbDevCupStats.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height=\"60\" width=\"50\">" + str3 + "</td>");
                    this.sbDevCupStats.Append("<td width=\"75\" align=\"left\" style='padding-left:4px'><font color=#3333>" + str2 + "</font></td>");
                    this.sbDevCupStats.Append("<td width=20>" + str4 + "</td>");
                    this.sbDevCupStats.Append(string.Concat(new object[] { "<td width=\"90\" align=\"center\"><font color=#3333>", num, "/", num2, "</font></td>" }));
                    if (num5 == 100)
                    {
                        num5 = num4;
                    }
                    if (num5 < num4)
                    {
                        switch (num6)
                        {
                            case 2:
                            case 3:
                                this.sbDevCupStats.Append(string.Concat(new object[] { "<td width=\"120\" align=\"center\"><font color='red'><a title='淘汰轮数' style='cursor:hand;'>", num5, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num4, "</a></font></td>" }));
                                goto Label_03CD;
                        }
                        this.sbDevCupStats.Append(string.Concat(new object[] { "<td width=\"120\" align=\"center\"><font color='red'><a title='淘汰轮数' style='cursor:hand;'>", num5, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num4, "</a></font></td>" }));
                    }
                    else
                    {
                        switch (num6)
                        {
                            case 2:
                            case 3:
                                this.sbDevCupStats.Append(string.Concat(new object[] { "<td width=\"120\" align=\"center\"><font color='green'><a title='参赛轮数' style='cursor:hand;'>", num4, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num4, "</a></font></td>" }));
                                goto Label_03CD;
                        }
                        this.sbDevCupStats.Append(string.Concat(new object[] { "<td width=\"120\" align=\"center\"><font color='green'><a title='参赛轮数' style='cursor:hand;'>", num4, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num4, "</a></font></td>" }));
                    }
                Label_03CD:
                    switch (num6)
                    {
                        case 0:
                            this.sbDevCupStats.Append("<td width=\"80\" align=\"center\"><a title='报名截止时间' style='CURSOR: hand'><font color=#3333>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</a></font></td>");
                            break;

                        case 1:
                            this.sbDevCupStats.Append("<td width=\"80\" align=\"center\"><a title='下轮比赛时间' style='CURSOR: hand'><font color=#3333>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</a></font></td>");
                            break;

                        default:
                            this.sbDevCupStats.Append("<td width=\"80\" align=\"center\"><font color=#3333>已结束</font></td>");
                            break;
                    }
                    if (num6 == 0)
                    {
                        this.sbDevCupStats.Append(string.Concat(new object[] { "<td align=\"center\"><a href='DevCup.aspx?Type=DEVCUPSTATS&Grade=1&DevCupID=", intDevCupID, "'>查看</a> | <a href='DevCup.aspx?Type=DEVCUPSTATS&Grade=2&DevCupID=", intDevCupID, "'>介绍</a></td></tr>" }));
                    }
                    else
                    {
                        this.sbDevCupStats.Append(string.Concat(new object[] { "<td align=\"center\"><a href='", str5, "'>赛程</a> | <a href='DevCup.aspx?Type=DEVCUPSTATS&Grade=2&DevCupID=", intDevCupID, "'>介绍</a></td></tr>" }));
                    }
                    this.sbDevCupStats.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            //reader.Close();
            if (this.sbDevCupStats.ToString().Trim() == "")
            {
                this.sbDevCupStats.Append("<tr><td colspan=7 align=\"center\" height=\"25\">没有报名的自定义杯赛</td></tr>");
            }
            this.sbDevCupStats.Append("<tr><td align=\"right\" colSpan=\"7\">" + this.sbDevCupStatshPage.ToString() + "</td></tr>");
        }

        private void GetCupList()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            string str3 = "";
            string strCurrentURL = "DevCup.aspx?Type=CREATEDEVCUP&Status=MANAGER&";
            this.sbScript.Append(this.GetScript(strCurrentURL));
            this.sbDevCupManageList.Append("<table width=\"536\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbDevCupManageList.Append("<tr class=\"BarHead\">");
            this.sbDevCupManageList.Append("<td width=\"50\" height=\"25\">奖章</td>");
            this.sbDevCupManageList.Append("<td width=\"100\">杯赛名称</td>");
            this.sbDevCupManageList.Append("<td width=\"15\"></td>");
            this.sbDevCupManageList.Append("<td width=\"80\">报名/名额</td>");
            this.sbDevCupManageList.Append("<td width=\"100\">报名游戏币</td>");
            this.sbDevCupManageList.Append("<td width=\"70\">状态</td>");
            this.sbDevCupManageList.Append("<td width=\"130\">操作</td>");
            this.sbDevCupManageList.Append("</tr>");
            DataTable reader = BTPDevCupManager.GetUserDevCupTable(this.intUserID, this.intPage, this.intPrePage, 0);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str4;
                    string str = row["Name"].ToString().Trim();
                    bool flag = (bool) row["IsSelfUnion"];
                    int num = (int) row["RegCount"];
                    int num2 = (int) row["Capacity"];
                    int num3 = (byte) row["Status"];
                    int num4 = (int) row["WealthCost"];
                    int num5 = (int) row["DevCupID"];
                    string str2 = row["BigLogo"].ToString().Trim();
                    int num6 = (byte) row["Round"];
                    if (flag)
                    {
                        string text1 = "<img src='" + SessionItem.GetImageURL() + "UnionLogo/Lock.gif' alt='仅本盟经理可报名' height='10' width='9' border='0'>";
                    }
                    switch (num3)
                    {
                        case 0:
                            str3 = "报名中";
                            break;

                        case 1:
                            str3 = "比赛中<br>[<font color='green'>" + num6 + "</font>轮]";
                            break;

                        case 2:
                            str3 = "比赛结束";
                            break;

                        case 3:
                            str3 = "奖励结束";
                            break;
                    }
                    if (num3 == 0)
                    {
                        str4 = string.Concat(new object[] { "<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=MODIFYDEVCUP&DevCupID=", num5, "'>修改</a> | <a href='DevCup.aspx?Type=CREATEDEVCUP&Status=CUPREGMANAGE&DevCupID=", num5, "'>人员</a> | <a href='SecretaryPage.aspx?Type=DELUSERDEVCUP&DevCupID=", num5, "'>删除</a>" });
                    }
                    else
                    {
                        str4 = "-- | -- | --";
                    }
                    this.sbDevCupManageList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbDevCupManageList.Append("<td height=\"55\"><img src=\"Images/DevMedal/" + str2 + "\" height=\"50\" width=\"40\" border=\"0\"></td>");
                    this.sbDevCupManageList.Append("<td align='left' style='padding-left:4px'>" + str + "</td>");
                    this.sbDevCupManageList.Append("<td></td>");
                    this.sbDevCupManageList.Append(string.Concat(new object[] { "<td>", num, "/", num2, "</td>" }));
                    this.sbDevCupManageList.Append("<td>" + num4 + "</td>");
                    this.sbDevCupManageList.Append("<td>" + str3 + "</td>");
                    this.sbDevCupManageList.Append("<td>" + str4 + "</td>");
                    this.sbDevCupManageList.Append("</tr>");
                    this.sbDevCupManageList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            else
            {
                this.sbDevCupManageList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbDevCupManageList.Append("<td height=\"55\" colspan=\"7\">暂无自定义杯赛</td>");
                this.sbDevCupManageList.Append("</tr>");
            }
            //reader.Close();
            this.sbDevCupManageList.Append("<tr>");
            this.sbDevCupManageList.Append("<td height=\"25\" colspan=\"7\" align=\"right\" style=\"padding-right:5px\">" + this.GetViewPage(strCurrentURL) + "</td>");
            this.sbDevCupManageList.Append("</tr>");
            this.sbDevCupManageList.Append("</table>");
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int request = (int) SessionItem.GetRequest("Grade", 0);
            int devCupCount = 0;
            if (this.strType == "DEVCUPREG")
            {
                devCupCount = BTPDevCupManager.GetDevCupCount(0);
                if (request == 2)
                {
                    int intDevCupID = (int) SessionItem.GetRequest("DevCupID", 0);
                    devCupCount = BTPDevCupRegManager.GetDevCupUserCount(intDevCupID);
                }
                return devCupCount;
            }
            if (this.strType == "DEVCUPSTATS")
            {
                if (request < 1)
                {
                    return BTPDevCupManager.GetDevCupAccountByUserID(this.intUserID);
                }
                int num4 = (int) SessionItem.GetRequest("DevCupID", 0);
                return BTPDevCupRegManager.GetDevCupUserCount(num4);
            }
            if (this.strType != "DEVCUPCHAMPION")
            {
                if (this.strStatus == "MANAGER")
                {
                    return BTPDevCupManager.GetUserDevCupCount(this.intUserID, 0, 0, 1);
                }
                if (this.strStatus == "CUPREGMANAGE")
                {
                    int num5 = (int) SessionItem.GetRequest("DevCupID", 0);
                    return BTPDevCupRegManager.GetDevCupUserCount(num5);
                }
            }
            return BTPDevCupManager.GetDevCupCount(3);
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
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            obj2 = str2 + " " + str3 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str4 });
        }

        private void IBtnAddUMatch_Click(object sender, ImageClickEventArgs e)
        {
            int request = (int) SessionItem.GetRequest("DevCupID", 0);
            DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(request);
            string line = devCupRowByDevCupID["RequirementXML"].ToString().Trim();
            int intWealth = (int) devCupRowByDevCupID["WealthCost"];
            int intSetID = (int) devCupRowByDevCupID["SetID"];
            string strPassWord = this.tbPassWord.Text.Trim();
            TagReader reader = new TagReader();
            reader.GetTagline(line, "<DevLvl>", "</DevLvl>").ToString().Trim();
            int intRank = 0;
            int level = DevCalculator.GetLevel(BTPDevManager.GetDevCodeByUserID(this.intUserID));
            if (this.CanRegDevCup(line, this.intUnionID, this.strDevCode, intWealth, intSetID))
            {
                if ((this.intCategory != 2) && (this.intCategory != 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=530");
                }
                else if (request > 0)
                {
                    switch (BTPDevCupRegManager.AddDevCupReg(request, this.intUserID, this.intClubID, this.strClubName, this.strShortName, this.strDevCode, this.strClubLogo, intRank, level, strPassWord))
                    {
                        case 1:
                            base.Response.Redirect("Report.aspx?Parameter=522!Type.DEVCUPREG");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=523");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=525");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=526");
                            return;

                        case 5:
                            base.Response.Redirect("Report.aspx?Parameter=528");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=530");
                            return;

                        case 7:
                            base.Response.Redirect("Report.aspx?Parameter=532");
                            return;

                        case 8:
                            base.Response.Redirect("Report.aspx?Parameter=535");
                            return;
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=529");
                }
            }
        }

        private void IBtnBreak_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("DevCup.aspx?Type=" + this.strType);
        }

        private void InitializeComponent()
        {
            this.IBtnAddUMatch.Click += new ImageClickEventHandler(this.IBtnAddUMatch_Click);
            this.IBtnBreak.Click += new ImageClickEventHandler(this.IBtnBreak_Click);
            this.btnCreate.Click += new ImageClickEventHandler(this.btnCreate_Click);
            this.btnModify.Click += new ImageClickEventHandler(this.btnModify_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void ModifyDevCup()
        {
            this.intDevCupID = (int) SessionItem.GetRequest("DevCupID", 0);
            TagReader reader = new TagReader();
            if (!base.IsPostBack)
            {
                DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(this.intDevCupID);
                if (devCupRowByDevCupID == null)
                {
                    this.btnModify.Visible = false;
                }
                else
                {
                    devCupRowByDevCupID["BigLogo"].ToString().Trim();
                    string str = devCupRowByDevCupID["Name"].ToString().Trim();
                    string line = devCupRowByDevCupID["RequirementXML"].ToString().Trim();
                    string str3 = devCupRowByDevCupID["Introduction"].ToString().Trim();
                    string str4 = devCupRowByDevCupID["Password"].ToString().Trim();
                    string str5 = devCupRowByDevCupID["RewardXML"].ToString().Trim();
                    int num = (int) devCupRowByDevCupID["Capacity"];
                    int num2 = (int) devCupRowByDevCupID["WealthCost"];
                    bool flag1 = (bool) devCupRowByDevCupID["IsSelfUnion"];
                    DateTime time = (DateTime) devCupRowByDevCupID["MatchTime"];
                    this.strLevel = reader.GetTagline(line, "<DevLvl>", "</DevLvl>").ToString().Trim();
                    this.strLevel = "第" + this.strLevel + "级联赛以下";
                    IEnumerator enumerator = reader.GetItems(str5, "<Reward>", "</Reward>").GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        string current = (string) enumerator.Current;
                        switch (Convert.ToInt32(reader.GetTagline(current, "<Place>", "</Place>")))
                        {
                            case 1:
                            {
                                this.strReward1 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                continue;
                            }
                            case 2:
                            {
                                this.strReward2 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                continue;
                            }
                            case 3:
                            {
                                this.strReward3 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                continue;
                            }
                            case 4:
                            {
                                this.strReward4 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                                continue;
                            }
                        }
                        this.strReward1 = this.strReward2 = this.strReward3 = this.strReward4 = "0";
                    }
                    this.strCupName = str;
                    this.datMatchTime = time;
                    this.intWealthCostM = num2;
                    this.intCapacity = num;
                    this.tbIntroM.Text = str3;
                    this.tbPasswordM.Text = str4;
                }
            }
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
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                this.strClubName = onlineRowByUserID["CLubName5"].ToString();
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strShortName = onlineRowByUserID["ShortName"].ToString();
                this.strDevCode = onlineRowByUserID["DevCode"].ToString().Trim();
                this.strClubLogo = onlineRowByUserID["ClubLogo"].ToString();
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intWealth = (int) onlineRowByUserID["Wealth"];
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.tbDevCupStats.Visible = false;
                this.tblUMatchList.Visible = false;
                this.tblUMatchAdd.Visible = false;
                this.IBtnAddUMatch.Visible = false;
                this.tbLookList.Visible = false;
                this.tblPassWord.Visible = false;
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                this.strStatus = SessionItem.GetRequest("Status", 1).ToString().Trim();
                this.tbDevCupChampion.Visible = false;
                this.tblCreateDevCup.Visible = false;
                this.tblCreate.Visible = false;
                this.tblManager.Visible = false;
                this.tblModifyDevCup.Visible = false;
                this.tblCupRegManager.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetDevCupAdd()
        {
            int request = (int) SessionItem.GetRequest("DevCupID", 0);
            DevCalculator.GetLevel(this.strDevCode);
            TagReader reader = new TagReader();
            string str = "0";
            string str2 = "0";
            string str3 = "0";
            string str4 = "0";
            string str5 = "";
            DataRow devCupRowByDevCupID = BTPDevCupManager.GetDevCupRowByDevCupID(request);
            if (devCupRowByDevCupID != null)
            {
                this.strBigLogo = "<IMG src='" + SessionItem.GetImageURL() + "DevMedal/" + devCupRowByDevCupID["BigLogo"].ToString().Trim() + "' height='50' width='40'>";
                this.strDevCupName = devCupRowByDevCupID["Name"].ToString();
                this.intWealthCost = (int) devCupRowByDevCupID["WealthCost"];
                this.strRequirementXML = devCupRowByDevCupID["RequirementXML"].ToString();
                this.strIntroduction = devCupRowByDevCupID["Introduction"].ToString().Trim();
                int num2 = (int) devCupRowByDevCupID["RegCount"];
                int num3 = (int) devCupRowByDevCupID["Capacity"];
                int num1 = (int) devCupRowByDevCupID["UnionID"];
                int num4 = Convert.ToInt32(devCupRowByDevCupID["Status"]);
                bool flag = (bool) devCupRowByDevCupID["IsSelfUnion"];
                this.strDevCupName = devCupRowByDevCupID["Name"].ToString().Trim();
                string line = devCupRowByDevCupID["RewardXML"].ToString().Trim();
                if (devCupRowByDevCupID["Password"].ToString().Trim() == "")
                {
                    this.tblPassWord.Visible = false;
                }
                this.strUnionName = devCupRowByDevCupID["UnionName"].ToString().Trim();
                if (num4 == 0)
                {
                    this.strBeginTime = StringItem.FormatDate((DateTime) devCupRowByDevCupID["MatchTime"], "yyyy-MM-dd hh:mm");
                }
                else
                {
                    this.strBeginTime = "已截止";
                }
                this.strMatchTime = StringItem.FormatDate((DateTime) devCupRowByDevCupID["MatchTime"], "yyyy-MM-dd hh:mm");
                str5 = reader.GetTagline(this.strRequirementXML, "<DevLvl>", "</DevLvl>").ToString().Trim();
                if (str5.Trim().Equals("0") && !flag)
                {
                    this.strRequirement = "无限制";
                }
                else if (str5.IndexOf("|") > 0)
                {
                    string[] strArray = new string[] { str5.Substring(0, str5.IndexOf("|")), str5.Substring(str5.IndexOf("|") + 1) };
                    this.strRequirement = "第" + strArray[0] + "级联赛至第" + strArray[1] + "级联赛";
                }
                else if (!str5.Trim().Equals("0") && !flag)
                {
                    this.strRequirement = "第" + str5 + "级联赛以下";
                }
                else if (!str5.Trim().Equals("0") && flag)
                {
                    this.strRequirement = "第" + str5 + "级联赛以下的" + this.strUnionName + "盟员";
                }
                else
                {
                    this.strRequirement = this.strUnionName + "盟员";
                }
                IEnumerator enumerator = reader.GetItems(line, "<Reward>", "</Reward>").GetEnumerator();
                while (enumerator.MoveNext())
                {
                    string current = (string) enumerator.Current;
                    switch (Convert.ToInt32(reader.GetTagline(current, "<Place>", "</Place>")))
                    {
                        case 1:
                        {
                            str = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                            continue;
                        }
                        case 2:
                        {
                            str2 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                            continue;
                        }
                        case 3:
                        {
                            str3 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                            continue;
                        }
                        case 4:
                        {
                            str4 = reader.GetTagline(current, "<Wealth>", "</Wealth>");
                            continue;
                        }
                    }
                    str = str2 = str3 = str4 = "0";
                }
                this.strReward = "第一名奖励：" + str + "游戏币<br>第二名奖励：" + str2 + "游戏币<br>第三-四名奖励：" + str3 + "游戏币<br>第五-八名奖励：" + str4 + "游戏币";
                this.strCount = num2 + "/" + num3;
            }
        }

        private void SetPageIntro()
        {
            switch (this.strType)
            {
                case "DEVCUPREG":
                    this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tblUMatchList.Visible = true;
                        this.DevCupList();
                    }
                    else if (this.intGrade == 1)
                    {
                        this.IBtnAddUMatch.Visible = true;
                        this.tblUMatchAdd.Visible = true;
                        this.tblPassWord.Visible = true;
                        this.SetDevCupAdd();
                    }
                    else if (this.intGrade == 2)
                    {
                        this.tbLookList.Visible = true;
                        this.DevCupLookList();
                    }
                    else if (this.intGrade == 3)
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetDevCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1'>杯赛报名</li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPSTATS'>杯赛成绩</a></li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "DEVCUPSTATS":
                    this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tbDevCupStats.Visible = true;
                        this.DevCupStatsList();
                    }
                    else if (this.intGrade == 1)
                    {
                        this.tbLookList.Visible = true;
                        this.DevCupLookList();
                    }
                    else if (this.intGrade == 2)
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetDevCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1a'><a href='DevCup.aspx?Type=DEVCUPREG'>杯赛报名</a></li><li class='qian2'>杯赛成绩</li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "DEVCUPCHAMPION":
                    this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tbDevCupChampion.Visible = true;
                        this.DevCupChampionList();
                    }
                    this.strPageIntro = "<ul><li class='qian1a'><a href='DevCup.aspx?Type=DEVCUPREG'>杯赛报名</a></li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPSTATS'>杯赛成绩</a></li><li class='qian2'>冠军榜</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                case "CREATEDEVCUP":
                    this.tblCreate.Visible = true;
                    this.strPageIntro = "<ul><li class='qian1a'><a href='DevCup.aspx?Type=DEVCUPREG'>杯赛报名</a></li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPSTATS'>杯赛成绩</a></li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;

                default:
                    this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
                    if (this.intGrade < 1)
                    {
                        this.tblUMatchList.Visible = true;
                        this.DevCupList();
                    }
                    else
                    {
                        this.tblUMatchAdd.Visible = true;
                        this.SetDevCupAdd();
                    }
                    this.strPageIntro = "<ul><li class='qian1'>杯赛报名</li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPSTATS'>杯赛成绩</a></li><li class='qian2a'><a href='DevCup.aspx?Type=DEVCUPCHAMPION'>冠军榜</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                    break;
            }
            //switch (this.strStatus)
            //{
            //    case "CREATE":
            //        this.strPageIntro1 = "<font color='red'>创建杯赛</font>&nbsp;|&nbsp;<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=MANAGER'>杯赛管理</a>";
            //        if (!base.IsPostBack)
            //        {
            //            DataView view = new DataView(DDLItem.GetDevLevelItem());
            //            this.ddlClubLevel.DataSource = view;
            //            this.ddlClubLevel.DataTextField = "Name";
            //            this.ddlClubLevel.DataValueField = "Category";
            //            this.ddlClubLevel.DataBind();
            //        }
            //        this.tblCreateDevCup.Visible = true;
            //        this.strModalTime = StringItem.FormatDate(DateTime.Today.AddHours(63.0), "yyyy-MM-dd hh:mm");
            //        return;

            //    case "MANAGER":
            //        this.strPageIntro1 = "<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=CREATE'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
            //        this.tblManager.Visible = true;
            //        this.GetCupList();
            //        return;

            //    case "MODIFYDEVCUP":
            //        this.strPageIntro1 = "<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=CREATE'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
            //        this.tblModifyDevCup.Visible = true;
            //        this.ModifyDevCup();
            //        return;

            //    case "CUPREGMANAGE":
            //        this.strPageIntro1 = "<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=CREATE'>创建杯赛</a>&nbsp;|&nbsp;<font color='red'>杯赛管理</font>";
            //        this.tblCupRegManager.Visible = true;
            //        this.CupRegManager();
            //        return;
            //}
            this.strPageIntro1 = "<font color='red'>创建杯赛</font>&nbsp;|&nbsp;<a href='DevCup.aspx?Type=CREATEDEVCUP&Status=MANAGER'>杯赛管理</a>";
            //if (!base.IsPostBack)
            //{
            //    DataView view2 = new DataView(DDLItem.GetDevLevelItem());
            //    this.ddlClubLevel.DataSource = view2;
            //    this.ddlClubLevel.DataTextField = "Name";
            //    this.ddlClubLevel.DataValueField = "Category";
            //    this.ddlClubLevel.DataBind();
            //}
            this.tblCreateDevCup.Visible = true;
            this.strModalTime = StringItem.FormatDate(DateTime.Today.AddHours(63.0), "yyyy-MM-dd hh:mm");
        }
    }
}


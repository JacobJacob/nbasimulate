namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class CupList : Page
    {
        protected ImageButton btnBack;
        protected ImageButton btnBackA;
        protected ImageButton Cancel;
        private int intClubID;
        private int intCupID;
        private int intGrade;
        private int intLevel;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intRank;
        private int intScore;
        private int intStatus;
        private int intUnionID;
        private int intUserID;
        protected ImageButton OK;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        private string strClubLogo;
        private string strClubName;
        public string strList;
        private string strNickName;
        public string strPageInfo;
        public string strScript;
        private string strShortName;
        private string strType;
        protected HtmlTable tblChampion;
        protected HtmlTable tblCheck;
        protected HtmlTable tblKiloCup;
        protected HtmlTable tblKiloCupA;
        protected HtmlTable tblReg;
        protected HtmlTable tblSignUp;
        protected HtmlTable tblView;
        protected HtmlTable tblViewBig;
        protected HtmlTable tblViewBigA;
        protected HtmlTable tblViewSmallA;

        private void Cancel_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("CupList.aspx?Type=SIGNUP&Grade=0&Page=1");
        }

        private void GetCheckList()
        {
            this.sbList = new StringBuilder();
            string strCurrentURL = string.Concat(new object[] { "CupList.aspx?Type=CHECK&CupID=", this.intCupID, "&Grade=0&Status=", this.intStatus, "&" });
            this.intPerPage = 5;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            BTPCupRegManager.GetRegCupCountNew(this.intCupID);
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            SqlDataReader reader = BTPCupRegManager.GetRegCupListNew(this.intCupID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num1 = (int) reader["ClubID"];
                    int num = (int) reader["Levels"];
                    int num2 = (int) reader["Score"];
                    string str2 = reader["NickName"].ToString().Trim();
                    string strIn = reader["ClubName"].ToString().Trim();
                    string str4 = reader["ClubLogo"].ToString().Trim();
                    int num3 = (int) reader["UserID"];
                    this.sbList.Append("<tr class='BarContent'>");
                    this.sbList.Append("<td height='50'><img src='" + str4 + "' height=\"46\" width='46'></td>");
                    this.sbList.Append(string.Concat(new object[] { "<td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right' title='", strIn, "'>", StringItem.GetShortString(strIn, 0x17, "."), "</a></td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td><a href='ShowClub.aspx?UserID=", num3, "&Type=3' target='Right'>", str2, "</a></td>" }));
                    this.sbList.Append("<td>" + num + "</td>");
                    this.sbList.Append("<td>" + num2 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
            else
            {
                this.sbList.Append("<tr class='BarContent'><td height='25' colspan='5'>暂时没有球队报名本杯赛。</td></tr>");
            }
            reader.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }

        private void GetRegTrueList()
        {
            this.sbList = new StringBuilder();
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            string str = cupRowByCupID["BigLogo"].ToString().Trim();
            string str2 = cupRowByCupID["Name"].ToString().Trim();
            int num = (byte) cupRowByCupID["Levels"];
            int num2 = (int) cupRowByCupID["MoneyCost"];
            int num3 = (int) cupRowByCupID["UnionID"];
            string str3 = cupRowByCupID["Introduction"].ToString().Trim();
            byte num1 = (byte) cupRowByCupID["Category"];
            if ((num3 != 0) && (num3 != this.intUnionID))
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                if (this.strType == "REG")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在申请参加的杯赛</td></tr>";
                }
                else if (this.strType == "VIEWSMALLA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看小杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "VIEWBIGA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看大杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "VIEWBIGA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看大杯赛奖励情况</td></tr>";
                }
                else if (this.strType == "KILOCUPA")
                {
                    this.strList = "<tr align='center'><td colspan='4' height='25' class='BarHead'>正在查看千人杯赛奖励情况</td></tr>";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
                this.sbList.Append("<tr align='center' class='BarContent'>");
                this.sbList.Append("<td height='50'><img src='Images/Cup/" + str + "'></td>");
                this.sbList.Append("<td><font color=''><strong>" + str2 + "</strong></font></td>");
                this.sbList.Append("<td><font color=''><strong>第" + num + "等级</strong></font></td>");
                this.sbList.Append("<td><font color=''><strong>报名费：" + num2 + "</strong></font></td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr>");
                this.sbList.Append("<td align='center' height='50'>介绍：</td>");
                this.sbList.Append("<td colspan='3' style='padding-left:4px'>" + str3 + "</td>");
                this.sbList.Append("</tr>");
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private void GetSignList()
        {
            this.sbList = new StringBuilder();
            string strCupIDs = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["CupIDs"].ToString().Trim().Replace("|", ",");
            string strSetIDs = "";
            DataTable setIDsTable = BTPCupManager.GetSetIDsTable(strCupIDs);
            if (setIDsTable != null)
            {
                for (int i = 0; i < setIDsTable.Rows.Count; i++)
                {
                    DataRow row2 = setIDsTable.Rows[i];
                    strSetIDs = strSetIDs + row2["SetID"].ToString().Trim();
                    if (i < (setIDsTable.Rows.Count - 1))
                    {
                        strSetIDs = strSetIDs + ",";
                    }
                }
            }
            else
            {
                strSetIDs = "0";
            }
            DataTable setIDTable = BTPCupManager.GetSetIDTable(strSetIDs);
            if (setIDTable == null)
            {
                this.sbList.Append("<tr><td height='25' class='BarContent' colspan='8'>暂时没有可报名的杯赛！</td></tr>");
            }
            else
            {
                foreach (DataRow row3 in setIDTable.Rows)
                {
                    int intSetID = (int) row3["SetID"];
                    DataRow row4 = BTPCupManager.GetCupRowBySetID(this.intUserID, intSetID, strCupIDs);
                    if (row4 != null)
                    {
                        string str6;
                        int num3 = (byte) row4["Levels"];
                        int num4 = (int) row4["RegCount"];
                        int num5 = (int) row4["Capacity"];
                        int num6 = (int) row4["MoneyCost"];
                        string str3 = row4["Name"].ToString().Trim();
                        string str4 = row4["BigLogo"].ToString().Trim();
                        DateTime datIn = (DateTime) row4["MatchTime"];
                        DateTime time2 = (DateTime) row4["EndRegTime"];
                        int num8 = (int) row4["CupID"];
                        int num9 = (byte) row4["Category"];
                        int num7 = (int) row4["UnionID"];
                        if ((num9 == 2) && (str3 == "新人杯"))
                        {
                            str6 = "alt='新人不要错过训练的机会，报名杯赛可以让街头球员获得更多的训练点。'";
                        }
                        else
                        {
                            str6 = "alt='报名杯赛可以让街头球员获得更多的训练点。'";
                        }
                        if ((num7 != 0) && (num7 != this.intUnionID))
                        {
                            this.strList = this.strList + "";
                        }
                        else
                        {
                            string str5;
                            if (num9 == 4)
                            {
                                str5 = "Union.aspx?Type=UNIONCUP&Kind=IMPERIALCUPCHECK&Page=1&CupID=" + num8;
                            }
                            else if ((num7 == this.intUnionID) && (num7 != 0))
                            {
                                str5 = "Union.aspx?Type=MYUNION&Kind=UNIONCUPCHECK&Page=1&CupID=" + num8;
                            }
                            else
                            {
                                str5 = string.Concat(new object[] { "CupList.aspx?Type=CHECK&Status=", num9, "&CupID=", num8, "&Grade=0&Page=1" });
                            }
                            this.sbList.Append("<tr class='BarContent'>");
                            this.sbList.Append("<td height='50'><img " + str6 + " src='Images/Cup/" + str4 + "'></td>");
                            this.sbList.Append("<td>" + str3 + "</td>");
                            this.sbList.Append("<td>" + num3 + "</td>");
                            this.sbList.Append("<td>" + StringItem.FormatDate(time2, "yy-MM-dd<br>hh:mm:ss") + "</td>");
                            this.sbList.Append("<td>" + StringItem.FormatDate(datIn, "yy-MM-dd<br>hh:mm:ss") + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td>", num4, "/", num5, "</td>" }));
                            this.sbList.Append("<td>" + num6 + "</td>");
                            this.sbList.Append(string.Concat(new object[] { "<td><a href='", str5, "'>查看</a> | <a href='CupList.aspx?Type=REG&Grade=0&Page=1&CupID=", num8, "'>报名</a></td>" }));
                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='8'></td></tr>");
                        }
                        continue;
                    }
                    if (setIDTable.Rows.Count == 1)
                    {
                        this.sbList.Append("<tr><td height='25' class='BarContent' colspan='8'>暂时没有可报名的杯赛！</td></tr>");
                    }
                }
            }
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            int regCupCountNew = 0;
            if (this.strType == "CHECK")
            {
                regCupCountNew = BTPCupRegManager.GetRegCupCountNew(this.intCupID);
            }
            else if (this.strType == "VIEW")
            {
                regCupCountNew = BTPCupRegManager.GetRegCupCountUIDCategoryNew(this.intUserID, 2);
            }
            else if (this.strType == "CHAMPION")
            {
                regCupCountNew = BTPCupManager.GetChampionUserCountNew();
            }
            else if (this.strType == "VIEWBIG")
            {
                regCupCountNew = BTPCupRegManager.GetRegCupCountUIDCategoryNew(this.intUserID, 1);
            }
            else if (this.strType == "KILOCUP")
            {
                regCupCountNew = BTPCupRegManager.GetRegKiloCupCountNew(this.intUserID);
            }
            int num2 = (regCupCountNew / this.intPerPage) + 1;
            if ((regCupCountNew % this.intPerPage) == 0)
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
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", regCupCountNew, "个记录 跳转", str4 });
        }

        private void InitializeComponent()
        {
            this.OK.Click += new ImageClickEventHandler(this.OK_Click);
            this.Cancel.Click += new ImageClickEventHandler(this.Cancel_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void OK_Click(object sender, ImageClickEventArgs e)
        {
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            if (cupRowByCupID != null)
            {
                byte num1 = (byte) cupRowByCupID["Category"];
                int intTicketCategory = (byte) cupRowByCupID["TicketCategory"];
                if ((intTicketCategory > 0) && !BTPToolLinkManager.HasTicket(this.intUserID, 1, intTicketCategory))
                {
                    base.Response.Redirect("Report.aspx?Parameter=98");
                }
                else
                {
                    int num2 = (byte) cupRowByCupID["Coin"];
                    int num3 = (int) cupRowByCupID["RegCount"];
                    int num4 = (int) cupRowByCupID["Capacity"];
                    int num5 = (int) cupRowByCupID["UnionID"];
                    if ((num5 != 0) && (this.intUnionID != num5))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                    else if (num3 >= num4)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=97");
                    }
                    else if ((num2 > 0) && (this.intPayType != 1))
                    {
                        base.Response.Redirect("SecretaryPage.aspx?Type=REGCUP&CupID=" + this.intCupID);
                    }
                    else if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID) < 4)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=96");
                    }
                    else
                    {
                        DataTable setIDsTable = BTPCupManager.GetSetIDsTable(BTPAccountManager.GetAccountRowByUserID(this.intUserID)["CupIDs"].ToString().Trim().Replace("|", ","));
                        int num9 = 0;
                        if (setIDsTable != null)
                        {
                            for (int i = 0; i < setIDsTable.Rows.Count; i++)
                            {
                                DataRow row3 = setIDsTable.Rows[i];
                                int num7 = (int) row3["SetID"];
                                int num8 = (int) BTPCupManager.GetCupRowByCupID(this.intCupID)["SetID"];
                                if (num8 == num7)
                                {
                                    num9 = 1;
                                }
                            }
                        }
                        if (num9 == 1)
                        {
                            base.Response.Redirect("Report.aspx?Parameter=94");
                        }
                        else
                        {
                            switch (BTPCupRegManager.SetCupReg(this.intUserID, this.intCupID, this.intClubID, this.strShortName + this.strClubName, this.strNickName, this.intLevel, this.strClubLogo, this.intScore, this.intRank, ""))
                            {
                                case 1:
                                    if (intTicketCategory > 0)
                                    {
                                        BTPToolLinkManager.DeleteTicket(this.intUserID, 1, intTicketCategory);
                                    }
                                    base.Response.Redirect("Report.aspx?Parameter=93");
                                    return;

                                case 2:
                                    base.Response.Redirect("Report.aspx?Parameter=95");
                                    return;
                            }
                            base.Response.Redirect("Report.aspx?Parameter=94");
                        }
                    }
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.strNickName = onlineRowByUserID["NickName"].ToString();
            this.intClubID = (int) onlineRowByUserID["ClubID3"];
            this.intPayType = (int) onlineRowByUserID["PayType"];
            this.intUnionID = (int) onlineRowByUserID["UnionID"];
            this.strShortName = onlineRowByUserID["ShortName"].ToString().Trim() + "-";
            if (this.intUnionID < 1)
            {
                this.strShortName = "";
            }
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            this.intLevel = (byte) accountRowByUserID["Levels"];
            this.intScore = (int) accountRowByUserID["Score"];
            this.intRank = this.intScore;
            this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
            this.strClubLogo = onlineRowByUserID["ClubLogo"].ToString().Trim();
            this.strType = (string) SessionItem.GetRequest("Type", 1);
            this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.tblSignUp.Visible = false;
            this.tblView.Visible = false;
            this.tblChampion.Visible = false;
            this.tblReg.Visible = false;
            this.tblCheck.Visible = false;
            this.tblViewBig.Visible = false;
            this.tblViewBigA.Visible = false;
            this.tblViewSmallA.Visible = false;
            this.tblKiloCup.Visible = false;
            this.tblKiloCupA.Visible = false;
            this.sbPageIntro = new StringBuilder();
            switch (this.strType)
            {
                case "SIGNUP":
                    this.sbPageIntro.Append("<ul><li class='qian1'>杯赛报名</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblSignUp.Visible = true;
                    this.GetSignList();
                    goto Label_0AE0;

                case "VIEW":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>小杯赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblView.Visible = true;
                    this.SetSmallList();
                    goto Label_0AE0;

                case "VIEWBIG":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>大杯赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblViewBig.Visible = true;
                    this.SetBigList();
                    goto Label_0AE0;

                case "KILOCUP":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>经典赛事</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblKiloCup.Visible = true;
                    this.SetKiloList();
                    goto Label_0AE0;

                case "CHAMPION":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>冠军榜</li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblChampion.Visible = true;
                    this.SetChampionList();
                    goto Label_0AE0;

                case "REG":
                    this.sbPageIntro.Append("<ul><li class='qian1'>杯赛报名</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
                    this.tblReg.Visible = true;
                    this.GetRegTrueList();
                    goto Label_0AE0;

                case "CHECK":
                    this.intStatus = (int) SessionItem.GetRequest("Status", 0);
                    if (this.intStatus != 2)
                    {
                        if (this.intStatus == 1)
                        {
                            this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>大杯赛</li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                            this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        }
                        else if ((this.intStatus == 3) || (this.intStatus == 8))
                        {
                            this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                            this.sbPageIntro.Append("<li class='qian2'>经典赛事</li>");
                            this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                            this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=1");
                        }
                        break;
                    }
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>小杯赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    break;

                case "VIEWSMALLA":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>小杯赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
                    this.tblViewSmallA.Visible = true;
                    this.GetRegTrueList();
                    goto Label_0AE0;

                case "VIEWBIGA":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>大杯赛</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
                    this.tblViewBigA.Visible = true;
                    this.GetRegTrueList();
                    goto Label_0AE0;

                case "KILOCUPA":
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=SIGNUP\"' href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>杯赛报名</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>经典赛事</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
                    this.tblKiloCupA.Visible = true;
                    this.GetRegTrueList();
                    goto Label_0AE0;

                default:
                    this.sbPageIntro.Append("<ul><li class='qian1'>杯赛报名</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEW\"' href='CupList.aspx?Type=VIEW&Grade=0&Page=1'>小杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=VIEWBIG\"' href='CupList.aspx?Type=VIEWBIG&Grade=0&Page=1'>大杯赛</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=KILOCUP\"' href='CupList.aspx?Type=KILOCUP&Grade=0&Page=1'>经典赛事</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Cup.aspx?Type=CHAMPION\"' href='CupList.aspx?Type=CHAMPION&Grade=0&Page=1'>冠军榜</a></li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblSignUp.Visible = true;
                    goto Label_0AE0;
            }
            this.intCupID = (int) SessionItem.GetRequest("CupID", 0);
            this.tblCheck.Visible = true;
            this.SetPageInfo();
            this.GetCheckList();
        Label_0AE0:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetBigList()
        {
            int num;
            string str2;
            string str3;
            int num2;
            string str4;
            DateTime time;
            string str5;
            int num4;
            int num5;
            int num6;
            string str6;
            string str7;
            this.sbList = new StringBuilder();
            string strCurrentURL = "CupList.aspx?Type=VIEWBIG&Grade=0&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            BTPCupRegManager.GetRegCupCountUIDCategoryNew(this.intUserID, 1);
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            SqlDataReader reader = BTPCupRegManager.GetRegCupListByUIDCategoryNew(this.intUserID, 1, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_04CD;
            }
            this.strList = "<tr class='BarContent'><td height='25' colspan='7'>您暂时还没有报名过大杯赛。</td></tr>";
            goto Label_04D9;
        Label_02B0:
            switch (num6)
            {
                case 2:
                    str5 = "等待发奖";
                    break;

                case 3:
                    str5 = "已结束";
                    break;

                case 0:
                    str5 = "<a title='赛程排定时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;

                default:
                    str5 = "<a title='下轮比赛时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;
            }
            if (num6 == 0)
            {
                str4 = string.Concat(new object[] { "<a href='CupList.aspx?Type=CHECK&CupID=", num, "&Grade=0&Page=1&Status=1'>查看</a> | <a href='CupList.aspx?Type=VIEWBIGA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            else
            {
                str4 = string.Concat(new object[] { "<a href='", str6, "'>赛程</a> | <a href='CupList.aspx?Type=VIEWBIGA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            this.sbList.Append("<tr class='BarContent'>");
            this.sbList.Append("<td height='50'><img src='Images/Cup/" + str2 + "'></td>");
            this.sbList.Append("<td>" + str3 + "</td>");
            this.sbList.Append("<td>" + num2 + "</td>");
            this.sbList.Append("<td>" + str7 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "<td>", num4, "/", num5, "</td>" }));
            this.sbList.Append("<td>" + str5 + "</td>");
            this.sbList.Append("<td>" + str4 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
        Label_04CD:
            if (reader.Read())
            {
                num = (int) reader["CupID"];
                int deadRound = BTPCupRegManager.GetDeadRound(num, this.intClubID);
                str2 = reader["BigLogo"].ToString().Trim();
                str3 = reader["Name"].ToString().Trim();
                num2 = (byte) reader["Levels"];
                int num3 = (byte) reader["Round"];
                time = (DateTime) reader["MatchTime"];
                DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(num);
                num4 = (int) cupRowByCupID["RegCount"];
                num5 = (int) cupRowByCupID["Capacity"];
                num6 = (byte) cupRowByCupID["Status"];
                str6 = cupRowByCupID["LadderURL"].ToString().Trim();
                if (deadRound == 100)
                {
                    deadRound = num3;
                }
                if (deadRound < num3)
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_02B0;
                    }
                    str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                else
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_02B0;
                    }
                    str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                goto Label_02B0;
            }
        Label_04D9:
            reader.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }

        private void SetChampionList()
        {
            this.sbList = new StringBuilder();
            string strCurrentURL = "CupList.aspx?Type=CHAMPION&Grade=0&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            BTPCupManager.GetChampionUserCountNew();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            SqlDataReader championListNew = BTPCupManager.GetChampionListNew(this.intPage, this.intPerPage);
            if (championListNew.HasRows)
            {
                while (championListNew.Read())
                {
                    int num1 = (int) championListNew["CupID"];
                    string str2 = championListNew["BigLogo"].ToString().Trim();
                    string str3 = championListNew["Name"].ToString().Trim();
                    int num = (byte) championListNew["Levels"];
                    string str6 = championListNew["ChampionClubName"].ToString().Trim();
                    string str4 = championListNew["LadderURL"].ToString().Trim();
                    if ((ServerParameter.strCopartner == "XBA") || (ServerParameter.strCopartner == "CGA"))
                    {
                        if (str4.ToLower().IndexOf("http://match") == -1)
                        {
                            str4 = Config.GetDomain() + str4;
                        }
                    }
                    else if (str4.ToLower().IndexOf("http://xbam") == -1)
                    {
                        str4 = Config.GetDomain() + str4;
                    }
                    string str5 = "<a href='" + str4 + "'>查看</a>";
                    DateTime datIn = (DateTime) championListNew["EndRegTime"];
                    this.sbList.Append("<tr class='BarContent'>");
                    this.sbList.Append("<td height='50'><img src='Images/Cup/" + str2 + "'></td>");
                    this.sbList.Append("<td>" + str3 + "</td>");
                    this.sbList.Append("<td>" + num + "</td>");
                    this.sbList.Append("<td>" + StringItem.FormatDate(datIn, "yyyy-MM-dd<br>hh:mm:ss") + "</td>");
                    this.sbList.Append("<td>" + str5 + "</td>");
                    this.sbList.Append("<td>" + str6 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>");
                }
            }
            else
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>暂时没有杯赛冠军。</td></tr>";
            }
            championListNew.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }

        private void SetKiloList()
        {
            int num;
            string str2;
            string str3;
            int num2;
            string str4;
            DateTime time;
            string str5;
            int num4;
            int num5;
            int num6;
            string str6;
            string str7;
            this.sbList = new StringBuilder();
            string strCurrentURL = "CupList.aspx?Type=KILOCUP&Grade=0&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            BTPCupRegManager.GetRegKiloCupCountNew(this.intUserID);
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            SqlDataReader reader = BTPCupRegManager.GetRegKiloCupListNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_04CB;
            }
            this.strList = "<tr class='BarContent'><td height='25' colspan='7'>您暂时还没有报名过千人杯赛。</td></tr>";
            goto Label_04D7;
        Label_02AE:
            switch (num6)
            {
                case 2:
                    str5 = "等待发奖";
                    break;

                case 3:
                    str5 = "已结束";
                    break;

                case 0:
                    str5 = "<a title='赛程排定时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;

                default:
                    str5 = "<a title='下轮比赛时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;
            }
            if (num6 == 0)
            {
                str4 = string.Concat(new object[] { "<a href='CupList.aspx?Type=CHECK&CupID=", num, "&Grade=0&Page=1&Status=3'>查看</a> | <a href='CupList.aspx?Type=KILOCUPA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            else
            {
                str4 = string.Concat(new object[] { "<a href='", str6, "'>赛程</a> | <a href='CupList.aspx?Type=KILOCUPA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            this.sbList.Append("<tr class='BarContent'>");
            this.sbList.Append("<td height='50'><img src='Images/Cup/" + str2 + "'></td>");
            this.sbList.Append("<td>" + str3 + "</td>");
            this.sbList.Append("<td>" + num2 + "</td>");
            this.sbList.Append("<td>" + str7 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "<td>", num4, "/", num5, "</td>" }));
            this.sbList.Append("<td>" + str5 + "</td>");
            this.sbList.Append("<td>" + str4 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
        Label_04CB:
            if (reader.Read())
            {
                num = (int) reader["CupID"];
                int deadRound = BTPCupRegManager.GetDeadRound(num, this.intClubID);
                str2 = reader["BigLogo"].ToString().Trim();
                str3 = reader["Name"].ToString().Trim();
                num2 = (byte) reader["Levels"];
                int num3 = (byte) reader["Round"];
                time = (DateTime) reader["MatchTime"];
                DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(num);
                num4 = (int) cupRowByCupID["RegCount"];
                num5 = (int) cupRowByCupID["Capacity"];
                num6 = (byte) cupRowByCupID["Status"];
                str6 = cupRowByCupID["LadderURL"].ToString().Trim();
                if (deadRound == 100)
                {
                    deadRound = num3;
                }
                if (deadRound < num3)
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_02AE;
                    }
                    str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                else
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_02AE;
                    }
                    str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                goto Label_02AE;
            }
        Label_04D7:
            reader.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }

        private void SetPageInfo()
        {
            DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(this.intCupID);
            string str = cupRowByCupID["BigLogo"].ToString().Trim();
            cupRowByCupID["Name"].ToString().Trim();
            int num = (int) cupRowByCupID["Capacity"];
            int num2 = (int) cupRowByCupID["RegCount"];
            int num3 = (int) cupRowByCupID["MoneyCost"];
            this.strPageInfo = string.Concat(new object[] { "<table width='100%' bgcolor='#fcf2ec' border='0' cellspacing='0' cellpadding='0'><tr><td style='paddin-left:5px'><img src='Images/Cup/", str, "'>      <strong>名额：</strong>", num, "　<strong>报名：</strong>", num2, "　<strong>报名费：</strong>", num3, "</td><td align='right' valign='baseline' style='padding-right:5px'><a href='CupList.aspx?Type=SIGNUP&Grade=0&Page=1'>返回报名</a></td></table>" });
        }

        private void SetSmallList()
        {
            int num;
            string str2;
            string str3;
            int num2;
            string str4;
            DateTime time;
            string str5;
            int num4;
            int num5;
            int num6;
            string str6;
            string str7;
            this.sbList = new StringBuilder();
            string strCurrentURL = "CupList.aspx?Type=VIEW&Grade=0&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            BTPCupRegManager.GetRegCupCountUIDCategoryNew(this.intUserID, 2);
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            SqlDataReader reader = BTPCupRegManager.GetRegCupListByUIDCategoryNew(this.intUserID, 2, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_0535;
            }
            this.strList = "<tr class='BarContent'><td height='25' colspan='7'>您暂时还没有报名过小杯赛。</td></tr>";
            goto Label_0541;
        Label_0318:
            switch (num6)
            {
                case 2:
                    str5 = "等待发奖";
                    break;

                case 3:
                    str5 = "已结束";
                    break;

                case 0:
                    str5 = "<a title='赛程排定时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;

                default:
                    str5 = "<a title='下轮比赛时间' style='cursor:hand;'><font color='#660066'>" + StringItem.FormatDate(time, "yy-MM-dd<br>hh:mm:ss") + "</font></a>";
                    break;
            }
            if (num6 == 0)
            {
                str4 = string.Concat(new object[] { "<a href='CupList.aspx?Type=CHECK&CupID=", num, "&Grade=0&Page=1&Status=2'>查看</a> | <a href='CupList.aspx?Type=VIEWSMALLA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            else
            {
                str4 = string.Concat(new object[] { "<a href='", str6, "'>赛程</a> | <a href='CupList.aspx?Type=VIEWSMALLA&Grade=0&Page=1&CupID=", num, "'>介绍</a>" });
            }
            this.sbList.Append("<tr class='BarContent'>");
            this.sbList.Append("<td height='50'><img src='Images/Cup/" + str2 + "'></td>");
            this.sbList.Append("<td>" + str3 + "</td>");
            this.sbList.Append("<td>" + num2 + "</td>");
            this.sbList.Append("<td>" + str7 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "<td>", num4, "/", num5, "</td>" }));
            this.sbList.Append("<td>" + str5 + "</td>");
            this.sbList.Append("<td>" + str4 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
        Label_0535:
            if (reader.Read())
            {
                num = (int) reader["CupID"];
                int deadRound = BTPCupRegManager.GetDeadRound(num, this.intClubID);
                str2 = reader["BigLogo"].ToString().Trim();
                str3 = reader["Name"].ToString().Trim();
                num2 = (byte) reader["Levels"];
                int num3 = (byte) reader["Round"];
                time = (DateTime) reader["MatchTime"];
                DataRow cupRowByCupID = BTPCupManager.GetCupRowByCupID(num);
                num4 = (int) cupRowByCupID["RegCount"];
                num5 = (int) cupRowByCupID["Capacity"];
                num6 = (byte) cupRowByCupID["Status"];
                str6 = cupRowByCupID["LadderURL"].ToString().Trim();
                if ((ServerParameter.strCopartner == "XBA") || (ServerParameter.strCopartner == "CGA"))
                {
                    if (str6.ToLower().IndexOf("http://match") == -1)
                    {
                        str6 = Config.GetDomain() + str6;
                    }
                }
                else if (str6.ToLower().IndexOf("http://xbam") == -1)
                {
                    str6 = Config.GetDomain() + str6;
                }
                if (deadRound == 100)
                {
                    deadRound = num3;
                }
                if (deadRound < num3)
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_0318;
                    }
                    str7 = string.Concat(new object[] { "<font color='red'><a title='淘汰轮数' style='cursor:hand;'>", deadRound, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                else
                {
                    switch (num6)
                    {
                        case 2:
                        case 3:
                            str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='red'><a title='比赛结束轮数' style='cursor:hand;'>", num3, "</a></font>" });
                            goto Label_0318;
                    }
                    str7 = string.Concat(new object[] { "<font color='green'><a title='参赛轮数' style='cursor:hand;'>", num3, "</a></font> / <font color='green'><a title='比赛进行轮数' style='cursor:hand;'>", num3, "</a></font>" });
                }
                goto Label_0318;
            }
        Label_0541:
            reader.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='7'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }
    }
}


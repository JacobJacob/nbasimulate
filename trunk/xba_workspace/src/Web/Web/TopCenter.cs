namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class TopCenter : Page
    {
        protected ImageButton btnOK;
        public int intA;
        public int intB;
        private int intCategory;
        private int intClubID;
        private int intClubID5;
        public int intDevIndexI;
        public int intLevelI;
        private int intPage;
        private int intPerPage;
        private int intRegPMatchMoney;
        public int intServerCategory = ServerParameter.intGameCategory;
        public int intTag;
        public int intType;
        public int intUserID;
        protected LinkButton lbtnInDev;
        public StringBuilder sbList;
        public string strClubLogoAI;
        public string strClubLogoHI;
        public string strClubNameAI;
        public string strClubNameHI;
        public string strClubSayAI;
        public string strClubSayHI;
        public string strDevSay = "";
        public string strEndTime;
        public string strGuideCode;
        public string strImg;
        public string strInfo;
        public string strList;
        public string strScript;
        public string strScriptR;
        public string strServer = ServerParameter.strCopartner;
        public string strType;
        public string strURL = "";
        protected HtmlTable tblDevision;
        protected HtmlTable tblDevNew;
        protected HtmlTable tblInstant;
        protected HtmlTable tblJoinDev;
        protected HtmlTable tblNew;
        protected HtmlTable tblSelectTeam;
        protected HtmlTable tblViewTeam;
        protected HtmlTableRow trbOK;
        protected HtmlTableRow trHead;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            BTPGameManager.GetTurn();
            if (this.intCategory == 2)
            {
                if (BTPAccountManager.HasEnoughMoney(this.intUserID, this.intRegPMatchMoney))
                {
                    switch (BTPDevManager.SetFinanlDevByUserID(this.intUserID))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=101");
                            return;

                        case 1:
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 5);
                            base.Response.Redirect("Report.aspx?Parameter=102");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=103");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=106");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=101");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void GetLVTList()
        {
            string str;
            this.sbList.Append("<tr align='center' class='BarHead'>");
            this.sbList.Append("<td width='100%' height='25' style='padding-left:5px'>申请联赛</td>");
            this.sbList.Append("</tr>");
            DataRow gameRow = BTPGameManager.GetGameRow();
            int num = (int) gameRow["Turn"];
            int num1 = (int) gameRow["Days"];
            if (num < 0x1b)
            {
                str = "职业联赛正在进行<br><br>目前是第" + num + "轮<br><br>";
            }
            else
            {
                str = "赛季结束<br><br>";
            }
            int devCount = BTPDevManager.GetDevCount();
            if (BTPDevManager.GetDevTotal() < devCount)
            {
                int num3 = devCount;
            }
            DataRow drParameter = Global.drParameter;
            this.intRegPMatchMoney = (int) drParameter["RegPMatchMoney"];
            this.trbOK.Visible = true;
            int devBlank = BTPDevManager.GetDevBlank();
            if (devBlank > 0)
            {
                if (this.intCategory == 2)
                {
                    this.sbList.Append(string.Concat(new object[] { "<tr><td height='200' valign='center' align='center'>", str, "XBA职业联赛中已有", devCount, "家职业俱乐部注册<br><br>还有", devBlank, "个联赛空位。<br><br>职业俱乐部注册费：", this.intRegPMatchMoney, "元<br><br><br>" }));
                    this.sbList.Append("</td></tr>");
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_14.gif";
                }
                else
                {
                    this.sbList.Append("<tr><td height='200' valign='center' align='center'>" + str + "您还不能申请联赛！<br><br><br>");
                    this.sbList.Append("</td></tr>");
                    this.btnOK.Visible = false;
                    this.strImg = "<img src='" + SessionItem.GetImageURL() + "button_G_14.gif' width='66' height='24'>";
                }
            }
            else
            {
                this.sbList.Append("<tr><td height='200' valign='center' align='center'>XBA职业联赛中已有" + devCount + "家职业俱乐部注册。<br><br><br>目前已无可供申请的联赛空位，请随时留意新闻公告。<br/><br>");
                this.sbList.Append("</td></tr>");
                this.btnOK.Visible = false;
                this.strImg = "<img src='" + SessionItem.GetImageURL() + "button_G_14.gif' width='66' height='24'>";
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private void GetSTList()
        {
            string strCurrentURL = "TopCenter.aspx?UserID=" + this.intUserID + "&Type=LIST&";
            this.intPerPage = 10;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strInfo = "你可以出价收购以下球队";
            this.strEndTime = "<td colspan='4' align='center'>截止时间：" + StringItem.FormatDate(DateTime.Today, "yyyy-MM-dd") + "\t23:00:00</td>";
            DataTable table = BTPClubManager.GetSellClub5Table(this.intPage, this.intPerPage, total, intCount);
            this.sbList.Append("<tr align='center' class='BarHead'>");
            this.sbList.Append("<td width='15%' height='25'>编号</td>");
            this.sbList.Append("<td width='15%'>球队名称</td>");
            this.sbList.Append("<td width='15%'>球员数量</td>");
            this.sbList.Append("<td width='15%'>售价</td>");
            this.sbList.Append("<td width='20%'>查看</td>");
            this.sbList.Append("<td width='20%'>操作</td>");
            this.sbList.Append("</tr>");
            if (table == null)
            {
                this.sbList.Append("<tr align='center' class='BarContent'><td height='25' colspan='5'>暂时没有拍卖中的职业队。</td></tr>");
            }
            else
            {
                string str3 = "";
                string str4 = "";
                foreach (DataRow row in table.Rows)
                {
                    int intClubID = (int) row["ClubID"];
                    row["Name"].ToString().Trim();
                    int intUserID = (int) row["BidderID"];
                    string nickNameByUserID = BTPAccountManager.GetNickNameByUserID(intUserID);
                    DateTime time1 = (DateTime) row["EndBidTime"];
                    switch (nickNameByUserID)
                    {
                        case null:
                        case "":
                            nickNameByUserID = "暂无";
                            break;
                    }
                    if (this.intCategory == 2)
                    {
                        str4 = string.Concat(new object[] { "<a href='TopCenter.aspx?Type=VIEW&ClubID=", intClubID, "&UserID=", this.intUserID, "'>查看</a>" });
                        str3 = "--";
                    }
                    else if (this.intCategory == 5)
                    {
                        str4 = "--";
                        str3 = "--";
                    }
                    else if (this.intCategory == 1)
                    {
                        if (intUserID == this.intUserID)
                        {
                            str4 = string.Concat(new object[] { "<a href='TopCenter.aspx?Type=VIEW&ClubID=", intClubID, "&UserID=", this.intUserID, "'>查看</a>" });
                            str3 = "--";
                        }
                        else
                        {
                            str4 = string.Concat(new object[] { "<a href='TopCenter.aspx?Type=VIEW&ClubID=", intClubID, "&UserID=", this.intUserID, "'>查看</a>" });
                            str3 = "<a href='SecretaryPage.aspx?Type=BUYCLUB&ClubID=" + intClubID + "'>购买</a>";
                        }
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                    int num3 = BTPPlayer5Manager.GetPlayer5CountByClubID(intClubID);
                    int num4 = (int) row["Price"];
                    this.sbList.Append("<tr align='center' class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><font color='#660066'>" + intClubID + "</font></td>");
                    this.sbList.Append("<td>职业球队</td>");
                    this.sbList.Append("<td>" + num3 + "名</td>");
                    this.sbList.Append("<td>" + num4 + "</td>");
                    this.sbList.Append("<td>" + str4 + "</td>");
                    this.sbList.Append("<td>" + str3 + "</td>");
                    this.sbList.Append("</tr>");
                }
                this.sbList.Append("<tr><td height='25' align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
            }
        }

        private int GetTotal()
        {
            return BTPClubManager.GetSellClub5Count();
        }

        private void GetViewList()
        {
            int clubPrice = BTPClubManager.GetClubPrice(this.intClubID);
            this.strInfo = "查看球队球员";
            DataTable table = BTPPlayer5Manager.GetSellPlayer5Table(this.intClubID);
            this.sbList.Append("<tr align='center' class='BarHead'>");
            this.sbList.Append("<td width='100' height='25'>球员</td>");
            this.sbList.Append("<td width='78'>年龄</td>");
            this.sbList.Append("<td width='78'>身高</td>");
            this.sbList.Append("<td width='78'>体重</td>");
            this.sbList.Append("<td width='78'>位置</td>");
            this.sbList.Append("<td width='78'>综合</td>");
            this.sbList.Append("<td width='78'>薪水</td>");
            this.sbList.Append("</tr>");
            if (table == null)
            {
                this.sbList.Append("<tr align='center' class='BarContent'><td height='25' colspan='7'>暂时没有球员。</td></tr>");
            }
            else
            {
                DateTime time1 = (DateTime) BTPClubManager.GetClubRowByID(this.intClubID)["EndBidTime"];
                string str2 = "";
                if (this.intCategory == 1)
                {
                    str2 = "<a href='SecretaryPage.aspx?Type=BUYCLUB&ClubID=" + this.intClubID + "'>出价</a> | <a href='#' onclick='javascript:window.history.back();'>返回</a>";
                }
                else if (this.intCategory == 2)
                {
                    str2 = "<a href='#' onclick='javascript:window.history.back();'>返回</a>";
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                foreach (DataRow row2 in table.Rows)
                {
                    string str = row2["Name"].ToString().Trim();
                    int num = (byte) row2["Age"];
                    int num2 = (byte) row2["Height"];
                    int num3 = (byte) row2["Weight"];
                    int intPosition = (byte) row2["Pos"];
                    int intAbility = (int) row2["Ability"];
                    int num6 = (int) row2["Salary"];
                    float single1 = ((float) intAbility) / 10f;
                    this.sbList.Append("<tr align='center' class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><font color='#660066'>" + str + "</font></td>");
                    this.sbList.Append("<td>" + num + "</td>");
                    this.sbList.Append("<td>" + num2 + "</td>");
                    this.sbList.Append("<td>" + num3 + "</td>");
                    this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</a></td>");
                    this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                    this.sbList.Append("<td>" + num6 + "</td>");
                    this.sbList.Append("</tr>");
                }
                clubPrice = BTPClubManager.GetClubPrice(this.intClubID);
                this.sbList.Append("<tr align='center' class='BarContent'>");
                this.sbList.Append("<td height='25' colspan='5'>球队现在的竞价是：" + clubPrice + "</td>");
                this.sbList.Append("<td colspan='2'>" + str2 + "</td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='10' colspan='7'></td></tr>");
                this.sbList.Append("<tr align='center' class='BarCountent'>");
                this.sbList.Append("<td height='25' colspan='7'></td>");
                this.sbList.Append("</tr>");
            }
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
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
            return string.Concat(new object[] { str2, " ", str3, " 共", total, "个记录 跳转", str4 });
        }

        private string GetVTList()
        {
            string str3;
            int num7;
            int intRound = (int) BTPGameManager.GetGameRow()["Turn"];
            bool flag = true;
            DataRow clubRowByID = BTPClubManager.GetClubRowByID(this.intClubID5);
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(this.intClubID5);
            int level = DevCalculator.GetLevel(devCodeByClubID);
            DevCalculator.GetDev(devCodeByClubID);
            int devIndex = DevCalculator.GetDevIndex(devCodeByClubID);
            int orderByClubID = BTPDevManager.GetOrderByClubID(this.intClubID5);
            string line = clubRowByID["MainXML"].ToString().Trim();
            if ((bool) BTPDevManager.GetDevRowByClubID(this.intClubID5)["HasNewMsg"])
            {
                str3 = " [<font color='red'>新</font>]";
            }
            else
            {
                str3 = "";
            }
            TagReader reader = new TagReader(line);
            int intScore = 0;
            int num6 = 0;
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            string str10 = "0";
            string str11 = "0";
            string str12 = "";
            string str13 = "0";
            string str14 = "0";
            string str15 = "0";
            string str16 = "0";
            string str17 = "0";
            string tagline = "";
            string str19 = "";
            string str20 = "";
            string str21 = "";
            string str22 = "";
            string str23 = "";
            try
            {
                if (intRound == 1)
                {
                    intScore = 0;
                    num6 = 0;
                    str4 = "";
                    str5 = "";
                    str6 = "";
                    str7 = "";
                    str10 = "0";
                    str11 = "0";
                    str12 = "";
                    str13 = "0";
                    str14 = "0";
                    str15 = "0";
                    str16 = "0";
                    str17 = "0";
                    str8 = "";
                    str9 = "";
                }
                else
                {
                    intScore = Convert.ToInt32(reader.GetTagline("ScoreH"));
                    num6 = Convert.ToInt32(reader.GetTagline("ScoreA"));
                    str4 = reader.GetTagline("ClubNameH");
                    str5 = reader.GetTagline("ClubNameA");
                    str6 = reader.GetTagline("ClubLogoH");
                    str7 = reader.GetTagline("ClubLogoA");
                    str10 = reader.GetTagline("Tickets");
                    str11 = reader.GetTagline("Income");
                    str12 = reader.GetTagline("MVPName");
                    str8 = reader.GetTagline("ClubSayH");
                    str9 = reader.GetTagline("ClubSayA");
                    if ((str4 == "") || (str5 == ""))
                    {
                        str13 = "0";
                        str14 = "0";
                        str15 = "0";
                        str16 = "0";
                        str17 = "0";
                        str8 = "";
                        str9 = "";
                    }
                    else
                    {
                        string[] strArray = reader.GetTagline("MVPStas").Split(new char[] { '|' });
                        str13 = strArray[0];
                        str14 = strArray[1];
                        str15 = strArray[2];
                        str17 = strArray[3];
                        str16 = strArray[4];
                    }
                }
                tagline = reader.GetTagline("NClubNameH");
                str19 = reader.GetTagline("NClubNameA");
                str20 = reader.GetTagline("NClubLogoH");
                str21 = reader.GetTagline("NClubLogoA");
                str22 = reader.GetTagline("NClubSayH");
                str23 = reader.GetTagline("NClubSayA");
            }
            catch
            {
                flag = false;
            }
            StringBuilder builder = new StringBuilder();
            if (intRound != 1)
            {
                num7 = intRound - 1;
            }
            else
            {
                num7 = intRound;
            }
            if ((BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, num7) == null) || !flag)
            {
                builder.Append("<table width='536'  border='0' cellspacing='0' cellpadding='0'>");
                builder.Append("<tr class='BarHead'>");
                builder.Append("<td height='25' width='268' align='right'>已注册球队</td>");
                builder.Append("<td width='268' align='right'>第");
                builder.Append(level + "等级 第");
                builder.Append(devIndex);
                builder.Append("联赛&nbsp;&nbsp;&nbsp;&nbsp;</td>");
                builder.Append("</tr>");
                BTPDevManager.GetClubCountByDevCode(devCodeByClubID);
                if (BTPDevManager.GetClubTableByDevCode(devCodeByClubID) == null)
                {
                    builder.Append("<tr><td width='100%' align='center' colspan='2'>联赛暂无球队。</td></tr><table>");
                }
                else
                {
                    string str24;
                    string str25;
                    string str26;
                    int num8;
                    int num9;
                    builder.Append("<tr><td width='268'><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
                    DataTable table2 = BTPDevManager.GetClubTableByDevCodeTop7(devCodeByClubID);
                    if (table2 == null)
                    {
                        builder.Append("<tr><td></td></tr>");
                    }
                    else
                    {
                        foreach (DataRow row4 in table2.Rows)
                        {
                            num9 = (int) row4["ClubID"];
                            if (num9 != 0)
                            {
                                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(num9);
                                str24 = clubRowByClubID["ClubLogo"].ToString().Trim();
                                str25 = clubRowByClubID["ClubName"].ToString().Trim();
                                int num1 = (int) clubRowByClubID["UserID"];
                                str26 = clubRowByClubID["NickName"].ToString().Trim();
                                num8 = (byte) clubRowByClubID["Levels"];
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(SessionItem.GetImageURL());
                                builder.Append("Club/Logo/" + num8 + "/");
                                builder.Append(str24);
                                builder.Append(".gif'></td>");
                                builder.Append("<td style='padding-left:5px'>球队：");
                                builder.Append(str25);
                                builder.Append("<br>经理：");
                                builder.Append(str26);
                                builder.Append("</td></tr>");
                            }
                            else
                            {
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(SessionItem.GetImageURL());
                                builder.Append("Club/Logo/0.gif'></td>");
                                builder.Append("<td style='padding-left:5px'></td>");
                                builder.Append("</tr>");
                            }
                        }
                    }
                    builder.Append("</table></td>");
                    builder.Append("<td width='268'><table bgcolor='#FBE2D4' width='100%'  border='0' cellspacing='0' cellpadding='0'>");
                    DataTable table3 = BTPDevManager.GetClubTableByDevCodeEnd7(devCodeByClubID);
                    if (table3 == null)
                    {
                        builder.Append("<tr><td></td></tr>");
                    }
                    else
                    {
                        foreach (DataRow row6 in table3.Rows)
                        {
                            num9 = (int) row6["ClubID"];
                            if (num9 != 0)
                            {
                                DataRow row7 = BTPClubManager.GetClubRowByClubID(num9);
                                str24 = row7["ClubLogo"].ToString().Trim();
                                str25 = row7["ClubName"].ToString().Trim();
                                int num10 = (int) row7["UserID"];
                                str26 = row7["NickName"].ToString().Trim();
                                num8 = (byte) row7["Levels"];
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(SessionItem.GetImageURL());
                                builder.Append("Club/Logo/");
                                builder.Append(num8);
                                builder.Append("/");
                                builder.Append(str24);
                                builder.Append(".gif'></td>");
                                builder.Append("<td style='padding-left:5px'>球队：");
                                builder.Append(str25);
                                builder.Append("<br>经理：");
                                builder.Append(str26);
                                builder.Append("</td>");
                                builder.Append("</tr>");
                            }
                            else
                            {
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(SessionItem.GetImageURL());
                                builder.Append("Club/Logo/0.gif'></td>");
                                builder.Append("<td style='padding-left:5px'></td>");
                                builder.Append("</tr>");
                            }
                        }
                    }
                    builder.Append("</table></td></tr></table>");
                }
            }
            else
            {
                DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound);
                StringBuilder builder2 = new StringBuilder();
                if (devMRowByClubIDRound != null)
                {
                    int num11 = (int) devMRowByClubIDRound["DevMatchID"];
                    builder2.Append("\t<tr class='BarHead' height='25'>");
                    builder2.Append("\t\t<td><strong>本轮比赛对手</strong></td>");
                    builder2.Append("\t</tr>");
                    builder2.Append("\t<tr>");
                    builder2.Append("\t\t<td height='150' align='center'>");
                    if ((tagline == "") || (str19 == ""))
                    {
                        builder2.Append("按照赛程，比赛轮空");
                    }
                    else
                    {
                        builder2.Append("\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td height='50'>");
                        builder2.Append("\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        builder2.Append("\t\t\t\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px;'><font style='line-height:140%'><font color='#660066'><strong>" + tagline + "</strong></font><br><font color='#666666'>" + str22 + "</font></font></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='10%'><img src='Images/Club/Logo/" + str20 + "' border='0' width='46' height='46'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='30%' align='center'><img src='Images/Score/99.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/99.gif' width='19' height='28' border='0'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='Images/Club/Logo/" + str21 + "' border='0' width='46' height='46'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str19 + "</strong></font><br><font color='#666666'>" + str23 + "</font></font></td>");
                        builder2.Append("\t\t\t\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t\t\t</table>");
                        builder2.Append("\t\t\t\t\t</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25' align='center'>常规赛期间，每日4:00-10:00进行比赛，第13轮和第14轮之间休赛一天</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>比赛球场</strong>：" + tagline + "主场</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append(string.Concat(new object[] { "\t\t\t\t\t<td style='padding-left:5px;' height='25'><a href='Main_P.aspx?Tag=", this.intUserID, "&Type=DEVISION' target='_parent'>查看联赛</a>&nbsp;|&nbsp;<a href='Main_P.aspx?Tag=", this.intUserID, "&Type=VARRANGE' target='_parent'>战术安排</a></td>" }));
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25' align='right'><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str3 + "</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t</table>");
                    }
                    builder2.Append("\t\t</td>");
                    builder2.Append("\t</tr>");
                }
                else
                {
                    builder2.Append("\t<tr class='BarHead' height='25'>");
                    builder2.Append("\t\t<td><strong>本轮比赛对手</strong></td>");
                    builder2.Append("\t</tr>");
                    builder2.Append("\t<tr>");
                    builder2.Append("\t\t<td height='150' align='center'>赛季结束</td>");
                    builder2.Append("\t</tr>");
                }
                builder.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                builder.Append("\t<tr class='BarHead' height='25'>");
                builder.Append("\t\t<td>");
                builder.Append("\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                builder.Append("\t\t\t\t<tr>");
                builder.Append("\t\t\t\t\t<td width='55%' align='right'><strong>上轮比赛结果</strong></td>");
                builder.Append(string.Concat(new object[] { "\t\t\t\t\t<td width='45%' align='right' style='padding-right:4px;'>第", level, "等级 第", devIndex, "联赛 第", orderByClubID, "名</td>" }));
                builder.Append("\t\t\t\t</tr>");
                builder.Append("\t\t\t</table>");
                builder.Append("\t\t</td>");
                builder.Append("\t</tr>");
                builder.Append("\t<tr>");
                builder.Append("\t\t<td height='150' align='center'>");
                if (intRound == 1)
                {
                    builder.Append("无比赛");
                }
                else if ((str4 == "") || (str5 == ""))
                {
                    builder.Append("按照赛程，比赛轮空");
                }
                else
                {
                    DataRow row9 = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound - 1);
                    string str27 = Config.GetDomain() + "VRep.aspx?Tag=" + row9["DevMatchID"].ToString() + "&Type=2&A=" + row9["ClubHID"].ToString() + "&B=" + row9["ClubAID"].ToString();
                    string str28 = Config.GetDomain() + "VStas.aspx?Tag=" + row9["DevMatchID"].ToString() + "&Type=2&A=" + row9["ClubHID"].ToString() + "&B=" + row9["ClubAID"].ToString();
                    int num12 = (int) row9["DevMatchID"];
                    int num13 = (int) row9["MsgCount"];
                    builder.Append("\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td height='50'>");
                    builder.Append("\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str4 + "</strong></font><br><font color='#666666'>" + str8 + "</font></font></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='10%'><img src='Images/Club/Logo/" + str6 + "' border='0' width='46' height='46'></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='30%' align='center'>" + MatchItem.GetScore(intScore) + "<img src='Images/Score/00.gif' width='19' height='28' border='0'>" + MatchItem.GetScore(num6) + "</td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='Images/Club/Logo/" + str7 + "' border='0' width='46' height='46'></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str5 + "</strong></font><br><font color='#666666'>" + str9 + "</font></font></td>");
                    builder.Append("\t\t\t\t\t\t\t</tr>");
                    builder.Append("\t\t\t\t\t\t</table>");
                    builder.Append("\t\t\t\t\t</td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>门票出售</strong>：" + str10 + "张&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>总收入</strong>：" + str11 + "</td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>本场MVP</strong>：" + str12 + "&nbsp;[&nbsp;得分：" + str13 + "&nbsp;|&nbsp;篮板：" + str14 + "&nbsp;|&nbsp;助攻：" + str15 + "&nbsp;|&nbsp;盖帽：" + str16 + "&nbsp;|&nbsp;抢断：" + str17 + "&nbsp;]</td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><a href='" + str28 + "' target='_blank'>比赛统计</a>&nbsp;|&nbsp;<a href='" + str27 + "' target='_blank'>比赛战报</a></td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25' align='right'><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str3 + "</td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t</table>");
                }
                builder.Append("\t\t</td>");
                builder.Append("\t</tr>");
                builder.Append(builder2.ToString());
                builder.Append("</table>");
            }
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            this.lbtnInDev.Click += new EventHandler(this.lbtnInDev_Click);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void lbtnInDev_Click(object sender, EventArgs e)
        {
            BTPGameManager.GetTurn();
            int intMoney = (int) Global.drParameter["RegPMatchMoney"];
            if (this.intCategory == 2)
            {
                if (BTPAccountManager.HasEnoughMoney(this.intUserID, intMoney))
                {
                    switch (BTPDevManager.SetFinanlDevByUserID(this.intUserID))
                    {
                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=101");
                            return;

                        case 1:
                            DTOnlineManager.ChangeCategoryByUserID(this.intUserID, 5);
                            base.Response.Redirect("Report.aspx?Parameter=102");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=103");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=106");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=101");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
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
            this.intCategory = (int) onlineRowByUserID["Category"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.strGuideCode = onlineRowByUserID["GuideCode"].ToString().Trim();
            this.strType = SessionItem.GetRequest("Type", 1).ToString();
            if ((((this.intCategory == 5) && (this.strType != "INSTANT")) && ((this.strType != "NEW") && (this.strType != "DEVNEW"))) && !SessionItem.CanUseAfterUpdate())
            {
                base.Response.Redirect("Report.aspx?Parameter=1001a");
                return;
            }
            if (((this.intCategory != 1) && (this.intCategory != 2)) && (this.intCategory != 5))
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            this.trbOK.Visible = false;
            this.tblSelectTeam.Visible = false;
            this.tblViewTeam.Visible = false;
            this.tblJoinDev.Visible = false;
            this.tblDevision.Visible = false;
            this.tblNew.Visible = false;
            this.tblDevNew.Visible = false;
            this.trHead.Visible = true;
            this.lbtnInDev.Visible = false;
            this.tblInstant.Visible = false;
            this.strScriptR = this.strScriptR + "<script type=\"text/javascript\"> \nfunction copytoZoneBoard() \n{\nvar clipBoardContent=\"\";\nclipBoardContent=\"http://union.xba.com.cn/zone/" + this.intUserID.ToString() + ".aspx\";\nwindow.clipboardData.setData(\"Text\",clipBoardContent);\nalert(\"复制成功，已经复制到剪贴板，您可以打开QQ或者MSN的对话框，然后使用（Ctrl+V或鼠标右键）粘贴功能给您的好友^_^\");\n}\n</script>\n";
            switch (this.strType)
            {
                case "NEW":
                    this.trHead.Visible = false;
                    this.tblNew.Visible = true;
                    goto Label_0547;

                case "DEVNEW":
                    this.trHead.Visible = false;
                    this.tblDevNew.Visible = true;
                    if (this.intCategory == 2)
                    {
                        this.lbtnInDev.Visible = true;
                    }
                    else
                    {
                        this.lbtnInDev.Visible = false;
                        DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                        if (accountRowByUserID != null)
                        {
                            int clubCountByDevCode = BTPDevManager.GetClubCountByDevCode(accountRowByUserID["DevCode"].ToString().Trim());
                            clubCountByDevCode = 14 - clubCountByDevCode;
                            if (clubCountByDevCode > 0)
                            {
                                this.strDevSay = "距离排定赛程还剩" + clubCountByDevCode + "人，请于排定赛程次日早9点观看比赛直播。";
                            }
                            else
                            {
                                this.strDevSay = "请于排定赛程次日早9点观看比赛直播。";
                            }
                        }
                        else
                        {
                            base.Response.Redirect("Report.aspx?Parameter=3");
                        }
                    }
                    goto Label_0547;

                case "LIST":
                    if (this.intCategory != 1)
                    {
                        if (this.intCategory != 2)
                        {
                            if (this.intCategory != 5)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=3");
                                return;
                            }
                            this.sbList = new StringBuilder();
                            this.strList = this.GetVTList();
                            this.tblDevision.Visible = true;
                        }
                        else
                        {
                            this.sbList = new StringBuilder();
                            this.GetLVTList();
                            this.tblJoinDev.Visible = true;
                        }
                    }
                    else
                    {
                        this.sbList = new StringBuilder();
                        this.GetSTList();
                        this.tblSelectTeam.Visible = true;
                    }
                    goto Label_0547;

                case "INSTANT":
                {
                    if (this.intCategory != 5)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1002");
                        return;
                    }
                    this.intType = (int) SessionItem.GetRequest("Status", 0);
                    this.intTag = (int) SessionItem.GetRequest("Tag", 0);
                    this.intA = (int) SessionItem.GetRequest("A", 0);
                    this.intB = (int) SessionItem.GetRequest("B", 0);
                    this.strURL = Config.GetDomain();
                    this.tblInstant.Visible = true;
                    string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(this.intClubID5);
                    this.intLevelI = DevCalculator.GetLevel(devCodeByClubID);
                    this.intDevIndexI = DevCalculator.GetDevIndex(devCodeByClubID);
                    DataRow clubRowByID = BTPClubManager.GetClubRowByID(this.intClubID5);
                    if (clubRowByID == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                    TagReader reader = new TagReader(clubRowByID["MainXML"].ToString().Trim());
                    this.strClubNameHI = reader.GetTagline("ClubNameH");
                    this.strClubNameAI = reader.GetTagline("ClubNameA");
                    this.strClubLogoHI = reader.GetTagline("ClubLogoH");
                    this.strClubLogoAI = reader.GetTagline("ClubLogoA");
                    this.strClubSayHI = reader.GetTagline("ClubSayH");
                    this.strClubSayAI = reader.GetTagline("ClubSayA");
                    break;
                }
                case "VIEW":
                    this.intClubID = (int) SessionItem.GetRequest("ClubID", 0);
                    if (this.intCategory == 1)
                    {
                        this.sbList = new StringBuilder();
                        this.GetViewList();
                        this.tblViewTeam.Visible = true;
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                    break;
            }
        Label_0547:
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


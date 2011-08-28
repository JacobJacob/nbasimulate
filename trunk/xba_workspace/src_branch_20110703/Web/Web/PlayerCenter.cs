namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class PlayerCenter : Page
    {
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        private int intCount;
        private int intPage;
        private int intPerPage;
        private int intTotal;
        public int intType;
        private int intUserID;
        public long longFirstPlayerID;
        public long longInTeamPlayerID;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        public string strBtnCancel = "";
        public string strNickName;
        private string strOp;
        public string strSalaryTop = "";
        public string strSalaryTopDown = "";
        protected HtmlTable tbMedicineCenter;
        protected HtmlTable tbPlayerList;

        private void GetChoosePlayerList(DataTable dt)
        {
            if (dt == null)
            {
                this.sbList.Append("");
            }
            else
            {
                int num12 = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string str2;
                    long longPlayerID = (long) row["PlayerID"];
                    if (num12 == 0)
                    {
                        this.longFirstPlayerID = longPlayerID;
                    }
                    string strName = row["Name"].ToString();
                    int num2 = (byte) row["Number"];
                    int num3 = (byte) row["Age"];
                    int intPosition = (byte) row["Pos"];
                    int intPower = (byte) row["Power"];
                    int num6 = (byte) row["Height"];
                    int num7 = (byte) row["Weight"];
                    int intAbility = (int) row["Ability"];
                    float single1 = ((float) ((int) row["Ability"])) / 10f;
                    int intStatus = (byte) row["Status"];
                    int num9 = (byte) row["Category"];
                    string strEvent = row["Event"].ToString();
                    int intSuspend = (byte) row["Suspend"];
                    DataRow row2 = BTPArrange3Manager.GetCheckArrange3(this.intClubID3, longPlayerID);
                    if (num9 == 1)
                    {
                        if (row2 != null)
                        {
                            str2 = "阵容中";
                        }
                        else if (num3 > 0x11)
                        {
                            str2 = "<a href='SecretaryPage.aspx?Type=CHOOSE&PlayerID=" + longPlayerID + "'>职业选拔</a>";
                        }
                        else
                        {
                            str2 = "--";
                        }
                    }
                    else if (num9 == 4)
                    {
                        str2 = "<font color='#660066'>拍卖中</font>";
                    }
                    else
                    {
                        str2 = "禁止操作";
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\" width='16' height='19' border='0'></td>" }));
                    this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 3, 1, 1) + "</td>");
                    this.sbList.Append("<td>" + num3 + "</td>");
                    this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetPowerColor(intPower) + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "</td>");
                    this.sbList.Append("<td>" + num6 + "</td>");
                    this.sbList.Append("<td>" + num7 + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                    this.sbList.Append("<td>" + str2 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='10'></td></tr>");
                    num12++;
                }
            }
        }

        private string GetOldPlayerList(DataTable dt)
        {
            if (dt == null)
            {
                return "";
            }
            int num2 = 0;
            foreach (DataRow row in dt.Rows)
            {
                long num = (long) row["PlayerID"];
                if (num2 == 0)
                {
                    this.longFirstPlayerID = num;
                }
                num2++;
            }
            return "";
        }

        private void GetPlayerList(DataTable dt)
        {
            string str2 = "";
            string str5 = "";
            bool flag = false;
            if (dt == null)
            {
                this.sbList.Append("");
            }
            else
            {
                int num15 = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DataRow row2;
                    long longPlayerID = (long) row["PlayerID"];
                    if (num15 == 0)
                    {
                        this.longFirstPlayerID = longPlayerID;
                    }
                    string strName = row["Name"].ToString();
                    int num2 = (byte) row["Number"];
                    int num3 = (byte) row["Age"];
                    int intPosition = (byte) row["Pos"];
                    int intPower = (byte) row["Power"];
                    int num6 = (byte) row["Height"];
                    int num7 = (byte) row["Weight"];
                    float single1 = ((float) ((int) row["Ability"])) / 10f;
                    int intStatus = (byte) row["Status"];
                    int num9 = (byte) row["Category"];
                    string strEvent = row["Event"].ToString();
                    int intAbility = (int) row["Ability"];
                    if (num9 == 3)
                    {
                        intAbility = 999;
                    }
                    int intSuspend = (byte) row["Suspend"];
                    int num14 = (byte) row["Happy"];
                    bool flag2 = (bool) row["IsRetire"];
                    if (this.intType == 5)
                    {
                        flag = (bool) row["IsDevision"];
                    }
                    if (this.intType == 3)
                    {
                        row2 = BTPArrange3Manager.GetCheckArrange3(this.intClubID3, longPlayerID);
                    }
                    else
                    {
                        row2 = BTPArrange5Manager.GetCheckArrange5(this.intClubID5, longPlayerID);
                    }
                    if (num9 == 1)
                    {
                        if (row2 != null)
                        {
                            str2 = "阵容中";
                        }
                        else if (this.intType == 3)
                        {
                            str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=PLAYER", this.intType, "BID&PlayerID=", longPlayerID, "'>选秀</a> | <a href='SecretaryPage.aspx?Type=PLAYER", this.intType, "FIRE&PlayerID=", longPlayerID, "'>下放</a>" });
                        }
                        else if (this.intType == 5)
                        {
                            str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=PLAYER", this.intType, "BID&PlayerID=", longPlayerID, "'>出售</a>" });
                        }
                    }
                    else if ((this.intType == 3) && (num9 == 4))
                    {
                        str2 = "<font color='#660066'>选秀中</font>";
                    }
                    else if ((this.intType == 5) && (num9 == 2))
                    {
                        str2 = "<font color='#660066'>拍卖中</font>";
                    }
                    else if ((this.intType == 5) && (num9 == 3))
                    {
                        if (row2 == null)
                        {
                            str2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=UNSETTRIAL&PlayerID=", longPlayerID, "'>结束试训</a>" });
                        }
                        else
                        {
                            str2 = "试训中";
                        }
                    }
                    else
                    {
                        str2 = "禁止操作";
                    }
                    switch (this.intType)
                    {
                        case 3:
                            this.strOp = "";
                            goto Label_0369;

                        case 5:
                            if (!flag || (((byte) row["Contract"]) >= 10))
                            {
                                break;
                            }
                            this.strOp = "<a href='Extend.aspx?PlayerID=" + longPlayerID + "' >续约</a>";
                            goto Label_0369;

                        default:
                            this.strOp = "";
                            goto Label_0369;
                    }
                    this.strOp = "--";
                Label_0369:
                    if (this.intType == 5)
                    {
                        int num11 = (int) row["Salary"];
                        int num12 = (byte) row["Contract"];
                        this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\" width='16' height='19' border='0'></td>" }));
                        this.sbList.Append("<td align='left' style='padding-left:3px' height='25'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, this.intType, 1, 1) + "</td>");
                        if (flag2)
                        {
                            this.sbList.Append("<td><a href=\"javascript:;\" style=\"cursor:pointer; color:red\" title=\"球员将在赛季结束后退役\" >" + num3 + "!</a></td>");
                        }
                        else
                        {
                            this.sbList.Append("<td>" + num3 + "</td>");
                        }
                        this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</a></td>");
                        this.sbList.Append("<td>" + num6 + "</td>");
                        this.sbList.Append("<td>" + num7 + "</td>");
                        this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                        this.sbList.Append("<td>" + num11 + "</td>");
                        this.sbList.Append("<td>" + num12 + "</td>");
                        this.sbList.Append("<td>" + str2 + "</td>");
                        this.sbList.Append("<td>" + this.strOp + "</td>");
                        this.sbList.Append("</tr>");
                        this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                    }
                    else
                    {
                        string str4;
                        if ((num14 >= 0) && (num14 < 50))
                        {
                            str4 = "6.gif";
                            str5 = "alt='坚决罢训'";
                        }
                        else if ((num14 >= 50) && (num14 < 0x41))
                        {
                            str4 = "5.gif";
                            str5 = "alt='反感训练'";
                        }
                        else if ((num14 >= 0x41) && (num14 < 0x4b))
                        {
                            str4 = "4.gif";
                            str5 = "alt='训练懈怠'";
                        }
                        else if ((num14 >= 0x4b) && (num14 < 0x55))
                        {
                            str4 = "3.gif";
                            str5 = "alt='正常训练'";
                        }
                        else if ((num14 >= 0x55) && (num14 < 0x5f))
                        {
                            str4 = "2.gif";
                            str5 = "alt='训练积极'";
                        }
                        else
                        {
                            str4 = "1.gif";
                            str5 = "alt='渴望训练'";
                        }
                        this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\" width='16' height='19' border='0'></td>" }));
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, this.intType, 1, 1) + "</td>");
                        if (flag2)
                        {
                            this.sbList.Append("<td><a href=\"javascript:;\" style=\"cursor:pointer; color:red\" title=\"球员将在赛季结束后退役\" >" + num3 + "!</a></td>");
                        }
                        else
                        {
                            this.sbList.Append("<td>" + num3 + "</td>");
                        }
                        this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</a></td>");
                        this.sbList.Append("<td>" + PlayerItem.GetPowerColor(intPower) + "</td>");
                        this.sbList.Append("<td>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "&nbsp;&nbsp;<img src='" + SessionItem.GetImageURL() + "Player/Face/" + str4 + "' " + str5 + " hight='12' width='12' border='0'></td>");
                        this.sbList.Append("<td>" + num6 + "</td>");
                        this.sbList.Append("<td>" + num7 + "</td>");
                        this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                        this.sbList.Append("<td>" + str2 + "</td>");
                        this.sbList.Append("<td>" + this.strOp + "</td>");
                        this.sbList.Append("</tr>");
                        this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                    }
                    num15++;
                }
            }
        }

        public string GetSalaryTop(int intSalaryTop, int intClubID)
        {
            int clubSalaryByClubID = BTPPlayer5Manager.GetClubSalaryByClubID(intClubID);
            int clubAllSalaryByClubID = BTPPlayer5Manager.GetClubAllSalaryByClubID(intClubID);
            int num3 = clubSalaryByClubID - intSalaryTop;
            if (num3 > 0)
            {
                this.strSalaryTop = string.Concat(new object[] { "主力工资", clubSalaryByClubID, " 工资帽", intSalaryTop, " <font color='red'>已超工资帽</font>" });
                return string.Concat(new object[] { "球员工资总和：", clubAllSalaryByClubID, " 需要交纳 <font color='red'>", DevCalculator.GetPenalty(intSalaryTop, clubSalaryByClubID), "</font> 的奢侈税。" });
            }
            this.strSalaryTop = string.Concat(new object[] { "主力工资", clubSalaryByClubID, " 工资帽", intSalaryTop, " <font color='red'>未达工资帽</font>" });
            return ("球员工资总和：" + clubAllSalaryByClubID + " 奢侈税无。");
        }

        public string GetSalaryTopChoose(int intSalaryTop, int intClubID)
        {
            int clubSalaryByClubID = BTPPlayer5Manager.GetClubSalaryByClubID(intClubID);
            int clubAllSalaryByClubID = BTPPlayer5Manager.GetClubAllSalaryByClubID(intClubID);
            int num3 = clubSalaryByClubID - intSalaryTop;
            if (num3 > 0)
            {
                return string.Concat(new object[] { "工资总和", clubAllSalaryByClubID, "，主力工资帽", intSalaryTop, "，奢侈税", DevCalculator.GetPenalty(intSalaryTop, clubSalaryByClubID) });
            }
            return string.Concat(new object[] { "工资总和", clubAllSalaryByClubID, "，主力工资帽", intSalaryTop, "，未达工资帽" });
        }

        private void GetSearchPlayerEndList(DataTable dt)
        {
            string str2 = "";
            string str3 = "未录用";
            if (dt == null)
            {
                this.sbList.Append("");
            }
            else
            {
                int num9 = 0;
                string str4 = "";
                int intType = 7;
                string str5 = "";
                foreach (DataRow row in dt.Rows)
                {
                    long longPlayerID = (long) row["PlayerID"];
                    if (num9 == 0)
                    {
                        this.longFirstPlayerID = longPlayerID;
                    }
                    string strName = row["Name"].ToString();
                    int num3 = (byte) row["Number"];
                    int num4 = (byte) row["Age"];
                    int intPosition = (byte) row["Pos"];
                    int num6 = (byte) row["Height"];
                    int num7 = (byte) row["Weight"];
                    float single1 = ((float) ((int) row["Ability"])) / 10f;
                    byte num1 = (byte) row["Category"];
                    int intAbility = (int) row["Ability"];
                    int num2 = (int) row["AbilityMax"];
                    if (num2 > 550)
                    {
                        str2 = "<font color=\"#fe0022\">天下的巨星</font>";
                        str5 = "不愧是" + this.strNickName + "经理！您竟然一次就选中了一个" + str2 + "！果然是慧眼识英才！您的球队一定会越来越强大的！";
                    }
                    else if ((num2 > 520) && (num2 <= 550))
                    {
                        str2 = "<font color=\"#0000fe\">旷世的奇才</font>";
                        str5 = "太厉害了！您选择了一个" + str2 + "，将来的他一定会成为您球队中的一线球员！我很期待再次与您合作！";
                    }
                    else if ((num2 > 480) && (num2 <= 520))
                    {
                        str2 = "<font color=\"#b01b2f\">篮坛的奇葩</font>";
                        str5 = "您选择的球员是一个" + str2 + "，他的能力也是得到我的肯定的，不过应该还有更加有潜力的球员，下次努力吧！";
                    }
                    else
                    {
                        str2 = "<font color=\"#009418\">碌碌无为的</font>";
                        str5 = "您选择的是一个" + str2 + "球员，请看一下我对其他球员的评价，希望您下次会有更好的眼光。";
                    }
                    if (longPlayerID == this.longInTeamPlayerID)
                    {
                        str4 = str5;
                        intType = 3;
                        str3 = "已录用";
                    }
                    else
                    {
                        intType = 7;
                        str3 = "<font color=\"#CCCCCC\">未录用</font>";
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num3, ".gif\" width='16' height='19' border='0'></td>" }));
                    this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, intType, 1, 1) + "</td>");
                    this.sbList.Append("<td>" + num4 + "</td>");
                    this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</a></td>");
                    this.sbList.Append("<td>" + num6 + "</td>");
                    this.sbList.Append("<td>" + num7 + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                    this.sbList.Append("<td><strong>" + str2 + "</strong></td>");
                    this.sbList.Append("<td>" + str3 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                    num9++;
                }
                this.sbList.Append("<tr><td colspan='9' height=\"20\"></td></tr>");
                this.sbList.Append("<tr><td colspan='9'>");
                this.sbList.Append("<table  width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sbList.Append("<tr>");
                this.sbList.Append("<td width=\"183\"><img src=\"Images/t.gif\" border=\"0\" width=\"183\" height=\"135\"></td>");
                this.sbList.Append(string.Concat(new object[] { "<td style=\"padding-left:4px; line-height:18px; padding-left:10px;\">", str4, "<br><a href=\"PlayerCenter.aspx?Type=7&UserID=", this.intUserID, "\">再次寻找球员</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"PlayerCenter.aspx?UserID=", this.intUserID, "&Type=3\">返回</a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("</table>");
                this.sbList.Append("</td></tr>");
            }
        }

        private void GetSearchPlayerList(DataTable dt)
        {
            if (dt == null)
            {
                this.sbList.Append("");
            }
            else
            {
                int num8 = 0;
                foreach (DataRow row in dt.Rows)
                {
                    long longPlayerID = (long) row["PlayerID"];
                    if (num8 == 0)
                    {
                        this.longFirstPlayerID = longPlayerID;
                    }
                    string strName = row["Name"].ToString();
                    int num2 = (byte) row["Number"];
                    int num3 = (byte) row["Age"];
                    int intPosition = (byte) row["Pos"];
                    int num5 = (byte) row["Height"];
                    int num6 = (byte) row["Weight"];
                    float single1 = ((float) ((int) row["Ability"])) / 10f;
                    byte num1 = (byte) row["Category"];
                    int intAbility = (int) row["Ability"];
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append(string.Concat(new object[] { "<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif\" width='16' height='19' border='0'></td>" }));
                    this.sbList.Append("<td height='25' align='left' style='padding-left:3px'>" + PlayerItem.GetPlayerNameInfo(longPlayerID, strName, this.intType, 1, 1) + "</td>");
                    this.sbList.Append("<td>" + num3 + "</td>");
                    this.sbList.Append("<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</a></td>");
                    this.sbList.Append("<td>" + num5 + "</td>");
                    this.sbList.Append("<td>" + num6 + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetAbilityColor(intAbility) + "</td>");
                    this.sbList.Append("<td  style=\"color:#b01f2e;\">未知</td>");
                    this.sbList.Append("<td><a href='SecretaryPage.aspx?Type=HIRE&PlayerID=" + longPlayerID + "'>录用</a></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                    num8++;
                }
                this.sbList.Append("<tr><td colspan='9'>");
                this.sbList.Append("<table  width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sbList.Append("<tr>");
                this.sbList.Append("<td width=\"183\"><img src=\"Images/t.gif\" border=\"0\" width=\"183\" height='135' ></td>");
                this.sbList.Append(string.Concat(new object[] { "<td style=\"padding-left:4px; line-height:18px; padding-left:10px;\">尊敬的", this.strNickName, "经理，以上是我本次为您找到的球员，您可以在他们中任选一名进入您的街球球队。如果您现在拿不定主意，可以先返回球队考虑，我会在此等候您。<br>您也可以<a href=\"SecretaryPage.aspx?Type=SEARCHAGAIN\">点击此处</a>再搜一次<fonr color=\"red\">(需100游戏币)</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"PlayerCenter.aspx?UserID=", this.intUserID, "&Type=3\">返回</a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("</table>");
                this.sbList.Append("</td></tr>");
            }
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MedicineCenter(long lngPlayerID, int intPlayerType)
        {
            DataRow parameterRow;
            byte num;
            byte num2;
            long num19;
            byte num3 = 0;
            bool flag = false;
            int num4 = 0;
            if (intPlayerType == 3)
            {
                parameterRow = BTPPlayer3Manager.GetPlayerRowByPlayerID(lngPlayerID);
                if (parameterRow == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
                int num5 = (int) parameterRow["ClubID"];
                byte num6 = (byte) parameterRow["Category"];
                num = (byte) parameterRow["Status"];
                num2 = (byte) parameterRow["Power"];
                num3 = (byte) parameterRow["Happy"];
                num4 = (int) parameterRow["Ability"];
                if ((num5 != this.intClubID3) || (num6 != 1))
                {
                    base.Response.Redirect("Report.aspx?Parameter=710");
                    return;
                }
            }
            else
            {
                parameterRow = BTPPlayer5Manager.GetPlayerRowByPlayerID(lngPlayerID);
                if (parameterRow != null)
                {
                    int num7 = (int) parameterRow["ClubID"];
                    byte num8 = (byte) parameterRow["Category"];
                    num = (byte) parameterRow["Status"];
                    num2 = (byte) parameterRow["Power"];
                    byte num1 = (byte) parameterRow["Age"];
                    flag = (bool) parameterRow["IsRetire"];
                    num4 = (int) parameterRow["Ability"];
                    if ((num7 != this.intClubID5) || (num8 != 1))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=710");
                        return;
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
            }
            byte num9 = (byte) parameterRow["Age"];
            parameterRow = BTPParameterManager.GetParameterRow();
            int num10 = (int) parameterRow["AddPowerWealth"];
            int num11 = (int) parameterRow["AddHappyWealth"];
            int num12 = num4 - 650;
            if (num12 > 0)
            {
                num12 *= 10;
            }
            else
            {
                num12 *= 5;
            }
            int num13 = (int) parameterRow["MinusAgeWealth"];
            num13 += num12;
            if (num13 < 500)
            {
                num13 = 500;
            }
            int num14 = (int) parameterRow["AddStatusWealth"];
            int num15 = (int) parameterRow["AddMaxWealth"];
            int num16 = (int) parameterRow["TimeHouseWealth"];
            int num17 = (int) parameterRow["TimeHouse5Wealth"];
            int num18 = (int) parameterRow["TimeHouse5LowWealthint"];
            if (this.Session["MedicinePID"] != null)
            {
                num19 = Convert.ToInt64(this.Session["MedicinePID"]);
            }
            else
            {
                num19 = 0L;
            }
            if (num19 > 0L)
            {
                this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayer5.aspx?PID=", num19, "\";'>" });
            }
            else
            {
                this.strBtnCancel = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"TrainPlayerCenter.aspx?Type=", intPlayerType, "&UserID=", this.intUserID, "\";'>" });
            }
            if (num2 < 100)
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>康复理疗</strong></td>");
                this.sbList.Append("<td align='left' >可一次性将球员的体力恢复到100</td>");
                this.sbList.Append("<td><font color='red'>" + num10 + "</font></td>");
                this.sbList.Append(string.Concat(new object[] { "<td><a href='SecretaryPage.aspx?PlayerType=", intPlayerType, "&Type=ADDDATAONLY&PlayerID=", lngPlayerID, "&Category=1' target='Center'>使用<a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            else
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>康复理疗</strong></td>");
                this.sbList.Append("<td align='left' >可一次性将球员的体力恢复到100</td>");
                this.sbList.Append("<td><font color='red'>" + num10 + "</font></td>");
                this.sbList.Append("<td style='color:#666666'>使用</td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            if (intPlayerType == 3)
            {
                if (num3 < 100)
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>振奋士气</strong></td>");
                    this.sbList.Append("<td align='left' >可一次性将街头球员的训练热情恢复到最佳。</td>");
                    this.sbList.Append("<td><font color='red'>" + num11 + "</font></td>");
                    this.sbList.Append(string.Concat(new object[] { "<td><a href='SecretaryPage.aspx?PlayerType=", intPlayerType, "&Type=ADDDATAONLY&PlayerID=", lngPlayerID, "&Category=2' target='Center'>使用<a></td>" }));
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                }
                else
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>振奋士气</strong></td>");
                    this.sbList.Append("<td align='left' >可一次性将街头球员的训练热情恢复到最佳。</td>");
                    this.sbList.Append("<td><font color='red'>" + num11 + "</font></td>");
                    this.sbList.Append("<td style='color:#666666'>使用</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                }
            }
            if (num9 > 0x1a)
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>返老还童</strong></td>");
                this.sbList.Append("<td align='left' >使用之后，球员的年龄将会降低1岁，小于27岁的球员无效。</td>");
                this.sbList.Append("<td><font color='red'>" + num13 + "</font></td>");
                this.sbList.Append(string.Concat(new object[] { "<td><a href='SecretaryPage.aspx?PlayerType=", intPlayerType, "&Type=ADDDATAONLY&PlayerID=", lngPlayerID, "&Category=3' target='Center'>使用<a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            else
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>返老还童</strong></td>");
                this.sbList.Append("<td align='left' >使用之后，球员的年龄将会降低1岁，小于27岁的球员无效。</td>");
                this.sbList.Append("<td><font color='red'>" + num13 + "</font></td>");
                this.sbList.Append("<td style='color:#666666'>使用</td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            if (num != 1)
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>伤病治愈</strong></td>");
                this.sbList.Append("<td align='left' >可以立即让受伤的球员恢复健康。</td>");
                this.sbList.Append("<td><font color='red'>" + num14 + "</font></td>");
                this.sbList.Append(string.Concat(new object[] { "<td><a href='SecretaryPage.aspx?PlayerType=", intPlayerType, "&Type=ADDDATAONLY&PlayerID=", lngPlayerID, "&Category=4' target='Center'>使用<a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            else
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>伤病治愈</strong></td>");
                this.sbList.Append("<td align='left' >可以立即让受伤的球员恢复健康。</td>");
                this.sbList.Append("<td><font color='red'>" + num14 + "</font></td>");
                this.sbList.Append("<td style='color:#666666'>使用</td>");
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            if (intPlayerType == 5)
            {
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='25'><strong>魔鬼训练</strong></td>");
                this.sbList.Append("<td align='left'>训练成功则现有能力值降低2点，扣除10点体力，并增加球员某一项能力的最大值2点。训练失败则仅扣除10点体力</td>");
                this.sbList.Append("<td><font color='red'>" + num15 + "</font></td>");
                this.sbList.Append(string.Concat(new object[] { "<td><a href='SecretaryPage.aspx?PlayerType=", intPlayerType, "&Type=ADDDATAONLY&PlayerID=", lngPlayerID, "&Category=5' target='Center'>使用<a></td>" }));
                this.sbList.Append("</tr>");
                this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
            }
            if (intPlayerType == 3)
            {
                if (num9 < 0x16)
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>神之领域</strong></td>");
                    this.sbList.Append("<td align='left'>送球员到神之领域中修炼，可以瞬间获得1个赛季的潜力成长，同时年龄增加1岁。22岁以下有效。</td>");
                    this.sbList.Append("<td><font color='red'>" + num16 + "</font></td>");
                    this.sbList.Append("<td><a href='SecretaryPage.aspx?Type=TIMEHOUSE&PlayerID=" + lngPlayerID + "' target='Center'>使用<a></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                }
                else
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>神之领域</strong></td>");
                    this.sbList.Append("<td align='left'>送球员到神之领域中修炼，可以瞬间获得1个赛季的潜力成长，同时年龄增加1岁。22岁以下有效。</td>");
                    this.sbList.Append("<td><font color='red'>" + num16 + "</font></td>");
                    this.sbList.Append("<td style='color:#666666'>使用</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                }
            }
            if (intPlayerType == 5)
            {
                string str;
                if (num18 < num17)
                {
                    str = "<font color='green'>" + num18 + "<br>优惠价</font>";
                }
                else
                {
                    str = "<font color='red'>" + num17 + "</font>";
                }
                if (!flag)
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>极速特训</strong></td>");
                    this.sbList.Append("<td align='left'>送球员参加极速特训，可以瞬间获得1个赛季的现有能力的成长，同时年龄增加1岁。</td>");
                    this.sbList.Append("<td>" + str + "</td>");
                    this.sbList.Append("<td><a href='Player5AutoTrain.aspx?PlayerID=" + lngPlayerID + "' target='Center'>使用<a></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
                }
                else
                {
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><strong>极速特训</strong></td>");
                    this.sbList.Append("<td align='left'>送球员参加极速特训，可以瞬间获得1个赛季的现有能力的成长，同时年龄增加1岁。</td>");
                    this.sbList.Append("<td>" + str + "</td>");
                    this.sbList.Append("<td style='color:#666666'>使用</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>");
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
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                this.intType = (int) SessionItem.GetRequest("Type", 0);
                if (((this.intCategory == 1) && (this.intType != 3)) && ((this.intType != 6) && (this.intType != 9)))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
            this.SetList();
        }

        private void SetList()
        {
            DataTable playerTableByClubID;
            switch (this.intType)
            {
                case 3:
                    if (this.intClubID3 != 0)
                    {
                        playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
                        this.sbList = new StringBuilder();
                        this.sbList.Append("<tr class='BarHead'>");
                        this.sbList.Append("<td width='40'></td>");
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                        this.sbList.Append("<td width='35'>年龄</td>");
                        this.sbList.Append("<td width='35'>位置</td>");
                        this.sbList.Append("<td width='35'>体力</td>");
                        this.sbList.Append("<td width='65'>状态</td>");
                        this.sbList.Append("<td width='40'>身高</td>");
                        this.sbList.Append("<td width='49'>体重</td>");
                        this.sbList.Append("<td width='50'>综合</td>");
                        this.sbList.Append("<td width='102'>操作</td>");
                        this.sbList.Append("<td width='1'></td>");
                        this.sbList.Append("</tr>");
                        this.GetPlayerList(playerTableByClubID);
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;

                case 4:
                    if (this.intClubID3 != 0)
                    {
                        playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
                        this.sbList = new StringBuilder();
                        this.sbList.Append("<tr class='BarHead'>");
                        this.sbList.Append("<td width='40'></td>");
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                        this.sbList.Append("<td width='40'>年龄</td>");
                        this.sbList.Append("<td width='40'>位置</td>");
                        this.sbList.Append("<td width='40'>体力</td>");
                        this.sbList.Append("<td width='50'>状态</td>");
                        this.sbList.Append("<td width='40'>身高</td>");
                        this.sbList.Append("<td width='50'>体重</td>");
                        this.sbList.Append("<td width='50'>综合</td>");
                        this.sbList.Append("<td width='102'>操作</td>");
                        this.sbList.Append("</tr>");
                        this.GetChoosePlayerList(playerTableByClubID);
                        this.strSalaryTopDown = "警告：俱乐部若超出工资帽，球队状态有可能因此而大幅下滑，请在球员管理页面查看详情。";
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;

                case 5:
                    if (this.intClubID5 != 0)
                    {
                        int salaryTop;
                        playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(this.intClubID5);
                        this.sbList = new StringBuilder();
                        this.sbList.Append("<tr class='BarHead'>");
                        this.sbList.Append("<td width='40'></td>");
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                        this.sbList.Append("<td width='40'>年龄</td>");
                        this.sbList.Append("<td width='40'>位置</td>");
                        this.sbList.Append("<td width='35'>身高</td>");
                        this.sbList.Append("<td width='35'>体重</td>");
                        this.sbList.Append("<td width='50'>综合</td>");
                        this.sbList.Append("<td width='40'>薪水</td>");
                        this.sbList.Append("<td width='35'>合同</td>");
                        this.sbList.Append("<td width='80'>操作</td>");
                        this.sbList.Append("<td width='57'>续约</td>");
                        this.sbList.Append("</tr>");
                        this.GetPlayerList(playerTableByClubID);
                        string devCodeByUserID = BTPDevManager.GetDevCodeByUserID(this.intUserID);
                        if (this.intCategory == 5)
                        {
                            salaryTop = DevCalculator.GetSalaryTop(devCodeByUserID);
                        }
                        else
                        {
                            salaryTop = DevCalculator.GetSalaryTop("0000000000");
                        }
                        this.strSalaryTopDown = this.GetSalaryTop(salaryTop, this.intClubID5);
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;

                case 6:
                    this.intPerPage = 10;
                    this.intPage = (int) SessionItem.GetRequest("Page", 0);
                    this.intTotal = BTPOldPlayerManager.GetHPlayerCount(this.intUserID);
                    this.intCount = this.intPage * this.intPerPage;
                    playerTableByClubID = BTPOldPlayerManager.GetHPlayerList(this.intUserID, this.intPage, this.intPerPage, this.intTotal, this.intCount);
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr class='BarHead'>");
                    this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                    this.sbList.Append("<td width='40'>号码</td>");
                    this.sbList.Append("<td width='40'>年龄</td>");
                    this.sbList.Append("<td width='40'>位置</td>");
                    this.sbList.Append("<td width='40'>状态</td>");
                    this.sbList.Append("<td width='40'>身高</td>");
                    this.sbList.Append("<td width='40'>体重</td>");
                    this.sbList.Append("<td width='75'>综合</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append(this.GetOldPlayerList(playerTableByClubID));
                    return;

                case 7:
                    if (this.intClubID3 != 0)
                    {
                        playerTableByClubID = BTPPlayer3Manager.GetPlayer3TableByCIDCat(this.intClubID3, 0x58);
                        if (playerTableByClubID != null)
                        {
                            if (playerTableByClubID.Rows.Count < 8)
                            {
                                BTPPlayer3Manager.DeletePlayer3ByCIDCat(this.intClubID3, 0x58);
                                playerTableByClubID = BTPPlayer3Manager.SearchPlayer3New(this.intClubID3, 0x58, DateTime.Now.ToString().Trim(), 120, 0x66);
                                if (playerTableByClubID == null)
                                {
                                    base.Response.Redirect("Report.aspx?Parameter=913");
                                }
                            }
                        }
                        else
                        {
                            playerTableByClubID = BTPPlayer3Manager.SearchPlayer3New(this.intClubID3, 0x58, DateTime.Now.ToString().Trim(), 120, 0x66);
                            if (playerTableByClubID == null)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=913");
                            }
                        }
                        this.sbList = new StringBuilder();
                        this.sbList.Append("<tr class='BarHead'>");
                        this.sbList.Append("<td width='40'></td>");
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                        this.sbList.Append("<td width='35'>年龄</td>");
                        this.sbList.Append("<td width='35'>位置</td>");
                        this.sbList.Append("<td width='35'>身高</td>");
                        this.sbList.Append("<td width='39'>体重</td>");
                        this.sbList.Append("<td width='50'>综合</td>");
                        this.sbList.Append("<td width='100'>评价</td>");
                        this.sbList.Append("<td width='102'>操作</td>");
                        this.sbList.Append("<td width='1'></td>");
                        this.sbList.Append("</tr>");
                        this.GetSearchPlayerList(playerTableByClubID);
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;

                case 8:
                    if (this.intClubID3 != 0)
                    {
                        playerTableByClubID = BTPPlayer3Manager.GetPlayer3TableByCIDCat(this.intClubID3, 0x58);
                        if (playerTableByClubID != null)
                        {
                            if (playerTableByClubID.Rows.Count < 8)
                            {
                                this.longInTeamPlayerID = (long) SessionItem.GetRequest("INID", 3);
                                playerTableByClubID = BTPPlayer3Manager.InTeamSearchTable(this.intClubID3, this.longInTeamPlayerID, 0x58);
                                if (playerTableByClubID == null)
                                {
                                    base.Response.Redirect("PlayerCenter.aspx?Type=7&UserID=" + this.intUserID);
                                }
                                else if (playerTableByClubID.Rows.Count != 8)
                                {
                                    base.Response.Redirect("PlayerCenter.aspx?Type=7&UserID=" + this.intUserID);
                                }
                            }
                            else
                            {
                                base.Response.Redirect("PlayerCenter.aspx?Type=7&UserID=" + this.intUserID);
                            }
                        }
                        else
                        {
                            base.Response.Redirect("PlayerCenter.aspx?Type=7&UserID=" + this.intUserID);
                        }
                        this.sbList = new StringBuilder();
                        this.sbList.Append("<tr class='BarHead'>");
                        this.sbList.Append("<td width='40'></td>");
                        this.sbList.Append("<td height='25' align='left' style='padding-left:3px' width='100'>姓名</td>");
                        this.sbList.Append("<td width='35'>年龄</td>");
                        this.sbList.Append("<td width='35'>位置</td>");
                        this.sbList.Append("<td width='35'>身高</td>");
                        this.sbList.Append("<td width='39'>体重</td>");
                        this.sbList.Append("<td width='50'>综合</td>");
                        this.sbList.Append("<td width='100'>评价</td>");
                        this.sbList.Append("<td width='102'>操作</td>");
                        this.sbList.Append("<td width='1'></td>");
                        this.sbList.Append("</tr>");
                        this.GetSearchPlayerEndList(playerTableByClubID);
                        return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;

                case 9:
                {
                    long request = (long) SessionItem.GetRequest("PlayerID", 3);
                    int intPlayerType = (int) SessionItem.GetRequest("PlayerType", 0);
                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr class='BarHead'>");
                    this.sbList.Append("<td height='25' width='100'>项目</td>");
                    this.sbList.Append("<td >说明</td>");
                    this.sbList.Append("<td width='40'>游戏币</td>");
                    this.sbList.Append("<td width='72'>操作</td>");
                    this.sbList.Append("</tr>");
                    this.MedicineCenter(request, intPlayerType);
                    this.tbMedicineCenter.Visible = true;
                    this.tbPlayerList.Visible = false;
                    return;
                }
            }
            if (this.intClubID3 == 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
            else
            {
                playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
                this.sbList = new StringBuilder();
                this.sbList.Append("<tr class='BarHead'>");
                this.sbList.Append("<td height='25'align='left' style='padding-left:3px' width='100'>姓名</td>");
                this.sbList.Append("<td width='40'>号码</td>");
                this.sbList.Append("<td width='40'>年龄</td>");
                this.sbList.Append("<td width='40'>位置</td>");
                this.sbList.Append("<td width='40'>体力</td>");
                this.sbList.Append("<td width='40'>状态</td>");
                this.sbList.Append("<td width='40'>身高</td>");
                this.sbList.Append("<td width='40'>体重</td>");
                this.sbList.Append("<td width='50'>综合</td>");
                this.sbList.Append("<td width='102'>操作</td>");
                this.sbList.Append("<td width='20'></td>");
                this.sbList.Append("</tr>");
                this.GetPlayerList(playerTableByClubID);
            }
        }

        private void SetPageIntro()
        {
            string str;
            string str2;
            if (this.intCategory == 1)
            {
                str = "<li class='qian2a'><font color='#aaaaaa'>职业队</font></li>";
            }
            else
            {
                str = "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/PlayerCenter.aspx?Type=PLAYER5\"' href='PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=5'>职业队</a></li>";
            }
            if (this.intCategory == 5)
            {
                str2 = "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/PlayerCenter.aspx?Type=PLAYER3TO5\"' href='PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=4'>选拔球员</a></li>";
            }
            else
            {
                str2 = "<li class='qian2a'><font color='#aaaaaa'>选拔球员</font></li>";
            }
            switch (this.intType)
            {
                case 3:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append(str2);
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.strSalaryTopDown = "<a onclick='javascript:window.top.Main.Right.location=\"Intro/PlayerCenter.aspx?Type=SEARCH\"' href=\"PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=7\"><img Width=\"64\" Height=\"24\" src=\"Images/qt.gif\" id=\"imgSearchPlayer\" border=\"0\" /></a>";
                    return;

                case 4:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/PlayerCenter.aspx?Type=PLAYER3\"' href='PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=3'>街球队</a></li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append("<li class='qian2'>选拔球员</li>");
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    return;

                case 5:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/PlayerCenter.aspx?Type=PLAYER3\"' href='PlayerCenter.aspx?UserID=" + this.intUserID + "&Type=3'>街球队</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>职业队</li>");
                    this.sbPageIntro.Append(str2);
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    return;

                case 7:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append(str2);
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    return;

                case 8:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append(str2);
                    this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    return;

                case 9:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "MenuCard/PlayerCenter/llzc2.GIF' border='0' height='58' width='237'>");
                    return;
            }
            this.sbPageIntro = new StringBuilder();
            this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
            this.sbPageIntro.Append(str);
            this.sbPageIntro.Append(str2);
            this.sbPageIntro.Append("</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
        }
    }
}


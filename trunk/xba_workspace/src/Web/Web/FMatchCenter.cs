namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class FMatchCenter : Page
    {
        protected ImageButton btGetWealth;
        protected ImageButton btnReg;
        protected ImageButton btnSend;
        protected ImageButton btSaveWealth;
        protected DropDownList ddlCategory;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        private int intLevel;
        private int intPage;
        private int intPayType;
        private int intPerPage;
        private int intRUserID;
        private int intUserID;
        protected Label lblIntro;
        protected Label lblNickName;
        protected RadioButton rbS;
        protected RadioButton rbV;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbWealthFinance = new StringBuilder();
        public StringBuilder sbWealthFinancePage = new StringBuilder();
        private string strClubName;
        public string strEndTime = "";
        public string strErrList;
        public string strErrMsg;
        private string strLogo;
        private string strNickName;
        public string strPlayerWealth;
        private string strType;
        public string strUseWealth;
        protected TextBox tbGetWealth;
        protected TextBox tbIntro;
        protected HtmlTable tblFMatchHistory;
        protected HtmlTable tblFMatchList;
        protected HtmlTable tblFMatchSend;
        protected HtmlTable tblTrainCenter;
        protected HtmlTable tblWealth;
        protected TextBox tbNickName;
        protected TextBox tbSaveWealth;

        private void btnReg_Click(object sender, ImageClickEventArgs e)
        {
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                string strShortName = BTPAccountManager.GetAccountRowByUserID(this.intUserID)["ShortName"].ToString().Trim();
                int num = MatchItem.TrainMatchType(this.intClubID3, 0, 2);
                if (BTPFriMatchManager.GetRegRowByClubID(this.intClubID3, 3) == null)
                {
                    switch (num)
                    {
                        case 1:
                            BTPFriMatchManager.RegTrainCenter(this.intClubID3, this.intLevel, this.strClubName, strShortName, this.strLogo, 1, 50, 3);
                            base.Response.Redirect("Report.aspx?Parameter=4108");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=4100");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=4102");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=4102");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=4107");
                }
            }
        }

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                int num3;
                string strNickName = this.tbNickName.Text.ToString().Trim();
                string strIn = this.tbIntro.Text.ToString().Trim();
                int intCategory = Convert.ToInt16(this.ddlCategory.SelectedValue);
                int intUserID = 0;
                if (this.rbS.Checked)
                {
                    num3 = 3;
                }
                else
                {
                    num3 = 5;
                }
                if (num3 == 3)
                {
                    if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3) < 4)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=495");
                        return;
                    }
                    int clubIDByNickName = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                    if (clubIDByNickName == 0)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=497");
                        return;
                    }
                    if (BTPPlayer3Manager.GetPlayer3CountByCID(clubIDByNickName) < 4)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=496");
                        return;
                    }
                    intUserID = BTPClubManager.GetUserIDByClubID(clubIDByNickName);
                }
                if (num3 == 5)
                {
                    if (intCategory == 2)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=499");
                        return;
                    }
                    if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) < 6)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=495");
                        return;
                    }
                    int intClubID = BTPClubManager.GetClubIDByNickName(strNickName, 5);
                    if (intClubID == 0)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=497");
                        return;
                    }
                    if (BTPPlayer5Manager.GetPlayer5CountByClubID(intClubID) < 6)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=496");
                        return;
                    }
                    intUserID = BTPClubManager.GetUserIDByClubID(intClubID);
                }
                if (intCategory == 2)
                {
                    int clubIDByUIDCategory = BTPClubManager.GetClubIDByUIDCategory(this.intUserID, 3);
                    int intClubIDB = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                    switch (MatchItem.TrainMatchType(clubIDByUIDCategory, intClubIDB, 1))
                    {
                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=4100");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=4101");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=4102");
                            return;

                        case 5:
                            base.Response.Redirect("Report.aspx?Parameter=4103");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=4106");
                            return;
                    }
                }
                strIn = StringItem.GetValidWords(strIn);
                if (!StringItem.IsValidName(strIn, 0, 200))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3ab");
                }
                else
                {
                    switch (BTPFriMatchManager.SetFriMatch(strNickName, this.intUserID, intCategory, num3, strIn, 0, 0, 0, 0))
                    {
                        case 1:
                            DTOnlineManager.SetHasMsgByUserID(intUserID);
                            base.Response.Redirect("Report.aspx?Parameter=40");
                            return;

                        case 2:
                            base.Response.Redirect("Report.aspx?Parameter=48");
                            return;

                        case 3:
                            base.Response.Redirect("Report.aspx?Parameter=491");
                            return;

                        case 4:
                            base.Response.Redirect("Report.aspx?Parameter=492");
                            return;

                        case 0:
                            base.Response.Redirect("Report.aspx?Parameter=493");
                            return;

                        case 6:
                            base.Response.Redirect("Report.aspx?Parameter=498");
                            return;
                    }
                    base.Response.Redirect("Report.aspx?Parameter=47");
                }
            }
        }

        private void FMatchHistory()
        {
            string str2;
            string str5;
            string str7;
            this.sbList = new StringBuilder();
            string strCurrentURL = "FMatchCenter.aspx?Type=TRAINCENTER&";
            this.intPerPage = 10;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetMsgTotal();
            this.GetMsgScript(strCurrentURL);
            SqlDataReader reader = BTPFriMatchManager.GetHistoryFriMatchTableByUserIDNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                goto Label_0805;
            }
            this.sbList.Append("<tr><td height='25' colspan='7' align='center'>没有历史约战！</td></tr>");
            goto Label_0810;
        Label_077D:
            this.sbList.Append("<td>" + str5 + "</td>");
            this.sbList.Append("<td>" + str2 + "</td>");
            this.sbList.Append("<td>" + str7 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
        Label_0805:
            if (reader.Read())
            {
                string str6;
                int num7 = (int) reader["FMatchID"];
                int num = (int) reader["ClubIDA"];
                int num2 = (int) reader["ClubIDB"];
                int num8 = (int) reader["ScoreA"];
                int num9 = (int) reader["ScoreB"];
                byte num3 = (byte) reader["Category"];
                int num10 = (byte) reader["Type"];
                int num11 = (int) reader["WealthA"];
                int num12 = (int) reader["WealthB"];
                int num13 = (int) reader["ClubAPoint"];
                int num14 = (int) reader["ClubBPoint"];
                if (num10 == 3)
                {
                    str6 = "<font color='green'>街球<font>";
                    if (num3 == 2)
                    {
                        str7 = "--";
                    }
                    else
                    {
                        str7 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "SRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "SStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                            "' target='_blank'>统计</a>"
                         });
                    }
                }
                else
                {
                    str6 = "<font color='red'>职业</font>";
                    if (num3 == 2)
                    {
                        str7 = "--";
                    }
                    else
                    {
                        str7 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                            "' target='_blank'>统计</a>"
                         });
                    }
                }
                byte num4 = (byte) reader["Status"];
                if (num4 == 3)
                {
                    str7 = "<font color='red'>比赛取消</font>";
                }
                reader["Intro"].ToString().Trim();
                DateTime datIn = (DateTime) reader["CreateTime"];
                str2 = StringItem.FormatDate(datIn, "MM-dd hh:mm");
                string strNickName = reader["ClubInfoA"].ToString().Trim();
                string str4 = reader["ClubInfoB"].ToString().Trim();
                str5 = reader["ChsCategory"].ToString().Trim();
                string[] strArray = strNickName.Split(new char[] { '|' });
                int intUserID = Convert.ToInt32(strArray[0]);
                strNickName = strArray[1].Trim();
                string[] strArray2 = str4.Split(new char[] { '|' });
                int num6 = Convert.ToInt32(strArray2[0]);
                str4 = strArray2[1].Trim();
                this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("<td height='32' align='center'>" + str6 + "</td>");
                switch (num3)
                {
                    case 3:
                        this.sbList.Append(string.Concat(new object[] { "<td height='32'><div>", MessageItem.GetNickNameInfo(intUserID, strNickName, 1), "</div><div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                        this.sbList.Append(string.Concat(new object[] { "<td>", num8, ":", num9, "</td>" }));
                        this.sbList.Append(string.Concat(new object[] { "<td><div>", MessageItem.GetNickNameInfo(num6, str4, 1), "</div><div><a title='接受方支付游戏币'>", num12, "</a></div></td>" }));
                        goto Label_077D;

                    case 4:
                        if (num13 == 0)
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(intUserID, strNickName, 1), "<div><a title='发起方支付游戏币'>", num11, "[+", num14, "]</a></div></td>" }));
                        }
                        else
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(intUserID, strNickName, 1), "<div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                        }
                        this.sbList.Append(string.Concat(new object[] { "<td>", num8, ":", num9, "</td>" }));
                        if (num14 == 0)
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str4, 1), "<div><a title='发起方支付游戏币'>", num12, "[+", num13, "]</a></div></td>" }));
                        }
                        else
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str4, 1), "<div><a title='发起方支付游戏币'>", num12, "</a></div></td>" }));
                        }
                        goto Label_077D;
                }
                this.sbList.Append("<td>" + MessageItem.GetNickNameInfo(intUserID, strNickName, 1) + "</td>");
                this.sbList.Append(string.Concat(new object[] { "<td>", num8, ":", num9, "</td>" }));
                this.sbList.Append("<td>" + MessageItem.GetNickNameInfo(num6, str4, 1) + "</td>");
                goto Label_077D;
            }
        Label_0810:
            reader.Close();
            this.sbList.Append("<tr><td height='30' align='right' colspan='7'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
        }

        private void FMatchList()
        {
            int num;
            int num2;
            byte num3;
            string str;
            DateTime time;
            string str2;
            byte num4;
            string str3;
            string str4;
            string str5;
            string str6;
            int num5;
            int num6;
            int num7;
            int num8;
            string str7;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            this.sbList = new StringBuilder();
            string strCurrentURL = "FMatchCenter.aspx?Type=TRAINCENTER&";
            this.intPerPage = 10;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetMsgTotal();
            this.GetMsgScript(strCurrentURL);
            DataTable table = BTPFriMatchManager.GetFriMatchRow2ByUserID(this.intUserID);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    num7 = (int) row["FMatchID"];
                    num = (int) row["ClubIDA"];
                    num2 = (int) row["ClubIDB"];
                    num11 = (int) row["WealthA"];
                    num12 = (int) row["WealthB"];
                    num13 = (int) row["ClubAPoint"];
                    num14 = (int) row["ClubBPoint"];
                    num3 = (byte) row["Category"];
                    num8 = (byte) row["Type"];
                    if (num8 == 3)
                    {
                        str7 = "<font color='green'>街球<font>";
                    }
                    else
                    {
                        str7 = "<font color='red'>职业</font>";
                    }
                    num4 = (byte) row["Status"];
                    str = row["Intro"].ToString().Trim();
                    time = (DateTime) row["CreateTime"];
                    str3 = StringItem.FormatDate(time, "hh:mm:ss");
                    str4 = row["ClubInfoA"].ToString().Trim();
                    str5 = row["ClubInfoB"].ToString().Trim();
                    str6 = row["ChsCategory"].ToString().Trim();
                    string[] strArray = str4.Split(new char[] { '|' });
                    num5 = Convert.ToInt32(strArray[0]);
                    str4 = strArray[1].Trim();
                    string[] strArray2 = str5.Split(new char[] { '|' });
                    num6 = Convert.ToInt32(strArray2[0]);
                    str5 = strArray2[1].Trim();
                    num9 = (int) row["ScoreA"];
                    num10 = (int) row["ScoreB"];
                    if (num8 == 3)
                    {
                        if ((num4 == 1) && (num == this.intClubID3))
                        {
                            str2 = "等待 | <a href='DelFriMatch.aspx?FMatchID=" + num7 + "&Type=3'>取消</a>";
                            str3 = "<a  style='cursor:hand;' title='发出邀请时间'>" + str3 + "</a>";
                        }
                        else if ((num4 == 1) && (num2 == this.intClubID3))
                        {
                            str2 = string.Concat(new object[] { "<a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=2&Type=3&UserIDA=", num5, "'>接受</a> | <a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=3&Type=3&UserIDA=", num5, "'>拒绝</a>" });
                            str3 = "<a  style='cursor:hand;' title='发出邀请时间'>" + str3 + "</a>";
                        }
                        else if (num4 == 2)
                        {
                            str2 = "<span style='cursor:pointer;' >比赛中</span>";
                            str3 = "<a  style='cursor:hand;' title='比赛开始时间'>" + str3 + "</a>";
                        }
                        else if ((num4 == 3) && (num == this.intClubID3))
                        {
                            str2 = "被拒绝";
                            str3 = "--";
                        }
                        else if ((num4 == 3) && (num2 == this.intClubID3))
                        {
                            str2 = "已拒绝请求";
                            str3 = "--";
                        }
                        else
                        {
                            str2 = string.Concat(new object[] { 
                                "<a href='", DBLogin.GetMatchURL(), "SRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "SStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                                "' target='_blank'>统计</a>"
                             });
                        }
                    }
                    else if ((num4 == 1) && (num == this.intClubID5))
                    {
                        str2 = "等待 | <a href='DelFriMatch.aspx?FMatchID=" + num7 + "&Type=5'>取消</a>";
                        str3 = "<a  style='cursor:pointer;' title='发出邀请时间'>" + str3 + "</a>";
                    }
                    else if ((num4 == 1) && (num2 == this.intClubID5))
                    {
                        str2 = string.Concat(new object[] { "<a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=2&Type=5'>接受</a> | <a href='ChangeFriMatch.aspx?FMatchID=", num7, "&&Status=3&Type=5'>拒绝</a>" });
                        str3 = "<a  style='cursor:pointer;' title='发出邀请时间'>" + str3 + "</a>";
                    }
                    else if (num4 == 2)
                    {
                        str2 = "<span style='cursor:pointer;' >比赛中</span>";
                        str3 = StringItem.FormatDate(DateTime.Now.AddMinutes(5.0), "hh:mm:ss");
                    }
                    else if ((num4 == 3) && (num == this.intClubID5))
                    {
                        str2 = "被拒绝";
                        str3 = "--";
                    }
                    else if ((num4 == 3) && (num2 == this.intClubID5))
                    {
                        str2 = "已拒绝请求";
                        str3 = "--";
                    }
                    else
                    {
                        str3 = "已结束";
                        str2 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                            "' target='_blank'>统计</a>"
                         });
                    }
                    TimeSpan span = (TimeSpan) (time.AddMinutes(5.0) - DateTime.Now);
                    if (span.TotalSeconds > 0.0)
                    {
                        this.sbList.Append(string.Concat(new object[] { "<script type=\"text/javascript\" language=\"javascript\">var Counter", num7, " = new clsCounter(\"Counter", num7, "\", \"已结束\");\n" }));
                        string str9 = "<a href=\"FMatchCenter.aspx?Type=TRAINCENTER&Page=1\">刷新列表</a>";
                        this.sbList.Append(string.Concat(new object[] { "Counter", num7, ".init('FTime", num7, "','Opa", num7, "','", str9, "',", span.TotalSeconds, ",null);\n</script>" }));
                    }
                    else
                    {
                        str3 = "00:00:00";
                        str2 = "<span style='cursor:pointer;' title='比赛延时请稍候。'>比赛延时</span>";
                    }
                    this.sbList.Append("<tr class='BarContent' style=\"background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
                    this.sbList.Append("<td align='center' height='32'>" + str7 + "</td>");
                    switch (num3)
                    {
                        case 3:
                            this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                            if ((num9 == 0) && (num10 == 0))
                            {
                                this.sbList.Append("<td>--:--</td>");
                            }
                            else
                            {
                                this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                            }
                            this.sbList.Append(string.Concat(new object[] { "<td>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='接受方支付游戏币'>", num12, "</a></div></td>" }));
                            break;

                        case 4:
                            if (num13 > 0)
                            {
                                this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                                if ((num9 == 0) && (num10 == 0))
                                {
                                    this.sbList.Append("<td>--:--</td>");
                                }
                                else
                                {
                                    this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                                }
                                this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='发起方支付游戏币'>", num12, "[+", num13, "]</a></div></td>" }));
                            }
                            else
                            {
                                this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "[+", num14, "]</a></div></td>" }));
                                if ((num9 == 0) && (num10 == 0))
                                {
                                    this.sbList.Append("<td>--:--</td>");
                                }
                                else
                                {
                                    this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                                }
                                this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='发起方支付游戏币'>", num12, "</a></div></td>" }));
                            }
                            break;

                        default:
                            this.sbList.Append("<td height='32' align='right' style='padding-right:4px;'>" + MessageItem.GetNickNameInfoTitle(num5, str4, 1, str4 + "说：" + str) + "</td>");
                            if ((num9 == 0) && (num10 == 0))
                            {
                                this.sbList.Append("<td>-- : --</td>");
                            }
                            else
                            {
                                this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                            }
                            this.sbList.Append("<td align='left' style='padding-left:3px'>" + MessageItem.GetNickNameInfo(num6, str5, 1) + "</td>");
                            break;
                    }
                    this.sbList.Append("<td>" + str6 + "</td>");
                    this.sbList.Append(string.Concat(new object[] { "<td id=\"FTime", num7, "\">", str3, "</td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td align='left' style='padding-left:3px' id=\"Opa", num7, "\">", str2, "</td>" }));
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
            }
            int count = 0;
            if (table != null)
            {
                count = table.Rows.Count;
            }
            this.intPerPage = 9 - count;
            SqlDataReader reader = BTPFriMatchManager.GetFriMatchTableByUserIDNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows || (count >= 1))
            {
                goto Label_1796;
            }
            this.sbList.Append("<tr><td height='25' colspan='7' align='center'>没有约战！</td></tr>");
            goto Label_17A2;
        Label_132D:
            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td align='center' height='32'>" + str7 + "</td>");
            switch (num3)
            {
                case 3:
                    this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='接受方支付游戏币'>", num12, "</a></div></td>" }));
                    break;

                case 4:
                    if (num13 > 0)
                    {
                        this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "</a></div></td>" }));
                        if ((num9 == 0) && (num10 == 0))
                        {
                            this.sbList.Append("<td>--:--</td>");
                        }
                        else
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                        }
                        this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='发起方支付游戏币'>", num12, "[+", num13, "]</a></div></td>" }));
                    }
                    else
                    {
                        this.sbList.Append(string.Concat(new object[] { "<td height='32' align='right' style='padding-right:4px;'>", MessageItem.GetNickNameInfo(num5, str4, 1), "<div><a title='发起方支付游戏币'>", num11, "[+", num14, "]</a></div></td>" }));
                        if ((num9 == 0) && (num10 == 0))
                        {
                            this.sbList.Append("<td>--:--</td>");
                        }
                        else
                        {
                            this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                        }
                        this.sbList.Append(string.Concat(new object[] { "<td height='32'>", MessageItem.GetNickNameInfo(num6, str5, 1), "<div><a title='发起方支付游戏币'>", num12, "</a></div></td>" }));
                    }
                    break;

                default:
                    this.sbList.Append("<td height='32' align='right' style='padding-right:4px;'>" + MessageItem.GetNickNameInfoTitle(num5, str4, 1, str4 + "说：" + str) + "</td>");
                    if ((num9 == 0) && (num10 == 0))
                    {
                        this.sbList.Append("<td>-- : --</td>");
                    }
                    else
                    {
                        this.sbList.Append(string.Concat(new object[] { "<td>", num9, ":", num10, "</td>" }));
                    }
                    this.sbList.Append("<td align='left' style='padding-left:3px'>" + MessageItem.GetNickNameInfo(num6, str5, 1) + "</td>");
                    break;
            }
            this.sbList.Append("<td>" + str6 + "</td>");
            this.sbList.Append("<td>" + str3 + "</td>");
            this.sbList.Append("<td align='left' style='padding-left:3px'>" + str2 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
        Label_1796:
            if (reader.Read())
            {
                num7 = (int) reader["FMatchID"];
                num = (int) reader["ClubIDA"];
                num2 = (int) reader["ClubIDB"];
                num11 = (int) reader["WealthA"];
                num12 = (int) reader["WealthB"];
                num13 = (int) reader["ClubAPoint"];
                num14 = (int) reader["ClubBPoint"];
                num3 = (byte) reader["Category"];
                num8 = (byte) reader["Type"];
                if (num8 == 3)
                {
                    str7 = "<font color='green'>街球<font>";
                }
                else
                {
                    str7 = "<font color='red'>职业</font>";
                }
                num4 = (byte) reader["Status"];
                str = reader["Intro"].ToString().Trim();
                time = (DateTime) reader["CreateTime"];
                str3 = StringItem.FormatDate(time, "hh:mm:ss");
                str4 = reader["ClubInfoA"].ToString().Trim();
                str5 = reader["ClubInfoB"].ToString().Trim();
                str6 = reader["ChsCategory"].ToString().Trim();
                string[] strArray3 = str4.Split(new char[] { '|' });
                strArray3[0].Trim();
                num5 = Convert.ToInt32(strArray3[0]);
                str4 = strArray3[1].Trim();
                string[] strArray4 = str5.Split(new char[] { '|' });
                num6 = Convert.ToInt32(strArray4[0]);
                str5 = strArray4[1].Trim();
                num9 = (int) reader["ScoreA"];
                num10 = (int) reader["ScoreB"];
                if (num8 == 3)
                {
                    if ((num4 == 1) && (num == this.intClubID3))
                    {
                        str2 = "等待 | <a href='DelFriMatch.aspx?FMatchID=" + num7 + "&Type=3'>取消</a>";
                        str3 = "<a  style='cursor:hand;' title='发出邀请时间'>" + str3 + "</a>";
                    }
                    else if ((num4 == 1) && (num2 == this.intClubID3))
                    {
                        str2 = string.Concat(new object[] { "<a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=2&Type=3&UserIDA=", num5, "'>接受</a> | <a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=3&Type=3&UserIDA=", num5, "'>拒绝</a>" });
                        str3 = "<a  style='cursor:hand;' title='发出邀请时间'>" + str3 + "</a>";
                    }
                    else if (num4 == 2)
                    {
                        str2 = "<span style='cursor:pointer;' >比赛中</span>";
                        str3 = StringItem.FormatDate(DateTime.Now.AddMinutes(5.0), "hh:mm:ss");
                        str3 = "<a  style='cursor:hand;' title='比赛开始时间'>" + str3 + "</a>";
                    }
                    else if ((num4 == 3) && (num == this.intClubID3))
                    {
                        str2 = "被拒绝 ";
                        str3 = "--";
                    }
                    else if ((num4 == 3) && (num2 == this.intClubID3))
                    {
                        str2 = "已拒绝请求";
                        str3 = "--";
                    }
                    else
                    {
                        switch (num3)
                        {
                            case 2:
                            case 5:
                                str2 = "-- --";
                                goto Label_132D;
                        }
                        str2 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "SRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "SStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                            "' target='_blank'>统计</a>"
                         });
                    }
                }
                else if ((num4 == 1) && (num == this.intClubID5))
                {
                    str2 = "等待 | <a href='DelFriMatch.aspx?FMatchID=" + num7 + "&Type=5'>取消</a>";
                    str3 = "<a  style='cursor:pointer;' title='发出邀请时间'>" + str3 + "</a>";
                }
                else if ((num4 == 1) && (num2 == this.intClubID5))
                {
                    str2 = string.Concat(new object[] { "<a href='ChangeFriMatch.aspx?FMatchID=", num7, "&Status=2&Type=5'>接受</a> | <a href='ChangeFriMatch.aspx?FMatchID=", num7, "&&Status=3&Type=5'>拒绝</a>" });
                    str3 = "<a  style='cursor:pointer;' title='发出邀请时间'>" + str3 + "</a>";
                }
                else if (num4 == 2)
                {
                    str2 = "<span style='cursor:pointer;'>比赛中</span>";
                    str3 = StringItem.FormatDate(DateTime.Now.AddMinutes(5.0), "hh:mm:ss");
                    str3 = "<a style='cursor:pointer;' title='比赛开始时间'>" + str3 + "</a>";
                }
                else if ((num4 == 3) && (num == this.intClubID5))
                {
                    str2 = "被拒绝";
                    str3 = "--";
                }
                else if ((num4 == 3) && (num2 == this.intClubID5))
                {
                    str2 = "已拒绝请求";
                    str3 = "--";
                }
                else
                {
                    switch (num3)
                    {
                        case 10:
                        case 11:
                            str2 = "-- --";
                            goto Label_132D;
                    }
                    str2 = string.Concat(new object[] { 
                        "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, "' target='_blank'>战报</a> | <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", num7, "&A=", num, "&B=", num2, 
                        "' target='_blank'>统计</a>"
                     });
                }
                goto Label_132D;
            }
        Label_17A2:
            reader.Close();
            this.sbList.Append("<tr><td height='30' align='right' colspan='7'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
        }

        private void FMatchListNew()
        {
            Cuter cuter = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID]));
            if (cuter.GetIndex("0") == 1)
            {
                cuter.SetCuter(1, "1");
                string strAdvanceOp = cuter.GetCuter();
                BTPAccountManager.UpdateAdvanceOp(this.intUserID, strAdvanceOp);
                this.Session["Advance" + this.intUserID] = strAdvanceOp;
                BTPAccountManager.AddMoneyWithFinance(0x4e20, this.intUserID, 3, "完成高级新手任务二奖励球队资金。");
                BTPPlayer3Manager.UpdatePlayerByUserID(this.intUserID);
            }
            string strCurrentURL = "FMatchCenter.aspx?Type=TRAINCENTER&";
            this.GetMsgScript(strCurrentURL);
            this.intPerPage = 10;
            this.intPage = 1;
            string str3 = "00:00:05";
            string str4 = "";
            TimeSpan span = (TimeSpan) (DateTime.Now.AddSeconds(5.0) - DateTime.Now);
            if (span.TotalSeconds > 0.0)
            {
                this.sbList.Append("<script type=\"text/javascript\" language=\"javascript\">var CounterAdv = new clsCounter(\"CounterAdv\", \"已结束\");\n");
                string str5 = "<a href=\"Main_I.aspx\" target=\"Main\">任务完成</a>";
                this.sbList.Append(string.Concat(new object[] { "CounterAdv.init('FTimeAdv','OpaAdv','", str5, "',", span.TotalSeconds, ",null,'ScoreAd','45:44');\n</script>" }));
            }
            else
            {
                str3 = "00:00:00";
                str4 = "<span style='cursor:pointer;' title='比赛延时请稍候。'>比赛延时</span>";
            }
            this.sbList.Append("<tr class='BarContent' style=\"background-color:#FBE2D4\" onmouseover=\"this.style.backgroundColor=''\" onmouseout=\"this.style.backgroundColor='#FBE2D4'\">");
            this.sbList.Append("<td align='center' height='23'><font color='green'>街球<font></td>");
            this.sbList.Append("<td align='left'>" + MessageItem.GetNickNameInfo(this.intUserID, this.strClubName, 1) + "</td>");
            this.sbList.Append("<td align='center'><span id=\"ScoreAd\">-- : --</span></td>");
            this.sbList.Append("<td align='left'><a href=\"RookieShowClub.aspx\" target=\"Right\">XBA魔鬼队</a></td>");
            this.sbList.Append("<td align='center'>训练赛</td>");
            this.sbList.Append("<td align='center' id=\"FTimeAdv\">" + str3 + "</td>");
            this.sbList.Append("<td align='left' style='padding-left:3px' id=\"OpaAdv\">" + str4 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
            this.sbList.Append("<tr><td height='30' align='right' colspan='7'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
        }

        private void FMatchSend()
        {
            this.intRUserID = (int) SessionItem.GetRequest("UserID", 0);
            this.sbList = new StringBuilder();
            this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                this.rbV.Visible = false;
            }
            if (!base.IsPostBack)
            {
                DataView view = new DataView(DDLItem.GetFriMatchItem(1, this.intCategory));
                this.ddlCategory.DataSource = view;
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Category";
                this.ddlCategory.DataBind();
                if (this.intRUserID == 0)
                {
                    this.tbNickName.Text = "";
                }
                else
                {
                    string str = BTPAccountManager.GetNickNameByUserID(this.intRUserID).Trim();
                    this.tbNickName.Text = str;
                }
            }
        }

        private void GetMsgScript(string strCurrentURL)
        {
            this.sbScript.Append("<script language=\"javascript\">");
            this.sbScript.Append("function JumpPage()");
            this.sbScript.Append("{");
            this.sbScript.Append("var strPage=document.all.Page.value;");
            this.sbScript.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.sbScript.Append("}");
            this.sbScript.Append("</script>");
        }

        private int GetMsgTotal()
        {
            if (this.strType != "FMATCHLIST")
            {
                if (this.strType == "FMATCHHISTORY")
                {
                    return BTPFriMatchManager.GetHistoryFriMatchCountByUserIDNew(this.intUserID);
                }
                if (this.strType == "WEALTHMANAGE")
                {
                    return BTPWealthFinanceManager.GameServiceTotal(this.intUserID);
                }
            }
            return BTPFriMatchManager.GetFriMatchCountByUserIDNew(this.intUserID);
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string str5;
            string[] strArray;
            int msgTotal = this.GetMsgTotal();
            int num2 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            str2 = "<span style='margin-right:40px;color:red'> 购买“双倍训练卡”可获得双倍训练点，<a href=\"ManagerTool.aspx?Type=STORE&amp;Page=1\">点此购买</a></span>";
            if (this.intPage == 1)
            {
                str2 = str2 + "上一页";
            }
            else
            {
                str5 = str2;
                strArray = new string[6];
                strArray[0] = str5;
                strArray[1] = "<a href='";
                strArray[2] = strCurrentURL;
                strArray[3] = "Page=";
                int num4 = this.intPage - 1;
                strArray[4] = num4.ToString();
                strArray[5] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str5 = str3;
                strArray = new string[] { str5, "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
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
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        private void InitializeComponent()
        {
            switch (this.strType)
            {
                case "FMATCHLIST":
                    this.sbPageIntro.Append("<a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTERREG\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_04.GIF' border='0' height='24' width='76'></a>");
                    this.sbPageIntro.Append("<img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_02.GIF' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<a href='FriMatchMessage.aspx?Type=ONLINE&Page=1'><img align='absmiddle' onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_06.GIF' border='0' height='24' width='75'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' href=\"FMatchCenter.aspx?Type=TRAINCENTER&Page=1\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<img  align=\"absmiddle\" src=\"images/SendMatch.gif\" onclick=\"javascript:window.location='FriMatchMessage.aspx?Type=FMATCHSEND';javascript:window.top.Main.Right.location='Intro/OnlineList.aspx?Type=FMATCHSEND';\" alt=\"发起约战\" width=\"64\" height=\"24\" style=\"margin-left:60px;CURSOR:pointer\">");
                    this.FMatchList();
                    break;

                case "FMATCHHISTORY":
                    this.tblFMatchHistory.Visible = true;
                    this.sbPageIntro.Append("<a onclick='javascript:window.top.Main.Right.location=\"Intro/MatchMy.aspx?Type=FMATCHLIST\"' href='FMatchCenter.aspx?Type=FMATCHLIST&Page=1'><img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_C_01.GIF' border='0' height='24' width='76'></a>");
                    this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "MenuCard/FMatchCenter/FMatchCenter_03.GIF' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' href=\"FMatchCenter.aspx?Type=TRAINCENTER&Page=1\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<img style=\"margin:30px\" align=\"absmiddle\" src=\"images/SendMatch.gif\" onclick=\"javascript:window.location='FriMatchMessage.aspx?Type=FMATCHSEND';javascript:window.top.Main.Right.location='Intro/OnlineList.aspx?Type=FMATCHSEND';\" alt=\"发起约战\" width=\"64\" height=\"24\" style=\"margin-left:65px;CURSOR:pointer\">");
                    this.FMatchHistory();
                    break;

                default:
                    SessionItem.GetRequest("CC", 1).ToString().Trim();
                    this.tblFMatchList.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG'>街球训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=TRAINCENTER\"' href='FriMatchMessage.aspx?Type=TRAINCENTERREG5'>职业训练</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>我的约战</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/OnlineList.aspx?Type=ONLINELIST\"' href='FriMatchMessage.aspx?Type=ONLINE&Page=1'>在线经理</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' href=\"FMatchCenter.aspx?Type=TRAINCENTER&Page=1\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Refresh.GIF' border='0' height='24' width='19'></a>");
                    this.sbPageIntro.Append("<img  align=\"absmiddle\" src=\"images/SendMatch.gif\" onclick=\"javascript:window.location='FriMatchMessage.aspx?Type=FMATCHSEND';javascript:window.top.Main.Right.location='Intro/OnlineList.aspx?Type=FMATCHSEND';\" alt=\"发起约战\" width=\"64\" height=\"24\" style=\"margin-left:100px;CURSOR:pointer\">");
                    this.sbList = new StringBuilder();
                    this.FMatchList();
                    break;
            }
            this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
            this.btnReg.Click += new ImageClickEventHandler(this.btnReg_Click);
            base.Load += new EventHandler(this.Page_Load);
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
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intPayType = (int) onlineRowByUserID["PayType"];
                this.strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
                this.intLevel = (int) onlineRowByUserID["Levels"];
                this.strLogo = onlineRowByUserID["ClubLogo"].ToString().Trim();
                this.tblFMatchList.Visible = false;
                this.tblFMatchSend.Visible = false;
                this.tblFMatchHistory.Visible = false;
                this.tblTrainCenter.Visible = false;
                this.tblWealth.Visible = false;
                this.btnReg.ImageUrl = SessionItem.GetImageURL() + "button_29.GIF";
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        public void SendTrain()
        {
            if ((this.intCategory == 0) || (this.intCategory == 3))
            {
                base.Response.Redirect("Report.aspx?Parameter=4112");
            }
            else
            {
                int request = (int) SessionItem.GetRequest("ClubID", 0);
                int intClubID = this.intClubID3;
                string strNickName = BTPAccountManager.GetAccountRowByClubID3(request)["NickName"].ToString().Trim();
                string strIntro = "";
                if (request == intClubID)
                {
                    base.Response.Redirect("Report.aspx?Parameter=4104");
                }
                else if (BTPPlayer3Manager.GetPlayer3CountByCID(intClubID) < 4)
                {
                    base.Response.Redirect("Report.aspx?Parameter=495");
                }
                else if (BTPClubManager.GetClubIDByNickName(strNickName, 3) == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=497");
                }
                else if (BTPPlayer3Manager.GetPlayer3CountByCID(request) < 4)
                {
                    base.Response.Redirect("Report.aspx?Parameter=496");
                }
                else
                {
                    int num6 = MatchItem.TrainMatchType(intClubID, request, 1);
                    switch (num6)
                    {
                        case 3:
                        case 4:
                            BTPFriMatchManager.DeleteFriMatchRowByCIDB(request);
                            break;
                    }
                    if (num6 == 1)
                    {
                        switch (BTPFriMatchManager.SetTrainMatch(strNickName, this.intUserID, 2, 3, strIntro))
                        {
                            case 1:
                                base.Response.Redirect("Report.aspx?Parameter=40");
                                return;

                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=48");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=491");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=492");
                                return;

                            case 0:
                                base.Response.Redirect("Report.aspx?Parameter=493");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=498");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=47");
                    }
                    else
                    {
                        switch (num6)
                        {
                            case 2:
                                base.Response.Redirect("Report.aspx?Parameter=4100");
                                return;

                            case 3:
                                base.Response.Redirect("Report.aspx?Parameter=4101");
                                return;

                            case 4:
                                base.Response.Redirect("Report.aspx?Parameter=4102");
                                return;

                            case 5:
                                base.Response.Redirect("Report.aspx?Parameter=4103");
                                return;

                            case 6:
                                base.Response.Redirect("Report.aspx?Parameter=4106");
                                return;
                        }
                        base.Response.Redirect("Report.aspx?Parameter=3");
                    }
                }
            }
        }

        private void TrainCenterRegList()
        {
            DataTable table;
            this.sbList = new StringBuilder();
            if (this.strType == "SIFTCENTER")
            {
                if (this.intPayType != 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=4105");
                    return;
                }
                table = BTPFriMatchManager.GetTrainCenterTable(this.intUserID, 2, this.intPayType);
            }
            else
            {
                table = BTPFriMatchManager.GetTrainCenterTable(this.intUserID, 1, this.intPayType);
            }
            if (table == null)
            {
                this.sbList.Append("<tr><td height='25' colspan='5' align='center'>暂时没有等待中的球队！</td></tr>");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["ClubID"];
                    int num2 = (int) row["Levels"];
                    string str = row["ClubName"].ToString().Trim();
                    string str2 = "<a href='FMatchCenter.aspx?Type=SENDTRAIN&ClubID=" + num + "'>训练约战</a>";
                    int num1 = (int) row["RegID"];
                    string str3 = row["ShortName"].ToString().Trim();
                    row["ClubLogo"].ToString().Trim();
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append(string.Concat(new object[] { "<td align='center' height='50'><img src='", SessionItem.GetImageURL(), "Club/Logo/", num2, "/", this.strLogo, ".gif'></td>" }));
                    this.sbList.Append(string.Concat(new object[] { "<td><a href='ShowClub.aspx?ClubID=", num, "&Type=3' title='", str3, str, "' target='Right'>" }));
                    this.sbList.Append(StringItem.GetShortString(str3 + str, 10, ".") + "</a></td>");
                    this.sbList.Append("<td>" + num2 + "</td>");
                    this.sbList.Append("<td>" + str2 + "</td>");
                    this.sbList.Append("<td></td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>");
                }
            }
        }
    }
}


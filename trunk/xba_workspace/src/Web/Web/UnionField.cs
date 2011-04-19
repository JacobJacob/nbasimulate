namespace Web
{
    using AjaxPro;
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using System.Xml;
    using Web.DBData;
    using Web.Helper;

    public class UnionField : Page
    {
        protected ImageButton btnUnionFieldReg;
        private int intClubID5;
        private int intPage = 1;
        private int intPerPage = 10;
        private int intReputationNow = 0;
        private int intUnionIDH;
        private int intUnionIDMy;
        private int intUserID;
        protected HtmlInputHidden RunStatus;
        public StringBuilder sbList = new StringBuilder("");
        public StringBuilder sbPageIntro = new StringBuilder("");
        public StringBuilder sbScript = new StringBuilder("");
        public StringBuilder sbTop = new StringBuilder("");
        public string strFind = "";
        private string strNickName;
        public string strOnLoad = "";
        public string strReputationMax = "";
        private string strType;
        public string strUnionName = "";
        protected HtmlTable tblFiledUnion;
        protected HtmlTable tblMatchRun;
        protected HtmlTable tblMatchUnion;
        protected HtmlTable tblUnionFiled;
        protected TextBox tbReputation;
        protected HtmlTableCell tdReg;

        private void btnUnionFieldReg_Click(object sender, ImageClickEventArgs e)
        {
            string str = this.tbReputation.Text.Trim();
            if (this.intClubID5 < 1)
            {
                this.tbReputation.Style.Add("color", "#999999");
                this.tbReputation.Attributes["onfocus"] = "SetText(this)";
                this.tbReputation.Text = "您没有职业队";
            }
            else if (StringItem.IsNumber(str) && (str.Length < 7))
            {
                int num = Convert.ToInt32(str);
                if ((num > 0) && (num <= this.intReputationNow))
                {
                    base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=FIELDREG&UID=", this.intUnionIDH, "&RT=", num }));
                }
                else
                {
                    this.tbReputation.Style.Add("color", "#999999");
                    this.tbReputation.Attributes["onfocus"] = "SetText(this)";
                    this.tbReputation.Text = "请重新输入";
                }
            }
            else
            {
                this.tbReputation.Style.Add("color", "#999999");
                this.tbReputation.Attributes["onfocus"] = "SetText(this)";
                this.tbReputation.Text = "请重新输入";
            }
        }

        private void FieldList()
        {
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(this.intUnionIDH);
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            DataRow row3 = BTPUnionManager.GetUnionRowByID(this.intUnionIDMy);
            string str = unionRowByID["Name"].ToString().Trim();
            string str2 = unionRowByID["ShortName"].ToString().Trim();
            int num = (int) unionRowByID["Reputation"];
            int num2 = (int) accountRowByUserID["UnionReputation"];
            this.tbReputation.Style.Add("color", "#999999");
            this.tbReputation.Attributes["onfocus"] = "SetText(this)";
            this.tbReputation.Text = "输入抵押威望";
            num /= 10;
            if (num < 1)
            {
                num = 1;
            }
            this.intReputationNow = BTPUnionFieldManager.GetTopReputation(this.intUserID, this.intUnionIDH);
            this.strReputationMax = "最大可用威望：" + this.intReputationNow;
            if (this.intUnionIDH == this.intUnionIDMy)
            {
                this.tdReg.InnerHtml = "";
                this.strReputationMax = "最大可用威望：" + num2;
                this.strUnionName = string.Concat(new object[] { "当前联盟：<a href='Union.aspx?Type=MYUNION&Kind=UNIONINFO&UnionID=", this.intUnionIDH, "&Page=1'>", str, "[", str2, "]</a>" });
            }
            else
            {
                this.strUnionName = string.Concat(new object[] { "当前联盟：<a href='Union.aspx?Type=UNION&Kind=UNIONINTRO&UnionID=", this.intUnionIDH, "&Page=1'>", str, "[", str2, "]</a>" });
            }
            if (row3 != null)
            {
                DateTime time = (DateTime) row3["CreateTime"];
                int num3 = (int) row3["Reputation"];
                if (time >= DateTime.Now.AddDays(-15.0))
                {
                    this.tdReg.InnerHtml = "您的联盟正在保护期不能挑战其它联盟，保护期将持续到" + StringItem.FormatDate(time.AddDays(15.0), "yy-MM-dd hh:mm:ss");
                    this.strReputationMax = "";
                }
                if (num3 < 1)
                {
                    this.tdReg.InnerHtml = "您的联盟威望小于1即将被解散不能发起挑战";
                    this.strReputationMax = "";
                }
            }
            else
            {
                this.tdReg.InnerHtml = "";
                this.strReputationMax = "您未有加入联盟不能参加盟战";
            }
            this.sbPageIntro.Append("<span style='margin-left:20px'>" + this.strReputationMax + "</span>");
            int request = (int) SessionItem.GetRequest("Category", 0);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            if (request < 1)
            {
                request = 1;
            }
            this.strFind = string.Concat(new object[] { "<a href=\"UnionField.aspx?Type=FIELDLIST&UID=", this.intUnionIDH, "&Category=3&Page=", this.intPage, "\">搜索未应战的挑战</a>" });
            this.sbList.Append("<tr>");
            this.sbList.Append("\t<td align=\"right\" bgColor=\"#fcc6a4\">挑战球队</td>");
            this.sbList.Append(string.Concat(new object[] { "\t<td align=\"center\" bgColor=\"#fcc6a4\"><a href=\"UnionField.aspx?Type=FIELDLIST&UID=", this.intUnionIDH, "&Category=2&Page=", this.intPage, "\">威望抵押</a></td>" }));
            this.sbList.Append("\t<td align=\"left\" bgColor=\"#fcc6a4\">应战球队</td>");
            this.sbList.Append(string.Concat(new object[] { "\t<td align=\"center\" bgColor=\"#fcc6a4\"><a href=\"UnionField.aspx?Type=FIELDLIST&UID=", this.intUnionIDH, "&Category=1&Page=", this.intPage, "\">挑战时间</a></td>" }));
            this.sbList.Append("\t<td align=\"center\" bgColor=\"#fcc6a4\">操作</td>");
            this.sbList.Append("\t<td align=\"center\" bgColor=\"#fcc6a4\"></td>");
            this.sbList.Append("</tr>");
            SqlDataReader reader = BTPUnionFieldManager.GetUnionFieldTable(this.intUnionIDH, request, this.intPage, this.intPerPage, false);
            if (reader.HasRows)
            {
                string str9;
                while (reader.Read())
                {
                    string strIn = reader["ClubNameA"].ToString().Trim();
                    string str4 = reader["UnionNameA"].ToString().Trim();
                    int num5 = (int) reader["FieldID"];
                    int num6 = (int) reader["UnionIDH"];
                    int num7 = (int) reader["UserIDA"];
                    int num1 = (int) reader["UnionIDA"];
                    int num8 = (byte) reader["Status"];
                    int num9 = (int) reader["Reputation"];
                    int intFMatchID = (int) reader["FMatchID"];
                    string str5 = "-- --";
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    DateTime time3 = (DateTime) reader["MatchTime"];
                    string strNickName = "-- --";
                    string str7 = "";
                    string str8 = "<span style='width:75px;text-align:center'>正式比赛时间" + StringItem.FormatDate(time3.AddSeconds(298.0), "MM-dd hh:mm") + "</span>";
                    switch (num8)
                    {
                        case 0:
                            if (num6 == this.intUnionIDMy)
                            {
                                str7 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?UID=", num7, "&Type=FIELDDEF&CID=", num5, "'>立刻应战</a>" });
                            }
                            else
                            {
                                str7 = "尚未应战";
                            }
                            str8 = "尚未应战";
                            break;

                        case 1:
                        {
                            int intUserID = (int) reader["UserIDH"];
                            strNickName = reader["ClubNameH"].ToString().Trim();
                            strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 0x10);
                            str7 = "已经应战";
                            break;
                        }
                        case 2:
                        {
                            int num12 = (int) reader["UserIDH"];
                            strNickName = reader["ClubNameH"].ToString().Trim();
                            DataRow friMatchRowByFID = BTPFriMatchManager.GetFriMatchRowByFID(intFMatchID);
                            int num13 = (int) friMatchRowByFID["ScoreA"];
                            int num14 = (int) friMatchRowByFID["ScoreB"];
                            int num15 = (int) friMatchRowByFID["ClubIDA"];
                            int num16 = (int) friMatchRowByFID["ClubIDB"];
                            if ((num13 > 0) || (num14 > 0))
                            {
                                str5 = string.Concat(new object[] { 
                                    "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", intFMatchID, "&A=", num15, "&B=", num16, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>  <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", intFMatchID, "&A=", num15, 
                                    "&B=", num16, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                                 });
                            }
                            strNickName = AccountItem.GetNickNameInfo(num12, strNickName, "Right", 0x10);
                            str7 = "<font color=green>防守成功</font>";
                            break;
                        }
                        default:
                            if (num8 > 2)
                            {
                                str7 = "<font color=red>防守失败</font>";
                                strNickName = reader["ClubNameH"].ToString().Trim();
                                DataRow row5 = BTPFriMatchManager.GetFriMatchRowByFID(intFMatchID);
                                if (row5 != null)
                                {
                                    int num17 = (int) row5["ScoreA"];
                                    int num18 = (int) row5["ScoreB"];
                                    int num19 = (int) row5["ClubIDA"];
                                    int num20 = (int) row5["ClubIDB"];
                                    if ((num17 > 0) || (num18 > 0))
                                    {
                                        str5 = string.Concat(new object[] { 
                                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", intFMatchID, "&A=", num19, "&B=", num20, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>  <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", intFMatchID, "&A=", num19, 
                                            "&B=", num20, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                                         });
                                    }
                                    if (num8 != 2)
                                    {
                                        if (num17 > num18)
                                        {
                                            str7 = "<font color=green>防守成功</font>";
                                        }
                                        else
                                        {
                                            str7 = "<font color=red>防守失败</font>";
                                        }
                                    }
                                }
                                if (strNickName == "")
                                {
                                    strNickName = "－－－－";
                                }
                                else
                                {
                                    int num21 = (int) reader["UserIDH"];
                                    strNickName = AccountItem.GetNickNameInfo(num21, strNickName, "Right", 0x10);
                                }
                            }
                            break;
                    }
                    this.sbList.Append("<tr>");
                    this.sbList.Append(string.Concat(new object[] { "<td align=\"right\" ><a href=\"ShowClub.aspx?Type=5&UserID=", num7, "\" title=\"来自：", str4, "\" target=\"Right\">", StringItem.GetShortString(strIn, 0x10, "."), "</a></td>" }));
                    this.sbList.Append("<td align=\"center\" >" + num9 + "</td>");
                    this.sbList.Append("<td align=\"left\" >" + strNickName + "</td>");
                    this.sbList.Append("<td align=\"center\" ><a href=\"javascript:;\" title=\"" + str8 + "\">" + StringItem.FormatDate(datIn, "MM-dd &nbsp; hh:mm") + "</a></td>");
                    this.sbList.Append("<td align=\"center\" >" + str7 + "</td>");
                    this.sbList.Append("<td align=\"center\" >" + str5 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
                reader.Close();
                int num22 = (int) SessionItem.GetRequest("Back", 0);
                if (num22 > 0)
                {
                    str9 = string.Concat(new object[] { "UnionField.aspx?Back=", num22, "&Type=FIELDLIST&UID=", this.intUnionIDH, "&Category=", request, "&" });
                }
                else
                {
                    str9 = string.Concat(new object[] { "UnionField.aspx?Type=FIELDLIST&UID=", this.intUnionIDH, "&Category=", request, "&" });
                }
                this.GetMsgScript(str9);
                this.sbList.Append("<tr><td height='30' align='right' colspan='7'><span style='margin:0,180px,0,0'>" + this.strFind + "</span>" + this.GetMsgViewPage(str9) + "</td></tr>");
            }
            else
            {
                this.sbList.Append("<tr><td height='24' align='center' colspan='7'>暂无记录</td></tr>");
            }
        }

        private void FieldUnion()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 10;
            SqlDataReader reader = BTPUnionFieldManager.GetUnionFieldHistoryTable(this.intUnionIDH, this.intPage, this.intPerPage, false);
            if (reader.HasRows)
            {
                string str7;
                while (reader.Read())
                {
                    string str5;
                    string str6;
                    int num1 = (int) reader["FMatchID"];
                    int num7 = (int) reader["Reputation"];
                    int intUserID = (int) reader["UserIDH"];
                    int num2 = (int) reader["UserIDA"];
                    int intUnionID = (int) reader["UnionIDH"];
                    int num4 = (int) reader["UnionIDA"];
                    string strNickName = reader["ClubNameH"].ToString().Trim();
                    string str2 = reader["ClubNameA"].ToString().Trim();
                    string strIn = reader["UnionNameH"].ToString().Trim();
                    string str4 = reader["UnionNameA"].ToString().Trim();
                    DateTime datIn = (DateTime) reader["MatchTime"];
                    datIn = datIn.AddSeconds(298.0);
                    int num5 = (byte) reader["Status"];
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 12);
                    str2 = AccountItem.GetNickNameInfo(num2, str2, "Right", 12);
                    if (num5 == 6)
                    {
                        str5 = " <font color='red'>胜</font> ";
                        str6 = " <font color='red'>败</font> ";
                        if (BTPUnionManager.GetUnionRowByID(intUnionID) != null)
                        {
                            strIn = string.Concat(new object[] { "<a href='Union.aspx?Type=UNION&Kind=UNIONINTRO&UnionID=", intUnionID, "&Page=1'>", StringItem.GetShortString(strIn, 0x10), "</a>" });
                        }
                    }
                    else
                    {
                        str5 = " <font color='red'>败</font> ";
                        str6 = " <font color='red'>胜</font> ";
                        if (BTPUnionManager.GetUnionRowByID(num4) != null)
                        {
                            str4 = string.Concat(new object[] { "<a href='Union.aspx?Type=UNION&Kind=UNIONINTRO&UnionID=", num4, "&Page=1'>", StringItem.GetShortString(str4, 0x10), "</a>" });
                        }
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                    this.sbList.Append("    <td align=\"right\">" + strIn + "</td>\n");
                    this.sbList.Append("    <td align=\"right\" width=\"20\">" + str5 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" width=\"5\">:</td>\n");
                    this.sbList.Append("    <td align=\"left\" width=\"20\">" + str6 + "</td>\n");
                    this.sbList.Append("    <td align=\"left\">" + str4 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">联盟解散</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss") + "</td>\n");
                    this.sbList.Append("    <td align=\"center\"></td>\n");
                    this.sbList.Append("</tr>\n");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='8'></td></tr>");
                }
                reader.Close();
                int request = (int) SessionItem.GetRequest("Back", 0);
                if (request > 0)
                {
                    str7 = string.Concat(new object[] { "UnionField.aspx?Back=", request, "&Type=FIELDUNION&UID=", this.intUnionIDH, "&" });
                }
                else
                {
                    str7 = "UnionField.aspx?Type=FIELDUNION&UID=" + this.intUnionIDH + "&";
                }
                this.GetMsgScript(str7);
                this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetMsgViewPage(str7) + "</td></tr>");
            }
            else
            {
                this.sbList.Append("<tr><td height='24' align='center' colspan='8'>暂无记录</td></tr>");
            }
        }

        private string GetMatchTimes(DataRow dr, int intClubID, int intFMatchID, int intType)
        {
            StringBuilder builder = new StringBuilder();
            int num = (int) dr["ClubIDA"];
            int num2 = (int) dr["ClubIDB"];
            int num3 = (int) dr["ScoreA"];
            int num4 = (int) dr["ScoreB"];
            int count = 0;
            string url = DBLogin.GetMatchURL() + dr["RepURL"].ToString().Trim();
            bool flag = false;
            if ((((num3 == 20) || (num3 == 2)) && (num4 == 0)) || (((num4 == 20) || (num4 == 2)) && (num3 == 0)))
            {
                flag = true;
            }
            if (!flag)
            {
                XmlDataDocument document = new XmlDataDocument();
                document.DataSet.ReadXmlSchema(base.Server.MapPath("../MatchXML/RepSchema.xsd"));
                try
                {
                    XmlTextReader reader = new XmlTextReader(url);
                    reader.MoveToContent();
                    document.Load(reader);
                    DataSet dataSet = document.DataSet;
                    DataTable table = dataSet.Tables["Quarter"];
                    DataTable dt = dataSet.Tables["Arrange"];
                    count = table.Rows.Count;
                    int intNum = 1;
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = row["QuarterID"].ToString();
                        string str3 = row["ScoreH"].ToString();
                        string str4 = row["ScoreA"].ToString();
                        DataRow row2 = XmlHelper.GetRow(dt, string.Concat(new object[] { "QuarterID=", str2, " AND ClubID=", num }), "");
                        DataRow row3 = XmlHelper.GetRow(dt, string.Concat(new object[] { "QuarterID=", str2, " AND ClubID=", num2 }), "");
                        string vOffName = MatchItem.GetVOffName((int) row2["Offense"]);
                        string vDefName = MatchItem.GetVDefName((int) row2["Defense"]);
                        string str7 = MatchItem.GetVOffName((int) row3["Offense"]);
                        string str8 = MatchItem.GetVDefName((int) row3["Defense"]);
                        if (intType == 1)
                        {
                            builder.Append("<div class=\"zdiv\" id='MatchNode" + intNum + "'>");
                        }
                        else
                        {
                            builder.Append("<div class=\"zdiv\" id='MatchNode" + intNum + "' style=\"display:none\">");
                        }
                        builder.Append(string.Concat(new object[] { "<span id='NodeScoreA", intNum, "' style=\"display:none\">", str3, "</span>" }));
                        builder.Append(string.Concat(new object[] { "<span id='NodeScoreB", intNum, "' style=\"display:none\">", str4, "</span>" }));
                        builder.Append("<span class=\"zspan1\">" + MatchItem.GetQName(intNum, 5) + "</span>");
                        builder.Append("<span class=\"zspan1\">" + vOffName + "</span>");
                        builder.Append("<span class=\"zspan2\">" + vDefName + "</span>");
                        builder.Append("<span class=\"zspan1\">" + str7 + "</span>");
                        builder.Append("<span class=\"zspan1\">" + str8 + "</span>");
                        builder.Append("</div>");
                        intNum++;
                    }
                    reader.Close();
                }
                catch
                {
                    if (!flag)
                    {
                        return "";
                    }
                }
            }
            else
            {
                builder.Append("<div class=\"zdiv\" id='MatchNode1'></div>");
                builder.Append("<div class=\"zdiv\" id='MatchNode2'></div>");
                if (num == intClubID)
                {
                    if (num3 == 0)
                    {
                        builder.Append("<div class=\"zdiv\" id='MatchNode3'>您的球员人数不足，无法进行比赛！</div>");
                    }
                    else
                    {
                        builder.Append("<div class=\"zdiv\" id='MatchNode3'>对方的球员人数不足，无法进行比赛！</div>");
                    }
                }
                else if (num4 == 0)
                {
                    builder.Append("<div class=\"zdiv\" id='MatchNode3'>您的球员人数不足，无法进行比赛！</div>");
                }
                else
                {
                    builder.Append("<div class=\"zdiv\" id='MatchNode3'>对方的球员人数不足，无法进行比赛！</div>");
                }
                builder.Append("<div class=\"zdiv\" id='MatchNode4'></div>");
                builder.Append("<div class=\"zdiv\" id='MatchNode5'></div>");
            }
            builder.Append("," + count);
            return builder.ToString();
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
            switch (((string) SessionItem.GetRequest("Type", 1)))
            {
                case "FIELDLIST":
                    return BTPUnionFieldManager.GetUnionFieldCount(this.intUnionIDH);

                case "MATCHUNION":
                    return BTPUnionFieldManager.GetUnionFieldMatchCount(this.intUserID);

                case "FIELDUNION":
                    return BTPUnionFieldManager.GetUnionFieldHistoryCount(this.intUnionIDH);
            }
            return BTPUnionFieldManager.GetUnionFieldCount(this.intUnionIDH);
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string str5;
            string[] strArray;
            int request = (int) SessionItem.GetRequest("Page", 0);
            if (request < 1)
            {
                request = 1;
            }
            int msgTotal = this.GetMsgTotal();
            int num3 = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                num3--;
            }
            if (num3 == 0)
            {
                num3 = 1;
            }
            string str2 = "";
            str2 = "";
            if (request == 1)
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
                int num5 = request - 1;
                strArray[4] = num5.ToString();
                strArray[5] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (request == num3)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str5 = str3;
                strArray = new string[] { str5, "<a href='", strCurrentURL, "Page=", (request + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num3; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == request)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        [AjaxMethod]
        public int GetStatus()
        {
            DataRow row = BTPUnionFieldManager.GegUnionFieldRowByUserID(SessionItem.CheckLogin(1));
            int num2 = -1;
            if (row != null)
            {
                num2 = (byte) row["Status"];
            }
            return num2;
        }

        private void InitializeComponent()
        {
            int request = (int) SessionItem.GetRequest("Back", 0);
            switch (this.strType)
            {
                case "FIELDLIST":
                    this.sbPageIntro.Append("<ul><li class='qian1'>竞技场</li>");
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDMY'>我的比赛</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=MATCHUNION&Page=1'>比赛历史</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDUNION&Page=1'>大事记</a></li></ul>" }));
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblUnionFiled.Visible = true;
                    this.btnUnionFieldReg.ImageUrl = SessionItem.GetImageURL() + "FieldReg.gif";
                    if (request > 0)
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.location=\"Union.aspx?Type=UNION&Kind=VIEWUNION&Page=" + request + "\";'>[返回]</span>");
                    }
                    else
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.history.back();'>[返回]</span>");
                    }
                    this.FieldList();
                    break;

                case "FIELDMY":
                    this.sbPageIntro.Append(string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDLIST'>竞技场</a></li>" }));
                    this.sbPageIntro.Append("<li class='qian2'>我的比赛</li>");
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=MATCHUNION&Page=1'>比赛历史</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDUNION&Page=1'>大事记</a></li></ul>" }));
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblMatchRun.Visible = true;
                    if (request > 0)
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.location=\"Union.aspx?Type=UNION&Kind=VIEWUNION&Page=" + request + "\";'>[返回]</span>");
                    }
                    else
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.history.back();'>[返回]</span>");
                    }
                    this.strOnLoad = "MatchRunLoad(1)";
                    break;

                case "MATCHUNION":
                    this.sbPageIntro.Append(string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDLIST'>竞技场</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDMY'>我的比赛</a></li>" }));
                    this.sbPageIntro.Append("<li class='qian2'>比赛历史</li>");
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDUNION&Page=1'>大事记</a></li></ul>" }));
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblMatchUnion.Visible = true;
                    if (request > 0)
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.location=\"Union.aspx?Type=UNION&Kind=VIEWUNION&Page=" + request + "\";'>[返回]</span>");
                    }
                    else
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.history.back();'>[返回]</span>");
                    }
                    this.MatchUnion();
                    break;

                case "FIELDUNION":
                    this.sbPageIntro.Append(string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDLIST'>竞技场</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDMY'>我的比赛</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=MATCHUNION&Page=1'>比赛历史</a></li>" }));
                    this.sbPageIntro.Append("<li class='qian2'>大事记</li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblFiledUnion.Visible = true;
                    if (request > 0)
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.location=\"Union.aspx?Type=UNION&Kind=VIEWUNION&Page=" + request + "\";'>[返回]</span>");
                    }
                    else
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.history.back();'>[返回]</span>");
                    }
                    this.FieldUnion();
                    break;

                default:
                    this.sbPageIntro.Append("<ul><li class='qian1'>竞技场</li>");
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDMY'>我的比赛</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=MATCHUNION&Page=1'>比赛历史</a></li>" }));
                    this.sbPageIntro.Append(string.Concat(new object[] { "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/UnionField.htm\"' href='UnionField.aspx?Back=", request, "&UID=", this.intUnionIDH, "&Type=FIELDUNION&Page=1'>大事记</a></li></ul>" }));
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.tblUnionFiled.Visible = true;
                    if (request > 0)
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.location=\"Union.aspx?Type=UNION&Kind=VIEWUNION&Page=" + request + "\";'>[返回]</span>");
                    }
                    else
                    {
                        this.sbPageIntro.Append("&nbsp;&nbsp;&nbsp;&nbsp;<span style='CURSOR: hand;color:blue' onclick='javascript:window.history.back();'>[返回]</span>");
                    }
                    this.FieldList();
                    break;
            }
            base.Load += new EventHandler(this.Page_Load);
            this.btnUnionFieldReg.Click += new ImageClickEventHandler(this.btnUnionFieldReg_Click);
        }

        [AjaxMethod]
        public string MatchRunLoad()
        {
            StringBuilder builder = new StringBuilder();
            int intUserID = SessionItem.CheckLogin(1);
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(intUserID);
            int num2 = 0;
            if (onlineRowByUserID != null)
            {
                int intClubID = (int) onlineRowByUserID["ClubID5"];
                int num4 = (int) onlineRowByUserID["UnionID"];
                string str = this.RepBadWord(onlineRowByUserID["ShortName"].ToString().Trim());
                string strNickName = this.RepBadWord(onlineRowByUserID["ClubName5"].ToString().Trim());
                string str3 = this.RepBadWord(onlineRowByUserID["NickName"].ToString().Trim());
                string str4 = onlineRowByUserID["ClubLogo"].ToString().Trim();
                if (num4 > 0)
                {
                    strNickName = str + "-" + strNickName;
                }
                strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 0x10);
                str3 = AccountItem.GetNickNameInfo(intUserID, str3, "Right", 0x10);
                onlineRowByUserID = BTPUnionFieldManager.GegUnionFieldRowByUserID(intUserID);
                int intFMatchID = 0;
                if (onlineRowByUserID == null)
                {
                    num2 = -1;
                }
                else
                {
                    num2 = (byte) onlineRowByUserID["Status"];
                    intFMatchID = (int) onlineRowByUserID["FMatchID"];
                }
                builder.Append("<table cellSpacing=\"0\" cellPadding=\"4\" width=\"552\" border=\"0\" >");
                switch (num2)
                {
                    case -1:
                        builder.Append("  <tr  >\n");
                        builder.Append("    <td height=\"100\" align=\"center\" ></td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr  >\n");
                        builder.Append("    <td height=\"20\" align=\"center\" >您现在没有任何盟战比赛</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("</table>|0");
                        goto Label_1155;

                    case 0:
                    {
                        DateTime time = (DateTime) onlineRowByUserID["CreateTime"];
                        int num6 = (int) onlineRowByUserID["Reputation"];
                        string str5 = this.RepBadWord(onlineRowByUserID["UnionNameH"].ToString().Trim());
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"22\" align=\"center\" style=\" color:#001d59\">\n");
                        builder.Append("    \n");
                        builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                        builder.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>" + strNickName + "</font><br>\n");
                        builder.Append("      <font color='#666666'>" + str3 + "</font></font>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='10%'><img src='" + str4 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='10%' align='center'><img src='Images/NoNick.gif' border='0' width='46' height='46'>\n");
                        builder.Append("    </td>\n");
                        builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font color='#888888' style='line-height:140%'><span id='ClubNameA'>等待中……</span><br>\n");
                        builder.Append("      <font color='#888888'><span id='NickNameA'></span></font></font>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\n");
                        builder.Append("    </tr>\t\t\t\t\n");
                        builder.Append("    </table>\n");
                        builder.Append("    \n");
                        builder.Append("    </td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >本次挑战抵押" + num6 + "威望</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\" id=\"WaitSay\">" + str5 + " 如果在" + StringItem.FormatDate(time.AddHours(24.0), "yy-MM-dd hh:mm:ss") + "之前没有盟员接受您的挑战，您将获得本次挑战的胜利</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"180\" align=\"center\" id=\"tbText\">");
                        builder.Append("</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("</table>|1;GetStatus()");
                        goto Label_1155;
                    }
                    case 1:
                    {
                        int num7 = (int) onlineRowByUserID["UserIDH"];
                        int num8 = (int) onlineRowByUserID["UserIDA"];
                        int num1 = (int) onlineRowByUserID["UnionIDH"];
                        int num23 = (int) onlineRowByUserID["UnionIDA"];
                        int num9 = (int) onlineRowByUserID["Reputation"];
                        string str6 = this.RepBadWord(onlineRowByUserID["NickNameH"].ToString().Trim());
                        string str7 = this.RepBadWord(onlineRowByUserID["NickNameA"].ToString().Trim());
                        string str8 = this.RepBadWord(onlineRowByUserID["ClubNameH"].ToString().Trim());
                        string str9 = this.RepBadWord(onlineRowByUserID["ClubNameA"].ToString().Trim());
                        string[] strArray = onlineRowByUserID["ClubInfoH"].ToString().Split(new char[] { '|' });
                        string[] strArray2 = onlineRowByUserID["ClubInfoA"].ToString().Split(new char[] { '|' });
                        string str10 = strArray[2].Trim();
                        string str11 = strArray2[2].Trim();
                        DateTime datIn = (DateTime) onlineRowByUserID["MatchTime"];
                        datIn = datIn.AddSeconds(298.0);
                        str8 = AccountItem.GetNickNameInfo(num7, str8, "Right", 0x10);
                        str9 = AccountItem.GetNickNameInfo(num8, str9, "Right", 0x10);
                        str6 = AccountItem.GetNickNameInfo(num7, str6, "Right", 0x10);
                        str7 = AccountItem.GetNickNameInfo(num8, str7, "Right", 0x10);
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"22\" align=\"center\" style=\" color:#001d59\">\n");
                        builder.Append("    \n");
                        builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                        builder.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>" + str8 + "</font><br>\n");
                        builder.Append("      <font color='#666666'>" + str6 + "</font></font>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='10%'><img src='" + str10 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                        builder.Append("    <td width='10%' align='center'><img src='" + str11 + "' border='0' width='46' height='46'>\n");
                        builder.Append("    </td>\n");
                        builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font color='#888888' style='line-height:140%'><font color='#660066'>" + str9 + "</font><br>\n");
                        builder.Append("      <font color='#666666'>" + str7 + "</font></font>\n");
                        builder.Append("    </td>\t\t\t\t\t\t\t\n");
                        builder.Append("    </tr>\t\t\t\t\n");
                        builder.Append("    </table>\n");
                        builder.Append("    \n");
                        builder.Append("    </td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >本次挑战抵押" + num9 + "威望</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        if (num7 == intUserID)
                        {
                            builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\" id=\"WaitSay\">您已接受了" + str7 + "经理的挑战，比赛将在<span id='WaitTime'></span>正式开始</td>\n");
                        }
                        else
                        {
                            builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" style=\"color:#009933\" id=\"WaitSay\">" + str6 + "经理接受您的挑战，比赛将在<span id='WaitTime'></span>正式开始</td>\n");
                        }
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"180\" align=\"center\" id=\"tbText\">");
                        builder.Append("</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"24\" align=\"center\" valign=\"bottom\" ><img id='btnSetArrange' style='cursor:pointer' onclick='javascript:window.location=\"VArrange.aspx?Type=0\";' src=\"Images/button_SetArrange.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /></td>\n");
                        builder.Append("  </tr>\n");
                        TimeSpan span = (TimeSpan) (datIn - DateTime.Now);
                        builder.Append(string.Concat(new object[] { "</table>|1;ReadMatchBegin(", Convert.ToInt32(span.TotalSeconds), ",'", StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss"), "')" }));
                        goto Label_1155;
                    }
                }
                if ((num2 == 3) && (intFMatchID == 0))
                {
                    int num10 = (int) onlineRowByUserID["Reputation"];
                    builder.Append("  <tr  >\n");
                    builder.Append("    <td height=\"100\" align=\"center\" ></td>\n");
                    builder.Append("  </tr>\n");
                    builder.Append("  <tr  >\n");
                    builder.Append("    <td height=\"20\" align=\"center\" >对方联盟在24小时内没有接受您的挑战，您直接获得了胜利，联盟获得" + num10 + "威望 </td>\n");
                    builder.Append("  </tr>\n");
                    builder.Append("</table>|0");
                }
                else if (num2 > 1)
                {
                    int num16;
                    int num11 = (int) onlineRowByUserID["UserIDH"];
                    int num12 = (int) onlineRowByUserID["UserIDA"];
                    int num24 = (int) onlineRowByUserID["UnionIDH"];
                    int num25 = (int) onlineRowByUserID["UnionIDA"];
                    int num13 = (int) onlineRowByUserID["Reputation"];
                    intFMatchID = (int) onlineRowByUserID["FMatchID"];
                    string str12 = this.RepBadWord(onlineRowByUserID["NickNameH"].ToString().Trim());
                    string str13 = this.RepBadWord(onlineRowByUserID["NickNameA"].ToString().Trim());
                    string str14 = this.RepBadWord(onlineRowByUserID["ClubNameH"].ToString().Trim());
                    string str15 = this.RepBadWord(onlineRowByUserID["ClubNameA"].ToString().Trim());
                    string[] strArray3 = onlineRowByUserID["ClubInfoH"].ToString().Split(new char[] { '|' });
                    string[] strArray4 = onlineRowByUserID["ClubInfoA"].ToString().Split(new char[] { '|' });
                    string str16 = strArray3[2].Trim();
                    string str17 = strArray4[2].Trim();
                    int num14 = Convert.ToInt32(strArray3[0].Trim());
                    int num15 = Convert.ToInt32(strArray4[0].Trim());
                    str14 = AccountItem.GetNickNameInfo(num11, str14, "Right", 0x10);
                    str15 = AccountItem.GetNickNameInfo(num12, str15, "Right", 0x10);
                    str12 = AccountItem.GetNickNameInfo(num11, str12, "Right", 0x10);
                    str13 = AccountItem.GetNickNameInfo(num12, str13, "Right", 0x10);
                    DateTime time3 = (DateTime) onlineRowByUserID["MatchTime"];
                    time3 = time3.AddSeconds(298.0);
                    TimeSpan span2 = (TimeSpan) (DateTime.Now - time3);
                    if (span2.TotalSeconds < 60.0)
                    {
                        num16 = 2;
                    }
                    else
                    {
                        num16 = 1;
                    }
                    onlineRowByUserID = BTPFriMatchManager.GetFriMatchRowByFID(intFMatchID);
                    int num26 = (int) onlineRowByUserID["ClubIDA"];
                    int num17 = (int) onlineRowByUserID["ClubIDB"];
                    int num18 = (int) onlineRowByUserID["ScoreA"];
                    int num19 = (int) onlineRowByUserID["ScoreB"];
                    string[] strArray5 = this.GetMatchTimes(onlineRowByUserID, intClubID, intFMatchID, num16).Split(new char[] { ',' });
                    string str18 = "";
                    int num20 = 0;
                    if (strArray5[0] != null)
                    {
                        str18 = strArray5[0];
                    }
                    if (strArray5[1] != null)
                    {
                        num20 = Convert.ToInt32(strArray5[1]);
                    }
                    builder.Append("  <tr>\n");
                    builder.Append("    <td height=\"22\" align=\"center\" style=\" color:#001d59\">\n");
                    builder.Append("    \n");
                    builder.Append("    <table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t\n");
                    builder.Append("    <tr>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'>" + str14 + "</font><br>\n");
                    builder.Append("      <font color='#666666'>" + str12 + "</font></font>\n");
                    builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='10%'><img src='" + str16 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t\n");
                    if (num16 == 1)
                    {
                        string str19;
                        string str20;
                        int num21 = num18 / 100;
                        if (num21 == 0)
                        {
                            str19 = "99";
                        }
                        else
                        {
                            str19 = num21.ToString();
                        }
                        int num22 = num19 / 100;
                        if (num22 == 0)
                        {
                            str20 = "99";
                        }
                        else
                        {
                            str20 = num22.ToString();
                        }
                        builder.Append(string.Concat(new object[] { "    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/", str19, ".gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/", (num18 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/", num18 % 10, ".gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/", str20, ".gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/", (num19 / 10) % 10, ".gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/", num19 % 10, ".gif' border='0' width='19' height='28'>\n" }));
                    }
                    else
                    {
                        builder.Append("    <td width='30%' align='center'><img id='ScoreA1' src='Images/Score/99.gif' border='0' width='19' height='28'><img id='ScoreA2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreA3' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img id='ScoreB1' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB2' src='Images/Score/Bar.gif' border='0' width='19' height='28'><img id='ScoreB3' src='Images/Score/99.gif' border='0' width='19' height='28'>\n");
                    }
                    builder.Append("    </td>\t\t\t\t\t\t\t\t\n");
                    builder.Append("    <td width='10%' align='center'><img src='" + str17 + "' border='0' width='46' height='46'>\n");
                    builder.Append("    </td>\n");
                    builder.Append("    <td width='25%' align='center' valign='top' style='Padding-Top:10px'><font color='#888888' style='line-height:140%'><font color='#660066'>" + str15 + "</font><br>\n");
                    builder.Append("      <font color='#666666'>" + str13 + "</font></font>\n");
                    builder.Append("    </td>\t\t\t\t\t\t\t\n");
                    builder.Append("    </tr>\t\t\t\t\n");
                    builder.Append("    </table>\n");
                    builder.Append("    \n");
                    builder.Append("    </td>\n");
                    builder.Append("  </tr>\n");
                    if (num16 == 1)
                    {
                        string str21 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", intFMatchID, "&A=", num14, "&B=", num15, "' target='_blank'>比赛战报</a>  <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", intFMatchID, "&A=", num14, "&B=", num15, 
                            "' target='_blank'>比赛统计</a>"
                         });
                        builder.Append("  <tr>\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >比赛已经结束 " + str21 + "</td>\n");
                        builder.Append("  </tr>\n");
                    }
                    else
                    {
                        string str22 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", intFMatchID, "&A=", num14, "&B=", num15, "' target='_blank'>比赛战报</a>  <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", intFMatchID, "&A=", num14, "&B=", num15, 
                            "' target='_blank'>比赛统计</a>"
                         });
                        builder.Append("  <tr id='trMatchEnd'style=\"display:none\">\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >比赛已经结束 " + str22 + "</td>\n");
                        builder.Append("  </tr>\n");
                        builder.Append("  <tr id='trMatchRun'>\n");
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" ><span id='MatchTime'></span></td>\n");
                        builder.Append("  </tr>\n");
                    }
                    builder.Append("  <tr>\n");
                    builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" ><font color='green'>本次挑战抵押" + num13 + "威望</font></td>\n");
                    builder.Append("  </tr>\n");
                    builder.Append("  <tr>\n");
                    builder.Append("    <td height=\"180\" align=\"center\" id=\"tbText\">");
                    builder.Append(str18);
                    builder.Append("</td>\n");
                    builder.Append("  </tr>\n");
                    string str23 = "";
                    if (num16 != 1)
                    {
                        str23 = "style=\"display:none\"";
                    }
                    builder.Append("<tr id='trEndSay' " + str23 + " >\n");
                    if (num17 == intClubID)
                    {
                        if (num18 < num19)
                        {
                            builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >挑战成功，您所在联盟获得了<font color=\"green\" style=\"font-size:16px\"><strong>" + num13 + "</strong></font>联盟威望</td>\n");
                        }
                        else
                        {
                            builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >挑战失败，您所在联盟损失了<font color=\"red\" style=\"font-size:16px\"><strong>" + num13 + "</strong></font>联盟威望</td>\n");
                        }
                    }
                    else if (num18 > num19)
                    {
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >防守成功，您的联盟没有损失威望</td>\n");
                    }
                    else
                    {
                        builder.Append("    <td height=\"20\" align=\"center\" valign=\"bottom\" >防守失败，您所在联盟损失了<font color=\"red\" style=\"font-size:16px\"><strong>" + num13 + "</strong></font>联盟威望</td>\n");
                    }
                    builder.Append("  </tr>\n");
                    builder.Append("  <tr>\n");
                    if (num16 == 1)
                    {
                        builder.Append("    <td height=\"24\" align=\"center\" valign=\"bottom\" ><img style='display:none' id='imgArrage' src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /></td>\n");
                    }
                    else
                    {
                        builder.Append("    <td height=\"24\" align=\"center\" valign=\"bottom\" ><img id='imgArrage' src=\"Images/button_SetArrangeh.gif\" width=\"60\" height=\"24\" align=\"absmiddle\" /></td>\n");
                    }
                    builder.Append("  </tr>\n");
                    builder.Append("</table>");
                    if ((num16 != 1) && (num20 > 0))
                    {
                        builder.Append(string.Concat(new object[] { "|1;RunMatch(", Convert.ToInt32(span2.TotalSeconds), ",", num20, ")" }));
                    }
                    else
                    {
                        builder.Append("|0;");
                    }
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return "";
            }
        Label_1155:
            return builder.ToString();
        }

        private void MatchUnion()
        {
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 10;
            this.intUnionIDH = this.intUnionIDMy;
            SqlDataReader reader = BTPUnionFieldManager.GetUnionFieldMatchTable(this.intUserID, this.intPage, this.intPerPage, false);
            if (reader.HasRows)
            {
                string str5;
                while (reader.Read())
                {
                    int num = (int) reader["FMatchID"];
                    int num2 = (int) reader["Reputation"];
                    int intUserID = (int) reader["UserIDH"];
                    int num4 = (int) reader["UserIDA"];
                    int num5 = (int) reader["ClubIDA"];
                    int num6 = (int) reader["ClubIDB"];
                    int num7 = (int) reader["ScoreA"];
                    int num8 = (int) reader["ScoreB"];
                    string strNickName = reader["ClubNameH"].ToString().Trim();
                    string str2 = reader["ClubNameA"].ToString().Trim();
                    DateTime datIn = (DateTime) reader["MatchTime"];
                    datIn = datIn.AddSeconds(298.0);
                    byte num1 = (byte) reader["Status"];
                    strNickName = AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", 12);
                    str2 = AccountItem.GetNickNameInfo(num4, str2, "Right", 12);
                    string str3 = "";
                    string str4 = "";
                    if ((num7 > 0) || (num8 > 0))
                    {
                        str4 = string.Concat(new object[] { 
                            "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Type=1&Tag=", num, "&A=", num5, "&B=", num6, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>  <a href='", DBLogin.GetMatchURL(), "VStas.aspx?Type=1&Tag=", num, "&A=", num5, 
                            "&B=", num6, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>"
                         });
                    }
                    if (intUserID == this.intUserID)
                    {
                        if (num7 > num8)
                        {
                            str3 = "";
                        }
                        else
                        {
                            str3 = "<font color='red'>-" + num2 + "</font>";
                        }
                    }
                    else if (num7 < num8)
                    {
                        str3 = "<font color='green'>+" + num2 + "</font>";
                    }
                    else
                    {
                        str3 = "<font color='red'>-" + num2 + "</font>";
                    }
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">\n");
                    this.sbList.Append("    <td align=\"right\">" + strNickName + "</td>\n");
                    this.sbList.Append("    <td align=\"right\" width=\"20\">" + num7 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\" width=\"5\">:</td>\n");
                    this.sbList.Append("    <td align=\"left\" width=\"20\">" + num8 + "</td>\n");
                    this.sbList.Append("    <td align=\"left\">" + str2 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str3 + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss") + "</td>\n");
                    this.sbList.Append("    <td align=\"center\">" + str4 + "</td>\n");
                    this.sbList.Append("</tr>\n");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='8'></td></tr>");
                }
                reader.Close();
                int request = (int) SessionItem.GetRequest("Back", 0);
                if (request > 0)
                {
                    str5 = string.Concat(new object[] { "UnionField.aspx?Back=", request, "&Type=MATCHUNION&UID=", this.intUnionIDH, "&" });
                }
                else
                {
                    str5 = "UnionField.aspx?Type=MATCHUNION&UID=" + this.intUnionIDH + "&";
                }
                this.GetMsgScript(str5);
                this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetMsgViewPage(str5) + "</td></tr>");
            }
            else
            {
                this.sbList.Append("<tr><td height='24' align='center' colspan='8'>暂无记录</td></tr>");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(UnionField));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                this.strNickName = accountRowByUserID["NickName"].ToString();
                this.intClubID5 = (int) accountRowByUserID["ClubID5"];
                this.intUnionIDMy = (int) accountRowByUserID["UnionID"];
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                this.intUnionIDH = (int) SessionItem.GetRequest("UID", 0);
                if (this.intUnionIDH < 1)
                {
                    if (this.intUnionIDMy < 1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=1a");
                        return;
                    }
                    this.intUnionIDH = this.intUnionIDMy;
                }
                this.tblMatchRun.Visible = false;
                this.tblUnionFiled.Visible = false;
                this.tblMatchUnion.Visible = false;
                this.tblFiledUnion.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private string RepBadWord(string strBadWord)
        {
            Encoding aSCII = Encoding.ASCII;
            string str = strBadWord;
            byte[] bytes = new byte[1];
            for (byte i = 0; i < 0x20; i = (byte) (i + 1))
            {
                bytes[0] = i;
                str = str.Replace(aSCII.GetString(bytes), "*");
            }
            return str;
        }
    }
}


namespace Web
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Xml;
    using Web.DBData;
    using Web.Helper;

    public class VStas : Page
    {
        private int intClubIDA;
        private int intClubIDB;
        private int intTag;
        private int intType;
        public StringBuilder sb;
        public StringBuilder sbTitle;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intTag = (int) SessionItem.GetRequest("Tag", 0);
            this.intType = (short) SessionItem.GetRequest("Type", 2);
            this.intClubIDA = (int) SessionItem.GetRequest("A", 0);
            this.intClubIDB = (int) SessionItem.GetRequest("B", 0);
            if (this.intType == 2)
            {
                SessionItem.CheckCanUseAfterUpdate(5);
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetList();
        }

        private void SetList()
        {
            this.sb = new StringBuilder();
            this.sbTitle = new StringBuilder();
            string path = "";
            if (this.intType == 1)
            {
                DataRow friRowByID = BTPFriMatchManager.GetFriRowByID(this.intTag);
                if (friRowByID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = friRowByID["StasURL"].ToString().Trim();
            }
            else if (this.intType == 4)
            {
                if (this.intTag == 3)
                {
                    DataRow row2 = BTPXCupMatchManager.GetMatchByXGameIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                    if (row2 == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=2");
                        return;
                    }
                    path = row2["StasURL"].ToString().Trim();
                }
                else
                {
                    DataRow xGroupMatchRowByID = BTPXGroupMatchManager.GetXGroupMatchRowByID(this.intTag);
                    if (xGroupMatchRowByID == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=2");
                        return;
                    }
                    path = xGroupMatchRowByID["StasURL"].ToString().Trim();
                }
            }
            else if (this.intType == 5)
            {
                DataRow row4 = BTPXCupMatchManager.GetMatchByXGameIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                if (row4 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row4["StasURL"].ToString().Trim();
            }
            else if (this.intType == 6)
            {
                DataRow row5 = BTPDevCupMatchManager.GetMatchByDevCupIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                if (row5 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row5["StasURL"].ToString().Trim();
            }
            else
            {
                DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(this.intTag);
                if (devMRowByDevMatchID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = devMRowByDevMatchID["StasURL"].ToString().Trim();
            }
            if (path == "")
            {
                base.Response.Redirect("Report.aspx?Parameter=2");
            }
            else
            {
                DataRow row;
                string str2;
                int num;
                int num2;
                int num3;
                int num5;
                int num7;
                int num9;
                int num11;
                int num13;
                int num15;
                int num17;
                int num19;
                int num21;
                int num23;
                int num25;
                int num27;
                int num29;
                string str3;
                string str5;
                string str7;
                XmlDataDocument document = new XmlDataDocument();
                document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/StasSchema.xsd"));
                XmlTextReader reader = new XmlTextReader(base.Server.MapPath(path));
                try
                {
                    reader.MoveToContent();
                    document.Load(reader);
                }
                catch
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                DataSet dataSet = document.DataSet;
                DataTable dt = dataSet.Tables["Club"];
                DataTable table2 = dataSet.Tables["Player"];
                DataTable table3 = dataSet.Tables["Intro"];
                DataRow row8 = XmlHelper.GetRow(dt, "Type=1", "");
                string str9 = row8["ClubID"].ToString();
                string str10 = row8["ClubName"].ToString();
                string str11 = row8["Logo"].ToString();
                string str12 = row8["Score"].ToString();
                DataView view = XmlHelper.GetView(table2, "ClubID=" + str9, "Score DESC");
                str11 = Config.GetDomain() + str11;
                int num31 = 0;
                try
                {
                    num31 = (int) row8["AllAdd"];
                }
                catch
                {
                    num31 = 0;
                }
                DataRow row9 = XmlHelper.GetRow(dt, "Type=2", "");
                string str13 = row9["ClubID"].ToString();
                string str14 = row9["ClubName"].ToString();
                string str15 = row9["Logo"].ToString();
                string str16 = row9["Score"].ToString();
                str15 = Config.GetDomain() + str15;
                int num32 = 0;
                try
                {
                    num32 = (int) row9["AllAdd"];
                }
                catch
                {
                    num32 = 0;
                }
                string str17 = "";
                if (table3.Rows.Count > 0)
                {
                    str17 = XmlHelper.GetRow(table3, "", "")["Intro"].ToString();
                }
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDA);
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDB);
                DataView view2 = XmlHelper.GetView(table2, "ClubID=" + str13, "Score DESC");
                this.sbTitle.Append(str10 + " VS " + str14 + " (XBA职业比赛技术统计)");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr align=\"center\">");
                this.sb.Append("    <td width=\"100\" height=\"70\">&nbsp;</td>");
                this.sb.Append("    <td width=\"100\"><img src=\"" + str11 + "\" width=\"46\" height=\"46\"></td>");
                this.sb.Append("    <td width=\"250\">" + str10 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str12 + " </td>");
                this.sb.Append("    <td width=\"100\">VS</td>");
                this.sb.Append("    <td width=\"250\">" + str16 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str14 + " </td>");
                this.sb.Append("    <td width=\"100\"><img src=\"" + str15 + "\" width=\"46\" height=\"46\"></td>");
                this.sb.Append("    <td width=\"100\">&nbsp;</td>");
                this.sb.Append("  </tr>");
                if ((num31 > 0) || (num32 > 0))
                {
                    this.sb.Append("\t<tr>");
                    this.sb.Append("\t\t<td width=\"100\" >&nbsp;</td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\" ></td>");
                    this.sb.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num31 + "%</font></td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\"></td>");
                    this.sb.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num32 + "%</font></td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\">");
                    this.sb.Append("\t\t<td width=\"100\">&nbsp;</td>");
                    this.sb.Append("\t</tr>");
                }
                this.sb.Append("</table>");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（主队）" + str10 + "&nbsp;" + str12 + " </td>");
                this.sb.Append("  </tr>");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("      <tr bgcolor=\"#fbe2d4\" align=\"center\">");
                this.sb.Append("        <td height=\"25\" width=\"120\">姓名</td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"号码\">号码</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"位置\">位置</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"得分\">得分</a></td>");
                this.sb.Append("\t\t<td width=\"43\"<a style=\"CURSOR:hand\" title=\"篮板\">篮板</A></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"助攻\">助攻</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"抢断\">抢断</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"封盖\">封盖</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"失误\">失误</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"犯规\">犯规</a></td>");
                this.sb.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"2分命中/2分出手\">2分</a></td>");
                this.sb.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"2分命中率\">2分%</a></td>");
                this.sb.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"罚球命中/罚球次数\">罚球</a></td>");
                this.sb.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"罚球命中率\">罚球%</a></td>");
                this.sb.Append("\t\t<td width=\"76\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"3分命中/3分出手\">3分</a></td>");
                this.sb.Append("\t\t<td width=\"68\"><a style=\"CURSOR:hand\" title=\"3分命中率\">3分%</a></td>");
                this.sb.Append("\t\t<td width=\"72\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"进攻篮板/防守篮板\">篮板球</a></td>");
                this.sb.Append("    </tr>");
                int intA = 0;
                int intB = 0;
                int num8 = 0;
                int num10 = 0;
                int num12 = 0;
                int num14 = 0;
                int num16 = 0;
                int num18 = 0;
                int num20 = 0;
                int num22 = 0;
                int num24 = 0;
                int num26 = 0;
                int num28 = 0;
                int num30 = 0;
                for (int i = 0; i < view.Count; i++)
                {
                    row = view[i].Row;
                    str2 = row["Name"].ToString();
                    num = (int) row["Number"];
                    num2 = (int) row["Pos"];
                    num2 = (int) row["Pos"];
                    num3 = (int) row["FG"];
                    num5 = (int) row["FGs"];
                    num7 = (int) row["FT"];
                    num9 = (int) row["FTs"];
                    num11 = (int) row["ThreeP"];
                    num13 = (int) row["ThreePs"];
                    num15 = (int) row["To"];
                    num17 = (int) row["Score"];
                    num19 = (int) row["OReb"];
                    num21 = (int) row["DReb"];
                    num23 = (int) row["Ast"];
                    num25 = (int) row["Stl"];
                    num27 = (int) row["Blk"];
                    num29 = (int) row["Foul"];
                    str3 = StringItem.GetPercent(num3, num5, 2);
                    str5 = StringItem.GetPercent(num7, num9, 2);
                    str7 = StringItem.GetPercent(num11, num13, 2);
                    intA += num3;
                    intB += num5;
                    num8 += num7;
                    num10 += num9;
                    num12 += num11;
                    num14 += num13;
                    num16 += num15;
                    num18 += num17;
                    num20 += num19;
                    num22 += num21;
                    num24 += num23;
                    num26 += num25;
                    num28 += num27;
                    num30 += num29;
                    this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sb.Append("\t  \t<td height=\"25\">" + str2 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                    this.sb.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                    this.sb.Append("\t\t<td>" + num17 + "</td>");
                    this.sb.Append("\t\t<td>" + (num19 + num21) + "</td>");
                    this.sb.Append("\t\t<td>" + num23 + "</td>");
                    this.sb.Append("\t\t<td>" + num25 + "</td>");
                    this.sb.Append("\t\t<td>" + num27 + "</td>");
                    this.sb.Append("\t\t<td>" + num15 + "</td>");
                    this.sb.Append("\t\t<td>" + num29 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num5, "</td>" }));
                    this.sb.Append("\t\t<td>" + str3 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num9, "</td>" }));
                    this.sb.Append("\t\t<td>" + str5 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num13, "</td>" }));
                    this.sb.Append("\t\t<td>" + str7 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num21, "</td>" }));
                    this.sb.Append("\t  </tr>");
                }
                string str4 = StringItem.GetPercent(intA, intB, 2);
                string str6 = StringItem.GetPercent(num8, num10, 2);
                string str8 = StringItem.GetPercent(num12, num14, 2);
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">合计</td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td>" + num18 + "</td>");
                this.sb.Append("\t\t<td>" + (num20 + num22) + "</td>");
                this.sb.Append("\t\t<td>" + num24 + "</td>");
                this.sb.Append("\t\t<td>" + num26 + "</td>");
                this.sb.Append("\t\t<td>" + num28 + "</td>");
                this.sb.Append("\t\t<td>" + num16 + "</td>");
                this.sb.Append("\t\t<td>" + num30 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
                this.sb.Append("\t\t<td>" + str4 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num8, "/", num10, "</td>" }));
                this.sb.Append("\t\t<td>" + str6 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num12, "/", num14, "</td>" }));
                this.sb.Append("\t\t<td>" + str8 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num20, "/", num22, "</td>" }));
                this.sb.Append("\t  </tr>");
                this.sb.Append("<tr>");
                this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
                this.sb.Append("      </tr>");
                this.sb.Append("    </table></td>");
                this.sb.Append("  </tr>");
                this.sb.Append("</table>");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（客队）" + str14 + "&nbsp;" + str16 + " </td>");
                this.sb.Append("  </tr>");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("      <tr bgcolor=\"#fbe2d4\" align=\"center\">");
                this.sb.Append("        <td height=\"25\" width=\"120\">姓名</td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"号码\">号码</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"位置\">位置</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"得分\">得分</a></td>");
                this.sb.Append("\t\t<td width=\"43\"<a style=\"CURSOR:hand\" title=\"篮板\">篮板</A></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"助攻\">助攻</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"抢断\">抢断</a></td>");
                this.sb.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"封盖\">封盖</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"失误\">失误</a></td>");
                this.sb.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"犯规\">犯规</a></td>");
                this.sb.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"2分命中/2分出手\">2分</a></td>");
                this.sb.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"2分命中率\">2分%</a></td>");
                this.sb.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"罚球命中/罚球次数\">罚球</a></td>");
                this.sb.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"罚球命中率\">罚球%</a></td>");
                this.sb.Append("\t\t<td width=\"76\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"3分命中/3分出手\">3分</a></td>");
                this.sb.Append("\t\t<td width=\"68\"><a style=\"CURSOR:hand\" title=\"3分命中率\">3分%</a></td>");
                this.sb.Append("\t\t<td width=\"72\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"进攻篮板/防守篮板\">篮板球</a></td>");
                this.sb.Append("      </tr>");
                intA = 0;
                intB = 0;
                num8 = 0;
                num10 = 0;
                num12 = 0;
                num14 = 0;
                num16 = 0;
                num18 = 0;
                num20 = 0;
                num22 = 0;
                num24 = 0;
                num26 = 0;
                num28 = 0;
                num30 = 0;
                for (int j = 0; j < view2.Count; j++)
                {
                    row = view2[j].Row;
                    str2 = row["Name"].ToString();
                    num = (int) row["Number"];
                    num2 = (int) row["Pos"];
                    num2 = (int) row["Pos"];
                    num3 = (int) row["FG"];
                    num5 = (int) row["FGs"];
                    num7 = (int) row["FT"];
                    num9 = (int) row["FTs"];
                    num11 = (int) row["ThreeP"];
                    num13 = (int) row["ThreePs"];
                    num15 = (int) row["To"];
                    num17 = (int) row["Score"];
                    num19 = (int) row["OReb"];
                    num21 = (int) row["DReb"];
                    num23 = (int) row["Ast"];
                    num25 = (int) row["Stl"];
                    num27 = (int) row["Blk"];
                    num29 = (int) row["Foul"];
                    str3 = StringItem.GetPercent(num3, num5, 2);
                    str5 = StringItem.GetPercent(num7, num9, 2);
                    str7 = StringItem.GetPercent(num11, num13, 2);
                    intA += num3;
                    intB += num5;
                    num8 += num7;
                    num10 += num9;
                    num12 += num11;
                    num14 += num13;
                    num16 += num15;
                    num18 += num17;
                    num20 += num19;
                    num22 += num21;
                    num24 += num23;
                    num26 += num25;
                    num28 += num27;
                    num30 += num29;
                    this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sb.Append("\t  \t<td height=\"25\">" + str2 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                    this.sb.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                    this.sb.Append("\t\t<td>" + num17 + "</td>");
                    this.sb.Append("\t\t<td>" + (num19 + num21) + "</td>");
                    this.sb.Append("\t\t<td>" + num23 + "</td>");
                    this.sb.Append("\t\t<td>" + num25 + "</td>");
                    this.sb.Append("\t\t<td>" + num27 + "</td>");
                    this.sb.Append("\t\t<td>" + num15 + "</td>");
                    this.sb.Append("\t\t<td>" + num29 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num5, "</td>" }));
                    this.sb.Append("\t\t<td>" + str3 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num9, "</td>" }));
                    this.sb.Append("\t\t<td>" + str5 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num13, "</td>" }));
                    this.sb.Append("\t\t<td>" + str7 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num21, "</td>" }));
                    this.sb.Append("\t  </tr>");
                }
                str4 = StringItem.GetPercent(intA, intB, 2);
                str6 = StringItem.GetPercent(num8, num10, 2);
                str8 = StringItem.GetPercent(num12, num14, 2);
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">合计</td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td>" + num18 + "</td>");
                this.sb.Append("\t\t<td>" + (num20 + num22) + "</td>");
                this.sb.Append("\t\t<td>" + num24 + "</td>");
                this.sb.Append("\t\t<td>" + num26 + "</td>");
                this.sb.Append("\t\t<td>" + num28 + "</td>");
                this.sb.Append("\t\t<td>" + num16 + "</td>");
                this.sb.Append("\t\t<td>" + num30 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
                this.sb.Append("\t\t<td>" + str4 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num8, "/", num10, "</td>" }));
                this.sb.Append("\t\t<td>" + str6 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num12, "/", num14, "</td>" }));
                this.sb.Append("\t\t<td>" + str8 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num20, "/", num22, "</td>" }));
                this.sb.Append("\t  </tr>");
                this.sb.Append("<tr>");
                this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
                this.sb.Append("      </tr>");
                this.sb.Append("    </table></td>");
                this.sb.Append("  </tr>");
                if (str17 != "")
                {
                    this.sb.Append("  <tr>");
                    this.sb.Append("<td>比赛总结:" + str17.Trim() + "<br><br><br></td>");
                    this.sb.Append("  </tr>");
                }
                this.sb.Append("</table>");
            }
        }
    }
}


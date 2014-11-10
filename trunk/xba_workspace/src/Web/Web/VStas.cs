namespace Web
{
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
            this.intTag = SessionItem.GetRequest("Tag", 0);
            this.intType = SessionItem.GetRequest("Type", 2);
            this.intClubIDA = SessionItem.GetRequest("A", 0);
            this.intClubIDB = SessionItem.GetRequest("B", 0);
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
            else if (this.intType == 7)
            {
                DataRow unionCupMatchByID = BTPUnionCupManager.GetUnionCupMatchByID(this.intTag);
                if (unionCupMatchByID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = unionCupMatchByID["StasURL"].ToString().Trim();
            }
            else if (this.intType == 9)
            {
                DataRow oneStarMatchByID = BTPStarMatchManager.GetOneStarMatchByID(this.intTag);
                if (oneStarMatchByID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = oneStarMatchByID["StasURL"].ToString().Trim();
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
                int num4;
                int num5;
                int num6;
                int num7;
                int num8;
                int num9;
                int num10;
                int num11;
                int num12;
                int num13;
                int num14;
                int num15;
                int num16;
                string str3;
                string str4;
                string str5;
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
                DataRow row10 = XmlHelper.GetRow(dt, "Type=1", "");
                string str6 = row10["ClubID"].ToString();
                string str7 = row10["ClubName"].ToString();
                string str8 = row10["Logo"].ToString();
                string str9 = row10["Score"].ToString();
                DataView view = XmlHelper.GetView(table2, "ClubID=" + str6, "Score DESC");
                str8 = Config.GetDomain() + str8;
                int num17 = 0;
                try
                {
                    num17 = (int) row10["AllAdd"];
                }
                catch
                {
                    num17 = 0;
                }
                DataRow row11 = XmlHelper.GetRow(dt, "Type=2", "");
                string str10 = row11["ClubID"].ToString();
                string str11 = row11["ClubName"].ToString();
                string str12 = row11["Logo"].ToString();
                string str13 = row11["Score"].ToString();
                str12 = Config.GetDomain() + str12;
                int num18 = 0;
                try
                {
                    num18 = (int) row11["AllAdd"];
                }
                catch
                {
                    num18 = 0;
                }
                string str14 = "";
                if (table3.Rows.Count > 0)
                {
                    str14 = XmlHelper.GetRow(table3, "", "")["Intro"].ToString();
                }
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDA);
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDB);
                DataView view2 = XmlHelper.GetView(table2, "ClubID=" + str10, "Score DESC");
                this.sbTitle.Append(str7 + " VS " + str11 + " (XBA职业比赛技术统计)");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr align=\"center\">");
                this.sb.Append("    <td width=\"100\" height=\"70\">&nbsp;</td>");
                this.sb.Append("    <td width=\"100\"><img src=\"" + str8 + "\" width=\"46\" height=\"46\"></td>");
                this.sb.Append("    <td width=\"250\">" + str7 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str9 + " </td>");
                this.sb.Append("    <td width=\"100\">VS</td>");
                this.sb.Append("    <td width=\"250\">" + str13 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str11 + " </td>");
                this.sb.Append("    <td width=\"100\"><img src=\"" + str12 + "\" width=\"46\" height=\"46\"></td>");
                this.sb.Append("    <td width=\"100\">&nbsp;</td>");
                this.sb.Append("  </tr>");
                if ((num17 > 0) || (num18 > 0))
                {
                    this.sb.Append("\t<tr>");
                    this.sb.Append("\t\t<td width=\"100\" >&nbsp;</td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\" ></td>");
                    this.sb.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num17 + "%</font></td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\"></td>");
                    this.sb.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num18 + "%</font></td>");
                    this.sb.Append("\t\t<td width=\"100\" align=\"center\">");
                    this.sb.Append("\t\t<td width=\"100\">&nbsp;</td>");
                    this.sb.Append("\t</tr>");
                }
                this.sb.Append("</table>");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（主队）" + str7 + "&nbsp;" + str9 + " </td>");
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
                int num21 = 0;
                int num22 = 0;
                int num23 = 0;
                int num24 = 0;
                int num25 = 0;
                int num26 = 0;
                int num27 = 0;
                int num28 = 0;
                int num29 = 0;
                int num30 = 0;
                int num31 = 0;
                int num32 = 0;
                for (int i = 0; i < view.Count; i++)
                {
                    row = view[i].Row;
                    str2 = row["Name"].ToString();
                    num = (int) row["Number"];
                    num2 = (int) row["Pos"];
                    num2 = (int) row["Pos"];
                    num3 = (int) row["FG"];
                    num4 = (int) row["FGs"];
                    num5 = (int) row["FT"];
                    num6 = (int) row["FTs"];
                    num7 = (int) row["ThreeP"];
                    num8 = (int) row["ThreePs"];
                    num9 = (int) row["To"];
                    num10 = (int) row["Score"];
                    num11 = (int) row["OReb"];
                    num12 = (int) row["DReb"];
                    num13 = (int) row["Ast"];
                    num14 = (int) row["Stl"];
                    num15 = (int) row["Blk"];
                    num16 = (int) row["Foul"];
                    str3 = StringItem.GetPercent(num3, num4, 2);
                    str4 = StringItem.GetPercent(num5, num6, 2);
                    str5 = StringItem.GetPercent(num7, num8, 2);
                    intA += num3;
                    intB += num4;
                    num21 += num5;
                    num22 += num6;
                    num23 += num7;
                    num24 += num8;
                    num25 += num9;
                    num26 += num10;
                    num27 += num11;
                    num28 += num12;
                    num29 += num13;
                    num30 += num14;
                    num31 += num15;
                    num32 += num16;
                    this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sb.Append("\t  \t<td height=\"25\">" + str2 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                    this.sb.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                    this.sb.Append("\t\t<td>" + num10 + "</td>");
                    this.sb.Append("\t\t<td>" + (num11 + num12) + "</td>");
                    this.sb.Append("\t\t<td>" + num13 + "</td>");
                    this.sb.Append("\t\t<td>" + num14 + "</td>");
                    this.sb.Append("\t\t<td>" + num15 + "</td>");
                    this.sb.Append("\t\t<td>" + num9 + "</td>");
                    this.sb.Append("\t\t<td>" + num16 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num4, "</td>" }));
                    this.sb.Append("\t\t<td>" + str3 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                    this.sb.Append("\t\t<td>" + str4 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                    this.sb.Append("\t\t<td>" + str5 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                    this.sb.Append("\t  </tr>");
                }
                string str15 = StringItem.GetPercent(intA, intB, 2);
                string str16 = StringItem.GetPercent(num21, num22, 2);
                string str17 = StringItem.GetPercent(num23, num24, 2);
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">合计</td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td>" + num26 + "</td>");
                this.sb.Append("\t\t<td>" + (num27 + num28) + "</td>");
                this.sb.Append("\t\t<td>" + num29 + "</td>");
                this.sb.Append("\t\t<td>" + num30 + "</td>");
                this.sb.Append("\t\t<td>" + num31 + "</td>");
                this.sb.Append("\t\t<td>" + num25 + "</td>");
                this.sb.Append("\t\t<td>" + num32 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
                this.sb.Append("\t\t<td>" + str15 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
                this.sb.Append("\t\t<td>" + str16 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num23, "/", num24, "</td>" }));
                this.sb.Append("\t\t<td>" + str17 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num27, "/", num28, "</td>" }));
                this.sb.Append("\t  </tr>");
                this.sb.Append("<tr>");
                this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
                this.sb.Append("      </tr>");
                this.sb.Append("    </table></td>");
                this.sb.Append("  </tr>");
                this.sb.Append("</table>");
                this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                this.sb.Append("  <tr>");
                this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（客队）" + str11 + "&nbsp;" + str13 + " </td>");
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
                num21 = 0;
                num22 = 0;
                num23 = 0;
                num24 = 0;
                num25 = 0;
                num26 = 0;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                num30 = 0;
                num31 = 0;
                num32 = 0;
                for (int j = 0; j < view2.Count; j++)
                {
                    row = view2[j].Row;
                    str2 = row["Name"].ToString();
                    num = (int) row["Number"];
                    num2 = (int) row["Pos"];
                    num2 = (int) row["Pos"];
                    num3 = (int) row["FG"];
                    num4 = (int) row["FGs"];
                    num5 = (int) row["FT"];
                    num6 = (int) row["FTs"];
                    num7 = (int) row["ThreeP"];
                    num8 = (int) row["ThreePs"];
                    num9 = (int) row["To"];
                    num10 = (int) row["Score"];
                    num11 = (int) row["OReb"];
                    num12 = (int) row["DReb"];
                    num13 = (int) row["Ast"];
                    num14 = (int) row["Stl"];
                    num15 = (int) row["Blk"];
                    num16 = (int) row["Foul"];
                    str3 = StringItem.GetPercent(num3, num4, 2);
                    str4 = StringItem.GetPercent(num5, num6, 2);
                    str5 = StringItem.GetPercent(num7, num8, 2);
                    intA += num3;
                    intB += num4;
                    num21 += num5;
                    num22 += num6;
                    num23 += num7;
                    num24 += num8;
                    num25 += num9;
                    num26 += num10;
                    num27 += num11;
                    num28 += num12;
                    num29 += num13;
                    num30 += num14;
                    num31 += num15;
                    num32 += num16;
                    this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sb.Append("\t  \t<td height=\"25\">" + str2 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                    this.sb.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                    this.sb.Append("\t\t<td>" + num10 + "</td>");
                    this.sb.Append("\t\t<td>" + (num11 + num12) + "</td>");
                    this.sb.Append("\t\t<td>" + num13 + "</td>");
                    this.sb.Append("\t\t<td>" + num14 + "</td>");
                    this.sb.Append("\t\t<td>" + num15 + "</td>");
                    this.sb.Append("\t\t<td>" + num9 + "</td>");
                    this.sb.Append("\t\t<td>" + num16 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num4, "</td>" }));
                    this.sb.Append("\t\t<td>" + str3 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                    this.sb.Append("\t\t<td>" + str4 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                    this.sb.Append("\t\t<td>" + str5 + "</td>");
                    this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                    this.sb.Append("\t  </tr>");
                }
                str15 = StringItem.GetPercent(intA, intB, 2);
                str16 = StringItem.GetPercent(num21, num22, 2);
                str17 = StringItem.GetPercent(num23, num24, 2);
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">合计</td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td></td>");
                this.sb.Append("\t\t<td>" + num26 + "</td>");
                this.sb.Append("\t\t<td>" + (num27 + num28) + "</td>");
                this.sb.Append("\t\t<td>" + num29 + "</td>");
                this.sb.Append("\t\t<td>" + num30 + "</td>");
                this.sb.Append("\t\t<td>" + num31 + "</td>");
                this.sb.Append("\t\t<td>" + num25 + "</td>");
                this.sb.Append("\t\t<td>" + num32 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
                this.sb.Append("\t\t<td>" + str15 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
                this.sb.Append("\t\t<td>" + str16 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num23, "/", num24, "</td>" }));
                this.sb.Append("\t\t<td>" + str17 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num27, "/", num28, "</td>" }));
                this.sb.Append("\t  </tr>");
                this.sb.Append("<tr>");
                this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
                this.sb.Append("      </tr>");
                this.sb.Append("    </table></td>");
                this.sb.Append("  </tr>");
                if (str14 != "")
                {
                    this.sb.Append("  <tr>");
                    this.sb.Append("<td>比赛总结:" + str14.Trim() + "<br><br><br></td>");
                    this.sb.Append("  </tr>");
                }
                this.sb.Append("</table>");
            }
        }
    }
}


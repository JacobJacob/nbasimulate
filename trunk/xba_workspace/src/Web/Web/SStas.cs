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

    public class SStas : Page
    {
        private int intClubIDA;
        private int intClubIDB;
        private int intTag;
        private int intType;
        public StringBuilder sbList;
        public StringBuilder sbTitle;
        public string strList;

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
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetList();
        }

        private void SetList()
        {
            DataRow row;
            string str;
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
            string str2;
            string str3;
            string str4;
            this.sbList = new StringBuilder();
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
            else
            {
                DataRow row3 = BTPCupMatchManager.GetMatchByCupIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                if (row3 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row3["StasURL"].ToString().Trim();
            }
            XmlDataDocument document = new XmlDataDocument();
            document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/StasSchema.xsd"));
            XmlTextReader reader = new XmlTextReader(base.Server.MapPath(path));
            try
            {
                reader.MoveToContent();
                document.Load(reader);
            }
            catch (Exception exception)
            {
                exception.ToString();
                base.Response.Redirect("Report.aspx?Parameter=2");
                return;
            }
            DataSet dataSet = document.DataSet;
            DataTable dt = dataSet.Tables["Club"];
            DataTable table2 = dataSet.Tables["Player"];
            DataTable table3 = dataSet.Tables["Intro"];
            DataRow row4 = XmlHelper.GetRow(dt, "Type=1", "");
            string str6 = row4["ClubID"].ToString();
            string str7 = row4["ClubName"].ToString();
            string str8 = row4["Logo"].ToString();
            string str9 = row4["Score"].ToString();
            DataView view = XmlHelper.GetView(table2, "ClubID=" + str6, "Score DESC");
            str8 = DBLogin.URLString(ServerParameter.intGameCategory) + str8;
            DataRow row5 = XmlHelper.GetRow(dt, "Type=2", "");
            string str10 = row5["ClubID"].ToString();
            string str11 = row5["ClubName"].ToString();
            string str12 = row5["Logo"].ToString();
            string str13 = row5["Score"].ToString();
            DataView view2 = XmlHelper.GetView(table2, "ClubID=" + str10, "Score DESC");
            str12 = DBLogin.URLString(ServerParameter.intGameCategory) + str12;
            string str14 = "";
            if (table3.Rows.Count > 0)
            {
                str14 = XmlHelper.GetRow(table3, "", "")["Intro"].ToString();
            }
            this.sbTitle.Append(str7 + " VS " + str11 + " (XBA街球比赛战报)");
            this.sbList.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbList.Append("  <tr align=\"center\">");
            this.sbList.Append("    <td width=\"10%\" height=\"70\">&nbsp;</td>");
            this.sbList.Append("    <td width=\"10%\"><img src=\"" + str8 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("    <td width=\"25%\">" + str7 + "&nbsp;" + str9 + " </td>");
            this.sbList.Append("    <td width=\"10%\">VS</td>");
            this.sbList.Append("    <td width=\"25%\">" + str11 + "&nbsp;" + str13 + " </td>");
            this.sbList.Append("    <td width=\"10%\"><img src=\"" + str12 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("    <td width=\"10%\">&nbsp;</td>");
            this.sbList.Append("  </tr>");
            this.sbList.Append("</table>");
            this.sbList.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbList.Append("  <tr>");
            this.sbList.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（主队）" + str7 + "&nbsp;" + str9 + " </td>");
            this.sbList.Append("  </tr>");
            this.sbList.Append("  <tr>");
            this.sbList.Append("    <td><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbList.Append("      <tr bgcolor=\"#fbe2d4\" align=\"center\">");
            this.sbList.Append("        <td height=\"25\" width=\"120\">姓名</td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"号码\">号码</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"位置\">位置</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"得分\">得分</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"<a style=\"CURSOR:hand\" title=\"篮板\">篮板</A></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"助攻\">助攻</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"抢断\">抢断</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"封盖\">封盖</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"失误\">失误</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"犯规\">犯规</a></td>");
            this.sbList.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"2分命中/2分出手\">2分</a></td>");
            this.sbList.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"2分命中率\">2分%</a></td>");
            this.sbList.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"罚球命中/罚球次数\">罚球</a></td>");
            this.sbList.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"罚球命中率\">罚球%</a></td>");
            this.sbList.Append("\t\t<td width=\"76\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"3分命中/3分出手\">3分</a></td>");
            this.sbList.Append("\t\t<td width=\"68\"><a style=\"CURSOR:hand\" title=\"3分命中率\">3分%</a></td>");
            this.sbList.Append("\t\t<td width=\"72\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"进攻篮板/防守篮板\">篮板球</a></td>");
            this.sbList.Append("    </tr>");
            int intA = 0;
            int intB = 0;
            int num19 = 0;
            int num20 = 0;
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
            for (int i = 0; i < view.Count; i++)
            {
                row = view[i].Row;
                str = row["Name"].ToString();
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
                str2 = StringItem.GetPercent(num3, num4, 2);
                str3 = StringItem.GetPercent(num5, num6, 2);
                str4 = StringItem.GetPercent(num7, num8, 2);
                intA += num3;
                intB += num4;
                num19 += num5;
                num20 += num6;
                num21 += num7;
                num22 += num8;
                num23 += num9;
                num24 += num10;
                num25 += num11;
                num26 += num12;
                num27 += num13;
                num28 += num14;
                num29 += num15;
                num30 += num16;
                this.sbList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("\t  \t<td height=\"25\">" + str + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                this.sbList.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                this.sbList.Append("\t\t<td>" + num10 + "</td>");
                this.sbList.Append("\t\t<td>" + (num11 + num12) + "</td>");
                this.sbList.Append("\t\t<td>" + num13 + "</td>");
                this.sbList.Append("\t\t<td>" + num14 + "</td>");
                this.sbList.Append("\t\t<td>" + num15 + "</td>");
                this.sbList.Append("\t\t<td>" + num9 + "</td>");
                this.sbList.Append("\t\t<td>" + num16 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num4, "</td>" }));
                this.sbList.Append("\t\t<td>" + str2 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                this.sbList.Append("\t\t<td>" + str3 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                this.sbList.Append("\t\t<td>" + str4 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                this.sbList.Append("\t  </tr>");
            }
            string str15 = StringItem.GetPercent(intA, intB, 2);
            string str16 = StringItem.GetPercent(num19, num20, 2);
            string str17 = StringItem.GetPercent(num21, num22, 2);
            this.sbList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("\t  \t<td height=\"25\">合计</td>");
            this.sbList.Append("\t\t<td></td>");
            this.sbList.Append("\t\t<td></td>");
            this.sbList.Append("\t\t<td>" + num24 + "</td>");
            this.sbList.Append("\t\t<td>" + (num25 + num26) + "</td>");
            this.sbList.Append("\t\t<td>" + num27 + "</td>");
            this.sbList.Append("\t\t<td>" + num28 + "</td>");
            this.sbList.Append("\t\t<td>" + num29 + "</td>");
            this.sbList.Append("\t\t<td>" + num23 + "</td>");
            this.sbList.Append("\t\t<td>" + num30 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
            this.sbList.Append("\t\t<td>" + str15 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num20, "</td>" }));
            this.sbList.Append("\t\t<td>" + str16 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
            this.sbList.Append("\t\t<td>" + str17 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num25, "/", num26, "</td>" }));
            this.sbList.Append("\t  </tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
            this.sbList.Append("      </tr>");
            this.sbList.Append("    </table></td>");
            this.sbList.Append("  </tr>");
            this.sbList.Append("</table>");
            this.sbList.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbList.Append("  <tr>");
            this.sbList.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（客队）" + str11 + "&nbsp;" + str13 + " </td>");
            this.sbList.Append("  </tr>");
            this.sbList.Append("  <tr>");
            this.sbList.Append("    <td><table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sbList.Append("      <tr bgcolor=\"#fbe2d4\" align=\"center\">");
            this.sbList.Append("        <td height=\"25\" width=\"120\">姓名</td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"号码\">号码</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"位置\">位置</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"得分\">得分</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"<a style=\"CURSOR:hand\" title=\"篮板\">篮板</A></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"助攻\">助攻</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"抢断\">抢断</a></td>");
            this.sbList.Append("\t\t<td width=\"43\"><a style=\"CURSOR:hand\" title=\"封盖\">封盖</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"失误\">失误</a></td>");
            this.sbList.Append("\t\t<td width=\"42\"><a style=\"CURSOR:hand\" title=\"犯规\">犯规</a></td>");
            this.sbList.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"2分命中/2分出手\">2分</a></td>");
            this.sbList.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"2分命中率\">2分%</a></td>");
            this.sbList.Append("\t\t<td width=\"75\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"罚球命中/罚球次数\">罚球</a></td>");
            this.sbList.Append("\t\t<td width=\"67\"><a style=\"CURSOR:hand\" title=\"罚球命中率\">罚球%</a></td>");
            this.sbList.Append("\t\t<td width=\"76\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"3分命中/3分出手\">3分</a></td>");
            this.sbList.Append("\t\t<td width=\"68\"><a style=\"CURSOR:hand\" title=\"3分命中率\">3分%</a></td>");
            this.sbList.Append("\t\t<td width=\"72\" style=\"font-size:12px;\"><a style=\"CURSOR:hand\" title=\"进攻篮板/防守篮板\">篮板球</a></td>");
            this.sbList.Append("      </tr>");
            intA = 0;
            intB = 0;
            num19 = 0;
            num20 = 0;
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
            for (int j = 0; j < view2.Count; j++)
            {
                row = view2[j].Row;
                str = row["Name"].ToString();
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
                str2 = StringItem.GetPercent(num3, num4, 2);
                str3 = StringItem.GetPercent(num5, num6, 2);
                str4 = StringItem.GetPercent(num7, num8, 2);
                intA += num3;
                intB += num4;
                num19 += num5;
                num20 += num6;
                num21 += num7;
                num22 += num8;
                num23 += num9;
                num24 += num10;
                num25 += num11;
                num26 += num12;
                num27 += num13;
                num28 += num14;
                num29 += num15;
                num30 += num16;
                this.sbList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sbList.Append("\t  \t<td height=\"25\">" + str + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num, ".gif\" width=\"16\" height=\"19\" border=\"0\"></td>" }));
                this.sbList.Append("\t\t<td>" + PlayerItem.GetPlayerEngPosition(num2) + "</td>");
                this.sbList.Append("\t\t<td>" + num10 + "</td>");
                this.sbList.Append("\t\t<td>" + (num11 + num12) + "</td>");
                this.sbList.Append("\t\t<td>" + num13 + "</td>");
                this.sbList.Append("\t\t<td>" + num14 + "</td>");
                this.sbList.Append("\t\t<td>" + num15 + "</td>");
                this.sbList.Append("\t\t<td>" + num9 + "</td>");
                this.sbList.Append("\t\t<td>" + num16 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num3, "/", num4, "</td>" }));
                this.sbList.Append("\t\t<td>" + str2 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                this.sbList.Append("\t\t<td>" + str3 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                this.sbList.Append("\t\t<td>" + str4 + "</td>");
                this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                this.sbList.Append("\t  </tr>");
            }
            str15 = StringItem.GetPercent(intA, intB, 2);
            str16 = StringItem.GetPercent(num19, num20, 2);
            str17 = StringItem.GetPercent(num21, num22, 2);
            this.sbList.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("\t  \t<td height=\"25\">合计</td>");
            this.sbList.Append("\t\t<td></td>");
            this.sbList.Append("\t\t<td></td>");
            this.sbList.Append("\t\t<td>" + num24 + "</td>");
            this.sbList.Append("\t\t<td>" + (num25 + num26) + "</td>");
            this.sbList.Append("\t\t<td>" + num27 + "</td>");
            this.sbList.Append("\t\t<td>" + num28 + "</td>");
            this.sbList.Append("\t\t<td>" + num29 + "</td>");
            this.sbList.Append("\t\t<td>" + num23 + "</td>");
            this.sbList.Append("\t\t<td>" + num30 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
            this.sbList.Append("\t\t<td>" + str15 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num20, "</td>" }));
            this.sbList.Append("\t\t<td>" + str16 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
            this.sbList.Append("\t\t<td>" + str17 + "</td>");
            this.sbList.Append(string.Concat(new object[] { "\t\t<td>", num25, "/", num26, "</td>" }));
            this.sbList.Append("\t  </tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
            this.sbList.Append("      </tr>");
            this.sbList.Append("    </table></td>");
            this.sbList.Append("  </tr>");
            if (str14 != "")
            {
                this.sbList.Append("  <tr>");
                this.sbList.Append("<td>比赛总结:" + str14.Trim() + "<br><br><br></td>");
                this.sbList.Append("  </tr>");
            }
            this.sbList.Append("</table>");
        }
    }
}


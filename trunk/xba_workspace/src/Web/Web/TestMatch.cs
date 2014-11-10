namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using Web.DBConnection;
    using Web.Helper;
    using Web.VMatchEngine;

    public class TestMatch : Page
    {
        protected Button btnSim;
        public StringBuilder sb = new StringBuilder();
        public StringBuilder sbList = new StringBuilder();
        protected TextBox tbMatchID;

        private void btnSim_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(this.tbMatchID.Text);
            string commandText = "SELECT * FROM BTP_FriMatch WHERE FMatchID=" + num;
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (row != null)
            {
                int intClubIDH = (int) row["ClubIDA"];
                int intClubIDA = (int) row["ClubIDB"];
                int intTag = (int) row["FMatchID"];
                Match m = new Match(intClubIDH, intClubIDA, false, 7, intTag, false, false, 0, 0);
                m.Run();
                this.SetRepString(m);
                this.SetStasString(m);
            }
        }

        private void InitializeComponent()
        {
            this.btnSim.Click += new EventHandler(this.btnSim_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetRepString(Match m)
        {
            XmlDataDocument document = new XmlDataDocument();
            document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/RepSchema.xsd"));
            XmlTextReader reader = new XmlTextReader(m.sbRepXml.ToString(), XmlNodeType.Element, null);
            reader.MoveToContent();
            document.Load(reader);
            DataSet dataSet = document.DataSet;
            DataTable dt = dataSet.Tables["Club"];
            DataTable table2 = dataSet.Tables["Quarter"];
            DataTable table3 = dataSet.Tables["Arrange"];
            DataTable table4 = dataSet.Tables["Player"];
            DataTable table5 = dataSet.Tables["Script"];
            DataRow row = XmlHelper.GetRow(dt, "Type=1", "");
            string str = row["ClubID"].ToString();
            string str2 = row["ClubName"].ToString();
            string str3 = row["Logo"].ToString();
            string str4 = row["Score"].ToString();
            DataRow row2 = XmlHelper.GetRow(dt, "Type=2", "");
            string str5 = row2["ClubID"].ToString();
            string str6 = row2["ClubName"].ToString();
            string str7 = row2["Logo"].ToString();
            string str8 = row2["Score"].ToString();
            this.sbList.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
            this.sbList.Append("\t<tr>");
            this.sbList.Append("\t\t<td width=\"10%\" height=\"70\">&nbsp;</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\"><img src=\"" + str3 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("\t\t<td width=\"25%\" align=\"center\">" + str2 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str4 + "</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\">VS</td>");
            this.sbList.Append("\t\t<td width=\"25%\" align=\"center\">" + str8 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str6 + "</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\"><img src=\"" + str7 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("\t\t<td width=\"10%\">&nbsp;</td>");
            this.sbList.Append("\t</tr>");
            this.sbList.Append("</table>");
            int intNum = 1;
            foreach (DataRow row3 in table2.Rows)
            {
                string str9 = row3["QuarterID"].ToString();
                string str10 = row3["ScoreH"].ToString();
                string str11 = row3["ScoreA"].ToString();
                try
                {
                    DataRow row4 = XmlHelper.GetRow(table3, "QuarterID=" + str9 + " AND ClubID=" + str, "");
                    DataRow row5 = XmlHelper.GetRow(table3, "QuarterID=" + str9 + " AND ClubID=" + str5, "");
                    this.sbList.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td height=\"30\" bgcolor=\"#fcc6a4\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">" + MatchItem.GetQName(intNum, 5) + "&nbsp;&nbsp;&nbsp;&nbsp;");
                    this.sbList.Append("\t\t\t" + str10 + "：" + str11);
                    this.sbList.Append("\t\t</td>");
                    this.sbList.Append("\t</tr>");
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td height=\"25\"><table width=\"1002\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td colspan=\"3\" height=\"10\"></td>");
                    this.sbList.Append("\t\t\t\t</tr>");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td height=\"25\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\"><font color=\"#ad1a2c\">" + str2 + "");
                    this.sbList.Append("\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row4["Offense"]) + "&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetVDefName((int) row4["Defense"]) + "</font></td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"1\" bgcolor=\"#fcc6a4\"></td>");
                    this.sbList.Append("\t\t\t\t\t<td><font color=\"#ad1a2c\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">" + str6);
                    this.sbList.Append("\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetVOffName((int) row5["Offense"]) + "&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetVDefName((int) row5["Defense"]) + "</font></td>");
                    this.sbList.Append("\t\t\t\t</tr>");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"501\" height=\"25\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
                    this.sbList.Append("\t\t\t\t\t\t\t<tr align=\"center\">");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"5%\" height=\"25\">&nbsp;</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">球员</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">号码</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">年龄</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">身高(cm)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">体重(kg)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">体力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">综合能力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    DataView view = XmlHelper.GetView(table4, "ArrangeID=" + row4["ArrangeID"].ToString(), "");
                    for (int i = 0; i < view.Count; i++)
                    {
                        this.sbList.Append("\t\t\t\t\t<tr align=\"center\">");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition(i + 1) + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Name"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Number"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Age"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Height"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Weight"].ToString() + "</td>");
                        try
                        {
                            this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Power"].ToString() + "</td>");
                        }
                        catch
                        {
                            this.sbList.Append("\t\t\t\t\t\t\t\t<td>--</td>");
                        }
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Ability"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    }
                    this.sbList.Append("\t\t\t\t</table>");
                    this.sbList.Append("\t\t\t\t\t</td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"1\" bgcolor=\"#fcc6a4\"></td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"500\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
                    this.sbList.Append("\t\t\t\t\t\t\t<tr align=\"center\">");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"5%\" height=\"25\">&nbsp;</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">球员</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">号码</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">年龄</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">身高(cm)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">体重(kg)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">体力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"15%\">综合能力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    DataView view2 = XmlHelper.GetView(table4, "ArrangeID=" + row5["ArrangeID"].ToString(), "");
                    for (int j = 0; j < view2.Count; j++)
                    {
                        this.sbList.Append("\t\t\t\t\t<tr align=\"center\">");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td height=\"25\">" + PlayerItem.GetPlayerEngPosition(j + 1) + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Name"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Number"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Age"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Height"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Weight"].ToString() + "</td>");
                        try
                        {
                            this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Power"].ToString() + "</td>");
                        }
                        catch
                        {
                            this.sbList.Append("\t\t\t\t\t\t\t\t<td>--</td>");
                        }
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Ability"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    }
                    this.sbList.Append("\t\t\t\t</table>");
                    this.sbList.Append("\t\t\t\t\t</td>");
                    this.sbList.Append("\t\t\t\t</tr>");
                    this.sbList.Append("\t\t\t</table>");
                    this.sbList.Append("\t\t</td>");
                    this.sbList.Append("\t</tr>");
                    this.sbList.Append("</table>");
                    this.sbList.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td colspan=\"3\" height=\"10\"><table width=\"1002\">");
                    this.sbList.Append("\t\t\t</table>");
                    this.sbList.Append("\t\t</td>");
                    this.sbList.Append("\t</tr>");
                    this.sbList.Append("\t<tr align=\"center\" bgcolor=\"#FBE2D4\">");
                    this.sbList.Append("\t\t<td width=\"8%\" height=\"25\">时间</td>");
                    this.sbList.Append("\t\t<td width=\"12%\">主：客</td>");
                    this.sbList.Append("\t\t<td width=\"80%\">描 述</td>");
                    this.sbList.Append("\t</tr>");
                    DataView view3 = XmlHelper.GetView(table5, "QuarterID=" + str9, "ScriptID ASC");
                    for (int k = 0; k < view3.Count; k++)
                    {
                        this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append("\t\t<td height=\"25\" align=\"center\">" + view3[k].Row["Time"].ToString() + "</td>");
                        this.sbList.Append("\t\t<td align=\"center\">" + view3[k].Row["Score"].ToString() + "</td>");
                        this.sbList.Append("\t\t<td style=\"padding:4px;\">" + view3[k].Row["Content"].ToString() + "</td>");
                        this.sbList.Append("\t</tr>");
                    }
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td colspan=\"3\" height=\"25\" align=\"center\"></td>");
                    this.sbList.Append("\t</tr>");
                    this.sbList.Append("</table>");
                    intNum++;
                }
                catch
                {
                }
            }
        }

        private void SetStasString(Match m)
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
            XmlDataDocument document = new XmlDataDocument();
            document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/StasSchema.xsd"));
            XmlTextReader reader = new XmlTextReader(m.sbStasXml.ToString(), XmlNodeType.Element, null);
            reader.MoveToContent();
            document.Load(reader);
            DataSet dataSet = document.DataSet;
            DataTable dt = dataSet.Tables["Club"];
            DataTable table2 = dataSet.Tables["Player"];
            DataRow row2 = XmlHelper.GetRow(dt, "Type=1", "");
            string str5 = row2["ClubID"].ToString();
            string str6 = row2["ClubName"].ToString();
            string str7 = row2["Logo"].ToString();
            string str8 = row2["Score"].ToString();
            DataView view = XmlHelper.GetView(table2, "ClubID=" + str5, "Score DESC");
            DataRow row3 = XmlHelper.GetRow(dt, "Type=2", "");
            string str9 = row3["ClubID"].ToString();
            string str10 = row3["ClubName"].ToString();
            string str11 = row3["Logo"].ToString();
            string str12 = row3["Score"].ToString();
            DataView view2 = XmlHelper.GetView(table2, "ClubID=" + str9, "Score DESC");
            this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sb.Append("  <tr align=\"center\">");
            this.sb.Append("    <td width=\"10%\" height=\"70\">&nbsp;</td>");
            this.sb.Append("    <td width=\"10%\"><img src=\"" + str7 + "\" width=\"46\" height=\"46\"></td>");
            this.sb.Append("    <td width=\"25%\">" + str6 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str8 + " </td>");
            this.sb.Append("    <td width=\"10%\">VS</td>");
            this.sb.Append("    <td width=\"25%\">" + str12 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + str10 + " </td>");
            this.sb.Append("    <td width=\"10%\"><img src=\"" + str11 + "\" width=\"46\" height=\"46\"></td>");
            this.sb.Append("    <td width=\"10%\">&nbsp;</td>");
            this.sb.Append("  </tr>");
            this.sb.Append("</table>");
            this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sb.Append("  <tr>");
            this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（主队）" + str6 + "&nbsp;" + str8 + " </td>");
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
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">" + str + "</td>");
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
                this.sb.Append("\t\t<td>" + str2 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                this.sb.Append("\t\t<td>" + str3 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                this.sb.Append("\t\t<td>" + str4 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                this.sb.Append("\t  </tr>");
            }
            string str13 = StringItem.GetPercent(intA, intB, 2);
            string str14 = StringItem.GetPercent(num19, num20, 2);
            string str15 = StringItem.GetPercent(num21, num22, 2);
            this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("\t  \t<td height=\"25\">合计</td>");
            this.sb.Append("\t\t<td></td>");
            this.sb.Append("\t\t<td></td>");
            this.sb.Append("\t\t<td>" + num24 + "</td>");
            this.sb.Append("\t\t<td>" + (num25 + num26) + "</td>");
            this.sb.Append("\t\t<td>" + num27 + "</td>");
            this.sb.Append("\t\t<td>" + num28 + "</td>");
            this.sb.Append("\t\t<td>" + num29 + "</td>");
            this.sb.Append("\t\t<td>" + num23 + "</td>");
            this.sb.Append("\t\t<td>" + num30 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
            this.sb.Append("\t\t<td>" + str13 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num20, "</td>" }));
            this.sb.Append("\t\t<td>" + str14 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
            this.sb.Append("\t\t<td>" + str15 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num25, "/", num26, "</td>" }));
            this.sb.Append("\t  </tr>");
            this.sb.Append("<tr>");
            this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
            this.sb.Append("      </tr>");
            this.sb.Append("    </table></td>");
            this.sb.Append("  </tr>");
            this.sb.Append("</table>");
            this.sb.Append("<table width=\"100%\"  border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            this.sb.Append("  <tr>");
            this.sb.Append("    <td height=\"30\" bgcolor=\"#fcc6a4\" >（客队）" + str10 + "&nbsp;" + str12 + " </td>");
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
                this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                this.sb.Append("\t  \t<td height=\"25\">" + str + "</td>");
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
                this.sb.Append("\t\t<td>" + str2 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num5, "/", num6, "</td>" }));
                this.sb.Append("\t\t<td>" + str3 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num7, "/", num8, "</td>" }));
                this.sb.Append("\t\t<td>" + str4 + "</td>");
                this.sb.Append(string.Concat(new object[] { "\t\t<td>", num11, "/", num12, "</td>" }));
                this.sb.Append("\t  </tr>");
            }
            str13 = StringItem.GetPercent(intA, intB, 2);
            str14 = StringItem.GetPercent(num19, num20, 2);
            str15 = StringItem.GetPercent(num21, num22, 2);
            this.sb.Append("<tr align=\"center\" onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sb.Append("\t  \t<td height=\"25\">合计</td>");
            this.sb.Append("\t\t<td></td>");
            this.sb.Append("\t\t<td></td>");
            this.sb.Append("\t\t<td>" + num24 + "</td>");
            this.sb.Append("\t\t<td>" + (num25 + num26) + "</td>");
            this.sb.Append("\t\t<td>" + num27 + "</td>");
            this.sb.Append("\t\t<td>" + num28 + "</td>");
            this.sb.Append("\t\t<td>" + num29 + "</td>");
            this.sb.Append("\t\t<td>" + num23 + "</td>");
            this.sb.Append("\t\t<td>" + num30 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", intA, "/", intB, "</td>" }));
            this.sb.Append("\t\t<td>" + str13 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num19, "/", num20, "</td>" }));
            this.sb.Append("\t\t<td>" + str14 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num21, "/", num22, "</td>" }));
            this.sb.Append("\t\t<td>" + str15 + "</td>");
            this.sb.Append(string.Concat(new object[] { "\t\t<td>", num25, "/", num26, "</td>" }));
            this.sb.Append("\t  </tr>");
            this.sb.Append("<tr>");
            this.sb.Append("        <td colspan=\"17\" height=\"25\"><table width=\"1002\"></table></td>");
            this.sb.Append("      </tr>");
            this.sb.Append("    </table></td>");
            this.sb.Append("  </tr>");
            this.sb.Append("</table>");
        }
    }
}


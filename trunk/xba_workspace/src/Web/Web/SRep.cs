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

    public class SRep : Page
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
                path = friRowByID["RepURL"].ToString().Trim();
            }
            else
            {
                DataRow row2 = BTPCupMatchManager.GetMatchByCupIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                if (row2 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row2["RepURL"].ToString().Trim();
            }
            XmlDataDocument document = new XmlDataDocument();
            document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/RepSchema.xsd"));
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
            DataTable table2 = dataSet.Tables["Quarter"];
            DataTable table3 = dataSet.Tables["Arrange"];
            DataTable table4 = dataSet.Tables["Player"];
            DataTable table5 = dataSet.Tables["Script"];
            DataRow row3 = XmlHelper.GetRow(dt, "Type=1", "");
            string str2 = row3["ClubID"].ToString();
            string str3 = row3["ClubName"].ToString();
            string str4 = row3["Logo"].ToString();
            string str5 = row3["Score"].ToString();
            str4 = DBLogin.URLString(ServerParameter.intGameCategory) + str4;
            DataRow row4 = XmlHelper.GetRow(dt, "Type=2", "");
            string str6 = row4["ClubID"].ToString();
            string str7 = row4["ClubName"].ToString();
            string str8 = row4["Logo"].ToString();
            string str9 = row4["Score"].ToString();
            str8 = DBLogin.URLString(ServerParameter.intGameCategory) + str8;
            this.sbTitle.Append(str3 + " VS " + str7 + " (XBA街球比赛战报)");
            this.sbList.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
            this.sbList.Append("\t<tr>");
            this.sbList.Append("\t\t<td width=\"10%\" height=\"70\">&nbsp;</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\"><img src=\"" + str4 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("\t\t<td width=\"25%\" align=\"center\">" + str3 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str5 + "</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\">VS</td>");
            this.sbList.Append("\t\t<td width=\"25%\" align=\"center\">" + str9 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str7 + "</td>");
            this.sbList.Append("\t\t<td width=\"10%\" align=\"center\"><img src=\"" + str8 + "\" width=\"46\" height=\"46\"></td>");
            this.sbList.Append("\t\t<td width=\"10%\">&nbsp;</td>");
            this.sbList.Append("\t</tr>");
            this.sbList.Append("</table>");
            int intNum = 1;
            foreach (DataRow row5 in table2.Rows)
            {
                string str10 = row5["QuarterID"].ToString();
                string str11 = row5["ScoreH"].ToString();
                string str12 = row5["ScoreA"].ToString();
                try
                {
                    DataRow row6 = XmlHelper.GetRow(table3, "QuarterID=" + str10 + " AND ClubID=" + str2, "");
                    DataRow row7 = XmlHelper.GetRow(table3, "QuarterID=" + str10 + " AND ClubID=" + str6, "");
                    this.sbList.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td height=\"30\" bgcolor=\"#fcc6a4\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">" + MatchItem.GetQName(intNum, 3) + "&nbsp;&nbsp;&nbsp;&nbsp;");
                    this.sbList.Append("\t\t\t" + str11 + "：" + str12);
                    this.sbList.Append("\t\t</td>");
                    this.sbList.Append("\t</tr>");
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td height=\"25\"><table width=\"1002\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td colspan=\"3\" height=\"10\"></td>");
                    this.sbList.Append("\t\t\t\t</tr>");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td height=\"25\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\"><font color=\"#ad1a2c\">" + str3 + "");
                    this.sbList.Append("\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetSOffName((int) row6["Offense"]) + "&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetSDefName((int) row6["Defense"]) + "</font></td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"1\" bgcolor=\"#fcc6a4\"></td>");
                    this.sbList.Append("\t\t\t\t\t<td><font color=\"#ad1a2c\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">" + str7);
                    this.sbList.Append("\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetSOffName((int) row7["Offense"]) + "&nbsp;&nbsp;&nbsp;&nbsp;" + MatchItem.GetSDefName((int) row7["Defense"]) + "</font></td>");
                    this.sbList.Append("\t\t\t\t</tr>");
                    this.sbList.Append("\t\t\t\t<tr>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"501\" height=\"25\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
                    this.sbList.Append("\t\t\t\t\t\t\t<tr align=\"center\">");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"5%\" height=\"25\">&nbsp;</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">名称</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">号码</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">年龄</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"18%\">身高(cm)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"17%\">体重(kg)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">综合能力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    DataView view = XmlHelper.GetView(table4, "ArrangeID=" + row6["ArrangeID"].ToString(), "");
                    for (int i = 0; i < view.Count; i++)
                    {
                        this.sbList.Append("\t\t\t\t\t<tr align=\"center\">");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td height=\"25\">" + PlayerItem.GetSRepPosName(i + 1) + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Name"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Number"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Age"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Height"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Weight"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view[i].Row["Ability"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    }
                    this.sbList.Append("\t\t\t\t</table>");
                    this.sbList.Append("\t\t\t\t\t</td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"1\" bgcolor=\"#fcc6a4\"></td>");
                    this.sbList.Append("\t\t\t\t\t<td width=\"500\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
                    this.sbList.Append("\t\t\t\t\t\t\t<tr align=\"center\">");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"5%\" height=\"25\">&nbsp;</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">名称</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">号码</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"10%\">年龄</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"18%\">身高(cm)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"17%\">体重(kg)</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t\t<td width=\"20%\">综合能力</td>");
                    this.sbList.Append("\t\t\t\t\t\t\t</tr>");
                    DataView view2 = XmlHelper.GetView(table4, "ArrangeID=" + row7["ArrangeID"].ToString(), "");
                    for (int j = 0; j < view2.Count; j++)
                    {
                        this.sbList.Append("\t\t\t\t\t<tr align=\"center\">");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td height=\"25\">" + PlayerItem.GetSRepPosName(j + 1) + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Name"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Number"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Age"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Height"].ToString() + "</td>");
                        this.sbList.Append("\t\t\t\t\t\t\t\t<td>" + view2[j].Row["Weight"].ToString() + "</td>");
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
                    DataView view3 = XmlHelper.GetView(table5, "QuarterID=" + str10, "");
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
    }
}


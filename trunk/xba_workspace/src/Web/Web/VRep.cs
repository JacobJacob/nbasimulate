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

    public class VRep : Page
    {
        private int intClubIDA;
        private int intClubIDB;
        private int intTag;
        private int intType;
        public StringBuilder sbList;
        public StringBuilder sbTitle;
        private string strIntro = "";

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
            this.sbList = new StringBuilder();
            this.sbTitle = new StringBuilder();
            SessionItem.BeginReadReport();
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
                    path = row2["RepURL"].ToString().Trim();
                }
                else
                {
                    DataRow xGroupMatchRowByID = BTPXGroupMatchManager.GetXGroupMatchRowByID(this.intTag);
                    if (xGroupMatchRowByID == null)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=2");
                        return;
                    }
                    path = xGroupMatchRowByID["RepURL"].ToString().Trim();
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
                path = row4["RepURL"].ToString().Trim();
            }
            else if (this.intType == 6)
            {
                DataRow row5 = BTPDevCupMatchManager.GetMatchByDevCupIDClubID(this.intTag, this.intClubIDA, this.intClubIDB);
                if (row5 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row5["RepURL"].ToString().Trim();
            }
            else if (this.intType == 7)
            {
                DataRow row5 = BTPUnionCupManager.GetUnionCupMatchByID(this.intTag);
                if (row5 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row5["RepURL"].ToString().Trim();
            }
            else if (this.intType == 9)
            {
                DataRow row5 = BTPStarMatchManager.GetOneStarMatchByID(this.intTag);
                if (row5 == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = row5["RepURL"].ToString().Trim();
            }
            else
            {
                DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(this.intTag);
                if (devMRowByDevMatchID == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=2");
                    return;
                }
                path = devMRowByDevMatchID["RepURL"].ToString().Trim();
            }
            if (path == "")
            {
                base.Response.Redirect("Report.aspx?Parameter=2");
            }
            else
            {
                XmlDataDocument document = new XmlDataDocument();
                document.DataSet.ReadXmlSchema(base.Server.MapPath("MatchXML/RepSchema.xsd"));
                String temp = base.Server.MapPath(path);
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
                DataTable table6 = dataSet.Tables["Intro"];
                DataRow row7 = XmlHelper.GetRow(dt, "Type=1", "");
                string str2 = row7["ClubID"].ToString();
                string str3 = row7["ClubName"].ToString();
                string str4 = row7["Logo"].ToString();
                string str5 = row7["Score"].ToString();
                int num = 0;
                try
                {
                    num = (int) row7["AllAdd"];
                }
                catch
                {
                    num = 0;
                }
                str4 = Config.GetDomain() + str4;
                DataRow row8 = XmlHelper.GetRow(dt, "Type=2", "");
                string str6 = row8["ClubID"].ToString();
                string str7 = row8["ClubName"].ToString();
                string str8 = row8["Logo"].ToString();
                string str9 = row8["Score"].ToString();
                int num2 = 0;
                try
                {
                    num2 = (int) row8["AllAdd"];
                }
                catch
                {
                    num2 = 0;
                }
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDA);
                BTPAccountManager.GetAccountRowByClubID(this.intClubIDB);
                str8 = Config.GetDomain() + str8;
                if (table6.Rows.Count > 0)
                {
                    DataRow row9 = XmlHelper.GetRow(table6, "", "");
                    try
                    {
                        this.strIntro = row9["Intro"].ToString();
                    }
                    catch
                    {
                        this.strIntro = "";
                    }
                }
                this.sbTitle.Append(str3 + " VS " + str7 + " (XBA职业比赛战报)");
                this.sbList.Append("<table width=\"1002\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">");
                this.sbList.Append("\t<tr>");
                this.sbList.Append("\t\t<td width=\"100\" height=\"70\">&nbsp;</td>");
                this.sbList.Append("\t\t<td width=\"100\" align=\"center\"><img src=\"" + str4 + "\" width=\"46\" height=\"46\"></td>");
                this.sbList.Append("\t\t<td width=\"250\" align=\"center\">" + str3 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str5 + "</td>");
                this.sbList.Append("\t\t<td width=\"100\" align=\"center\">VS</td>");
                this.sbList.Append("\t\t<td width=\"250\" align=\"center\">" + str9 + "&nbsp;&nbsp;&nbsp;&nbsp;" + str7 + "</td>");
                this.sbList.Append("\t\t<td width=\"100\" align=\"center\"><img src=\"" + str8 + "\" width=\"46\" height=\"46\"></td>");
                this.sbList.Append("\t\t<td width=\"100\">&nbsp;</td>");
                this.sbList.Append("\t</tr>");
                if ((num > 0) || (num2 > 0))
                {
                    this.sbList.Append("\t<tr>");
                    this.sbList.Append("\t\t<td width=\"100\" >&nbsp;</td>");
                    this.sbList.Append("\t\t<td width=\"100\" align=\"center\" ></td>");
                    this.sbList.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num + "%</font></td>");
                    this.sbList.Append("\t\t<td width=\"100\" align=\"center\"></td>");
                    this.sbList.Append("\t\t<td width=\"250\" align=\"center\"><font color=\"#999999\" style=\"font-size:12px\">球队状态较平时提升" + num2 + "%</font></td>");
                    this.sbList.Append("\t\t<td width=\"100\" align=\"center\">");
                    this.sbList.Append("\t\t<td width=\"100\">&nbsp;</td>");
                    this.sbList.Append("\t</tr>");
                }
                this.sbList.Append("</table>");
                int intNum = 1;
                foreach (DataRow row12 in table2.Rows)
                {
                    string str12 = row12["QuarterID"].ToString();
                    string str10 = row12["ScoreH"].ToString();
                    string str11 = row12["ScoreA"].ToString();
                    try
                    {
                        DataRow row10 = XmlHelper.GetRow(table3, "QuarterID=" + str12 + " AND ClubID=" + str2, "");
                        DataRow row11 = XmlHelper.GetRow(table3, "QuarterID=" + str12 + " AND ClubID=" + str6, "");
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
                        this.sbList.Append("\t\t\t\t\t<td height=\"25\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\"><font color=\"#ad1a2c\">" + str3 + "");
                        this.sbList.Append(string.Concat(new object[] { "\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;", MatchItem.GetVOffName((int) row10["Offense"]), "&nbsp;&nbsp;&nbsp;&nbsp;", MatchItem.GetVDefName((int) row10["Defense"]), "&nbsp;&nbsp;&nbsp;&nbsp;阵容状态：", ((int) row10["OffHard"]) - 10, "%</font></td>" }));
                        this.sbList.Append("\t\t\t\t\t<td width=\"1\" bgcolor=\"#fcc6a4\"></td>");
                        this.sbList.Append("\t\t\t\t\t<td><font color=\"#ad1a2c\" style=\"PADDING-RIGHT:4px;PADDING-LEFT:4px;PADDING-BOTTOM:4px;PADDING-TOP:4px\">" + str7);
                        this.sbList.Append(string.Concat(new object[] { "\t\t\t\t\t\t&nbsp;&nbsp;&nbsp;&nbsp;", MatchItem.GetVOffName((int) row11["Offense"]), "&nbsp;&nbsp;&nbsp;&nbsp;", MatchItem.GetVDefName((int) row11["Defense"]), "&nbsp;&nbsp;&nbsp;&nbsp;阵容状态：", ((int) row11["OffHard"]) - 10, "%</font></td>" }));
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
                        DataView view = XmlHelper.GetView(table4, "ArrangeID=" + row10["ArrangeID"].ToString().Trim(), "");
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
                        DataView view2 = XmlHelper.GetView(table4, "ArrangeID=" + row11["ArrangeID"].ToString(), "");
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
                        DataView view3 = XmlHelper.GetView(table5, "QuarterID=" + str12, "ScriptID ASC");
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
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                if (this.strIntro != "")
                {
                    this.sbList.Append("<p align=\"center\">比赛总结:" + this.strIntro.Trim() + "<br><br><br></p>");
                }
            }
        }
    }
}


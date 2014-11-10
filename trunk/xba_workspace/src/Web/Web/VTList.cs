namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class VTList : Page
    {
        private int intCategory;
        private int intClubID5;
        private int intUserID;
        private string strGuideCode;
        public string strList;
        public string strScriptR;

        [AjaxMethod]
        public string GetDevOpration()
        {
            int num = (int) BTPAccountManager.GetAccountRowByUserID(SessionItem.CheckLogin(0))["ClubID5"];
            int intRound = (int) BTPGameManager.GetGameRow()["Turn"];
            DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(num, intRound);
            if (devMRowByClubIDRound == null)
            {
                return "";
            }
            int num1 = (int) devMRowByClubIDRound["DevMatchID"];
            int num3 = (int) devMRowByClubIDRound["ClubHID"];
            int num5 = (int) devMRowByClubIDRound["ClubAID"];
            bool flag1 = (bool) devMRowByClubIDRound["UseStaffH"];
            bool flag2 = (bool) devMRowByClubIDRound["UseStaffA"];
            byte num6 = (byte) devMRowByClubIDRound["MangerSayH"];
            byte num7 = (byte) devMRowByClubIDRound["MangerSayA"];
            bool flag3 = (bool) devMRowByClubIDRound["AddArrangeLvlH"];
            bool flag4 = (bool) devMRowByClubIDRound["AddArrangeLvlA"];
            string str = "";
            int request = SessionItem.GetRequest("Ref", 0);
            if (request == 1)
            {
                request = 0;
            }
            else
            {
                request = 1;
            }
            return ("<span style='margin:30px'>" + str + "</span>");
        }

        private string GetVTList()
        {
            string str;
            int num;
            DataRow gameRow = BTPGameManager.GetGameRow();
            int intRound = (int) gameRow["Turn"];
            int num1 = (int) gameRow["Days"];
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
                str = "";
            }
            else
            {
                str = "";
            }
            TagReader reader = new TagReader(line);
            int intScore = 0;
            int num7 = 0;
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
                    num7 = 0;
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
                    num7 = Convert.ToInt32(reader.GetTagline("ScoreA"));
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
                num = intRound - 1;
            }
            else
            {
                num = intRound;
            }
            if ((BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, num) == null) || !flag)
            {
                builder.Append("<table width='552'  border='0' cellspacing='0' cellpadding='0'>");
                builder.Append("<tr class='BarHead'>");
                builder.Append("<td height='25' width='284' align='right'>已注册球队</td>");
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
                    string clubLogo;
                    string str25;
                    string str26;
                    int num8;
                    builder.Append("<tr><td width='284'><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
                    DataTable table = BTPDevManager.GetClubTableByDevCodeTop7(devCodeByClubID);
                    if (table == null)
                    {
                        builder.Append("<tr><td></td></tr>");
                    }
                    else
                    {
                        foreach (DataRow row3 in table.Rows)
                        {
                            num8 = (int) row3["ClubID"];
                            if (num8 != 0)
                            {
                                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(num8);
                                clubLogo = BTPClubManager.GetClubLogo(num8);
                                str25 = clubRowByClubID["ClubName"].ToString().Trim();
                                int num12 = (int) clubRowByClubID["UserID"];
                                str26 = clubRowByClubID["NickName"].ToString().Trim();
                                byte num13 = (byte) clubRowByClubID["Levels"];
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(clubLogo);
                                builder.Append("'></td>");
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
                    DataTable table2 = BTPDevManager.GetClubTableByDevCodeEnd7(devCodeByClubID);
                    if (table2 == null)
                    {
                        builder.Append("<tr><td></td></tr>");
                    }
                    else
                    {
                        foreach (DataRow row5 in table2.Rows)
                        {
                            num8 = (int) row5["ClubID"];
                            if (num8 != 0)
                            {
                                DataRow row6 = BTPClubManager.GetClubRowByClubID(num8);
                                clubLogo = row6["ClubLogo"].ToString().Trim();
                                str25 = row6["ClubName"].ToString().Trim();
                                int num14 = (int) row6["UserID"];
                                str26 = row6["NickName"].ToString().Trim();
                                byte num15 = (byte) row6["Levels"];
                                string str27 = BTPClubManager.GetClubLogo(num8);
                                builder.Append("<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\">");
                                builder.Append("<td width='60' height='50' align='center'><img src='");
                                builder.Append(str27);
                                builder.Append("'></td>");
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
                    int num9 = (int) devMRowByClubIDRound["DevMatchID"];
                    int num10 = (int) devMRowByClubIDRound["ClubHID"];
                    int num16 = (int) devMRowByClubIDRound["ClubAID"];
                    bool flag2 = (bool) devMRowByClubIDRound["UseStaffH"];
                    bool flag3 = (bool) devMRowByClubIDRound["UseStaffA"];
                    byte num17 = (byte) devMRowByClubIDRound["MangerSayH"];
                    byte num18 = (byte) devMRowByClubIDRound["MangerSayA"];
                    bool flag1 = (bool) devMRowByClubIDRound["AddArrangeLvlH"];
                    bool flag4 = (bool) devMRowByClubIDRound["AddArrangeLvlA"];
                    string str28 = "";
                    int request = SessionItem.GetRequest("Ref", 0);
                    if (request == 1)
                    {
                        request = 0;
                    }
                    else
                    {
                        request = 1;
                    }
                    if (num10 == this.intClubID5)
                    {
                        if (flag2)
                        {
                            string text1 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            string text2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                        }
                    }
                    else if (flag3)
                    {
                        string text3 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                    }
                    else
                    {
                        string text4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                    }
                    builder2.Append("\t<tr class='BarHead' height='25'>");
                    builder2.Append("\t\t<td><strong>本轮比赛对手</strong></td>");
                    builder2.Append("\t</tr>");
                    builder2.Append("\t<tr>");
                    builder2.Append("\t\t<td height='150' align='center'>");
                    if ((tagline == "") || (str19 == ""))
                    {
                        builder2.Append("按照赛程，比赛轮空<br/>");
                    }
                    else
                    {
                        builder2.Append("\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td height='50'>");
                        builder2.Append("\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                        builder2.Append("\t\t\t\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px;'><font style='line-height:140%'><font color='#660066'><strong>" + tagline + "</strong></font><br><font color='#666666'>" + str22 + "</font></font></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='10%'><img src='" + str20 + "' border='0' width='46' height='46'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='30%' align='center'><img src='Images/Score/bank.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/bank.gif' width='19' height='28' border='0'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='" + str21 + "' border='0' width='46' height='46'></td>");
                        builder2.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str19 + "</strong></font><br><font color='#666666'>" + str23 + "</font></font></td>");
                        builder2.Append("\t\t\t\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t\t\t</table>");
                        builder2.Append("\t\t\t\t\t</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25' align='center'>职业联赛每日早9:00进行比赛，您可以在比赛时段观看文字现场直播</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>比赛球场</strong>：" + tagline + "主场</td>");
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append(string.Concat(new object[] { "\t\t\t\t\t<td style='padding-left:5px;' height='25'><a href='Main_P.aspx?Tag=", this.intUserID, "&Type=DEVISION' target='_parent'>查看联赛</a>&nbsp;|&nbsp;<a href='Main_P.aspx?Tag=", this.intUserID, "&Type=VARRANGE' target='_parent'>战术安排</a><span style='margin-left:80px' id='DevOpration'>", str28, "</span></td>" }));
                        builder2.Append("\t\t\t\t</tr>");
                        builder2.Append("\t\t\t\t<tr>");
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='20' align='right'><font color=''>特别提醒： 14天不登录将视为自动放弃俱乐部！</font><span style='width:60px'></span><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str + "</td>");
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
                    builder2.Append("\t\t<td height='150' align='center'>赛季结束<br/>");
                    builder2.Append("</td>");
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
                builder.Append("\t\t<td height='120' align='center'>");
                if (intRound == 1)
                {
                    builder.Append("无比赛<br/>");
                }
                else if ((str4 == "") || (str5 == ""))
                {
                    builder.Append("按照赛程，比赛轮空<br/>");
                }
                else
                {
                    DataRow row8 = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound - 1);
                    string str29 = Config.GetDomain() + "VRep.aspx?Tag=" + row8["DevMatchID"].ToString() + "&Type=2&A=" + row8["ClubHID"].ToString() + "&B=" + row8["ClubAID"].ToString();
                    string str30 = Config.GetDomain() + "VStas.aspx?Tag=" + row8["DevMatchID"].ToString() + "&Type=2&A=" + row8["ClubHID"].ToString() + "&B=" + row8["ClubAID"].ToString();
                    int num19 = (int) row8["DevMatchID"];
                    int num20 = (int) row8["MsgCount"];
                    builder.Append("\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td height='50'>");
                    builder.Append("\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str4 + "</strong></font><br><font color='#666666'>" + str8 + "</font></font></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='10%'><img src='" + str6 + "' border='0' width='46' height='46'></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='30%' align='center'>" + MatchItem.GetScore(intScore) + "<img src='Images/Score/00.gif' width='19' height='28' border='0'>" + MatchItem.GetScore(num7) + "</td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='" + str7 + "' border='0' width='46' height='46'></td>");
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
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><a href='" + str30 + "' target='_blank'>比赛统计</a>&nbsp;|&nbsp;<a href='" + str29 + "' target='_blank'>比赛战报</a><span style='text-align:right;margin-left:320px'><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str + "</span></td>");
                    builder.Append("\t\t\t\t</tr>");
                    builder.Append("\t\t\t</table>");
                }
                builder.Append("\t\t</td>");
                builder.Append("\t</tr>");
                builder.Append(builder2.ToString());
                builder.Append("</table>");
            }
            builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\">");
            builder.Append("<tr>");
            builder.Append("\t<td id=\"XBACup\" align=\"center\" ></td>");
            builder.Append("\t</tr>");
            builder.Append("</table> ");
            return builder.ToString();
        }

        [AjaxMethod]
        public string GetXBATip()
        {
            StringBuilder builder = new StringBuilder("");
            DataRow parameterRow = BTPParameterManager.GetParameterRow();
            if ((parameterRow != null) && !((bool) parameterRow["CanXBAGame"]))
            {
                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                builder.Append("<tr bgColor=\"#fde3d4\">");
                builder.Append("\t<td align=\"center\" >本赛季暂不开放冠军杯赛，敬请在下个赛季关注！</td>");
                builder.Append("</table> ");
                return builder.ToString();
            }
            int intUserID = SessionItem.CheckLogin(0);
            int intClubID = (int) BTPAccountManager.GetAccountRowByUserID(intUserID)["ClubID5"];
            int num3 = (int) BTPGameManager.GetGameRow()["Turn"];
            if ((num3 < 3) || ((num3 == 3) && (DateTime.Now.Hour < 0x10)))
            {
                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                builder.Append("<tr bgColor=\"#fde3d4\">");
                switch (num3)
                {
                    case 1:
                        builder.Append("\t<td align=\"center\" >冠军杯报名第一天！请持有冠军杯邀请函的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
                        goto Label_0D82;

                    case 2:
                        builder.Append("\t<td align=\"center\" >冠军杯报名第二天！请持有冠军杯邀请函及冠军杯邀请函（盟）的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
                        goto Label_0D82;
                }
                builder.Append("\t<td align=\"center\" >冠军杯赛报名截止至16：00！请欲参赛的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
            }
            else
            {
                DataRow lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(1);
                int num4 = (byte) lastGameRowByCategory["Status"];
                int num5 = (byte) lastGameRowByCategory["Round"];
                DateTime time = (DateTime) lastGameRowByCategory["MatchTime"];
                bool flag = false;
                if ((DateTime.Now.Day == time.Day) && (num4 == 3))
                {
                    flag = true;
                }
                if (((DateTime.Now.Hour == 0x10) && (num3 > 2)) && !flag)
                {
                    lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                    string str3 = "";
                    if (lastGameRowByCategory != null)
                    {
                        num4 = (byte) lastGameRowByCategory["Status"];
                        num5 = (byte) lastGameRowByCategory["Round"];
                        int num1 = (int) lastGameRowByCategory["Capacity"];
                        string str4 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                        if (((num4 > 0) && (str4 != "")) && (str4.ToLower().IndexOf("http://match") == -1))
                        {
                            str4 = Config.GetDomain() + str4;
                        }
                        if (num4 == 3)
                        {
                            string str5 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                            int num6 = (int) lastGameRowByCategory["ChampionUserID"];
                            str3 = string.Concat(new object[] { "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num6, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str5, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str4, "')>请点击此处</a>" });
                        }
                        else
                        {
                            str3 = "冠军杯比赛进行中，请在17点以后查看战况";
                        }
                    }
                    builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td align=\"center\" >" + str3 + "</td>");
                    builder.Append("\t</tr>");
                    builder.Append("</table> ");
                }
                else
                {
                    object obj2;
                    if (BTPXGroupTeamManager.GetGroupTeamRowByCID(intClubID) != null)
                    {
                        DataTable championCupUPDown = BTPXGroupTeamManager.GetChampionCupUPDown(intClubID);
                        if ((championCupUPDown != null) && (championCupUPDown.Rows.Count > 1))
                        {
                            string str2;
                            DataRow row3 = championCupUPDown.Rows[1];
                            int num12 = (int) row3["ClubAID"];
                            int num13 = (int) row3["ClubBID"];
                            byte num27 = (byte) row3["Type"];
                            if (num12 == -1)
                            {
                                str2 = "您已被淘汰，无缘下轮比赛";
                            }
                            else
                            {
                                string str9;
                                if (num12 == intClubID)
                                {
                                    str9 = row3["NameB"].ToString().Trim();
                                }
                                else
                                {
                                    str9 = row3["NameA"].ToString().Trim();
                                }
                                str2 = "下轮对手：" + str9;
                            }
                            row3 = championCupUPDown.Rows[0];
                            int num14 = (int) row3["MatchID"];
                            num12 = (int) row3["ClubAID"];
                            num13 = (int) row3["ClubBID"];
                            int num28 = (int) row3["UserIDA"];
                            int num29 = (int) row3["UserIDB"];
                            int num15 = (int) row3["ClubAScore"];
                            int num16 = (int) row3["ClubBScore"];
                            string str10 = row3["NameA"].ToString().Trim();
                            string str11 = row3["NameB"].ToString().Trim();
                            int num17 = (byte) row3["Type"];
                            if (flag)
                            {
                                str2 = "休息一轮";
                            }
                            if ((num12 > 0) && (num13 > 0))
                            {
                                string str12 = string.Concat(new object[] { 
                                    "<a href='", Config.GetDomain(), "VRep.aspx?Tag=", num14, "&Type=", num17, "&A=", num12, "&B=", num13, "' target='_blank'>战报</a>|<a href='", Config.GetDomain(), "VStas.aspx?Tag=", num14, "&Type=", num17, 
                                    "&A=", num12, "&B=", num13, "' target='_blank'>统计</a>"
                                 });
                                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                                builder.Append("<tr bgColor=\"#fde3d4\">");
                                builder.Append("\t<td width='80' align=\"center\"><strong>【冠军杯】</strong></td>");
                                builder.Append(string.Concat(new object[] { "\t<td width='350'  align=\"left\">本轮对手：", str10, " ", num15, "：", num16, " ", str11, "</td>" }));
                                builder.Append("\t<td align=\"left\">" + str12 + "</td>");
                                builder.Append("\t</tr>");
                                builder.Append("<tr bgColor=\"#fde3d4\">");
                                builder.Append("\t<td align=\"center\"></td>");
                                builder.Append("\t<td width='350' align=\"left\">" + str2 + "</td>");
                                builder.Append("\t<td align=\"left\"><a target='Main' href=\"Main_P.aspx?Tag=" + intUserID + "&Type=XBACUP\">查看详细</a></td>");
                                builder.Append("\t</tr>");
                                builder.Append("</table> ");
                            }
                            else if ((num12 == 0) && (num13 == 0))
                            {
                                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                                builder.Append("<tr bgColor=\"#fde3d4\">");
                                builder.Append("\t<td align=\"center\"></td>");
                                builder.Append("\t<td width='350' align=\"left\">" + str2 + "</td>");
                                builder.Append("\t<td align=\"left\"><a target='Main' href=\"Main_P.aspx?Tag=" + intUserID + "&Type=XBACUP\">查看详细</a></td>");
                                builder.Append("\t</tr>");
                                builder.Append("</table> ");
                            }
                            else
                            {
                                DataRow xCupRegRowByClubID = BTPXCupRegManager.GetXCupRegRowByClubID(intClubID);
                                int num18 = 0;
                                if (xCupRegRowByClubID != null)
                                {
                                    num18 = (byte) xCupRegRowByClubID["DeadRound"];
                                }
                                string str = "";
                                lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                                if (lastGameRowByCategory != null)
                                {
                                    num4 = (byte) lastGameRowByCategory["Status"];
                                    num5 = (byte) lastGameRowByCategory["Round"];
                                    int num19 = (int) lastGameRowByCategory["Capacity"];
                                    int num20 = 1;
                                    string str13 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                                    if (((num4 > 0) && (str13 != "")) && (str13.ToLower().IndexOf("http://match") == -1))
                                    {
                                        str13 = Config.GetDomain() + str13;
                                    }
                                    if (num4 == 1)
                                    {
                                        if (num18 == 0)
                                        {
                                            str = "您未进入淘汰赛。";
                                        }
                                        else
                                        {
                                            str = "您在第" + num18 + "轮淘汰赛中被淘汰出局。";
                                        }
                                        while (num19 > 1)
                                        {
                                            num19 /= 2;
                                            num20++;
                                        }
                                        int num21 = num20 - num5;
                                        if (num5 == 1)
                                        {
                                            str = "现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str13 + "')>淘汰赛</a>";
                                        }
                                        else if (num21 > 5)
                                        {
                                            obj2 = str;
                                            str = string.Concat(new object[] { obj2, "冠军杯淘汰赛进行到第", num5, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str13, "')>请点击此处</a>" });
                                        }
                                        else if (num21 > 2)
                                        {
                                            string str14 = str;
                                            str = str14 + "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num21).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                        }
                                        else
                                        {
                                            switch (num21)
                                            {
                                                case 1:
                                                    str = str + "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                                    break;

                                                case 2:
                                                    str = str + "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string str15 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                                        int num22 = (int) lastGameRowByCategory["ChampionUserID"];
                                        str = string.Concat(new object[] { "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num22, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str15, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str13, "')>请点击此处</a>" });
                                    }
                                }
                                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                                builder.Append("<tr bgColor=\"#fde3d4\">");
                                builder.Append("\t<td align=\"center\" bgColor=\"#fcc6a4\" >" + str + "</td>");
                                builder.Append("\t</tr>");
                                builder.Append("</table> ");
                            }
                        }
                    }
                    else
                    {
                        string str6 = "";
                        if (flag)
                        {
                            str6 = "冠军杯小组赛结束，淘汰赛即将展开，敬请关注";
                        }
                        else if (num4 == 1)
                        {
                            str6 = "冠军杯小组赛进行至第" + num5 + "轮";
                        }
                        else
                        {
                            lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                            if (lastGameRowByCategory != null)
                            {
                                num4 = (byte) lastGameRowByCategory["Status"];
                                num5 = (byte) lastGameRowByCategory["Round"];
                                int num7 = (int) lastGameRowByCategory["Capacity"];
                                int num8 = 1;
                                string str7 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                                DateTime time1 = (DateTime) lastGameRowByCategory["MatchTime"];
                                string str8 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                                int num9 = (int) lastGameRowByCategory["ChampionUserID"];
                                lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(1);
                                int num10 = (byte) lastGameRowByCategory["Category"];
                                DateTime time5 = (DateTime) lastGameRowByCategory["MatchTime"];
                                if ((num10 == 3) && (num5 == 1))
                                {
                                    if (((num4 > 0) && (str7 != "")) && (str7.ToLower().IndexOf("http://match") == -1))
                                    {
                                        str7 = Config.GetDomain() + str7;
                                    }
                                    str6 = "冠军杯小组赛结束，当前正停赛一轮安排淘汰赛的赛程，查看赛程<a href='javascript:;' onclick=window.open('" + str7 + "')>请点击此处</a>";
                                }
                                else if (((num4 > 0) && (str7 != "")) && (str7.ToLower().IndexOf("http://match") == -1))
                                {
                                    str7 = Config.GetDomain() + str7;
                                }
                                if (num4 == 1)
                                {
                                    while (num7 > 1)
                                    {
                                        num7 /= 2;
                                        num8++;
                                    }
                                    int num11 = num8 - num5;
                                    if (num5 == 1)
                                    {
                                        str6 = "现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str7 + "')>淘汰赛</a>";
                                    }
                                    else if (num11 > 5)
                                    {
                                        str6 = string.Concat(new object[] { "冠军杯淘汰赛进行到第", num5, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str7, "')>请点击此处</a>" });
                                    }
                                    else if (num11 > 2)
                                    {
                                        str6 = "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num11).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str7 + "')>请点击此处</a>";
                                    }
                                    else
                                    {
                                        switch (num11)
                                        {
                                            case 1:
                                                str6 = "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str7 + "')>请点击此处</a>";
                                                break;

                                            case 2:
                                                str6 = "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str7 + "')>请点击此处</a>";
                                                break;
                                        }
                                    }
                                }
                                else if (num9 != intUserID)
                                {
                                    obj2 = str6;
                                    str6 = string.Concat(new object[] { obj2, "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num9, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str8, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str7, "')>请点击此处</a>" });
                                }
                                else
                                {
                                    str6 = str6 + "本赛季冠军杯正式结束，恭喜您获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str7 + "')>请点击此处</a>";
                                }
                            }
                        }
                        builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#f8ece6; margin-top:6px;\">");
                        builder.Append("<tr bgColor=\"#fde3d4\">");
                        builder.Append("\t<td align=\"center\" >" + str6 + "</td>");
                        builder.Append("\t</tr>");
                        builder.Append("</table> ");
                    }
                }
                return builder.ToString().Trim();
            }
        Label_0D82:
            builder.Append("\t</tr>");
            builder.Append("</table> ");
            return builder.ToString().Trim();
        }

        private void InitializeComponent()
        {
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
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.strGuideCode = onlineRowByUserID["GuideCode"].ToString().Trim();
                SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                if (((this.intCategory != 1) && (this.intCategory != 2)) && (this.intCategory != 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    this.strScriptR = this.strScriptR + "<script type=\"text/javascript\"> function copytoZoneBoard() \n{\nvar clipBoardContent=\"\";\nclipBoardContent=\"http://union.xba.com.cn/zone/" + this.intUserID.ToString() + ".aspx\";\nwindow.clipboardData.setData(\"Text\",clipBoardContent);\nalert(\"复制成功，已经复制到剪贴板，您可以打开QQ或者MSN的对话框，然后使用（Ctrl+V或鼠标右键）粘贴功能给您的好友^_^\");\n}\n</script>\n";
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(VTList));
            this.strList = this.GetVTList();
        }
    }
}


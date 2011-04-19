namespace Web
{
    using AjaxPro;
    using LoginParameter;
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
            int num3 = (int) devMRowByClubIDRound["DevMatchID"];
            int num4 = (int) devMRowByClubIDRound["ClubHID"];
            int num1 = (int) devMRowByClubIDRound["ClubAID"];
            bool flag1 = (bool) devMRowByClubIDRound["UseStaffH"];
            bool flag2 = (bool) devMRowByClubIDRound["UseStaffA"];
            byte num5 = (byte) devMRowByClubIDRound["MangerSayH"];
            byte num6 = (byte) devMRowByClubIDRound["MangerSayA"];
            bool flag3 = (bool) devMRowByClubIDRound["AddArrangeLvlH"];
            bool flag4 = (bool) devMRowByClubIDRound["AddArrangeLvlA"];
            string str = "";
            int request = (int) SessionItem.GetRequest("Ref", 0);
            if (request == 1)
            {
                request = 0;
            }
            else
            {
                request = 1;
            }
            if (num4 == num)
            {
                if (num5 > 0)
                {
                    str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num5, "&Pos=3&Type=MANGERSAY&DevMatchID=", num3, "'><img align='absmiddle' alt='球队状态较平时提升", num5, "%' src='", SessionItem.GetImageURL(), "louder", num5, ".gif' border='0' width='14' height='16'> 球队状态较平时提升", num5, "%</a>" });
                }
                else
                {
                    str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num5, "&Pos=3&Type=MANGERSAY&DevMatchID=", num3, "'><img align='absmiddle' alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'> 给球员发奖金</a>" });
                }
            }
            else if (num6 > 0)
            {
                str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num6, "&Pos=3&Type=MANGERSAY&DevMatchID=", num3, "'><img align='absmiddle' alt='球队状态较平时提升", num6, "%' src='", SessionItem.GetImageURL(), "louder", num6, ".gif' border='0' width='14' height='16'> 球队状态较平时提升", num6, "%</a>" });
            }
            else
            {
                str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num6, "&Pos=3&Type=MANGERSAY&DevMatchID=", num3, "'><img align='absmiddle' alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'> 给球员发奖金</a>" });
            }
            return ("<span style='margin:30px'>" + str + "</span>");
        }

        private string GetVTList()
        {
            string str3;
            int num7;
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
                str3 = "";
            }
            else
            {
                str3 = "";
            }
            TagReader reader = new TagReader(line);
            int intScore = 0;
            int num6 = 0;
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
                    num6 = 0;
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
                    num6 = Convert.ToInt32(reader.GetTagline("ScoreA"));
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
                num7 = intRound - 1;
            }
            else
            {
                num7 = intRound;
            }
            if ((BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, num7) == null) || !flag)
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
                    DataTable table2 = BTPDevManager.GetClubTableByDevCodeTop7(devCodeByClubID);
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
                                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(num8);
                                clubLogo = BTPClubManager.GetClubLogo(num8);
                                str25 = clubRowByClubID["ClubName"].ToString().Trim();
                                int num14 = (int) clubRowByClubID["UserID"];
                                str26 = clubRowByClubID["NickName"].ToString().Trim();
                                byte num15 = (byte) clubRowByClubID["Levels"];
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
                    DataTable table3 = BTPDevManager.GetClubTableByDevCodeEnd7(devCodeByClubID);
                    if (table3 == null)
                    {
                        builder.Append("<tr><td></td></tr>");
                    }
                    else
                    {
                        foreach (DataRow row7 in table3.Rows)
                        {
                            num8 = (int) row7["ClubID"];
                            if (num8 != 0)
                            {
                                DataRow row8 = BTPClubManager.GetClubRowByClubID(num8);
                                clubLogo = row8["ClubLogo"].ToString().Trim();
                                str25 = row8["ClubName"].ToString().Trim();
                                int num16 = (int) row8["UserID"];
                                str26 = row8["NickName"].ToString().Trim();
                                byte num17 = (byte) row8["Levels"];
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
                    int num18 = (int) devMRowByClubIDRound["ClubAID"];
                    bool flag3 = (bool) devMRowByClubIDRound["UseStaffH"];
                    bool flag4 = (bool) devMRowByClubIDRound["UseStaffA"];
                    byte num11 = (byte) devMRowByClubIDRound["MangerSayH"];
                    byte num12 = (byte) devMRowByClubIDRound["MangerSayA"];
                    bool flag1 = (bool) devMRowByClubIDRound["AddArrangeLvlH"];
                    bool flag5 = (bool) devMRowByClubIDRound["AddArrangeLvlA"];
                    string str28 = "";
                    int request = (int) SessionItem.GetRequest("Ref", 0);
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
                        if (num11 > 0)
                        {
                            str28 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num11, "&Pos=3&Type=MANGERSAY&DevMatchID=", num9, "'><img alt='球员状态提升", num11, "%' src='", SessionItem.GetImageURL(), "louder", num11, ".gif' border='0' width='14' height='16'> 球员状态提升", num11, "%</a>" });
                        }
                        else
                        {
                            str28 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num11, "&Pos=3&Type=MANGERSAY&DevMatchID=", num9, "'><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'> 给球员发奖金</a>" });
                        }
                        if (flag3)
                        {
                            string text1 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            string text2 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                        }
                    }
                    else
                    {
                        if (num12 > 0)
                        {
                            str28 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num12, "&Pos=3&Type=MANGERSAY&DevMatchID=", num9, "'><img alt='球员状态提升", num12, "%' src='", SessionItem.GetImageURL(), "louder", num12, ".gif' border='0' width='14' height='16'> 球员状态提升", num12, "%</a>" });
                        }
                        else
                        {
                            str28 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", num12, "&Pos=3&Type=MANGERSAY&DevMatchID=", num9, "'><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'> 给球员发奖金</a>" });
                        }
                        if (flag4)
                        {
                            string text3 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            string text4 = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Ref=", request, "&Pos=1&Type=USESTAFF&DevMatchID=", num9, "'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                        }
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
                        builder2.Append("\t\t\t\t\t<td style='padding-left:5px;' height='20' align='right'><font color=''>特别提醒： 14天不登录将视为自动放弃俱乐部！</font><span style='width:60px'></span><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str3 + "</td>");
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
                    DataRow row10 = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound - 1);
                    string str29 = DBLogin.GetMatchURL() + "VRep.aspx?Tag=" + row10["DevMatchID"].ToString() + "&Type=2&A=" + row10["ClubHID"].ToString() + "&B=" + row10["ClubAID"].ToString();
                    string str30 = DBLogin.GetMatchURL() + "VStas.aspx?Tag=" + row10["DevMatchID"].ToString() + "&Type=2&A=" + row10["ClubHID"].ToString() + "&B=" + row10["ClubAID"].ToString();
                    int num19 = (int) row10["DevMatchID"];
                    int num20 = (int) row10["MsgCount"];
                    builder.Append("\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t<td height='50'>");
                    builder.Append("\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                    builder.Append("\t\t\t\t\t\t\t<tr>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='25%' align='center' valign='top' style='Padding-Top:10px'><font style='line-height:140%'><font color='#660066'><strong>" + str4 + "</strong></font><br><font color='#666666'>" + str8 + "</font></font></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='10%'><img src='" + str6 + "' border='0' width='46' height='46'></td>");
                    builder.Append("\t\t\t\t\t\t\t\t<td width='30%' align='center'>" + MatchItem.GetScore(intScore) + "<img src='Images/Score/00.gif' width='19' height='28' border='0'>" + MatchItem.GetScore(num6) + "</td>");
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
                    builder.Append("\t\t\t\t\t<td style='padding-left:5px;' height='25'><a href='" + str30 + "' target='_blank'>比赛统计</a>&nbsp;|&nbsp;<a href='" + str29 + "' target='_blank'>比赛战报</a><span style='text-align:right;margin-left:320px'><a href='DevMessage.aspx?Type=ADD'>我要留言</a>&nbsp;|&nbsp;<a href='DevMessage.aspx?Type=VIEW&Page=1'>联赛留言</a>" + str3 + "</span></td>");
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
            string str9;
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
            if ((num3 >= 3) && ((num3 != 3) || (DateTime.Now.Hour >= 0x10)))
            {
                string str7;
                object obj2;
                DataRow lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(1);
                int num4 = (byte) lastGameRowByCategory["Status"];
                int num5 = (byte) lastGameRowByCategory["Round"];
                DateTime time = (DateTime) lastGameRowByCategory["MatchTime"];
                bool flag2 = false;
                if ((DateTime.Now.Day == time.Day) && (num4 == 3))
                {
                    flag2 = true;
                }
                if (((DateTime.Now.Hour == 0x10) && (num3 > 2)) && !flag2)
                {
                    lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                    string str = "";
                    if (lastGameRowByCategory != null)
                    {
                        num4 = (byte) lastGameRowByCategory["Status"];
                        num5 = (byte) lastGameRowByCategory["Round"];
                        int num1 = (int) lastGameRowByCategory["Capacity"];
                        string str2 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                        if (((num4 > 0) && (str2 != "")) && (str2.ToLower().IndexOf("http://match") == -1))
                        {
                            str2 = DBLogin.GetMatchURL() + str2;
                        }
                        if (num4 == 3)
                        {
                            string str3 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                            int num6 = (int) lastGameRowByCategory["ChampionUserID"];
                            str = string.Concat(new object[] { "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num6, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str3, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str2, "')>请点击此处</a>" });
                        }
                        else
                        {
                            str = "冠军杯比赛进行中，请在17点以后查看战况";
                        }
                    }
                    builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td align=\"center\" >" + str + "</td>");
                    builder.Append("\t</tr>");
                    builder.Append("</table> ");
                    goto Label_0E09;
                }
                if (BTPXGroupTeamManager.GetGroupTeamRowByCID(intClubID) == null)
                {
                    string str12 = "";
                    if (flag2)
                    {
                        str12 = "冠军杯小组赛结束，淘汰赛即将展开，敬请关注";
                    }
                    else if (num4 == 1)
                    {
                        str12 = "冠军杯小组赛进行至第" + num5 + "轮";
                    }
                    else
                    {
                        lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                        if (lastGameRowByCategory != null)
                        {
                            num4 = (byte) lastGameRowByCategory["Status"];
                            num5 = (byte) lastGameRowByCategory["Round"];
                            int num18 = (int) lastGameRowByCategory["Capacity"];
                            int num19 = 1;
                            string str13 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                            DateTime time1 = (DateTime) lastGameRowByCategory["MatchTime"];
                            string str14 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                            int num20 = (int) lastGameRowByCategory["ChampionUserID"];
                            lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(1);
                            int num21 = (byte) lastGameRowByCategory["Category"];
                            DateTime time3 = (DateTime) lastGameRowByCategory["MatchTime"];
                            if ((num21 == 3) && (num5 == 1))
                            {
                                if (((num4 > 0) && (str13 != "")) && (str13.ToLower().IndexOf("http://match") == -1))
                                {
                                    str13 = DBLogin.GetMatchURL() + str13;
                                }
                                str12 = "冠军杯小组赛结束，当前正停赛一轮安排淘汰赛的赛程，查看赛程<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                            }
                            else if (((num4 > 0) && (str13 != "")) && (str13.ToLower().IndexOf("http://match") == -1))
                            {
                                str13 = DBLogin.GetMatchURL() + str13;
                            }
                            if (num4 == 1)
                            {
                                while (num18 > 1)
                                {
                                    num18 /= 2;
                                    num19++;
                                }
                                int num22 = num19 - num5;
                                if (num5 == 1)
                                {
                                    str12 = "现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str13 + "')>淘汰赛</a>";
                                }
                                else if (num22 > 5)
                                {
                                    str12 = string.Concat(new object[] { "冠军杯淘汰赛进行到第", num5, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str13, "')>请点击此处</a>" });
                                }
                                else if (num22 > 2)
                                {
                                    str12 = "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num22).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                }
                                else
                                {
                                    switch (num22)
                                    {
                                        case 2:
                                            str12 = "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                            break;

                                        case 1:
                                            str12 = "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                                            break;
                                    }
                                }
                            }
                            else if (num20 != intUserID)
                            {
                                obj2 = str12;
                                str12 = string.Concat(new object[] { obj2, "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num20, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str14, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str13, "')>请点击此处</a>" });
                            }
                            else
                            {
                                str12 = str12 + "本赛季冠军杯正式结束，恭喜您获得总冠军，详情<a href='javascript:;' onclick=window.open('" + str13 + "')>请点击此处</a>";
                            }
                        }
                    }
                    builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#f8ece6; margin-top:6px;\">");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td align=\"center\" >" + str12 + "</td>");
                    builder.Append("\t</tr>");
                    builder.Append("</table> ");
                    goto Label_0E09;
                }
                DataTable championCupUPDown = BTPXGroupTeamManager.GetChampionCupUPDown(intClubID);
                if ((championCupUPDown == null) || (championCupUPDown.Rows.Count <= 1))
                {
                    goto Label_0E09;
                }
                DataRow row4 = championCupUPDown.Rows[1];
                int num7 = (int) row4["ClubAID"];
                int num8 = (int) row4["ClubBID"];
                byte num24 = (byte) row4["Type"];
                if (num7 == -1)
                {
                    str7 = "您已被淘汰，无缘下轮比赛";
                }
                else
                {
                    string str6;
                    if (num7 == intClubID)
                    {
                        str6 = row4["NameB"].ToString().Trim();
                    }
                    else
                    {
                        str6 = row4["NameA"].ToString().Trim();
                    }
                    str7 = "下轮对手：" + str6;
                }
                row4 = championCupUPDown.Rows[0];
                int num11 = (int) row4["MatchID"];
                num7 = (int) row4["ClubAID"];
                num8 = (int) row4["ClubBID"];
                int num25 = (int) row4["UserIDA"];
                int num26 = (int) row4["UserIDB"];
                int num9 = (int) row4["ClubAScore"];
                int num10 = (int) row4["ClubBScore"];
                string str4 = row4["NameA"].ToString().Trim();
                string str5 = row4["NameB"].ToString().Trim();
                int num12 = (byte) row4["Type"];
                if (flag2)
                {
                    str7 = "休息一轮";
                }
                if ((num7 > 0) && (num8 > 0))
                {
                    string str8 = string.Concat(new object[] { 
                        "<a href='", DBLogin.GetMatchURL(), "VRep.aspx?Tag=", num11, "&Type=", num12, "&A=", num7, "&B=", num8, "' target='_blank'>战报</a>|<a href='", DBLogin.GetMatchURL(), "VStas.aspx?Tag=", num11, "&Type=", num12, 
                        "&A=", num7, "&B=", num8, "' target='_blank'>统计</a>"
                     });
                    builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td width='80' align=\"center\"><strong>【冠军杯】</strong></td>");
                    builder.Append(string.Concat(new object[] { "\t<td width='350'  align=\"left\">本轮对手：", str4, " ", num9, "：", num10, " ", str5, "</td>" }));
                    builder.Append("\t<td align=\"left\">" + str8 + "</td>");
                    builder.Append("\t</tr>");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td align=\"center\"></td>");
                    builder.Append("\t<td width='350' align=\"left\">" + str7 + "</td>");
                    builder.Append("\t<td align=\"left\"><a target='Main' href=\"Main_P.aspx?Tag=" + intUserID + "&Type=XBACUP\">查看详细</a></td>");
                    builder.Append("\t</tr>");
                    builder.Append("</table> ");
                    goto Label_0E09;
                }
                if ((num7 == 0) && (num8 == 0))
                {
                    builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                    builder.Append("<tr bgColor=\"#fde3d4\">");
                    builder.Append("\t<td align=\"center\"></td>");
                    builder.Append("\t<td width='350' align=\"left\">" + str7 + "</td>");
                    builder.Append("\t<td align=\"left\"><a target='Main' href=\"Main_P.aspx?Tag=" + intUserID + "&Type=XBACUP\">查看详细</a></td>");
                    builder.Append("\t</tr>");
                    builder.Append("</table> ");
                    goto Label_0E09;
                }
                DataRow xCupRegRowByClubID = BTPXCupRegManager.GetXCupRegRowByClubID(intClubID);
                int num13 = 0;
                if (xCupRegRowByClubID != null)
                {
                    num13 = (byte) xCupRegRowByClubID["DeadRound"];
                }
                str9 = "";
                lastGameRowByCategory = BTPXGameManager.GetLastGameRowByCategory(5);
                if (lastGameRowByCategory != null)
                {
                    num4 = (byte) lastGameRowByCategory["Status"];
                    num5 = (byte) lastGameRowByCategory["Round"];
                    int num14 = (int) lastGameRowByCategory["Capacity"];
                    int num15 = 1;
                    string str10 = lastGameRowByCategory["LadderURL"].ToString().Trim();
                    if (((num4 > 0) && (str10 != "")) && (str10.ToLower().IndexOf("http://match") == -1))
                    {
                        str10 = DBLogin.GetMatchURL() + str10;
                    }
                    if (num4 == 1)
                    {
                        if (num13 == 0)
                        {
                            str9 = "您未进入淘汰赛。";
                        }
                        else
                        {
                            str9 = "您在第" + num13 + "轮淘汰赛中被淘汰出局。";
                        }
                        while (num14 > 1)
                        {
                            num14 /= 2;
                            num15++;
                        }
                        int num16 = num15 - num5;
                        if (num5 == 1)
                        {
                            str9 = "现在停赛一轮正在安排淘汰赛的赛程。点击<a href='javascript:;' onclick=window.open('" + str10 + "')>淘汰赛</a>";
                        }
                        else if (num16 > 5)
                        {
                            obj2 = str9;
                            str9 = string.Concat(new object[] { obj2, "冠军杯淘汰赛进行到第", num5, "轮，关注赛事详情<a href='javascript:;' onclick=window.open('", str10, "')>请点击此处</a>" });
                        }
                        else if (num16 > 2)
                        {
                            string str15 = str9;
                            str9 = str15 + "冠军杯淘汰赛进行到" + Math.Pow(2.0, (double) num16).ToString() + "强阶段，关注赛事详情<a href='javascript:;' onclick=window.open('" + str10 + "')>请点击此处</a>";
                        }
                        else
                        {
                            switch (num16)
                            {
                                case 2:
                                    str9 = str9 + "冠军杯淘汰赛进行半决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str10 + "')>请点击此处</a>";
                                    goto Label_0A4A;

                                case 1:
                                    str9 = str9 + "冠军杯淘汰赛进行决赛阶段！关注赛事详情<a href='javascript:;' onclick=window.open('" + str10 + "')>请点击此处</a>";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        string str11 = lastGameRowByCategory["ChampionClubName"].ToString().Trim();
                        int num17 = (int) lastGameRowByCategory["ChampionUserID"];
                        str9 = string.Concat(new object[] { "本赛季冠军杯正式结束，<a href='javascript:;' onclick=window.open('ShowClubIFrom.aspx?UserID=", num17, "','','height=452,width=224,status=no,toolbar=no,menubar=no,location=no');>", str11, "</a>获得总冠军，详情<a href='javascript:;' onclick=window.open('", str10, "')>请点击此处</a>" });
                    }
                }
            }
            else
            {
                builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
                builder.Append("<tr bgColor=\"#fde3d4\">");
                switch (num3)
                {
                    case 1:
                        builder.Append("\t<td align=\"center\" >冠军杯报名第一天！请持有冠军杯邀请函的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
                        break;

                    case 2:
                        builder.Append("\t<td align=\"center\" >冠军杯报名第二天！请持有冠军杯邀请函及冠军杯邀请函（盟）的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
                        break;

                    default:
                        builder.Append("\t<td align=\"center\" >冠军杯赛报名截止至16：00！请欲参赛的球队尽快报名！<a target='Main' href=\"Main_P.aspx?Tag=" + this.intUserID + "&Type=XBACUP\">立即报名</a></td>");
                        break;
                }
                builder.Append("\t</tr>");
                builder.Append("</table> ");
                return builder.ToString().Trim();
            }
        Label_0A4A:
            builder.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"4\" align=\"center\" style=\"border-top:1px solid #f1c2a3; background:#fcc6a4; margin-top:6px;\">");
            builder.Append("<tr bgColor=\"#fde3d4\">");
            builder.Append("\t<td align=\"center\" bgColor=\"#fcc6a4\" >" + str9 + "</td>");
            builder.Append("\t</tr>");
            builder.Append("</table> ");
        Label_0E09:
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


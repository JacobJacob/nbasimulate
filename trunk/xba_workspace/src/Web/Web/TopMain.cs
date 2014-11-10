namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TopMain : Page
    {
        private int intCategory;
        private int intClubID5;
        private int intUserID;
        public string strMain;

        private string GetVTList()
        {
            int num;
            string str;
            string str2;
            int intRound = (int) BTPGameManager.GetGameRow()["Turn"];
            bool flag = true;
            DataRow clubRowByID = BTPClubManager.GetClubRowByID(this.intClubID5);
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(this.intClubID5);
            int level = DevCalculator.GetLevel(devCodeByClubID);
            DevCalculator.GetDev(devCodeByClubID);
            int devIndex = DevCalculator.GetDevIndex(devCodeByClubID);
            int orderByClubID = BTPDevManager.GetOrderByClubID(this.intClubID5);
            TagReader reader = new TagReader(clubRowByID["MainXML"].ToString().Trim());
            int intScore = 0;
            int num7 = 0;
            string str4 = "";
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "0";
            string str9 = "0";
            string str10 = "";
            string str11 = "0";
            string str12 = "0";
            string str13 = "0";
            string str14 = "0";
            string str15 = "0";
            string tagline = "";
            string str17 = "";
            string str18 = "";
            string str19 = "";
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
                    str8 = "0";
                    str9 = "0";
                    str10 = "";
                    str11 = "0";
                    str12 = "0";
                    str13 = "0";
                    str14 = "0";
                    str15 = "0";
                }
                else
                {
                    intScore = Convert.ToInt32(reader.GetTagline("ScoreH"));
                    num7 = Convert.ToInt32(reader.GetTagline("ScoreA"));
                    str4 = reader.GetTagline("ClubNameH");
                    str5 = reader.GetTagline("ClubNameA");
                    str6 = reader.GetTagline("ClubLogoH");
                    str7 = reader.GetTagline("ClubLogoA");
                    str8 = reader.GetTagline("Tickets");
                    str9 = reader.GetTagline("Income");
                    str10 = reader.GetTagline("MVPName");
                    if ((str4 == "") || (str5 == ""))
                    {
                        str11 = "0";
                        str12 = "0";
                        str13 = "0";
                        str14 = "0";
                        str15 = "0";
                    }
                    else
                    {
                        string[] strArray = reader.GetTagline("MVPStas").Split(new char[] { '|' });
                        str11 = strArray[0];
                        str12 = strArray[1];
                        str13 = strArray[2];
                        str15 = strArray[3];
                        str14 = strArray[4];
                    }
                }
                tagline = reader.GetTagline("NClubNameH");
                str17 = reader.GetTagline("NClubNameA");
                str18 = reader.GetTagline("NClubLogoH");
                str19 = reader.GetTagline("NClubLogoA");
            }
            catch
            {
                flag = false;
            }
            string str20 = "";
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
                string str21;
                string str22;
                string str23;
                int num8;
                int num9;
                object obj2;
                str20 = string.Concat(new object[] { "<table width='536'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td height='25' width='268' align='right'>已注册球队</td><td width='268' align='right'>第", level, "等级 第", devIndex, "联赛&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>" });
                BTPDevManager.GetClubCountByDevCode(devCodeByClubID);
                if (BTPDevManager.GetClubTableByDevCode(devCodeByClubID) == null)
                {
                    return (str20 + "<tr><td width='100%' align='center' colspan='2'>联赛暂无球队。</td></tr><table>");
                }
                str20 = str20 + "<tr><td width='268'><table width='100%'  border='0' cellspacing='0' cellpadding='0'>";
                DataTable table = BTPDevManager.GetClubTableByDevCodeTop7(devCodeByClubID);
                if (table == null)
                {
                    str20 = str20 + "<tr><td></td></tr>";
                }
                else
                {
                    foreach (DataRow row2 in table.Rows)
                    {
                        num9 = (int) row2["ClubID"];
                        if (num9 != 0)
                        {
                            DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(num9);
                            str21 = clubRowByClubID["ClubLogo"].ToString().Trim();
                            str22 = clubRowByClubID["ClubName"].ToString().Trim();
                            int num1 = (int) clubRowByClubID["UserID"];
                            str23 = clubRowByClubID["NickName"].ToString().Trim();
                            num8 = (byte) clubRowByClubID["Levels"];
                            obj2 = str20;
                            str20 = string.Concat(new object[] { obj2, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width='60' height='50' align='center'><img src='", SessionItem.GetImageURL(), "Club/Logo/", num8, "/", str21, ".gif'></td><td style='padding-left:5px'>球队：", str22, "<br>经理：", str23, "</td></tr>" });
                        }
                        else
                        {
                            str20 = str20 + "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width='60' height='50' align='center'><img src='" + SessionItem.GetImageURL() + "Club/Logo/0.gif'></td><td style='padding-left:5px'></td></tr>";
                        }
                    }
                }
                str20 = str20 + "</table></td><td width='268'><table bgcolor='#FBE2D4' width='100%'  border='0' cellspacing='0' cellpadding='0'>";
                DataTable table2 = BTPDevManager.GetClubTableByDevCodeEnd7(devCodeByClubID);
                if (table2 == null)
                {
                    str20 = str20 + "<tr><td></td></tr>";
                }
                else
                {
                    foreach (DataRow row4 in table2.Rows)
                    {
                        num9 = (int) row4["ClubID"];
                        if (num9 != 0)
                        {
                            DataRow row5 = BTPClubManager.GetClubRowByClubID(num9);
                            str21 = row5["ClubLogo"].ToString().Trim();
                            str22 = row5["ClubName"].ToString().Trim();
                            int num10 = (int) row5["UserID"];
                            str23 = row5["NickName"].ToString().Trim();
                            num8 = (byte) row5["Levels"];
                            obj2 = str20;
                            str20 = string.Concat(new object[] { obj2, "<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td width='60' height='50' align='center'><img src='", SessionItem.GetImageURL(), "Club/Logo/", num8, "/", str21, ".gif'></td><td style='padding-left:5px'>球队：", str22, "<br>经理：", str23, "</td></tr>" });
                        }
                        else
                        {
                            str20 = str20 + "<tr onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td width='60' height='50' align='center'><img src='" + SessionItem.GetImageURL() + "Club/Logo/0.gif'></td><td style='padding-left:5px'></td></tr>";
                        }
                    }
                }
                return (str20 + "</table></td></tr></table>");
            }
            DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound);
            if (devMRowByClubIDRound != null)
            {
                int num11 = (int) devMRowByClubIDRound["DevMatchID"];
                str = "\t<tr class='BarHead' height='25'>\t\t<td><strong>本轮比赛对手</strong></td>\t</tr>\t<tr>\t\t<td height='100' align='center'>";
                if ((tagline == "") || (str17 == ""))
                {
                    str = str + "按照赛程，比赛轮空";
                }
                else
                {
                    str2 = str;
                    str = str2 + "\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td height='50'>\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td width='25%' align='center'><font color='#660066'><strong>" + tagline + "</strong></font></td>\t\t\t\t\t\t\t\t<td width='10%'><img src='Images/Club/Logo/" + str18 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t<td width='30%' align='center'><img src='Images/Score/99.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/00.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/Bar.gif' width='19' height='28' border='0'><img src='Images/Score/99.gif' width='19' height='28' border='0'></td>\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='Images/Club/Logo/" + str19 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t<td width='25%' align='center'><font color='#660066'><strong>" + str17 + "</strong></font></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t</table>\t\t\t\t\t</td>\t\t\t\t</tr>\t\t\t\t<tr>\t\t\t\t\t<td style='padding-left:5px;' height='25' align='center'>常规赛期间，每日6:00-10:00进行比赛，第13轮和第14轮之间休赛一天</td>\t\t\t\t</tr>\t\t\t\t<tr>\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>比赛球场</strong>：" + tagline + "主场</td>\t\t\t\t</tr>\t\t\t</table>";
                }
                str = str + "\t\t</td>\t</tr>";
            }
            else
            {
                str = "\t<tr class='BarHead' height='25'>\t\t<td><strong>本轮比赛对手</strong></td>\t</tr>\t<tr>\t\t<td height='100' align='center'>赛季结束</td>\t</tr>";
            }
            str20 = string.Concat(new object[] { "<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t<tr class='BarHead' height='25'>\t\t<td>\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td width='55%' align='right'><strong>上轮比赛结果</strong></td>\t\t\t\t\t<td width='45%' align='right' style='padding-right:4px;'>第", level, "等级 第", devIndex, "联赛 第", orderByClubID, "名</td>\t\t\t\t</tr>\t\t\t</table>\t\t</td>\t</tr>\t<tr>\t\t<td height='100' align='center'>" });
            if (intRound == 1)
            {
                str20 = str20 + "无比赛";
            }
            else if ((str4 == "") || (str5 == ""))
            {
                str20 = str20 + "按照赛程，比赛轮空";
            }
            else
            {
                DataRow row7 = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, intRound - 1);
                string text1 = Config.GetDomain() + "VRep.aspx?Tag=" + row7["DevMatchID"].ToString() + "&Type=2&A=" + row7["ClubHID"].ToString() + "&B=" + row7["ClubAID"].ToString();
                string text2 = Config.GetDomain() + "VStas.aspx?Tag=" + row7["DevMatchID"].ToString() + "&Type=2&A=" + row7["ClubHID"].ToString() + "&B=" + row7["ClubAID"].ToString();
                int num12 = (int) row7["DevMatchID"];
                int num13 = (int) row7["MsgCount"];
                str2 = str20;
                str20 = str2 + "\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t<tr>\t\t\t\t\t<td height='50'>\t\t\t\t\t\t<table width='100%' border='0' cellspacing='0' cellpadding='0'>\t\t\t\t\t\t\t<tr>\t\t\t\t\t\t\t\t<td width='25%' align='center'><font color='#660066'><strong>" + str4 + "</strong></font></td>\t\t\t\t\t\t\t\t<td width='10%'><img src='Images/Club/Logo/" + str6 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t<td width='30%' align='center'>" + MatchItem.GetScore(intScore) + "<img src='Images/Score/00.gif' width='19' height='28' border='0'>" + MatchItem.GetScore(num7) + "</td>\t\t\t\t\t\t\t\t<td width='10%' align='center'><img src='Images/Club/Logo/" + str7 + "' border='0' width='46' height='46'></td>\t\t\t\t\t\t\t\t<td width='25%' align='center'><font color='#660066'><strong>" + str5 + "</strong></font></td>\t\t\t\t\t\t\t</tr>\t\t\t\t\t\t</table>\t\t\t\t\t</td>\t\t\t\t</tr>\t\t\t\t<tr>\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>门票出售</strong>：" + str8 + "张&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>总收入</strong>：" + str9 + "</td>\t\t\t\t</tr>\t\t\t\t<tr>\t\t\t\t\t<td style='padding-left:5px;' height='25'><strong>本场MVP</strong>：" + str10 + "&nbsp;[&nbsp;得分：" + str11 + "&nbsp;|&nbsp;篮板：" + str12 + "&nbsp;|&nbsp;助攻：" + str13 + "&nbsp;|&nbsp;盖帽：" + str14 + "&nbsp;|&nbsp;抢断：" + str15 + "&nbsp;]</td>\t\t\t\t</tr>\t\t\t</table>";
            }
            return (str20 + "\t\t</td>\t</tr>" + str + "</table>");
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
                SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                if (((this.intCategory != 1) && (this.intCategory != 2)) && (this.intCategory != 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=10112");
                }
                else
                {
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.strMain = this.GetVTList();
        }
    }
}


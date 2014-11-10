namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class TrainPlayer5 : Page
    {
        protected HtmlInputHidden AbilityAdd1;
        protected HtmlInputHidden AbilityAdd10;
        protected HtmlInputHidden AbilityAdd11;
        protected HtmlInputHidden AbilityAdd2;
        protected HtmlInputHidden AbilityAdd3;
        protected HtmlInputHidden AbilityAdd4;
        protected HtmlInputHidden AbilityAdd5;
        protected HtmlInputHidden AbilityAdd6;
        protected HtmlInputHidden AbilityAdd7;
        protected HtmlInputHidden AbilityAdd8;
        protected HtmlInputHidden AbilityAdd9;
        protected ImageButton btnOK;
        private int intClubID5;
        private int intNumber;
        private int intPower;
        public int intTeamDay;
        private int intUserID;
        private long lngPlayerID;
        public StringBuilder sbAbility = new StringBuilder("");
        public StringBuilder sbAbility1 = new StringBuilder("");
        public StringBuilder sbReturn = new StringBuilder("");
        public string strFace = "";
        public string strNumber = "";
        public string strPageIntro = "";
        public string strPlayerID = "";
        public string strPoint = "";
        public string strShirt = "";

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int[] addAbility = new int[] { Convert.ToInt32(this.AbilityAdd1.Value), Convert.ToInt32(this.AbilityAdd2.Value), Convert.ToInt32(this.AbilityAdd3.Value), Convert.ToInt32(this.AbilityAdd4.Value), Convert.ToInt32(this.AbilityAdd5.Value), Convert.ToInt32(this.AbilityAdd6.Value), Convert.ToInt32(this.AbilityAdd7.Value), Convert.ToInt32(this.AbilityAdd8.Value), Convert.ToInt32(this.AbilityAdd9.Value), Convert.ToInt32(this.AbilityAdd10.Value), Convert.ToInt32(this.AbilityAdd11.Value) };
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < 11; i++)
            {
                num += addAbility[i];
            }
            num2 = (num / 10) + (((num % 10) > 0) ? 1 : 0);
            if ((this.intPower - num2) >= 30)
            {
                switch (BTPPlayer5Manager.TrainPlayer5ByPlayerID(this.lngPlayerID, this.intClubID5, addAbility))
                {
                    case -3:
                        base.Response.Redirect("Report.aspx?Parameter=913d!PID=" + this.lngPlayerID);
                        return;

                    case -2:
                        base.Response.Redirect("Report.aspx?Parameter=913b!PID=" + this.lngPlayerID);
                        return;

                    case -1:
                        base.Response.Redirect("Report.aspx?Parameter=913c!PID=" + this.lngPlayerID);
                        return;

                    case 0:
                        return;

                    case 1:
                        base.Response.Write(string.Concat(new object[] { "<script>window.top.Main.Right.location=\"ShowPlayer.aspx?Type=5&Kind=1&Check=1&PlayerID=", this.lngPlayerID, "\";window.location=\"Report.aspx?Parameter=914!Type.5^UserID.", this.intUserID, "\";</script>" }));
                        return;
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=913b!PID=" + this.lngPlayerID);
            }
        }

        private string GetHtmlTable(int intAbility, int intAbilityMax, string strName)
        {
            string str;
            int num = intAbility / 9;
            int num2 = intAbilityMax - intAbility;
            if (num2 < 0)
            {
                num2 = 0;
            }
            if (this.intTeamDay > 4)
            {
                if (intAbilityMax < 0)
                {
                    str = "12";
                }
                else if (num2 > 250)
                {
                    str = "1";
                }
                else if ((num2 <= 250) && (num2 > 200))
                {
                    str = "2";
                }
                else if ((num2 <= 200) && (num2 > 160))
                {
                    str = "3";
                }
                else if ((num2 <= 160) && (num2 > 0x7d))
                {
                    str = "4";
                }
                else if ((num2 <= 0x7d) && (num2 > 0x5f))
                {
                    str = "5";
                }
                else if ((num2 <= 0x5f) && (num2 > 70))
                {
                    str = "6";
                }
                else if ((num2 <= 70) && (num2 > 0x30))
                {
                    str = "7";
                }
                else if ((num2 <= 0x30) && (num2 > 30))
                {
                    str = "8";
                }
                else if ((num2 <= 30) && (num2 > 15))
                {
                    str = "9";
                }
                else if ((num2 <= 15) && (num2 > 7))
                {
                    str = "10";
                }
                else
                {
                    str = "11";
                }
            }
            else
            {
                str = "0";
            }
            string str2 = "<table cellpadding='0' cellspacing='0' border='0' width='157'>";
            object obj2 = str2;
            return string.Concat(new object[] { obj2, "<tr><td width='120' bgcolor='#EEEEEE' height='18'><img src='", SessionItem.GetImageURL(), "Player/Ability/Color", str, ".gif' width='", num, "' height='8'></td><td width='37' align='center'>", PlayerItem.GetAbilityColor(intAbility), "<input type='hidden'  id='", strName, "' value='", str, "'></td></tr></table>" });
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.btnOK.Attributes["onclick"] = base.GetPostBackEventReference(this.btnOK) + ";this.disabled=true;";
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
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string str;
            int num = ToolItem.HasTool(this.intUserID, 9, 0);
            int num2 = ToolItem.HasTool(this.intUserID, 10, 0);
            if ((num > 0) || (num2 > 0))
            {
                str = "";
            }
            else
            {
                str = "";
            }
            this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/TrainPlayer.aspx?Type=PLAYER3\"' href='TrainPlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'>街球队</a></li><li class='qian2'>职业队</a></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>", str });
            this.SetTrainPlayer5();
        }

        private void SetTrainPlayer5()
        {
            this.btnOK.Attributes.Add("onclick", "return CheckOnClick");
            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "baocun.gif";
            this.lngPlayerID = SessionItem.GetRequest("PID", 3);
            this.Session.Add("MedicinePID", this.lngPlayerID);
            DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.lngPlayerID);
            if (playerRowByPlayerID != null)
            {
                int intClubID = (int) playerRowByPlayerID["ClubID"];
                if (this.intClubID5 == intClubID)
                {
                    int num2;
                    int num3;
                    string str;
                    string str2;
                    int intAbility = (int) playerRowByPlayerID["Speed"];
                    int num5 = (int) playerRowByPlayerID["Jump"];
                    int num6 = (int) playerRowByPlayerID["Strength"];
                    int num7 = (int) playerRowByPlayerID["Stamina"];
                    int num8 = (int) playerRowByPlayerID["Shot"];
                    int num9 = (int) playerRowByPlayerID["Point3"];
                    int num10 = (int) playerRowByPlayerID["Dribble"];
                    int num11 = (int) playerRowByPlayerID["Pass"];
                    int num12 = (int) playerRowByPlayerID["Rebound"];
                    int num13 = (int) playerRowByPlayerID["Steal"];
                    int num14 = (int) playerRowByPlayerID["Block"];
                    int num15 = (int) playerRowByPlayerID["Attack"];
                    int num16 = (int) playerRowByPlayerID["Defense"];
                    int num17 = (int) playerRowByPlayerID["Team"];
                    int intAbilityMax = (int) playerRowByPlayerID["SpeedMax"];
                    int num19 = (int) playerRowByPlayerID["JumpMax"];
                    int num20 = (int) playerRowByPlayerID["StrengthMax"];
                    int num21 = (int) playerRowByPlayerID["StaminaMax"];
                    int num22 = (int) playerRowByPlayerID["ShotMax"];
                    int num23 = (int) playerRowByPlayerID["Point3Max"];
                    int num24 = (int) playerRowByPlayerID["DribbleMax"];
                    int num25 = (int) playerRowByPlayerID["PassMax"];
                    int num26 = (int) playerRowByPlayerID["ReboundMax"];
                    int num27 = (int) playerRowByPlayerID["StealMax"];
                    int num28 = (int) playerRowByPlayerID["BlockMax"];
                    int num1 = (int) playerRowByPlayerID["AttackMax"];
                    int num50 = (int) playerRowByPlayerID["DefenseMax"];
                    int num51 = (int) playerRowByPlayerID["TeamMax"];
                    this.intTeamDay = (int) playerRowByPlayerID["TeamDay"];
                    int num29 = (int) playerRowByPlayerID["SpeedMax"];
                    int num30 = (int) playerRowByPlayerID["JumpMax"];
                    int num31 = (int) playerRowByPlayerID["StrengthMax"];
                    int num32 = (int) playerRowByPlayerID["StaminaMax"];
                    int num33 = (int) playerRowByPlayerID["ShotMax"];
                    int num34 = (int) playerRowByPlayerID["Point3Max"];
                    int num35 = (int) playerRowByPlayerID["DribbleMax"];
                    int num36 = (int) playerRowByPlayerID["PassMax"];
                    int num37 = (int) playerRowByPlayerID["ReboundMax"];
                    int num38 = (int) playerRowByPlayerID["StealMax"];
                    int num39 = (int) playerRowByPlayerID["BlockMax"];
                    int num52 = (int) playerRowByPlayerID["AttackMax"];
                    int num53 = (int) playerRowByPlayerID["DefenseMax"];
                    int num54 = (int) playerRowByPlayerID["TeamMax"];
                    if ((intAbility + 50) < intAbilityMax)
                    {
                        num29 = 0;
                    }
                    if ((num5 + 50) < num19)
                    {
                        num30 = 0;
                    }
                    if ((num6 + 50) < num20)
                    {
                        num31 = 0;
                    }
                    if ((num7 + 50) < num21)
                    {
                        num32 = 0;
                    }
                    if ((num8 + 50) < num22)
                    {
                        num33 = 0;
                    }
                    if ((num9 + 50) < num23)
                    {
                        num34 = 0;
                    }
                    if ((num10 + 50) < num24)
                    {
                        num35 = 0;
                    }
                    if ((num11 + 50) < num25)
                    {
                        num36 = 0;
                    }
                    if ((num12 + 50) < num26)
                    {
                        num37 = 0;
                    }
                    if ((num13 + 50) < num27)
                    {
                        num38 = 0;
                    }
                    if ((num14 + 50) < num28)
                    {
                        num39 = 0;
                    }
                    int intPosition = (byte) playerRowByPlayerID["Pos"];
                    int num41 = (byte) playerRowByPlayerID["Age"];
                    int num42 = (byte) playerRowByPlayerID["PlayedYear"];
                    int num43 = (byte) playerRowByPlayerID["Height"];
                    int num44 = (byte) playerRowByPlayerID["Weight"];
                    this.intPower = (byte) playerRowByPlayerID["Power"];
                    int num45 = (int) playerRowByPlayerID["Ability"];
                    int intStatus = (byte) playerRowByPlayerID["Status"];
                    int intSuspend = (byte) playerRowByPlayerID["Suspend"];
                    string strEvent = playerRowByPlayerID["Event"].ToString();
                    int num55 = (int) playerRowByPlayerID["TeamDay"];
                    byte num56 = (byte) playerRowByPlayerID["Category"];
                    string str4 = playerRowByPlayerID["Name"].ToString().Trim();
                    int num48 = (int) playerRowByPlayerID["Salary"];
                    this.strFace = playerRowByPlayerID["Face"].ToString().Trim();
                    int num49 = (int) playerRowByPlayerID["TrainPoint"];
                    DataRow accountRowByClubID = BTPAccountManager.GetAccountRowByClubID(intClubID);
                    this.strPoint = num49.ToString();
                    if (accountRowByClubID == null)
                    {
                        num2 = 1;
                        str = "无";
                        num3 = 0;
                    }
                    else
                    {
                        num2 = Convert.ToInt16(accountRowByClubID["Shirt"].ToString().Trim());
                        str = accountRowByClubID["NickName"].ToString().Trim();
                        num3 = (int) accountRowByClubID["UserID"];
                        str = AccountItem.GetNickNameInfo(num3, str, "", 8);
                    }
                    this.intNumber = (byte) playerRowByPlayerID["Number"];
                    if (num2 > 15)
                    {
                        this.strNumber = (0x526c + this.intNumber) + "";
                    }
                    else
                    {
                        this.strNumber = (0x5208 + this.intNumber) + "";
                    }
                    this.strShirt = (0x4e20 + num2) + "";
                    this.sbReturn.Append("<table width='92' border='0' cellspacing='0' cellpadding='0'>\n");
                    this.sbReturn.Append("\t<tr>\n");
                    this.sbReturn.Append("\t\t<td width='92' valign='top'><img id='imgCharactor' src='" + SessionItem.GetImageURL() + "Player/Charactor/NewPlayer.png' width='90' height='130'><br>\n");
                    this.sbReturn.Append(string.Concat(new object[] { "\t\t\t&nbsp;&nbsp;</td>\n" }));
                    this.sbReturn.Append("\t\t<td valign='top'>\n");
                    this.sbReturn.Append("\t\t\t<table width='109' border='0' cellspacing='0' cellpadding='2'>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td colspan=\"2\" height='30' align='left'>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "&nbsp;<strong>" + str4 + "</strong></td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' width='37' align='center'>年龄</td>\n");
                    this.sbReturn.Append(string.Concat(new object[] { "\t\t\t\t\t<td width='78' align=\"left\"><a title='第", playerRowByPlayerID["BirthTurn"].ToString(), "轮生日' style='CURSOR: hand'>", num41, "</a>&nbsp;<a title='球龄' style='CURSOR: hand'>[", num42, "]</a></td>\n" }));
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' align='center'>薪水</td>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td align=\"left\">" + num48 + "</td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' width='37' align='center'>位置</td>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td width='78' align=\"left\"><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' align='center'>身高</td>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td align=\"left\">" + num43 + "CM</td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' align='center'>体重</td>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td align=\"left\">" + num44 + "KG</td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' align='center'>体力</td>\n");
                    this.sbReturn.Append(string.Concat(new object[] { "\t\t\t\t\t<td align=\"left\"><span id='PowerNow'>", PlayerItem.GetPowerColor(this.intPower), "</span><input type='hidden' id='Power' value='", this.intPower, "'></td>\n" }));
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t\t<tr>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td height='18' align='center'>综合</td>\n");
                    this.sbReturn.Append("\t\t\t\t\t<td align=\"left\"><span id='Ability'>" + PlayerItem.GetAbilityColor(num45) + "</span></td>\n");
                    this.sbReturn.Append("\t\t\t\t</tr>\n");
                    this.sbReturn.Append("\t\t\t</table>\n");
                    this.sbReturn.Append(string.Concat(new object[] { "<input type='hidden' id='Ability12' value='", num15, "'><input type='hidden' id='Ability13' value='", num16, "'><input type='hidden' id='Ability14' value='", num17, "'></td>\n" }));
                    this.sbReturn.Append("\t</tr>\n");
                    this.sbReturn.Append("</table>\n");
                    if (intAbility < intAbilityMax)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("<table width=\"230\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#fcf2ec\" id=\"tblDetail\">\n");
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td width=\"44\" height=\"22\" align=\"center\"><input type='hidden' id='Ability1' value='", intAbility, "'><input type='hidden' id='AbilityMax1' value='", num29, "'>\n" }));
                    this.sbAbility.Append("\t\t\t\t速度</td>\n");
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility1'>" + this.GetHtmlTable(intAbility, intAbilityMax, "tblAbility1") + "</td>");
                    this.sbAbility.Append("\t\t\t<td width=\"20\" align=\"center\"><img id='btnAdd1' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt1'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num5 < num19)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">弹跳<input type='hidden' id='Ability2' value='", num5, "'><input type='hidden' id='AbilityMax2' value='", num30, "'></td>\n" }));
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility2'>" + this.GetHtmlTable(num5, num19, "tblAbility2") + "\n");
                    this.sbAbility.Append("\t\t\t<td align=\"center\"><img id='btnAdd2' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt2'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num6 < num20)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">强壮<input type='hidden' id='Ability3' value='", num6, "'><input type='hidden' id='AbilityMax3' value='", num31, "'></td>\n" }));
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility3'>" + this.GetHtmlTable(num6, num20, "tblAbility3") + "\n");
                    this.sbAbility.Append("\t\t\t<td align=\"center\"><img id='btnAdd3' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt3'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num7 < num21)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">耐力<input type='hidden' id='Ability4' value='", num7, "'><input type='hidden' id='AbilityMax4' value='", num32, "'></td>\n" }));
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility4'>" + this.GetHtmlTable(num7, num21, "tblAbility4") + "\n");
                    this.sbAbility.Append("\t\t\t<td align=\"center\"><img id='btnAdd4' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt4'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num8 < num22)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">投篮<input type='hidden' id='Ability5' value='", num8, "'><input type='hidden' id='AbilityMax5' value='", num33, "'></td>\n" }));
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility5'>" + this.GetHtmlTable(num8, num22, "tblAbility5") + "\n");
                    this.sbAbility.Append("\t\t\t<td align=\"center\"><img id='btnAdd5' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt5'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num9 < num23)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t\t<tr>\n");
                    this.sbAbility.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">三分<input type='hidden' id='Ability6' value='", num9, "'><input type='hidden' id='AbilityMax6' value='", num34, "'></td>\n" }));
                    this.sbAbility.Append("\t\t\t<td width=\"157\" id='tdAbility6'>" + this.GetHtmlTable(num9, num23, "tblAbility6") + "\n");
                    this.sbAbility.Append("\t\t\t<td align=\"center\"><img id='btnAdd6' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility.Append("\t\t\t<td align=\"left\"><span id='alt6'></span></td>");
                    this.sbAbility.Append("\t\t</tr>\n");
                    if (num10 < num24)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility.Append("\t</table>\n");
                    this.sbAbility1.Append("<table width=\"230\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" bgcolor=\"#fcf2ec\" id=\"tblDetail2\">\n");
                    this.sbAbility1.Append("\t\t<tr>\n");
                    this.sbAbility1.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">运球<input type='hidden' id='Ability7' value='", num10, "'><input type='hidden' id='AbilityMax7' value='", num35, "'></td>\n" }));
                    this.sbAbility1.Append("\t\t\t<td width=\"157\" id='tdAbility7'>" + this.GetHtmlTable(num10, num24, "tblAbility7") + "\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"center\"><img id='btnAdd7' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"left\"><span id='alt7'></span></td>");
                    this.sbAbility1.Append("\t\t</tr>\n");
                    if (num11 < num25)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility1.Append("\t\t<tr>\n");
                    this.sbAbility1.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">传球<input type='hidden' id='Ability8' value='", num11, "'><input type='hidden' id='AbilityMax8' value='", num36, "'></td>\n" }));
                    this.sbAbility1.Append("\t\t\t<td width=\"157\" id='tdAbility8'>" + this.GetHtmlTable(num11, num25, "tblAbility8") + "\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"center\"><img id='btnAdd8' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"left\"><span id='alt8'></span></td>");
                    this.sbAbility1.Append("\t\t</tr>\n");
                    if (num12 < num26)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility1.Append("\t\t<tr>\n");
                    this.sbAbility1.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">篮板<input type='hidden' id='Ability9' value='", num12, "'><input type='hidden' id='AbilityMax9' value='", num37, "'></td>\n" }));
                    this.sbAbility1.Append("\t\t\t<td width=\"157\" id='tdAbility9'>" + this.GetHtmlTable(num12, num26, "tblAbility9") + "\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"center\"><img id='btnAdd9' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"left\"><span id='alt9'></span></td>");
                    this.sbAbility1.Append("\t\t</tr>\n");
                    if (num13 < num27)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility1.Append("\t\t<tr>\n");
                    this.sbAbility1.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">抢断<input type='hidden' id='Ability10' value='", num13, "'><input type='hidden' id='AbilityMax10' value='", num38, "'></td>\n" }));
                    this.sbAbility1.Append("\t\t\t<td width=\"157\" id='tdAbility10'>" + this.GetHtmlTable(num13, num27, "tblAbility10") + "\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"center\"><img id='btnAdd10' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"left\"><span id='alt10'></span></td>");
                    this.sbAbility1.Append("\t\t</tr>\n");
                    if (num14 < num28)
                    {
                        str2 = "onclick=\"AddAbility(this,1)\" src=\"images/zengjia.gif\"";
                    }
                    else
                    {
                        str2 = "src=\"images/zengjia_C.gif\"";
                    }
                    this.sbAbility1.Append("\t\t<tr>\n");
                    this.sbAbility1.Append(string.Concat(new object[] { "\t\t\t<td height=\"22\" align=\"center\">封盖<input type='hidden' id='Ability11' value='", num14, "'><input type='hidden' id='AbilityMax11' value='", num39, "'></td>\n" }));
                    this.sbAbility1.Append("\t\t\t<td width=\"157\" id='tdAbility11'>" + this.GetHtmlTable(num14, num28, "tblAbility11") + "\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"center\"><img id='btnAdd11' " + str2 + " style=\"cursor:pointer;\" width=\"12\" height=\"12\" alt=\"训练提升该项目值\"></td>\n");
                    this.sbAbility1.Append("\t\t\t<td align=\"left\"><span id='alt11'></span></td>");
                    this.sbAbility1.Append("\t\t</tr>\n");
                    this.sbAbility1.Append("\t</table>\n");
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=913c");
            }
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class TrainPlayerCenter : Page
    {
        protected ImageButton btnOK;
        protected HtmlInputHidden hidTrains;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        public int intType;
        private int intUserID;
        public long longFirstPlayerID;
        public string strAddAllPower = "";
        public string strDevMsg;
        public string strList;
        public string strMainScript;
        public string strManagerSay;
        public string strPageIntro;
        public string strScript;
        public string strTools;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
        }

        private void GetManagerSay()
        {
            int turn = BTPGameManager.GetTurn();
            if (turn <= 0x1a)
            {
                DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(this.intClubID5, turn);
                if (devMRowByClubIDRound != null)
                {
                    int num2 = (int) devMRowByClubIDRound["DevMatchID"];
                    int num3 = (int) devMRowByClubIDRound["ClubHID"];
                    int num1 = (int) devMRowByClubIDRound["ClubAID"];
                    byte num4 = (byte) devMRowByClubIDRound["MangerSayH"];
                    byte num5 = (byte) devMRowByClubIDRound["MangerSayA"];
                    if (num3 == this.intClubID5)
                    {
                        if (num4 > 0)
                        {
                            this.strManagerSay = string.Concat(new object[] { "球员们都十分感激您对他们的奖励，士气大增，状态提高<font color='red'>", num4, "%</font> <a href='SecretaryPage.aspx?Pos=2&Type=MANGERSAY&DevMatchID=", num2, "'>再次奖励</a>" });
                        }
                        else
                        {
                            this.strManagerSay = "您想让球员发挥出超常的水平吗？额外的发一些奖金是个不错的选择哦！ <a href='SecretaryPage.aspx?Pos=2&Type=MANGERSAY&DevMatchID=" + num2 + "'>奖励球员</a>";
                        }
                    }
                    else if (num5 > 0)
                    {
                        this.strManagerSay = string.Concat(new object[] { "球员们都十分感激您对他们的奖励，士气大增，状态提高<font color='red'>", num5, "%</font> <a href='SecretaryPage.aspx?Pos=2&Type=MANGERSAY&DevMatchID=", num2, "'>再次奖励</a>" });
                    }
                    else
                    {
                        this.strManagerSay = "您想让球员发挥出超常的水平吗？额外的发一些奖金是个不错的选择哦！ <a href='SecretaryPage.aspx?Pos=2&Type=MANGERSAY&DevMatchID=" + num2 + "'>奖励球员</a>";
                    }
                }
                else
                {
                    this.strManagerSay = "　";
                }
            }
            else
            {
                this.strManagerSay = "　";
            }
        }

        private string GetOldPlayerList(DataTable dt)
        {
            if (dt != null)
            {
                int num = 0;
                foreach (DataRow row in dt.Rows)
                {
                    long num2 = (long) row["PlayerID"];
                    if (num == 0)
                    {
                        this.longFirstPlayerID = num2;
                    }
                    num++;
                }
            }
            return "";
        }

        private string GetPlayerList(DataTable dt)
        {
            string str = "";
            if (dt == null)
            {
                return "";
            }
            int num = 0;
            foreach (DataRow row in dt.Rows)
            {
                string str2;
                string str3;
                object obj2;
                long longPlayerID = (long) row["PlayerID"];
                if (num == 0)
                {
                    this.longFirstPlayerID = longPlayerID;
                }
                string strName = row["Name"].ToString();
                int num3 = (byte) row["Number"];
                int num4 = (byte) row["Age"];
                int intPosition = (byte) row["Pos"];
                int intPower = (byte) row["Power"];
                int num7 = (byte) row["Height"];
                int num8 = (byte) row["Weight"];
                float single1 = ((float) ((int) row["Ability"])) / 10f;
                int intStatus = (byte) row["Status"];
                byte num1 = (byte) row["Category"];
                float single2 = ((float) ((int) row["Speed"])) / 10f;
                float single3 = ((float) ((int) row["Jump"])) / 10f;
                float single4 = ((float) ((int) row["Strength"])) / 10f;
                float single5 = ((float) ((int) row["Stamina"])) / 10f;
                float single6 = ((float) ((int) row["Shot"])) / 10f;
                float single7 = ((float) ((int) row["Point3"])) / 10f;
                float single8 = ((float) ((int) row["Dribble"])) / 10f;
                float single9 = ((float) ((int) row["Pass"])) / 10f;
                float single10 = ((float) ((int) row["Rebound"])) / 10f;
                float single11 = ((float) ((int) row["Steal"])) / 10f;
                float single12 = ((float) ((int) row["Block"])) / 10f;
                float single13 = ((float) ((int) row["Attack"])) / 10f;
                float single14 = ((float) ((int) row["Defense"])) / 10f;
                float single15 = ((float) ((int) row["Team"])) / 10f;
                int num10 = (int) row["TrainPoint"];
                byte num39 = (byte) row["TrainType"];
                string strEvent = row["Event"].ToString();
                int intAbility = (int) row["Ability"];
                int intSuspend = (byte) row["Suspend"];
                bool flag = (bool) row["IsRetire"];
                int num13 = Convert.ToInt32(row["Happy"]);
                int num14 = (int) row["Speed"];
                int num15 = (int) row["Jump"];
                int num16 = (int) row["Strength"];
                int num17 = (int) row["Stamina"];
                int num18 = (int) row["Shot"];
                int num19 = (int) row["Point3"];
                int num20 = (int) row["Dribble"];
                int num21 = (int) row["Pass"];
                int num22 = (int) row["Rebound"];
                int num23 = (int) row["Steal"];
                int num24 = (int) row["Block"];
                int num25 = (int) row["SpeedMax"];
                int num26 = (int) row["JumpMax"];
                int num27 = (int) row["StrengthMax"];
                int num28 = (int) row["StaminaMax"];
                int num29 = (int) row["ShotMax"];
                int num30 = (int) row["Point3Max"];
                int num31 = (int) row["DribbleMax"];
                int num32 = (int) row["PassMax"];
                int num33 = (int) row["ReboundMax"];
                int num34 = (int) row["StealMax"];
                int num35 = (int) row["BlockMax"];
                int num36 = Convert.ToInt32(row["Category"]);
                int num37 = (((((((((num14 + num15) + num16) + num17) + num18) + num19) + num20) + num21) + num22) + num23) + num24;
                int num38 = (((((((((num25 + num26) + num27) + num28) + num29) + num30) + num31) + num32) + num33) + num34) + num35;
                if (this.intType == 3)
                {
                    str2 = "<td>" + num10 + "</td>";
                    if (((intStatus == 1) && (intPower > 30)) && ((num10 > this.MinPoint(row)) && (num37 < num38)))
                    {
                        str3 = "<td><a href='#' onclick='ChangeTrain(" + longPlayerID + ")'>训练</a></td>";
                    }
                    else
                    {
                        str3 = "<td align='center'>--</td>";
                    }
                }
                else
                {
                    str2 = "<td>" + num10 + "</td>";
                    str3 = "<td>";
                    if (((intPower > 30) && (num10 > 50)) && ((intStatus == 1) && (num37 < num38)))
                    {
                        if (num36 == 3)
                        {
                            obj2 = str3;
                            str3 = string.Concat(new object[] { obj2, "训练 | " });
                        }
                        else
                        {
                            obj2 = str3;
                            str3 = string.Concat(new object[] { obj2, "<a href='#' onclick='ChangeTrain(", longPlayerID, ")'>训练</a> | " });
                        }
                    }
                    else
                    {
                        str3 = str3 + "<font color='#666666'>训练</font> | ";
                    }
                    if (num36 != 3)
                    {
                        obj2 = str3;
                        str3 = string.Concat(new object[] { obj2, "<a href='Dialogue.aspx?PlayerID=", longPlayerID, "'>谈话</a></td>" });
                    }
                    else
                    {
                        obj2 = str3;
                        str3 = string.Concat(new object[] { obj2, "谈话</td>" });
                        intAbility = 0x3e7;
                    }
                }
                string str6 = "";
                string str7 = "";
                if ((num13 >= 0) && (num13 < 50))
                {
                    str6 = "6.gif";
                    str7 = "alt='坚决罢训'";
                }
                else if ((num13 >= 50) && (num13 < 0x41))
                {
                    str6 = "5.gif";
                    str7 = "alt='反感训练'";
                }
                else if ((num13 >= 0x41) && (num13 < 0x4b))
                {
                    str6 = "4.gif";
                    str7 = "alt='训练懈怠'";
                }
                else if ((num13 >= 0x4b) && (num13 < 0x55))
                {
                    str6 = "3.gif";
                    str7 = "alt='正常训练'";
                }
                else if ((num13 >= 0x55) && (num13 < 0x5f))
                {
                    str6 = "2.gif";
                    str7 = "alt='训练积极'";
                }
                else
                {
                    str6 = "1.gif";
                    str7 = "alt='渴望训练'";
                }
                obj2 = str;
                str = string.Concat(new object[] { obj2, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td><img src=\"", SessionItem.GetImageURL(), "Player/Number/", num3, ".gif\"></td><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, this.intType, 1, 1), "</td>" });
                if (this.intType == 5)
                {
                    str = str + "<td>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "</td>";
                }
                if (flag)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "<td><a href=\"javascript:;\" style=\"cursor:pointer; color:red\" title=\"球员将在赛季结束后退役\" >", num4, "!</a></td>" });
                }
                else
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, "<td>", num4, "</td>" });
                }
                string str8 = str;
                str = str8 + "<td><a title='" + PlayerItem.GetPlayerChsPosition(intPosition) + "' style='CURSOR: hand'>" + PlayerItem.GetPlayerEngPosition(intPosition) + "</td><td>" + PlayerItem.GetPowerColor(intPower) + "</td>";
                if (this.intType == 3)
                {
                    str8 = str;
                    str = str8 + "<td>" + PlayerItem.GetPlayerStatus(intStatus, strEvent, intSuspend) + "&nbsp;&nbsp;<img src='" + SessionItem.GetImageURL() + "Player/Face/" + str6 + "' " + str7 + " hight='12' width='12' border='0'></td>";
                }
                obj2 = str;
                str = string.Concat(new object[] { obj2, "<td>", num7, "</td><td>", num8, "</td><td>", PlayerItem.GetAbilityColor(intAbility), "</td>", str2 });
                str = str + str3 + "</tr>";
                if (this.intType == 3)
                {
                    str = str + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='12'></td></tr>";
                }
                else
                {
                    str = str + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='11'></td></tr>";
                }
                num++;
            }
            return str;
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private int MinPoint(DataRow dr)
        {
            int intAge = (byte) dr["Age"];
            int intAbility = (int) dr["Ability"];
            return this.UsePoint(1, intAbility, intAge);
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
                this.strMainScript = "";
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.btnOK.Visible = false;
                this.intType = SessionItem.GetRequest("Type", 0);
                if (((this.intCategory == 1) && (this.intType != 3)) && (this.intType != 6))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["MedicinePID"] != null)
            {
                this.Session.Remove("MedicinePID");
            }
            this.SetScript();
            this.SetPageIntro();
            this.SetList();
        }

        private void SetList()
        {
            DataTable playerTableByClubID;
            this.strList = "";
            switch (this.intType)
            {
                case 3:
                    playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID3);
                    this.strList = this.strList + "<tr class='BarHead'><td width='40'></td><td height='25' width='120'align='left' style='padding-left:3px'>姓名</td><td width='40'>年龄</td><td width='40'>位置</td><td width='40'>体力</td><td width='60'>状态</td><td width='43'>身高</td><td width='43'>体重</td><td width='34'>综合</td><td width='60'>训练点</td><td width='40'>操作</td></tr>";
                    this.strList = this.strList + this.GetPlayerList(playerTableByClubID);
                    this.strTools = "<p align=\"center\" style=\"color:red;\">购买“<a href=\"ManagerTool.aspx?Type=STORE&Page=1\">双倍训练卡</a>”可获得双倍训练点，“理疗中心”可以让球员恢复最佳状态。</p>";
                    return;

                case 4:
                    break;

                case 5:
                    playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(this.intClubID5);
                    this.strDevMsg = "<font color=\"red\">注：确认训练后，训练效果将在第二天显示。</font>";
                    this.strList = this.strList + "<tr class='BarHead'><td width='40'></td><td height='25' width='100' align='left' style='padding-left:3px'>姓名</td><td width='50'>状态</td><td width='33'>年龄</td><td width='33'>位置</td><td width='33'>体力</td><td width='33'>身高</td><td width='33'>体重</td><td width='50'>综合</td><td width='60'>训练点</td><td width='80'>操作</td></tr>";
                    SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                    this.strList = this.strList + this.GetPlayerList(playerTableByClubID);
                    this.GetManagerSay();
                    this.strTools = "";
                    break;

                default:
                    return;
            }
        }

        private void SetPageIntro()
        {
            string str;
            object strAddAllPower;
            if (this.intCategory == 1)
            {
                str = "<li class='qian2a'><font color='#aaaaaa'>职业队</font></li>";
            }
            else
            {
                str = "<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/TrainPlayer.aspx?Type=PLAYER5\"' href='TrainPlayerCenter.aspx?Type=5&UserID=" + this.intUserID + "'>职业队</a></li>";
            }
            if (this.intType == 3)
            {
                if (Config.GetPlayer3RecoverEnable() == 1)
                {
                    strAddAllPower = this.strAddAllPower;
                    this.strAddAllPower = string.Concat(new object[] { strAddAllPower, "<a href='SecretaryPage.aspx?PlayerType=", this.intType, "&Type=ADDPLAYERALL&Category=1' target='Center'><img src='", SessionItem.GetImageURL(), "allt.gif' border='0' height='24' width='64' align='absmiddle'></a>" });
                    strAddAllPower = this.strAddAllPower;
                    this.strAddAllPower = string.Concat(new object[] { strAddAllPower, "<img src='", SessionItem.GetImageURL(), "blanks.gif' border='0' height='0' width='5'><a href='SecretaryPage.aspx?PlayerType=", this.intType, "&Type=ADDPLAYERALL&Category=2' target='Center'><img src='", SessionItem.GetImageURL(), "allx.gif' border='0' height='24' width='64' align='absmiddle'></a>" });
                }
                else
                {
                    this.strAddAllPower = "";
                }
            }
            else if (Config.GetPlayer5RecoverEnable() == 1)
            {
                strAddAllPower = this.strAddAllPower;
                this.strAddAllPower = string.Concat(new object[] { strAddAllPower, "<img src='", SessionItem.GetImageURL(), "blanks.gif' border='0' height='0' width='5'><a href='SecretaryPage.aspx?PlayerType=", this.intType, "&Type=ADDPLAYERALL&Category=1' target='Center'><img src='", SessionItem.GetImageURL(), "allt.gif' border='0' height='24' width='64' align='absmiddle'></a>" });
            }
            else
            {
                this.strAddAllPower = "";
            }
            string str2 = "";
            int num = ToolItem.HasTool(this.intUserID, 9, 0);
            int num2 = ToolItem.HasTool(this.intUserID, 10, 0);
            if ((num > 0) || (num2 > 0))
            {
                str2 = "<span style='color:red;margin-left:130px;line-height:20px;margin-top:4px'><strong>双倍训练卡:</strong> 已使用 球员训练效果翻倍</span>";
            }
            else
            {
                str2 = "<span style='color:red;margin-left:30px;line-height:20px;margin-top:4px'><strong>双倍训练卡:</strong> 未使用 <a href=\"ManagerTool.aspx?Type=STORE&Page=1\">点此购买</a> 球员将获得双倍训练点数</span>";
            }
            switch (this.intType)
            {
                case 3:
                    this.strPageIntro = "<ul><li class='qian1'>街球队</li>" + str + "</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19' align='absmiddle'></a>" + str2;
                    return;

                case 5:
                    this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/TrainPlayer.aspx?Type=PLAYER3\"' href='TrainPlayerCenter.aspx?Type=3&UserID=", this.intUserID, "'>街球队</a></li><li class='qian2'>职业队</a></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>", str2 });
                    return;
            }
            this.strPageIntro = "<img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/PlayerCenter/Center_01.GIF' border='0' height='24' width='63'>" + str + "</ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img align='absmiddle' src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
        }

        public void SetScript()
        {
            this.strScript = "";
            if (this.intType == 3)
            {
                this.strScript = this.strScript + "function ChangeTrain(longPlayerID){window.top.Main.Right.location=\"Intro/TrainPlayer.aspx?Type=TPLAYER3\";window.location='TrainPlayer3.aspx?PID='+longPlayerID;}";
            }
            else
            {
                this.strScript = this.strScript + "function ChangeTrain(longPlayerID){window.top.Main.Right.location=\"Intro/TrainPlayer.aspx?Type=TPLAYER5\";window.location='TrainPlayer5.aspx?PID='+longPlayerID;}";
            }
        }

        private int UsePoint(int intAdd, int intAbility, int intAge)
        {
            int num = (intAbility * 40) / 0x1d1;
            int num2 = intAbility / 12;
            int num3 = ((((400 - (((30 - intAge) * 100) / 20)) + 100) * 200) - 0x1388) / 400;
            return ((((num * num2) + (num3 * 10)) * 11) / 700);
        }
    }
}


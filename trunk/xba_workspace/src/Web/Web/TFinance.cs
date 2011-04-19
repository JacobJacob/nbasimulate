﻿namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TFinance : Page
    {
        private int intPage;
        private int intPerPage;
        private int intSeason;
        private int intTurn;
        private int intUserID;
        private string strKind;
        public string strList;
        public string strlongMoney;
        private string strNickName;
        public string strPageIntro;
        public string strScript;
        private string strType;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            if (this.strType == "SEASON")
            {
                return BTPFinanceManager.GetAllTFinanceCountNew(this.intUserID);
            }
            return BTPFinanceManager.GetTurnTFinanceCountNew(this.intUserID, this.intSeason);
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            int total = this.GetTotal();
            int num2 = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
            {
                num2--;
            }
            if (num2 == 0)
            {
                num2 = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                strArray = new string[5];
                strArray[0] = "<a href='";
                strArray[1] = strCurrentURL;
                strArray[2] = "Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == num2)
            {
                str3 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str4 = str4 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str4 = str4 + " selected";
                }
                object obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            return string.Concat(new object[] { str2, " ", str3, " 共", total, "个记录 跳转", str4 });
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
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.strKind = (string) SessionItem.GetRequest("Kind", 1);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                this.intPerPage = 10;
                DataRow gameRow = BTPGameManager.GetGameRow();
                this.intSeason = (int) gameRow["Season"];
                this.intTurn = (int) gameRow["Turn"];
                switch (this.strType)
                {
                    case "TURN":
                        this.strPageIntro = "<ul><li class='qian1'>本赛季财政</li><li class='qian2a'><a href='TFinance.aspx?Status=1&Type=SEASON&Kind=SHOW&Page=1'>所有赛季</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.TurnList();
                        break;

                    case "SEASON":
                        this.strPageIntro = "<ul><li class='qian1a'><a href='TFinance.aspx?Status=1&Type=TURN&Kind=SHOW&Page=1'>本赛季财政</a></li><li class='qian2'>所有赛季</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.SeasonList();
                        break;

                    default:
                        this.strPageIntro = "<ul><li class='qian1'>本赛季财政</li><li class='qian2a'><a href='TFinance.aspx?Status=1&Type=SEASON&Kind=SHOW&Page=1'>所有赛季</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.TurnList();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SeasonList()
        {
            string strCurrentURL = "TFinance.aspx?Type=SEASON&Kind=SHOW&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            long num4 = (long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Money"];
            this.strlongMoney = "<strong>球队资金</strong>：" + Convert.ToString(num4);
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td width='60' height='25'>赛季</td><td width='96'>收入</td><td width='95'>支出</td><td width='110'>时间</td></tr>";
            SqlDataReader reader = BTPFinanceManager.GetAllTFinanceListNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num = (int) reader["Season"];
                    int num2 = (int) reader["Income"];
                    int num3 = (int) reader["Outcome"];
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='30'><font color='#7B1F76'>", num, "</font></td><td><font color='green'>", num2, "</font></td><td><font color='red'>", num3, "</font></td><td>", StringItem.FormatDate(datIn, "yy-MM-dd"), "</td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='4'></td></tr>";
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='4'>您暂时还没有财政记录！</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr></table>";
        }

        private void TurnList()
        {
            string strCurrentURL = "TFinance.aspx?Type=Turn&Kind=SHOW&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            long num6 = (long) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Money"];
            this.strlongMoney = "<strong>球队资金</strong>：" + Convert.ToString(num6);
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td width='50'  height='25'>赛季</td><td width='50'>轮数</td><td width='56'>收入</td><td width='55'>支出</td><td width='100'>时间</td><td width='50'>操作</td></tr>";
            SqlDataReader reader = BTPFinanceManager.GetTurnTFinanceListNew(this.intUserID, this.intSeason, this.intPage, this.intPerPage);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int num = (int) reader["Season"];
                    int num2 = (byte) reader["Turn"];
                    int num3 = (int) reader["Income"];
                    int num4 = (int) reader["Outcome"];
                    DateTime datIn = (DateTime) reader["CreateTime"];
                    int num5 = (int) reader["TFinanceID"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='30'><font color='#7B1F76'>", num, "</font></td><td><font color='#7B1F76'>", num2, "</font></td><td><font color='green'>", num3, "</font></td><td><font color='red'>", num4, "</font></td><td>", StringItem.FormatDate(datIn, "yy-MM-dd"), "</td><td><a href='TUFinance.aspx?Status=1&TFinanceID=", num5, "&Season=", num, "&Turn=", 
                        num2, "&Page=1' target='Right'>查看</a></td></tr>"
                     });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='20' colspan='6'>您暂时还没有财政记录！</td></tr>";
            }
            reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr></table>";
        }
    }
}


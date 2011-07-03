namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Honour : Page
    {
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strList;
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
            if (this.strType == "TEAM")
            {
                return BTPHonorManager.GetHonorCountNew(this.intUserID);
            }
            return BTPOldPlayerManager.GetHPlayerCountNew(this.intUserID);
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

        private void HonorList()
        {
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='25' colspan='10' align='right' style='padding-right:15px'><a href='Honour.aspx?UserID=" + this.intUserID + "&Type=PLAYER&Page=1'>返回名人堂</a></td></tr><tr class='BarHead'><td width='53' height='25'></td><td width='100' align='left' style='padding-left:3px'>姓名</td><td width='33'>号码</td><td width='50'>得分</td><td width='50'>篮板</td><td width='50'>助攻</td><td width='50'>抢断</td><td width='50'>盖帽</td><td width='50'>出场</td><td width='50'>查看</td></tr>";
            DataTable table = BTPOldPlayerManager.GetCanHonorPlayer3(this.intUserID);
            DataTable table2 = BTPOldPlayerManager.GetCanHonorPlayer5(this.intUserID);
            if ((table == null) && (table2 == null))
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='10'>您暂时没有可加入名人堂的球员！</td></tr>";
            }
            else
            {
                string str;
                string str2;
                int num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                int num7;
                long num8;
                object strList;
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        str = row["Name"].ToString().Trim();
                        num = (byte) row["Number"];
                        num2 = (int) row["LifeScore"];
                        num3 = (int) row["LifeRebound"];
                        num4 = (int) row["LifeAssist"];
                        num5 = (int) row["LifeSteal"];
                        num6 = (int) row["LifeBlock"];
                        num7 = (int) row["Played"];
                        byte num1 = (byte) row["Category"];
                        num8 = (long) row["PlayerID"];
                        str2 = "<a href='SecretaryPage.aspx?Type=HONOR&PlayerID=" + num8 + "&Category=3'>添加</a>";
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td><font color='#00cc00'>[ 街球 ]</font></td><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(num8, str, 3, 1, 1), "</td><td>", num, "</td><td>", num2, "</td><td>", num3, "</td><td>", num4, "</td><td>", num5, "</td><td>", num6, "</td><td>", 
                            num7, "</td><td>", str2, "</td></tr>"
                         });
                    }
                }
                if (table2 != null)
                {
                    foreach (DataRow row2 in table2.Rows)
                    {
                        str = row2["Name"].ToString().Trim();
                        num = (byte) row2["Number"];
                        num2 = (int) row2["LifeScore"];
                        num3 = (int) row2["LifeRebound"];
                        num4 = (int) row2["LifeAssist"];
                        num5 = (int) row2["LifeSteal"];
                        num6 = (int) row2["LifeBlock"];
                        num7 = (int) row2["Played"];
                        byte num9 = (byte) row2["Category"];
                        num8 = (long) row2["PlayerID"];
                        str2 = "<a href='SecretaryPage.aspx?Type=HONOR&PlayerID=" + num8 + "&Category=5'>添加</a>";
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td><font color='#FF0000'>[ 职业 ]</font></td><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(num8, str, 5, 1, 1), "</td><td>", num, "</td><td>", num2, "</td><td>", num3, "</td><td>", num4, "</td><td>", num5, "</td><td>", num6, "</td><td>", 
                            num7, "</td><td>", str2, "</td></tr>"
                         });
                    }
                }
            }
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
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                this.intPerPage = 10;
                switch (this.strType)
                {
                    case "TEAM":
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1'>球队荣誉</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Honour.aspx?Type=PLAYER\"' href='Honour.aspx?UserID=", this.intUserID, "&Type=PLAYER&Page=1'>名人堂</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.TeamList();
                        break;

                    case "PLAYER":
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Honour.aspx?Type=TEAM\"' href='Honour.aspx?UserID=", this.intUserID, "&Type=TEAM&Page=1'>球队荣誉</a></li><li class='qian2'>名人堂</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.PlayerList();
                        break;

                    case "HONOR":
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Honour.aspx?Type=TEAM\"' href='Honour.aspx?UserID=", this.intUserID, "&Type=TEAM&Page=1'>球队荣誉</a></li><li class='qian2'>名人堂</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.HonorList();
                        break;

                    default:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='qian1'>球队荣誉</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Honour.aspx?Type=PLAYER\"' href='Honour.aspx?UserID=", this.intUserID, "&Type=PLAYER&Page=1'>名人堂</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>" });
                        this.TeamList();
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void PlayerList()
        {
            string strCurrentURL = "Honor.aspx?Type=PLAYER&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 4;
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td height='25' colspan='10' align='right' style='padding-right:15px'><a href='Honour.aspx?UserID=" + this.intUserID + "&Type=HONOR&Page=1'>添加球员</a></td></tr><tr class='BarHead'><td width='53' height='25'></td><td width='100' align='left' style='padding-left:3px'>姓名</td><td width='33'>号码</td><td width='50'>得分</td><td width='50'>篮板</td><td width='50'>助攻</td><td width='50'>抢断</td><td width='50'>盖帽</td><td width='50'>出场</td><td width='50'>查看</td></tr>";
            DataTable reader = BTPOldPlayerManager.GetHPlayerListNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str5;
                    string strName = row["Name"].ToString().Trim();
                    int num = (byte) row["Number"];
                    int num2 = (int) row["LifeScore"];
                    int num3 = (int) row["LifeRebound"];
                    int num4 = (int) row["LifeAssist"];
                    int num5 = (int) row["LifeSteal"];
                    int num6 = (int) row["LifeBlock"];
                    int num7 = (int) row["Played"];
                    int num8 = (byte) row["Category"];
                    long longPlayerID = (long) row["PlayerID"];
                    string str3 = "<a href='ShowPlayer.aspx?Type=6&Kind=1&Check=0&PlayerID=" + longPlayerID + "' target='Right'>查看</a>";
                    string str4 = row["Remark"].ToString().Trim();
                    if (num8 == 2)
                    {
                        str5 = "<font color='#FF0000'>[ 职业 ]</font>";
                    }
                    else
                    {
                        str5 = "<font color='#00cc00'>[ 街球 ]</font>";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { 
                        strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td>", str5, "</td><td height='25'align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 6, 1, 0), "</td><td>", num, "</td><td>", num2, "</td><td>", num3, "</td><td>", num4, "</td><td>", num5, "</td><td>", 
                        num6, "</td><td>", num7, "</td><td>", str3, "</td></tr><tr><td colspan='2' height='50' style='font-color:#AF1F30' align='center'>介绍：</td><td colspan='8'>", str4, "</td></tr>"
                     });
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='10'>您暂时没有添加任何荣誉球员！</td></tr>";
            }
            //reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='10'>" + this.GetViewPage(strCurrentURL) + "</td></tr></table>";
        }

        private void TeamList()
        {
            string strCurrentURL = "Honour.aspx?Type=TEAM&";
            this.intPerPage = 6;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr class='BarHead'><td width='178' height='25'>奖杯</td><td width='179'>荣誉</td><td width='179'>获得时间</td></tr>";
            DataTable reader = BTPHonorManager.GetHonorListNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str2 = row["BigLogo"].ToString().Trim();
                    string str3 = row["Remark"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str4 = row["Devision"].ToString().Trim();
                    if (str4 != "")
                    {
                        str3 = str3 + "<br/>" + str4;
                    }
                    else
                    {
                        str3 = str3 + "<br/>" + StringItem.FormatDate(datIn, "yyMMdd");
                    }
                    string strList = this.strList;
                    this.strList = strList + "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><img src='" + SessionItem.GetImageURL() + "Cup/" + str2 + "'></td><td>" + str3 + "</td><td>" + StringItem.FormatDate(datIn, "yy-MM-dd") + "</td></tr>";
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='3'></td></tr>";
                }
            }
            else
            {
                this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='3'>你暂时没有获得任何荣誉！</td></tr>";
            }
            //reader.Close();
            this.strList = this.strList + "<tr><td height='25' align='right' colspan='3'>" + this.GetViewPage(strCurrentURL) + "</td></tr></table>";
        }
    }
}


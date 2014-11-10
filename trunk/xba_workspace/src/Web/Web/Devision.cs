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

    public class Devision : Page
    {
        protected ImageButton btnGo;
        protected ImageButton btnSend;
        protected DropDownList DropDownList1;
        protected DropDownList DropDownList2;
        private int intCategory;
        private int intClubID;
        private int intPage;
        private int intPerPage;
        private int intStatus;
        private int intUserID;
        public StringBuilder sbList = new StringBuilder("");
        public string strDevCode;
        public string strDownList;
        public string strIntro;
        public string strLeftList;
        public string strList;
        public string strNextList;
        private string strNickName;
        public string strPageIntro;
        public string strRightList;
        public string strScript;
        private string strType;
        public string strUPList;
        protected TextBox tbContent;
        protected TextBox tbDev;
        protected TextBox tbLevel;
        protected HtmlTable tblList;
        protected HtmlTable tblPic;
        protected HtmlTable tblRival;
        protected HtmlTable tblStat;
        protected HtmlTable tblView;
        protected HtmlTableCell tdDDL;

        private void btnGo_Click(object sender, ImageClickEventArgs e)
        {
            int num;
            string devCode = "";
            int index = 0;
            int level = 0;
            try
            {
                level = Convert.ToInt32(this.tbLevel.Text);
                index = Convert.ToInt32(this.tbDev.Text);
                num = 1;
                devCode = DevCalculator.GetDevCode(level, index);
                if (index > Math.Pow(2.0, (double) (level - 1)))
                {
                    num = 0;
                }
            }
            catch
            {
                num = 0;
            }
            switch (num)
            {
                case 0:
                    base.Response.Redirect("Report.aspx?Parameter=105!Type." + this.strType + "^Devision." + this.strDevCode + "^Page.1");
                    return;

                case 1:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=", this.strType, "&Devision=", devCode, "&Page=1" }));
                    return;
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            if (this.strType == "PIC")
            {
                string strDevCode = SessionItem.GetRequest("Devision", 1).ToString().Trim();
                return BTPDevMatchManager.GetDevLogCount(1, strDevCode);
            }
            return BTPDevManager.GetClubCountByDevCode(this.strDevCode);
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
            string str = "";
            if (this.intPage == 1)
            {
                str = "上一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage - 1).ToString(), "'>上一页</a>" };
                str = string.Concat(strArray);
            }
            string str2 = "";
            if (this.intPage == num2)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            string str3 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str3 = str3 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str3 = str3 + " selected";
                }
                object obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            return string.Concat(new object[] { str, " ", str2, " 共", total, "个记录 跳转", str3 });
        }

        private void InitializeComponent()
        {
            this.btnGo.Click += new ImageClickEventHandler(this.btnGo_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                SessionItem.CheckCanUseAfterUpdate(5);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strType = SessionItem.GetRequest("Type", 1);
                this.strDevCode = SessionItem.GetRequest("Devision", 1);
                this.tblList.Visible = false;
                this.tblStat.Visible = false;
                this.tblRival.Visible = false;
                this.tblView.Visible = false;
                this.tdDDL.Visible = false;
                this.tblPic.Visible = false;
                if (!base.IsPostBack)
                {
                    this.tbLevel.Text = DevCalculator.GetLevel(this.strDevCode).ToString();
                    this.tbDev.Text = DevCalculator.GetDevIndex(this.strDevCode).ToString();
                }
                this.btnGo.ImageUrl = SessionItem.GetImageURL() + "button_go.gif";
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetList()
        {
            string strCurrentURL = string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&" });
            this.intPerPage = 14;
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            int num = 1;
            string str2 = "";
            DataTable clubTableByDevCode = BTPDevManager.GetClubTableByDevCode(this.strDevCode);
            if (clubTableByDevCode == null)
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>第" + DevCalculator.GetDev(this.strDevCode) + "联赛暂时没有职业球队</td></tr>";
            }
            else
            {
                foreach (DataRow row in clubTableByDevCode.Rows)
                {
                    string str3;
                    int count = clubTableByDevCode.Rows.Count;
                    int num3 = (int) row["Win"];
                    int num4 = (int) row["Lose"];
                    int num5 = (int) row["Score"];
                    int intClubID = (int) row["ClubID"];
                    if (intClubID == 0)
                    {
                        str3 = "空缺";
                    }
                    else
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(intClubID, 5, "Right", 0x11);
                    }
                    if (num < 3)
                    {
                        str2 = "#fce5d2";
                    }
                    else if (num > (count - 4))
                    {
                        str2 = "#e7e7e7";
                    }
                    else
                    {
                        str2 = "";
                    }
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' bgColor='", str2, "' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", num, "</font></td><td  align='left'>", str3, "</td><td>", num3, "</td><td>", num4, "</td><td>", num5, "</td><td></td></tr>" });
                    this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                    num++;
                }
            }
        }

        private void SetPageIntro()
        {
            if (this.intCategory == 5)
            {
                switch (this.strType)
                {
                    case "LIST":
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2'>联赛排名</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.SetList();
                        this.tblList.Visible = true;
                        this.tdDDL.Visible = true;
                        return;

                    case "VIEW":
                        base.Response.Redirect(string.Concat(new object[] { "DevisionView.aspx?UserID=", this.intUserID, "&Type=VIEW&Devision=", this.strDevCode, "&Page=1" }));
                        return;

                    case "STAT":
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2'>技术统计</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.SetStat();
                        this.tblStat.Visible = true;
                        this.tdDDL.Visible = true;
                        return;

                    case "RIVAL":
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2'>上轮/本轮</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.SetRival();
                        this.tblRival.Visible = true;
                        this.tdDDL.Visible = true;
                        return;

                    case "PIC":
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, 
                            "&Page=1'>联赛排名</a></li><li class='qian2'>联赛日志</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.SetsbList();
                        this.tblPic.Visible = true;
                        this.tdDDL.Visible = true;
                        return;
                }
                base.Response.Redirect(string.Concat(new object[] { "DevisionView.aspx?UserID=", this.intUserID, "&Type=VIEW&Devision=", this.strDevCode, "&Page=1" }));
            }
            else
            {
                string strType = this.strType;
                if (strType != null)
                {
                    switch (strType)
                    {
                        case "LIST":
                            this.strPageIntro = string.Concat(new object[] { 
                                "<img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_02.GIF' border='0' height='24' width='72'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_04.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_C_03.GIF' border='0' height='24' width='71'></a><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_01.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1'><img src='", SessionItem.GetImageURL(), 
                                "MenuCard/Devision/Devision_C_05.GIF' border='0' height='24' width='71'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                             });
                            this.SetList();
                            this.tblList.Visible = true;
                            this.tdDDL.Visible = true;
                            return;

                        case "STAT":
                            this.strPageIntro = string.Concat(new object[] { 
                                "<img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_02.GIF' border='0' height='24' width='72'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_04.GIF' border='0' height='24' width='71'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_03.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_C_01.GIF' border='0' height='24' width='71'></a><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1'><img src='", SessionItem.GetImageURL(), 
                                "MenuCard/Devision/Devision_C_05.GIF' border='0' height='24' width='71'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                             });
                            this.SetStat();
                            this.tblStat.Visible = true;
                            this.tdDDL.Visible = true;
                            return;

                        case "PIC":
                            this.strPageIntro = string.Concat(new object[] { 
                                "<img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_02.GIF' border='0' height='24' width='72'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_04.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_C_03.GIF' border='0' height='24' width='71'></a><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'><img src='", SessionItem.GetImageURL(), 
                                "MenuCard/Devision/Devision_C_01.GIF' border='0' height='24' width='71'></a><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_05.GIF' border='0' height='24' width='71'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                             });
                            this.SetsbList();
                            this.tblPic.Visible = true;
                            this.tdDDL.Visible = true;
                            return;
                    }
                }
                this.strPageIntro = string.Concat(new object[] { 
                    "<img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_02.GIF' border='0' height='24' width='72'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_G_04.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_C_03.GIF' border='0' height='24' width='71'></a><img src='", SessionItem.GetImageURL(), "MenuCard/Devision/Devision_01.GIF' border='0' height='24' width='71'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1'><img src='", SessionItem.GetImageURL(), 
                    "MenuCard/Devision/Devision_C_05.GIF' border='0' height='24' width='71'></a><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                 });
                this.SetList();
                this.tblList.Visible = true;
                this.tdDDL.Visible = true;
            }
        }

        private void SetRival()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string strUPList;
            int intRound = (int) BTPGameManager.GetGameRow()["Turn"];
            DataTable devMatchTableByRound = BTPDevMatchManager.GetDevMatchTableByRound(intRound - 1, this.strDevCode);
            if (devMatchTableByRound == null)
            {
                this.strUPList = "<tr class='BarContent'><td height='25'>无比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in devMatchTableByRound.Rows)
                {
                    int num1 = (int) row["Round"];
                    num = (int) row["DevMatchID"];
                    num2 = (int) row["ClubHID"];
                    num4 = (int) row["ClubAID"];
                    num3 = (int) row["ClubHScore"];
                    num5 = (int) row["ClubAScore"];
                    if (((num2 != 0) && (num4 != 0)) && ((num3 != 0) || (num5 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num, "&A=", num2, "&B=", num4, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num, "&A=", num2, "&B=", num4, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str = "";
                        str2 = "";
                    }
                    str3 = "";
                    str4 = "";
                    if ((num3 == 0) && (num5 == 0))
                    {
                        str5 = "<td width='20' align='center' cospan='3'>--</td>";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<td width='20' align='right'>", num3, "</td><td width='10' align='center'>:</td><td width='20' align='left'>", num5, "</td>" });
                    }
                    if (num2 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num2, 5, 12, num);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num4 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num4, 5, 12, num);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    strUPList = this.strUPList;
                    this.strUPList = strUPList + "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' width='90' align='right'>" + str3 + "</td>" + str5 + "<td width='90' align='left'>" + str4 + "</td><td width='40'>" + str + "&nbsp;" + str2 + "</td></tr>";
                    this.strUPList = this.strUPList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
            DataTable table2 = BTPDevMatchManager.GetDevMatchTableByRound(intRound, this.strDevCode);
            if (table2 == null)
            {
                this.strDownList = "<tr class='BarContent'><td height='25'>无比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in table2.Rows)
                {
                    int num7 = (int) row2["Round"];
                    num = (int) row2["DevMatchID"];
                    num2 = (int) row2["ClubHID"];
                    num4 = (int) row2["ClubAID"];
                    num3 = (int) row2["ClubHScore"];
                    num5 = (int) row2["ClubAScore"];
                    if (((num2 != 0) && (num4 != 0)) && ((num3 != 0) || (num5 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num, "&A=", num2, "&B=", num4, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num, "&A=", num2, "&B=", num4, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str = "";
                        str2 = "";
                    }
                    str3 = "";
                    str4 = "";
                    if ((num3 == 0) && (num5 == 0))
                    {
                        str5 = "<td width='20' align='center' cospan='3'>--</td>";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<td width='20' align='right'>", num3, "</td><td width='10' align='center'>:</td><td width='20' align='left'>", num5, "</td>" });
                    }
                    if (num2 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num2, 5, 12, num);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num4 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num4, 5, 12, num);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    strUPList = this.strDownList;
                    this.strDownList = strUPList + "<tr class='BarContent' bgColor='#FBE2D4' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' width='90' align='right'>" + str3 + "</td>" + str5 + "<td width='90' align='left'>" + str4 + "</td><td width='40'>" + str + "&nbsp;" + str2 + "</td></tr>";
                    this.strDownList = this.strDownList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>";
                }
            }
        }

        private void SetsbList()
        {
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            string strDevCode = "";
            strDevCode = SessionItem.GetRequest("Devision", 1).ToString().Trim();
            if (strDevCode == string.Empty)
            {
                strDevCode = accountRowByUserID["DevCode"].ToString().Trim();
            }
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage <= 0)
            {
                this.intPage = 1;
            }
            this.intPerPage = 10;
            DataTable table = BTPDevMatchManager.GetDevLogByDevCode(0, this.intPage, this.intPerPage, strDevCode);
            int num = 1;
            string str2 = "";
            if (table != null)
            {
                foreach (DataRow row2 in table.Rows)
                {
                    row2["NickName"].ToString().Trim();
                    row2["ClubName"].ToString().Trim();
                    string str3 = row2["LogEvent"].ToString().Trim();
                    int intClubID = (int) row2["ClubID"];
                    DateTime datIn = (DateTime) row2["CreateTime"];
                    if ((num++ % 2) == 1)
                    {
                        str2 = "#FBE2D4";
                    }
                    else
                    {
                        str2 = "";
                    }
                    this.sbList.Append("<tr bgcolor='" + str2 + "'>");
                    this.sbList.Append("<td align=\"center\" >" + StringItem.FormatDate(datIn, "MM-dd hh:mm") + "</td>");
                    this.sbList.Append("<td height=\"25\" style=\"padding:2px;\" align=\"left\">" + BTPClubManager.GetClubNameByClubID(intClubID, 5, "Right", 20) + "</td>");
                    this.sbList.Append("<td align=\"left\" style=\"padding:2px;\">" + str3 + "</td></tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='3'></td></tr>");
                }
                this.sbList.Append("<tr><td align=right colspan=3>" + this.GetViewPage(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Devision=", strDevCode, "&" })) + "</td></tr>");
                this.strScript = this.GetScript(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Devision=", strDevCode, "&" }));
            }
            else
            {
                this.sbList.Append("<tr><td colspan=3 height='25' align='center' style=\"padding:2px;\">暂无联赛日志</td></tr>");
            }
        }

        private void SetStat()
        {
            string str;
            string str2;
            string playerEngPosition;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            float num8;
            DataTable mVPTable;
            object strList;
            this.intStatus = SessionItem.GetRequest("Status", 0);
            switch (this.intStatus)
            {
                case 1:
                    this.strIntro = string.Concat(new object[] { 
                        "<font color='red'>MVP</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='63'>身高</td><td width='63'>体重</td><td width='70'>位置</td><td width='100'>所在球队</td><td width='70'>综合</td><td width='70'>贡献</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetMVPTable(this.strDevCode);
                    if (mVPTable != null)
                    {
                        foreach (DataRow row in mVPTable.Rows)
                        {
                            str2 = row["Name"].ToString().Trim();
                            num = (byte) row["Height"];
                            num2 = (byte) row["Weight"];
                            num3 = (byte) row["Pos"];
                            num4 = (int) row["ClubID"];
                            str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                            playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                            num6 = (int) row["Ability"];
                            float single1 = ((float) num6) / 10f;
                            long num1 = (long) row["PlayerID"];
                            num7 = (int) row["MVPValue"];
                            strList = this.strList;
                            this.strList = string.Concat(new object[] { 
                                strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                                num7, "</td></tr>"
                             });
                            this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>";
                        }
                        break;
                    }
                    this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有球员。</td></tr>";
                    return;

                case 2:
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <font color='red'>得分</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='43'>身高</td><td width='43'>体重</td><td width='43'>位置</td><td width='100'>所在球队</td><td width='40'>综合</td><td width='50'>总得分</td><td width='50'>总出场</td><td width='67'>场均得分</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetTop12Table(this.strDevCode);
                    if (mVPTable != null)
                    {
                        foreach (DataRow row2 in mVPTable.Rows)
                        {
                            str2 = row2["Name"].ToString().Trim();
                            num = (byte) row2["Height"];
                            num2 = (byte) row2["Weight"];
                            num3 = (byte) row2["Pos"];
                            int num9 = (int) row2["SeasonScore"];
                            num5 = (int) row2["SeasonPlayed"];
                            num4 = (int) row2["ClubID"];
                            num6 = (int) row2["Ability"];
                            float single2 = ((float) num6) / 10f;
                            str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                            playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                            if (num5 != 0)
                            {
                                num8 = ((float) ((num9 * 100) / num5)) / 100f;
                            }
                            else
                            {
                                num8 = 0f;
                            }
                            long num15 = (long) row2["PlayerID"];
                            strList = this.strList;
                            this.strList = string.Concat(new object[] { 
                                strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                                num9, "</td><td>", num5, "</td><td>", num8, "</td></tr>"
                             });
                            this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                        }
                        break;
                    }
                    this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                    return;

                case 3:
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <font color='red'>篮板</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='43'>身高</td><td width='43'>体重</td><td width='43'>位置</td><td width='100'>所在球队</td><td width='40'>综合</td><td width='50'>总篮板</td><td width='50'>总出场</td><td width='67'>场均篮板</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetReboundTop12Table(this.strDevCode);
                    if (mVPTable == null)
                    {
                        this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                        return;
                    }
                    foreach (DataRow row3 in mVPTable.Rows)
                    {
                        str2 = row3["Name"].ToString().Trim();
                        num = (byte) row3["Height"];
                        num2 = (byte) row3["Weight"];
                        num3 = (byte) row3["Pos"];
                        int num10 = (int) row3["SeasonRebound"];
                        num5 = (int) row3["SeasonPlayed"];
                        num4 = (int) row3["ClubID"];
                        num6 = (int) row3["Ability"];
                        float single3 = ((float) num6) / 10f;
                        str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                        playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                        if (num5 != 0)
                        {
                            num8 = ((float) ((num10 * 100) / num5)) / 100f;
                        }
                        else
                        {
                            num8 = 0f;
                        }
                        long num16 = (long) row3["PlayerID"];
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                            num10, "</td><td>", num5, "</td><td>", num8, "</td></tr>"
                         });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                    }
                    break;

                case 4:
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <font color='red'>助攻</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='43'>身高</td><td width='43'>体重</td><td width='43'>位置</td><td width='100'>所在球队</td><td width='40'>综合</td><td width='50'>总助攻</td><td width='50'>总出场</td><td width='67'>场均助攻</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetAssistTop12Table(this.strDevCode);
                    if (mVPTable == null)
                    {
                        this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                        return;
                    }
                    foreach (DataRow row4 in mVPTable.Rows)
                    {
                        str2 = row4["Name"].ToString().Trim();
                        num = (byte) row4["Height"];
                        num2 = (byte) row4["Weight"];
                        num3 = (byte) row4["Pos"];
                        int num11 = (int) row4["SeasonAssist"];
                        num5 = (int) row4["SeasonPlayed"];
                        num4 = (int) row4["ClubID"];
                        num6 = (int) row4["Ability"];
                        float single4 = ((float) num6) / 10f;
                        str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                        playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                        if (num5 != 0)
                        {
                            num8 = ((float) ((num11 * 100) / num5)) / 100f;
                        }
                        else
                        {
                            num8 = 0f;
                        }
                        long num17 = (long) row4["PlayerID"];
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                            num11, "</td><td>", num5, "</td><td>", num8, "</td></tr>"
                         });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                    }
                    break;

                case 5:
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=4&Page=1'>助攻</a> | <font color='red'>抢断</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='43'>身高</td><td width='43'>体重</td><td width='43'>位置</td><td width='100'>所在球队</td><td width='40'>综合</td><td width='50'>总抢断</td><td width='50'>总出场</td><td width='67'>场均抢断</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetStealTop12Table(this.strDevCode);
                    if (mVPTable == null)
                    {
                        this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                        return;
                    }
                    foreach (DataRow row5 in mVPTable.Rows)
                    {
                        str2 = row5["Name"].ToString().Trim();
                        num = (byte) row5["Height"];
                        num2 = (byte) row5["Weight"];
                        num3 = (byte) row5["Pos"];
                        int num12 = (int) row5["SeasonSteal"];
                        num5 = (int) row5["SeasonPlayed"];
                        num4 = (int) row5["ClubID"];
                        num6 = (int) row5["Ability"];
                        float single5 = ((float) num6) / 10f;
                        str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                        playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                        if (num5 != 0)
                        {
                            num8 = ((float) ((num12 * 100) / num5)) / 100f;
                        }
                        else
                        {
                            num8 = 0f;
                        }
                        long num18 = (long) row5["PlayerID"];
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                            num12, "</td><td>", num5, "</td><td>", num8, "</td></tr>"
                         });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                    }
                    break;

                case 6:
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=5&Page=1'>抢断</a> | <font color='red'>封盖</font> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='43'>身高</td><td width='43'>体重</td><td width='43'>位置</td><td width='100'>所在球队</td><td width='40'>综合</td><td width='50'>总封盖</td><td width='50'>总出场</td><td width='67'>场均封盖</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetBlockTop12Table(this.strDevCode);
                    if (mVPTable == null)
                    {
                        this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                        return;
                    }
                    foreach (DataRow row6 in mVPTable.Rows)
                    {
                        str2 = row6["Name"].ToString().Trim();
                        num = (byte) row6["Height"];
                        num2 = (byte) row6["Weight"];
                        num3 = (byte) row6["Pos"];
                        int num13 = (int) row6["SeasonBlock"];
                        num5 = (int) row6["SeasonPlayed"];
                        num4 = (int) row6["ClubID"];
                        num6 = (int) row6["Ability"];
                        float single6 = ((float) num6) / 10f;
                        str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                        playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                        if (num5 != 0)
                        {
                            num8 = ((float) ((num13 * 100) / num5)) / 100f;
                        }
                        else
                        {
                            num8 = 0f;
                        }
                        long num19 = (long) row6["PlayerID"];
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                            num13, "</td><td>", num5, "</td><td>", num8, "</td></tr>"
                         });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                    }
                    break;

                case 7:
                    base.Response.Redirect(string.Concat(new object[] { "DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1" }));
                    return;

                default:
                    this.strIntro = string.Concat(new object[] { 
                        "<font color='red'>MVP</font> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <a href='DevisionView.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=7&Page=1'>本队统计</a>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100'>姓名</td><td width='63'>身高</td><td width='63'>体重</td><td width='70'>位置</td><td width='100'>所在球队</td><td width='70'>综合</td><td width='70'>贡献</td></tr>";
                    mVPTable = BTPPlayer5Manager.GetMVPTable(this.strDevCode);
                    if (mVPTable == null)
                    {
                        this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='7'>暂时没有球员。</td></tr>";
                        return;
                    }
                    foreach (DataRow row7 in mVPTable.Rows)
                    {
                        str2 = row7["Name"].ToString().Trim();
                        num = (byte) row7["Height"];
                        num2 = (byte) row7["Weight"];
                        num3 = (byte) row7["Pos"];
                        num4 = (int) row7["ClubID"];
                        str = BTPClubManager.GetClubNameByClubID(num4, 5, "Right", 13);
                        playerEngPosition = PlayerItem.GetPlayerEngPosition(num3);
                        num6 = (int) row7["Ability"];
                        float single7 = ((float) num6) / 10f;
                        long num20 = (long) row7["PlayerID"];
                        num7 = (int) row7["MVPValue"];
                        strList = this.strList;
                        this.strList = string.Concat(new object[] { 
                            strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color='#660066'>", str2, "</font></td><td>", num, "</td><td>", num2, "</td><td><a title='", PlayerItem.GetPlayerChsPosition(num3), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td align='left'>", str, "</td><td>", PlayerItem.GetAbilityColor(num6), "</td><td>", 
                            num7, "</td></tr>"
                         });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>";
                    }
                    break;
            }
        }

        private void SetView()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            object strUPList;
            DataTable uPTable = BTPDevMatchManager.GetUPTable(this.intClubID, this.strDevCode);
            if (uPTable == null)
            {
                this.strUPList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in uPTable.Rows)
                {
                    num = (int) row["Round"];
                    num2 = (int) row["DevMatchID"];
                    num3 = (int) row["ClubHID"];
                    num5 = (int) row["ClubAID"];
                    num4 = (int) row["ClubHScore"];
                    num6 = (int) row["ClubAScore"];
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str = "";
                        str2 = "";
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, "Right", 9);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, "Right", 9);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        str5 = "--";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strUPList;
                    this.strUPList = string.Concat(new object[] { strUPList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "&nbsp;", str2, "</td></tr>" });
                }
            }
            DataTable downTable = BTPDevMatchManager.GetDownTable(this.intClubID, this.strDevCode);
            if (downTable == null)
            {
                this.strDownList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in downTable.Rows)
                {
                    num = (int) row2["Round"];
                    num2 = (int) row2["DevMatchID"];
                    num3 = (int) row2["ClubHID"];
                    num5 = (int) row2["ClubAID"];
                    num4 = (int) row2["ClubHScore"];
                    num6 = (int) row2["ClubAScore"];
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' title='战报' target='_blank'><img src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' title='统计' target='_blank'><img src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else
                    {
                        str = "";
                        str2 = "";
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, "Right", 9);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, "Right", 9);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        str5 = "--";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { strUPList, "<tr class='BarContent' bgColor='#FBE2D4' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "&nbsp;", str2, "</td></tr>" });
                }
            }
        }
    }
}


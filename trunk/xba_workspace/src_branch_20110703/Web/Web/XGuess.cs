namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class XGuess : Page
    {

        private int intPage;
        private int intPerPage;
        private int intTotal;
        public string strType;
        private int intUserID;
        public string strScript;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        protected HtmlTable tblUserAbilityTop;
        protected HtmlTable tbPlayerList;

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
                this.intPage = Convert.ToInt32(SessionItem.GetRequest("Page", 0));
                if (this.intPage <= 0)
                {
                    this.intPage = 1;
                }
                this.intPerPage = 13;
                this.strType = (string)SessionItem.GetRequest("Type", 1);
                if (this.strType == "1")
                {
                    this.strType = "CLUBLIST";
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
            this.SetList();
        }

        private string GetViewPage(string strCurrentURL)
        {
            string str5;
            string[] strArray;
            int msgTotal = this.intTotal;
            int totalPage = (msgTotal / this.intPerPage) + 1;
            if ((msgTotal % this.intPerPage) == 0)
            {
                totalPage--;
            }
            if (totalPage == 0)
            {
                totalPage = 1;
            }
            string str2 = "";
            if (this.intPage == 1)
            {
                str2 = "上一页";
            }
            else
            {
                str5 = str2;
                strArray = new string[6];
                strArray[0] = str5;
                strArray[1] = "<a href='";
                strArray[2] = strCurrentURL;
                strArray[3] = "Page=";
                int num4 = this.intPage - 1;
                strArray[4] = num4.ToString();
                strArray[5] = "'>上一页</a>";
                str2 = string.Concat(strArray);
            }
            string str3 = "";
            if (this.intPage == totalPage)
            {
                str3 = str3 + "下一页";
            }
            else
            {
                str5 = str3;
                strArray = new string[] { str5, "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str3 = string.Concat(strArray);
            }
            string str4 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= totalPage; i++)
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
            return string.Concat(new object[] { str2, " ", str3, " 共", msgTotal, "个记录 跳转", str4 });
        }

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }


        private void SetList()
        {
            string strCurrentURL = null;

            DataTable dataTable;
            switch (this.strType)
            {
                case "CLUBLIST":

                    strCurrentURL = "XGuess.aspx?Type=CLUBLIST&" ;

                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">排名</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">球队名</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">夺冠赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">下注</td>");
                    this.sbList.Append("</tr>");

                    this.intTotal = BTPXGuessManager.GetXGuessCount();
                    if (this.intTotal > 0)
                    {
                        dataTable = BTPXGuessManager.GetXGuess(this.intPage, this.intPerPage);
                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='4'>暂无数据</td></tr>");
                            return;
                        }
                        int count = 1;
                        foreach (DataRow row in dataTable.Rows)
                        {

                            string strClubName = Convert.ToString(row["ClubName"]);
                            int intClubID = Convert.ToInt32(row["ClubID"]);
                            int intUserID = Convert.ToInt32(row["UserID"]);
                            int intGuessID = Convert.ToInt32(row["GuessID"]);
                            double floatOdds = Convert.ToDouble(row["Odds"]);
                            int intSort = (this.intPage - 1) * this.intPerPage + count;
                            count++;

                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intSort + "</td>");
                            this.sbList.Append("<td align=\"left\" ><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");

                            this.sbList.Append(" <td align=\"center\">" + floatOdds + "</td>");

                            this.sbList.Append("<td align=\"center\"><a href='SecretaryPage.aspx?Type=XGUESS&GuessID=" + intGuessID + "'>下注</a></td>");

                            this.sbList.Append("</tr>");
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");
                            
                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);
                            
                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='4'>暂无数据</td></tr>");

                    }
                    return;

                case "MYGUESS":

                    strCurrentURL = "MYGUESS.aspx?Type=MYGUESS&";

                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\" height=\"24\">序号</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" height=\"24\">竞猜冠军</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">竞猜资金</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">盈亏</td>");
                    //this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">状态</td>");
                    this.sbList.Append("</tr>");

                    this.intTotal = BTPXGuessManager.GetXGuessRecordCount(this.intUserID);
                    if (this.intTotal > 0)
                    {
                        dataTable = BTPXGuessManager.GetXGuessRecord(this.intUserID, this.intPage, this.intPerPage);
               
                        if (dataTable == null)
                        {
                            this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                            return;
                        }

                        int count = 1;

                        foreach (DataRow row in dataTable.Rows)
                        {

                     
                            int intUserID = Convert.ToInt32(row["UserID"]);
                            string strClubName = Convert.ToString(row["ClubName"]);
                            double douOdds = Convert.ToDouble(row["Odds"]);
                            int intClubID = Convert.ToInt32(row["ClubID"]);
                            int intMoney = Convert.ToInt32(row["Money"]);
                            int intStatus = Convert.ToInt32(row["Status"]);

                            int intSort = (this.intPage - 1) * this.intPerPage + count;
                            count++;

                            DataRow accountRow = BTPAccountManager.GetAccountRowByClubID5(intClubID);
                            int intClubUserID = Convert.ToInt32(accountRow["UserID"]);

                            this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                            this.sbList.Append("<td align=\"center\" height=\"24\">" + intSort + "</td>");
                            this.sbList.Append("<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=" + intClubUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");
                            this.sbList.Append("<td align=\"center\" >" + douOdds + "</td>");
                            this.sbList.Append("<td align=\"center\" >" + intMoney + "</td>");
                            if (intStatus == 1)
                            {
                                this.sbList.Append("<td align=\"center\" >未平盘</td>");
                            }
                            else if (intStatus == 2)
                            {
                                double douWin = (int)douOdds * intMoney;
                                this.sbList.Append("<td align=\"center\" ><font color=\"red\">+" + douWin + "</font></td>");
                            }
                            else
                            {
                                this.sbList.Append("<td align=\"center\" ><font color=\"green\">-" + intMoney + "</font></td>");
                            }
                            this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                        }
                        this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                        this.strScript = this.GetScript(strCurrentURL);

                    }
                    else
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='5'>暂无数据</td></tr>");

                    }
                    return;

                case "NEWGUESS":

                    strCurrentURL = "MYGUESS.aspx?Type=NEWGUESS&";

                    this.sbList = new StringBuilder();
                    this.sbList.Append("<tr>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">序号</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\" height=\"24\">竞猜经理</td>");
                    this.sbList.Append("<td align=\"left\" bgColor=\"#fcc6a4\">竞猜冠军</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">赔率</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">竞猜资金</td>");
                    this.sbList.Append("<td align=\"center\" bgColor=\"#fcc6a4\">盈亏</td>");
                    this.sbList.Append("</tr>");

                    dataTable = BTPXGuessManager.GetXGuessNewRecord();

                    if (dataTable == null)
                    {
                        this.sbList.Append("<tr><td height='30' align='center' colspan='8'>暂无数据</td></tr>");
                        return;
                    }
                    int intIndex = 1;
                    foreach (DataRow row in dataTable.Rows)
                    {


                        int intUserID = Convert.ToInt32(row["UserID"]);
                        string strNickName = BTPAccountManager.GetNickNameByUserID(intUserID);
                        string strClubName = Convert.ToString(row["ClubName"]);
                        double douOdds = Convert.ToDouble(row["Odds"]);
                        int intClubID = Convert.ToInt32(row["ClubID"]);
                        int intMoney = Convert.ToInt32(row["Money"]);
                        int intStatus = Convert.ToInt32(row["Status"]);

                        DataRow accountRow = BTPAccountManager.GetAccountRowByClubID5(intClubID);
                        int intClubUserID = Convert.ToInt32(accountRow["UserID"]);

                        this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append("<td align=\"center\" height=\"24\">" + intIndex + "</td>");
                        this.sbList.Append("<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=" + intUserID + "\" title=\"" + strNickName + "\" target=\"Right\">" + strNickName + "</a></td>");
                        this.sbList.Append("<td align=\"left\" height=\"24\"><a href=\"ShowClub.aspx?Type=5&UserID=" + intClubUserID + "\" title=\"" + strClubName + "\" target=\"Right\">" + strClubName + "</a></td>");
                        this.sbList.Append("<td align=\"center\" >" + douOdds + "</td>");
                        this.sbList.Append("<td align=\"center\" >" + intMoney + "</td>");
                        if (intStatus == 1)
                        {
                            this.sbList.Append("<td align=\"center\" >未平盘</td>");
                        }
                        else if (intStatus == 2)
                        {
                            double douWin = (int)douOdds * intMoney;
                            this.sbList.Append("<td align=\"center\" ><font color=\"red\">+" + douWin + "</font></td>");
                        }
                        else
                        {
                            this.sbList.Append("<td align=\"center\" ><font color=\"green\">-" + intMoney + "</font></td>");
                        }
                        this.sbList.Append("<tr><td height='1' background='Images/RM/Border_07.gif' colspan='8'></td></tr>");

                        intIndex++;

                    }
                    //this.sbList.Append("<tr><td height='30' align='right' colspan='8'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                    this.strScript = this.GetScript(strCurrentURL);

                    return;

                default:
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
            }
             
        }

        private void SetPageIntro()
        {
            switch (this.strType)
            {
                case "CLUBLIST":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1'>竞猜下注</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'  href='XGuess.aspx?Type=MYGUESS&Page=1'>我的下注</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\" 'href='XGuess.aspx?Type=NEWGUESS'>最新下注</a></li></ul></td>");
                    return;

                case "MYGUESS":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"' href='XGuess.aspx?Type=CLUBLIST'>竞猜下注</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>我的下注</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'href='XGuess.aspx?Type=NEWGUESS'>最新下注</a></li></ul></td>");
                    return;

                case "NEWGUESS":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<td><ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"' href='XGuess.aspx?Type=CLUBLIST'>竞猜下注</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/XGuessCenter.htm\"'  href='XGuess.aspx?Type=MYGUESS&Page=1'>我的下注</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>最新下注</li></ul></td>");
                    return;
            }
            
        }
    }
}


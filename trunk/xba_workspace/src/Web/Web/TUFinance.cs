namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TUFinance : Page
    {
        private int intPage;
        private int intPerPage;
        private int intSeason;
        private int intStatus;
        private int intTFinanceID;
        private int intTurn;
        private int intUserID;
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strScript;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPFinanceManager.GetDFinanceCountNew(this.intUserID, this.intTFinanceID);
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
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                this.intStatus = (int) SessionItem.GetRequest("Status", 0);
                if (this.intStatus == 1)
                {
                    this.intTurn = (int) SessionItem.GetRequest("Turn", 0);
                    this.intSeason = (int) SessionItem.GetRequest("Season", 0);
                    this.intTFinanceID = (int) SessionItem.GetRequest("TFinanceID", 0);
                }
                else
                {
                    DataRow gameRow = BTPGameManager.GetGameRow();
                    this.intTurn = (int) gameRow["Turn"];
                    this.intSeason = (int) gameRow["Season"];
                    DataRow row3 = BTPFinanceManager.GetFinanceIDRow(this.intUserID, this.intSeason, this.intTurn);
                    if (row3 == null)
                    {
                        this.intTFinanceID = 0;
                    }
                    else
                    {
                        this.intTFinanceID = (int) row3["TFinanceID"];
                    }
                }
                this.intPerPage = 10;
                if ((this.intSeason != 0) && (this.intTurn != 0))
                {
                    this.strPageIntro = string.Concat(new object[] { "<td height='24'><ul><li class='qian1'>财政详情</li></ul></td><td align='right' style='padding-right:5px'>第", this.intSeason, "赛季 第", this.intTurn, "轮</td>" });
                }
                else
                {
                    this.strPageIntro = "<td height='24'></td><td align='right' style='padding-right:5px'>";
                }
                this.SetList();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetList()
        {
            string strCurrentURL = string.Concat(new object[] { "TUFinance.aspx?Status=1&TFinanceID=", this.intTFinanceID, "&Season=", this.intSeason, "&Turn=", this.intTurn, "&" });
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            if (this.intTFinanceID == 0)
            {
                this.strList = "<tr class='BarContent'><td height='25' colspan='6'>点击“查看”可以查看财政记录详情。</td></tr>";
            }
            else
            {
                SqlDataReader reader = BTPFinanceManager.GetDFinanceListNew(this.intUserID, this.intTFinanceID, this.intPage, this.intPerPage);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string str2;
                        int num = (byte) reader["Category"];
                        if (num == 3)
                        {
                            str2 = "<font color='#00cc00'>街球</font>";
                        }
                        else
                        {
                            str2 = "<font color='#FF0000'>职业</font>";
                        }
                        long num2 = (long)reader["Income"];
                        long num3 = (long)reader["Outcome"];
                        string str3 = reader["Event"].ToString().Trim();
                        DateTime datIn = (DateTime) reader["CreateTime"];
                        object strList = this.strList;
                        this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='30'>", str2, "</td><td align='left'>", str3, "</td><td><font color='green'>", num2, "</font></td><td><font color='red'>", num3, "</font></td><td>", StringItem.FormatDate(datIn, "yy-MM-dd"), "</td></tr>" });
                        this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                    }
                }
                else
                {
                    this.strList = "<tr class='BarContent'><td height='25' colspan='6'>没有找到详细记录。</td></tr>";
                }
                reader.Close();
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
        }
    }
}


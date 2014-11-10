namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Statistiken : Page
    {
        protected ImageButton btnSearch;
        private int intPage;
        private int intPerPage;
        private int intTotal;
        private int intType;
        private int intUserID;
        public StringBuilder sbList;
        public StringBuilder Scriptsb = new StringBuilder();
        public string strClubURL = "";
        public string strMenu = "";
        protected TextBox tbNickName;

        private void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string strNickName = this.tbNickName.Text.ToString().Trim();
            if (strNickName != "")
            {
                int num = 0;
                DataRow accountRowByNickName = BTPAccountManager.GetAccountRowByNickName(strNickName);
                if (accountRowByNickName != null)
                {
                    num = (int) accountRowByNickName["UserID"];
                    base.Response.Redirect(string.Concat(new object[] { "Statistiken.aspx?Type=", this.intType, "&Tag=", num }));
                }
            }
            else
            {
                base.Response.Redirect("Statistiken.aspx?Type=" + this.intType + "&Page=1");
            }
        }

        private void GetList()
        {
            int num;
            int num2;
            this.sbList = new StringBuilder();
            if (this.intUserID < 0)
            {
                this.intUserID = 0;
            }
            this.intTotal = BTPAccountManager.GetAccountCount(this.intType);
            DataRow ladderCount = BTPAccountManager.GetLadderCount(this.intUserID);
            if (ladderCount != null)
            {
                num = (int) ladderCount["StrCount"];
                num2 = (int) ladderCount["VCount"];
            }
            else
            {
                num = 0;
                num2 = 0;
            }
            int num3 = 1;
            int num4 = 0;
            if (this.intUserID > 0)
            {
                if (this.intType == 3)
                {
                    num4 = (num + 1) % this.intPerPage;
                    num3 = (num + 1) / this.intPerPage;
                    if (num4 > 0)
                    {
                        num3++;
                    }
                }
                else
                {
                    num4 = (num2 + 1) % this.intPerPage;
                    num3 = (num2 + 1) / this.intPerPage;
                    if (num4 > 0)
                    {
                        num3++;
                    }
                }
            }
            if (this.intPage == 0)
            {
                this.intPage = num3;
            }
            DataTable table = BTPAccountManager.GetAccountList(this.intPage, this.intPerPage, this.intType);
            if (table != null)
            {
                int num5 = 0;
                foreach (DataRow row2 in table.Rows)
                {
                    int num6;
                    string dev;
                    int num7 = (int) row2["UserID"];
                    int num8 = (int) row2["UCount"];
                    int num9 = (int) row2["CCount"];
                    int num1 = (int) row2["UnionID"];
                    string str2 = row2["NickName"].ToString().Trim();
                    string str3 = row2["ClubName"].ToString().Trim();
                    int num10 = (byte) row2["Category"];
                    string str4 = row2["ShortName"].ToString().Trim();
                    string str5 = row2["Levels"].ToString().Trim();
                    if (num10 == 5)
                    {
                        dev = DevCalculator.GetDev(row2["DevCode"].ToString().Trim());
                    }
                    else
                    {
                        dev = "";
                    }
                    if (this.intType == 3)
                    {
                        num5 = (int) row2["ClubID3"];
                        num6 = num8;
                    }
                    else
                    {
                        num5 = (int) row2["ClubID5"];
                        num6 = num9;
                    }
                    string str6 = "";
                    if (this.intUserID == num7)
                    {
                        str6 = "background-color:fbe2d4;";
                    }
                    string str7 = "";
                    string str8 = "";
                    if (SessionItem.CheckLogin(1) > 0)
                    {
                        str8 = string.Concat(new object[] { "<a href=\"ShowClub.aspx?ClubID=", num5, "&Type=", this.intType, "\" target=\"Right\">", str2, "</a>" });
                        str7 = string.Concat(new object[] { "<a href=\"ShowClub.aspx?ClubID=", num5, "&Type=", this.intType, "\" target=\"Right\">", str3, "</a>" });
                    }
                    else
                    {
                        str8 = str2;
                        str7 = str3;
                    }
                    this.sbList.Append(string.Concat(new object[] { "<tr style=\"", str6, "\"><td  align=\"center\">", num6 }));
                    this.sbList.Append("</td>");
                    this.sbList.Append("<td>" + str7);
                    this.sbList.Append("</td>");
                    this.sbList.Append("<td>" + str8);
                    this.sbList.Append("</td>");
                    this.sbList.Append("<td align='center'>" + str5);
                    this.sbList.Append("</td>");
                    this.sbList.Append("<td align='center'>" + dev);
                    this.sbList.Append("</td>");
                    this.sbList.Append("<td>" + str4);
                    this.sbList.Append("</td></tr>");
                }
            }
            string strCurrentURL = "Statistiken.aspx?Type=" + this.intType + "&";
            this.GetMsgScript(strCurrentURL);
            this.sbList.Append("<tr><td height='25' align='right' colspan='6'>" + this.GetMsgViewPage(strCurrentURL) + "</td></tr>");
        }

        private void GetMsgScript(string strCurrentURL)
        {
            this.Scriptsb.Append("<script language=\"javascript\">");
            this.Scriptsb.Append("function JumpPage()");
            this.Scriptsb.Append("{");
            this.Scriptsb.Append("var strPage=document.all.Page.value;");
            this.Scriptsb.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.Scriptsb.Append("}");
            this.Scriptsb.Append("</script>");
        }

        private string GetMsgViewPage(string strCurrentURL)
        {
            string[] strArray;
            int num = (this.intTotal / this.intPerPage) + 1;
            if ((this.intTotal % this.intPerPage) == 0)
            {
                num--;
            }
            if (num == 0)
            {
                num = 1;
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
            if (this.intPage == num)
            {
                str2 = "下一页";
            }
            else
            {
                strArray = new string[] { "<a href='", strCurrentURL, "Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str2 = string.Concat(strArray);
            }
            return string.Concat(new object[] { "<a href='", strCurrentURL, "Page=1'>首页</a> ", str, " ", str2, " <a href='", strCurrentURL, "Page=", num, "'>尾页</a> 共", this.intTotal, "个记录" });
        }

        private void InitializeComponent()
        {
            this.btnSearch.Click += new ImageClickEventHandler(this.btnSearch_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intType = SessionItem.GetRequest("Type", 0);
            if ((this.intType != 3) && (this.intType != 5))
            {
                this.intType = 3;
            }
            this.intUserID = SessionItem.GetRequest("Tag", 0);
            if (this.intUserID == 0)
            {
                this.intUserID = SessionItem.CheckLogin(1);
                if (this.intUserID > 0)
                {
                    DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                    int num = 0;
                    if (onlineRowByUserID != null)
                    {
                        if (this.intType == 3)
                        {
                            num = (int) onlineRowByUserID["ClubID3"];
                        }
                        else
                        {
                            num = (int) onlineRowByUserID["ClubID5"];
                        }
                    }
                    this.strClubURL = string.Concat(new object[] { "ShowClub.aspx?ClubID=", num, "&Type=", this.intType });
                }
            }
            this.intPage = SessionItem.GetRequest("Page", 0);
            this.intPerPage = 12;
            if (this.intType == 3)
            {
                this.strMenu = "<li id=\"pnavc\"><a href=\"Statistiken.aspx?Type=3\">街球排行</a></li><li><a href=\"Statistiken.aspx?Type=5\">职业排行</a></li>";
            }
            else
            {
                this.strMenu = "<li><a href=\"Statistiken.aspx?Type=3\">街球排行</a></li><li id=\"pnavc\"><a href=\"Statistiken.aspx?Type=5\">职业排行</a></li>";
            }
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.GetList();
        }
    }
}


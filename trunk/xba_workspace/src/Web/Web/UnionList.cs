namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class UnionList : Page
    {
        private int intPage;
        private int intPerPage;
        private int intUnionID;
        private int intUserID;
        public string strList;
        public string strScript;
        public string strUnionName;

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }

        private int GetTotal()
        {
            return BTPUnionManager.GetUnionUserCountByID(this.intUnionID);
        }

        private string GetViewPage(string strCurrentURL)
        {
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
            string str2 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str2 = str2 + "<option value=" + i;
                if (i == this.intPage)
                {
                    str2 = str2 + " selected";
                }
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str2 = str2 + "</select>";
            return string.Concat(new object[] { "共", total, "人 跳转", str2 });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private void List()
        {
            this.intPerPage = 7;
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.intUnionID = (int) SessionItem.GetRequest("UnionID", 0);
            string str = "";
            DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(this.intUnionID);
            if (unionRowByID != null)
            {
                this.strUnionName = unionRowByID["Name"].ToString().Trim();
                str = unionRowByID["ShortName"].ToString().Trim();
            }
            string strCurrentURL = "UnionList.aspx?UnionID=" + this.intUnionID + "&";
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            int intCount = this.intPage * this.intPerPage;
            int total = this.GetTotal();
            DataTable table = BTPUnionManager.GetUnionUserListByID(this.intUnionID, this.intPage, this.intPerPage, intCount, total);
            if (table != null)
            {
                foreach (DataRow row2 in table.Rows)
                {
                    int intUserID = (int) row2["UserID"];
                    string strNickName = row2["NickName"].ToString().Trim();
                    row2["Levels"].ToString().Trim();
                    string str4 = row2["ClubLogo"].ToString().Trim();
                    string str5 = row2["ClubName"].ToString().Trim();
                    str4 = "<img src='" + str4 + "' width='46' height='46' border='0'>";
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='48'>", str4, "</td><td>", AccountItem.GetNickNameInfo(intUserID, strNickName, ""), AccountItem.GetNickNameInfo(intUserID, "[" + str + "-" + str5 + "]", "", 13), "</td><td><a href='MessageCenter.aspx?Type=SENDMSG&UserID=", intUserID, "' target='Center'>短信</a><br><a href='FMatchCenter.aspx?Type=FMATCHSEND&UserID=", intUserID, "' target='Center'>约战</a></td></tr>" });
                }
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='3'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
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
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.List();
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class WealthList : Page
    {
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strList;
        public string strScript;

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return ROOTWealthManager.GetWealthCountByUserID(this.intUserID);
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
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
                obj2 = str4;
                str4 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str4 = str4 + "</select>";
            obj2 = str2 + " " + str3 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, " 跳转", str4 });
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
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intPerPage = 10;
            this.SetList();
        }

        private void SetList()
        {
            string strCurrentURL = "WealthList.aspx?";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            DataTable table = ROOTWealthManager.GetWealthTable(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='30' colspan='4' align='center'>暂时没有游戏币记录</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    int num1 = (int) row["WealthID"];
                    int num3 = (int) row["Income"];
                    int num4 = (int) row["Outcome"];
                    byte num5 = (byte) row["Category"];
                    string str2 = row["Event"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr align='center' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'>", str2, "</td><td>", num3, "</td><td>", num4, "</td><td>", StringItem.FormatDate(datIn, "yyyy-MM-dd hh:mm:ss"), "</td></tr>" });
                }
            }
            this.strList = this.strList + "<tr><td height='30' colspan='4' align='right' style='padding-right:15px'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }
    }
}


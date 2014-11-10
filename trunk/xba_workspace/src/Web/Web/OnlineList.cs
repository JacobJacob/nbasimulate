namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class OnlineList : Page
    {
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strList;
        public string strPageIntro;
        public string strScript;

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }

        private int GetTotal()
        {
            return DTOnlineManager.GetOnlineMCount();
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
            string str = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
            {
                str = str + "<option value=" + i;
                if (i == this.intPage)
                {
                    str = str + " selected";
                }
                object obj2 = str;
                str = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str = str + "</select>";
            return string.Concat(new object[] { "共", total, "人在线 跳转", str });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        private void List()
        {
            string strCurrentURL = "OnlineList.aspx?";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPerPage = 7;
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            DataRow[] onlineMRow = DTOnlineManager.GetOnlineMRow();
            int length = onlineMRow.Length;
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            int num2 = this.intPage * this.intPerPage;
            int num3 = (this.intPage - 1) * this.intPerPage;
            int num4 = length;
            if (length > num2)
            {
                num4 = num2;
            }
            if (length > 0)
            {
                for (int i = num3; i < num4; i++)
                {
                    string str2;
                    DataRow row = onlineMRow[i];
                    int intUserID = (int) row["UserID"];
                    int num7 = (int) row["UnionID"];
                    if (num7 < 1)
                    {
                        str2 = "";
                    }
                    else
                    {
                        str2 = row["ShortName"].ToString().Trim() + "-";
                    }
                    string strNickName = row["NickName"].ToString();
                    string str4 = row["Levels"].ToString();
                    string str5 = row["ClubLogo"].ToString();
                    string str6 = row["ClubName3"].ToString();
                    str5 = SessionItem.GetImageURL() + "Club/Logo/" + str4 + "/" + str5 + ".gif";
                    str5 = "<img src='" + str5 + "' width='46' height='46' border='0'>";
                    bool blnSex = (bool) row["Sex"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='48'>", str5, "</td><td>", AccountItem.GetNickNameInfo(intUserID, strNickName, "", blnSex), AccountItem.GetNickNameInfo(intUserID, "[" + str2 + str6 + "]", "", 13), "</td><td><a href='MessageCenter.aspx?Type=SENDMSG&UserID=", intUserID, "' target='Center'>短信</a><br><a href='FMatchCenter.aspx?Type=FMATCHSEND&UserID=", intUserID, "' target='Center'>约战</a></td></tr>" });
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
            this.strPageIntro = "<strong>在线经理</strong>";
            this.List();
        }
    }
}


namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Staff : Page
    {
        private int intGrade;
        private int intPage;
        private int intPerPage;
        private int intType;
        private int intUserID;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        public StringBuilder sbPageIntro1;
        public StringBuilder sbScript = new StringBuilder();
        private string strNickName;
        public string strSayScript;

        private void GetScript(string strCurrentURL)
        {
            this.sbScript.Append("<script language=\"javascript\">");
            this.sbScript.Append("function JumpPage()");
            this.sbScript.Append("{");
            this.sbScript.Append("var strPage=document.all.Page.value;");
            this.sbScript.Append("window.location=\"" + strCurrentURL + "Page=\"+strPage;");
            this.sbScript.Append("}");
            this.sbScript.Append("</script>");
        }

        private SqlDataReader GetStaffTable(int intCount, int intTotal)
        {
            if (this.intType == 0)
            {
                if (this.intGrade == 0)
                {
                    return BTPStaffManager.GetStaffListByLevelNew(1, this.intPage, this.intPerPage);
                }
                return BTPStaffManager.GetStaffListByLevelNew(this.intGrade, this.intPage, this.intPerPage);
            }
            if (this.intGrade == 0)
            {
                return BTPStaffManager.GetStaffListByTLNew(this.intType, 1, this.intPage, this.intPerPage);
            }
            return BTPStaffManager.GetStaffListByTLNew(this.intType, this.intGrade, this.intPage, this.intPerPage);
        }

        private int GetTotal()
        {
            if (this.intType == 0)
            {
                if (this.intGrade == 0)
                {
                    return BTPStaffManager.GetStaffCountByLevelNew(1);
                }
                return BTPStaffManager.GetStaffCountByLevelNew(this.intGrade);
            }
            if (this.intGrade == 0)
            {
                return BTPStaffManager.GetStaffCountByTLNew(this.intType, 1);
            }
            return BTPStaffManager.GetStaffCountByTLNew(this.intType, this.intGrade);
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
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intType = (int) SessionItem.GetRequest("Type", 0);
                this.intGrade = (int) SessionItem.GetRequest("Grade", 0);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
                int request = (int) SessionItem.GetRequest("Refresh", 0);
                if (request == 1)
                {
                    base.Response.Write("<script language=\"javascript\">parent.Center.location.reload();</script>");
                }
                this.intPerPage = 12;
                switch (this.intType)
                {
                    case 0:
                        this.sbPageIntro = new StringBuilder();
                        this.sbPageIntro.Append("<ul><li class='qian1'>所有职员</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=1&Grade=" + this.intGrade + "&Page=1&Refresh=0'>训练员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=2&Grade=" + this.intGrade + "&Page=1&Refresh=0'>营养师</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=3&Grade=" + this.intGrade + "&Page=1&Refresh=0'>队医</a></li></ul>");
                        break;

                    case 1:
                        this.sbPageIntro = new StringBuilder();
                        this.sbPageIntro.Append("<ul><li class='qian1a'><a href='Staff.aspx?Type=0&Grade=" + this.intGrade + "&Page=1&Refresh=0'>所有职员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2'>训练员</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=2&Grade=" + this.intGrade + "&Page=1&Refresh=0'>营养师</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=3&Grade=" + this.intGrade + "&Page=1&Refresh=0'>队医</a></li></ul>");
                        break;

                    case 2:
                        this.sbPageIntro = new StringBuilder();
                        this.sbPageIntro.Append("<ul><li class='qian1a'><a href='Staff.aspx?Type=0&Grade=" + this.intGrade + "&Page=1&Refresh=0'>所有职员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=1&Grade=" + this.intGrade + "&Page=1&Refresh=0'>训练员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2'>营养师</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=3&Grade=" + this.intGrade + "&Page=1&Refresh=0'>队医</a></li></ul>");
                        break;

                    case 3:
                        this.sbPageIntro = new StringBuilder();
                        this.sbPageIntro.Append("<ul><li class='qian1a'><a href='Staff.aspx?Type=0&Grade=" + this.intGrade + "&Page=1&Refresh=0'>所有职员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=1&Grade=" + this.intGrade + "&Page=1&Refresh=0'>训练员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=2&Grade=" + this.intGrade + "&Page=1&Refresh=0'>营养师</a></li>");
                        this.sbPageIntro.Append("<li class='qian2'>队医</li></ul>");
                        break;

                    default:
                        this.sbPageIntro = new StringBuilder();
                        this.sbPageIntro.Append("<ul><li class='qian1'>所有职员</li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=1&Grade=" + this.intGrade + "&Page=1&Refresh=0'>训练员</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=2&Grade=" + this.intGrade + "&Page=1&Refresh=0'>营养师</a></li>");
                        this.sbPageIntro.Append("<li class='qian2a'><a href='Staff.aspx?Type=3&Grade=" + this.intGrade + "&Page=1&Refresh=0'>队医</a></li></ul>");
                        break;
                }
                switch (this.intGrade)
                {
                    case 1:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<font color='#FF0000'>1级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 2:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>2级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 3:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>3级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 4:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>4级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 5:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>5级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 6:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>6级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;

                    case 7:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=1&Page=1&Refresh=0'>1级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<font color='#FF0000'>7级</font>");
                        break;

                    default:
                        this.sbPageIntro1 = new StringBuilder();
                        this.sbPageIntro1.Append("<font color='#FF0000'>1级</font> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=2&Page=1&Refresh=0'>2级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=3&Page=1&Refresh=0'>3级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=4&Page=1&Refresh=0'>4级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=5&Page=1&Refresh=0'>5级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=6&Page=1&Refresh=0'>6级</a> | ");
                        this.sbPageIntro1.Append("<a href='Staff.aspx?Type=" + this.intType + "&Grade=7&Page=1&Refresh=0'>7级</a>");
                        break;
                }
                this.SetList();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetList()
        {
            string strCurrentURL = string.Concat(new object[] { "Staff.aspx?Type=", this.intType, "&Refresh=0&Grade=", this.intGrade, "&" });
            this.sbList = new StringBuilder();
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.GetScript(strCurrentURL);
            SqlDataReader staffTable = this.GetStaffTable(intCount, total);
            if (staffTable.HasRows)
            {
                while (staffTable.Read())
                {
                    string str2 = staffTable["Name"].ToString().Trim();
                    byte num3 = (byte) staffTable["Age"];
                    byte intPosition = (byte) staffTable["Type"];
                    byte num5 = (byte) staffTable["Contract"];
                    int num6 = (int) staffTable["Salary"];
                    int num7 = (int) staffTable["StaffID"];
                    string str3 = "<a href='SecretaryPage.aspx?StaffID=" + num7 + "&Type=GETSTAFF'>雇佣</a>";
                    this.sbList.Append("<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td height='25'><font color='#660066'>" + str2 + "</font></td>");
                    this.sbList.Append("<td>" + num3 + "</td>");
                    this.sbList.Append("<td>" + PlayerItem.GetStaffChsPosition(intPosition) + "</td>");
                    this.sbList.Append("<td>" + num5 + "</td>");
                    this.sbList.Append("<td>" + num6 + "</td>");
                    this.sbList.Append("<td>" + str3 + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>");
                }
            }
            else
            {
                this.sbList.Append("<tr class='BarContent'><td height='20' colspan='6'>暂时没有职员！</td></tr>");
            }
            staffTable.Close();
            this.sbList.Append("<tr><td height='25' align='right' colspan='6'>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
        }
    }
}


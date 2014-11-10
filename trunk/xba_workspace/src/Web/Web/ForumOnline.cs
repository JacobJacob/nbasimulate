namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class ForumOnline : Page
    {
        public int intC;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strBoardInfo;
        public string strList;
        private string strNickName;
        public string strOnline;
        public string strPageInfo;
        public string strPageIntro;
        private string strPassword;
        public string strScript;
        private string strUserName;

        private string GetScript(string strCurrentURL)
        {
            return ("function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}");
        }

        private int GetTotal()
        {
            return DTOnlineManager.GetOnlineCount();
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
            return string.Concat(new object[] { "共", total, "位经理在线 跳转", str });
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID > -1)
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strUserName = onlineRowByUserID["UserName"].ToString().Trim();
                this.strPassword = onlineRowByUserID["Password"].ToString().Trim();
            }
            this.intPage = SessionItem.GetRequest("Page", 0);
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageInfo();
            this.SetPageIntro();
            this.SetBoardInfo();
            this.SetList();
        }

        private void SetBoardInfo()
        {
            string str;
            DataRow newsByBoardID = ROOTBoardManager.GetNewsByBoardID();
            if (newsByBoardID == null)
            {
                str = " 暂时没有公告信息！";
            }
            else
            {
                int num = (int) newsByBoardID["TopicID"];
                string str2 = newsByBoardID["Title"].ToString().Trim();
                str = string.Concat(new object[] { " <a href='Topic.aspx?TopicID=", num, "&BoardID=001001&Page=1'>", str2, "</a>" });
            }
            this.strBoardInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td width='70%'><img src='" + SessionItem.GetImageURL() + "Forum/face1.gif' border=0 align=absmiddle>" + str + "</td><td width='30%' height='35'></td></tr></table>";
        }

        private void SetList()
        {
            string strCurrentURL = "ForumOnline.aspx?";
            this.strScript = this.GetScript(strCurrentURL);
            this.intPerPage = 60;
            this.intC = 6;
            this.intPage = SessionItem.GetRequest("Page", 0);
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            DataRow[] onlineRow = DTOnlineManager.GetOnlineRow();
            int length = onlineRow.Length;
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "";
            int num2 = this.intPage * this.intPerPage;
            int num3 = (this.intPage - 1) * this.intPerPage;
            int num4 = length;
            if (length > num2)
            {
                num4 = num2;
            }
            int num5 = RandomItem.rnd.Next(0, 10);
            if (length > 0)
            {
                object strList;
                int num6 = num4 - num3;
                int num7 = num6 % this.intC;
                int num8 = num6 / this.intC;
                if (num7 > 0)
                {
                    num8++;
                }
                for (int i = 0; i < num8; i++)
                {
                    this.strList = this.strList + "<tr>";
                    for (int j = 0; j < this.intC; j++)
                    {
                        string str2;
                        int index = (i * this.intC) + j;
                        int num12 = i + j;
                        if ((num12 % 2) == 1)
                        {
                            str2 = "#FFFFFF";
                        }
                        else
                        {
                            str2 = "";
                        }
                        if (index < num6)
                        {
                            string str3;
                            DataRow row = onlineRow[index];
                            if ((bool) row["Sex"])
                            {
                                str3 = "#ff005a";
                            }
                            else
                            {
                                str3 = "#0002ff";
                            }
                            string str4 = row["DiskURL"].ToString();
                            string str5 = row["NickName"].ToString();
                            string str6 = DBLogin.URLString(0) + str4 + "Face.png";
                            strList = this.strList;
                            this.strList = string.Concat(new object[] { strList, "<td height='65' width='", 100 / this.intC, "%' bgcolor='", str2, "' align='center'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str6, "?RndID=", num5, ");width:37px;height:40px'></div><font color='", str3, "'>", str5, "</font></td>" });
                        }
                        else
                        {
                            this.strList = this.strList + "<td bgcolor='" + str2 + "' class='Forum003' height='65'></td>";
                        }
                    }
                    this.strList = this.strList + "</tr>";
                }
                strList = this.strList;
                this.strList = string.Concat(new object[] { strList, "<tr><td height='25' align='right' colspan='", this.intC, "'>", this.GetViewPage(strCurrentURL), "</td></tr>" });
            }
        }

        private void SetPageInfo()
        {
            this.strPageInfo = BoardItem.GetPageInfo("在线经理列表", "0");
        }

        private void SetPageIntro()
        {
            this.strPageIntro = BoardItem.GetPageIntro(this.intUserID, this.strNickName, this.strUserName, this.strPassword);
        }
    }
}


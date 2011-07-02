namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class XBAFinanceAD : Page
    {
        protected ImageButton btnUpdate;
        public int intCoin;
        private int intPage;
        private int intPerPage;
        private int intUserID;
        public string strAD;
        public string strCoin;
        public string strIndexEnd;
        public string strIndexHead;
        public string strList;
        public string strMsg;
        public string strNickName;
        public string strScript;
        private string strType;
        protected TextBox tbFlackURL;
        protected HtmlTable tblFlack;
        protected HtmlTable tblViewFlack;
        protected TextBox tbPayUser;
        protected HtmlTable tbXbaAD;

        private void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string strFlackURL = this.tbFlackURL.Text.ToString().Trim().ToLower();
            if (((strFlackURL.Length < 30) || (strFlackURL.IndexOf("http://") == -1)) || (((strFlackURL.IndexOf(".") == -1) || (strFlackURL.IndexOf("http://www.xba.com.cn") > 0)) || (strFlackURL.IndexOf("baidu.com") > 0)))
            {
                base.Response.Redirect("Report.aspx?Parameter=410");
            }
            else
            {
                switch (ROOTUserManager.AddFlack(this.intUserID, strFlackURL))
                {
                    case 1:
                        base.Response.Redirect("Report.aspx?Parameter=404");
                        return;

                    case 0:
                        base.Response.Redirect("Report.aspx?Parameter=405");
                        return;

                    case 2:
                        base.Response.Redirect("Report.aspx?Parameter=406");
                        return;

                    case 3:
                        base.Response.Redirect("Report.aspx?Parameter=409");
                        return;
                }
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
        }

        private void GetFreeCoin()
        {
            string str = "http://www.xba.com.cn/FromURL.aspx?FromID=" + this.intUserID + "&JumpToURL=";
            this.strList = "<table width='462' border='0' cellpadding='0' cellspacing='0' height='315' bordercolor='#FFFFFF'><tr><td><table width='462' border='0' cellpadding='0' cellspacing='0' ><tr><td height='75' colspan='2' style='color:#9E0F00' style='padding-left:4px'>&nbsp;&nbsp;&nbsp;&nbsp;XBA已经为每位经理分配一个“推广链”。您的朋友通过此链接进入XBA注册后，只要其<font color='red'>街球队</font>达到一定的活跃度，我们将会给您<font color='red'>3枚金币</font>作为奖励。<br>您可以将此地址拷贝发给您QQ、MSN或者论坛上的朋友。</td></tr><tr><td style='color:#9E0F00' valign='top' width='50' align='center'>图例：</td><td style='color:#9E0F00' align='center' width='382'><img src='" + SessionItem.GetImageURL() + "Web/FreeCoin.gif'></td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' style='padding-left:4px'><font color='red'>您的推广链地址是：</font>" + str + "</td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' align='center'><a href=\"javascript:\" onclick=\"clipboardData.setData('Text','" + str + "')\"><img src='" + SessionItem.GetImageURL() + "Web/btnCopy.gif' height='19' width='68' border='0'></a></td></tr><tr><td height='25' colspan='2' style='color:#9E0F00' style='padding-left:4px'><font color='red'>注：</font>您的朋友只有点击<font color='red'>完整的广告链</font>进入XBA注册才能生效，请仔细记录此链接。</td></tr></table></td></tr></table>";
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int flackCount = 0;
            if (this.strType == "FLACK")
            {
                flackCount = ROOTUserManager.GetFlackCount(this.intUserID);
            }
            return flackCount;
        }

        private void GetViewFlack()
        {
            string strCurrentURL = "AccountManage.aspx?Type=VIEWFLACK&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 10;
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            DataTable table = ROOTUserManager.GetFlackList(this.intUserID, this.intPage, this.intPerPage, intCount, total);
            if (table == null)
            {
                this.strList = "<tr><td height='25' align='center' colspan='3'><font color=\"#af1a2e\">暂无宣传帖记录。</font></td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str3;
                    string str2 = row["FlackURL"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    int num4 = (byte) row["CoinGain"];
                    row["CoinGain"].ToString().Trim();
                    int num3 = (byte) row["Status"];
                    if (num3 == 0)
                    {
                        str3 = "等待审核";
                    }
                    else if ((num3 == 2) && (num4 == 1))
                    {
                        str3 = "奖励1金币";
                    }
                    else if ((num3 == 2) && (num4 == 2))
                    {
                        str3 = "奖励2金币";
                    }
                    else if ((num3 == 2) && (num4 == 3))
                    {
                        str3 = "奖励3金币";
                    }
                    else if ((num3 == 2) && (num4 == 5))
                    {
                        str3 = "奖励5金币";
                    }
                    else
                    {
                        str3 = "未通过审核";
                    }
                    string strList = this.strList;
                    this.strList = strList + "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><font color=\"#af1a2e\"> " + StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss") + "</font></td><td><a href='" + str2 + "' target='_blank'><font color=\"#af1a2e\"> " + str2 + "</font></a></td><td align='center'><font color=\"#af1a2e\"> " + str3 + "</font></td></tr>";
                }
                this.strList = this.strList + "<tr><td height='25' align='right' colspan='5'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
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

        private void GetWealthList()
        {
            string strCurrentURL = "XBAFinance.aspx?Type=WEALTH&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 11;
            if (this.intPage == 0)
            {
                this.intPage = 1;
            }
            this.GetTotal();
            this.strScript = this.GetScript(strCurrentURL);
            this.strList = "<table width='432' border='2' cellpadding='0' cellspacing='0' bordercolor='#FFFFFF'><tr><td width='65' height='26' align='center' valign='middle' bgcolor='#AE1100'><strong>收入</strong></td><td width='65' align='center' valign='middle' bgcolor='#AE1100'><strong>支出</strong></td><td width='112' align='center' valign='middle' bgcolor='#AE1100'><strong>时间</strong></td><td width='179' align='center' bgcolor='#AE1100'><strong>事件</strong></td><td width='68' align='center' bgcolor='#AE1100'><strong>备注</strong></td></tr>";
            DataTable reader = ROOTWealthManager.GetWealthTableNew(this.intUserID, this.intPage, this.intPerPage);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str2 = row["Event"].ToString();
                    int num = (int) row["Income"];
                    int num2 = (int) row["Outcome"];
                    DateTime datIn = (DateTime) row["CreateTime"];
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='20' align='center' style='color:#9E0F00'>", num, "</td><td align='center' style='color:#9E0F00'>", num2, "</td><td align='center' style='color:#9E0F00'>", StringItem.FormatDate(datIn, "yy-MM-dd hh:mm:ss"), "</td><td style='color:#9E0F00' style='padding:2px;'>", str2, "</td><td style='color:#9E0F00' style='padding:2px;'>无</td></tr>" });
                }
                strCurrentURL = "XBAFinanceAD.aspx?Type=FLACK";
                this.strList = this.strList + "<tr><td height='28' align='right' colspan='5' style='color:#9E0F00'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
            }
            else
            {
                this.strList = this.strList + "<tr><td height='25' align='center' colspan='5' style='color:#9E0F00'>暂无消费记录。</td></tr>";
            }
            //reader.Close();
            this.strList = this.strList + "</table>";
        }

        private void InitializeComponent()
        {
            this.btnUpdate.Click += new ImageClickEventHandler(this.btnUpdate_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                this.strNickName = "--";
                this.strCoin = "--";
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                string str3;
                DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(this.intUserID);
                this.strNickName = userInfoByID["NickName"].ToString().Trim();
                this.intCoin = (int) userInfoByID["Coin"];
                this.strCoin = Convert.ToString(this.intCoin).Trim();
                string strUserName = userInfoByID["UserName"].ToString().Trim();
                string strPassword = userInfoByID["Password"].ToString().Trim();
                this.strIndexHead = BoardItem.GetIndexHead(this.intUserID, strUserName, strPassword);
                this.strIndexEnd = BoardItem.GetIndexEnd();
                this.tblViewFlack.Visible = false;
                this.tblFlack.Visible = false;
                this.tbXbaAD.Visible = false;
                this.strType = SessionItem.GetRequest("TYPE", 1).ToString().Trim();
                if (((str3 = this.strType) != null) && (string.IsInterned(str3) == "FLACK"))
                {
                    this.tblFlack.Visible = true;
                    this.tblViewFlack.Visible = true;
                    this.GetViewFlack();
                }
                else
                {
                    this.tbXbaAD.Visible = true;
                    this.GetFreeCoin();
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


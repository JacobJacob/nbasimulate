namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class MessageCenter : Page
    {
        private bool blnBtn;
        protected ImageButton btnAdd;
        protected ImageButton btnSearchOK;
        protected ImageButton btnSend;
        protected HtmlSelect city;
        protected DropDownList ddlAge;
        protected DropDownList ddlSearch;
        protected HtmlInputHidden hddCity;
        protected HtmlInputHidden hddText;
        protected HtmlInputHidden HidCity;
        protected HtmlInputHidden Hidcity2;
        protected HtmlInputHidden HidProvince;
        public int intCheck;
        private int intGender;
        private int intPage;
        private int intPrePage = 10;
        private int intRUserID;
        private int intUserID;
        protected HtmlSelect prv;
        protected RadioButtonList rbGender;
        public StringBuilder sbCityScript = new StringBuilder();
        public StringBuilder sbHotCity = new StringBuilder();
        public StringBuilder sbList = new StringBuilder();
        public StringBuilder sbPageIntro = new StringBuilder();
        public StringBuilder sbProvinceScript = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();
        public StringBuilder sbSearchPageIntro = new StringBuilder();
        private string strCity;
        private string strEndYear;
        private string strNickName;
        private string strProvince;
        public string strQQ;
        public string strScript = "";
        public string strScript2 = "";
        public string strSearchName = "";
        private string strStartYear;
        private string strStatus;
        private string strType;
        protected TextBox tbContent;
        protected TextBox tbFriendName;
        protected HtmlTable tblAddFriend;
        protected HtmlTable tblAge;
        protected HtmlTable tblCity;
        protected HtmlTable tblClub;
        protected HtmlTable tblNick;
        protected HtmlTable tblSearchManager;
        protected HtmlTable tblSendMsg;
        protected TextBox tbNickName;
        protected TextBox tbSearchClub;
        protected TextBox tbSearchNick;
        protected TextBox tbText;
        protected HtmlTableRow trNickName;

        private void AddFriend()
        {
        }

        private void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            this.sbList = new StringBuilder();
            string validWords = StringItem.GetValidWords(this.tbFriendName.Text);
            if (!StringItem.IsValidName(validWords, 2, 0x10))
            {
                this.sbList.Append("<font color='#FF0000'>您填写的经理名称有错误！</font>");
            }
            else if (validWords == this.strNickName)
            {
                this.sbList.Append("<font color='#FF0000'>您无需将自己添加为好友！</font>");
            }
            else
            {
                switch (BTPFriendManager.SetFriend(this.intUserID, validWords))
                {
                    case -1:
                        this.sbList.Append("<font color='#FF0000'>没有找到此经理！</font>");
                        return;

                    case 0:
                        this.sbList.Append("<font color='#FF0000'>您已经添加过此经理为好友！</font>");
                        return;

                    case 2:
                        this.sbList.Append("<font color='#FF0000'>您不能添加超过20个的好友！</font>");
                        return;
                }
                base.Response.Redirect("Report.aspx?Parameter=30");
            }
        }

        private void btnSearchOK_Click(object sender, ImageClickEventArgs e)
        {
            this.blnBtn = true;
            this.strStatus = this.Session["Status"].ToString().Trim();
            if (this.strStatus == "NICKNAME")
            {
                this.GetNickNameResult();
            }
            else if (this.strStatus == "CLUBNAME")
            {
                this.GetClubNameResult();
            }
            else if (this.strStatus == "CITY")
            {
                this.GetCityResult();
            }
            else if (this.strStatus == "AGE")
            {
                this.GetAgeResult();
            }
            else if (this.strStatus == "GENDER")
            {
                this.GetGenderResult();
            }
            else
            {
                base.Response.Redirect("");
            }
        }

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            string text = this.tbNickName.Text;
            string htmlEncode = StringItem.GetHtmlEncode(this.tbContent.Text.ToString().Trim());
            this.sbList = new StringBuilder();
            text = StringItem.GetValidWords(text);
            if (StringItem.IsValidName(text, 2, 0x10))
            {
                htmlEncode = StringItem.GetValidWords(htmlEncode);
                if (!StringItem.IsValidContent(htmlEncode, 2, 500))
                {
                    this.sbList.Append("<font color='#FF0000'>消息内容不符合要求或存在非法字符！</font>");
                }
                else
                {
                    BTPMessageManager.SetHasMsg(text);
                    if (BTPMessageManager.AddMessage(this.intUserID, this.strNickName, text, htmlEncode))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=20");
                    }
                    else
                    {
                        this.sbList.Append("<font color='#FF0000'>" + text + "并不存在，请查证是否输入正确，或对方以更改名称！</font>");
                    }
                }
            }
            else
            {
                this.sbList.Append("<font color='#FF0000'>经理昵称不合要求！</font>");
            }
        }

        private void GetAgeResult()
        {
            this.sbList = new StringBuilder();
            string str = this.ddlAge.SelectedValue.ToString().Trim();
            if (str == "0")
            {
                this.strStartYear = Convert.ToString(DateTime.Today.AddYears(-15).Year).Trim() + "-1-1 0:00:00";
                this.strEndYear = Convert.ToString(DateTime.Now.Year).Trim() + "-12-31 23:59:59";
            }
            else if (str == "1")
            {
                this.strStartYear = Convert.ToString(DateTime.Today.AddYears(-22).Year).Trim() + "-1-1 0:00:00";
                this.strEndYear = Convert.ToString(DateTime.Today.AddYears(-16).Year).Trim() + "-12-31 23:59:59";
            }
            else if (str == "2")
            {
                this.strStartYear = Convert.ToString(DateTime.Today.AddYears(-30).Year).Trim() + "-1-1 0:00:00";
                this.strEndYear = Convert.ToString(DateTime.Today.AddYears(-23).Year).Trim() + "-12-31 23:59:59";
            }
            else if (str == "3")
            {
                this.strStartYear = Convert.ToString(DateTime.Today.AddYears(-40).Year).Trim() + "-1-1 0:00:00";
                this.strEndYear = Convert.ToString(DateTime.Today.AddYears(-31).Year).Trim() + "-12-31 23:59:59";
            }
            else
            {
                this.strStartYear = Convert.ToString(DateTime.Today.AddYears(-100).Year).Trim() + "-1-1 0:00:00";
                this.strEndYear = Convert.ToString(DateTime.Today.AddYears(-41).Year).Trim() + "-12-31 23:59:59";
            }
            this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
            DataTable table = BTPAccountManager.GetSearchResultByAge(0, this.intPage, this.intPrePage, this.strStartYear, this.strEndYear, this.intGender);
            if (table == null)
            {
                this.sbList.Append("<tr align='center'><td colspan='5' height='25'>没有任何记录。</td></tr>");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2;
                    string strNickName = row["NickName"].ToString().Trim();
                    row["ClubName"].ToString().Trim();
                    int intUserID = (int) row["UserID"];
                    string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                    int num2 = (byte) row["Levels"];
                    string strQQ = row["QQ"].ToString().Trim();
                    bool blnSex = (bool) row["Sex"];
                    int intWealth = (int) row["Wealth"];
                    string wealthName = AccountItem.GetWealthName(intWealth);
                    if (strQQ != "")
                    {
                        strQQ = StringItem.GetQQURL(strQQ);
                    }
                    else
                    {
                        strQQ = "暂无资料";
                    }
                    if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                    {
                        str2 = "<font color='666666'>否</font>";
                    }
                    else
                    {
                        str2 = "<font color='red'>是</font>";
                    }
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#eeeeee'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align='center' height='28'>" + wealthName + "</td>");
                    this.sbList.Append("<td align='center'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbList.Append("<td align='center'>" + num2 + "</td>");
                    this.sbList.Append("<td align='center'>" + dev + "</td>");
                    this.sbList.Append("<td align='center'>" + str2 + "</td>");
                    this.sbList.Append("<td align='center'>" + strQQ + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
                string strCurrentURL = "MessageCenter.aspx?Type=SEARCH&Status=AGE&Check=1&";
                this.sbList.Append("<tr><td align='right' colspan=6>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                this.strScript2 = this.GetScript(strCurrentURL);
            }
        }

        private void GetCityResult()
        {
            DataTable table;
            this.sbList = new StringBuilder();
            this.strCity = this.hddCity.Value.ToString().Trim();
            this.strProvince = this.prv.Items[this.prv.SelectedIndex].Value.ToString().Trim();
            if (this.strProvince == "")
            {
                this.strProvince = SessionItem.GetRequest("Province", 1).ToString();
                if (this.strProvince.Trim() == "")
                {
                    this.sbList.Append("<tr align='center'><td colspan='5' height='25'><font color='red'>你输入的查询条件不足，请重新输入！</font></td></tr>");
                    return;
                }
            }
            if (this.strCity == "")
            {
                this.strCity = SessionItem.GetRequest("City", 1).ToString();
                if (this.strCity.Trim() == "")
                {
                    this.sbList.Append("<tr align='center'><td colspan='5' height='25'><font color='red'>你输入的查询条件不足，请重新输入！</font></td></tr>");
                    return;
                }
            }
            HttpCookie cookie = new HttpCookie("Province") {
                Value = this.strProvince,
                Name = "Province"
            };
            cookie.Expires.AddDays(365.0);
            base.Response.Cookies.Add(cookie);
            HttpCookie cookie2 = new HttpCookie("City") {
                Value = this.strCity,
                Name = "City"
            };
            cookie2.Expires.AddDays(365.0);
            base.Response.Cookies.Add(cookie2);
            this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
            this.intPrePage = 10;
            int request = SessionItem.GetRequest("Sex", 0);
            if ((request == 0) || this.blnBtn)
            {
                this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
            }
            else
            {
                switch (request)
                {
                    case 1:
                        this.intGender = 0;
                        goto Label_0211;

                    case 2:
                        this.intGender = 1;
                        goto Label_0211;
                }
                this.intGender = 2;
            }
        Label_0211:
            table = BTPAccountManager.GetSearchResultByCity(0, this.intPage, this.intPrePage, this.strProvince, this.strCity, this.intGender);
            if (table == null)
            {
                this.sbList.Append("<tr align='center'><td colspan='5' height='25'>没有任何记录。</td></tr>");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str;
                    string strNickName = row["NickName"].ToString().Trim();
                    row["ClubName"].ToString().Trim();
                    int intUserID = (int) row["UserID"];
                    string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                    int num3 = (byte) row["Levels"];
                    string strQQ = row["QQ"].ToString().Trim();
                    bool blnSex = (bool) row["Sex"];
                    int intWealth = (int) row["Wealth"];
                    string wealthName = AccountItem.GetWealthName(intWealth);
                    if (strQQ != "")
                    {
                        strQQ = StringItem.GetQQURL(strQQ);
                    }
                    else
                    {
                        strQQ = "暂无资料";
                    }
                    if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                    {
                        str = "<font color='666666'>否</font>";
                    }
                    else
                    {
                        str = "<font color='red'>是</font>";
                    }
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#eeeeee'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align='center' height='28'>" + wealthName + "</td>");
                    this.sbList.Append("<td align='center'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbList.Append("<td align='center'>" + num3 + "</td>");
                    this.sbList.Append("<td align='center'>" + dev + "</td>");
                    this.sbList.Append("<td align='center'>" + str + "</td>");
                    this.sbList.Append("<td align='center'>" + strQQ + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='6'></td></tr>");
                }
                string strCurrentURL = string.Concat(new object[] { "MessageCenter.aspx?Type=SEARCH&Status=CITY&Province=", this.strProvince, "&City=", this.strCity, "&Check=1&Sex=", this.intGender + 1, "&" });
                this.sbList.Append("<tr><td align='right' colspan=6>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                this.strScript2 = this.GetScript(strCurrentURL);
                this.HidProvince.Value = this.strProvince;
                this.Hidcity2.Value = this.strCity;
            }
        }

        private void GetCityScript()
        {
            this.sbCityScript.Append("<SCRIPT language=\"JavaScript\">");
            this.sbCityScript.Append("function setcity() {");
            this.sbCityScript.Append("switch (document.all.prv.value) {");
            this.sbCityScript.Append("case \"\" :");
            this.sbCityScript.Append("var cityOptions = new Array(\"请先选省份\", \"\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"安徽\" :");
            this.sbCityScript.Append("var cityOptions = new Array(");
            this.sbCityScript.Append("\"不限\", \"不限\",\"合肥\", \"合肥\",\"安庆\", \"安庆\",\"蚌埠\", \"蚌埠\",\"亳州\", \"亳州\",\"巢湖\", \"巢湖\",\"滁州\", \"滁州\",\"阜阳\", \"阜阳\",\"贵池\", \"贵池\",\"淮北\", \"淮北\",\"淮化\", \"淮化\",\"淮南\", \"淮南\", \"黄山\", \"黄山\", \"九华山\", \"九华山\", \"六安\", \"六安\",\"马鞍山\", \"马鞍山\",\"宿州\", \"宿州\",\"铜陵\", \"铜陵\",\"屯溪\", \"屯溪\",\"芜湖\", \"芜湖\", \"宣城\", \"宣城\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"北京\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"北京\", \"北京\",\"东城\",\"西城\",\"崇文\",\"宣武\",\"朝阳\",\"丰台\",\"石景山\",\"海淀\",\"门头沟\",\"房山\",\"通州\",\"顺义\",\"昌平\",\"大兴\",\"平谷\",\"怀柔\",\"密云\",\"延庆\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"重庆\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"重庆\", \"重庆\",\"万州\",\"涪陵\",\"渝中\",\"大渡口\",\"江北\",\"沙坪坝\",\"九龙坡\",\"南岸\",\"北碚\",\"万盛\",\"双挢\",\"渝北\",\"巴南\",\"黔江\",\"长寿\",\"綦江\",\"潼南\",\"铜梁\",\"大足\",\"荣昌\",\"壁山\",\"梁平\",\"城口\",\"丰都\",\"垫江\",\"武隆\",\"忠县\",\"开县\",\"云阳\",\"奉节\",\"巫山\",\"巫溪\",\"石柱\",\"秀山\",\"酉阳\",\"彭水\",\"江津\",\"合川\",\"永川\",\"南川\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"福建\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"福州\", \"福州\",\"福安\", \"福安\",\"龙岩\", \"龙岩\",\"南平\", \"南平\",\"宁德\", \"宁德\",\"莆田\", \"莆田\",\"泉州\", \"泉州\",\"三明\", \"三明\",\"邵武\", \"邵武\",\"石狮\", \"石狮\",\"永安\",\"永安\", \"武夷山\", \"武夷山\",\"厦门\", \"厦门\", \"漳州\", \"漳州\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"甘肃\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"兰州(*)\", \"兰州\",");
            this.sbCityScript.Append("\"白银\", \"白银\",");
            this.sbCityScript.Append("\"定西\", \"定西\",");
            this.sbCityScript.Append("\"敦煌\", \"敦煌\",");
            this.sbCityScript.Append("\"甘南\", \"甘南\",");
            this.sbCityScript.Append("\"金昌\", \"金昌\",");
            this.sbCityScript.Append("\"酒泉\", \"酒泉\",");
            this.sbCityScript.Append("\"临夏\", \"临夏\",");
            this.sbCityScript.Append("\"平凉\", \"平凉\",");
            this.sbCityScript.Append("\"天水\", \"天水\",");
            this.sbCityScript.Append("\"武都\", \"武都\", ");
            this.sbCityScript.Append("\"西峰\", \"西峰\", ");
            this.sbCityScript.Append("\"张掖\", \"张掖\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"广东\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"广州(*)\", \"广州\",");
            this.sbCityScript.Append("\"潮阳\", \"潮阳\",");
            this.sbCityScript.Append("\"潮州\", \"潮州\",");
            this.sbCityScript.Append("\"澄海\", \"澄海\",");
            this.sbCityScript.Append("\"东莞\", \"东莞\",");
            this.sbCityScript.Append("\"佛山\", \"佛山\",");
            this.sbCityScript.Append("\"河源\", \"河源\",");
            this.sbCityScript.Append("\"惠州\", \"惠州\",");
            this.sbCityScript.Append("\"江门\", \"江门\",");
            this.sbCityScript.Append("\"揭阳\", \"揭阳\",");
            this.sbCityScript.Append("\"开平\", \"开平\",");
            this.sbCityScript.Append("\"茂名\", \"茂名\",");
            this.sbCityScript.Append("\"梅州\", \"梅州\",");
            this.sbCityScript.Append("\"清远\", \"清远\",");
            this.sbCityScript.Append("\"汕头\", \"汕头\",");
            this.sbCityScript.Append("\"汕尾\", \"汕尾\",");
            this.sbCityScript.Append("\"韶关\", \"韶关\",");
            this.sbCityScript.Append("\"深圳\", \"深圳\",");
            this.sbCityScript.Append("\"顺德\", \"顺德\",");
            this.sbCityScript.Append("\"阳江\", \"阳江\",");
            this.sbCityScript.Append("\"阳江\", \"阳江\",");
            this.sbCityScript.Append("\"英德\", \"英德\",");
            this.sbCityScript.Append("\"云浮\", \"云浮\",");
            this.sbCityScript.Append("\"增城\", \"增城\",");
            this.sbCityScript.Append("\"湛江\", \"湛江\",");
            this.sbCityScript.Append("\"肇庆\", \"肇庆\", ");
            this.sbCityScript.Append("\"中山\", \"中山\", ");
            this.sbCityScript.Append("\"珠海\", \"珠海\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"广西\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"南宁(*)\", \"南宁\",");
            this.sbCityScript.Append("\"百色\", \"百色\",");
            this.sbCityScript.Append("\"北海\", \"北海\",");
            this.sbCityScript.Append("\"桂林\", \"桂林\",");
            this.sbCityScript.Append("\"防城港\", \"防城港\",");
            this.sbCityScript.Append("\"河池\", \"河池\",");
            this.sbCityScript.Append("\"柳州\", \"柳州\",");
            this.sbCityScript.Append("\"钦州\", \"钦州\", ");
            this.sbCityScript.Append("\"梧州\", \"梧州\", ");
            this.sbCityScript.Append("\"玉林\", \"玉林\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"贵州\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"贵阳(*)\", \"贵阳\",");
            this.sbCityScript.Append("\"安顺\", \"安顺\",");
            this.sbCityScript.Append("\"毕节\", \"毕节\",");
            this.sbCityScript.Append("\"都匀\", \"都匀\",");
            this.sbCityScript.Append("\"凯里\", \"凯里\",");
            this.sbCityScript.Append("\"六盘水\", \"六盘水\",");
            this.sbCityScript.Append("\"铜仁\", \"铜仁\",");
            this.sbCityScript.Append("\"兴义\", \"兴义\", ");
            this.sbCityScript.Append("\"玉屏\", \"玉屏\", ");
            this.sbCityScript.Append("\"遵义\", \"遵义\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"海南\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"海口(*)\", \"海口\",");
            this.sbCityScript.Append("\"儋县\", \"儋县\",");
            this.sbCityScript.Append("\"陵水\", \"陵水\",");
            this.sbCityScript.Append("\"琼海\", \"琼海\",");
            this.sbCityScript.Append("\"三亚\", \"三亚\", ");
            this.sbCityScript.Append("\"通什\", \"通什\", ");
            this.sbCityScript.Append("\"万宁\", \"万宁\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"河北\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"石家庄(*)\", \"石家庄\",");
            this.sbCityScript.Append("\"保定\", \"保定\",");
            this.sbCityScript.Append("\"北戴河\", \"北戴河\",");
            this.sbCityScript.Append("\"沧州\", \"沧州\",");
            this.sbCityScript.Append("\"承德\", \"承德\",");
            this.sbCityScript.Append("\"丰润\", \"丰润\",");
            this.sbCityScript.Append("\"邯郸\", \"邯郸\",");
            this.sbCityScript.Append("\"衡水\", \"衡水\",");
            this.sbCityScript.Append("\"廊坊\", \"廊坊\",");
            this.sbCityScript.Append("\"南戴河\", \"南戴河\",");
            this.sbCityScript.Append("\"秦皇岛\", \"秦皇岛\",");
            this.sbCityScript.Append("\"唐山\", \"唐山\",");
            this.sbCityScript.Append("\"新城\", \"新城\",");
            this.sbCityScript.Append("\"邢台\", \"邢台\", ");
            this.sbCityScript.Append("\"张家口\", \"张家口\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"黑龙江\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"哈尔滨(*)\", \"哈尔滨\",");
            this.sbCityScript.Append("\"北安\", \"北安\",");
            this.sbCityScript.Append("\"大庆\", \"大庆\",");
            this.sbCityScript.Append("\"大兴安岭\", \"大兴安岭\",");
            this.sbCityScript.Append("\"鹤岗\", \"鹤岗\",");
            this.sbCityScript.Append("\"黑河\", \"黑河\",");
            this.sbCityScript.Append("\"佳木斯\", \"佳木斯\",");
            this.sbCityScript.Append("\"鸡西\", \"鸡西\",");
            this.sbCityScript.Append("\"牡丹江\", \"牡丹江\",");
            this.sbCityScript.Append("\"齐齐哈尔\", \"齐齐哈尔\",");
            this.sbCityScript.Append("\"七台河\", \"七台河\",");
            this.sbCityScript.Append("\"双鸭山\", \"双鸭山\",");
            this.sbCityScript.Append("\"绥化\", \"绥化\",");
            this.sbCityScript.Append("\"伊春\", \"伊春\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"河南\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"郑州(*)\", \"郑州\",");
            this.sbCityScript.Append("\"安阳\", \"安阳\",");
            this.sbCityScript.Append("\"鹤壁\", \"鹤壁\",");
            this.sbCityScript.Append("\"潢川\", \"潢川\",");
            this.sbCityScript.Append("\"焦作\", \"焦作\",");
            this.sbCityScript.Append("\"开封\", \"开封\",");
            this.sbCityScript.Append("\"漯河\", \"漯河\",");
            this.sbCityScript.Append("\"洛阳\", \"洛阳\",");
            this.sbCityScript.Append("\"南阳\", \"南阳\",");
            this.sbCityScript.Append("\"平顶山\", \"平顶山\",");
            this.sbCityScript.Append("\"濮阳\", \"濮阳\",");
            this.sbCityScript.Append("\"三门峡\", \"三门峡\",");
            this.sbCityScript.Append("\"商丘\", \"商丘\",");
            this.sbCityScript.Append("\"新乡\", \"新乡\",");
            this.sbCityScript.Append("\"信阳\", \"信阳\",");
            this.sbCityScript.Append("\"许昌\", \"许昌\",");
            this.sbCityScript.Append("\"周口\", \"周口\", ");
            this.sbCityScript.Append("\"驻马店\", \"驻马店\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"香港\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"香港\", \"香港\", ");
            this.sbCityScript.Append("\"九龙\", \"九龙\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"湖北\" : ");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"武汉(*)\", \"武汉\",");
            this.sbCityScript.Append("\"恩施\", \"恩施\",");
            this.sbCityScript.Append("\"鄂州\", \"鄂州\",");
            this.sbCityScript.Append("\"黄岗\", \"黄岗\",");
            this.sbCityScript.Append("\"黄石\", \"黄石\",");
            this.sbCityScript.Append("\"荆门\", \"荆门\",");
            this.sbCityScript.Append("\"荆州\", \"荆州\",");
            this.sbCityScript.Append("\"潜江\", \"潜江\",");
            this.sbCityScript.Append("\"十堰\", \"十堰\",");
            this.sbCityScript.Append("\"随州\", \"随州\",");
            this.sbCityScript.Append("\"武穴\", \"武穴\",");
            this.sbCityScript.Append("\"仙桃\", \"仙桃\",");
            this.sbCityScript.Append("\"咸宁\", \"咸宁\",");
            this.sbCityScript.Append("\"襄阳\", \"襄阳\",");
            this.sbCityScript.Append("\"襄樊\", \"襄樊\",");
            this.sbCityScript.Append("\"孝感\", \"孝感\",");
            this.sbCityScript.Append("\"宜昌\", \"宜昌\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"湖南\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"长沙(*)\", \"长沙\",");
            this.sbCityScript.Append("\"常德\", \"常德\",");
            this.sbCityScript.Append("\"郴州\", \"郴州\",");
            this.sbCityScript.Append("\"衡阳\", \"衡阳\",");
            this.sbCityScript.Append("\"怀化\", \"怀化\",");
            this.sbCityScript.Append("\"吉首\", \"吉首\",");
            this.sbCityScript.Append("\"娄底\", \"娄底\",");
            this.sbCityScript.Append("\"邵阳\", \"邵阳\",");
            this.sbCityScript.Append("\"湘潭\", \"湘潭\",");
            this.sbCityScript.Append("\"益阳\", \"益阳\",");
            this.sbCityScript.Append("\"岳阳\", \"岳阳\",");
            this.sbCityScript.Append("\"永州\", \"永州\",");
            this.sbCityScript.Append("\"张家界\", \"张家界\",");
            this.sbCityScript.Append("\"株洲\", \"株洲\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"江苏\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"南京(*)\", \"南京\",");
            this.sbCityScript.Append("\"常熟\", \"常熟\",");
            this.sbCityScript.Append("\"常州\", \"常州\",");
            this.sbCityScript.Append("\"海门\", \"海门\",");
            this.sbCityScript.Append("\"淮安\", \"淮安\",");
            this.sbCityScript.Append("\"江都\", \"江都\",");
            this.sbCityScript.Append("\"江阴\", \"江阴\",");
            this.sbCityScript.Append("\"昆山\", \"昆山\",");
            this.sbCityScript.Append("\"连云港\", \"连云港\",");
            this.sbCityScript.Append("\"南通\", \"南通\",");
            this.sbCityScript.Append("\"启东\", \"启东\",");
            this.sbCityScript.Append("\"沭阳\", \"沭阳\",");
            this.sbCityScript.Append("\"苏州\", \"苏州\",");
            this.sbCityScript.Append("\"太仓\", \"太仓\",");
            this.sbCityScript.Append("\"泰州\", \"泰州\",");
            this.sbCityScript.Append("\"同里\", \"同里\",");
            this.sbCityScript.Append("\"无锡\", \"无锡\",");
            this.sbCityScript.Append("\"徐州\", \"徐州\",");
            this.sbCityScript.Append("\"盐城\", \"盐城\",");
            this.sbCityScript.Append("\"扬州\", \"扬州\",");
            this.sbCityScript.Append("\"宜兴\", \"宜兴\",");
            this.sbCityScript.Append("\"仪征\", \"仪征\",");
            this.sbCityScript.Append("\"张家港\", \"张家港\", ");
            this.sbCityScript.Append("\"镇江\", \"镇江\", ");
            this.sbCityScript.Append("\"周庄\", \"周庄\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"江西\" :");
            this.sbCityScript.Append("var cityOptions = new Array(");
            this.sbCityScript.Append("\"不限\", \"不限\",\"南昌(*)\", \"南昌\",");
            this.sbCityScript.Append("\"抚州\", \"抚州\",");
            this.sbCityScript.Append("\"赣州\", \"赣州\",");
            this.sbCityScript.Append("\"吉安\", \"吉安\",");
            this.sbCityScript.Append("\"景德镇\", \"景德镇\",");
            this.sbCityScript.Append("\"井冈山\", \"井冈山\",");
            this.sbCityScript.Append("\"九江\", \"九江\",");
            this.sbCityScript.Append("\"庐山\", \"庐山\",");
            this.sbCityScript.Append("\"萍乡\", \"萍乡\",");
            this.sbCityScript.Append("\"上饶\", \"上饶\",");
            this.sbCityScript.Append("\"新余\", \"新余\", ");
            this.sbCityScript.Append("\"宜春\", \"宜春\", ");
            this.sbCityScript.Append("\"鹰潭\", \"鹰潭\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"吉林\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"长春(*)\", \"长春\",");
            this.sbCityScript.Append("\"白城\", \"白城\",");
            this.sbCityScript.Append("\"白山\", \"白山\",");
            this.sbCityScript.Append("\"珲春\", \"珲春\",");
            this.sbCityScript.Append("\"辽源\", \"辽源\",");
            this.sbCityScript.Append("\"梅河\", \"梅河\",");
            this.sbCityScript.Append("\"吉林\", \"吉林\",");
            this.sbCityScript.Append("\"四平\", \"四平\",");
            this.sbCityScript.Append("\"松原\", \"松原\",");
            this.sbCityScript.Append("\"通化\", \"通化\",");
            this.sbCityScript.Append("\"延吉\", \"延吉\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"辽宁\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"沈阳(*)\", \"沈阳\",");
            this.sbCityScript.Append("\"鞍山\", \"鞍山\",");
            this.sbCityScript.Append("\"本溪\", \"本溪\",");
            this.sbCityScript.Append("\"朝阳\", \"朝阳\",");
            this.sbCityScript.Append("\"大连\", \"大连\",");
            this.sbCityScript.Append("\"丹东\", \"丹东\",");
            this.sbCityScript.Append("\"抚顺\", \"抚顺\",");
            this.sbCityScript.Append("\"阜新\", \"阜新\",");
            this.sbCityScript.Append("\"葫芦岛\", \"葫芦岛\",");
            this.sbCityScript.Append("\"锦州\", \"锦州\",");
            this.sbCityScript.Append("\"辽阳\", \"辽阳\",");
            this.sbCityScript.Append("\"盘锦\", \"盘锦\",");
            this.sbCityScript.Append("\"铁岭\", \"铁岭\",");
            this.sbCityScript.Append("\"营口\", \"营口\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"澳门\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"澳门\", \"澳门\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"内蒙古\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"呼和浩特(*)\", \"呼和浩特\",");
            this.sbCityScript.Append("\"阿拉善盟\", \"阿拉善盟\",");
            this.sbCityScript.Append("\"包头\", \"包头\",");
            this.sbCityScript.Append("\"赤峰\", \"赤峰\",");
            this.sbCityScript.Append("\"东胜\", \"东胜\",");
            this.sbCityScript.Append("\"海拉尔\", \"海拉尔\",");
            this.sbCityScript.Append("\"集宁\", \"集宁\",");
            this.sbCityScript.Append("\"临河\", \"临河\",");
            this.sbCityScript.Append("\"通辽\", \"通辽\",");
            this.sbCityScript.Append("\"乌海\", \"乌海\",");
            this.sbCityScript.Append("\"乌兰浩特\", \"乌兰浩特\", ");
            this.sbCityScript.Append("\"锡林浩特\", \"锡林浩特\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"宁夏\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"银川(*)\", \"银川\",");
            this.sbCityScript.Append("\"固源\", \"固源\", ");
            this.sbCityScript.Append("\"石嘴山\", \"石嘴山\", ");
            this.sbCityScript.Append("\"吴忠\", \"吴忠\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"青海\" :");
            this.sbCityScript.Append("var cityOptions = new Array(");
            this.sbCityScript.Append("\"不限\", \"不限\",\"西宁(*)\", \"西宁\",");
            this.sbCityScript.Append("\"德令哈\", \"德令哈\",");
            this.sbCityScript.Append("\"格尔木\", \"格尔木\",");
            this.sbCityScript.Append("\"共和\", \"共和\",");
            this.sbCityScript.Append("\"海东\", \"海东\",");
            this.sbCityScript.Append("\"海晏\", \"海晏\",");
            this.sbCityScript.Append("\"玛沁\", \"玛沁\",");
            this.sbCityScript.Append("\"同仁\", \"同仁\", ");
            this.sbCityScript.Append("\"玉树\", \"玉树\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"山东\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"济南(*)\", \"济南\",");
            this.sbCityScript.Append("\"滨州\", \"滨州\",");
            this.sbCityScript.Append("\"兖州\", \"兖州\",");
            this.sbCityScript.Append("\"德州\", \"德州\",");
            this.sbCityScript.Append("\"东营\", \"东营\",");
            this.sbCityScript.Append("\"荷泽\", \"荷泽\",");
            this.sbCityScript.Append("\"济宁\", \"济宁\",");
            this.sbCityScript.Append("\"莱芜\", \"莱芜\",");
            this.sbCityScript.Append("\"聊城\", \"聊城\",");
            this.sbCityScript.Append("\"临沂\", \"临沂\",");
            this.sbCityScript.Append("\"蓬莱\", \"蓬莱\",");
            this.sbCityScript.Append("\"青岛\", \"青岛\",");
            this.sbCityScript.Append("\"曲阜\", \"曲阜\",");
            this.sbCityScript.Append("\"日照\", \"日照\",");
            this.sbCityScript.Append("\"泰安\", \"泰安\",");
            this.sbCityScript.Append("\"潍坊\", \"潍坊\",");
            this.sbCityScript.Append("\"威海\", \"威海\",");
            this.sbCityScript.Append("\"烟台\", \"烟台\",");
            this.sbCityScript.Append("\"枣庄\", \"枣庄\",");
            this.sbCityScript.Append("\"淄博\", \"淄博\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"上海\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"上海\", \"上海\", ");
            this.sbCityScript.Append("\"崇明\", \"崇明\", ");
            this.sbCityScript.Append("\"朱家角\", \"朱家角\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"山西\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"太原(*)\", \"太原\",");
            this.sbCityScript.Append("\"长治\", \"长治\",");
            this.sbCityScript.Append("\"大同\", \"大同\",");
            this.sbCityScript.Append("\"候马\", \"候马\",");
            this.sbCityScript.Append("\"晋城\", \"晋城\",");
            this.sbCityScript.Append("\"离石\", \"离石\",");
            this.sbCityScript.Append("\"临汾\", \"临汾\",");
            this.sbCityScript.Append("\"宁武\", \"宁武\",");
            this.sbCityScript.Append("\"朔州\", \"朔州\",");
            this.sbCityScript.Append("\"忻州\", \"忻州\",");
            this.sbCityScript.Append("\"阳泉\", \"阳泉\", ");
            this.sbCityScript.Append("\"榆次\", \"榆次\", ");
            this.sbCityScript.Append("\"运城\", \"运城\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"陕西\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"西安(*)\", \"西安\",");
            this.sbCityScript.Append("\"安康\", \"安康\",");
            this.sbCityScript.Append("\"宝鸡\", \"宝鸡\",");
            this.sbCityScript.Append("\"汉中\", \"汉中\",");
            this.sbCityScript.Append("\"渭南\", \"渭南\",");
            this.sbCityScript.Append("\"商州\", \"商州\",");
            this.sbCityScript.Append("\"绥德\", \"绥德\",");
            this.sbCityScript.Append("\"铜川\", \"铜川\",");
            this.sbCityScript.Append("\"咸阳\", \"咸阳\",");
            this.sbCityScript.Append("\"延安\", \"延安\",");
            this.sbCityScript.Append("\"榆林\", \"榆林\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"四川\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"成都(*)\", \"成都\",");
            this.sbCityScript.Append("\"巴中\", \"巴中\",");
            this.sbCityScript.Append("\"达安\", \"达安\",");
            this.sbCityScript.Append("\"德阳\", \"德阳\",");
            this.sbCityScript.Append("\"都江堰\", \"都江堰\",");
            this.sbCityScript.Append("\"峨眉山\", \"峨眉山\",");
            this.sbCityScript.Append("\"涪陵\", \"涪陵\",");
            this.sbCityScript.Append("\"广安\", \"广安\",");
            this.sbCityScript.Append("\"广元\", \"广元\",");
            this.sbCityScript.Append("\"九寨沟\", \"九寨沟\",");
            this.sbCityScript.Append("\"康定\", \"康定\",");
            this.sbCityScript.Append("\"乐山\", \"乐山\",");
            this.sbCityScript.Append("\"泸州\", \"泸州\",");
            this.sbCityScript.Append("\"马尔康\", \"马尔康\",");
            this.sbCityScript.Append("\"绵阳\", \"绵阳\",");
            this.sbCityScript.Append("\"南充\", \"南充\",");
            this.sbCityScript.Append("\"内江\", \"内江\",");
            this.sbCityScript.Append("\"攀枝花\", \"攀枝花\",");
            this.sbCityScript.Append("\"遂宁\", \"遂宁\",");
            this.sbCityScript.Append("\"汶川\", \"汶川\",");
            this.sbCityScript.Append("\"西昌\", \"西昌\",");
            this.sbCityScript.Append("\"雅安\", \"雅安\",");
            this.sbCityScript.Append("\"宜宾\", \"宜宾\", ");
            this.sbCityScript.Append("\"自贡\", \"自贡\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"台湾\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"台北(*)\", \"台北\",");
            this.sbCityScript.Append("\"基隆\", \"基隆\", ");
            this.sbCityScript.Append("\"台南\", \"台南\", ");
            this.sbCityScript.Append("\"台中\", \"台中\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"天津\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"天津\", \"天津\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"新疆\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"乌鲁木齐(*)\", \"乌鲁木齐\",");
            this.sbCityScript.Append("\"阿克苏\", \"阿克苏\",");
            this.sbCityScript.Append("\"阿勒泰\", \"阿勒泰\",");
            this.sbCityScript.Append("\"阿图什\", \"阿图什\",");
            this.sbCityScript.Append("\"博乐\", \"博乐\",");
            this.sbCityScript.Append("\"昌吉\", \"昌吉\",");
            this.sbCityScript.Append("\"东山\", \"东山\",");
            this.sbCityScript.Append("\"哈密\", \"哈密\",");
            this.sbCityScript.Append("\"和田\", \"和田\",");
            this.sbCityScript.Append("\"喀什\", \"喀什\",");
            this.sbCityScript.Append("\"克拉玛依\", \"克拉玛依\",");
            this.sbCityScript.Append("\"库车\", \"库车\",");
            this.sbCityScript.Append("\"库尔勒\", \"库尔勒\",");
            this.sbCityScript.Append("\"奎屯\", \"奎屯\",");
            this.sbCityScript.Append("\"石河子\", \"石河子\",");
            this.sbCityScript.Append("\"塔城\", \"塔城\",");
            this.sbCityScript.Append("\"吐鲁番\", \"吐鲁番\", ");
            this.sbCityScript.Append("\"伊宁\", \"伊宁\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"西藏\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"拉萨(*)\", \"拉萨\",");
            this.sbCityScript.Append("\"阿里\", \"阿里\",");
            this.sbCityScript.Append("\"昌都\", \"昌都\",");
            this.sbCityScript.Append("\"林芝\", \"林芝\",");
            this.sbCityScript.Append("\"那曲\", \"那曲\", ");
            this.sbCityScript.Append("\"日喀则\", \"日喀则\", ");
            this.sbCityScript.Append("\"山南\", \"山南\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"云南\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"昆明(*)\", \"昆明\",");
            this.sbCityScript.Append("\"大理\", \"大理\",");
            this.sbCityScript.Append("\"保山\", \"保山\",");
            this.sbCityScript.Append("\"楚雄\", \"楚雄\",");
            this.sbCityScript.Append("\"大理\", \"大理\",");
            this.sbCityScript.Append("\"东川\", \"东川\",");
            this.sbCityScript.Append("\"个旧\", \"个旧\",");
            this.sbCityScript.Append("\"景洪\", \"景洪\",");
            this.sbCityScript.Append("\"开远\", \"开远\",");
            this.sbCityScript.Append("\"临沧\", \"临沧\",");
            this.sbCityScript.Append("\"丽江\", \"丽江\",");
            this.sbCityScript.Append("\"六库\", \"六库\",");
            this.sbCityScript.Append("\"潞西\", \"潞西\",");
            this.sbCityScript.Append("\"曲靖\", \"曲靖\",");
            this.sbCityScript.Append("\"思茅\", \"思茅\",");
            this.sbCityScript.Append("\"文山\", \"文山\",");
            this.sbCityScript.Append("\"西双版纳\", \"西双版纳\",");
            this.sbCityScript.Append("\"玉溪\", \"玉溪\", ");
            this.sbCityScript.Append("\"中甸\", \"中甸\", ");
            this.sbCityScript.Append("\"昭通\", \"昭通\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"浙江\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"杭州(*)\", \"杭州\",");
            this.sbCityScript.Append("\"安吉\", \"安吉\",");
            this.sbCityScript.Append("\"慈溪\", \"慈溪\",");
            this.sbCityScript.Append("\"定海\", \"定海\",");
            this.sbCityScript.Append("\"奉化\", \"奉化\",");
            this.sbCityScript.Append("\"海盐\", \"海盐\",");
            this.sbCityScript.Append("\"黄岩\", \"黄岩\",");
            this.sbCityScript.Append("\"湖州\", \"湖州\",");
            this.sbCityScript.Append("\"嘉兴\", \"嘉兴\",");
            this.sbCityScript.Append("\"金华\", \"金华\",");
            this.sbCityScript.Append("\"临安\", \"临安\",");
            this.sbCityScript.Append("\"临海\", \"临海\",");
            this.sbCityScript.Append("\"丽水\", \"丽水\",");
            this.sbCityScript.Append("\"宁波\", \"宁波\",");
            this.sbCityScript.Append("\"瓯海\", \"瓯海\",");
            this.sbCityScript.Append("\"平湖\", \"平湖\",");
            this.sbCityScript.Append("\"千岛湖\", \"千岛湖\",");
            this.sbCityScript.Append("\"衢州\", \"衢州\",");
            this.sbCityScript.Append("\"瑞安\", \"瑞安\",");
            this.sbCityScript.Append("\"绍兴\", \"绍兴\",");
            this.sbCityScript.Append("\"嵊州\", \"嵊州\",");
            this.sbCityScript.Append("\"台州\", \"台州\",");
            this.sbCityScript.Append("\"温岭\", \"温岭\",");
            this.sbCityScript.Append("\"温州\", \"温州\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("case \"其他\" :");
            this.sbCityScript.Append("var cityOptions = new Array( ");
            this.sbCityScript.Append("\"不限\", \"不限\",\"其他\", \"其他\");");
            this.sbCityScript.Append("break;");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("document.all.city.options.length = 0;");
            this.sbCityScript.Append("for(var i = 0; i < cityOptions.length/2; i++) {");
            this.sbCityScript.Append("document.all.city.options[i]=new Option(cityOptions[i*2],cityOptions[i*2+1]);");
            this.sbCityScript.Append("if (document.all.city.options[i].value==\"\") document.all.city.selectedIndex = i;");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("ChangeCity(document.all.city.options[document.all.city.selectedIndex].value);");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("function initprovcity() {");
            this.sbCityScript.Append("for(var i = 0; i < document.all.prv.options.length; i++) {");
            this.sbCityScript.Append("if (document.all.prv.options[i].value==\"\") document.all.prv.selectedIndex = i;");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("setcity();");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("function window.onload()");
            this.sbCityScript.Append("{");
            this.sbCityScript.Append("initprovcity();");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("");
            this.sbCityScript.Append("function ChangeCity(City)");
            this.sbCityScript.Append("{");
            this.sbCityScript.Append("document.all.hddCity.value=City;");
            this.sbCityScript.Append("}");
            this.sbCityScript.Append("</Script>");
        }

        private void GetClubNameResult()
        {
            this.sbList = new StringBuilder();
            this.strSearchName = this.tbSearchClub.Text.ToString().Trim();
            if (this.strSearchName == "")
            {
                this.sbList.Append("<tr align='center'><td colspan='5' height='25'><font color='red'>你输入的查询条件不足，请重新输入！</font></td></tr>");
            }
            else
            {
                this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
                DataTable searchResultByClubName = BTPAccountManager.GetSearchResultByClubName(this.strSearchName, this.intGender);
                if (searchResultByClubName == null)
                {
                    this.sbList.Append("<tr align='center'><td colspan='5' height='25'>没有任何记录。</td></tr>");
                }
                else
                {
                    foreach (DataRow row in searchResultByClubName.Rows)
                    {
                        string str;
                        string strNickName = row["NickName"].ToString().Trim();
                        row["ClubName"].ToString().Trim();
                        int intUserID = (int) row["UserID"];
                        string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                        int num2 = (byte) row["Levels"];
                        string strQQ = row["QQ"].ToString().Trim();
                        bool blnSex = (bool) row["Sex"];
                        int intWealth = (int) row["Wealth"];
                        string wealthName = AccountItem.GetWealthName(intWealth);
                        if (strQQ != "")
                        {
                            strQQ = StringItem.GetQQURL(strQQ);
                        }
                        else
                        {
                            strQQ = "暂无资料";
                        }
                        if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                        {
                            str = "否";
                        }
                        else
                        {
                            str = "<font color='red'>是</font>";
                        }
                        this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append("<td align='center' height='28'>" + wealthName + "</td>");
                        this.sbList.Append("<td align='center'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                        this.sbList.Append("<td align='center'>" + num2 + "</td>");
                        this.sbList.Append("<td align='center'>" + dev + "</td>");
                        this.sbList.Append("<td align='center'>" + str + "</td>");
                        this.sbList.Append("<td align='center'>" + strQQ + "</td>");
                        this.sbList.Append("</tr>");
                        this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                    }
                }
            }
        }

        private void GetGenderResult()
        {
            DataTable table;
            this.sbList = new StringBuilder();
            int request = SessionItem.GetRequest("Sex", 0);
            if ((request == 0) || this.blnBtn)
            {
                this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
            }
            else
            {
                switch (request)
                {
                    case 1:
                        this.intGender = 0;
                        goto Label_006E;

                    case 2:
                        this.intGender = 1;
                        goto Label_006E;
                }
                this.intGender = 2;
            }
        Label_006E:
            table = BTPAccountManager.GetSearchResultByGender(0, this.intPage, this.intPrePage, this.intGender);
            if (table == null)
            {
                this.sbList.Append("<tr align='center'><td colspan='5' height='25'>没有任何记录。</td></tr>");
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str;
                    string strNickName = row["NickName"].ToString().Trim();
                    row["ClubName"].ToString().Trim();
                    int intUserID = (int) row["UserID"];
                    string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                    int num3 = (byte) row["Levels"];
                    string strQQ = row["QQ"].ToString().Trim();
                    bool blnSex = (bool) row["Sex"];
                    int intWealth = (int) row["Wealth"];
                    string wealthName = AccountItem.GetWealthName(intWealth);
                    if (strQQ != "")
                    {
                        strQQ = StringItem.GetQQURL(strQQ);
                    }
                    else
                    {
                        strQQ = "暂无资料";
                    }
                    if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                    {
                        str = "<font color='666666'>否</font>";
                    }
                    else
                    {
                        str = "<font color='red'>是</font>";
                    }
                    this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#eeeeee'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbList.Append("<td align='center' height='28'>" + wealthName + "</td>");
                    this.sbList.Append("<td align='center'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                    this.sbList.Append("<td align='center'>" + num3 + "</td>");
                    this.sbList.Append("<td align='center'>" + dev + "</td>");
                    this.sbList.Append("<td align='center'>" + str + "</td>");
                    this.sbList.Append("<td align='center'>" + strQQ + "</td>");
                    this.sbList.Append("</tr>");
                    this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                }
                string strCurrentURL = "MessageCenter.aspx?Type=SEARCH&Status=GENDER&Check=1&Sex=" + (this.intGender + 1) + "&";
                this.sbList.Append("<tr><td align='right' colspan=6>" + this.GetViewPage(strCurrentURL) + "</td></tr>");
                this.strScript2 = this.GetScript(strCurrentURL);
            }
        }

        private void GetNickNameResult()
        {
            this.sbList = new StringBuilder();
            this.strSearchName = this.tbSearchNick.Text.ToString().Trim();
            if (this.strSearchName == "")
            {
                this.sbList.Append("<tr align='center'><td colspan='5' height='25'><font color='red'>你输入的查询条件不足，请重新输入！</font></td></tr>");
            }
            else
            {
                this.intGender = Convert.ToInt32(this.rbGender.SelectedValue);
                DataTable searchResultByNickName = BTPAccountManager.GetSearchResultByNickName(this.strSearchName, this.intGender);
                if (searchResultByNickName == null)
                {
                    this.sbList.Append("<tr align='center'><td colspan='5' height='25'>没有任何记录。</td></tr>");
                }
                else
                {
                    foreach (DataRow row in searchResultByNickName.Rows)
                    {
                        string str;
                        string strNickName = row["NickName"].ToString().Trim();
                        row["ClubName"].ToString().Trim();
                        int intUserID = (int) row["UserID"];
                        string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(intUserID));
                        int num2 = (byte) row["Levels"];
                        string strQQ = row["QQ"].ToString().Trim();
                        bool blnSex = (bool) row["Sex"];
                        int intWealth = (int) row["Wealth"];
                        string wealthName = AccountItem.GetWealthName(intWealth);
                        if (strQQ != "")
                        {
                            strQQ = StringItem.GetQQURL(strQQ);
                        }
                        else
                        {
                            strQQ = "暂无资料";
                        }
                        if (DTOnlineManager.GetOnlineRowByUserID(intUserID) == null)
                        {
                            str = "否";
                        }
                        else
                        {
                            str = "<font color='red'>是</font>";
                        }
                        this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#eeeeee'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbList.Append("<td align='center' height='28'>" + wealthName + "</td>");
                        this.sbList.Append("<td align='center'>" + AccountItem.GetNickNameInfo(intUserID, strNickName, "Right", blnSex) + "</td>");
                        this.sbList.Append("<td align='center'>" + num2 + "</td>");
                        this.sbList.Append("<td align='center'>" + dev + "</td>");
                        this.sbList.Append("<td align='center'>" + str + "</td>");
                        this.sbList.Append("<td align='center'>" + strQQ + "</td>");
                        this.sbList.Append("</tr>");
                        this.sbList.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                    }
                }
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int num = 0;
            if (this.strStatus == "GENDER")
            {
                return BTPAccountManager.GetSearchCountByGender(1, this.intGender);
            }
            if (this.strStatus == "CITY")
            {
                return BTPAccountManager.GetSearchCountByCity(1, 0, 0, this.strProvince, this.strCity, this.intGender);
            }
            if (this.strStatus == "AGE")
            {
                num = BTPAccountManager.GetSearchCountByAge(1, 0, 0, this.strStartYear, this.strEndYear, this.intGender);
            }
            return num;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            int total = this.GetTotal();
            int num2 = (total / this.intPrePage) + 1;
            if ((total % this.intPrePage) == 0)
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
            switch (this.strType)
            {
                case "MSGLIST":
                    base.Response.Redirect("MessageAndFriend.aspx?Type=MSGLIST&Page=1");
                    break;

                case "FRIENDLIST":
                    base.Response.Redirect("MessageAndFriend.aspx?Type=FRIENDLIST&Page=1");
                    break;

                case "SENDMSG":
                    this.tblSendMsg.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a href='MessageAndFriend.aspx?Type=MSGLIST&Page=1'>短信列表</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageAndFriend.aspx?Type=SENDMSGMY'>发件箱</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>发送短信</li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=FRIENDLIST\"' href='MessageAndFriend.aspx?Type=FRIENDLIST'>好友列表</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.SendMsg();
                    this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
                    break;

                case "ADDFRIEND":
                    this.tblAddFriend.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a href='MessageAndFriend.aspx?Type=MSGLIST&Page=1'>短信列表</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                    this.sbPageIntro.Append("<a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=FRIENDLIST'><img src='" + SessionItem.GetImageURL() + "MenuCard/Message/Message_C_03.gif' border='0' height='24' width='75'></a>");
                    this.sbPageIntro.Append("<img src='" + SessionItem.GetImageURL() + "MenuCard/Message/Message_04.gif' border='0' height='24' width='75'>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=SEARCH\"' href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>经理查询</a></li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.btnAdd.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.AddFriend();
                    this.btnAdd.Click += new ImageClickEventHandler(this.btnAdd_Click);
                    break;

                case "SEARCH":
                    this.tblSearchManager.Visible = true;
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a href='MessageAndFriend.aspx?Type=MSGLIST&Page=1'>短信列表</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageAndFriend.aspx?Type=SENDMSGMY'>发件箱</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=MSGLIST\"' href='MessageCenter.aspx?Type=SENDMSG&UserID=0'>发送短信</a></li>");
                    this.sbPageIntro.Append("<li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/MessageCenter.aspx?Type=FRIENDLIST\"' href='MessageAndFriend.aspx?Type=FRIENDLIST'>好友列表</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>经理查询</li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.intPage = SessionItem.GetRequest("Page", 0);
                    this.intCheck = SessionItem.GetRequest("Check", 0);
                    if (this.intPage <= 0)
                    {
                        this.intPage = 1;
                    }
                    break;

                default:
                    base.Response.Redirect("MessageAndFriend.aspx?Type=MSGLIST&Page=1");
                    break;
            }
            if (this.strType == "SEARCH")
            {
                string strStatus = this.strStatus;
                if (strStatus == null)
                {
                    goto Label_0504;
                }
                if (strStatus != "CITY")
                {
                    if (strStatus == "GENDER")
                    {
                        this.sbSearchPageIntro.Append("<a href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>城市查询</a>");
                        this.sbSearchPageIntro.Append(" | <font color='red'>性别查询</font>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&&Status=NICKNAME'>经理名查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=CLUBNAME'>俱乐部名查询</a>");
                        goto Label_055A;
                    }
                    if (strStatus == "NICKNAME")
                    {
                        this.sbSearchPageIntro.Append("<a href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>城市查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=GENDER'>性别查询</a>");
                        this.sbSearchPageIntro.Append(" | <font color='red'>经理名查询</font>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=CLUBNAME'>俱乐部名查询</a>");
                        this.tblNick.Visible = true;
                        goto Label_055A;
                    }
                    if (strStatus == "AGE")
                    {
                        this.sbSearchPageIntro.Append("<a href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>城市查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=GENDER'>性别查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&&Status=NICKNAME'>经理名查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=CLUBNAME'>俱乐部名查询</a>");
                        this.tblAge.Visible = true;
                        goto Label_055A;
                    }
                    if (strStatus == "CLUBNAME")
                    {
                        this.sbSearchPageIntro.Append("<a href='MessageCenter.aspx?Type=SEARCH&Status=CITY'>城市查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=GENDER'>性别查询</a>");
                        this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&&Status=NICKNAME'>经理名查询</a>");
                        this.sbSearchPageIntro.Append(" | <font color='red'>俱乐部名查询</font>");
                        this.tblClub.Visible = true;
                        goto Label_055A;
                    }
                    goto Label_0504;
                }
                this.sbSearchPageIntro.Append("<font color='red'>城市查询</font>");
                this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=GENDER'>性别查询</a>");
                this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&&Status=NICKNAME'>经理名查询</a>");
                this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=CLUBNAME'>俱乐部名查询</a>");
                this.GetCityScript();
                this.tblCity.Visible = true;
                this.SetProvince();
            }
            goto Label_055A;
        Label_0504:
            this.sbSearchPageIntro.Append("<font color='red'>城市查询</font>");
            this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=GENDER'>性别查询</a>");
            this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&&Status=NICKNAME'>经理名查询</a>");
            this.sbSearchPageIntro.Append(" | <a href='MessageCenter.aspx?Type=SEARCH&Status=CLUBNAME'>俱乐部名查询</a>");
            this.GetCityScript();
            this.tblCity.Visible = true;
        Label_055A:
            this.btnSearchOK.Click += new ImageClickEventHandler(this.btnSearchOK_Click);
            if ((this.intPage > 1) || (this.intCheck == 1))
            {
                this.Search();
            }
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
                this.tblSendMsg.Visible = false;
                this.tblAddFriend.Visible = false;
                this.tblSearchManager.Visible = false;
                this.tblAge.Visible = false;
                this.tblCity.Visible = false;
                this.tblNick.Visible = false;
                this.tblClub.Visible = false;
                this.btnSearchOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.strType = SessionItem.GetRequest("Type", 1);
                this.strStatus = SessionItem.GetRequest("Status", 1);
                this.Session["Status"] = this.strStatus;
                this.intCheck = SessionItem.GetRequest("Check", 0);
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void Search()
        {
            this.strStatus = this.Session["Status"].ToString().Trim();
            if (this.strStatus == "NICKNAME")
            {
                this.GetNickNameResult();
            }
            else if (this.strStatus == "CLUBNAME")
            {
                this.GetClubNameResult();
            }
            else if (this.strStatus == "CITY")
            {
                this.GetCityResult();
            }
            else if (this.strStatus == "AGE")
            {
                this.GetAgeResult();
            }
            else if (this.strStatus == "GENDER")
            {
                this.GetGenderResult();
            }
            else
            {
                base.Response.Redirect("");
            }
        }

        private void SendMsg()
        {
            this.intRUserID = SessionItem.GetRequest("UserID", 0);
            if (this.intRUserID == 0)
            {
                this.tbNickName.Text = "";
                this.strQQ = "";
            }
            else
            {
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intRUserID);
                string str = accountRowByUserID["NickName"].ToString().Trim();
                this.tbNickName.Text = str;
                bool flag1 = accountRowByUserID["QQ"].ToString().Trim() != "";
            }
        }

        private void SetProvince()
        {
            string[] strArray = new string[] { "上海", "北京", "广东", "广东", "浙江", "山东", "江苏", "江苏", "江苏", "江苏", "浙江", "福建", "福建" };
            string[] strArray2 = new string[] { "上海", "北京", "广州", "深圳", "杭州", "青岛", "南京", "常州", "无锡", "苏州", "宁波", "福州", "厦门" };
            for (int i = 0; i < 13; i++)
            {
                this.sbHotCity.Append("<a href='MessageCenter.aspx?Type=SEARCH&Status=CITY&Province=" + strArray[i] + "&City=" + strArray2[i] + "&Check=1'>" + strArray2[i] + "</a> | ");
            }
            string str = SessionItem.GetRequest("Province", 1).ToString().Trim();
            if (str != "")
            {
                HttpCookie cookie = new HttpCookie("Province") {
                    Value = str,
                    Name = "Province"
                };
                cookie.Expires.AddDays(365.0);
                base.Response.Cookies.Add(cookie);
                HttpCookie cookie2 = new HttpCookie("City") {
                    Value = "不限",
                    Name = "City"
                };
                cookie2.Expires.AddDays(365.0);
                base.Response.Cookies.Add(cookie2);
            }
            HttpContext current = HttpContext.Current;
            if (current.Request.Cookies["Province"] != null)
            {
                this.HidProvince.Value = current.Request.Cookies["Province"].Value;
                if (current.Request.Cookies["City"] == null)
                {
                    this.Hidcity2.Value = "不限";
                }
                else
                {
                    this.Hidcity2.Value = current.Request.Cookies["City"].Value;
                }
            }
            else
            {
                this.HidProvince.Value = "";
            }
            this.sbProvinceScript.Append("<SCRIPT language=\"JavaScript\">function ChangeProvince(){if(document.all.HidProvince.value != \"\"){document.all.prv.value=document.all.HidProvince.value;");
            this.sbProvinceScript.Append("setcity();document.all.city.value=document.all.Hidcity2.value;}}</Script>");
            this.strScript = "ChangeProvince()";
        }
    }
}


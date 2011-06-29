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

    public class ShowClub : Page
    {
        protected ImageButton btnOK;
        protected ImageButton btnSend;
        protected DropDownList ddlCategory;
        private DateTime dtCreateTime;
        protected HtmlInputHidden hidSelectValue;
        private int intCategory;
        private int intClub3ID;
        private int intClub5ID;
        private int intClubID = 0;
        private int intClubID3;
        private int intClubID3My;
        private int intClubID5;
        private int intClubID5My;
        private int intLevel;
        private int intPage;
        public int intPayType;
        private int intPerPage;
        private int intType;
        private int intUserID;
        private int intUserIDA;
        protected RadioButton rbAlway;
        protected RadioButton rbMain;
        protected RadioButton rbS;
        protected RadioButton rbV;
        public string strClubInfo;
        private string strClubLogo;
        private string strClubName3;
        private string strClubName5;
        public string strList;
        private string strLogoLink;
        public string strMainScript;
        private string strNickName;
        private string strNickNameA;
        public string strOnload = "";
        public string strPageIntro;
        public string strPlayerList;
        public string strScript;
        public string strToolsImg;
        protected TextBox tbIntro;
        protected HtmlTable tblShowClub;
        protected TextBox tbMsg;
        protected TextBox tbNickName;
        protected TextBox tbNickNameF;
        protected TextBox tbPoint;
        protected TextBox tbWealth;
        protected TextBox tbWealthA;
        protected TextBox tbWealthB;
        protected HtmlTableCell tdFMatch;
        protected HtmlTableCell tdMsg;
        protected HtmlTableCell tdPlayer;
        protected HtmlTableRow trPageIntro;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            int num7;
            string strNickName = this.tbNickNameF.Text.ToString().Trim();
            string strIn = this.tbIntro.Text.ToString().Trim();
            int intCategory = Convert.ToInt16(this.hidSelectValue.Value);
            int intUserID = 0;
            int intWealthA = 0;
            int intWealthB = 0;
            int intClubAPoint = 0;
            int intClubBPoint = 0;
            this.strOnload = this.strOnload + "SetRB()";
            if (this.rbS.Checked)
            {
                num7 = 3;
            }
            else
            {
                num7 = 5;
            }
            if (strNickName == "")
            {
                this.strList = "<font color='red'>经理名不能为空，无法发出约战请求！</font>";
            }
            else
            {
                switch (num7)
                {
                    case 3:
                    {
                        if (BTPPlayer3Manager.GetPlayer3CountByCID(this.intClubID3) < 4)
                        {
                            this.strList = "<font color='red'>您的街球队人数不足，无法发出约战请求！</font>";
                            return;
                        }
                        int clubIDByNickName = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                        if (clubIDByNickName == 0)
                        {
                            this.strList = "<font color='red'>未找到该球队！</font>";
                            return;
                        }
                        if (BTPPlayer3Manager.GetPlayer3CountByCID(clubIDByNickName) < 4)
                        {
                            this.strList = "<font color='red'>对方球队人数不足，无法发出约战请求！</font>";
                            return;
                        }
                        intUserID = BTPClubManager.GetUserIDByClubID(clubIDByNickName);
                        break;
                    }
                    case 5:
                    {
                        if (intCategory == 2)
                        {
                            this.strList = "<font color='red'>暂时没有开放职业训练赛功能！</font>";
                            return;
                        }
                        if (BTPPlayer5Manager.GetPlayer5CountByClubID(this.intClubID5) < 6)
                        {
                            this.strList = "<font color='red'>您的职业球队人数不足，无法发出约战请求！</font>";
                            return;
                        }
                        int intClubIDB = BTPClubManager.GetClubIDByNickName(strNickName, 5);
                        if (MatchItem.InOnlyOneMatch(this.intClubID5, intClubIDB))
                        {
                            this.strList = "<font color='red'>有一方在胜者为王赛中报名，无法发起约战！</font>";
                            return;
                        }
                        if (intClubIDB == 0)
                        {
                            this.strList = "<font color='red'>未找到该球队！</font>";
                            return;
                        }
                        if (BTPPlayer5Manager.GetPlayer5CountByClubID(intClubIDB) < 6)
                        {
                            this.strList = "<font color='red'>对方球队人数不足，无法发出约战请求！</font>";
                            return;
                        }
                        intUserID = BTPClubManager.GetUserIDByClubID(intClubIDB);
                        break;
                    }
                }
                if (intCategory == 2)
                {
                    int intClubIDA = this.intClubID3My;
                    int num15 = BTPClubManager.GetClubIDByNickName(strNickName, 3);
                    switch (MatchItem.TrainMatchType(intClubIDA, num15, 1))
                    {
                        case 2:
                            this.strList = "<font color='red'>球员训练赛热情不足！</font>";
                            return;

                        case 3:
                            this.strList = "<font color='red'>球员训练赛热情不足！</font>";
                            return;

                        case 4:
                            this.strList = "<font color='red'>有正在进行或等待进行的比赛，无法进行训练赛！请在约战信息中查看是否有没有接受或者拒绝的训练赛、友谊赛等。</font>";
                            return;

                        case 5:
                            this.strList = "<font color='red'>您与此球队在本赛季已经训练过了，不能重复训练！</font>";
                            return;

                        case 6:
                            this.strList = "<font color='red'>您或者对方在今日已经不能再打训练赛了！</font>";
                            return;
                    }
                }
                if (intCategory == 10)
                {
                    int clubIDByUIDCategory = BTPClubManager.GetClubIDByUIDCategory(this.intUserID, 5);
                    int num18 = BTPClubManager.GetClubIDByNickName(strNickName, 5);
                    switch (MatchItem.TrainMatchType5(clubIDByUIDCategory, num18, 1))
                    {
                        case 2:
                            this.strList = "<font color='red'>您有正在等待进行的友谊赛！</font>";
                            return;

                        case 3:
                            this.strList = "<font color='red'>您有正在等待进行的表演赛！</font>";
                            return;

                        case 4:
                            this.strList = "<font color='red'>您有正在等待进行的训练赛！</font>";
                            return;

                        case 5:
                            this.strList = "<font color='red'>与该球队在本赛季有过训练赛！</font>";
                            return;

                        case 6:
                            this.strList = "<font color='red'>您本日的训练赛已经超过三场！</font>";
                            return;

                        case 7:
                            this.strList = "<font color='red'>对方有正在等待进行的友谊赛！</font>";
                            return;

                        case 8:
                            this.strList = "<font color='red'>对方有正在等待进行的表演赛！</font>";
                            return;

                        case 9:
                            this.strList = "<font color='red'>对方有正在等待进行的训练赛！</font>";
                            return;

                        case 10:
                            this.strList = "<font color='red'>您的球队体力不足！</font>";
                            return;

                        case 11:
                            this.strList = "<font color='red'>对方的球队体力不足！</font>";
                            return;
                    }
                }
                if (intCategory == 3)
                {
                    int num20 = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
                    int num21 = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["Wealth"];
                    string str3 = "";
                    string str4 = "";
                    if (StringItem.IsNumber(str3) && StringItem.IsNumber(str4))
                    {
                        if ((StringItem.GetStrLength(str3.Trim()) <= 4) || (StringItem.GetStrLength(str4.Trim()) <= 4))
                        {
                            intWealthA = Convert.ToInt32(str3);
                            intWealthB = Convert.ToInt32(str4);
                        }
                        else
                        {
                            this.strList = "<font color='red'>您输入的游戏币超出范围请输入10-1000之内的（半角）数字！</font>";
                            return;
                        }
                        if (((intWealthA > 0x3e8) || (intWealthB > 0x3e8)) || ((intWealthA < 10) || (intWealthB < 10)))
                        {
                            this.strList = "<font color='red'>您输入的游戏币超出范围请输入10-1000之内的（半角）数字！</font>";
                            return;
                        }
                    }
                    else
                    {
                        this.strList = "请您在游戏币和让分栏中输入正确的半角数字！";
                        return;
                    }
                    if (intWealthA > num20)
                    {
                        this.strList = "不能超过你取出游戏币！";
                        return;
                    }
                    if (intWealthB > num21)
                    {
                        this.strList = "<font color='red'>对方现在没有足够游戏币，不能超出他游戏币！</font>";
                        return;
                    }
                    int runMatchCount = BTPFriMatchManager.GetRunMatchCount();
                    if ((this.intPayType != 1) && (runMatchCount >= 20))
                    {
                        this.strList = "<font color='red'>有20场以上约战在进行中请稍后再约（会员不受此限制）！</font>";
                        return;
                    }
                    if (num7 == 3)
                    {
                        this.strList = "<font color='red'>暂时只对职业队开放！</font>";
                        return;
                    }
                }
                if (intCategory == 4)
                {
                    int num23 = (int) BTPAccountManager.GetAccountRowByUserID(this.intUserID)["Wealth"];
                    int num24 = (int) BTPAccountManager.GetAccountRowByNickName(strNickName)["Wealth"];
                    string str5 = this.tbWealth.Text.ToString();
                    string str6 = this.tbPoint.Text.ToString();
                    if (StringItem.IsNumber(str5) && StringItem.IsNumber(str6))
                    {
                        if (StringItem.GetStrLength(str5.Trim()) <= 4)
                        {
                            intWealthA = Convert.ToInt32(str5);
                            intWealthB = Convert.ToInt32(str5);
                        }
                        else
                        {
                            this.strList = "<font color='red'>您输入的游戏币超出范围请输入10-1000之内的（半角）数字！</font>";
                            return;
                        }
                        if ((intWealthA > 0x3e8) || (intWealthA < 10))
                        {
                            this.strList = "<font color='red'>您输入的游戏币超出范围请输入10-1000之内的（半角）数字！</font>";
                            return;
                        }
                        if (!this.rbMain.Checked && !this.rbAlway.Checked)
                        {
                            this.strList = "<font color='red'>请选择是主队让分还是客队让分！</font>";
                            return;
                        }
                        if (StringItem.GetStrLength(str6.Trim()) >= 3)
                        {
                            this.strList = "<font color='red'>您输入的让分超出范围请输入1-100之内（半角）数字！</font>";
                            return;
                        }
                        if (this.rbMain.Checked)
                        {
                            intClubAPoint = Convert.ToInt32(str6);
                        }
                        if (this.rbAlway.Checked)
                        {
                            intClubBPoint = Convert.ToInt32(str6);
                        }
                    }
                    else
                    {
                        this.strList = "<font color='red'>请您在游戏币和让分栏中输入正确的半角数字！</font>";
                        return;
                    }
                    if (intWealthA > num23)
                    {
                        this.strList = "<font color='red'>不能超过你取出游戏币！</font>";
                        return;
                    }
                    if (intWealthB > num24)
                    {
                        this.strList = "<font color='red'>对方现在没有足够游戏币，不能超出他游戏币！</font>";
                        return;
                    }
                    int num25 = BTPFriMatchManager.GetRunMatchCount();
                    if ((this.intPayType != 1) && (num25 >= 20))
                    {
                        this.strList = "<font color='red'>有20场以上约战在进行中请稍后再约（会员不受此限制）！</font>";
                        return;
                    }
                    if (num7 == 3)
                    {
                        this.strList = "<font color='red'>暂时只对职业队开放！</font>";
                        return;
                    }
                }
                strIn = StringItem.GetValidWords(strIn);
                if (!StringItem.IsValidName(strIn, 0, 200))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3ab");
                }
                else
                {
                    switch (BTPFriMatchManager.SetFriMatch(strNickName, this.intUserID, intCategory, num7, strIn, intWealthA, intWealthB, intClubAPoint, intClubBPoint))
                    {
                        case 8:
                            this.strList = "<font color='red'>接收到的约战信息有错误！</font>";
                            return;

                        case 9:
                            this.strList = "<font color='red'>您输入的游戏币超出范围请输入10-1000之内的（半角）数字！</font>";
                            return;

                        case 10:
                            this.strList = "<font color='red'>您输入的让分超出范围请输入1-100之内（半角）数字！</font>";
                            return;

                        case 11:
                            this.strList = "<font color='red'>游戏币不能小于10，让分不能小于1！</font>";
                            return;

                        case 12:
                            this.strList = "<font color='red'>游戏币不能小于10！</font>";
                            return;

                        case 1:
                            DTOnlineManager.SetHasMsgByUserID(intUserID);
                            this.strList = "<font color='red'>发送已成功！</font>";
                            this.tbIntro.Text = "";
                            return;

                        case 2:
                            this.strList = "<font color='red'>没有此球队，您不能向其发送约战邀请！</font>";
                            return;

                        case 3:
                            this.strList = "<font color='red'>您本轮已经打过一场街球训练赛了！</font>";
                            return;

                        case 4:
                            this.strList = "<font color='red'>您本轮已经打过一场职业训练赛了！</font>";
                            return;

                        case 0:
                            this.strList = "<font color='red'>您或此球队正在进行一场比赛，暂时不能再次发送请求！</font>";
                            return;

                        case 6:
                            this.strList = "<font color='red'>您不能与自己约战！</font>";
                            return;
                    }
                    this.strList = "<font color='red'>您无法对其发送约战请求，请核查！</font>";
                }
            }
        }

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            string text = this.tbNickName.Text;
            string validWords = StringItem.GetValidWords(StringItem.GetHtmlEncode(this.tbMsg.Text.ToString().Trim()));
            text = StringItem.GetValidWords(text);
            if (StringItem.IsValidName(text, 2, 0x10))
            {
                if (!StringItem.IsValidContent(validWords, 2, 500))
                {
                    this.strList = "<font color='#FF0000'>消息内容必须在2-500个字符之间！</font>";
                }
                else
                {
                    BTPMessageManager.SetHasMsg(text);
                    if (BTPMessageManager.AddMessage(this.intUserID, this.strNickNameA, text, validWords))
                    {
                        this.strList = "<font color='#FF0000'>消息发送成功！</font>";
                        this.tbMsg.Text = "";
                    }
                    else
                    {
                        this.strList = "<font color='#FF0000'>" + text + "并不存在，请查证是否输入正确，或对方以更改名称！</font>";
                    }
                }
            }
            else
            {
                this.strList = "<font color='#FF0000'>经理昵称不合要求！</font>";
            }
        }

        private void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void FMatchSend()
        {
            this.intUserIDA = (int) SessionItem.GetRequest("UserID", 0);
            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.strList = "";
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                this.rbV.Visible = false;
            }
            if (!base.IsPostBack)
            {
                DataView view = new DataView(DDLItem.GetFriMatchItem(1, this.intCategory));
                this.ddlCategory.DataSource = view;
                this.ddlCategory.DataTextField = "Name";
                this.ddlCategory.DataValueField = "Category";
                this.ddlCategory.DataBind();
                if (this.intUserIDA == 0)
                {
                    this.tbNickNameF.Text = "";
                }
                else
                {
                    string str = BTPAccountManager.GetNickNameByUserID(this.intUserIDA).Trim();
                    this.tbNickNameF.Text = str;
                }
            }
        }

        private void GetClubInfo()
        {
            string str;
            string str2;
            string str8;
            if (this.intType == 3)
            {
                str = this.strClubName3;
            }
            else if ((this.intCategory == 2) || (this.intCategory == 5))
            {
                str = this.strClubName5;
            }
            else
            {
                str = this.strClubName3;
            }
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserIDA);
            string strNickName = accountRowByUserID["NickName"].ToString().Trim();
            string strCity = accountRowByUserID["City"].ToString().Trim();
            string strProvince = accountRowByUserID["Province"].ToString().Trim();
            string str5 = accountRowByUserID["Birth"].ToString();
            DateTime dtActiveTime = (DateTime) accountRowByUserID["ActiveTime"];
            bool blnSex = (bool) accountRowByUserID["Sex"];
            int intUnionID = (int) accountRowByUserID["UnionID"];
            long lngOnlyPoint = (long) accountRowByUserID["OnlyPoint"];
            byte bytPayType = (byte) accountRowByUserID["PayType"];
            string strClubName = "";
            if (intUnionID > 0)
            {
                DataRow unionRowByID = BTPUnionManager.GetUnionRowByID(intUnionID);
                if (unionRowByID != null)
                {
                    strClubName = unionRowByID["Name"].ToString().Trim();
                }
            }
            int num5 = Convert.ToInt32(accountRowByUserID["Category"]);
            str = accountRowByUserID["ClubName"].ToString().Trim();
            int intPayType = Convert.ToInt32(accountRowByUserID["PayType"]);
            if (StringItem.IsNumber(str5.Substring(0, 4)))
            {
                str5 = str5.Substring(0, 4) + "-01-01";
            }
            else
            {
                str5 = "1980-01-01";
            }
            DateTime time2 = Convert.ToDateTime(str5);
            int intAge = DateTime.Now.Year - time2.Year;
            string dev = DevCalculator.GetDev(BTPDevManager.GetDevCodeByUserID(this.intUserIDA));
            if (num5 != 5)
            {
                dev = "未加入联赛";
            }
            if (((accountRowByUserID["ShortName"].ToString().Trim() == "") || (accountRowByUserID["ShortName"] == null)) || (intUnionID == 0))
            {
                str8 = "";
            }
            else
            {
                str8 = accountRowByUserID["ShortName"].ToString().Trim() + "-";
            }
            if (this.strLogoLink.Trim() == "")
            {
                str2 = AccountItem.GetNickLogoCard(strNickName, blnSex, intAge, strCity, strProvince, dev, dtActiveTime, intPayType, strClubName, this.strClubLogo, lngOnlyPoint);
            }
            else
            {
                str2 = "<a href='" + this.strLogoLink + "' target='_blank'>" + AccountItem.GetNickLogoCard(strNickName, blnSex, intAge, strCity, strProvince, dev, dtActiveTime, intPayType, strClubName, this.strClubLogo, lngOnlyPoint) + "</a>";
            }
            this.strClubInfo = "<table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td width='55' height=53 style='padding-left:3px;'　>" + str2 + "</td><td><table width='100%'  border='0' cellspacing='0' cellpadding='0'><tr><td colspan=2 ><strong>球队</strong>：" + StringItem.GetShortName(str8 + str, 11, ".") + "</td></tr><tr><td width=150><strong>经理</strong>：" + StringItem.GetShortName(strNickName, 8, ".", bytPayType) + "</td><td align=center width=50>" + AccountItem.GetNickCard(strNickName, blnSex, intAge, strCity, strProvince, dev, dtActiveTime, intPayType, strClubName, lngOnlyPoint) + "</td></tr></table></td></tr></table>";
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            return BTPHonorManager.GetHonorCount(this.intUserIDA);
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
            return string.Concat(new object[] { "共", total, "个记录 跳转", str2 });
        }

        private void HonorList()
        {
            string strCurrentURL = "ShowClub.aspx?Type=6&UserID=" + this.intUserIDA + "&";
            this.intPage = (int) SessionItem.GetRequest("Page", 0);
            this.intPerPage = 5;
            int intCount = this.intPerPage * this.intPage;
            int honorCount = BTPHonorManager.GetHonorCount(this.intUserIDA);
            this.strScript = this.GetScript(strCurrentURL);
            this.strPlayerList = "<tr class='BarHead' width='100%'><td width='40' height='25'>奖杯</td><td width='75'>荣誉</td><td width='86'>获得时间</td></tr>";
            DataTable table = BTPHonorManager.GetHonorList(this.intUserIDA, this.intPage, this.intPerPage, honorCount, intCount);
            if (table == null)
            {
                this.strPlayerList = this.strPlayerList + "<tr class='BarContent'><td height='25' colspan='3'>无任何荣誉！</td></tr>";
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2 = row["BigLogo"].ToString().Trim();
                    string str3 = row["Remark"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    string str4 = row["Devision"].ToString().Trim();
                    if (str4 != "")
                    {
                        str3 = str3 + "<br/>" + str4;
                    }
                    else
                    {
                        str3 = str3 + "<br/>" + StringItem.FormatDate(datIn, "yyMMdd");
                    }
                    string strPlayerList = this.strPlayerList;
                    this.strPlayerList = strPlayerList + "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25'><img src='" + SessionItem.GetImageURL() + "Cup/" + str2 + "'></td><td>" + str3 + "</td><td>" + StringItem.FormatDate(datIn, "yy-MM-dd") + "</td></tr>";
                }
            }
            this.strPlayerList = this.strPlayerList + "<tr><td height='25' align='right' colspan='3'>" + this.GetViewPage(strCurrentURL) + "</td></tr>";
        }

        private void InitializeComponent()
        {
            this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
            this.ddlCategory.SelectedIndexChanged += new EventHandler(this.ddlCategory_SelectedIndexChanged);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
                return;
            }
            this.strMainScript = "";
            DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
            this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
            this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
            this.intClubID3My = this.intClubID3;
            this.intClubID5My = this.intClubID5;
            this.intPayType = (int) onlineRowByUserID["PayType"];
            this.strNickNameA = onlineRowByUserID["NickName"].ToString().Trim();
            this.trPageIntro.Visible = false;
            this.tdMsg.Visible = false;
            this.tdPlayer.Visible = false;
            this.tdFMatch.Visible = false;
            this.intUserIDA = (int) SessionItem.GetRequest("UserID", 0);
            if (this.intUserIDA == 0)
            {
                this.intClubID = (int) SessionItem.GetRequest("ClubID", 0);
                this.intUserIDA = BTPClubManager.GetUserIDByClubID(this.intClubID);
            }
            this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserIDA);
            if (accountRowByUserID != null)
            {
                this.strLogoLink = accountRowByUserID["LogoLink"].ToString().Trim();
                this.dtCreateTime = (DateTime) accountRowByUserID["CreateTime"];
                this.strNickName = accountRowByUserID["NickName"].ToString().Trim();
                this.intCategory = (byte) accountRowByUserID["Category"];
                this.intClub3ID = (int) accountRowByUserID["ClubID3"];
                this.intClub5ID = (int) accountRowByUserID["ClubID5"];
                if ((bool) accountRowByUserID["Sex"])
                {
                    this.strNickName = "<font color='#ff005a'>" + this.strNickName + "</font>";
                }
                else
                {
                    this.strNickName = "<font color='blue'>" + this.strNickName + "</font>";
                }
                this.intLevel = (byte) accountRowByUserID["Levels"];
                int num = (int) accountRowByUserID["UnionID"];
                string str = accountRowByUserID["ShortName"].ToString().Trim();
                if (num > 0)
                {
                    str = str + "-";
                }
                else
                {
                    str = "";
                }
                this.strClubName3 = accountRowByUserID["ClubName"].ToString().Trim();
                this.strClubName5 = this.strClubName3;
                this.strClubName3 = StringItem.GetShortName(str + this.strClubName3, 0x11, ".");
                this.strClubName5 = StringItem.GetShortName(str + this.strClubName5, 0x11, ".");
                this.strClubLogo = accountRowByUserID["ClubLogo"].ToString().Trim();
            }
            this.intType = (int) SessionItem.GetRequest("Type", 0);
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                if (this.intCategory != 1)
                {
                    if (this.intCategory == 3)
                    {
                        base.Response.Redirect("Intro/PlayerCenter.aspx?Type=NONICKINFO");
                    }
                    else
                    {
                        base.Response.Redirect("Intro/PlayerCenter.aspx?Type=NONICKINFO");
                    }
                    goto Label_0A37;
                }
                switch (this.intType)
                {
                    case 3:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1'>街球</li> <li class='sqian2a'><font color='#aaaaaa'>职业</font></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.trPageIntro.Visible = true;
                        this.tdPlayer.Visible = true;
                        this.StreetPlayerList();
                        goto Label_0A37;

                    case 4:
                    case 5:
                        goto Label_0986;

                    case 6:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><font color='#aaaaaa'>职业</font></li><li class='sqian2'>荣誉</li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.tdPlayer.Visible = true;
                        this.HonorList();
                        goto Label_0A37;

                    case 7:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><font color='#aaaaaa'>职业</font></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2'>短信</li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.tdMsg.Visible = true;
                        this.SendMsg();
                        goto Label_0A37;

                    case 8:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><font color='#aaaaaa'>职业</font></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2'>约战</li></ul>" });
                        this.tdFMatch.Visible = true;
                        this.FMatchSend();
                        goto Label_0A37;
                }
            }
            else
            {
                switch (this.intType)
                {
                    case 3:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1'>街球</li><li class='sqian2a'><a href='ShowClub.aspx?Type=5&UserID=", this.intUserIDA, "' title='职业队'>职业</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.trPageIntro.Visible = true;
                        this.tdPlayer.Visible = true;
                        this.StreetPlayerList();
                        goto Label_0A37;

                    case 5:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2'>职业</li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.trPageIntro.Visible = true;
                        this.tdPlayer.Visible = true;
                        this.ProPlayerList();
                        goto Label_0A37;

                    case 6:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=5&UserID=", this.intUserIDA, "' title='职业队'>职业</a></li><li class='sqian2'>荣誉</li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.tdPlayer.Visible = true;
                        this.HonorList();
                        goto Label_0A37;

                    case 7:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=5&UserID=", this.intUserIDA, "' title='职业队'>职业</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2'>短信</li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                        this.tdMsg.Visible = true;
                        this.SendMsg();
                        goto Label_0A37;

                    case 8:
                        this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1a'><a href='ShowClub.aspx?Type=3&UserID=", this.intUserIDA, "'  title='街球队'>街球</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=5&UserID=", this.intUserIDA, "' title='职业队'>职业</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2'>约战</li></ul>" });
                        this.tdFMatch.Visible = true;
                        this.rbS.Attributes.Add("onclick", "CheckRB(this)");
                        this.rbV.Attributes.Add("onclick", "CheckRB(this)");
                        this.FMatchSend();
                        goto Label_0A37;
                }
                this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1'>街球</li>//<font color='#FF0000'>街球队</font><li class='sqian2a'><a href='ShowClub.aspx?Type=5&UserID=", this.intUserIDA, "' title='职业队'>职业</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
                this.trPageIntro.Visible = true;
                this.tdPlayer.Visible = true;
                this.StreetPlayerList();
                goto Label_0A37;
            }
        Label_0986:;
            this.strPageIntro = string.Concat(new object[] { "<ul><li class='sqian1'>街球</li><li class='sqian2a'><font color='#aaaaaa'>职业</font></li><li class='sqian2a'><a href='ShowClub.aspx?Type=6&UserID=", this.intUserIDA, "&Page=1' title='球队荣誉'>荣誉</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=7&UserID=", this.intUserIDA, "' title='短消息'>短信</a></li><li class='sqian2a'><a href='ShowClub.aspx?Type=8&UserID=", this.intUserIDA, "&Page=1' title='约战'>约战</a></li></ul>" });
            this.trPageIntro.Visible = true;
            this.tdPlayer.Visible = true;
            this.StreetPlayerList();
        Label_0A37:
            this.GetClubInfo();
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void ProPlayerList()
        {
            DataTable playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(this.intClub5ID);
            string devCodeByClubID = BTPDevManager.GetDevCodeByClubID(this.intClub5ID);
            //this.strToolsImg = string.Concat(new object[] { "<a href='DevisionView.aspx?UserID=", this.intUserIDA, "&Type=MATCHLOOK&Devision=", devCodeByClubID, "&Status=1&Page=1' target='Center'><img src='", SessionItem.GetImageURL(), "MinMatchLook.gif' width='12' height='12' border='0' alt='查看比赛录像'><a>" });
            SqlDataReader reader = BTPToolLinkManager.CheckClubLink(this.intUserID, this.intClub5ID, 5);
            bool flag = false;
            while (reader.Read())
            {
                this.intCategory = (byte) reader["Category"];
                if (this.intCategory == 1)
                {
                    flag = true;
                }
            }
            reader.Close();
            /*if (flag)
            {
                this.strToolsImg = string.Concat(new object[] { this.strToolsImg, "&nbsp;&nbsp;<a href='VArrange.aspx?ClubID=", this.intClub5ID, "&UserID=", this.intUserIDA, "&Type=6' target=Center><img src='", SessionItem.GetImageURL(), "MinMatchLev.gif' width='12' height='12' border='0'  alt='查看战术等级'><a>" });
            }
            else
            {
                this.strToolsImg = string.Concat(new object[] { this.strToolsImg, "&nbsp;&nbsp;<a href='SecretaryPage.aspx?ClubID=", this.intClub5ID, "&UserID=", this.intUserIDA, "&Type=MATCHLEV&ClubType=5' target=Center><img src='", SessionItem.GetImageURL(), "MinMatchLev.gif' width='12' height='12' border='0'  alt='查看战术等级'><a>" });
            }*/
            this.strToolsImg = "";
            if (playerTableByClubID == null)
            {
                this.strPlayerList = "<tr  class='BarContent'><td colspan='4'>没有任何球员！</td></tr>";
            }
            else
            {
                foreach (DataRow row in playerTableByClubID.Rows)
                {
                    string str = row["Name"].ToString().Trim();
                    int intPosition = (byte) row["Pos"];
                    int num2 = (byte) row["Number"];
                    int intAbility = (int) row["Ability"];
                    string playerEngPosition = PlayerItem.GetPlayerEngPosition(intPosition);
                    int num4 = (byte) row["Height"];
                    int num5 = (byte) row["Weight"];
                    int num6 = (byte) row["Age"];
                    long num7 = (long) row["PlayerID"];
                    object strPlayerList = this.strPlayerList;
                    this.strPlayerList = string.Concat(new object[] { 
                        strPlayerList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width='33'><img width='16' height='19' src='", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif'></td><td height='24' align='left' style='padding-left:4px'><div class=\"DIVPlayerName\"><a title='年龄：", num6, "<br>身高：", num4, " CM<br>体重：", num5, " KG' style='CURSOR: hand;color:#660066' href='ShowPlayer.aspx?Type=5&Kind=1&Check=0&PlayerID=", num7, "'>", str, "</a></div></td><td><a title='", 
                        PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td>", PlayerItem.GetAbilityColor(intAbility), "</td></tr>"
                     });
                }
            }
        }

        private void SendMsg()
        {
            this.intUserIDA = (int) SessionItem.GetRequest("UserID", 0);
            if (this.intUserIDA == 0)
            {
                this.tbNickName.Text = "";
            }
            else
            {
                string str = BTPAccountManager.GetAccountRowByUserID(this.intUserIDA)["NickName"].ToString().Trim();
                this.tbNickName.Text = str;
            }
        }

        private void StreetPlayerList()
        {
            SqlDataReader reader = BTPToolLinkManager.CheckClubLink(this.intUserID, this.intClub3ID, 3);
            bool flag = false;
            while (reader.Read())
            {
                this.intCategory = (byte) reader["Category"];
                if (this.intCategory == 1)
                {
                    flag = true;
                }
            }
            reader.Close();
            /*if (flag)
            {
                this.strToolsImg = string.Concat(new object[] { "<a href='SArrange.aspx?ClubID=", this.intClub3ID, "&UserID=", this.intUserIDA, "&Type=5' target=Center><img src='", SessionItem.GetImageURL(), "MinMatchLev.gif' width='12' height='12' border='0'  alt='查看战术等级'><a>" });
            }
            else
            {
                this.strToolsImg = string.Concat(new object[] { "" });
            }*/
            this.strToolsImg = "";
            DataTable playerTableByClubID = BTPPlayer3Manager.GetPlayerTableByClubID(this.intClub3ID);
            if (playerTableByClubID == null)
            {
                this.strPlayerList = "<tr height='25' class='BarContent'><td colspan='4'>没有任何球员！</td></tr>";
            }
            else
            {
                foreach (DataRow row in playerTableByClubID.Rows)
                {
                    string str = row["Name"].ToString().Trim();
                    int intPosition = (byte) row["Pos"];
                    int num2 = (byte) row["Number"];
                    int intAbility = (int) row["Ability"];
                    string playerEngPosition = PlayerItem.GetPlayerEngPosition(intPosition);
                    int num4 = (byte) row["Height"];
                    int num5 = (byte) row["Weight"];
                    int num6 = (byte) row["Age"];
                    long num7 = (long) row["PlayerID"];
                    object strPlayerList = this.strPlayerList;
                    this.strPlayerList = string.Concat(new object[] { 
                        strPlayerList, "<tr  class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td width='33'><img width='16' height='19' src='", SessionItem.GetImageURL(), "Player/Number/", num2, ".gif'></td><td height='25' align='left' style='padding-left:4px'><div class=\"DIVPlayerName\"><a title='年龄：", num6, "<br>身高：", num4, " CM<br>体重：", num5, " KG' style='CURSOR: hand;color:#660066' href='ShowPlayer.aspx?Type=3&Kind=1&Check=0&PlayerID=", num7, "'>", str, "</a></div></td><td><a title='", 
                        PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td>", PlayerItem.GetAbilityColor(intAbility), "</td></tr>"
                     });
                }
            }
        }
    }
}


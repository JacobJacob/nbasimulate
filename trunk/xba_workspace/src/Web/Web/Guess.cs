namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Guess : Page
    {
        protected ImageButton btnOK;
        protected HtmlGenericControl divAddGuess;
        protected ImageButton Imagebutton1;
        private int intGuessID;
        private int intPage;
        private int intPrePage;
        private int intUserID;
        protected RadioButton rbCheckA;
        protected RadioButton rbCheckB;
        public StringBuilder sbGuess = new StringBuilder("");
        public string strEndTime = "";
        public string strGetScript = "";
        public string strGetViewPage = "";
        public string strMoneyA = "";
        public string strMoneyB = "";
        public string strMoneyType = "";
        public string strNameA = "";
        public string strNameB = "";
        private string strNickName = "";
        public string strPageIntro = "";
        private string strType = "";
        protected HtmlTable tblCalendar;
        protected HtmlTable tblGuess;
        protected HtmlTable tblMyGuess;
        protected TextBox tbMoney;

        private void AddGuess()
        {
            this.tbMoney.Attributes["onkeyup"] = " getMoney();";
            this.intGuessID = SessionItem.GetRequest("GuessID", 0);
            DataRow useGuessRowByGuessID = BTPGuessManager.GetUseGuessRowByGuessID(this.intGuessID);
            if (useGuessRowByGuessID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=811");
            }
            else
            {
                this.strEndTime = useGuessRowByGuessID["EndTime"].ToString();
                this.strNameA = useGuessRowByGuessID["NameA"].ToString();
                this.strNameB = useGuessRowByGuessID["NameB"].ToString();
                this.strMoneyA = useGuessRowByGuessID["NameAMoney"].ToString();
                this.strMoneyB = useGuessRowByGuessID["NameBMoney"].ToString();
                DateTime time = Convert.ToDateTime(useGuessRowByGuessID["EndTime"]);
                Convert.ToBoolean(useGuessRowByGuessID["MoneyType"]);
                int num = Convert.ToInt32(useGuessRowByGuessID["HasResult"]);
                string str = useGuessRowByGuessID["ResultText"].ToString().Trim();
                if ((num != 0) || (str.Trim().Length > 0))
                {
                    base.Response.Redirect("Report.aspx?Parameter=819");
                }
                else if (DateTime.Now > time)
                {
                    base.Response.Redirect("Report.aspx?Parameter=812");
                }
                else if (Convert.ToInt32(useGuessRowByGuessID["MoneyType"]) == 0)
                {
                    this.strMoneyType = "资金";
                }
                else
                {
                    this.strMoneyType = "游戏币";
                }
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            DataRow guessRowByGuessID = BTPGuessManager.GetGuessRowByGuessID(this.intGuessID);
            DateTime time = Convert.ToDateTime(guessRowByGuessID["EndTime"]);
            bool flag = Convert.ToBoolean(guessRowByGuessID["MoneyType"]);
            int num = Convert.ToInt32(guessRowByGuessID["HasResult"]);
            string str = guessRowByGuessID["ResultText"].ToString().Trim();
            if ((num != 0) || (str.Trim().Length > 0))
            {
                base.Response.Redirect("Report.aspx?Parameter=819");
            }
            else if (DateTime.Now > time)
            {
                base.Response.Redirect("Report.aspx?Parameter=812");
            }
            else
            {
                string str2;
                string str3 = this.tbMoney.Text.ToString();
                if (StringItem.IsNumber(str3))
                {
                    long num2 = Convert.ToInt64(str3);
                    if (!flag)
                    {
                        if ((num2 < 0x2710L) || (num2 > 0x989680L))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=817");
                            return;
                        }
                    }
                    else if ((num2 < 10L) || (num2 > 0x2710L))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=818");
                        return;
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=816");
                    return;
                }
                if (this.rbCheckA.Checked)
                {
                    str2 = "0";
                }
                else
                {
                    str2 = "1";
                }
                base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=ADDGUESSRECORD&GuessID=", this.intGuessID, "&Money=", str3, "&ResultType=", str2 }));
            }
        }

        private string GetScript(string strCurrentURL)
        {
            return ("<script language=\"javascript\">function JumpPage(){var strPage=document.all.Page.value;window.location=\"" + strCurrentURL + "Page=\"+strPage;}</script>");
        }

        private int GetTotal()
        {
            int useGuessCountByHasResult = 0;
            if (this.strType == "GUESS")
            {
                useGuessCountByHasResult = BTPGuessManager.GetUseGuessCountByHasResult(0);
            }
            if (this.strType == "MYGUESS")
            {
                useGuessCountByHasResult = BTPGuessManager.GetGuessRecordCountByUserID(this.intUserID);
            }
            return useGuessCountByHasResult;
        }

        private string GetViewPage(string strCurrentURL)
        {
            string[] strArray;
            object obj2;
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
                obj2 = str3;
                str3 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str3 = str3 + "</select>";
            obj2 = str + " " + str2 + " ";
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str3 });
        }

        private void Imagebutton1_Click(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("Guess.aspx?Type=GUESS");
        }

        private void InitializeComponent()
        {
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            this.Imagebutton1.Click += new ImageClickEventHandler(this.Imagebutton1_Click);
            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.GIF";
            this.Imagebutton1.ImageUrl = SessionItem.GetImageURL() + "button_13.GIF";
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
                this.strType = SessionItem.GetRequest("Type", 1);
                this.intPage = SessionItem.GetRequest("Page", 0);
                this.tblGuess.Visible = false;
                this.tblMyGuess.Visible = false;
                this.tblCalendar.Visible = false;
                this.divAddGuess.Visible = false;
                DataRow parameterRow = BTPParameterManager.GetParameterRow();
                int num = 0;
                if (parameterRow != null)
                {
                    num = (byte) parameterRow["CanBeginGuess"];
                }
                if (num == 1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=912b");
                }
                else
                {
                    switch (this.strType)
                    {
                        case "GUESS":
                            this.strPageIntro = "<ul><li class='qian1'>竞猜大厅</li><li class='qian2a'><a href='Guess.aspx?Type=MYGUESS&Page=1'>我的竞猜</a></li><li class='qian2a'><a href='Guess.aspx?Type=CALENDAR&Page=1'>赛程日历</a></li></ul>";
                            this.tblGuess.Visible = true;
                            this.SetGuess();
                            break;

                        case "MYGUESS":
                            this.strPageIntro = "<ul><li class='qian1a'><a href='Guess.aspx?Type=GUESS'>竞猜大厅</a></li><li class='qian2'>我的竞猜</li><li class='qian2a'><a href='Guess.aspx?Type=CALENDAR&Page=1'>赛程日历</a></li></ul>";
                            this.tblMyGuess.Visible = true;
                            this.SetMyGuess();
                            break;

                        case "ADDGUESS":
                            this.divAddGuess.Visible = true;
                            this.AddGuess();
                            break;

                        case "CALENDAR":
                            this.strPageIntro = "<ul><li class='qian1a'><a href='Guess.aspx?Type=GUESS'>竞猜大厅</a></li><li class='qian2a'><a href='Guess.aspx?Type=MYGUESS&Page=1'>我的竞猜</a></li><li class='qian2'>赛程日历</li></ul>";
                            this.tblCalendar.Visible = true;
                            break;

                        default:
                            this.strPageIntro = "<ul><li class='qian1'>竞猜大厅</li><li class='qian2a'><a href='Guess.aspx?Type=MYGUESS&Page=1'>我的竞猜</a></li><li class='qian2a'><a href='Guess.aspx?Type=CALENDAR&Page=1'>赛程日历</a></li></ul>";
                            this.tblGuess.Visible = true;
                            this.SetGuess();
                            break;
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetGuess()
        {
            this.intPrePage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 8;
            string str = "";
            string str2 = "";
            string str3 = "";
            DataTable table = BTPGuessManager.GetUseGuessTableByHasResult(0, 0, this.intPage, 0x2710);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str4;
                    string str5;
                    string str6 = row["Type"].ToString().Trim();
                    string str7 = row["NameA"].ToString().Trim();
                    string str8 = row["NameB"].ToString().Trim();
                    long num = (long) row["NameAMoney"];
                    long num2 = (long) row["NameBMoney"];
                    int num3 = Convert.ToInt32(row["MoneyType"]);
                    int num4 = (int) row["GuessID"];
                    bool flag = Convert.ToBoolean(row["Hot"]);
                    DateTime datIn = Convert.ToDateTime(row["EndTime"]);
                    if (flag)
                    {
                        str6 = "<font color=red>荐</font>" + str6;
                    }
                    if (num3 == 0)
                    {
                        str = "资";
                    }
                    else
                    {
                        str = "币";
                    }
                    str2 = "<a href='Guess.aspx?Type=ADDGUESS&GuessID=" + num4 + "'>竞猜</a>";
                    if (num3 == 0)
                    {
                        str4 = (num / 0x2710L) + "万";
                        str5 = (num2 / 0x2710L) + "万";
                    }
                    else
                    {
                        str4 = num.ToString();
                        str5 = num2.ToString();
                    }
                    if (num == num2)
                    {
                        str3 = str3 = "<font color=red size=1>1</font>";
                    }
                    else
                    {
                        double num5;
                        if (num > num2)
                        {
                            if (num2 != 0L)
                            {
                                num5 = ((double) num) / ((double) num2);
                                num5 = StringItem.getFloat(num5, 2);
                                str3 = "<font color=red size=1>" + num5 + "</font>";
                            }
                            else
                            {
                                str3 = "<font color=red>暂无</font>";
                            }
                            str5 = "<font color=3333ff>" + str5 + "</font>";
                        }
                        else
                        {
                            if (num != 0L)
                            {
                                num5 = ((double) num2) / ((double) num);
                                num5 = StringItem.getFloat(num5, 2);
                                str3 = "<font color=red size=1>" + num5 + "</font>";
                            }
                            else
                            {
                                str3 = "<font color=red>暂无</font>";
                            }
                            str4 = "<font color=#3333ff >" + str4 + "</font>";
                        }
                    }
                    if (DateTime.Now <= datIn)
                    {
                        this.sbGuess.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbGuess.Append("<td align=center>" + str6 + "</td><td width=143 align=center>" + str7 + "<br><font color='#ff6600'>[" + str4 + "]</font></td><td width=50 align=center>VS<br>" + str3 + "</td>");
                        this.sbGuess.Append("<td width=143 align=center>" + str8 + "<br><font color='#ff6600'>[" + str5 + "]</font></td><td align=center>" + str + "</td>");
                        this.sbGuess.Append("<td align=center>" + StringItem.FormatDate(datIn, "MM-dd<br>hh:mm") + "</td><td align=center>" + str2 + "</td></tr>");
                        this.sbGuess.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                    }
                }
            }
            else
            {
                this.sbGuess.Append("<tr><td colspan=7 align=center>无内容</td></tr>");
            }
        }

        private void SetMyGuess()
        {
            this.intPrePage = SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 8;
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            DataTable table = BTPGuessManager.GetGuessRecordTableByUserID(this.intUserID, 0, this.intPage, this.intPrePage);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str6;
                    int intGuessID = (int) row["GuessID"];
                    long num2 = (long) row["MyMoney"];
                    bool flag = (bool) row["ResultType"];
                    DataRow guessRowByGuessID = BTPGuessManager.GetGuessRowByGuessID(intGuessID);
                    string str7 = guessRowByGuessID["Type"].ToString().Trim();
                    string str8 = guessRowByGuessID["NameA"].ToString().Trim();
                    string str9 = guessRowByGuessID["NameB"].ToString().Trim();
                    long num3 = (long) guessRowByGuessID["NameAMoney"];
                    long num4 = (long) guessRowByGuessID["NameBMoney"];
                    int num5 = Convert.ToInt32(guessRowByGuessID["MoneyType"]);
                    int num6 = Convert.ToInt32(guessRowByGuessID["HasResult"]);
                    DateTime datIn = Convert.ToDateTime(guessRowByGuessID["EndTime"]);
                    str2 = guessRowByGuessID["ResultText"].ToString().Trim();
                    bool flag2 = Convert.ToBoolean(guessRowByGuessID["Hot"]);
                    if (num5 == 0)
                    {
                        str4 = (num3 / 0x2710L) + "万";
                        str5 = (num4 / 0x2710L) + "万";
                    }
                    else
                    {
                        str4 = num3.ToString();
                        str5 = num4.ToString();
                    }
                    if (num5 == 0)
                    {
                        str = "资";
                    }
                    else
                    {
                        str = "币";
                    }
                    if (flag2)
                    {
                        str7 = "<font color=red>荐</font>" + str7;
                    }
                    if ((num2 == 0L) && (num6 < 3))
                    {
                        if ((DateTime.Now > datIn) && (num6 > 0))
                        {
                            str3 = "<font color=#999999>结算中</font>";
                        }
                        else
                        {
                            str3 = "<font color=#999999>未平盘</font>";
                        }
                    }
                    else if (num2 > 0L)
                    {
                        str3 = "<font color=green>+" + num2 + "</font>";
                    }
                    else if (num2 < 0L)
                    {
                        str3 = "<font color=red>" + num2 + "</font>";
                    }
                    else if ((num2 == 0L) && (num6 == 3))
                    {
                        str3 = "<font color=green>+" + 0 + "</font>";
                    }
                    if (!flag)
                    {
                        str8 = "<font color=green>" + str8 + "</font>";
                    }
                    else
                    {
                        str9 = "<font color=green>" + str9 + "</font>";
                    }
                    if (num3 == num4)
                    {
                        str6 = str6 = "<font color=red size=1>1</font>";
                    }
                    else
                    {
                        double num7;
                        if (num3 > num4)
                        {
                            if (num4 != 0L)
                            {
                                num7 = ((double) num3) / ((double) num4);
                                num7 = StringItem.getFloat(num7, 2);
                                str6 = "<font color=red size=1>" + num7 + "</font>";
                            }
                            else
                            {
                                str6 = "<font color=red>暂无</font>";
                            }
                            str5 = "<font color=3333ff>" + str5 + "</font>";
                        }
                        else
                        {
                            if (num3 != 0L)
                            {
                                num7 = ((double) num4) / ((double) num3);
                                num7 = StringItem.getFloat(num7, 2);
                                str6 = "<font color=red size=1>" + num7 + "</font>";
                            }
                            else
                            {
                                str6 = "<font color=red>暂无</font>";
                            }
                            str4 = "<font color=#3333ff >" + str4 + "</font>";
                        }
                    }
                    this.sbGuess.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbGuess.Append("<td align=center>" + str7 + "</td><td width=103 align=center>" + str8 + "<br><font color='#ff6600'>[" + str4 + "]</font></td><td width=50 align=center>VS<br>" + str6 + "</td>");
                    this.sbGuess.Append("<td width=103 align=center>" + str9 + "<br><font color='#ff6600'>[" + str5 + "]</font></td><td align=center width=80 >" + str3 + "</td><td align=center>" + str + "</td>");
                    this.sbGuess.Append("<td align=center>" + StringItem.FormatDate(datIn, "MM-dd<br>hh:mm") + "</td><td align=center>" + str2 + "</td></tr>");
                    this.sbGuess.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='8'></td></tr>");
                }
            }
            else
            {
                this.sbGuess.Append("<tr><td colspan=8 align=center>无内容</td></tr>");
            }
            this.strGetViewPage = this.GetViewPage("Guess.aspx?Type=MYGUESS&");
            this.strGetScript = this.GetScript("Guess.aspx?Type=MYGUESS&");
        }
    }
}


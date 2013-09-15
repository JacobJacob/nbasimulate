namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
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
            this.intGuessID = (int) SessionItem.GetRequest("GuessID", 0);
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
                string str3;
                string str2 = this.tbMoney.Text.ToString();
                if (StringItem.IsNumber(str2))
                {
                    long num2 = Convert.ToInt64(str2);
                    if (flag)
                    {
                        if ((num2 < 10L) || (num2 > 0x2710L))
                        {
                            base.Response.Redirect("Report.aspx?Parameter=818");
                            return;
                        }
                    }
                    else if ((num2 < 0x2710L) || (num2 > 0x989680L))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=817");
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
                    str3 = "0";
                }
                else
                {
                    str3 = "1";
                }
                base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=ADDGUESSRECORD&GuessID=", this.intGuessID, "&Money=", str2, "&ResultType=", str3 }));
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
            return string.Concat(new object[] { obj2, "总数:", total, "  跳转 ", str4 });
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
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.intPage = (int) SessionItem.GetRequest("Page", 0);
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
            this.intPrePage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 8;
            string str4 = "";
            string str5 = "";
            string str8 = "";
            DataTable reader = BTPGuessManager.GetUseGuessTableByHasResult(0, 0, this.intPage, 0x2710);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str6;
                    string str7;
                    string str = row["Type"].ToString().Trim();
                    string str2 = row["NameA"].ToString().Trim();
                    string str3 = row["NameB"].ToString().Trim();
                    long num = (long) row["NameAMoney"];
                    long num2 = (long) row["NameBMoney"];
                    int num4 = Convert.ToInt32(row["MoneyType"]);
                    int num5 = (int) row["GuessID"];
                    bool flag = Convert.ToBoolean(row["Hot"]);
                    DateTime datIn = Convert.ToDateTime(row["EndTime"]);
                    if (flag)
                    {
                        str = "<font color=red>荐</font>" + str;
                    }
                    if (num4 == 0)
                    {
                        str4 = "资";
                    }
                    else
                    {
                        str4 = "币";
                    }
                    str5 = "<a href='Guess.aspx?Type=ADDGUESS&GuessID=" + num5 + "'>竞猜</a>";
                    if (num4 == 0)
                    {
                        str6 = (num / 0x2710L) + "万";
                        str7 = (num2 / 0x2710L) + "万";
                    }
                    else
                    {
                        str6 = num.ToString();
                        str7 = num2.ToString();
                    }
                    if (num == num2)
                    {
                        str8 = str8 = "<font color=red size=1>1</font>";
                    }
                    else
                    {
                        double num3;
                        if (num > num2)
                        {
                            if (num2 != 0L)
                            {
                                num3 = ((double) num) / ((double) num2);
                                num3 = StringItem.getFloat(num3, 2);
                                str8 = "<font color=red size=1>" + num3 + "</font>";
                            }
                            else
                            {
                                str8 = "<font color=red>暂无</font>";
                            }
                            str7 = "<font color=3333ff>" + str7 + "</font>";
                        }
                        else
                        {
                            if (num != 0L)
                            {
                                num3 = ((double) num2) / ((double) num);
                                num3 = StringItem.getFloat(num3, 2);
                                str8 = "<font color=red size=1>" + num3 + "</font>";
                            }
                            else
                            {
                                str8 = "<font color=red>暂无</font>";
                            }
                            str6 = "<font color=#3333ff >" + str6 + "</font>";
                        }
                    }
                    if (DateTime.Now <= datIn)
                    {
                        this.sbGuess.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                        this.sbGuess.Append("<td align=center>" + str + "</td><td width=143 align=center>" + str2 + "<br><font color='#ff6600'>[" + str6 + "]</font></td><td width=50 align=center>VS<br>" + str8 + "</td>");
                        this.sbGuess.Append("<td width=143 align=center>" + str3 + "<br><font color='#ff6600'>[" + str7 + "]</font></td><td align=center>" + str4 + "</td>");
                        //this.sbGuess.Append("<td width=143 align=center>" + str3 + "<br><font color='#ff6600'>[" + str7 + "]</font></td>");
                        this.sbGuess.Append("<td align=center>" + StringItem.FormatDate(datIn, "MM-dd<br>hh:mm") + "</td><td align=center>" + str5 + "</td></tr>");
                        this.sbGuess.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='7'></td></tr>");
                    }
                }
               
                //reader.Close();
            }
            else
            {
                this.sbGuess.Append("<tr><td colspan=7 align=center>无内容</td></tr>");
            }
        }

        private void SetMyGuess()
        {
            this.intPrePage = (int) SessionItem.GetRequest("Page", 0);
            if (this.intPage < 1)
            {
                this.intPage = 1;
            }
            this.intPrePage = 8;
            string str5 = "";
            string str6 = "";
            string str7 = "";
            string str8 = "";
            string str9 = "";
            DataTable reader = BTPGuessManager.GetGuessRecordTableByUserID(this.intUserID, 0, this.intPage, this.intPrePage);
            if (reader != null)
            {
                foreach (DataRow row in reader.Rows)
                {
                    string str;
                    int intGuessID = (int) row["GuessID"];
                    long num = (long) row["MyMoney"];
                    bool flag = (bool) row["ResultType"];
                    DataRow guessRowByGuessID = BTPGuessManager.GetGuessRowByGuessID(intGuessID);
                    string str2 = guessRowByGuessID["Type"].ToString().Trim();
                    string str3 = guessRowByGuessID["NameA"].ToString().Trim();
                    string str4 = guessRowByGuessID["NameB"].ToString().Trim();
                    long num2 = (long) guessRowByGuessID["NameAMoney"];
                    long num3 = (long) guessRowByGuessID["NameBMoney"];
                    int num5 = Convert.ToInt32(guessRowByGuessID["MoneyType"]);
                    int num7 = Convert.ToInt32(guessRowByGuessID["HasResult"]);
                    DateTime datIn = Convert.ToDateTime(guessRowByGuessID["EndTime"]);
                    str6 = guessRowByGuessID["ResultText"].ToString().Trim();
                    bool flag2 = Convert.ToBoolean(guessRowByGuessID["Hot"]);
                    if (num5 == 0)
                    {
                        str8 = (num2 / 0x2710L) + "万";
                        str9 = (num3 / 0x2710L) + "万";
                    }
                    else
                    {
                        str8 = num2.ToString();
                        str9 = num3.ToString();
                    }
                    if (num5 == 0)
                    {
                        str5 = "资";
                    }
                    else
                    {
                        str5 = "币";
                    }
                    if (flag2)
                    {
                        str2 = "<font color=red>荐</font>" + str2;
                    }
                    if ((num == 0L) && (num7 < 3))
                    {
                        if ((DateTime.Now > datIn) && (num7 > 0))
                        {
                            str7 = "<font color=#999999>结算中</font>";
                        }
                        else
                        {
                            str7 = "<font color=#999999>未平盘</font>";
                        }
                    }
                    else if (num > 0L)
                    {
                        str7 = "<font color=green>+" + num + "</font>";
                    }
                    else if (num < 0L)
                    {
                        str7 = "<font color=red>" + num + "</font>";
                    }
                    else if ((num == 0L) && (num7 == 3))
                    {
                        str7 = "<font color=green>+" + 0 + "</font>";
                    }
                    if (!flag)
                    {
                        str3 = "<font color=green>" + str3 + "</font>";
                    }
                    else
                    {
                        str4 = "<font color=green>" + str4 + "</font>";
                    }
                    if (num2 == num3)
                    {
                        str = str = "<font color=red size=1>1</font>";
                    }
                    else
                    {
                        double num4;
                        if (num2 > num3)
                        {
                            if (num3 != 0L)
                            {
                                num4 = ((double) num2) / ((double) num3);
                                num4 = StringItem.getFloat(num4, 2);
                                str = "<font color=red size=1>" + num4 + "</font>";
                            }
                            else
                            {
                                str = "<font color=red>暂无</font>";
                            }
                            str9 = "<font color=3333ff>" + str9 + "</font>";
                        }
                        else
                        {
                            if (num2 != 0L)
                            {
                                num4 = ((double) num3) / ((double) num2);
                                num4 = StringItem.getFloat(num4, 2);
                                str = "<font color=red size=1>" + num4 + "</font>";
                            }
                            else
                            {
                                str = "<font color=red>暂无</font>";
                            }
                            str8 = "<font color=#3333ff >" + str8 + "</font>";
                        }
                    }
                    this.sbGuess.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
                    this.sbGuess.Append("<td align=center>" + str2 + "</td><td width=103 align=center>" + str3 + "<br><font color='#ff6600'>[" + str8 + "]</font></td><td width=50 align=center>VS<br>" + str + "</td>");
                    this.sbGuess.Append("<td width=103 align=center>" + str4 + "<br><font color='#ff6600'>[" + str9 + "]</font></td><td align=center width=80 >" + str7 + "</td><td align=center>" + str5 + "</td>");
                    //this.sbGuess.Append("<td width=103 align=center>" + str4 + "<br><font color='#ff6600'>[" + str9 + "]</font></td><td align=center width=80 >" + str7 + "</td>");
                    this.sbGuess.Append("<td align=center>" + StringItem.FormatDate(datIn, "MM-dd<br>hh:mm") + "</td><td align=center>" + str6 + "</td></tr>");
                    this.sbGuess.Append("<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='8'></td></tr>");
                }
                //reader.Close();
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


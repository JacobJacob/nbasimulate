namespace Web
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class DevisionView : Page
    {
        protected ImageButton btnSearchOK;
        private int intCategory;
        private int intClubID;
        private int intStatus;
        public int intTurn;
        private int intUserID;
        public string strDevCode;
        public string strDownList;
        public string strIntro;
        public string strList = "";
        public string strMainScript;
        private string strNickName;
        public string strPageIntro;
        private string strType;
        public string strUPList;
        public string strViewScript = "";
        protected HtmlTable tbFindNickName;
        protected TextBox tbFriendName;
        protected HtmlTable tblDevView;
        protected HtmlTable tblStat;
        protected HtmlTable tblView;

        private void btnSearchOK_Click(object sender, ImageClickEventArgs e)
        {
            string validWords = StringItem.GetValidWords(this.tbFriendName.Text);
            if (!StringItem.IsValidName(validWords, 4, 0x10))
            {
                base.Response.Redirect("Report.aspx?Parameter=538");
            }
            else
            {
                DataRow accountRowByNickName = BTPAccountManager.GetAccountRowByNickName(validWords);
                if (accountRowByNickName == null)
                {
                    base.Response.Redirect("Report.aspx?Parameter=539");
                }
                else
                {
                    this.intClubID = (int) accountRowByNickName["ClubID5"];
                    if (this.intUserID != ((int) accountRowByNickName["UserID"]))
                    {
                        this.intUserID = (int) accountRowByNickName["UserID"];
                    }
                    else
                    {
                        base.Response.Redirect("Report.aspx?Parameter=551");
                        return;
                    }
                    this.strDevCode = BTPDevManager.GetDevCodeByClubID(this.intClubID);
                    base.Response.Write(string.Concat(new object[] { "<script>window.location=\"DevisionView.aspx?Type=MATCHLOOK&UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Status=1&Page=1\";</script>" }));
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnSearchOK.Click += new ImageClickEventHandler(this.btnSearchOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private bool IsMatchH(int intClubHID)
        {
            return (intClubHID == this.intClubID);
        }

        private void MatchLook()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            object strUPList;
            int request = (int) SessionItem.GetRequest("UserID", 0);
            this.intClubID = BTPClubManager.GetClubIDByUserID(request);
            this.strViewScript = "<script language=\"javascript\" type=\"text/javascript\" src=\"http://js.users.51.la/902042.js\"></script> <noscript><a href=\"http://www.51.la/?902042\" target=\"_blank\"><img alt=\"&#x6211;&#x8981;&#x5566;&#x514D;&#x8D39;&#x7EDF;&#x8BA1;\" src=\"http://img.users.51.la/902042.asp\" style=\"border:none\" /></a></noscript>";
            DataTable uPTable = BTPDevMatchManager.GetUPTable(this.intClubID, this.strDevCode);
            if (uPTable == null)
            {
                this.strUPList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in uPTable.Rows)
                {
                    num = (int) row["Round"];
                    num2 = (int) row["DevMatchID"];
                    num3 = (int) row["ClubHID"];
                    num5 = (int) row["ClubAID"];
                    num4 = (int) row["ClubHScore"];
                    num6 = (int) row["ClubAScore"];
                    str2 = StringItem.MD5Encrypt(num2.ToString(), Global.strMD5Key);
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=MATCHLOOK&UserID=", request, "&DevCode=", this.strDevCode, "&Tag=", str2, "&A=", num3, "&B=", num5, "' ><img src='", SessionItem.GetImageURL(), "MinMatchLook.gif' border='0' width='12' height='12'></a>" });
                    }
                    else
                    {
                        str = "";
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, "Right", 9);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, "Right", 9);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        str5 = "--";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strUPList;
                    this.strUPList = string.Concat(new object[] { strUPList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "</td></tr>" });
                    this.strUPList = this.strUPList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
            }
            DataTable downTable = BTPDevMatchManager.GetDownTable(this.intClubID, this.strDevCode);
            if (downTable == null)
            {
                this.strDownList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in downTable.Rows)
                {
                    num = (int) row2["Round"];
                    num2 = (int) row2["DevMatchID"];
                    num3 = (int) row2["ClubHID"];
                    num5 = (int) row2["ClubAID"];
                    num4 = (int) row2["ClubHScore"];
                    num6 = (int) row2["ClubAScore"];
                    str2 = StringItem.MD5Encrypt(num2.ToString(), Global.strMD5Key);
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='SecretaryPage.aspx?Type=MATCHLOOK&UserID=", request, "&DevCode=", this.strDevCode, "&Tag=", str2, "&A=", num3, "&B=", num5, "' ><img src='", SessionItem.GetImageURL(), "MinMatchLook.gif' border='0' width='12' height='12'></a>" });
                    }
                    else
                    {
                        str = "";
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, "Right", 9);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, "Right", 9);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        str5 = "--";
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { strUPList, "<tr class='BarContent' bgColor='#FBE2D4' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "</td></tr>" });
                    this.strDownList = this.strDownList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.btnSearchOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.strMainScript = "";
                DataRow gameRow = BTPGameManager.GetGameRow();
                this.intTurn = (int) gameRow["Turn"];
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                SessionItem.CheckCanUseAfterUpdate(5);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strType = (string) SessionItem.GetRequest("Type", 1);
                this.strDevCode = (string) SessionItem.GetRequest("Devision", 1);
                this.intStatus = (int) SessionItem.GetRequest("Status", 0);
                this.tblView.Visible = false;
                this.tbFindNickName.Visible = false;
                this.tblDevView.Visible = false;
                this.tblStat.Visible = false;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetPageIntro();
        }

        private void SetPageIntro()
        {
            if (this.intCategory == 5)
            {
                switch (this.strType)
                {
                    case "VIEW":
                        /*this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1'>联赛赛程</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=MATCHLOOK\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Type=MATCHLOOK&Devision=", this.strDevCode, "'>比赛录像</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });*/
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1'>联赛赛程</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.tblDevView.Visible = true;
                        return;

                    case "STAT":
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2'>技术统计</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                            "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        this.SetStat();
                        this.tblStat.Visible = true;
                        return;

                    case "MATCHLOOK":
                        /*this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, 
                            "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, "'>联赛日志</a></li><li class='qian2'>比赛录像</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });*/
                        this.strPageIntro = string.Concat(new object[] { 
                            "<ul><li class='qian1a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=VIEW\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Devision=", this.strDevCode, "&Page=1'>联赛赛程</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, 
                            "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                         });
                        if (this.intUserID == ((int) SessionItem.GetRequest("UserID", 0)))
                        {
                            if (((int) SessionItem.GetRequest("Status", 0)) > 0)
                            {
                                base.Response.Redirect("Report.aspx?Parameter=551");
                                return;
                            }
                            this.tbFindNickName.Visible = true;
                            return;
                        }
                        this.MatchLook();
                        this.tbFindNickName.Visible = true;
                        this.tblView.Visible = true;
                        return;
                }
                /*this.strPageIntro = string.Concat(new object[] { 
                    "<ul><li class='qian1'>联赛赛程</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                    "'>联赛日志</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=MATCHLOOK\"' href='DevisionView.aspx?UserID=", this.intUserID, "&Type=MATCHLOOK&Devision=", this.strDevCode, "'>比赛录像</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                 });*/
                this.strPageIntro = string.Concat(new object[] { 
                    "<ul><li class='qian1'>联赛赛程</li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=RIVAL\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=RIVAL&Devision=", this.strDevCode, "&Page=1'>上轮/本轮</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=STAT\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Page=1'>技术统计</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=LIST\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1'>联赛排名</a></li><li class='qian2a'><a onclick='javascript:window.top.Main.Right.location=\"Intro/Devision.aspx?Type=PIC\"' href='Devision.aspx?UserID=", this.intUserID, "&Type=PIC&Page=1&Devision=", this.strDevCode, 
                    "'>联赛日志</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='", SessionItem.GetImageURL(), "MenuCard/Help.GIF' border='0' height='24' width='19'></a>"
                 });
                this.tblDevView.Visible = true;
            }
            else
            {
                base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=LIST&Devision=", this.strDevCode, "&Page=1" }));
            }
        }

        private void SetStat()
        {
            this.intStatus = (int) SessionItem.GetRequest("Status", 0);
            switch (this.intStatus)
            {
                case 1:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1" }));
                    return;

                case 2:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1" }));
                    return;

                case 3:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1" }));
                    return;

                case 4:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=4&Page=1" }));
                    return;

                case 5:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=5&Page=1" }));
                    return;

                case 6:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1" }));
                    return;

                case 7:
                {
                    this.strIntro = string.Concat(new object[] { 
                        "<a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1'>MVP</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=2&Page=1'>得分</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=3&Page=1'>篮板</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, 
                        "&Status=4&Page=1'>助攻</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=5&Page=1'>抢断</a> | <a href='Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=6&Page=1'>封盖</a> | <font color='red'>本队统计</font>"
                     });
                    this.strList = "<tr align='center' class='BarHead'><td height='25' width='100' align='left' style='padding-left:3px'>姓名</td><td width='56'>球衣</td><td width='40'>位置</td><td width='40'>综合</td><td width='60'>得分</td><td width='60'>篮板</td><td width='60'>助攻</td><td width='60'>抢断</td><td width='60'>封盖</td></tr>";
                    DataTable playerTableByID = BTPPlayer5Manager.GetPlayerTableByID(BTPClubManager.GetClubIDByUserID(this.intUserID));
                    if (playerTableByID != null)
                    {
                        foreach (DataRow row in playerTableByID.Rows)
                        {
                            float num10;
                            float num11;
                            float num12;
                            float num13;
                            float num14;
                            string strName = row["Name"].ToString().Trim();
                            int num9 = (byte) row["Number"];
                            byte num1 = (byte) row["Height"];
                            byte num18 = (byte) row["Weight"];
                            int intPosition = (byte) row["Pos"];
                            byte num19 = (byte) row["Age"];
                            int num20 = (int) row["TeamDay"];
                            int num21 = (int) row["ClubID"];
                            string playerEngPosition = PlayerItem.GetPlayerEngPosition(intPosition);
                            int intAbility = (int) row["Ability"];
                            float single1 = ((float) intAbility) / 10f;
                            long longPlayerID = (long) row["PlayerID"];
                            int num6 = (int) row["SeasonBlock"];
                            int num3 = (int) row["SeasonPlayed"];
                            int num2 = (int) row["SeasonScore"];
                            int num5 = (int) row["SeasonSteal"];
                            int num7 = (int) row["SeasonAssist"];
                            int num8 = (int) row["SeasonRebound"];
                            int intCategory = Convert.ToInt32(row["Category"]);
                            if (intCategory == 3)
                            {
                                intAbility = 999;
                            }
                            if (num3 != 0)
                            {
                                num10 = ((float) ((num2 * 100) / num3)) / 100f;
                                num11 = ((float) ((num5 * 100) / num3)) / 100f;
                                num12 = ((float) ((num6 * 100) / num3)) / 100f;
                                num13 = ((float) ((num7 * 100) / num3)) / 100f;
                                num14 = ((float) ((num8 * 100) / num3)) / 100f;
                            }
                            else
                            {
                                num10 = 0f;
                                num11 = 0f;
                                num12 = 0f;
                                num13 = 0f;
                                num14 = 0f;
                            }
                            object strList = this.strList;
                            this.strList = string.Concat(new object[] { 
                                strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' align='left' style='padding-left:3px'>", PlayerItem.GetPlayerNameInfo(longPlayerID, strName, 5, 1, 1), "</td><td><img width='16' height='19' src='Images/Player/Number/", num9, ".gif'></td><td><a alt='", PlayerItem.GetPlayerChsPosition(intPosition), "' style='CURSOR: hand'>", playerEngPosition, "</a></td><td>", PlayerItem.GetAbilityColor(intAbility), "</td><td>", num10, "</td><td>", num14, "</td><td>", 
                                num13, "</td><td>", num11, "</td><td>", num12, "</td></tr>"
                             });
                            this.strList = this.strList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='9'></td></tr>";
                        }
                        break;
                    }
                    this.strList = this.strList + "<tr class='BarContent'><td height='25' colspan='9'>暂时没有球员。</td></tr>";
                    return;
                }
                default:
                    base.Response.Redirect(string.Concat(new object[] { "Devision.aspx?UserID=", this.intUserID, "&Type=STAT&Devision=", this.strDevCode, "&Status=1&Page=1" }));
                    break;
            }
        }

        private void SetView()
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            string str3;
            string str4;
            string str5;
            object strUPList;
            string str = "";
            string str2 = "";
            DataTable uPTable = BTPDevMatchManager.GetUPTable(this.intClubID, this.strDevCode);
            if (uPTable == null)
            {
                this.strUPList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row in uPTable.Rows)
                {
                    num = (int) row["Round"];
                    num2 = (int) row["DevMatchID"];
                    num3 = (int) row["ClubHID"];
                    num5 = (int) row["ClubAID"];
                    num4 = (int) row["ClubHScore"];
                    num6 = (int) row["ClubAScore"];
                    num = Convert.ToInt32(row["Round"]);
                    bool flag = (bool) row["UseStaffH"];
                    bool flag2 = (bool) row["UseStaffA"];
                    byte num7 = (byte) row["MangerSayH"];
                    byte num8 = (byte) row["MangerSayA"];
                    bool flag1 = (bool) row["AddArrangeLvlH"];
                    bool flag5 = (bool) row["AddArrangeLvlA"];
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else if (num < this.intTurn)
                    {
                        str = "";
                        str2 = "";
                    }
                    else if (this.IsMatchH(num3))
                    {
                        if (num7 > 0)
                        {
                            //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";'><img alt='球队状态较平时提升", num7, "%' src='", SessionItem.GetImageURL(), "louder", num7, ".gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";' ><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'></a>" });
                        }
                    }
                    else if (num8 > 0)
                    {
                        //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";' ><img alt='球队状态较平时提升", num8, "%' src='", SessionItem.GetImageURL(), "louder", num8, ".gif' border='0' width='14' height='16'></a>" });
                    }
                    else
                    {
                        //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";'><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'></a>" });
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, 9, num2);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, 9, num2);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        if (num < this.intTurn)
                        {
                            str5 = "--";
                        }
                        else if (this.IsMatchH(num3))
                        {
                            if (flag)
                            {
                                str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                            }
                            else
                            {
                                str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";' ><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                            }
                        }
                        else if (flag2)
                        {
                            str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                        }
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strUPList;
                    this.strUPList = string.Concat(new object[] { strUPList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "&nbsp;", str2, "</td></tr>" });
                    this.strUPList = this.strUPList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
            }
            DataTable downTable = BTPDevMatchManager.GetDownTable(this.intClubID, this.strDevCode);
            if (downTable == null)
            {
                this.strDownList = "<tr class='BarContent'><td height='25'>暂时没有比赛</td></tr>";
            }
            else
            {
                foreach (DataRow row2 in downTable.Rows)
                {
                    num = (int) row2["Round"];
                    num2 = (int) row2["DevMatchID"];
                    num3 = (int) row2["ClubHID"];
                    num5 = (int) row2["ClubAID"];
                    num4 = (int) row2["ClubHScore"];
                    num6 = (int) row2["ClubAScore"];
                    num = Convert.ToInt32(row2["Round"]);
                    bool flag3 = (bool) row2["UseStaffH"];
                    bool flag4 = (bool) row2["UseStaffA"];
                    byte num9 = (byte) row2["MangerSayH"];
                    byte num10 = (byte) row2["MangerSayA"];
                    bool flag6 = (bool) row2["AddArrangeLvlH"];
                    bool flag7 = (bool) row2["AddArrangeLvlA"];
                    if (((num3 != 0) && (num5 != 0)) && ((num4 != 0) || (num6 != 0)))
                    {
                        str = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VRep.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' target='_blank'><img alt='战报' src='", SessionItem.GetImageURL(), "RepLogo.gif' border='0' width='13' height='13'></a>" });
                        str2 = string.Concat(new object[] { "<a href='", Config.GetDomain(), "VStas.aspx?Type=2&Tag=", num2, "&A=", num3, "&B=", num5, "' target='_blank'><img alt='统计' src='", SessionItem.GetImageURL(), "StasLogo.gif' border='0' width='13' height='13'></a>" });
                    }
                    else if (num < this.intTurn)
                    {
                        str = "";
                        str2 = "";
                    }
                    else if (this.IsMatchH(num3))
                    {
                        if (num9 > 0)
                        {
                            //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";'><img alt='球队状态较平时提升", num9, "%' src='", SessionItem.GetImageURL(), "louder", num9, ".gif' border='0' width='14' height='16'></a>" });
                            str2 = "";
                        }
                        else
                        {
                            //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";'><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'></a>" });
                            str2 = "";
                        }
                    }
                    else if (num10 > 0)
                    {
                        //str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";' ><img alt='球队状态较平时提升", num10, "%' src='", SessionItem.GetImageURL(), "louder", num10, ".gif' border='0' width='14' height='16'></a>" });
                        str2 = "";
                    }
                    else
                    {
                       // str2 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Pos=1&Type=MANGERSAY&DevMatchID=", num2, "\";' ><img alt='给球员发奖金' src='", SessionItem.GetImageURL(), "louder.gif' border='0' width='14' height='16'></a>" });
                        str2 = "";
                    }
                    if (num3 != 0)
                    {
                        str3 = BTPClubManager.GetClubNameByClubID(num3, 5, 9, num2);
                    }
                    else
                    {
                        str3 = "轮空";
                    }
                    if (num5 != 0)
                    {
                        str4 = BTPClubManager.GetClubNameByClubID(num5, 5, 9, num2);
                    }
                    else
                    {
                        str4 = "轮空";
                    }
                    if ((num4 == 0) && (num6 == 0))
                    {
                        if (num < this.intTurn)
                        {
                            str5 = "--";
                        }
                        else if (this.IsMatchH(num3))
                        {
                            if (flag3)
                            {
                                str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";' ><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                            }
                            else
                            {
                                str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";'><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                            }
                        }
                        else if (flag4)
                        {
                            str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";'><img alt='已设置助理教练' src='", SessionItem.GetImageURL(), "coach2ok.gif' border='0' width='14' height='16'></a>" });
                        }
                        else
                        {
                            str5 = string.Concat(new object[] { "<a onclick='javascript:window.top.main.Center.location=\"SecretaryPage.aspx?Type=USESTAFF&DevMatchID=", num2, "\";' ><img alt='未设置助理教练' src='", SessionItem.GetImageURL(), "coach2.gif' border='0' width='14' height='16'></a>" });
                        }
                    }
                    else
                    {
                        str5 = string.Concat(new object[] { "<font color='40'>", num4, ":", num6, "</font>" });
                    }
                    strUPList = this.strDownList;
                    this.strDownList = string.Concat(new object[] { strUPList, "<tr class='BarContent' bgColor='#FBE2D4' onmouseover=\"this.style.backgroundColor='#fcf1eb'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='18'><font color='#7B1F76'>", num, "</font></td><td width='90' align='left'>", str3, "</td><td width='50' align='left'>", str5, "</td><td width='90' align='left'>", str4, "</td><td width='40'>", str, "&nbsp;", str2, "</td></tr>" });
                    this.strDownList = this.strDownList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='5'></td></tr>";
                }
            }
        }
    }
}


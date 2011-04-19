﻿namespace Web.MyControls
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using Web.DBData;
    using Web.Helper;

    public class Topic : UserControl
    {
        public int intPage;
        public int intPerPage;
        public int intTopicID;
        public int intTotalPage;
        public int intUserID;
        public StringBuilder sb = new StringBuilder();
        public StringBuilder sbLock = new StringBuilder();
        public StringBuilder sbTitle = new StringBuilder();
        public string strBoardID;
        public string strMaster;
        public HtmlTable tbReply;

        public SqlDataReader GetListTable()
        {
            int intCount = this.intPerPage * this.intPage;
            int total = this.GetTotal();
            return ROOTTopicManager.GetTopicReplyTableByID(this.strBoardID, this.intTopicID, this.intPage, this.intPerPage, intCount, total);
        }

        public int GetTotal()
        {
            return ROOTTopicManager.GetTopicReplyCountByID(this.strBoardID, this.intTopicID);
        }

        public string GetViewPage()
        {
            object[] objArray;
            StringBuilder builder = new StringBuilder();
            int total = this.GetTotal();
            this.intTotalPage = (total / this.intPerPage) + 1;
            if ((total % this.intPerPage) == 0)
            {
                this.intTotalPage--;
            }
            if (this.intTotalPage == 0)
            {
                this.intTotalPage = 1;
            }
            string str = "";
            string str2 = "";
            string str3 = "";
            if (this.intPage == 1)
            {
                str = "<font color='#A9A9A9'>首页</font>";
                str3 = "<font color='#A9A9A9'>上一页</font>";
            }
            else
            {
                objArray = new object[7];
                objArray[0] = "<a href='Topic.aspx?TopicID=";
                objArray[1] = this.intTopicID;
                objArray[2] = "&BoardID=";
                objArray[3] = this.strBoardID;
                objArray[4] = "&&Page=";
                int num3 = this.intPage - 1;
                objArray[5] = num3.ToString();
                objArray[6] = "'>上一页</a>";
                str3 = string.Concat(objArray);
                str = string.Concat(new object[] { "<a href='Topic.aspx?TopicID=", this.intTopicID, "&BoardID=", this.strBoardID, "&&Page=1'>首页</a>" });
            }
            string str4 = "";
            if (this.intPage == this.intTotalPage)
            {
                str4 = "<font color='#A9A9A9'>下一页</font>";
                str2 = "<font color='#A9A9A9'>尾页</font>";
            }
            else
            {
                objArray = new object[] { "<a href='Topic.aspx?TopicID=", this.intTopicID, "&BoardID=", this.strBoardID, "&&Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str4 = string.Concat(objArray);
                str2 = string.Concat(new object[] { "<a href='Topic.aspx?TopicID=", this.intTopicID, "&BoardID=", this.strBoardID, "&&Page=", this.intTotalPage, "'>尾页</a>" });
            }
            string str5 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= this.intTotalPage; i++)
            {
                str5 = str5 + "<option value=" + i;
                if (i == Convert.ToInt32(this.intPage))
                {
                    str5 = str5 + " selected";
                }
                object obj2 = str5;
                str5 = string.Concat(new object[] { obj2, ">第", i, "页</option>" });
            }
            str5 = str5 + "</select>";
            builder.Append("<font class='Forum001'>分页</font> ");
            builder.Append(str);
            builder.Append(" ");
            builder.Append(str3);
            builder.Append(" ");
            builder.Append(str4);
            builder.Append(" ");
            builder.Append(str2);
            builder.Append(" 页次：");
            builder.Append(this.intPage);
            builder.Append("/");
            builder.Append(this.intTotalPage);
            builder.Append("页 ");
            builder.Append(this.intPerPage);
            builder.Append("个记录/页 共");
            builder.Append(total);
            builder.Append("个记录 跳转");
            builder.Append(str5);
            return builder.ToString();
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetList();
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(this.sb.ToString());
        }

        private void SetList()
        {
            int num = RandomItem.rnd.Next(0, 10);
            SqlDataReader listTable = this.GetListTable();
            if (!listTable.HasRows)
            {
                this.sb.Append("<tr><td height='25' align='center' colspan='6'><font color='red'>暂时没有任何信息。</font></td></tr>");
                this.tbReply.Visible = false;
            }
            else
            {
                string str9 = "";
                this.sb.Append("<tr bgcolor='#fcc6a4'>");
                this.sb.Append("<td width='130' height='25' class='Forum001'>发表人</td>");
                for (int i = 0; listTable.Read(); i++)
                {
                    int num6 = (int) listTable["ReplyID"];
                    bool flag5 = (bool) listTable["IsVote"];
                    if (!flag5 || (num6 <= 0))
                    {
                        string str;
                        string str3;
                        string str5;
                        int num4;
                        string str6;
                        int num5;
                        int num7;
                        int num8;
                        string str10;
                        string str11;
                        string str12;
                        string str13;
                        int num9;
                        string str14;
                        if (i == 0)
                        {
                            string mainTitle;
                            if (num6 == 0)
                            {
                                mainTitle = listTable["Title"].ToString().Trim();
                                this.sb.Append("<td class='Forum001'>主题：" + mainTitle + "</td>");
                                this.sb.Append("</tr>");
                            }
                            else
                            {
                                mainTitle = ROOTTopicManager.GetMainTitle(this.intTopicID);
                                this.sb.Append("<td class='Forum001'>主题：" + mainTitle + "</td>");
                                this.sb.Append("</tr>");
                            }
                            this.sbTitle.Append(mainTitle);
                        }
                        this.sbLock.Append(listTable["OnLock"].ToString());
                        string str2 = listTable["Logo"].ToString().Trim();
                        DateTime datIn = (DateTime) listTable["CreateTime"];
                        int num2 = (int) listTable["TopicID"];
                        string strContent = listTable["Content"].ToString().Trim();
                        int intUserID = (int) listTable["UserID"];
                        if (intUserID == -1)
                        {
                            str5 = "XBA篮球经理";
                            num4 = 0;
                            str6 = SessionItem.GetImageURL() + "Images/Face/Del.png";
                            num5 = 0;
                            str = "XBA篮球经理";
                            num7 = 0;
                            num8 = 0;
                            str10 = "--";
                            str11 = "";
                            str12 = "--";
                            str13 = "--";
                            num9 = 0;
                            str14 = "";
                        }
                        else
                        {
                            DataRow userInfoByID = ROOTUserManager.GetUserInfoByID(intUserID);
                            if (userInfoByID != null)
                            {
                                str5 = userInfoByID["Say"].ToString().Trim();
                                num4 = (int) userInfoByID["TopicCount"];
                                str6 = DBLogin.URLString(0) + userInfoByID["DiskURL"].ToString().Trim() + "Face.png";
                                str = userInfoByID["NickName"].ToString().Trim();
                                int num1 = (int) userInfoByID["Reputation"];
                                DateTime time2 = (DateTime) userInfoByID["CreateTime"];
                                StringItem.FormatDate(time2, "yyyy-MM-dd");
                                num5 = Convert.ToInt16(userInfoByID["PayType"]);
                                num7 = (int) userInfoByID["Wealth"];
                                num8 = (int) userInfoByID["Levels"];
                                str10 = userInfoByID["DevCode"].ToString().Trim();
                                if (str10 == "")
                                {
                                    str10 = "无";
                                }
                                str11 = userInfoByID["Cups"].ToString().Trim();
                                str12 = userInfoByID["Province"].ToString().Trim();
                                str13 = userInfoByID["City"].ToString().Trim();
                                num9 = (byte) userInfoByID["Category"];
                                str14 = DBLogin.GameNameChinese(num9);
                            }
                            else
                            {
                                str5 = "";
                                num4 = 0;
                                str6 = SessionItem.GetImageURL() + "Images/Face/Del.png";
                                num5 = 0;
                                str = "<font color='#666666'>用户已注销</font>";
                                num7 = 0;
                                num8 = 0;
                                str10 = "--";
                                str11 = "";
                                str12 = "--";
                                str13 = "--";
                                num9 = 0;
                                str14 = "--";
                            }
                        }
                        bool flag = (bool) listTable["Elite"];
                        bool flag2 = (bool) listTable["OnTop"];
                        bool flag3 = (bool) listTable["OnLock"];
                        bool flag4 = (bool) listTable["IsResolve"];
                        if (flag3)
                        {
                            this.tbReply.Visible = false;
                        }
                        string userLevel = UserData.GetUserLevel(num7);
                        if (num5 == 1)
                        {
                            userLevel = userLevel + "　<font color='red'>[会员]</font>";
                        }
                        if ((i % 2) == 1)
                        {
                            str3 = "#FCF1EB";
                        }
                        else
                        {
                            str3 = "#FFFFFF";
                        }
                        StringBuilder builder = new StringBuilder();
                        if (str11 != "")
                        {
                            Cuter cuter = new Cuter(str11);
                            string[] arrCuter = cuter.GetArrCuter();
                            int size = cuter.GetSize();
                            if (size > 0)
                            {
                                for (int j = 0; j < (size - 1); j++)
                                {
                                    builder.Append("<img src='");
                                    builder.Append(SessionItem.GetImageURL());
                                    builder.Append("Cup/SmallCup/");
                                    builder.Append(arrCuter[j]);
                                    builder.Append("' height='16' width='16' border='0'> ");
                                }
                            }
                        }
                        if (BoardItem.IsBoardMaster(this.intUserID, this.strMaster))
                        {
                            object obj2;
                            str9 = string.Concat(new object[] { "　　　　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Delete.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=DELETE' onclick='return TopicChange(1);'>删除</a>" });
                            if ((this.intUserID == intUserID) && (intUserID != -1))
                            {
                                obj2 = str9;
                                str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Edit.gif' width='16' height='19' border=0 align='absmiddle'> <a href='EditTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Page=", this.intPage, "'>编辑</a>" });
                            }
                            if ((this.intUserID != intUserID) && (intUserID != -1))
                            {
                                obj2 = str9;
                                str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/AddWealth.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=ADDWEALTH'>加分</a>" });
                            }
                            if (num6 == 0)
                            {
                                if (flag)
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Elite.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=CANCELELITE' onclick='return TopicChange(2);'>取消精华</a>" });
                                }
                                else
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Elite.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=ELITE' onclick='return TopicChange(3);'>精华</a>" });
                                }
                                if (flag2)
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/OnTop.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=CANCELONTOP' onclick='return TopicChange(4);'>取消置顶</a>" });
                                }
                                else
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/OnTop.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=ONTOP' onclick='return TopicChange(5);'>置顶</a>" });
                                }
                                if (flag3)
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Lock.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=UNLOCK' onclick='return TopicChange(6);'>解锁</a>" });
                                }
                                else
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Lock.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=LOCK' onclick='return TopicChange(7);'>锁帖</a>" });
                                }
                                if (flag4)
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Resolve.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=UNRESOLVE' onclick='return TopicChange(8);'>未解决</a>" });
                                }
                                else
                                {
                                    obj2 = str9;
                                    str9 = string.Concat(new object[] { obj2, "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Resolve.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=RESOLVE' onclick='return TopicChange(9);'>已解决</a>" });
                                }
                            }
                        }
                        else if ((this.intUserID == intUserID) && (intUserID != -1))
                        {
                            str9 = string.Concat(new object[] { "　　　　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/Edit.gif' width='16' height='19' border=0 align='absmiddle'> <a href='EditTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "'>编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" });
                        }
                        else if ((this.intUserID != -1) && (intUserID != -1))
                        {
                            str9 = string.Concat(new object[] { "　　<img src='", SessionItem.GetImageURL(), "Forum/TopicLogo/AddWealth.gif' width='16' height='19' border=0 align='absmiddle'> <a href='OprateTopic.aspx?TopicID=", num2, "&BoardID=", this.strBoardID, "&Type=ADDWEALTH'>加分</a>" });
                        }
                        else
                        {
                            str9 = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                        StringBuilder builder2 = new StringBuilder();
                        if (intUserID != -1)
                        {
                            builder2.Append("<td height='200' valign='top' width='134'>");
                            builder2.Append("<table width='100%' cellspacing='0' cellpadding='0'>");
                            builder2.Append("<tr><td height='5' width='134'></td></tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumUName'>" + str + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLevel'>" + userLevel + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append(string.Concat(new object[] { "<td height='40' class='ForumLogin'><div style='filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src=", str6, "?RndID=", num, ");width:37px;height:40px'></div></td>" }));
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>财富：" + num7 + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>发帖：" + num4 + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>城市：" + str12 + " " + str13 + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>分区：" + str14 + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>街球：" + num8 + "级</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin'>联赛：" + str10 + "</td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td><hr size='1' width='95%' noshade color='#FCC6A4'></td>");
                            builder2.Append("</tr>");
                            builder2.Append("<tr>");
                            builder2.Append("<td height='20' class='ForumLogin' valign='top'>");
                            builder2.Append(builder.ToString());
                            builder2.Append("</td>");
                            builder2.Append("</tr>");
                            builder2.Append("</table>");
                            builder2.Append("</td>");
                        }
                        else
                        {
                            builder2.Append("<td height='200' valign='center' align='center'>");
                            builder2.Append("<img src='" + SessionItem.GetImageURL() + "Forum/Wealth.gif'>");
                            builder2.Append("</td>");
                        }
                        if (flag5 && (num6 == 0))
                        {
                            strContent = BoardItem.GetIsVoteContent(this.strBoardID, this.intTopicID, strContent);
                        }
                        this.sb.Append("<tr bgcolor='" + str3 + "'>");
                        this.sb.Append(builder2.ToString());
                        this.sb.Append("<td><table width='100%' cellspacing='0' cellpadding='0' height='100%' style='padding:4px;table-layout:fixed;word-break:break-all;'>");
                        this.sb.Append("<tr>");
                        this.sb.Append("<td height='*'><img src='" + SessionItem.GetImageURL() + "Forum/TopicLogo/");
                        this.sb.Append(str2 + "' width='12' height='12' border=0 align='absmiddle'> <font class='Forum008'>发表于</font> " + StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh:mm:ss</font>"));
                        this.sb.Append(str9);
                        this.sb.Append("<hr size='1' noshade color='#FCC6A4'></td>");
                        this.sb.Append("</tr>");
                        this.sb.Append("<tr>");
                        this.sb.Append("<td valign='Top' height='100%' style='color:#333333;'>" + strContent + "</td>");
                        this.sb.Append("</tr>");
                        this.sb.Append("<tr>");
                        this.sb.Append("<td height='*'><hr size='1' noshade color='#FCC6A4'>" + str5 + "</td>");
                        this.sb.Append("</tr>");
                        this.sb.Append("</table></td>");
                        this.sb.Append("</tr>");
                    }
                }
                listTable.Close();
                this.sb.Append("<tr><td height='30' colspan='6' align='right' style='padding-right:15px'>");
                this.sb.Append(this.GetViewPage());
                this.sb.Append("</td></tr>");
            }
        }
    }
}


namespace Web.MyControls
{
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class FrameBoard : UserControl
    {
        public int intPage;
        public int intPerPage;
        public int intTableWidth;
        private StringBuilder sb = new StringBuilder();
        public string strBoardID;

        public SqlDataReader GetListTable()
        {
            this.GetTotal();
            return ROOTBoardManager.GetBoardTableByBoardIDNew(this.strBoardID, this.intPage, this.intPerPage, 0);
        }

        public int GetTotal()
        {
            return ROOTBoardManager.GetBoardCountByBoardIDNew(this.strBoardID, 0);
        }

        public string GetViewPage()
        {
            string[] strArray;
            StringBuilder builder = new StringBuilder();
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
                strArray = new string[5];
                strArray[0] = "<a href='FrameBoard.aspx?BoardID=";
                strArray[1] = this.strBoardID;
                strArray[2] = "&&Page=";
                int num4 = this.intPage - 1;
                strArray[3] = num4.ToString();
                strArray[4] = "'>上一页</a>";
                str3 = string.Concat(strArray);
                str = "<a href='FrameBoard.aspx?BoardID=" + this.strBoardID + "&&Page=1'>首页</a>";
            }
            string str4 = "";
            if (this.intPage == num2)
            {
                str4 = "<font color='#A9A9A9'>下一页</font>";
                str2 = "<font color='#A9A9A9'>尾页</font>";
            }
            else
            {
                strArray = new string[] { "<a href='FrameBoard.aspx?BoardID=", this.strBoardID, "&&Page=", (this.intPage + 1).ToString(), "'>下一页</a>" };
                str4 = string.Concat(strArray);
                str2 = string.Concat(new object[] { "<a href='FrameBoard.aspx?BoardID=", this.strBoardID, "&&Page=", num2, "'>尾页</a>" });
            }
            string str5 = "<select name='Page' onChange=JumpPage()>";
            for (int i = 1; i <= num2; i++)
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
            builder.Append(" " + str3);
            builder.Append(" ");
            builder.Append(str4);
            builder.Append(" " + str2);
            builder.Append(" 页次：");
            builder.Append(this.intPage);
            builder.Append("/");
            builder.Append(num2);
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
            this.GetTotal();
            SqlDataReader reader = ROOTBoardManager.GetBoardTableByBoardIDNew(this.strBoardID, this.intPage, this.intPerPage, 0);
            this.sb.Append("<table width=\"" + this.intTableWidth + "\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#fcc6a4\" bgcolor=\"#fcf1eb\">");
            this.sb.Append("<tr bgcolor=\"#fcc6a4\"><td height=\"25\" width='30'></td><td width='100' class=\"Forum002\">回复/点击</td><td width='40%' class=\"Forum002\">主 题</td><td width='120' class=\"Forum002\">作者</td><td width='30%' class=\"Forum002\">最后更新</td></tr>");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string str;
                    string str2 = reader["Logo"].ToString().Trim();
                    int num = (int) reader["Hits"];
                    string strIn = reader["Title"].ToString().Trim();
                    string str4 = reader["NickName"].ToString().Trim();
                    int num2 = (int) reader["ReplyCount"];
                    DateTime datIn = (DateTime) reader["ReplyTime"];
                    string str5 = reader["ReplyUser"].ToString().Trim();
                    int num3 = (int) reader["TopicID"];
                    bool flag = (bool) reader["OnTop"];
                    bool flag2 = (bool) reader["Hot"];
                    bool flag3 = (bool) reader["Elite"];
                    bool flag4 = (bool) reader["OnLock"];
                    if (flag)
                    {
                        str = "OnTop.gif";
                    }
                    else if (flag3)
                    {
                        str = "Elite.gif";
                    }
                    else if (flag2)
                    {
                        str = "Hot.gif";
                    }
                    else if (flag4)
                    {
                        str = "OnLock.gif";
                    }
                    else
                    {
                        str = "Ordinarily.gif";
                    }
                    if (str5 != "")
                    {
                        str5 = "by " + str5;
                    }
                    this.sb.Append("<tr>");
                    this.sb.Append("<td height='25' align='center'><img src='");
                    this.sb.Append(SessionItem.GetImageURL());
                    this.sb.Append("Forum/");
                    this.sb.Append(str);
                    this.sb.Append("' width='15' height='19' border=0 align='absmiddle'></td>");
                    this.sb.Append("<td align='center'>");
                    this.sb.Append(num2);
                    this.sb.Append("/");
                    this.sb.Append(num);
                    this.sb.Append("</td>");
                    this.sb.Append("<td class='Forum003'><img src='");
                    this.sb.Append(SessionItem.GetImageURL());
                    this.sb.Append("Forum/TopicLogo/");
                    this.sb.Append(str2);
                    this.sb.Append("' width='12' height='12' border=0 align='absmiddle'> <a href='FrameTopic.aspx?TopicID=" + num3 + "&BoardID=");
                    this.sb.Append(this.strBoardID);
                    this.sb.Append("&Page=1'> ");
                    this.sb.Append(StringItem.GetShortString(strIn, 0x24, "..."));
                    this.sb.Append("</a></td>");
                    this.sb.Append("<td align='center'>");
                    this.sb.Append(StringItem.GetShortString(str4, 12, "."));
                    this.sb.Append("</td>");
                    this.sb.Append("<td class='Forum005'>");
                    this.sb.Append(StringItem.FormatDate(datIn, "MM-dd <font CLASS='ForumTime'>hh:mm:ss</font>"));
                    this.sb.Append(" ");
                    this.sb.Append(StringItem.GetShortString(str5, 12, "."));
                    this.sb.Append(" </td>");
                    this.sb.Append("</tr>");
                }
                reader.Close();
                this.sb.Append("<tr><td height='30' colspan='6' align='right' style='padding-right:15px'>" + this.GetViewPage() + "</td></tr>");
                this.sb.Append("</table>");
            }
            else
            {
                this.sb.Append("<tr><td height='25' align='center' colspan='5'><font color='red'>暂时没有任何信息。</font></td></tr>");
            }
        }
    }
}


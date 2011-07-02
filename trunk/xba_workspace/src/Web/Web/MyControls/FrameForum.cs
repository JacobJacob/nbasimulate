namespace Web.MyControls
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class FrameForum : UserControl
    {
        public int intTableWidth = 760;

        private string GetBlock(string strBoardID, int intC)
        {
            StringBuilder builder = new StringBuilder();
            string str = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            DataTable boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            int num3 = 80 / intC;
            builder.Append("<table width=\"" + this.intTableWidth + "\" border=\"1\" bgcolor=\"#fcf1eb\" bordercolor=\"#fcc6a4\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr bgcolor='#FCC6A4'>");
            builder.Append("<td height='25'></td>");
            builder.Append("<td class='Forum001'>" + str + "</td>");
            builder.Append("<td></td>");
            builder.Append("<td></td>");
            builder.Append("<td></td>");
            builder.Append("</tr>");
            builder.Append("<tr><td colspan='5'>");
            builder.Append("<table width='100%' cellspacing='0' cellpadding='0'>");
            int boardCountByTopID = ROOTBoardManager.GetBoardCountByTopID(strBoardID);
            int num5 = boardCountByTopID % intC;
            int num6 = boardCountByTopID / intC;
            if (num5 > 0)
            {
                num6++;
            }
            for (int i = 0; i < num6; i++)
            {
                builder.Append("<tr>");
                for (int j = 0; j < intC; j++)
                {
                    string str6;
                    int num7 = (i * intC) + j;
                    int num8 = i + j;
                    if ((num8 % 2) == 1)
                    {
                        str6 = "#FFFFFF";
                    }
                    else
                    {
                        str6 = "";
                    }
                    if ((num7 < boardCountByTopID) && boardByTopID != null)
                    {
                        foreach (DataRow row in boardByTopID.Rows)
                        {
                            string str2 = row["BoardID"].ToString().Trim();
                            string str3 = row["Name"].ToString().Trim();
                            int intCategory = (byte)row["Category"];
                            BoardItem.GetCategory(intCategory);
                            row["Intro"].ToString().Trim();
                            string strMaster = row["Master"].ToString().Trim();
                            int num2 = (int)row["TopicCount"];
                            row["NewTitle"].ToString().Trim();
                            row["NewLogo"].ToString().Trim();
                            int num1 = (int)row["NewTitleID"];
                            DateTime time1 = (DateTime)row["NewTime"];
                            row["NewAuthor"].ToString().Trim();
                            string str5 = row["Logo"].ToString().Trim();
                            builder.Append("<td bgcolor='" + str6 + "' class='Forum003' height='50' width='40' align='center'><img src='" + SessionItem.GetImageURL() + "Forum/BoardLogo/" + str5 + "'></td>");
                            builder.Append(string.Concat(new object[] { "<td bgcolor='", str6, "' width='", num3, "%' class='Forum003' height='50'><a href='FrameBoard.aspx?BoardID=", str2 }));
                            builder.Append("&Page=1'><strong>" + str3 + "</strong></a><br>");
                            builder.Append("<strong><font color='#333333'>主题数:</font></strong> <font color='#666666'>" + num2 + "</font><br>");
                            builder.Append("<strong><font color='#333333'>版主:</font></strong> <font class='ForumTime'>");
                            builder.Append(BoardItem.GetMasterNickName(strMaster) + "</font></td>");

                        }
                    }
                    else
                    {
                        builder.Append("<td bgcolor='" + str6 + "' class='Forum003' height='50'></td><td bgcolor='" + str6 + "' class='Forum003' height='50'></td>");
                    }
                }
                builder.Append("<tr>");
            }
            //boardByTopID.Close();
            builder.Append("</table></td></tr>");
            builder.Append("</table>");
            return builder.ToString();
        }

        private string GetList(string strBoardID)
        {
            StringBuilder builder = new StringBuilder();
            string str = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            DataTable boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            builder.Append("<table width=\"" + this.intTableWidth + "\" border=\"1\" bgcolor=\"#fcf1eb\" bordercolor=\"#fcc6a4\" cellspacing=\"0\" cellpadding=\"0\">");
            builder.Append("<tr bgcolor='#FCC6A4'>");
            builder.Append("<td Height='25' class='Forum001' colspan=\"2\">" + str + "</td>");
            builder.Append("<td width='80' class='Forum002'>主题数</td>");
            builder.Append("<td width='35%' class='Forum002'>最后发表</td>");
            builder.Append("<td class='Forum002'>版主</td>");
            builder.Append("</tr>");
            if (boardByTopID != null)
            {
                foreach (DataRow row in boardByTopID.Rows)
                {
                    string str2 = row["BoardID"].ToString().Trim();
                    string str3 = row["Name"].ToString().Trim();
                    int intCategory = (byte)row["Category"];
                    BoardItem.GetCategory(intCategory);
                    string str4 = row["Intro"].ToString().Trim();
                    string strMaster = row["Master"].ToString().Trim();
                    int num2 = (int)row["TopicCount"];
                    string strIn = row["NewTitle"].ToString().Trim();
                    string str7 = row["NewLogo"].ToString().Trim();
                    int num3 = (int)row["NewTitleID"];
                    DateTime datIn = (DateTime)row["NewTime"];
                    string str8 = row["NewAuthor"].ToString().Trim();
                    string str9 = row["Logo"].ToString().Trim();
                    builder.Append("<tr>");
                    builder.Append("<td height='55' align='center'width=\"40\"><img src='" + SessionItem.GetImageURL() + "Forum/BoardLogo/" + str9 + "'></td>");
                    builder.Append("<td class='Forum003' width='30%'><a href='FrameBoard.aspx?BoardID=" + str2 + "&Page=1'><strong>" + str3 + "</strong></a><br>");
                    builder.Append("<font color='#666666'>" + str4 + "</font></td>");
                    builder.Append("<td class='Forum004'>" + num2 + "</td>");
                    builder.Append("<td class='Forum005'><img align=absmiddle src='" + SessionItem.GetImageURL() + "Forum/TopicLogo/" + str7 + "' width='12' height='12'> ");
                    builder.Append(string.Concat(new object[] { "<a href='FrameTopic.aspx?BoardID=", str2, "&TopicID=", num3, "&Page=1'>", StringItem.GetShortString(strIn, 70, "..."), "</a><br>" }));
                    builder.Append(StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh:mm:ss</font>") + " [ " + str8 + " ]</td>");
                    builder.Append("<td class='Forum005'><font class='ForumTime'>" + BoardItem.GetMasterNickName(strMaster) + "</font></td>");
                    builder.Append("</tr>");
                }
            }
            //row.Close();
            builder.Append("</table><br>");
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
        }

        protected override void Render(HtmlTextWriter output)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.GetList("002"));
            builder.Append(this.GetList("001"));
            output.Write(builder.ToString());
        }
    }
}


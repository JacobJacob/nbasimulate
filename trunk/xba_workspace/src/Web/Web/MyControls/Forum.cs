namespace Web.MyControls
{
    using System;
    using System.Data.SqlClient;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Forum : UserControl
    {
        private string GetBlock(string strBoardID, int intC)
        {
            StringBuilder builder = new StringBuilder();
            string str = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            SqlDataReader boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            int num3 = 80 / intC;
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
                    if ((num7 < boardCountByTopID) && boardByTopID.Read())
                    {
                        string str2 = boardByTopID["BoardID"].ToString().Trim();
                        string str3 = boardByTopID["Name"].ToString().Trim();
                        int intCategory = (byte) boardByTopID["Category"];
                        BoardItem.GetCategory(intCategory);
                        boardByTopID["Intro"].ToString().Trim();
                        string strMaster = boardByTopID["Master"].ToString().Trim();
                        int num2 = (int) boardByTopID["TopicCount"];
                        boardByTopID["NewTitle"].ToString().Trim();
                        boardByTopID["NewLogo"].ToString().Trim();
                        int num1 = (int) boardByTopID["NewTitleID"];
                        DateTime time1 = (DateTime) boardByTopID["NewTime"];
                        boardByTopID["NewAuthor"].ToString().Trim();
                        string str5 = boardByTopID["Logo"].ToString().Trim();
                        builder.Append("<td bgcolor='" + str6 + "' class='Forum003' height='50' width='40' align='center'><img src='" + SessionItem.GetImageURL() + "Forum/BoardLogo/" + str5 + "'></td>");
                        builder.Append(string.Concat(new object[] { "<td bgcolor='", str6, "' width='", num3, "%' class='Forum003' height='50'><a href='Board.aspx?BoardID=", str2 }));
                        builder.Append("&Page=1'><strong>" + str3 + "</strong></a><br>");
                        builder.Append("<strong><font color='#333333'>主题数:</font></strong> <font color='#666666'>" + num2 + "</font><br>");
                        builder.Append("<strong><font color='#333333'>版主:</font></strong> <font class='ForumTime'>");
                        builder.Append(BoardItem.GetMasterNickName(strMaster) + "</font></td>");
                    }
                    else
                    {
                        builder.Append("<td bgcolor='" + str6 + "' class='Forum003' height='50'></td><td bgcolor='" + str6 + "' class='Forum003' height='50'></td>");
                    }
                }
                builder.Append("<tr>");
            }
            boardByTopID.Close();
            builder.Append("</table></td></tr>");
            return builder.ToString();
        }

        private string GetList(string strBoardID)
        {
            StringBuilder builder = new StringBuilder();
            string str = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            SqlDataReader boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            builder.Append("<tr bgcolor='#FCC6A4'>");
            builder.Append("<td height='25' width='40'></td>");
            builder.Append("<td width='45%' class='Forum001'>" + str + "</td>");
            builder.Append("<td width='80' class='Forum002'>主题数</td>");
            builder.Append("<td width='30%' class='Forum002'>最后发表</td>");
            builder.Append("<td class='Forum002'>版主</td>");
            builder.Append("</tr>");
            while (boardByTopID.Read())
            {
                string str2 = boardByTopID["BoardID"].ToString().Trim();
                string str3 = boardByTopID["Name"].ToString().Trim();
                int intCategory = (byte) boardByTopID["Category"];
                BoardItem.GetCategory(intCategory);
                string str4 = boardByTopID["Intro"].ToString().Trim();
                string strMaster = boardByTopID["Master"].ToString().Trim();
                int num2 = (int) boardByTopID["TopicCount"];
                string str6 = boardByTopID["NewTitle"].ToString().Trim();
                string str7 = boardByTopID["NewLogo"].ToString().Trim();
                int num3 = (int) boardByTopID["NewTitleID"];
                DateTime datIn = (DateTime) boardByTopID["NewTime"];
                string str8 = boardByTopID["NewAuthor"].ToString().Trim();
                string str9 = boardByTopID["Logo"].ToString().Trim();
                builder.Append("<tr>");
                builder.Append("<td height='55' align='center'><img src='" + SessionItem.GetImageURL() + "Forum/BoardLogo/" + str9 + "'></td>");
                builder.Append("<td class='Forum003'><a href='Board.aspx?BoardID=" + str2 + "&Page=1'><strong>" + str3 + "</strong></a><br>");
                builder.Append("<font color='#666666'>" + str4 + "</font></td>");
                builder.Append("<td class='Forum004'>" + num2 + "</td>");
                builder.Append("<td class='Forum005'><img align=absmiddle src='" + SessionItem.GetImageURL() + "Forum/TopicLogo/" + str7 + "' width='12' height='12'> ");
                builder.Append(string.Concat(new object[] { "<a href='Topic.aspx?BoardID=", str2, "&TopicID=", num3, "&Page=1'>", str6, "</a><br>" }));
                builder.Append(StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh:mm:ss</font>") + " [ " + str8 + " ]</td>");
                builder.Append("<td class='Forum005'><font class='ForumTime'>" + BoardItem.GetMasterNickName(strMaster) + "</font></td>");
                builder.Append("</tr>");
            }
            boardByTopID.Close();
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


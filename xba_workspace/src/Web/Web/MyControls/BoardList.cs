﻿namespace Web.MyControls
{
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class BoardList : UserControl
    {
        public string strBoardID;

        private string GetBlock(string strBoardID, int intC)
        {
            string str = "";
            string str2 = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            SqlDataReader boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            str = (str + "<tr bgcolor='#FCC6A4'><td height='25'></td><td class='Forum001'>" + str2 + "</td><td></td><td></td><td></td></tr>") + "<tr><td colspan='5'><table width='100%' cellspacing='0' cellpadding='0'>";
            int boardCountByTopID = ROOTBoardManager.GetBoardCountByTopID(strBoardID);
            int num4 = boardCountByTopID % intC;
            int num5 = boardCountByTopID / intC;
            if (num4 > 0)
            {
                num5++;
            }
            int num7 = 0;
            while (boardByTopID.Read())
            {
                str = str + "<tr>";
                for (int i = 0; i < intC; i++)
                {
                    string str7;
                    int num6 = (num7 * intC) + i;
                    if ((num6 % 2) == 1)
                    {
                        str7 = "#FFFFFF";
                    }
                    else
                    {
                        str7 = "";
                    }
                    if (num6 < boardCountByTopID)
                    {
                        string str3 = boardByTopID["BoardID"].ToString().Trim();
                        string str4 = boardByTopID["Name"].ToString().Trim();
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
                        string str6 = boardByTopID["Logo"].ToString().Trim();
                        object obj2 = str;
                        str = string.Concat(new object[] { 
                            obj2, "<td bgcolor='", str7, "' class='Forum003' height='50' width='30' align='center'><img src='", SessionItem.GetImageURL(), "Forum/BoardLogo/", str6, "' width='20' height='20'></td><td bgcolor='", str7, "' class='Forum003' height='50'><a href='Board.aspx?BoardID=", str3, "'><strong>", str4, "</strong></a><br><strong><font color='#333333'>主题数:</font></strong> <font color='#666666'>", num2, "</font><br><strong><font color='#333333'>版主:</font></strong> <font class='ForumTime'>", 
                            BoardItem.GetMasterNickName(strMaster), "</font></td>"
                         });
                    }
                    else
                    {
                        string str8 = str;
                        str = str8 + "<td bgcolor='" + str7 + "' class='Forum003' height='50'></td><td bgcolor='" + str7 + "' class='Forum003' height='50'></td>";
                    }
                }
                num7 += intC;
                str = str + "<tr>";
            }
            boardByTopID.Close();
            return (str + "</table></td></tr>");
        }

        private string GetList(string strBoardID)
        {
            string str = "";
            string str2 = ROOTBoardManager.GetBoardByBoardID(strBoardID)["Name"].ToString().Trim();
            SqlDataReader boardByTopID = ROOTBoardManager.GetBoardByTopID(strBoardID);
            str = str + "<tr bgcolor='#FCC6A4'><td height='25' width='40'></td><td width='45%' class='Forum001'>" + str2 + "</td><td width='80' class='Forum002'>主题数</td><td width='30%' class='Forum002'>最后发表</td><td class='Forum002'>版主</td></tr>";
            while (boardByTopID.Read())
            {
                string str3 = boardByTopID["BoardID"].ToString().Trim();
                string str4 = boardByTopID["Name"].ToString().Trim();
                int intCategory = (byte) boardByTopID["Category"];
                BoardItem.GetCategory(intCategory);
                string str5 = boardByTopID["Intro"].ToString().Trim();
                string strMaster = boardByTopID["Master"].ToString().Trim();
                int num2 = (int) boardByTopID["TopicCount"];
                string str7 = boardByTopID["NewTitle"].ToString().Trim();
                string str8 = boardByTopID["NewLogo"].ToString().Trim();
                int num3 = (int) boardByTopID["NewTitleID"];
                DateTime datIn = (DateTime) boardByTopID["NewTime"];
                string str9 = boardByTopID["NewAuthor"].ToString().Trim();
                string str10 = boardByTopID["Logo"].ToString().Trim();
                object obj2 = str;
                str = string.Concat(new object[] { 
                    obj2, "<tr><td height='55' align='center'><img src='", SessionItem.GetImageURL(), "Forum/BoardLogo/", str10, "'></td><td class='Forum003'><a href='Board.aspx?BoardID=", str3, "&Page=1'><strong>", str4, "</strong></a><br><font color='#666666'>", str5, "</font></td><td class='Forum004'>", num2, "</td><td class='Forum005'><img align=absmiddle src='", SessionItem.GetImageURL(), "Forum/TopicLogo/", 
                    str8, "' width='12' height='12'> <a href='Topic.aspx?BoardID=", str3, "&TopicID=", num3, "&Page=1'>", str7, "</a><br>", StringItem.FormatDate(datIn, "yyyy-MM-dd <font CLASS='ForumTime'>hh-mm-ss</font>"), " [ ", str9, " ]</td><td class='Forum005'><font class='ForumTime'>", BoardItem.GetMasterNickName(strMaster), "</font></td></tr>"
                 });
            }
            boardByTopID.Close();
            return str;
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
            output.Write(this.GetList(this.strBoardID));
        }
    }
}


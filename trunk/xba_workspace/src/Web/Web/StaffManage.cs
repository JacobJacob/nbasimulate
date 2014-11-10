namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class StaffManage : Page
    {
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        private int intPayType;
        private int intUserID;
        public StringBuilder sbList;
        public StringBuilder sbPageIntro;
        private string strType;

        private void InitializeComponent()
        {
            string str;
            if ((this.intCategory != 2) && (this.intCategory != 5))
            {
                str = "<li class='qian2a'><font color='#aaaaaa'>职业队</font></li></ul>";
            }
            else
            {
                str = "<li class='qian2a'><a href='StaffManage.aspx?Type=PRO'>职业队</a></li></ul>";
            }
            switch (this.strType)
            {
                case "STREET":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.StreetList();
                    break;

                case "PRO":
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1a'><a href='StaffManage.aspx?Type=STREET'>街球队</a></li>");
                    this.sbPageIntro.Append("<li class='qian2'>职业队</li></ul>");
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.ProList();
                    break;

                default:
                    this.sbPageIntro = new StringBuilder();
                    this.sbPageIntro.Append("<ul><li class='qian1'>街球队</li>");
                    this.sbPageIntro.Append(str);
                    this.sbPageIntro.Append("<a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>");
                    this.StreetList();
                    break;
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
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.intPayType = (int) onlineRowByUserID["PayType"];
                this.strType = SessionItem.GetRequest("Type", 1);
                if (((this.intCategory != 2) && (this.intCategory != 5)) && (this.strType != "STREET"))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        public void ProList()
        {
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            int num;
            object obj2;
            this.sbList = new StringBuilder();
            DataRow staffRow = BTPStaffManager.GetStaffRow(this.intClubID5, 1);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                num = 0;
                str7 = "解雇";
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>训练员：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' align='right' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
            staffRow = BTPStaffManager.GetStaffRow(this.intClubID5, 2);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                str7 = "解雇";
                num = 0;
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>营养师：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' align='right' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
            staffRow = BTPStaffManager.GetStaffRow(this.intClubID5, 3);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                str7 = "解雇";
                num = 0;
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>队　医：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
        }

        public void StreetList()
        {
            string str;
            string str2;
            string str3;
            string str4;
            string str5;
            string str6;
            string str7;
            int num;
            object obj2;
            this.sbList = new StringBuilder();
            DataRow staffRow = BTPStaffManager.GetStaffRow(this.intClubID3, 1);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                str7 = "解雇";
                num = 0;
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr><td colspan='3' height='10'></td></tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>训练员：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr><td height='10' colspan='3'></td></tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
            staffRow = BTPStaffManager.GetStaffRow(this.intClubID3, 2);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                num = 0;
                str7 = "解雇";
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr><td colspan='3' height='10'></td></tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>营养师：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' align='right' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
            staffRow = BTPStaffManager.GetStaffRow(this.intClubID3, 3);
            if (staffRow == null)
            {
                str = "暂无";
                str2 = "未知";
                str3 = "未知";
                str4 = "未知";
                str5 = "未知";
                str6 = "暂无";
                num = 0;
                str7 = "解雇";
            }
            else
            {
                str = staffRow["Name"].ToString().Trim();
                str2 = staffRow["Age"].ToString().Trim();
                str3 = staffRow["Levels"].ToString().Trim();
                str4 = staffRow["Salary"].ToString().Trim();
                str5 = staffRow["Contract"].ToString().Trim();
                str6 = staffRow["Face"].ToString().Trim();
                str6 = "<img src='" + SessionItem.GetImageURL() + "Staff/" + str6 + ".gif' width='37' height='40'>";
                num = (int) staffRow["StaffID"];
                str7 = "<a href='SecretaryPage.aspx?Type=FIRESTAFF&StaffID=" + num + "'>解雇</a>";
                if (this.intPayType == 1)
                {
                    obj2 = str7;
                    str7 = string.Concat(new object[] { obj2, " | <a href='SecretaryPage.aspx?Type=EXTENDSTAFF&StaffID=", num, "'>续约</a>" });
                }
                else
                {
                    str7 = str7 + " | <a title='会员功能' style='color:#666666'>续约</a>";
                }
            }
            this.sbList.Append("<tr onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\">");
            this.sbList.Append("<td><table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
            this.sbList.Append("<tr><td colspan='3' height='10'></td></tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td rowspan='3' width='100' align='center'>" + str6 + "<br><br>" + str7 + "</td>");
            this.sbList.Append("<td height='25' colspan='2'>队　医：<strong><font color='#660066'>" + str + "</font></strong></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25' width='125'>年龄：" + str2 + "</td>");
            this.sbList.Append("<td height='25'>等级：" + str3 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='25'>薪水：" + str4 + "</td>");
            this.sbList.Append("<td height='25'>合同：" + str5 + "</td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("<tr>");
            this.sbList.Append("<td height='10' align='right' colspan='3'></td>");
            this.sbList.Append("</tr>");
            this.sbList.Append("</table></td>");
            this.sbList.Append("</tr>");
        }
    }
}


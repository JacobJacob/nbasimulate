namespace Web
{
    using System;
    using System.Data;
    using System.Text;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class TopLeft : Page
    {
        private int intCategory;
        private int intPayType;
        private int intUnionID;
        private int intUserID;
        public StringBuilder sb;

        private void GetSTList()
        {
            string str = "";
            string str2 = "";
            this.sb = new StringBuilder();
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            if (accountRowByUserID == null)
            {
                base.Response.Redirect("Report.aspx?Parameter=1");
            }
            else
            {
                int num = (int) accountRowByUserID["UCount"];
                int num2 = (int) accountRowByUserID["UOldCount"];
                int num3 = (int) accountRowByUserID["CCount"];
                int num4 = (int) accountRowByUserID["COldCount"];
                if (num == 0)
                {
                    str = "----";
                }
                else
                {
                    string str3;
                    if (num > num2)
                    {
                        str3 = "<font color='red'>↓" + (num - num2) + "</font>";
                    }
                    else
                    {
                        str3 = "<font color='green'>↑" + (num2 - num) + "</font>";
                    }
                    str = string.Concat(new object[] { "第", num, "名[", str3, "]" });
                }
                if (((DateTime.Now >= DateTime.Today.AddHours((double) Global.intStartUpdate)) && (DateTime.Now <= DateTime.Today.AddHours(10.0))) || (num3 == 0))
                {
                    str2 = "----";
                }
                else if ((this.intCategory != 5) || (num == 0))
                {
                    str2 = "----";
                }
                else
                {
                    string str4;
                    if (num3 > num4)
                    {
                        str4 = "<font color='red'>↓" + (num3 - num4) + "</font>";
                    }
                    else
                    {
                        str4 = "<font color='green'>↑" + (num4 - num3) + "</font>";
                    }
                    str2 = string.Concat(new object[] { "第", num3, "名[", str4, "]" });
                }
                this.sb.Append("<table width='100%'  border='0' cellspacing='0' cellpadding='0'>");
                this.sb.Append("<tr width=\"200\">");
                this.sb.Append("<td rowspan='3' valign='middle' align='center' height='58'><img src='Images/Border/icon_07.GIF' width='27' height='27'></td>");
                this.sb.Append("<td height='30' class='TCClass2' valign='bottom'><table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><strong>街球榜</strong></td><td align='right' style='padding-right:4px'>" + str + "</td></tr></table></td>");
                this.sb.Append("</tr>");
                this.sb.Append("<tr>");
                this.sb.Append("<td height='1' background='Images/Border/line.gif'></td>");
                this.sb.Append("</tr>");
                this.sb.Append("<tr>");
                this.sb.Append("<td height='24' class='TCClass2'><table border='0' cellspacing='0' cellpadding='0' width='100%'><tr><td><strong>职业榜</strong></td><td align='right' style='padding-right:4px'>" + str2 + "</td></tr></table></td>");
                this.sb.Append("</tr>");
                this.sb.Append("<tr><td colspan='2' height='5'></td></tr>");
                this.sb.Append("</table>");
            }
        }

        private string GetVTList()
        {
            return "";
        }

        private void InitializeComponent()
        {
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
                this.intUnionID = (int) onlineRowByUserID["UnionID"];
                this.intPayType = (int) onlineRowByUserID["PayType"];
                if (((this.intCategory != 1) && (this.intCategory != 2)) && (this.intCategory != 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (((this.intCategory == 1) || (this.intCategory == 2)) || (this.intCategory == 5))
            {
                this.GetSTList();
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
            }
        }
    }
}


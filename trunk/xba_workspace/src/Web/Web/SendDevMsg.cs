namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class SendDevMsg : Page
    {
        protected ImageButton btnSend;
        private int intClubID;
        private int intUserID;
        private string strDevCode;
        public string strLeftList;
        public string strList;
        private string strNickName;
        protected TextBox tbContent;
        protected HtmlTable tblSendMsg;

        private void btnSend_Click(object sender, ImageClickEventArgs e)
        {
            string validWords = StringItem.GetValidWords(this.tbContent.Text);
            if (StringItem.IsValidName(validWords, 2, 200))
            {
                if ((StringItem.GetStrLength(validWords) > 200) || (StringItem.GetStrLength(validWords) < 2))
                {
                    this.strList = "<font color='#FF0000'>留言内容必须在2-200个字符之间！</font>";
                }
                else
                {
                    BTPDevMessageManager.AddDevMessage(this.strDevCode, this.intUserID, this.strNickName, validWords);
                    base.Response.Redirect("SendDevMsg.aspx?Devision=" + this.strDevCode + "&Page=1");
                }
            }
            else
            {
                this.strList = "<font color='#FF0000'>留言内容中含有非法字符！</font>";
            }
        }

        private void GetList()
        {
            BTPDevMessageManager.GetDevMessageCountByCode(this.strDevCode);
            DataTable topTableByDevCode = BTPDevMessageManager.GetTopTableByDevCode(this.strDevCode, 3);
            if (topTableByDevCode == null)
            {
                this.strLeftList = "<tr class='BarContent'><td height='25'>暂无联赛留言</td></tr>";
            }
            else
            {
                foreach (DataRow row in topTableByDevCode.Rows)
                {
                    string str = row["NickName"].ToString().Trim();
                    string str2 = row["Content"].ToString().Trim();
                    DateTime datIn = (DateTime) row["CreateTime"];
                    int intUserID = (int) row["UserID"];
                    int clubIDByUIDCategory = BTPClubManager.GetClubIDByUIDCategory(intUserID, 5);
                    object strLeftList = this.strLeftList;
                    this.strLeftList = string.Concat(new object[] { strLeftList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td height='25' Width='530' style='word-break:break-all' align='left' colspan='2' ><font color='#7B1F76'>[", StringItem.FormatDate(datIn, "hh:mm"), "]</font>　", str2, "　<a href='ShowClub.aspx?ClubID=", clubIDByUIDCategory, "&Type=5' target='Right'>", str, "</a></td></tr>" });
                    this.strLeftList = this.strLeftList + "<tr><td height='1' background='" + SessionItem.GetImageURL() + "RM/Border_07.gif' colspan='2'></td></tr>";
                }
                BTPDevManager.UpdateHasNewMsg(this.intClubID);
            }
            this.tblSendMsg.Visible = true;
            this.btnSend.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
            this.strList = "";
        }

        private void InitializeComponent()
        {
            this.btnSend.Click += new ImageClickEventHandler(this.btnSend_Click);
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
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                SessionItem.CheckCanUseAfterUpdate(5);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.strDevCode = SessionItem.GetRequest("Devision", 1);
                this.GetList();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


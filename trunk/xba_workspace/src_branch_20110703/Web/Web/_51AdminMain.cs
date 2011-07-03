namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class _51AdminMain : Page
    {
        protected Button btnADOK;
        protected Button btnAnOK;
        protected int intUserID;
        public string strEndAD;
        public string strErrMsg;
        public string strGameAD01;
        public string strHeadAD;
        public string strList;
        public string strType;
        protected TextBox tbAnnouce;
        protected TextBox tbEndAD;
        protected TextBox tbGameAD01;
        protected TextBox tbHeadAD;
        protected HtmlTable tblAD;
        protected HtmlTable tblAnnouce;

        private void btnADOK_Click(object sender, EventArgs e)
        {
            string str = this.tbHeadAD.Text.ToString().Trim();
            string str2 = this.tbEndAD.Text.ToString().Trim();
            string str3 = this.tbGameAD01.Text.ToString().Trim();
            string commandText = "UPDATE BTP_GameAD SET LoginAD01='" + str + "',LoginAD02='" + str2 + "',GameAD01='" + str3 + "' WHERE GameADID=1";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            this.strErrMsg = "广告修改成功。";
        }

        private void btnAnOK_Click(object sender, EventArgs e)
        {
            string str = this.tbAnnouce.Text.ToString().Trim();
            if (str != null)
            {
                string commandText = "INSERT INTO BTP_Announce (Title) VALUES ('" + str + "')";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            base.Response.Redirect("Report.aspx?Parameter=704");
        }

        private void GetAnnouce()
        {
            DataTable announceTable = BTPAnnounceManager.GetAnnounceTable();
            if (announceTable != null)
            {
                foreach (DataRow row in announceTable.Rows)
                {
                    string str = row["Title"].ToString().Trim();
                    int num = (int) row["AnnounceID"];
                    DateTime datIn = (DateTime) row["CreateTime"];
                    this.strList = this.strList + "<tr>";
                    this.strList = this.strList + "<td height='25' style='PADDING-LEFT:4px'>" + str + "</td>";
                    this.strList = this.strList + "<td style='PADDING-LEFT:4px'>" + StringItem.FormatDate(datIn, "yyMMdd hh:mm:ss") + "</td>";
                    object strList = this.strList;
                    this.strList = string.Concat(new object[] { strList, "<td align='center'><a href='Modify51Announce.aspx?ID=", num, "'>修改</a> | <a href='51AnnounceDel.aspx?ID=", num, "' onclick='return MessageDel(1);'>删除</a></td>" });
                    this.strList = this.strList + "</tr>";
                }
            }
        }

        private void GetGameAD()
        {
            DataRow gameADRow = BTPGameADManager.GetGameADRow();
            if (gameADRow != null)
            {
                this.strHeadAD = gameADRow["LoginAD01"].ToString().Trim();
                this.strEndAD = gameADRow["LoginAD02"].ToString().Trim();
                this.strGameAD01 = gameADRow["GameAD01"].ToString().Trim();
                if (!base.IsPostBack)
                {
                    this.tbHeadAD.Text = this.strHeadAD;
                    this.tbEndAD.Text = this.strEndAD;
                    this.tbGameAD01.Text = this.strGameAD01;
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnADOK.Click += new EventHandler(this.btnADOK_Click);
            this.btnAnOK.Click += new EventHandler(this.btnAnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.Check51Login();
            if (this.intUserID <= 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=701");
            }
            else
            {
                this.tblAD.Visible = false;
                this.tblAnnouce.Visible = false;
                this.strType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                switch (this.strType)
                {
                    case "AD":
                        this.tblAD.Visible = true;
                        this.tblAnnouce.Visible = false;
                        this.GetGameAD();
                        return;

                    case "ANNOUNCE":
                        this.tblAD.Visible = false;
                        this.tblAnnouce.Visible = true;
                        this.GetAnnouce();
                        return;
                }
                this.tblAD.Visible = true;
                this.tblAnnouce.Visible = false;
                this.GetGameAD();
            }
        }
    }
}


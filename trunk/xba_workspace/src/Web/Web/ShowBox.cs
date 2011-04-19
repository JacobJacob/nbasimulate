namespace Web
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class ShowBox : Page
    {
        protected ImageButton btnNext;
        protected ImageButton btnOK;
        protected HtmlGenericControl div0;
        protected HtmlGenericControl div1;
        protected HtmlGenericControl div2;
        protected HtmlGenericControl div3;
        protected HtmlGenericControl div4;
        protected HtmlGenericControl div5;
        protected HtmlGenericControl div6;
        protected HtmlGenericControl div7;
        protected HtmlGenericControl div8;
        private int intBoxCount = 8;
        private int intUserID;
        public string strScript = "";
        public string strToolName = "";
        protected HtmlTable tblShow;
        protected HtmlTable tblWait;

        private void btnNext_Click(object sender, ImageClickEventArgs e)
        {
            int num = 0;
            string str = "";
            int num2 = (int) BTPParameterManager.GetParameterRow()["BoxWealth"];
            DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
            int num3 = ToolItem.HasTool(this.intUserID, 12, 1);
            int num4 = (int) accountRowByUserID["Wealth"];
            if (num4 < num2)
            {
                num = -4;
            }
            else if (num3 > 0)
            {
                accountRowByUserID = BTPBoxManager.GetPayBoxByUserID(this.intUserID);
                switch (((int) accountRowByUserID["Status"]))
                {
                    case 1:
                        str = accountRowByUserID["Name"].ToString().Trim();
                        break;

                    case 2:
                    {
                        int num5 = (int) accountRowByUserID["AddDate"];
                        string commandText = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", DateTime.Now.AddDays((double) num5).ToString(), "' WHERE UserID=", this.intUserID });
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
                        DTOnlineManager.ChangePayTypeByUserID(this.intUserID, 1);
                        base.Response.Redirect("Report.aspx?Parameter=402");
                        return;
                    }
                }
            }
            else
            {
                num = -3;
            }
            base.Response.Redirect(string.Concat(new object[] { "ShowBox.aspx?Index=", num, "&Name=", str }));
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str = BTPBoxManager.GetMyBoxListByUserID(this.intUserID)["Name"].ToString().Trim();
            base.Response.Redirect("SecretaryPage.aspx?Type=FREEBOX&Name=" + str);
        }

        private void InitializeComponent()
        {
            this.btnNext.Click += new ImageClickEventHandler(this.btnNext_Click);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
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
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "Box/getit.gif";
                this.btnNext.ImageUrl = SessionItem.GetImageURL() + "Box/getanother.gif";
                string str = SessionItem.GetRequest("Type", 1).ToString();
                int request = (int) SessionItem.GetRequest("Index", 0);
                string str2 = SessionItem.GetRequest("Name", 1).ToString();
                if (str != "SHOW")
                {
                    this.tblShow.Visible = false;
                    this.tblWait.Visible = true;
                    this.strScript = string.Concat(new object[] { "BoxWait(", request, ",\"", str2, "\");" });
                }
                else
                {
                    this.tblShow.Visible = true;
                    this.tblWait.Visible = false;
                    this.SetBox();
                }
            }
        }

        private void SetBox()
        {
            if (ToolItem.HasTool(this.intUserID, 12, 1) < 1)
            {
                base.Response.Redirect("SecretaryPage.aspx?Type=NOBOX");
            }
            else
            {
                DataTable boxListByUserID = BTPBoxManager.GetBoxListByUserID(this.intUserID, this.intBoxCount);
                DataRow myBoxListByUserID = BTPBoxManager.GetMyBoxListByUserID(this.intUserID);
                string str = myBoxListByUserID["Img"].ToString().Trim();
                int num = (int) myBoxListByUserID["Amount"];
                string str2 = num.ToString();
                if (num > 0x2710)
                {
                    float num2 = num / 0x2710;
                    str2 = num2.ToString().Trim() + "万";
                }
                string str3 = myBoxListByUserID["Name"].ToString().Trim();
                this.strToolName = str3;
                this.div0.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + str + "\" alt=\"" + str3 + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                try
                {
                    Random rnd = RandomItem.rnd;
                    ArrayList list = new ArrayList();
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    list.Add(null);
                    int num3 = Convert.ToInt32(myBoxListByUserID["Rnd"]);
                    string[] strArray = new string[3];
                    foreach (DataRow row2 in boxListByUserID.Rows)
                    {
                        string[] strArray2 = new string[] { row2["Img"].ToString().Trim(), row2["Name"].ToString().Trim(), row2["Amount"].ToString().Trim() };
                        list[num3 % 8] = strArray2;
                        num3++;
                    }
                    strArray = (string[]) list[0];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num4 = num / 0x2710;
                        str2 = num4.ToString().Trim() + "万";
                        if (num4 >= 10000f)
                        {
                            str2 = (num4 / 10000f) + "亿";
                        }
                    }
                    this.div1.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[1];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num5 = num / 0x2710;
                        str2 = num5.ToString().Trim() + "万";
                        if (num5 >= 10000f)
                        {
                            str2 = (num5 / 10000f) + "亿";
                        }
                    }
                    this.div2.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[2];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num6 = num / 0x2710;
                        str2 = num6.ToString().Trim() + "万";
                        if (num6 >= 10000f)
                        {
                            str2 = (num6 / 10000f) + "亿";
                        }
                    }
                    this.div3.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[3];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num7 = num / 0x2710;
                        str2 = num7.ToString().Trim() + "万";
                        if (num7 >= 10000f)
                        {
                            str2 = (num7 / 10000f) + "亿";
                        }
                    }
                    this.div4.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[4];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num8 = num / 0x2710;
                        str2 = num8.ToString().Trim() + "万";
                        if (num8 >= 10000f)
                        {
                            str2 = (num8 / 10000f) + "亿";
                        }
                    }
                    this.div5.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[5];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num9 = num / 0x2710;
                        str2 = num9.ToString().Trim() + "万";
                        if (num9 >= 10000f)
                        {
                            str2 = (num9 / 10000f) + "亿";
                        }
                    }
                    this.div6.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[6];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num10 = num / 0x2710;
                        str2 = num10.ToString().Trim() + "万";
                        if (num10 >= 10000f)
                        {
                            str2 = (num10 / 10000f) + "亿";
                        }
                    }
                    this.div7.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                    strArray = (string[]) list[7];
                    str2 = strArray[2];
                    num = Convert.ToInt32(str2);
                    if (num > 0x2710)
                    {
                        float num11 = num / 0x2710;
                        str2 = num11.ToString().Trim() + "万";
                        if (num11 >= 10000f)
                        {
                            str2 = (num11 / 10000f) + "亿";
                        }
                    }
                    this.div8.InnerHtml = "<img src=\"" + SessionItem.GetImageURL() + "Tools/" + strArray[0] + "\" alt=\"" + strArray[1] + "\" style=\"margin:0 0 1px 0;width:30px; height:30px;\" /><br />" + str2;
                }
                catch
                {
                    BTPBoxManager.DelBoxByUserID(this.intUserID);
                    base.Response.Redirect("ManagerTool.aspx?Type=USEBOX");
                }
            }
        }
    }
}


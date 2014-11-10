namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class VArrangeDev : Page
    {
        protected ImageButton btnOK;
        protected HtmlInputHidden hidArrDev;
        private int intCategory;
        private int intClubID;
        private int intTag;
        private int intUserID;
        public string strArrange;
        public string strArrDev;
        public string strArrDevClub;
        public string strDef1;
        public string strDef2;
        public string strDef3;
        public string strDef4;
        public string strDef5;
        public string strDef6;
        public string strDef7;
        private string strDevCode;
        public string strInfo;
        public string strOff1;
        public string strOff2;
        public string strOff3;
        public string strOff4;
        public string strOff5;
        public string strOff6;
        public string strOff7;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string strArrange = this.hidArrDev.Value.ToString().Trim();
            string[] strArray = strArrange.Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string str2 = strArray[i];
                if (this.strArrange.IndexOf(str2) == -1)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
                if ((strArray[i] == "0") && (i < 5))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                    return;
                }
            }
            if (strArrange.IndexOf("||") > -1)
            {
                base.Response.Redirect("Report.aspx?Parameter=70");
            }
            else
            {
                BTPDevMatchManager.PrearrangeManage(this.intClubID, this.intTag, strArrange);
                base.Response.Redirect(string.Concat(new object[] { "Report.aspx?Parameter=78a!UserID.", this.intUserID, "^Devision.", this.strDevCode, "^Page.1" }));
            }
        }

        private string GetDeffArrName(int intDefense)
        {
            switch (intDefense)
            {
                case 1:
                    return "2-3联防";

                case 2:
                    return "3-2联防";

                case 3:
                    return "2-1-2联防";

                case 4:
                    return "盯人防守";

                case 5:
                    return "盯人内线";

                case 6:
                    return "盯人外线";
            }
            return "";
        }

        private string GetOffArrName(int intOffense)
        {
            switch (intOffense)
            {
                case 1:
                    return "强打内线";

                case 2:
                    return "中锋策应";

                case 3:
                    return "外线投篮";

                case 4:
                    return "快速进攻";

                case 5:
                    return "整体配合";

                case 6:
                    return "掩护挡拆";
            }
            return "";
        }

        private void InitializeComponent()
        {
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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                if (onlineRowByUserID != null)
                {
                    this.intClubID = (int) onlineRowByUserID["ClubID5"];
                    this.intCategory = (int) onlineRowByUserID["Category"];
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=12");
                }
                SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                if ((this.intCategory != 5) && (this.intCategory != 2))
                {
                    base.Response.Redirect("Report.aspx?Parameter=90");
                }
                else
                {
                    DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intClubID);
                    if (clubRowByClubID != null)
                    {
                        this.strArrDevClub = clubRowByClubID["ArrangeDev"].ToString().Trim();
                    }
                    DataTable arrTableByClubID = BTPArrange5Manager.GetArrTableByClubID(this.intClubID);
                    if (arrTableByClubID != null)
                    {
                        int num = 0;
                        string str = "";
                        int intOffense = 0;
                        int intDefense = 0;
                        string offArrName = "";
                        string deffArrName = "";
                        int num4 = 0;
                        this.strArrange = "0|不采用|无|无,";
                        foreach (DataRow row3 in arrTableByClubID.Rows)
                        {
                            num4++;
                            num = (int) row3["Arrange5ID"];
                            str = row3["Name"].ToString().Trim();
                            intOffense = (byte) row3["Offense"];
                            intDefense = (byte) row3["Defense"];
                            offArrName = this.GetOffArrName(intOffense);
                            deffArrName = this.GetDeffArrName(intDefense);
                            object strArrange = this.strArrange;
                            this.strArrange = string.Concat(new object[] { strArrange, num, "|", str, "|", offArrName, "|", deffArrName });
                            if (num4 < 4)
                            {
                                this.strArrange = this.strArrange + ",";
                            }
                        }
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intTag = SessionItem.GetRequest("Tag", 0);
            BTPGameManager.GetTurn();
            DataRow devMRowByDevMatchID = BTPDevMatchManager.GetDevMRowByDevMatchID(this.intTag);
            if (devMRowByDevMatchID != null)
            {
                int intClubID = 0;
                int num2 = (int) devMRowByDevMatchID["ClubAID"];
                int num3 = (int) devMRowByDevMatchID["ClubHID"];
                string str = devMRowByDevMatchID["ArrangeH"].ToString().Trim();
                string str2 = devMRowByDevMatchID["ArrangeA"].ToString().Trim();
                this.strDevCode = devMRowByDevMatchID["DevCode"].ToString().Trim();
                int num4 = (int) devMRowByDevMatchID["Round"];
                if (num2 == this.intClubID)
                {
                    intClubID = num3;
                    if (str2 != "NO")
                    {
                        this.strArrDev = str2;
                    }
                    else
                    {
                        this.strArrDev = this.strArrDevClub;
                    }
                }
                else if (num3 == this.intClubID)
                {
                    intClubID = num2;
                    if (str != "NO")
                    {
                        this.strArrDev = str;
                    }
                    else
                    {
                        this.strArrDev = this.strArrDevClub;
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                DataRow row2 = BTPAccountManager.GetAccountRowByClubID5(intClubID);
                if (row2 != null)
                {
                    string str3 = row2["ShortName"].ToString().Trim();
                    int num5 = (int) row2["UnionID"];
                    string str4 = row2["ClubName"].ToString().Trim();
                    int num6 = (int) row2["UserID"];
                    if (num5 > 0)
                    {
                        str4 = str3 + "-" + str4;
                    }
                    this.strInfo = string.Concat(new object[] { "第", num4, "轮阵容战术&nbsp;&nbsp;&nbsp;&nbsp;对方球队：<a href=\"ShowClub.aspx?Type=5&UserID=", num6, "\" target=\"Right\">", str4, "</a>" });
                }
                if (!base.IsPostBack)
                {
                    this.hidArrDev.Value = this.strArrDev;
                }
            }
        }
    }
}


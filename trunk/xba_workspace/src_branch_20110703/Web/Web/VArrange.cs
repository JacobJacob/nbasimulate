namespace Web
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class VArrange : Page
    {
        protected ImageButton btnOK;
        protected ImageButton btnSet;
        protected DropDownList ddlArrange1;
        protected DropDownList ddlArrange2;
        protected DropDownList ddlArrange3;
        protected DropDownList ddlArrange4;
        protected DropDownList ddlArrange5;
        protected DropDownList ddlArrange6;
        protected DropDownList ddlArrange7;
        protected DropDownList ddlDefCenter;
        protected DropDownList ddlOffCenter;
        protected HtmlInputHidden form_c;
        protected HtmlInputHidden form_img1;
        protected HtmlInputHidden form_img3;
        protected HtmlInputHidden form_pf;
        protected HtmlInputHidden form_pg;
        protected HtmlInputHidden form_sf;
        protected HtmlInputHidden form_sg;
        protected HtmlInputHidden hidArrCup;
        protected HtmlInputHidden hidArrDev;
        protected HtmlInputHidden hidArrOther;
        protected HtmlInputHidden hidCup;
        protected HtmlInputHidden hidOther;
        private int intCategory;
        private int intClubID;
        private int intType;
        private int intUserID;
        public long longCID;
        public long longPFID;
        public long longPGID;
        public long longSFID;
        public long longSGID;
        protected RadioButton rbDef1;
        protected RadioButton rbDef2;
        protected RadioButton rbDef3;
        protected RadioButton rbDef4;
        protected RadioButton rbDef5;
        protected RadioButton rbDef6;
        protected RadioButton rbLUnuse;
        protected RadioButton rbLUse;
        protected RadioButton rbOff1;
        protected RadioButton rbOff2;
        protected RadioButton rbOff3;
        protected RadioButton rbOff4;
        protected RadioButton rbOff5;
        protected RadioButton rbOff6;
        protected RadioButton rbWUnuse;
        protected RadioButton rbWUse;
        private SalaryData SD;
        protected DropDownList sDefHard;
        protected DropDownList sOffHard;
        public string strArrange = "";
        public string strArrangeLvl = "";
        public string strArrCup = "";
        public string strArrDev = "";
        public string strArrOther = "";
        public string strAssignBC;
        public string strAssScript;
        public string strDefStrongPercent;
        private string strDevCode;
        public string strDevTs = "NO";
        public string strList;
        private string strNickName;
        public string strOffStrongPercent;
        public string strPageIntro;
        public string strPageLoad;
        public string strPlayerList;
        public string strPlayerPosArray;
        protected HtmlTable tblArrangeLvl;
        protected HtmlTable tblSelectArrange;
        protected HtmlTable tblSetArrange;
        protected TextBox tbName;

        private void Assign()
        {
            if (!this.Page.IsPostBack)
            {
                DataRow arrByCategory = BTPArrange5Manager.GetArrByCategory(this.intClubID, this.intType);
                string str = arrByCategory["Name"].ToString().Trim();
                this.longCID = (long) arrByCategory["CID"];
                this.longPFID = (long) arrByCategory["PFID"];
                this.longSFID = (long) arrByCategory["SFID"];
                this.longSGID = (long) arrByCategory["SGID"];
                this.longPGID = (long) arrByCategory["PGID"];
                int num = (byte) arrByCategory["Offense"];
                int num2 = (byte) arrByCategory["Defense"];
                byte num1 = (byte) arrByCategory["OffHard"];
                byte num27 = (byte) arrByCategory["DefHard"];
                int num3 = (byte) arrByCategory["OffCenter"];
                int num4 = (byte) arrByCategory["DefCenter"];
                this.SD = new SalaryData(this.intClubID, this.longCID, this.longPFID, this.longSFID, this.longSGID, this.longPGID);
                this.strOffStrongPercent = this.SD.OffStrong.ToString().Trim();
                this.strDefStrongPercent = this.SD.DefStrong.ToString().Trim();
                this.strAssignBC = string.Concat(new object[] { "document.all.imgOffBC.src='", SessionItem.GetImageURL(), "Match/Vocation/Off", num, ".gif';document.all.imgDefBC.src='", SessionItem.GetImageURL(), "Match/Vocation/Def", num2, "_100.gif';" });
                this.tbName.Text = str;
                this.ddlOffCenter.SelectedValue = num3.ToString();
                this.ddlDefCenter.SelectedValue = num4.ToString();
                switch (num)
                {
                    case 1:
                        this.rbOff1.Checked = true;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        this.rbOff5.Checked = false;
                        this.rbOff6.Checked = false;
                        break;

                    case 2:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = true;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        this.rbOff5.Checked = false;
                        this.rbOff6.Checked = false;
                        break;

                    case 3:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = true;
                        this.rbOff4.Checked = false;
                        this.rbOff5.Checked = false;
                        this.rbOff6.Checked = false;
                        break;

                    case 4:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = true;
                        this.rbOff5.Checked = false;
                        this.rbOff6.Checked = false;
                        break;

                    case 5:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        this.rbOff5.Checked = true;
                        this.rbOff6.Checked = false;
                        break;

                    case 6:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        this.rbOff5.Checked = false;
                        this.rbOff6.Checked = true;
                        break;
                }
                switch (num2)
                {
                    case 1:
                        this.rbDef1.Checked = true;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        this.rbDef5.Checked = false;
                        this.rbDef6.Checked = false;
                        break;

                    case 2:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = true;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        this.rbDef5.Checked = false;
                        this.rbDef6.Checked = false;
                        break;

                    case 3:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = true;
                        this.rbDef4.Checked = false;
                        this.rbDef5.Checked = false;
                        this.rbDef6.Checked = false;
                        break;

                    case 4:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = true;
                        this.rbDef5.Checked = false;
                        this.rbDef6.Checked = false;
                        break;

                    case 5:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        this.rbDef5.Checked = true;
                        this.rbDef6.Checked = false;
                        break;

                    case 6:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        this.rbDef5.Checked = false;
                        this.rbDef6.Checked = true;
                        break;
                }
            }
            int num24 = 200;
            int num25 = 20;
            foreach (DataRow row2 in BTPPlayer5Manager.GetArrPlayerTable(this.intClubID).Rows)
            {
                long num5 = (long) row2["PlayerID"];
                int intCategory = Convert.ToInt32(row2["Category"]);
                int num11 = (byte) row2["Age"];
                string str2 = row2["Name"].ToString().Trim();
                int intPosition = (byte) row2["Pos"];
                string playerChsPosition = PlayerItem.GetPlayerChsPosition(intPosition);
                int num7 = (byte) row2["Number"];
                int num8 = (byte) row2["Power"];
                float num12 = ((float) ((int) row2["Ability"])) / 10f;
                int num9 = (byte) row2["Height"];
                int num10 = (byte) row2["Weight"];
                float num13 = ((float) ((int) row2["Speed"])) / 10f;
                float num14 = ((float) ((int) row2["Jump"])) / 10f;
                float num15 = ((float) ((int) row2["Strength"])) / 10f;
                float num16 = ((float) ((int) row2["Stamina"])) / 10f;
                float num17 = ((float) ((int) row2["Shot"])) / 10f;
                float num18 = ((float) ((int) row2["Point3"])) / 10f;
                float num19 = ((float) ((int) row2["Dribble"])) / 10f;
                float num20 = ((float) ((int) row2["Pass"])) / 10f;
                float num21 = ((float) ((int) row2["Rebound"])) / 10f;
                float num22 = ((float) ((int) row2["Steal"])) / 10f;
                float num23 = ((float) ((int) row2["Block"])) / 10f;
                float single1 = ((float) ((int) row2["Attack"])) / 10f;
                float single2 = ((float) ((int) row2["Defense"])) / 10f;
                float single3 = ((float) ((int) row2["Team"])) / 10f;
                if (intCategory == 3)
                {
                    num12 = 99.9f;
                    num13 = 99.9f;
                    num14 = 99.9f;
                    num15 = 99.9f;
                    num16 = 99.9f;
                    num17 = 99.9f;
                    num18 = 99.9f;
                    num19 = 99.9f;
                    num20 = 99.9f;
                    num21 = 99.9f;
                    num22 = 99.9f;
                    num23 = 99.9f;
                    single1 = 99.9f;
                    single2 = 99.9f;
                    single3 = 99.9f;

                }
                object strPlayerList = this.strPlayerList;
                this.strPlayerList = string.Concat(new object[] { 
                    strPlayerList, "<DIV onmouseup=putdown(this);down=false; onmousedown=logpos(this); id='", num5, "' style='Z-INDEX:100;LEFT:", num25, "px;CURSOR:hand;POSITION:absolute;TOP:", num24, "px;HEIGHT:30px;WEIGHT:28px' ><DIV title='姓名：", str2, "[", num11, "]<br>位置：", playerChsPosition, "<br>身高：", num9, "CM<br>体重：", 
                    num10, "KG<br>体力：", num8, "<br>综合：", num12, "<br><br>速度：", num13, "<br>弹跳：", num14, "<br>强壮：", num15, "<br>耐力：", num16, "<br>投篮：", num17, "<br>三分：", 
                    num18, "<br>运球：", num19, "<br>传球：", num20, "<br>篮板：", num21, "<br>抢断：", num22, "<br>封盖：", num23, "' style='Z-INDEX: 1; BACKGROUND: url(", SessionItem.GetImageURL(), "Player/Number/", num7, ".gif); WIDTH: 16px; CURSOR: hand; COLOR: white; HEIGHT: 19px' align='center'></DIV></DIV>"
                 });
                num25 += 30;
                strPlayerList = this.strPlayerPosArray;
                this.strPlayerPosArray = string.Concat(new object[] { strPlayerList, "player_pos[\"", num5, "\"] = getpos(document.getElementById(\"", num5, "\"));" });
            }
        }

        private void AssignArrange()
        {
            if (!base.IsPostBack)
            {
                DataTable arrTableByClubID = BTPArrange5Manager.GetArrTableByClubID(this.intClubID);
                if (arrTableByClubID != null)
                {
                    int num = 0;
                    string str = "";
                    int num2 = 0;
                    this.strArrange = "0|不采用,";
                    foreach (DataRow row in arrTableByClubID.Rows)
                    {
                        num2++;
                        num = (int) row["Arrange5ID"];
                        str = row["Name"].ToString().Trim();
                        object strArrange = this.strArrange;
                        this.strArrange = string.Concat(new object[] { strArrange, num, "|", str });
                        if (num2 < 4)
                        {
                            this.strArrange = this.strArrange + ",";
                        }
                    }
                }
                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intClubID);
                if (clubRowByClubID != null)
                {
                    this.strArrDev = clubRowByClubID["ArrangeDev"].ToString().Trim();
                    this.strArrCup = clubRowByClubID["ArrangeCup"].ToString().Trim();
                    this.strArrOther = clubRowByClubID["ArrangeOther"].ToString().Trim();
                    this.hidArrDev.Value = this.strArrDev;
                    this.hidArrCup.Value = this.strArrCup;
                    this.hidArrOther.Value = this.strArrOther;
                    if (!SessionItem.CanUseAfterUpdate())
                    {
                        this.strDevTs = "NO";
                    }
                    else
                    {
                        this.strDevTs = this.strArrDev;
                    }
                    if (this.strArrCup == "NO")
                    {
                        this.hidCup.Value = "0";
                    }
                    else
                    {
                        this.hidCup.Value = "1";
                    }
                    if (this.strArrOther == "NO")
                    {
                        this.hidOther.Value = "0";
                    }
                    else
                    {
                        this.hidOther.Value = "1";
                    }
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str;
            try
            {
                string validWords = StringItem.GetValidWords(this.tbName.Text);
                if (StringItem.IsValidContent(validWords, 2, 20))
                {
                    int num6;
                    int num7;
                    long lngPlayerIDC = Convert.ToInt64(this.form_c.Value);
                    long lngPlayerIDPF = Convert.ToInt64(this.form_pf.Value);
                    long lngPlayerIDSF = Convert.ToInt64(this.form_sf.Value);
                    long lngPlayerIDSG = Convert.ToInt64(this.form_sg.Value);
                    long lngPlayerIDPG = Convert.ToInt64(this.form_pg.Value);
                    if (this.rbOff1.Checked)
                    {
                        num6 = 1;
                    }
                    else if (this.rbOff2.Checked)
                    {
                        num6 = 2;
                    }
                    else if (this.rbOff3.Checked)
                    {
                        num6 = 3;
                    }
                    else if (this.rbOff4.Checked)
                    {
                        num6 = 4;
                    }
                    else if (this.rbOff5.Checked)
                    {
                        num6 = 5;
                    }
                    else
                    {
                        num6 = 6;
                    }
                    if (this.rbDef1.Checked)
                    {
                        num7 = 1;
                    }
                    else if (this.rbDef2.Checked)
                    {
                        num7 = 2;
                    }
                    else if (this.rbDef3.Checked)
                    {
                        num7 = 3;
                    }
                    else if (this.rbDef4.Checked)
                    {
                        num7 = 4;
                    }
                    else if (this.rbDef5.Checked)
                    {
                        num7 = 5;
                    }
                    else
                    {
                        num7 = 6;
                    }
                    this.SD = new SalaryData(this.intClubID, lngPlayerIDC, lngPlayerIDPF, lngPlayerIDSF, lngPlayerIDSG, lngPlayerIDPG);
                    int strong = this.SD.Strong;
                    int intDefHard = this.SD.Strong;
                    int intOffCenter = Convert.ToInt16(this.ddlOffCenter.SelectedItem.Value);
                    int intDefCenter = Convert.ToInt16(this.ddlDefCenter.SelectedItem.Value);
                    BTPArrange5Manager.SetArrange(this.intClubID, validWords, this.intType, lngPlayerIDC, lngPlayerIDPF, lngPlayerIDSF, lngPlayerIDSG, lngPlayerIDPG, strong, intDefHard, num6, num7, intOffCenter, intDefCenter);
                    switch (strong)
                    {
                        case 110:
                            str = "Report.aspx?Parameter=75a!Type." + this.intType;
                            goto Label_0487;

                        case 0x6d:
                            str = "Report.aspx?Parameter=75b!Type." + this.intType;
                            goto Label_0487;

                        case 0x6c:
                            str = "Report.aspx?Parameter=75c!Type." + this.intType;
                            goto Label_0487;

                        case 0x6b:
                            str = "Report.aspx?Parameter=75d!Type." + this.intType;
                            goto Label_0487;

                        case 0x6a:
                            str = "Report.aspx?Parameter=75e!Type." + this.intType;
                            goto Label_0487;

                        case 0x69:
                            str = "Report.aspx?Parameter=75f!Type." + this.intType;
                            goto Label_0487;

                        case 0x68:
                            str = "Report.aspx?Parameter=75j!Type." + this.intType;
                            goto Label_0487;

                        case 0x67:
                            str = "Report.aspx?Parameter=75h!Type." + this.intType;
                            goto Label_0487;

                        case 0x66:
                            str = "Report.aspx?Parameter=75i!Type." + this.intType;
                            goto Label_0487;

                        case 0x65:
                            str = "Report.aspx?Parameter=75j!Type." + this.intType;
                            goto Label_0487;

                        case 100:
                            str = "Report.aspx?Parameter=75k!Type." + this.intType;
                            goto Label_0487;

                        case 0x63:
                            str = "Report.aspx?Parameter=75l!Type." + this.intType;
                            goto Label_0487;

                        case 0x62:
                            str = "Report.aspx?Parameter=75m!Type." + this.intType;
                            goto Label_0487;

                        case 0x61:
                            str = "Report.aspx?Parameter=75n!Type." + this.intType;
                            goto Label_0487;

                        case 0x60:
                            str = "Report.aspx?Parameter=75o!Type." + this.intType;
                            goto Label_0487;

                        case 0x5f:
                            str = "Report.aspx?Parameter=75p!Type." + this.intType;
                            goto Label_0487;

                        case 0x5e:
                            str = "Report.aspx?Parameter=75q!Type." + this.intType;
                            goto Label_0487;

                        case 0x5d:
                            str = "Report.aspx?Parameter=75r!Type." + this.intType;
                            goto Label_0487;

                        case 0x5c:
                            str = "Report.aspx?Parameter=75s!Type." + this.intType;
                            goto Label_0487;

                        case 0x5b:
                            str = "Report.aspx?Parameter=75t!Type." + this.intType;
                            goto Label_0487;
                    }
                    str = "Report.aspx?Parameter=75u!Type." + this.intType;
                }
                else
                {
                    str = "Report.aspx?Parameter=77!Type." + this.intType;
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
                str = "Report.aspx?Parameter=74";
            }
        Label_0487:
            base.Response.Redirect(str);
        }

        private void btnSet_Click(object sender, ImageClickEventArgs e)
        {
            string str = "";
            DataTable arrTableByClubID = BTPArrange5Manager.GetArrTableByClubID(this.intClubID);
            if (arrTableByClubID != null)
            {
                int num2 = 0;
                foreach (DataRow row in arrTableByClubID.Rows)
                {
                    int num = (int) row["Arrange5ID"];
                    num2++;
                    str = str + num;
                    if (num2 < 4)
                    {
                        str = str + ",";
                    }
                }
            }
            str = str + ",0";
            string strArrangeDev = "NO";
            string strArrangeCup = "NO";
            string strArrangeOther = "NO";
            strArrangeDev = this.hidArrDev.Value.ToString().Trim();
            strArrangeCup = this.hidArrCup.Value.ToString().Trim();
            strArrangeOther = this.hidArrOther.Value.ToString().Trim();
            string str5 = this.hidCup.Value.ToString().Trim();
            string str6 = this.hidOther.Value.ToString().Trim();
            string[] strArray = strArrangeDev.Split(new char[] { '|' });
            for (int i = 0; i < strArray.Length; i++)
            {
                if (str.IndexOf(strArray[i]) == -1)
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
            if ((strArrangeCup != "NO") && (str5 == "1"))
            {
                string[] strArray2 = strArrangeCup.Split(new char[] { '|' });
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (str.IndexOf(strArray2[j]) == -1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                    if ((strArray2[j] == "0") && (j < 5))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                }
            }
            else
            {
                strArrangeCup = "NO";
            }
            if ((strArrangeOther != "NO") && (str6 == "1"))
            {
                string[] strArray3 = strArrangeOther.Split(new char[] { '|' });
                for (int k = 0; k < strArray3.Length; k++)
                {
                    if (str.IndexOf(strArray3[k]) == -1)
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                    if ((strArray3[k] == "0") && (k < 5))
                    {
                        base.Response.Redirect("Report.aspx?Parameter=3");
                        return;
                    }
                }
            }
            else
            {
                strArrangeOther = "NO";
            }
            if (((strArrangeDev.IndexOf("||") > -1) || (strArrangeCup.IndexOf("||") > -1)) || (strArrangeOther.IndexOf("||") > -1))
            {
                base.Response.Redirect("Report.aspx?Parameter=70");
            }
            else
            {
                BTPClubManager.SetArrange5(this.intClubID, strArrangeDev, strArrangeCup, strArrangeOther);
                int request = (int) SessionItem.GetRequest("Jump", 0);
                if (request == 1)
                {
                    base.Response.Redirect("OnlyOneCenter.aspx?Type=MATCHREG");
                }
                else
                {
                    base.Response.Redirect("Report.aspx?Parameter=76!Type.0");
                }
            }
        }

        private void InitializeComponent()
        {
            this.btnSet.Click += new ImageClickEventHandler(this.btnSet_Click);
            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click);
            base.Load += new EventHandler(this.Page_Load);
        }

        private void MatchLev(int intUserIDA, int intClubIDA)
        {
            this.strList = "";
            intUserIDA = (int) SessionItem.GetRequest("UserID", 0);
            intClubIDA = (int) SessionItem.GetRequest("ClubID", 0);
            DataTable reader = BTPToolLinkManager.CheckClubLink(this.intUserID, intClubIDA, 5);
            bool flag = false;
            if (reader != null)
            {
                foreach (DataRow dataRow in reader.Rows)
                {
                    int num = (byte)dataRow["Category"];
                    if (num == 1)
                    {
                        flag = true;
                    }
                }
            }
            //reader.Close();
            if (flag)
            {
                this.intUserID = intUserIDA;
            }
            DataRow row = BTPArrangeLvlManager.GetArrange5(this.intUserID);
            int intLevel = (byte) row["VOInside"];
            int num3 = (byte) row["VOCHelp"];
            int num4 = (byte) row["VOOutside"];
            int num5 = (byte) row["VOSpeed"];
            int num6 = (byte) row["VOAll"];
            int num7 = (byte) row["VOBlock"];
            int num8 = (byte) row["VDArea23"];
            int num9 = (byte) row["VDArea32"];
            int num10 = (byte) row["VDArea212"];
            int num11 = (byte) row["VDOne"];
            int num12 = (byte) row["VDOneInside"];
            int num13 = (byte) row["VDOneOutside"];
            int num14 = (int) row["VOInsideP"];
            int num15 = (int) row["VOCHelpP"];
            int num16 = (int) row["VOOutsideP"];
            int num17 = (int) row["VOSpeedP"];
            int num18 = (int) row["VOAllP"];
            int num19 = (int) row["VOBlockP"];
            int num20 = (int) row["VDArea23P"];
            int num21 = (int) row["VDArea32P"];
            int num22 = (int) row["VDArea212P"];
            int num23 = (int) row["VDOneP"];
            int num24 = (int) row["VDOneInsideP"];
            int num25 = (int) row["VDOneOutsideP"];
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>内线强攻</td><td>", intLevel, "</td><td>", num14, "/", BTPArrangeLvlManager.Get5ArrangeNeed(intLevel), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>中锋策应</td><td>", num3, "</td><td>", num15, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num3), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>外线投篮</td><td>", num4, "</td><td>", num16, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num4), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>快速进攻</td><td>", num5, "</td><td>", num17, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num5), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>整体配合</td><td>", num6, "</td><td>", num18, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num6), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>掩护挡拆</td><td>", num7, "</td><td>", num19, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num7), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>2-3联防</td><td>", num8, "</td><td>", num20, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num8), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>3-2联防</td><td>", num9, "</td><td>", num21, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num9), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>2-1-2联防</td><td>", num10, "</td><td>", num22, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num10), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人防守</td><td>", num11, "</td><td>", num23, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num11), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人内线</td><td>", num12, "</td><td>", num24, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num12), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人外线</td><td>", num13, "</td><td>", num25, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num13), "</td><td></td></tr>" });
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
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intClubID = (int) onlineRowByUserID["ClubID5"];
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.strDevCode = onlineRowByUserID["DevCode"].ToString().Trim();
                if ((this.intCategory != 5) && (this.intCategory != 2))
                {
                    base.Response.Redirect("Report.aspx?Parameter=90");
                }
                else
                {
                    this.intType = (int) SessionItem.GetRequest("Type", 0);
                    if (this.intType != 0)
                    {
                        SessionItem.CheckCanUseAfterUpdate(this.intCategory);
                    }
                    this.tblSelectArrange.Visible = false;
                    this.tblSetArrange.Visible = false;
                    this.tblArrangeLvl.Visible = false;
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    this.btnSet.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    switch (this.intType)
                    {
                        case 0:
                            this.strPageIntro = "<ul><li class='qian1'>配置阵容</li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSelectArrange.Visible = true;
                            this.AssignArrange();
                            break;

                        case 1:
                            this.strPageIntro = "<ul><li class='qian1a'><a href='VArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2'>阵容一</li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSetArrange.Visible = true;
                            this.strPageLoad = "PageLoad();";
                            this.Assign();
                            this.SetArrScript();
                            break;

                        case 2:
                            this.strPageIntro = "<ul><li class='qian1a'><a href='VArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2'>阵容二</li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSetArrange.Visible = true;
                            this.strPageLoad = "PageLoad();";
                            this.Assign();
                            this.SetArrScript();
                            break;

                        case 3:
                            this.strPageIntro = "<ul><li class='qian1a'><a href='VArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2'>阵容三</li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSetArrange.Visible = true;
                            this.strPageLoad = "PageLoad();";
                            this.Assign();
                            this.SetArrScript();
                            break;

                        case 4:
                            this.strPageIntro = "<ul><li class='qian1a'><a href='VArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2'>阵容四</li><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSetArrange.Visible = true;
                            this.strPageLoad = "PageLoad();";
                            this.Assign();
                            this.SetArrScript();
                            break;

                        case 5:
                            this.strPageIntro = "<ul><li class='qian1a'><a href='VArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li><li class='qian2'>战术等级</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblArrangeLvl.Visible = true;
                            this.SetList();
                            break;

                        case 6:
                        {
                            this.strPageIntro = "<img src='" + SessionItem.GetImageURL() + "MenuCard/VArrange/SArrange_07.GIF' border='0' height='24' width='73'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            int request = (int) SessionItem.GetRequest("UserID", 0);
                            int intClubIDA = (int) SessionItem.GetRequest("ClubID", 0);
                            if ((request > 0) && (intClubIDA > 0))
                            {
                                this.tblArrangeLvl.Visible = true;
                                this.MatchLev(request, intClubIDA);
                            }
                            break;
                        }
                        default:
                            this.strPageIntro = "<ul><li class='qian1'>配置阵容</li><li class='qian2a'><a href='VArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='VArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='VArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='VArrange.aspx?Type=4'>阵容四</a></li></ul><li class='qian2a'><a href='VArrange.aspx?Type=5'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                            this.tblSelectArrange.Visible = true;
                            this.AssignArrange();
                            break;
                    }
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string s = "<script language=\"javascript\">parent.Right.location=\"ShowClub.aspx?UserID=" + this.intUserID + "&Type=5\"</script>";
            base.Response.Write(s);
        }

        private void SetArrScript()
        {
            DataTable playerTableByClubID = BTPPlayer5Manager.GetPlayerTableByClubID(this.intClubID);
            string str = "";
            int count = playerTableByClubID.Rows.Count;
            if (playerTableByClubID.Rows.Count > 1)
            {
                foreach (DataRow row in playerTableByClubID.Rows)
                {
                    long num2 = (long) row["PlayerID"];
                    int num3 = (int) row["Salary"];
                    object obj2 = str;
                    str = string.Concat(new object[] { obj2, num2, ",", num3, "|" });
                }
            }
            else
            {
                base.Response.Redirect("Report.aspx?Parameter=3");
                return;
            }
            this.strAssScript = string.Concat(new object[] { "var pos_c = false;\nvar pos_pf = false;\nvar pos_sf = false;\nvar pos_sg = false;\nvar pos_pg = false;\nvar down = false;\nvar max_z_index = 100;\nvar saveobj = false;\nvar LogObj = null;\nvar player_pos = new Array();\n", this.strPlayerPosArray, "\nvar position = new Array();\nvar cobj = document.getElementById(\"c\");\nposition[\"c\"] = getpos(cobj);\nvar fobj = document.getElementById(\"pf\");\nposition[\"pf\"] = getpos(fobj);\n\nvar fobj = document.getElementById(\"sf\");\nposition[\"sf\"] = getpos(fobj);\n\nvar gobj = document.getElementById(\"sg\");\nposition[\"sg\"] = getpos(gobj);\nvar gobj = document.getElementById(\"pg\");\nposition[\"pg\"] = getpos(gobj);\ndocument.getElementById(\"SalaryTop\").value=", DevCalculator.GetSalaryTop(this.strDevCode), ";\ndocument.getElementById(\"PlayerSalary\").value=\"", str, "\";\ndocument.getElementById(\"PlayerCount\").value=", count, ";\n" });
        }

        private void SetList()
        {
            this.strList = "";
            DataRow row = BTPArrangeLvlManager.GetArrange5(this.intUserID);
            int intLevel = (byte) row["VOInside"];
            int num2 = (byte) row["VOCHelp"];
            int num3 = (byte) row["VOOutside"];
            int num4 = (byte) row["VOSpeed"];
            int num5 = (byte) row["VOAll"];
            int num6 = (byte) row["VOBlock"];
            int num7 = (byte) row["VDArea23"];
            int num8 = (byte) row["VDArea32"];
            int num9 = (byte) row["VDArea212"];
            int num10 = (byte) row["VDOne"];
            int num11 = (byte) row["VDOneInside"];
            int num12 = (byte) row["VDOneOutside"];
            int num13 = (int) row["VOInsideP"];
            int num14 = (int) row["VOCHelpP"];
            int num15 = (int) row["VOOutsideP"];
            int num16 = (int) row["VOSpeedP"];
            int num17 = (int) row["VOAllP"];
            int num18 = (int) row["VOBlockP"];
            int num19 = (int) row["VDArea23P"];
            int num20 = (int) row["VDArea32P"];
            int num21 = (int) row["VDArea212P"];
            int num22 = (int) row["VDOneP"];
            int num23 = (int) row["VDOneInsideP"];
            int num24 = (int) row["VDOneOutsideP"];
            long num1 = (long) row["AllPiont"];
            string str = "";
            this.strArrangeLvl = "<img src='" + SessionItem.GetImageURL() + "/button_xd.GIF' width='70' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"AfreshArrange.aspx\";'>";
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>内线强攻</td><td>", intLevel, str, "</td><td>", num13, "/", BTPArrangeLvlManager.Get5ArrangeNeed(intLevel), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>中锋策应</td><td>", num2, str, "</td><td>", num14, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num2), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>外线投篮</td><td>", num3, str, "</td><td>", num15, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num3), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>快速进攻</td><td>", num4, str, "</td><td>", num16, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num4), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>整体配合</td><td>", num5, str, "</td><td>", num17, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num5), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>掩护挡拆</td><td>", num6, str, "</td><td>", num18, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num6), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>2-3联防</td><td>", num7, str, "</td><td>", num19, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num7), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>3-2联防</td><td>", num8, str, "</td><td>", num20, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num8), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>2-1-2联防</td><td>", num9, str, "</td><td>", num21, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num9), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人防守</td><td>", num10, str, "</td><td>", num22, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num10), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人内线</td><td>", num11, str, "</td><td>", num23, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num11), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人外线</td><td>", num12, str, "</td><td>", num24, "/", BTPArrangeLvlManager.Get5ArrangeNeed(num12), "</td><td></td></tr>" });
        }
    }
}


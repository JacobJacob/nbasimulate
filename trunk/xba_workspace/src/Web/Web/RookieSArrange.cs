namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class RookieSArrange : Page
    {
        protected ImageButton btnOK;
        protected ImageButton btnSet;
        protected DropDownList ddlArrange1;
        protected DropDownList ddlArrange2;
        protected DropDownList ddlArrange3;
        protected DropDownList ddlArrange4;
        protected DropDownList ddlArrange5;
        protected HtmlInputHidden form_c;
        protected HtmlInputHidden form_f;
        protected HtmlInputHidden form_g;
        private int intClubID;
        private int intType;
        private int intUserID;
        public long longCID;
        public long longFID;
        public long longGID;
        protected RadioButton rbDef1;
        protected RadioButton rbDef2;
        protected RadioButton rbDef3;
        protected RadioButton rbDef4;
        protected RadioButton rbOff1;
        protected RadioButton rbOff2;
        protected RadioButton rbOff3;
        protected RadioButton rbOff4;
        protected DropDownList sDefHard;
        protected DropDownList sOffHard;
        private string strAName;
        public string strAssignBC;
        public string strAssScript;
        public string strList;
        private string strNickName;
        public string strPageIntro;
        public string strPageLoad;
        public string strPlayerList;
        public string strPlayerPosArray;
        protected HtmlTable tblArrangeLvl;
        protected HtmlTable tblSelectArrange;
        protected HtmlTable tblSetArrange;

        private void Assign()
        {
            if (!this.Page.IsPostBack)
            {
                DataRow arrByCategory = BTPArrange3Manager.GetArrByCategory(this.intClubID, this.intType);
                this.strAName = arrByCategory["Name"].ToString().Trim();
                this.longCID = (long) arrByCategory["CID"];
                this.longFID = (long) arrByCategory["FID"];
                this.longGID = (long) arrByCategory["GID"];
                int num = (byte) arrByCategory["Offense"];
                int num2 = (byte) arrByCategory["Defense"];
                byte num1 = (byte) arrByCategory["OffHard"];
                int num3 = (byte) arrByCategory["DefHard"];
                this.strAssignBC = string.Concat(new object[] { "document.all.imgOffBC.src='", SessionItem.GetImageURL(), "Match/Street/Off", num, ".gif';document.all.imgDefBC.src='", SessionItem.GetImageURL(), "Match/Street/Def", num2, "_", num3, ".gif';" });
                switch (num)
                {
                    case 1:
                        this.rbOff1.Checked = true;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        break;

                    case 2:
                        this.rbOff1.Checked = true;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        break;

                    case 3:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = true;
                        this.rbOff4.Checked = false;
                        break;

                    case 4:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = true;
                        break;
                }
                switch (num2)
                {
                    case 1:
                        this.rbDef1.Checked = true;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        break;

                    case 2:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = true;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = false;
                        break;

                    case 3:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = true;
                        this.rbDef4.Checked = false;
                        break;

                    case 4:
                        this.rbDef1.Checked = false;
                        this.rbDef2.Checked = false;
                        this.rbDef3.Checked = false;
                        this.rbDef4.Checked = true;
                        break;
                }
            }
            int num4 = 0xff;
            int num5 = 20;
            foreach (DataRow row2 in BTPPlayer3Manager.GetArrPlayerTable(this.intClubID).Rows)
            {
                long num6 = (long) row2["PlayerID"];
                int num7 = (byte) row2["Age"];
                string str = row2["Name"].ToString().Trim();
                int intPosition = (byte) row2["Pos"];
                string playerChsPosition = PlayerItem.GetPlayerChsPosition(intPosition);
                int num9 = (byte) row2["Number"];
                int num10 = (byte) row2["Power"];
                float num11 = ((float) ((int) row2["Ability"])) / 10f;
                int num12 = (byte) row2["Height"];
                int num13 = (byte) row2["Weight"];
                float num14 = ((float) ((int) row2["Speed"])) / 10f;
                float num15 = ((float) ((int) row2["Jump"])) / 10f;
                float num16 = ((float) ((int) row2["Strength"])) / 10f;
                float num17 = ((float) ((int) row2["Stamina"])) / 10f;
                float num18 = ((float) ((int) row2["Shot"])) / 10f;
                float num19 = ((float) ((int) row2["Point3"])) / 10f;
                float num20 = ((float) ((int) row2["Dribble"])) / 10f;
                float num21 = ((float) ((int) row2["Pass"])) / 10f;
                float num22 = ((float) ((int) row2["Rebound"])) / 10f;
                float num23 = ((float) ((int) row2["Steal"])) / 10f;
                float num24 = ((float) ((int) row2["Block"])) / 10f;
                float single1 = ((float) ((int) row2["Attack"])) / 10f;
                float single2 = ((float) ((int) row2["Defense"])) / 10f;
                float single3 = ((float) ((int) row2["Team"])) / 10f;
                object strPlayerList = this.strPlayerList;
                this.strPlayerList = string.Concat(new object[] { 
                    strPlayerList, "<DIV onmouseup=putdown(this);down=false; onmousedown=logpos(this); id='", num6, "' style='Z-INDEX:100;LEFT:", num5, "px;CURSOR:hand;POSITION:absolute;TOP:", num4, "px;HEIGHT:30px;WEIGHT:28px' ondblclick=\"alert('", num6, "');\"><DIV title='姓名：", str, "[", num7, "]<br>位置：", playerChsPosition, "<br>身高：", 
                    num12, "CM<br>体重：", num13, "KG<br>体力：", num10, "<br>综合：", num11, "<br><br>速度：", num14, "<br>弹跳：", num15, "<br>强壮：", num16, "<br>耐力：", num17, "<br>投篮：", 
                    num18, "<br>三分：", num19, "<br>运球：", num20, "<br>传球：", num21, "<br>篮板：", num22, "<br>抢断：", num23, "<br>封盖：", num24, "' style='Z-INDEX: 1; BACKGROUND: url(", SessionItem.GetImageURL(), "Player/Number/", 
                    num9, ".gif); WIDTH: 16px; CURSOR: hand; COLOR: white; HEIGHT: 19px' align='center'></DIV></DIV>"
                 });
                num5 += 30;
                strPlayerList = this.strPlayerPosArray;
                this.strPlayerPosArray = string.Concat(new object[] { strPlayerList, "player_pos[\"", num6, "\"] = getpos(document.getElementById(\"", num6, "\"));" });
            }
        }

        private void AssignArrange()
        {
            if (!base.IsPostBack)
            {
                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intClubID);
                int num = (int) clubRowByClubID["Arrange1"];
                int num2 = (int) clubRowByClubID["Arrange2"];
                int num3 = (int) clubRowByClubID["Arrange3"];
                int num4 = (int) clubRowByClubID["Arrange4"];
                int num5 = (int) clubRowByClubID["Arrange5"];
                DataView view = new DataView(BTPArrange3Manager.GetArrTableByClubID(this.intClubID));
                this.ddlArrange1.DataSource = view;
                this.ddlArrange1.DataBind();
                this.ddlArrange2.DataSource = view;
                this.ddlArrange2.DataBind();
                this.ddlArrange3.DataSource = view;
                this.ddlArrange3.DataBind();
                this.ddlArrange4.DataSource = view;
                this.ddlArrange4.DataBind();
                this.ddlArrange5.DataSource = view;
                this.ddlArrange5.DataBind();
                this.ddlArrange1.SelectedValue = num.ToString();
                this.ddlArrange2.SelectedValue = num2.ToString();
                this.ddlArrange3.SelectedValue = num3.ToString();
                this.ddlArrange4.SelectedValue = num4.ToString();
                this.ddlArrange5.SelectedValue = num5.ToString();
            }
        }

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string s = "";
            DataRow arrByCategory = BTPArrange3Manager.GetArrByCategory(this.intClubID, this.intType);
            this.strAName = arrByCategory["Name"].ToString().Trim();
            try
            {
                this.strAName = StringItem.GetValidWords(this.strAName);
                if (StringItem.IsValidName(this.strAName, 2, 20))
                {
                    int num;
                    int num2;
                    long longCID = Convert.ToInt64(this.form_c.Value);
                    long longFID = Convert.ToInt64(this.form_f.Value);
                    long longGID = Convert.ToInt64(this.form_g.Value);
                    if (this.rbOff1.Checked)
                    {
                        num = 1;
                    }
                    else if (this.rbOff2.Checked)
                    {
                        num = 2;
                    }
                    else if (this.rbOff3.Checked)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 4;
                    }
                    if (this.rbDef1.Checked)
                    {
                        num2 = 1;
                    }
                    else if (this.rbDef2.Checked)
                    {
                        num2 = 2;
                    }
                    else if (this.rbDef3.Checked)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    int intOffHard = 100;
                    int intDefHard = 100;
                    if ((num == 2) && (num2 == 2))
                    {
                        BTPArrange3Manager.SetArrange(this.intClubID, this.strAName, this.intType, longCID, longFID, longGID, intOffHard, intDefHard, num, num2);
                        int intRookieOpIndex = AccountItem.SetRookieOp(this.intUserID, 5);
                        if (intRookieOpIndex != 5)
                        {
                            base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                        }
                        else
                        {
                            base.Response.Write("<script>window.top.Main.location=\"" + StringItem.GetRookieURL(6).ToString().Trim() + "\";</script>");
                        }
                    }
                    else
                    {
                        s = "<script>alert('您最好选用“中锋策应”的进攻战术和“盯人外线”的防守战术才有可能克制“XBA魔鬼队”！');window.top.Main.location='RookieMain_P.aspx?Type=SARRANGE'</script>";
                    }
                }
                else
                {
                    s = "<script>alert('您的阵容（战术）设置不完整，请重新设置！');window.top.Main.location='RookieMain_P.aspx?Type=SARRANGE'</script>";
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
                s = "<script>alert('您的阵容（战术）设置不完整，请重新设置！');window.top.Main.location='RookieMain_P.aspx?Type=SARRANGE'</script>";
            }
            base.Response.Write(s);
        }

        private void btnSet_Click(object sender, ImageClickEventArgs e)
        {
            int num = Convert.ToInt32(this.ddlArrange1.SelectedItem.Value);
            int num2 = Convert.ToInt32(this.ddlArrange2.SelectedItem.Value);
            int num3 = Convert.ToInt32(this.ddlArrange3.SelectedItem.Value);
            int num4 = Convert.ToInt32(this.ddlArrange4.SelectedItem.Value);
            int num5 = Convert.ToInt32(this.ddlArrange5.SelectedItem.Value);
            BTPClubManager.SetArrange3(this.intClubID, num, num2, num3, num4, num5);
            base.Response.Redirect("Report.aspx?Parameter=72!Type.0");
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
            intUserIDA = SessionItem.GetRequest("UserID", 0);
            intClubIDA = SessionItem.GetRequest("ClubID", 0);
            DataTable table = BTPToolLinkManager.CheckClubLink(this.intUserID, intClubIDA, 3);
            bool flag = false;
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (byte) row["Category"];
                    if (num == 1)
                    {
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                this.intUserID = intUserIDA;
            }
            DataRow row2 = BTPArrangeLvlManager.GetArrange3(this.intUserID);
            int intLevel = (byte) row2["SOInside"];
            int num3 = (byte) row2["SOCHelp"];
            int num4 = (byte) row2["SOOutside"];
            int num5 = (byte) row2["SOAll"];
            int num6 = (byte) row2["SDOneInside"];
            int num7 = (byte) row2["SDOneOutside"];
            int num8 = (byte) row2["SDOne"];
            int num9 = (byte) row2["SDArea"];
            int num10 = (int) row2["SOInsideP"];
            int num11 = (int) row2["SOCHelpP"];
            int num12 = (int) row2["SOOutsideP"];
            int num13 = (int) row2["SOAllP"];
            int num14 = (int) row2["SDOneInsideP"];
            int num15 = (int) row2["SDOneOutsideP"];
            int num16 = (int) row2["SDOneP"];
            int num17 = (int) row2["SDAreaP"];
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>内线强攻</td><td>", intLevel, "</td><td>", num10, "/", BTPArrangeLvlManager.Get3ArrangeNeed(intLevel), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>中锋策应</td><td>", num3, "</td><td>", num11, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num3), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>外线投篮</td><td>", num4, "</td><td>", num12, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num4), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>整体进攻</td><td>", num5, "</td><td>", num13, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num5), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人内线</td><td>", num6, "</td><td>", num14, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num6), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人外线</td><td>", num7, "</td><td>", num15, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num7), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人防守</td><td>", num8, "</td><td>", num16, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num8), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>区域联防</td><td>", num9, "</td><td>", num17, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num9), "</td><td></td></tr>" });
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
                int intRookieOpIndex = AccountItem.RookieOpIndex(this.intUserID);
                if (intRookieOpIndex != 5)
                {
                    base.Response.Write("<script>window.top.location=\"" + StringItem.GetRookieURL(intRookieOpIndex) + "\";</script>");
                }
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intClubID = (int) onlineRowByUserID["ClubID3"];
                this.intType = 1;
                this.tblSelectArrange.Visible = false;
                this.tblSetArrange.Visible = false;
                this.tblArrangeLvl.Visible = false;
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "new/b6.gif";
                this.tblSetArrange.Visible = true;
                this.strPageLoad = "PageLoad();";
                this.Assign();
                this.SetArrScript();
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }

        private void SetArrScript()
        {
            this.strAssScript = "var pos_c = false;\nvar pos_f = false;\nvar pos_g = false;\nvar down = false;\nvar max_z_index = 100;\nvar saveobj = false;\nvar LogObj = null;\nvar player_pos = new Array();\n" + this.strPlayerPosArray + "\nvar position = new Array();\nvar cobj = document.getElementById(\"c\");\nposition[\"c\"] = getpos(cobj);\nvar fobj = document.getElementById(\"f\");\nposition[\"f\"] = getpos(fobj);\n\nvar gobj = document.getElementById(\"g\");\nposition[\"g\"] = getpos(gobj);\n";
        }

        private void SetList()
        {
            this.strList = "";
            DataRow row = BTPArrangeLvlManager.GetArrange3(this.intUserID);
            int intLevel = (byte) row["SOInside"];
            int num2 = (byte) row["SOCHelp"];
            int num3 = (byte) row["SOOutside"];
            int num4 = (byte) row["SOAll"];
            int num5 = (byte) row["SDOneInside"];
            int num6 = (byte) row["SDOneOutside"];
            int num7 = (byte) row["SDOne"];
            int num8 = (byte) row["SDArea"];
            int num9 = (int) row["SOInsideP"];
            int num10 = (int) row["SOCHelpP"];
            int num11 = (int) row["SOOutsideP"];
            int num12 = (int) row["SOAllP"];
            int num13 = (int) row["SDOneInsideP"];
            int num14 = (int) row["SDOneOutsideP"];
            int num15 = (int) row["SDOneP"];
            int num16 = (int) row["SDAreaP"];
            object strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>内线强攻</td><td>", intLevel, "</td><td>", num9, "/", BTPArrangeLvlManager.Get3ArrangeNeed(intLevel), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>中锋策应</td><td>", num2, "</td><td>", num10, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num2), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>外线投篮</td><td>", num3, "</td><td>", num11, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num3), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#00cc00'>进攻</font></td><td>整体进攻</td><td>", num4, "</td><td>", num12, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num4), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人内线</td><td>", num5, "</td><td>", num13, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num5), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人外线</td><td>", num6, "</td><td>", num14, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num6), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>盯人防守</td><td>", num7, "</td><td>", num15, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num7), "</td><td></td></tr>" });
            strList = this.strList;
            this.strList = string.Concat(new object[] { strList, "<tr class='BarContent' onmouseover=\"this.style.backgroundColor='#FBE2D4'\" onmouseout=\"this.style.backgroundColor=''\"><td></td><td height='25'><font color='#FF0000'>防守</font></td><td>区域联防</td><td>", num8, "</td><td>", num16, "/", BTPArrangeLvlManager.Get3ArrangeNeed(num8), "</td><td></td></tr>" });
        }
    }
}


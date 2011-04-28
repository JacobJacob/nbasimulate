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

    public class SArrange : Page
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
        private int intDefMis;
        private int intOffMis;
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
        protected TextBox tbName;

        private void Assign()
        {
            if (!this.Page.IsPostBack)
            {
                DataRow arrByCategory = BTPArrange3Manager.GetArrByCategory(this.intClubID, this.intType);
                string str = arrByCategory["Name"].ToString().Trim();
                this.longCID = (long) arrByCategory["CID"];
                this.longFID = (long) arrByCategory["FID"];
                this.longGID = (long) arrByCategory["GID"];
                int num = (byte) arrByCategory["Offense"];
                int num2 = (byte) arrByCategory["Defense"];
                int num3 = (byte) arrByCategory["OffHard"];
                int num4 = (byte) arrByCategory["DefHard"];
                this.intOffMis = num;
                this.intDefMis = num2;
                this.strAssignBC = string.Concat(new object[] { "document.all.imgOffBC.src='", SessionItem.GetImageURL(), "Match/Street/Off", num, ".gif';document.all.imgDefBC.src='", SessionItem.GetImageURL(), "Match/Street/Def", num2, "_", num4, ".gif';" });
                this.tbName.Text = str;
                DataView hardDataView = MatchItem.GetHardDataView();
                this.sOffHard.DataSource = hardDataView;
                this.sOffHard.DataTextField = "Name";
                this.sOffHard.DataValueField = "Index";
                this.sOffHard.DataBind();
                DataView view2 = MatchItem.GetHardDataView();
                this.sDefHard.DataSource = view2;
                this.sDefHard.DataTextField = "Name";
                this.sDefHard.DataValueField = "Index";
                this.sDefHard.DataBind();
                this.sOffHard.SelectedValue = num3.ToString();
                this.sDefHard.SelectedValue = num4.ToString();
                switch (num)
                {
                    case 1:
                        this.rbOff1.Checked = true;
                        this.rbOff2.Checked = false;
                        this.rbOff3.Checked = false;
                        this.rbOff4.Checked = false;
                        break;

                    case 2:
                        this.rbOff1.Checked = false;
                        this.rbOff2.Checked = true;
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
            int num24 = 200;
            int num25 = 20;
            foreach (DataRow row2 in BTPPlayer3Manager.GetArrPlayerTable(this.intClubID).Rows)
            {
                long num5 = (long) row2["PlayerID"];
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
                DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intClubID);
                int num = (int) clubRowByClubID["Arrange1"];
                int num2 = (int) clubRowByClubID["Arrange2"];
                int num3 = (int) clubRowByClubID["Arrange3"];
                int num4 = (int) clubRowByClubID["Arrange4"];
                int num5 = (int) clubRowByClubID["Arrange5"];
                DataView view = new DataView(BTPArrange3Manager.GetArrTableByClubID(this.intClubID));
                this.ddlArrange1.DataSource = view;
                this.ddlArrange1.DataTextField = "Name";
                this.ddlArrange1.DataValueField = "Arrange3ID";
                this.ddlArrange1.DataBind();
                this.ddlArrange2.DataSource = view;
                this.ddlArrange2.DataTextField = "Name";
                this.ddlArrange2.DataValueField = "Arrange3ID";
                this.ddlArrange2.DataBind();
                this.ddlArrange3.DataSource = view;
                this.ddlArrange3.DataTextField = "Name";
                this.ddlArrange3.DataValueField = "Arrange3ID";
                this.ddlArrange3.DataBind();
                this.ddlArrange4.DataSource = view;
                this.ddlArrange4.DataTextField = "Name";
                this.ddlArrange4.DataValueField = "Arrange3ID";
                this.ddlArrange4.DataBind();
                this.ddlArrange5.DataSource = view;
                this.ddlArrange5.DataTextField = "Name";
                this.ddlArrange5.DataValueField = "Arrange3ID";
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
            string str;
            try
            {
                string validWords = StringItem.GetValidWords(this.tbName.Text);
                if (StringItem.IsValidContent(validWords, 2, 20))
                {
                    int num4;
                    int num5;
                    long longCID = Convert.ToInt64(this.form_c.Value);
                    long longFID = Convert.ToInt64(this.form_f.Value);
                    long longGID = Convert.ToInt64(this.form_g.Value);
                    if (this.rbOff1.Checked)
                    {
                        num4 = 1;
                    }
                    else if (this.rbOff2.Checked)
                    {
                        num4 = 2;
                    }
                    else if (this.rbOff3.Checked)
                    {
                        num4 = 3;
                    }
                    else
                    {
                        num4 = 4;
                    }
                    if (this.rbDef1.Checked)
                    {
                        num5 = 1;
                    }
                    else if (this.rbDef2.Checked)
                    {
                        num5 = 2;
                    }
                    else if (this.rbDef3.Checked)
                    {
                        num5 = 3;
                    }
                    else
                    {
                        num5 = 4;
                    }
                    int intOffHard = Convert.ToInt16(this.sOffHard.SelectedItem.Value);
                    int intDefHard = Convert.ToInt16(this.sDefHard.SelectedItem.Value);
                    BTPArrange3Manager.SetArrange(this.intClubID, validWords, this.intType, longCID, longFID, longGID, intOffHard, intDefHard, num4, num5);
                    Cuter cuter = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID]));
                    if ((cuter.GetIndex("0") == 0) && ((this.intOffMis != num4) || (this.intDefMis != num5)))
                    {
                        cuter.SetCuter(0, "1");
                        string strAdvanceOp = cuter.GetCuter();
                        BTPAccountManager.UpdateAdvanceOp(this.intUserID, strAdvanceOp);
                        this.Session["Advance" + this.intUserID] = strAdvanceOp;
                        BTPAccountManager.AddMoneyWithFinance(0x2710, this.intUserID, 3, "完成高级新手任务一奖励球队资金。");
                    }
                    str = "Report.aspx?Parameter=71!Type." + this.intType;
                }
                else
                {
                    str = "Report.aspx?Parameter=73!Type." + this.intType;
                }
            }
            catch (Exception exception)
            {
                exception.ToString();
                str = "Report.aspx?Parameter=70";
            }
            base.Response.Redirect(str);
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
            intUserIDA = (int) SessionItem.GetRequest("UserID", 0);
            intClubIDA = (int) SessionItem.GetRequest("ClubID", 0);
            SqlDataReader reader = BTPToolLinkManager.CheckClubLink(this.intUserID, intClubIDA, 3);
            bool flag = false;
            while (reader.Read())
            {
                int num = (byte) reader["Category"];
                if (num == 1)
                {
                    flag = true;
                }
            }
            reader.Close();
            if (flag)
            {
                this.intUserID = intUserIDA;
            }
            DataRow row = BTPArrangeLvlManager.GetArrange3(this.intUserID);
            int intLevel = (byte) row["SOInside"];
            int num3 = (byte) row["SOCHelp"];
            int num4 = (byte) row["SOOutside"];
            int num5 = (byte) row["SOAll"];
            int num6 = (byte) row["SDOneInside"];
            int num7 = (byte) row["SDOneOutside"];
            int num8 = (byte) row["SDOne"];
            int num9 = (byte) row["SDArea"];
            int num10 = (int) row["SOInsideP"];
            int num11 = (int) row["SOCHelpP"];
            int num12 = (int) row["SOOutsideP"];
            int num13 = (int) row["SOAllP"];
            int num14 = (int) row["SDOneInsideP"];
            int num15 = (int) row["SDOneOutsideP"];
            int num16 = (int) row["SDOneP"];
            int num17 = (int) row["SDAreaP"];
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
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString();
                this.intClubID = (int) onlineRowByUserID["ClubID3"];
                this.intType = (int) SessionItem.GetRequest("Type", 0);
                this.tblSelectArrange.Visible = false;
                this.tblSetArrange.Visible = false;
                this.tblArrangeLvl.Visible = false;
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.btnSet.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                switch (this.intType)
                {
                    case 0:
                        this.strPageIntro = "<ul><li class='qian1'>配置阵容</li><li class='qian2a'><a href='SArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='SArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='SArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='SArrange.aspx?Type=4'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblSelectArrange.Visible = true;
                        this.AssignArrange();
                        break;

                    case 1:
                        this.strPageIntro = "<ul><li class='qian1a'><a href='SArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2'>阵容一</li><li class='qian2a'><a href='SArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='SArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='SArrange.aspx?Type=4'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblSetArrange.Visible = true;
                        this.strPageLoad = "PageLoad();";
                        this.Assign();
                        this.SetArrScript();
                        break;

                    case 2:
                        this.strPageIntro = "<ul><li class='qian1a'><a href='SArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='SArrange.aspx?Type=1'>阵容一</a></li><li class='qian2'>阵容二</li><li class='qian2a'><a href='SArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='SArrange.aspx?Type=4'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblSetArrange.Visible = true;
                        this.strPageLoad = "PageLoad();";
                        this.Assign();
                        this.SetArrScript();
                        break;

                    case 3:
                        this.strPageIntro = "<ul><li class='qian1a'><a href='SArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='SArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='SArrange.aspx?Type=2'>阵容二</a></li><li class='qian2'>阵容三</li><li class='qian2a'><a href='SArrange.aspx?Type=4'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblSetArrange.Visible = true;
                        this.strPageLoad = "PageLoad();";
                        this.Assign();
                        this.SetArrScript();
                        break;

                    case 4:
                        this.strPageIntro = "<ul><li class='qian1a'><a href='SArrange.aspx?Type=0'>配置阵容</a></li><li class='qian2a'><a href='SArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='SArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='SArrange.aspx?Type=3'>阵容三</a></li><li class='qian2'>战术等级</li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblArrangeLvl.Visible = true;
                        this.SetList();
                        break;

                    case 5:
                    {
                        this.strPageIntro = "<img src='Images/MenuCard/SArrange/SArrange_06.GIF' border='0' height='24' width='75'><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
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
                        this.strPageIntro = "<ul><li class='qian1'>配置阵容</li><li class='qian2a'><a href='SArrange.aspx?Type=1'>阵容一</a></li><li class='qian2a'><a href='SArrange.aspx?Type=2'>阵容二</a></li><li class='qian2a'><a href='SArrange.aspx?Type=3'>阵容三</a></li><li class='qian2a'><a href='SArrange.aspx?Type=4'>战术等级</a></li></ul><a style='cursor:hand;' onclick=\"javascript:NewHelpWin('03');\"><img src='" + SessionItem.GetImageURL() + "MenuCard/Help.GIF' border='0' height='24' width='19'></a>";
                        this.tblSelectArrange.Visible = true;
                        break;
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            string s = "<script language=\"javascript\">parent.Right.location=\"ShowClub.aspx?UserID=" + this.intUserID + "&Type=3\"</script>";
            base.Response.Write(s);
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


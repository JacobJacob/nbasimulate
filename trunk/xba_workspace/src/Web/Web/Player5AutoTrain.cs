namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Player5AutoTrain : Page
    {
        protected ImageButton btnOK;
        protected CheckBox cbBlock;
        protected CheckBox cbDribble;
        protected CheckBox cbJump;
        protected CheckBox cbPass;
        protected CheckBox cbPoint3;
        protected CheckBox cbRebound;
        protected CheckBox cbShot;
        protected CheckBox cbSpeed;
        protected CheckBox cbStamina;
        protected CheckBox cbSteal;
        protected CheckBox cbStrength;
        protected HtmlInputHidden CheckedCount;
        protected DropDownList ddlBlock;
        protected DropDownList ddlDribble;
        protected DropDownList ddlJump;
        protected DropDownList ddlPass;
        protected DropDownList ddlPoint3;
        protected DropDownList ddlRebound;
        protected DropDownList ddlShot;
        protected DropDownList ddlSpeed;
        protected DropDownList ddlStamina;
        protected DropDownList ddlSteal;
        protected DropDownList ddlStrength;
        private int intCategory;
        private int intClubID5;
        private int intUserID;
        private long lngPlayerID;
        public string strImgReturn = "";
        public string strModel = "";
        public string strTitle = "";
        public string strtrTitle = "";
        protected HtmlTable tblModel1;
        protected HtmlTable tblModel2;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            string str = SessionItem.GetRequest("Type", 1).ToString();
            this.lngPlayerID = (long) SessionItem.GetRequest("PlayerID", 3);
            if (str == "Model1")
            {
                int intSpeedAdd = Convert.ToInt32(this.ddlSpeed.SelectedValue);
                int intJumpAdd = Convert.ToInt32(this.ddlJump.SelectedValue);
                int intStrengthAdd = Convert.ToInt32(this.ddlStrength.SelectedValue);
                int intStaminaAdd = Convert.ToInt32(this.ddlStamina.SelectedValue);
                int intShotAdd = Convert.ToInt32(this.ddlShot.SelectedValue);
                int num6 = Convert.ToInt32(this.ddlPoint3.SelectedValue);
                int intDribbleAdd = Convert.ToInt32(this.ddlDribble.SelectedValue);
                int intPassAdd = Convert.ToInt32(this.ddlPass.SelectedValue);
                int intReboundAdd = Convert.ToInt32(this.ddlRebound.SelectedValue);
                int intStealAdd = Convert.ToInt32(this.ddlSteal.SelectedValue);
                int intBlockAdd = Convert.ToInt32(this.ddlBlock.SelectedValue);
                int num12 = (((((((((intSpeedAdd + intJumpAdd) + intStrengthAdd) + intStaminaAdd) + intShotAdd) + num6) + intDribbleAdd) + intPassAdd) + intReboundAdd) + intStealAdd) + intBlockAdd;
                if (num12 == 0x1a)
                {
                    int num13 = BTPToolLinkManager.AddPlayer5ByPlayerID(this.lngPlayerID, this.intClubID5, intSpeedAdd, intJumpAdd, intStrengthAdd, intStaminaAdd, intShotAdd, num6, intDribbleAdd, intPassAdd, intReboundAdd, intStealAdd, intBlockAdd);
                    base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=TIMEHOUSE5&Status=", num13, "&PlayerID=", this.lngPlayerID }));
                }
                else
                {
                    base.Response.Redirect("SecretaryPage.aspx?Type=TIMEHOUSE5&Status=-1&PlayerID=" + this.lngPlayerID);
                }
            }
            else
            {
                int[] numArray = new int[11];
                int num14 = Convert.ToInt32(this.CheckedCount.Value);
                int num15 = 0x1a / num14;
                int num16 = 0x1a % num14;
                int num17 = 0;
                if (this.cbSpeed.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbJump.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbStrength.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbStamina.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbShot.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbPoint3.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbDribble.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbPass.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbRebound.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbSteal.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                if (this.cbBlock.Checked)
                {
                    numArray[num17++] = num15 + ((num16-- > 0) ? 1 : 0);
                }
                else
                {
                    numArray[num17++] = 0;
                }
                int num18 = BTPToolLinkManager.AddPlayer5ByPlayerID(this.lngPlayerID, this.intClubID5, numArray[0], numArray[1], numArray[2], numArray[3], numArray[4], numArray[5], numArray[6], numArray[7], numArray[8], numArray[9], numArray[10]);
                base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=TIMEHOUSE5&Status=", num18, "&PlayerID=", this.lngPlayerID }));
            }
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
                this.intCategory = (int) onlineRowByUserID["Category"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.lngPlayerID = (long) SessionItem.GetRequest("PlayerID", 3);
                if (((this.intCategory != 5) && (this.intCategory != 2)) || (this.lngPlayerID == 0L))
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    if (SessionItem.GetRequest("Type", 1).ToString() == "Model1")
                    {
                        this.tblModel1.Visible = true;
                        this.tblModel2.Visible = false;
                        this.strModel = "<a href='Player5AutoTrain.aspx?PlayerID=" + this.lngPlayerID + "'>标准模式</a>";
                        this.strTitle = "：高级模式";
                        this.strtrTitle = "选择各能力增长的轮数，所有轮数之和须为26";
                    }
                    else
                    {
                        this.tblModel2.Visible = true;
                        this.tblModel1.Visible = false;
                        this.strModel = "<a href='Player5AutoTrain.aspx?Type=Model1&PlayerID=" + this.lngPlayerID + "'>高级模式</a>";
                        this.strtrTitle = "请选择训练项目";
                    }
                    this.strImgReturn = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "button_48.GIF' width='40' height='24' style='CURSOR: hand' onclick='javascript:window.location=\"PlayerCenter.aspx?PlayerType=5&Type=9&PlayerID=", this.lngPlayerID, "\";'>" });
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.SetItem(this.ddlSpeed);
            this.SetItem(this.ddlJump);
            this.SetItem(this.ddlStrength);
            this.SetItem(this.ddlStamina);
            this.SetItem(this.ddlShot);
            this.SetItem(this.ddlPoint3);
            this.SetItem(this.ddlDribble);
            this.SetItem(this.ddlPass);
            this.SetItem(this.ddlRebound);
            this.SetItem(this.ddlSteal);
            this.SetItem(this.ddlBlock);
        }

        private void SetItem(DropDownList ddlItem)
        {
            for (int i = 0; i < 0x1b; i++)
            {
                ListItem item = new ListItem(i.ToString(), i.ToString());
                ddlItem.Items.Add(item);
            }
        }
    }
}


namespace Web
{
    using AjaxPro;
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class Main_I : Page
    {
        private int intCategory;
        private int intUserID;
        public string strCenterURL;
        private string strGuideCode;
        public string strLeftURL;
        public string strSayScript;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            Utility.RegisterTypeForAjax(typeof(Main_P));
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.intCategory = (int) onlineRowByUserID["Category"];
                if (((int) onlineRowByUserID["ClubID3"]) == 0)
                {
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                else
                {
                    int num2 = (int) onlineRowByUserID["ClubID5"];
                    this.strGuideCode = onlineRowByUserID["GuideCode"].ToString().Trim();
                    DataRow gameRow = BTPGameManager.GetGameRow();
                    int intRound = (int) gameRow["Turn"];
                    int num4 = (int) gameRow["Days"];
                    int index = new Cuter(Convert.ToString(this.Session["Advance" + this.intUserID])).GetIndex("0");
                    if ((((intRound > 0x1a) && (num4 > 0x1c)) && ((DateTime.Now.Hour >= Global.intStartUpdate) && (DateTime.Now.Hour < 10))) && (this.intCategory == 5))
                    {
                        if (BTPDevMatchManager.GetDevMRowByClubIDRound(num2, intRound) == null)
                        {
                            this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&Page=1&UserID=" + this.intUserID;
                        }
                        else
                        {
                            this.strCenterURL = "Report.aspx?Parameter=1006b";
                        }
                    }
                    else if ((((intRound == 14) && (num4 == 15)) && ((DateTime.Now.Hour >= Global.intStartUpdate) && (DateTime.Now.Hour < 10))) && (this.intCategory == 5))
                    {
                        if (BTPDevMatchManager.GetDevMRowByClubIDRound(num2, intRound) == null)
                        {
                            this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&Page=1&UserID=" + this.intUserID;
                        }
                        else
                        {
                            this.strCenterURL = "Report.aspx?Parameter=1006a";
                        }
                    }
                    else if ((((intRound == 1) && (num4 == 1)) && ((DateTime.Now.Hour >= Global.intStartUpdate) && (DateTime.Now.Hour < 10))) && (this.intCategory == 5))
                    {
                        if (BTPDevMatchManager.GetDevMRowByClubIDRound(num2, intRound) == null)
                        {
                            this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&Page=1&UserID=" + this.intUserID;
                        }
                        else
                        {
                            this.strCenterURL = "Report.aspx?Parameter=1006c";
                        }
                    }
                    else if (((this.intCategory == 5) && (DateTime.Now >= DateTime.Today.AddHours(9.0))) && (DateTime.Now <= DateTime.Today.AddHours(10.0)))
                    {
                        if (index != -1)
                        {
                            this.strCenterURL = "MissionCenter.aspx";
                        }
                        else
                        {
                            DataRow devMRowByClubIDRound = BTPDevMatchManager.GetDevMRowByClubIDRound(num2, intRound - 1);
                            if (devMRowByClubIDRound == null)
                            {
                                this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&Page=1&UserID=" + this.intUserID;
                            }
                            else
                            {
                                int num6 = (int) devMRowByClubIDRound["DevMatchID"];
                                int num7 = (int) devMRowByClubIDRound["ClubHID"];
                                int num8 = (int) devMRowByClubIDRound["ClubAID"];
                                if ((num7 == 0) || (num8 == 0))
                                {
                                    this.strCenterURL = "TopCenter.aspx?Type=LIST&Page=1&UserID=" + this.intUserID;
                                }
                                else
                                {
                                    this.strCenterURL = string.Concat(new object[] { "TopCenter.aspx?Type=INSTANT&UserID=", this.intUserID, "&Status=2&Tag=", num6, "&A=", num7, "&B=", num8 });
                                }
                            }
                        }
                    }
                    else if (index != -1)
                    {
                        this.strCenterURL = "MissionCenter.aspx";
                    }
                    else if (this.intCategory == 5)
                    {
                        if (BTPDevMatchManager.GetDevMatchTableByClubID(num2) == null)
                        {
                            this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&UserID=" + this.intUserID;
                        }
                        else
                        {
                            this.strCenterURL = "VTList.aspx?UserID=" + this.intUserID;
                        }
                    }
                    else if (this.intCategory == 1)
                    {
                        this.strCenterURL = "TopCenter.aspx?Type=NEW&UserID=" + this.intUserID;
                    }
                    else
                    {
                        this.strCenterURL = "TopCenter.aspx?Type=DEVNEW&UserID=" + this.intUserID;
                    }
                    if (index != -1)
                    {
                        this.strLeftURL = "MissionLeft.aspx";
                    }
                    else
                    {
                        this.strLeftURL = "TopLeft.aspx?UserID=" + this.intUserID;
                    }
                    this.strSayScript = "";
                    this.InitializeComponent();
                    base.OnInit(e);
                }
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


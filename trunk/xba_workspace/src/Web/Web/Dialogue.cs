namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class Dialogue : Page
    {
        protected ImageButton btnOK;
        protected DropDownList ddlType;
        private int intSalary;
        private int intUserID;
        private long longPlayerID;
        public string strCancelBtn;
        public string strCategory;
        public string strddlType;
        private string strDialogueType = "";
        public string strFace;
        private string strNickName;
        public string strNumber;
        private string strPlayerName;
        public string strSay;
        public string strShirt;

        private void btnOK_Click_Awareness(object sender, ImageClickEventArgs e)
        {
            int intType = Convert.ToInt32(this.ddlType.SelectedValue);
            BTPPlayer5Manager.SetAwarenessTrain(this.longPlayerID, intType);
            base.Response.Redirect("Dialogue.aspx?Type=AwarenessDone&PlayerID=" + this.longPlayerID);
        }

        private void btnOK_Click_DialogueDone(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("TrainPlayerCenter.aspx?UserID=" + this.intUserID + "&Type=5");
        }

        private void btnOK_Click_Holiday(object sender, ImageClickEventArgs e)
        {
            BTPPlayer5Manager.SetPlayer5Holiday(this.longPlayerID, this.strPlayerName, this.intSalary, this.intUserID);
            base.Response.Redirect("TrainPlayerCenter.aspx?UserID=" + this.intUserID + "&Type=5");
        }

        private void btnOK_Click_None(object sender, ImageClickEventArgs e)
        {
            base.Response.Redirect("TrainPlayerCenter.aspx?UserID=" + this.intUserID + "&Type=5");
        }

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.ddlType.Visible = false;
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.strCategory = ServerParameter.intGameCategory.ToString().Trim();
                DataRow onlineRowByUserID = DTOnlineManager.GetOnlineRowByUserID(this.intUserID);
                this.strNickName = onlineRowByUserID["NickName"].ToString().Trim();
                string strClubName = onlineRowByUserID["ClubName3"].ToString().Trim();
                int intClubID = (int) onlineRowByUserID["ClubID5"];
                int num2 = (int) onlineRowByUserID["ClubID3"];
                bool blnSex = (bool) onlineRowByUserID["Sex"];
                SecretaryItem si = new SecretaryItem(this.intUserID, blnSex);
                this.longPlayerID = SessionItem.GetRequest("PlayerID", 3);
                this.strDialogueType = SessionItem.GetRequest("Type", 1).ToString().Trim();
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(this.longPlayerID);
                this.strPlayerName = playerRowByPlayerID["Name"].ToString().Trim();
                this.strFace = playerRowByPlayerID["Face"].ToString().Trim();
                int num3 = (int) playerRowByPlayerID["ClubID"];
                int num4 = (byte) playerRowByPlayerID["TrainAType"];
                if ((num3 != num2) && (num3 != intClubID))
                {
                    base.Response.Redirect("Report.aspx?Parameter=107");
                }
                else
                {
                    int num5;
                    bool flag2;
                    DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(num3);
                    if (clubRowByClubID == null)
                    {
                        num5 = 1;
                    }
                    else
                    {
                        num5 = (byte) clubRowByClubID["Shirt"];
                    }
                    int num6 = (byte) playerRowByPlayerID["Number"];
                    this.strShirt = (0x4e20 + num5) + "";
                    if (num5 > 15)
                    {
                        this.strNumber = (0x526c + num6) + "";
                    }
                    else
                    {
                        this.strNumber = (0x5208 + num6) + "";
                    }
                    long longPlayerID = (long) playerRowByPlayerID["PlayerID"];
                    int intStatus = (byte) playerRowByPlayerID["Status"];
                    int intAge = (byte) playerRowByPlayerID["Age"];
                    int intHappy = (byte) playerRowByPlayerID["Happy"];
                    int intPower = (byte) playerRowByPlayerID["Power"];
                    this.intSalary = (int) playerRowByPlayerID["Salary"];
                    if (BTPArrange5Manager.GetCheckArrange5(intClubID, this.longPlayerID) == null)
                    {
                        flag2 = false;
                    }
                    else
                    {
                        flag2 = true;
                    }
                    this.strSay = PlayerItem.GetPlayerSay(longPlayerID, this.strPlayerName, intAge, strClubName, this.strNickName, si, blnSex, intStatus, flag2, intHappy, intPower);
                    this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                    if (this.strDialogueType != "AwarenessDone")
                    {
                        if ((intHappy >= 60) && (intStatus == 1))
                        {
                            this.ddlType.Visible = true;
                            if (!base.IsPostBack && (num4 != 0))
                            {
                                this.ddlType.SelectedValue = num4.ToString();
                            }
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Awareness);
                            this.strCancelBtn = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' border='0' style='cursor:hand;' onclick='window.history.back();'>";
                        }
                        else if ((intStatus == 1) && !flag2)
                        {
                            this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_24.gif";
                            this.strCancelBtn = "<img src='" + SessionItem.GetImageURL() + "button_13.GIF' width='40' height='24' border='0' style='cursor:hand;' onclick='window.history.back();'>";
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_Holiday);
                        }
                        else
                        {
                            this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_None);
                        }
                    }
                    else
                    {
                        switch (RandomItem.rnd.Next(1, 6))
                        {
                            case 1:
                                this.strSay = this.strNickName + "，与您的谈话很愉快！";
                                break;

                            case 2:
                                this.strSay = this.strNickName + "，我一定不会辜负您的期望的！";
                                break;

                            case 3:
                                this.strSay = this.strNickName + "，谢谢你的指点，我会加强这方面的意识！";
                                break;

                            case 4:
                                this.strSay = this.strNickName + "，与你聊天总是特别投缘，咱们做朋友吧！";
                                break;

                            case 5:
                                this.strSay = this.strNickName + "，你的要求我将铭记在心，绝不会让你失望的！";
                                break;

                            default:
                                this.strSay = this.strNickName + "，与您的谈话很愉快！";
                                break;
                        }
                        this.btnOK.Click += new ImageClickEventHandler(this.btnOK_Click_DialogueDone);
                    }
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


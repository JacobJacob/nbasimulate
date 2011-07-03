namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Web.DBData;
    using Web.Helper;

    public class SetAutoTrain : Page
    {
        protected ImageButton btnOK;
        private int intCategory;
        private int intClubID3;
        private int intClubID5;
        private int intUserID;
        protected TextBox tbTrainHours;
        protected TextBox tbTrainHoursDev;

        private void btnOK_Click(object sender, ImageClickEventArgs e)
        {
            if (StringItem.IsNumber(this.tbTrainHours.Text.Trim()))
            {
                int num = Convert.ToInt32(this.tbTrainHours.Text);
                int num2 = Convert.ToInt32(this.tbTrainHoursDev.Text);
                base.Response.Redirect(string.Concat(new object[] { "SecretaryPage.aspx?Type=SETAUTOTRAIN&Hours=", num, "&DevH=", num2 }));
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
                this.intClubID3 = (int) onlineRowByUserID["ClubID3"];
                this.intClubID5 = (int) onlineRowByUserID["ClubID5"];
                this.btnOK.ImageUrl = SessionItem.GetImageURL() + "button_11.gif";
                this.tbTrainHours.Attributes["onkeyup"] = "SetPayCoin(this);";
                this.tbTrainHoursDev.Attributes["onkeyup"] = "SetDevPayCoin(this)";
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


namespace Web
{
    using System;
    using System.Data;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class MissionLeft : Page
    {
        private int intUserID;
        public string strMissionList;

        private void InitializeComponent()
        {
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
                DataRow accountRowByUserID = BTPAccountManager.GetAccountRowByUserID(this.intUserID);
                string cuter = "";
                if (accountRowByUserID != null)
                {
                    cuter = accountRowByUserID["AdvanceOp"].ToString().Trim();
                }
                int index = new Cuter(cuter).GetIndex("0");
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                switch (index)
                {
                    case 0:
                        str6 = "";
                        str2 = "<br>";
                        str3 = "<br>";
                        str4 = "<br>";
                        str5 = "<br>";
                        str7 = "<br>";
                        break;

                    case 1:
                        str6 = "<span style=\"color:#333333;\">";
                        str2 = "</span><br>";
                        str3 = "<br>";
                        str4 = "<br>";
                        str5 = "<br>";
                        str7 = "<br>";
                        break;

                    case 2:
                        str6 = "<span style=\"color:#333333;\">";
                        str2 = "<br>";
                        str3 = "</span><br>";
                        str4 = "<br>";
                        str5 = "<br>";
                        str7 = "<br>";
                        break;

                    case 3:
                        str6 = "<span style=\"color:#333333;\">";
                        str2 = "<br>";
                        str3 = "<br>";
                        str4 = "</span><br>";
                        str5 = "<br>";
                        str7 = "<br>";
                        break;

                    case 4:
                    case 5:
                        str6 = "<span style=\"color:#333333;\">";
                        str2 = "<br>";
                        str3 = "<br>";
                        str4 = "<br>";
                        str5 = "</span><br>";
                        str7 = "<br>";
                        break;

                    default:
                        if (index == -1)
                        {
                            str6 = "<span style=\"color:#333333;\">";
                            str2 = "<br>";
                            str3 = "<br>";
                            str4 = "<br>";
                            str5 = "<br>";
                            str7 = "</span><br>";
                        }
                        else
                        {
                            str6 = "<span style=\"color:#333333;\">";
                            str2 = "<br>";
                            str3 = "<br>";
                            str4 = "<br>";
                            str5 = "<br>";
                            str7 = "</span><br>";
                        }
                        break;
                }
                this.strMissionList = str6 + "任务一：运筹帷幄，决胜千里" + str2 + "任务二：纸上谈兵，空口无凭" + str3 + "任务三：用人不疑，疑人不用" + str4 + "任务四：忠臣良将，各司其职" + str5 + "任务五：康复理疗，振奋士气" + str7;
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


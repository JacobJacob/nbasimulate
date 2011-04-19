namespace Web
{
    using LoginParameter;
    using System;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBData;
    using Web.Helper;

    public class AlterFace : Page
    {
        private int intGameCategory;
        private int intUserID;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(0);
            if (this.intUserID < 0)
            {
                base.Response.Redirect("Report.aspx?Parameter=12");
            }
            else
            {
                this.intGameCategory = (int) SessionItem.GetRequest("GameCategory", 0);
                SqlDataReader userRowByUserID = ROOTUserManager.GetUserRowByUserID(this.intUserID);
                if (userRowByUserID.Read())
                {
                    string path = userRowByUserID["DiskURL"].ToString().Trim();
                    bool flag = (bool) userRowByUserID["Sex"];
                    string strFace = userRowByUserID["Face"].ToString().Trim();
                    string str3 = "Boy";
                    if (flag)
                    {
                        str3 = "Girl";
                    }
                    string strFacePath = base.Server.MapPath("Images/Face/" + str3 + "/");
                    path = base.Server.MapPath(path);
                    FaceItem.CreateFace(strFacePath, path, strFace);
                    userRowByUserID.Close();
                    base.Response.Redirect(DBLogin.URLString(this.intGameCategory) + "AlterOtherInfo.aspx?Type=SEXFACE&Kind=YES");
                }
                else
                {
                    userRowByUserID.Close();
                    base.Response.Redirect("Report.aspx?Parameter=3");
                }
                this.InitializeComponent();
                base.OnInit(e);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
        }
    }
}


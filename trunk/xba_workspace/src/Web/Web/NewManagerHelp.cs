namespace Web
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Web.UI;
    using Web.DBConnection;
    using Web.DBData;
    using Web.Helper;

    public class NewManagerHelp : Page
    {
        private int intUserID;
        private string strDeptTag;

        private void InitializeComponent()
        {
            base.Load += new EventHandler(this.Page_Load);
        }

        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }

        private void Page_Load(object sender, EventArgs e)
        {
            this.intUserID = SessionItem.CheckLogin(1);
            if (this.intUserID < 0)
            {
                if (!ServerParameter.blnUseServer)
                {
                    SessionItem.JumpToRequestCookiePage(1);
                }
                else
                {
                    string str = SessionItem.GetDomainUserID().ToString().Trim();
                    if (str != string.Empty)
                    {
                        this.intUserID = Convert.ToInt32(str);
                        if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) != null)
                        {
                            DataRow reader = ROOTUserManager.Get40UserRowByUserID(this.intUserID);
                            if (reader != null)
                            {
                                this.strDeptTag = reader["DeptTag"].ToString().Trim();
                            }
                            //reader.Close();
                            string str2 = Convert.ToString((int) (ServerParameter.intGameCategory + 0x7d0)).Trim();
                            if (this.strDeptTag.IndexOf(str2) == -1)
                            {
                                this.strDeptTag = this.strDeptTag + str2 + ",";
                                string commandText = string.Concat(new object[] { "UPDATE Main_User SET DeptTag='", this.strDeptTag, "' WHERE UserID=", this.intUserID });
                                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
                            }
                            base.Response.Redirect("SmartMain.aspx?Type=mmm");
                        }
                    }
                }
            }
            else if (BTPAccountManager.GetAccountRowByUserID(this.intUserID) != null)
            {
                DataRow reader2 = ROOTUserManager.Get40UserRowByUserID(this.intUserID);
                if (reader2 != null)
                {
                    this.strDeptTag = reader2["DeptTag"].ToString().Trim();
                }
                //reader2.Close();
                string str4 = Convert.ToString((int) (ServerParameter.intGameCategory + 0x7d0)).Trim();
                if (this.strDeptTag.IndexOf(str4) == -1)
                {
                    this.strDeptTag = this.strDeptTag + str4 + ",";
                    string str5 = string.Concat(new object[] { "UPDATE Main_User SET DeptTag='", this.strDeptTag, "' WHERE UserID=", this.intUserID });
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, str5);
                }
                base.Response.Redirect("SmartMain.aspx?Type=mmm");
            }
        }
    }
}


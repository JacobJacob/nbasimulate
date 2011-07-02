namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class AdminUserManager
    {
        public static DataTable GetInfoByUserNamePassword(string strUserName, string strPassword)
        {
            string commandText = "SELECT * FROM Main_AdminUser WHERE UserName=@UserName AND Password=@Password";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@Password", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }
    }
}


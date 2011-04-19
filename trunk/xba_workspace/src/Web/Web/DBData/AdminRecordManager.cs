namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class AdminRecordManager
    {
        public static void SetAdminRecord(int intUserID, string strOpIndex, string strOperation, string strOpIP)
        {
            string commandText = "INSERT INTO Main_AdminRecord (UserID,OpIndex,Operation,OpIP)VALUES(@UserID,@OpIndex,@Operation,@OpIP)";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@OpIndex", SqlDbType.NChar, 20), new SqlParameter("@Operation", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@OpIP", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strOpIndex;
            commandParameters[2].Value = strOperation;
            commandParameters[3].Value = strOpIP;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPLogManager
    {
        public static void DeleteLogByTime(string strTime)
        {
            string commandText = "Exec NewBTP.dbo.DeleteLogByTime @strTime";
            new SqlParameter[] { new SqlParameter("@strTime", SqlDbType.DateTime, 8) }[0].Value = strTime;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


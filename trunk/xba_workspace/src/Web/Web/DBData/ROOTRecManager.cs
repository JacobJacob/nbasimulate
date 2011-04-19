namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTRecManager
    {
        public static void AddOrderRecord(string strContent)
        {
            string commandText = "INSERT INTO Order_Record (Content)VALUES('" + strContent + "')";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void DeleteRecByTime(string strTime)
        {
            string commandText = "Exec ROOT_Data.dbo.DeleteRecByTime @strTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = Convert.ToDateTime(strTime);
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }
    }
}


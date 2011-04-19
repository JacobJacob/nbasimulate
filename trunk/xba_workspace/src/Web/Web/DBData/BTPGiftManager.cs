namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPGiftManager
    {
        public static SqlDataReader GetGiftTable()
        {
            string commandText = "SELECT * FROM BTP_Gift";
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


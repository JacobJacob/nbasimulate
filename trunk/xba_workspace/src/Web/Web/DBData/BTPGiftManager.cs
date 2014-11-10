namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPGiftManager
    {
        public static DataTable GetGiftTable()
        {
            string commandText = "SELECT * FROM BTP_Gift";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


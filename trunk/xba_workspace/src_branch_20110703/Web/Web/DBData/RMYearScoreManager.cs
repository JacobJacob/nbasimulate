namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class RMYearScoreManager
    {
        public static DataTable GetTopYearScore()
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopYearScore";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


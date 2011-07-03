namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPAnnounceManager
    {
        public static DataTable GetAnnounceTable()
        {
            string commandText = "Exec NewBTP.dbo.GetAnnounceTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAnnounceTableByType()
        {
            string commandText = "Exec NewBTP.dbo.GetAnnounceTableByType";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


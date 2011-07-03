namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPGameScheduleManager
    {
        public static DataTable GetGameScheduleAllTable()
        {
            string commandText = "Exec NewBTP.dbo.GetGameScheduleAllTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGameScheduleDayTable(int intDay)
        {
            string commandText = "Exec NewBTP.dbo.GetGameScheduleDayTable " + intDay;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


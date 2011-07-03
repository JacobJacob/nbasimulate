namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class ROOTPayRecordManager
    {
        public static DataTable GetPayRecordTable(int intYear, int intMonth)
        {
            string commandText = string.Concat(new object[] { "EXEC ROOT_Data.dbo.GetPayRecordTable ", intYear, ",", intMonth });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetPayRecordTableByDay(int intYear, int intMonth, int intDay)
        {
            string commandText = string.Concat(new object[] { "EXEC ROOT_Data.dbo.GetPayRecordTableByDay ", intYear, ",", intMonth, ",", intDay });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


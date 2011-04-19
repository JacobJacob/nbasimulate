namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class RMMonthScoreManager
    {
        public static DataTable GetMonthScoreByMonthIDOrderByPointAndStatus(int intMonthID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetMonthScoreByMonthID ", intMonthID, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetMonthScoreList(int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMonthScoreList " + intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetTopMonthScore()
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopMonthScore";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetStatusByUserIDMonthID(int intUserID, int intMonthID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetStatusByUserIDMonthID ", intUserID, ",", intMonthID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


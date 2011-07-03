namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class RMMatchManager
    {
        public static DataTable GetEndMatches()
        {
            string commandText = "Exec ROOT_Data.dbo.GetEndMatches";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetLastEndMatchRow()
        {
            string commandText = "Exec ROOT_Data.dbo.GetLastEndMatchRow";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetMatchRowByMatchID(int intMatchID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMatchRowByMatchID " + intMatchID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetNextMatchRow()
        {
            string commandText = "Exec ROOT_Data.dbo.GetNextMatchRow";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetRMMatchTable()
        {
            string commandText = "Exec ROOT_Data.dbo.GetRMMatchTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetMatchStatus(int intMatchID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetMatchStatus ", intMatchID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void UpdateRMScore(int intMatchID, int intHomeScore, int intAwayScore, int intYaoScore)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.UpdateRMScore ", intMatchID, ",", intHomeScore, ",", intAwayScore, ",", intYaoScore });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


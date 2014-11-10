namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPPoint3Manager
    {
        public static DataTable GetPoint3MatchTable(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.[GetPoint3MatchTable] @intClubID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetPoint3MatchTopTable()
        {
            string commandText = "Exec NewBTP.dbo.[GetPoint3MatchTop]";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int RegPoint3Match(int intPlayerID, int intFlg)
        {
            string commandText = "Exec NewBTP.dbo.[RegPoint3Match] @intPlayerID, @intFlg";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intPlayerID", SqlDbType.Int, 4), new SqlParameter("@intFlg", SqlDbType.Int, 4) };
            commandParameters[0].Value = intPlayerID;
            commandParameters[1].Value = intFlg;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


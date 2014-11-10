namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPArenaManager
    {
        public static int AddArenaMatch(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.[AddArenaMatch] @intUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int ArenaMemberReg(int userID, int arenaID)
        {
            string commandText = "Exec NewBTP.dbo.[ArenaMemberReg] @UserID, @ArenaID, @RetCode OUTPUT";
            SqlParameter parameter = SqlHelper.CreateOutParam("@RetCode", SqlDbType.Int, 4);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ArenaID", SqlDbType.Int, 4), parameter };
            commandParameters[0].Value = userID;
            commandParameters[1].Value = arenaID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            return SqlHelper.GetIntValue(parameter, 100);
        }

        public static int ArenaUnionReg(int userID, int arenaID)
        {
            string commandText = "Exec NewBTP.dbo.[ArenaUnionReg] @UserID, @ArenaID, @RetCode OUTPUT";
            SqlParameter parameter = SqlHelper.CreateOutParam("@RetCode", SqlDbType.Int, 4);
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ArenaID", SqlDbType.Int, 4), parameter };
            commandParameters[0].Value = userID;
            commandParameters[1].Value = arenaID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            return SqlHelper.GetIntValue(parameter, 100);
        }

        public static DataTable GetArenaMatchTableByPage(int DoCountint, int PageIndex, int PageSize, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetArenaMatch] 0,", PageIndex, ",", PageSize, ",", intClubID });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetArenaMatchTableCount(int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetArenaMatch] 1, 0 , 0,", intClubID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArenaRegTable(int arenaID)
        {
            string commandText = "Exec NewBTP.dbo.GetArenaRegTable @ArenaID ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ArenaID", SqlDbType.Int, 4) };
            commandParameters[0].Value = arenaID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetArenaRow(int arenaID)
        {
            string commandText = "Exec NewBTP.dbo.GetArenaTable @ArenaID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ArenaID", SqlDbType.Int, 4) };
            commandParameters[0].Value = arenaID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetArenaTable()
        {
            string commandText = "Exec NewBTP.dbo.GetArenaTable ";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetArenaMatchEnd(int matchID)
        {
            string commandText = "Exec NewBTP.dbo.SetArenaMatchEnd @intMatchID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = matchID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


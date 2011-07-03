namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPXGroupMatchManager
    {
        public static void AddMatch(int intCategory, int intGroupIndex, int intRound, int intTeamAIndex, int intTeamBIndex, int intClubAID, int intClubBID, string strMatchTime)
        {
            string commandText = "Exec NewBTP.dbo.XGroupAddMatch @intCategory,@intGroupIndex,@intRound,@intTeamAIndex,@intTeamBIndex,@intClubAID,@intClubBID,@strMatchTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intGroupIndex", SqlDbType.Int, 4), new SqlParameter("@intRound", SqlDbType.TinyInt, 1), new SqlParameter("@intTeamAIndex", SqlDbType.TinyInt, 1), new SqlParameter("@intTeamBIndex", SqlDbType.TinyInt, 1), new SqlParameter("@intClubAID", SqlDbType.Int, 4), new SqlParameter("@intClubBID", SqlDbType.Int, 4), new SqlParameter("@strMatchTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = intCategory;
            commandParameters[1].Value = intGroupIndex;
            commandParameters[2].Value = intRound;
            commandParameters[3].Value = intTeamAIndex;
            commandParameters[4].Value = intTeamBIndex;
            commandParameters[5].Value = intClubAID;
            commandParameters[6].Value = intClubBID;
            commandParameters[7].Value = Convert.ToDateTime(strMatchTime);
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void CancelChampionArrange(int intClubID5, int intDevMatchID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "CancelChampionArrange", commandParameters);
        }

        public static void ChampionManage(int intClubID5, int intDevMatchID, string strArrange)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4), new SqlParameter("@Arrange", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            commandParameters[2].Value = strArrange;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "ChampionManage", commandParameters);
        }

        public static DataTable ChampionMatchByTurn(int intClubID, int intRound)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@Round", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intRound;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "ChampionMatchByTurn", commandParameters);
        }

        public static void ChampionOpenbyCIDMID(int intClubID5, int intDevMatchID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "ChampionOpenbyCIDMID", commandParameters);
        }

        public static DataTable GetGroupMatchByCategoryGroupIndex(int intClubID, int intGroupCategory, int intGroupIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXGroupMatchTableByCG ", intClubID, ",", intGroupCategory, ",", intGroupIndex });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupMatchToPlay()
        {
            string commandText = "Exec NewBTP.dbo.GetGroupMatchToPlay";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetXGroupMatchRowByID(int intXGroupMatch)
        {
            string commandText = "Exec NewBTP.dbo.GetXGroupMatchRowByID " + intXGroupMatch;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SendChampionCupB(int intSendID, int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SendID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intSendID;
            commandParameters[1].Value = intUserID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendChampionCupB", commandParameters);
        }

        public static void UpdateResult(int intMatchID, int clubAScore, int clubBScore, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.UpdateXCupResult @intMatchID,@clubAScore,@clubBScore,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intMatchID", SqlDbType.Int, 4), new SqlParameter("@clubAScore", SqlDbType.Int, 4), new SqlParameter("@clubBScore", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intMatchID;
            commandParameters[1].Value = clubAScore;
            commandParameters[2].Value = clubBScore;
            commandParameters[3].Value = strRepURL;
            commandParameters[4].Value = strStasURL;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


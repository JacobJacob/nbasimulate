namespace Web.DBData
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPStarMatchManager
    {
        /**
         *获取全明星赛的球员 
         */

        public static DataTable GetStarPlayersByClubID(int intClubID)
        {
            string commandText = "EXEC NewBTP.dbo.GetStarPlayersByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        /**
         *获取全明星赛阵容 
         */

        public static DataRow GetStarArrange5RowByStarArrange5ID(int intArrange5ID)
        {
            string commandText = "EXEC NewBTP.dbo.GetStarArrange5RowByStarArrange5ID " + intArrange5ID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

 
        public static int GetStarPlayerCount(int intPos, int intZone)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@Zone", SqlDbType.Int, 4), new SqlParameter("@Pos", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = intZone;
            commandParameters[4].Value = intPos;
             return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetStarPlayer", commandParameters);
        }

        public static DataTable GetStarPlayer(int intPos, int intZone, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStarPlayer  0, ", intPage, ",", intPerPage, ",", intZone, ",", intPos });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetOneStarMatchByID(int intStarMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOneStarMatchByID  ", intStarMatchID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetOneStarMatch(int intSeason)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOneStarMatch  ", intSeason});
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int VoteStarPlayer(int intUserID, long intPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.VoteStarPlayer  ", intUserID, ",", intPlayerID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

             public static DataRow GetVoteRecord(int intUserID, int intPos)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetVoteRecord  ", intUserID, ",", intPos });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStarMatchCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetStarMatch", commandParameters);
        }

        public static DataTable GetStarMatch(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStarMatch  0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }


        public static void UpdateStarMatchScore(int intStarMatchID, int intScoreA, int intScoreB, string strRepURL, string strStasURL, string strMVPPlayer, long intMVPPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.UpdateStarMatchScore @intStarMatchID,@intScoreA,@intScoreB,@strRepURL,@strStasURL,@strMVPPlayer, @intMVPPlayerID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intStarMatchID", SqlDbType.Int, 4), new SqlParameter("@intScoreA", SqlDbType.Int, 4), new SqlParameter("@intScoreB", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100), new SqlParameter("@strMVPPlayer", SqlDbType.NVarChar, 50), new SqlParameter("@intMVPPlayerID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intStarMatchID;
            commandParameters[1].Value = intScoreA;
            commandParameters[2].Value = intScoreB;
            commandParameters[3].Value = strRepURL;
            commandParameters[4].Value = strStasURL;
            commandParameters[5].Value = strMVPPlayer;
            commandParameters[6].Value = intMVPPlayerID;
             SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }


    }
}


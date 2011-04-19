namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPXCupMatchManager
    {
        public static void AddMatch(int intCupID, string strCode, int intClubIDA, int intClubIDB, int intScoreA, int intScoreB, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.AddXMatch @intCupID,@strCode,@intClubIDA,@intClubIDB,@intScoreA,@intScoreB,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCupID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NVarChar, 50), new SqlParameter("@intClubIDA", SqlDbType.Int, 4), new SqlParameter("@intClubIDB", SqlDbType.Int, 4), new SqlParameter("@intScoreA", SqlDbType.Int, 4), new SqlParameter("@intScoreB", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 50), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intCupID;
            commandParameters[1].Value = strCode;
            commandParameters[2].Value = intClubIDA;
            commandParameters[3].Value = intClubIDB;
            commandParameters[4].Value = intScoreA;
            commandParameters[5].Value = intScoreB;
            commandParameters[6].Value = strRepURL;
            commandParameters[7].Value = strStasURL;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteAllMatchByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteAllXMatchByCupID " + intCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetMatchByXGameIDClubID(int intXGameID, int intClubAID, int intClubBID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMatchByXGameIDClubID ", intXGameID, ",", intClubAID, ",", intClubBID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetXCupMatchByClubID(int intClubID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXCupMatchByClubID", commandParameters);
        }

        public static DataRow GetXResultRow(int intXGameID, string strGainCode)
        {
            string commandText = "Exec NewBTP.dbo.GetXResultRow @intXGameID,@strGainCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intXGameID", SqlDbType.Int, 4), new SqlParameter("@strGainCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intXGameID;
            commandParameters[1].Value = strGainCode;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


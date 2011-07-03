namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPCupMatchManager
    {
        public static void AddMatch(int intCupID, string strCode, int intClubIDA, int intClubIDB, int intScoreA, int intScoreB, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.AddMatch @intCupID ,@strCode ,@intClubIDA ,@intClubIDB ,@intScoreA ,@intScoreB ,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCupID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NChar, 50), new SqlParameter("@intClubIDA", SqlDbType.Int, 4), new SqlParameter("@intClubIDB", SqlDbType.Int, 4), new SqlParameter("@intScoreA", SqlDbType.Int, 4), new SqlParameter("@intScoreB", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100) };
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
            string commandText = "Exec NewBTP.dbo.DeleteAllMatchByCupID " + intCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetMatchByCupIDClubID(int intCupID, int intClubIDA, int intClubIDB)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMatchByCupIDClubID ", intCupID, ",", intClubIDA, ",", intClubIDB });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


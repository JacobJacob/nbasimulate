namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPDevCupMatchManager
    {
        public static void AddMatch(int intDevCupID, string strCode, int intClubIDA, int intClubIDB, int intScoreA, int intScoreB, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.AddDevCupMatch @intDevCupID,@strCode,@intClubIDA,@intClubIDB,@intScoreA,@intScoreB,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDevCupID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NChar, 50), new SqlParameter("@intClubIDA", SqlDbType.Int, 4), new SqlParameter("@intClubIDB", SqlDbType.Int, 4), new SqlParameter("@intScoreA", SqlDbType.Int, 4), new SqlParameter("@intScoreB", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intDevCupID;
            commandParameters[1].Value = strCode;
            commandParameters[2].Value = intClubIDA;
            commandParameters[3].Value = intClubIDB;
            commandParameters[4].Value = intScoreA;
            commandParameters[5].Value = intScoreB;
            commandParameters[6].Value = strRepURL;
            commandParameters[7].Value = strStasURL;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteAllMatchByDevCupID(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteAllMatchByDevCupID " + intDevCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetMatchByDevCupIDClubID(int intDevCupID, int intClubIDA, int intClubIDB)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMatchByDevCupIDClubID ", intDevCupID, ",", intClubIDA, ",", intClubIDB });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetMaxDevCupMatchID()
        {
            string commandText = "Exec NewBTP.dbo.[GetMaxDevCupMatchID] ";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


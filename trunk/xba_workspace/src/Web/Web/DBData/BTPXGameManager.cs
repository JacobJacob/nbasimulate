namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPXGameManager
    {
        public static void AddGame(int intCategory, int intCapacity, string strIntroduction, string strRewardXML)
        {
            string commandText = "Exec NewBTP.dbo.AddGame @intCategory,@intCapacity,@strIntroduction,@strRewardXML";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intCapacity", SqlDbType.Int, 4), new SqlParameter("@strIntroduction", SqlDbType.NVarChar, 150), new SqlParameter("@strRewardXML", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intCategory;
            commandParameters[1].Value = intCapacity;
            commandParameters[2].Value = strIntroduction;
            commandParameters[3].Value = strRewardXML;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetAssignXGameTable()
        {
            string commandText = "SELECT * FROM BTP_XGame WHERE Status=0 AND MatchTime<'" + DateTime.Now + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCanDoXGameGroup()
        {
            string commandText = "SELECT * FROM BTP_XGame WHERE Status<3 AND Category IN (1,2)";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetChampionCupKemp(int intPage, int intPageSize)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = false;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPageSize;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetChampionCupKemp", commandParameters);
        }

        public static int GetChampionCupKempCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = true;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetChampionCupKemp", commandParameters);
        }

        public static DataRow GetGameRowByGameID(int intGameID)
        {
            string commandText = "Exec NewBTP.dbo.GetGameRowByGameID " + intGameID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupGameByStatus(int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.GetXGroupGameByStatus " + intStatus;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetLastGameRowByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetLastGameRowByCategory " + intCategory;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXCupRegCount()
        {
            string commandText = "Exec NewBTP.dbo.GetXCupRegCount ";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXGameCount()
        {
            string commandText = "Exec NewBTP.dbo.GetXGameCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXGameRewardGroupTable()
        {
            string commandText = "SELECT * FROM BTP_XGame WHERE Status=2 AND Category IN (1,2)";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetXGameRowByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetXGameRowByCategory " + intCategory;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetXGameTable()
        {
            string commandText = "SELECT TOP 1 * FROM BTP_XGame WHERE Status=0 AND Category=5 ORDER BY XGameID DESC";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXGameTable(int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXGameTable ", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXGameToCupRewardTable()
        {
            string commandText = "SELECT * FROM BTP_XGame WHERE Status=2 AND Category IN (3,4,5)";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXGameToPlayTable()
        {
            string commandText = "Exec NewBTP.dbo.GetXGameToPlayTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetChampion(int intGameID, int intChampionID, string strChampionName)
        {
            string commandText = "Exec NewBTP.dbo.SetXBAChampion @intGameID,@intChampionID,@strChampionName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intGameID", SqlDbType.Int, 4), new SqlParameter("@intChampionID", SqlDbType.Int, 4), new SqlParameter("@strChampionName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intGameID;
            commandParameters[1].Value = intChampionID;
            commandParameters[2].Value = strChampionName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetMatchTime(int intGameID, DateTime dtMatchTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetMatchTime ", intGameID, ",'", dtMatchTime, "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetRoundByCupID(int intCupID, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetRoundByXCupID ", intCupID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetStatus(int intXGameID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetXBAStatus ", intXGameID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetStatusByCupID(int intCupID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetStatusByXCupID ", intCupID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateMatchTimeByXGameID(int intXGameID)
        {
            string commandText = "UPDATE BTP_XGame SET MatchTime=DATEADD(Day,1,MatchTime) WHERE XGameID=" + intXGameID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


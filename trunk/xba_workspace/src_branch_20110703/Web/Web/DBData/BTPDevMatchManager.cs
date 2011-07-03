namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPDevMatchManager
    {
        public static int AddArrangeLvlByDevMatchID(int intUserID, int intDevMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddArrangeLvlByDevMatchID ", intUserID, ",", intDevMatchID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddMatch(int intRound, int intClubHID, int intClubAID, string code)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddDevMatch ", intRound, ",", intClubHID, ",", intClubAID, ",'", code, "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CancelPrearrange(int intClubID5, int intDevMatchID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "CancelPrearrange", commandParameters);
        }

        public static void DeleteAllMatches()
        {
            string commandText = "Exec NewBTP.dbo.DeleteAllMatches";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DelMatchByCode(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.DelMatchByCode @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetDevLogByDevCode(int DoCountint, int PageIndex, int PageSize, string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevLogByDevCode @DoCountint,@PageIndex,@PageSize,@strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCountint", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@strDevCode", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = DoCountint;
            commandParameters[1].Value = PageIndex;
            commandParameters[2].Value = PageSize;
            commandParameters[3].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetDevLogCount(int DoCountint, string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevLogByDevCode 1,0,0,@strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static string GetDevMatchArrange(int intDevMatchID, bool blIsHome)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevMatchID", SqlDbType.Int, 4), new SqlParameter("@IsHome", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intDevMatchID;
            commandParameters[1].Value = blIsHome;
            return (string) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetDevMatchArrange", commandParameters);
        }

        public static DataTable GetDevMatchTableByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevMatchTableByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevMatchTableByRound(int intRound, string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevMatchTableByRound @intRound,@strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intRound", SqlDbType.Int, 4), new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intRound;
            commandParameters[1].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetDevMRowByClubIDRound(int intClubID5, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevMRowByClubIDRound ", intClubID5, ",", intRound });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetDevMRowByDevMatchID(int intDevMatchID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevMRowByDevMatchID " + intDevMatchID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevTicketSoldTable(int ClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevTicketSoldTable " + ClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDownTable(int intClubID, string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDownTable @intClubID,@strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetMatchTableByDevCodeRound(string strDevCode, int intRound)
        {
            string commandText = "Exec NewBTP.dbo.GetMatchTableByDevCodeRound @strDevCode,@intRound";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20), new SqlParameter("@intRound", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intRound;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetMatchTableByRound(int intDevMatchID, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMatchTableByRound ", intDevMatchID, ",", intRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetMaxMatchID()
        {
            string commandText = "Exec NewBTP.dbo.GetMaxMatchID";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetTicketSoldSum(int ClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetTicketSoldSum " + ClubID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUPTable(int intClubID, string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetUPTable @intClubID,@strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void PrearrangeManage(int intClubID5, int intDevMatchID, string strArrange)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4), new SqlParameter("@Arrange", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            commandParameters[2].Value = strArrange;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "PrearrangeManage", commandParameters);
        }

        public static void PrearrangeOpenbyCIDMID(int intClubID5, int intDevMatchID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@DevMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID5;
            commandParameters[1].Value = intDevMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "PrearrangeOpenbyCIDMID", commandParameters);
        }

        public static int SetMangerSay(int intUserID, int intDevMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetMangerSay ", intUserID, ",", intDevMatchID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetUseStaff(int intUserID, int intDevMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetUseStaff ", intUserID, ",", intDevMatchID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateArrangeDev()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateArrangeDev");
        }

        public static void UpdateScore(int intDevMatchID, int intClubHScore, int intClubAScore, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.UpdateScore @intDevMatchID,@intClubHScore,@intClubAScore,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDevMatchID", SqlDbType.Int, 4), new SqlParameter("@intClubHScore", SqlDbType.Int, 4), new SqlParameter("@intClubAScore", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intDevMatchID;
            commandParameters[1].Value = intClubHScore;
            commandParameters[2].Value = intClubAScore;
            commandParameters[3].Value = strRepURL;
            commandParameters[4].Value = strStasURL;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateTicketsPrice(int intDevMatchID, int intTickets, int intPrice)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateTicketsPrice ", intDevMatchID, ",", intTickets, ",", intPrice });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUnionFieldManager
    {
        public static void DayUpdateUnionFieldGame()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DayUpdateUnionFieldGame");
        }

        public static void DeleteUnionField()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DeleteUnionField");
        }

        public static DataTable GegACTFieldTableByUnionID(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUnionID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GegACTFieldTableByUnionID", commandParameters);
        }

        public static DataRow GegUnionFieldRowByUserID(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GegUnionFieldRowByUserID", commandParameters);
        }

        public static int GetTopReputation(int intUserID, int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetTopReputation", commandParameters);
        }

        public static int GetTopRInField(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUnionID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetTopRInField", commandParameters);
        }

        public static int GetUnionFieldCount(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = 1;
            commandParameters[3].Value = 1;
            commandParameters[4].Value = 1;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldTable", commandParameters);
        }

        public static int GetUnionFieldHistoryCount(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = 1;
            commandParameters[3].Value = 1;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldHistoryTable", commandParameters);
        }

        public static SqlDataReader GetUnionFieldHistoryTable(int intUnionID, int intPageIndex, int intPageSize, bool blDoCount)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = blDoCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldHistoryTable", commandParameters);
        }

        public static int GetUnionFieldMatchCount(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = 1;
            commandParameters[3].Value = 1;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldMatchTable", commandParameters);
        }

        public static SqlDataReader GetUnionFieldMatchTable(int intUnionID, int intPageIndex, int intPageSize, bool blDoCount)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = blDoCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldMatchTable", commandParameters);
        }

        public static int GetUnionFieldRowByUnionID(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUnionID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldRowByUnionID", commandParameters);
        }

        public static SqlDataReader GetUnionFieldTable(int intUnionID, int intCategory, int intPageIndex, int intPageSize, bool blDoCount)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intPageIndex;
            commandParameters[3].Value = intPageSize;
            commandParameters[4].Value = blDoCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionFieldTable", commandParameters);
        }

        public static void NightUpdateDeleteUnion()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "NightUpdateDeleteUnion");
        }

        public static int SetUReputation(int intUserID, int intReputation, int intSetUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Reputation", SqlDbType.Int, 4), new SqlParameter("@SetUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intReputation;
            commandParameters[2].Value = intSetUserID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SetUReputation", commandParameters);
        }

        public static int UnionFieldDef(int intUserIDH, int intFieldID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDH", SqlDbType.Int, 4), new SqlParameter("@FieldID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserIDH;
            commandParameters[1].Value = intFieldID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UnionFieldDef", commandParameters);
        }

        public static DataRow UnionFieldReg(int intUserIDA, int intUnionIDH, int intReputation)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDA", SqlDbType.Int, 4), new SqlParameter("@UnionIDH", SqlDbType.Int, 4), new SqlParameter("@Reputation", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserIDA;
            commandParameters[1].Value = intUnionIDH;
            commandParameters[2].Value = intReputation;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UnionFieldReg", commandParameters);
        }

        public static void UpdateUnionFieldGame(int intFMatchID, int intScoreH, int intScoreA)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FMatchID", SqlDbType.Int, 4), new SqlParameter("@ScoreH", SqlDbType.Int, 4), new SqlParameter("@ScoreA", SqlDbType.Int, 4) };
            commandParameters[0].Value = intFMatchID;
            commandParameters[1].Value = intScoreH;
            commandParameters[2].Value = intScoreA;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateUnionFieldGame", commandParameters);
        }
    }
}


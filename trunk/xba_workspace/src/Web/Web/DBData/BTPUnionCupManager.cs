namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUnionCupManager
    {
        public static void AddMatch(int intCupID, string strCode, int intClubIDA, int intClubIDB, int intScoreA, int intScoreB, string strRepURL, string strStasURL, int index)
        {
            string commandText = "Exec NewBTP.dbo.AddUnionCupMatch @intCupID ,@strCode ,@intClubIDA ,@intClubIDB ,@intScoreA ,@intScoreB ,@strRepURL,@strStasURL,@intIndex";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCupID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NChar, 50), new SqlParameter("@intClubIDA", SqlDbType.Int, 4), new SqlParameter("@intClubIDB", SqlDbType.Int, 4), new SqlParameter("@intScoreA", SqlDbType.Int, 4), new SqlParameter("@intScoreB", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100), new SqlParameter("@intIndex", SqlDbType.Int, 4) };
            commandParameters[0].Value = intCupID;
            commandParameters[1].Value = strCode;
            commandParameters[2].Value = intClubIDA;
            commandParameters[3].Value = intClubIDB;
            commandParameters[4].Value = intScoreA;
            commandParameters[5].Value = intScoreB;
            commandParameters[6].Value = strRepURL;
            commandParameters[7].Value = strStasURL;
            commandParameters[8].Value = index;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int AddUnionCupReg(int intUserID, int intUnionCupID)
        {
            string commandText = "Exec NewBTP.dbo.AddUnionCupReg @intUnionCupID,@intUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUnionCupID", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUnionCupID;
            commandParameters[1].Value = intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMaxUnionCupMatchID()
        {
            string commandText = "Exec NewBTP.dbo.GetMaxUnionCupMatchID ";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionCupClubRegCount()
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupClubRegTable 1, 1, 1" });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionCupClubRegTable(int DoCountint, int PageIndex, int PageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupClubRegTable ", DoCountint, ",", PageIndex, ",", PageSize });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionCupCount(int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.[GetUnionCupTableByPage] 1,1,1," + intStatus;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUnionCupMatchByID(int intMatchID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionCupMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intMatchID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionCupMatchByID", commandParameters);
        }

        public static DataRow GetUnionCupRowByUnionCupID(int intUnionCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionCupRowByUnionCupID " + intUnionCupID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionCupTableByPage(int DoCountint, int PageIndex, int PageSize, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupTableByPage ", DoCountint, ",", PageIndex, ",", PageSize, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SendUnionCup(int intSendID, int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SendID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intSendID;
            commandParameters[1].Value = intUserID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendUnionCup", commandParameters);
        }
    }
}


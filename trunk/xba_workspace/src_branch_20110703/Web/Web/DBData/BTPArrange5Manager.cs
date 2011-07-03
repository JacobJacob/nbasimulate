namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPArrange5Manager
    {
        public static void AddArrange(int intClubID, long longCID, long longPFID, long longSFID, long longSGID, long longPGID, int intOffense, int intDefense, int intOffHard, int intDefHard)
        {
            string commandText = string.Concat(new object[] { 
                "EXEC NewBTP.dbo.AddArrange5 ", intClubID, ",", longCID, ",", longPFID, ",", longSFID, ",", longSGID, ",", longPGID, ",", intOffense, ",", intDefense, 
                ",", intOffHard, ",", intDefHard
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreateArrange5(int intClubID, long longCID, long longPFID, long longSFID, long longPGID, long longSGID)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.CreateArrange5 ", intClubID, ",", longCID, ",", longPFID, ",", longSFID, ",", longPGID, ",", longSGID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange5RowByArrange5ID(int intArrange5ID)
        {
            string commandText = "EXEC NewBTP.dbo.GetArrange5RowByArrange5ID " + intArrange5ID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrByCategory(int intClubID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.GetArr5ByCategory ", intClubID, ",", intCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrTableByClubID(int intClubID)
        {
            string commandText = "EXEC NewBTP.dbo.GetArrTable5ByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCheckArrange5(int intClubID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.GetCheckArrange5 ", intClubID, ",", longPlayerID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ReSetArrange5(int intArrangeID, long[] longIDs)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.ReSetArrange5 ", intArrangeID, ",", longIDs[0], ",", longIDs[1], ",", longIDs[2], ",", longIDs[3], ",", longIDs[4] });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetArrange(int intClubID, string strName, int intCategory, long longCID, long longPFID, long longSFID, long longSGID, long longPGID, int intOffHard, int intDefHard, int intOffense, int intDefense, int intOffCenter, int intDefCenter)
        {
            string commandText = "EXEC NewBTP.dbo.SetArrange5 @ClubID,@Name,@Category,@CID,@PFID,@SFID,@SGID,@PGID,@OffHard,@DefHard,@Offense,@Defense,@OffCenter,@DefCenter";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NChar, 10), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@CID", SqlDbType.BigInt, 8), new SqlParameter("@PFID", SqlDbType.BigInt, 8), new SqlParameter("@SFID", SqlDbType.BigInt, 8), new SqlParameter("@SGID", SqlDbType.BigInt, 8), new SqlParameter("@PGID", SqlDbType.BigInt, 8), new SqlParameter("@OffHard", SqlDbType.TinyInt, 1), new SqlParameter("@DefHard", SqlDbType.TinyInt, 1), new SqlParameter("@Offense", SqlDbType.TinyInt, 1), new SqlParameter("@Defense", SqlDbType.TinyInt, 1), new SqlParameter("@OffCenter", SqlDbType.TinyInt, 1), new SqlParameter("@DefCenter", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strName;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = longCID;
            commandParameters[4].Value = longPFID;
            commandParameters[5].Value = longSFID;
            commandParameters[6].Value = longSGID;
            commandParameters[7].Value = longPGID;
            commandParameters[8].Value = intOffHard;
            commandParameters[9].Value = intDefHard;
            commandParameters[10].Value = intOffense;
            commandParameters[11].Value = intDefense;
            commandParameters[12].Value = intOffCenter;
            commandParameters[13].Value = intDefCenter;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateAddArrangeLvl(int intUserIDH, int intUserIDA)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDH", SqlDbType.Int, 4), new SqlParameter("@UserIDA", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserIDH;
            commandParameters[1].Value = intUserIDA;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateAddArrangeLvl", commandParameters);
        }
    }
}


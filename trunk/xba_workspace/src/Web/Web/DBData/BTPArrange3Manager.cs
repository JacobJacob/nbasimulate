namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPArrange3Manager
    {
        public static void AddArrange(int intClubID, long longCID, long longFID, long longGID, int intOffense, int intDefense, int intOffHard, int intDefHard)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.AddArrange3 ", intClubID, ",", longCID, ",", longFID, ",", longGID, ",", intOffense, ",", intDefense, ",", intOffHard, ",", intDefHard });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange3FirstByClubID(int intClubID)
        {
            string commandText = "SELECT TOP 1 * FROM BTP_Arrange3 WHERE ClubID= " + intClubID + " ORDER BY Arrange3ID ASC";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange3RowByArrange3ID(int intArrange3ID)
        {
            string commandText = "EXEC NewBTP.dbo.GetArrange3RowByArrange3ID " + intArrange3ID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrByCategory(int intClubID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.GetArr3ByCategory ", intClubID, ",", intCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrTableByClubID(int intClubID)
        {
            string commandText = "EXEC NewBTP.dbo.GetArrTable3ByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCheckArrange3(int intClubID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.GetCheckArrange3 ", intClubID, ",", longPlayerID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ReSetArrange3(int intArrangeID, long[] longIDs)
        {
            string commandText = string.Concat(new object[] { "EXEC NewBTP.dbo.ReSetArrange3 ", intArrangeID, ",", longIDs[0], ",", longIDs[1], ",", longIDs[2] });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetArrange(int intClubID, string strName, int intCategory, long longCID, long longFID, long longGID, int intOffHard, int intDefHard, int intOffense, int intDefense)
        {
            string commandText = "EXEC NewBTP.dbo.SetArrange3 @ClubID,@Name,@Category,@CID,@FID,@GID,@OffHard,@DefHard,@Offense,@Defense";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Name", SqlDbType.NChar, 10), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@CID", SqlDbType.BigInt, 8), new SqlParameter("@FID", SqlDbType.BigInt, 8), new SqlParameter("@GID", SqlDbType.BigInt, 8), new SqlParameter("@OffHard", SqlDbType.TinyInt, 1), new SqlParameter("@DefHard", SqlDbType.TinyInt, 1), new SqlParameter("@Offense", SqlDbType.TinyInt, 1), new SqlParameter("@Defense", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strName;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = longCID;
            commandParameters[4].Value = longFID;
            commandParameters[5].Value = longGID;
            commandParameters[6].Value = intOffHard;
            commandParameters[7].Value = intDefHard;
            commandParameters[8].Value = intOffense;
            commandParameters[9].Value = intDefense;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


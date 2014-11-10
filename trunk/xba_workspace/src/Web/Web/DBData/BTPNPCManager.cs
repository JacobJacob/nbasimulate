namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPNPCManager
    {
        public static int AddNPCMatch(int intUserID, int intNpcID, int intFreeTimes)
        {
            string commandText = "Exec NewBTP.dbo.[AddNPCMatch] @intUserID,@intNPCID,@intFreeTimes";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intNpcID", SqlDbType.Int, 4), new SqlParameter("@intFreeTimes", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intNpcID;
            commandParameters[2].Value = intFreeTimes;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetDropOffTableByPage(int DoCountint, int PageIndex, int PageSize, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetNPCDrop] 0,", PageIndex, ",", PageSize, ",", intUserID });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDropOffTableCount(int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetNPCDrop] 1, 0 , 0,", intUserID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetNPCMatchTableByPage(int DoCountint, int PageIndex, int PageSize, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetNPCMatch] 0,", PageIndex, ",", PageSize, ",", intClubID });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetNPCMatchTableCount(int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.[GetNPCMatch] 1, 0 , 0,", intClubID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetNPCRow(long longNpcID)
        {
            string commandText = "Exec NewBTP.dbo.GetNPCRow @longNpcID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longNpcID", SqlDbType.Int, 4) };
            commandParameters[0].Value = longNpcID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetNPCTable()
        {
            string commandText = "Exec NewBTP.dbo.GetNPCTable ";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetNPCMatchEnd(int matchID)
        {
            string commandText = "Exec NewBTP.dbo.SetNPCMatchEnd @intMatchID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intMatchID", SqlDbType.Int, 4) };
            commandParameters[0].Value = matchID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


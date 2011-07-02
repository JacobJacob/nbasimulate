namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPOnlyOneCenterReg
    {
        public static int GetOnlyOneCountByStatus(int intStatus, int intOnlyPay)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOnlyOneCountByStatus ", intStatus, ",", intOnlyPay });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetOnlyOneCountMy(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetOnlyOneMatchMy " + intClubID + ",1,0,0,0";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOnlyOneMatchMy(int intClubID, int intPageIndex, int intPageSize, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOnlyOneMatchMy ", intClubID, ",0,", intPageIndex, ",", intPageSize, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetOnlyOneMatchRow(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetOnlyOneMatchRow " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOnlyOneRegOnePay(int intPageIndex, int intPageSize, int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = 0;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetOnlyOneRegOnePay", commandParameters);
        }

        public static int GetOnlyOneRegOnePayCount(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = 0;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = intUserID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetOnlyOneRegOnePay", commandParameters);
        }

        public static int GetOnlyOneTodayCount()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlyOneTodayTop 1,0,0,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOnlyOneTodayTop(int intStatus, int intPageIndex, int intPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOnlyOneTodayTop 0,", intPageIndex, ",", intPageSize, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOnlyOneTop(int intStatus, int intPageIndex, int intPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOnlyOneTop 0,", intPageIndex, ",", intPageSize, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetOnlyOneTopCount()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlyOneTop 1,0,0,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTopWealthByClubID(int intClubID5)
        {
            string commandText = "Exec NewBTP.dbo.GetTopWealthByClubID " + intClubID5;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OnlyErrorOutByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.OnlyErrorOutByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OnlyOneCenterReg(int intUserID, int intTopWealth, byte blnOnlyPay)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.OnlyOneCenterReg ", intUserID, ",", intTopWealth, ",", blnOnlyPay });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void OnlyOneError()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "OnlyOneError");
        }

        public static int OnlyOneMatchEnd(int intFMatchID)
        {
            string commandText = "Exec NewBTP.dbo.OnlyOneMatchEnd " + intFMatchID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OnlyOneMatchGoOn(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.OnlyOneMatchGoOn " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OnlyOneMatchOut(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.OnlyOneMatchOut " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SendWealthByOnlyDayPoint()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendWealthByOnlyDayPoint");
        }

        public static int SetOnlyPayMatch(int intUserID, int intUserIDB)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserIDB", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUserIDB;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SetOnlyPayMatch", commandParameters));
        }

        public static int SetTopWealth(int intUserID, int intUserIDB)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserIDB", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUserIDB;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SetTopWealth", commandParameters);
        }
    }
}


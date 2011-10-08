namespace Web.DBData
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPXGuessManager
    {
        public static int GetXGuessCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] {new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXGuess", commandParameters);
        }

        public static DataTable GetXGuess(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXGuess  0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }


        public static int BetXGuess(int intUserID, int intXGuessID, int intAmount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BetXGuess  ", intUserID, ",", intXGuessID, ",", intAmount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXGuessRecordCount(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = intUserID;
            return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXGuessRecord", commandParameters);
        }

        public static DataTable GetXGuessRecord(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXGuessRecord  0, ", intPage, ",", intPerPage, ",", intUserID });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXGuessNewRecord()
        {
            string commandText = "Exec NewBTP.dbo.GetXGuessNewRecord ";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

    }
}


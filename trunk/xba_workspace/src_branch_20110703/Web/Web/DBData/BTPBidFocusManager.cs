namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPBidFocusManager
    {
        public static void AddFocus(int intUserID, long longPlayerID, int intCategory, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddFocus ", intUserID, ",", longPlayerID, ",", intCategory, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int AddFocusValues(int intUserID, long longPlayerID, int intCategory, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddFocusValues ", intUserID, ",", longPlayerID, ",", intCategory, ",", intStatus });
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CancelFocus(int intUserID, long longPlayerID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CancelFocus ", intUserID, ",", longPlayerID, ",", intCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetFocusTableByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFocusTableByUserID " + intUserID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


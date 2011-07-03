namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPBoxManager
    {
        public static void DelBoxByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.DelBoxByUserID " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetBoxListByUserID(int intUserID, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetBoxListByUserID ", intUserID, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFreeBoxByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFreeBoxByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetMyBoxListByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetMyBoxListByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetPayBoxByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetPayBoxByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


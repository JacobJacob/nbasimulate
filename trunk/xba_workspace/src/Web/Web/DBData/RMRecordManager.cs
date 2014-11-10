namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class RMRecordManager
    {
        public static void GainPoint(int intUserID, int intMatchID, int intPoint)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GainPoint ", intUserID, ",", intMatchID, ",", intPoint });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetRecordByMatchID(int intMatchID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetRecordByMatchID " + intMatchID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool GetRecordByUIDMID(int intUserID, int intMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetRecordByUIDMID ", intUserID, ",", intMatchID });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetRecordByUserID(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetRecordByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetRecordByUserIDNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetRecordByUserIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetRecordCountByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetRecordCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetRecordCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetRecordByUserIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int SetRecord(int intUserID, int intMatchID, int intHomeScore, int intAwayScore, int intYaoScore)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetRecord ", intUserID, ",", intMatchID, ",", intHomeScore, ",", intAwayScore, ",", intYaoScore });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


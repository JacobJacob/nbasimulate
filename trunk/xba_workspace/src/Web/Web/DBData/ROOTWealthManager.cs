namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTWealthManager
    {
        public static DataTable GetTopWealthTable(int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopWealthTable " + intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetWealthCountByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetWealthCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetWealthCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetWealthTableNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetWealthTable(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetWealthTable ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetWealthTableNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetWealthTableNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetWealthTableNew(int intUserID, int intPage, int intPerPage, bool blnGetTable)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetWealthTableNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPWealthToolManager
    {
        public static int GetWealthToolCount()
        {
            string commandText = "Exec NewBTP.dbo.GetWealthToolList 0,0,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetWealthToolList(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetWealthToolList ", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetWealthToolRowByID(int intToolID)
        {
            string commandText = "Exec NewBTP.dbo.GetWealthToolRowByID " + intToolID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


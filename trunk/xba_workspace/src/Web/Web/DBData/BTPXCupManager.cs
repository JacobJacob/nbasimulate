namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPXCupManager
    {
        public static DataRow GetXBACupRowByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetXBACupRowByCategory " + intCategory;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetXBACupTable(int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXBACupTable ", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXCupCount()
        {
            string commandText = "Exec NewBTP.dbo.GetXCupCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


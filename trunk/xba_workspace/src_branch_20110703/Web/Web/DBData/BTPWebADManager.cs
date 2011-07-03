namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPWebADManager
    {
        public static DataRow GetWebADRow()
        {
            string commandText = "Exec NewBTP.dbo.GetWebADRow";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


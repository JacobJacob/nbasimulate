namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPGameADManager
    {
        public static DataRow GetGameADRow()
        {
            string commandText = "SELECT * FROM BTP_GameAD";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


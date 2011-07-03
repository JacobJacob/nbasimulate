namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class ROOTLockedIPManager
    {
        public static DataRow GetLockedIPRowByIP(string strLockedIP)
        {
            string commandText = "EXEC ROOT_Data.dbo.GetLockedIPRowByIP '" + strLockedIP + "'";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


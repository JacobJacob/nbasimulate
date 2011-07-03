namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class RMTeamManager
    {
        public static string GetTeamNameByTeamID(int intTeamID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTeamNameByTeamID " + intTeamID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText).Trim();
        }
    }
}


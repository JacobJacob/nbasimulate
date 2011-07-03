namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class ROOTEmailContentManager
    {
        public static DataTable GetEmailTable()
        {
            string commandText = "Exec ROOT_Data.dbo.GetEmailTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetEmailContent(string strTitle, string strContent, DateTime datSendTime, int intUserTeam)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetEmailContent  '", strTitle, "','", strContent, "','", datSendTime, "',", intUserTeam });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPADLinkManager
    {
        public static void AddADLink(int intClubID, int intADID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddADLink ", intClubID, ",", intADID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetADLCountByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetADLCountByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetADLinkRowBy2ID(int intClubID, int intADID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetADLinkRowBy2ID ", intClubID, ",", intADID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetADLinkTable(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetADLinkTable " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void NextTurn()
        {
            string commandText = "Exec NewBTP.dbo.NextTurn";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


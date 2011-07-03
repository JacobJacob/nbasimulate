namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPStadiumManager
    {
        public static void BuildStadium()
        {
            string commandText = "Exec NewBTP.dbo.BuildStadium";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreateStadium()
        {
            string commandText = "Exec NewBTP.dbo.CreateStadium";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetStadiumRowByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetStadiumRowByClubID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetStadiumRowByID(int intStadiumID)
        {
            string commandText = "Exec NewBTP.dbo.GetStadiumRowByID " + intStadiumID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetStadiumRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetStadiumRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTicketPrice(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetTicketPrice " + intClubID;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetStadiumUpdate(int intUserID, int intStadiumID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetStadiumUpdate ", intUserID, ",", intStadiumID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UpdateTicketPrice(int intUserID, int intTicketPrice)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateTicketPrice ", intUserID, ",", intTicketPrice });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


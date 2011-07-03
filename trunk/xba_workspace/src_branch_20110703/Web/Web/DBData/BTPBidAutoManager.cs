namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPBidAutoManager
    {
        public static void DeleteBidHelperByID(int intBidHelperID)
        {
            string commandText = "DELETE FROM BTP_BidAuto WHERE BidAutoID=" + intBidHelperID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAutoBid()
        {
            string commandText = "SELECT * FROM BTP_BidAuto";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAutoBidRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetAutoBidRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetBidHelperActionTable()
        {
            return null;
        }

        public static void SetStatucByPlayerID(int intPlayerID, int intStatus)
        {
        }

        public static void SetStatusByID(int intBidHelperID, int intStatus)
        {
        }
    }
}


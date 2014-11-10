namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPBidderManager
    {
        public static void BidBlockRecord(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.Bid_BlockRecord " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int BidQuickBuyByUserID(int intUserID, long lngPlayerID, string strIP)
        {
            string commandText = "Exec NewBTP.dbo.Bid_PreQuickBuy @lngPlayerID,@intUserID,@BidIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@lngPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@BidIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = lngPlayerID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strIP;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DelOldBid()
        {
            try
            {
                string commandText = "DELETE FROM BTP_Bidder WHERE CreateTime+5<getdate()";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                DelOldBid();
            }
        }

        public static int GetBidCount(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetBidCount " + longPlayerID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetBidderByPlayerID(long longPlayerID)
        {
            return null;
        }

        public static DataRow GetBidderByPlayerIDUserID(long longPlayerID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetBidderByPlayerIDUserID ", longPlayerID, ",", intUserID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetBidderByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetBidderByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetBidderCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.BidderCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetChooseBidCount(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetChooseBidCount " + longPlayerID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


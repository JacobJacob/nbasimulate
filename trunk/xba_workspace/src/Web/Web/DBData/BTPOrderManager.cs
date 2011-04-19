namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPOrderManager
    {
        public static DataRow AddWealthOrder(int intUserID, int intCategory, int intPrice, int intWealth, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddWealthOrder ", intUserID, ",", intCategory, ",", intPrice, ",", intWealth, ",", intType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow AddWealthOrderAdmin(int intCategory, int intPrice, int intWealth)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddWealthOrderAdmin 0,", intCategory, ",", intPrice, ",", intWealth, ",1" });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CancelOrder(int intOrderID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CancelOrder ", intOrderID, ",", intUserID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CancelOrderAdmin()
        {
            string commandText = "Exec NewBTP.dbo.CancelOrderAdmin";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetOrderBusinessByUserID(int intUserID, int DoCountint, int PageIndex, int PageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetOrderBusinessByUserID ", intUserID, ",", DoCountint, ",", PageIndex, ",", PageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetOrderBusinessCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetOrderBusinessByUserID " + intUserID + ",1,1,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetOrderParameter()
        {
            string commandText = "Exec NewBTP.dbo.GetOrderParameter ";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetOrderRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetOrderRowByUserID " + intUserID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OurToolUseCount()
        {
            string commandText = "Exec NewBTP.dbo.OurToolUseCount 1,1,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader OurToolUseCountList(int DoCountint, int PageIndex, int PageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.OurToolUseCount ", DoCountint, ",", PageIndex, ",", PageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPStockManager
    {
        public static int GetGetStockCompanyCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] {new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetStockCompany", commandParameters);
        }

        public static DataTable GetGetStockCompany(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStockCompany  0, ", intPage, ",", intPerPage});
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }


        public static int BuyStock(int intUserID, int intStockUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BuyStock  ", intUserID, ",", intStockUserID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStockTeamDay(int intUserId, int intStockUserId)
        {
            string commandText = string.Concat(new Object[] { "SELECT TOP 1 TeamDay FROM BTP_Stock WHERE UserID = ", intUserId, " AND StockUserID =", intStockUserId });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        /*获取经理的市值*/

        public static int GetUserStockPrice(int intUserId)
        {
            string commandText = string.Concat(new Object[] { "SELECT TOP 1 Price FROM BTP_Company WHERE UserID = ", intUserId});
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

       /*public static int GetPlayer5AbilityTopCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] {new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int)SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer5AbilityTop200", commandParameters);
        }

        public static DataTable GetPlayer5AbilityTop(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetPlayer5AbilityTop200  0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }*/

    }
}


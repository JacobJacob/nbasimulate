namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPXBATopManager
    {
        public static DataTable GetPlayer3AbilityTop(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetPlayer3AbilityTop200  0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3AbilityTopCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer3AbilityTop200", commandParameters);
        }

        public static DataTable GetPlayer5AbilityTop(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetPlayer5AbilityTop200  0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5AbilityTopCount()
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer5AbilityTop200", commandParameters);
        }

        public static DataTable GetXBATopByCategory(int intCategory, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXBATopTableByCategory ", intCategory, ", 0, ", intPage, ",", intPerPage });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXBATopByCategoryCount(int intCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Category", SqlDbType.TinyInt, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4) };
            commandParameters[0].Value = intCategory;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = 0;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXBATopTableByCategory", commandParameters);
        }
    }
}


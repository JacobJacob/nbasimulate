namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUWealthFinanceManager
    {
        public static void AddUnionWealthFinance(int intUserID, int intCategory, int intType, int intWealth, int intUserCategory, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.AddUWealthFinance @intUserID,@intCategory,@intType,@intWealth,@intUserCategory,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@intUserCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strRemark", SqlDbType.NVarChar, 300) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intType;
            commandParameters[3].Value = intWealth;
            commandParameters[4].Value = intUserCategory;
            commandParameters[5].Value = strRemark;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetUnionWealthCount(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUWFinanceTableByUnionID 1,1,1," + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUWFinanceTableByUnionID(int intDoCount, int intPage, int intPerPage, int intUnionID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUWFinanceTableByUnionID ", intDoCount, ",", intPage, ",", intPerPage, ",", intUnionID });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPWealthFinanceManager
    {
        public static int GameServiceTotal(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetWealthFinanceTableByUserID 1,0,0," + intUserID;
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetWealthFinanceTableByUserID(int DoCountint, int PageIndex, int PageSize, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetWealthFinanceTableByUserID ", DoCountint, ",", PageIndex, ",", PageSize, ",", intUserID });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetWealthFinance(int intUserID, string strNickName, int intCategory, int intWealth, string strEvent, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.SetWealthFinance @intUserID,@strNickName,@intCategory,@intWealth,@strEvent,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 300), new SqlParameter("@strRemark", SqlDbType.NVarChar, 300) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intWealth;
            commandParameters[4].Value = strEvent;
            commandParameters[5].Value = strRemark;
            return SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


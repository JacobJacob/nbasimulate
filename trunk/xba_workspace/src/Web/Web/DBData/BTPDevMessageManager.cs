namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPDevMessageManager
    {
        public static void AddDevMessage(string strDevCode, int intUserID, string strNickName, string strContent)
        {
            strContent = StringItem.GetValidWords(strContent);
            string commandText = "Exec NewBTP.dbo.AddDevMessage @DevCode,@UserID,@NickName,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCode", SqlDbType.Char, 20), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetDevMessageCountByCode(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevMessageCountByCode @DevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTableByDevCode(string strDevCode, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = "Exec NewBTP.dbo.GetTableByDevCode @DevCode,@Page,@PerPage,@Count,@Total";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCode", SqlDbType.Char, 20), new SqlParameter("@Page", SqlDbType.Int, 4), new SqlParameter("@PerPage", SqlDbType.Int, 4), new SqlParameter("@Count", SqlDbType.Int, 4), new SqlParameter("@Total", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = intCount;
            commandParameters[4].Value = intTotal;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTopTableByDevCode(string strDevCode, int intTop)
        {
            string commandText = "Exec NewBTP.dbo.GetTopTableByDevCode @DevCode,@Top";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCode", SqlDbType.NChar, 20), new SqlParameter("@Top", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPWealthMatchManager
    {
        public static int AddWealthMatch(int intUserID, string strNickName, int intClubID, string strClubName, int intType, int intDevLevel, int intCategory, int intWealth)
        {
            string commandText = "Exec NewBTP.dbo.AddWealthMatch @intUserID,@strNickName,@intClubID,@strClubName,@intType,@intDevLevel,@intCategory,@intWealth";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 20), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intDevLevel", SqlDbType.TinyInt, 1), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intWealth", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = intClubID;
            commandParameters[3].Value = strClubName;
            commandParameters[4].Value = intType;
            commandParameters[5].Value = intDevLevel;
            commandParameters[6].Value = intCategory;
            commandParameters[7].Value = intWealth;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetWealthMatchTable(int DoCountint, int PageIndex, int PageSize, int intCategory, int intMaxWealth, int intMinWealth)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.WealthMatch_Pagination ", DoCountint, ",", PageIndex, ",", PageSize, ",", intCategory, ",", intMaxWealth, ",", intMinWealth });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


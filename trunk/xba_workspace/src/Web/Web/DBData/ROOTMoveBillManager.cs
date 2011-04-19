namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTMoveBillManager
    {
        public static void AddMoveBill(string strBstrMoMessageID, string strMoveBillNumber, int intUserID, string strUserName, int intType, int intCategory, int intMessageCount)
        {
            string commandText = "Exec ROOT_Data.dbo.AddMoveBill @strBstrMoMessageID,@strMoveBillNumber,@intUserID,@strUserName,@intType,@intCategory,@intMessageCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBstrMoMessageID", SqlDbType.NChar, 20), new SqlParameter("@strMoveBillNumber", SqlDbType.NChar, 20), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@intType", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@intMessageCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBstrMoMessageID;
            commandParameters[1].Value = strMoveBillNumber;
            commandParameters[2].Value = intUserID;
            commandParameters[3].Value = strUserName;
            commandParameters[4].Value = intType;
            commandParameters[5].Value = intCategory;
            commandParameters[6].Value = intMessageCount;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetMoveBillByNumber(string strNumber, int intCategory)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMoveBillByNumber @strNumber,@intCategory";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNumber", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNumber;
            commandParameters[1].Value = intCategory;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetMoveBillByUserName(string strUserName, int intCategory)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMoveBillByUserName @strUserName,@intCategory";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.Int, 4) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intCategory;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMoveBillCount()
        {
            string commandText = "Exec ROOT_Data.dbo.GetMoveBillTable 1,0,0";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetMoveBillCountByType(int intType, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetMoveBillTableByType 1,0,0", intType, ",", intCategory });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetMoveBillTable(int intDoCount, int intPageIndex, int intintPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetMoveBillTable ", intDoCount, ",", intPageIndex, ",", intintPageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetMoveBillTableByType(int intDoCount, int intPageIndex, int intPageSize, int intType, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetMoveBillTableByType ", intDoCount, ",", intPageIndex, ",", intPageSize, ",", intType, ",", intCategory });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


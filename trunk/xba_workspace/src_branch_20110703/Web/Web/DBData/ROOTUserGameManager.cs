namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTUserGameManager
    {
        public static byte AddUserGame(int intUserID, int intCategory, string strGame)
        {
            string commandText = "Exec ROOT_Data.dbo.AddUserGame @intUserID,@intCategory,@strGame";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strGame", SqlDbType.NChar, 10) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strGame;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static string GetBadWord()
        {
            string commandText = "SELECT TOP 1 BadWord FROM ROOT_Main";
            return SqlHelper.ExecuteStringReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUGRowByUIDCategory(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetUGRowByUIDCategory ", intUserID, ",", intCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetUserGameTableByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserGameTableByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool HasUserGame(int intUserID, int intCategory)
        {
            if (GetUGRowByUIDCategory(intUserID, intCategory) == null)
            {
                return false;
            }
            return true;
        }

        public static int UpdateDeptTagByUserID(int intUserID, string strDeptTag)
        {
            string commandText = "Exec maitiam_main.dbo.UpdateDeptTagByUserID @UserID,@DeptTag";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@DeptTag", SqlDbType.NChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strDeptTag;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }
    }
}


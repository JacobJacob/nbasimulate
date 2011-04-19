namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTTopicManager
    {
        public static void AddEliteWealth(int intUserIDM, int intUserID, int intAddWealth)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.AddEliteWealth ", intUserIDM, ",", intUserID, ",", intAddWealth });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void AddGiveWealth(int intUserID, int intAddWealth, string strEvent)
        {
            string commandText = "Exec ROOT_Data.dbo.AddGiveWealth @intUserID,@intAddWealth,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intAddWealth", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intAddWealth;
            commandParameters[2].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddReply(string strBoardID, int intUserID, string strNickName, string strLogo, string strTitle, string strContent, int intTopicID, string strMainTitle, string strMainLogo, bool blnIsVote)
        {
            string commandText = "Exec ROOT_Data.dbo.AddReply @strBoardID,@intUserID,@strNickName,@strLogo,@strTitle,@strContent,@intTopicID,@strMainTitle,@strMainLogo,@blnIsVote";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@strLogo", SqlDbType.NChar, 20), new SqlParameter("@strTitle", SqlDbType.NVarChar, 100), new SqlParameter("@strContent", SqlDbType.NText), new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@strMainTitle", SqlDbType.NVarChar, 100), new SqlParameter("@strMainLogo", SqlDbType.NChar, 20), new SqlParameter("@blnIsVote", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strLogo;
            commandParameters[4].Value = strTitle;
            commandParameters[5].Value = strContent;
            commandParameters[6].Value = intTopicID;
            commandParameters[7].Value = strMainTitle;
            commandParameters[8].Value = strMainLogo;
            commandParameters[9].Value = blnIsVote;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddTopic(string strBoardID, int intUserID, string strNickName, string strLogo, string strTitle, string strContent, string strKeyword, bool blnIsVote)
        {
            string commandText = "Exec ROOT_Data.dbo.AddTopic @strBoardID,@intUserID,@strNickName,@strLogo,@strTitle,@strContent,@strKeyword,@blnIsVote";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@strLogo", SqlDbType.NChar, 20), new SqlParameter("@strTitle", SqlDbType.NVarChar, 100), new SqlParameter("@strContent", SqlDbType.NText), new SqlParameter("@strKeyword", SqlDbType.NChar, 20), new SqlParameter("@blnIsVote", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strLogo;
            commandParameters[4].Value = strTitle;
            commandParameters[5].Value = strContent;
            commandParameters[6].Value = strKeyword;
            commandParameters[7].Value = blnIsVote;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddTopicWealth(int intUserID, int intAddWealth)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.AddTopicWealth ", intUserID, ",", intAddWealth });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void AddVoteIP(int intTopicID, string strIP, string strCookie)
        {
            string commandText = "Exec ROOT_Data.dbo.AddVoteIP @TopicID,@IP,@Cookie";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@IP", SqlDbType.NChar, 20), new SqlParameter("@Cookie", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = strIP;
            commandParameters[2].Value = strCookie;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddWealth(int intUserIDM, int intUserID, int intAddWealth)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.AddWealth ", intUserIDM, ",", intUserID, ",", intAddWealth });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void CancelEliteByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.CancelEliteByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void CancelOnTopByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.CancelOnTopByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void CanEliteWealth(int intUserIDM, int intUserID, int intAddWealth)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.CanEliteWealth ", intUserIDM, ",", intUserID, ",", intAddWealth });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void DeleteTopicByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.DELETETopicByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void DelTopicByID(int intTopicID, int intWealth, string strDelReason)
        {
            string commandText = "Exec ROOT_Data.dbo.DelTopicByID @intTopicID,@intWealth,@strDelReason";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@strDelReason", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = intWealth;
            commandParameters[2].Value = strDelReason;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void DelWealth(int intUserIDM, int intUserID, int intAddWealth)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.DelWealth ", intUserIDM, ",", intUserID, ",", intAddWealth });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void EditTopic(int intTopicID, string strNickName, string strKeyword, string strLogo, string strContent, string strTitle)
        {
            string commandText = "Exec ROOT_Data.dbo.EditTopic @intTopicID,@strNickName,@strKeyword,@strLogo,@strContent,@strTitle";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@strKeyword", SqlDbType.NChar, 20), new SqlParameter("@strLogo", SqlDbType.NChar, 20), new SqlParameter("@strContent", SqlDbType.NText), new SqlParameter("@strTitle", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strKeyword;
            commandParameters[3].Value = strLogo;
            commandParameters[4].Value = strContent;
            commandParameters[5].Value = strTitle;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetEliteTopic(int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetEliteTopic " + intTop;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DateTime GetLatestByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetLatestByUserID " + intUserID;
            return SqlHelper.ExecuteDateTimeDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static string GetMainTitle(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMainTitle " + intTopicID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetNewEliteByTop(int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetNewEliteByTop " + intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetSearchCount(string strKeyword, string strCategory)
        {
            string commandText = "Exec ROOT_Data.dbo.GetSearchCount @strKeyword,@strCategory";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strKeyword", SqlDbType.NVarChar, 50), new SqlParameter("@strCategory", SqlDbType.NChar, 10) };
            commandParameters[0].Value = strKeyword;
            commandParameters[1].Value = strCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetSearchCountNew(string strKeyword, string strCategory)
        {
            string commandText = "Exec ROOT_Data.dbo.GetSearchTableNew @strKeyword,@strCategory,0,0,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strKeyword", SqlDbType.NVarChar, 50), new SqlParameter("@strCategory", SqlDbType.NChar, 10) };
            commandParameters[0].Value = strKeyword;
            commandParameters[1].Value = strCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetSearchTable(string strKeyword, string strCategory, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = "Exec ROOT_Data.dbo.GetSearchTable @strKeyword,@strCategory,@intPage,@intPerPage,@intCount,@intTotal";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strKeyword", SqlDbType.NVarChar, 50), new SqlParameter("@strCategory", SqlDbType.NChar, 10), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4) };
            commandParameters[0].Value = strKeyword;
            commandParameters[1].Value = strCategory;
            commandParameters[2].Value = intPage;
            commandParameters[3].Value = intPerPage;
            commandParameters[4].Value = intCount;
            commandParameters[5].Value = intTotal;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetSearchTableNew(string strKeyword, string strCategory, int intPage, int intPerPage)
        {
            string commandText = "Exec ROOT_Data.dbo.GetSearchTableNew @strKeyword,@strCategory,@intPage,@intPerPage,0";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strKeyword", SqlDbType.NVarChar, 50), new SqlParameter("@strCategory", SqlDbType.NChar, 10), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4) };
            commandParameters[0].Value = strKeyword;
            commandParameters[1].Value = strCategory;
            commandParameters[2].Value = intPage;
            commandParameters[3].Value = intPerPage;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTableByBoardID(string strBoardID, int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTableByBoardID @strBoardID,@intTop";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTop", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTableByBoardIDElite(string strBoardID, int intTop, int intIsElite)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTableByBoardIDElite @strBoardID,@intTop,@intIsElite";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTop", SqlDbType.Int, 4), new SqlParameter("@intIsElite", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTop;
            commandParameters[2].Value = Convert.ToBoolean(intIsElite);
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTableByTop(int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTableByTop " + intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetTableWithoutBoardID(string strBoardID, int intTop)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTableWithoutBoardID @strBoardID,@intTop";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTop", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTop;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetTopicCountByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicTableByUserID  1,0,0," + intUserID;
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetTopicReplyCountByID(string strBoardID, int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicReplyCountByID @strBoardID,@intTopicID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTopicID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTopicID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetTopicReplyCountByIDNew(string strBoardID, int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicReplyTableByIDNew @strBoardID,@intTopicID,0,0,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTopicID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTopicID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetTopicReplyTableByID(string strBoardID, int intTopicID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicReplyTableByID @strBoardID,@intTopicID,@intPage,@intPerPage,@intCount,@intTotal";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTopicID;
            commandParameters[2].Value = intPage;
            commandParameters[3].Value = intPerPage;
            commandParameters[4].Value = intCount;
            commandParameters[5].Value = intTotal;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetTopicReplyTableByIDNew(string strBoardID, int intTopicID, int intPage, int intPerPage)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicReplyTableByIDNew @strBoardID,@intTopicID,@intPage,@intPerPage,0";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intTopicID;
            commandParameters[2].Value = intPage;
            commandParameters[3].Value = intPerPage;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetTopicRowByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTopicRowByID " + intTopicID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTopicTableByUserID(int intPageIndex, int intPageSize, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetTopicTableByUserID  0,", intPageIndex, ",", intPageSize, ",", intUserID });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool HasVote(int intUserID, int intTopicID)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.HasVote ", intUserID, ",", intTopicID });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool HasVoteIP(int intTopicID, string strIP, string strCookie)
        {
            string commandText = "Exec ROOT_Data.dbo.HasVoteIP @TopicID,@IP,@Cookie";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@IP", SqlDbType.NChar, 20), new SqlParameter("@Cookie", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = strIP;
            commandParameters[2].Value = strCookie;
            if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters) == null)
            {
                return false;
            }
            return true;
        }

        public static void RevertDelByNickName(string strNickName, int intDay)
        {
            string commandText = "Exec ROOT_Data.dbo.RevertDelByNickName @strNickName,@intDay";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intDay", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intDay;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetDelByNickName(string strNickName, int intDay)
        {
            string commandText = "Exec ROOT_Data.dbo.SetDelByNickName @strNickName,@intDay";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intDay", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intDay;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetEliteByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetEliteByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetLockByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetLockByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetOnTopByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetOnTopByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetResolveByID(int intTopicID, int intIsResolve, string strTitle)
        {
            string commandText = "Exec ROOT_Data.dbo.SetResolveByID @intTopicID,@intIsResolve,@strTitle";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intTopicID", SqlDbType.Int, 4), new SqlParameter("@intIsResolve", SqlDbType.Int, 4), new SqlParameter("@strTitle", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = intIsResolve;
            commandParameters[2].Value = strTitle;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetUnLockByID(int intTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetUnLockByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


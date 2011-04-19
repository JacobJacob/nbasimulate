namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTCollectTopicManager
    {
        public static int CollectTopic(int intUserID, int intTopicID)
        {
            string commandText = string.Concat(new object[] { "EXEC ROOT_Data.dbo.CollectTopic ", intUserID, ",", intTopicID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int DelCollectTopic(int intUserID, int intTopicID)
        {
            string commandText = string.Concat(new object[] { "EXEC ROOT_Data.dbo.DelCollectTopic ", intUserID, ",", intTopicID });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCollectTopicCount(int intUserID)
        {
            string commandText = "EXEC ROOT_Data.dbo.GetCollectTopicTable " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetCollectTopicTable(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "EXEC ROOT_Data.dbo.GetCollectTopicTable ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


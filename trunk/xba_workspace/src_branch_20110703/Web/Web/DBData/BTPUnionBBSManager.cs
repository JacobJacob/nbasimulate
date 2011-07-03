namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUnionBBSManager
    {
        public static void AddUnionReply(int intUnionID, int intUserID, string strNickName, string strLogo, string strTitle, string strContent, int intTopicID, string strMainTitle, string strMainLogo, string strSendIP)
        {
            string commandText = "Exec NewBTP.dbo.AddUnionReply @UnionID,@UserID,@NickName,@Logo,@Title,@Content,@TopicID,@MainTitle,@MainLogo,@SendIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Logo", SqlDbType.NChar, 20), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Content", SqlDbType.NText), new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@MainTitle", SqlDbType.NVarChar, 100), new SqlParameter("@MainLogo", SqlDbType.NChar, 20), new SqlParameter("@SendIP", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strLogo;
            commandParameters[4].Value = strTitle;
            commandParameters[5].Value = strContent;
            commandParameters[6].Value = intTopicID;
            commandParameters[7].Value = strMainTitle;
            commandParameters[8].Value = strMainLogo;
            commandParameters[9].Value = strSendIP;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddUnionTopic(int intUnionID, int intUserID, string strNickName, string strLogo, string strTitle, string strContent, string strSendIP)
        {
            string commandText = "Exec NewBTP.dbo.AddUnionTopic @UnionID,@UserID,@NickName,@Logo,@Title,@Content,@SendIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Logo", SqlDbType.NChar, 20), new SqlParameter("@Title", SqlDbType.NVarChar, 100), new SqlParameter("@Content", SqlDbType.NText), new SqlParameter("@SendIP", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strLogo;
            commandParameters[4].Value = strTitle;
            commandParameters[5].Value = strContent;
            commandParameters[6].Value = strSendIP;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void CancelUnionEliteByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.CancelUnionEliteByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CancelUnionOnTopByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.CancelUnionOnTopByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteUnionTopicByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteUnionTopicByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void EditUnionTopic(int intTopicID, string strNickName, string strLogo, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.EditUnionTopic @TopicID,@NickName,@Logo,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@TopicID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Logo", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NText) };
            commandParameters[0].Value = intTopicID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strLogo;
            commandParameters[3].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DateTime GetLatestByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetLatestByUserID " + intUserID;
            return SqlHelper.ExecuteDateTimeDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUnionBoardRowByUnionID(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionBoardRowByUnionID " + intUnionID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetUnionMainTitle(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionMainTitle " + intTopicID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionTopicCount(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionTopicCount " + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionTopicDetailCount(int intUnionID, int intTopicID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionTopicDetailCount ", intUnionID, ",", intTopicID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionTopicDetailTable(int intUnionID, int intTopic, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionTopicDetailTable ", intUnionID, ",", intTopic, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUnionTopicRowByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionTopicRowByID " + intTopicID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionTopicTable(int intUnionID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionTopicTable ", intUnionID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionEliteByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionEliteByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionLockByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionLockByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionOnTopByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionOnTopByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionUnLockByID(int intTopicID)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionUnLockByID " + intTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


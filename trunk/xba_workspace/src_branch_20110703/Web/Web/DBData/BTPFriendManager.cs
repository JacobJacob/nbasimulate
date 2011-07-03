namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPFriendManager
    {
        public static bool DeleteFriend(int intUserID, int intFriendID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteFriend ", intUserID, ",", intFriendID });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriendTableByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriendTableByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetFriend(int intUserID, string strFriendName)
        {
            string commandText = "Exec NewBTP.dbo.SetFriend @UserID,@FriendName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@FriendName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strFriendName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


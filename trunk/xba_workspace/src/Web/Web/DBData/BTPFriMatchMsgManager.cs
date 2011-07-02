namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPFriMatchMsgManager
    {
        public static void AddFriMatchMsg(int intUserID, string strNickName, string strClubName, string strContent)
        {
            strContent = StringItem.GetValidWords(strContent);
            string commandText = "Exec NewBTP.dbo.AddFriMatchMsg @UserID,@NickName,@ClubName,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@ClubName", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strClubName;
            commandParameters[3].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DelFriMatchMsg()
        {
            try
            {
                string commandText = "DELETE FROM BTP_FriMatchMsg";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                DelFriMatchMsg();
            }
        }

        public static DataTable GetAddFriMatchMsg(int intFMatchMsgID, string strContent, int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FMatchMsgID", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar, 100), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intFMatchMsgID;
            commandParameters[1].Value = strContent;
            commandParameters[2].Value = intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetAddFriMatchMsg", commandParameters);
        }

        public static int GetFriMatchMsgCount()
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchMsgCount";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetFriMatchMsgCountNew()
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchMsgTableNew 0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatchMsgNew(int intFMatchMsgID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FMatchMsgID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intFMatchMsgID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetFriMatchMsgNew", commandParameters);
        }

        public static DataRow GetFriMatchMsgRowTop1ByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchMsgRowTop1 " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatchMsgTable(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFriMatchMsgTable ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatchMsgTableNew(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFriMatchMsgTableNew ", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPMessageManager
    {
        public static bool AddMessage(int intSendID, string strSender, string strNickName, string strContent)
        {
            strContent = StringItem.GetValidWords(strContent);
            string commandText = "Exec NewBTP.dbo.AddMessage @SendID,@Sender,@NickName,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SendID", SqlDbType.Int, 4), new SqlParameter("@Sender", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intSendID;
            commandParameters[1].Value = strSender;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strContent;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMessage(int intUserID, int intCategory, int intSendID, string strSender, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.AddNewMessage @UserID,@Category,@SendID,@Sender,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@SendID", SqlDbType.Int, 4), new SqlParameter("@Sender", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intSendID;
            commandParameters[3].Value = strSender;
            commandParameters[4].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMessage(int intUserID, int intCategory, int intSendID, string strSender, string strContent, int intGameCategory)
        {
            string commandText = "Exec NewBTP.dbo.AddNewMessage @UserID,@Category,@SendID,@Sender,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@SendID", SqlDbType.Int, 4), new SqlParameter("@Sender", SqlDbType.NChar, 20), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intSendID;
            commandParameters[3].Value = strSender;
            commandParameters[4].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBLogin.ConnString(intGameCategory), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMessageByClubID3(int intClubID, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.AddMessageByClubID3 @ClubID,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMessageByClubID5(int intClubID, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.AddMessageByClubID5 @ClubID,@Content";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int AddPunishMessage(int intUserID, string strNickName, int intCategory, int intSendID, string strSender, string strContent)
        {
            return 1;
        }

        public static void AddRookieMessage(int intUserID, int intKind)
        {
            string rookieMessage = MessageItem.GetRookieMessage(intKind);
            AddMessage(intUserID, 2, 0, "秘书报告", rookieMessage);
            SetHasMsg(BTPAccountManager.GetNickNameByUserID(intUserID));
        }

        public static bool DeleteMessage(int intUserID, int intMessageID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteMessage ", intUserID, ",", intMessageID, ",", intType });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteMsgByTime(string strTime)
        {
            string commandText = "Exec NewBTP.dbo.DeleteMsgByTime @Time";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Time", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = Convert.ToDateTime(strTime);
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMessageCountBySendID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetMessageTableBySendID " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetMessageCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetMessageCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetMessageCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetMessageTableByUserIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetMessageTableBySendID(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMessageTableBySendID ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetMessageTableByUserID(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMessageTableByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetMessageTableByUserIDNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetMessageTableByUserIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTableWealthMatchMsg(int intWMMID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@WMMID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intWMMID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetTableWealthMatchMsg", commandParameters);
        }

        public static void SendMessageForNewUser()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendMessageForNewUser");
        }

        public static void SendPCMsg()
        {
            DataTable pCClubTable = BTPClubManager.GetPCClubTable();
            if (pCClubTable != null)
            {
                foreach (DataRow row in pCClubTable.Rows)
                {
                    int intClubID = (int) row["ClubID"];
                    AddMessageByClubID5(intClubID, "您队中有球员的合同快到期了，请及时续签！");
                }
            }
        }

        public static void SendSystemMsg(string strContent)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendSystemMsg", commandParameters);
        }

        public static void SetHasMsg(string strNickName)
        {
            if (DTOnlineManager.GetOnlineRowByNickName(strNickName) != null)
            {
                int intUserID = -1;
                DataRow accountRowByNickName = BTPAccountManager.GetAccountRowByNickName(strNickName);
                if (accountRowByNickName != null)
                {
                    intUserID = (int) accountRowByNickName["UserID"];
                    DTOnlineManager.SetHasMsgByUserID(intUserID);
                }
            }
        }
    }
}


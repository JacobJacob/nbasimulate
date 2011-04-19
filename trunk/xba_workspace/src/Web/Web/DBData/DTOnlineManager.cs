namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class DTOnlineManager
    {
        public static void ChangeCategoryByUserID(int intUserID, int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.ChangeCategoryByUserID @UserID, @Category";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeClubLogoByUserID(int intUserID, string strClubLogo)
        {
            string commandText = "Exec NewBTP.dbo.ChangeClubLogoByUserID @UserID, @ClubLogo";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ClubLogo", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClubLogo;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeClubNameByUserID(int intUserID, string strClubName)
        {
            string commandText = "Exec NewBTP.dbo.ChangeClubNameByUserID @UserID, @ClubName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ClubName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClubName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangePayTypeByUserID(int intUserID, int intPayType)
        {
            string commandText = "Exec NewBTP.dbo.ChangePayTypeByUserID @UserID, @PayType";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PayType", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPayType;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeQQByUserID(int intUserID, string strQQ)
        {
            string commandText = "Exec NewBTP.dbo.ChangeQQByUserID @UserID, @QQ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@QQ", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strQQ;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeUnionIDByUserID(int intUserID, int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.ChangeUnionIDByUserID @UserID, @UnionID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteOnlineRow(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteOnlineRow @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetOnlineCount()
        {
            DataRow[] onlineRow = GetOnlineRow();
            if (onlineRow == null)
            {
                return 0;
            }
            return onlineRow.Length;
        }

        public static int GetOnlineMCount()
        {
            DataRow[] onlineMRow = GetOnlineMRow();
            if (onlineMRow == null)
            {
                return 0;
            }
            return onlineMRow.Length;
        }

        public static DataRow[] GetOnlineMRow()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineTable";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            DateTime time = DateTime.Now.AddMinutes(-30.0);
            DataRow[] rowArray = null;
            if (table != null)
            {
                rowArray = table.Select("LatestActiveTime>'" + time.ToString() + "' AND (Category=1 OR Category=2 OR Category=5)", "Wealth DESC");
            }
            return rowArray;
        }

        public static DataRow[] GetOnlineRow()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineTable";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            DateTime time = DateTime.Now.AddMinutes(-30.0);
            DataRow[] rowArray = null;
            if (table != null)
            {
                rowArray = table.Select("LatestActiveTime>'" + time.ToString() + "'", "OnlineID DESC");
            }
            return rowArray;
        }

        public static DataRow GetOnlineRowByNickName(string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineRowByNickName @NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetOnlineRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineRowByUserID @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow[] GetOnlineRowWithoutLimit()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineTable";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            DataRow[] rowArray = null;
            if (table != null)
            {
                rowArray = table.Select("", "OnlineID DESC");
            }
            return rowArray;
        }

        public static DataTable GetOnlineTableWithoutLimit()
        {
            string commandText = "Exec NewBTP.dbo.GetOnlineTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void InsertOnlineInfo(int intUserID, int intGameCategory, int intCategory, int intPayType, bool blnSex, string strDiskURL, string strUserName, string strPassword, string strNickName, int intClubID3, int intClubID5, string strClubName3, string strClubName5, string strClubLogo, DateTime datNow, int intMsgFlag, int intLevel, int intUnionID, string strShortName, string strGuideCode, string strQQ, string strDevCode, int intWealth, int intIschild)
        {
            string commandText = "Exec NewBTP.dbo.InsertOnlineInfo @UserID,@GameCategory,@Category,@PayType,@Sex,@DiskURL,@UserName,@Password,@NickName,@ClubID3,@ClubID5,@ClubName3,@ClubName5,@ClubLogo,@Now,@MsgFlag,@Level,@UnionID,@ShortName,@GuideCode,@QQ,@DevCode,@Wealth,@Ischild";
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@GameCategory", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4), new SqlParameter("@PayType", SqlDbType.Int, 4), new SqlParameter("@Sex", SqlDbType.Bit, 1), new SqlParameter("@DiskURL", SqlDbType.NVarChar, 500), new SqlParameter("@UserName", SqlDbType.NVarChar, 50), new SqlParameter("@Password", SqlDbType.NVarChar, 50), new SqlParameter("@NickName", SqlDbType.NVarChar, 50), new SqlParameter("@ClubID3", SqlDbType.Int, 4), new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@ClubName3", SqlDbType.NVarChar, 50), new SqlParameter("@ClubName5", SqlDbType.NVarChar, 50), new SqlParameter("@ClubLogo", SqlDbType.NVarChar, 500), new SqlParameter("@Now", SqlDbType.DateTime, 8), new SqlParameter("@MsgFlag", SqlDbType.Int, 4), 
                new SqlParameter("@Level", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@ShortName", SqlDbType.NVarChar, 50), new SqlParameter("@GuideCode", SqlDbType.NVarChar, 50), new SqlParameter("@QQ", SqlDbType.NVarChar, 50), new SqlParameter("@DevCode", SqlDbType.NVarChar, 50), new SqlParameter("@Wealth", SqlDbType.Int, 4), new SqlParameter("@Ischild", SqlDbType.TinyInt, 1)
             };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intGameCategory;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intPayType;
            commandParameters[4].Value = blnSex;
            commandParameters[5].Value = strDiskURL;
            commandParameters[6].Value = strUserName;
            commandParameters[7].Value = strPassword;
            commandParameters[8].Value = strNickName;
            commandParameters[9].Value = intClubID3;
            commandParameters[10].Value = intClubID5;
            commandParameters[11].Value = strClubName3;
            commandParameters[12].Value = strClubName5;
            commandParameters[13].Value = strClubLogo;
            commandParameters[14].Value = datNow;
            commandParameters[15].Value = intMsgFlag;
            commandParameters[0x10].Value = intLevel;
            commandParameters[0x11].Value = intUnionID;
            commandParameters[0x12].Value = strShortName;
            commandParameters[0x13].Value = strGuideCode;
            commandParameters[20].Value = strQQ;
            commandParameters[0x15].Value = strDevCode;
            commandParameters[0x16].Value = intWealth;
            commandParameters[0x17].Value = intIschild;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetHasMsgByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetHasMsgByUserID @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetNoHasMsgByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetNoHasMsgByUserID @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow UpdateGuideCode(int intUserID, string strGuideCode)
        {
            string commandText = "Exec NewBTP.dbo.UpdateGuideCode @UserID, @GuideCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ClubLogo", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strGuideCode;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


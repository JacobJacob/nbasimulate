namespace Web.DBData
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPDevManager
    {
        public static void AddClub(string strDevCode, int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.AddClub @strDevCode,@intClubID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20), new SqlParameter("@intClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddClub(string strDevCode, int intClubID, int intLevel)
        {
            string commandText = "Exec NewBTP.dbo.AddClubANDDevLvl @strDevCode,@intClubID,@Level";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@Level", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intClubID;
            commandParameters[2].Value = intLevel;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddDev(string strDevCode)
        {
            for (int i = 0; i < 14; i++)
            {
                AddClub(strDevCode, 0);
            }
        }

        public static void AddDev(string strDevCode, int intLevel)
        {
            for (int i = 0; i < 14; i++)
            {
                AddClub(strDevCode, 0, intLevel);
            }
        }

        public static void AssignDev()
        {
            string commandText = "Exec NewBTP.dbo.AssignDev";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DegradeOneClub(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.DegradeOneClub " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetBestClubByDevCode(string code)
        {
            string commandText = "Exec NewBTP.dbo.GetBestClubByDevCode '" + code + "'";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetBestClubByLevel(int intLevel)
        {
            string commandText = "Exec NewBTP.dbo.GetBestClubByLevel " + intLevel;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubCountByDevCode(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetClubCountByDevCode @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByCode(string strDevCode, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByCode @strDevCode,@intPage,@intPerPage,@intCount,@intTotal";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = intCount;
            commandParameters[4].Value = intTotal;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByDevCode(string strCode)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByDevCode @strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByDevCodeByResult(string strCode)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByCodeByResult @strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByDevCodeEnd7(string strCode)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByDevCodeEnd7 @strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByDevCodeTop7(string strCode)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByDevCodeTop7 @strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetClubTableByLvl(int intLvl)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByDevLvl " + intLvl;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetClubTableByLvl_Asc(int intLvl)
        {
            string commandText = "Exec NewBTP.dbo.GetClubTableByDevLvl_ASC " + intLvl;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevBlank()
        {
            string commandText = "Exec NewBTP.dbo.GetDevBlank";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevBlank(int intGameCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetDevBlank";
            return SqlHelper.ExecuteIntDataField(DBLogin.ConnString(intGameCategory), CommandType.Text, commandText);
        }

        public static string GetDevCodeByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCodeByClubID " + intClubID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetDevCodeByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCodeByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetDevCodeToSchedule()
        {
            string commandText = "Exec NewBTP.dbo.GetDevCodeToSchedule";
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevCount()
        {
            string commandText = "Exec NewBTP.dbo.GetDevCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetDevRowByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevRowByClubID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetDevRowByLevel(int intLevel)
        {
            string commandText = "SELECT * FROM BTP_Dev WHERE Level=" + intLevel;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevTableByLvlCID(int intLvl)
        {
            string commandText = "Exec NewBTP.dbo.GetDevTableByLvlCID " + intLvl;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevTotal()
        {
            string commandText = "Exec NewBTP.dbo.GetDevTotal";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetFillClubCountByDevCode(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetFillClubCountByDevCode @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetOrderByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetOrderByClubID " + intClubID;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRewardList(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetRewardList @DevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasMatched(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetSumWinByDevCode @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return (SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters) > 0);
        }

        public static void ResetDevWinLostScore(int intWin, int intLose, int intScore)
        {
            string commandText = string.Concat(new object[] { "UPDATE BTP_Dev SET Win=", intWin, ",Lose=", intLose, ",Score=", intScore, " WHERE ClubID>0" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ResetResult(int intClubID, int intWin, int intLose, int intScore)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ResetResult ", intClubID, ",", intWin, ",", intLose, ",", intScore });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ResetResultByDevID(int intDevID, int intWin, int intLose, int intScore)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ResetResultByDevID ", intDevID, ",", intWin, ",", intLose, ",", intScore });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void RewardDev(string strDevCode, string strDevision)
        {
            string commandText = "Exec NewBTP.dbo.RewardDev @strDevCode,@Devision";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20), new SqlParameter("@Devision", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = strDevCode;
            commandParameters[1].Value = strDevision;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void RewardDevByClubID(int intClubID, int intMoney, string strRewardName, string strBigLogo, string strDevision)
        {
            string commandText = "Exec NewBTP.dbo.RewardDevByClubID @ClubID,@Money,@RewardName,@BigLogo,@Devision";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Money", SqlDbType.Int, 4), new SqlParameter("@RewardName", SqlDbType.NVarChar, 50), new SqlParameter("@BigLogo", SqlDbType.NVarChar, 50), new SqlParameter("@Devision", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intMoney;
            commandParameters[2].Value = strRewardName;
            commandParameters[3].Value = strBigLogo;
            commandParameters[4].Value = strDevision;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void RewardDevByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.RewardDevByUserID " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetDevByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetDevByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetDevLevel()
        {
            string commandText = "Exec NewBTP.dbo.GetAllDevID";
            foreach (DataRow row in SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText).Rows)
            {
                SetDevLevel(row);
            }
            commandText = "SELECT Max(Levels) From BTP_Dev WHERE ClubID>0";
            int num = Convert.ToInt32(SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.Text, commandText));
            commandText = "UPDATE BTP_Game SET DevLevelSum=" + num;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetDevLevel(DataRow rowDev)
        {
            int num = (int) rowDev["DevID"];
            int num2 = rowDev["DevCode"].ToString().Trim().Length + 1;
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetDevLvl ", num, ",", num2 });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetDevLevelByDevID(int intDevID, string strDevCode)
        {
            int num = strDevCode.Length + 1;
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetDevLvl ", intDevID, ",", num });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetDevLogByClubID5(int intClubID, string strLogEvent)
        {
            string commandText = "Exec NewBTP.dbo.SetDevLogByClubID5 @ClubID,@LogEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int), new SqlParameter("@LogEvent", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strLogEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int SetFinanlDevByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetFinanlDevByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetTopDevResult()
        {
            string commandText = "UPDATE BTP_Dev SET Win=0,Lose=0,Score=0 WHERE LEN(DevCode)<(SELECT TOP 1 LEN(DevCode) FROM BTP_Dev WHERE ClubID>0 ORDER BY LEN(DevCode) ASC)";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateClubDevCode(int intDevID, string strCode)
        {
            string commandText = "Exec NewBTP.dbo.UpdateClubDevCode @intDevID,@strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDevID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intDevID;
            commandParameters[1].Value = strCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateHasNewMsg(int intClubID5)
        {
            string commandText = "Exec NewBTP.dbo.UpdateHasNewMsg " + intClubID5;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateReorderDevCode(int intDevID, string strCode)
        {
            string commandText = "Exec NewBTP.dbo.UpdateReorderDevCode @intDevID,@strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDevID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intDevID;
            commandParameters[1].Value = strCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateResult(int intClubID, int intWin, int intLose, int intScoreDiff)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateResult ", intClubID, ",", intWin, ",", intLose, ",", intScoreDiff });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPFriMatchManager
    {
        public static void AddDevLogByFriMatch(int intUserIDH, int intUserIDA, int intScoreH, int intScoreA, string strLvlH, string strLvlA)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDH", SqlDbType.Int, 4), new SqlParameter("@UserIDA", SqlDbType.Int, 4), new SqlParameter("@ScoreH", SqlDbType.Int, 4), new SqlParameter("@ScoreA", SqlDbType.Int, 4), new SqlParameter("@LvlH", SqlDbType.VarChar, 10), new SqlParameter("@LvlA", SqlDbType.VarChar, 10) };
            commandParameters[0].Value = intUserIDH;
            commandParameters[1].Value = intUserIDA;
            commandParameters[2].Value = intScoreH;
            commandParameters[3].Value = intScoreA;
            commandParameters[4].Value = strLvlH;
            commandParameters[5].Value = strLvlA;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddDevLogByFriMatch", commandParameters);
        }

        public static void AddMoneyForMatch(int intUserIDH, int intUserIDA, int intScoreH, int intScoreA, bool blnCanAddH, bool blnCanAddA)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDH", SqlDbType.Int, 4), new SqlParameter("@UserIDA", SqlDbType.Int, 4), new SqlParameter("@ScoreH", SqlDbType.Int, 4), new SqlParameter("@ScoreA", SqlDbType.Int, 4), new SqlParameter("@CanAddH", SqlDbType.Bit, 1), new SqlParameter("@CanAddA", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserIDH;
            commandParameters[1].Value = intUserIDA;
            commandParameters[2].Value = intScoreH;
            commandParameters[3].Value = intScoreA;
            commandParameters[4].Value = blnCanAddH;
            commandParameters[5].Value = blnCanAddA;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddMoneyForMatch", commandParameters);
        }

        public static bool CancelFriMatch(int intUserID, int intFMatchID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CancelFriMatch ", intUserID, ",", intFMatchID, ",", intType });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteFriMatchRowByCIDB(int intClubIDB)
        {
            string commandText = "Exec NewBTP.dbo.DeleteFriMatchRowByCIDB " + intClubIDB;
            SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DelOldFriWealthMatch(DateTime datTime)
        {
            try
            {
                string commandText = "SELECT * FROM BTP_FriMatch WHERE (Category=3 OR Category=4) AND CreateTime<'" + datTime.ToString().Trim() + "' AND Status=1";
                DataTable reader = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (reader != null)
                {
                    foreach (DataRow dataRow in reader.Rows)
                    {
                        int intClubID = (int)dataRow["ClubIDA"];
                        int intWealth = (int)dataRow["WealthA"];
                        DataRow row = BTPAccountManager.GetAccountRowByClubID5(intClubID);
                        if (row != null)
                        {
                            int intUserID = (int)row["UserID"];
                            row["NickName"].ToString().Trim();
                            string strContent = "约占被取消，返还游戏币" + intWealth;
                            BTPAccountManager.AddWealthByFinance(intUserID, intWealth, 1, strContent);
                        }
                    }
                }
                //reader.Close();
                string str3 = "DELETE FROM BTP_FriMatch WHERE (Category=3 OR Category=4) AND CreateTime<'" + datTime + "' AND Status=1";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str3);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                DelOldFriWealthMatch(datTime);
            }
        }

        public static void DelTrainRegRowByClubID(int intClub3ID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DelTrainRegRowByClubID ", intClub3ID, ",", intType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatch(int intType)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatch " + intType;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetFriMatchCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetFriMatchCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchTableByUserIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatchRow2ByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchRow2ByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFriMatchRowByFID(int intFMatchID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriMatchRowByFID " + intFMatchID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFriMatchRowByUserID(int intClubID, int intCategory, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFriMatchRowByUserID ", intClubID, ",", intCategory, ",", intStatus });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetFriMatchTableByUserID(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFriMatchTableByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetFriMatchTableByUserIDNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFriMatchTableByUserIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFriRowByID(int intFMatchID)
        {
            string commandText = "Exec NewBTP.dbo.GetFriRowByID " + intFMatchID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetHistoryFriMatchCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHistoryFMCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetHistoryFriMatchCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHistoryFMTableByUIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHistoryFriMatchTableByUserID(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHistoryFMTableByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetHistoryFriMatchTableByUserIDNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHistoryFMTableByUIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHistoryTableByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHistoryTableByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetRegRowByClubID(int intClubID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegRowByClubID ", intClubID, ",", intStatus });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRunMatchCount()
        {
            string commandText = "select COUNT(FMatchID)  FROM NewBTP.dbo.BTP_FriMatch WHERE Status= 2 ";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetScoreTable()
        {
            string commandText = "Exec NewBTP.dbo.GetScoreTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTrainCenterCount(int intUserID, int intType, int intPayType, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTrainCenterTableNew 1,0,0,", intUserID, ",", intType, ",", intStatus });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetTrainCenterRowByClubID(int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTrainCenterRowByClubID ", intClubID, ",", intType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTrainCenterTable(int intUserID, int intType, int intPayType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTrainCenterTable ", intUserID, ",", intType, ",", intPayType });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTrainCenterTableNew(int intDoCount, int intPageIndex, int intPageSize, int intUserID, int intType, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTrainCenterTableNew ", intDoCount, ",", intPageIndex, ",", intPageSize, ",", intUserID, ",", intType, ",", intStatus });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetWaitMatchCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetWaitMatchCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void NightUpdateOnlyOneGame()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "NightUpdateOnlyOneGame");
        }

        public static void RegTrainCenter(int intClubID, int intLevel, string strClubName, string strShortName, string strLogo, int intType, int intConsume, int intStatus)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Level", SqlDbType.Int, 4), new SqlParameter("@ClubName", SqlDbType.NVarChar, 20), new SqlParameter("@ShortName", SqlDbType.NVarChar, 10), new SqlParameter("@ClubLogo", SqlDbType.NVarChar, 50), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Consume", SqlDbType.Int, 4), new SqlParameter("@Status", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intLevel;
            commandParameters[2].Value = strClubName;
            commandParameters[3].Value = strShortName;
            commandParameters[4].Value = strLogo;
            commandParameters[5].Value = intType;
            commandParameters[6].Value = intConsume;
            commandParameters[7].Value = intStatus;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "RegTrainCenter", commandParameters);
        }

        public static int SetFriMatch(string strNickName, int intSenderID, int intCategory, int intType, string strIntro, int intWealthA, int intWealthB, int intClubAPoint, int intClubBPoint)
        {
            string commandText = "Exec NewBTP.dbo.SetFriMatch @strNickName,@intSenderID,@intCategory,@intType,@strIntro,@intWealthA,@intWealthB,@intClubAPoint,@intClubBPoint";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intSenderID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@strIntro", SqlDbType.NVarChar, 200), new SqlParameter("@intWealthA", SqlDbType.Int, 4), new SqlParameter("@intWealthB", SqlDbType.Int, 4), new SqlParameter("@intClubAPoint", SqlDbType.Int, 4), new SqlParameter("@intClubBPoint", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intSenderID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intType;
            commandParameters[4].Value = strIntro;
            commandParameters[5].Value = intWealthA;
            commandParameters[6].Value = intWealthB;
            commandParameters[7].Value = intClubAPoint;
            commandParameters[8].Value = intClubBPoint;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetMatchEnd(int intFMatchID, int intStatus, int intHomeScore, int intAwayScore, string strRepURL, string strStasURL)
        {
            string commandText = "Exec NewBTP.dbo.SetMatchEnd @intFMatchID,@intStatus,@intHomeScore,@intAwayScore,@strRepURL,@strStasURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intFMatchID", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.TinyInt, 1), new SqlParameter("@intHomeScore", SqlDbType.Int, 4), new SqlParameter("@intAwayScore", SqlDbType.Int, 4), new SqlParameter("@strRepURL", SqlDbType.NVarChar, 100), new SqlParameter("@strStasURL", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intFMatchID;
            commandParameters[1].Value = intStatus;
            commandParameters[2].Value = intHomeScore;
            commandParameters[3].Value = intAwayScore;
            commandParameters[4].Value = strRepURL;
            commandParameters[5].Value = strStasURL;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetOnlyOneGame()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SetOnlyOneGame");
        }

        public static int SetTrainMatch(string strNickName, int intSenderID, int intCategory, int intType, string strIntro)
        {
            string commandText = "Exec NewBTP.dbo.SetTrainMatch @strNickName,@intSenderID,@intCategory,@intType,@strIntro";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intSenderID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@strIntro", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intSenderID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intType;
            commandParameters[4].Value = strIntro;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void TrainCenterMatchEnd5ByFriMatchID(int intFriMatchID)
        {
            string commandText = "Exec NewBTP.dbo.TrainCenterMatchEnd5ByFriMatchID " + intFriMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void TrainCenterMatchEndByFriMatchID(int intFriMatchID)
        {
            string commandText = "Exec NewBTP.dbo.TrainCenterMatchEndByFriMatchID " + intFriMatchID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpDateFriMatchScore(int intFMatchID, int intStatus, int intScoreA, int intScoreB)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.EndFriMatch ", intFMatchID, ",", intStatus, ",", intScoreA, ",", intScoreB });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static byte UpDateFriMatchStatus(int intUserID, int intFMatchID, int intStatus, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpDateFriMatch ", intUserID, ",", intFMatchID, ",", intStatus, ",", intType });
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


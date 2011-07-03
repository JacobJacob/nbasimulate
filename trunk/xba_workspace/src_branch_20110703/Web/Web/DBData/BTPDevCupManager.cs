namespace Web.DBData
{
    using LoginParameter;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPDevCupManager
    {
        public static int CreateDevCup(int intUserID, int intUnionID, int intRegClub, string strCupName, string strPassword, string strDevCupIntro, int intRegCharge, string strLogo, string strRequirementXML, string strRewardXML, int intCupSize, DateTime datEndTime, int intCreateCharge, int intMedalCharge, int intHortationAll, string strLadderURL)
        {
            string commandText = "Exec NewBTP.dbo.CreateDevCup @intUserID,@intUnionID,@intRegClub,@strCupName,@strPassword,@strDevCupIntro,@intRegCharge,@strLogo,@strRequirementXML,@strRewardXML,@intCupSize,@datEndTime,@intCreateCharge,@intMedalCharge,@intHortationAll,@strLadderURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intRegClub", SqlDbType.Bit, 1), new SqlParameter("@strCupName", SqlDbType.NVarChar, 20), new SqlParameter("@strPassword", SqlDbType.NVarChar, 20), new SqlParameter("@strDevCupIntro", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@intRegCharge", SqlDbType.Int, 4), new SqlParameter("@strLogo", SqlDbType.NVarChar, 50), new SqlParameter("@strRequirementXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@strRewardXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@intCupSize", SqlDbType.Int, 4), new SqlParameter("@datEndTime", SqlDbType.DateTime, 8), new SqlParameter("@intCreateCharge", SqlDbType.Int, 4), new SqlParameter("@intMedalCharge", SqlDbType.Int, 4), new SqlParameter("@intHortationAll", SqlDbType.Int, 4), new SqlParameter("@strLadderURL", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            commandParameters[2].Value = intRegClub;
            commandParameters[3].Value = strCupName;
            commandParameters[4].Value = strPassword;
            commandParameters[5].Value = strDevCupIntro;
            commandParameters[6].Value = intRegCharge;
            commandParameters[7].Value = strLogo;
            commandParameters[8].Value = strRequirementXML;
            commandParameters[9].Value = strRewardXML;
            commandParameters[10].Value = intCupSize;
            commandParameters[11].Value = datEndTime;
            commandParameters[12].Value = intCreateCharge;
            commandParameters[13].Value = intMedalCharge;
            commandParameters[14].Value = intHortationAll;
            commandParameters[15].Value = strLadderURL;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int CreateNewDevCup(int intUserID, int intUnionID, int intRegClub, string strCupName, string strPassword, string strDevCupIntro, int intRegCharge, string strLogo, string strRequirementXML, string strRewardXML, int intCupSize, DateTime datEndTime, int intCreateCharge, int intMedalCharge, int intHortationAll, string strLadderURL, int intServer, int intSetID)
        {
            string commandText = "Exec NewBTP.dbo.CreateNewDevCup @intUserID,@intUnionID,@intRegClub,@strCupName,@strPassword,@strDevCupIntro,@intRegCharge,@strLogo,@strRequirementXML,@strRewardXML,@intCupSize,@datEndTime,@intCreateCharge,@intMedalCharge,@intHortationAll,@strLadderURL,@intSetID";
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intRegClub", SqlDbType.Bit, 1), new SqlParameter("@strCupName", SqlDbType.NVarChar, 20), new SqlParameter("@strPassword", SqlDbType.NVarChar, 20), new SqlParameter("@strDevCupIntro", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@intRegCharge", SqlDbType.Int, 4), new SqlParameter("@strLogo", SqlDbType.NVarChar, 50), new SqlParameter("@strRequirementXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@strRewardXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@intCupSize", SqlDbType.Int, 4), new SqlParameter("@datEndTime", SqlDbType.DateTime, 8), new SqlParameter("@intCreateCharge", SqlDbType.Int, 4), new SqlParameter("@intMedalCharge", SqlDbType.Int, 4), new SqlParameter("@intHortationAll", SqlDbType.Int, 4), new SqlParameter("@strLadderURL", SqlDbType.VarChar, 50), 
                new SqlParameter("@intSetID", SqlDbType.Int, 4)
             };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            commandParameters[2].Value = intRegClub;
            commandParameters[3].Value = strCupName;
            commandParameters[4].Value = strPassword;
            commandParameters[5].Value = strDevCupIntro;
            commandParameters[6].Value = intRegCharge;
            commandParameters[7].Value = strLogo;
            commandParameters[8].Value = strRequirementXML;
            commandParameters[9].Value = strRewardXML;
            commandParameters[10].Value = intCupSize;
            commandParameters[11].Value = datEndTime;
            commandParameters[12].Value = intCreateCharge;
            commandParameters[13].Value = intMedalCharge;
            commandParameters[14].Value = intHortationAll;
            commandParameters[15].Value = strLadderURL;
            commandParameters[0x10].Value = intSetID;
            return SqlHelper.ExecuteIntDataField(DBLogin.ConnString(intServer), CommandType.Text, commandText, commandParameters);
        }

        public static int CreateUserDevCup(int intUserID, int intUnionID, int intRegClub, string strCupName, string strPassword, string strDevCupIntro, int intRegCharge, string strLogo, string strRequirementXML, string strRewardXML, int intCupSize, DateTime datEndTime, int intCreateCharge, int intMedalCharge, int intHortationAll, string strLadderURL)
        {
            string commandText = "Exec NewBTP.dbo.CreateUserDevCup @intUserID,@intUnionID,@intRegClub,@strCupName,@strPassword,@strDevCupIntro,@intRegCharge,@strLogo,@strRequirementXML,@strRewardXML,@intCupSize,@datEndTime,@intCreateCharge,@intMedalCharge,@intHortationAll,@strLadderURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intRegClub", SqlDbType.Bit, 1), new SqlParameter("@strCupName", SqlDbType.NVarChar, 20), new SqlParameter("@strPassword", SqlDbType.NVarChar, 20), new SqlParameter("@strDevCupIntro", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@intRegCharge", SqlDbType.Int, 4), new SqlParameter("@strLogo", SqlDbType.NVarChar, 50), new SqlParameter("@strRequirementXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@strRewardXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@intCupSize", SqlDbType.Int, 4), new SqlParameter("@datEndTime", SqlDbType.DateTime, 8), new SqlParameter("@intCreateCharge", SqlDbType.Int, 4), new SqlParameter("@intMedalCharge", SqlDbType.Int, 4), new SqlParameter("@intHortationAll", SqlDbType.Int, 4), new SqlParameter("@strLadderURL", SqlDbType.VarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            commandParameters[2].Value = intRegClub;
            commandParameters[3].Value = strCupName;
            commandParameters[4].Value = strPassword;
            commandParameters[5].Value = strDevCupIntro;
            commandParameters[6].Value = intRegCharge;
            commandParameters[7].Value = strLogo;
            commandParameters[8].Value = strRequirementXML;
            commandParameters[9].Value = strRewardXML;
            commandParameters[10].Value = intCupSize;
            commandParameters[11].Value = datEndTime;
            commandParameters[12].Value = intCreateCharge;
            commandParameters[13].Value = intMedalCharge;
            commandParameters[14].Value = intHortationAll;
            commandParameters[15].Value = strLadderURL;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int DeleteDevCup(int intUserID, int intDevCupID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteDevCup ", intUserID, ",", intDevCupID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int DeleteUserDevCup(int intUserID, int intDevCupID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteUserDevCup ", intUserID, ",", intDevCupID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevCupAccountByUserID(int intUser)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableByUserID 1,1,1," + intUser;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevCupCount(int intState)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableByPage 1,1,1," + intState;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevCupCountByUID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableByUID " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetDevCupRowByDevCupID(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupRowByDevCupID " + intDevCupID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevCupTableByPage(int DoCountint, int PageIndex, int PageSize, int intStatic)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevCupTableByPage ", DoCountint, ",", PageIndex, ",", PageSize, ",", intStatic });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevCupTableByStatus(int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableByStatus " + intStatus;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetDevCupTableByUID(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevCupTableByUID ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevCupTableByUserID(int DoCountint, int PageIndex, int PageSize, int UserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevCupTableByUserID ", DoCountint, ",", PageIndex, ",", PageSize, ",", UserID });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDevCupTableToArrage()
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableToArrage";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRunDevCupTable()
        {
            string commandText = "Exec NewBTP.dbo.GetRunDevCupTable @strNow";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNow", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = DateTime.Now;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetUserDevCupCount(int intUserID, int intPageIndex, int intPageSize, int intDoCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUserDevCupTable ", intUserID, ",", intPageIndex, ",", intPageSize, ",", intDoCount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUserDevCupTable(int intUserID, int intPageIndex, int intPageSize, int intDoCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUserDevCupTable ", intUserID, ",", intPageIndex, ",", intPageSize, ",", intDoCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int ModifyDevCup(int intUserID, int intDevCupID, int intIsSelfUnion, string strPassword, string strDevCupIntro, string strRequirement)
        {
            string commandText = "Exec NewBTP.dbo.ModifyDevCup @intUserID,@intDevCupID,@intIsSelfUnion,@strPassword,@strDevCupIntro,@strRequirement";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intDevCupID", SqlDbType.Int, 4), new SqlParameter("@intIsSelfUnion", SqlDbType.Bit, 1), new SqlParameter("@strPassword", SqlDbType.NVarChar, 20), new SqlParameter("@strDevCupIntro", SqlDbType.NVarChar, 400), new SqlParameter("@strRequirement", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intDevCupID;
            commandParameters[2].Value = intIsSelfUnion;
            commandParameters[3].Value = strPassword;
            commandParameters[4].Value = strDevCupIntro;
            commandParameters[5].Value = strRequirement;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int ModifyUserDevCup(int intUserID, int intDevCupID, int intIsSelfUnion, string strPassword, string strDevCupIntro, string strRequirement)
        {
            string commandText = "Exec NewBTP.dbo.ModifyUserDevCup @intUserID,@intDevCupID,@intIsSelfUnion,@strPassword,@strDevCupIntro,@strRequirement";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intDevCupID", SqlDbType.Int, 4), new SqlParameter("@intIsSelfUnion", SqlDbType.Bit, 1), new SqlParameter("@strPassword", SqlDbType.NVarChar, 20), new SqlParameter("@strDevCupIntro", SqlDbType.NVarChar, 400), new SqlParameter("@strRequirement", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intDevCupID;
            commandParameters[2].Value = intIsSelfUnion;
            commandParameters[3].Value = strPassword;
            commandParameters[4].Value = strDevCupIntro;
            commandParameters[5].Value = strRequirement;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetChampion(int intDevCupID, int intChampionID, string strChampionName)
        {
            string commandText = "Exec NewBTP.dbo.SetDevCupChampion @DevCupID,@ChampionID,@ChampionName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@DevCupID", SqlDbType.Int, 4), new SqlParameter("@ChampionID", SqlDbType.Int, 4), new SqlParameter("@ChampionName", SqlDbType.NVarChar, 30) };
            commandParameters[0].Value = intDevCupID;
            commandParameters[1].Value = intChampionID;
            commandParameters[2].Value = strChampionName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetRoundByCupID(int intDevCupID, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetRoundByDevCupID ", intDevCupID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetStatusByCupID(int intDevCupID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetStatusByDevCupID ", intDevCupID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPCupManager
    {
        public static void AddCup(int intSetID, int intCategory, int intLevels, int intUnionID, string strName, string strIntroduction, int intMoneyCost, string strSmallLogo, string strBigLogo, string strRequirementXML, string strRewardXML, int intRound, int intCapacity, string strEndRegTime, string strMatchTime, int intCoin, string strCupLadder, int intTicketCategory)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.AddCup @intSetID,@intCategory,@intLevels,@intUnionID,@strName,@strIntroduction,@intMoneyCost,@strSmallLogo,@strBigLogo,@strRequirementXML,@strRewardXML,@intRound,@intCapacity,@strEndRegTime,@strMatchTime,@intCoin,@strCupLadder,@intTicketCategory";
                SqlParameter[] commandParameters = new SqlParameter[] { 
                    new SqlParameter("@intSetID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@intLevels", SqlDbType.Int, 4), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@strName", SqlDbType.NVarChar, 50), new SqlParameter("@strIntroduction", SqlDbType.NVarChar, 0x7d0), new SqlParameter("@intMoneyCost", SqlDbType.Int, 4), new SqlParameter("@strSmallLogo", SqlDbType.NVarChar, 50), new SqlParameter("@strBigLogo", SqlDbType.NVarChar, 50), new SqlParameter("@strRequirementXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@strRewardXML", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@intRound", SqlDbType.Int, 4), new SqlParameter("@intCapacity", SqlDbType.Int, 4), new SqlParameter("@strEndRegTime", SqlDbType.DateTime, 8), new SqlParameter("@strMatchTime", SqlDbType.DateTime, 8), new SqlParameter("@intCoin", SqlDbType.TinyInt, 1), 
                    new SqlParameter("@strCupLadder", SqlDbType.NVarChar, 50), new SqlParameter("@intTicketCategory", SqlDbType.TinyInt, 1)
                 };
                commandParameters[0].Value = intSetID;
                commandParameters[1].Value = intCategory;
                commandParameters[2].Value = intLevels;
                commandParameters[3].Value = intUnionID;
                commandParameters[4].Value = strName;
                commandParameters[5].Value = strIntroduction;
                commandParameters[6].Value = intMoneyCost;
                commandParameters[7].Value = strSmallLogo;
                commandParameters[8].Value = strBigLogo;
                commandParameters[9].Value = strRequirementXML;
                commandParameters[10].Value = strRewardXML;
                commandParameters[11].Value = intRound;
                commandParameters[12].Value = intCapacity;
                commandParameters[13].Value = strEndRegTime;
                commandParameters[14].Value = strMatchTime;
                commandParameters[15].Value = intCoin;
                commandParameters[0x10].Value = strCupLadder;
                commandParameters[0x11].Value = intTicketCategory;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                AddCup(intSetID, intCategory, intLevels, intUnionID, strName, strIntroduction, intMoneyCost, strSmallLogo, strBigLogo, strRequirementXML, strRewardXML, intRound, intCapacity, strEndRegTime, strMatchTime, intCoin, strCupLadder, intTicketCategory);
            }
        }

        public static DataTable GetChampionList(int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetChampionList ", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetChampionListNew(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetChampionListNew ", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetChampionUserCount()
        {
            string commandText = "Exec NewBTP.dbo.GetChampionUserCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetChampionUserCountNew()
        {
            string commandText = "Exec NewBTP.dbo.GetChampionListNew 0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCupRowByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetCupRowByCupID " + intCupID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCupRowByCupIDCategory(int intCupID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetCupRowByCupIDCategory ", intCupID, ",", intCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCupRowBySetID(int intUserID, int intSetID, string strCupIDs)
        {
            string commandText = "Exec NewBTP.dbo.GetCupRowBySetID @intUserID,@intSetID,@strCupIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intSetID", SqlDbType.Int, 4), new SqlParameter("@strCupIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intSetID;
            commandParameters[2].Value = strCupIDs;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetCupTableByCategory(int intCategory)
        {
            string commandText = "Select * From BTP_Cup where Category =" + intCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCupTableByStatus(int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.GetCupTableByStatus " + intStatus;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCupTableToArrage()
        {
            string commandText = "Exec NewBTP.dbo.GetCupTableToArrage";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetImperialCupCount()
        {
            string commandText = "Exec NewBTP.dbo.GetImperialCupCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetImperialCupCountNew()
        {
            string commandText = "Exec NewBTP.dbo.GetImperialCupListNew 0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetImperialCupList(int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetImperialCupList ", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetImperialCupListNew(int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetImperialCupListNew ", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRunCupTable()
        {
            string commandText = "Exec NewBTP.dbo.GetRunCupTable @strNow";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNow", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = DateTime.Now;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSetIDsTable(string strCupIDs)
        {
            string commandText = "Exec NewBTP.dbo.GetSetIDsTable @strCupIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strCupIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strCupIDs;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSetIDTable(string strSetIDs)
        {
            string commandText = "Exec NewBTP.dbo.GetSetIDTable @strSetIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strSetIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strSetIDs;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetTopSetID()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetTopSetID";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return GetTopSetID();
            }
        }

        public static int GetUnionCupCount(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupCount ", intUserID, ",", intCategory });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionCupCountNew(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupListByUIDNew ", intUserID, ",", intCategory, ",0,0,1" });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionCupListByUserID(int intUserID, int intCategory, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupListByUserID ", intUserID, ",", intCategory, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUnionCupListByUserIDNew(int intUserID, int intCategory, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionCupListByUIDNew ", intUserID, ",", intCategory, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetChampion(int intCupID, int intChampionID, string strChampionName)
        {
            string commandText = "Exec NewBTP.dbo.SetChampion @CupID,@ChampionID,@ChampionName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@CupID", SqlDbType.Int, 4), new SqlParameter("@ChampionID", SqlDbType.Int, 4), new SqlParameter("@ChampionName", SqlDbType.NVarChar, 30) };
            commandParameters[0].Value = intCupID;
            commandParameters[1].Value = intChampionID;
            commandParameters[2].Value = strChampionName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetRoundByCupID(int intCupID, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetRoundByCupID ", intCupID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetStatusByCupID(int intCupID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetStatusByCupID ", intCupID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


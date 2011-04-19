namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPFinanceManager
    {
        public static void AddFinance(int intUserID, int intTCategory, int intDCategory, int intMoney, int intFinanceType, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.AddFinance @intUserID,@intTCategory,@intDCategory,@intMoney,@intFinanceType,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intTCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intDCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intMoney", SqlDbType.Int, 4), new SqlParameter("@intFinanceType", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intTCategory;
            commandParameters[2].Value = intDCategory;
            commandParameters[3].Value = intMoney;
            commandParameters[4].Value = intFinanceType;
            commandParameters[5].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddTFinanceSeason(int intUserID, int Income, int Outcome, int intSeason, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddTFinanceSeason ", intUserID, ",", Income, ",", Outcome, ",", intSeason, ",", intCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteFinance(long longDFinanceID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteFinance " + longDFinanceID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteFinanceByUserID(int intUserID)
        {
            string commandText = "DELETE FROM BTP_DFinance WHERE UserID=" + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            commandText = "DELETE FROM BTP_TFinance WHERE UserID=" + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteTurnFinance(int intSeason)
        {
            string commandText = "Exec NewBTP.dbo.DeleteTurnFinance " + intSeason;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAllFinanceCount(string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.GetAllFinanceCount @strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetAllFinanceCountNew(string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.GetAllFinanceListNew @strNickName,0,0,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetAllFinanceList(string strNickName, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = "Exec NewBTP.dbo.GetAllFinanceList @strNickName,@intPage,@intPerPage,@intTotal,@intCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = intTotal;
            commandParameters[4].Value = intCount;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetAllFinanceListNew(string strNickName, int intPage, int intPerPage)
        {
            string commandText = "Exec NewBTP.dbo.GetAllFinanceListNew @strNickName,@intPage,@intPerPage,0";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetAllTFinanceCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetTFinanceCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAllTFinanceCountNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetTFinanceListNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllTFinanceList(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTFinanceList ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetAllTFinanceListNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTFinanceListNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDFinanceCount(int intUserID, int intTFinanceID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDFinanceCount ", intUserID, ",", intTFinanceID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDFinanceCountNew(int intUserID, int intTFinanceID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDFinanceListNew ", intUserID, ",", intTFinanceID, ",0,0,1" });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetDFinanceList(int intUserID, int intTFinanceID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDFinanceList ", intUserID, ",", intTFinanceID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetDFinanceListNew(int intUserID, int intTFinanceID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDFinanceListNew ", intUserID, ",", intTFinanceID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFinanceIDRow(int intUserID, int intSeason, int intTurn)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetFinanceIDRow ", intUserID, ",", intSeason, ",", intTurn });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetTFinanceSeason(int intUserID, int intSeason)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTFinanceSeason ", intUserID, ",", intSeason });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTurnTFinanceCount(int intUserID, int intSeason)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTurnTFinanceCount ", intUserID, ",", intSeason });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTurnTFinanceCountNew(int intUserID, int intSeason)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTurnTFinanceListNew ", intUserID, ",", intSeason, ",0,0,1" });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTurnTFinanceList(int intUserID, int intSeason, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTurnTFinanceList ", intUserID, ",", intSeason, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTurnTFinanceListNew(int intUserID, int intSeason, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTurnTFinanceListNew ", intUserID, ",", intSeason, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ManagerAddFinance(int intUserID, int intTCategory, int intDCategory, int intMoney, int intFinanceType, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.ManagerAddFinance @intUserID,@intTCategory,@intDCategory,@intMoney,@intFinanceType,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intTCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intDCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intMoney", SqlDbType.Int, 4), new SqlParameter("@intFinanceType", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intTCategory;
            commandParameters[2].Value = intDCategory;
            commandParameters[3].Value = intMoney;
            commandParameters[4].Value = intFinanceType;
            commandParameters[5].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void Payment(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.Payment " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


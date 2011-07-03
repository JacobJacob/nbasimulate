namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPCupRegManager
    {
        public static void DelCupReg()
        {
            string commandText = "Exec NewBTP.dbo.DelCupReg";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteCupRegByRegID(int intRegID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteCupRegByRegID " + intRegID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAliveRegTableByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetAliveRegTableByCupID " + intCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetCupSize(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetCupSize " + intCupID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDeadRound(int intCupID, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDeadRound ", intCupID, ",", intClubID });
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegByCupIDEndRound(int intCupID, int intDeadRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegByCupIDEndRound ", intCupID, ",", intDeadRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCount(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegCupCount " + intCupID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegCupCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCountNew(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegCupListNew " + intCupID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCountUIDCategory(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupCountUIDCategory ", intUserID, ",", intCategory });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCountUIDCategoryNew(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupListByUIDCatNew ", intUserID, ",", intCategory, ",0,0,1" });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegCupList(int intCupID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupList ", intCupID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegCupListByUIDCategory(int intUserID, int intCategory, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupListByUIDCategory ", intUserID, ",", intCategory, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetRegCupListByUIDCategoryNew(int intUserID, int intCategory, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupListByUIDCatNew ", intUserID, ",", intCategory, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegCupListByUserID(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupListByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetRegCupListNew(int intCupID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegCupListNew ", intCupID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegKiloCupCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegKiloCupCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegKiloCupCountNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegKiloCupListNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegKiloCupList(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegKiloCupList ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetRegKiloCupListNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegKiloCupListNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegTableByCupID " + intCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupIDDeadRound(int intCupID, int intDeadRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegTableByCupIDDeadRound ", intCupID, ",", intDeadRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void InitCupRegByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.InitCupRegByCupID " + intCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetClubDeadRound(int intCupID, int intClubID, int intRound)
        {
            string str = BTPAccountManager.GetCupIDsByClubID(intClubID).Replace("|", ",");
            if (str.IndexOf(",") != -1)
            {
                Cuter cuter = new Cuter(str);
                cuter.DelItem(intCupID.ToString());
                str = cuter.ToString().Replace(",", "|");
            }
            string commandText = "Exec NewBTP.dbo.SetCupIDsByClubID @intClubID ,@strCupIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strCupIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = str;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetClubDeadRound ", intCupID, ",", intClubID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetCodeByRegID(int intRegID, string strCode)
        {
            string commandText = "Exec NewBTP.dbo.SetCodeByRegID @intRegID,@strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intRegID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intRegID;
            commandParameters[1].Value = strCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int SetCupReg(int intUserID, int intCupID, int intClubID, string strClubName, string strNickName, int intLevel, string strClubLogo, int intScore, int intRank, string strBaseCode)
        {
            string commandText = "Exec NewBTP.dbo.SetCupReg @intUserID,@intCupID,@intClubID,@strClubName,@strNickName,@intLevel,@strClubLogo,@intScore ,@intRank ,@strBaseCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCupID", SqlDbType.Int, 4), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 30), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intLevel", SqlDbType.Int, 4), new SqlParameter("@strClubLogo", SqlDbType.NChar, 50), new SqlParameter("@intScore", SqlDbType.Int, 4), new SqlParameter("@intRank", SqlDbType.Int, 4), new SqlParameter("@strBaseCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCupID;
            commandParameters[2].Value = intClubID;
            commandParameters[3].Value = strClubName;
            commandParameters[4].Value = strNickName;
            commandParameters[5].Value = intLevel;
            commandParameters[6].Value = strClubLogo;
            commandParameters[7].Value = intScore;
            commandParameters[8].Value = intRank;
            commandParameters[9].Value = strBaseCode;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


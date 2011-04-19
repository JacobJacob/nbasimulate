namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPDevCupRegManager
    {
        public static int AddDevCupReg(int intDevCupID, int intUserID, int intClubID, string strClubName, string strShortName, string strDevCode, string strClubLogo, int intRank, int intUserRank, string strPassWord)
        {
            string commandText = "Exec NewBTP.dbo.AddDevCupReg @intDevCupID,@intUserID,@intClubID,@strClubName,@strShortName,@strDevCode,@strClubLogo,@intRank,@intUserRank,@strPassWord";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDevCupID", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 30), new SqlParameter("@strShortName", SqlDbType.NChar, 10), new SqlParameter("@strDevCode", SqlDbType.NChar, 20), new SqlParameter("@strClubLogo", SqlDbType.NChar, 50), new SqlParameter("@intRank", SqlDbType.Int, 4), new SqlParameter("@intUserRank", SqlDbType.Int, 4), new SqlParameter("@strPassWord", SqlDbType.NChar, 50) };
            commandParameters[0].Value = intDevCupID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = intClubID;
            commandParameters[3].Value = strClubName;
            commandParameters[4].Value = strShortName;
            commandParameters[5].Value = strDevCode;
            commandParameters[6].Value = strClubLogo;
            commandParameters[7].Value = intRank;
            commandParameters[8].Value = intUserRank;
            commandParameters[9].Value = strPassWord;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool CheckDevCupReg(int intUserID, int intSetID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckDevCupReg ", intUserID, ",", intSetID });
            return (SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) == 1);
        }

        public static int DelDevCupReg(int intUserID, int intDevCupID, int intUnionUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DelDevCupReg ", intUserID, ",", intDevCupID, ",", intUnionUserID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteCupRegByRegID(int intRegID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteDevCupRegByRegID " + intRegID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int DelUserDevCupReg(int intUserID, int intDevCupID, int intUnionUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DelUserDevCupReg ", intUserID, ",", intDevCupID, ",", intUnionUserID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAliveRegTableByCupID(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetAliveRegTableByDevCupID " + intDevCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetCupSize(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupSize " + intDevCupID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetDevCupTableByDevCupID(int DoCountint, int PageIndex, int PageSize, int intDevCupID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevCupTableByDevCupID ", DoCountint, ",", PageIndex, ",", PageSize, ",", intDevCupID });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevCupUserCount(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupTableByDevCupID 1,1,1," + intDevCupID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegByDevCupIDEndRound(int intDevCupID, int intDeadRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegByDevCupIDEndRound ", intDevCupID, ",", intDeadRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetRegRowByDevCupIDUserID(int intDevCupID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetRegRowByDevCupIDUserID ", intDevCupID, ",", intUserID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupID(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetRegTableByDevCupID " + intDevCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupIDOrderByDeadRound(int intDIYCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetRTableByDCupIDOrdDR " + intDIYCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void InitCupRegByCupID(int intDevCupID)
        {
            string commandText = "Exec NewBTP.dbo.InitDevCupRegByCupID " + intDevCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetClubDeadRound(int intDevCupID, int intClubID, int intRound)
        {
            string str = BTPAccountManager.GetDevCupIDsByClubID(intClubID).Replace("|", ",");
            if (str.IndexOf(",") != -1)
            {
                Cuter cuter = new Cuter(str);
                cuter.DelItem(intDevCupID.ToString());
                str = cuter.ToString().Replace(",", "|");
            }
            string commandText = "Exec NewBTP.dbo.SetDevCupIDsByClubID @intClubID,@strDevCupIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strDevCupIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = str;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetDevClubDeadRound ", intDevCupID, ",", intClubID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetCodeByRegID(int intRegID, string strCode)
        {
            string commandText = "Exec NewBTP.dbo.SetCodeByDevRegID @intRegID,@strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intRegID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intRegID;
            commandParameters[1].Value = strCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetDevCupDeadRound(int intDevCupID, int intUserID, int intRound)
        {
            string str = BTPAccountManager.GetAccountRowByUserID(intUserID)["DevCupIDs"].ToString().Replace("|", ",");
            if (str.IndexOf(",") != -1)
            {
                Cuter cuter = new Cuter(str);
                cuter.DelItem(intDevCupID.ToString());
                str = cuter.ToString().Replace(",", "|");
            }
            string commandText = "Exec NewBTP.dbo.SetDevCupIDsByUserID @intUserID,@strDevCupIDs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strDevCupIDs", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = str;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            if (intRound >= 1)
            {
                commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetDevCupDeadRound ", intDevCupID, ",", intUserID, ",", intRound });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
        }
    }
}


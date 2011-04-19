namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPXCupRegManager
    {
        public static void AddCupReg(int intCategory, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddXCupReg ", intCategory, ",", intClubID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteCupRegByRegID(int intRegID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteXCupRegByRegID " + intRegID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAliveRegTableByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetAliveXRegTableByCupID " + intCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetRegCupCount(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetXRegCupCount " + intCupID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.GetXRegTableByCupID " + intCupID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupIDDeadRound(int intCupID, int intDeadRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXRegTableByCupIDDeadRound ", intCupID, ",", intDeadRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetRegTableByCupIDEndRound(int intCupID, int intDeadRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXRegTableByCupIDEndRound ", intCupID, ",", intDeadRound });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetXCupRegRowByClubID(int intClubID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXCupRegRowByClubID", commandParameters);
        }

        public static void InitCupRegByCupID(int intCupID)
        {
            string commandText = "Exec NewBTP.dbo.InitXCupRegByCupID " + intCupID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetClubDeadRound(int intCupID, int intClubID, int intRound)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetXClubDeadRound ", intCupID, ",", intClubID, ",", intRound });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetCodeByRegID(int intRegID, string strCode)
        {
            string commandText = "Exec NewBTP.dbo.SetCodeByXRegID @intRegID,@strCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intRegID", SqlDbType.Int, 4), new SqlParameter("@strCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intRegID;
            commandParameters[1].Value = strCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


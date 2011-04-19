namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPOldPlayerManager
    {
        public static DataTable GetCanHonorPlayer(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetCanHonorPlayer " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCanHonorPlayer3(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetCanHonorPlayer3 " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCanHonorPlayer5(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetCanHonorPlayer5 " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetHPlayerCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHPlayerCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetHPlayerCountNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHPlayerListNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHPlayerList(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHPlayerList ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetHPlayerListNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHPlayerListNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetPlayerRowByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerRowByPlayerID " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetOldPlayer(long longPlayerID, int intUserID, int intCategory, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.SetOldPlayer @PlayerID,@UserID,@Category,@Remark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Remark", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = strRemark;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


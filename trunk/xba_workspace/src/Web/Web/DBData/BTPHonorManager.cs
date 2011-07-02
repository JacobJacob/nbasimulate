namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPHonorManager
    {
        public static int GetHonorCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHonorCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetHonorCountNew(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHonorListNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHonorList(int intUserID, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHonorList ", intUserID, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHonorListNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetHonorListNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetHonorTableByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetHonorTableByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetHonorTableByUserIDRemark(int intUserID, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.GetHonorTableByUserIDRemark @intUserID,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strRemark", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strRemark;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


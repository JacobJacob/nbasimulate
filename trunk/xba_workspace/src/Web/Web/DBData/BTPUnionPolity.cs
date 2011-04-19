namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUnionPolity
    {
        public static int DelateMaster(int intUserID, string strReason)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Reason", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strReason;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DelateMaster", commandParameters);
        }

        public static int DelateMasterReg(int intUserID, int intPolityID, bool blnCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PolityID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPolityID;
            commandParameters[2].Value = blnCategory;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DelateMasterReg", commandParameters);
        }

        public static DataRow GetDelMasterRow(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[2];
            commandParameters[0] = new SqlParameter("@UnionID", SqlDbType.Int, 4);
            commandParameters[0].Value = intUnionID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetDelMasterRow", commandParameters);
        }

        public static void UpdateDelateMaster()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateDelateMaster");
        }
    }
}


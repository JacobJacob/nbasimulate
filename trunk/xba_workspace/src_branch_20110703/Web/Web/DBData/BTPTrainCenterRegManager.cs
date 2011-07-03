namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPTrainCenterRegManager
    {
        public static void SetTrainCenterMatch()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SetTrainCenterMatch");
        }
    }
}


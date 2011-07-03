namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPUnionMsgSendManager
    {
        public static void SendUnionMsg()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendUnionMsg");
        }
    }
}


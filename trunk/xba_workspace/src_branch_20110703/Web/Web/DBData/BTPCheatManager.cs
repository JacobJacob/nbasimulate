namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPCheatManager
    {
        public static void AddCheatScore(int intUserIDA, int intUserIDB, int intScore)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddCheatScore ", intUserIDA, ",", intUserIDB, ",", intScore });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetCheatScore(int intUserIDA, int intUserIDB)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetCheatScore ", intUserIDA, ",", intUserIDB });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


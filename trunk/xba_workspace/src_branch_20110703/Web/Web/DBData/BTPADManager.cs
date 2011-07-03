namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPADManager
    {
        public static void AddAD(string strADName, string strLogoURL, int intTurns, int intPay)
        {
            string commandText = "Exec NewBTP.dbo.AddAD @ADName,@LogoURL,@Turns,@Pay";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ADName", SqlDbType.NVarChar, 50), new SqlParameter("@LogoURL", SqlDbType.NVarChar, 100), new SqlParameter("@Turns", SqlDbType.TinyInt, 1), new SqlParameter("@Pay", SqlDbType.Int, 4) };
            commandParameters[0].Value = strADName;
            commandParameters[1].Value = strLogoURL;
            commandParameters[2].Value = intTurns;
            commandParameters[3].Value = intPay;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteAD(int intADID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteAD " + intADID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetADRowByADID(int intADID)
        {
            string commandText = "Exec NewBTP.dbo.GetADRowByADID " + intADID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetADTable()
        {
            string commandText = "Exec NewBTP.dbo.GetADTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void NextTurn()
        {
            string commandText = "Exec NewBTP.dbo.NextTurnAD";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetPrice(int intADID, int intPay)
        {
            string commandText = "Exec NewBTP.dbo.SetPrice";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetRandADPay()
        {
            string commandText = "Exec NewBTP.dbo.SetRandADPay";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetTurn(int intADID, int intTurns)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetTurns ", intADID, ",", intTurns });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


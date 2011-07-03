namespace Web.DBData
{
    using LoginParameter;
    using System;
    using System.Data;
    using Web.DBConnection;

    public class BTPParameterManager
    {
        public static string GetBadWord()
        {
            string commandText = "SELECT TOP 1 BadWord FROM BTP_Parameter";
            return SqlHelper.ExecuteStringReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetParameterRow()
        {
            string commandText = "Exec NewBTP.dbo.GetParameterRow";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetParameterRow(int intServerType)
        {
            string commandText = "Exec NewBTP.dbo.GetParameterRow";
            return SqlHelper.ExecuteDataRow(DBLogin.ConnString(intServerType), CommandType.Text, commandText);
        }

        public static void SetCanBeginGuess(int intType)
        {
            string commandText = "UPDATE BTP_Parameter SET CanBeginGuess=" + intType;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


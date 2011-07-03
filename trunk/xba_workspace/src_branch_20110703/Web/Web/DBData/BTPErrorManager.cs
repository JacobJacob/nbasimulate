namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPErrorManager
    {
        public static void AddError(string strType, string strDetial)
        {
            string commandText = "Exec NewBTP.dbo.AddError @strType,@strDetial";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strType", SqlDbType.NVarChar, 50), new SqlParameter("@strDetial", SqlDbType.NVarChar, 0xfa0) };
            commandParameters[0].Value = strType;
            commandParameters[1].Value = strDetial;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            Console.WriteLine("Error: " + strType + "\n" + strDetial);
        }
    }
}


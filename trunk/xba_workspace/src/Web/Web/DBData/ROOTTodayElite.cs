namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTTodayElite
    {
        public static void AddTodayElite(string strTitle, string strURL, string strImgURL, string strShortName, int intCategory, int intState, int intType)
        {
            string commandText = "Exec ROOT_Data.dbo.AddTodayElite @strTitle,@strURL,@strImgURL,@strShortName,@intCategory,@intState,@intType";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strTitle", SqlDbType.NVarChar, 200), new SqlParameter("@strURL", SqlDbType.NVarChar, 100), new SqlParameter("@strImgURL", SqlDbType.NVarChar, 100), new SqlParameter("@strShortName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@intState", SqlDbType.Bit, 1), new SqlParameter("@intType", SqlDbType.Int, 4) };
            commandParameters[0].Value = strTitle;
            commandParameters[1].Value = strURL;
            commandParameters[2].Value = strImgURL;
            commandParameters[3].Value = strShortName;
            commandParameters[4].Value = intCategory;
            commandParameters[5].Value = intState;
            commandParameters[6].Value = intType;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteTodayElite(int intEliteTopicID)
        {
            string commandText = "Exec ROOT_Data.dbo.DeleteTodayElite " + intEliteTopicID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


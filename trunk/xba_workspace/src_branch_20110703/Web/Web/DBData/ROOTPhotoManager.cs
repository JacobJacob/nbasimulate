namespace Web.DBData
{
    using System;
    using System.Data;
    using Web.DBConnection;

    public class ROOTPhotoManager
    {
        public static int GetPhotoCount()
        {
            string commandText = "EXEC ROOT_Data.dbo.GetPhotoCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetPhotoTableTop2()
        {
            string commandText = "EXEC ROOT_Data.dbo.GetPhotoTableTop2";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTBoardManager
    {
        public static DataRow GetBoardByBoardID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardByBoardID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetBoardByTopID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardByTopID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetBoardCountByBoardID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardCountByBoardID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetBoardCountByBoardIDNew(string strBoardID, int intElite)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardTableByBoardIDNew @strBoardID,0,0,1,@intElite";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intElite", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intElite;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetBoardCountByTopID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardCountByTopID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static string GetBoardNameByBoardID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardNameByBoardID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetBoardRowByBoardID(string strBoardID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardRowByBoardID @strBoardID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strBoardID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetBoardTableByBoardID(string strBoardID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardTableByBoardID @strBoardID,@intPage,@intPerPage,@intCount,@intTotal";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = intCount;
            commandParameters[4].Value = intTotal;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetBoardTableByBoardIDNew(string strBoardID, int intPage, int intPerPage, int intElite)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardTableByBoardIDNew @strBoardID,@intPage,@intPerPage,0,@intElite";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intElite", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = intElite;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetMasterNickName(string strMaster)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMasterNickName @strMaster";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strMaster", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strMaster;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetNewsByBoardID()
        {
            string commandText = "Exec ROOT_Data.dbo.GetNewsByBoardID";
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetTodayCountByBordID(string strBoardID, DateTime dtBeginTime, DateTime dtEndTime)
        {
            string commandText = "Exec ROOT_Data.dbo.GetTodayCountByBordID @strBoardID,@dtBeginTime,@dtEndTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strBoardID", SqlDbType.NVarChar, 50), new SqlParameter("@dtBeginTime", SqlDbType.DateTime, 8), new SqlParameter("@dtEndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = strBoardID;
            commandParameters[1].Value = dtBeginTime;
            commandParameters[2].Value = dtEndTime;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }
    }
}


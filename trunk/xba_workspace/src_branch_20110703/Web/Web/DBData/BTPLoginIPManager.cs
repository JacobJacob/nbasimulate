namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPLoginIPManager
    {
        public static void EmptyLoginIPStore(int intDays)
        {
            string commandText = "DELETE FROM BTP_LoginIPStore WHERE CreateDate<'" + DateTime.Now.AddDays((double) -intDays).ToString() + "'";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void EmptyLoginIPTable()
        {
            string commandText = "Exec NewBTP.dbo.EmptyLoginIPTableAll";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void EmptyLoginIPTable(DateTime datTime)
        {
            DateTime time = datTime;
            DateTime time2 = datTime.AddDays(1.0);
            string commandText = "Exec NewBTP.dbo.EmptyLoginIPTable @StartTime,@EndTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StartTime", SqlDbType.DateTime, 8), new SqlParameter("@EndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = time;
            commandParameters[1].Value = time2;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void EmptyLoginIPTable(string strIPHead)
        {
            string commandText = "Exec NewBTP.dbo.EmptyLoginIPTableByIP @strIPHead";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strIPHead", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strIPHead;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void EmptyLoginIPTable(string strIPHead, DateTime datTime)
        {
            DateTime time = datTime;
            DateTime time2 = datTime.AddDays(1.0);
            string commandText = "Exec NewBTP.dbo.EmptyLoginIPTableByIPTime @strIPHead,@StartTime,@EndTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IPHead", SqlDbType.NVarChar, 50), new SqlParameter("@StartTime", SqlDbType.DateTime, 8), new SqlParameter("@strEndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = strIPHead;
            commandParameters[1].Value = time;
            commandParameters[2].Value = time2;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetLoginIPTable(string strIPHead)
        {
            string commandText = "Exec NewBTP.dbo.GetLoginIPTableByIP @strIPHead";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strIPHead", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strIPHead;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetLoginIPTable(string strStart, bool blnFix, DateTime datTime)
        {
            string str = datTime.ToString();
            string str2 = datTime.AddDays(1.0).ToString();
            string commandText = "";
            if (blnFix)
            {
                commandText = "Exec NewBTP.dbo.GetLoginIPTableA @strStart,@strStartTime,@strEndTime";
            }
            else
            {
                commandText = "Exec NewBTP.dbo.GetLoginIPTable @strStart,@strStartTime,@strEndTime";
            }
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strStart", SqlDbType.NVarChar, 20), new SqlParameter("@strStartTime", SqlDbType.DateTime, 8), new SqlParameter("@strEndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = strStart;
            commandParameters[1].Value = Convert.ToDateTime(str);
            commandParameters[2].Value = Convert.ToDateTime(str2);
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool IsEmpty()
        {
            string commandText = "Exec NewBTP.dbo.IsEmpty";
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


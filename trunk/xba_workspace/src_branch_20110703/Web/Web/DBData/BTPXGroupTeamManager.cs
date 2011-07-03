namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPXGroupTeamManager
    {
        public static void AddGroupTeam(int intCategory, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddGroupTeam ", intCategory, ",", intClubID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetChampionCupUPDown(int intClubID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetChampionCupUPDown", commandParameters);
        }

        public static int GetGroupCountByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetGroupCountByCategory " + intCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGroupIndexByClubID(int intClubID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGroupIndexByClubID ", intClubID, ",", intCategory });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGroupSize(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetGroupSize " + intCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupTeamByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetGroupTeamByCategory " + intCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupTeamByCategoryGroupIndexDP(int intCategory, int GroupIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGroupTeamByCatGrpIndexDP ", intCategory, ",", GroupIndex });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupTeamByCategoryGroupIndexOrderByResult(int intCategory, int GroupIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGroupTeamByCGIOrderByResult ", intCategory, ",", GroupIndex });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupTeamByCategoryPasswordGroupIndex(int intCategory, string strPassword, int GroupIndex)
        {
            string commandText = "Exec NewBTP.dbo.GetGroupTeamByCPWGI @intCategory,@strPassword,@GroupIndex";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strPassword", SqlDbType.NVarChar, 50), new SqlParameter("@GroupIndex", SqlDbType.Int, 4) };
            commandParameters[0].Value = intCategory;
            commandParameters[1].Value = strPassword;
            commandParameters[2].Value = GroupIndex;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetGroupTeamByCatGroupIndex(int intCategory, int GroupIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGroupTeamByCatGroupIndex ", intCategory, ",", GroupIndex });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetGroupTeamByCG(int intGroupCategory, int intGroupIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetXGroupTeamTableByCG ", intGroupCategory, ",", intGroupIndex });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGroupTeamCount(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetRegXBACount " + intCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetGroupTeamRowByCID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetGroupTeamRowByCID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int RegXBACup(int intClubID)
        {
            string str = RandomItem.rnd.Next(0x2710, 0x186a0).ToString() + RandomItem.rnd.Next(0x2710, 0x186a0).ToString();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@PassWord", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = str;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "RegXBACup", commandParameters);
        }

        public static int RegXBACup(int intClubID, int intCategory, string strPassword, int intTicketCategory, bool blCheck)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Password", SqlDbType.NVarChar, 50), new SqlParameter("@TicketCategory", SqlDbType.TinyInt, 1), new SqlParameter("@Check", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strPassword;
            commandParameters[3].Value = intTicketCategory;
            commandParameters[4].Value = blCheck;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "RegXBACupForInsert", commandParameters);
        }

        public static int RegXBACupUnPay(int intClubID, int intCategory, string strPassword, int intTicketCategory, int intCoin)
        {
            string commandText = "Exec NewBTP.dbo.RegXBACupUnPay @ClubID,@Category,@Password,@TicketCategory,@Coin";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Password", SqlDbType.NVarChar, 50), new SqlParameter("@TicketCategory", SqlDbType.TinyInt, 1), new SqlParameter("@Coin", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strPassword;
            commandParameters[3].Value = intTicketCategory;
            commandParameters[4].Value = intCoin;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetGroup(int intGroupTeamID, int intGoupIndex, int intTeamIndex)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetGroup ", intGroupTeamID, ",", intGoupIndex, ",", intTeamIndex });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetPassword(int intGoupTeamID, string strPassword)
        {
            string commandText = "Exec NewBTP.dbo.SetPassword @GoupTeamID,@Password";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@GoupTeamID", SqlDbType.Int, 4), new SqlParameter("@Password", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intGoupTeamID;
            commandParameters[1].Value = strPassword;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


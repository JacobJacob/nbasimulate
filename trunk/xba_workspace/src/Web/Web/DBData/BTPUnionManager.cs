namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPUnionManager
    {
        public static int AddMessageByUserID(int intUserID, string strContent)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Message", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strContent;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddMessageByUserID", commandParameters);
        }

        public static void AddReputation(int intUserID, string strNickName, int intUnionID, int intReputation, string strNote)
        {
            string commandText = "Exec NewBTP.dbo.AddUnionReputation @intUserID,@strNickName,@intUnionID,@intReputation,@strNote";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intReputation", SqlDbType.Int, 4), new SqlParameter("@strNote", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = intUnionID;
            commandParameters[3].Value = intReputation;
            commandParameters[4].Value = strNote;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddUnionWealthByDevCupID(int intDevCupID, int intWealth)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddUWealthByDevCupID ", intDevCupID, ",", intWealth });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CreateSociety(int intUserID, int intCategory, string strName, string strShortName, string strRemark, string strLogo, string strUQQ, string strUBBS)
        {
            string commandText = "Exec NewBTP.dbo.CreateSociety @UserID,@Category,@Name,@ShortName,@Remark,@Logo,@UQQ,@UBBS";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Name", SqlDbType.NVarChar, 20), new SqlParameter("@ShortName", SqlDbType.NVarChar, 10), new SqlParameter("@Remark", SqlDbType.NVarChar, 300), new SqlParameter("@Logo", SqlDbType.NVarChar, 50), new SqlParameter("@UQQ", SqlDbType.NVarChar, 50), new SqlParameter("@UBBS", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strName;
            commandParameters[3].Value = strShortName;
            commandParameters[4].Value = strRemark;
            commandParameters[5].Value = strLogo;
            commandParameters[6].Value = strUQQ;
            commandParameters[7].Value = strUBBS;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int DelUnionMessage(int intUserID, int intUnionMessageID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionMessageID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionMessageID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DelUnionMessage", commandParameters);
        }

        public static int DemiseCreater(int intUnionID, int intUserID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.DemiseCreater @intUnionID,@intUserID,@strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.Char, 20) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void FireUnionUser(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.FireUnionUser " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAllInfoByUnionID(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetAllInfoByUnionID " + intUnionID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllUnionTable()
        {
            string commandText = "Exec NewBTP.dbo.GetAllUnionTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAllUnionUserCount()
        {
            string commandText = "Exec NewBTP.dbo.GetAllUnionUserCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetMessageByUnionID(int intUnionID, int intPage, int intPageSize, bool DoCount)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = DoCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetMessageByUnionID", commandParameters);
        }

        public static int GetMessageCount(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = true;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetMessageByUnionID", commandParameters);
        }

        public static DataRow GetMessageRowByID(int intMessageID)
        {
            string commandText = "Exec NewBTP.dbo.GetMessageRowByID " + intMessageID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetReputationByUnion(int intUnionID, int intPage, int intPageSize)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = false;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetReputationByUnion", commandParameters);
        }

        public static int GetReputationByUnionCount(int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = true;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetReputationByUnion", commandParameters);
        }

        public static SqlDataReader GetReputationByUserID(int intUserID, int intPage, int intPageSize)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = false;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetReputationByUserID", commandParameters);
        }

        public static int GetReputationByUserIDCount(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = true;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetReputationByUserID", commandParameters);
        }

        public static int GetReputationCount(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionReputationCount " + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetReputationCountNew(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionReputationListNew " + intUnionID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetReputationList(int intUnionID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionReputationList ", intUnionID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetReputationListNew(int intUnionID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionReputationListNew ", intUnionID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetSocietyCount()
        {
            string commandText = "Exec NewBTP.dbo.GetSocietyCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetSocietyList(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetSocietyList ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUManagerCount(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUManagerCount " + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUManagerTable(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUManagerTable " + intUnionID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionCount()
        {
            string commandText = "Exec NewBTP.dbo.GetUnionCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionCountNew(string strName)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NChar, 20), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strName.Trim();
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = 1;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionListNew", commandParameters);
        }

        public static DataTable GetUnionList(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionList ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUnionListNew(string strName, int intPage, int intPerPage)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Name", SqlDbType.NChar, 20), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strName.Trim();
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPerPage;
            commandParameters[3].Value = 0;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetUnionListNew", commandParameters);
        }

        public static DataRow GetUnionRowByID(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionRowByID " + intUnionID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUnionUserByNickName(int intUnionID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionUserByNickName @intUnionID,@strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strNickName;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetUnionUserCountByID(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionUserCountByID " + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUnionUserCountByIDNew(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionUserListByIDNew " + intUnionID + ",0,0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetUnionUserListByID(int intUnionID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionUserListByID ", intUnionID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUnionUserListByIDNew(int intUnionID, int intPage, int intPerPage, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUnionUserListByIDNew ", intUnionID, ",", intCategory, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasShortName(string strShortName)
        {
            string commandText = "Exec NewBTP.dbo.HasShortName @strShortName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strShortName", SqlDbType.NVarChar, 10) };
            commandParameters[0].Value = strShortName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasUnionName(string strUnionName)
        {
            string commandText = "Exec NewBTP.dbo.HasUnionName @strUnionName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUnionName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strUnionName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void IntoUnion(int intUserID, int intUnionID, int intMessageID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.IntoUnion ", intUserID, ",", intUnionID, ",", intMessageID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int InviteUser(int intUnionID, string strNickName, int intCreaterID)
        {
            string commandText = "Exec NewBTP.dbo.InviteUser @UnionID,@NickName,@CreaterID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 20), new SqlParameter("@CreaterID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = intCreaterID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool IsUManager(int intUnionID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.IsUManager @UnionID,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strNickName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void OutUnion(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.OutUnion " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void RefuseUnion(int intUserID, int intUnionID, int intMessageID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RefuseUnion ", intUserID, ",", intUnionID, ",", intMessageID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SendMsgReadByUserID(int intUserID, int intUnionID, string strContent, bool blnNoName)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.NVarChar, 0x3e8), new SqlParameter("@NoName", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            commandParameters[2].Value = strContent;
            commandParameters[3].Value = blnNoName;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendMsgReadByUserID", commandParameters);
        }

        public static int SendWealthByUserID(int intUserID, int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SendWealthByUserID", commandParameters);
        }

        public static int SetUBBS(int intUnionID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetUBBS ", intUnionID, ",", intCategory });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionLogo(int intUnionID, string strLogo)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionLogo @intUnionID,@strLogo";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@strLogo", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strLogo;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int SetUnionManager(int intUnionID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionManager @UnionID,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetUnionRemark(int intUnionID, string strRemark, string strUQQ, string strUBBS)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionRemark @UnionID,@Remark,@UQQ,@UBBS";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UnionID", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.NVarChar, 500), new SqlParameter("@UQQ", SqlDbType.NVarChar, 50), new SqlParameter("@UBBS", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intUnionID;
            commandParameters[1].Value = strRemark;
            commandParameters[2].Value = strUQQ;
            commandParameters[3].Value = strUBBS;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetUnionUserCategory(int intUserID, int intUnionCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetUnionUserCategory ", intUserID, ",", intUnionCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetUWealthByID(int intUserID, int intUnionID, int intCategory, int intUnionCategory, int intType, int intWealth, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.SetUWealthByID @intUserID,@intUnionID,@intCategory,@intUnionCategory,@intType,@intWealth,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intUnionID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@intUnionCategory", SqlDbType.Int, 4), new SqlParameter("@intType", SqlDbType.Int, 4), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@strRemark", SqlDbType.NVarChar, 300) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intUnionCategory;
            commandParameters[4].Value = intType;
            commandParameters[5].Value = intWealth;
            commandParameters[6].Value = strRemark;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int UnlayUnion(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.UnlayUnion " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


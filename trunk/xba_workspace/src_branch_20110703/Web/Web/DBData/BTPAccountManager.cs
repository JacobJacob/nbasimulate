namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPAccountManager
    {
        public static void AddAccount(int intUserID, string strUserName, string strNickName)
        {
            string name = new NameGenerator().GetName();
            string str2 = RandomItem.rnd.Next(1, 9).ToString();
            string commandText = "Exec NewBTP.dbo.AddAccount @UserID,@UserName,@NickName,@SecName,@SecFace";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@SecName", SqlDbType.NVarChar, 20), new SqlParameter("@SecFace", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUserName;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = name;
            commandParameters[4].Value = str2;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddAccountCupIDs(int intUserID, int intCupID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddAccountCupIDs ", intUserID, ",", intCupID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddBadUserByUserID(int intUserID, int intCategory, int intAddDay)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4), new SqlParameter("@Content", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intAddDay;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddBadUserByUserID", commandParameters);
        }

        public static void AddDevInCome(int intUserIDH, int intUserIDA, int intSumH, int intSumA)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserIDH", SqlDbType.Int, 4), new SqlParameter("@UserIDA", SqlDbType.Int, 4), new SqlParameter("@SumH", SqlDbType.Int, 4), new SqlParameter("@SumA", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserIDH;
            commandParameters[1].Value = intUserIDA;
            commandParameters[2].Value = intSumH;
            commandParameters[3].Value = intSumA;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddDevInCome", commandParameters);
        }

        public static void AddFullAccount(int intUserID, string strUserName, string strNickName, string strPassword, bool blnSex, int intPayType, string strDiskURL, string strBirth, string strProvince, string strCity, string strQQ, int intRecomUserID)
        {
            string name = new NameGenerator().GetName();
            string str2 = RandomItem.rnd.Next(1, 9).ToString();
            string commandText = "Exec NewBTP.dbo.AddFullAccount @UserID,@UserName,@NickName,@SecName,@SecFace,@Password,@Sex,@PayType,@DiskURL,@Birth,@Province,@City,@QQ,@RecomUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@SecName", SqlDbType.NVarChar, 20), new SqlParameter("@SecFace", SqlDbType.NVarChar, 20), new SqlParameter("@Password", SqlDbType.NChar, 20), new SqlParameter("@Sex", SqlDbType.Bit, 1), new SqlParameter("@PayType", SqlDbType.TinyInt, 1), new SqlParameter("@DiskURL", SqlDbType.NVarChar, 50), new SqlParameter("@Birth", SqlDbType.NChar, 10), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@QQ", SqlDbType.NChar, 20), new SqlParameter("@RecomUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUserName;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = name;
            commandParameters[4].Value = str2;
            commandParameters[5].Value = strPassword;
            commandParameters[6].Value = blnSex;
            commandParameters[7].Value = intPayType;
            commandParameters[8].Value = strDiskURL;
            commandParameters[9].Value = strBirth;
            commandParameters[10].Value = strProvince;
            commandParameters[11].Value = strCity;
            commandParameters[12].Value = strQQ;
            commandParameters[13].Value = intRecomUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddFullAccount(int intUserID, string strUserName, string strNickName, string strPassword, bool blnSex, int intPayType, string strDiskURL, string strBirth, string strProvince, string strCity, string strQQ, string strEmail)
        {
            string name = new NameGenerator().GetName();
            string str2 = RandomItem.rnd.Next(1, 9).ToString();
            string commandText = "Exec NewBTP.dbo.AddFullAccountNew @UserID,@UserName,@NickName,@SecName,@SecFace,@Password,@Sex,@PayType,@DiskURL,@Birth,@Province,@City,@QQ,@Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@SecName", SqlDbType.NVarChar, 20), new SqlParameter("@SecFace", SqlDbType.NVarChar, 20), new SqlParameter("@Password", SqlDbType.NChar, 20), new SqlParameter("@Sex", SqlDbType.Bit, 1), new SqlParameter("@PayType", SqlDbType.TinyInt, 1), new SqlParameter("@DiskURL", SqlDbType.NVarChar, 50), new SqlParameter("@Birth", SqlDbType.NChar, 10), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@QQ", SqlDbType.NChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUserName;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = name;
            commandParameters[4].Value = str2;
            commandParameters[5].Value = strPassword;
            commandParameters[6].Value = blnSex;
            commandParameters[7].Value = intPayType;
            commandParameters[8].Value = strDiskURL;
            commandParameters[9].Value = strBirth;
            commandParameters[10].Value = strProvince;
            commandParameters[11].Value = strCity;
            commandParameters[12].Value = strQQ;
            commandParameters[13].Value = strEmail;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddLoginIP(int intUserID, string strIP)
        {
            string commandText = "Exec NewBTP.dbo.AddLoginIP @intUserID,@strIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strIP;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool AddMoneyAdmin(string strUserName, int intMoney, int intFinanceType, int intType, string strEvent, string strRemark)
        {
            string commandText = "Exec NewBTP.dbo.AddMoneyAdmin @UserName,@Money,@FinanceType,@Type,@Event,@Remark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@Money", SqlDbType.Int, 4), new SqlParameter("@FinanceType", SqlDbType.Int, 4), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Event", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intMoney;
            commandParameters[2].Value = intFinanceType;
            commandParameters[3].Value = intType;
            commandParameters[4].Value = strEvent;
            commandParameters[5].Value = strRemark;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMoneyByUserID(int intUserID, long longMoney)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddMoneyByUserID ", intUserID, ",", longMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddMoneyWithFinance(int intMoney, int intUserID, int intType, string strEvent)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddMoneyByUserID ", intUserID, ",", intMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            BTPFinanceManager.ManagerAddFinance(intUserID, 1, intType, intMoney, 1, strEvent);
        }

        public static void AddMoneyWithFinance(int intMoney, int intUserID, int intType, string strEvent, int intCategory, int intTicketCategory, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddMoneyByUserID ", intUserID, ",", intMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            commandText = string.Concat(new object[] { "UPDATE BTP_Tool SET AmountInStock=AmountInStock-", intCount, " WHERE Category=", intCategory, " AND TicketCategory=", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            BTPFinanceManager.ManagerAddFinance(intUserID, 1, intType, intMoney, 1, strEvent);
        }

        public static void AddWealth(int intUserID, int intWealth, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.AddWealth @intUserID,@intWealth,@strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@strContent", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intWealth;
            commandParameters[2].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddWealth(int intUserID, int intWealth, string strContent, int intCategory, int intTicketCategory, int intCount)
        {
            string commandText = "Exec NewBTP.dbo.BuyWealth @intUserID,@intWealth,@strContent,@intCategory,@intTicketCategory,@intCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@strContent", SqlDbType.NVarChar, 500), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intTicketCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intWealth;
            commandParameters[2].Value = strContent;
            commandParameters[3].Value = intCategory;
            commandParameters[4].Value = intTicketCategory;
            commandParameters[5].Value = intCount;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddWealthByFinance(int intUserID, int intWealth, int intCategory, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.AddWealthByFinance @intUserID,@intWealth,@intCategory,@strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intWealth", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strContent", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intWealth;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int AverageSalary(int intDevlev)
        {
            string commandText = "Exec NewBTP.dbo.AverageSalary " + intDevlev;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ChangeClubName(int intUserID, string strClubName, DateTime datChangeClubTime)
        {
            string commandText = "Exec NewBTP.dbo.ChangeClubName @UserID,@ClubName,@ChangeClubTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ClubName", SqlDbType.NChar, 20), new SqlParameter("@ChangeClubTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClubName;
            commandParameters[2].Value = datChangeClubTime;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeNickName(int intUserID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.ChangeNickName @UserID,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangePassword(int intUserID, string strPassword, string strConn)
        {
            string commandText = "Exec NewBTP.dbo.ChangePassword @UserID,@Password";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Password", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strPassword;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeSex(int intUserID, bool blnSex, string strConn)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ChangeSex ", intUserID, ",", Convert.ToInt16(blnSex) });
            SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, commandText);
        }

        public static bool CheckUserActiveTime(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.CheckUserActiveTime @UserID";
            SqlParameter[] commandParameters = new SqlParameter[2];
            commandParameters[0] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            commandParameters[0].Value = intUserID;
            return (bool) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow ComputeSalaryTop(int intDevlev)
        {
            string commandText = "Exec NewBTP.dbo.ComputeSalaryTop " + intDevlev;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DelBadUserByUserID(int intUserID, int intCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DelBadUserByUserID", commandParameters);
        }

        public static void DeleteUnion(int intUserID, int intUnionID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intUnionID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DeleteUnion", commandParameters);
        }

        public static int ExchangeWealth(int intUserID, int intWealth)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ExchangeWealth ", intUserID, ",", intWealth });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int ExchangeOnlyPoint(int intUserID, int intAmount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ExchangeOnlyPoint ", intUserID, ",", intAmount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        /**
         *推广积分换游戏币 
         */

        public static int ExchangePromotionPoint(int intUserID, int intAmount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ExchangePromotionPoint ", intUserID, ",", intAmount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAccountCount()
        {
            string commandText = "Exec NewBTP.dbo.GetAccountCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAccountCount(int intType)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountList 1,0,0," + intType;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAccountList(int intPage, int intPerPage, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetAccountList 0,", intPage, ",", intPerPage, ",", intType });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByClubID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByClubID3(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByClubID3 " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByClubID5(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByClubID5 " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByClubName(string strClubName)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByClubName @ClubName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strClubName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetAccountRowByNickName(string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByNickName @strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetAccountRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByUserID(int intUserID, string strConn)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(strConn, CommandType.Text, commandText);
        }

        public static DataRow GetAccountRowByUserName(string strUserName)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountRowByUserName @strUserName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = strUserName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetAccountTableByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetAccountTableByCategory " + intCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllAccount()
        {
            string commandText = "Exec NewBTP.dbo.GetAllAccount";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void GetBoxByWinner(int intUserID)
        {
            SqlParameter parameter = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameter.Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetBoxByWinner", new SqlParameter[] { parameter });
        }

        public static bool GetCanChoose(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetCanChoose " + intUserID;
            return !SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetChooseClub(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetChooseClub " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetCupIDsByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetCupIDsByClubID " + intClubID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetDevCupIDsByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevCupIDsByClubID " + intClubID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetGuideCode(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetGuideCode " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetInfoByUserNamePassword(string strUserName, string strPassword)
        {
            string commandText = "Exec NewBTP.dbo.GetInfoByUserNamePassword @strUserName,@strPassword";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@strPassword", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetLadderCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetLadderCount " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DateTime GetLatestRegTimeByIP(string strIP)
        {
            string commandText = "Exec NewBTP.dbo.GetLatestRegTimeByIP @IP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@IP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strIP;
            return SqlHelper.ExecuteDateTimeDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetLoginRec(string strUserName, string strPassword, string strLoginIP)
        {
            string commandText = "Exec NewBTP.dbo.GetLoginRec @UserName,@Password,@LoginIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@Password", SqlDbType.NChar, 20), new SqlParameter("@LoginIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            commandParameters[2].Value = strLoginIP;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMaxLevels()
        {
            string commandText = "Exec NewBTP.dbo.GetMaxLevels";
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetNickNameByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetNickNameByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText).Trim();
        }

        public static int GetResultCountByClubName(string strSearchName, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetResultCountByNickName @SerachName,@Gender";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SerachName", SqlDbType.NChar, 30), new SqlParameter("@Gender", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = strSearchName;
            commandParameters[1].Value = intGender;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetResultCountByNickName(string strSearchName, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetResultCountByNickName @SerachName,@Gender";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SerachName", SqlDbType.NChar, 20), new SqlParameter("@Gender", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = strSearchName;
            commandParameters[1].Value = intGender;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchCountAge(int intDoCount, int intPageIndex, int intPageSize, string strStartYear, string strEndYear, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByAge @intDoCount,@intPageIndex,@intPageSize,@StartYear,@EndYear,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StartYear", SqlDbType.Int, 4), new SqlParameter("@EndYear", SqlDbType.Int, 4), new SqlParameter("@Gneder", SqlDbType.Int, 4), new SqlParameter("@StartYear", SqlDbType.NChar, 20), new SqlParameter("@EndYear", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intDoCount;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = strStartYear;
            commandParameters[4].Value = strEndYear;
            commandParameters[5].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetSearchCountByAge(int intDoCount, int intPageIndex, int intPageSize, string strStartYear, string strEndYear, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByAge @intDoCount,@intPageIndex,@intPageSize,@StartYear,@EndYear,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StartYear", SqlDbType.Int, 4), new SqlParameter("@EndYear", SqlDbType.Int, 4), new SqlParameter("@Gneder", SqlDbType.Int, 4), new SqlParameter("@StartYear", SqlDbType.NChar, 20), new SqlParameter("@EndYear", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = strStartYear;
            commandParameters[4].Value = strEndYear;
            commandParameters[5].Value = intGender;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetSearchCountByCity(int intDoCount, int intPageIndex, int intPageSize, string strProvince, string strCity, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByCity @intDoCount,@intPageIndex,@intPageSize,@Province,@City,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDoCount", SqlDbType.Int, 4), new SqlParameter("@intPageIndex", SqlDbType.Int, 4), new SqlParameter("@intPageSize", SqlDbType.Int, 4), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intDoCount;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = strProvince;
            commandParameters[4].Value = strCity;
            commandParameters[5].Value = intGender;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetSearchCountByGender(int intDoCount, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByGender @intDoCount,@intPageIndex,@intGender,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDoCount", SqlDbType.Int, 4), new SqlParameter("@intPageIndex", SqlDbType.Int, 4), new SqlParameter("@intGender", SqlDbType.Int, 4), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = 1;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = intGender;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchResultByAge(int intDoCount, int intPageIndex, int intPageSize, string strStartYear, string strEndYear, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByAge @intDoCount,@intPageIndex,@intPageSize,@StartYear,@EndYear,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@StartYear", SqlDbType.Int, 4), new SqlParameter("@EndYear", SqlDbType.Int, 4), new SqlParameter("@Gneder", SqlDbType.Int, 4), new SqlParameter("@StartYear", SqlDbType.NChar, 20), new SqlParameter("@EndYear", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intPageIndex;
            commandParameters[1].Value = intPageSize;
            commandParameters[2].Value = strStartYear;
            commandParameters[3].Value = strStartYear;
            commandParameters[4].Value = strEndYear;
            commandParameters[5].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchResultByCity(int intDoCount, int intPageIndex, int intPageSize, string strProvince, string strCity, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByCity @intDoCount,@intPageIndex,@intPageSize,@Province,@City,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDoCount", SqlDbType.Int, 4), new SqlParameter("@intPageIndex", SqlDbType.Int, 4), new SqlParameter("@intPageSize", SqlDbType.Int, 4), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intDoCount;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = strProvince;
            commandParameters[4].Value = strCity;
            commandParameters[5].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchResultByClubName(string strSearchName, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByClubName @SerachName,@Gender";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SerachName", SqlDbType.NChar, 30), new SqlParameter("@Gender", SqlDbType.TinyInt, 4) };
            commandParameters[0].Value = strSearchName;
            commandParameters[1].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchResultByGender(int intDoCount, int intPageIndex, int intPageSize, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByGender @intDoCount,@intPageIndex,@intGender,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intDoCount", SqlDbType.Int, 4), new SqlParameter("@intPageIndex", SqlDbType.Int, 4), new SqlParameter("@intGender", SqlDbType.Int, 4), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intDoCount;
            commandParameters[1].Value = intPageIndex;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSearchResultByNickName(string strSearchName, int intGender)
        {
            string commandText = "Exec NewBTP.dbo.GetSearchResultByNickName @SerachName,@Gneder";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@SerachName", SqlDbType.NChar, 20), new SqlParameter("@Gneder", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = strSearchName;
            commandParameters[1].Value = intGender;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetSecRowByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetSecRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetSTLadder()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetSTLadder";
                return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return GetSTLadder();
            }
        }

        public static int GetUnionUserCount(int intUnionID)
        {
            string commandText = "Exec NewBTP.dbo.GetUnionUserCount " + intUnionID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUPByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetUPByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUserInfoByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetUserInfoByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetUserRookieOpByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetUserRookieOpByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetVTLadder()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetVTLadder";
                return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return GetVTLadder();
            }
        }

        public static void GiveOldUserGift(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GiveOldUserGift", commandParameters);
        }

        public static bool HasClubName(string strClubName)
        {
            string commandText = "Exec NewBTP.dbo.HasClubName @ClubName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strClubName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasEnoughMoney(int intUserID, int intMoney)
        {
            string commandText = "Exec NewBTP.dbo.GetMoneyByUserID " + intUserID;
            if ((SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) < intMoney) && (intMoney > 0))
            {
                return false;
            }
            return true;
        }

        public static int HasEnoughWealth(int intUserID, int intWealth)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Wealth", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intWealth;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "HasEnoughWealth", commandParameters);
        }

        public static bool HasNickName(string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.HasNickName @NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static bool InBadUserByUserID(int intUserID, int intCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCategory;
            return (bool) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "InBadUserByUserID", commandParameters);
        }

        public static int IschildGame(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "IschildGame", commandParameters);
        }

        public static int IschildLogin(int intUserID, DateTime dtActiveTime)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ActiveTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = dtActiveTime;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "IschildLogin", commandParameters);
        }

        public static void ManagerMoney(string strNickName, int intCategory, int intType, int intMoney, string strEvent)
        {
            string commandText = "EXEC NewBTP.dbo.ManagerMoney @strNickName,@intCategory,@intType,@intMoney,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intMoney", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = intType;
            commandParameters[3].Value = intMoney;
            commandParameters[4].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ModifyClothes(int intUserID, string strClothes)
        {
            string commandText = "Exec NewBTP.dbo.ModifyClothes @intUserID,@strClothes";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strClothes", SqlDbType.NChar, 6) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClothes;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ModifyClubLogo(int intUserID, string strLogo)
        {
            string commandText = "Exec NewBTP.dbo.ModifyClubLogo @intUserID,@strLogo";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strLogo", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strLogo;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void OldUserLogin(int intUserID, DateTime dtActiveTime, DateTime dtOldTime)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ActiveTime", SqlDbType.DateTime, 8), new SqlParameter("@OldTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = dtActiveTime;
            commandParameters[2].Value = dtOldTime;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "OldUserLogin", commandParameters);
        }

        public static DataRow RefashionPlayerSkill(int intUserID, long longPlayerID, int intPlayerType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RefashionPlayerSkill ", intUserID, ",", longPlayerID, ",", intPlayerType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow RefashionSafePlayerSkill(int intUserID, long longPlayerID, int intPlayerType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RefashionSafePlayerSkill ", intUserID, ",", longPlayerID, ",", intPlayerType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool RegAssociator(int intUserID, DateTime datMemberExpireTime, string strConn)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RegAssociator ", intUserID, ",'", datMemberExpireTime, "'" });
            return SqlHelper.ExecuteBoolDataField(strConn, CommandType.Text, commandText);
        }

        public static int ReplaceClub(int intUserIDA, int intUserIDB, string strClubName)
        {
            string commandText = "Exec NewBTP.dbo.ReplaceClub @intUserIDA,@intUserIDB,@strClubName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserIDA", SqlDbType.Int, 4), new SqlParameter("@intUserIDB", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserIDA;
            commandParameters[1].Value = intUserIDB;
            commandParameters[2].Value = strClubName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void ResetFriMatchFlag(int intUserID, int intFriMatch)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ResetFriMatchFlag ", intUserID, ",", intFriMatch });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void RewardCupByClubID(int intClubID, int intCupID, int intRound, int intMoney, int intScore)
        {
            try
            {
                string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RewardCupByClubID ", intClubID, ",", intCupID, ",", intRound, ",", intMoney, ",", intScore });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch
            {
                Console.WriteLine(string.Concat(new object[] { "CupID：", intCupID, "--ClubID：", intClubID, "杯赛奖励错误，5秒钟后重试。" }));
                Thread.Sleep(0x1388);
                RewardCupByClubID(intClubID, intCupID, intRound, intMoney, intScore);
            }
        }

        public static void RewardRMUser(int intUserID, int intMoney, int intRank)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RewardRMUser ", intUserID, ",", intMoney, ",", intRank });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void RewardXCupByClubID(int intClubID, int intGameID, int intRound, int intMoney)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RewardXCupByClubID ", intClubID, ",", intGameID, ",", intRound, ",", intMoney });
            SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable SendMsgToLast3()
        {
            string commandText = "Exec NewBTP.dbo.SendMsgToLast3";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetAccountCategory(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetAccountCategory ", intUserID, ",", intCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetAccountLogoLink(int intUserID, string strLogoLink)
        {
            string commandText = "Exec NewBTP.dbo.SetAccountLogoLink @intUserID,@strLogoLink";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strLogoLink", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strLogoLink;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetContinuePayByUserID(int intUserID, int intContinuePay)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetContinuePayByUserID ", intUserID, ",", intContinuePay });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetGuideCode(int intUserID, string strGuideCode)
        {
            string commandText = "Exec NewBTP.dbo.SetGuideCode @intUserID,@strGuideCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strGuideCode", SqlDbType.VarChar, 150) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strGuideCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetHasChoose(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetHasChoose " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetUnionPosition(int intUserID, string strUnionPosition)
        {
            string commandText = "Exec NewBTP.dbo.SetUnionPosition @UserID,@UnionPosition";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@UnionPosition", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUnionPosition;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetUserRookieOpByUserID(int intUserID, string strRookieOp)
        {
            string commandText = "Exec NewBTP.dbo.SetUserRookieOpByUserID @UserID,@RookieOp";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@RookieOp", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strRookieOp;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int SetWealthByUserID(int intClubID, int intCategory, int intWealth)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetWealthByUserID ", intClubID, ",", intCategory, ",", intWealth });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SpendMoneyWithFinance(int intMoney, int intUserID, int intType, string strEvent)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddMoneyByUserID ", intUserID, ",", -intMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            BTPFinanceManager.ManagerAddFinance(intUserID, 1, intType, intMoney, 2, strEvent);
        }

        public static void SpendMoneyWithMsg(int intMoney, int intUserID, int intType, string strEvent, string strContent)
        {
        }

        public static void UpdateAdvanceOp(int intUserID, string strAdvanceOp)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@AdvanceOp", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strAdvanceOp;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateAdvanceOp", commandParameters);
        }

        public static void UpdateDevSay()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdateSay");
        }

        public static void UpdateDevSays(int intUserID, string strDevSays)
        {
            string commandText = "Exec NewBTP.dbo.UpdateDevSays @intUserID,@strDevSays";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strDevSays", SqlDbType.NVarChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strDevSays;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateLockTime()
        {
            try
            {
                string commandText = "UPDATE BTP_Account SET LockTime=LockTime-1 WHERE LockTime>0";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdateLockTime();
            }
        }

        public static void UpdatePayType()
        {
            try
            {
                string commandText = "SELECT UserID,ContinuePay,MemberExpireTime,PayType FROM BTP_Account WHERE (PayType=1 OR ContinuePay=1) AND MemberExpireTime<=GETDATE()";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                DataRow toolRowByID = BTPToolManager.GetToolRowByID(0x1c);
                int intCoin = (int) toolRowByID["CoinCost"];
                string str2 = toolRowByID["ToolName"].ToString().Trim();
                if (table != null)
                {
                    foreach (DataRow row2 in table.Rows)
                    {
                        int intUserID = (int) row2["UserID"];
                        bool flag = (bool) row2["ContinuePay"];
                        DateTime time = (DateTime) row2["MemberExpireTime"];
                        int num3 = Convert.ToInt32(row2["PayType"]);
                        /*if (flag)
                        {
                            if (ROOTUserManager.SpendCoin40Return(intUserID, intCoin, "购买1个" + str2, "") == 1)
                            {
                                BTPToolLinkManager.BuyVIPCard(intUserID, DateTime.Now.AddMonths(1));
                                string str3 = DateTime.Now.AddMonths(1).ToString();
                                commandText = string.Concat(new object[] { "UPDATE BTP_Account SET PayType=1,ContinuePay=1,MemberExpireTime='", str3, "' WHERE UserID=", intUserID });
                                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                                commandText = string.Concat(new object[] { "UPDATE Main_User SET IsMember=1,MemberExpireTime='", str3, "' WHERE UserID=", intUserID });
                                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
                                DataTable giftTable = BTPGiftManager.GetGiftTable();
                                if (giftTable != null)
                                {
                                    string str4 = "会员自动续费成功！";
                                    foreach (DataRow row in giftTable.Rows)
                                    {
                                        int intToolID = (int)row["ToolID"];
                                        int intWealth = (int)row["Amount"];
                                        int intType = (byte)row["Type"];
                                        if (intType == 3)
                                        {
                                            AddWealthByFinance(intUserID, intWealth, 1, "购买会员卡赠送！");
                                            object obj2 = str4;
                                            str4 = string.Concat(new object[] { obj2, "获赠", intWealth, "枚游戏币！" });
                                        }
                                        else
                                        {
                                            if ((intToolID == 0x21) && (intType == 1))
                                            {
                                                BTPToolLinkManager.BuyDoubleExperience(intToolID, intUserID, intWealth);
                                                continue;
                                            }
                                            BTPToolLinkManager.GiftTool(intUserID, intToolID, intWealth, intType);
                                            if (str4.IndexOf("游戏币道具") == -1)
                                            {
                                                str4 = str4 + "并获赠超值游戏币道具！";
                                            }
                                        }
                                    }
                                    BTPMessageManager.AddMessage(intUserID, 2, 0, "秘书报告", str4);
                                    //giftTable.Close();
                                    continue;
                                }
                               
                            }
                            if ((time > DateTime.Now.AddDays(-3.0)) || (num3 == 1))
                            {
                                commandText = "UPDATE BTP_Account SET PayType=0 WHERE UserID=" + intUserID;
                                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                                string str5 = "会员自动续费失败！您将失去会员资格，请手动购买会员卡，继续享受超值会员特权。";
                                BTPMessageManager.AddMessage(intUserID, 2, 0, "秘书报告", str5);
                            }
                            continue;
                        }*/
                        commandText = "UPDATE BTP_Account SET PayType=0 WHERE UserID=" + intUserID;
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                        string strContent = "您已失去会员资格，请手动购买会员卡，继续享受超值会员特权。";
                        BTPMessageManager.AddMessage(intUserID, 2, 0, "秘书报告", strContent);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                string str7 = exception.ToString();
                try
                {
                    string str8 = "INSERT INTO BTP_UpdateLog (LogName,LogContent)VALUES('UpdatePayType','" + str7 + "')";
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str8);
                }
                catch
                {
                }
            }
        }

        public static void UpdateQQ(int intUserID, string strQQ)
        {
            string commandText = "Exec NewBTP.dbo.UpdateQQ @intUserID,@strQQ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strQQ", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strQQ;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int CheckRegisterInfo(string strUserName, string strNickName, string strEmail)
        {
            string commandText = "Exec NewBTP.dbo.CheckRegisterInfo @UserName,@NickName,@Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strEmail;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMaxUserID()
        {
            string commandText = "Exec NewBTP.dbo.[GetMaxUserID] ";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool CheckInviteCode(string strInviteCode)
        {
            string commandText = string.Concat(new Object[] {"SELECT TOP 1 Code FROM BTP_InviteCode WHERE Code = '", strInviteCode, "' AND Status = 2 " });
            string code = SqlHelper.ExecuteStringReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (code != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void UpdateInviteCodeUsed(string strInviteCode)
        {
            string commandText = string.Concat(new Object[] { "UPDATE BTP_InviteCode SET Status = 3 WHERE Code = '", strInviteCode, "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
           
        }

    }
}


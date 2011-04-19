namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class ROOTUserManager
    {
        public static int Add51eduUser(string strUserName, int intCategory, string strPassword, string strNickName, bool blnSex, string strFace, string strYear, string strMonth, string strEmail, string strProvince, string strCity, string strIntroNickName, string strSay, string strIP, string strDiskURL, string strFromURL, string strQQ, string strChannel, string strCookies)
        {
            int num = Convert.ToInt16(blnSex);
            string commandText = "Exec ROOT_Data.dbo.Add51eduUser @UserName,@Category,@Password,@NickName,@Sex,@Face,@Year,@Month,@Email,@Province,@City,@IntroNickName,@Say,@IP,@DiskURL,@FromURL,@QQ,@Channel,@Cookies";
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Password", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Sex", SqlDbType.Bit, 1), new SqlParameter("@Face", SqlDbType.NVarChar, 100), new SqlParameter("@Year", SqlDbType.NChar, 4), new SqlParameter("@Month", SqlDbType.NChar, 5), new SqlParameter("@Email", SqlDbType.NVarChar, 50), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@IntroNickName", SqlDbType.NChar, 20), new SqlParameter("@Say", SqlDbType.NVarChar, 100), new SqlParameter("@IP", SqlDbType.NVarChar, 50), new SqlParameter("@DiskURL", SqlDbType.NVarChar, 50), new SqlParameter("@FromURL", SqlDbType.NVarChar, 50), 
                new SqlParameter("@QQ", SqlDbType.NChar, 20), new SqlParameter("@Channel", SqlDbType.NChar, 20), new SqlParameter("@Cookies", SqlDbType.NChar, 20)
             };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = strPassword;
            commandParameters[3].Value = strNickName;
            commandParameters[4].Value = num;
            commandParameters[5].Value = strFace;
            commandParameters[6].Value = strYear;
            commandParameters[7].Value = strMonth;
            commandParameters[8].Value = strEmail;
            commandParameters[9].Value = strProvince;
            commandParameters[10].Value = strCity;
            commandParameters[11].Value = strIntroNickName;
            commandParameters[12].Value = strSay;
            commandParameters[13].Value = strIP;
            commandParameters[14].Value = strDiskURL;
            commandParameters[15].Value = strFromURL;
            commandParameters[0x10].Value = strQQ;
            commandParameters[0x11].Value = strChannel;
            commandParameters[0x12].Value = strCookies;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int Add51eduUser40(string strUserName, string strPassword, string strNickName, string strEmail, int intGender, string strBirthDay, string strPro, string strCity, string strIntro, string strQQ, string strMSN)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("UserName", SqlDbType.VarChar), new SqlParameter("Password", SqlDbType.VarChar), new SqlParameter("NickName", SqlDbType.VarChar), new SqlParameter("Email", SqlDbType.VarChar), new SqlParameter("Gender", SqlDbType.Bit), new SqlParameter("BirthDay", SqlDbType.DateTime), new SqlParameter("Province", SqlDbType.VarChar), new SqlParameter("City", SqlDbType.VarChar), new SqlParameter("Intro", SqlDbType.VarChar), new SqlParameter("QQ", SqlDbType.VarChar), new SqlParameter("MSN", SqlDbType.VarChar) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = strEmail;
            commandParameters[4].Value = intGender;
            commandParameters[5].Value = strBirthDay;
            commandParameters[6].Value = strPro;
            commandParameters[7].Value = strCity;
            commandParameters[8].Value = strIntro;
            commandParameters[9].Value = strQQ;
            commandParameters[10].Value = strMSN;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "RegisterUser", commandParameters);
        }

        public static bool AddCoin(string strUserName, int intCoin)
        {
            string commandText = "Exec ROOT_Data.dbo.AddCoin @strUserName,@intCoin";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NVarChar, 20), new SqlParameter("@intCoin", SqlDbType.Int, 4) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intCoin;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool AddCoinAdmin(string strUserName, int intCoin, string strEvent, string strRemark)
        {
            string commandText = "Exec ROOT_Data.dbo.AddCoinAdmin @UserName,@Coin,@Event,@Remark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@Coin", SqlDbType.Int, 4), new SqlParameter("@Event", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            commandParameters[3].Value = strRemark;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddCoinFinance(int intUserID, int intCoin, int intStatus, string strEvent, string strRemark)
        {
            string commandText = "Exec ROOT_Data.dbo.AddCoinFinance @intUserID,@intCoin,@intStatus,@strEvent,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 200), new SqlParameter("@strRemark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = intStatus;
            commandParameters[3].Value = strEvent;
            commandParameters[4].Value = strRemark;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool AddCoinReceive(string strUserName, int intCoin, string strOID)
        {
            string commandText = "Exec ROOT_Data.dbo.AddCoinReceive @strUserName,@intCoin,@strOID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NVarChar, 20), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@strOID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strOID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int AddFlack(int intUserID, string strFlackURL)
        {
            string commandText = "Exec ROOT_Data.dbo.AddFlack @UserID,@FlackURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@FlackURL", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strFlackURL;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool AddFreeCoin(int intUserID, int intCoin)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.AddFreeCoin ", intUserID, ",", intCoin });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int AddHonors(string strNickName, string strHonor)
        {
            string commandText = "Exec ROOT_Data.dbo.AddHonors @strNickName,@strHonor";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@strHonor", SqlDbType.VarChar, 20) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = strHonor;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddMobileMsg(string strContent)
        {
            string commandText = "Exec ROOT_Data.dbo.AddMobileMsg @strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strContent", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = strContent;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddPayOrder(int intUserID, string strUserName, int intCategory, string strOID, string strOrderName, int intOrderCategory, int intCount, int intPrice)
        {
            string commandText = "Exec ROOT_Data.dbo.AddPayOrder @intUserID,@strUserName,@intCategory,@strOID,@strOrderName,@intOrderCategory,@intCount,@intPrice";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strOID", SqlDbType.NVarChar, 50), new SqlParameter("@strOrderName", SqlDbType.NVarChar, 50), new SqlParameter("@intOrderCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUserName;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = strOID;
            commandParameters[4].Value = strOrderName;
            commandParameters[5].Value = intOrderCategory;
            commandParameters[6].Value = intCount;
            commandParameters[7].Value = intPrice;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddPayOrderByMoveBill(int intUserID, string strUserName, int intCategory, string strOID, string strOrderName, int intOrderCategory, int intCount, int intPrice, string MoveBillPhone, DateTime datToday)
        {
            string commandText = "Exec ROOT_Data.dbo.AddPayOrderByMoveBill @intUserID,@strUserName,@intCategory,@strOID,@strOrderName,@intOrderCategory,@intCount,@intPrice,@MoveBillPhone,@datToday";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strOID", SqlDbType.NVarChar, 50), new SqlParameter("@strOrderName", SqlDbType.NVarChar, 50), new SqlParameter("@intOrderCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4), new SqlParameter("@MoveBillPhone", SqlDbType.Char, 20), new SqlParameter("@datToday", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strUserName;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = strOID;
            commandParameters[4].Value = strOrderName;
            commandParameters[5].Value = intOrderCategory;
            commandParameters[6].Value = intCount;
            commandParameters[7].Value = intPrice;
            commandParameters[8].Value = MoveBillPhone;
            commandParameters[9].Value = datToday;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int AddUser(string strUserName, int intCategory, string strPassword, string strNickName, bool blnSex, string strFace, string strYear, string strMonth, string strDay, string strEmail, string strProvince, string strCity, string strIntroNickName, string strSay, string strIP, string strDiskURL, string strFromURL, string strQQ, string strChannel, string strCookies)
        {
            int num = Convert.ToInt16(blnSex);
            string commandText = "Exec ROOT_Data.dbo.AddUser @UserName,@Category,@Password,@NickName,@Sex,@Face,@Year,@Month,@Day,@Email,@Province,@City,@IntroNickName,@Say,@IP,@DiskURL,@FromURL,@QQ,@Channel,@Cookies";
            SqlParameter[] commandParameters = new SqlParameter[] { 
                new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@Category", SqlDbType.TinyInt, 1), new SqlParameter("@Password", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Sex", SqlDbType.Bit, 1), new SqlParameter("@Face", SqlDbType.NVarChar, 100), new SqlParameter("@Year", SqlDbType.NChar, 4), new SqlParameter("@Month", SqlDbType.NChar, 2), new SqlParameter("@Day", SqlDbType.NChar, 2), new SqlParameter("@Email", SqlDbType.NVarChar, 50), new SqlParameter("@Province", SqlDbType.NChar, 20), new SqlParameter("@City", SqlDbType.NChar, 20), new SqlParameter("@IntroNickName", SqlDbType.NChar, 20), new SqlParameter("@Say", SqlDbType.NVarChar, 100), new SqlParameter("@IP", SqlDbType.NVarChar, 50), new SqlParameter("@DiskURL", SqlDbType.NVarChar, 50), 
                new SqlParameter("@FromURL", SqlDbType.NVarChar, 50), new SqlParameter("@QQ", SqlDbType.NChar, 20), new SqlParameter("@Channel", SqlDbType.NChar, 20), new SqlParameter("@Cookies", SqlDbType.NChar, 20)
             };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = 1;
            commandParameters[2].Value = strPassword;
            commandParameters[3].Value = strNickName;
            commandParameters[4].Value = num;
            commandParameters[5].Value = strFace;
            commandParameters[6].Value = strYear;
            commandParameters[7].Value = strMonth;
            commandParameters[8].Value = strDay;
            commandParameters[9].Value = strEmail;
            commandParameters[10].Value = strProvince;
            commandParameters[11].Value = strCity;
            commandParameters[12].Value = strIntroNickName;
            commandParameters[13].Value = strSay;
            commandParameters[14].Value = strIP;
            commandParameters[15].Value = strDiskURL;
            commandParameters[0x10].Value = strFromURL;
            commandParameters[0x11].Value = strQQ;
            commandParameters[0x12].Value = strChannel;
            commandParameters[0x13].Value = strCookies;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeNickName(int intUserID, string strNickName)
        {
            string commandText = "Exec ROOT_Data.dbo.ChangeNickName @intUserID,@strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strNickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickName;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int ChangePS(int intUserID, string strEmail, string strPassword, string strNPassword)
        {
            string commandText = "Exec ROOT_Data.dbo.ChangePS @intUserID,@strEmail,@strPassword,@strNPassword";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strEmail", SqlDbType.NVarChar, 50), new SqlParameter("@strPassword", SqlDbType.NChar, 20), new SqlParameter("@strNPassword", SqlDbType.NChar, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strEmail;
            commandParameters[2].Value = strPassword;
            commandParameters[3].Value = strNPassword;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void ChangeSexFace(int intUserID, bool blnSex, string strFace)
        {
            string commandText = "Exec ROOT_Data.dbo.ChangeSexFace @intUserID,@blnSex,@strFace";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@blnSex", SqlDbType.Bit, 1), new SqlParameter("@strFace", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = blnSex;
            commandParameters[2].Value = strFace;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int CheckPayOrderByDate(string strMoveBillPhone, int intUserID, DateTime dtFristTime, DateTime dtToday)
        {
            string commandText = "Exec ROOT_Data.dbo.CheckPayOrderByDate @strMoveBillPhone,@intUserID,@dtFristTime,@dtToday";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strMoveBillPhone", SqlDbType.Char, 20), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@dtFristTime", SqlDbType.DateTime, 8), new SqlParameter("@dtToday", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = strMoveBillPhone;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = dtFristTime;
            commandParameters[3].Value = dtToday;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void CoinFinance(int intUserID, int intCoin, string strEvent, string strRemark, int intType)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@Coin", SqlDbType.Int, 4), new SqlParameter("@Event", SqlDbType.VarChar, 500), new SqlParameter("@Remark", SqlDbType.VarChar, 500), new SqlParameter("@Type", SqlDbType.Int, 500), new SqlParameter("@Out", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, null) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            commandParameters[3].Value = strRemark;
            commandParameters[4].Value = intType;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "CoinFinance", commandParameters);
        }

        public static int ConferCoin(int intUserID, string strNickNameA, int intCoin)
        {
            string commandText = "Exec ROOT_Data.dbo.ConferCoin @UserID,@NickNameA,@Coin";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickNameA", SqlDbType.NChar, 20), new SqlParameter("@Coin", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strNickNameA;
            commandParameters[2].Value = intCoin;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int DelFinance(int intUserID, int intFinanceID)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.DelFinance ", intUserID, ",", intFinanceID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void delMobileMsg(int intMobileMsgID)
        {
            string commandText = "Exec ROOT_Data.dbo.delMobileMsg " + intMobileMsgID;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool DelPayOrder(int intUserID, int intOrderID, string strOID)
        {
            string commandText = "Exec ROOT_Data.dbo.DelPayOrder @intUserID,@intOrderID,@strOID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intOrderID", SqlDbType.Int, 4), new SqlParameter("@strOID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intOrderID;
            commandParameters[2].Value = strOID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader Get40UserRowByUserID(int intUserID)
        {
            string commandText = "Exec dbo.GetUserRowByUserID @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetActiveCount(DateTime datDateNow)
        {
            string commandText = "Exec ROOT_Data.dbo.GetActiveCount '" + datDateNow + "'";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetAllUserCountByTime(DateTime datCreateTime, DateTime datCreateTimeA)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetAllUserCountByTime '", datCreateTime, "','", datCreateTimeA, "'" });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetAllUserReader()
        {
            string commandText = "SELECT * FROM ROOT_User ORDER BY UserID ASC";
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetAllUserTable()
        {
            string commandText = "SELECT * FROM ROOT_User ORDER BY UserID ASC";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetAllUserTableByGC(int intGameCategory)
        {
            string commandText = "SELECT U.UserID,U.Sex,U.Password,U.Category,U.Birth,U.Province,U.City,U.QQ FROM ROOT_User U,ROOT_UserGame G WHERE U.UserID=G.UserID AND G.Category=" + intGameCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static string GetBoardIDByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetBoardIDByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCoin40ByUserID(int intUserID)
        {
            string commandText = "SELECT Coin FROM ROOT_User WHERE UserID=" + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static DataTable GetCoinFinanceByUserID(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetCoinFinanceByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetCoinFinanceByUserIDNew(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetCoinFinanceByUserIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCoinFinanceCount(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetCoinFinanceCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCoinFinanceCountNew(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetCoinFinanceByUserIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCoinInCount(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = 0;
            commandParameters[2].Value = 0;
            commandParameters[3].Value = 1;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "GetCoinInFinanceList", commandParameters);
        }

        public static SqlDataReader GetCoinInFinanceList(int intUserID, int intPage, int intPageSize)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PageIndex", SqlDbType.Int, 4), new SqlParameter("@PageSize", SqlDbType.Int, 4), new SqlParameter("@DoCount", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPage;
            commandParameters[2].Value = intPageSize;
            commandParameters[3].Value = 0;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "GetCoinInFinanceList", commandParameters);
        }

        public static string GetContentByMobileMsgID(int intMsgID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetContentByMobileMsgID " + intMsgID;
            return SqlHelper.ExecuteStringReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetCount()
        {
            string commandText = "Exec ROOT_Data.dbo.GetCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetFinanceCountByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetFinanceTableByUserID 1,0,0," + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetFinanceTableByUserID(int intDoCount, int intPageIndex, int intPageSize, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetFinanceTableByUserID ", intDoCount, ",", intPageIndex, ",", intPageSize, ",", intUserID });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetFlackCount(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetFlackCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetFlackList(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetFlackList ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetHonorsByNickName(string strNickName)
        {
            string commandText = "Exec ROOT_Data.dbo.GetHonorsByNickName @strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DateTime GetLatestRegTimeByIP(string strIP)
        {
            string commandText = "Exec ROOT_Data.dbo.GetLatestRegTimeByIP @strIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strIP;
            return SqlHelper.ExecuteDateTimeDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetLoginRec(string strUserName, string strPassword, string strLoginIP)
        {
            string commandText = "Exec ROOT_Data.dbo.GetLoginRec @strUserName,@strPassword,@strLoginIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@strPassword", SqlDbType.NChar, 20), new SqlParameter("@strLoginIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            commandParameters[2].Value = strLoginIP;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetMobileMsgIDByOID(string strUserName, string strMO_MESSAGE_ID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetMobileMsgIDByOID @strUserName,@strMO_MESSAGE_ID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strUserName", SqlDbType.NChar, 20), new SqlParameter("@strMO_MESSAGE_ID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strMO_MESSAGE_ID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetNFlackCount()
        {
            string commandText = "Exec ROOT_Data.dbo.GetNFlackCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetNFlackList(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetNFlackList ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetOrderCountByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOrderCountByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetOrderCountByUserIDNew(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOrderTableByUserIDNew " + intUserID + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetOrderRowByOID(string strOID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOrderRowByOID @strOID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strOID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strOID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetOrderTableByUserID(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetOrderTableByUserID ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetOrderTableByUserIDNew(int intUserID, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetOrderTableByUserIDNew ", intUserID, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetOriginCountByTime(DateTime datTime)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOriginCountByTime @Time";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Time", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = datTime;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetOriginCountByTimeURL(DateTime datTime, string strFromURL)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOriginCountByTimeURL @Time,@FromURL";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Time", SqlDbType.DateTime, 8), new SqlParameter("@FromURL", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = datTime;
            commandParameters[1].Value = strFromURL;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetOriginList(string strBeginTime, string strEndTime)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOriginList @BeginTime,@EndTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BeginTime", SqlDbType.DateTime, 8), new SqlParameter("@EndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = Convert.ToDateTime(strBeginTime);
            commandParameters[1].Value = Convert.ToDateTime(strEndTime);
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetOriginListByURL(string strFromURL, string strBeginTime, string strEndTime)
        {
            string commandText = "Exec ROOT_Data.dbo.GetOriginListByURL @FromURL,@BeginTime,@EndTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FromURL", SqlDbType.NVarChar, 50), new SqlParameter("@BeginTime", SqlDbType.DateTime, 8), new SqlParameter("@EndTime", SqlDbType.DateTime, 8) };
            commandParameters[0].Value = strFromURL;
            commandParameters[1].Value = Convert.ToDateTime(strBeginTime);
            commandParameters[2].Value = Convert.ToDateTime(strEndTime);
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetPayTypeByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetPayTypeByUserID " + intUserID;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetPayTypeCount()
        {
            string commandText = "Exec ROOT_Data.dbo.GetPayTypeCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int GetQueryOrderCount()
        {
            string commandText = "Exec ROOT_Data.dbo.GetQueryOrderCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetQueryOrderTable(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.GetQueryOrderTable ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUPByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUPByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUPByUserID40(int intUserID)
        {
            string commandText = "Exec dbo.GetUPByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static int GetUserCountByProvince(string strProvince)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserCountByProvince @Province";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Province", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strProvince;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetUserIDByNickname(string strNickname)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strNickname;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "GetUserIDByNickName", commandParameters));
        }

        public static int GetUserIDByNickName(string strNickName)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserIDByNickName @strNickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetUserIDByUserName(string strUserName)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserIDByUserName " + strUserName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUserInfoByID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserInfoByID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUserInfoByID40(int intUserID)
        {
            string commandText = "Exec dbo.GetUserRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static DataRow GetUserRow40(int intUserID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("main40"), CommandType.StoredProcedure, "GetUserRowByUserID", commandParameters);
        }

        public static DataRow GetUserRowByNickName(string strNickName)
        {
            string commandText = "SELECT * FROM ROOT_User WHERE NickName=@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetUserRowByUIDName(string strNickName, int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserRowByUIDName @strNickName,@intUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow GetUserRowByUIDName40(string strNickName, int intUserID)
        {
            string commandText = "Exec dbo.GetUserRowByUIDName @strNickName,@intUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetUserRowByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserRowByUserID @UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetUserRowByUserName(string strUserName)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserRowByUserName @UserName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetUserRowByUserNamePWD(string strUserName, string strPassword)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserRowByUserNamePWD @UserName,@Password";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@Password", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strPassword;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetUserTableByPayType(int intPayType)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserTableByPayType " + intPayType;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataTable GetUserTableByTime(DateTime datTime)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserTableByTime '" + datTime + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static DataRow GetUserTableRowByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetUserRowByUserID " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static string GetWealthByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.GetWealthByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static string GetWealthByUserID40(int intUserID)
        {
            string commandText = "Exec dbo.GetUserWealthByUserID " + intUserID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static bool HasEmail(string strEmail)
        {
            string commandText = "Exec ROOT_Data.dbo.HasEmail @Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strEmail;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasEmail40(string strEmail)
        {
            string commandText = "Exec dbo.HasEmail @Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Email", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strEmail;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static int HasInputInfo(string strUserName, string strNickName, string strEmail)
        {
            string commandText = "Exec ROOT_Data.dbo.HasInputInfo @UserName,@NickName,@Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strEmail;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static int HasInputInfo40(string strUserName, string strNickName, string strEmail)
        {
            string commandText = "Exec dbo.HasInputInfo @UserName,@NickName,@Email";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Email", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strNickName;
            commandParameters[2].Value = strEmail;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasNickName(string strNickName)
        {
            string commandText = "Exec ROOT_Data.dbo.HasNickName @NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasNickName40(string strNickName)
        {
            string commandText = "Exec dbo.HasNickName @NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strNickName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasUserName(string strUserName)
        {
            string commandText = "Exec ROOT_Data.dbo.HasUserName @UserName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool HasUserName40(string strUserName)
        {
            string commandText = "Exec dbo.HasUserName @UserName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static int HasUserNameNickName(string strUserName, string strNickName)
        {
            string commandText = "Exec ROOT_Data.dbo.HasUserNameNickName @UserName,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NChar, 20), new SqlParameter("@NickName", SqlDbType.NChar, 20) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader ListMobileMsg()
        {
            string commandText = "Exec ROOT_Data.dbo.ListMobileMsg ";
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool ReduceCoinAdmin(string strUserName, int intCoin, string strEvent, string strRemark)
        {
            string commandText = "Exec ROOT_Data.dbo.ReduceCoinAdmin @UserName,@Coin,@Event,@Remark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserName", SqlDbType.NVarChar, 20), new SqlParameter("@Coin", SqlDbType.Int, 4), new SqlParameter("@Event", SqlDbType.NVarChar, 200), new SqlParameter("@Remark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = strUserName;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            commandParameters[3].Value = strRemark;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static bool RegAssociator(int intUserID, DateTime datMemberExpireTime)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.RegAssociator ", intUserID, ",'", datMemberExpireTime, "'" });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void RewardCoin(int intUserID, int intCoin, string strEvent)
        {
            string commandText = "Exec ROOT_Data.dbo.RewardCoin @intUserID,@intCoin,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetOrderStatus(int intOrderID, int intStatus)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetOrderStatus ", intOrderID, ",", intStatus });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static bool SetQuery(int intUserID, int intOrderID, string strOID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetQuery @intUserID,@intOrderID,@strOID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intOrderID", SqlDbType.Int, 4), new SqlParameter("@strOID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intOrderID;
            commandParameters[2].Value = strOID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetUserAsMemberByUserID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.SetUserAsMemberByUserID " + intUserID;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void SetUserCategory(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetUserCategory ", intUserID, ",", intCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int SetWealthByUserID(int intUserID, int intCategory, int GetWealth, string strEvent)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.SetWealthByUserID ", intUserID, ",", intCategory, ",", GetWealth, ",", strEvent });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static int SetWealthByUserID40(int intUserID, byte intCategory, int GetWealth, string strEvent)
        {
            string commandText = string.Concat(new object[] { "Exec dbo.SetWealthByUserID ", intUserID, ",", intCategory, ",", GetWealth, ",", strEvent });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static void SpendCoin(int intUserID, int intCoin, int intStatus, string strEvent, string strRemark)
        {
            string commandText = "Exec ROOT_Data.dbo.SpendCoin @intUserID,@intCoin,@intStatus,@strEvent,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 200), new SqlParameter("@strRemark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = intStatus;
            commandParameters[3].Value = strEvent;
            commandParameters[4].Value = strRemark;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void SpendCoin40(int intUserID, int intCoin, string strEvent, string strRemark)
        {
            string commandText = "Exec dbo.CostCoin @intUserID,@intCoin,@strEvent,@strRemark,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 200), new SqlParameter("@strRemark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            commandParameters[3].Value = strRemark;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static int SpendCoin40Return(int intUserID, int intCoin, string strEvent, string strRemark)
        {
            string commandText = "Exec dbo.CostCoinReturn @intUserID,@intCoin,@strEvent,@strRemark";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCoin", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 200), new SqlParameter("@strRemark", SqlDbType.NVarChar, 500) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intCoin;
            commandParameters[2].Value = strEvent;
            commandParameters[3].Value = strRemark;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("main40"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateActiveTimeByID(int intUserID)
        {
            string commandText = "Exec ROOT_Data.dbo.UpdateActiveTimeByID " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void UpdateActiveTimeByID40(int intUserID)
        {
            string commandText = "Exec dbo.UpdateActiveTimeByID " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("main40"), CommandType.Text, commandText);
        }

        public static void UpdateFlack(int intFlackID, int intStatus, int intCoinGain)
        {
            string commandText = string.Concat(new object[] { "Exec ROOT_Data.dbo.UpdateFlack ", intFlackID, ",", intStatus, ",", intCoinGain });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void UpdateIntroTagByUserID(int intUserID, int intTag)
        {
            string commandText = string.Concat(new object[] { "UPDATE ROOT_User SET IntroRewardTag=", intTag, " WHERE UserID=", intUserID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void UpdateLockTime()
        {
            string commandText = "UPDATE ROOT_User SET LockTime=LockTime-1 WHERE LockTime>0";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
        }

        public static void UpdatePayType()
        {
            string commandText = "SELECT UserID FROM ROOT_User WHERE PayType=1 AND MemberExpireTime<=GETDATE()";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("root"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["UserID"];
                    commandText = "UPDATE ROOT_User SET PayType=0,Face='0|0|0|0|0|0|0|0|0' WHERE UserID=" + num;
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText);
                }
            }
        }

        public static void UpdateUser(int intUserID, string strYear, string strMonth, string strDay, string strProvince, string strCity, string strSay, string strQQ, int intCategory)
        {
            string commandText = "Exec ROOT_Data.dbo.UpdateUser @intUserID,@strYear,@strMonth,@strDay,@strProvince,@strCity,@strSay,@strQQ,@intCategory";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strYear", SqlDbType.NChar, 4), new SqlParameter("@strMonth", SqlDbType.NChar, 2), new SqlParameter("@strDay", SqlDbType.NChar, 2), new SqlParameter("@strProvince", SqlDbType.NChar, 20), new SqlParameter("@strCity", SqlDbType.NChar, 20), new SqlParameter("@strSay", SqlDbType.NVarChar, 100), new SqlParameter("@strQQ", SqlDbType.NVarChar, 11), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strYear;
            commandParameters[2].Value = strMonth;
            commandParameters[3].Value = strDay;
            commandParameters[4].Value = strProvince;
            commandParameters[5].Value = strCity;
            commandParameters[6].Value = strSay;
            commandParameters[7].Value = strQQ;
            commandParameters[8].Value = intCategory;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateUserDataFromBTP(int intUserID, int intLevels, string strDevCode, string strHonor)
        {
            string commandText = "Exec ROOT_Data.dbo.UpdateUserDataFromBTP @intUserID,@intLevels,@strDevCode,@strHonor";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intLevels", SqlDbType.Int, 4), new SqlParameter("@strDevCode", SqlDbType.NVarChar, 50), new SqlParameter("@strHonor", SqlDbType.NVarChar, 300) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intLevels;
            commandParameters[2].Value = strDevCode;
            commandParameters[3].Value = strHonor;
            SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("root"), CommandType.Text, commandText, commandParameters);
        }
    }
}


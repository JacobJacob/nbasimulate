namespace Web.DBData
{
    using LoginParameter;
    using ServerManage;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPClubManager
    {
        public static void AddClub3(int intUserID, string strClubName, string strClubLogo, string strShirt)
        {
            string commandText = "Exec NewBTP.dbo.AddClub3 @intUserID,@strClubName,@strClubLogo,@strShirt";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 20), new SqlParameter("@strClubLogo", SqlDbType.NChar, 50), new SqlParameter("@strShirt", SqlDbType.NChar, 6) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClubName;
            commandParameters[2].Value = strClubLogo;
            commandParameters[3].Value = strShirt;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void AddReputation(int intClubID, int intReputation)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddReputation ", intClubID, ",", intReputation });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AssignArrange(int intClubID, int intArrange1ID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AssignArrange ", intClubID, ",", intArrange1ID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AssignArrange5(int intClubID, int intArrange1ID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AssignArrange5 ", intClubID, ",", intArrange1ID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int BuyClub5(int intUserID, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BuyClub5 ", intUserID, ",", intClubID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ChangeReputation(int intClubIDH, int intClubIDA, int intScoreH, int intScoreA)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ChangeReputation ", intClubIDH, ",", intClubIDA, ",", intScoreH, ",", intScoreA });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreateClub5()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.CreateClub5";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                CreateClub5();
            }
        }

        public static void CreateNewClub(int intUserID, string strClubName, string strClubLogo, string strShirt)
        {
            string commandText = "Exec NewBTP.dbo.CreateNewClub @intUserID,@strClubName,@strClubLogo,@strShirt";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strClubName", SqlDbType.NChar, 20), new SqlParameter("@strClubLogo", SqlDbType.NChar, 50), new SqlParameter("@strShirt", SqlDbType.NChar, 6) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = strClubName;
            commandParameters[2].Value = strClubLogo;
            commandParameters[3].Value = strShirt;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void DeleteFromLeague(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteFromLeague " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteNoneClub()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetNoneClubTable";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        int num = (int) row["ClubID"];
                        commandText = "Exec NewBTP.dbo.ClearBidDevClub " + num;
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                DeleteNoneClub();
            }
        }

        public static void DeleteSB(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.DeleteTeam " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteTeam(int intUserID)
        {
            DeleteFromLeague(GetClubIDByUIDCategory(intUserID, 5));
            DeleteVB(intUserID);
            DeleteSB(intUserID);
        }

        public static void DeleteVB(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.DeteleVB " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeteteAccount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.DeteteAccount " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllClub3()
        {
            string commandText = "Exec NewBTP.dbo.GetAllClub3";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllClub5()
        {
            string commandText = "Exec NewBTP.dbo.GetAllClub5";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCArrangeRowByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetCArrangeRowByClubID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClub3PlayerByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClub3PlayerByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubCountByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetClubCountByCategory " + intCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubCountByCategory(int intGameCategory, int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetClubCountByCategory " + intCategory;
            return SqlHelper.ExecuteIntDataField(DBLogin.ConnString(intGameCategory), CommandType.Text, commandText);
        }

        public static int GetClubIDByNickName(string strNickName, int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetClubIDByNickName @NickName,@Category";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@NickName", SqlDbType.NChar, 20), new SqlParameter("@Category", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = strNickName;
            commandParameters[1].Value = intCategory;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetClubIDByUIDCategory(int intUserID, int intCategory)
        {
            try
            {
                string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubIDUIDCategory ", intUserID, ",", intCategory });
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return GetClubIDByUIDCategory(intUserID, intCategory);
            }
        }

        public static int GetClubIDByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubIDByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetClubLogo(int intClubID)
        {
            return BTPAccountManager.GetAccountRowByUserID(GetUserIDByClubID(intClubID))["ClubLogo"].ToString().Trim();
        }

        public static string GetClubNameByClubID(int intClubID)
        {
            string commandText = "SELECT [Name] FROM BTP_Club WHERE ClubID=" + intClubID;
            return SqlHelper.ExecuteStringReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetClubNameByClubID(int intClubID, byte byteCategory, int intLength, int intDevMatchID)
        {
            string str2;
            string str3;
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubNameByDevMatchID ", intClubID, ",", intDevMatchID });
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            string str4 = "";
            if (row != null)
            {
                object obj2;
                str2 = row["ClubName"].ToString().Trim();
                int num = (int) row["UnionID"];
                str3 = row["ShortName"].ToString().Trim();
                if (num > 0)
                {
                    str3 = str3 + "-";
                }
                else
                {
                    str3 = "";
                }
                intDevMatchID = (int) row["DevMatchID"];
                int num2 = (int) row["ClubHID"];
                int num1 = (int) row["ClubAID"];
                bool flag = (bool) row["UseStaffH"];
                bool flag2 = (bool) row["UseStaffA"];
                byte num3 = (byte) row["MangerSayH"];
                byte num4 = (byte) row["MangerSayA"];
                bool flag3 = (bool) row["AddArrangeLvlH"];
                bool flag4 = (bool) row["AddArrangeLvlA"];
                if (num2 == intClubID)
                {
                    if ((flag || (num3 > 0)) || flag3)
                    {
                        str4 = "</br>";
                        if (num3 > 0)
                        {
                            obj2 = str4;
                            str4 = string.Concat(new object[] { obj2, "士气+", num3, " " });
                        }
                        if (flag3)
                        {
                            str4 = str4 + "战术等级+3 ";
                        }
                        if (flag)
                        {
                            str4 = str4 + "聘请了助理教练 ";
                        }
                    }
                }
                else if ((flag2 || (num4 > 0)) || flag4)
                {
                    str4 = "</br>";
                    if (num4 > 0)
                    {
                        obj2 = str4;
                        str4 = string.Concat(new object[] { obj2, "士气+", num4, " " });
                    }
                    if (flag4)
                    {
                        str4 = str4 + "战术等级+3 ";
                    }
                    if (flag2)
                    {
                        str4 = str4 + "聘请了助理教练 ";
                    }
                }
            }
            else
            {
                str3 = "";
                str2 = "-- --";
            }
            if (str4 == "")
            {
                return string.Concat(new object[] { "<a href='ShowClub.aspx?ClubID=", intClubID, "&Type=", byteCategory, "' title='", str3, str2, "' target='Right'>", StringItem.GetShortString(str3 + str2, intLength, "."), "</a>" });
            }
            return string.Concat(new object[] { "<a href='ShowClub.aspx?ClubID=", intClubID, "&Type=", byteCategory, "' title='", str3, str2, str4, "' target='Right' style='color:red'>", StringItem.GetShortString(str3 + str2, intLength, "."), "</a>" });
        }

        public static string GetClubNameByClubID(int intClubID, byte byteCategory, string strTarget, int intLength)
        {
            string str2;
            string str3;
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubNameByClubID ", intClubID, ",", byteCategory });
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (row != null)
            {
                str2 = row["ClubName"].ToString().Trim();
                int num = (int) row["UnionID"];
                str3 = row["ShortName"].ToString().Trim();
                if (num > 0)
                {
                    str3 = str3 + "-";
                }
                else
                {
                    str3 = "";
                }
            }
            else
            {
                str3 = "";
                str2 = "-- --";
            }
            if (strTarget == "Index")
            {
                return ("<a style='cursor:hand;' title='" + str3 + str2 + "'>" + StringItem.GetShortString(str3 + str2, intLength, ".") + "</a>");
            }
            if (strTarget == "Management")
            {
                return StringItem.GetShortString(str3 + str2, intLength, ".");
            }
            return string.Concat(new object[] { "<a href='ShowClub.aspx?ClubID=", intClubID, "&Type=", byteCategory, "' title='", str3, str2, "' target='", strTarget, "'>", StringItem.GetShortString(str3 + str2, intLength, "."), "</a>" });
        }

        public static string GetClubNameByUserID(int intUserID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubNameByUserID ", intUserID, ",", intType });
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubPrice(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubPrice " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetClubRowByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubRowByClubID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetClubRowByID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubRowByID " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetClubRowByUIDCate(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubRowByUIDCate ", intUserID, ",", intCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetClubTable()
        {
            string commandText = "Exec NewBTP.dbo.GetClubTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void GetClubTrue()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetClubTrue";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        int intClubID = (int) row["ClubID"];
                        UpdateClubInfo(intClubID);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                GetClubTrue();
            }
        }

        public static DataTable GetDevClub5Table()
        {
            string commandText = "Exec NewBTP.dbo.GetDevClub5Table";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetEventClub()
        {
            string commandText = "Exec NewBTP.dbo.GetEventClub";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetFansSum(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetFansSum " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetLogoLevel(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetLogoLevel " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetMarketCodeByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetMarketCodeByClubID " + intClubID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPCClubTable()
        {
            string commandText = "Exec NewBTP.dbo.GetPCClubTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetSellClub5Count()
        {
            string commandText = "Exec NewBTP.dbo.GetSellClub5Count";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetSellClub5Table(int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetSellClub5Table ", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUserIDByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetUserIDByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static string GetXClubNameByClubID(int intClubID, int intCategory, string strTarget, int intLength)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubNameByClubID ", intClubID, ",", intCategory });
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            string str2 = row["ClubName"].ToString().Trim();
            int num = (int) row["UnionID"];
            string str3 = row["ShortName"].ToString().Trim();
            if (num > 0)
            {
                str3 = str3 + "-";
            }
            else
            {
                str3 = "";
            }
            if (strTarget == "Index")
            {
                return ("<a style='cursor:hand;' title='" + str3 + str2 + "'>" + StringItem.GetShortString(str3 + str2, intLength, ".") + "</a>");
            }
            string str5 = "";
            str5 = DBLogin.URLString(ServerParameter.intGameCategory);
            return string.Concat(new object[] { "<a href='", str5, "ShowClub.aspx?ClubID=", intClubID, "&Type=", intCategory, "' title='", str3, str2, "' target='", strTarget, "'>", StringItem.GetShortString(str3 + str2, intLength, "."), "</a>" });
        }

        public static bool HasBuyClub(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.HasBuyClub " + intUserID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasClubName(string strClubName)
        {
            string commandText = "Exec NewBTP.dbo.HasClubName @ClubName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubName", SqlDbType.NChar, 30) };
            commandParameters[0].Value = strClubName;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetArrange3(int intClubID, int intArrange1, int intArrange2, int intArrange3, int intArrange4, int intArrange5)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetClubArrange3 ", intClubID, ",", intArrange1, ",", intArrange2, ",", intArrange3, ",", intArrange4, ",", intArrange5 });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetArrange5(int intClubID, string strArrangeDev, string strArrangeCup, string strArrangeOther)
        {
            string commandText = "Exec NewBTP.dbo.SetClubArrange5New @ClubID,@ArrangeDev,@ArrangeCup,@ArrangeOther";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@ArrangeDev", SqlDbType.NVarChar, 200), new SqlParameter("@ArrangeCup", SqlDbType.NVarChar, 200), new SqlParameter("@ArrangeOther", SqlDbType.NVarChar, 200) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strArrangeDev;
            commandParameters[2].Value = strArrangeCup;
            commandParameters[3].Value = strArrangeOther;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetArrange5(int intClubID, int intArrange1, int intArrange2, int intArrange3, int intArrange4, int intArrange5, int intArrange6, int intArrange7, int intWUse, int intLUse)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.SetClubArrange5 ", intClubID, ",", intArrange1, ",", intArrange2, ",", intArrange3, ",", intArrange4, ",", intArrange5, ",", intArrange6, ",", intArrange7, 
                ",", intWUse, ",", intLUse
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetMainXMLByClubID(int intClubID, string strMainXML)
        {
            string commandText = "Exec NewBTP.dbo.SetMainXMLByClubID @intClubID,@strMainXML";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@strMainXML", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = strMainXML;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdateAccountLevel(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.UpdateAccountLevel " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateAllAccountLevel()
        {
            string commandText = "SELECT ClubID FROM BTP_Club WHERE Category=3";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                int num2 = 1;
                foreach (DataRow row in table.Rows)
                {
                    int intClubID = (int) row["ClubID"];
                    UpdateAccountLevel(intClubID);
                    if ((num2 % 100) == 0)
                    {
                        Console.WriteLine(num2 + "个俱乐部已经更新完毕");
                    }
                    num2++;
                }
            }
        }

        public static void UpdateClubInfo(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.UpdateClubInfo " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateFanRNumber()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdateFansRNumber";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdateFanRNumber();
            }
        }

        public static int UpdatePrice(int intUserID, int intClubID, int intPrice)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdatePrice ", intUserID, ",", intClubID, ",", intPrice });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UpdatePrice(int intUserID, int intClubID, int intPrice, DateTime datEndBidTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdatePriceEndTime ", intUserID, ",", intClubID, ",", intPrice, ",'", datEndBidTime.ToString(), "'" });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPPlayer5Manager
    {
        public static void AddMessageByIsRetire()
        {
            string commandText = "Exec NewBTP.dbo.AddMessageByIsRetire ";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int AddPlayer5AutoExpByUserID(int intUserID)
        {
            int num6 = 0;
            long num7 = -1L;
            string commandText = "SELECT ClubID5,AutoTrainDev,(DATEDIFF(Hour,AutoTrainTime,getdate())) AS Hour,(DATEDIFF(minute,AutoTrainTime,getdate()))%60 AS Minute FROM BTP_Account WHERE UserID=" + intUserID;
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (row != null)
            {
                int num5;
                int num = (int) row["ClubID5"];
                int num2 = (int) row["AutoTrainDev"];
                int num3 = (int) row["Hour"];
                int num4 = (int) row["Minute"];
                string str2 = "SELECT PlayerID FROM BTP_Player5 WHERE ClubID=" + num + " AND TrainPointAuto>0";
                num7 = SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, str2);
                if (num2 <= 0)
                {
                    return num6;
                }
                if (num2 > (num3 * 3))
                {
                    if (num4 > 0)
                    {
                        num5 = ((num3 * 3) + (num4 / 20)) + 1;
                    }
                    else if (num3 > 0)
                    {
                        num5 = num3 * 3;
                    }
                    else
                    {
                        num5 = 1;
                    }
                    string str3 = string.Concat(new object[] { "UPDATE BTP_Account SET AutoTrainDev=", num5, " WHERE UserID=", intUserID });
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str3);
                }
                else
                {
                    num5 = num2;
                }
                num6 = num5;
                if (num7 < 1L)
                {
                    string str4 = "SELECT ArrangeOther,ArrangeDev FROM BTP_Club WHERE ClubID=" + num;
                    DataRow row2 = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, str4);
                    if (row2 == null)
                    {
                        return num6;
                    }
                    string str5 = row2["ArrangeOther"].ToString().Trim();
                    string str6 = row2["ArrangeDev"].ToString().Trim();
                    string str7 = "";
                    int num8 = 0;
                    if (str5 == "NO")
                    {
                        str7 = str6;
                    }
                    else
                    {
                        str7 = str5;
                    }
                    string[] arrCuter = new Cuter(str7.Replace("|", ",")).GetArrCuter();
                    for (int i = 0; i < 4; i++)
                    {
                        num8 = Convert.ToInt32(arrCuter[i]);
                        string str8 = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayer5AutoExpByUserID ", num, ",", num8, ",", num5 });
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str8);
                    }
                }
            }
            return num6;
        }

        public static void AddPlayer5ExpByUserID(int intUserID, int intPercent, int intCoin)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayer5ExpByUserID ", intUserID, ",", intPercent, ",", intCoin });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddPlayerReg(int intPlayerID, int intType, int intAge, int intHeight, int intWeight, int intAbility, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayerReg ", intPlayerID, " , ", intType, " , ", intAge, " , ", intHeight, " , ", intWeight, " , ", intAbility, " , ", intUserID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddTeamDay()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.AddPlayer5TeamDay";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                AddTeamDay();
            }
        }

        public static void AfterPlayer5CloseTran(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.AfterPlayer5CloseTran " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AfterPlayer5Transfer(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.AfterPlayer5Transfer " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void BidEndOpen(long longPlayerID, int intNumber)
        {
            try
            {
                string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.Bid_EndOpen ", longPlayerID, ",", intNumber });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "职业转会市场后期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidEndOpen(longPlayerID, intNumber);
            }
        }

        public static void BidEndRookie(long longPlayerID, int intNumber)
        {
            try
            {
                string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.Bid_EndRookie ", longPlayerID, ",", intNumber });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "职业选秀后期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidEndRookie(longPlayerID, intNumber);
            }
        }

        public static DataTable BidGetEndOpen()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetEndOpen '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable BidGetEndRookie()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_GetEndRookie '" + DateTime.Now.ToString() + "'";
                return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return BidGetEndRookie();
            }
        }

        public static DataTable BidGetOpen()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetOpen '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable BidGetRookie()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_GetRookie '" + DateTime.Now.ToString() + "'";
                return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return BidGetRookie();
            }
        }

        public static void BidPreOpen(long longPlayerID)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_PreOpen " + longPlayerID;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "职业转会市场前期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidPreOpen(longPlayerID);
            }
        }

        public static void BidPreRookie(long longPlayerID)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_PreRookie " + longPlayerID;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "职业选秀前期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidPreRookie(longPlayerID);
            }
        }

        public static bool CanPickPlayer(long longPlayerID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CanPickPlayer ", longPlayerID, ",", intUserID });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ClearPlayer5Season()
        {
            string commandText = "Exec NewBTP.dbo.ClearPlayer5Season";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ClearPlayer5Stas()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.ClearPlayer5Stas";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                ClearPlayer5Stas();
            }
        }

        public static void CreateDevPlayer(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.CreateDevPlayer " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        /*
            填充职业球员
         */

        public static void CreateFillPlayer5(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.CreateFillPlayer5 " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreatePlayer5(int intCount, int intCategory, string strEndBidTime, int intNowPoint, int intMaxPoint)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.CreatePlayer5 @intCount,@intCategory,@strEndBidTime,@intNowPoint,@intMaxPoint";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@strEndBidTime", SqlDbType.DateTime, 8), new SqlParameter("@intNowPoint", SqlDbType.Int, 4), new SqlParameter("@intMaxPoint", SqlDbType.Int, 4) };
                commandParameters[0].Value = intCount;
                commandParameters[1].Value = intCategory;
                commandParameters[2].Value = Convert.ToDateTime(strEndBidTime);
                commandParameters[3].Value = intNowPoint;
                commandParameters[4].Value = intMaxPoint;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                CreatePlayer5(intCount, intCategory, strEndBidTime, intNowPoint, intMaxPoint);
            }
        }

        public static void DeleteMVPRecord()
        {
            string commandText = "DELETE FROM BTP_MVPPlayer";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeletePlayer5(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.DeletePlayer5 " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeletePlayer5Open()
        {
            string commandText = "Exec NewBTP.dbo.DeletePlayer5Open";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void EndCloseBidMarket(long longPlayerID, int intClubIDSell, int intClubIDBuy, string strMarketCode, int intNumber, int intPrice, string strEvent, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.EndCloseBidMarket @longPlayerID,@intClubIDSell,@intClubIDBuy,@strMarketCode,@intNumber,@intPrice,@strEvent,@strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intClubIDSell", SqlDbType.Int, 4), new SqlParameter("@intClubIDBuy", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intNumber", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100), new SqlParameter("@strContent", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intClubIDSell;
            commandParameters[2].Value = intClubIDBuy;
            commandParameters[3].Value = strMarketCode;
            commandParameters[4].Value = intNumber;
            commandParameters[5].Value = intPrice;
            commandParameters[6].Value = strEvent;
            commandParameters[7].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void EndOpenBidMarket(long longPlayerID, int intClubIDSell, int intClubIDBuy, int intNumber, int intPrice, string strEvent, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.EndOpenBidMarket @longPlayerID,@intClubIDSell,@intClubIDBuy,@intNumber,@intPrice,@strEvent,@strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intClubIDSell", SqlDbType.Int, 4), new SqlParameter("@intClubIDBuy", SqlDbType.Int, 4), new SqlParameter("@intNumber", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100), new SqlParameter("@strContent", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intClubIDSell;
            commandParameters[2].Value = intClubIDBuy;
            commandParameters[3].Value = intNumber;
            commandParameters[4].Value = intPrice;
            commandParameters[5].Value = strEvent;
            commandParameters[6].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void FirePlayer5ByPlayerID(int intClubID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.FirePlayer5ByPlayerID ", intClubID, ",", longPlayerID });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrage5PlayerList(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrage5PlayerList " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrPlayerTable(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrPlayer5Table " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAssistTop12Table(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetAssistTop12Table @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetAutoTrainPlayerList(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetAutoTrainPlayerList " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetBlockTop12Table(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetBlockTop12Table @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static long GetCanPlay5ID(long[] longIDs, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetCanPlay5ID ", intClubID, ",", longIDs[0], ",", longIDs[1], ",", longIDs[2], ",", longIDs[3], ",", longIDs[4] });
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetClub5PlayerByHeight(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClub5PlayerByHeight " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubAllSalaryByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubAllSalaryByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubSalaryByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubSalaryByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetClubSalaryByPlayerID(long lngPlayerIDC, long lngPlayerIDPF, long lngPlayerIDSF, long lngPlayerIDSG, long lngPlayerIDPG)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetClubSalaryByPlayerID ", lngPlayerIDC, ",", lngPlayerIDPF, ",", lngPlayerIDSF, ",", lngPlayerIDSG, ",", lngPlayerIDPG });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetEventPlayer5(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetEventPlayer5 " + intClubID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGameCapital()
        {
            string commandText = "Exec NewBTP.dbo.GetGameCapital ";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static long GetGameCapitalLong()
        {
            string commandText = "Exec NewBTP.dbo.GetGameCapital ";
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetMVPPlayer(int intClubHID, int intClubAID, int intScoreH, int intScoreA)
        {
            int num;
            if (intScoreH > intScoreA)
            {
                num = intClubHID;
            }
            else
            {
                num = intClubAID;
            }
            string commandText = "Exec NewBTP.dbo.GetMVPPlayer " + num;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetMVPTable(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetMVPTable @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetOnPlayer5TableByCID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetOnPlayer5TableByCID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOpenBidTable()
        {
            string commandText = "Exec NewBTP.dbo.GetOpenBidTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5Cost(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerCost5 " + longPlayerID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5CountByCID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer5CountByCID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5CountByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer5CountByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5HappyNotFullCount(int intClubID)
        {
            SqlParameter parameter = new SqlParameter("@ClubID", SqlDbType.Int, 4);
            parameter.Value = intClubID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer5HappyNotFullCount", new SqlParameter[] { parameter });
        }

        public static long GetPlayer5IDByPlayerName(string strPlayerName)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer5IDByPlayerName @PlayerName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strPlayerName;
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetPlayer5NumByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer5NumByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer5PowerNotFullCount(int intClubID)
        {
            SqlParameter parameter = new SqlParameter("@ClubID", SqlDbType.Int, 4);
            parameter.Value = intClubID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer5PowerNotFullCount", new SqlParameter[] { parameter });
        }

        public static DataTable GetPlayerListByCategory(int intCategory)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerListByCategory " + intCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetPlayerRowByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerRowByPlayerID5 " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPlayerTableByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer5TableByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPlayerTableByID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerTableByID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPTrainTableByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetP5TrainTableByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetReboundTop12Table(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetReboundTop12Table @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSelectPlayer5Table()
        {
            string commandText = "Exec NewBTP.dbo.GetSelectPlayer5Table";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetSelectPlayerRowByID(long lngPlayerID)
        {
            SqlParameter parameter = new SqlParameter("@PlayerID", SqlDbType.BigInt, 8);
            parameter.Value = lngPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetSelectPlayerRowByID", new SqlParameter[] { parameter });
        }

        public static int GetSellClub5PCount(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetSellClub5PCount " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static long GetSellMoneyPlayer5(int intUserID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetSellMoneyPlayer5 ", intUserID, ",", longPlayerID });
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetSellPlayer5Table(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetSellPlayer5Table " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetShirtSum(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetShirtSum " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStealTop12Table(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetStealTop12Table @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTableOderByShirt(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetTableOderByShirt " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTop10Table()
        {
            string commandText = "Exec NewBTP.dbo.GetTop10Table";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTop12Table(string strDevCode)
        {
            string commandText = "Exec NewBTP.dbo.GetTop12Table @strDevCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = strDevCode;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetTop5ByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetTop5ByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasPickPlayer(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.HasPickPlayer " + intUserID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int NewSellPlayer5(int intUserID, long longPlayerID, string strMarketCode, long intBidPrice, DateTime datEndBidTime, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.NewSellPlayer5 @intUserID,@longPlayerID,@strMarketCode,@intBidPrice,@datEndBidTime,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intBidPrice", SqlDbType.Int, 4), new SqlParameter("@datEndBidTime", SqlDbType.DateTime, 8), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = strMarketCode;
            commandParameters[3].Value = intBidPrice;
            commandParameters[4].Value = datEndBidTime;
            commandParameters[5].Value = strEvent;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int PickPlayer(long longPlayerID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.PickPlayer ", longPlayerID, ",", intUserID });
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player5AddAge()
        {
            string commandText = "Exec NewBTP.dbo.Player5AddAge";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player5Aging()
        {
            string commandText = "Exec NewBTP.dbo.Player5Aging";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player5Contract()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Player5Contract";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                Player5Contract();
            }
        }

        public static void Player5Holiday()
        {
            string commandText = "Exec NewBTP.dbo.Player5Holiday";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable Player5Retire()
        {
            string commandText = "Exec NewBTP.dbo.Player5PreRetire";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player5RetireMsg()
        {
            string commandText = "Exec NewBTP.dbo.Player5PreRetire";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int intClubID = (int) row["ClubID"];
                    long num1 = (long) row["PlayerID"];
                    string str2 = row["Name"].ToString().Trim();
                    int num2 = (byte) row["Age"];
                    string strContent = str2 + "宣布将于本赛季结束后退役。";
                    BTPMessageManager.AddMessageByClubID5(intClubID, strContent);
                    string clubNameByClubID = BTPClubManager.GetClubNameByClubID(intClubID);
                    string strLogEvent = string.Concat(new object[] { clubNameByClubID, "中的", str2, "（", num2, "岁）宣布将于本赛季结束后退役。" });
                    BTPDevManager.SetDevLogByClubID5(intClubID, strLogEvent);
                }
            }
        }

        public static void PlayerLevelDown(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.PlayerLevelDown " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int RecoverHealthy5()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RecoverHealthy5";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return RecoverHealthy5();
            }
        }

        public static int RecoverPower5()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RecoverPower5";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return RecoverPower5();
            }
        }

        public static void RenewName5()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RenewName5";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                RenewName5();
            }
        }

        public static void ReplacePlayerByPlayID(int intPlayerID, int intType, int intAge, int intHeight, int intWeight, int intSpeed, int intJump, int intStrength, int intStamina, int intShot, int intPoint3, int intDribble, int intPass, int intRebound, int intSteal, int intBlock, int intAttack, int intDefense, int intTeam, int intSpeedMax, int intJumpMax, int intStrengthMax, int intStaminaMax, int intShotMax, int intPoint3Max, int intDribbleMax, int intPassMax, int intReboundMax, int intStealMax, int intBlockMax, int intAttackMax, int intDefenseMax, int intTeamMax, int intPos, DateTime dtEndBidTime, int intCategory, int intBidPrice, string strName, int intRetireAge, int intPlayedYear)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.ReplacePlayerByPlayID ", intPlayerID, ",", intType, ",", intAge, ",", intHeight, ",", intWeight, ",", intSpeed, ",", intJump, ",", intStrength, 
                ",", intStamina, ",", intShot, ",", intPoint3, ",", intDribble, ",", intPass, ",", intRebound, ",", intSteal, ",", intBlock, 
                ",", intAttack, ",", intDefense, ",", intTeam, ",", intSpeedMax, ",", intJumpMax, ",", intStrengthMax, ",", intStaminaMax, ",", intShotMax, 
                ",", intPoint3Max, ",", intDribbleMax, ",", intPassMax, ",", intReboundMax, ",", intStealMax, ",", intBlockMax, ",", intAttackMax, ",", intDefenseMax, 
                ",", intTeamMax, ",", intPos, ",'", dtEndBidTime.ToString(), "',", intCategory, ",", intBidPrice, ",'", strName, "',", intRetireAge, ",", intPlayedYear
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ResetAllPlayerPop()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.ResetAllPlayerPop";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                ResetAllPlayerPop();
            }
        }

        public static void ResetAllPlayerShirt()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.ResetAllPlayerShirt";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                ResetAllPlayerShirt();
            }
        }

        public static void RewordMVPPlayer()
        {
            string commandText = "Exec NewBTP.dbo.GetDistinctDevTable";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string str2 = row[0].ToString().Trim();
                    commandText = "Exec NewBTP.dbo.RewordMVPByDevCode @strDevCode";
                    SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strDevCode", SqlDbType.Char, 20) };
                    commandParameters[0].Value = str2;
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
                }
            }
        }

        public static DataRow SellPlayer5(int intUserID, long longPlayerID, string strMarketCode, int intBidPrice, DateTime datEndBidTime, int intSellAss)
        {
            string commandText = "Exec NewBTP.dbo.SellPlayer5 @intUserID,@longPlayerID,@strMarketCode,@intBidPrice,@datEndBidTime,@intSellAss";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intBidPrice", SqlDbType.Int, 4), new SqlParameter("@datEndBidTime", SqlDbType.DateTime, 8), new SqlParameter("@intSellAss", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = strMarketCode;
            commandParameters[3].Value = intBidPrice;
            commandParameters[4].Value = datEndBidTime;
            commandParameters[5].Value = intSellAss;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SendHealthyMessage()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetSuspend5Table";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string str3;
                        string str2 = row["Name"].ToString().Trim();
                        int intClubID = (int) row["ClubID"];
                        int userIDByClubID = BTPClubManager.GetUserIDByClubID(intClubID);
                        int num3 = (byte) row["Status"];
                        if (num3 == 2)
                        {
                            str3 = str2 + "康复归队！";
                        }
                        else
                        {
                            str3 = str2 + "回到队中！";
                        }
                        commandText = "Exec NewBTP.dbo.AddNewMessage @intUserID,2,0,'秘书报告',@strContent";
                        SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strContent", SqlDbType.NVarChar, 0x3e8) };
                        commandParameters[0].Value = userIDByClubID;
                        commandParameters[1].Value = str3;
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                SendHealthyMessage();
            }
        }

        public static void SendSalary()
        {
            try
            {
                DataTable table = BTPClubManager.GetDevClub5Table();
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        int num = (int) row["ClubID"];
                        int num2 = (int) row["UserID"];
                        string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SendPlayer5Salary ", num, ",", num2 });
                        SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                SendSalary();
            }
        }

        public static bool SetAssignDevPlayerInClub(string strPlayerIDA, string strPlayerIDB, string strPlayerIDC, string strPlayerIDD, string strPlayerIDE, string strPlayerIDF, string strPlayerIDG, string strPlayerIDH, int intValue, int intClubID, int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetAssignDevPlayerInClub @PlayerIDA,@PlayerIDB,@PlayerIDC,@PlayerIDD,@PlayerIDE,@PlayerIDF,@PlayerIDG,@PlayerIDH,@Value,@ClubID,@UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerIDA", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDB", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDC", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDD", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDE", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDF", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDG", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDH", SqlDbType.BigInt, 8), new SqlParameter("@Value", SqlDbType.Int, 4), new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strPlayerIDA;
            commandParameters[1].Value = strPlayerIDB;
            commandParameters[2].Value = strPlayerIDC;
            commandParameters[3].Value = strPlayerIDD;
            commandParameters[4].Value = strPlayerIDE;
            commandParameters[5].Value = strPlayerIDF;
            commandParameters[6].Value = strPlayerIDG;
            commandParameters[7].Value = strPlayerIDH;
            commandParameters[8].Value = intValue;
            commandParameters[9].Value = intClubID;
            commandParameters[10].Value = intUserID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetAwarenessTrain(long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetAwarenessTrain ", longPlayerID, ",", intType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetIsStaffByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.SetIsStaffByClubID " + intClubID + "";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetMarketLvlByPlayerID(long longPlayerID, int MarketLvl)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetMarketLvlByPlayerID ", longPlayerID, ",", MarketLvl });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetPlayer3to5(int intClubID, long longPlayerID, int intNumber, string strMarketCode)
        {
            string commandText = "Exec NewBTP.dbo.SetPlayer3to5 @intClubID,@longPlayerID,@intNumber,@strMarketCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intNumber", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = intNumber;
            commandParameters[3].Value = strMarketCode;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetPlayer5Bid(long longPlayerID, int intBidPrice)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetPlayer5Bid ", longPlayerID, ",", intBidPrice });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetPlayer5Event(long longPlayerID, string strEvent)
        {
            int num = RandomItem.rnd.Next(3, 5) + RandomItem.rnd.Next(0, 3);
            string commandText = "UPDATE BTP_Player5 SET Status=3,Event=@strEvent,Suspend=@intSuspend WHERE PlayerID=@longPlayerID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strEvent", SqlDbType.NVarChar, 0xff), new SqlParameter("@intSuspend", SqlDbType.TinyInt, 1), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8) };
            commandParameters[0].Value = strEvent;
            commandParameters[1].Value = num;
            commandParameters[2].Value = longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetPlayer5Holiday(long longPlayerID, string strPlayerName, int intSalary, int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetPlayer5Holiday @longPlayerID,@strPlayerName,@intSalary,@intUserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@strPlayerName", SqlDbType.NVarChar, 20), new SqlParameter("@intSalary", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = strPlayerName;
            commandParameters[2].Value = intSalary;
            commandParameters[3].Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetTrain(long longPlayerID, int intTrainType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetTrain ", longPlayerID, ",", intTrainType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow TrainPlayer3ByPlayerID(long lngPlayerID, int intClubID, int[] AddAbility)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Type1", SqlDbType.Int, 4), new SqlParameter("@Type2", SqlDbType.Int, 4), new SqlParameter("@Type3", SqlDbType.Int, 4), new SqlParameter("@Type4", SqlDbType.Int, 4), new SqlParameter("@Type5", SqlDbType.Int, 4), new SqlParameter("@Type6", SqlDbType.Int, 4), new SqlParameter("@Type7", SqlDbType.Int, 4), new SqlParameter("@Type8", SqlDbType.Int, 4), new SqlParameter("@Type9", SqlDbType.Int, 4), new SqlParameter("@Type10", SqlDbType.Int, 4), new SqlParameter("@Type11", SqlDbType.Int, 4) };
            commandParameters[0].Value = lngPlayerID;
            commandParameters[1].Value = intClubID;
            for (int i = 0; i < 11; i++)
            {
                commandParameters[i + 2].Value = AddAbility[i];
            }
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "TrainPlayer3ByPlayerID", commandParameters);
        }

        public static void TrainPlayer5()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.TrainPlayer5";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                TrainPlayer5();
            }
        }

        public static int TrainPlayer5ByPlayerID(long lngPlayerID, int intClubID, int[] AddAbility)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Type1", SqlDbType.Int, 4), new SqlParameter("@Type2", SqlDbType.Int, 4), new SqlParameter("@Type3", SqlDbType.Int, 4), new SqlParameter("@Type4", SqlDbType.Int, 4), new SqlParameter("@Type5", SqlDbType.Int, 4), new SqlParameter("@Type6", SqlDbType.Int, 4), new SqlParameter("@Type7", SqlDbType.Int, 4), new SqlParameter("@Type8", SqlDbType.Int, 4), new SqlParameter("@Type9", SqlDbType.Int, 4), new SqlParameter("@Type10", SqlDbType.Int, 4), new SqlParameter("@Type11", SqlDbType.Int, 4) };
            commandParameters[0].Value = lngPlayerID;
            commandParameters[1].Value = intClubID;
            for (int i = 0; i < 11; i++)
            {
                commandParameters[i + 2].Value = AddAbility[i];
            }
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "TrainPlayer5ByPlayerID", commandParameters);
        }

        public static DataRow TrainPlayer5ByPoint(long longPlayerID, int intTrainType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.TrainPlayer5ByPoint ", longPlayerID, ",", intTrainType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int TrainPlayer5ByPointNew(long lngPlayerID, int intType, int intPoint)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@Point", SqlDbType.Int, 4) };
            commandParameters[0].Value = lngPlayerID;
            commandParameters[1].Value = intType;
            commandParameters[2].Value = intPoint;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "TrainPlayer5ByPointNew", commandParameters);
        }

        public static void UpdateAwarenessTrain()
        {
            string commandText = "Exec NewBTP.dbo.UpdateAwarenessTrain";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateContractByPlayerID(long longPlayerID, int intContract, int intSalary)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateContractByPlayerID ", longPlayerID, ",", intContract, ",", intSalary });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateEndBidTime(long longPlayerID, DateTime datEndBidTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateEndBidTime5 ", longPlayerID, ",'", datEndBidTime.ToString(), "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateNumPosByPlayerID5(long longPlayerID, int intNumber, int intPosition)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateNumPosByPlayerID5 ", longPlayerID, ",", intNumber, ",", intPosition });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetTrialPlayer(int intUserID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetTrialPlayer ", intUserID, ",", longPlayerID});
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UnSetTrialPlayer(int clubID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UnSetTrialPlayer ", clubID, ',', longPlayerID });
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        

        public static void UpdateNumPosNameByPlayerID5(long longPlayerID, int intNumber, int intPosition, string strPlayerName)
        {
            string commandText = "Exec NewBTP.dbo.UpdateNumPosNameByPlayerID5 @PlayerID,@Number,@Position,@PlayerName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Number", SqlDbType.TinyInt, 1), new SqlParameter("@Position", SqlDbType.TinyInt, 1), new SqlParameter("@PlayerName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intNumber;
            commandParameters[2].Value = intPosition;
            commandParameters[3].Value = strPlayerName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdatePlayer5Ability()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdatePlayer5Ability";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdatePlayer5Ability();
            }
        }

        public static void UPDATEPlayer5AbilityPV()
        {
            string commandText = "Exec NewBTP.dbo.UPDATEPlayer5AbilityPV";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UpdatePlayer5Body(long longPlayerID, int intClubID5, string strPlayerBody, int intUserID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.UpdatePlayer5Body @PlayerID,@ClubID5,@PlayerBody,@UserID,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@ClubID5", SqlDbType.Int, 4), new SqlParameter("@PlayerBody", SqlDbType.NVarChar, 100), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intClubID5;
            commandParameters[2].Value = strPlayerBody;
            commandParameters[3].Value = intUserID;
            commandParameters[4].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdatePlayer5PV()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdatePlayer5PV";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdatePlayer5PV();
            }
        }

        public static void UPDATEPlayer5Salary()
        {
            string commandText = "Exec NewBTP.dbo.UPDATEPlayer5Salary";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdatePlayer5TrainPoint(long longPlayerID, int intHappy, int intPower, int intTrainPoint, int intStatus, string strEvent, int intSuspend)
        {
            string commandText = "Exec NewBTP.dbo.UpdatePlayer5TrainPoint @longPlayerID,@intHappy,@intPower,@intTrainPoint,@intStatus,@strEvent,@intSuspend";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intHappy", SqlDbType.TinyInt, 1), new SqlParameter("@intPower", SqlDbType.TinyInt, 1), new SqlParameter("@intTrainPoint", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.TinyInt, 1), new SqlParameter("@strEvent", SqlDbType.NVarChar, 0xff), new SqlParameter("@intSuspend", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intHappy;
            commandParameters[2].Value = intPower;
            commandParameters[3].Value = intTrainPoint;
            commandParameters[4].Value = intStatus;
            commandParameters[5].Value = strEvent;
            commandParameters[6].Value = intSuspend;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdatePlayerPower(long lngPlayerID, int intPower)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Power", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = lngPlayerID;
            commandParameters[1].Value = intPower;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdatePlayerPower", commandParameters);
        }

        public static void UpdatePlayerStas(long longPlayerID, int intScore, int intRebound, int intAssist, int intBlock, int intSteal, bool blnPlayed, int intHappy, int intPower, int intTrainPoint, int intStatus, string strEvent, int intSuspend)
        {
            string commandText = "Exec NewBTP.dbo.UpdatePlayer5Stas @longPlayerID,@intScore,@intRebound,@intAssist,@intBlock,@intSteal,@intHappy,@intPower,@intTrainPoint,@intStatus,@strEvent,@intSuspend,@Played";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intScore", SqlDbType.Int, 4), new SqlParameter("@intRebound", SqlDbType.Int, 4), new SqlParameter("@intAssist", SqlDbType.Int, 4), new SqlParameter("@intBlock", SqlDbType.Int, 4), new SqlParameter("@intSteal", SqlDbType.Int, 4), new SqlParameter("@intHappy", SqlDbType.TinyInt, 1), new SqlParameter("@intPower", SqlDbType.TinyInt, 1), new SqlParameter("@intTrainPoint", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.TinyInt, 1), new SqlParameter("@strEvent", SqlDbType.NVarChar, 0xff), new SqlParameter("@intSuspend", SqlDbType.TinyInt, 1), new SqlParameter("@Played", SqlDbType.Bit, 1) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intScore;
            commandParameters[2].Value = intRebound;
            commandParameters[3].Value = intAssist;
            commandParameters[4].Value = intBlock;
            commandParameters[5].Value = intSteal;
            commandParameters[6].Value = intHappy;
            commandParameters[7].Value = intPower;
            commandParameters[8].Value = intTrainPoint;
            commandParameters[9].Value = intStatus;
            commandParameters[10].Value = strEvent;
            commandParameters[11].Value = intSuspend;
            commandParameters[12].Value = blnPlayed;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdatePlayerStasType7(long longPlayerID, int intHappy, int intPower, int intStatus, int intSuspend, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.UpdatePlayerStasType7 @longPlayerID,@intHappy,@intPower,@intStatus,@intSuspend,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intHappy", SqlDbType.Int, 4), new SqlParameter("@intPower", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.Int, 4), new SqlParameter("@intSuspend", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 0xff) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intHappy;
            commandParameters[2].Value = intPower;
            commandParameters[3].Value = intStatus;
            commandParameters[4].Value = intSuspend;
            commandParameters[5].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


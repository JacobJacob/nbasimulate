namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;
    using Web.Helper;

    public class BTPPlayer3Manager
    {
        public static int AddPlayerAutoExpByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.AddPlayerAutoExpByUserID " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddPlayerExpByUserID(int intUserID, int intPercent, int intCoin)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayerExpByUserID ", intUserID, ",", intPercent, ",", intCoin });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow AddPlayerMaxByPlayerID(long lngPlayerID, int intUserID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayerMaxByPlayerID ", lngPlayerID, ",", intUserID });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddSkillMax(int intClubID, long longPlayerID, int intRndAdd, int intPos, string strName)
        {
            string str;
            string str2;
            int num = RandomItem.rnd.Next(0, 100);
            if (intPos == 1)
            {
                if (num < 5)
                {
                    str = "Speed";
                    str2 = "速度";
                }
                else if ((num >= 5) && (num < 10))
                {
                    str = "Jump";
                    str2 = "弹跳";
                }
                else if ((num >= 10) && (num < 30))
                {
                    str = "Strength";
                    str2 = "强壮";
                }
                else if ((num >= 30) && (num < 40))
                {
                    str = "Stamina";
                    str2 = "耐力";
                }
                else if ((num >= 40) && (num < 0x2d))
                {
                    str = "Shot";
                    str2 = "投篮";
                }
                else if ((num >= 0x2d) && (num < 0x2d))
                {
                    str = "Point3";
                    str2 = "三分";
                }
                else if ((num >= 0x2d) && (num < 0x2d))
                {
                    str = "Dribble";
                    str2 = "运球";
                }
                else if ((num >= 0x2d) && (num < 0x2d))
                {
                    str = "Pass";
                    str2 = "传球";
                }
                else if ((num >= 0x2d) && (num < 0x41))
                {
                    str = "Rebound";
                    str2 = "篮板";
                }
                else if ((num >= 0x41) && (num < 0x41))
                {
                    str = "Steal";
                    str2 = "抢断";
                }
                else if ((num >= 0x41) && (num < 0x55))
                {
                    str = "Block";
                    str2 = "封盖";
                }
                else if ((num >= 0x55) && (num < 90))
                {
                    str = "Attack";
                    str2 = "进攻意识";
                }
                else if ((num >= 90) && (num < 0x5f))
                {
                    str = "Defense";
                    str2 = "防守意识";
                }
                else
                {
                    str = "Team";
                    str2 = "团队意识";
                }
            }
            else if (intPos == 2)
            {
                if (num < 7)
                {
                    str = "Speed";
                    str2 = "速度";
                }
                else if ((num >= 7) && (num < 12))
                {
                    str = "Jump";
                    str2 = "弹跳";
                }
                else if ((num >= 12) && (num < 0x1b))
                {
                    str = "Strength";
                    str2 = "强壮";
                }
                else if ((num >= 0x1b) && (num < 0x25))
                {
                    str = "Stamina";
                    str2 = "耐力";
                }
                else if ((num >= 0x25) && (num < 0x2c))
                {
                    str = "Shot";
                    str2 = "投篮";
                }
                else if ((num >= 0x2c) && (num < 0x2c))
                {
                    str = "Point3";
                    str2 = "三分";
                }
                else if ((num >= 0x2c) && (num < 0x2c))
                {
                    str = "Dribble";
                    str2 = "运球";
                }
                else if ((num >= 0x2c) && (num < 0x2f))
                {
                    str = "Pass";
                    str2 = "传球";
                }
                else if ((num >= 0x2f) && (num < 0x43))
                {
                    str = "Rebound";
                    str2 = "篮板";
                }
                else if ((num >= 0x43) && (num < 70))
                {
                    str = "Steal";
                    str2 = "抢断";
                }
                else if ((num >= 70) && (num < 0x55))
                {
                    str = "Block";
                    str2 = "封盖";
                }
                else if ((num >= 0x55) && (num < 90))
                {
                    str = "Attack";
                    str2 = "进攻意识";
                }
                else if ((num >= 90) && (num < 0x5f))
                {
                    str = "Defense";
                    str2 = "防守意识";
                }
                else
                {
                    str = "Team";
                    str2 = "团队意识";
                }
            }
            else if (intPos == 3)
            {
                if (num < 10)
                {
                    str = "Speed";
                    str2 = "速度";
                }
                else if ((num >= 10) && (num < 15))
                {
                    str = "Jump";
                    str2 = "弹跳";
                }
                else if ((num >= 15) && (num < 0x19))
                {
                    str = "Strength";
                    str2 = "强壮";
                }
                else if ((num >= 0x19) && (num < 0x23))
                {
                    str = "Stamina";
                    str2 = "耐力";
                }
                else if ((num >= 0x23) && (num < 0x2d))
                {
                    str = "Shot";
                    str2 = "投篮";
                }
                else if ((num >= 0x2d) && (num < 0x37))
                {
                    str = "Point3";
                    str2 = "三分";
                }
                else if ((num >= 0x37) && (num < 60))
                {
                    str = "Dribble";
                    str2 = "运球";
                }
                else if ((num >= 60) && (num < 0x41))
                {
                    str = "Pass";
                    str2 = "传球";
                }
                else if ((num >= 0x41) && (num < 0x4b))
                {
                    str = "Rebound";
                    str2 = "篮板";
                }
                else if ((num >= 0x4b) && (num < 80))
                {
                    str = "Steal";
                    str2 = "抢断";
                }
                else if ((num >= 80) && (num < 0x55))
                {
                    str = "Block";
                    str2 = "封盖";
                }
                else if ((num >= 0x55) && (num < 90))
                {
                    str = "Attack";
                    str2 = "进攻意识";
                }
                else if ((num >= 90) && (num < 0x5f))
                {
                    str = "Defense";
                    str2 = "防守意识";
                }
                else
                {
                    str = "Team";
                    str2 = "团队意识";
                }
            }
            else if (intPos == 4)
            {
                if (num < 10)
                {
                    str = "Speed";
                    str2 = "速度";
                }
                else if ((num >= 10) && (num < 15))
                {
                    str = "Jump";
                    str2 = "弹跳";
                }
                else if ((num >= 15) && (num < 15))
                {
                    str = "Strength";
                    str2 = "强壮";
                }
                else if ((num >= 15) && (num < 0x19))
                {
                    str = "Stamina";
                    str2 = "耐力";
                }
                else if ((num >= 0x19) && (num < 0x2d))
                {
                    str = "Shot";
                    str2 = "投篮";
                }
                else if ((num >= 0x2d) && (num < 0x41))
                {
                    str = "Point3";
                    str2 = "三分";
                }
                else if ((num >= 0x41) && (num < 0x49))
                {
                    str = "Dribble";
                    str2 = "运球";
                }
                else if ((num >= 0x49) && (num < 0x4e))
                {
                    str = "Pass";
                    str2 = "传球";
                }
                else if ((num >= 0x4e) && (num < 0x4e))
                {
                    str = "Rebound";
                    str2 = "篮板";
                }
                else if ((num >= 0x4e) && (num < 0x55))
                {
                    str = "Steal";
                    str2 = "抢断";
                }
                else if ((num >= 0x55) && (num < 0x55))
                {
                    str = "Block";
                    str2 = "封盖";
                }
                else if ((num >= 0x55) && (num < 90))
                {
                    str = "Attack";
                    str2 = "进攻意识";
                }
                else if ((num >= 90) && (num < 0x5f))
                {
                    str = "Defense";
                    str2 = "防守意识";
                }
                else
                {
                    str = "Team";
                    str2 = "团队意识";
                }
            }
            else if (num < 10)
            {
                str = "Speed";
                str2 = "速度";
            }
            else if ((num >= 10) && (num < 15))
            {
                str = "Jump";
                str2 = "弹跳";
            }
            else if ((num >= 15) && (num < 15))
            {
                str = "Strength";
                str2 = "强壮";
            }
            else if ((num >= 15) && (num < 0x19))
            {
                str = "Stamina";
                str2 = "耐力";
            }
            else if ((num >= 0x19) && (num < 40))
            {
                str = "Shot";
                str2 = "投篮";
            }
            else if ((num >= 40) && (num < 50))
            {
                str = "Point3";
                str2 = "三分";
            }
            else if ((num >= 50) && (num < 60))
            {
                str = "Dribble";
                str2 = "运球";
            }
            else if ((num >= 60) && (num < 80))
            {
                str = "Pass";
                str2 = "传球";
            }
            else if ((num >= 80) && (num < 80))
            {
                str = "Rebound";
                str2 = "篮板";
            }
            else if ((num >= 80) && (num < 0x55))
            {
                str = "Steal";
                str2 = "抢断";
            }
            else if ((num >= 0x55) && (num < 0x55))
            {
                str = "Block";
                str2 = "封盖";
            }
            else if ((num >= 0x55) && (num < 90))
            {
                str = "Attack";
                str2 = "进攻意识";
            }
            else if ((num >= 90) && (num < 0x5f))
            {
                str = "Defense";
                str2 = "防守意识";
            }
            else
            {
                str = "Team";
                str2 = "团队意识";
            }
            string commandText = string.Concat(new object[] { "UPDATE BTP_Player3 SET ", str, "Max=", str, "Max+", intRndAdd, " WHERE ", str, "Max<700 AND PlayerID=", longPlayerID });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            commandText = string.Concat(new object[] { "SELECT ", str, "Max FROM BTP_Player3 WHERE PlayerID=", longPlayerID });
            if (SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) < 700)
            {
                BTPMessageManager.AddMessage(BTPClubManager.GetUserIDByClubID(intClubID), 2, 0, "训练员报告", string.Concat(new object[] { strName, "在刻苦的训练中", str2, "潜力提升", ((float) intRndAdd) / 10f, "" }));
            }
        }

        public static void AddTeamDay()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.AddPlayer3TeamDay";
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

        public static void AfterPlayer3Transfer(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.AfterPlayer3Transfer " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ArrangeNumber(long longPlayerID, int intNumber)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ArrangeNumber ", longPlayerID, ",", intNumber });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void BidEndClose(long longPlayerID, int intNumber, string strMarketCode)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_EndClose @longPlayerID,@intNumber,@strMarketCode";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intNumber", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
                commandParameters[0].Value = longPlayerID;
                commandParameters[1].Value = intNumber;
                commandParameters[2].Value = strMarketCode;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "街球选秀市场后期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidEndClose(longPlayerID, intNumber, strMarketCode);
            }
        }

        public static void BidEndFree(long longPlayerID, int intNumber)
        {
            try
            {
                string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.Bid_EndFree ", longPlayerID, ",", intNumber });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "自由转会市场后期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidEndFree(longPlayerID, intNumber);
            }
        }

        public static DataTable BidGetClose()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetClose '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable BidGetEndClose()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetEndClose '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable BidGetEndFree()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetEndFree '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable BidGetFree()
        {
            string commandText = "Exec NewBTP.dbo.Bid_GetFree '" + DateTime.Now.ToString() + "'";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void BidPreClose(long longPlayerID)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_PreClose " + longPlayerID;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "街球选秀市场前期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidPreClose(longPlayerID);
            }
        }

        public static void BidPreFree(long longPlayerID)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.Bid_PreFree " + longPlayerID;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Concat(new object[] { "自由转会市场前期更新错误PlayerID：", longPlayerID, "\n", exception.ToString() }));
                Thread.Sleep(0x7d0);
                BidPreFree(longPlayerID);
            }
        }

        public static void ClearPlayer3Season()
        {
            string commandText = "Exec NewBTP.dbo.ClearPlayer3Season";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreateFillPlayer3(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.CreateFillPlayer3 " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreatePlayer3(int intCount, int intCategory, string strEndBidTime)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.CreatePlayer3 @intCount,@intCategory,@strEndBidTime";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intCount", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@strEndBidTime", SqlDbType.NVarChar, 20) };
                commandParameters[0].Value = intCount;
                commandParameters[1].Value = intCategory;
                commandParameters[2].Value = strEndBidTime;
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                CreatePlayer3(intCount, intCategory, strEndBidTime);
            }
        }

        public static void CreateSelectPlayer3(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.CreateSelectPlayer3 " + intClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeletePlayer3(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.DeletePlayer3 " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeletePlayer3ByCIDCat(int intClubID, int intCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intCategory;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DeletePlayer3ByCIDCat", commandParameters);
        }

        public static void DeletePlayer3Close()
        {
            string commandText = "Exec NewBTP.dbo.DeletePlayer3Close";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeletePlayer3Free()
        {
            string commandText = "Exec NewBTP.dbo.DeletePlayer3Free";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteSearchPlayer3(long longPlayerID)
        {
            string commandText = "DELETE FROM BTP_Player3 WHERE Category=88 AND ClubID=0 AND PlayerID= " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void EndStreetFreeMarket(long longPlayerID, int intClubID, int intNumber, int intPrice, int intUserID, string strEvent, string strContent)
        {
            string commandText = "Exec NewBTP.dbo.EndStreetFreeMarket @longPlayerID,@intClubID,@intNumber,@intPrice,@intUserID,@strEvent,@strContent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@intNumber", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100), new SqlParameter("@strContent", SqlDbType.NVarChar, 0x3e8) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intClubID;
            commandParameters[2].Value = intNumber;
            commandParameters[3].Value = intPrice;
            commandParameters[4].Value = intUserID;
            commandParameters[5].Value = strEvent;
            commandParameters[6].Value = strContent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void FirePlayer3ByPlayerID(int intClubID, long longPlayerID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.FirePlayer3ByPlayerID ", intClubID, ",", longPlayerID });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrage3PlayerList(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrage3PlayerList " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetArrPlayerTable(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrPlayer3Table " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static long GetCanPlay3ID(long[] longIDs, int intClubID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetCanPlay3ID ", intClubID, ",", longIDs[0], ",", longIDs[1], ",", longIDs[2] });
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetCloseBidTable()
        {
            string commandText = "Exec NewBTP.dbo.GetCloseBidTable";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetClubPlayerByHeight(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetClubPlayerByHeight " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetOnPlayer3TableByCID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetOnPlayer3TableByCID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3Cost(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerCost3 " + longPlayerID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3CountByCID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer3CountByCID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3CountByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer3CountByClubID " + intClubID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3HappyNotFullCount(int intClubID)
        {
            SqlParameter parameter = new SqlParameter("@ClubID", SqlDbType.Int, 4);
            parameter.Value = intClubID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer3HappyNotFullCount", new SqlParameter[] { parameter });
        }

        public static long GetPlayer3IDByPlayerName(string strPlayerName)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer3IDByPlayerName @PlayerName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = strPlayerName;
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static string GetPlayer3NameByPlayerID(long longPlayerID)
        {
            string commandText = "SELECT Name FROM BTP_Player3 WHERE PlayerID=" + longPlayerID;
            return SqlHelper.ExecuteDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPlayer3NumByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer3NumByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetPlayer3PowerNotFullCount(int intClubID)
        {
            SqlParameter parameter = new SqlParameter("@ClubID", SqlDbType.Int, 4);
            parameter.Value = intClubID;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetPlayer3PowerNotFullCount", new SqlParameter[] { parameter });
        }

        public static DataTable GetPlayer3TableByCIDCat(int intClubID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetPlayer3TableByCIDCat ", intClubID, ",", intCategory });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetPlayerRowByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayerRowByPlayerID3 " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetPlayerTableByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetPlayer3TableByClubID @ClubID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable GetSelectPlayer3Table()
        {
            string commandText = "Exec NewBTP.dbo.GetSelectPlayer3TableNew";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetSelectPlayer3Table(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetSelectPlayer3Table " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStreetFreePlayer()
        {
            string commandText = "Exec NewBTP.dbo.GetStreetFreePlayer";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetTop3ByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetTop3ByClubID " + intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetYPlayerTableByClubID(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.GetYPlayer3TableByClubID @ClubID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable InTeamSearchTable(int intClubID, long lngPlayerID, int intCategory)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Category", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = lngPlayerID;
            commandParameters[2].Value = intCategory;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "InTeamSearchTable", commandParameters);
        }

        public static void MakePlayer3Grow()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.MakePlayer3Grow";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                MakePlayer3Grow();
            }
        }

        public static void Player3AddAge()
        {
            string commandText = "Exec NewBTP.dbo.Player3AddAge";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player3Aging()
        {
            string commandText = "Exec NewBTP.dbo.Player3Aging";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player3GrowMsg()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetPlayer3GrowTable";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = row["Name"].ToString().Trim();
                        int intClubID = (int) row["ClubID"];
                        string strContent = str2 + "身高增长！";
                        BTPMessageManager.AddMessage(BTPClubManager.GetUserIDByClubID(intClubID), 2, 0, "营养师报告", strContent);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                Player3GrowMsg();
            }
        }

        public static DataTable Player3Retire()
        {
            string commandText = "Exec NewBTP.dbo.Player3PreRetire";
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void Player3RetireMsg()
        {
            string commandText = "Exec NewBTP.dbo.Player3PreRetire";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int intClubID = (int) row["ClubID"];
                    long num1 = (long) row["PlayerID"];
                    string strContent = row["Name"].ToString() + "宣布将于本赛季结束后退役。您可以使用“理疗中心”的“返老还童”延长球员的篮球生涯！";
                    BTPMessageManager.AddMessageByClubID3(intClubID, strContent);
                }
            }
        }

        public static void PlayerGrow3()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.PlayerGrow3";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                PlayerGrow3();
            }
        }

        public static void PlayerLevelUP(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.PlayerLevelUP " + longPlayerID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int PlayerSkillMaxUP3()
        {
            try
            {
                int num = 1;
                DataTable table = BTPClubManager.GetAllClub3();
                if (table != null)
                {
                    foreach (DataRow row2 in table.Rows)
                    {
                        int intClubID = (int) row2["ClubID"];
                        DataRow staffRow = BTPStaffManager.GetStaffRow(intClubID, 1);
                        if (staffRow != null)
                        {
                            int num4 = (int) staffRow["StaffID"];
                            int num3 = (byte) staffRow["Ability"];
                            num3 = Convert.ToInt16(Math.Pow((double) (num3 * 100), 0.5));
                            DataTable yPlayerTableByClubID = GetYPlayerTableByClubID(intClubID);
                            if (yPlayerTableByClubID != null)
                            {
                                foreach (DataRow row3 in yPlayerTableByClubID.Rows)
                                {
                                    long longPlayerID = (long) row3["PlayerID"];
                                    string strName = row3["Name"].ToString().Trim();
                                    int intPos = (byte) row3["Pos"];
                                    int num5 = RandomItem.rnd.Next(0, 260);
                                    Console.WriteLine(string.Format("{0}-->{1}", num3, num5));
                                    if (num3 > num5)
                                    {
                                        int num6;
                                        if ((longPlayerID % 4L) == (num4 % 4))
                                        {
                                            num6 = RandomItem.rnd.Next(0x23, 70);
                                            Console.WriteLine("start add skill max");
                                            AddSkillMax(intClubID, longPlayerID, num6, intPos, strName);
                                        }
                                        else
                                        {
                                            num6 = RandomItem.rnd.Next(20, 0x29);
                                            Console.WriteLine("start add skill max");
                                            AddSkillMax(intClubID, longPlayerID, num6, intPos, strName);
                                        }
                                        if ((num % 500) == 0)
                                        {
                                            Console.WriteLine("已经更新了" + num + "个俱乐部。");
                                        }
                                        num++;
                                    }
                                }
                                continue;
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return PlayerSkillMaxUP3();
            }
        }

        public static int RecoverHappy3()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RecoverHappy3";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return RecoverHappy3();
            }
        }

        public static int RecoverHealthy3()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RecoverHealthy3";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return RecoverHealthy3();
            }
        }

        public static int RecoverPower3()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RecoverPower3";
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return RecoverPower3();
            }
        }

        public static void RenewName3()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.RenewName3";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                RenewName3();
            }
        }

        public static DataRow SearchPlayer3(int intCount, int intCategory, string strEndBidTime, int intAbilityPercent, int intAbilityMaxPercent)
        {
            string commandText = "Exec NewBTP.dbo.SearchPlayer3 @Count,@Category,@EndBidTime,@AbilityPercent,@AbilityMaxPercent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Count", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4), new SqlParameter("@EndBidTime", SqlDbType.NVarChar, 20), new SqlParameter("@AbilityPercent", SqlDbType.Int, 4), new SqlParameter("@AbilityMaxPercent", SqlDbType.Int, 4) };
            commandParameters[0].Value = intCount;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strEndBidTime;
            commandParameters[3].Value = intAbilityPercent;
            commandParameters[4].Value = intAbilityMaxPercent;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable SearchPlayer3New(int intClubID, int intCategory, string strDateTime, int intNowPercent, int intMaxPercent)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@Category", SqlDbType.Int, 4), new SqlParameter("@EndBidTime", SqlDbType.NVarChar, 20), new SqlParameter("@AbilityPercent", SqlDbType.Int, 4), new SqlParameter("@AbilityMaxPercent", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = intCategory;
            commandParameters[2].Value = strDateTime;
            commandParameters[3].Value = intNowPercent;
            commandParameters[4].Value = intMaxPercent;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "SearchPlayer3New", commandParameters);
        }

        public static DataRow SearchPlayerInTeam(int intClubID, long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.SearchPlayerInTeam @ClubID,@PlayerID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@PlayerID", SqlDbType.Int, 8) };
            commandParameters[0].Value = intClubID;
            commandParameters[1].Value = longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataRow SearchPlayerInTeamNew(int intUserID, int intClubID, long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.SearchPlayerInTeamNew @UserID,@ClubID,@PlayerID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@PlayerID", SqlDbType.Int, 8) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intClubID;
            commandParameters[2].Value = longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SendHealthyMessage()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetSuspend3Table";
                DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (table != null)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        string str2 = row["Name"].ToString().Trim();
                        int intClubID = (int) row["ClubID"];
                        int userIDByClubID = BTPClubManager.GetUserIDByClubID(intClubID);
                        byte num1 = (byte) row["Status"];
                        string str3 = str2 + "康复！";
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

        public static bool SetAssignPlayerInClub(string strPlayerIDA, string strPlayerIDB, string strPlayerIDC, string strPlayerIDD, int intValue, int intClubID, int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.SetAssignPlayerInClub @PlayerIDA,@PlayerIDB,@PlayerIDC,@PlayerIDD,@Value,@ClubID,@UserID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerIDA", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDB", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDC", SqlDbType.BigInt, 8), new SqlParameter("@PlayerIDD", SqlDbType.BigInt, 8), new SqlParameter("@Value", SqlDbType.Int, 4), new SqlParameter("@ClubID", SqlDbType.Int, 4), new SqlParameter("@UserID", SqlDbType.Int, 4) };
            commandParameters[0].Value = strPlayerIDA;
            commandParameters[1].Value = strPlayerIDB;
            commandParameters[2].Value = strPlayerIDC;
            commandParameters[3].Value = strPlayerIDD;
            commandParameters[4].Value = intValue;
            commandParameters[5].Value = intClubID;
            commandParameters[6].Value = intUserID;
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetPlayer3Bid(long longPlayerID, int intBidPrice, DateTime datBidTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SetPlayer3Bid ", longPlayerID, ",", intBidPrice, ",'", datBidTime.ToString(), "'" });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetPlayer3ClubID(long longPlayerID, int intClubID, int intNumber)
        {
        }

        public static void SetPlayer3To5(long longPlayerID)
        {
            DataRow playerRowByPlayerID = GetPlayerRowByPlayerID(longPlayerID);
            string str = playerRowByPlayerID["Name"].ToString().Trim();
            int intClubID = (int) playerRowByPlayerID["ClubID"];
            int userIDByClubID = BTPClubManager.GetUserIDByClubID(intClubID);
            int clubIDByUIDCategory = BTPClubManager.GetClubIDByUIDCategory(userIDByClubID, 5);
            int num4 = PlayerItem.GetGoodNum5(clubIDByUIDCategory);
            string marketCodeByClubID = BTPClubManager.GetMarketCodeByClubID(clubIDByUIDCategory);
            string commandText = "Exec NewBTP.dbo.SetPlayer3To5 @intClubID5,@longPlayerID,@intNumber,@strMarketCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID5", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intNumber", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = clubIDByUIDCategory;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = num4;
            commandParameters[3].Value = marketCodeByClubID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddNewMessage ", userIDByClubID, ",2,0,'秘书报告','球员", str, "已转入职业队。'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow TrainPlayer3(long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.TrainPlayer3 ", longPlayerID, ",", intType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateEndBidTime(long longPlayerID, DateTime datEndBidTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateEndBidTime ", longPlayerID, ",'", datEndBidTime.ToString(), "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateNumPosByPlayerID3(long longPlayerID, int intNumber, int intPosition)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateNumPosByPlayerID3 ", longPlayerID, ",", intNumber, ",", intPosition });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateNumPosNameByPlayerID3(long longPlayerID, int intNumber, int intPosition, string strPlayerName)
        {
            string commandText = "Exec NewBTP.dbo.UpdateNumPosNameByPlayerID3 @PlayerID,@Number,@Position,@PlayerName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Number", SqlDbType.TinyInt, 1), new SqlParameter("@Position", SqlDbType.TinyInt, 1), new SqlParameter("@PlayerName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intNumber;
            commandParameters[2].Value = intPosition;
            commandParameters[3].Value = strPlayerName;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UPDATEPlayer3AbilityPV()
        {
            string commandText = "Exec NewBTP.dbo.UPDATEPlayer3AbilityPV";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UpdatePlayer3Body(long longPlayerID, int intClubID3, string strPlayerBody, int intUserID, string strNickName)
        {
            string commandText = "Exec NewBTP.dbo.UpdatePlayer3Body @PlayerID,@ClubID3,@PlayerBody,@UserID,@NickName";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@ClubID3", SqlDbType.Int, 4), new SqlParameter("@PlayerBody", SqlDbType.NVarChar, 100), new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@NickName", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intClubID3;
            commandParameters[2].Value = strPlayerBody;
            commandParameters[3].Value = intUserID;
            commandParameters[4].Value = strNickName;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UpdatePlayer3Happy()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdatePlayer3Happy");
        }

        public static void UpdatePlayer3PV()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdatePlayer3PV";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdatePlayer3PV();
            }
        }

        public static void UpdatePlayerByUserID(int intUserID)
        {
            SqlParameter parameter = new SqlParameter("@UserID", SqlDbType.Int, 4);
            parameter.Value = intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "UpdatePlayerByUserID", new SqlParameter[] { parameter });
        }

        public static void UpdatePlayerStas(long longPlayerID, int intScore, int intRebound, int intAssist, int intBlock, int intSteal, bool blnPlayed, int intHappy, int intPower, int intTrainPoint, int intStatus, string strEvent, int intSuspend)
        {
            if (blnPlayed)
            {
                string commandText = "Exec NewBTP.dbo.UpdatePlayer3Stas @longPlayerID,@intScore,@intRebound,@intAssist,@intBlock,@intSteal,@intHappy,@intPower,@intTrainPoint,@intStatus,@strEvent,@intSuspend";
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intScore", SqlDbType.Int, 4), new SqlParameter("@intRebound", SqlDbType.Int, 4), new SqlParameter("@intAssist", SqlDbType.Int, 4), new SqlParameter("@intBlock", SqlDbType.Int, 4), new SqlParameter("@intSteal", SqlDbType.Int, 4), new SqlParameter("@intHappy", SqlDbType.TinyInt, 1), new SqlParameter("@intPower", SqlDbType.TinyInt, 1), new SqlParameter("@intTrainPoint", SqlDbType.Int, 4), new SqlParameter("@intStatus", SqlDbType.TinyInt, 1), new SqlParameter("@strEvent", SqlDbType.NVarChar, 0xff), new SqlParameter("@intSuspend", SqlDbType.TinyInt, 1) };
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
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
            }
            else
            {
                string str2 = string.Concat(new object[] { "Exec NewBTP.dbo.UpdateUPlayer3Stas ", longPlayerID, ",", intHappy, ",", intTrainPoint });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str2);
            }
        }
    }
}


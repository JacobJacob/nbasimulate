namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPToolLinkManager
    {
        public static void AddCardByActiveTime()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "AddCardByActiveTime");
        }

        public static DataRow AddDataOnly(long lngPlayer, int intType, int intClubID, int intCategory, int intSell)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddDataOnly ", lngPlayer, ",", intType, ",", intClubID, ",", intCategory, ",", intSell });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow AddDataOnlyNew(long lngPlayer, int intType, int intClubID, int intCategory, int intSell)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddDataOnlyNew ", lngPlayer, ",", intType, ",", intClubID, ",", intCategory, ",", intSell });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int AddPlayer5ByPlayerID(long lngPlayerID, int intClubID5, int intSpeedAdd, int intJumpAdd, int intStrengthAdd, int intStaminaAdd, int intShotAdd, int intPoint3Add, int intDribbleAdd, int intPassAdd, int intReboundAdd, int intStealAdd, int intBlockAdd)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.AddPlayer5ByPlayerID ", lngPlayerID, ",", intClubID5, ",", intSpeedAdd, ",", intJumpAdd, ",", intStrengthAdd, ",", intStaminaAdd, ",", intShotAdd, ",", intPoint3Add, 
                ",", intDribbleAdd, ",", intPassAdd, ",", intReboundAdd, ",", intStealAdd, ",", intBlockAdd
             });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int AddPlayerAll(int intUserID, int intType, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddPlayerAll\t ", intUserID, ",", intType, ",", intCategory });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddTicket(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddTicket ", intUserID, ",", intCategory, ",", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddTool(int intUserID, int intCategory, int intTicketCategory, int intDay, int intAmount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddTool ", intUserID, ",", intCategory, ",", intTicketCategory, ",", intDay, ",", intAmount });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddWealthTool(int intUserID, int intCategory, int intTicketCategory, int intDay, int intAmount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddWealthTool ", intUserID, ",", intCategory, ",", intTicketCategory, ",", intDay, ",", intAmount });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddXBATicket(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddXBATicket ", intUserID, ",", intCategory, ",", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static long AfreshArrangeLvlByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.AfreshArrangeLvlByUserID " + intUserID;
            return SqlHelper.ExecuteLongDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int BuyDoubleExperience(int intToolID, int intUserID, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BuyDoubleExperience ", intToolID, ",", intUserID, ",", intCount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int BuyTool(int intToolID, int intUserID, DateTime datTime, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BuyTool ", intToolID, ",", intUserID, ",'", datTime, "',", intCount });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void BuyVIPCard(int intUserID, DateTime datMemberExpireTime)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.BuyVIPCard ", intUserID, ",'", datMemberExpireTime, "'" });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void BuyWealthTool(int intToolID, int intUserID, DateTime datTime, string strNickName, int intType, int intCount)
        {
            string commandText = "Exec NewBTP.dbo.BuyWealthTool @intToolID,@intUserID,@datTime,@strNickName,@intType,@intCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intToolID", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@datTime", SqlDbType.DateTime, 8), new SqlParameter("@strNickName", SqlDbType.NVarChar, 20), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = intToolID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = datTime;
            commandParameters[3].Value = strNickName;
            commandParameters[4].Value = intType;
            commandParameters[5].Value = intCount;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void BuyWealthVIPCard(int intUserID, DateTime datMemberExpireTime, string strNickName, int States)
        {
            string commandText = "Exec NewBTP.dbo.BuyWealthVIPCard @intUserID,@datMemberExpireTime,@strNickName,@States";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@datMemberExpireTime", SqlDbType.DateTime, 8), new SqlParameter("@strNickName", SqlDbType.NChar, 20), new SqlParameter("@States", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = datMemberExpireTime;
            commandParameters[2].Value = strNickName;
            commandParameters[3].Value = States;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static DataTable CheckClubLink(int intUserID, int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckClubLink ", intUserID, ",", intClubID, ",", intType });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CheckHide(int intUserID, long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckHide ", intUserID, ",", longPlayerID, ",", intType });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CheckMatchLev(int intUserID, int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckHide ", intUserID, ",", intClubID, ",", intType });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CheckMatchLook(int intUserID, int intDevMatchID)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckMatchLook ", intUserID, ",", intDevMatchID });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable CheckPlayer5PLink(int intUserID, long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckPlayer5PLink ", intUserID, ",", longPlayerID, ",", intType });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int CheckPrivateSkill(int intUserID, long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.CheckPrivateSkill ", intUserID, ",", longPlayerID, ",", intType });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteAllTicket(int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteAllTicket ", intCategory, ",", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteAllUnionXBATicket()
        {
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "DeleteAllUnionXBATicket");
        }

        public static void DeleteATool(int intUserID, int intToolCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteATool ", intUserID, ",", intToolCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void DeleteExpiredTool()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.DeleteExpiredTool";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                DeleteExpiredTool();
            }
        }

        public static void DeletePick()
        {
            string commandText = "SELECT ToolID FROM BTP_Tool WHERE Category=5";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["ToolID"];
                    commandText = "DELETE FROM BTP_ToolLink WHERE ToolID=" + num + " AND Type=1";
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                }
            }
        }

        public static void DeleteTicket(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.DeleteTicket ", intUserID, ",", intCategory, ",", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllUserToolByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetAllUserToolByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetAllWealthToolByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetAllWealthToolByUserID " + intUserID;
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetToolByUserIDTCategory(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetToolByUserIDTCategory ", intUserID, ",", intCategory, ",", intTicketCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetToolLinkCount(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetToolLinkCount " + intUserID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetToolLinkList(int intUserID, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetToolLinkList ", intUserID, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetToolLinkUserTable(int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetToolLinkUserTable ", intCategory, ",", intTicketCategory });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetWealthToolByUserID(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetWealthToolByUserID ", intUserID, ",", intCategory, ",", intTicketCategory });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetXBACount()
        {
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetXBACount");
        }

        public static void GiftTool(int intUserID, int intToolID, int intAmount, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GiftTool ", intUserID, ",", intToolID, ",", intAmount, ",", intType });
            SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasPick(int intUserID, int intCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.HasPick ", intUserID, ",", intCategory });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasTicket(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.HasTicket ", intUserID, ",", intCategory, ",", intTicketCategory });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasTool(int intUserID, int intType, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.HasTool ", intUserID, ",", intType, ",", intCategory, ",", intTicketCategory });
            return SqlHelper.ExecuteBoolReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void MinusAge(int intUserID, long longPlayerID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.MinusAge\t ", intUserID, ",", longPlayerID, ",", intType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void PresentTool(int intUserID, int intCategory, int intTicketCategory, int intAmount)
        {
            string commandText = string.Concat(new object[] { "SELECT * FROM BTP_ToolLink L,BTP_Tool T WHERE L.ToolID=T.ToolID AND T.Category=", intCategory, " AND T.TicketCategory=", intTicketCategory, " AND L.UserID=", intUserID });
            DataRow row = SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (row == null)
            {
                commandText = string.Concat(new object[] { "SELECT ToolID FROM BTP_Tool WHERE Category=", intCategory, " AND TicketCategory=", intTicketCategory });
                int num = SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                commandText = string.Concat(new object[] { "INSERT INTO BTP_ToolLink (ToolID,UserID,Amount,ExpireTime)VALUES(", num, ",", intUserID, ",", intAmount, ",'", DateTime.Now.AddMonths(1), "')" });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            else
            {
                int num2 = (int) row["ToolLinkID"];
                commandText = string.Concat(new object[] { "UPDATE BTP_ToolLink SET Amount=Amount+", intAmount, ",ExpireTime=DATEADD(Month,1,GETDATE()) WHERE ToolLinkID=", num2 });
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
        }

        public static void PresentTool(int intUserID, int intCategory, int intTicketCategory, int intAmount, string strConn)
        {
            string commandText = string.Concat(new object[] { "SELECT * FROM BTP_ToolLink L,BTP_Tool T WHERE L.ToolID=T.ToolID AND T.Category=", intCategory, " AND T.TicketCategory=", intTicketCategory, " AND L.UserID=", intUserID });
            DataRow row = SqlHelper.ExecuteDataRow(strConn, CommandType.Text, commandText);
            if (row == null)
            {
                commandText = string.Concat(new object[] { "SELECT ToolID FROM BTP_Tool WHERE Category=", intCategory, " AND TicketCategory=", intTicketCategory });
                int num = SqlHelper.ExecuteIntDataField(strConn, CommandType.Text, commandText);
                commandText = string.Concat(new object[] { "INSERT INTO BTP_ToolLink (ToolID,UserID,Amount,ExpireTime)VALUES(", num, ",", intUserID, ",", intAmount, ",'", DateTime.Now.AddMonths(1), "')" });
                SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, commandText);
            }
            else
            {
                int num2 = (int) row["ToolLinkID"];
                commandText = string.Concat(new object[] { "UPDATE BTP_ToolLink SET Amount=Amount+", intAmount, ",ExpireTime=DATEADD(Month,1,GETDATE()) WHERE ToolLinkID=", num2 });
                SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, commandText);
            }
        }

        public static void ProvideChooseCard(int intUserID, int intCategory, int intTicketCategory)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.ProvideChooseCard ", intUserID, ",", intCategory, ",", intTicketCategory });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseAddPower(int intUserID, long longPlayerID, int intType, bool blnCanAdd)
        {
            string commandText = "Exec NewBTP.dbo.UseAddPower @UserID,@PlayerID,@Type,@CanAdd";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UserID", SqlDbType.Int, 4), new SqlParameter("@PlayerID", SqlDbType.BigInt, 8), new SqlParameter("@Type", SqlDbType.Int, 4), new SqlParameter("@CanAdd", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = intType;
            commandParameters[3].Value = blnCanAdd;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void UseAutoTrain(int intUserID, int intHours)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseAutoTrain ", intUserID, ",", intHours });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseAutoTrain(int intUserID, int intHours, int intDevHours)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseAutoTrain ", intUserID, ",", intHours, ",", intDevHours });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseBidHelper(int intUserID, int intMarket, long longPlayerID, int intMaxMoney)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseBidHelper ", intUserID, ",", intMarket, ",", longPlayerID, ",", intMaxMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseHide(int intUserID, long longPlayerID, int intType, int intCategory, int intPlayType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseHide\t ", intUserID, ",", longPlayerID, ",", intType, ",", intCategory, ",", intPlayType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseMatchLev(int intUserID, int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseMatchLev\t ", intUserID, ",", intClubID, ",", intType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int UseMatchLook(int intUserID, int intDevMatchID, int intPayType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseMatchLook ", intUserID, ",", intDevMatchID, ",", intPayType });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UsePrivateSkill(int intUserID, long longPlayerID, int intMarket)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UsePrivateSkill ", intUserID, ",", longPlayerID, ",", intMarket });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UseToolCategory3(int intUserID, long longPlayerID, int intMarket)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.UseToolCategory3 ", intUserID, ",", longPlayerID, ",", intMarket });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


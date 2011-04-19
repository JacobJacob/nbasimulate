namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;

    public class BTPTransferManager
    {
        public static DataRow GetBidderByPlayerID(long longPlayerID, int intBidPrice)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetBidderByPlayerID ", longPlayerID, ",", intBidPrice });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetCloseBidderByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetCloseBidderByPlayerID " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevChooseCount(int intUserID, int intPos, string strMarketCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevChooseCount @intUserID,@intPos,@strMarketCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intPos", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPos;
            commandParameters[2].Value = strMarketCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetDevChooseCountNew(int intUserID, int intPos, string strMarketCode)
        {
            string commandText = "Exec NewBTP.dbo.GetDevChooseListNew @intUserID,@intPos,0,@strMarketCode,0,0,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPos;
            commandParameters[2].Value = strMarketCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetDevChooseList(int intUserID, int intPos, int intOrder, string strMarketCode, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = "Exec NewBTP.dbo.GetDevChooseList @intUserID,@intPos,@intOrder,@strMarketCode,@intPage,@intPerPage,@intTotal,@intCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@intOrder", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPos;
            commandParameters[2].Value = intOrder;
            commandParameters[3].Value = strMarketCode;
            commandParameters[4].Value = intPage;
            commandParameters[5].Value = intPerPage;
            commandParameters[6].Value = intTotal;
            commandParameters[7].Value = intCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetDevChooseListNew(int intUserID, int intPos, int intOrder, string strMarketCode, int intPage, int intPerPage)
        {
            string commandText = "Exec NewBTP.dbo.GetDevChooseListNew @intUserID,@intPos,@intOrder,@strMarketCode,@intPage,@intPerPage,0";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@intOrder", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = intPos;
            commandParameters[2].Value = intOrder;
            commandParameters[3].Value = strMarketCode;
            commandParameters[4].Value = intPage;
            commandParameters[5].Value = intPerPage;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetDevStreet(int intPos, int intOrder, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevStreet ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevStreetCount(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetDevStreetCount " + intPos;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetDevStreetCountNew(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetDevStreetNew " + intPos + ",0,0,0,1";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetDevStreetNew(int intPos, int intOrder, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetDevStreetNew ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetDevTranTopUser(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetDevTranTopUser " + longPlayerID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetFreeBidderByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetFreeBidderByPlayerID " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetOpenBidderByPlayerID(long longPlayerID)
        {
            string commandText = "Exec NewBTP.dbo.GetOpenBidderByPlayerID " + longPlayerID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStreetChooseCount(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetStreetChooseCount " + intPos;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStreetChooseCountNew(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetTranStreetChooseNew " + intPos + ",0,0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStreetFreeCount(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetStreetFreeCount " + intPos;
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStreetFreeCountNew(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetTranStreetFreeNew " + intPos + ",0,0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTransfer(int intPos, int intOrder, string strMarketCode, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = "Exec NewBTP.dbo.GetTransfer @intPos,@intOrder,@strMarketCode,@intPage,@intPerPage,@intTotal,@intCount";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@intOrder", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4), new SqlParameter("@intTotal", SqlDbType.Int, 4), new SqlParameter("@intCount", SqlDbType.Int, 4) };
            commandParameters[0].Value = intPos;
            commandParameters[1].Value = intOrder;
            commandParameters[2].Value = strMarketCode;
            commandParameters[3].Value = intPage;
            commandParameters[4].Value = intPerPage;
            commandParameters[5].Value = intTotal;
            commandParameters[6].Value = intCount;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetTransferCount(int intPos, string strMarketCode)
        {
            string commandText = "Exec NewBTP.dbo.GetTransferCount @intPos,@strMarketCode";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intPos;
            commandParameters[1].Value = strMarketCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int GetTransferCountNew(int intPos, string strMarketCode)
        {
            string commandText = "Exec NewBTP.dbo.GetTransferNew @intPos,0,@strMarketCode,0,0,1";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@strMarketCode", SqlDbType.Char, 20) };
            commandParameters[0].Value = intPos;
            commandParameters[1].Value = strMarketCode;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetTransferNew(int intPos, int intOrder, string strMarketCode, int intPage, int intPerPage)
        {
            string commandText = "Exec NewBTP.dbo.GetTransferNew @intPos,@intOrder,@strMarketCode,@intPage,@intPerPage,0";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intPos", SqlDbType.TinyInt, 1), new SqlParameter("@intOrder", SqlDbType.Int, 4), new SqlParameter("@strMarketCode", SqlDbType.Char, 20), new SqlParameter("@intPage", SqlDbType.Int, 4), new SqlParameter("@intPerPage", SqlDbType.Int, 4) };
            commandParameters[0].Value = intPos;
            commandParameters[1].Value = intOrder;
            commandParameters[2].Value = strMarketCode;
            commandParameters[3].Value = intPage;
            commandParameters[4].Value = intPerPage;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static SqlDataReader GetTranStreetChoose(int intPos, int intOrder, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTranStreetChoose ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTranStreetChooseNew(int intPos, int intOrder, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTranStreetChooseNew ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTranStreetFree(int intPos, int intOrder, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTranStreetFree ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetTranStreetFreeNew(int intPos, int intOrder, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetTranStreetFreeNew ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUtmostCount(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetUtmostCount " + intPos;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUtmostCountNew(int intPos)
        {
            string commandText = "Exec NewBTP.dbo.GetUtmostListNew " + intPos + ",0,0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUtmostList(int intPos, int intOrder, int intPage, int intPerPage, int intTotal, int intCount)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUtmostList ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",", intTotal, ",", intCount });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUtmostListNew(int intPos, int intOrder, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUtmostListNew ", intPos, ",", intOrder, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int SetChooseBid(int intUserID, long longPlayerID, int intCategory, int intType, int intFreeBidPrice, string strCreateTime, string strIP)
        {
            string commandText = "Exec NewBTP.dbo.SetChooseBid @intUserID,@longPlayerID,@intCategory,@intType,@intFreeBidPrice,@strCreateTime,@strIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intFreeBidPrice", SqlDbType.Int, 4), new SqlParameter("@strCreateTime", SqlDbType.DateTime, 8), new SqlParameter("@strIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intType;
            commandParameters[4].Value = intFreeBidPrice;
            commandParameters[5].Value = Convert.ToDateTime(strCreateTime);
            commandParameters[6].Value = strIP;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetDevisionTran(long longPlayerID, int intUserID, int intPrice, string strCreateTime, string strIP, int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.SetDevisionTran @longPlayerID,@intUserID,@intPrice,@strCreateTime,@strIP,@intStatus";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intPrice", SqlDbType.Int, 4), new SqlParameter("@strCreateTime", SqlDbType.DateTime, 8), new SqlParameter("@strIP", SqlDbType.NVarChar, 50), new SqlParameter("@intStatus", SqlDbType.TinyInt, 1) };
            commandParameters[0].Value = longPlayerID;
            commandParameters[1].Value = intUserID;
            commandParameters[2].Value = intPrice;
            commandParameters[3].Value = strCreateTime;
            commandParameters[4].Value = strIP;
            commandParameters[5].Value = intStatus;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void SetFreeBid(int intUserID, long longPlayerID, int intCategory, int intType, int intFreeBidPrice, string strCreateTime, string strIP)
        {
            string commandText = "Exec NewBTP.dbo.SetFreeBid @intUserID,@longPlayerID,@intCategory,@intType,@intFreeBidPrice,@strCreateTime,@strIP";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@longPlayerID", SqlDbType.BigInt, 8), new SqlParameter("@intCategory", SqlDbType.TinyInt, 1), new SqlParameter("@intType", SqlDbType.TinyInt, 1), new SqlParameter("@intFreeBidPrice", SqlDbType.Int, 4), new SqlParameter("@strCreateTime", SqlDbType.DateTime, 8), new SqlParameter("@strIP", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = intUserID;
            commandParameters[1].Value = longPlayerID;
            commandParameters[2].Value = intCategory;
            commandParameters[3].Value = intType;
            commandParameters[4].Value = intFreeBidPrice;
            commandParameters[5].Value = Convert.ToDateTime(strCreateTime);
            commandParameters[6].Value = strIP;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }
    }
}


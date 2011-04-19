namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPGuessManager
    {
        public static void AddGuess(string strType, int intMoneyType, string strNameA, string strNameB, bool blHot, DateTime dtEndTime, DateTime dtShowTime)
        {
            string commandText = "Exec NewBTP.dbo.AddGuess @strType,@intMoneyType,@strNameA,@strNameB,@blHot,@dtEndTime,@dtShowTime";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strType", SqlDbType.NChar, 20), new SqlParameter("@intMoneyType", SqlDbType.TinyInt, 1), new SqlParameter("@strNameA", SqlDbType.NChar, 50), new SqlParameter("@strNameB", SqlDbType.NChar, 50), new SqlParameter("@dtEndTime", SqlDbType.DateTime, 8), new SqlParameter("@dtShowTime", SqlDbType.DateTime, 8), new SqlParameter("@blHot", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strType;
            commandParameters[1].Value = intMoneyType;
            commandParameters[2].Value = strNameA;
            commandParameters[3].Value = strNameB;
            commandParameters[4].Value = dtEndTime;
            commandParameters[5].Value = dtShowTime;
            commandParameters[6].Value = blHot;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int AddGuessRecord(int intUserID, int intGuessID, long lgMoney, int blResultType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddGuessRecord ", intUserID, ",", intGuessID, ",", lgMoney, ",", blResultType });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int DelOldGuessByGuessID(int intGuessID)
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.DelOldGuessByGuessID " + intGuessID;
                return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return DelOldGuessByGuessID(intGuessID);
            }
        }

        public static void EditGuess(string strType, string strNameA, string strNameB, bool blHot, DateTime dtEndTime, DateTime dtShowTime, int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.EditGuess @strType,@strNameA,@strNameB,@blHot,@dtEndTime,@dtShowTime,@intGuessID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@strType", SqlDbType.NChar, 20), new SqlParameter("@strNameA", SqlDbType.NChar, 50), new SqlParameter("@strNameB", SqlDbType.NChar, 50), new SqlParameter("@dtEndTime", SqlDbType.DateTime, 8), new SqlParameter("@dtShowTime", SqlDbType.DateTime, 8), new SqlParameter("@intGuessID", SqlDbType.Int, 4), new SqlParameter("@blHot", SqlDbType.Bit, 1) };
            commandParameters[0].Value = strType;
            commandParameters[1].Value = strNameA;
            commandParameters[2].Value = strNameB;
            commandParameters[3].Value = dtEndTime;
            commandParameters[4].Value = dtShowTime;
            commandParameters[5].Value = intGuessID;
            commandParameters[6].Value = blHot;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void GetGuessBeginEnd(int intGuessID, long lngMoneyA, long lngMoneyB, int intGuessTax, int intMoneyType, bool blnResultType)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@GuessID", SqlDbType.Int, 4), new SqlParameter("@MoneyA", SqlDbType.BigInt, 8), new SqlParameter("@MoneyB", SqlDbType.BigInt, 8), new SqlParameter("@GuessTax", SqlDbType.Int, 4), new SqlParameter("@MoneyType", SqlDbType.TinyInt, 1), new SqlParameter("@ResultType", SqlDbType.Bit, 1) };
            commandParameters[0].Value = intGuessID;
            commandParameters[1].Value = lngMoneyA;
            commandParameters[2].Value = lngMoneyB;
            commandParameters[3].Value = intGuessTax;
            commandParameters[4].Value = intMoneyType;
            commandParameters[5].Value = blnResultType;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GetGuessBeginEnd", commandParameters);
        }

        public static int GetGuessCountByHasResult(int intHasResult)
        {
            string commandText = "Exec NewBTP.dbo.GetGuessTableByHasResult " + intHasResult + ",1,0,0";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGuessRecordCountByUserID(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetGuessRecordTableByUserID " + intUserID + ",1,0,0";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetGuessRecordTableByGuessID(int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.GetGuessRecordTableByGuessID " + intGuessID;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetGuessRecordTableByUserID(int intUserID, int intDoCount, int intPageIndex, int intPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGuessRecordTableByUserID ", intUserID, ",", intDoCount, ",", intPageIndex, ",", intPageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetGuessRowByDel(int intDel)
        {
            string commandText = "Exec NewBTP.dbo.GetGuessRowByDel " + intDel;
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetGuessRowByGuessID(int intGuessID)
        {
            string commandText = "SELECT TOP 1 * FROM BTP_Guess WHERE GuessID=" + intGuessID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetGuessRowByResult()
        {
            string commandText = "Exec NewBTP.dbo.GetGuessRowHasResult ";
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetGuessTableByHasResult(int intHasResult, int intDoCount, int intPageIndex, int intPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetGuessTableByHasResult ", intHasResult, ",", intDoCount, ",", intPageIndex, ",", intPageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetUseGuessCountByHasResult(int intHasResult)
        {
            string commandText = "Exec NewBTP.dbo.GetUseGuessTableByHasResult " + intHasResult + ",1,0,0";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetUseGuessRowByGuessID(int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.GetGuessRowByGuessID " + intGuessID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetUseGuessTableByHasResult(int intHasResult, int intDoCount, int intPageIndex, int intPageSize)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetUseGuessTableByHasResult ", intHasResult, ",", intDoCount, ",", intPageIndex, ",", intPageSize });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GuessBegin(int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.GuessBegin " + intGuessID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GuessBegin2(int intGuessRecordID, int intMoneyType, bool blnResultType, int intHasResult, string strNameA, string strNameB, long lngMoneyA, long lngMoneyB, int intGuessTax)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@GuessRecordID", SqlDbType.Int, 4), new SqlParameter("@MoneyType", SqlDbType.TinyInt, 1), new SqlParameter("@ResultType", SqlDbType.Bit, 1), new SqlParameter("@HasResult", SqlDbType.TinyInt, 1), new SqlParameter("@NameA", SqlDbType.NChar, 50), new SqlParameter("@NameB", SqlDbType.NChar, 50), new SqlParameter("@MoneyA", SqlDbType.BigInt, 8), new SqlParameter("@MoneyB", SqlDbType.BigInt, 8), new SqlParameter("@GuessTax", SqlDbType.Int, 4) };
            commandParameters[0].Value = intGuessRecordID;
            commandParameters[1].Value = intMoneyType;
            commandParameters[2].Value = blnResultType;
            commandParameters[3].Value = intHasResult;
            commandParameters[4].Value = strNameA;
            commandParameters[5].Value = strNameB;
            commandParameters[6].Value = lngMoneyA;
            commandParameters[7].Value = lngMoneyB;
            commandParameters[8].Value = intGuessTax;
            return (int) SqlHelper.ExecuteScalar(DBSelector.GetConnection("btp01"), CommandType.StoredProcedure, "GuessBegin2", commandParameters);
        }

        public static void MonthGuessUpdate()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.MonthGuessUpdate ";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                MonthGuessUpdate();
            }
        }

        public static int OurBackGuess(int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.OurBackGuess " + intGuessID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int OurDelGuess(int intGuessID)
        {
            string commandText = "Exec NewBTP.dbo.OurDelGuess " + intGuessID;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetDelGuess()
        {
            SqlDataReader guessRowByDel = GetGuessRowByDel(1);
            while (guessRowByDel.Read())
            {
                int intGuessID = (int) guessRowByDel["GuessID"];
                if (OurDelGuess(intGuessID) != 1)
                {
                    Console.WriteLine("GuessID=" + intGuessID + "的竞猜单为非法单！");
                }
            }
            guessRowByDel.Close();
        }

        public static void SetGuessBegin()
        {
            SqlDataReader reader = GetGuessTableByHasResult(1, 0, 1, 100);
            while (reader.Read())
            {
                int intGuessID = (int) reader["GuessID"];
                if (GuessBegin(intGuessID) != 1)
                {
                    Console.WriteLine("GuessID=" + intGuessID + "的竞猜单为非法单！");
                }
            }
            reader.Close();
        }

        public static void SetGuessBegin2()
        {
            SqlDataReader reader = GetGuessTableByHasResult(1, 0, 1, 100);
            while (reader.Read())
            {
                int intGuessID = (int) reader["GuessID"];
                if (GuessBegin(intGuessID) != 1)
                {
                    Console.WriteLine("GuessID=" + intGuessID + "的竞猜单为非法单！");
                }
            }
            reader.Close();
        }

        public static void SetOurBackGuess()
        {
            SqlDataReader guessRowByDel = GetGuessRowByDel(2);
            while (guessRowByDel.Read())
            {
                int intGuessID = (int) guessRowByDel["GuessID"];
                if (OurBackGuess(intGuessID) != 1)
                {
                    Console.WriteLine("GuessID=" + intGuessID + "的竞猜单为非法单！");
                }
            }
            guessRowByDel.Close();
        }
    }
}


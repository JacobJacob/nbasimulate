namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Threading;
    using Web.DBConnection;

    public class BTPStaffManager
    {
        public static void AddStaff()
        {
            int num4;
            string str;
            int staffCountByType = GetStaffCountByType(1);
            int num2 = GetStaffCountByType(2);
            int num3 = GetStaffCountByType(3);
            if (staffCountByType < 800)
            {
                num4 = 0x7d0 - staffCountByType;
                str = "Exec NewBTP.dbo.AddStaff " + num4 + ",1";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str);
            }
            if (num2 < 800)
            {
                num4 = 0x7d0 - num2;
                str = "Exec NewBTP.dbo.AddStaff " + num4 + ",2";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str);
            }
            if (num3 < 800)
            {
                num4 = 0x7d0 - num3;
                str = "Exec NewBTP.dbo.AddStaff " + num4 + ",3";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, str);
            }
        }

        public static void AddStaffByLvl(int intType, int intLevel)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.AddStaffByLvl ", intType, ",", intLevel });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ExtendStaff(int intStaffID, int intContract, int intSalary, int intClubID, int intDCategory, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.ExtendStaff @intStaffID,@intContract,@intSalary,@intClubID,@intDCategory,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intStaffID", SqlDbType.Int, 4), new SqlParameter("@intContract", SqlDbType.TinyInt, 1), new SqlParameter("@intSalary", SqlDbType.Int, 4), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@intDCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intStaffID;
            commandParameters[1].Value = intContract;
            commandParameters[2].Value = intSalary;
            commandParameters[3].Value = intClubID;
            commandParameters[4].Value = intDCategory;
            commandParameters[5].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static int FireStaff(int intStaffID)
        {
            string commandText = "Exec NewBTP.dbo.FireStaff " + intStaffID;
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetAllStaffCount()
        {
            string commandText = "Exec NewBTP.dbo.GetAllStaffCount";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void GetStaff(int intStaffID, int intClubID, int intDCategory, string strEvent)
        {
            string commandText = "Exec NewBTP.dbo.GetStaff @intStaffID,@intClubID,@intDCategory,@strEvent";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intStaffID", SqlDbType.Int, 4), new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@intDCategory", SqlDbType.TinyInt, 1), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
            commandParameters[0].Value = intStaffID;
            commandParameters[1].Value = intClubID;
            commandParameters[2].Value = intDCategory;
            commandParameters[3].Value = strEvent;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
        }

        public static void GetStaffByClubIDType(int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffByClubIDType ", intClubID, ",", intType });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCount(int intType, int intLevels)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffCount ", intType, ",", intLevels });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCountByLevel(int intLevel)
        {
            string commandText = "Exec NewBTP.dbo.GetStaffCountByLevel " + intLevel;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCountByLevelNew(int intLevel)
        {
            string commandText = "Exec NewBTP.dbo.GetStaffListByLevelNew " + intLevel + ",0,0,1";
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCountByTL(int intType, int intLevel)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffCountByTL ", intType, ",", intLevel });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCountByTLNew(int intType, int intLevel)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByTLNew ", intType, ",", intLevel, ",0,0,1" });
            return SqlHelper.ExecuteIntReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStaffCountByType(int intType)
        {
            string commandText = "Exec NewBTP.dbo.GetStaffCountByType " + intType;
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStaffList(int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffList ", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStaffListByLevel(int intLevel, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByLevel ", intLevel, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetStaffListByLevelNew(int intLevel, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByLevelNew ", intLevel, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStaffListByTL(int intType, int intLevel, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByTL ", intType, ",", intLevel, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static SqlDataReader GetStaffListByTLNew(int intType, int intLevel, int intPage, int intPerPage)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByTLNew ", intType, ",", intLevel, ",", intPage, ",", intPerPage, ",0" });
            return SqlHelper.ExecuteReader(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataTable GetStaffListByType(int intType, int intPage, int intPerPage, int intCount, int intTotal)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffListByType ", intType, ",", intPage, ",", intPerPage, ",", intCount, ",", intTotal });
            return SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetStaffRow(int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.GetStaffRow ", intClubID, ",", intType });
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetStaffRowByID(int intStaffID)
        {
            string commandText = "Exec NewBTP.dbo.GetStaffRowByID " + intStaffID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasEnoughMoney(int intUserID, int intSalary)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.HasEnoughMoney ", intUserID, ",", intSalary });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool HasStaff(int intClubID, int intType)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.HasStaff ", intClubID, ",", intType });
            return SqlHelper.ExecuteBoolDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void ResetStaffSalary()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.ResetStaffSalary";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                ResetStaffSalary();
            }
        }

        public static void SendSalary()
        {
            DataTable clubTable = BTPClubManager.GetClubTable();
            if (clubTable != null)
            {
                foreach (DataRow row in clubTable.Rows)
                {
                    string str2;
                    int num = (int) row["ClubID"];
                    int num2 = (int) row["UserID"];
                    int num3 = (byte) row["Category"];
                    if (num3 == 3)
                    {
                        str2 = "支付街球队职员工资。";
                    }
                    else
                    {
                        str2 = "支付职业队职员工资。";
                    }
                    string commandText = "Exec NewBTP.dbo.SendStaffSalary @intClubID,@intUserID,@intCategory,@strEvent";
                    SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@intClubID", SqlDbType.Int, 4), new SqlParameter("@intUserID", SqlDbType.Int, 4), new SqlParameter("@intCategory", SqlDbType.Int, 4), new SqlParameter("@strEvent", SqlDbType.NVarChar, 100) };
                    commandParameters[0].Value = num;
                    commandParameters[1].Value = num2;
                    commandParameters[2].Value = num3;
                    commandParameters[3].Value = str2;
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters);
                }
            }
        }

        public static void SetStaffSalary(int intStaffID)
        {
            string commandText = "Exec NewBTP.dbo.SetStaffSalary " + intStaffID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SpendMoney(int intUserID, int intSpendMoney)
        {
            string commandText = string.Concat(new object[] { "Exec NewBTP.dbo.SpendMoney ", intUserID, ",", intSpendMoney });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void StaffAddAge()
        {
            string commandText = "Exec NewBTP.dbo.UpdateStaffAge";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void StaffContract()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.StaffContract";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                StaffContract();
            }
        }

        public static void StaffRetire()
        {
            string commandText = "Exec NewBTP.dbo.StaffRetireTable";
            DataTable table = SqlHelper.ExecuteDataTable(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int num = (int) row["StaffID"];
                    int num2 = (int) row["ClubID"];
                    if (num2 == 0)
                    {
                        commandText = "Exec NewBTP.dbo.StaffRetire " + num;
                    }
                    else
                    {
                        commandText = string.Concat(new object[] { "Exec NewBTP.dbo.RetireAndMessage ", num, ",", num2 });
                    }
                    SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                }
            }
        }

        public static void UpdateStaffStatus()
        {
            string commandText = "Exec NewBTP.dbo.UpdateStaffStatus ";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


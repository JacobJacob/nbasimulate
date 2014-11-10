namespace Web.Helper
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using Web.DBConnection;
    using Web.VMatchEngine;

    public class MatchItem
    {
        public static int CanTrainMatchType(int intClubID)
        {
            string commandText = "Exec NewBTP.dbo.CanTrainMatchType @ClubID";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ClubID", SqlDbType.Int, 4) };
            commandParameters[0].Value = intClubID;
            if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText, commandParameters) != null)
            {
                return 0;
            }
            return 1;
        }

        public static int GetArrangeAdd(int intLvl)
        {
            return intLvl;
        }

        public static string GetBothMainXML(Team tHome, Team tAway, int intTicketCount, int intIncome)
        {
            return "";
        }

        public static string GetHard(int intChoice)
        {
            return (intChoice + "%");
        }

        public static DataView GetHardDataView()
        {
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("Index", typeof(int)));
            table.Columns.Add(new DataColumn("Name", typeof(string)));
            for (int i = 80; i <= 110; i += 5)
            {
                DataRow row = table.NewRow();
                row["Index"] = i;
                row["Name"] = GetHard(i);
                table.Rows.Add(row);
            }
            return new DataView(table);
        }

        public static string GetMatchCategory(byte byteCategory)
        {
            switch (byteCategory)
            {
                case 1:
                    return "友谊赛";

                case 2:
                    return "训练赛";

                case 3:
                    return "挑战赛";

                case 4:
                    return "让分赛";
            }
            return "友谊赛";
        }

        public static string GetMatchPath()
        {
            return IniUtil.ReadIniData("PathConfig", "MatchPath", "", @"C:\xba.ini");
        }

        public static int GetOneByOneLosePower(int intStamina, int intHard)
        {
            int num = RandomItem.rnd.Next(0, 0x3e8);
            if (num >= (0x44c - intStamina))
            {
                return 0;
            }
            if (num < (900 - ((500 * intHard) / 100)))
            {
                return 0;
            }
            return 1;
        }

        public static int GetPowerAffect(int intPower)
        {
            if (intPower > 90)
            {
                intPower = 90;
            }
            if (intPower < 30)
            {
                intPower = 30;
            }
            return (intPower + 10);
        }

        public static string GetQName(int intNum, int intType)
        {
            if (intType == 3)
            {
                if (intNum == 1)
                {
                    return "常规";
                }
                return ("加时(" + (intNum - 1) + ")");
            }
            if (intNum == 1)
            {
                return "第一节";
            }
            if (intNum == 2)
            {
                return "第二节";
            }
            if (intNum == 3)
            {
                return "第三节";
            }
            if (intNum == 4)
            {
                return "第四节";
            }
            return ("加时赛(" + (intNum - 4) + ")");
        }

        public static string GetScore(int intScore)
        {
            int num = intScore % 10;
            string str = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "Score/", num, ".gif' border='0' width='19' height='28'>" });
            int num2 = (intScore / 10) % 10;
            if ((num2 == 0) && (intScore < 10))
            {
                num2 = 0x63;
            }
            string str2 = string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "Score/", num2, ".gif' border='0' width='19' height='28'>" });
            int num3 = (intScore / 100) % 10;
            if ((num3 == 0) && (intScore < 100))
            {
                num3 = 0x63;
            }
            return (string.Concat(new object[] { "<img src='", SessionItem.GetImageURL(), "Score/", num3, ".gif' border='0' width='19' height='28'>" }) + str2 + str);
        }

        public static string GetSDefName(int intDef)
        {
            if (intDef == 1)
            {
                return "盯人内线";
            }
            if (intDef == 2)
            {
                return "盯人外线";
            }
            if (intDef == 3)
            {
                return "盯人防守";
            }
            return "区域联防";
        }

        public static string GetSelfMainXML(int intClubID)
        {
            return "";
        }

        public static int GetSLosePower(int intStamina, int intHard)
        {
            int num = RandomItem.rnd.Next(0, 0x3e8);
            if (num >= (0x44c - intStamina))
            {
                return 0;
            }
            if (num < (0x3e8 - ((500 * intHard) / 100)))
            {
                return 0;
            }
            return 1;
        }

        public static string GetSOffName(int intOff)
        {
            if (intOff == 1)
            {
                return "强打内线";
            }
            if (intOff == 2)
            {
                return "中锋策应";
            }
            if (intOff == 3)
            {
                return "外线投篮";
            }
            return "整体进攻";
        }

        public static string GetVDefName(int intDef)
        {
            if (intDef == 1)
            {
                return "2-3联防";
            }
            if (intDef == 2)
            {
                return "3-2联防";
            }
            if (intDef == 3)
            {
                return "2-1-2联防";
            }
            if (intDef == 4)
            {
                return "盯人防守";
            }
            if (intDef == 5)
            {
                return "盯人内线";
            }
            return "盯人外线";
        }

        public static int GetVLosePower(int intStamina, int intHard)
        {
            int num = RandomItem.rnd.Next(0, 0x3e8);
            if (num >= (0x3e8 - (intStamina / 3)))
            {
                return 0;
            }
            if (num < (0x3e8 - ((500 * intHard) / 100)))
            {
                return 0;
            }
            return RandomItem.rnd.Next(2, 4);
        }

        public static string GetVOffName(int intOff)
        {
            if (intOff == 1)
            {
                return "强打内线";
            }
            if (intOff == 2)
            {
                return "中锋策应";
            }
            if (intOff == 3)
            {
                return "外线投篮";
            }
            if (intOff == 4)
            {
                return "快速进攻";
            }
            if (intOff == 5)
            {
                return "整体配合";
            }
            return "掩护挡拆";
        }

        public static string GetWinLose(int intScoreA, int intScoreB)
        {
            if (intScoreA > intScoreB)
            {
                return "胜";
            }
            if (intScoreA == intScoreB)
            {
                return "平";
            }
            return "负";
        }

        public static bool InOnlyOneMatch(int intClubID5)
        {
            string commandText = "SELECT TOP 1 OnlyOneID FROM BTP_OnlyOneCenterReg WHERE ClubID=" + intClubID5;
            return (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null);
        }

        public static bool InOnlyOneMatch(int intClubIDA, int intClubIDB)
        {
            string commandText = string.Concat(new object[] { "SELECT TOP 1 OnlyOneID FROM BTP_OnlyOneCenterReg WHERE ClubID=", intClubIDA, " OR ClubID=", intClubIDB });
            return (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null);
        }

        public static int TrainMatchType(int intClubIDA, int intClubIDB, int intType)
        {
            string commandText = "SELECT AVG(Happy) FROM BTP_Player3 WHERE ClubID=" + intClubIDA;
            int num = SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            commandText = "SELECT MIN(Happy) FROM BTP_Player3 WHERE ClubID=" + intClubIDA;
            int num2 = SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if (num < 0x4b)
            {
                return 2;
            }
            if (num2 < 0x41)
            {
                return 2;
            }
            if (intType < 3)
            {
                commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND Category=2 AND (ClubIDA=", intClubIDA, " OR ClubIDB=", intClubIDA, ")" });
                if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                {
                    return 4;
                }
            }
            if ((intType == 1) || (intType == 3))
            {
                commandText = "SELECT AVG(Happy) FROM BTP_Player3 WHERE ClubID=" + intClubIDB;
                int num3 = SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                commandText = "SELECT MIN(Happy) FROM BTP_Player3 WHERE ClubID=" + intClubIDB;
                int num4 = SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if (num3 < 0x4b)
                {
                    return 3;
                }
                if (num4 < 0x41)
                {
                    return 3;
                }
                if (intType < 3)
                {
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND (Category=2 OR Category=5)  AND (ClubIDA=", intClubIDB, " OR ClubIDB=", intClubIDB, ")" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 4;
                    }
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE Category=2 AND Status<>3 AND ((ClubIDA=", intClubIDA, " AND ClubIDB=", intClubIDB, ") OR (ClubIDB=", intClubIDA, " AND ClubIDA=", intClubIDB, "))" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 5;
                    }
                }
            }
            return 1;
        }

        public static int TrainMatchType5(int intClubIDA, int intClubIDB, int intType)
        {
            string commandText = "";
            commandText = "SELECT AVG(Power) FROM BTP_Player5 WHERE ClubID=" + intClubIDA;
            int num = SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            commandText = "SELECT MIN(Power) FROM BTP_Player5 WHERE ClubID=" + intClubIDA;
            int num2 = SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            if ((num < 70) || (num2 < 50))
            {
                return 10;
            }
            if (intType < 3)
            {
                commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND (Category=10 OR Category=11) AND (ClubIDA=", intClubIDA, " OR ClubIDB=", intClubIDA, ")" });
                if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                {
                    return 4;
                }
                commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND (Category=1) AND (Type=5) AND (ClubIDA=", intClubIDA, " OR ClubIDB=", intClubIDA, ")" });
                if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                {
                    return 2;
                }
                commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND Category=7 AND (ClubIDA=", intClubIDA, " OR ClubIDB=", intClubIDA, ")" });
                if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                {
                    return 3;
                }
            }
            if ((intType == 1) || (intType == 3))
            {
                commandText = "SELECT AVG(Power) FROM BTP_Player5 WHERE ClubID=" + intClubIDB;
                int num3 = SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                commandText = "SELECT MIN(Power) FROM BTP_Player5 WHERE ClubID=" + intClubIDB;
                int num4 = SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
                if ((num3 < 70) || (num4 < 50))
                {
                    return 11;
                }
                if (intType < 3)
                {
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND (Category=10 OR Category=11) AND (ClubIDA=", intClubIDB, " OR ClubIDB=", intClubIDB, ")" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 9;
                    }
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND Category=1 AND (Type=5) AND (ClubIDA=", intClubIDB, " OR ClubIDB=", intClubIDB, ")" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 7;
                    }
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE (Status=1 OR Status=2) AND Category=7 AND (ClubIDA=", intClubIDB, " OR ClubIDB=", intClubIDB, ")" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 8;
                    }
                    commandText = string.Concat(new object[] { "SELECT FMatchID FROM BTP_FriMatch WHERE Category=10 AND Status<>3 AND ((ClubIDA=", intClubIDA, " AND ClubIDB=", intClubIDB, ") OR (ClubIDB=", intClubIDA, " AND ClubIDA=", intClubIDB, "))" });
                    if (SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText) != null)
                    {
                        return 5;
                    }
                }
            }
            return 1;
        }
    }
}


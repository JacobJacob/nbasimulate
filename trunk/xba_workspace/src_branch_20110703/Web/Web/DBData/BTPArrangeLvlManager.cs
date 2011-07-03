namespace Web.DBData
{
    using System;
    using System.Data;
    using System.Threading;
    using Web.DBConnection;

    public class BTPArrangeLvlManager
    {
        public static void AddArrange5Lvl(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.AddArrange5Lvl " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddArrangeLvl(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.AddArrangeLvl " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void CreateArrangeLvl(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.CreateArrangeLvl " + intUserID;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int Get3ArrangeNeed(int intLevel)
        {
            intLevel++;
            return ((intLevel * 5) + ((intLevel * intLevel) * intLevel));
        }

        public static int Get5ArrangeNeed(int intLevel)
        {
            intLevel++;
            return (3 * ((intLevel * 5) + ((intLevel * intLevel) * intLevel)));
        }

        public static DataRow GetArrange3(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrange3 " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange3Lvl(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrange3Lvl " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange5(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrange5 " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static DataRow GetArrange5Lvl(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetArrange5Lvl " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetArrange3Point(int intClubID, int[] intOffs, int[] intDefs)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.SetArrange3Point ", intClubID, ",", intOffs[0], ",", intOffs[1], ",", intOffs[2], ",", intOffs[3], ",", intDefs[0], ",", intDefs[1], ",", intDefs[2], 
                ",", intDefs[3]
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetArrange5Point(int intClubID, int[] intOffs, int[] intDefs)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.SetArrange5Point ", intClubID, ",", intOffs[0], ",", intOffs[1], ",", intOffs[2], ",", intOffs[3], ",", intOffs[4], ",", intOffs[5], ",", intDefs[0], 
                ",", intDefs[1], ",", intDefs[2], ",", intDefs[3], ",", intDefs[4], ",", intDefs[5]
             });
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateArrange3Lvl()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdateArrange3Lvl";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdateArrange3Lvl();
            }
        }

        public static void UpdateArrange5Lvl()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.UpdateArrange5Lvl";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                UpdateArrange5Lvl();
            }
        }

        public static int UpdateArrangeLvlByUserID(int intUserID, int[] intOffs, int[] intDefs)
        {
            string commandText = string.Concat(new object[] { 
                "Exec NewBTP.dbo.UpdateArrangeLvlByUserID ", intUserID, ",", intOffs[0], ",", intOffs[1], ",", intOffs[2], ",", intOffs[3], ",", intOffs[4], ",", intOffs[5], ",", intDefs[0], 
                ",", intDefs[1], ",", intDefs[2], ",", intDefs[3], ",", intDefs[4], ",", intDefs[5]
             });
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


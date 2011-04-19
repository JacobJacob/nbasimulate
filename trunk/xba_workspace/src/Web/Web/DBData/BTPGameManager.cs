namespace Web.DBData
{
    using ServerManage;
    using System;
    using System.Data;
    using System.Threading;
    using Web;
    using Web.DBConnection;

    public class BTPGameManager
    {
        public static void AddPlayedYear()
        {
            string commandText = "Exec NewBTP.dbo.AddPlayedYear";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void AddPlayerAge()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.AddPlayerAge";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                AddPlayerAge();
            }
        }

        public static int GetConnTime()
        {
            if (ServerParameter.blnIsExe)
            {
                return 0x270f;
            }
            return 10;
        }

        public static int GetDevLevelSum()
        {
            string commandText = "Exec NewBTP.dbo.GetDevLevelSum";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetGameCategory()
        {
            return ServerParameter.intGameCategory;
        }

        public static int GetGameDays()
        {
            return (int) GetGameRow()["Days"];
        }

        public static DataRow GetGameRow()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.GetGameRow ";
                return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                return GetGameRow();
            }
        }

        public static DataRow GetGameRow(string strConn)
        {
            string commandText = "Exec NewBTP.dbo.GetGameRow ";
            return SqlHelper.ExecuteDataRow(strConn, CommandType.Text, commandText);
        }

        public static DataRow GetIsChooseInfo(int intUserID)
        {
            string commandText = "Exec NewBTP.dbo.GetIsChooseInfo " + intUserID;
            return SqlHelper.ExecuteDataRow(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetSeason()
        {
            string commandText = "Exec NewBTP.dbo.GetSeason";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetStartUpdate()
        {
            return Global.intStartUpdate;
        }

        public static int GetStatus()
        {
            string commandText = "Exec NewBTP.dbo.GetStatus";
            return SqlHelper.ExecuteTinyIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static int GetTurn()
        {
            string commandText = "Exec NewBTP.dbo.GetTurn";
            return SqlHelper.ExecuteIntDataField(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static bool GetUseServer()
        {
            return ServerParameter.blnUseServer;
        }

        public static string GetWebKey()
        {
            return Global.strWebKey;
        }

        public static string GetWebName()
        {
            return Global.strWebName;
        }

        public static void IncDays()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.SetDays";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                IncDays();
            }
        }

        public static void IncLevel()
        {
            string commandText = "Exec NewBTP.dbo.IncLevel";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void IncSeason()
        {
            string commandText = "Exec NewBTP.dbo.SetSeason ";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void IncTurn()
        {
            try
            {
                string commandText = "Exec NewBTP.dbo.SetTurn ";
                SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                Console.WriteLine("Three ms later...\n");
                Thread.Sleep(0x2bf20);
                IncTurn();
            }
        }

        public static bool IsMainServer()
        {
            return (ServerParameter.intGameCategory == 0);
        }

        public static void PlayerPreRetire()
        {
            string commandText = "Exec NewBTP.dbo.PlayerPreRetire";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void SetStatus(int intStatus)
        {
            string commandText = "Exec NewBTP.dbo.SetStatus " + intStatus;
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateGameBegin()
        {
            string commandText = "Exec NewBTP.dbo.UpdateGameBegin";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }

        public static void UpdateGameFinish()
        {
            string commandText = "Exec NewBTP.dbo.UpdateGameFinish";
            SqlHelper.ExecuteNonQuery(DBSelector.GetConnection("btp01"), CommandType.Text, commandText);
        }
    }
}


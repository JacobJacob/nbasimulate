namespace Web.Helper
{
    using System;

    internal class Config
    {
        public static int DAY_NPC_BUXINZHE_TIMES = -1;
        public static int DAY_NPC_FREE_TIMES = -1;
        public static int DAY_NPC_HUREN_TIMES = -1;
        public static string DOMAIN = null;
        public static int[] NPC_CLUB_IDS = null;
        public static string PAY_LINK = null;
        public static int PLAYER3_FL_ENABLE = -1;
        public static int PLAYER3_QT_ENABLE = -1;
        public static int PLAYER3_RECOVER_ENABLE = -1;
        public static int PLAYER3_SZ_ENABLE = -1;
        public static int PLAYER5_COUNT = -1;
        public static int PLAYER5_FL_ENABLE = -1;
        public static int PLAYER5_JS_ENABLE = -1;
        public static int PLAYER5_MG_ENABLE = -1;
        public static int PLAYER5_RECOVER_ENABLE = -1;
        public static double PLAYER5_TRAIN_POINT_MULTIPLE = -1.0;
        public static int TOOL_ENABLE = -1;
        public static int TRAIN_POINT_MULTIPLE = -1;
        public static int XIDIAN_ENABLE = -1;

        public static int DayNpcFreeTimes()
        {
            if (DAY_NPC_FREE_TIMES == -1)
            {
                DAY_NPC_FREE_TIMES = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "DayNpcFreeTimes", "10", @"C:\xba.ini"));
            }
            return DAY_NPC_FREE_TIMES;
        }

        public static string GetDomain()
        {
            if (DOMAIN == null)
            {
                DOMAIN = IniUtil.ReadIniData("PathConfig", "Domain", "", @"C:\xba.ini");
            }
            return DOMAIN;
        }

        public static string GetPayLink()
        {
            if (PAY_LINK == null)
            {
                PAY_LINK = IniUtil.ReadIniData("PathConfig", "PayLink", "http://www.xs1234.com/?sid=11365", @"C:\xba.ini");
            }
            return PAY_LINK;
        }

        public static int GetPlayer3RecoverEnable()
        {
            if (PLAYER3_RECOVER_ENABLE == -1)
            {
                PLAYER3_RECOVER_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player3RecoverEnable", "0", @"C:\xba.ini"));
            }
            return PLAYER3_RECOVER_ENABLE;
        }

        public static int GetPlayer5RecoverEnable()
        {
            if (PLAYER5_RECOVER_ENABLE == -1)
            {
                PLAYER5_RECOVER_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player5RecoverEnable", "0", @"C:\xba.ini"));
            }
            return PLAYER5_RECOVER_ENABLE;
        }

        public static double GetPlayer5TrainPointMultiple()
        {
            if (PLAYER5_TRAIN_POINT_MULTIPLE == -1.0)
            {
                PLAYER5_TRAIN_POINT_MULTIPLE = Convert.ToDouble(IniUtil.ReadIniData("GameConfig", "Player5TrainPointMultiple", "1.0", @"C:\xba.ini"));
            }
            return PLAYER5_TRAIN_POINT_MULTIPLE;
        }

        public static int GetToolEnable()
        {
            if (TOOL_ENABLE == -1)
            {
                TOOL_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "ToolEnable", "0", @"C:\xba.ini"));
            }
            return TOOL_ENABLE;
        }

        public static int GetTrainPointMultiple()
        {
            if (TRAIN_POINT_MULTIPLE == -1)
            {
                TRAIN_POINT_MULTIPLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "TrainPointMultiple", "1", @"C:\xba.ini"));
            }
            return TRAIN_POINT_MULTIPLE;
        }

        public static int GetXiDianEnable()
        {
            if (XIDIAN_ENABLE == -1)
            {
                XIDIAN_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "XiDianEnable", "0", @"C:\xba.ini"));
            }
            return XIDIAN_ENABLE;
        }

        public static bool IsNPCClub(int clubID)
        {
            if (NPC_CLUB_IDS == null)
            {
                string[] strArray = IniUtil.ReadIniData("GameConfig", "NpcClubIDS", "", @"C:\xba.ini").Split(new char[] { ',' });
                NPC_CLUB_IDS = new int[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    NPC_CLUB_IDS[i] = Convert.ToInt32(strArray[i]);
                }
            }
            foreach (int num2 in NPC_CLUB_IDS)
            {
                if (num2 == clubID)
                {
                    return true;
                }
            }
            return false;
        }

        public static int NpcBuXinZheTimes()
        {
            if (DAY_NPC_BUXINZHE_TIMES == -1)
            {
                DAY_NPC_BUXINZHE_TIMES = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "NpcBuXinZheTimes", "60", @"C:\xba.ini"));
            }
            return DAY_NPC_BUXINZHE_TIMES;
        }

        public static int NpcHurenTimes()
        {
            if (DAY_NPC_HUREN_TIMES == -1)
            {
                DAY_NPC_HUREN_TIMES = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "NpcHuRenTimes", "20", @"C:\xba.ini"));
            }
            return DAY_NPC_HUREN_TIMES;
        }

        public static bool Player3FLEnable()
        {
            if (PLAYER3_FL_ENABLE == -1)
            {
                PLAYER3_FL_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player3FLEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER3_FL_ENABLE == 1);
        }

        public static bool Player3QTEnable()
        {
            if (PLAYER3_QT_ENABLE == -1)
            {
                PLAYER3_QT_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player3QTEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER3_QT_ENABLE == 1);
        }

        public static bool Player3SZEnable()
        {
            if (PLAYER3_SZ_ENABLE == -1)
            {
                PLAYER3_SZ_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player3SZEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER3_SZ_ENABLE == 1);
        }

        public static int Player5Count()
        {
            if (PLAYER5_COUNT == -1)
            {
                PLAYER5_COUNT = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player5Count", "14", @"C:\xba.ini"));
            }
            return PLAYER5_COUNT;
        }

        public static bool Player5FLEnable()
        {
            if (PLAYER5_FL_ENABLE == -1)
            {
                PLAYER5_FL_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player5FLEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER5_FL_ENABLE == 1);
        }

        public static bool Player5JSEnable()
        {
            if (PLAYER5_JS_ENABLE == -1)
            {
                PLAYER5_JS_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player5JSEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER5_JS_ENABLE == 1);
        }

        public static bool Player5MGEnable()
        {
            if (PLAYER5_MG_ENABLE == -1)
            {
                PLAYER5_MG_ENABLE = Convert.ToInt32(IniUtil.ReadIniData("GameConfig", "Player5MGEnable", "0", @"C:\xba.ini"));
            }
            return (PLAYER5_MG_ENABLE == 1);
        }
    }
}


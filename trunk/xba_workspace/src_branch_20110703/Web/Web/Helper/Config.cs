using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Helper
{
	class Config
	{

        public static string DOMAIN = null;
        public static int TRAIN_POINT_MULTIPLE = -1;
        public static int TOOL_ENABLE = -1;
        public static int XIDIAN_ENABLE = -1;
        public static int PLAYER3_RECOVER_ENABLE = -1;
        public static int PLAYER5_RECOVER_ENABLE = -1;
        public static double PLAYER5_TRAIN_POINT_MULTIPLE = -1;

        /**
         * 域名
         */

        public static string GetDomain()
        {
            if (DOMAIN == null)
            {
                DOMAIN = IniUtil.ReadIniData("PathConfig", "Domain", "", "C:\\xba.ini");
            }

            return DOMAIN;
            
        }

        /**
         *训练点倍数 
         */

        public static int GetTrainPointMultiple()
        {
            if (TRAIN_POINT_MULTIPLE == -1)
            {
                string trainPointMultiple = IniUtil.ReadIniData("GameConfig", "TrainPointMultiple", "1", "C:\\xba.ini");
                TRAIN_POINT_MULTIPLE = Convert.ToInt32(trainPointMultiple);
            }

            return TRAIN_POINT_MULTIPLE;
        }

        /**
         * 是否启用道具
         */

        public static int GetToolEnable()
        {
            if (TOOL_ENABLE == -1)
            {
                string toolEnable = IniUtil.ReadIniData("GameConfig", "ToolEnable", "0", "C:\\xba.ini");
                TOOL_ENABLE = Convert.ToInt32(toolEnable);
            }

            return TOOL_ENABLE;
        }

        /**
         * 是否启用洗点
         */

        public static int GetXiDianEnable()
        {
            if (XIDIAN_ENABLE == -1)
            {
                string xidianEnable = IniUtil.ReadIniData("GameConfig", "XiDianEnable", "0", "C:\\xba.ini");
                XIDIAN_ENABLE = Convert.ToInt32(xidianEnable);
            }

            return XIDIAN_ENABLE;
        }

        /**
        * 是否启用职业体力恢复
        */

        public static int GetPlayer5RecoverEnable()
        {
            if (PLAYER5_RECOVER_ENABLE == -1)
            {
                string player5RecovereEnable = IniUtil.ReadIniData("GameConfig", "Player5RecoverEnable", "0", "C:\\xba.ini");
                PLAYER5_RECOVER_ENABLE = Convert.ToInt32(player5RecovereEnable);
            }

            return PLAYER5_RECOVER_ENABLE;
        }

        /**
        * 是否启用街头体力恢复
        */

        public static int GetPlayer3RecoverEnable()
        {
            if (PLAYER3_RECOVER_ENABLE == -1)
            {
                string player3RecovereEnable = IniUtil.ReadIniData("GameConfig", "Player3RecoverEnable", "0", "C:\\xba.ini");
                PLAYER3_RECOVER_ENABLE = Convert.ToInt32(player3RecovereEnable);
            }

            return PLAYER3_RECOVER_ENABLE;
        }

         /**
        * 是否启用街头体力恢复
        */

        public static double GetPlayer5TrainPointMultiple()
        {
            if (PLAYER5_TRAIN_POINT_MULTIPLE == -1)
            {
                string getPlayer5TrainPointMultiple = IniUtil.ReadIniData("GameConfig", "Player5TrainPointMultiple", "1.0", "C:\\xba.ini");
                PLAYER5_TRAIN_POINT_MULTIPLE = Convert.ToDouble(getPlayer5TrainPointMultiple);
            }

            return PLAYER5_TRAIN_POINT_MULTIPLE;
        }

	}
}

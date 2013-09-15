using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Helper
{
	class Config
	{

        public static string DOMAIN = null;
        public static string PAY_LINK = null;
        public static int TRAIN_POINT_MULTIPLE = -1;
        public static int TOOL_ENABLE = -1;
        public static int XIDIAN_ENABLE = -1;
        public static int PLAYER3_RECOVER_ENABLE = -1;
        public static int PLAYER5_RECOVER_ENABLE = -1;
        public static double PLAYER5_TRAIN_POINT_MULTIPLE = -1;

        /*ħ��ѵ��*/
        public static int PLAYER5_MG_ENABLE = -1;
        /*����ѵ��*/
        public static int PLAYER5_JS_ENABLE = -1;
        /*���ϻ�ͯ*/
        public static int PLAYER5_FL_ENABLE = -1;


        /*��֮����*/
        public static int PLAYER3_SZ_ENABLE = -1;
        /*���ϻ�ͯ*/
        public static int PLAYER3_FL_ENABLE = -1;
        /*��̽����*/
        public static int PLAYER3_QT_ENABLE = -1;

        /*ְҵ��Ա����*/
        public static int PLAYER5_COUNT = -1;

        /*NPC��ս���˴���*/
        public static int DAY_NPC_HUREN_TIMES = -1;

        /*NPC��ս�����ߴ���*/
        public static int DAY_NPC_BUXINZHE_TIMES = -1;

        public static int[] NPC_CLUB_IDS = null;
       


        /**
         * ����
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
         * ����
         */

        public static string GetPayLink()
        {
            if (PAY_LINK == null)
            {
                PAY_LINK = IniUtil.ReadIniData("PathConfig", "PayLink", "http://www.xs1234.com/?sid=11365", "C:\\xba.ini");
            }

            return PAY_LINK;
        }     
       
        /**
         *ѵ���㱶�� 
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
         * �Ƿ����õ���
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
         * �Ƿ�����ϴ��
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
        * �Ƿ�����ְҵ�����ָ�
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
        * �Ƿ����ý�ͷ�����ָ�
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
        * ְҵѵ����ѵ���㷢��ָ��
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

      /**
      * �Ƿ����÷��ϻ�ͯ(��ͷ)
      */

        public static bool Player3FLEnable()
        {
            if (PLAYER3_FL_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player3FLEnable", "0", "C:\\xba.ini");
                PLAYER3_FL_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER3_FL_ENABLE == 1;
        }

        /**
         * �Ƿ�������̽����
         */

        public static bool Player3QTEnable()
        {
            if (PLAYER3_QT_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player3QTEnable", "0", "C:\\xba.ini");
                PLAYER3_QT_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER3_QT_ENABLE == 1;
        }

        /**
         * �Ƿ�������֮����
         */

        public static bool Player3SZEnable()
        {
            if (PLAYER3_SZ_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player3SZEnable", "0", "C:\\xba.ini");
                PLAYER3_SZ_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER3_SZ_ENABLE == 1;
        }

        /**
        * �Ƿ����ü���ѵ��
        */

        public static bool Player5JSEnable()
        {
            if (PLAYER5_JS_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player5JSEnable", "0", "C:\\xba.ini");
                PLAYER5_JS_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER5_JS_ENABLE == 1;
        }

       /**
       * �Ƿ�����ħ��ѵ��
       */

        public static bool Player5MGEnable()
        {
            if (PLAYER5_MG_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player5MGEnable", "0", "C:\\xba.ini");
                PLAYER5_MG_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER5_MG_ENABLE == 1;
        }

       /**
       * �Ƿ����÷��ϻ�ͯ(ְҵ)
       */

        public static bool Player5FLEnable()
        {
            if (PLAYER5_FL_ENABLE == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player5FLEnable", "0", "C:\\xba.ini");
                PLAYER5_FL_ENABLE = Convert.ToInt32(value);
            }

            return PLAYER5_FL_ENABLE == 1;
        }

      /**
       * �Ƿ����÷��ϻ�ͯ(ְҵ)
       */

        public static int Player5Count()
        {
            if (PLAYER5_COUNT == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "Player5Count", "14", "C:\\xba.ini");
                PLAYER5_COUNT = Convert.ToInt32(value);
            }

            return PLAYER5_COUNT;
        }

        /// <summary>
        /// ������ս����
        /// </summary>
        /// <returns></returns>
        public static int NpcHurenTimes()
        {
            if (DAY_NPC_HUREN_TIMES == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "NpcHuRenTimes", "20", "C:\\xba.ini");
                DAY_NPC_HUREN_TIMES = Convert.ToInt32(value);
            }

            return DAY_NPC_HUREN_TIMES;
        }

        /// <summary>
        /// ��������ս����
        /// </summary>
        /// <returns></returns>
        public static int NpcBuXinZheTimes()
        {
            if (DAY_NPC_BUXINZHE_TIMES == -1)
            {
                string value = IniUtil.ReadIniData("GameConfig", "NpcBuXinZheTimes", "60", "C:\\xba.ini");
                DAY_NPC_BUXINZHE_TIMES = Convert.ToInt32(value);
            }

            return DAY_NPC_BUXINZHE_TIMES;
        }


        /// <summary>
        /// �Ƿ���NPC
        /// </summary>
        /// <param name="clubID"></param>
        /// <returns></returns>
        public static bool IsNPCClub(int clubID)
        {
            if (NPC_CLUB_IDS == null)
            {
                string npcClubIDS = IniUtil.ReadIniData("GameConfig", "NpcClubIDS", "", "C:\\xba.ini");
                string[] ids = npcClubIDS.Split(',');
                NPC_CLUB_IDS = new int[ids.Length];
                for (int i = 0; i < ids.Length; i++)
                {
                    NPC_CLUB_IDS[i] = Convert.ToInt32(ids[i]);
                }
            }

            foreach(int id in NPC_CLUB_IDS)
            {
                if(id == clubID)
                {
                    return true;
                }
            }

            return false;
        }

	}
}

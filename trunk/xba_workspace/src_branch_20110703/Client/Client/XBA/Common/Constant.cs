using System;
using System.Collections.Generic;
using System.Text;

namespace Client.XBA.Common
{
    class Constant
    {

        /*街球赛*/
        public static int MATCH_TYPE_3 = 3;


        /*职业赛*/
        public static int MATCH_TYPE_5 = 5;


        /*等待中*/
        public static int MATCH_STATUS_WAIT = 2;
        /*已经打完*/
        public static int MATCH_STATUS_FINISH = 4;


        /*街球比赛处理客户端*/
        public static String CLIENT_TYPE_CLUB3_MATCH_HANDLER = "club3_match_handler";

        /*职业比赛处理客户端*/
        public static String CLIENT_TYPE_CLUB5_MATCH_HANDLER = "club5_match_handler";

        /*联赛比赛处理客户端*/
        public static String CLIENT_TYPE_DEV_MATCH_HANDLER = "dev_match_handler";

        /*轮次更新客户端*/
        public static String CLIENT_TYPE_ROUND_UPDATE_HANDLER = "round_update_handler";


        /*友谊赛*/
        public static int MATCH_CATEGORY_FRIEND = 1;

        /*胜者为王*/
        public static int MATCH_CATEGORY_ONLY_ONE = 8;

        /*职业联赛*/
        public static int MATCH_CATEGORY_DEV_MATCH = 2;

        /*职业训练*/
        public static int MATCH_CATEGORY_TRAIN_A = 10;

        /*职业训练*/
        public static int MATCH_CATEGORY_TRAIN = 11;
        
        
        /* 这是街球的, > 2的为经典赛事*/
        /*小杯赛*/
        public static int CUP_TYPE_SMALL = 2;
        /*大杯赛*/
        public static int CUP_TYPE_BIG = 1;
        /*联盟至尊杯*/
        public static int CUP_TYPE_IMPERIAL = 4;
    }
}

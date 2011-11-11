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

        /*赛季更新客户端*/
        public static String CLIENT_TYPE_SEASON_UPDATE_HANDLER = "season_update_handler";

        /*自定义杯赛比赛客户端*/
        public static String CLIENT_TYPE_DEVCUP_MATCH_HANDLER = "devcup_match_handler";

        /*街球杯赛比赛客户端*/
        public static String CLIENT_TYPE_CUP_MATCH_HANDLER = "cup_match_handler";

        /*冠军杯赛比赛客户端*/
        public static String CLIENT_TYPE_XCUP_MATCH_HANDLER = "xcup_match_handler";

        /*将球员从阵容中替掉*/
        public static String CHANGE_PLAYER_FROM_ARRANGE5_HANDLER = "change_player_from_arrange5_handler";

        /*全明星赛比赛处理客户端*/
        public static String CLIENT_TYPE_STAR_MATCH_HANDLER = "star_match_handler";

        /*测试正常执行*/
        public static String CLIENT_TYPE_TEST_SUCCESS = "test_success";

        /*测测抛错*/
        public static String CLIENT_TYPE_TEST_ERROR = "test_error";


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

        /*盟战*/
        public static int MATCH_CATEGORY_UNION_FIELD = 9;

        /*自定义杯赛比赛*/
        public static int MATCH_CATEGORY_DEVCUP_MATCH = 6;

        /*冠军杯赛比赛*/
        public static int MATCH_CATEGORY_XCUP_MATCH = 6;
        
        
        /* 这是街球的, > 2的为经典赛事*/
        /*小杯赛*/
        public static int CUP_TYPE_SMALL = 2;
        /*大杯赛*/
        public static int CUP_TYPE_BIG = 1;
        /*联盟至尊杯*/
        public static int CUP_TYPE_IMPERIAL = 4;
    }
}

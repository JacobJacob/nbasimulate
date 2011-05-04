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


        /*比赛处理客户端*/
        public static String CLIENT_TYPE_MATCH_HANDLER = "match_handler";


        /*友谊赛*/
        public static int MATCH_CATEGORY_FRIEND = 1;

        /*胜者为王*/
        public static int MATCH_CATEGORY_ONLY_ONE = 8;
        
        
        /* 这是街球的, > 2的为经典赛事*/
        /*小杯赛*/
        public static int CUP_TYPE_SMALL = 2;
        /*大杯赛*/
        public static int CUP_TYPE_BIG = 1;
    }
}

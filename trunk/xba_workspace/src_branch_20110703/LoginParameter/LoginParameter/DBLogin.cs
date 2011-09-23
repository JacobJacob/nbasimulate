namespace LoginParameter
{
    using ServerManage;
    using System;
    using System.Data.SqlClient;

    public class DBLogin
    {
        public static bool CanConn(int intCategory)
        {
            try
            {
                new SqlConnection(TestConnString(intCategory)).Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ConnString(int intCategory)
        {
            return GetConnWithTime(intCategory, GetConnTime());
        }

        public static string GameNameChinese(int intCategory)
        {
            /*if (ServerParameter.strCopartner == "XBA")
            {
                switch (intCategory)
                {
                    case 0:
                        return "主站";

                    case 1:
                        return "北方大陆1";

                    case 2:
                        return "南方大陆1";

                    case 3:
                        return "南方大陆2";

                    case 4:
                        return "双线区1";

                    case 40:
                        return "主站";

                    case 0x31:
                        return "测试服务器";

                    case 50:
                        return "测试服务器";

                    case 0x33:
                        return "测试服务器";
                }
                return null;
            }
            if (ServerParameter.strCopartner == "CGA")
            {
                switch (intCategory)
                {
                    case 0:
                        return "主站";

                    case 1:
                        return "浩方一区";

                    case 2:
                        return "浩方二区";

                    case 40:
                        return "主站";
                }
                return null;
            }*/
            return "无道服";
        }

        public static string GameNameEnglish(int intCategory)
        {
            if (ServerParameter.strCopartner == "XBA")
            {
                switch (intCategory)
                {
                    case 0:
                        return "Main";

                    case 1:
                        return "XBA01";

                    case 2:
                        return "XBA02";

                    case 3:
                        return "XBA03";

                    case 4:
                        return "XBA04";

                    case 40:
                        return "Main";

                    case 0x31:
                        return "TestServer";

                    case 50:
                        return "TestServer";

                    case 0x33:
                        return "TestServer";
                }
                return null;
            }
            if (ServerParameter.strCopartner == "CGA")
            {
                switch (intCategory)
                {
                    case 0:
                        return "Main";

                    case 1:
                        return "HF01";

                    case 2:
                        return "HF02";

                    case 40:
                        return "Main";
                }
                return null;
            }
            return null;
        }

        public static int GetConnTime()
        {
            if (ServerParameter.blnIsExe)
            {
                return 0x270f;
            }
            return 10;
        }

        public static string GetConnWithTime(int intCategory, int intTimeout)
        {

            return ("user id = BTPAdmin;password = BTPAdmin123;initial catalog=NewBTP;data source=127.0.0.1;Connection Timeout=" + intTimeout + ";");

            //if (!ServerParameter.blnUseServer)
            //{

            //    switch (intCategory)
            //    {
            //        case 0:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=ROOT_Data;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 1:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 2:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 3:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 4:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 40:
            //            return @"Data Source=192.168.1.11\SQL2005;Initial Catalog=maitiam_main;Persist Security Info=True;User ID=MaitiamAdmin;Password=ehfe24@#da%@547@";

            //        case 0x31:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=ROOT_Data;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 50:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=ROOT_Data;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");

            //        case 0x33:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=192.168.1.11;Connection Timeout=" + intTimeout + ";");
            //    }
            //    return null;
            //}
            //if (ServerParameter.strCopartner == "XBA")
            //{
            //    switch (intCategory)
            //    {
            //        case 0:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=ROOT_Data;data source=222.191.251.134;Connection Timeout=" + intTimeout + ";");

            //        case 1:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=221.130.187.77;Connection Timeout=" + intTimeout + ";");

            //        case 2:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=121.205.90.19;Connection Timeout=" + intTimeout + ";");

            //        case 3:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=121.205.90.81;Connection Timeout=" + intTimeout + ";");

            //        case 4:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=60.28.222.181;Connection Timeout=" + intTimeout + ";");

            //        case 40:
            //            return "Data Source=121.205.90.11,2149;Initial Catalog=maitiam_main;Persist Security Info=True;User ID=MaitiamAdmin;Password=ehfe24@#da%@547@";
            //    }
            //    return null;
            //}
            //if (ServerParameter.strCopartner == "CGA")
            //{
            //    switch (intCategory)
            //    {
            //        case 1:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=121.205.90.77;Connection Timeout=" + intTimeout + ";");

            //        case 2:
            //            return ("user id = BTPAdmin;password = btp20031118;initial catalog=NewBTP;data source=121.205.90.79;Connection Timeout=" + intTimeout + ";");

            //        case 40:
            //            return "Data Source=121.205.90.75,2149;Initial Catalog=maitiam_main;Persist Security Info=True;User ID=MaitiamAdmin;Password=ehfe24@#da%@547@";
            //    }
            //    return null;
            //}
            //return null;
        }

     
        public static string TestConnString(int intCategory)
        {
            return GetConnWithTime(intCategory, 5);
        }

        public static string URLString(int intCategory)
        {
            /*if (!ServerParameter.blnUseServer)
            {
                switch (intCategory)
                {
                    case -1:
                        return "http://localhost:102/Forum/";

                    case 0:
                        return "http://localhost:102/Main/";

                    case 1:
                        return "http://localhost:102/Web/";

                    case 2:
                        return "http://localhost:102/Web/";

                    case 3:
                        return "http://localhost:102/Web/";

                    case 4:
                        return "http://localhost:102/Web/";

                    case 40:
                        return "http://localhost:102/Main/";

                    case 0x31:
                        return "http://forum.testxba.com.cn";

                    case 50:
                        return "http://www.testxba.com.cn";

                    case 0x33:
                        return "http://game.testxba.com.cn";
                }
                return null;
            }
            if (ServerParameter.strCopartner == "XBA")
            {
                switch (intCategory)
                {
                    case -1:
                        return "http://bbs.xba.com.cn/";

                    case 0:
                        return "http://www.xba.com.cn/";

                    case 1:
                        return "http://n1.xba.com.cn/";

                    case 2:
                        return "http://s1.xba.com.cn/";

                    case 3:
                        return "http://s2.xba.com.cn/";

                    case 4:
                        return "http://d1.xba.com.cn/";

                    case 40:
                        return "http://www.113388.net/";
                }
                return null;
            }
            if (ServerParameter.strCopartner == "CGA")
            {
                switch (intCategory)
                {
                    case 0:
                        return "http://xba.cga.com.cn/";

                    case 1:
                        return "http://web1.xba.cga.com.cn/";

                    case 2:
                        return "http://web2.xba.cga.com.cn/";

                    case 40:
                        return "http://xba.cga.com.cn/";
                }
                return null;
            }
            return null;*/
            return "http://www.113388.net/";
        }
    }
}


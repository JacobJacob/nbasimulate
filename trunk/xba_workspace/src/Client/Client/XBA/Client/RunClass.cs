using System;
using System.Collections.Generic;
using System.Text;
using Client.XBA.Common;
using Web.Helper;
using System.Collections;

namespace Client.XBA.Client
{
    class RunClass
    {

        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                Console.WriteLine(args[0]);
                LogManager.WriteLog("run", args[0]);
                if (args[0] == Constant.CLIENT_TYPE_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    new MatchHandler().start();
                }
                else if (args[0] == Constant.CLIENT_TYPE_DEV_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 2)
                    {
                        int matchId = Convert.ToInt32(args[1]);
                        new DevMatchHandler(matchId).start();
                    }
                    else
                    {
                        Console.WriteLine(String.Format("please input the match id"));
                    }
                }
            }
            else
            {

                Console.WriteLine("please input the command");
            }

            Hashtable info = new Hashtable();
            info.Add("ScoreH", 4);
            info.Add("ScoreA", 5);
            info.Add("ClubNameH", "name11");
            info.Add("ClubNameA", "name22");
            info.Add("ClubLogoH", "logo11");
            info.Add("ClubLogoA", "logo22");
            info.Add("MVPName", "mvp1");
            string value = MainXmlHelper.GetNewMainXml(null, info);
            Console.WriteLine(value);
            return;
        }

    }
}

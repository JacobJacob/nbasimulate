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
                if (args[0] == Constant.CLIENT_TYPE_CLUB5_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    new Club5MatchHandler().start();
                }
                else if (args[0] == Constant.CLIENT_TYPE_CLUB3_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    new Club3MatchHandler().start();
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
                else if (args[0] == Constant.CLIENT_TYPE_ROUND_UPDATE_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 2)
                    {
                        int step = Convert.ToInt32(args[1]);
                        new RoundUpdateHandler(step).start();
                    }
                    else
                    {
                        new RoundUpdateHandler().start();
                    }
                }
                else if (args[0] == Constant.CLIENT_TYPE_DEVCUP_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 6)
                    {
                        int devCupID = Convert.ToInt32(args[1]);
                        string gainCode = args[2];
                        int clubA = Convert.ToInt32(args[3]);
                        int clubB = Convert.ToInt32(args[4]);
                        int round = Convert.ToInt32(args[5]);
                        new DevCupMatchHandler(devCupID, gainCode, clubA, clubB, round).start();
                    }
                    else
                    {
                        Console.WriteLine("param error while handle devcup match!!!!");
                    }
                }
            }
            else
            {

                Console.WriteLine("please input the command");
            }

            return;
        }

    }
}

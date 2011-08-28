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
                else if (args[0] == Constant.CLIENT_TYPE_XCUP_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 8)
                    {
                        int xCupID = Convert.ToInt32(args[1]);
                        string gainCode = args[2];
                        int clubA = Convert.ToInt32(args[3]);
                        int clubB = Convert.ToInt32(args[4]);
                        int round = Convert.ToInt32(args[5]);
                        int category = Convert.ToInt32(args[6]);
                        int matchId = Convert.ToInt32(args[7]);
                        new XCupMatchHandler(xCupID, gainCode, clubA, clubB, round, category, matchId).start();
                    }
                    else
                    {
                        Console.WriteLine("param error while handle devcup match!!!!");
                    }
                }
                else if (args[0] == Constant.CHANGE_PLAYER_FROM_ARRANGE5_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 4)
                    {
                        long playerID = Convert.ToInt64(args[1]);
                        int clubID = Convert.ToInt32(args[2]);
                        int category = Convert.ToInt32(args[3]);
                        new ChangePlayerFromArrange5Handler(playerID, clubID, category).start();
                    }
                    else
                    {
                        Console.WriteLine("param error while change player from arrange5!!!!");
                    }
                }
                else if (args[0] == Constant.CLIENT_TYPE_CUP_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 7)
                    {
                        int cupID = Convert.ToInt32(args[1]);
                        string gainCode = args[2];
                        int clubA = Convert.ToInt32(args[3]);
                        int clubB = Convert.ToInt32(args[4]);
                        int round = Convert.ToInt32(args[5]);
                        int category = Convert.ToInt32(args[6]);
                        new CupMatchHandler(cupID, gainCode, clubA, clubB, round, category).start();
                    }
                    else
                    {
                        Console.WriteLine("param error while handle cup match!!!!");
                    }
                }
                else if (args[0] == Constant.CLIENT_TYPE_SEASON_UPDATE_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    if (args.Length == 2)
                    {
                        int step = Convert.ToInt32(args[1]);
                        new SeasonUpdateHandler(step).start();
                    }
                    else
                    {
                        Console.WriteLine("param error while handle cup match!!!!");
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

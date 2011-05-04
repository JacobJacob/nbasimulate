using System;
using System.Collections.Generic;
using System.Text;
using Client.XBA.Common;

namespace Client.XBA.Client
{
    class RunClass
    {

        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine(args[0]);
                LogManager.WriteLog("run", args[0]);
                if (args[0] == Constant.CLIENT_TYPE_MATCH_HANDLER)
                {
                    Console.WriteLine(String.Format("start to run {0}", args[0]));
                    new MatchHandler().start();
                }
            }
            Console.ReadLine();
            return;
        }

    }
}

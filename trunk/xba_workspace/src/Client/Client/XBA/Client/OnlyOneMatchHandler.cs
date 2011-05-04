using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Web.DBData;
using Web.VMatchEngine;
using Client.XBA.Common;

namespace Client.XBA.Client
{
    class OnlyOneMatchHandler
    {

        public void run()
        {

            DataTable matchs = BTPFriMatchManager.GetFriMatch(Constant.MATCH_TYPE_5);
            foreach(DataRow row in matchs.Rows)
            {
                int matchId =(int)row["FMatchID"];
                int clubIDA = (int)row["ClubIDA"];
                int clubIDB = (int)row["ClubIDB"];
                Console.WriteLine(matchId);
                Console.WriteLine(clubIDA);
                Console.WriteLine(clubIDB);
                Match match = new Match(clubIDA, clubIDB, true, Constant.MATCH_CATEGORY_ONLY_ONE, 1, false, false, 0, 0);
                match.Run();
                String reportUrl = match.sbRepURL.ToString();
                String statUrl = match.sbStasURL.ToString();
                int homeScore = match.GetHomeScore();
                int awayScore = match.GetAwayScore();
                Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
                BTPFriMatchManager.SetMatchEnd(matchId, Constant.MATCH_STATUS_FINISH, homeScore, awayScore, reportUrl, statUrl);
                BTPOnlyOneCenterReg.OnlyOneMatchEnd(matchId);
                
            }


        }

    }
}

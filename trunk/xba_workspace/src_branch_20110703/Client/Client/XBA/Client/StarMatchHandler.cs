using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Web.TMatchEngine;
using Client.XBA.Common;
using System.Collections;
using Web.Helper;

namespace Client.XBA.Client
{
    class StarMatchHandler:BaseClient
    {

        private int intSeason;

        public StarMatchHandler(int intSeason)
        {
            this.intSeason = intSeason;
        }

        protected override void run()
        {

            this.HandleMatch();

            this.go = false;
            
        }


        private void HandleMatch()
        {
            DataRow dataRow = BTPStarMatchManager.GetOneStarMatch(this.intSeason);
            if (dataRow == null)
            {
                Console.WriteLine("not star match for season:" + this.intSeason);
                return;
            }

            int intStarMatchID = Convert.ToInt32(dataRow["StarMatchID"]);

            int intClubA = 1;
            int intClubB = 2;
            Match match = new Match(intClubA, intClubB, this.intSeason);
            match.Run();
            String mvpPlyaer = match.pMVP.strName;
            long intMVPPlayerID = match.pMVP.longPlayerID;


            String reportUrl = match.sbRepURL.ToString();
            String statUrl = match.sbStasURL.ToString();
            int homeScore = match.GetHomeScore();
            int awayScore = match.GetAwayScore();
            Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);

            BTPStarMatchManager.UpdateStarMatchScore(intStarMatchID, homeScore, awayScore, reportUrl, statUrl, mvpPlyaer, intMVPPlayerID);
          
        }

    }
}

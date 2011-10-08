using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Web.VMatchEngine;
using Client.XBA.Common;
using System.Collections;
using Web.Helper;

namespace Client.XBA.Client
{
    class XCupMatchHandler:BaseClient
    {

        private int xCupID;
        private string gainCode;
        private int clubA;
        private int clubB;
        private int round;
        private int category;
        private int matchID;

        public XCupMatchHandler(int intXCupID, string strGainCode, int intClubA, int intClubB, int round, int category, int matchID)
        {
            this.xCupID = intXCupID;
            this.gainCode = strGainCode;
            this.clubA = intClubA;
            this.clubB = intClubB;
            this.round = round;
            this.category = category;
            this.matchID = matchID;
        }

        protected override void run()
        {

            this.HandleMatch();

            this.go = false;
            
        }


        private void HandleMatch()
        {
            //int matchId = BTPDevCupMatchManager.GetMaxDevCupMatchID();
            Match match = new Match(this.clubA, this.clubB, false, Constant.MATCH_CATEGORY_XCUP_MATCH, this.xCupID, false, false, 0, 0);
            match.Run();
            String reportUrl = match.sbRepURL.ToString();
            String statUrl = match.sbStasURL.ToString();
            int homeScore = match.GetHomeScore();
            int awayScore = match.GetAwayScore();
            Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
            if (this.category == 5)
            {
                BTPXCupMatchManager.AddMatch(this.xCupID, this.gainCode, this.clubA, this.clubB, homeScore, awayScore, reportUrl, statUrl);
                if (homeScore < awayScore)
                {
                    BTPXCupRegManager.SetClubDeadRound(this.xCupID, this.clubA, this.round);
                }
                else
                {
                    BTPXCupRegManager.SetClubDeadRound(this.xCupID, this.clubB, this.round);
                }
            }
            else
            {
                BTPXGroupMatchManager.UpdateResult(this.matchID, homeScore, awayScore, reportUrl, statUrl);

            }
        }

    }
}

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
    class DevCupMatchHandler:BaseClient
    {

        private int devCupID;
        private string gainCode;
        private int clubA;
        private int clubB;
        private int round;

        public DevCupMatchHandler(int intDevCupID, string strGainCode, int intClubA, int intClubB, int round)
        {
            this.devCupID = intDevCupID;
            this.gainCode = strGainCode;
            this.clubA = intClubA;
            this.clubB = intClubB;
            this.round = round;
        }

        protected override void run()
        {

            this.HandleMatch();

            this.go = false;
            
        }


        private void HandleMatch()
        {
            //int matchId = BTPDevCupMatchManager.GetMaxDevCupMatchID();
            Match match = new Match(this.clubA, this.clubB, false, Constant.MATCH_CATEGORY_DEVCUP_MATCH, this.devCupID, false, false, 0, 0);
            match.Run();
            String reportUrl = match.sbRepURL.ToString();
            String statUrl = match.sbStasURL.ToString();
            int homeScore = match.GetHomeScore();
            int awayScore = match.GetAwayScore();
            Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
            BTPDevCupMatchManager.AddMatch(devCupID, gainCode, this.clubA, this.clubB, homeScore, awayScore, reportUrl, statUrl);
            if (homeScore < awayScore)
            {
                BTPDevCupRegManager.SetClubDeadRound(this.devCupID, this.clubA, this.round);
            }
            else
            {
                BTPDevCupRegManager.SetClubDeadRound(this.devCupID, this.clubB, this.round);
            } 
        }

    }
}

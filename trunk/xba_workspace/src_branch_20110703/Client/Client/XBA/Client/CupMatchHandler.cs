using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Web.SMatchEngine;
using Client.XBA.Common;
using System.Collections;
using Web.Helper;

namespace Client.XBA.Client
{
    class CupMatchHandler:BaseClient
    {

        private int cupID;
        private string gainCode;
        private int clubA;
        private int clubB;
        private int round;
        private int category;

        public CupMatchHandler(int intCupID, string strGainCode, int intClubA, int intClubB, int round, int category)
        {
            this.cupID = intCupID;
            this.gainCode = strGainCode;
            this.clubA = intClubA;
            this.clubB = intClubB;
            this.round = round;
            this.category = category;
        }

        protected override void run()
        {

            this.HandleMatch();

            this.go = false;
            
        }


        private void HandleMatch()
        {

            bool blnPower = false;
            if(this.category == 2)
            {
                blnPower = true;
            }
            Match match = new Match(this.clubA, this.clubB, blnPower, this.category, this.cupID);
            match.Run();
            String reportUrl = match.sbRepURL.ToString();
            String statUrl = match.sbStasURL.ToString();
            int homeScore = match.GetHomeScore();
            int awayScore = match.GetAwayScore();
            Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
            BTPCupMatchManager.AddMatch(this.cupID, this.gainCode, this.clubA, this.clubB, homeScore, awayScore, reportUrl, statUrl);
            if (homeScore < awayScore)
            {
                BTPCupRegManager.SetClubDeadRound(this.cupID, this.clubA, this.round);
            }
            else
            {
                BTPCupRegManager.SetClubDeadRound(this.cupID, this.clubB, this.round);
            } 
        }

    }
}

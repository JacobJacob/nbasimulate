using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Web.SMatchEngine;
using Client.XBA.Common;

namespace Client.XBA.Client
{
    class Club3MatchHandler:BaseClient
    {
        protected override void run()
        {

            DataTable matchs = this.GetFriMatch();
            if(matchs!=null)
            {
                Console.WriteLine(String.Format("has {0} new matchs", matchs.Rows.Count));  
                foreach(DataRow row in matchs.Rows)
                {
                    int category = Convert.ToInt32(row["Category"]);
                    this.HandleMatch(row);
                }
            }
            else
            {
                Console.WriteLine("not matchs now, sleep 10s");
                Thread.Sleep(10*1000);
            }
            
        }

        private DataTable GetFriMatch()
        {
            while(true)
            {
                try
                {
                    return BTPFriMatchManager.GetFriMatch(Constant.MATCH_TYPE_3);
                }
                catch(Exception e)
                {
                   Console.WriteLine(e.Message);
                }
                Thread.Sleep(10*1000);
            }
        }

        private void HandleMatch(DataRow row)
        {
              int matchId =(int)row["FMatchID"];
              int clubIDA = (int)row["ClubIDA"];
              int clubIDB = (int)row["ClubIDB"];
              int category = Convert.ToInt32(row["Category"]);
              Console.WriteLine(matchId);
              Console.WriteLine(clubIDA);
              Console.WriteLine(clubIDB);
              Match match = new Match(clubIDA, clubIDB, true, category, matchId);
              match.Run();
              String reportUrl = match.sbRepURL.ToString();
              String statUrl = match.sbStasURL.ToString();
              int homeScore = match.GetHomeScore();
              int awayScore = match.GetAwayScore();
              Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
              BTPFriMatchManager.SetMatchEnd(matchId, Constant.MATCH_STATUS_FINISH, homeScore, awayScore, reportUrl, statUrl);
             
        }

    }
}

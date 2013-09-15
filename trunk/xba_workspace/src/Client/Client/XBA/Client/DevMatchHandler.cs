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
    class DevMatchHandler:BaseClient
    {

        private int matchId;

        public DevMatchHandler(int matchId)
        {
            this.matchId = matchId;
        }

        protected override void run()
        {

            DataRow match = this.GetDevMatch();

            if(match != null)
            {
                this.HandleMatch(match);
            }

            this.go = false;
            
        }

        private DataRow GetDevMatch()
        {
            while(true)
            {
                try
                {
                    return BTPDevMatchManager.GetDevMRowByDevMatchID(this.matchId);
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

              int homeClubHScore = (int)row["ClubHScore"];
              int homeClubAScore = (int)row["ClubAScore"];
              if (homeClubHScore > 0 || homeClubAScore > 0)
              {
                  Console.WriteLine("match has finish, return");
                  return; 
              }
              int matchId = (int)row["DevMatchID"];
              int clubIDA = (int)row["ClubHID"];
              int clubIDB = (int)row["ClubAID"];
              Console.WriteLine(string.Format("MatchID:{0}, Home Club ID:{1}, Away Club ID:{2}",matchId, clubIDA, clubIDB));
              Match match = new Match(clubIDA, clubIDB, true, Constant.MATCH_CATEGORY_DEV_MATCH, matchId, false, false, 0, 0);
              match.Run();
              String reportUrl = match.sbRepURL.ToString();
              String statUrl = match.sbStasURL.ToString();
              int homeScore = match.GetHomeScore();
              int awayScore = match.GetAwayScore();
              Console.WriteLine("home score:" + homeScore + ":away score" + awayScore);
              BTPFriMatchManager.SetMatchEnd(matchId, Constant.MATCH_STATUS_FINISH, homeScore, awayScore, reportUrl, statUrl);
              BTPDevMatchManager.UpdateScore(matchId, homeScore, awayScore, reportUrl, statUrl);

              DataRow homeClub = BTPClubManager.GetClubRowByID(clubIDA);
              DataRow awayClub = BTPClubManager.GetClubRowByID(clubIDB);

              Hashtable info = new Hashtable();
              info.Add("ScoreH", homeScore);
              info.Add("ScoreA", awayScore);
              info.Add("ClubNameH", homeClub["Name"]);
              info.Add("ClubNameA", awayClub["Name"]);
              info.Add("ClubLogoH", homeClub["Logo"]);
              info.Add("ClubLogoA", awayClub["Logo"]);

              string mvpPlayer = match.pMVP.strName;

              if (mvpPlayer != null)
              {
                  mvpPlayer = mvpPlayer.Replace("&lt;u&gt;", "");
                  mvpPlayer = mvpPlayer.Replace("&lt;/u&gt;", "");
              }

              info.Add("MVPName", mvpPlayer);
              info.Add("MVPStas", string.Format("{0}|{1}|{2}|{3}|{4}", match.pMVP.intScore, match.pMVP.intOReb + match.pMVP.intDReb, match.pMVP.intAst, match.pMVP.intStl, match.pMVP.intBlk));

              /*更新主队的MainXML*/
              string homeOldXml = null;
              if (homeClub["MainXML"] != DBNull.Value)
              {
                  homeOldXml = (string)homeClub["MainXML"];
              }
              string homeNewXml = MainXmlHelper.GetNewMainXml(homeOldXml, info);
              BTPClubManager.SetMainXMLByClubID(clubIDA, homeNewXml);
              /*更新客队的MainXML*/
              string awayOldXml = null;
              if (awayClub["MainXML"] != DBNull.Value)
              {
                  awayOldXml = (string)awayClub["MainXML"];
              }
              string awayNewXml = MainXmlHelper.GetNewMainXml(awayOldXml, info);
              BTPClubManager.SetMainXMLByClubID(clubIDB, awayNewXml);

              /*更新联赛胜负场次*/
              int homeWin = 0;
              int homeLose = 0;
              int awayWin = 0;
              int awayLose = 0;
              int diff = 0;
              if (homeScore > awayScore)
              {
                  homeWin = 1;
                  awayLose = 1;
                  diff = homeScore - awayScore;
              }
              else
              {
                  homeLose = 1;
                  awayWin = 1;
                  diff = awayScore - homeScore;
              }
              /*主队*/
              BTPDevManager.UpdateResult(clubIDA, homeWin, homeLose, diff);
              /*客队*/
              BTPDevManager.UpdateResult(clubIDB, awayWin, awayLose, diff);

              BTPClubManager.ChangeReputation(clubIDA, clubIDB, homeScore, awayScore);
        }

    }
}

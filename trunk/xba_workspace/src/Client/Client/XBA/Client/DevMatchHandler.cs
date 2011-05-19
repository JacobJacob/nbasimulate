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
              int matchId = (int)row["DevMatchID"];
              int clubIDA = (int)row["ClubHID"];
              int clubIDB = (int)row["ClubAID"];
              Console.WriteLine(matchId);
              Console.WriteLine(clubIDA);
              Console.WriteLine(clubIDB);
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
              info.Add("MVPName", match.pMVP.strName);
              info.Add("MVPStas", string.Format("{0}|{1}|{2}|{3}|{4}", match.pMVP.intScore, match.pMVP.intRebound, match.pMVP.intAst, match.pMVP.intBlock, match.pMVP.intSteal));

              /*更新主队的MainXML*/
              string homeOldXml = (string)homeClub["MainXML"];
              string homeNewXml = MainXmlHelper.GetNewMainXml(homeOldXml, info);
              BTPClubManager.SetMainXMLByClubID(clubIDA, homeNewXml);
              /*更新客队的MainXML*/
              string awayOldXml = (string)awayClub["MainXML"];
              string awayNewXml = MainXmlHelper.GetNewMainXml(awayOldXml, info);
              BTPClubManager.SetMainXMLByClubID(clubIDB, awayNewXml);
        }

    }
}

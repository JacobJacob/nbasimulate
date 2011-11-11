namespace Web.TMatchEngine
{
    using System;
    using System.Collections;
    using System.Data;
    using Web.DBData;
    using Web.Helper;

    public class Team
    {
        public DateTime dtActiveTime;
        public int intAChange;
        public int intClubID;
        public int intLUse = 1;
        public int intScore;
        public int intUserID;
        public int intWUse = 1;
        public Hashtable players;
        public string strClubName;

        public Team(int intClubID, bool blnIsHome)
        {
            this.intClubID = intClubID;
            this.intScore = 0;
            this.intAChange = 0;
            //DataRow clubRowByClubID = BTPClubManager.GetClubRowByClubID(this.intClubID);
            //this.strClubName = StringItem.GetXMLTrueBody(clubRowByClubID["ClubName"].ToString().Trim());
            //this.intUserID = (int) clubRowByClubID["UserID"];
            //this.dtActiveTime = (DateTime) clubRowByClubID["ActiveTime"];
            this.dtActiveTime = DateTime.Now;
            this.intUserID = 0;
            this.players = new Hashtable();
            DataTable rows = BTPStarMatchManager.GetStarPlayersByClubID(this.intClubID);
            foreach (DataRow row2 in rows.Rows)
            {
                string key = row2["PlayerID"].ToString();
                long longPlayerID = Convert.ToInt64(key);
                DataRow playerRow = BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerID);
                Player player = new Player(playerRow, blnIsHome);
                this.players.Add(key, player);
            }
        }
    }
}


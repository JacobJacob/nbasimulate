namespace Web.TMatchEngine
{
    using System;
    using System.Collections;
    using System.Data;
    using Web.DBData;

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
            this.dtActiveTime = DateTime.Now;
            this.intUserID = 0;
            this.players = new Hashtable();
            foreach (DataRow row in BTPStarMatchManager.GetStarPlayersByClubID(this.intClubID).Rows)
            {
                string str = row["PlayerID"].ToString();
                long longPlayerID = Convert.ToInt64(str);
                DataRow playerRowByPlayerID = BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerID);
                if (playerRowByPlayerID == null)
                {
                    Console.WriteLine("player not exists[" + longPlayerID + "]");
                }
                Player player = new Player(playerRowByPlayerID, blnIsHome);
                this.players.Add(str, player);
            }
        }
    }
}


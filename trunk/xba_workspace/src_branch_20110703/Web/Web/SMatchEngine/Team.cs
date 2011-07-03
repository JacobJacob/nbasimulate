namespace Web.SMatchEngine
{
    using System;
    using System.Collections;
    using System.Data;
    using Web.DBData;
    using Web.Helper;

    public class Team
    {
        public int intAChange;
        public int intClubID;
        public int intScore;
        public int intUserID;
        public Hashtable players;
        public string strClubName;

        public Team(int intClubID, bool blnIsHome)
        {
            this.intClubID = intClubID;
            this.intScore = 0;
            this.intAChange = 0;
            DataRow clubRowByID = BTPClubManager.GetClubRowByID(this.intClubID);
            this.strClubName = StringItem.GetXMLTrueBody(clubRowByID["Name"].ToString().Trim());
            this.intUserID = (int) clubRowByID["UserID"];
            this.players = new Hashtable();
            foreach (DataRow row2 in BTPPlayer3Manager.GetPlayerTableByClubID(this.intClubID).Rows)
            {
                string key = row2["PlayerID"].ToString();
                Player player = new Player(row2, blnIsHome);
                this.players.Add(key, player);
            }
        }
    }
}


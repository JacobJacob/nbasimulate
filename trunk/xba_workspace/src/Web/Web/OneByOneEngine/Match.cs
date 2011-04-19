namespace Web.OneByOneEngine
{
    using System;
    using System.Text;
    using Web.DBData;

    public class Match
    {
        public Player pA;
        public Player pH;
        public StringBuilder sbPlayer = new StringBuilder();
        public StringBuilder sbScript = new StringBuilder();

        public Match(long longPlayerIDH, long longPlayerIDA)
        {
            Player player = new Player(BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerIDH), true);
            Player player2 = new Player(BTPPlayer5Manager.GetPlayerRowByPlayerID(longPlayerIDA), false);
            this.pH = player;
            this.pA = player2;
        }

        public void Finished()
        {
        }

        public int GetAwayScore()
        {
            return this.pH.intScore;
        }

        public int GetHomeScore()
        {
            return this.pH.intScore;
        }

        public void Run()
        {
            Quarter quarter = new Quarter(this.pH, this.pA);
            quarter.Run();
            this.sbPlayer.Append(quarter.sbPlayer.ToString());
            this.sbScript.Append(quarter.sbScript.ToString());
            this.pH = quarter.pH;
            this.pA = quarter.pA;
        }
    }
}


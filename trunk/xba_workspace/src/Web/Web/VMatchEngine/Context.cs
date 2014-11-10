namespace Web.VMatchEngine
{
    using System;

    public class Context
    {
        private Web.VMatchEngine.MatchTotal matchTotal;

        public Context(int type, int matchId)
        {
            this.matchTotal = new Web.VMatchEngine.MatchTotal(type, matchId);
        }

        public Web.VMatchEngine.MatchTotal MatchTotal
        {
            get
            {
                return this.matchTotal;
            }
        }
    }
}


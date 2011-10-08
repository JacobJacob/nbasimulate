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
    class ChangePlayerFromArrange5Handler:BaseClient
    {

        private long playerID;
        private int clubID;
        private int category;

        public ChangePlayerFromArrange5Handler(long playerId, int clubID, int category)
        {
            this.playerID = playerId;
            this.clubID = clubID;
            this.category = category;
        }

        protected override void run()
        {
            if (this.category == 3)
            {
                PlayerItem.ChangePlayerFromArrange3(this.playerID, this.clubID, true);
            }
            else
            {
                PlayerItem.ChangePlayerFromArrange5(this.playerID, this.clubID, true);
            }
            this.go = false;
            
        }

    }
}

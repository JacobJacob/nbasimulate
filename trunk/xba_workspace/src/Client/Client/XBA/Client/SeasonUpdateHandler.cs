using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using Web.DBData;
using Client.XBA.Common;

/* 赛季更新客户端 */

namespace Client.XBA.Client
{
    class SeasonUpdateHandler:BaseClient
    {
        private int step;

        public SeasonUpdateHandler(int step)
        {
            this.step = step;
        }

        public SeasonUpdateHandler()
        {
            this.step = 1;
        }

        private void BeforeRun()
        {
         

        }

        protected override void run()
        {
            if (this.step == 1)
            {
                BTPPlayer5Manager.Player5RetireMsg();
                BTPPlayer3Manager.Player3RetireMsg();
            }
            else
            {

            }

            this.go = false;
          
        }

    }
}

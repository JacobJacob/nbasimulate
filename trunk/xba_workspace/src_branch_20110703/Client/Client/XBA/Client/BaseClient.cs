using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Client.XBA.Client
{
    abstract class BaseClient
    {
        protected bool go = true;

        abstract protected void run();


        public void start()
        {
            while (go)
            {
                this.run();
            }

        }

    }
}

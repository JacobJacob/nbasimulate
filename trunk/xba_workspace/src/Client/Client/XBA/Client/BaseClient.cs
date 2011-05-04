using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Client.XBA.Client
{
    abstract class BaseClient
    {
        abstract protected void run();


        public void start()
        {
            while (true)
            {
                this.run();
            }

        }

    }
}

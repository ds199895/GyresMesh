using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hsy.Core
{
    public abstract class HS_Thread
    {
        Thread thread=null;
        public HS_Thread()
        {
            start();
        }
        public abstract void run();

        public virtual void start()
        {
            if (thread == null)
                thread = new Thread(run);
            thread.Start();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Modules
{
    internal class RegistrationModule
    {
        public List<ParamForSend> buffer;
        private int sendInterval;
        private Timer timer;

        private bool active;
        public bool Active
        {
            get { return active; }
        }

        public void Start()
        {
            active = true;
        }

        public void Stop()
        {
            active = false;
        }

        public RegistrationModule()
        {
            buffer = new List<ParamForSend>();
        }

        public void SetSendInterval(int interval)
        {
            sendInterval = interval;
        }
    }
}

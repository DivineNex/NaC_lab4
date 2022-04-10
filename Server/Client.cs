using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal struct Client
    {
        public string ip_port;
        public eClientType type;
        public Thread socketThread;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal struct Client
    {
        public Socket socket;
        public string ip_port;
        public Thread socketThread;

        private eClientType type;

        public eClientType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal enum eMessageType
    {
        not_assigned,
        initMessage,
        initParamsMessage,
        paramsMessage,
        auth_message,
        registration_message
    }
}

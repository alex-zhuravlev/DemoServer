using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using DemoServer.Core;

namespace DemoServer
{
    public class ActionFactory : enFactory<Action, string>
    {
        public ActionFactory()
        {
            Register("ClientMessageMove", typeof(ClientActionMove));
            Register("ClientMessageDealDamage", typeof(ClientActionDealDamage));
            Register("ClientMessageHeal", typeof(ClientActionHeal));
        }
    }
}

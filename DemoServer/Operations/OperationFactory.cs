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
    public class OperationFactory : enFactory<Action, string>
    {
        public OperationFactory()
        {
            Register("OperationDealDamage", typeof(OperationDealDamage));
            Register("OperationHeal", typeof(OperationHeal));
            Register("OperationMove", typeof(OperationMove));
        }
    }
}

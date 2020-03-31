using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Xml;

namespace DemoServer
{
    public class Action
    {
        public GameClient GameClient = null;

        public virtual bool Init(XmlDocument oXml) // Here should be specific data format (System Xml is slow under high load. Only as example)
        {
            return true;
        }

        public virtual void Execute()
        {

        }
    }
}

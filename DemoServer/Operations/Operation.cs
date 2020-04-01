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
    public class Operation
    {
        public GameRoom GameRoom { get; set; }

        public virtual void Execute(GameRoom.GameStatus oGameStatus)
        {

        }
    }
}

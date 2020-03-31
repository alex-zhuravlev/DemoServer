using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DemoServer.Core;
using System.Drawing;
using System.Xml;

namespace DemoServer
{
    public class Player
    {
        public ulong Id { get; private set; }

        public Point Position { get; set; } = new Point(0, 0);
        public int Health { get; set; } = 100;

        private Player() { }
        public Player(ulong iId)
        {
            Id = iId;
        }
    }
}

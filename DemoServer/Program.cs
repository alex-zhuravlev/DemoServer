using System;

namespace DemoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer oGameServer = new GameServer();
            oGameServer.Start();
        }       
    }
}

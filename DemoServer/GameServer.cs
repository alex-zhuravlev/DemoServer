using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace DemoServer
{
    public class GameServer
    {
        private TCPListener m_oListener = null;
        private GameRoom m_oGameRoom = new GameRoom(); // Yet single room per game server process
        private bool m_bStop = false;

        public bool Init()
        {
            m_oListener = new TCPListener();
            if (!m_oListener.Init())
                return false;

            m_oGameRoom = new GameRoom();
            if (!m_oGameRoom.Init())
                return false;

            return true;
        }

        public void Start()
        {
            m_bStop = false;
            m_oListener.Start();

            HandleClients();
        }

        public void Stop()
        {
            m_bStop = true;
            m_oListener.Stop();
        }

        private void HandleClients()
        {
            while (!m_bStop)
            {
                List<TcpClient> aNewTcpClients = m_oListener.GrabNewClients();
                if (aNewTcpClients != null)
                {
                    foreach (TcpClient oTcpClient in aNewTcpClients)
                    {
                        GameClient oGameClient = new GameClient(oTcpClient);
                        m_oGameRoom.AddClient(oGameClient);
                    }
                }
            }
        }
    }
}

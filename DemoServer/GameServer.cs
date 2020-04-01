using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DemoServer
{
    public class GameServer
    {
        private struct tClientRecord
        {
            public GameClient m_oClient;
            public Thread m_hClientThread;
            public ulong m_iClientId;
        }

        private TCPListener m_oListener = null;
        private List<tClientRecord> m_aClientRecords = new List<tClientRecord>();
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
                // Add new clients
                List<TcpClient> aNewTcpClients = m_oListener.GrabNewClients();
                if (aNewTcpClients != null)
                {
                    foreach (TcpClient oTcpClient in aNewTcpClients)
                    {
                        GameClient oGameClient = new GameClient(oTcpClient);
                        oGameClient.GameRoom = m_oGameRoom;
                        m_oGameRoom.AddPlayer(oGameClient.Id);

                        Console.WriteLine(String.Format("GameServer: Added new Player[PlayerID={0}]", oGameClient.Id));

                        tClientRecord oRecord = new tClientRecord();
                        oRecord.m_oClient = oGameClient;
                        oRecord.m_hClientThread = new Thread(oGameClient.Work);
                        oRecord.m_iClientId = oGameClient.Id;
                        m_aClientRecords.Add(oRecord);

                        oRecord.m_hClientThread.Start();
                    }
                }

                // Process existing clients
                for (int i = m_aClientRecords.Count - 1; i >= 0; i--)
                {
                    // Remove client record when client's thread has finished
                    if (!m_aClientRecords[i].m_hClientThread.IsAlive)
                    {
                        ulong iId = m_aClientRecords[i].m_iClientId;
                        m_oGameRoom.RemovePlayer(iId);
                        m_aClientRecords.RemoveAt(i);

                        Console.WriteLine(String.Format("GameServer: Removed Player[PlayerID={0}]", iId));
                    }
                }

                // Process game room
                m_oGameRoom.Tick();

                Thread.Sleep(10); // TMP. Save CPU
            }

            // Finish remaining clients
            if (m_aClientRecords.Count > 0)
            {
                foreach (tClientRecord oRecord in m_aClientRecords)
                {
                    oRecord.m_oClient.FinishClient();
                }

                // Wait until clients finish
                foreach (tClientRecord oRecord in m_aClientRecords)
                {
                    oRecord.m_hClientThread.Join();
                }
            }
        }
    }
}

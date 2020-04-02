using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml;

namespace DemoServer
{
    public class GameRoom
    {
        public class GameStatus
        {
            public List<Player> Players = new List<Player>();
            public List<List<ulong>> Groups = new List<List<ulong>>(); // List of player Ids. TODO.
        }

        private GameStatus m_oGameStatus = new GameStatus();
        private ConcurrentQueue<Operation> m_aOperations = new ConcurrentQueue<Operation>();

        public bool Init()
        {
            // Load initial world state and game configs from cache or db

            return true;
        }

        public void Tick()
        {
            lock (m_oGameStatus)
            {
                Operation oOperation = null;
                while (m_aOperations.TryDequeue(out oOperation))
                {
                    oOperation.Execute(m_oGameStatus);
                }
            }
        }

        public bool AddPlayer(ulong iId)
        {
            lock (m_oGameStatus)
            {
                if (m_oGameStatus.Players.Find(item => item.Id == iId) != null)
                    return false; // Check for duplicates

                m_oGameStatus.Players.Add(new Player(iId));
            }
            return true;
        }

        public bool RemovePlayer(ulong iId)
        {
            lock (m_oGameStatus)
            {
                int iIndex = m_oGameStatus.Players.FindIndex(item => item.Id == iId);
                if (iIndex == -1)
                    return false;

                m_oGameStatus.Players.RemoveAt(iIndex);
            }
            return true;
        }

        public void AddOperation(Operation o)
        {
            o.GameRoom = this;
            m_aOperations.Enqueue(o);
        }

        public XmlDocument SaveGameStatusForClient(ulong iClientId)
        {
            lock (m_oGameStatus)
            {
                Thread.Sleep(10); // Synthetic delay

                // TODO. Build up game data relative to iClientId (also limit by client's viewport)
                return new XmlDocument();
            }
        }
    }
}

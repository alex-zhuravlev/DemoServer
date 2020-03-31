using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DemoServer
{
    public class GameRoom
    {
        public class GameStatus
        {
            public List<Player> Players = new List<Player>();
            public List<List<ulong>> Groups = new List<List<ulong>>(); // List of player Ids
        }

        private List<GameClient> m_aClients = new List<GameClient>();
        private GameStatus m_oGameStatus = new GameStatus();
        private ConcurrentQueue<Operation> m_aOperations = new ConcurrentQueue<Operation>();

        public bool Init()
        {
            // Load initial world state and game configs from cache or db

            return true;
        }

        public void Tick()
        {
            for (int i = m_aClients.Count - 1; i >= 0; i--)
            {
                if(m_aClients[i].CanBeDeleted)
                {
                    ulong iId = m_aClients[i].Id;
                    int iIndex = m_oGameStatus.Players.FindIndex(item => item.Id == iId);
                    m_oGameStatus.Players.RemoveAt(iIndex);

                    m_aClients.RemoveAt(i);
                } 
            }
        }

        public bool AddClient(GameClient oClient)
        {
            oClient.GameRoom = this;
            m_aClients.Add(oClient);
            m_oGameStatus.Players.Add(new Player(oClient.Id));
            return true;
        }

        public void AddOperation(Operation o)
        {
            o.GameStatus = m_oGameStatus;
            m_aOperations.Enqueue(o);
        }

        public void DealDamageToPlayer(ulong iSourcePlayer, ulong iTargetPlayer, int iValue)
        {
            int iIndex = m_oGameStatus.Players.FindIndex(item => item.Id == iTargetPlayer);
            if(iIndex != -1)
            {
                m_oGameStatus.Players[iIndex].Health -= iValue;
            }
        }

        public XmlDocument SaveForClient(ulong iClientId)
        {
            return new XmlDocument();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DemoServer
{
    public class GameRoom
    {
        List<GameClient> m_aClients = new List<GameClient>();

        List<Player> m_aPlayers = new List<Player>();

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
                    int iIndex = m_aPlayers.FindIndex(item => item.Id == iId);
                    m_aPlayers.RemoveAt(iIndex);

                    m_aClients.RemoveAt(i);
                } 
            }
        }

        public bool AddClient(GameClient oClient)
        {
            oClient.GameRoom = this;
            m_aClients.Add(oClient);
            m_aPlayers.Add(new Player(oClient.Id));
            return true;
        }

        public void DealDamageToClient(ulong iSource, ulong iTarget, int iValue)
        {
            int iIndex = m_aPlayers.FindIndex(item => item.Id == iTarget);
            if(iIndex != -1)
            {
                m_aPlayers[iIndex].Health -= iValue;
            }
        }

        public XmlDocument SaveForClient(ulong iClientId)
        {
            return new XmlDocument();
        }
    }
}

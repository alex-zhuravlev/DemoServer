using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Xml;
using DemoServer.Core;

namespace DemoServer
{
    public class ClientActionDealDamage : Action
    {
        private ulong m_iOpponentId;
        private int m_iDamageValue;

        public override bool Init(XmlDocument oXml)
        {
            m_iOpponentId = (ulong)enRandom.Get(Conf.MAX_CLIENTS_PER_ROOM); // TMP.
            m_iDamageValue = enRandom.Get(1, 25); // TMP.
            return true;
        }

        public override void Execute()
        {
            GameClient.GameRoom.DealDamageToClient(GameClient.Id, m_iOpponentId, m_iDamageValue);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Drawing;
using System.Xml;
using DemoServer.Core;

namespace DemoServer
{
    public class ClientActionMove : Action
    {
        private ulong m_iPlayerId = 0L;
        private Point m_p2Offset = new Point();

        public override bool Init(XmlDocument oXml)
        {
            m_iPlayerId = (ulong)enRandom.Get(Conf.MAX_CLIENTS_PER_ROOM); // TMP.
            m_p2Offset.X = enRandom.Get(0, 10); // TMP.
            m_p2Offset.Y = enRandom.Get(0, 10); // TMP.
            return true;
        }

        public override void Execute()
        {
            Operation oMove = new OperationMove(m_iPlayerId, m_p2Offset);
            GameClient.GameRoom.AddOperation(oMove);
        }
    }
}

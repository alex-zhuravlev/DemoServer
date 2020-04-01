using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;
using System.Xml;
using DemoServer.Core;
using System.Drawing;

namespace DemoServer
{
    public class OperationRespawn : Operation
    {
        private ulong m_iPlayerId = 0L;
        private Point m_p2Offset;

        private OperationRespawn() { }

        public OperationRespawn(ulong iPlayerId)
        {
            m_iPlayerId = iPlayerId;
        }

        public override void Execute(GameRoom.GameStatus oGameStatus)
        {
            int iIndex = oGameStatus.Players.FindIndex(item => item.Id == m_iPlayerId);
            if (iIndex != -1)
            {
                Point p2Pos = new Point();
                p2Pos.X = enRandom.Get(0, 1000); // TMP.
                p2Pos.Y = enRandom.Get(0, 1000); // TMP.

                oGameStatus.Players[iIndex].Health = 100;
                oGameStatus.Players[iIndex].Position = p2Pos; // Respawn at random position.

                // TODO. Discard all operations applied to this iPlayerId but in previous life
                // TODO. Respond to client
            }
        }
    }
}

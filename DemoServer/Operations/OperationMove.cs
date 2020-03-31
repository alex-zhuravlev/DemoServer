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
    public class OperationMove : Operation
    {
        private ulong m_iPlayerId = 0L;
        private Point m_p2Offset;

        private OperationMove() { }

        public OperationMove(ulong iPlayerId, Point p2Offset)
        {
            m_iPlayerId = iPlayerId;
            m_p2Offset = p2Offset;
        }

        public override void Execute()
        {
            int iIndex = GameStatus.Players.FindIndex(item => item.Id == m_iPlayerId);
            if (iIndex != -1)
            {
                Point p2Pos = GameStatus.Players[iIndex].Position;
                p2Pos.X += m_p2Offset.X;
                p2Pos.Y += m_p2Offset.Y;
                GameStatus.Players[iIndex].Position = p2Pos;
            }
        }
    }
}

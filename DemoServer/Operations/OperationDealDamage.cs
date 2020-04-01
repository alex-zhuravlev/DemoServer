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
    public class OperationDealDamage : Operation
    {
        private ulong m_iSourcePlayerId = 0L;
        private ulong m_iTargetPlayerId = 0L;
        private int m_iDamageValue = 0;

        private OperationDealDamage() { }

        public OperationDealDamage(ulong iSourcePlayer, ulong iTargetPlayer, int iDamageValue)
        {
            m_iSourcePlayerId = iSourcePlayer;
            m_iTargetPlayerId = iTargetPlayer;
            m_iDamageValue = iDamageValue;
        }

        public override void Execute(GameRoom.GameStatus oGameStatus)
        {
            int iIndex = oGameStatus.Players.FindIndex(item => item.Id == m_iTargetPlayerId);
            if (iIndex != -1)
            {
                oGameStatus.Players[iIndex].Health -= m_iDamageValue;
                if(oGameStatus.Players[iIndex].Health <= 0)
                {
                    GameRoom.AddOperation(new OperationRespawn(m_iTargetPlayerId));
                }
            }
        }
    }
}

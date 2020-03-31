using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

namespace DemoServer
{
    public class OperationHeal : Operation
    {
        private ulong m_iSourcePlayerId = 0L;
        private ulong m_iTargetPlayerId = 0L;
        private int m_iHealValue = 0;

        private OperationHeal() { }

        public OperationHeal(ulong iSourcePlayer, ulong iTargetPlayer, int iHealValue)
        {
            m_iSourcePlayerId = iSourcePlayer;
            m_iTargetPlayerId = iTargetPlayer;
            m_iHealValue = iHealValue;
        }

        public override void Execute()
        {
            int iIndex = GameStatus.Players.FindIndex(item => item.Id == m_iTargetPlayerId);
            if (iIndex != -1)
            {
                GameStatus.Players[iIndex].Health += m_iHealValue;
            }
        }
    }
}

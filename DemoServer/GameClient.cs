using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DemoServer.Core;
using System.Drawing;
using System.Xml;
using System.Threading;

namespace DemoServer
{
    public class GameClient : TCPMessenger
    {
        public GameRoom GameRoom = null;

        public ulong Id { get; private set; }

        public bool CanBeDeleted { get; private set; } = false;

        private bool m_bFinishRequest = false;

        private static ActionFactory s_oActionFactory = new ActionFactory();

        public GameClient(TcpClient oTcpClient) : base(oTcpClient)
        {
            Id = enUnique.GenerateId();
        }

        public void FinishClient()
        {
            m_bFinishRequest = true;
        }

        public void Work()
        {
            enTimer oGameStatusTimer = new enTimer();

            while (!(CanBeFinished() || m_bFinishRequest))
            {
                // Handle TCP operations
                ReadWrite();

                // Handle protocol messages
                XmlDocument oMessage = GrabMessage();
                if (oMessage != null)
                {
                    string sActionName = String.Empty;
                    if (!enMessage.GetActionName(oMessage, ref sActionName))
                    {
                        Console.WriteLine("Failed to get action name");
                        SendErrorMessage("Invalid message");
                        break;
                    }

                    Action oAction = s_oActionFactory.CreateObject(sActionName);
                    if (oAction == null)
                    {
                        Console.WriteLine(String.Format("Failed to create action[MessageName={0}]", sActionName));
                        SendErrorMessage("Invalid message");
                        break;
                    }

                    oAction.GameClient = this;
                    oAction.Execute();
                }

                // Send game status by timeout
                const double fTimeout = 50.0f;
                if (!oGameStatusTimer.IsWorking)
                {
                    oGameStatusTimer.StartTimer(fTimeout);
                }
                else if (oGameStatusTimer.IsElapsed())
                {
                    oGameStatusTimer.RestartTimer(fTimeout);

                    XmlDocument oGameStatus = GameRoom.SaveGameStatusForClient(Id);
                    XmlDocument oGameStatusMessage = enMessage.CreateMessage("ServerActionUpdateGameStatus", oGameStatus);
                    AddOutgoingMessage(oGameStatusMessage);
                }

                Thread.Sleep(10); // TMP. Save CPU
            }

            // TODO. Send final message if m_bFinishRequest

            CanBeDeleted = true;
        }
    }
}

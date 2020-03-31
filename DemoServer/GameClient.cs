using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using DemoServer.Core;
using System.Drawing;
using System.Xml;

namespace DemoServer
{
    public class GameClient : TCPMessenger
    {
        public GameRoom GameRoom = null;

        public ulong Id { get; private set; }

        public bool CanBeDeleted { get; private set; } = false;

        private static ActionFactory m_oMessageFactory = new ActionFactory();

        public GameClient(TcpClient oTcpClient) : base(oTcpClient)
        {
            Id = enUnique.GenerateId();
        }

        public void Work()
        {
            while(!CanBeFinished())
            {
                ReadWrite();

                XmlDocument oMessage = GrabMessage();
                if (oMessage != null)
                {
                    string sActionName = String.Empty;
                    if (!enMessage.GetActionName(oMessage, ref sActionName))
                    {
                        SendErrorMessage("Got invalid message");
                        break;
                    }

                    Action oAction = m_oMessageFactory.CreateObject(sActionName);
                    if(oAction == null)
                    {
                        SendErrorMessage("Got invalid message");
                        break;
                    }

                    oAction.GameClient = this;
                    oAction.Execute();
                }
            }

            CanBeDeleted = true;
        }
    }
}

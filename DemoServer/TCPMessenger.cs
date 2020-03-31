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
    public class TCPMessenger
    {
        private TcpClient m_oTcpClient = null;
        private List<XmlDocument> m_aInboundMessages = new List<XmlDocument>();
        private List<XmlDocument> m_aOutgoingMessages = new List<XmlDocument>();

        public TCPMessenger(TcpClient oTcpClient)
        {
            m_oTcpClient = oTcpClient;
        }

        protected void ReadWrite()
        {
            // Read()
            // Write();
        }

        protected void Read()
        {
            // Read to m_aInboundMessages via TcpClient
        }

        protected void Write()
        {
            // Send m_aOutboundMessages via TcpClient
        }

        protected void AddOutgoingMessage(XmlDocument oMessage)
        {
            m_aOutgoingMessages.Add(oMessage);
        }

        protected XmlDocument GrabMessage()
        {
            if(m_aInboundMessages.Count > 0)
            {
                XmlDocument oMessage = m_aInboundMessages[0];
                m_aInboundMessages.RemoveAt(0);
                return oMessage;
            }
            return null;
        }

        protected void SendErrorMessage(string sErrorMessage)
        {
            m_aOutgoingMessages.Clear();

            XmlDocument oMessage = enMessage.CreateErrorMessage(sErrorMessage);
            m_aOutgoingMessages.Add(oMessage);

            // TODO. Auto-close socket after send
        }

        protected bool CanBeFinished()
        {
            return m_oTcpClient.Client.Connected;
        }
    }
}

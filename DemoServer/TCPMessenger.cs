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
    public class TCPMessenger // TODO! Must be reworked in non-blocking manner
    {
        private TcpClient m_oTcpClient = null;
        private NetworkStream m_oStream = null;
        private List<XmlDocument> m_aInboundMessages = new List<XmlDocument>();
        private List<XmlDocument> m_aOutgoingMessages = new List<XmlDocument>();

        public TCPMessenger(TcpClient oTcpClient)
        {
            m_oTcpClient = oTcpClient;
            m_oStream = oTcpClient.GetStream();
        }

        protected void ReadWrite()
        {
            Read();
            Write();
        }

        protected void Read()
        {
            // Read to m_aInboundMessages via TcpClient

            if (m_oStream.DataAvailable)
            {
                byte[] aReadBuffer = new byte[1024];
                int iNumfBytesRead = 0;

                iNumfBytesRead = m_oStream.Read(aReadBuffer, 0, aReadBuffer.Length);

                // Console.WriteLine(String.Format("TCPMessenger: Has read {0} bytes", iNumfBytesRead));

                // TMP. Assume that we have extracted full message
                m_aInboundMessages.Add(new XmlDocument());
            }
        }

        protected void Write()
        {
            // Send m_aOutboundMessages via TcpClient

            if (m_aOutgoingMessages.Count == 0)
                return;

            if (m_oStream.CanWrite)
            {
                byte[] aWriteBuffer = Encoding.UTF8.GetBytes("Test Write");
                try
                {
                    m_oStream.Write(aWriteBuffer, 0, aWriteBuffer.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("TCPMessenger: Write exception: {0}", ex.Message));
                    m_oTcpClient.Close();
                }

                // Console.WriteLine(String.Format("TCPMessenger: Has sent {0} bytes", aWriteBuffer.Length));
            }

            m_aOutgoingMessages.Clear();
        }

        protected void AddOutgoingMessage(XmlDocument oMessage)
        {
            m_aOutgoingMessages.Add(oMessage);
        }

        protected XmlDocument GrabMessage()
        {
            if (m_aInboundMessages.Count > 0)
            {
                XmlDocument oMessage = m_aInboundMessages[0];
                m_aInboundMessages.RemoveAt(0);
                return oMessage;
            }
            return null;
        }

        protected void SendErrorMessage(string sErrorMessage)
        {
            Console.WriteLine(String.Format("TCPMessenger: Got error message: {0}", sErrorMessage));

            m_aOutgoingMessages.Clear();

            XmlDocument oMessage = enMessage.CreateErrorMessage(sErrorMessage);
            m_aOutgoingMessages.Add(oMessage);

            // TODO. Auto-close socket after send
        }

        protected bool CanBeFinished()
        {
            bool bCanBeFinished = !m_oTcpClient.Client.Connected;
            return bCanBeFinished;
        }
    }
}

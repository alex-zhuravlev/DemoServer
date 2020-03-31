using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Concurrent;

namespace DemoServer
{
    public class TCPListener
    {
        private TcpListener m_oTcpListener = null;
        private List<TcpClient> m_aNewTcpClients = new List<TcpClient>();
        private Thread m_oListenerThread = null;
        private bool m_bStop = false;

        public bool Init()
        {
            m_oTcpListener = new TcpListener(IPAddress.Any, Conf.CLIENT_PORT);

            return true;
        }

        public void Start()
        {
            m_oTcpListener.Start();
            Console.WriteLine("TCPListener: Listening clients at " + Conf.CLIENT_PORT);

            m_bStop = false;
            m_oListenerThread = new Thread(AcceptConnections);
            m_oListenerThread.Start();
        }

        public void Stop()
        {
            m_bStop = true;
            m_oListenerThread = null;
        }

        public List<TcpClient> GrabNewClients()
        {
            List<TcpClient> aNewClients = null;
            lock (m_aNewTcpClients)
            {
                aNewClients = new List<TcpClient>(m_aNewTcpClients);
                m_aNewTcpClients.Clear();
            }
            return aNewClients;
        }

        private void AcceptConnections()
        {
            while (true)
            {
                if (m_bStop) break;

                if (m_oTcpListener.Pending())
                {
                    TcpClient oNewClient = null;
                    try
                    {
                        oNewClient = m_oTcpListener.AcceptTcpClient();
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine("TCPListener:" + ex.Message);
                    }
                    if (oNewClient != null)
                    {
                        lock (m_aNewTcpClients)
                        {
                            m_aNewTcpClients.Add(oNewClient);
                        }
                        Console.WriteLine("TCPListener: Added new TcpClient");
                    }
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}

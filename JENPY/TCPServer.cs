using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using JENPY.Exceptions;
using JENPY.Request;
using JENPY.Utils;

namespace JENPY
{
    class TcpServer
    {
        private TcpListener _server;
        private Boolean _isRunning;
        private static JenpyServerRequestHandler handler = new JenpyServerRequestHandler();

        public TcpServer(int port)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            LoopClients();

        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                // wait for client connection
                TcpClient newClient = _server.AcceptTcpClient();

                // client found.
                // create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            sWriter.WriteLine("welcome to JENPY server {0}", ipAddress);
            sWriter.Flush();

            Boolean bClientConnected = true;
            try
            {
                while (bClientConnected)
                {
                    runClientConnection(sReader, sWriter);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("caught exception at the server{0}", e.GetBaseException());
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                //sWriter.Close();
                //sReader.Close();
            }

        }

        private void runClientConnection(StreamReader sReader, StreamWriter sWriter)
        {
            try
            {
                handler.handleRequest(sReader, sWriter);
            }
            catch (JenpyException e)
            {
                sWriter.WriteLine("An JENPY exception occured {0}", e.Message);
                sWriter.Flush();
            }
            //catch (ObjectDisposedException e)
            //{
            //    Console.WriteLine("An client's stream closed with exception message {0}, effectively closing resources", e.Message);

            //}
        }

    }

}

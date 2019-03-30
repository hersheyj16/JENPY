using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
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

        public List<TcpClient> peersList { get; set; }

        public TcpServer(int port, List<string> peers)
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            _isRunning = true;

            this.peersList = new List<TcpClient>();
            LoopPeers(peers);

            LoopClients();

        }

        private void LoopPeers(List<string> peers)
        {
            if (peers.Count == 0)
            {
                return;
            }

            // for simplicity's sake, let's just p2p with the first guy.
            string p1 = peers[0];
            string[] Info = p1.Split(':');

            string ip = Info[0];
            int port = Int32.Parse(Info[1]);
            Console.WriteLine("my peer is {0}:{1}", ip, port);
            TcpClient PeerClient = new TcpClient();


            // just wait a little for the peer to come online ...
            while (!PeerClient.Connected)
            {
                try
                {
                    PeerClient.Connect(ip, port);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(500);
                }
            }

            this.peersList.Add(PeerClient);

            StreamWriter cWriter = new StreamWriter(PeerClient.GetStream(), Encoding.ASCII);
            StreamReader cReader = new StreamReader(PeerClient.GetStream(), Encoding.ASCII);

            bool connectedToPeer = true;
            while (connectedToPeer)
            {
                Console.WriteLine("First P2P introduction ");

                cWriter.WriteLine("GETV | hersheys: .");
                cWriter.Flush();


                string sDataIncomming = cReader.ReadLine();
                Console.WriteLine("received: {0}", sDataIncomming);
                while (!string.IsNullOrEmpty(sDataIncomming))
                {
                    sDataIncomming = cReader.ReadLine();
                    Console.WriteLine("received: {0}", sDataIncomming);
                }

                connectedToPeer = false;
            }
            cReader.Close();
            cWriter.Close();

            Console.WriteLine("End of P2P Initiation");
        }
        public IEnumerable<string> ReadLines(Func<Stream> streamProvider,
                                     Encoding encoding)
        {
            using (var stream = streamProvider())
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
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
        }

    }



}

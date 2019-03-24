using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JENPY
{
    class TcpServer
    {
        private TcpListener _server;
        private Boolean _isRunning;

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

            Boolean bClientConnected = true;
            String sData = null;
            try
            {
                while (bClientConnected)
                {

                    // reads from stream
                    sData = sReader.ReadLine();

                    if (sData == "EXIT") {
                        sWriter.Write("Exiting\n");
                        sWriter.Flush();
                        sWriter.Close();
                        sReader.Close();
                        return;
                    }

                    // shows content on the console.
                    Console.WriteLine("Client &gt; " + sData);

                    // to write something back.
                    sWriter.WriteLine("Meaningfull things here");
                    sWriter.Flush();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("caught exception");
                Console.WriteLine(e.StackTrace);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Multi-Threaded TCP Server Demo");
            TcpServer server = new TcpServer(5555);
        }
    }
}

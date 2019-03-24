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

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            sWriter.WriteLine("welcome to JENPY server {0}", ipAddress);
            sWriter.Flush();

            Boolean bClientConnected = true;
            try
            {
                while (bClientConnected)
                {
                    // reads from stream
                    handleResponse(sReader, sWriter);

                    Console.WriteLine("received from handle response");
                    // to write something back.
                    sWriter.WriteLine("MOCK Response Meaningfull things here");
                    sWriter.Flush();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("caught exception");
                Console.WriteLine(e.StackTrace);
            }
  
        }

  

        private void handleResponse(StreamReader sReader, StreamWriter sWriter)
        {

            String sData = sReader.ReadLine();

            JenpyObject req = JenpyObjectParser.toJenpy(sData);

            if (sData == "EXIT .")
            {
                sWriter.Write("Exiting\n");
                sWriter.Flush();
                sWriter.Close();
                sReader.Close();
                return;
            }

            // shows content on the console.
            Console.WriteLine("Client &gt; " + sData);
        }
    }

}

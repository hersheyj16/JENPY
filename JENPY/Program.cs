using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JENPY
{
    class Program
    {

        // For peers

        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            int port = 5555;
            //dotnet run 3333 filename

            //TODO - maybe invest in a commandline parser:
            //CommandLineParser
            if (args.Length > 0)
            {
                port = Int32.Parse(args[0]);
                Console.WriteLine("port is {0}", port);
            }

            List<string> peers = new List<string>();
            if (args.Length > 1)
            {
                string fileName = args[1];
                peers = getPeers(fileName);
            }

            Console.WriteLine("Launching Multi-Threaded TCP JENPY Server {0} on port {1}", ipAddress, port);
            TcpServer server = new TcpServer(port, peers);

            foreach (string ip in peers)
            {
                Console.WriteLine("peers found: {0}", ip);
            }
        }

        private static List<string> getPeers(string fileName)
        {
            List<string> PeersData = new List<string>();
            string line;
            using (var fileStream = new System.IO.StreamReader(fileName))
            {
                while ((line = fileStream.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    PeersData.Add(line);
                }
            }
            return PeersData;
        }
    }
}

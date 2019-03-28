using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JENPY
{
    class Program
    {
        public const int port = 5555;

        static void Main(string[] args)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            Console.WriteLine("Launching Multi-Threaded TCP JENPY Server {0}", ipAddress);
            TcpServer server = new TcpServer(port);
        }
    }
}

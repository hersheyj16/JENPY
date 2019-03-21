using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace JENPY
{
    class Program
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public const int Port = 5026;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StartListening();

        }

        private static void StartListening()
        {

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            //creating the socket...
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            //bind the socket
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true) {
                    allDone.Reset(); // TODO, learn MRE

                    Console.WriteLine("waiting for a connection on: " + ipAddress + "port " + Port);
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    allDone.WaitOne();
                }
            }
            catch(Exception e) {
                Console.WriteLine("oh e" + e.StackTrace);
            }

            Console.WriteLine("press ENTER to continue");
            Console.Read();

        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Console.WriteLine("calling accept callback");
            allDone.Set(); // signal to main thread to continue

        }

        static void HandleAsyncCallback(IAsyncResult ar)
        {
        }

    }
}

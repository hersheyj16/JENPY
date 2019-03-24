using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace JENPY
{
public class handleClient
    {
        TcpClient clientSocket;

        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Chat);
            ctThread.Start();
        }

        private void Chat()
        {
            StreamReader reader = new StreamReader(clientSocket.GetStream());

            try
            {
                while (true)
                {
                    string message = reader.ReadLine();
                    Console.WriteLine("received message" + message);
                    //foreach (var client in Program.GetClients())
                    //{
                    //    StreamWriter writer = new StreamWriter(client.GetStream());
                    //    writer.Write(message);
                    //}
                    StreamWriter writer = new StreamWriter(clientSocket.GetStream());
                    writer.Write(message);
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine($"client disconnecting: {clientSocket.Client.RemoteEndPoint}");
                clientSocket.Client.Shutdown(SocketShutdown.Both);
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException reading from {clientSocket.Client.RemoteEndPoint}: {e.Message}");
            }

            clientSocket.Close();
            Program.RemoveClient(clientSocket);
            Console.WriteLine($"{Program.GetClientCount()} clients connected");
        }
    }
}
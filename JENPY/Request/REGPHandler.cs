using System;
using System.Collections.Generic;
using System.Net.Sockets;
using JENPY.Utils;

namespace JENPY.Request
{
    public class REGPHandler : RequestHandler
    {

        public JenpyObject Handle(JenpyObject req)
        {
            JenpyObject res = new JenpyObjectBuilder().Build();

            IDictionary<string, string> DataBody = req.ObjectData;
            foreach (var key in DataBody.Keys)
            {

                Console.WriteLine("peer data {0}:{1}", key, DataBody[key]);
                string ip = key;
                int port = Int32.Parse(DataBody[key]);
                TcpClient PeerClient = new TcpClient();
                PeerClient.Connect(ip, port);
                TcpServer.peersList.Add(PeerClient);
            }
            return res;
        }
    }
}

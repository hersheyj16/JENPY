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

            IDictionary<string, string> DataBody = req.ObjectData;
            IDictionary<string, string> resBody = new Dictionary<string, string>();

            foreach (var key in DataBody.Keys)

            {
                String regPeer = String.Format("peer data {0}:{1}", key, DataBody[key]);
                Console.WriteLine(regPeer);
                string ip = key;
                int port = Int32.Parse(DataBody[key]);
                TcpClient PeerClient = new TcpClient();
                PeerClient.Connect(ip, port);
                TcpServer.peersList.Add(PeerClient);

                resBody.Add(regPeer, JenpyConstants.SUCCESS);
            }
            JenpyObject res = new JenpyObjectBuilder()
                .WithVerb(JenpyConstants.OK)
                .WithObjectData(resBody).Build();

            return res;
        }
    }
}

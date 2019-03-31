using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using JENPY.Storage;
using JENPY.Utils;

namespace JENPY.Request
{
    public class PUTVHandler : RequestHandler
    {
        public JenpyObject Handle(JenpyObject req)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> entry in req.ObjectData)
            {
                String value = JenpyConstants.SUCCESS;
                if (DataStore.DataValues.ContainsKey(entry.Key))
                {
                    value = JenpyConstants.FAIL;
                }
                else
                {
                    // TODO: make this async...
                    // Also write to peer...
                    // TODO: conflict resolution for multiple read / write transactions
                    foreach (TcpClient peer in TcpServer.peersList)
                    {
                        StreamWriter sWriter = new StreamWriter(peer.GetStream(), Encoding.ASCII);
                        StreamReader sReader = new StreamReader(peer.GetStream(), Encoding.ASCII);

                        var input = JenpyObjectParser.SerializeToString(req);
                        Console.WriteLine("Jenpy request to pass to peer - serialized to {0}", input);
                        sWriter.Write(input);
                        sWriter.Flush();

                        string output = sReader.ReadLine();
                        while (!string.IsNullOrEmpty(output))
                        {
                            Console.WriteLine("peer responded {0}", output);
                            output = sReader.ReadLine();
                        }
                    }
                    // Peer ended

                    DataStore.DataValues.Add(entry.Key, entry.Value);
                    writeToDisk(entry);
                }
                data.Add(entry.Key, value);
            }

            JenpyObject resp = new JenpyObjectBuilder()
                .WithVerb(JenpyConstants.OK)
                .WithObjectData(data)
                .Build();
            return resp;
        }

        private void writeToDisk(KeyValuePair<string, string> entry)
        {
            string FileName = "/Users/jenny/Projects/JENPY/JENPY/Storage/mockDisk";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName, true))
            {
                String s = String.Format("{0}, {1}", entry.Key, entry.Value);
                file.WriteLine(s);
            }
        }
    }
}

using System;
using System.Collections.Generic;
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
                    DataStore.DataValues.Add(entry.Key, entry.Value);
                    //TODO... design this better as background task maybe on the dataStore that periodically goes around and make backups on disk
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

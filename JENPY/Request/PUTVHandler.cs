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
                else {
                    DataStore.DataValues.Add(entry.Key, entry.Value);
                }
                data.Add(entry.Key, value);
            }

            JenpyObject resp = new JenpyObjectBuilder()
                .WithVerb(JenpyConstants.OK)
                .WithObjectData(data)
                .Build();
            return resp;
        }
    }
}

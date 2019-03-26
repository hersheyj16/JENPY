using System;
using System.Collections.Generic;
using JENPY.Utils;
using JENPY.Storage;
namespace JENPY.Request
{

    public class GETVHandler : RequestHandler
    {
        public JenpyObject Handle(JenpyObject req)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();

       
            foreach (KeyValuePair<string,string> entry in req.ObjectData){
                String keyString = entry.Key;
                String valString = JenpyConstants.FAIL;
                if (DataStore.DataValues.ContainsKey(keyString)){
                    valString = DataStore.DataValues[entry.Key];
                }
                data.Add(entry.Key, valString);
            }

            JenpyObject resp = new JenpyObjectBuilder()
                .WithVerb(JenpyConstants.OK)
                .WithObjectData(data)
                .Build();
            return resp;

        }
    }
}

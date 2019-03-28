using System;
using System.Collections.Generic;
using JENPY.BlockChain;
using JENPY.Utils;

namespace JENPY.Request
{
    public class BDESHandler : RequestHandler
    {
        public JenpyObject Handle(JenpyObject req)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            foreach (var block in BlockStore.BlockChain) {
                data.Add(block.Hash, block.Data);
            }

            JenpyObject res = new JenpyObject(JenpyConstants.OK, data);
            return res;
        }
    }
}

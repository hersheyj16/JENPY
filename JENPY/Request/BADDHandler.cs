using System;
using System.Collections.Generic;
using System.Text;
using JENPY.BlockChain;
using JENPY.Utils;

namespace JENPY.Request
{
    public class BADDHandler : RequestHandler
    {

        public JenpyObject Handle(JenpyObject req)
        {
            // serialize the string from req.ObjectData;
            StringBuilder sb = new StringBuilder();

            foreach (string key in req.ObjectData.Keys){
                sb.Append(key);
                sb.Append(",");
                sb.Append(req.ObjectData[key]);
            }

            string input = sb.ToString();
            Block b = new Block(input);
            BlockStore.BlockChain.Add(b);
            JenpyObject res = new JenpyObject(JenpyConstants.OK, req.ObjectData);
            return res;
        }
    }
}

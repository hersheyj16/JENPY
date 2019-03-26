using System;
using JENPY.Utils;

namespace JENPY.Request
{
    public class EXITHandler : RequestHandler
    {
        public JenpyObject Handle(JenpyObject req)
        {
            //TODO write a builder
            JenpyObject response = new JenpyObject(JenpyConstants.TERM, null);
            return response;
        }
    }
}

using System;
using JENPY.Utils;

namespace JENPY.Request
{
    public class ECHOHandler : RequestHandler
    {

        public JenpyObject Handle(JenpyObject req)
        {
            req.Verb = JenpyConstants.OK;
            return req;
        }
    }
}

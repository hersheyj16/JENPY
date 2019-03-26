using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JENPY.Request;

namespace JENPY.Utils
{
    public class JenpyServerRequestHandler
    {
        IDictionary<String, RequestHandler> handlers = new Dictionary<String, RequestHandler>();

        public JenpyServerRequestHandler()
        {
            handlers.Add("ECHO", new ECHOHandler());
            handlers.Add("EXIT", new EXITHandler());
            handlers.Add("GETV", new GETVHandler());
            handlers.Add("PUTV", new PUTVHandler());
        }

        public void handleRequest(StreamReader sReader, StreamWriter sWriter)
        {

            String sData = sReader.ReadLine();
            JenpyObject req = JenpyObjectParser.toJenpy(sData);

            RequestHandler hander = determineHandler(req);
            JenpyObject res = hander.Handle(req);

            if (res.Verb == JenpyConstants.TERM)
            {
                sWriter.Write("Terminating\n");
                sWriter.Flush();
                sWriter.Dispose();
                sReader.Dispose();
                return;
            }
            // shows content on the console.
            Console.WriteLine("Client > " + sData);

            foreach (KeyValuePair<string, string> entry in req.ObjectData)
            {
                Console.WriteLine("key {0}, val {1}", entry.Key, entry.Value);
            }

            String JenpyData = "MOCK Response Meaningfull things here";
            JenpyData = JenpyObjectParser.SerializeToString(res);

            sWriter.WriteLine(JenpyData);
            sWriter.Flush();
        }

        private RequestHandler determineHandler(JenpyObject req)
        {

            return handlers[req.Verb];

        }


    }
}

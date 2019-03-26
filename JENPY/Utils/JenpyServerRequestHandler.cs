using System;
using System.Collections.Generic;
using System.IO;
using JENPY.Request;

namespace JENPY.Utils
{
    public class JenpyServerRequestHandler
    {
        public static void handleRequest(StreamReader sReader, StreamWriter sWriter)
        {

            String sData = sReader.ReadLine();
            JenpyObject req = JenpyObjectParser.toJenpy(sData);
            //RequestHandler handler = determineHandler(req.Verb);
        
            //JenpyObject res = handler.Handle(req);

            if (req.Verb == "EXIT")
            {
                sWriter.Write("Exiting\n");
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

            sWriter.WriteLine("MOCK Response Meaningfull things here");
            sWriter.Flush();
        }

        private static RequestHandler determineHandler(string verb)
        {
            if (verb == "EXIT")
            {
                return new EXITHandler();
            }
            return new EXITHandler();

        }

    }
}

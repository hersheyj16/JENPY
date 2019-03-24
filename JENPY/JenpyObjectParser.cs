using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace JENPY
{

    public class JenpyObjectParser
    {


        public static JenpyObject toJenpy(String s) {
            Console.WriteLine("Received " + s);

            string verb = "ECHO";

            IDictionary<string, string> data = new Dictionary<string, string>();
            return new JenpyObject(verb, data);
       }

       
    }


}

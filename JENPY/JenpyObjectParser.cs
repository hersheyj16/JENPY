using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace JENPY
{

    public class JenpyObjectParser
    {
        private const int VerbLength = 4;

        public static JenpyObject toJenpy(String input)
        {
            try
            {
                Console.WriteLine("Received " + input);


                //First look for the period.
                int EndIndex = input.IndexOf('.');

                if (EndIndex < 0)
                {
                    throw new Exception("Wrong format, expecting a period");
                }

                //Assume all verbs are 4 letters.
                string verb = input.Substring(0, VerbLength);

                //TODO see if we can add a logging library
                Console.WriteLine("Debug - Received verb {0}", verb);
                int BodyEnd = EndIndex - VerbLength - 1;
                string body = input.Substring(5, BodyEnd);

                Console.WriteLine("Debug - Received body {0}", body);


                IDictionary<string, string> data = parseBodyData(body);
                return new JenpyObject(verb, data);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new Exceptions.JenpyMalformException(e.Message);
            }
        }

        private static IDictionary<string, string> parseBodyData(string body)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();


            int DelimiterIndex = body.IndexOf('|');
            Console.WriteLine("Delimiter start at {0}", DelimiterIndex);
            return data;

        }
    }


}

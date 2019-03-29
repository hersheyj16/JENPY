using System;
using System.Collections.Generic;
using System.Text;
using JENPY.Exceptions;

namespace JENPY
{

    public class JenpyObjectParser
    {
        private const int VerbLength = 4;
        private const char TokenDelimiter = '|';
        private const char WithinTokenDelimiter = ':';

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
                Console.WriteLine("exception occured while parsing {0}", e.Message);
                Console.WriteLine(e.StackTrace);
                throw new JenpyException(e.Message);
            }
        }

        internal static string SerializeToString(JenpyObject res)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(res.Verb);

            sb.Append(" | ");

            // Key Value pairs

            foreach (KeyValuePair<string, string> entry in res.ObjectData)
            {
                sb.Append(entry.Key);
                sb.Append(":");
                sb.Append(entry.Value);
                sb.Append(" | ");
            }

            //remove the last |

            string bar = " | ";
            sb.Remove(sb.Length - bar.Length, bar.Length);
            sb.Append(" .");
            return sb.ToString();
        }

        private static IDictionary<string, string> parseBodyData(string body)
        {
            IDictionary<string, string> data;


            int DelimiterIndex = body.IndexOf('|');
            if (DelimiterIndex >= 0)
            {
                var tokens = body.Split(TokenDelimiter);
                data = getKeyValues(tokens);
            }
            else
            {
                data = new Dictionary<string, string>();
            }
            return data;

        }

        private static IDictionary<string, string> getKeyValues(string[] tokens)
        {
            IDictionary<string, string> data = new Dictionary<string, string>();
            foreach (string token in tokens)
            {
                var trimmed = token.Trim();
                if (!String.IsNullOrEmpty(trimmed))
                {
                    var KeyVal = trimmed.Split(WithinTokenDelimiter);
                    if (KeyVal.Length > 1)
                    {
                        data.Add(KeyVal[0], KeyVal[1]);
                    }
                    else
                    {
                        data.Add(KeyVal[0], "");
                    }
                }
            }
            return data;

        }
    }


}

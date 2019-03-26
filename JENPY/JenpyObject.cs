using System;
using System.Collections;
using System.Collections.Generic;

namespace JENPY
{
    public class JenpyObject
    {
        public string Verb { get; set; }
        IDictionary<string, string> ResponseData { get; set; }

        public JenpyObject(string verb, IDictionary<string, string> data)
        {
            this.Verb = verb;
            this.ResponseData = data;
        }
    }
}

using System;
using System.Collections.Generic;

namespace JENPY.Utils
{
    public class JenpyObjectBuilder
    {
        public string Verb { get; set; }
        public IDictionary<string, string> ObjectData { get; set; }

        public JenpyObjectBuilder()
        {
        }

        public JenpyObjectBuilder WithVerb(String verb)
        {
            this.Verb = verb;
            return this;
        }

        public JenpyObjectBuilder WithObjectData(IDictionary<string, string> data)
        {
            this.ObjectData = data;
            return this;
        }

        public JenpyObject Build()
        {
            return new JenpyObject(this.Verb, this.ObjectData);
        }
    }
}
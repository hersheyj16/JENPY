using System;
using System.Collections.Generic;

namespace JENPY.Storage
{
    public class DataStore
    {
        public static IDictionary<String, string> DataValues { get; set;}

        static DataStore() {
            DataValues = new Dictionary<string, string>();
       
        }
    }
}

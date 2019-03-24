using System;
using System.Collections;
using System.Collections.Generic;

namespace JENPY
{
    public class JenpyObject
    {
        public string Status { get; set; }
        IDictionary<string, string> ResponseData { get; set; }

        public JenpyObject(string status, IDictionary<string, string> data)
        {
            this.Status = status;
            this.ResponseData = data;
        }
    }
}

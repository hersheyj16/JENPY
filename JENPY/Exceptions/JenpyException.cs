using System;
namespace JENPY.Exceptions
{
    [Serializable()]
    public class JenpyException : System.Exception
    {
        public JenpyException() : base() { }
        public JenpyException(string message) : base(message)
        {
            message += " MALFORMED JENPY OBJECT ERROR";
        }

        public JenpyException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected JenpyException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

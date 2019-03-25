using System;
namespace JENPY.Exceptions
{
    [Serializable()]
    public class JenpyMalformException : System.Exception
    {
        public JenpyMalformException() : base() { }
        public JenpyMalformException(string message) : base(message)
        {
            message += " MALFORMED JENPY OBJECT ERROR";
        }

        public JenpyMalformException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected JenpyMalformException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}

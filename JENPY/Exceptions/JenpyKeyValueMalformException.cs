using System;
namespace JENPY.Exceptions
{
    public class JenpyKeyValueMalformException : JenpyMalformException
    {
        public JenpyKeyValueMalformException(string message) : base(message)
        {
        }
    }
}

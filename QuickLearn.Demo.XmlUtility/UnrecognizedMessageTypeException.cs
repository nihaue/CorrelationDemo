using System;
using System.Runtime.Serialization;

namespace QuickLearn.Demo.XmlUtility
{
    [Serializable]
    public class UnrecognizedMessageTypeException : Exception
    {
        public UnrecognizedMessageTypeException()
        {
        }

        public UnrecognizedMessageTypeException(string message) : base(message)
        {
        }

        public UnrecognizedMessageTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnrecognizedMessageTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
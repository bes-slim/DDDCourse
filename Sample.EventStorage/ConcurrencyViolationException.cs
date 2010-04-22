using System;
using System.Runtime.Serialization;

namespace Sample.EventStorage
{
    [Serializable]
    public class ConcurrencyViolationException : Exception
    {
        public ConcurrencyViolationException()
        {
        }

        public ConcurrencyViolationException(string message)
            : base(message)
        {
        }

        public ConcurrencyViolationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConcurrencyViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
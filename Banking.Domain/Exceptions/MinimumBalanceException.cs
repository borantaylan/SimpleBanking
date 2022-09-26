using System.Runtime.Serialization;

namespace Banking.Domain.Exceptions
{
    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException()
        {
        }

        public MinimumBalanceException(string? message) : base(message)
        {
        }

        public MinimumBalanceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MinimumBalanceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

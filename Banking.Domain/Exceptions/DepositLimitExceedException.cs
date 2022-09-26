using System.Runtime.Serialization;

namespace Banking.Domain.Exceptions
{
    public class DepositLimitExceedException : Exception
    {
        public DepositLimitExceedException()
        {
        }

        public DepositLimitExceedException(string? message) : base(message)
        {
        }

        public DepositLimitExceedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DepositLimitExceedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

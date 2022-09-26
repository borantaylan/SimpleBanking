using System.Runtime.Serialization;

namespace Banking.Domain.Exceptions
{
    public class WithdrawLimitExceedException : Exception
    {
        public WithdrawLimitExceedException()
        {
        }

        public WithdrawLimitExceedException(string? message) : base(message)
        {
        }

        public WithdrawLimitExceedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WithdrawLimitExceedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

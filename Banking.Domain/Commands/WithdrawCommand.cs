using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for withdrawing from an account
    /// </summary>
    public class WithdrawCommand : IRequest
    {
        public WithdrawCommand(string accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }

        /// <summary>
        /// Identifier of the account
        /// </summary>
        public string AccountNumber { get; }

        /// <summary>
        /// Balance being deposited
        /// </summary>
        public double Amount { get; }
    }
}

using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for depositing to an account
    /// </summary>
    public class DepositCommand : IRequest
    {
        public DepositCommand(string accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            Amount = amount;
        }

        /// <summary>
        /// AccountNumber of the account
        /// </summary>
        public string AccountNumber { get; }

        /// <summary>
        /// Balance being deposited
        /// </summary>
        public double Amount { get; }
    }
}

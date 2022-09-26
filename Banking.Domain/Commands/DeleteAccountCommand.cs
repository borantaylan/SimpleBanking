using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for deleting account
    /// </summary>
    public class DeleteAccountCommand : IRequest
    {
        /// <summary>
        /// Constructor having AccountNumber of the account
        /// </summary>
        public DeleteAccountCommand(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        /// <summary>
        /// AccountNumber of the account
        /// </summary>
        public string AccountNumber { get; }
    }
}

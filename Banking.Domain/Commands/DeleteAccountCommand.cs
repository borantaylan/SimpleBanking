using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for deleting account
    /// </summary>
    public class DeleteAccountCommand : IRequest
    {
        /// <summary>
        /// Constructor having Identifier of the account
        /// </summary>
        public DeleteAccountCommand(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        /// <summary>
        /// Identifier of the account
        /// </summary>
        public string AccountNumber { get; }
    }
}

using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for deleting account
    /// </summary>
    public class DeleteAllAccountsCommand : IRequest
    {
        /// <summary>
        /// Constructor having AccountNumber of the account
        /// </summary>
        public DeleteAllAccountsCommand(Guid userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// AccountNumber of the account
        /// </summary>
        public Guid UserId { get; }
    }
}

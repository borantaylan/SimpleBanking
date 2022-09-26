using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for deleting account
    /// </summary>
    public class DeleteAllAccountsCommand : IRequest
    {
        /// <summary>
        /// Constructor having Identifier of the account
        /// </summary>
        public DeleteAllAccountsCommand(Guid userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Identifier of the account
        /// </summary>
        public Guid UserId { get; }
    }
}

using MediatR;

namespace Banking.WebAPI.Commands
{
    /// <summary>
    /// The command for creating account
    /// </summary>
    public class CreateAccountCommand : IRequest<string>
    {
        public CreateAccountCommand(Guid userId, double initialDeposit)
        {
            UserId = userId;
            InitialDeposit = initialDeposit;
        }

        /// <summary>
        /// Owner of the account
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Initial money is being put
        /// </summary>
        public double InitialDeposit { get; }
    }
}

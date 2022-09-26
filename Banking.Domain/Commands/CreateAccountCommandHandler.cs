using Banking.Domain.Contracts;
using Banking.WebAPI.Commands;
using MediatR;

namespace Banking.Domain.Commands
{
    /// <summary>
    /// Handling create account command
    /// </summary>
    internal class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, string>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var accountId = GenerateAccountNumber();
            var account = new Account(request.InitialDeposit, request.UserId, accountId);
            unitOfWork.AccountRepository.Create(account);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return accountId;
        }

        private string GenerateAccountNumber()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

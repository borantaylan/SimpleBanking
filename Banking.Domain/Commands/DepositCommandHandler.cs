using Banking.Domain.Contracts;
using Banking.WebAPI.Commands;
using MediatR;

namespace Banking.Domain.Commands
{
    /// <summary>
    /// Handling deposit command
    /// </summary>
    internal class DepositCommandHandler : AsyncRequestHandler<DepositCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DepositCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task Handle(DepositCommand request, CancellationToken cancellationToken)
        {
            var account = await unitOfWork.AccountRepository.FindByAccountNumber(request.AccountNumber);
            account.Deposit(request.Amount);
            unitOfWork.AccountRepository.UpdateAccount(account);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

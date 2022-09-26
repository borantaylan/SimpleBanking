using Banking.Domain.Contracts;
using Banking.WebAPI.Commands;
using MediatR;

namespace Banking.Domain.Commands
{
    /// <summary>
    /// Handling deposit command
    /// </summary>
    internal class WithdrawCommandHandler : AsyncRequestHandler<WithdrawCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public WithdrawCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
        {
            var account = await unitOfWork.AccountRepository.FindByAccountNumber(request.AccountNumber);
            account.Withdraw(request.Amount);
            unitOfWork.AccountRepository.UpdateAccount(account);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

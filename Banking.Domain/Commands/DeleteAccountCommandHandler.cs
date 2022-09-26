using Banking.Domain.Contracts;
using Banking.WebAPI.Commands;
using MediatR;

namespace Banking.Domain.Commands
{
    /// <summary>
    /// Handling delete account command
    /// </summary>
    internal class DeleteAccountCommandHandler : AsyncRequestHandler<DeleteAccountCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteAccountCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.AccountRepository.Delete(request.AccountNumber);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

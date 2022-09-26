using Banking.Domain.Contracts;
using Banking.WebAPI.Commands;
using MediatR;

namespace Banking.Domain.Commands
{
    /// <summary>
    /// Handling delete all accounts by user command
    /// </summary>
    internal class DeleteAllAccountsCommandHandler : AsyncRequestHandler<DeleteAllAccountsCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteAllAccountsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override async Task Handle(DeleteAllAccountsCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.AccountRepository.DeleteByUserId(request.UserId);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}

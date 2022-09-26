using Banking.Domain.Contracts;

namespace Banking.Storage
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AccountContext context;

        public UnitOfWork(AccountContext context)
        {
            this.context = context;
        }
        public IAccountRepository AccountRepository => new AccountRepository(context);

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}

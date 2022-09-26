using Banking.Domain;
using Banking.Domain.Contracts;
using Banking.Domain.Queries;
using Banking.Storage.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Banking.Storage
{
    internal class AccountRepository : IAccountRepository, IAccountQueries
    {
        private readonly AccountContext accountContext;

        public AccountRepository(AccountContext accountContext)
        {
            this.accountContext = accountContext;
        }
        #region Commands
        /// <inheritdoc/>
        public void Create(Account account)
        {
            accountContext.Accounts.Add(account);
        }

        /// <inheritdoc/>
        public async Task Delete(string accountNumber)
        {
            var account = await accountContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if(account == null)
            {
                throw new EntityNotFoundException();
            }
            accountContext.Accounts.Remove(account);
        }

        /// <inheritdoc/>
        public async Task DeleteByUserId(Guid userId)
        {
            var accounts = await accountContext.Accounts.Where(x => x.UserId == userId).AsQueryable().ToListAsync();
            if(accounts.Count() == 0)
            {
                throw new EntityNotFoundException();
            }
            accountContext.Accounts.RemoveRange(accounts);
        }

        /// <inheritdoc/>
        public async Task<Account> FindByAccountNumber(string accountNumber)
        {
            var account = await accountContext.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new EntityNotFoundException();
            }
            return account;
        }

        /// <inheritdoc/>
        public void UpdateAccount(Account account)
        {
            //No need for operation since we use tracked version for now.
        }

        #endregion

        #region Queries

        /// <inheritdoc/>
        public async Task<IEnumerable<AccountView>> GetAccountByUserId(Guid userId)
        {
            var accounts = await accountContext.Accounts.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
            if (accounts.Count() == 0)
            {
                throw new EntityNotFoundException();
            }
            return accounts.Select(x=> new AccountView(x.AccountNumber,x.Balance, userId));
        }
        /// <inheritdoc/>
        public async Task<AccountView> GetAccount(string accountNumber)
        {
            var account = await accountContext.Accounts.AsNoTracking().SingleOrDefaultAsync(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new EntityNotFoundException();
            }
            return new AccountView(account.AccountNumber, account.Balance, account.UserId);
        }

        #endregion
    }
}
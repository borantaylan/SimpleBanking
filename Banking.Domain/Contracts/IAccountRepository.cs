namespace Banking.Domain.Contracts
{
    /// <summary>
    /// Account repo, having CRUD operation definitions
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Fetches tracked entity with account number
        /// </summary>
        /// <param name="accountNumber"></param>
        Task<Account> FindByAccountNumber(string accountNumber);

        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="account"></param>
        void Create(Account account);

        /// <summary>
        /// Deletes an account with given account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        Task Delete(string accountNumber);

        /// <summary>
        /// Deletes all accounts with the given UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task DeleteByUserId(Guid userId);

        /// <summary>
        /// Updates the account
        /// </summary>
        void UpdateAccount(Account account);
    }
}

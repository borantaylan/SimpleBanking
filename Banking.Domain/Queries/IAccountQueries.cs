namespace Banking.Domain.Queries
{
    /// <summary>
    /// Queries w.r.t accounts
    /// </summary>
    public interface IAccountQueries
    {
        /// <summary>
        /// Fetches the account for given account AccountNumber
        /// </summary>
        /// <param name="accountNumber">AccountNumber of the account as Guid</param>
        /// <returns>Single account</returns>
        Task<AccountView> GetAccount(string accountNumber);

        /// <summary>
        /// Fetches all the accounts for given user AccountNumber
        /// </summary>
        /// <param name="userId">AccountNumber of the user as Guid</param>
        /// <returns>List of accounts</returns>
        Task<IEnumerable<AccountView>> GetAccountByUserId(Guid userId);
    }
}

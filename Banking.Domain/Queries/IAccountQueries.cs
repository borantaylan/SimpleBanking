namespace Banking.Domain.Queries
{
    /// <summary>
    /// Queries w.r.t accounts
    /// </summary>
    public interface IAccountQueries
    {
        /// <summary>
        /// Fetches the account for given account Identifier
        /// </summary>
        /// <param name="accountNumber">Identifier of the account as Guid</param>
        /// <returns>Single account</returns>
        Task<AccountView> GetAccount(string accountNumber);

        /// <summary>
        /// Fetches all the accounts for given user Identifier
        /// </summary>
        /// <param name="userId">Identifier of the user as Guid</param>
        /// <returns>List of accounts</returns>
        Task<IEnumerable<AccountView>> GetAccountByUserId(Guid userId);
    }
}

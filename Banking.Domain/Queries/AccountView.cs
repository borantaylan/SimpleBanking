namespace Banking.Domain.Queries
{
    /// <summary>
    /// Query view for fetching accounts
    /// </summary>
    public class AccountView
    {
        public AccountView(string accountNumber, double balance, Guid userId)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            UserId = userId;
        }

        /// <summary>
        /// AccountNumber of the account
        /// </summary>
        public string AccountNumber { get; }

        /// <summary>
        /// Balance that account holds
        /// </summary>
        public double Balance { get; }

        /// <summary>
        /// User id of the account
        /// </summary>
        public Guid UserId { get; }
    }
}

using Banking.Domain.Contracts;

namespace Banking.Domain
{
    /// <summary>
    /// Account entity, defines the domain logic in terms of operations and domain logic.
    /// </summary>
    public class Account : IEntity<string>
    {
        //For EF to be able to map accountNumber to identifier
        private Account() { }
        public Account(double balance, Guid userId, string accountNumber)
        {
            Balance = balance;
            UserId = userId;
            Identifier = accountNumber;
            AccountValidator.ValidateBalance(balance);
        }

        /// <summary>
        /// Balance of the account
        /// </summary>
        public double Balance { get; private set; }

        /// <summary>
        /// Unique account number
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// Id of the user that account belongs to
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Deposits the given amount.
        /// </summary>
        /// <param name="amount">Balance in $ as double.</param>
        public void Deposit(double amount)
        {
            AccountValidator.ValidateDepositAmount(amount);
            Balance += amount;
        }

        /// <summary>
        /// Withdraws the given amount.
        /// </summary>
        /// <param name="amount">Balance in $ as double.</param>
        public void Withdraw(double amount)
        {
            AccountValidator.ValidateWithdrawAmount(amount, Balance);
            Balance -= amount;
        }
    }
}
using Banking.Domain.Exceptions;

namespace Banking.Domain
{
    internal static class AccountValidator
    {
        internal static void ValidateBalance(double balance)
        {
            if (balance < 100) throw new MinimumBalanceException("Balance can not be lower than 100$.");
        }

        internal static void ValidateDepositAmount(double amount)
        {
            if (amount <= 0) throw new ArgumentException("Less than 0$ deposit is not allowed.");
            if (amount >= 10000) throw new DepositLimitExceedException("More than 10000$ of deposit is not allowed.");
        }

        internal static void ValidateWithdrawAmount(double amount, double balance)
        {
            if (amount <= 0) throw new ArgumentException("Less than 0$ withdraw is not allowed.");
            if (amount > balance - 100) throw new WithdrawLimitExceedException("Balance can not be lower than 100$.");
            if (amount >= balance * 90 / 100) throw new WithdrawLimitExceedException("Withdrawing more than %90 of the current balance is not allowed.");
        }
    }

}

namespace BankAccountApp;

/// <summary>
/// Represents a bank account with basic banking operations.
/// </summary>
public class BankAccount
{
    public string AccountNumber { get; }
    public double Balance { get; private set; }
    public string AccountHolderName { get; }
    public string AccountType { get; }
    public DateTime DateOpened { get; }

    /// <summary>
    /// Initializes a new instance of the BankAccount class.
    /// </summary>
    /// <param name="accountNumber">The unique account number.</param>
    /// <param name="initialBalance">The initial balance for the account.</param>
    /// <param name="accountHolderName">The name of the account holder.</param>
    /// <param name="accountType">The type of account (e.g., Savings, Checking).</param>
    /// <param name="dateOpened">The date when the account was opened.</param>
    /// <exception cref="ArgumentNullException">Thrown when accountNumber, accountHolderName, or accountType is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when initialBalance is negative.</exception>
    public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentNullException(nameof(accountNumber));
        if (string.IsNullOrWhiteSpace(accountHolderName))
            throw new ArgumentNullException(nameof(accountHolderName));
        if (string.IsNullOrWhiteSpace(accountType))
            throw new ArgumentNullException(nameof(accountType));
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative", nameof(initialBalance));

        AccountNumber = accountNumber;
        Balance = initialBalance;
        AccountHolderName = accountHolderName;
        AccountType = accountType;
        DateOpened = dateOpened;
    }

    /// <summary>
    /// Credits (adds) the specified amount to the account balance.
    /// </summary>
    /// <param name="amount">The amount to credit to the account.</param>
    /// <exception cref="ArgumentException">Thrown when amount is zero or negative.</exception>
    public void Credit(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Credit amount must be positive", nameof(amount));

        if (amount == 0)
            return;

        Balance += amount;
    }

    /// <summary>
    /// Debits (subtracts) the specified amount from the account balance.
    /// </summary>
    /// <param name="amount">The amount to debit from the account.</param>
    /// <exception cref="ArgumentException">Thrown when amount is zero or negative.</exception>
    /// <exception cref="InsufficientBalanceException">Thrown when the account has insufficient funds.</exception>
    public void Debit(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Debit amount must be positive", nameof(amount));

        if (amount == 0)
            return;

        if (Balance < amount)
            throw new InsufficientBalanceException($"Insufficient balance for debit of {amount}");

        Balance -= amount;
    }

    /// <summary>
    /// Gets the current balance of the account.
    /// </summary>
    /// <returns>The current balance.</returns>
    public double GetBalance()
    {
        return Balance; // Math.Round(balance, 2);
    }

    /// <summary>
    /// Transfers money from this account to another account.
    /// </summary>
    /// <param name="toAccount">The destination account.</param>
    /// <param name="amount">The amount to transfer.</param>
    /// <exception cref="InsufficientBalanceException">Thrown when balance is insufficient.</exception>
    /// <exception cref="TransferLimitExceededException">Thrown when transfer exceeds limit for different account owners.</exception>
    public void Transfer(BankAccount toAccount, double amount)
    {
        if (toAccount == null)
            throw new ArgumentNullException(nameof(toAccount), "Target account cannot be null");

        if (this == toAccount)
            throw new ArgumentException("Cannot transfer to the same account");

        if (amount < 0)
            throw new ArgumentException("Transfer amount must be positive", nameof(amount));

        if (amount == 0)
            return;

        if (Balance < amount)
            throw new InsufficientBalanceException($"Insufficient balance for transfer of {amount}");

        Balance -= amount;
        toAccount.Balance += amount;
    }

    /// <summary>
    /// Prints the account statement showing the account number and current balance.
    /// </summary>
    public void PrintStatement()
    {
        Console.WriteLine($"Account Number: {AccountNumber}, Balance: {Balance}");
        // Add code here to print recent transactions
    }

    /// <summary>
    /// Calculates the interest earned on the current balance.
    /// </summary>
    /// <param name="interestRate">The interest rate to apply (as a decimal).</param>
    /// <returns>The amount of interest earned.</returns>
    public double CalculateInterest(double interestRate)
    {
        if (interestRate < 0)
            throw new ArgumentException("Interest rate cannot be negative", nameof(interestRate));

        return Balance * interestRate;
    }
}


/// <summary>
/// Exception thrown when an account has insufficient balance for an operation.
/// </summary>
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}
/// <summary>
/// Exception thrown when a transfer amount exceeds the maximum limit.
/// </summary>
public class TransferLimitExceededException : Exception
{
    public TransferLimitExceededException(string message) : base(message) { }
}
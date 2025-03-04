namespace BankAccountApp;

public class AccountSimulator
{
  private static readonly Random _random = new();
  private const int DEFAULT_NUMBER_OF_ACCOUNTS = 20;
  private const int TRANSACTIONS_PER_ACCOUNT = 100;
  private const double MIN_INITIAL_BALANCE = 10;
  private const double MAX_INITIAL_BALANCE = 50000;
  private const double MIN_TRANSACTION_AMOUNT = -500;
  private const double MAX_TRANSACTION_AMOUNT = 500;

  private readonly List<BankAccount> _accounts;

  public AccountSimulator()
  {
    _accounts = new List<BankAccount>();
  }

  public void RunSimulation()
  {
    CreateAccounts();
    SimulateTransactions();
    SimulateTransfers();
  }

  private void CreateAccounts()
  {
    int createdAccounts = 0;
    while (createdAccounts < DEFAULT_NUMBER_OF_ACCOUNTS)
    {
      try
      {
        var account = new BankAccount(
            $"Account {createdAccounts + 1}",
            GenerateRandomBalance(MIN_INITIAL_BALANCE, MAX_INITIAL_BALANCE),
            GenerateRandomAccountHolder(),
            GenerateRandomAccountType(),
            GenerateRandomDateOpened());

        _accounts.Add(account);
        createdAccounts++;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Account creation failed: {ex.Message}");
      }
    }
  }

  private void SimulateTransactions()
  {
    foreach (var account in _accounts)
    {
      for (int i = 0; i < TRANSACTIONS_PER_ACCOUNT; i++)
      {
        ProcessTransaction(account);
      }
      PrintAccountSummary(account);
    }
  }

  private void ProcessTransaction(BankAccount account)
  {
    double amount = GenerateRandomBalance(MIN_TRANSACTION_AMOUNT, MAX_TRANSACTION_AMOUNT);
    try
    {
      if (amount >= 0)
      {
        account.Credit(amount);
        PrintTransactionResult("Credit", amount, account);
      }
      else
      {
        account.Debit(-amount);
        PrintTransactionResult("Debit", amount, account);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Transaction failed: {ex.Message}");
    }
  }

  private void SimulateTransfers()
  {
    foreach (var fromAccount in _accounts)
    {
      foreach (var toAccount in _accounts.Where(a => a != fromAccount))
      {
        ProcessTransfer(fromAccount, toAccount);
      }
    }
  }

  private void ProcessTransfer(BankAccount fromAccount, BankAccount toAccount)
  {
    try
    {
      double transferAmount = GenerateRandomBalance(0, fromAccount.Balance);
      fromAccount.Transfer(toAccount, transferAmount);
      Console.WriteLine(
          $"Transfer: {transferAmount:C} from {fromAccount.AccountNumber} " +
          $"({fromAccount.AccountHolderName}, {fromAccount.AccountType}) to " +
          $"{toAccount.AccountNumber} ({toAccount.AccountHolderName}, {toAccount.AccountType})");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Transfer failed: {ex.Message}");
    }
  }

  private static double GenerateRandomBalance(double min, double max)
  {
    return Math.Round(_random.NextDouble() * (max - min) + min, 2);
  }

  private static string GenerateRandomAccountHolder()
  {
    string[] accountHolderNames = {
            "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois",
            "Liam Johnson", "Emma Martinez", "Noah Lee", "Olivia Kim",
            "William Chen", "Ava Wang", "James Brown", "Isabella Nguyen",
            "Benjamin Wilson", "Mia Li", "Lucas Anderson", "Charlotte Liu",
            "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim"
        };

    return accountHolderNames[_random.Next(accountHolderNames.Length)];
  }

  private static string GenerateRandomAccountType()
  {
    string[] accountTypes = {
            "Savings", "Checking", "Money Market",
            "Certificate of Deposit", "Retirement"
        };
    return accountTypes[_random.Next(accountTypes.Length)];
  }

  private static DateTime GenerateRandomDateOpened()
  {
    DateTime startDate = new(DateTime.Today.Year - 10, 1, 1);
    int daysRange = (DateTime.Today - startDate).Days;
    DateTime randomDate = startDate.AddDays(_random.Next(daysRange));

    if (randomDate.Year == DateTime.Today.Year && randomDate >= DateTime.Today)
    {
      randomDate = randomDate.AddDays(-1);
    }

    return randomDate;
  }

  private static void PrintTransactionResult(string type, double amount, BankAccount account)
  {
    Console.WriteLine(
        $"{type}: {amount:C}, Balance: {account.Balance:C}, " +
        $"Account Holder: {account.AccountHolderName}, " +
        $"Account Type: {account.AccountType}");
  }

  private static void PrintAccountSummary(BankAccount account)
  {
    Console.WriteLine(
        $"Account: {account.AccountNumber}, Balance: {account.Balance:C}, " +
        $"Account Holder: {account.AccountHolderName}, " +
        $"Account Type: {account.AccountType}");
  }
}
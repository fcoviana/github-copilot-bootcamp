using System;
using Xunit;
using BankAccountApp;

namespace BankAccountApp.UnitTests
{
    public class BankAccountTests
    {

        private readonly BankAccount _bankAccount;

        public BankAccountTests()
        {
            _bankAccount = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var accountNumber = "123456";
            var initialBalance = 1000.0;
            var accountHolderName = "John Doe";
            var accountType = "Savings";
            var dateOpened = DateTime.Now;

            // Act
            var account = new BankAccount(accountNumber, initialBalance, accountHolderName, accountType, dateOpened);

            // Assert
            Assert.Equal(accountNumber, account.AccountNumber);
            Assert.Equal(initialBalance, account.Balance);
            Assert.Equal(accountHolderName, account.AccountHolderName);
            Assert.Equal(accountType, account.AccountType);
            Assert.Equal(dateOpened, account.DateOpened);
        }

        [Fact]
        public void Credit_ShouldIncreaseBalance()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = 500.0;

            // Act
            account.Credit(amount);

            // Assert
            Assert.Equal(1500.0, account.Balance);
        }

        [Fact]
        public void Credit_ShouldThrowExceptionForNegativeAmount()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = -500.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Credit(amount));
        }

        [Fact]
        public void Debit_ShouldDecreaseBalance()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = 500.0;

            // Act
            account.Debit(amount);

            // Assert
            Assert.Equal(500.0, account.Balance);
        }

        [Fact]
        public void Debit_ShouldThrowExceptionForInsufficientBalance()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = 1500.0;

            // Act & Assert
            Assert.Throws<Exception>(() => account.Debit(amount));
        }

        [Fact]
        public void Debit_ShouldThrowExceptionForNegativeAmount()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = -500.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.Debit(amount));
        }

        [Fact]
        public void GetBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);

            // Act
            var balance = account.GetBalance();

            // Assert
            Assert.Equal(1000.0, balance);
        }

        [Fact]
        public void Transfer_ShouldTransferAmount()
        {
            // Arrange
            var fromAccount = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var toAccount = new BankAccount("654321", 500.0, "Jane Doe", "Savings", DateTime.Now);
            var amount = 200.0;

            // Act
            fromAccount.Transfer(toAccount, amount);

            // Assert
            Assert.Equal(800.0, fromAccount.Balance);
            Assert.Equal(700.0, toAccount.Balance);
        }

        [Fact]
        public void Transfer_ShouldThrowExceptionForInsufficientBalance()
        {
            // Arrange
            var fromAccount = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var toAccount = new BankAccount("654321", 500.0, "Jane Doe", "Savings", DateTime.Now);
            var amount = 1500.0;

            // Act & Assert
            Assert.Throws<Exception>(() => fromAccount.Transfer(toAccount, amount));
        }

        [Fact]
        public void Transfer_ShouldThrowExceptionForExceedingTransferLimit()
        {
            // Arrange
            var fromAccount = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var toAccount = new BankAccount("654321", 500.0, "Jane Doe", "Savings", DateTime.Now);
            var amount = 600.0;

            // Act & Assert
            Assert.Throws<Exception>(() => fromAccount.Transfer(toAccount, amount));
        }

        [Fact]
        public void Transfer_ShouldThrowExceptionForNegativeAmount()
        {
            // Arrange
            var fromAccount = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var toAccount = new BankAccount("654321", 500.0, "Jane Doe", "Savings", DateTime.Now);
            var amount = -200.0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => fromAccount.Transfer(toAccount, amount));
        }

        [Fact]
        public void Transfer_ShouldThrowExceptionForSameAccount()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var amount = 200.0;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => account.Transfer(account, amount));
        }

        [Fact]
        public void CalculateInterest_ShouldReturnCorrectInterest()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = 0.05;

            // Act
            var interest = account.CalculateInterest(interestRate);

            // Assert
            Assert.Equal(50.0, interest);
        }

        [Fact]
        public void CalculateInterest_ShouldReturnZeroForZeroBalance()
        {
            // Arrange
            var account = new BankAccount("123456", 0.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = 0.05;

            // Act
            var interest = account.CalculateInterest(interestRate);

            // Assert
            Assert.Equal(0.0, interest);
        }

        [Fact]
        public void CalculateInterest_ShouldThrowExceptionForNegativeInterestRate()
        {
            // Arrange
            var account = new BankAccount("123456", 1000.0, "John Doe", "Savings", DateTime.Now);
            var interestRate = -0.05;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => account.CalculateInterest(interestRate));
        }
    }

}
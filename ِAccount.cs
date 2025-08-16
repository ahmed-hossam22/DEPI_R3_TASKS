using System;
using System.Net;
using System.Transactions;
namespace Bank_Project
{
    public abstract class Account
    {
        private static int nextAccountNumber = 100001; 
        public int AccountNumber { get; private set; }
        public decimal Balance { get; protected set; }
        public DateTime DateOpened { get; private set; }
        public int CustomerId { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        protected Account(int customerId, decimal initialBalance = 0)
        {
            AccountNumber = nextAccountNumber++;
            CustomerId = customerId;
            Balance = initialBalance;
            DateOpened = DateTime.Now;
            Transactions = new List<Transaction>();

            if (initialBalance > 0)
            {
                RecordTransaction("Initial Deposit", initialBalance, TransactionType.Deposit);
            }
        }
        public virtual bool Deposit(decimal amount, string description = "Deposit")
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be greater than zero.");
                return false;
            }

            Balance += amount;
            RecordTransaction(description, amount, TransactionType.Deposit);
            Console.WriteLine($"Deposited: {amount:C}. New balance: {Balance:C}");
            return true;
        }

        public virtual bool Withdraw(decimal amount, string description = "Withdrawal")
        {
            if (amount <= 0)
            {
                Console.WriteLine("Withdrawal amount must be greater than zero.");
                return false;
            }

            if (!CanWithdraw(amount))
            {
                Console.WriteLine("Insufficient funds for withdrawal.");
                return false;
            }

            Balance -= amount;
            RecordTransaction(description, amount, TransactionType.Withdrawal);
            Console.WriteLine($"Withdrew: {amount:C}. New balance: {Balance:C}");
            return true;
        }
        public abstract bool CanWithdraw(decimal amount);
        public abstract decimal CalculateInterest();
        public abstract string GetAccountType();
        public bool TransferTo(Account destinationAccount, decimal amount, string description = "Transfer")
        {
            if (destinationAccount == null)
            {
                Console.WriteLine("Invalid destination account.");
                return false;
            }

            if (!CanWithdraw(amount))
            {
                Console.WriteLine("Insufficient funds for transfer.");
                return false;
            }

            Balance -= amount;
            RecordTransaction($"Transfer to Account #{destinationAccount.AccountNumber}: {description}",
                            amount, TransactionType.Transfer);

            destinationAccount.Balance += amount;
            destinationAccount.RecordTransaction($"Transfer from Account #{AccountNumber}: {description}",
                                               amount, TransactionType.Transfer);

            Console.WriteLine($"Transferred {amount:C} to Account #{destinationAccount.AccountNumber}");
            Console.WriteLine($"Your new balance: {Balance:C}");
            return true;
        }
        protected void RecordTransaction(string description, decimal amount, TransactionType type)
        {
            var transaction = new Transaction(AccountNumber, description, amount, type, Balance);
            Transactions.Add(transaction);
        }
        public void DisplayTransactionHistory()
        {
            Console.WriteLine($"\n=== Transaction History for Account #{AccountNumber} ===");
            if (Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
                return;
            }

            foreach (var transaction in Transactions)
            {
                Console.WriteLine(transaction);
            }
        }
        public virtual void DisplayAccountInfo()
        {
            Console.WriteLine($"\n=== {GetAccountType()} Information ===");
            Console.WriteLine($"Account Number: {AccountNumber}");
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Current Balance: {Balance:C}");
            Console.WriteLine($"Date Opened: {DateOpened:dd/MM/yyyy}");
            Console.WriteLine($"Number of Transactions: {Transactions.Count}");
        }

        public override string ToString()
        {
            return $"Account #{AccountNumber} ({GetAccountType()}): {Balance:C}";
        }
    }
}

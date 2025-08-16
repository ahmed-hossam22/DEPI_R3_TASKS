using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Project
{
    public class Transaction
    {
            public int TransactionId { get; private set; }
            public int AccountNumber { get; private set; }
            public string Description { get; private set; }
            public decimal Amount { get; private set; }
            public TransactionType Type { get; private set; }
            public DateTime Timestamp { get; private set; }
            public decimal BalanceAfter { get; private set; }

            private static int nextTransactionId = 1;

            public Transaction(int accountNumber, string description, decimal amount, TransactionType type, decimal balanceAfter)
            {
                TransactionId = nextTransactionId++;
                AccountNumber = accountNumber;
                Description = description;
                Amount = amount;
                Type = type;
                Timestamp = DateTime.Now;
                BalanceAfter = balanceAfter;
            }

            public override string ToString()
            {
                var sign = Type == TransactionType.Withdrawal || Type == TransactionType.Fee ? "-" : "+";
                return $"{Timestamp:dd/MM/yyyy HH:mm} | {Type} | {sign}{Amount:C} | {Description} | Balance: {BalanceAfter:C}";
            }
        }

        public enum TransactionType
        {
            Deposit,
            Withdrawal,
            Transfer,
            Interest,
            Fee
        }
    }


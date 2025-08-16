using System;
namespace Bank_Project
{
       public class CurrentAccount : Account
        {
            public decimal OverdraftLimit { get; set; }

            public CurrentAccount(int customerId, decimal overdraftLimit = 1000m, decimal initialBalance = 0)
                : base(customerId, initialBalance)
            {
                OverdraftLimit = overdraftLimit;
            }

            public override bool CanWithdraw(decimal amount)
            {
                return (Balance + OverdraftLimit) >= amount;
            }

            public override decimal CalculateInterest()
            {
       
                if (Balance < 0)
                {
                    return Balance * 0.02m;
                }
                return 0;
            }

            public decimal GetAvailableBalance()
            {
                return Balance + OverdraftLimit;
            }

            public bool IsOverdrawn()
            {
                return Balance < 0;
            }

            public void ApplyOverdraftFees()
            {
                if (IsOverdrawn())
                {
                    var fees = Math.Abs(CalculateInterest());
                    if (fees > 0)
                    {
                        Balance -= fees;
                        RecordTransaction("Overdraft Fee", fees, TransactionType.Fee);
                        Console.WriteLine($"Overdraft fee applied: {fees:C}. New balance: {Balance:C}");
                    }
                }
            }

            public override string GetAccountType()
            {
                return "Current Account";
            }

            public override void DisplayAccountInfo()
            {
                base.DisplayAccountInfo();
                Console.WriteLine($"Overdraft Limit: {OverdraftLimit:C}");
                Console.WriteLine($"Available Balance: {GetAvailableBalance():C}");
                Console.WriteLine($"Overdrawn: {(IsOverdrawn() ? "Yes" : "No")}");

                if (IsOverdrawn())
                {
                    Console.WriteLine($"Overdraft Amount: {Math.Abs(Balance):C}");
                }
            }
        }
    }

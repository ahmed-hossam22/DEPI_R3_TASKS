using System;
namespace Bank_Project
{
    public class Savings_account :Account
    {
        public decimal InterestRate { get; set; } 
        public DateTime LastInterestCalculation { get; private set; }

        public Savings_account(int customerId, decimal interestRate = 3.5m, decimal initialBalance = 0)
            : base(customerId, initialBalance)
        {
            InterestRate = interestRate;
            LastInterestCalculation = DateTime.Now;
        }

        public override bool CanWithdraw(decimal amount)
        {
            return Balance >= amount;
        }

        public override decimal CalculateInterest()
        {
            decimal monthlyRate = InterestRate / 100 / 12;
            return Balance * monthlyRate;
        }

        public void ApplyMonthlyInterest()
        {
            var interest = CalculateInterest();
            if (interest > 0)
            {
                Balance += interest;
                RecordTransaction($"Monthly Interest ({InterestRate}% annually)", interest, TransactionType.Interest);
                LastInterestCalculation = DateTime.Now;
                Console.WriteLine($"Interest applied: {interest:C}. New balance: {Balance:C}");
            }
        }

        public override string GetAccountType()
        {
            return "Savings Account";
        }

        public override void DisplayAccountInfo()
        {
            base.DisplayAccountInfo();
            Console.WriteLine($"Interest Rate: {InterestRate}% annually");
            Console.WriteLine($"Last Interest Calculation: {LastInterestCalculation:dd/MM/yyyy}");
            Console.WriteLine($"Estimated Monthly Interest: {CalculateInterest():C}");
        }
    }

}

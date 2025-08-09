using System;
using System.Collections.Generic;
namespace OOP_Task_2
{
    internal class SavingAccount : BankAccount
    {
        private decimal InterestRate;
        public SavingAccount(string name, string id, string num, int bal, string add, decimal interestRate)
            : base(name, id, num, bal, add)
        {
            InterestRate = interestRate;
        }

        public override decimal CalculateInterest()
        {
            return (Balance * InterestRate / 100 );
        }
        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
        }

    }
}

using System;
using System.Collections.Generic;
namespace OOP_Task_2
{
    internal class CurrentAccount : BankAccount
    {
        private decimal  OverdraftLimit;
        public CurrentAccount(string name, string id, string num, int bal, string add, decimal overdraftLimit)
            : base(name, id, num, bal, add)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override decimal CalculateInterest()
        {
            return 0;
        }
        public override void ShowAccountDetails()
        {
            base.ShowAccountDetails();
        }

    }
}

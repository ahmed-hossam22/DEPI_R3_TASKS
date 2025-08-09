namespace OOP_Task_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SavingAccount saving = new SavingAccount("Ahmed", "12345678912345", "01123456781", 5000, "Cairo", 5);

            CurrentAccount current = new CurrentAccount("Mohamed", "98765432198765", "01098765432", 8000, "Giza", 2000);

            List<BankAccount> accounts = new List<BankAccount> { saving, current };

            foreach (var account in accounts)
            {
                account.ShowAccountDetails();
                account.CalculateInterest();
            }
        }
    }
}

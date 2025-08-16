using System;
using System.Globalization;
using Bank_Project;
class Program
{
    static void Main()
    {
        Console.Write("Enter Bank Name: ");
        string bankName = Console.ReadLine();

        Console.Write("Enter Branch Code: ");
        string branchCode = Console.ReadLine();

        Bank bank = new Bank(bankName, branchCode);

        while (true)
        {
            Console.WriteLine("\n=== BANK SYSTEM MENU ===");
            Console.WriteLine("1. Add Customer");
            Console.WriteLine("2. Update Customer");
            Console.WriteLine("3. Remove Customer");
            Console.WriteLine("4. Search Customer");
            Console.WriteLine("5. Create Account");
            Console.WriteLine("6. Deposit");
            Console.WriteLine("7. Withdraw");
            Console.WriteLine("8. Transfer");
            Console.WriteLine("9. Show Customer Report");
            Console.WriteLine("10. Show Bank Report");
            Console.WriteLine("11. Show Transaction History");
            Console.WriteLine("12. Apply Interest to Savings");
            Console.WriteLine("0. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCustomer(bank);
                    break;
                case "2":
                    UpdateCustomer(bank);
                    break;
                case "3":
                    RemoveCustomer(bank);
                    break;
                case "4":
                    SearchCustomer(bank);
                    break;
                case "5":
                    CreateAccount(bank);
                    break;
                case "6":
                    Deposit(bank);
                    break;
                case "7":
                    Withdraw(bank);
                    break;
                case "8":
                    Transfer(bank);
                    break;
                case "9":
                    ShowCustomerReport(bank);
                    break;
                case "10":
                    bank.ShowReport();
                    break;
                case "11":
                    ShowTransactionHistory(bank);
                    break;
                case "12":
                    ApplyInterest(bank);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
    static Customer FindCustomerById(Bank bank)
    {
        Console.Write("Enter Customer ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            return Customer.SearchById(bank.Customers, id);
        }
        return null;
    }

    static void AddCustomer(Bank bank)
    {
        Console.Write("First Name: ");
        string first = Console.ReadLine();

        Console.Write("Last Name: ");
        string last = Console.ReadLine();

        Console.Write("National ID: ");
        string nid = Console.ReadLine();

        Console.Write("Date of Birth (dd/MM/yyyy): ");
        DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

        Customer c = new Customer(first, last, nid, dob);
        bank.AddCustomer(c);
        Console.WriteLine("Customer added successfully.");
    }

    static void UpdateCustomer(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null)
        {
            Console.Write("New First Name: ");
            string first = Console.ReadLine();
            Console.Write("New Last Name: ");
            string last = Console.ReadLine();
            Console.Write("New Date of Birth (dd/MM/yyyy): ");
            DateTime dob = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            c.UpdateCustomerDetails(first, last, dob);
            Console.WriteLine("Customer updated successfully.");
        }
        else Console.WriteLine("Customer not found.");
    }

    static void RemoveCustomer(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null)
        {
            if (c.CanBeRemoved())
            {
                bank.Customers.Remove(c);
                Console.WriteLine("Customer removed.");
            }
            else Console.WriteLine("Cannot remove customer with non-zero account balances.");
        }
        else Console.WriteLine("Customer not found.");
    }

    static void SearchCustomer(Bank bank)
    {
        Console.Write("Enter National ID: ");
        string nid = Console.ReadLine();
        var c = Customer.SearchByNationalId(bank.Customers, nid);
        if (c != null) c.DisplayInfo();
        else Console.WriteLine("Customer not found.");
    }

    static void CreateAccount(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null)
        {
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.Write("Choose account type: ");
            string t = Console.ReadLine();

            Console.Write("Initial Balance: ");
            decimal bal = decimal.Parse(Console.ReadLine());

            if (t == "1")
            {
                Console.Write("Interest Rate (%): ");
                decimal rate = decimal.Parse(Console.ReadLine());
                c.AddAccount(new Savings_account(c.CustomerId, rate, bal));
            }
            else if (t == "2")
            {
                Console.Write("Overdraft Limit: ");
                decimal limit = decimal.Parse(Console.ReadLine());
                c.AddAccount(new CurrentAccount(c.CustomerId, limit, bal));
            }
            Console.WriteLine("Account created.");
        }
        else Console.WriteLine("Customer not found.");
    }

    static Account FindAccount(Customer c)
    {
        Console.Write("Enter Account Number: ");
        int accNo = int.Parse(Console.ReadLine());
        foreach (var acc in c.Accounts)
        {
            if (acc.AccountNumber == accNo) return acc;
        }
        return null;
    }

    static void Deposit(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null)
        {
            var acc = FindAccount(c);
            if (acc != null)
            {
                Console.Write("Amount: ");
                decimal amt = decimal.Parse(Console.ReadLine());
                acc.Deposit(amt);
            }
        }
    }

    static void Withdraw(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null)
        {
            var acc = FindAccount(c);
            if (acc != null)
            {
                Console.Write("Amount: ");
                decimal amt = decimal.Parse(Console.ReadLine());
                acc.Withdraw(amt);
            }
        }
    }

    static void Transfer(Bank bank)
    {
        Console.WriteLine("Source Account:");
        var srcCust = FindCustomerById(bank);
        var srcAcc = FindAccount(srcCust);

        Console.WriteLine("Destination Account:");
        var destCust = FindCustomerById(bank);
        var destAcc = FindAccount(destCust);

        Console.Write("Amount: ");
        decimal amt = decimal.Parse(Console.ReadLine());

        srcAcc?.TransferTo(destAcc, amt);
    }

    static void ShowCustomerReport(Bank bank)
    {
        var c = FindCustomerById(bank);
        if (c != null) c.DisplayInfo();
    }

    static void ShowTransactionHistory(Bank bank)
    {
        var c = FindCustomerById(bank);
        var acc = FindAccount(c);
        acc?.DisplayTransactionHistory();
    }

    static void ApplyInterest(Bank bank)
    {
        foreach (var cust in bank.Customers)
        {
            foreach (var acc in cust.Accounts)
            {
                if (acc is Savings_account sa)
                {
                    sa.ApplyMonthlyInterest();
                }
            }
        }
    }
}

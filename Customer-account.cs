using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Bank_Project
{
    public class Customer
    {
        private static int nextId = 1;

        public int CustomerId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Account> Accounts { get; private set; }
        public DateTime DateCreated { get; private set; }

        public Customer(string firstName, string lastName, string nationalId, DateTime dateOfBirth)
        {
            CustomerId = nextId++;
            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
            DateOfBirth = dateOfBirth;
            Accounts = new List<Account>();
            DateCreated = DateTime.Now;
        }
        public string FullName => $"{FirstName} {LastName}";
        public void UpdateCustomerDetails(string firstName, string lastName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public void AddAccount(Account account)
        {
            if (account != null)
            {
                Accounts.Add(account);
            }
        }

        public bool RemoveAccount(Account account)
        {
            if (account != null && account.Balance == 0)
            {
                return Accounts.Remove(account);
            }
            return false;
        }
        public decimal GetTotalBalance()
        {
            decimal total = 0;
            foreach (var account in Accounts)
            {
                total += account.Balance;
            }
            return total;
        }
        public bool CanBeRemoved()
        {
            foreach (var account in Accounts)
            {
                if (account.Balance != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<Customer> SearchByName(List<Customer> customers, string firstName, string lastName)
        {
            var results = new List<Customer>();
            foreach (var customer in customers)
            {
                if (customer.FirstName.ToLower().Contains(firstName.ToLower()) &&
                    customer.LastName.ToLower().Contains(lastName.ToLower()))
                {
                    results.Add(customer);
                }
            }
            return results;
        }

        public static Customer SearchByNationalId(List<Customer> customers, string nationalId)
        {
            foreach (var customer in customers)
            {
                if (customer.NationalId == nationalId)
                {
                    return customer;
                }
            }
            return null;
        }

        public static Customer SearchById(List<Customer> customers, int customerId)
        {
            foreach (var customer in customers)
            {
                if (customer.CustomerId == customerId)
                {
                    return customer;
                }
            }
            return null;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Customer ID: {CustomerId}");
            Console.WriteLine($"Name: {FullName}");
            Console.WriteLine($"National ID: {NationalId}");
            Console.WriteLine($"Date of Birth: {DateOfBirth:dd/MM/yyyy}");
            Console.WriteLine($"Date Created: {DateCreated:dd/MM/yyyy}");
            Console.WriteLine($"Number of Accounts: {Accounts.Count}");
            Console.WriteLine($"Total Balance: {GetTotalBalance():C}");

            if (Accounts.Count > 0)
            {
                Console.WriteLine("Accounts:");
                foreach (var account in Accounts)
                {
                    Console.WriteLine($"  - Account #{account.AccountNumber}: {account.Balance:C}");
                }
            }
        }
        public override string ToString()
        {
            return $"ID: {CustomerId}, Name: {FullName}, NID: {NationalId}, DOB: {DateOfBirth:dd/MM/yyyy}";
        }
    }
}
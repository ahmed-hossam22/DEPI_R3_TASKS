using System;
using System.Collections.Generic;

namespace Bank_Project
{
    public class Bank
    {
        public string Name { get; set; }
        public string BranchCode { get; set; }
        public List<Customer> Customers { get; set; }

        public Bank(string name, string branchCode)
        {
            Name = name;
            BranchCode = branchCode;
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public void ShowCustomers()
        {
            foreach (var customer in Customers)
            {
                customer.DisplayInfo();
            }
        }

        public void ShowReport()
        {
            Console.WriteLine($"Bank: {Name} - Branch: {BranchCode}");
            Console.WriteLine($"Total Customers: {Customers.Count}");

            decimal total = 0;
            foreach (var customer in Customers)
            {
                total += customer.GetTotalBalance();
            }
            Console.WriteLine($"Total Balance: {total:C}");
        }
    }
}
using System;
namespace OOP_Task_2
{ 
      internal class BankAccount
    {
        private string _fullName;
        private String _nationalID;
        private String _phoneNumber;
        private string _address;
        private int _balance;

        public virtual decimal CalculateInterest() 
        {
            return 0; 
        }
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {

                if (string.IsNullOrEmpty(value))
                    Console.WriteLine("Invalid input!");
                else
                    _fullName = value;
            }
        }
        public String NationalID
        {
            get
            {
                return _nationalID;
            }
            set
            {
                if (value.Length == 14)
                    _nationalID = value;
                else
                    Console.WriteLine("Invalid input!");
            }
        }
        public String PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                if (value.StartsWith("01") && value.Length == 11)
                    _phoneNumber = value;
                else
                    Console.WriteLine("inavlid input!");
            }
        }
        public int Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                if (value >= 0)
                    _balance = value;
                else
                    Console.WriteLine("invalid input!");
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (string.IsNullOrEmpty(_address))
                    _address = value;
                else
                    Console.WriteLine("invalid input!");
            }
        }

        public BankAccount()
        {
            FullName = "Ahmed";
            NationalID = "12345678912345";
            PhoneNumber = "01123456781";
            Balance = 1000;
            Address = "Cairo";
        }
        public BankAccount(string name, String id, String num, int bal, string add)
        {
            FullName = name;
            NationalID = id;
            PhoneNumber = num;
            Balance = bal;
            Address = add;
        }
        public BankAccount(string name, String id, String num, string add)
        {
            FullName = name;
            NationalID = id;
            PhoneNumber = num;
            Balance = 0;
            Address = add;
        }
        public virtual void ShowAccountDetails()
        {
            Console.WriteLine($"Name        : {FullName}");
            Console.WriteLine($"National ID : {NationalID}");
            Console.WriteLine($"Phone       : {PhoneNumber}");
            Console.WriteLine($"Address     : {Address}");
            Console.WriteLine($"Balance     : {Balance}");
            Console.WriteLine("------------------------------\n");
        }

        public Boolean IsValidNationalID(String id)
        {
            if (id.Length == 14)
                return true;
            else
                return false;
        }
        public Boolean IsValidPhoneNumber(String ph)
        {
            if (ph.StartsWith("01") && ph.Length == 11)
                return true;
            else
                return false;
        }
    }
}

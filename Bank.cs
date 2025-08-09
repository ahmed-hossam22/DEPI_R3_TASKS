using System;
namespace OOP_TASK
{
    internal class Bank
    {
        //---------------------Start Of Create Fields (Data storage)-------------

        private const string BankCode = "BNK001";
        private readonly DateTime Createdate;
        private int _accountNumber;
        private string _fullName;
        private String _nationalID;
        private String _phoneNumber;
        private string _address;
        private int _balance;

        //---------------------Start Of Create properties (Data access)------------- 

        //property for FullName
        public string FullName {
            get
            { 
                 return _fullName;
            }
            set
            {

                if(string.IsNullOrEmpty(value))
                    Console.WriteLine("Invalid input!");
                else
                    _fullName = value;
            }  
                }

        // property for NationalID
        public String NationalID {
            get
            {
                return _nationalID;
            }
            set 
            { 
                if(value.Length ==14)
                    _nationalID = value;
                else
                    Console.WriteLine("Invalid input!");
            } 
        }
        //Propery for phoneNumber
        public String PhoneNumber {
            get
            { 
                return _phoneNumber;
            }
            set
            {
                if(value.StartsWith("01") && value.Length == 11 )
                    _phoneNumber = value;
                else
                    Console.WriteLine("inavlid input!");
            }
                }
        //Properity for Balance
        public int Balance {
            get
            {
                return _balance;
            }
            set
            {
                if(value >=0)
                    _balance = value;
                else
                    Console.WriteLine("invalid input!");
            }
                }
        //Properity for Address
        public string Address { 
            get
            {
                return _address;
            }
            set
            {
                if(string.IsNullOrEmpty(_address))
                    _address = value;
                else
                    Console.WriteLine("invalid input!");
            }
                }

        //--------------Start for Create constructors ----------------------

        //Default constructor
        public Bank ()
        {
            Createdate = DateTime.Now;
            FullName = "Ahmed";
            NationalID = "12345678912345";
            PhoneNumber = "01123456781";
            Balance = 1000;
            Address = "Cairo";
        }
        //Parameterized constructor
        public Bank (string name ,String id , String num , int bal , string add )
        {
            Createdate= DateTime.Now;
            FullName = name;
            NationalID = id;
            PhoneNumber = num;
            Balance = bal;
            Address = add;
        }
        //Overloaded constructor
        public Bank(string name, String id, String num, string add)
        {
            Createdate = DateTime.Now;
            FullName = name;
            NationalID = id;
            PhoneNumber = num;
            Balance = 0;
            Address = add;
        }

        //--------------Start for Create Methods ----------------------

        public void ShowAccountDetails()
        {
            Console.WriteLine($"Created     : {Createdate}");
            Console.WriteLine($"Name        : {FullName}");
            Console.WriteLine($"National ID : {NationalID}");
            Console.WriteLine($"Phone       : {PhoneNumber}");
            Console.WriteLine($"Address     : {Address}");
            Console.WriteLine($"Balance     : {Balance}");
            Console.WriteLine("------------------------------\n");
        }

        public Boolean IsValidNationalID(String id)
        { 
            if(id.Length==14)
                return true;
            else
                return false;
        }

        public Boolean IsValidPhoneNumber(String ph)
        { 
            if(ph.StartsWith("01") && ph.Length==11)
                return true;
            else 
                return false;
        }


    }
}

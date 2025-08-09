namespace OOP_TASK
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //First Object 
            Bank b1 = new Bank();
            b1.ShowAccountDetails();

            //Second Object
            Bank b2 = new Bank ("Ali", "23456789012345", "01123456789",50000,"Menofia" );
        
            b2.ShowAccountDetails();


            //Third Object
            Bank b3 = new Bank("Mohamed", "12345678901234", "01012345678", "Alex");
            b3.ShowAccountDetails();
        }
    }
}

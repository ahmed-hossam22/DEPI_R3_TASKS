using System;
namespace task1C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Simple Calculator";

            Console.WriteLine("Hello!");

            Console.Write("Input the first number: ");
            int num1 = Convert.ToInt32(Console.ReadLine());

            Console.Write("Input the second number: ");
            int num2 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("What do you want to do with those numbers?");
            Console.WriteLine("[A]dd");
            Console.WriteLine("[S]ubtract");
            Console.WriteLine("[M]ultiply");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid option");
                return;
            }

            char choice = char.ToUpper(input[0]);  // حول كل الحروف ل كابتل 

            switch (choice)
            {
                case 'A':
                    Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
                    break;
                case 'S':
                    Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
                    break;
                case 'M':
                    Console.WriteLine($"{num1} * {num2} = {num1 * num2}");
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}

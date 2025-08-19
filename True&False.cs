using System;
namespace OOP_Project_1
{
    internal class True_False : Question_Base
    {


        public bool Correct_Answer { get; set; }

        public True_False(string header, string body, int mark, bool correct) : base(header, body, mark) 
        { 
            Correct_Answer = correct;
        }
        public override bool Check_Correct_Answer(string User)
        {
            if (int.TryParse(User, out int user_Answer))
            {
                return (user_Answer == 1 && Correct_Answer) || (user_Answer == 2 && !Correct_Answer);
            }
            return false;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body} (Mark: {Mark})");
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }

        public override int GetAnswersCount()
        {
            return 1;
        }

        public override string GetCorrectAnswer()
        {
            return Correct_Answer ? "True" : "False";
        }
    }
}

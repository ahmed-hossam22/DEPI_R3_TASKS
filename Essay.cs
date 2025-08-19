using System;

namespace OOP_Project_1
{
    internal class Essay : Question_Base
    {
        public Essay(string header, string body, int mark) : base(header, body, mark)
        {
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body} (Mark: {Mark})");
            Console.WriteLine("Please write your answer:");
        }

        public override bool Check_Correct_Answer(string userAnswer)
        {
            return false;
        }

        public override string GetCorrectAnswer()
        {
            return "Manual checking required";
        }

        public override int GetAnswersCount()
        {
            return 1;
        }
    }
}

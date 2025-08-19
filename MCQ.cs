using System;
namespace OOP_Project_1
{
    internal class MCQ : Question_Base
    {
        public string Answer { get; set; }
        public int Correct_Answer_Index { get; set; }

        public MCQ(string header, string body, int mark, Answer[] answers, int correctAnswerIndex)
            : base(header, body, mark, answers)
        {
            Correct_Answer_Index = correctAnswerIndex;
        }


        public override void Display()
        {
            Console.WriteLine($"{Header}: {Body} (Mark: {Mark})");

            if (Answer != null && Answer.Length > 0)
            {
                for (int i = 0; i < Answer.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Answer[i]}");
                }
            }
            else
            {
                Console.WriteLine(" answers not found ");
            }
        }

        public override bool Check_Correct_Answer(string userAnswer)
        {
            if (int.TryParse(userAnswer, out int userChoice))
            {
                return userChoice == Correct_Answer_Index;
            }
            return false;
        }

        public override string GetCorrectAnswer()
        {
            if (Answer_list != null && Correct_Answer_Index >= 1 && Correct_Answer_Index <= Answer_list.Length)
            {
                return Answer_list[Correct_Answer_Index - 1].Answer_Text;
            }
            return "Invalid answer";
        }

        public override int GetAnswersCount()
        {
            return Answer != null ? Answer.Length : 0;
        }
    }
}

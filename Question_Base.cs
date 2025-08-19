using System;
namespace OOP_Project_1
{
    internal abstract class Question_Base
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }

        public Answer[] Answer_list { get; set; }

        public Answer Rigtht_Ans { get; set; }

        public Question_Base() { }

        public Question_Base (string header, string body , int mark , Answer[] answers, Answer rightAnswer)
        {
            Header = header;
            Body = body;
            Mark = mark;
            Answer_list = answers;
            Rigtht_Ans = rightAnswer;
        }

        public Question_Base(string header, string body, int mark)
        {
            Header = header;
            Body = body;
            Mark = mark;
        }

        public Question_Base(string header, string body, int mark, Answer[] answers) : this(header, body, mark)
        {
        }

        public abstract void Display();
        public abstract bool Check_Correct_Answer(string User);
        public abstract string GetCorrectAnswer();
        public abstract int GetAnswersCount();

    }
}

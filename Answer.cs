using System;
namespace OOP_Project_1
{
    internal class Answer
    {
        public int Answer_id { get; set; }
        public string Answer_Text{ get; set; }

        public Answer() 
        {

        }
        public Answer (int answer_id, string answer_Text)
        {
            Answer_id = answer_id;
            Answer_Text = answer_Text;
        }

        public override string ToString()
        {
            return $"{Answer_id}. {Answer_Text}";
        }
    }
}

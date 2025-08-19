using System;

namespace OOP_Project_1
{
    internal class Practical_Exam : Exam_Base
    {
        public Practical_Exam(string title, int time, Course course) : base(title, time, course)
        {
        }

        public override void Show_Exam_Info()
        {
            Console.WriteLine("=== Practical Exam Information ===");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Course: {Course.Title}");
            Console.WriteLine($"Time: {Time} minutes");
            Console.WriteLine($"Total Questions: {Questions.Count}");
            Console.WriteLine($"Total Marks: {GetTotalMarks()}/{Course.MaximumDegree}");
            Console.WriteLine($"Status: {(IsStarted ? "Started" : "Not Started")}");
        }

        public override Exam_Base CreateCopy(Course newCourse)
        {
            Practical_Exam copy = new Practical_Exam($"{Title} - Copy", Time, newCourse);

            foreach (Question_Base question in Questions)
            {
                copy.Questions.Add(question);
            }

            Console.WriteLine($"Practical exam copied for course: {newCourse.Title}");
            return copy;
        }
    }
}

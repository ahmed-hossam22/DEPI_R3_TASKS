using System;

namespace OOP_Project_1
{
    internal class ExamResult
    {
        public Student Student { get; set; }
        public Exam_Base Exam { get; set; }
        public int Score { get; set; }
        public bool IsPassed { get; set; }
        public DateTime ExamDate { get; set; }

        public ExamResult(Student student, Exam_Base exam, int score)
        {
            Student = student;
            Exam = exam;
            Score = score;
            ExamDate = DateTime.Now;

            // Calculate pass/fail (assuming 50% is passing)
            int passingScore = exam.Course.MaximumDegree / 2;
            IsPassed = score >= passingScore;
        }

        public void DisplayResult()
        {
            Console.WriteLine("=== Exam Result ===");
            Console.WriteLine($"Exam Title: {Exam.Title}");
            Console.WriteLine($"Student Name: {Student.Name}");
            Console.WriteLine($"Course Name: {Exam.Course.Title}");
            Console.WriteLine($"Score: {Score}/{Exam.Course.MaximumDegree}");
            Console.WriteLine($"Status: {(IsPassed ? "PASSED" : "FAILED")}");
            Console.WriteLine($"Date: {ExamDate.ToShortDateString()}");
        }

        public static void CompareStudents(ExamResult result1, ExamResult result2)
        {
            if (result1.Exam.Title != result2.Exam.Title)
            {
                Console.WriteLine("Cannot compare: Students took different exams");
                return;
            }

            Console.WriteLine("=== Student Comparison ===");
            Console.WriteLine($"Exam: {result1.Exam.Title}");
            Console.WriteLine($"Student 1: {result1.Student.Name} - Score: {result1.Score}");
            Console.WriteLine($"Student 2: {result2.Student.Name} - Score: {result2.Score}");

            if (result1.Score > result2.Score)
                Console.WriteLine($"{result1.Student.Name} performed better");
            else if (result2.Score > result1.Score)
                Console.WriteLine($"{result2.Student.Name} performed better");
            else
                Console.WriteLine("Both students have the same score");
        }
    }
}
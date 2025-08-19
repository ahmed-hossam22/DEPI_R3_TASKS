using System;
using System.Collections.Generic;

namespace OOP_Project_1
{
    internal abstract class Exam_Base
    {
        public int Time { get; set; }
        public string Title { get; set; }
        public Course Course { get; set; }
        public List<Question_Base> Questions { get; set; }
        public bool IsStarted { get; set; }

        public Exam_Base(string title, int time, Course course)
        {
            Title = title;
            Time = time;
            Course = course;
            Questions = new List<Question_Base>();
            IsStarted = false;
        }

        public virtual bool AddQuestion(Question_Base question)
        {
            if (IsStarted)
            {
                Console.WriteLine("Cannot modify exam after it has started!");
                return false;
            }

            int currentTotalMarks = GetTotalMarks();
            if (currentTotalMarks + question.Mark > Course.MaximumDegree)
            {
                Console.WriteLine($"Cannot add question. Total marks would exceed maximum degree ({Course.MaximumDegree})");
                return false;
            }

            Questions.Add(question);
            Console.WriteLine($"Question added successfully. Total marks: {currentTotalMarks + question.Mark}");
            return true;
        }

        public virtual bool RemoveQuestion(int index)
        {
            if (IsStarted)
            {
                Console.WriteLine("Cannot modify exam after it has started!");
                return false;
            }

            if (index >= 0 && index < Questions.Count)
            {
                Questions.RemoveAt(index);
                Console.WriteLine("Question removed successfully");
                return true;
            }

            Console.WriteLine("Invalid question index");
            return false;
        }

        public int GetTotalMarks()
        {
            int total = 0;
            foreach (Question_Base question in Questions)
            {
                total += question.Mark;
            }
            return total;
        }

        public void StartExam()
        {
            IsStarted = true;
            Console.WriteLine($"Exam '{Title}' has started!");
        }

        public abstract void Show_Exam_Info();
        public abstract Exam_Base CreateCopy(Course newCourse);
    }
}
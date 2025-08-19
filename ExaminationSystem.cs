using System;
using System.Collections.Generic;

namespace OOP_Project_1
{
    internal class ExaminationSystem
    {
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
        public List<Exam_Base> Exams { get; set; }
        public List<ExamResult> Results { get; set; }

        public ExaminationSystem()
        {
            Courses = new List<Course>();
            Students = new List<Student>();
            Instructors = new List<Instructor>();
            Exams = new List<Exam_Base>();
            Results = new List<ExamResult>();
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            Console.WriteLine($"Course '{course.Title}' added successfully");
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
            Console.WriteLine($"Student '{student.Name}' added successfully");
        }

        public void AddInstructor(Instructor instructor)
        {
            Instructors.Add(instructor);
            Console.WriteLine($"Instructor '{instructor.Name}' added successfully");
        }

        public void AddExam(Exam_Base exam)
        {
            Exams.Add(exam);
            Console.WriteLine($"Exam '{exam.Title}' added successfully");
        }

        public ExamResult TakeExam(Student student, Exam_Base exam)
        {
            if (!exam.IsStarted)
            {
                exam.StartExam();
            }

            Console.WriteLine($"\n{student.Name} is taking exam: {exam.Title}");
            Console.WriteLine($"Time allowed: {exam.Time} minutes");
            Console.WriteLine("==========================================");

            int totalScore = 0;

            for (int i = 0; i < exam.Questions.Count; i++)
            {
                Question_Base question = exam.Questions[i];
                Console.WriteLine($"\nQuestion {i + 1}:");
                question.Display();

                Console.Write("Your answer: ");
                string userAnswer = Console.ReadLine();

                if (question.Check_Correct_Answer(userAnswer))
                {
                    totalScore += question.Mark;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine($"Wrong! Correct answer: {question.GetCorrectAnswer()}");
                }
            }

            ExamResult result = new ExamResult(student, exam, totalScore);
            Results.Add(result);

            Console.WriteLine("\n==========================================");
            result.DisplayResult();

            return result;
        }

        public void DisplayAllResults()
        {
            Console.WriteLine("\n=== All Exam Results ===");
            foreach (ExamResult result in Results)
            {
                result.DisplayResult();
                Console.WriteLine("------------------");
            }
        }

        public Course FindCourseById(int id)
        {
            foreach (Course course in Courses)
            {
                if (course.Id == id)
                    return course;
            }
            return null;
        }

        public Student FindStudentById(int id)
        {
            foreach (Student student in Students)
            {
                if (student.Id == id)
                    return student;
            }
            return null;
        }

        public Instructor FindInstructorById(int id)
        {
            foreach (Instructor instructor in Instructors)
            {
                if (instructor.Id == id)
                    return instructor;
            }
            return null;
        }

        public Exam_Base FindExamByTitle(string title)
        {
            foreach (Exam_Base exam in Exams)
            {
                if (exam.Title == title)
                    return exam;
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;

namespace OOP_Project_1
{
    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Course> EnrolledCourses { get; set; }

        public Student()
        {
            EnrolledCourses = new List<Course>();
        }

        public Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            EnrolledCourses = new List<Course>();
        }

        public void EnrollInCourse(Course course)
        {
            if (!EnrolledCourses.Contains(course))
            {
                EnrolledCourses.Add(course);
                Console.WriteLine($"Student {Name} enrolled in {course.Title}");
            }
            else
            {
                Console.WriteLine($"Student {Name} is already enrolled in {course.Title}");
            }
        }

        public override string ToString()
        {
            return $"Student: {Name} (ID: {Id}, Email: {Email})";
        }
    }
}

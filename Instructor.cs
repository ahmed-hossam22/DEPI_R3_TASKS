using System;
using System.Collections.Generic;

namespace OOP_Project_1
{
    internal class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public List<Course> TeachingCourses { get; set; }

        public Instructor()
        {
            TeachingCourses = new List<Course>();
        }

        public Instructor(int id, string name, string specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
            TeachingCourses = new List<Course>();
        }

        public void AssignToCourse(Course course)
        {
            if (!TeachingCourses.Contains(course))
            {
                TeachingCourses.Add(course);
                Console.WriteLine($"Instructor {Name} assigned to teach {course.Title}");
            }
            else
            {
                Console.WriteLine($"Instructor {Name} is already teaching {course.Title}");
            }
        }

        public override string ToString()
        {
            return $"Instructor: {Name} (ID: {Id}, Specialization: {Specialization})";
        }
    }
}

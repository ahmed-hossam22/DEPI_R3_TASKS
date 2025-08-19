using System;

namespace OOP_Project_1
{
    internal class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaximumDegree { get; set; }

        public Course()
        {
        }

        public Course(int id, string title, string description, int maximumDegree)
        {
            Id = id;
            Title = title;
            Description = description;
            MaximumDegree = maximumDegree;
        }

        public override string ToString()
        {
            return $"Course: {Title} (ID: {Id}) - Max Degree: {MaximumDegree}";
        }
    }
}

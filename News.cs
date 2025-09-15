using EF_Project.Models;
using System;

namespace EF_Project.Models
{
    public class News
    {
        public int Id { get; set; } // Primary key
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public string? Brief { get; set; }
        public DateTime Date { get; set; }
        public string? Time { get; set; }

        // العلاقة مع Author
        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        // العلاقة مع Category
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}


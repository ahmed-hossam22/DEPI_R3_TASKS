using EF_Project.Models;
using System;
using System.Collections.Generic;

namespace EF_Project.Models
{
    public class Author
    {
        public int Id { get; set; } // Primary key
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } 
        public DateTime JoinDate { get; set; }

        // Navigation property 
        public ICollection<News> News { get; set; } = new List<News>();
    }
}

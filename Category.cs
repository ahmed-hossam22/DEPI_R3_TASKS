using System;
using EF_Project.Models;
using System.Collections.Generic;

namespace EF_Project.Models
{
    public class Category
    {
        public int Id { get; set; } // Primary key
        public string? Name { get; set; } 
        public string? Description { get; set; }

        // Navigation property 
        public ICollection<News> News { get; set; } = new List<News>();
    }
}


using Microsoft.EntityFrameworkCore;

namespace EF_Project.Models
{
    public class ProjectContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-R3OL02D;Database=EF_Simple_Project;Trusted_Connection=True;Trust Server Certificate=True;");
        }
    }
}


using Microsoft.EntityFrameworkCore;
using session2.Models;

namespace session2.Data
{
    public class ApplicationDbContext : DbContext
    {
       public DbSet<Student> students { get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LENOVO;database=MVC;trusted_connection=true;TrustServerCertificate=true");
        }
    }
}

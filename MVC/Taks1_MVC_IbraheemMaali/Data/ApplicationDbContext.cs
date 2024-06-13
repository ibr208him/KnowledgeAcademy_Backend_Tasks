using Microsoft.EntityFrameworkCore;
using WebApplication1._2.Models;

namespace WebApplication1._2.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Employee> employees { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LENOVO;database=MVC;trusted_connection=true;TrustServerCertificate=true");
        }

    }
}

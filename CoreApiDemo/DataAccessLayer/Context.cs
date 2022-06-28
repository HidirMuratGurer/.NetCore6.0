using Microsoft.EntityFrameworkCore;

namespace CoreApiDemo.DataAccessLayer
{
    public class Context :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server= ADONIS46\\MSSQLSERVER2;database=CoreApiDemo; integrated security=true;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}

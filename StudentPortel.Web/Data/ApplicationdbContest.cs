
using Microsoft.EntityFrameworkCore;
using StudentPortel.Web.Models.Entities;
namespace StudentPortel.Web.Data
{
    public class ApplicationdbContest : DbContext
    {
        public ApplicationdbContest(DbContextOptions<ApplicationdbContest> options): base(options)
        {
            
        }
        public DbSet<Student> Students  { get; set; }
        public DbSet<Employee> Employees  { get; set; }
        public DbSet<Addcomplaint> Addcomplaint { get; set; }
        public DbSet<LogIN> LogIN { get; set; }
    }
}

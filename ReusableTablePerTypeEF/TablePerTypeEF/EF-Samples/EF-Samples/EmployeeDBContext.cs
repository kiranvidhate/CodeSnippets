using System.Data.Entity;

namespace EF_Samples
{
    public class EmployeeDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}

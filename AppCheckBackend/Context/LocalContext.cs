using AppCheckBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCheckBackend.Context
{
    public class LocalContext : DbContext
    {
        public LocalContext(DbContextOptions<LocalContext> options) : base(options) { }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}

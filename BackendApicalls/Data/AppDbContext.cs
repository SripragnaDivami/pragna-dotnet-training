using BackendApicalls.Models.entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApicalls.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}

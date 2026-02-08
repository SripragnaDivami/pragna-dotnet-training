
using capstone_policy_management.Entities;
using Microsoft.EntityFrameworkCore;

namespace capstone_policy_management.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PolicyEnrollment> PolicyEnrollments { get; set; }
}
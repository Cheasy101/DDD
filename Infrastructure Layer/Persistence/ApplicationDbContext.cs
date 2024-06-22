using DDD.APP.Domain_Layer.Entities;
using DDD.APP.Infrastructure_Layer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DDD.APP.Infrastructure_Layer.Persistence;

 public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
    }

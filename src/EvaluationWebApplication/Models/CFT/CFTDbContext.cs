using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationWebApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EvaluationWebApplication.Models.CFT
{
    public class CFTDbContext : IdentityDbContext<ApplicationUser>
    {
        public CFTDbContext() : base()
        { }
        public CFTDbContext(DbContextOptions<CFTDbContext> options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        //public DbSet<ServiceClass> Services { get; set; }
        public DbSet<Contracts> Contracts { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<ServiceClass> Service { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Test\test;Database=EvaluationWebApplicationCoreDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

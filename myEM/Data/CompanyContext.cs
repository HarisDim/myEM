using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myEM.Models;

namespace myEM.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext (DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Skill>().ToTable("Skill");
        }
    }
}

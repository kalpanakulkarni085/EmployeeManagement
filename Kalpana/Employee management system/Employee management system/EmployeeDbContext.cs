using Employee_management_system.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Employee_management_system
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designation { get; set; }

    }
}

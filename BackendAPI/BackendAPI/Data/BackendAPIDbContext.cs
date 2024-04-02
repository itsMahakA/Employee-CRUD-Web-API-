using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class BackendAPIDbContext : DbContext
    {
        public BackendAPIDbContext(DbContextOptions options) : base (options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}

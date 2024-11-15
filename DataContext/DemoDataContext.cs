using DemoAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.DataContext
{
    public class DemoDataContext : IdentityDbContext
    {
        public DemoDataContext(DbContextOptions<DemoDataContext> options) : base(options)
        {            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

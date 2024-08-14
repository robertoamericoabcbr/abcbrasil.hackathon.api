using ABCBrasil.Hackthon.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCBrasil.Hackthon.Api.Infra.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Shared.Entity;
namespace Customers.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers => Set<Customer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Customer entity
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Firstname).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Lastname).IsRequired().HasMaxLength(255);
                entity.OwnsOne(e => e.Email, email =>
                {
                    email.Property(e => e.Value).IsRequired().HasMaxLength(255).HasColumnName("Email");
                });
            });
        }
    }
}

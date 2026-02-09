using Microsoft.EntityFrameworkCore;
using Products.Entity;

namespace Products.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure Customer entity
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Quantity).IsRequired();
                entity.OwnsOne(e => e.Email, email =>
                {
                    email.Property(e => e.Value).IsRequired().HasMaxLength(255).HasColumnName("Email");
                });
            });
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Products.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Quantity).IsRequired();
                entity.OwnsOne(e => e.Price, price =>
                {
                    price.Property(p => p.Amount)
                        .IsRequired()
                        .HasColumnName("PriceAmount");

                    price.Property(p => p.Currency)
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnName("PriceCurrency");
                })
                .Navigation(p => p.Price)
                .IsRequired();
            });
        }
    }
}

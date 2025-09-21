using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base (options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id);
                entity.Property(e=>e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e=>e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e=>e.Category).HasMaxLength(50);
                entity.Property(e => e.Brand).HasMaxLength(50);
                entity.Property(e => e.CreatedAt);//.HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt);// HasDefaultValueSql("GETUTCDATE()");
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.Category);
            });
                
        }
    }
}

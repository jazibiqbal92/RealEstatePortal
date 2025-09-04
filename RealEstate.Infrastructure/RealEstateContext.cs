using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure
{
    public class RealEstateContext : DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Using Composite key for Favorites (so that the pair stays unique)
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.PropertyId });

            // Relationships
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PropertyId);

            // Property Configuration
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(p => p.BedroomCount)
                    .HasDefaultValue(0);

                entity.Property(p => p.BathroomCount)
                    .HasDefaultValue(0);

                entity.Property(p => p.CarspotCount)
                    .HasDefaultValue(0);

                entity.Property(p => p.Description)
                    .HasMaxLength(1000);

                entity.Property(p => p.ImageUrl)
                    .HasMaxLength(500);

                entity.Property(p => p.ListingType)
                 .HasConversion<string>();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

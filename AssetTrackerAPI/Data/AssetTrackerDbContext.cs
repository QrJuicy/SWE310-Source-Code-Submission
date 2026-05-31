using Microsoft.EntityFrameworkCore;
using AssetTrackerAPI.Models.Domain;

namespace AssetTrackerAPI.Data
{
    public class AssetTrackerDbContext : DbContext
    {
        public AssetTrackerDbContext(DbContextOptions<AssetTrackerDbContext> options)
            : base(options)
        {
        }

        public DbSet<AssetCategory> AssetCategory { get; set; }

        public DbSet<Asset> Asset { get; set; }

        public DbSet<AssetVersion> AssetVersion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasOne(a => a.AssetCategory)
                .WithMany(c => c.Assets)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetVersion>()
                .HasOne(v => v.Asset)
                .WithMany(a => a.AssetVersions)
                .HasForeignKey(v => v.AssetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

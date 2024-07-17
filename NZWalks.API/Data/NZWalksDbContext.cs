using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options)
        {

        }

        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "NR",
                    Name = "Northern Region",
                    RegionImageUrl = "https://example.com/northern-region.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "SR",
                    Name = "Southern Region",
                    RegionImageUrl = "https://example.com/southern-region.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "ER",
                    Name = "Eastern Region",
                    RegionImageUrl = "https://example.com/eastern-region.jpg"
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Code = "WR",
                    Name = "Western Region",
                    RegionImageUrl = "https://example.com/western-region.jpg"
                }
            );
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}

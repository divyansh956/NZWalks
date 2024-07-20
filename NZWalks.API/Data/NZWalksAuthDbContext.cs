using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "0e383b1c-9501-48e8-b881-cd91ca943d37";
            var writerRoleId = "f3b3b1c-9501-48e8-b881-cd91ca943d37";

            var roles = new List<IdentityRole> {
                new IdentityRole {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

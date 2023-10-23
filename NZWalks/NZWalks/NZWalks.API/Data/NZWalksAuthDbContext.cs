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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "fd4f2f2b-6546-4deb-9531-67cb0f9d679c";
            var writerRole = "fd4f2f2b-6546-4deb-9531-67cb0f9d679d";
            var roles = new List<IdentityRole>() { 
                new IdentityRole() { 
                    Id = readerId, 
                    ConcurrencyStamp = readerId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole() {
                    Id = writerRole,
                    ConcurrencyStamp = writerRole,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

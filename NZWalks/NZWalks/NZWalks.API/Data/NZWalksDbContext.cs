using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions DbContextOptions) : base(DbContextOptions)
        {

        }

        // When exeucting migration command, these sets will be created 
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        // Seed data

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var difficulties = new List<Difficulty>() { 
                new Difficulty() { 
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af21"),
                    Name = "Easy"
                },
                new Difficulty() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af22"),
                    Name = "Medium"
                },
                new Difficulty() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af23"),
                    Name = "Hard"
                }
            };

            var regions = new List<Region>()
            {
                new Region() { 
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af24"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af25"),
                    Name = "Wellington",
                    Code = "WLG",
                    RegionImageUrl = null
                },
                new Region() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af26"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "akl.jpg"
                },
                new Region() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af27"),
                    Name = "Nelson",
                    Code = "NSL",
                    RegionImageUrl = "nelson.jpg"
                },
                new Region() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af28"),
                    Name = "Kandahar",
                    Code = "KDR",
                    RegionImageUrl = "kdr.jpg"
                },
                new Region() {
                    Id = Guid.Parse("f10e38c5-32a2-4f13-b115-71214fe6af29"),
                    Name = "Kabul",
                    Code = "KBL",
                    RegionImageUrl = "kbl.jpg"
                }
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
}
